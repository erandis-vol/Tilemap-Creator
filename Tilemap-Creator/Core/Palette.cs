using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TMC.Core
{
    /// <summary>
    /// Represents a collection of colors.
    /// </summary>
    public class Palette : IEnumerable<Color>
    {
        private Color[] colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class with the specified length.
        /// </summary>
        /// <param name="color">The color to repeat.</param>
        /// <param name="length">The number of colors.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than zero.</exception>
        public Palette(Color color, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length");

            colors = new Color[length];
            for (int i = 0; i < length; i++)
                colors[i] = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class from the specified <see cref="Color"/> values.
        /// </summary>
        /// <param name="colors">A enumerator of color values.</param>
        /// <exception cref="ArgumentNullException"><paramref name="colors"/> is null.</exception>
        public Palette(IEnumerable<Color> colors)
        {
            if (colors == null)
                throw new ArgumentNullException(nameof(colors));

            this.colors = colors.ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class from the specified file.
        /// </summary>
        /// <param name="filename">The file name and path of the palette.</param>
        /// <param name="format">The expected format of the file.</param>
        /// <exception cref="FileNotFoundException">the file could not be found.</exception>
        /// <exception cref="InvalidDataException">the file is not formatted as expected.</exception>
        /// <exception cref="NotSupportedException"><paramref name="format"/> is not supported for loading.</exception>
        public Palette(string filename, PaletteFormat format = PaletteFormat.Default)
            : this(new FileInfo(filename), format)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class from the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="format">The expected format of the file.</param>
        /// <exception cref="FileNotFoundException">the file could not be found.</exception>
        /// <exception cref="InvalidDataException">the file is not formatted as expected.</exception>
        /// <exception cref="NotSupportedException"><paramref name="format"/> is not supported for loading.</exception>
        public Palette(FileInfo file, PaletteFormat format = PaletteFormat.Default)
        {
            using (var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Load(stream, format);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The expected format of the stream.</param>
        public Palette(Stream stream, PaletteFormat format = PaletteFormat.Default)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new ArgumentException("Stream does not support reading.", nameof(stream));

            Load(stream, format);
        }

        /// <summary>
        /// Creates a new <see cref="Palette"/> from the specified image.
        /// </summary>
        /// <param name="bitmap">The image to create a palette from.</param>
        /// <returns></returns>
        public static Palette Create(Bitmap bitmap)
        {
            if ((bitmap.PixelFormat & PixelFormat.Indexed) != PixelFormat.Indexed)
            {
                var colors = new List<Color>();

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        var color = bitmap.GetPixel(x, y);
                        var index = colors.IndexOf(color);
                        if (index == -1)
                            colors.Add(color);
                    }
                }

                return new Palette(colors);
            }
            else
            {
                return new Palette(bitmap.Palette.Entries);
            }
        }

        private void Load(Stream stream, PaletteFormat format)
        {
            switch (format)
            {
                case PaletteFormat.Default:
                    LoadDefault(stream);
                    break;

                case PaletteFormat.PAL:
                    LoadPAL(stream);
                    break;

                case PaletteFormat.ACT:
                    LoadACT(stream);
                    break;

                default:
                    throw new NotSupportedException($"Palette format {format} is not supported for loading.");
            }
        }

        private void LoadDefault(Stream stream)
        {
            using (var br = new BinaryReader(stream))
            {
                var p = br.ReadByte();
                var l = br.ReadByte();
                var t = br.ReadByte();

                if (p != 'P' ||
                    l != 'L' ||
                    t != 'T')
                {
                    throw new InvalidDataException("This is not a palette file.");
                }

                var length = br.ReadInt32();

                colors = new Color[length];
                for (int i = 0; i < length; i++)
                    colors[i] = Color.FromArgb(br.ReadInt32());
            }
        }

        private void LoadPAL(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                if (sr.ReadLine() != "JASC-PAL")
                    throw new InvalidDataException("This is not a PAL file.");
                if (sr.ReadLine() != "0100")
                    throw new InvalidDataException("Unsupported PAL version.");

                var length = 0;
                if (!int.TryParse(sr.ReadLine(), out length))
                    throw new InvalidDataException("Invalid palette length.");

                colors = new Color[length];
                for (int i = 0; i < length; i++)
                {
                    try
                    {
                        var color = sr.ReadLine().Split(' ');

                        var r = byte.Parse(color[0]);
                        var g = byte.Parse(color[1]);
                        var b = byte.Parse(color[2]);

                        colors[i] = Color.FromArgb(r, g, b);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidDataException($"Invalid color on line {i + 4}.", ex);
                    }
                }
            }
        }

        private void LoadACT(Stream stream)
        {
            using (var br = new BinaryReader(stream))
            {
                // NOTE: An Adobe Color Table must always contain 256 colors.
                // http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1070626
                try
                {
                    colors = new Color[256];
                    for (int i = 0; i < 256; i++)
                    {
                        var r = br.ReadByte();
                        var g = br.ReadByte();
                        var b = br.ReadByte();

                        colors[i] = Color.FromArgb(r, g, b);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException("Invalid color table.", ex);
                }
            }
        }

        /// <summary>
        /// Gets or sets the specified color.
        /// </summary>
        /// <param name="index">The color's index.</param>
        /// <returns>A color.</returns>
        public Color this[int index]
        {
            get => colors[index];
            set => colors[index] = value;
        }

        /// <summary>
        /// Saves this <see cref="Palette"/> to the specified file.
        /// </summary>
        /// <param name="filename">The file name and path to save as.</param>
        /// <param name="format">The format to save the palette as.</param>
        /// <exception cref="NotSupportedException"><paramref name="format"/> is not supported for saving.</exception>
        public void Save(string filename, PaletteFormat format = PaletteFormat.Default)
        {
            Save(new FileInfo(filename), format);
        }

        /// <summary>
        /// Saves this <see cref="Palette"/> to the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="format">The format to save the palette as.</param>
        /// <exception cref="NotSupportedException"><paramref name="format"/> is not supported for saving.</exception>
        public void Save(FileInfo file, PaletteFormat format = PaletteFormat.Default)
        {
            using (var stream = file.Create())
            {
                Save(stream, format);
            }
        }

        /// <summary>
        /// Saves this <see cref="Palette"/> to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format to save the palette as.</param>
        public void Save(Stream stream, PaletteFormat format = PaletteFormat.Default)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanWrite)
                throw new ArgumentException("Stream does not support writing.", nameof(stream));

            switch (format)
            {
                case PaletteFormat.Default:
                    SaveDefault(stream);
                    break;

                case PaletteFormat.PAL:
                    SavePAL(stream);
                    break;

                case PaletteFormat.ACT:
                    SaveACT(stream);
                    break;

                default:
                    throw new NotSupportedException($"Palette format {format} is not supported for saving.");
            }
        }

        private void SaveDefault(Stream stream)
        {
            using (var bw = new BinaryWriter(stream))
            {
                bw.Write((byte)'P');
                bw.Write((byte)'L');
                bw.Write((byte)'T');

                bw.Write(colors.Length);
                foreach (var color in colors)
                    bw.Write(color.ToArgb());
            }
        }

        private void SavePAL(Stream stream)
        {
            using (var sw = new StreamWriter(stream))
            {
                sw.WriteLine("JASC-PAL");
                sw.WriteLine("0100");
                sw.WriteLine(colors.Length);

                for (int i = 0; i < colors.Length; i++)
                    sw.WriteLine("{0} {1} {2}", colors[i].R, colors[i].G, colors[i].B);
            }
        }

        private void SaveACT(Stream stream)
        {
            if (colors.Length != 256)
                throw new NotSupportedException("The palette does not contain 256 colors.");

            using (var bw = new BinaryWriter(stream))
            {
                foreach (var color in colors)
                {
                    bw.Write(color.R);
                    bw.Write(color.G);
                    bw.Write(color.B);
                }
            }
        }

        public int IndexOf(Color color)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == color)
                    return i;
            }

            return -1;
        }

        public int ClosestIndexOf(Color color)
        {
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

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Palette"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Color> GetEnumerator()
        {
            return ((IEnumerable<Color>)colors).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Palette"/>.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return colors.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of colors in this <see cref="Palette"/>.
        /// </summary>
        public int Length => colors.Length;
    }

    /*
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

        public static Color[] Create(DirectBitmap db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            var colors = new List<Color>();

            for (int y = 0; y < db.Height; y++)
            {
                for (int x = 0; x < db.Width; x++)
                {
                    var color = db.GetPixel(x, y);
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

        private static short ToApeColor(this Color c)
        {
            var g = c.ToBgr555();
            return (short)(((g & 0xFF) << 8) | (g >> 8));
        }
    }
    */
}
