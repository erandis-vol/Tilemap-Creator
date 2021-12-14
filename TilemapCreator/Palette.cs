using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TilemapCreator
{
    public class Palette
    {
        private readonly Bgr555[] _colors;

        public Palette(Bgr555 color, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            _colors = new Bgr555[length];
            Array.Fill(_colors, color);
        }

        public Palette(IEnumerable<Bgr555> colors)
        {
            if (colors is null)
                throw new ArgumentNullException(nameof(colors));
            _colors = colors.ToArray();
            if (_colors.Length == 0)
                throw new ArgumentException("Collection does not contain colors.", nameof(colors));
        }

#if DEBUG
        // creates a random palette for testing
        public static Palette CreateRandom(int length)
        {
            var random = new Random();
            var colors = new List<Bgr555>(length);
            for (int i = 0; i < length; i++)
                colors.Add(new Bgr555(random.Next(0, 0x1F), random.Next(0, 0x1F), random.Next(0, 0x1F)));
            return new Palette(colors);
        }
#endif

        // Creates a new grayscale palette with the specified length.
        public static Palette CreateGrayscale(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var colors = new Bgr555[length];
            if (length <= 16)
            {
                // Single set of 16 colors, stepping by 2
                for (int i = 0; i < colors.Length; i++)
                {
                    var g = i << 1;
                    colors[i] = new Bgr555(g, g, g);
                }
            }
            else
            {
                // Split palette into runs of 32 colors
                for (int i = 0; i < colors.Length; i++)
                {
                    var g = i % 32;
                    colors[i] = new Bgr555(g, g, g);
                }
            }
            return new Palette(colors);
        }

        public static Palette Load(string filename, PaletteFormat format)
        {
            return format switch
            {
                PaletteFormat.Pal => LoadPal(filename),
                _ => throw new NotSupportedException($"Palette format {format} is not supported for loading.")
            };
        }

        private static Palette LoadPal(string filename)
        {
            using var reader = new StreamReader(filename);
            if (reader.ReadLine() != "JASC-PAL")
                throw new InvalidDataException("Invalid palette. Expected \"JASC-PAL\".");
            if (reader.ReadLine() != "0100")
                throw new InvalidDataException("Unsupported palette format.");
            if (!int.TryParse(reader.ReadLine(), out var length))
                throw new InvalidDataException("Invalid palette length.");

            var colors = new Bgr555[length];
            for (int i = 0; i < colors.Length; i++)
            {
                try
                {
                    var color = reader.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var r = byte.Parse(color[0]);
                    var g = byte.Parse(color[1]);
                    var b = byte.Parse(color[2]);
                    colors[i] = new Bgr555(r / 8, g / 8, b / 8);
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException($"Invalid color format on line {i + 4}.", ex);
                }
            }

            return new Palette(colors);
        }

        public Bgr555 this[int index]
        {
            get => _colors[index];
            set => _colors[index] = value;
        }

        // determines the index of the color
        public int IndexOf(Bgr555 color)
        {
            for (int i = 0; i < _colors.Length; i++)
            {
                if (_colors[i] == color)
                    return i;
            }
            return -1;
        }

        // determines the index of the closest color in the palette
        // will always return a valid index
        public int ClosestIndexOf(Bgr555 color)
        {
            // calculates the "distance" between the two colors
            static int Distance(Bgr555 c1, Bgr555 c2)
            {
                var dr = c1.R - c2.R;
                var dg = c1.G - c2.G;
                var db = c1.B - c2.B;
                return dr * dr + dg * dg + db * db;
            }

            var besti = 0;
            var bestd = Distance(_colors[0], color);
            for (int i = 1; i < _colors.Length; i++)
            {
                var d = Distance(_colors[i], color);
                if (d  < bestd)
                {
                    besti = i;
                    bestd = d;
                }
            }
            return besti;
        }

        public void Save(string filename, PaletteFormat format)
        {
            using var stream = File.Create(filename);
            switch (format)
            {
                case PaletteFormat.Pal:
                    SavePal(stream);
                    break;

                case PaletteFormat.Act:
                    if (_colors.Length > 256)
                        throw new ArgumentException("Palette contains too many colors to save.", nameof(format));
                    SaveAct(stream);
                    break;

                case PaletteFormat.Gba:
                    SaveGba(stream);
                    break;

                default:
                    throw new ArgumentException("Unsupported palette format.", nameof(format));
            }
        }

        // windows/photoshop palette format
        private void SavePal(Stream stream)
        {
            using var writer = new StreamWriter(stream);
            writer.WriteLine("JASC-PAL");
            writer.WriteLine("0100");
            writer.WriteLine(_colors.Length);
            for (int i = 0; i < _colors.Length; i++)
            {
                var color = _colors[i];
                writer.Write(color.R << 3);
                writer.Write(' ');
                writer.Write(color.G << 3);
                writer.Write(' ');
                writer.Write(color.B << 3);
                writer.WriteLine();
            }
        }

        // ACT palette format
        private void SaveAct(Stream stream)
        {
            using var writer = new BinaryWriter(stream);
            // ACT must be 256 colors in size
            for (int i = 0; i < 256; i++)
            {
                if (i < _colors.Length)
                {
                    var color = _colors[i];
                    writer.Write(color.R << 3);
                    writer.Write(color.G << 3);
                    writer.Write(color.B << 3);
                }
                else
                {
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                }
            }
        }

        // GBA raw palette format
        private void SaveGba(Stream stream)
        {
            using var writer = new BinaryWriter(stream);
            for (int i = 0; i < _colors.Length; i++)
            {
                writer.Write(_colors[i].ToUInt16());
            }
        }

        // TODO: NDS palette format
        private void SaveNclr(Stream stream)
        {
            using var writer = new BinaryWriter(stream);

            if (_colors.Length != 256 && _colors.Length != 16)
                throw new ArgumentException("Palette should contain exactly 16 or 256 colors.");

            var is4bpp = _colors.Length == 16;

            // header section
            writer.Write((byte)'R'); // NCLR
            writer.Write((byte)'L');
            writer.Write((byte)'C');
            writer.Write((byte)'N');
            writer.Write((byte)0xFF); // bom
            writer.Write((byte)0xFE);
            writer.Write((byte)0); // version
            writer.Write((byte)1);
            writer.Write(is4bpp ? 0x0 : 0x228); // file size
            writer.Write((ushort)16); // header size
            writer.Write((ushort)1); // sections

            // palette section
            writer.Write((byte)'T'); // PLTT
            writer.Write((byte)'T');
            writer.Write((byte)'L');
            writer.Write((byte)'P');
            writer.Write(is4bpp ? 0x38 : 0x218); // section size -- color data + 36
            writer.Write((ushort)(is4bpp ? 3 : 4)); // bit depth: 3 = 4bpp, 4 = 8 bpp
            writer.Write((ushort)0); // could also be 0xA
            writer.Write(0); // could also be 1
            writer.Write(is4bpp ? 0x20 : 0x200); // "memory size"? tends to be size of color data in bytes
            writer.Write(0x10); // data offset -- start of section + 8

            for (int i = 0; i < _colors.Length; i++)
                writer.Write(_colors[i].ToUInt16());
        }

        /// <summary>
        /// Gets the total number of colors in the <see cref="Palette"/>.
        /// </summary>
        public int Length => _colors.Length;
    }
}
