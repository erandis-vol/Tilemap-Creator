using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private const int TileSize = Tileset.TileSize;
        private const int DefaultTilemapWidth = 30; // gba screen size
        private const int DefaultTilemapHeight = 20;

        private bool _ignore;
        private Tileset _tileset;
        private Palette _palette;
        private TilesetImage _tilesetImage;

        private int _tilesetColumns;
        private Bitmap _tilesetImg;
        private Bitmap _tilesetImgZoomed; // tileset image with scaling applied

        private int _tilesetMouseX;
        private int _tilesetMouseY;
        private bool _tilesetIsMouseOver;
        private bool _tilesetIsMouseDown;

        private Tilemap _tilemap;
        private Bitmap _tilemapImgZoomed;

        private int _zoom = 3; // current zoom, not interested in going below 100%

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
            using var dialog = new OpenTilesetDialog { Text = "Open Tileset" };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var tileset = dialog.GetTileset();
            var palette = dialog.GetPalette();
            if (tileset is null || palette is null)
            {
                // dialog should ensure both are non-null before selecting
                Debug.WriteLine(tileset is null ? "Tileset is null, canceling load." : "Palette is null, canceling load.");
                return;
            }

            _tileset = tileset;
            _palette = palette;

            // clear tilemap
            _tilemap = new Tilemap(DefaultTilemapWidth, DefaultTilemapHeight);
            FillTilemapWithRandomTiles();

            // ignore UI updates
            _ignore = true;

            // determine suggested tileset sizes
            comboTilesetWidth.Items.Clear();
            var tilesetSizes = tileset.GetSuggestedDimensions();
            for (int i = 0; i < tilesetSizes.Length; i++)
                comboTilesetWidth.Items.Add(tilesetSizes[i]);

            // tileset images are typically square, so the middle size is often closest
            comboTilesetWidth.SelectedIndex = tilesetSizes.Length / 2;
            _ignore = false;

            // update images
            RefreshTilesetImage();
            RefreshPaletteImage();
            RefreshTilemapImage();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            using var dialog = new AboutForm();
            dialog.ShowDialog();
        }

        /*
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
            for (int i = 0; i < tilesetSizes.Length; i++)
                comboTilesetWidth.Items.Add(tilesetSizes[i]);

            // cache tileset image
            watch.Restart();
            _tilesetImage ??= new TilesetImage();
            _tilesetImage.SetTileset(tileset, palette);
            watch.Stop();
            Debug.WriteLine($"Tileset drawn in {watch.Elapsed.TotalSeconds} s");

            // draw the tileset image
            var tilesetColors = tileset.GetColorCount();
            var tw = tilesetSizes[tilesetSizes.Length / 2];
            var th = tileset.Length / tw;

            var image = new Bitmap(tw * 8, th * 8);
            using (var fb = image.FastLock())
            {
                tileset.Draw(fb, tw, palette);
            }

            //pictureTileset.Image?.Dispose();
            pictureTileset.Image = _tilesetImage.GetImage();
        }
        */

        private void FillTilemapWithRandomTiles()
        {
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                var x = random.Next(0, _tilemap.Width);
                var y = random.Next(0, _tilemap.Height);
                var tile = random.Next(0, _tileset.Length);
                _tilemap.SetTile(x, y, tile, false, false);
            }
        }

        // Forces the current tileset image to be redrawn.
        private void RefreshTilesetImage()
        {
            Debug.Assert(_tileset != null);
            Debug.Assert(_palette != null);
            Debug.Assert(_tilesetColumns >= 0);

            // determine image size
            var columns = _tilesetColumns;
            var rows = Math.Max(1, _tileset.Length / columns);
            if (rows * columns != _tileset.Length)
                rows++; // NOTE: textbox updated elsewhere

            // render tileset image
            var img = new Bitmap(columns * TileSize, rows * TileSize);
            using (var fbd = img.FastLock())
                _tileset.Draw(fbd, columns, _palette);
            _tilesetImg?.Dispose();
            _tilesetImg = img;

            // render tileset zoomed
            if (_zoom > 1)
            {
                var tilesetWidthZoomed = _tilesetImg.Width * _zoom;
                var tilesetHeightZoomed = _tilesetImg.Height * _zoom;

                _tilesetImgZoomed?.Dispose();
                _tilesetImgZoomed = new Bitmap(tilesetWidthZoomed, tilesetHeightZoomed);

                using var graphics = Graphics.FromImage(_tilesetImgZoomed);
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.DrawImage(_tilesetImg, 0, 0, tilesetWidthZoomed, tilesetHeightZoomed);
                //graphics.DrawImage(
                //    _tilesetImg,
                //    new Rectangle(0, 0, tilesetWidthZoomed, tilesetHeightZoomed),
                //    new Rectangle(0, 0, _tilesetImg.Width, _tilesetImg.Height),
                //    GraphicsUnit.Pixel);
            }
            else
            {
                _tilesetImgZoomed?.Dispose();
                _tilesetImgZoomed = _tilesetImg;
            }

            pictureTileset.Image = _tilesetImgZoomed;
        }

        // Forces the current palette image to be redrawn.
        private void RefreshPaletteImage()
        {
        }

        // Forces the current tilemap image to be redrawn.
        private void RefreshTilemapImage()
        {
            Debug.Assert(_tilemap != null);
            RefreshTilemapImage(0, 0, _tilemap.Width, _tilemap.Height);
        }

        // Forces part of the current tilemap image to be redrawn.
        private void RefreshTilemapImage(int boundsX, int boundsY, int boundsW, int boundsH)
        {
            Debug.Assert(_tileset != null);
            Debug.Assert(_palette != null);
            Debug.Assert(_tilesetImgZoomed != null); // we only draw the tilemap zoomed
            Debug.Assert(_tilemap != null);

            Debug.Assert(boundsX >= 0 && boundsX < _tilemap.Width);
            Debug.Assert(boundsY >= 0 && boundsY < _tilemap.Height);
            Debug.Assert(boundsW > 0 && boundsW + boundsX <= _tilemap.Width);
            Debug.Assert(boundsH > 0 && boundsH + boundsY <= _tilemap.Height);

            var tileSizeZoomed = TileSize * _zoom;

            // ensure tilemap image exists and is zoomed properly
            var tilemapPixelW = _tilemap.Width * tileSizeZoomed;
            var tilemapPixelH = _tilemap.Height * tileSizeZoomed;
            var changedImg = false;
            if (_tilemapImgZoomed is null ||
                _tilemapImgZoomed.Width != tilemapPixelW ||
                _tilemapImgZoomed.Height != tilemapPixelH)
            {
                _tilemapImgZoomed?.Dispose();
                _tilemapImgZoomed = new Bitmap(tilemapPixelW, tilemapPixelH);
                changedImg = true;

                // if we had to recreate the tilemap image, render the entire tilemap
                boundsX = 0;
                boundsY = 0;
                boundsW = _tilemap.Width;
                boundsH = _tilemap.Height;
            }

            // tile destination points, reused for each draw call
            var destPoints = new Point[3]; // upper left, upper right, lower left
            var srcRect = new Rectangle(0, 0, TileSize, TileSize);
            var tilesetColumns = _tilesetImg.Width / TileSize;

            // draw tilemap image
            using var graphics = Graphics.FromImage(_tilemapImgZoomed);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            var boundsYH = boundsY + boundsH;
            var boundsXW = boundsX + boundsW;
            for (int tileY = boundsY; tileY < boundsYH; tileY++)
            {
                for (int tileX = boundsX; tileX < boundsXW; tileX++)
                {
                    ref var tile = ref _tilemap[tileX, tileY];

                    // if index outside tileset, use tile 0
                    var index = tile.Tile;
                    if (index >= _tileset.Length)
                        index = 0;

                    // tile image source rectangle
                    srcRect.X = (index % tilesetColumns) * TileSize;
                    srcRect.Y = (index / tilesetColumns) * TileSize;

                    // tile destination points
                    var destX = tileX * tileSizeZoomed;
                    var destY = tileY * tileSizeZoomed;

                    if (tile.FlipX)
                    {
                        // flip x axis
                        destPoints[0].X = destX + tileSizeZoomed;
                        destPoints[1].X = destX;
                        destPoints[2].X = destX + tileSizeZoomed;
                    }
                    else
                    {
                        // normal x axis
                        destPoints[0].X = destX;
                        destPoints[1].X = destX + tileSizeZoomed;
                        destPoints[2].X = destX;
                    }

                    if (tile.FlipY)
                    {
                        // flip y axis
                        destPoints[0].Y = destY + tileSizeZoomed;
                        destPoints[1].Y = destY + tileSizeZoomed;
                        destPoints[2].Y = destY;
                    }
                    else
                    {
                        // normal y axis
                        destPoints[0].Y = destY;
                        destPoints[1].Y = destY;
                        destPoints[2].Y = destY + tileSizeZoomed;
                    }

                    // draw tile image
                    //graphics.DrawImage(_tilesetImg, destPoints, srcRect, GraphicsUnit.Pixel);
                    graphics.DrawImage(_tilesetImg, new Rectangle(destX, destY, tileSizeZoomed, tileSizeZoomed), srcRect, GraphicsUnit.Pixel);
                }
            }

            if (changedImg)
            {
                pictureTilemap.Image = _tilemapImgZoomed;
            }
            else
            {
                // just the region redrawn
                pictureTilemap.Invalidate(new Rectangle(
                    boundsX * tileSizeZoomed,
                    boundsY * tileSizeZoomed,
                    boundsW * tileSizeZoomed,
                    boundsH * tileSizeZoomed));
            }
        }

        private void pictureTileset_Paint(object sender, PaintEventArgs e)
        {
            if (_tilesetIsMouseOver)
            {
                if (_zoom > 1)
                {
                    e.Graphics.DrawRectangle(Pens.Yellow,
                        _tilesetMouseX * TileSize * _zoom - 0.5f,
                        _tilesetMouseY * TileSize * _zoom - 0.5f,
                        TileSize * _zoom - 1,
                        TileSize * _zoom - 1);
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Yellow, _tilesetMouseX * TileSize, _tilesetMouseY * TileSize, TileSize - 1, TileSize - 1);
                }
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

            _tilesetMouseX = e.X / (TileSize * _zoom);
            _tilesetMouseY = e.Y / (TileSize * _zoom);

            pictureTileset.Invalidate();
        }

        private void pictureTileset_MouseUp(object sender, MouseEventArgs e)
        {
            _tilesetIsMouseDown = false;
        }

        private void comboTilesetWidth_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboTilesetWidth.Text, out var columns) && columns > 0 && _tilesetColumns != columns)
            {
                _tilesetColumns = columns;

                // if possible, we always want to update the height preview text
                if (_tileset != null)
                {
                    var rows = Math.Max(1, _tileset.Length / columns);
                    if (rows * columns != _tileset.Length)
                        rows++;
                    textTilesetHeight.Text = rows.ToString();
                }
                else
                {
                    textTilesetHeight.Text = "1";
                }

                // unless we're loading the tileset, we should also refresh the image
                if (!_ignore && _tileset != null)
                {
                    RefreshTilesetImage();
                }
            }
        }
    }
}
