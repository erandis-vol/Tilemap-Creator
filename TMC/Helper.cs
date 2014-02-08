using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TMC
{
    public static class Helper
    {
        public static Tilemap ImageToTilemap(string image, out Tileset tileset)
        {
            Bitmap img = new Bitmap(image);

            int width = img.Width / 8;
            int height = img.Height / 8;
            //MessageBox.Show("Size: " + width + " x " + height);
            Tilemap tm = new Tilemap(width, height);

            tileset = new Tileset(image);

            // Fill...
            for (int i = 0; i < tileset.Length; i++)
            {
                tm[i].Tile = i;
            }

            // Remove extra tiles
            List<Bitmap> result = new List<Bitmap>();
            Dictionary<int, int> tileMaps = new Dictionary<int, int>();
            tileMaps[0] = 0;
            result.Add(tileset[0]);

            for (int i = 1; i < tileset.Length; i++)
            {
                tileMaps[i] = i;

                Bitmap b = tileset[i];
                bool unique = true;

                for (int ii = 0; ii < result.Count; ii++)
                {
                    if (BitmapsAreSame(b, result[ii]))
                    {
                        tileMaps[i] = ii;
                        unique = false;
                        break;
                    }
                    else if (BitmapsAreSame(FlipBitmap(b, true, false), result[ii]))
                    {
                        tileMaps[i] = ii;
                        tm[i].Flip.X = true;
                        unique = false;
                        break;
                    }
                    else if (BitmapsAreSame(FlipBitmap(b, false, true), result[ii]))
                    {
                        tileMaps[i] = ii;
                        tm[i].Flip.Y = true;
                        unique = false;
                        break;
                    }
                    else if (BitmapsAreSame(FlipBitmap(b, true, true), result[ii]))
                    {
                        tileMaps[i] = ii;
                        tm[i].Flip.X = true;
                        tm[i].Flip.Y = true;
                        unique = false;
                        break;
                    }
                }

                if (unique)
                {
                    result.Add(b);
                    tileMaps[i] = result.Count - 1;
                }
            }

            for (int i = 0; i < tm.Width * tm.Height; i++)
            {
                tm[i].Tile = tileMaps[tm[i].Tile];
            }

            tileset.Tiles = result.ToArray();
            tileset.CalculateSizes();

            return tm;
        }

        private static bool BitmapsAreSame(Bitmap b1, Bitmap b2)
        {
            if (b1.Width != b2.Width || b1.Height != b2.Height) return false;

            int width = b1.Width; int height = b2.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (b1.GetPixel(x, y) != b2.GetPixel(x, y)) return false;
                }
            }

            return true;
        }

        public static Bitmap FlipBitmap(Bitmap b, bool x, bool y)
        {
            Bitmap bb = new Bitmap(b);
            if (x) bb.RotateFlip(RotateFlipType.RotateNoneFlipX);
            if (y) bb.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bb;
        }

        public static Color[] GetBitmapPalette(Bitmap b, int length = 16)
        {
            Color[] colors = new Color[length];

            int i = 0;
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    Color pixel = b.GetPixel(x, y);
                    if (!colors.Contains(pixel))
                    {
                        colors[i] = pixel;
                        i++;
                    }

                    if (i > length - 1) return colors;
                }
            }

            return colors;
        }

        public static void MatchBitmapToPalette(ref Bitmap b, Color[] palette)
        {
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    Color pixel = b.GetPixel(x, y);
                    int p = GetClosestColorFromPalette(pixel, palette);

                    b.SetPixel(x, y, palette[p]);
                }
            }
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

        public static void ReplaceColor(ref Bitmap b, Color oldColor, Color newColor)
        {
            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    if (b.GetPixel(x, y) == oldColor) b.SetPixel(x, y, newColor);
                }
            }
        }

        public static Color[] GenerateGreyscalePalette(int colors)
        {
            Color[] c = new Color[colors];
            int width = 256 / colors;
            for (int i = 0; i < colors; i++)
            {
                c[i] = Color.FromArgb(i * width, i * width, i * width);

            }
            return c;
        }

        public static void PerformColorSwaps(ref Bitmap b, ColorSwap[] swaps)
        {
            foreach (ColorSwap cs in swaps) ReplaceColor(ref b, cs.Old, cs.New);
        }

        // Palette stuff
        public static void SaveACT(string file, Color[] palette)
        {
            BinaryWriter bw = new BinaryWriter(File.Create(file));

            Color[] export = new Color[256];
            for (int i = 0; i < palette.Length; i++)
            {
                export[i] = palette[i];
            }

            if (palette.Length > 256) throw new Exception("Palette too large!");

            // Write the colors.
            //int extra = 256 - palette.Length;
            for (int i = 0; i < export.Length; i++)
            {
                bw.Write((byte)export[i].R);
                bw.Write((byte)export[i].G);
                bw.Write((byte)export[i].B);
            }

            // Write any filler colors...
            //for (int i = 0; i < extra; i++)
            //{
            //    bw.Write((byte)0);
            //    bw.Write((byte)0);
            //    bw.Write((byte)0);
            //}

            bw.Close();
            bw.Dispose();
        }

        public static void SavePAL(string file, Color[] palette)
        {
            StreamWriter sw = new StreamWriter(File.Create(file));

            Color[] export = new Color[256];
            for (int i = 0; i < palette.Length; i++)
            {
                export[i] = palette[i];
            }

            sw.WriteLine("JASC-PAL");
            sw.WriteLine("0100");

            // Write the colors
            sw.WriteLine("256"); // length of palette
            //int extra = 256 - palette.Length;
            for (int i = 0; i < export.Length; i++)
            {
                string s = export[i].R + " " + export[i].G + " " + export[i].B;
                sw.WriteLine(s);
            }

            //for (int i = 0; i < extra; i++)
            //{
            //    sw.WriteLine("0 0 0");
            //}

            sw.Close();
            sw.Dispose();
        }

    }
}
