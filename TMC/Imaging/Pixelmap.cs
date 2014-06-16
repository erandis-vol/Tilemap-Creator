using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace TMC.Imaging
{
    // Used when creating a Pixelmap from a Bitmap
    // Only two methods so far...
    public enum PaletteGenerationMethod { First, Frequency }

    // This will handle image manipulation
    // It will hopefully replace the Bitmap stuff, and allow a linked up palette...
    public class Pixelmap
    {
        private byte[] pixels;
        private int width, height;

        private Color[] palette;

        private Bitmap cachedImg;

        // Make a blank image
        public Pixelmap(int width, int height, int colors = 256)
        {
            this.width = width;
            this.height = height;
            this.pixels = new byte[width * height];
            this.palette = Helper.CreateGreyscalePalette(colors);

            this.cachedImg = null;

            Clear(0);
        }

        public Pixelmap(int width, int height, Color[] palette)
        {
            this.width = width;
            this.height = height;
            this.pixels = new byte[width * height];
            this.palette = palette;

            this.cachedImg = null;

            Clear(0);
        }

        public Pixelmap(Bitmap bmp, Color[] palette)
        {
            this.palette = palette;
            this.width = bmp.Width;
            this.height = bmp.Height;
            this.pixels = new byte[bmp.Width * bmp.Height];

            this.cachedImg = null;

            // Get pixels~
            // I should add stuff to make this better, but it'll work
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int index = Helper.GetClosestColorFromPalette(c, palette);
                    pixels[x + y * bmp.Width] = (byte)index;
                }
            }
        }

        public Pixelmap(Bitmap bmp, PaletteGenerationMethod paletteMethod, int colors = 256)
        {
            this.width = bmp.Width;
            this.height = bmp.Height;
            this.pixels = new byte[bmp.Width * bmp.Height];

            this.cachedImg = null;

            // Get palette~
            if (paletteMethod == PaletteGenerationMethod.First) this.palette = Helper.CreateBitmapPalette(bmp, colors);
            else this.palette = Helper.CreateBitmapFrequencyPalette(bmp, colors);

            // Get pixels~
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int index = Helper.GetClosestColorFromPalette(c, palette);
                    pixels[x + y * bmp.Width] = (byte)index;
                }
            }
        }

        public Pixelmap(Bitmap bmp)
        {
            // This will only be used with indexed images
            if (bmp.Palette == null || bmp.Palette.Entries.Length == 0) throw new Exception("Bitmap not indexed!");

            this.width = bmp.Width;
            this.height = bmp.Height;
            this.pixels = new byte[bmp.Width * bmp.Height];

            this.cachedImg = null;

            // Get palette!
            this.palette = new Color[bmp.Palette.Entries.Length];
            for (int p = 0; p < bmp.Palette.Entries.Length; p++)
            {
                palette[p] = bmp.Palette.Entries[p];
            }

            // Get pixels~
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int index = Helper.GetClosestColorFromPalette(c, palette);
                    pixels[x + y * bmp.Width] = (byte)index;
                }
            }
        }

        ~Pixelmap()
        {
            if (cachedImg != null) cachedImg.Dispose();
        }

        public void Clear(byte color)
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    pixels[x + y * width] = color;
        }

        public Pixelmap[] Tile()
        {
            int tiledWidth = width / 8;
            int tiledHeight = height / 8;
            int count = tiledWidth * tiledHeight;
            Pixelmap[] tiles = new Pixelmap[count];
            for (int y = 0; y < tiledHeight; y++)
            {
                for (int x = 0; x < tiledWidth; x++)
                {
                    Pixelmap t = new Pixelmap(8, 8, palette);
                    for (int yy = 0; yy < 8; yy++)
                    {
                        for (int xx = 0; xx < 8; xx++)
                        {
                            // woot~!
                            byte b = GetPixel(xx + x * 8, yy + y * 8);
                            t.SetPixel(xx, yy, b);
                        }
                    }
                    tiles[x + y * tiledWidth] = t;
                }
            }
            return tiles;
        }

        // Draw this pixelmap
        // Allows for zooming, although only one scale will be cached
        // Yeah...
        public Bitmap Draw(int zoom = 1)
        {
            // Should speed stuff up later~
            if (cachedImg != null && cachedImg.Width == width * zoom && cachedImg.Height == height * zoom)
            {
                return cachedImg;
            }
            else
            {
                // Drawing code, I wanna clean this up and I can probably speed it up too~
                Bitmap bmp = new Bitmap(width * zoom, height * zoom);
                Graphics g = Graphics.FromImage(bmp);
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        g.FillRectangle(new SolidBrush(palette[pixels[x + y * width]]),
                            x * zoom, y * zoom, zoom, zoom);
                    }
                g.Dispose();
                return bmp;
            }
        }

        public void SetPixel(int x, int y, byte pixel)
        {
            pixels[x + y * width] = pixel;
        }

        public void SetPixel(int x, int y, Color pixel)
        {
            byte p = (byte)Helper.GetClosestColorFromPalette(pixel, palette);
            pixels[x + y * width] = p;
        }

        public byte GetPixel(int x, int y)
        {
            return pixels[x + y * width];
        }

        #region Properties

        public Color[] Palette
        {
            get { return palette; }
            set
            {
                //if (palette.Length == value.Length) palette = value; // -- maybe?
                palette = value;
            }
        }

        #endregion
    }
}
