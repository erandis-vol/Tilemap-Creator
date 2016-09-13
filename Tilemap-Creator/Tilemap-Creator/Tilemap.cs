using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TMC
{
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Tile
    {
        public int TilesetIndex;
        public int PaletteIndex;
        public bool FlipX;
        public bool FlipY;
    }

    [Flags]
    public enum TilemapFormat
    {
        /// <summary>
        /// Gameboy Advance Raw Tilemap
        /// </summary>
        GBA = 0x00,
        /// <summary>
        /// TODO
        /// </summary>
        NDS = 0x01,

        /// <summary>
        /// 4 bits per pixel
        /// </summary>
        BPP4 = 0x40,
        /// <summary>
        /// 8 bits per pixel
        /// </summary>
        BPP8 = 0x80,
        
        Format = 0x0F,
        BitDepth = 0xF0,
    }

    // a basic resizable Tilemap
    public class Tilemap
    {
        Tile[] tiles;
        int width, height;

        public Tilemap(int width, int height)
        {
            this.width = width;
            this.height = height;

            tiles = new Tile[width * height];
            for (int i = 0; i < width * height; i++)
                tiles[i] = new Tile();
        }

        public Tile this[int index]
        {
            get { return tiles[index]; }
            set { tiles[index] = value; }
        }

        public Tile this[int x, int y]
        {
            get { return tiles[x + y * width]; }
            set { tiles[x + y * width] = value; }
        }

        public Tile this[System.Drawing.Point p]
        {
            get { return this[p.X, p.Y]; }
            set { this[p.X, p.Y] = value; }
        }

        public void Resize(int newWidth, int newHeight)
        {
            // declare new tile array
            Tile[] newTiles = new Tile[newWidth * newHeight];
            for (int i = 0; i < newWidth * newHeight; i++)
                newTiles[i] = new Tile();

            // the data needed to be copied
            int copyWidth = Math.Min(width, newWidth);
            int copyHeight = Math.Min(height, newHeight);
            
            // copy tiles over
            for (int y = 0; y < copyHeight; y++)
            {
                for (int x = 0; x < copyWidth; x++)
                {
                    // new (x, y) = old (x, y)
                    newTiles[x + y * newWidth] = tiles[x + y * width];
                }
            }

            // all done
            tiles = newTiles;
            width = newWidth;
            height = newHeight;
        }

        public void Save(string filename, TilemapFormat format, int extraBytes = 0)
        {
            switch (format & TilemapFormat.Format)
            {
                case TilemapFormat.GBA:
                    SaveGBA(filename, format & TilemapFormat.BitDepth, extraBytes);
                    break;
            }
        }

        void SaveGBA(string filename, TilemapFormat mode, int extraBytes)
        {
            // http://problemkaputt.de/gbatek.htm#lcdvrambgscreendataformatbgmap
            // TODO: support rotation/scaling mode

            // text mode:
            // 0x3FF tiles
            // rotation/scaling mode:
            // 0xFF tiles

            using (var fs = File.Create(filename))
            using (var bw = new BinaryWriter(fs))
            {
                // save text mode tilemap
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var t = this[x, y];

                        if (mode == TilemapFormat.BPP4)
                            bw.Write((ushort)(
                                (t.TilesetIndex & 0x3FF) |
                                (t.FlipX ? 1 : 0 << 10) |
                                (t.FlipY ? 1 : 0 << 11) |
                                (t.PaletteIndex << 12)
                                ));
                        else if (mode == TilemapFormat.BPP8)
                            bw.Write((ushort)(
                                (t.TilesetIndex & 0x3FF) |
                                (t.FlipX ? 1 : 0 << 10) |
                                (t.FlipY ? 1 : 0 << 11)
                                ));
                        else
                            bw.Write((byte)(t.TilesetIndex & 0xFF));
                    }
                }
            }
        }

        void SaveNSCR(string filename, TilemapFormat bitDepth, int extraBytes)
        {
            // http://llref.emutalk.net/docs/?file=xml/nscr.xml#xml-doc
            // there are actually a lot of options for this format
            // a separate editor may be better, honestly

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
                        var t = this[x, y];

                        if (bitDepth == TilemapFormat.BPP4)
                            bw.Write((ushort)(
                                (t.TilesetIndex & 0x3FF) |
                                (t.FlipX ? 1 : 0 << 10) |
                                (t.FlipY ? 1 : 0 << 11) |
                                (t.PaletteIndex << 12)
                                ));
                        else
                            bw.Write((ushort)(
                                (t.TilesetIndex & 0x3FF) |
                                (t.FlipX ? 1 : 0 << 10) |
                                (t.FlipY ? 1 : 0 << 11)
                                ));
                    }
                }

                // adjust file size in header
                bw.BaseStream.Position = 8L;
                bw.Write(bw.BaseStream.Length);
            }
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
