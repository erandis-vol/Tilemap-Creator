using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TilemapCreator
{
    static class ColorExtensions
    {
        public static Bgr555 ToBgr555(this Color color) => new Bgr555(color.A != 0, color.R >> 3, color.G >> 3, color.B >> 3);

        // convers to a 32-bit color with alpha channel
        public static Color ToArgb32(this Bgr555 bgr) => Color.FromArgb(bgr.A ? 255 : 0, bgr.R << 3, bgr.G << 3, bgr.B << 3);

        // converts to a 32-bit color without alpha channel
        public static Color ToRgb32(this Bgr555 bgr) => Color.FromArgb(bgr.R << 3, bgr.G << 3, bgr.B << 3);
    }
}
