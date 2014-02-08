using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TMC
{
    public struct TileFilp
    {
        public bool X, Y;

        public byte ToRaw()
        {
            byte b = 0;
            if (X) b++;
            if (Y) b += 2;
            return b;
        }
    }

    public class Index
    {
        public int Tile, Palette;
        public TileFilp Flip;

        public Index()
        {
            Tile = 0;
            Palette = 0;
            
            Flip = new TileFilp() { X = false, Y = false };
        }

        public byte[] ToRaw()
        {
            byte[] b = { 0, 0 };
            b[0] = (byte)(Tile & 255);
            b[1] = (byte)(((Tile & 0x300) >> 8) ^ (Palette << 4) ^ (Flip.ToRaw() << 2));
            return b;
        }
    }

    public enum BitDepth
    {
        BPP4, BPP8
    }
    
    public class Tilemap
    {
        private Index[] map;

        private int width, height;

        public Tilemap(int width, int height)
        {
            this.width = width;
            this.height = height;

            // Init. array
            map = new Index[width * height];
            for (int i = 0; i < width * height; i++)
                map[i] = new Index();
        }

        public static Tilemap FromRaw(string file, int width, BitDepth bpp)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(file));

            int len = (int)br.BaseStream.Length / 2;
            int height = len / width;

            Tilemap tm = new Tilemap(width, height);

            if (bpp == BitDepth.BPP4)
            {
                int y = 0;
                while (true)
                {
                    for (int i = 0; i < width; i++)
                    {
                        int index = i + y * width;

                        ushort temp = br.ReadUInt16();
                        tm[index].Tile = (temp & 0x3FF);
                        tm[index].Palette = ((temp & 0xF000) >> 0xC);

                        switch (((temp & 0xC00) >> 0xA))
                        {
                            case 3:
                                tm[index].Flip.X = true;
                                tm[index].Flip.Y = true;
                                break;
                            case 2:
                                tm[index].Flip.Y = true;
                                break;
                            case 1:
                                tm[index].Flip.X = true;
                                break;
                        }
                    }

                    y++;

                    len -= width;
                    if (len < width) break;
                }
            }
            else // 8 BPP
            {
                int y = 0;
                while (true)
                {
                    for (int i = 0; i < width; i++)
                    {
                        int index = i + y * width;

                        tm[index].Tile = br.ReadUInt16();
                        tm[index].Palette = 0;
                        tm[index].Flip.X = false;
                        tm[index].Flip.Y = false;
                    }

                    y++;

                    len -= width;
                    if (len < width) break;
                }
            }

            br.Close();
            br.Dispose();

            return tm;
        }

        public static Tilemap FromSphereMap(string file)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(file));

            if (Encoding.UTF8.GetString(br.ReadBytes(4)) != ".rmp")
            {
                br.Close();
                br.Dispose();
                MessageBox.Show("Incorrect file format!");
                return new Tilemap(1, 1);
            }

            br.BaseStream.Seek(0x112, SeekOrigin.Begin);
            int width = br.ReadUInt16();
            int height = br.ReadUInt16();

            Tilemap tm = new Tilemap(width, height);

            // Skip the image name...
            br.BaseStream.Seek(0x130, SeekOrigin.Begin);
            int len = br.ReadUInt16();
            br.BaseStream.Seek(len, SeekOrigin.Current);

            // And read the tile data
            for (int i = 0; i < width * height; i++)
            {
                tm[i].Tile = br.ReadUInt16();
            }

            br.Close();
            br.Dispose();

            return tm;
        }

        public void Save(string file, BitDepth bpp, int buffer = -1)
        {
            BinaryWriter bw = new BinaryWriter(File.Create(file));

            if (bpp == BitDepth.BPP4)
            {
                byte[] bytes = { 0, 0 };
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        bw.Write(this[x, y].ToRaw(), 0, 2);
                    }
                }
            }
            else
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        bw.Write((ushort)this[x, y].Tile);
                    }
                }
            }

            if (buffer > 0)
            {
                for (int i = 0; i < buffer; i++)
                {
                    bw.Write((byte)0);
                }
            }

            bw.Close();
            bw.Dispose();
        }

        public Index this[int x, int y]
        {
            get { return map[x + y * width]; }
            set { map[x + y * width] = value; }
        }

        public Index this[int index]
        {
            get { return map[index]; }
            set { map[index] = value; }
        }

        public void Resize(int newWidth, int newHeight)
        {
            int oldWidth = width;
            int oldHeight = height;
            
            width = newWidth;
            height = newHeight;

            // Break up old map data into list of rows
            Index[] oldMap = map;
            List<Index[]> oldMapRows = new List<Index[]>();
            for (int y = 0; y < oldHeight; y++)
            {
                int start = y * oldWidth;

                Index[] row = new Index[oldWidth];
                for (int x = 0; x < oldWidth; x++) row[x] = map[x + start];

                oldMapRows.Add(row);
            }

            // Make new map data in a list of rows
            List<Index[]> newMapRows = new List<Index[]>();
            for (int y = 0; y < newHeight; y++)
            {
                Index[] row = new Index[newWidth];
                for (int x = 0; x < newWidth; x++) row[x] = new Index();
                newMapRows.Add(row);
            }

            // Conditional resize!
            // I'm pretty proud of this one here.
            for (int y = 0; y < (oldHeight > newHeight ? newHeight : oldHeight); y++)
            {
                Index[] rowO = oldMapRows[y];
                Index[] rowN = newMapRows[y];

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
            map = new Index[width * height];
            for (int y = 0; y < height; y++)
            {
                int start = y * width;

                Index[] row = newMapRows[y];
                for (int x = 0; x < width; x++)
                {
                    map[start + x] = row[x];
                }
            }
        }

        public Bitmap Draw(Tileset tileset)
        {
            Bitmap b = new Bitmap(width * 8, height * 8);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Red);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = x + y * width;
                    //int tile = map[index].Tile;

                    g.DrawImage(FlipBitmap(tileset[map[index].Tile], map[index].Flip.X, map[index].Flip.Y), x * 8, y * 8);
                }
            }

            return b;
        }

        private Bitmap FlipBitmap(Bitmap b, bool x, bool y)
        {
            Bitmap bb = new Bitmap(b);
            if (x) bb.RotateFlip(RotateFlipType.RotateNoneFlipX);
            if (y) bb.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bb;
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Length
        {
            get { return width * height; }
        }
    }
}
