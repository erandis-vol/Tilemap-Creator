using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TMC.Core
{
    public enum PaletteFormat
    {
        /// <summary>
        /// Photoshop Palette
        /// </summary>
        PAL,
        /// <summary>
        /// Adobe Color Table
        /// </summary>
        ACT,
        /// <summary>
        /// Nitro Color Table
        /// </summary>
        NCLR,
        /// <summary>
        /// APE Palette
        /// </summary>
        GPL, // GIMP palette uses the same extension
    }

    public static class ColorExtensions
    {
        public static Color Quantize(this Color color)
        {
            return Color.FromArgb(color.R >> 3 << 3, color.G >> 3 << 3, color.B >> 3 << 3);
        }

        public static ushort ToRgb15(this Color color)
        {
            return (ushort)((color.R >> 3) | ((color.G >> 3) << 5) | ((color.B >> 3) << 10));
        }
    }

    public static class Palette
    {
        public static int IndexOf(this Color[] colors, Color color)
        {
            if (colors == null) return -1;

            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == color) return i;
            }

            return -1;
        }

        public static int ClosestIndexOf(this Color[] colors, Color color)
        {
            if (colors == null)
                throw new ArgumentNullException(nameof(colors));

            // Calculates the distance between two colors
            int Distance(Color c1, Color c2)
            {
                var dr = c1.R - c2.R;
                var dg = c1.R - c2.R;
                var db = c1.R - c2.R;
                return dr * dr + dg * dg + db * db;
            }

            // Determines the closest color
            var best = 0;
            var bestDist = Distance(colors[0], color);

            for (int i = 1; i < colors.Length; i++)
            {
                var dist = Distance(colors[i], color);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    best = i;
                }
            }

            return best;
        }

        public static Color[] Create(FastBitmap fb)
        {
            if (fb == null)
                throw new ArgumentNullException(nameof(fb));

            var colors = new List<Color>();

            for (int y = 0; y < fb.Height; y++)
            {
                for (int x = 0; x < fb.Width; x++)
                {
                    var color = fb.GetPixel(x, y);
                    if (!colors.Contains(color))
                    {
                        colors.Add(color);
                    }
                }
            }

            return colors.ToArray();
        }

        public static Color[] Load(string filename, PaletteFormat format)
        {
            switch (format)
            {
                case PaletteFormat.ACT:
                    return LoadAct(filename);
                case PaletteFormat.PAL:
                    return LoadPal(filename);

                default:
                    throw new NotSupportedException($"Cannot load palettes in {format} format.");
            }
        }

        private static Color[] LoadAct(string filename)
        {
            var colors = new Color[256];

            using (var fs = File.OpenRead(filename))
            {
                for (int i = 0; i < 256; i++)
                {
                    var r = fs.ReadByte();
                    var g = fs.ReadByte();
                    var b = fs.ReadByte();

                    if (r == -1 || g == -1 || b == -1)
                        throw new InvalidDataException();

                    colors[i] = Color.FromArgb(r, g, b);
                }
            }

            return colors;
        }

        private static Color[] LoadPal(string filename)
        {
            using (var sr = File.OpenText(filename))
            {
                if (sr.ReadLine() != "JASC-PAL")
                    throw new InvalidDataException();
                if (sr.ReadLine() != "0100")
                    throw new InvalidDataException();

                var size = int.Parse(sr.ReadLine());
                var colors = new Color[size];

                for (int i = 0; i < size; i++)
                {
                    var c = sr.ReadLine().Split(' ');

                    try
                    {
                        var r = int.Parse(c[0]);
                        var g = int.Parse(c[1]);
                        var b = int.Parse(c[2]);

                        colors[i] = Color.FromArgb(r, g, b);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidDataException("Invalid color.", ex);
                    }
                }

                return colors;
            }
        }

        public static void Save(Color[] colors, string filename, PaletteFormat format)
        {
            switch (format)
            {
                case PaletteFormat.PAL:
                    SavePal(colors, filename);
                    break;
                case PaletteFormat.ACT:
                    SaveAct(colors, filename);
                    break;
                case PaletteFormat.GPL:
                    SaveGpl(colors, filename);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private static void SavePal(Color[] colors, string filename)
        {
            // NOTE: convention states JASC-PAL files should only have
            // 256 or 16 colors, but we'll allow any number

            //if (colors.Length > 256)
            //    throw new Exception("The given palette has too many colors for a palette file!");

            using (var sw = File.CreateText(filename))
            {
                // header
                sw.WriteLine("JASC-PAL");
                sw.WriteLine("0100");
                sw.WriteLine(colors.Length);

                // colors
                for (int i = 0; i < colors.Length; i++)
                {
                    sw.WriteLine("{0} {1} {2}", colors[i].R, colors[i].G, colors[i].B);
                }
            }
        }

        private static void SaveAct(Color[] colors, string filename)
        {
            // An Adobe Color Table must always be 256 colors
            // http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1070626

            if (colors.Length > 256)
            {
                throw new ArgumentOutOfRangeException("The palette has too many colors for an Adobe Color Table.",
                    nameof(colors));
            }

            using (var bw = new BinaryWriter(File.Create(filename)))
            {
                for (int i = 0; i < 256; i++)
                {
                    if (i < colors.Length)
                    {
                        bw.Write(colors[i].R);
                        bw.Write(colors[i].G);
                        bw.Write(colors[i].B);
                    }
                    else
                    {
                        bw.Write(byte.MinValue);
                        bw.Write(byte.MinValue);
                        bw.Write(byte.MinValue);
                    }
                }
            }
        }

        private static void SaveGpl(Color[] colors, string filename)
        {
            // This is a nasty little palette format
            if (colors.Length > 256)
                throw new Exception("The given palette has too many colors for an APE palette.");

            using (var sw = File.CreateText(filename))
            {
                sw.WriteLine("[APE Palette]");

                for (int i = 0; i < 256; i++)
                {
                    // 1. Convert color to a GBA color
                    // 2. Reverse byte order
                    // 3. Signed
                    short color = 0;
                    if (i < colors.Length)
                        color = colors[i].ToApeColor();

                    sw.WriteLine("{0}{1} ", color < 0 ? "" : " ", color);
                }
            }
        }

        static short ToApeColor(this Color c)
        {
            var g = c.ToRgb15();
            return (short)(((g & 0xFF) << 8) | (g >> 8));
        }
    }
}
