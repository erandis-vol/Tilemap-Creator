// Copyright (c) 2014 itari
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at:
// 
//  http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace TMC.Imaging
{
    public enum ColorMode : int
    {
        Color16 = 16, // 4 bpp
        Color256 = 256 // 8 bpp
    }

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

            int diff = GetColorDifference(Color.White, Color.Black); // max difference

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
            for (int i = 0; i < colors; i++) palette[i] = Color.FromArgb(255 - i * gap, 255 - i * gap, 255 - i * gap);
            //for (int i = 0; i < colors; i++) palette[i] = Color.FromArgb(i * gap, i * gap, i * gap);

            return palette;
        }

        #endregion

        #region Palette Saving

        public static void SaveNCLR(string file, Color[][] palettes, ColorMode mode)
        {
            // This format is actually a lot more annoying than it could be.
            // Sometimes, I hate Nintendo.
            BinaryWriter bw = new BinaryWriter(File.Create(file));

            // Get the palette size
            uint cpp = (uint)(mode == ColorMode.Color16 ? 16 : 256);
            uint palSize = (uint)palettes.Length * cpp * 2;

            // Nitro header
            bw.Write((uint)0x4E434C52); // nclr
            bw.Write((uint)0x0100FEFF); // format
            bw.Write((uint)0x0); // file size -- later
            bw.Write((ushort)0x10); // header size
            bw.Write((ushort)0x2); // sections

            // pltt section
            bw.Write((uint)0x504C5454);
            bw.Write((uint)(24 + palSize)); // section size
            bw.Write((ushort)(mode == ColorMode.Color16 ? 3 : 4)); // bpp
            bw.Write((ushort)0x0); // padding?
            bw.Write((uint)0x0); // padding?

            // Not sure if I did this part right
            bw.Write(palSize); // num. palettes * colors per pal.
            bw.Write((uint)0x10); // data offset (always!)

            // Write colors
            for (int p = 0; p < palettes.Length; p++)
            {
                for (int i = 0; i < cpp; i++)
                {
                    if (i < palettes[p].Length) // defined
                    {
                        bw.Write(Color15.ColorToColor15(palettes[p][i]));
                    }
                    else bw.Write((ushort)0x0); // undefined
                }
            }

            // pcmp section
            // This section is used for counting the number of palettes
            // ...or something like that...
            bw.Write((uint)0x50434D50);
            bw.Write((uint)(16 + palettes.GetLength(0) * 2)); // section size
            bw.Write((ushort)palettes.GetLength(0)); // num. of palettes
            bw.Write((ushort)0xBEEF); // constant
            bw.Write((uint)0x8); // constant

            // The indexes for each palette
            for (ushort p = 0; p < palettes.Length; p++)
            {
                bw.Write(p);
            }

            // Write file size
            bw.Seek(8, SeekOrigin.Begin);
            bw.Write((uint)bw.BaseStream.Length);

            // Close
            bw.Close();
            bw.Dispose();

            // And that's a wrap~!
        }

        public static void SaveACT(string file, Color[] colors)
        {
            BinaryWriter bw = new BinaryWriter(File.Create(file));

            // This format is really simple.
            // It's just a list of 256 rgb colors.
            // Easy.

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

            bw.Close();
            bw.Dispose();
        }

        public static void SavePAL(string file, Color[] palette)
        {
            StreamWriter sw = new StreamWriter(File.Create(file));

            // header
            sw.WriteLine("JASC-PAL");
            sw.WriteLine("0100");

            // colors
            sw.WriteLine("256"); // length of palette
            for (int i = 0; i < 256; i++)
            {
                if (i < palette.Length)
                {
                    string s = palette[i].R + " " + palette[i].G + " " + palette[i].B;
                    sw.WriteLine(s);
                }
                else
                {
                    sw.WriteLine("0 0 0");
                }
            }

            sw.Close();
            sw.Dispose();
        }


        #endregion

        #region Palette Loading

        public static Color[] LoadACT(string file)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(file));
            Color[] colors = new Color[256];

            int i = 0;
            while (i < 256 && br.BaseStream.Position < br.BaseStream.Length - 3)
            {
                byte r = br.ReadByte();
                byte g = br.ReadByte();
                byte b = br.ReadByte();

                colors[i] = Color.FromArgb(r, g, b);

                i++;
            }

            br.Close();
            br.Dispose();

            return colors;
        }

        public static Color[] LoadPAL(string file)
        {
            StreamReader sr = File.OpenText(file);

            if (sr.ReadLine().Trim() != "JASC-PAL") throw new Exception("This is not a PAL!");
            if (sr.ReadLine().Trim() != "0100") throw new Exception("Unsupported PAL format!");

            int len = int.Parse(sr.ReadLine().Trim());
            Color[] colors = new Color[len];
            for (int i = 0; i < len; i++)
            {
                string[] l = sr.ReadLine().Trim().Split(' ');
                int r = int.Parse(l[0]);
                int g = int.Parse(l[1]);
                int b = int.Parse(l[2]);

                colors[i] = Color.FromArgb(r, g, b);
            }

            sr.Close();
            sr.Dispose();

            return colors;
        }

        public static Color[][] LoadNCLR(string file)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(file));

            // generic header
            if (br.ReadUInt32() != 0x4E434C52)
                throw new Exception("Bad NCLR format!");
            br.ReadUInt32(); // format info.
            br.ReadUInt32(); // file size
            br.ReadUInt16(); // header size
            ushort sections = br.ReadUInt16(); // sections

            // pltt section
            if (br.ReadUInt32() != 0x504C5454)
                throw new Exception("Bad PLTT format!");
            br.ReadUInt32(); // section size
            int cpp = br.ReadUInt16() == 3 ? 16 : 256;
            br.ReadUInt16(); // padding?
            br.ReadUInt32(); // padding?
            uint palSize = br.ReadUInt32(); // palette size
            br.ReadUInt32(); // 0x10 -- header size

            int numPalettes = (int)palSize / (cpp * 2);
            Color[][] palettes = new Color[numPalettes][];
            for (int p = 0; p < numPalettes; p++)
            {
                palettes[p] = new Color[cpp];
                for (int x = 0; x < cpp; x++)
                {
                    palettes[p][x] = Color15.Color15ToColor(br.ReadUInt16());
                }
            }

            // no sense in reading the pcmp section

            br.Close();
            br.Dispose();

            return palettes;
        }

        #endregion

        #region Image Saving

        public static void SaveNCGR(string file, Pixelmap pm)
        {
            BinaryWriter bw = new BinaryWriter(File.Create(file));
            ColorMode mode = pm.GetColorMode();

            // Generic header
            bw.Write((uint)0x4E434752); // NCGR
            bw.Write((uint)0x0001FEFF); // format info.
            bw.Write(0x0); // file size -- write at end
            bw.Write((ushort)0x10); // header size
            bw.Write((ushort)0x2); // section count

            // CHAR section
            // Responsible for holding the image data
            bw.Write((uint)0x43484152);

            ushort tiledHeight = (ushort)(pm.Height / 8);
            ushort tiledWidth = (ushort)(pm.Width / 8);
            // section size
            if (mode == ColorMode.Color16) bw.Write((uint)(tiledHeight * tiledWidth * 32) + 0x20u);
            else bw.Write((uint)(tiledHeight * tiledWidth * 64) + 0x20u);
            // dimension
            bw.Write(tiledHeight);
            bw.Write(tiledWidth);

            // Bit Depth (3 = 4 bpp, 4 = 8 bpp)
            if (mode == ColorMode.Color16) bw.Write((uint)3);
            else bw.Write((uint)4);

            bw.Write((ushort)0); // unknown
            bw.Write((ushort)0); // unknown
            bw.Write((uint)0); // flags
            // section size
            if (mode == ColorMode.Color16) bw.Write((uint)(tiledHeight * tiledWidth * 32));
            else bw.Write((uint)(tiledWidth * tiledHeight * 64));
            bw.Write((uint)0x18); // section offset (relative) -- always 0x18

            // Write tile data (this is a monstrosity)
            // I write this in "horizontal tile mode"
            // If I was writing in "no tile mode" I would just write
            // The pixels straight out
            for (int y = 0; y < tiledHeight; y++)
            {
                for (int x = 0; x < tiledWidth; x++)
                {
                    // write a tile
                    for (int yy = 0; yy < 8; yy++)
                    {
                        if (mode == ColorMode.Color16) // This is probably correct. It loads right, though.
                        {
                            for (int xx = 0; xx < 8; xx += 2)
                            {
                                byte l = pm.GetPixel(xx + x * 8, yy + y * 8);
                                byte r = pm.GetPixel(xx + 1 + x * 8, yy + y * 8);
                                bw.Write((byte)(((r & 15) << 4) | (l & 15)));
                            }
                        }
                        else // 8 BPP -- not sure if this actually works or not...
                        {
                            for (int xx = 0; xx < 8; xx++)
                            {
                                byte p = pm.GetPixel(xx + x * 8, yy + y * 8);
                                bw.Write(p);
                            }
                        }
                    }
                }
            }

            // SOPC section
            // I don't really know what this does...
            bw.Write((uint)0x43504F53);
            bw.Write((uint)0x10); // ???
            bw.Write((uint)0x0); // ???
            bw.Write((ushort)tiledWidth); // width in tiles
            bw.Write((ushort)tiledHeight); // height in tiels

            // Write file size
            bw.BaseStream.Seek(8L, SeekOrigin.Begin);
            bw.Write((uint)bw.BaseStream.Length);

            bw.Close();
            bw.Dispose();
        }

        public static void SaveBitmap4BPP(string file, Pixelmap pm)
        {
            // Safety?
            if (pm.GetColorMode() != ColorMode.Color16)
                throw new Exception("Bad Pixelmap Bit Depth!");

            // Custom, using a palette!
            // Uses stuff found in NSE 2.X for a base.
            int pixelArraySize = (pm.Width / 2) * pm.Height;
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
            data[9] = (byte)'4';

            data[0xE] = 40;

            byte[] w = BitConverter.GetBytes(pm.Width);
            w.CopyTo(data, 0x12);

            byte[] h = BitConverter.GetBytes(pm.Height);
            h.CopyTo(data, 0x16);

            data[0x1A] = 1;

            ilength.CopyTo(data, 0x22);

            // Write palette table
            for (int i = 0; i < pm.Palette.Length; i++)
            {
                data[0x36 + i * 4] = pm.Palette[i].B;
                data[0x37 + i * 4] = pm.Palette[i].G;
                data[0x38 + i * 4] = pm.Palette[i].R;
            }

            // Write pixels
            for (int y = 0; y < pm.Height; y++)
            {
                for (int x = 0; x < pm.Width; x += 2)
                {
                    byte left = pm.GetPixel(x, y);
                    byte right = pm.GetPixel(x + 1, y);

                    data[118 + (x / 2) + ((pm.Height - y - 1) * (pm.Width / 2))] = (byte)((byte)(left << 4) + right);
                }
            }

            // Save to file
            File.WriteAllBytes(file, data);
        }

        // Sort of works...
        public static void SaveBitmap8BPP(string file, Pixelmap pm)
        {
            // Safety?
            if (pm.GetColorMode() != ColorMode.Color256)
                throw new Exception("Bad Pixelmap Bit Depth!");

            // Custom, using a palette!
            // Uses stuff found in NSE 2.X for a base.
            int pixelArraySize = pm.Width * pm.Height;
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
            data[9] = (byte)'4';

            data[0xE] = 40;

            byte[] w = BitConverter.GetBytes(pm.Width);
            w.CopyTo(data, 0x12);

            byte[] h = BitConverter.GetBytes(pm.Height);
            h.CopyTo(data, 0x16);

            data[0x1A] = 1;

            ilength.CopyTo(data, 0x22);

            // Write palette table
            for (int i = 0; i < pm.Palette.Length; i++)
            {
                data[0x36 + i * 4] = pm.Palette[i].B;
                data[0x37 + i * 4] = pm.Palette[i].G;
                data[0x38 + i * 4] = pm.Palette[i].R;
            }

            // Write pixels
            for (int y = 0; y < pm.Height; y++)
            {
                for (int x = 0; x < pm.Width; x++)
                {
                    data[1114 + x + ((pm.Height - y - 1) * pm.Width)] = pm.GetPixel(x, y);
                }
            }

            // Save to file
            File.WriteAllBytes(file, data);
        }


        #endregion

        #region Image Loading

        // Bitmap handled with default libraries

        // Now, onto NCGR files...
        public static Pixelmap LoadNCGR(string file)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(file));
            Pixelmap pm;

            // header
            if (br.ReadUInt32() != 0x4E434752) throw new Exception("This is an NCGR!");
            br.BaseStream.Position += 10L;
            ushort sections = br.ReadUInt16();

            // char section
            // header
            if (br.ReadUInt32() != 0x43484152) throw new Exception("Bad NCGR format!");
            br.ReadUInt32(); // section size
            ushort tiledHeight = br.ReadUInt16();
            ushort tiledWidth = br.ReadUInt16();

            if (tiledHeight == 0xFFFF || tiledWidth == 0xFFFF) throw new Exception("Unsupported NCGR format!");

            ColorMode mode = ColorMode.Color16;
            if (br.ReadUInt32() == 0x3) mode = ColorMode.Color16;
            else mode = ColorMode.Color256;

            pm = new Pixelmap(tiledWidth * 8, tiledHeight * 8, (int)mode);
            br.ReadUInt16(); // unknown
            br.ReadUInt16(); // unknown
            if (br.ReadUInt32() != 0) throw new Exception("Unsupported NCGR format!"); // flags
            uint size = br.ReadUInt32();
            br.ReadUInt32(); // 0x18

            // read tile data
            for (int y = 0; y < tiledHeight; y++)
            {
                for (int x = 0; x < tiledWidth; x++)
                {
                    for (int yy = 0; yy < 8; yy++)
                    {
                        if (mode == ColorMode.Color16)
                        {
                            for (int xx = 0; xx < 8; xx += 2)
                            {
                                byte b = br.ReadByte();
                                int l = b & 15;
                                int r = (b >> 4) & 15;
                                pm.SetPixel(xx + x * 8, yy + y * 8, (byte)l);
                                pm.SetPixel(xx + 1 + x * 8, yy + y * 8, (byte)r);
                            }
                        }
                        else
                        {
                            for (int xx = 0; xx < 8; xx++)
                            {
                                pm.SetPixel(xx + x * 8, yy + y * 8, br.ReadByte());
                            }
                        }
                    }
                }
            }

            // not sure on this one
            if (sections > 1)
            {
                // pass
            }

            br.Close();
            br.Dispose();

            return pm;
        }

        #endregion

    }
}
