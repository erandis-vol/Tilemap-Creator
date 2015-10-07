using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TMC.Imaging
{
    public static class Extensions
    {
        /*public static Pixelmap Flip(this Pixelmap pmap, bool flipX, bool flipY)
        {
            Pixelmap pmap2 = new Pixelmap(pmap.Width, pmap.Height, pmap.Palette);

            if (flipX)
            {
                // rotate around the x thingy
                for (int y = 0; y < pmap.Height; y++)
                    for (int x = 0; x < pmap.Height / 2; x++)
                    {
                        pmap2.SetPixel(x, y, pmap.GetPixel(x, y));
                        pmap2.SetPixel(pmap.Width - x - 1, y, pmap.GetPixel(pmap.Width - x - 1, y));
                    }
            }

            if (flipY)
            {
                // rotate around the y thingy
                for (int x = 0; x < pmap.Width; x++)
                    for (int y = 0; y < pmap.Height / 2; y++)
                    {
                        pmap2.SetPixel(x, y, pmap.GetPixel(x, y));
                        pmap2.SetPixel(x, pmap.Height - y - 1, pmap.GetPixel(x, pmap.Height - y - 1));
                    }
            }

            return pmap2;
        }*/

        public static Palette ToGreyscale(this Palette pal)
        {
            Palette pal2 = new Palette(pal.Length);

            for (int i = 0; i < pal.Length; i++)
            {
                // Average the color
                Color c = pal[i];
                int avg = (c.R + c.G + c.B) / 3;

                // Add to new palette
                pal2[i] = Color.FromArgb(avg, avg, avg);
            }

            return pal2;
        }

        /// <summary>
        /// Creates a Palette starting with the top-left corner of the first n Colors.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="maxColors">The maximum size of the Palette.</param>
        /// <returns></returns>
        public static Palette CreatePalette(this Bitmap bmp, int maxColors = 256)
        {
            Palette pal = new Palette(maxColors);

            int i = 0;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixel = bmp.GetPixel(x, y);
                    if (!pal.Contains(pixel))
                    {
                        pal[i] = pixel;
                        i++;
                    }

                    if (i > maxColors - 1) return pal;
                }
            }

            return pal;
        }

        /// <summary>
        /// Creates a Palette of the most frequent colors.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="maxColors">The maximum size of the Palette.</param>
        /// <returns></returns>
        public static Palette CreatePalette2(this Bitmap bmp, int maxColors = 256)
        {
            // Setup
            Palette pal = new Palette(maxColors);
            Dictionary<Color, int> colorFrequencies = new Dictionary<Color, int>();

            // Get color frequencies
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    // Get and quantize the color
                    Color c = bmp.GetPixel(x, y).Quantize();

                    // Add it to the dictionary
                    if (colorFrequencies.ContainsKey(c))
                        colorFrequencies[c] += 1;
                    else
                        colorFrequencies.Add(c, 1);
                }
            }

            // Order color frequency dictionary
            var values = from pair in colorFrequencies
                         orderby pair.Value descending
                         select pair;

            // Get top n colors
            int w = 0;
            foreach (KeyValuePair<Color, int> pair in values)
            {
                // Get color, put in palette
                pal[w] = pair.Key;

                // Limiter
                w++;
                if (w >= pal.Length) break;
            }
            colorFrequencies.Clear();

            // Done~
            return pal;
        }

        /// <summary>
        /// Counts the number of unique, quantized colors in a Bitmap.
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static int CountColors(this Bitmap bmp)
        {
            FastPixel fp = new FastPixel(bmp);
            fp.Lock();

            List<Color> unique = new List<Color>();
            for (int y = 0; y < fp.Height; y++)
            {
                for (int x = 0; x < fp.Width; x++)
                {
                    Color c = fp.GetPixel(x, y).Quantize();
                    if (!unique.Contains(c))
                    {
                        unique.Add(c);
                    }
                }
            }

            return unique.Count;
        }

        /// <summary>
        /// Quantizes this System.Drawing.Color to be compatible with the GBA.
        /// (RGB components divisible by 8)
        /// </summary>
        /// <param name="color">The color to quantize.</param>
        /// <returns></returns>
        public static Color Quantize(this Color color)
        {
            return Color.FromArgb((color.R >> 3) << 3, (color.G >> 3) << 3, (color.B >> 3) << 3);
        }

        /// <summary>
        /// Converts this System.Drawing.Color to an unsigned 15-bit color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ushort ToColor15(this Color color)
        {
            return (ushort)((color.B / 8) * 1024 +
                (color.G / 8) * 32 + (color.R / 8));
        }

        /// <summary>
        /// Converts an unsigned 15-bit color to a System.Drawing.Color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(this ushort color)
        {
            return Color.FromArgb((color & 0x1F) * 8,
                (color >> 5 & 0x1F) * 8, (color >> 10 & 0x1F) * 8);
        }
    }
}
