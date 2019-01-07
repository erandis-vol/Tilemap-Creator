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
        public void Save(string filename, TilemapFormat format, int extraBytes = 0)
        {
            // http://problemkaputt.de/gbatek.htm#lcdvrambgscreendataformatbgmap

            using (var fs = File.Create(filename))
            using (var bw = new BinaryWriter(fs))
            {
                // --------------------------------
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var tile = this[x, y];

                        if (format == TilemapFormat.Text4)
                            bw.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0) |
                                (tile.Palette << 12)
                            ));
                        else if (format == TilemapFormat.Text8)
                            bw.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0)
                            ));
                        else
                            bw.Write((byte)tile.Index);
                    }
                }

                // --------------------------------
                for (int i = 0; i < extraBytes; i++)
                    bw.Write(byte.MinValue);
            }
        }

        /// <summary>
        /// Save this <see cref="Tilemap"/> as C array.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <param name="extraBytes"></param>
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

        private void SaveNSCR(string filename, TilemapFormat bitDepth, int extraBytes)
        {
            // http://llref.emutalk.net/docs/?file=xml/nscr.xml#xml-doc
            // there are actually a lot of options for this format

            if (bitDepth == TilemapFormat.RotationScaling)
                throw new NotSupportedException();

            using (var fs = File.Create(filename))
            using (var bw = new BinaryWriter(fs))
            {
                // NSCR header
                bw.Write(0x4E534352);
                bw.Write(0x0100FEFF);
                bw.Write(0);
                bw.Write(16);
                bw.Write(1);

                // NRCS section
                bw.Write(0x4E524353);
                bw.Write(20 + width * height * 2);
                bw.Write((ushort)(width << 3));     // width in pixels
                bw.Write((ushort)(height << 3));    // height in pixels -- max size = 0x1FFF by 0x1FFF
                bw.Write((ushort)0);
                bw.Write((ushort)0);
                bw.Write(width * height * 2);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var tile = this[x, y];
                        if (bitDepth == TilemapFormat.Text4)
                            bw.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0) |
                                (tile.Palette << 12)
                            ));
                        else //if (format == TilemapFormat.Text8)
                            bw.Write((ushort)(
                                (tile.Index & 0x3FF) |
                                (tile.FlipX ? 0x400 : 0) |
                                (tile.FlipY ? 0x800 : 0)
                            ));
                    }
                }

                // Adjust file size in header
                bw.BaseStream.Position = 8L;
                bw.Write(bw.BaseStream.Length);
            }
        }

        public void ShiftUp()
        {
            for (int y = 1; y < height; y++)
                for (int x = 0; x < width; x++)
                    this[x, y - 1] = this[x, y];

            for (int x = 0; x < width; x++)
                this[x, height - 1] = new TilemapEntry();
        }

        public void ShiftDown()
        {
            for (int y = height - 2; y >= 0; y--)
                for (int x = 0; x < width; x++)
                    this[x, y + 1] = this[x, y];

            for (int x = 0; x < width; x++)
                this[x, 0] = new TilemapEntry();
        }

        public void ShiftLeft()
        {
            for (int y = 0; y < height; y++)
                for (int x = 1; x < width; x++)
                    this[x - 1, y] = this[x, y];

            for (int y = 0; y < height; y++)
                this[width - 1, y] = new TilemapEntry();
        }

        public void ShiftRight()
        {
            for (int y = 0; y < height; y++)
                for (int x = width - 2; x >= 0; x--)
                    this[x + 1, y] = this[x, y];

            for (int y = 0; y < height; y++)
                this[0, y] = new TilemapEntry();
        }

        /// <summary>
        /// Draws the tilemap on an image.
        /// </summary>
        /// <param name="fb">The image to draw the tilemap on.</param>
        /// <param name="tileset">The tileset.</param>
        public void Draw(FastBitmap fb, Tileset tileset)
        {
            Draw(fb, tileset, 0, 0, width, height);
        }

        /// <summary>
        /// Draws a specified part of the tilemap on an image.
        /// </summary>
        /// <param name="fb">The image to draw the tilemap on.</param>
        /// <param name="tileset">The tileset.</param>
        public void Draw(FastBitmap fb, Tileset tileset, int srcX, int srcY, int srcWidth, int srcHeight)
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
