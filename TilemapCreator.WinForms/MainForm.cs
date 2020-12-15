using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TilemapCreator.Controls;
using TilemapCreator.Forms;

namespace TilemapCreator
{
    public partial class MainForm : Form
    {
        private Tileset _tileset;
        private Palette _palette;
        private TilesetImage _tilesetImage;
        private int _tilesetMouseX;
        private int _tilesetMouseY;
        private bool _tilesetIsMouseOver;
        private bool _tilesetIsMouseDown;

        public MainForm()
        {
            InitializeComponent();
            //menuStrip1.Renderer = new MyMenuRenderer();
            //toolStrip1.Renderer = new MyToolStripRenderer();
            //toolStrip2.Renderer = new MyToolStripRenderer();
            //toolStrip3.Renderer = new MyToolStripRenderer();
        }

        private void menuTilesetOpen_Click(object sender, EventArgs e)
        {
            /*
            using var dialog = new OpenFileDialog
            {
                Title = "Open Tileset...",
                Filter = "Image Files|*.bmp;*.png"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            OpenTileset(dialog.FileName);
            */

            using var dialog = new OpenTilesetDialog { Text = "Open Tileset" };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            _tileset = dialog.GetTileset();
            _palette = dialog.GetPalette();
            // ...
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            using var dialog = new AboutForm();
            dialog.ShowDialog();
        }

        private void OpenTileset(string filename)
        {
            // determine file format
            // TODO: maybe actually read the file
            var format = Path.GetExtension(filename)?.ToUpper() switch
            {
                ".BMP" => TilesetFormat.Bmp,
                ".PNG" => TilesetFormat.Png,
                //".BIN" => TilesetFormat.Gba,
                _ => throw new ArgumentException("Unsupported file format.", nameof(filename))
            };

            // load the tileset and palette
            var watch = Stopwatch.StartNew();
            Tileset tileset;
            Palette palette;
            try
            {
                (tileset, palette) = Tileset.Load(filename, format, default);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION:");
                Debug.WriteLine(ex);
                return;
            }
            finally
            {
                watch.Stop();
                Debug.WriteLine($"Tileset loaded in {watch.Elapsed.TotalSeconds} s");
            }

            Debug.Assert(tileset != null, "tileset is null");
            Debug.Assert(palette != null, "palette is null"); // it will be when gba is implemented

            // tileset loaded successfully, we can proceed
            _tileset = tileset;

            // determine valid dimensions, pick the one which often may match the original size
            comboTilesetWidth.Items.Clear();
            var tilesetSizes = tileset.GetSuggestedDimensions();
            for (int i = 0; i < tilesetSizes.Count; i++)
                comboTilesetWidth.Items.Add(tilesetSizes[i]);

            // cache tileset image
            watch.Restart();
            _tilesetImage ??= new TilesetImage();
            _tilesetImage.SetTileset(tileset, palette);
            watch.Stop();
            Debug.WriteLine($"Tileset drawn in {watch.Elapsed.TotalSeconds} s");

            // draw the tileset image
            var tilesetColors = tileset.GetColorCount();
            var tw = tilesetSizes[tilesetSizes.Count / 2];
            var th = tileset.Length / tw;

            var image = new Bitmap(tw * 8, th * 8);
            using (var fb = image.FastLock())
            {
                tileset.Draw(fb, tw, palette);
            }

            //pictureTileset.Image?.Dispose();
            pictureTileset.Image = _tilesetImage.GetImage();
        }

        private void pictureTileset_Paint(object sender, PaintEventArgs e)
        {
            if (_tilesetIsMouseOver)
            {
                e.Graphics.DrawRectangle(Pens.Yellow, _tilesetMouseX * 8, _tilesetMouseY * 8, 7, 7);
            }
        }

        private void pictureTileset_MouseDown(object sender, MouseEventArgs e)
        {
            _tilesetIsMouseDown = true;
        }

        private void pictureTileset_MouseEnter(object sender, EventArgs e)
        {
            _tilesetIsMouseOver = true;
            pictureTileset.Invalidate();
        }

        private void pictureTileset_MouseLeave(object sender, EventArgs e)
        {
            _tilesetIsMouseOver = false;
            pictureTileset.Invalidate();
        }

        private void pictureTileset_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= pictureTileset.Width || e.Y >= pictureTileset.Height)
                return;

            _tilesetMouseX = e.X / 8; // TODO: zoom factor
            _tilesetMouseY = e.Y / 8;

            pictureTileset.Invalidate();
        }

        private void pictureTileset_MouseUp(object sender, MouseEventArgs e)
        {
            _tilesetIsMouseDown = false;
        }
    }
}
