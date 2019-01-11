using System.Drawing;

namespace TMC.Core
{
    public static class ColorExtensions
    {
        public static Color Quantize(this Color color)
        {
            return Color.FromArgb(
                (color.R >> 3) << 3,
                (color.G >> 3) << 3,
                (color.B >> 3) << 3
            );
        }

        public static ushort ToBgr555(this Color color)
        {
            return (ushort)((color.R >> 3) | ((color.G >> 3) << 5) | ((color.B >> 3) << 10));
        }
    }
}
