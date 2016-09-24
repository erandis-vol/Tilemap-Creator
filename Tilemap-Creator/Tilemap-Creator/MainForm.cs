using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TMC
{
    public partial class MainForm : Form
    {
        int zoom = 2; // default zoom is 200%
        bool ignore = false;

        Color gridColor = Color.FromArgb(128, Color.White);
        Color gridColorS = Color.FromArgb(128, Color.Yellow);

        // TODO: remember information about last tileset/tilemap

        public MainForm()
        {
            InitializeComponent();

            mnuOpenTilemap.Enabled = false;
            mnuSaveTilemap.Enabled = false;
            mnuSaveTileset.Enabled = false;
            mnuPalette.Enabled = false;
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

        private void mnuNewTilemap_Click(object sender, EventArgs e)
        {
            if (tileset == null)
                return;

            tilemap = new Tilemap(30, 20);
            UpdateTilemap();
        }

        private void mnuOpenTilemap_Click(object sender, EventArgs e)
        {
            if (tileset == null)
                return;

            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open Tilemap";
            openFileDialog1.Filter = "GBA Raw Tilemap|*.raw;*.bin";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            using (var oa = new OpenTilemapDialog(openFileDialog1.FileName))
            {
                if (oa.ShowDialog() != DialogResult.OK)
                    return;

                tilemap = new Tilemap(oa.File, oa.Format, oa.FriendlySize.Width);
                UpdateTilemap();
            }
        }

        private void mnuSaveTilemap_Click(object sender, EventArgs e)
        {
            if (tileset == null || tilemap == null)
                return;

            // --------------------------------
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Save Tilemap";
            saveFileDialog1.Filter = "GBA Raw Tilemap|*.raw;*bin";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            // --------------------------------
            using (var sa = new SaveTilemapDialog(saveFileDialog1.FileName))
            {
                if (sa.ShowDialog() != DialogResult.OK)
                    return;

                tilemap.Save(sa.File, sa.Format, sa.ExtraBytes);
            }
        }

        private void mnuOpenTileset_Click(object sender, EventArgs e)
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

            mnuOpenTilemap.Enabled = true;
            mnuSaveTilemap.Enabled = true;
            mnuSaveTileset.Enabled = true;
            mnuPalette.Enabled = true;
        }

        private void mnuSaveTileset_Click(object sender, EventArgs e)
        {
            if (tileset == null || tilesetImage == null) return;

            saveFileDialog1.Title = "Save Tileset";
            saveFileDialog1.Filter = "Bitmaps|*.bmp"; //|Binary|*.bin";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            switch (saveFileDialog1.FilterIndex)
            {
                case 1:
                    tilesetImage.Save(saveFileDialog1.FileName, SpriteFormat.BMP);
                    break;
                //case 2:
                //    tilesetImage.Save(saveFileDialog1.FileName, SpriteFormat.GBA);
                //    break;
            }
        }

        private void mnuCreateTileset_Click(object sender, EventArgs e)
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
                Tileset.Create(sprite, mnuAllowFlipping.Checked, out tileset, out tilemap);
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

            mnuOpenTilemap.Enabled = true;
            mnuSaveTilemap.Enabled = true;
            mnuSaveTileset.Enabled = true;
            mnuPalette.Enabled = true;
        }

        private void mnuEditPalette_Click(object sender, EventArgs e)
        {
            if (tileset == null)
                return;

            using (var d = new PaletteDialog(tilesetImage.Palette))
            {
                if (d.ShowDialog() != DialogResult.OK)
                    return;

                // update palette for every tile
                for (int t = 0; t < tileset.Size; t++)
                {
                    tileset[t].Lock();
                    tileset[t].ReplacePalette(d.Palette);
                    tileset[t].Unlock();
                }

                // redraw tileset
                UpdateTileset(false);
                UpdateTilemap();
            }
        }

        private void mnuRearrangePalette_Click(object sender, EventArgs e)
        {
            if (tileset == null)
                return;

            using (var d = new RearrangePaletteDialog(tilesetImage.Palette))
            {
                if (d.ShowDialog() != DialogResult.OK)
                    return;

                // replace palette for every tile
                for (int t = 0; t < tileset.Size; t++)
                {
                    tileset[t].Lock();
                    tileset[t].RearrangePalette(d.Palette);
                    tileset[t].Unlock();
                }

                // redraw tileset
                UpdateTileset(false);
            }
        }

        private void mnuExportPalette_Click(object sender, EventArgs e)
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
                        Palette.Save(tilesetImage.Palette, saveFileDialog1.FileName, PaletteFormat.PAL);
                        break;
                    case 2:
                        Palette.Save(tilesetImage.Palette, saveFileDialog1.FileName, PaletteFormat.ACT);
                        break;
                    case 3:
                        Palette.Save(tilesetImage.Palette, saveFileDialog1.FileName, PaletteFormat.GPL);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuZoomIn_Click(object sender, EventArgs e)
        {
            //if (tileset == null)
            //    return;

            if (++zoom > 8)
                zoom = 8;

            UpdateTileset(false);
            UpdateTilemap();

            lZoom.Text = $"Zoom: {zoom * 100}%";
        }

        private void mnuZoomOut_Click(object sender, EventArgs e)
        {
            //if (tileset == null)
            //    return;

            if (--zoom <= 0)
                zoom = 1;

            UpdateTileset(false);
            UpdateTilemap();

            lZoom.Text = $"Zoom: {zoom * 100}%";
        }

        private void mnuGrid_Click(object sender, EventArgs e)
        {
            pTilemap.Invalidate();
            pTileset.Invalidate();
        }

        private void mnuStatusBar_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = mnuStatusBar.Checked;
        }

        private void mnuAllowFlip_Click(object sender, EventArgs e)
        {
            chkTilesetFlipX.Enabled = mnuAllowFlipping.Checked;
            chkTilesetFlipY.Enabled = mnuAllowFlipping.Checked;

            chkTilesetFlipX.Checked = false;
            chkTilesetFlipY.Checked = false;
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            using (var a = new AboutDialog())
                a.ShowDialog();
        }
    }
}
