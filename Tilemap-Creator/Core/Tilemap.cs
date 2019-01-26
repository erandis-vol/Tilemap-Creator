using System;
using System.Drawing;
using System.IO;

namespace TMC.Core
{
    public class Tilemap
    {
        TilemapEntry[] tiles;
        int width, height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tilemap"/> class with the specified size.
        /// </summary>
        /// <param name="width">The width in tiles.</param>
        /// <param name="height">The height in tiles.</param>
        public Tilemap(int width, int height)
        {
            this.width = width;
            this.height = height;

            tiles = new TilemapEntry[width * height];
            for (int i = 0; i < width * height; i++)
                tiles[i] = new TilemapEntry();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tilemap"/> class from the specified file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <param name="width"></param>
        public Tilemap(string filename, TilemapFormat format, int width)
        {
            const int IndexMask = 0x3FF;
            const int FlipXMask = 0x400;
            const int FlipYMask = 0x800;
            const int PaletteMask = 0xF;

            using (var fs = File.OpenRead(filename))
            using (var br = new BinaryReader(fs))
            {
                // --------------------------------
                var tileCount = (int)br.BaseStream.Length / (format == TilemapFormat.RotationScaling ? 1 : 2);

                // --------------------------------
                this.width = width;
                this.height = tileCount / width;

                // --------------------------------
                tiles = new TilemapEntry[width * height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (format == TilemapFormat.Text4)
                        {
                            var u = br.ReadUInt16();
                            tiles[x + y * width] = new TilemapEntry(
                                (short)(u & IndexMask),
                                (byte)((u >> 12) & PaletteMask),
                                (u & FlipXMask) == FlipXMask,
                                (u & FlipYMask) == FlipYMask
                            );
                        }
                        else if (format == TilemapFormat.Text8)
                        {
                            var u = br.ReadUInt16();
                            tiles[x + y * width] = new TilemapEntry(
                                (short)(u & IndexMask),
                                (u & FlipXMask) == FlipXMask,
                                (u & FlipYMask) == FlipYMask
                            );
                        }
                        else // RotationScaling
                        {
                            tiles[x + y * width] = new TilemapEntry(br.ReadByte());
                        }
                    }
                }
            }
        }

        #region Methods

        public ref TilemapEntry this[int index] => ref tiles[index];

        public ref TilemapEntry this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= width)
                    throw new ArgumentOutOfRangeException(nameof(x));

                if (y < 0 || y >= height)
                    throw new ArgumentOutOfRangeException(nameof(y));

                return ref tiles[x + y * width];
            }
        }

        public ref TilemapEntry this[Point p] => ref this[p.X, p.Y];

        /// <summary>
        /// Resizes the tilemap.
        /// </summary>
        /// <param name="newWidth">The new width in tiles.</param>
        /// <param name="newHeight">The new height in tiles.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="newWidth"/> or <paramref name="newHeight"/> is negative.
        /// </exception>
        public void Resize(int newWidth, int newHeight)
        {
            if (newWidth < 0 || newHeight < 0)
                throw new ArgumentOutOfRangeException(newWidth < 0 ? nameof(newWidth) : nameof(newHeight));

            var temp = new TilemapEntry[newWidth * newHeight];
            var copyWidth = Math.Min(width, newWidth);
            var copyHeight = Math.Min(height, newHeight);

            for (int y = 0; y < copyHeight; y++)
            {
                for (int x = 0; x < copyWidth; x++)
                {
                    temp[x + y * newWidth] = tiles[x + y * width];
                }
            }

            tiles = temp;
            width = newWidth;
            height = newHeight;
        }

        /// <summary>
        /// Save this <see cref="Tilemap"/> in raw GBA format.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <param name="extraBytes"></param>
        public void Save(TilemapFileOptions tilemapFileOptions)
        {
            // http://problemkaputt.de/gbatek.htm#lcdvrambgscreendataformatbgmap
            var filename = tilemapFileOptions.FileName;
            var format   = tilemapFileOptions.Format;
            var padding  = tilemapFileOptions.Padding;

            using (var fs = File.Create(filename))
            {
                // TODO: SaveC, SaveNSCR
                SaveGBA(fs, format, padding);
            }
        }

        private void SaveGBA(Stream stream, TilemapFormat format, int padding)
        {
            using (var writer = new BinaryWriter(stream))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        ref TilemapEntry tile = ref this[x, y];

                        if (format == TilemapFormat.Text4)
                        {
                            writer.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0) |
                                (tile.Palette << 12)
                            ));
                        }
                        else if (format == TilemapFormat.Text8)
                        {
                            writer.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0)
                            ));
                        }
                        else // TilemapFormat.RotationScaling
                        {
                            writer.Write((byte)tile.Index);
                        }
                    }
                }

                for (int i = 0; i < padding; i++)
                {
                    writer.Write(byte.MinValue);
                }
            }
        }

        private void SaveC(string filename, TilemapFormat format, int extraBytes = 0)
        {
            var variableName = Path.GetFileNameWithoutExtension(filename).ToLower().Replace(' ', '_');

            using (var sw = File.CreateText(filename))
            {
                sw.Write($"unsigned char {variableName}[] = ");
                sw.Write("{");

                // --------------------------------
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var tile = this[x, y];
                        if (format == TilemapFormat.Text4)
                            sw.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0) |
                                (tile.Palette << 12)
                            ));
                        else if (format == TilemapFormat.Text8)
                            sw.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0)
                            ));
                        else
                            sw.Write((byte)tile.Index);
                    }
                }

                // --------------------------------
                for (int i = 0; i < extraBytes; i++)
                    sw.Write(byte.MinValue);
            }
        }

        private void SaveNSCR(Stream stream, TilemapFormat format, int extraBytes)
        {
            const uint MagicNscr = 0x4E534352u;
            const uint MagicScrn = 0x4E524353u;

            // http://llref.emutalk.net/docs/?file=xml/nscr.xml#xml-doc

            if (format == TilemapFormat.RotationScaling)
                throw new NotSupportedException();

            int dataSize = width * height;
            if (format != TilemapFormat.RotationScaling)
                dataSize *= 2;

            using (var writer = new BinaryWriter(stream))
            {
                // Nitro header section
                writer.Write(MagicNscr);                // NSCR
                writer.Write(0x0100FEFF);               // version, byte order
                writer.Write(0);                        // file size
                writer.Write(16);                       // header size, always 16
                writer.Write(1);                        // sections, always 1 for this format

                // Screen section
                writer.Write(MagicScrn);                // SCRN
                writer.Write(20 + dataSize);            // section size
                writer.Write((ushort)(width << 3));     // width in pixels
                writer.Write((ushort)(height << 3));    // height in pixels -- max size = 0x1FFF by 0x1FFF
                writer.Write((ushort)0);                // internal screen size
                writer.Write((ushort)0);                // bg type
                writer.Write(dataSize);                 // data size

                // TODO: To finish this, we need to determine the screen size and BG type

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var tile = this[x, y];
                        if (format == TilemapFormat.Text4)
                        {
                            writer.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0) |
                                (tile.Palette << 12)
                            ));
                        }
                        else if (format == TilemapFormat.Text8)
                        {
                            writer.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0)
                            ));
                        }
                        else
                        {
                            writer.Write((byte)tile.Index);
                        }
                    }
                }

                // Adjust file size in header
                writer.BaseStream.Position = 8L;
                writer.Write((int)writer.BaseStream.Length);
            }
        }

        public void ShiftUp()
        {
            for (int y = 1; y < height; y++)
                for (int x = 0; x < width; x++)
                    this[x, y - 1] = this[x, y];

            for (int x = 0; x < width; x++)
                this[x, height - 1] = default(TilemapEntry);
        }

        public void ShiftDown()
        {
            for (int y = height - 2; y >= 0; y--)
                for (int x = 0; x < width; x++)
                    this[x, y + 1] = this[x, y];

            for (int x = 0; x < width; x++)
                this[x, 0] = default(TilemapEntry);
        }

        public void ShiftLeft()
        {
            for (int y = 0; y < height; y++)
                for (int x = 1; x < width; x++)
                    this[x - 1, y] = this[x, y];

            for (int y = 0; y < height; y++)
                this[width - 1, y] = default(TilemapEntry);
        }

        public void ShiftRight()
        {
            for (int y = 0; y < height; y++)
                for (int x = width - 2; x >= 0; x--)
                    this[x + 1, y] = this[x, y];

            for (int y = 0; y < height; y++)
                this[0, y] = default(TilemapEntry);
        }

        public void Clear() => Fill(default(TilemapEntry));

        public void Fill(short index) => Fill(new TilemapEntry { Index = index });

        public void Fill(TilemapEntry entry)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = entry;
            }
        }

        /// <summary>
        /// Draws the tilemap on an image.
        /// </summary>
        /// <param name="fb">The image to draw the tilemap on.</param>
        /// <param name="tileset">The tileset.</param>
        public void Draw(DirectBitmap fb, Tileset tileset)
        {
            Draw(fb, tileset, 0, 0, width, height);
        }

        /// <summary>
        /// Draws a specified part of the tilemap on an image.
        /// </summary>
        /// <param name="fb">The image to draw the tilemap on.</param>
        /// <param name="tileset">The tileset.</param>
        public void Draw(DirectBitmap fb, Tileset tileset, int srcX, int srcY, int srcWidth, int srcHeight)
        {
            if (fb == null || tileset == null)
                throw new ArgumentNullException(fb == null ? nameof(fb) : nameof(tileset));

            for (int y = srcY; y < srcY + srcHeight; y++)
            {
                for (int x = srcX; x < srcX + srcWidth; x++)
                {
                    ref var tile = ref this[x, y];
                    ref var sprite = ref tileset[tile.Index];

                    for (int j = 0; j < 8; j++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            var dstX = x * 8 + (tile.FlipX ? 7 - k : k);
                            var dstY = y * 8 + (tile.FlipY ? 7 - j : j);
                            fb.SetPixel(dstX, dstY, tileset.Palette[sprite[k, j]]);
                        }
                    }
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width, in tiles, of the <see cref="Tilemap"/>.
        /// </summary>
        public int Width => width;

        /// <summary>
        /// Gets the height, in tiles, of the <see cref="Tilemap"/>.
        /// </summary>
        public int Height => height;

        #endregion
    }
}
