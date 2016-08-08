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

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tileset?.Dispose();

            tilesetImage?.Dispose();
            tilemapImage?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // test creating a Tilemap/Tileset
            try
            {
                using (var bmp = new Bitmap("heiwa map.png"))
                using (var spr = new Sprite(bmp))
                {
                    // create tileset from image
                    Tileset.Create(spr, false, out tileset, out tilemap);
                    Console.WriteLine("tileset size: {0}, original size: {1}", tileset.Size, tilemap.Width * tilemap.Height);

                    // render tileset
                    tilesetImage = tileset.Smoosh(64);

                    pTileset.Size = new Size(tilesetImage.Width * 2, tilesetImage.Height * 2);
                    pTileset.Image = tilesetImage;

                    // render tilemap
                    tilemapImage = new Bitmap(tilemap.Width * Tileset.TileSize, tilemap.Height * Tileset.TileSize);
                    using (var g = Graphics.FromImage(tilemapImage))
                    {
                        for (int y = 0; y < tilemap.Height; y++)
                        {
                            for (int x = 0; x < tilemap.Width; x++)
                            {
                                g.DrawImage(tileset[tilemap[x, y].TilesetIndex], x * Tileset.TileSize, y * Tileset.TileSize);
                            }
                        }
                    }

                    pTilemap.Size = new Size(tilemapImage.Width * 2, tilemapImage.Height * 2);
                    pTilemap.Image = tilemapImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tilesetImage?.Save("test.bmp", SpriteFormat.BMP);
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

            Console.WriteLine("updating Tileset");

            // get Tileset size
            int width;
            if (!int.TryParse(cTilesetWidth.Text, out width))
                width = 1;

            int height = (tileset.Size / width) + (tileset.Size % width > 0 ? 1 : 0);

            // update height text
            tTilesetHeight.Value = height;

            // update Tileset image
            tilesetImage?.Dispose();
            tilesetImage = tileset.Smoosh(width);

            pTileset.Size = new Size(tilesetImage.Width * zoom, tilesetImage.Height * zoom);
            pTileset.Image = tilesetImage;
            ignore = false;
        }

        // updates Tilemap image (forced redraw)
        void UpdateTilemap()
        {
            if (tilemap == null || tileset == null) return;
            ignore = true;

            Console.WriteLine("updating Tilemap");

            // set size info
            tTilemapWidth.Value = tilemap.Width;
            tTilemapHeight.Value = tilemap.Height;

            // recreate Tilemap image
            tilemapImage?.Dispose();
            tilemapImage = new Bitmap(tilemap.Width * Tileset.TileSize, tilemap.Height * Tileset.TileSize);

            // render Tilemap fully
            using (var g = Graphics.FromImage(tilemapImage))
            {
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
            }

            // TODO: render Palettemap fully

            // finished
            pTilemap.Size = new Size(tilemapImage.Width * zoom, tilemapImage.Height * zoom);
            pTilemap.Image = tilemapImage;
            ignore = false;
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

        private void bResizeTilemap_Click(object sender, EventArgs e)
        {
            
        }
    }
}
