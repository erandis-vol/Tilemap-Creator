using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilemapCreator
{
    public partial class MainForm : Form
    {
        //private PinnedBitmap _tilesetImage;

        public MainForm()
        {
            InitializeComponent();
        }

        private void menuTilesetOpen_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Open Tileset...",
                Filter = "Image Files|*.bmp;*.png;*.jpg"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            LoadBitmap(dialog.FileName);
        }

        private void LoadBitmap(string filename)
        {
            using var bmp = new Bitmap(filename);
            if (bmp.Width < 8 || bmp.Height < 8)
                throw new ArgumentException("Image must be at least 8x8 pixels.", nameof(filename));

            var watch = Stopwatch.StartNew();

            /*
            // initialize palette from image
            // TODO: create palette at same time as tiles
            Palette palette;
            if ((bmp.PixelFormat & PixelFormat.Indexed) != PixelFormat.Indexed)
            {
                var colors = new List<Bgr555>();

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        var color = bmp.GetPixel(x, y).ToBgr555();
                        if (!colors.Contains(color))
                            colors.Add(color);
                    }
                }

                palette = new Palette(colors);
            }
            else
            {
                // this will result in loss of color in some images
                // this should be expected
                palette = new Palette(bmp.Palette.Entries.Select(x => x.ToBgr555()));
            }
            */

            // initialize color palette, copying existing palette if possible
            var colors = new List<Bgr555>();
            var isIndexed = (bmp.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed;
            if (isIndexed)
            {
                Debug.WriteLine("Image is indexed, copying palette.");
                colors.AddRange(bmp.Palette.Entries.Select(x => x.ToBgr555()));
            }

            // initialize tiles from image
            var tw = bmp.Width / 8;
            var th = bmp.Height / 8;
            //var tiles = new List<Tile>(tileWidth * tileHeight);
            var tileData = new int[tw * th * 64]; // store the entire tileset as an array of pixel data... :O

            // only clone the image when necessary
            Bitmap clone = null;
            if (Image.GetPixelFormatSize(bmp.PixelFormat) != 32)
                clone = bmp.ChangeFormat(PixelFormat.Format32bppPArgb);

            //var fb1 = new FastBitmap(bmp);
            //fb1.Lock(ImageLockMode.ReadOnly);
            //using var pb1 = new PinnedBitmap(bmp);
            using (var fb1 = (clone ?? bmp).FastLock())
            {
                // we transform the pixel data from standard row-order to tiles as the console views it
                int i = 0;
                for (int y = 0; y < th; y++)
                {
                    for (int x = 0; x < tw; x++)
                    {
                        //Span<int> pixels = stackalloc int[64]; // 1024 or 512 bytes is a good max limit, this is only 256
                        //var tile = new Tile();
                        for (int ty = 0; ty < 8; ty++)
                        {
                            for (int tx = 0; tx < 8; tx++)
                            {
                                var color = fb1.GetPixel(tx + x * 8, ty + y * 8).ToBgr555();
                                var colorIndex = colors.IndexOf(color);
                                if (colorIndex < 0)
                                {
                                    Debug.WriteLineIf(isIndexed, "Warning: Failed to find color in copied palette.");
                                    colors.Add(color);
                                    colorIndex = colors.Count - 1;
                                }
                                //pixels[j * 8 + i] = index;
                                //tile[i, j] = index;
                                tileData[i++] = colorIndex;
                            }
                        }
                        //tiles.Add(new Tile(pixels));
                        //tiles.Add(tile);
                    }
                }
            }

            clone?.Dispose();

            var palette = new Palette(colors);
            var tileset = new Tileset(tileData);
            var tilesetColors = tileset.GetColorCount();
            var tilesetSizes = tileset.GetSuggestedDimensions();

            var image = new Bitmap(tw * 8, th * 8);
            //_tilesetImage?.Dispose();
            //_tilesetImage = new PinnedBitmap(tileWidth * 8, tileHeight * 8);

            using (var fb2 = image.FastLock())
            {
                //// to draw it, we transform the data back
                //for (int y = 0; y < th; y++)
                //{
                //    for (int x = 0; x < tw; x++)
                //    {
                //        //var tile = tiles[x + y * tw];
                //        var tile0 = (x + y * tw) * 64;
                //        for (int ty = 0; ty < 8; ty++)
                //        {
                //            for (int tx = 0; tx < 8; tx++)
                //            {
                //                //var index = tile[tx, ty];
                //                var colorIndex = tileData[tile0 + tx + ty * 8];
                //                var color = palette[colorIndex];
                //                //image.SetPixel(x * 8 + tx, y * 8 + ty, color.ToRgb32());
                //                fb2.SetPixel(tx + x * 8, ty + y * 8, color.ToRgb32());
                //            }
                //        }
                //    }
                //}

                tileset.Draw(fb2, tw, palette);
            }

            watch.Stop();
            Debug.WriteLine($"Image loaded in: {watch.Elapsed.TotalSeconds} s");

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = image;
        }
    }
}
