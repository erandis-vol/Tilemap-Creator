using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TMC
{
    public class Tileset : IDisposable
    {
        private string image;
        private Bitmap[] tiles = null;

        private Size[] sizes;

        public Tileset(string file)
        {
            image = file;
            SplitTiles();
            CalculateSizes();
        }

        public void Dispose()
        {
            if (tiles != null)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].Dispose();
                }
            }
        }

        public Bitmap this[int index]
        {
            get { return (index < tiles.Length ? tiles[index] : tiles[0]); }
            set { tiles[index] = value; }
        }

        public Bitmap Draw(int width)
        {
            int height = tiles.Length / width;
            if (tiles.Length % width > 0) height++;

            Bitmap b = new Bitmap(width * 8, height * 8);
            Graphics g = Graphics.FromImage(b);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = x + y * width;

                    Bitmap bb = (index >= tiles.Length ? tiles[0] : tiles[index]);
                    g.DrawImage(bb, x * 8, y * 8);
                }
            }

            return b;
        }

        public void Save(string file, int width, ColorMode colorMode, Color[] palette = null)
        {
            if (colorMode == ColorMode.ColorTrue)
            {
                SaveBitmap(file, width);
            }
            else if (colorMode == ColorMode.Color16)
            {
                SaveBitmap4Bpp(file, width, palette);
            }
            else if (colorMode == ColorMode.Color256)
            {
                //SaveBitmap8Bpp(file, width, palette); // saves unindexed
                SaveBitmap(file, width);
            }
            // TODO: colorMode == ColorMode.Color256 (Indexed)
        }

        private void SaveBitmap4Bpp(string file, int width, Color[] palette)
        {
            // Custom, using a palette!
            // Uses stuff found in NSE 2.X for a base.
            Bitmap b = Draw(width);

            int pixelArraySize = (b.Width / 2) * b.Height;
            byte[] data = new byte[118 + pixelArraySize];

            // Write header
            data[0xA] = 0x76;

            data[0x1C] = 4;
            byte[] ilength = BitConverter.GetBytes(pixelArraySize);

            data[0] = (byte)'B';
            data[1] = (byte)'M';

            byte[] flength = BitConverter.GetBytes(data.Length);
            flength.CopyTo(data, 2);

            data[6] = (byte)'T';
            data[7] = (byte)'M';
            data[8] = (byte)'C';
            data[9] = (byte)'3';

            data[0xE] = 40;

            byte[] w = BitConverter.GetBytes(b.Width);
            w.CopyTo(data, 0x12);

            byte[] h = BitConverter.GetBytes(b.Height);
            h.CopyTo(data, 0x16);

            data[0x1A] = 1;

            ilength.CopyTo(data, 0x22);

            // Write palette table
            for (int i = 0; i < palette.Length; i++)
            {
                data[0x36 + i * 4] = palette[i].B;
                data[0x37 + i * 4] = palette[i].G;
                data[0x38 + i * 4] = palette[i].R;
            }

            // Write pixels
            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x += 2)
                {
                    int l = Helper.GetClosestColorFromPalette(b.GetPixel(x, y), palette);
                    int r = Helper.GetClosestColorFromPalette(b.GetPixel(x + 1, y), palette);

                    data[118 + (x / 2) + ((b.Height - y - 1) * (b.Width / 2))] = (byte)((byte)(l << 4) + (byte)r);
                }
            }

            // Save to file
            BinaryWriter bw = new BinaryWriter(File.Create(file));
            bw.Write(data);

            bw.Close();
            bw.Dispose();
        }

        // This doesn't work.
        private void SaveBitmap8Bpp(string file, int width, Color[] palette)
        {
            // Custom, using a palette!
            // Uses stuff found in NSE 2.X for a base.
            Bitmap b = Draw(width);

            int pixelArraySize = b.Width * b.Height;
            byte[] data = new byte[1114 + pixelArraySize];

            // Write header
            data[0xA] = 0x54;
            data[0xB] = 0x4;

            data[0x1C] = 8;
            byte[] ilength = BitConverter.GetBytes(pixelArraySize);

            data[0] = (byte)'B';
            data[1] = (byte)'M';

            byte[] flength = BitConverter.GetBytes(data.Length);
            flength.CopyTo(data, 2);

            data[6] = (byte)'T';
            data[7] = (byte)'M';
            data[8] = (byte)'C';
            data[9] = (byte)'3';

            data[0xE] = 40;

            byte[] w = BitConverter.GetBytes(b.Width);
            w.CopyTo(data, 0x12);

            byte[] h = BitConverter.GetBytes(b.Height);
            h.CopyTo(data, 0x16);

            data[0x1A] = 1;

            ilength.CopyTo(data, 0x22);

            // Write palette table
            for (int i = 0; i < palette.Length; i++)
            {
                data[0x36 + i * 4] = palette[i].B;
                data[0x37 + i * 4] = palette[i].G;
                data[0x38 + i * 4] = palette[i].R;
            }

            // Write pixels
            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    int l = Helper.GetClosestColorFromPalette(b.GetPixel(x, y), palette);

                    data[1114 + x + ((b.Height - y - 1) * b.Width)] = (byte)l;
                }
            }

            // Save to file
            BinaryWriter bw = new BinaryWriter(File.Create(file));
            bw.Write(data);

            bw.Close();
            bw.Dispose();
        }

        private void SaveBitmap(string file, int width)
        {
            // Default method
            Bitmap b = Draw(width);
            b.Save(file, ImageFormat.Bmp);
        }

        private Bitmap LoadBitmap(string file)
        {
            try
            {
                Bitmap b = new Bitmap(file);
                return b;
            }
            catch (Exception ex)
            {
                return new Bitmap(8, 8);
            }
        }

        private void SplitTiles()
        {
            Bitmap b = LoadBitmap(image);

            int width = b.Width / 8;
            int height = b.Height / 8;

            tiles = new Bitmap[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Bitmap bb = new Bitmap(8, 8);
                    for (int xx = 0; xx < 8; xx++)
                    {
                        for (int yy = 0; yy < 8; yy++)
                        {
                            bb.SetPixel(xx, yy, b.GetPixel(xx + x * 8, yy + y * 8));
                        }
                    }
                    tiles[x + y * width] = bb;
                }
            }
        }

        public void CalculateSizes()
        {
            List<Size> ss = new List<Size>();
            for (int i = 1; i < tiles.Length; i++)
            {
                if (tiles.Length % i == 0) ss.Add(new Size(i, tiles.Length / i));
            }
            sizes = ss.ToArray();
        }

        public int Length
        {
            get { return tiles.Length; }
        }

        public Bitmap[] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        public Size[] Sizes
        {
            get { return sizes; }
        }
    }

    
}
