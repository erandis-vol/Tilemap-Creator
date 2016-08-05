using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMC
{
    struct Color15
    {
        public ushort Value;

        public byte R
        {
            get { return (byte)((Value & 0x1F) << 3); }
        }

        public byte G
        {
            get { return (byte)((Value >> 5 & 0x1F) << 3); }
        }

        public byte B
        {
            get { return (byte)((Value >> 10 & 0x1F) << 3); }
        }

        public override bool Equals(object obj)
        {
            if (obj is Color15)
                return ((Color15)obj).Value == Value;
            return false;
        }

        public override int GetHashCode()
        {
            // warning: if setting of value is enabled, this
            return Value;
        }

        public override string ToString()
        {
            return $"color15 [value={Value:X4},r={R}, g={G}, b={B}]";
        }

        public static implicit operator Color15(Color color)
        {
            return new Color15 { Value = (ushort)((color.R >> 3) | ((color.G >> 3) * 0x20) | ((color.B >> 3) * 0x400)) };
        }

        public static implicit operator Color(Color15 color)
        {
            return Color.FromArgb(color.R, color.G, color.B);
        }

        // todo: convert these to getters
        public static Color15 Black = Color.Black;
        public static Color15 Red = Color.Red;
        public static Color15 Orange = Color.Orange;
        public static Color15 Yellow = Color.Yellow;
        public static Color15 Green = Color.Green;
        public static Color15 Blue = Color.Blue;
        public static Color15 Indigo = Color.Indigo;
        public static Color15 Violet = Color.Violet;
        public static Color15 White = Color.White;
    }

    class Palette
    {
        List<Color15> colors = new List<Color15>();

        public Color15 this[int index]
        {
            get { return colors[index]; }
        }

        public bool Contains(Color15 color)
        {
            return colors.Contains(color);
        }

        public int IndexOf(Color15 color)
        {
            return colors.IndexOf(color);
        }

        public void Add(Color15 color)
        {
            colors.Add(color);
        }

        public int Length
        {
            get { return colors.Count; }
        }
    }

    // stores image data for a GBA/NDS sprite
    // 
    class Sprite : IDisposable
    {
        int width, height;
        int[] data;
        Palette colors = new Palette();

        Bitmap cachedImage;
        int cachedZoom;
        bool needsRedraw = false;

        public Sprite(int width, int height)
        {
            this.width = width;
            this.height = height;
            data = new int[width * height];

            UpdateCache(1, true);
        }

        public Sprite(Bitmap source)
        {
            width = source.Width;
            height = source.Height;
            data = new int[width * height];

            // speed this up, too
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    SetPixel(x, y, source.GetPixel(x, y));
                }
            }

            Console.WriteLine("Total colors in Sprite: {0} colors", colors.Length);

            UpdateCache(1, true);
        }

        void UpdateCache(int zoom, bool force = false)
        {
            if (force || cachedZoom != zoom)
            {
                cachedImage?.Dispose();
                cachedImage = new Bitmap(width * zoom, height * zoom);

                cachedZoom = zoom;
                needsRedraw = true;
            }
        }

        public Bitmap Draw(int zoom)
        {
            if (zoom <= 0) throw new Exception("Zoom must be >= 1!");

            UpdateCache(zoom);
            if (needsRedraw)
            {
                // create a cache for the brushes we'll need
                // saves memory compared to making a new brush per pixel
                var brushes = new SolidBrush[colors.Length];
                for (int i = 0; i < brushes.Length; i++) brushes[i] = new SolidBrush(colors[i]);

                using (var gfx = Graphics.FromImage(cachedImage))
                {
                    for (int i = 0; i < width * height; i++)
                    {
                        gfx.FillRectangle(brushes[data[i]], (i % width) * zoom, (i / width) * zoom, zoom, zoom);
                    }
                }

                for (int i = 0; i < brushes.Length; i++) brushes[i].Dispose();
            }
            return cachedImage;
        }

        public Color15 GetPixel(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
                return colors[data[x + y * width]];

            return Color15.Black;
        }

        public void SetPixel(int x , int y, Color15 color)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                if (!colors.Contains(color))
                    colors.Add(color);

                data[x + y * width] = colors.IndexOf(color); ;
            }
        }

        public void Dispose()
        {
            cachedImage?.Dispose();
        }
    }
}
