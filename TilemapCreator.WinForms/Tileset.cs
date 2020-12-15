using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace TilemapCreator
{
    public partial class Tileset
    {
        private static (Tileset Tileset, Palette Palette) LoadBmp(Stream stream) => LoadBitmap(stream);

        private static (Tileset Tileset, Palette Palette) LoadPng(Stream stream) => LoadBitmap(stream);

        private static (Tileset Tileset, Palette Palette) LoadBitmap(Stream stream)
        {
            using var source = new Bitmap(stream);
            if (source.Width < 8 || source.Height < 8)
                throw new InvalidDataException("Image must be at least 8x8 pixels.");

            // initialize color palette, copying existing palette if possible
            var colors = new List<Bgr555>();
            var isIndexed = (source.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed;
            if (isIndexed)
            {
                Debug.WriteLine("Image is indexed, copying palette.");
                colors.AddRange(source.Palette.Entries.Select(x => x.ToBgr555()));
            }

            // initialize tileset data
            var tw = source.Width / 8;
            var th = source.Height / 8;
            var tdata = new int[tw * th * 64];

            // ensure bitmap is in a workable format
            var clone = default(Bitmap);
            if (Image.GetPixelFormatSize(source.PixelFormat) != 32)
            {
                Debug.WriteLine("Cloning image data to 32 bpp.");
                clone = source.ChangeFormat(PixelFormat.Format32bppPArgb);
            }

            try
            {
                // transform the pixel data from standard row-order to tiles as the console views it
                using var fb = (clone ?? source).FastLock();
                int i = 0;
                for (int y = 0; y < th; y++)
                {
                    for (int x = 0; x < tw; x++)
                    {
                        for (int ty = 0; ty < 8; ty++)
                        {
                            for (int tx = 0; tx < 8; tx++)
                            {
                                var color = fb.GetPixel(tx + x * 8, ty + y * 8).ToBgr555();
                                var colorIndex = colors.IndexOf(color);
                                if (colorIndex < 0)
                                {
                                    Debug.WriteLineIf(isIndexed, "Warning: Failed to find color in copied palette.");
                                    colors.Add(color);
                                    colorIndex = colors.Count - 1;
                                }
                                tdata[i++] = colorIndex;
                            }
                        }
                    }
                }
            }
            finally
            {
                // we no longer need to hold onto the cloned image
                clone?.Dispose();
            }

            return (new Tileset(tdata), new Palette(colors));
        }
    }
}
