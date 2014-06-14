using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TMC.Imaging
{
    public static class Helper
    {
        #region Palette-ing

        /// <summary>
        /// Creates a palette for a Bitmap of the most frequency Colors.
        /// Still in testing.
        /// </summary>
        /// <param name="bmp">The Bitmap.</param>
        /// <param name="maxColors">The maximum colors allowed in the palette.</param>
        /// <returns></returns>
        public static Color[] CreateBitmapFrequencyPalette(Bitmap bmp, int maxColors = 256)
        {
            // Setup
            Color[] output = new Color[maxColors];
            Dictionary<Color, int> colorFrequencies = new Dictionary<Color, int>();

            // Get color frequencies
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    // Get this pixel from the bitmap, and add it to frequency chart
                    Color c = bmp.GetPixel(x, y);
                    if (colorFrequencies.ContainsKey(c))
                        colorFrequencies[c] += 1;
                    else
                        colorFrequencies.Add(c, 1);
                }
            }

            // Fill output with blanks
            for (int i = 0; i < output.Length; i++) output[i] = Color.Black;

            // Order color frequency dictionary
            var values = from pair in colorFrequencies
                         orderby pair.Value descending
                         select pair;

            // Get top 16/256 colors
            int w = 0;
            foreach (KeyValuePair<Color, int> pair in values)
            {
                // Get color, put in palette
                output[w] = pair.Key;

                // Limiter
                w++;
                if (w >= output.Length) break;
            }
            colorFrequencies.Clear();

            // Done~!
            return output;
        }

        /// <summary>
        /// Creates a palette for a Bitmap using the first n-Colors from the top left corner.
        /// Based off of the method created by Wichu.
        /// </summary>
        /// <param name="bmp">The Bitmap.</param>
        /// <param name="maxColors">The maximum Colors in the palette.</param>
        /// <returns></returns>
        public static Color[] CreateBitmapPalette(Bitmap bmp, int maxColors = 256)
        {
            Color[] pal = new Color[maxColors];

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

        public static int GetClosestColorFromPalette(Color color, Color[] palette)
        {
            QuantizeColor(ref color);

            int diff = 765;

            int result = 0;
            for (int index = 0; index < palette.Length; index++)
            {
                int colorDiff = GetColorDifference(color, palette[index]);
                if (colorDiff < diff)
                {
                    diff = colorDiff;
                    result = index;
                }
            }
            return result;
        }

        public static int GetColorDifference(Color c1, Color c2)
        {
            int r = c1.R - c2.R;
            int g = c1.G - c2.G;
            int b = c1.B - c2.B;
            return (r * r + g * g + b * b);
        }

        public static void QuantizeColor(ref Color color)
        {
            color = Color.FromArgb((color.R >> 3) << 3, (color.G >> 3) << 3, (color.B >> 3) << 3);
        }

        public static Color[] CreateGreyscalePalette(int colors = 256)
        {
            Color[] palette = new Color[colors];
            int gap = 256 / colors;
            for (int i = 0; i < colors; i++) palette[i] = Color.FromArgb(256 - i * gap, 256 - i * gap, 256 - i * gap);

            return palette;
        }

        #endregion

    }
}
