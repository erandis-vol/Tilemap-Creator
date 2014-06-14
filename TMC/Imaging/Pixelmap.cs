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
