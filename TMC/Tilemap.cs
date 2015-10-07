using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace TMC
{
    public class Tile
    {
        public int Value, Palette;
        public bool FlipX, FlipY;

        public Tile()
        {
            Value = 0;
            Palette = 0;
            FlipX = false;
            FlipY = false;
        }

        public Tile(ushort gba)
        {
            Value = gba & 0x3FF;
            Palette = (gba & 0xF000) >> 0xC;

            switch ((gba & 0xC00) >> 0xA)
            {
                case 3:
                    FlipX = true;
                    FlipY = true;
                    break;

                case 2:
                    FlipX = false;
                    FlipY = true;
                    break;

                case 1:
                    FlipX = true;
                    FlipY = false;
                    break;

                case 0:
                default:
                    FlipX = false;
                    FlipY = false;
                    break;
            }
        }

        public byte[] ToGBA4BPP()
        {
            byte[] r = new byte[] { 0, 0 };

            // Format tile flipping
            int flip = 0;
            if (FlipX) flip += 1;
            if (FlipY) flip += 2;

            // Get data
            r[0] = (byte)(Value & 0xFF);
            r[1] = (byte)(((Value & 0x300) >> 8) ^ (Palette << 4) ^ (flip << 2));

            return r;
        }
    }

    public enum TilemapFormat
    {
        GBA4BPP, // 4 bpp raw gba format
        GBA8BPP, // 8 bpp raw gba format
        NSCR4BPP, // Nintendo Screen Format (Used on DS)
        NSCR8BPP
    }

    public class Tilemap
    {
        public const int TILE_SIZE = 8;

        private Tile[] tiles;
        private int width, height;

        private Bitmap image;
        private int zoom;

        /// <summary>
        /// Initializes a new Tilemap with the given width and height.
        /// </summary>
        /// <param name="width">The width of the Tilemap.</param>
        /// <param name="height">The height of the Tilemap.</param>
        public Tilemap(int width, int height)
        {
            this.width = width;
            this.height = height;

            tiles = new Tile[width * height];
            for (int i = 0; i < width * height; i++) tiles[i] = new Tile();
        }

        /// <summary>
        /// Initializes a new Tilemap from a file in the GBA raw tilemap format.
        /// </summary>
        /// <param name="file">The file to load from.</param>
        /// <param name="format">The expected format of the Tilemap.</param>
        /// <param name="width">The expected width of the Tilemap in tiles.</param>
        public Tilemap(string file, TilemapFormat format, int width = 1)
        {
            if (format == TilemapFormat.NSCR4BPP || format == TilemapFormat.NSCR8BPP)
            {
                throw new Exception("Expected GBA raw format!");
            }
            else
                using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
                {
                    // Calculte size
                    int len = (int)br.BaseStream.Length / 2;
                    int height = len / width;

                    // Initialize
                    this.width = width;
                    this.height = height;
                    this.tiles = new Tile[width * height];

                    // Loops x and y, while allowing for EOF buffers
                    int y = 0;
                    while (len >= width)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (format == TilemapFormat.GBA4BPP)
                            {
                                // Gets the tile in 4 BPP format
                                this[x, y] = new Tile(br.ReadUInt16());
                            }
                            else
                            {
                                // Sets the tile in 8 BPP format
                                this[x, y].Value = br.ReadUInt16();
                            }
                        }

                        y++;
                        len -= width;
                    }
                }

        }

        ~Tilemap()
        {
            if (image != null) image.Dispose();
        }

        public Tile this[int x, int y]
        {
            get
            {
                return tiles[x + y * width];
            }
            set
            {
                tiles[x + y * width] = value;
            }
        }

        public void Resize(int newWidth, int newHeight)
        {
            // I wrote this thing over two years ago
            // What a mess
            // But it works

            int oldWidth = width;
            int oldHeight = height;

            width = newWidth;
            height = newHeight;

            // Break up old map data into list of rows
            Tile[] oldMap = tiles;
            List<Tile[]> oldMapRows = new List<Tile[]>();
            for (int y = 0; y < oldHeight; y++)
            {
                int start = y * oldWidth;

                Tile[] row = new Tile[oldWidth];
                for (int x = 0; x < oldWidth; x++) row[x] = tiles[x + start];

                oldMapRows.Add(row);
            }

            // Make new map data in a list of rows
            List<Tile[]> newMapRows = new List<Tile[]>();
            for (int y = 0; y < newHeight; y++)
            {
                Tile[] row = new Tile[newWidth];
                for (int x = 0; x < newWidth; x++) row[x] = new Tile();
                newMapRows.Add(row);
            }

            // Conditional resize!
            // I'm pretty proud of this one here.
            for (int y = 0; y < (oldHeight > newHeight ? newHeight : oldHeight); y++)
            {
                Tile[] rowO = oldMapRows[y];
                Tile[] rowN = newMapRows[y];

                for (int x = 0; x < rowO.Length; x++)
                {
                    if (x >= (oldWidth > newWidth ? rowN.Length : rowO.Length)) break;
                    else
                    {
                        rowN[x] = rowO[x];
                    }
                }

                newMapRows[y] = rowN;
            }

            // Reconstruct
            tiles = new Tile[width * height];
            for (int y = 0; y < height; y++)
            {
                int start = y * width;

                Tile[] row = newMapRows[y];
                for (int x = 0; x < width; x++)
                {
                    tiles[start + x] = row[x];
                }
            }
        }

        public Bitmap Render(Tileset tileset, int zoom)
        {
            if (this.zoom != zoom || image == null)
            {
                image = new Bitmap(width * TILE_SIZE * zoom, height * TILE_SIZE * zoom);
                this.zoom = zoom;
            }

            //Bitmap bmp = new Bitmap(width * TILE_SIZE * zoom, height * TILE_SIZE * zoom);
            using (Graphics g = Graphics.FromImage(image))
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Tile t = this[x, y];

                    // I think this should speed things up a bit
                    if (t.FlipX || t.FlipY)
                    {
                        g.DrawImage(tileset[t.Value].Render(t.FlipX, t.FlipY, zoom), x * TILE_SIZE * zoom, y * TILE_SIZE * zoom);
                    }
                    else
                    {
                        g.DrawImage(tileset[t.Value].Render(zoom), x * TILE_SIZE * zoom, y * TILE_SIZE * zoom);
                    }
                    //g.DrawImage(tileset[t.Value].RenderFlipped(t.FlipX, t.FlipY, zoom), x * TILE_SIZE * zoom, y * TILE_SIZE * zoom);
                }
            //return bmp;

            return image;
        }

        public Bitmap Render(Tileset tileset, Rectangle window, int zoom)
        {
            // JIC
            if (this.zoom != zoom || image == null)
            {
                return Render(tileset, zoom);
            }

            // Otherwise, let's go
            using (Graphics g = Graphics.FromImage(image))
                for (int y = 0; y < window.Height; y++)
                    for (int x = 0; x < window.Width; x++)
                    {
                        int aX = window.X + x;
                        int aY = window.Y + y;

                        if (aX >= width || aY >= height) continue;

                        Tile t = this[aX, aY];

                        // I think this should speed things up a bit
                        if (t.FlipX || t.FlipY)
                        {
                            g.DrawImage(tileset[t.Value].Render(t.FlipX, t.FlipY, zoom), aX * TILE_SIZE * zoom, aY * TILE_SIZE * zoom);
                        }
                        else
                        {
                            g.DrawImage(tileset[t.Value].Render(zoom), aX * TILE_SIZE * zoom, aY * TILE_SIZE * zoom);
                        }
                        //g.DrawImage(tileset[t.Value].RenderFlipped(t.FlipX, t.FlipY, zoom), x * TILE_SIZE * zoom, y * TILE_SIZE * zoom);
                    }

            return image;
        }

        /// <summary>
        /// Save this Tilemap in the specified TilemapFormat.
        /// </summary>
        /// <param name="file">The file to overwrite.</param>
        /// <param name="format">The format to save in.</param>
        /// <param name="buffer">The number of bytes (if any) to append to the end of the file.</param>
        public void Save(string file, TilemapFormat format, int buffer = -1)
        {
            BinaryWriter bw = new BinaryWriter(File.Create(file));

            // Write tiles based on format
            // GBA
            if (format == TilemapFormat.GBA4BPP || format == TilemapFormat.GBA8BPP)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // There are two GBA formats
                        if (format == TilemapFormat.GBA4BPP)
                        {
                            bw.Write(this[x, y].ToGBA4BPP());
                        }
                        else
                        {
                            bw.Write((ushort)this[x, y].Value);
                        }
                    }
                }

                // Write EOF buffer
                if (buffer > 0)
                {
                    for (int i = 0; i < buffer; i++)
                    {
                        bw.Write((byte)0);
                    }
                }
            }
            else if (format == TilemapFormat.NSCR4BPP || format == TilemapFormat.NSCR8BPP)
            {
                // Generic header
                bw.Write(0x4E534352); // NSCR
                bw.Write(0x0100FEFF); // format info
                bw.Write((uint)0x0); // file size -- later
                bw.Write((uint)0x10); // header size
                bw.Write((uint)0x1); // 1 subsection

                // Format specific header
                uint dataSize = (uint)(width * height * 2);
                bw.Write((uint)0x4E524353); // NRCS
                bw.Write((uint)(20 + dataSize)); // section size -- header + dataSize
                bw.Write((ushort)(width * 8)); // width in pixels
                bw.Write((ushort)(height * 8)); // height in pixels
                bw.Write((ushort)0x0); // internal size -- we won't bother (0 = 256 x 256)
                bw.Write((ushort)0x0); // bg type -- we won't bother
                bw.Write(dataSize); // data size

                // NTFS (tile data)
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Tile t = this[x, y];

                        if (format == TilemapFormat.NSCR4BPP)
                        {
                            ushort u = (ushort)(((t.Palette & 0xF) << 12) |
                                ((t.FlipY ? 1 : 0) << 11) |
                                ((t.FlipX ? 1 : 0) << 10) |
                                (t.Value & 0x3FF));
                            bw.Write(u);
                        }
                        else
                        {
                            ushort u = (ushort)(((t.FlipY ? 1 : 0) << 11) |
                                ((t.FlipX ? 1 : 0) << 10) |
                                (t.Value & 0x3FF));
                            bw.Write(u);
                        }
                    }
                }

                // Write file size
                bw.BaseStream.Seek(8L, SeekOrigin.Begin);
                bw.Write((uint)bw.BaseStream.Length);
            }


            bw.Close();
            bw.Dispose();
        }

        private void LoadRaw(string file, int bpp = 4)
        {
            
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
    }
}
