using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Gets the total number of colors in the <see cref="Palette"/>.
        /// </summary>
        public int Length => _colors.Length;
    }
}
