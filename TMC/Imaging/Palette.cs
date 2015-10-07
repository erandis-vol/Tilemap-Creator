using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TMC.Imaging
{
    /// <summary>
    /// Specifies the file format of a Palette.
    /// </summary>
    public enum PaletteFormat
    {
        TMCP,
        PAL,
        ACT,
        NCLR
    }

    public class Palette
    {
        private Color[] colors;

        public Palette(int size = 16, bool greyscale = false)
        {
            // initialize
            colors = new Color[size];
            
            // fill with a default color
            if (greyscale)
            {
                int step = 256 / size;
                for (int i = 0; i < size; i++) colors[i] = Color.FromArgb(step * i, step * i, step * i);
            }
            else
            {
                for (int i = 0; i < size; i++) colors[i] = Color.Black;
            }
        }

        public Palette(Bitmap bmp)
        {
            if (bmp.PixelFormat == PixelFormat.Format4bppIndexed ||
                bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                // Palette *should* be the right length...
                colors = bmp.Palette.Entries;
            }
            else
            {
                throw new Exception("Image is not indexed!");
            }
        }

        /// <summary>
        /// Gets or sets the specified System.Drawing.Color in the Palette.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Color this[int index]
        {
            get { return colors[index]; }
            set { colors[index] = value; }
        }

        /// <summary>
        /// Gets the index of the closest matching System.Drawing.Color in the Palette.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public int this[Color color]
        {
            get
            {
                color = color.Quantize();

                int diff = 765;

                int result = 0;
                for (int index = 0; index < colors.Length; index++)
                {
                    int colorDiff = GetColorDifference(color, colors[index]);
                    if (colorDiff < diff)
                    {
                        diff = colorDiff;
                        result = index;
                    }
                }
                return result;
            }
        }

        #region Saving

        /// <summary>
        /// Save this Palette to the specified file in the specified format.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="format">The PaletteFormat for this Palette.</param>
        public void Save(string file, PaletteFormat format)
        {
            // No real speed difference at this size, but...
            // 5+ items would be a hash table
            // And if I add more formats~ that would be cool.
            switch (format)
            {
                case PaletteFormat.TMCP:
                    SaveTMCP(file);
                    break;

                case PaletteFormat.PAL:
                    SavePAL(file);
                    break;

                case PaletteFormat.ACT:
                    SaveACT(file);
                    break;

                case PaletteFormat.NCLR:
                    SaveNCLR(file);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void SaveTMCP(string file)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                // A simple format
                // Saves space by writing it in the format the GBA/DS would want

                bw.Write((uint)0x70434D54);
                bw.Write(colors.Length);

                for (int i = 0; i < colors.Length; i++)
                {
                    bw.Write(colors[i].ToColor15());
                }
            }
        }

        private void SaveNCLR(string file/*, Color[][] palettes, ColorMode mode*/)
        {
            // First, make sure this palette is the correct size
            if (colors.Length != 16 && colors.Length != 256)
            {
                throw new Exception("Palette must contain either 16 or 256 colors in order to save it as a NCLR!");
            }

            // Normally, this format supports multiple palettes
            // But we'll just limit it to one palette for now

            // This format is actually a lot more annoying than it could be.
            // Sometimes, I hate Nintendo.
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                // Get the palette size
                uint cpp = (uint)colors.Length;
                uint palSize = (uint)cpp * 2;

                // Nitro header
                bw.Write((uint)0x4E434C52); // nclr
                bw.Write((uint)0x0100FEFF); // format
                bw.Write((uint)0x0); // file size -- later
                bw.Write((ushort)0x10); // header size
                bw.Write((ushort)0x2); // sections

                // pltt section
                bw.Write((uint)0x504C5454);
                bw.Write((uint)(24 + palSize)); // section size
                bw.Write((ushort)(cpp == 16 ? 3 : 4)); // bpp
                bw.Write((ushort)0x0); // padding?
                bw.Write((uint)0x0); // padding?

                // Not sure if I did this part right
                bw.Write(palSize); // num. palettes * colors per pal.
                bw.Write((uint)0x10); // data offset (always!)

                // Write colors
                for (int i = 0; i < cpp; i++)
                {
                    if (i < colors.Length) // defined
                    {
                        //bw.Write(Color15.ColorToColor15(palettes[p][i]));
                        bw.Write(colors[i].ToColor15());
                    }
                    else bw.Write((ushort)0x0); // undefined
                }

                // pcmp section
                // This section is used for counting the number of palettes
                // ...or something like that...
                bw.Write((uint)0x50434D50);
                bw.Write((uint)(16 + cpp * 2)); // section size
                bw.Write((ushort)cpp); // num. of palettes
                bw.Write((ushort)0xBEEF); // constant
                bw.Write((uint)0x8); // constant

                // Write the palette's index
                bw.Write((ushort)0);

                // The indexes for each palette
                /*for (ushort p = 0; p < palettes.Length; p++)
                {
                    bw.Write(p);
                }*/

                // Write file size
                bw.Seek(8, SeekOrigin.Begin);
                bw.Write((uint)bw.BaseStream.Length);
            }

            // And that's a wrap~!
        }

        private void SaveACT(string file)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                // This format is really simple.
                // It's just a list of 256 rgb colors.

                for (int i = 0; i < 256; i++)
                {
                    if (i < colors.Length)
                    {
                        bw.Write((byte)colors[i].R);
                        bw.Write((byte)colors[i].G);
                        bw.Write((byte)colors[i].B);
                    }
                    else
                    {
                        bw.Write((byte)0);
                        bw.Write((byte)0);
                        bw.Write((byte)0);
                    }
                }
            }
        }

        private void SavePAL(string file)
        {
            using (StreamWriter sw = File.CreateText(file))
            {
                // header
                sw.WriteLine("JASC-PAL\n0100\n256");

                // colors
                for (int i = 0; i < 256; i++)
                {
                    if (i < colors.Length)
                    {
                        sw.WriteLine("{0} {1} {2}", colors[i].R, colors[i].G, colors[i].B);
                    }
                    else
                    {
                        sw.WriteLine("0 0 0");
                    }
                }
            }
        }

        #endregion

        #region Loading

        private void LoadTMPC(string file)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                if (br.ReadUInt32() != 0x70434D54)
                {
                    throw new Exception("Invalid palette file!");
                }

                int length = br.ReadInt32();

                colors = new Color[length];
                for (int i = 0; i < length; i++)
                {
                    colors[i] = br.ReadUInt16().ToColor();
                }
            }
        }

        #endregion

        public bool Contains(Color color)
        {
            return colors.Contains(color);
        }

        /// <summary>
        /// Returns whether this Palette is exactly the same as another Palette.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsSameAs(Palette other)
        {
            if (colors.Length != other.Length) return false;

            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] != other[i]) return false;
            }

            return true;
        }

        public int Length
        {
            get { return colors.Length; }
        }

        public static int GetColorDifference(Color c1, Color c2)
        {
            int r = c1.R - c2.R;
            int g = c1.G - c2.G;
            int b = c1.B - c2.B;
            return (r * r + g * g + b * b);
        }
    }
}
