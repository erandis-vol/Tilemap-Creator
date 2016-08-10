using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMC
{
    public partial class MainForm : Form
    {
        Tilemap tilemap;
        Tileset tileset;
        
        Sprite tilesetImage;
        Bitmap tilemapImage;
        Color[] palettemapColors = new Color[]
        {
            Color.FromArgb(128, Color.White),
            Color.FromArgb(128, Color.Yellow),
            Color.FromArgb(128, Color.Red),
            Color.FromArgb(128, Color.Gray),
            Color.FromArgb(128, Color.Cyan),
            Color.FromArgb(128, Color.Blue),
            Color.FromArgb(128, Color.Magenta),
            Color.FromArgb(128, Color.LightYellow),
            Color.FromArgb(128, Color.Teal),
            Color.FromArgb(128, Color.LightSteelBlue),
            Color.FromArgb(128, Color.Violet),
            Color.FromArgb(128, Color.Orange),
            Color.FromArgb(128, Color.LightGray),
            Color.FromArgb(128, Color.SandyBrown),
            Color.FromArgb(128, Color.Purple),
            Color.FromArgb(128, Color.LightPink),
        };
        Bitmap palettesetImage;

        int zoom = 3; // default zoom is 200%
        bool ignore = false;

        Color gridColor = Color.FromArgb(128, Color.White);
        Color gridColorS = Color.FromArgb(128, Color.Yellow);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DrawPalette();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tileset?.Dispose();

            tilesetImage?.Dispose();
            tilemapImage?.Dispose();
            palettesetImage?.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: ask to save old Tileset/Tilemap

            openFileDialog1.Title = "Open Tileset";
            openFileDialog1.Filter = "Images|*.bmp;*.png;*.jpg";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            // create new Tileset
            using (var bmp = new Bitmap(openFileDialog1.FileName))
            using (var sprite = new Sprite(bmp))
            {
                // sprite dimensions must be divisible by 8
                if (sprite.Width % 8 != 0 || sprite.Height % 8 != 0)
                {
                    MessageBox.Show("Tileset source Sprite dimensions are not divisible by 8!", "Invalid Sprite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tileset from sprite
                tileset?.Dispose();
                tileset = new Tileset(sprite);
            }

            // fill sizes for Tileset
            cTilesetWidth.Items.Clear();
            foreach (var size in tileset.PerfectSizes)
                cTilesetWidth.Items.Add(size.Width.ToString());

            // pick middle size
            cTilesetWidth.SelectedIndex = cTilesetWidth.Items.Count / 2;

            // finish
            UpdateTileset();

            // create new blank Tilemap
            tilemap = new Tilemap(30, 20);
            UpdateTilemap();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tileset == null || tilesetImage == null) return;

            saveFileDialog1.Title = "Save Tileset";
            saveFileDialog1.Filter = "Bitmaps|*.bmp";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            // save Tileset as .bmp (forced for now)
            tilesetImage.Save(saveFileDialog1.FileName, SpriteFormat.BMP);
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: ask to save Tilemap/Tileset

            openFileDialog1.Title = "Create Tileset";
            openFileDialog1.Filter = "Images|*.bmp;*.png;*.jpg";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            using (var bmp = new Bitmap(openFileDialog1.FileName))
            using (var sprite = new Sprite(bmp))
            {
                // Sprite dimensions must be divisible by 8
                if (sprite.Width % 8 != 0 || sprite.Height % 8 != 0)
                {
                    MessageBox.Show("Tileset source image dimensions are not divisible by 8!", "Invalid Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // create Tilemap/Tileset
                tileset?.Dispose();
                Tileset.Create(sprite, true, out tileset, out tilemap);
            }

            // fill sizes for Tileset
            cTilesetWidth.Items.Clear();
            foreach (var size in tileset.PerfectSizes)
                cTilesetWidth.Items.Add(size.Width.ToString());

            // pick middle size
            cTilesetWidth.SelectedIndex = cTilesetWidth.Items.Count / 2;

            // finish
            UpdateTileset();
            UpdateTilemap();
        }

        // updates Tileset size and image
        void UpdateTileset()
        {
            if (tileset == null) return;

            ignore = true;
            if (rModeTilemap.Checked)
            {

                // get Tileset size
                int width = cTilesetWidth.Value;
                if (width <= 0) width = 1;

                int height = (tileset.Size / width) + (tileset.Size % width > 0 ? 1 : 0);

                // update height text
                tTilesetHeight.Value = height;

                // update Tileset image
                tilesetImage?.Dispose();
                tilesetImage = tileset.Smoosh(width);

                pTileset.Size = new Size(tilesetImage.Width * zoom, tilesetImage.Height * zoom);
                pTileset.Image = tilesetImage;
            }
            else
            {
                pTileset.Size = new Size(palettesetImage.Width * zoom, palettesetImage.Height * zoom);
                pTileset.Image = palettesetImage;
            }
            ignore = false;
        }

        // updates Tilemap image (forced redraw)
        void UpdateTilemap()
        {
            if (tilemap == null || tileset == null) return;
            ignore = true;

            // set size info
            tTilemapWidth.Value = tilemap.Width;
            tTilemapHeight.Value = tilemap.Height;

            // draw tilemap image :D
            DrawTilemap();

            // finished
            pTilemap.Size = new Size(tilemapImage.Width * zoom, tilemapImage.Height * zoom);
            pTilemap.Image = tilemapImage;
            ignore = false;
        }

        void DrawTilemap()
        {
            // recreate Tilemap image
            tilemapImage?.Dispose();
            tilemapImage = new Bitmap(tilemap.Width * Tileset.TileSize, tilemap.Height * Tileset.TileSize);

            // render Tilemap fully
            using (var g = Graphics.FromImage(tilemapImage))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                for (int y = 0; y < tilemap.Height; y++)
                {
                    for (int x = 0; x < tilemap.Width; x++)
                    {
                        var tile = tilemap[x, y];
                        g.DrawImageFlipped(tileset[tile.TilesetIndex],
                            x * Tileset.TileSize, y * Tileset.TileSize,
                            tile.FlipX, tile.FlipY);
                    }
                }

                // render Palettemap on top
                if (rModePalette.Checked)
                {
                    var brushes = new SolidBrush[16];
                    for (int i = 0; i < 16; i++)
                        brushes[i] = new SolidBrush(palettemapColors[i]);
                    var font = new Font("Arial", 5f, FontStyle.Regular);

                    for (int y = 0; y < tilemap.Height; y++)
                    {
                        for (int x = 0; x < tilemap.Width; x++)
                        {
                            g.FillRectangle(brushes[tilemap[x, y].PaletteIndex],
                                x * Tileset.TileSize, y * Tileset.TileSize,
                                Tileset.TileSize, Tileset.TileSize);

                            g.DrawString(tilemap[x, y].PaletteIndex.ToString("x"), font,
                                Brushes.Black, 1 + x * Tileset.TileSize, 1 + y * Tileset.TileSize);
                        }
                    }

                    for (int i = 0; i < 16; i++)
                        brushes[i].Dispose();
                    font.Dispose();
                }
            }
        }

        void DrawPalette()
        {
            palettesetImage?.Dispose();
            palettesetImage = new Bitmap(4 * 8, 4 * 8);

            using (var g = Graphics.FromImage(palettesetImage))
            using (var font = new Font("Arial", 5.5f, FontStyle.Regular))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                for (int i = 0; i < 16; i++)
                {
                    using (var b = new SolidBrush(palettemapColors[i]))
                    {
                        g.FillRectangle(b, i % 4 * 8, i / 4 * 8, 8, 8);
                        g.DrawString(i.ToString("X"), font, Brushes.Black, 1 + i % 4 * 8, i / 4 * 8);
                    }
                }
            }
        }

        private void pTileset_Paint(object sender, PaintEventArgs e)
        {
            if (tileset == null) return;

            // draw grid
            using (var pen = new Pen(new SolidBrush(gridColor), 1f))
            {
                pen.DashPattern = new[] { 2f, 2f };

                int f = zoom * Tileset.TileSize;

                for (int x = 1; x < pTileset.Width / f; x++)
                {
                    e.Graphics.DrawLine(pen, x * f, 0, x * f, pTileset.Height);
                }

                for (int y = 1; y < pTileset.Height / f; y++)
                {
                    e.Graphics.DrawLine(pen, 0, y * f, pTileset.Width, y * f);
                }
            }
        }

        private void pTilemap_Paint(object sender, PaintEventArgs e)
        {
            if (tileset == null || tilemap == null) return;

            // draw grid
            using (var pen = new Pen(new SolidBrush(gridColor), 1f))
            using (var penS = new Pen(new SolidBrush(gridColorS), 1f))
            {
                pen.DashPattern = new[] { 2f, 2f };
                penS.DashPattern = new[] { 2f, 2f };

                int f = zoom * Tileset.TileSize;

                for (int x = 1; x < pTilemap.Width / f; x++)
                {
                    e.Graphics.DrawLine(x % 30 == 0 ? penS : pen, x * f, 0, x * f, pTilemap.Height);
                }

                for (int y = 1; y < pTilemap.Height / f; y++)
                {
                    e.Graphics.DrawLine(y % 20 == 0 ? penS : pen, 0, y * f, pTilemap.Width, y * f);
                }
            }
        }

        private void rMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ignore) return;

            // TODO: change selection
            UpdateTileset();
            UpdateTilemap();
        }
    }
}
