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
        int zoom = 2; // default zoom is 200%
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
            openFileDialog1.FileName = "";
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
            UpdateTileset(true);

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
            openFileDialog1.FileName = "";
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
                Tileset.Create(sprite, allowTileFlippingToolStripMenuItem.Checked, out tileset, out tilemap);
            }

            // fill sizes for Tileset
            cTilesetWidth.Items.Clear();
            foreach (var size in tileset.PerfectSizes)
                cTilesetWidth.Items.Add(size.Width.ToString());

            // pick middle size
            cTilesetWidth.SelectedIndex = cTilesetWidth.Items.Count / 2;

            // finish
            UpdateTileset(true);
            UpdateTilemap();
        }

        private void editPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var d = new PaletteDialog(tilesetImage.Palette))
            {
                d.ShowDialog();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tileset == null)
                return;

            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Paintshop Palette|*.pal|Adobe Color Table|*.act|APE Palette|*.gpl";
            saveFileDialog1.Title = "Export Tileset Palette";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        tilesetImage.Palette.Save(saveFileDialog1.FileName, PaletteFormat.PAL);
                        break;
                    case 2:
                        tilesetImage.Palette.Save(saveFileDialog1.FileName, PaletteFormat.ACT);
                        break;
                    case 3:
                        tilesetImage.Palette.Save(saveFileDialog1.FileName, PaletteFormat.GPL);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (tileset == null)
            //    return;

            if (++zoom > 8)
                zoom = 8;

            UpdateTileset(false);
            UpdateTilemap();

            lZoom.Text = $"{zoom * 100}%";
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (tileset == null)
            //    return;

            if (--zoom <= 0)
                zoom = 1;

            UpdateTileset(false);
            UpdateTilemap();

            lZoom.Text = $"{zoom * 100}%";
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pTilemap.Invalidate();
            pTileset.Invalidate();
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void allowTileFlippingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkTilesetFlipX.Enabled = allowTileFlippingToolStripMenuItem.Checked;
            chkTilesetFlipY.Enabled = allowTileFlippingToolStripMenuItem.Checked;

            chkTilesetFlipX.Checked = false;
            chkTilesetFlipY.Checked = false;
        }
    }
}
