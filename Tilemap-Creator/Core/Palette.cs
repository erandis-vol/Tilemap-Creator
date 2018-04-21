using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        GPL, // GIMP palette uses the same extension!!!
    }

    public static class ColorExtensions
    {
        public static ushort ToColor15(this Color color)
        {
            return (ushort)((color.R >> 3) | ((color.G >> 3) << 5) | ((color.B >> 3) << 10));
        }
    }

    public static class Palette
    {
        public static Color[] Load(string filename, PaletteFormat format)
        {
            switch (format)
            {
                case PaletteFormat.ACT:
                    return LoadAct(filename);
                case PaletteFormat.PAL:
                    return LoadPal(filename);

                default:
                    throw new NotImplementedException();
            }
        }

        static Color[] LoadAct(string filename)
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
                        break;

                    colors[i] = Color.FromArgb(r, g, b);
                }
            }

            return colors;
        }

        static Color[] LoadPal(string filename)
        {
            using (var sr = File.OpenText(filename))
            {
                if (sr.ReadLine() != "JASC-PAL")
                    throw new Exception();
                if (sr.ReadLine() != "0100")
                    throw new Exception();

                var size = int.Parse(sr.ReadLine());
                var colors = new Color[size];

                for (int i = 0; i < size; i++)
                {
                    var c = sr.ReadLine().Split(' ');

                    // no safe checking, we want an exception on error
                    var r = int.Parse(c[0]);
                    var g = int.Parse(c[1]);
                    var b = int.Parse(c[2]);
                    colors[i] = Color.FromArgb(r, g, b);
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

        static void SavePal(Color[] colors, string filename)
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

        static void SaveAct(Color[] colors, string filename)
        {
            // an Adobe Color Table must always be 256 colors
            // http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1070626

            if (colors.Length > 256)
                throw new Exception("The given palette has too many colors for an Adobe Color Table!");

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

        static void SaveGpl(Color[] colors, string filename)
        {
            // this is a truely HORRIBLE format for storing a palette
            // no clue what HackMew was thinking honestly
            if (colors.Length > 256)
                throw new Exception("The given palette has too many colors for an APE palette!");

            using (var sw = File.CreateText(filename))
            {
                sw.WriteLine("[APE Palette]");

                for (int i = 0; i < 256; i++)
                {
                    // convert color to GBA format
                    // BUT, we want it signed AND bytes flipped
                    short color = 0;
                    if (i < colors.Length)
                        color = colors[i].ToApeColor();

                    // ugly thing isn't it
                    sw.WriteLine("{0}{1} ", color < 0 ? "" : " ", color);
                }
            }
        }

        static short ToApeColor(this Color c)
        {
            var g = c.ToColor15();
            return (short)(((g & 0xFF) << 8) | (g >> 8));
        }
    }
}
