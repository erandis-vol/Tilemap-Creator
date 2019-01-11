using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TMC.Core;
using TMC.Forms;

namespace TMC
{
    public partial class MainForm : Form
    {
        private readonly static Color GridColor = Color.FromArgb(128, Color.White);
        private readonly static Color GridSelectionColor = Color.FromArgb(128, Color.Yellow);

        private bool ignore = false;
        private int zoom = 2; // default zoom is 200%

        private TilesetFileOptions tilesetFileOptions = null;
        private TilemapFileOptions tilemapFileOptions = null;

        // TODO: remember information about last tileset/tilemap

        public MainForm()
        {
            InitializeComponent();

            mnuSaveTileset.Enabled = false;
            mnuSaveTilesetAs.Enabled = false;
            mnuPalette.Enabled = false;

            mnuOpenTilemap.Enabled = false;
            mnuSaveTilemap.Enabled = false;
            mnuSaveTilemapAs.Enabled = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DrawPalette();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tilesetImage?.Dispose();
            tilemapImage?.Dispose();
            palettesetImage?.Dispose();
        }

        private void mnuNewTilemap_Click(object sender, EventArgs e)
        {
            if (tileset == null)
                return;

            tilemap = new Tilemap(30, 20);
            tilemapFileOptions = null;

            UpdateTilemap();
        }

        private void mnuOpenTilemap_Click(object sender, EventArgs e)
        {
            if (tileset == null)
                return;

            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open Tilemap";
            openFileDialog1.Filter = "Tilemap Files|*.raw;*.bin";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            using (var oa = new OpenTilemapDialog(openFileDialog1.FileName))
            {
                if (oa.ShowDialog() != DialogResult.OK)
                    return;

                tilemap = new Tilemap(oa.File, oa.Format, oa.FriendlySize.Width);
                tilemapFileOptions = new TilemapFileOptions {
                    FileName = oa.File,
                    Format = oa.Format,
                    Padding = 0 // TODO: We should detect this 
                };

                UpdateTilemap();
            }
        }

        private void mnuSaveTilemap_Click(object sender, EventArgs e)
        {
            if (tilemapFileOptions == null)
            {
                mnuSaveTilemapAs.PerformClick();
            }
            else
            {
                tilemap.Save(tilemapFileOptions);
            }
        }

        private void mnuSaveTilemapAs_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Save Tilemap";
            saveFileDialog1.Filter = "Tilemap Files|*.raw;*bin";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var dialog = new SaveTilemapDialog() { SelectedFile = saveFileDialog1.FileName })
                {
                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;

                    tilemapFileOptions = new TilemapFileOptions {
                        FileName = dialog.SelectedFile,
                        Format = dialog.SelectedFormat,
                        Padding = dialog.SelectedPadding
                    };
                }

                tilemap.Save(tilemapFileOptions);
            }
        }

        private void mnuCreateTileset_Click(object sender, EventArgs e)
        {
            // TODO: ask to save Tilemap/Tileset
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Create Tileset";
            openFileDialog1.Filter = "Image Files|*.bmp;*.png;*.jpg";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            using (var bmp = new Bitmap(openFileDialog1.FileName))
            {
                // Sprite dimensions must be divisible by 8
                if (bmp.Width % 8 != 0 || bmp.Height % 8 != 0)
                {
                    MessageBox.Show("Tileset source image dimensions are not divisible by 8!",
                        "Invalid Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create Tilemap/Tileset
                (tileset, tilemap) = Tileset.Create(bmp, true);
            }

            // fill sizes for Tileset
            cmbTilesetWidth.Items.Clear();
            foreach (var size in tileset.GetPerfectColumns())
                cmbTilesetWidth.Items.Add(size.ToString());

            // pick middle size
            cmbTilesetWidth.SelectedIndex = cmbTilesetWidth.Items.Count / 2;

            // finish
            tilesetFileOptions = null;
            tilemapFileOptions = null;
            UpdateTileset(true);
            UpdateTilemap();

            mnuOpenTilemap.Enabled = true;
            mnuSaveTilemap.Enabled = true;
            mnuSaveTilemapAs.Enabled = true;
            mnuSaveTileset.Enabled = true;
            mnuSaveTilesetAs.Enabled = true;
            mnuPalette.Enabled = true;
        }

        private void mnuOpenTileset_Click(object sender, EventArgs e)
        {
            // TODO: ask to save old Tileset/Tilemap
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open Tileset";
            openFileDialog1.Filter = "Image Files|*.bmp;*.png;*.jpg";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            // create new Tileset
            using (var bmp = new Bitmap(openFileDialog1.FileName))
            {
                // sprite dimensions must be divisible by 8
                if (bmp.Width % 8 != 0 || bmp.Height % 8 != 0)
                {
                    MessageBox.Show(
                        "Tileset dimensions are not divisible by 8!",
                        "Invalid Sprite",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Tileset from sprite
                tileset = new Tileset(bmp);
            }

            // fill sizes for Tileset
            cmbTilesetWidth.Items.Clear();
            foreach (var size in tileset.GetPerfectColumns())
                cmbTilesetWidth.Items.Add(size.ToString());

            // pick middle size
            cmbTilesetWidth.SelectedIndex = cmbTilesetWidth.Items.Count / 2;

            // finish
            tilesetFileOptions = new TilesetFileOptions {
                FileName = openFileDialog1.FileName,
                Format = TilesetFormat.BMP,
                Columns = 1 // this will be update later anyway
            };
            UpdateTileset(true);

            // create new blank Tilemap
            tilemap = new Tilemap(30, 20);
            tilemapFileOptions = null;
            UpdateTilemap();

            mnuOpenTilemap.Enabled = true;
            mnuSaveTilemap.Enabled = true;
            mnuSaveTilemapAs.Enabled = true;
            mnuSaveTileset.Enabled = true;
            mnuSaveTilesetAs.Enabled = true;
            mnuPalette.Enabled = true;
        }

        private void mnuSaveTileset_Click(object sender, EventArgs e)
        {
            if (tilesetFileOptions == null)
            {
                mnuSaveTilesetAs.PerformClick();
            }
            else
            {
                short tilesetWidth;
                if (!short.TryParse(cmbTilesetWidth.Text, out tilesetWidth) || tilesetWidth <= 0)
                    tilesetWidth = 1;

                tilesetFileOptions.Columns = tilesetWidth;

                try
                {
                    tileset.Save(tilesetFileOptions);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void mnuSaveTilesetAs_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Save Tileset";
            saveFileDialog1.Filter = "Bitmap Files|*.bmp|Binary Files|*.bin";

            short tilesetWidth;
            if (!short.TryParse(cmbTilesetWidth.Text, out tilesetWidth) || tilesetWidth <= 0)
                tilesetWidth = 1;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        tilesetFileOptions = new TilesetFileOptions {
                            FileName = saveFileDialog1.FileName,
                            Format = TilesetFormat.BMP,
                            Columns = tilesetWidth
                        };
                        break;

                    case 2:
                        tilesetFileOptions = new TilesetFileOptions {
                            FileName = saveFileDialog1.FileName,
                            Format = TilesetFormat.BMP,
                            Columns = tilesetWidth
                        };
                        break;
                }

                try
                {
                    tileset.Save(tilesetFileOptions);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void mnuSwapColors_Click(object sender, EventArgs e)
        {
            if (tileset == null) return;

            using (var d = new SwapColorsDialog(tileset.Palette))
            {
                if (d.ShowDialog() != DialogResult.OK)
                    return;

                // replace palette for every tile
                tileset.SwapColors(d.Palette);

                // redraw tileset
                UpdateTileset(false);
                UpdateTilemap();
            }
        }

        private void mnuReduceColors_Click(object sender, EventArgs e)
        {
            if (tileset == null) return;

            // Recude the tileset's colors based on user selection
            using (var dialog = new ReduceColorsDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                tileset.ReduceColors(dialog.Colors);
            }

            // Refresh the tileset
            UpdateTileset(false);
            UpdateTilemap();
        }

        private void mnuExportColors_Click(object sender, EventArgs e)
        {
            if (tileset == null) return;

            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Paintshop Palette|*.pal|Adobe Color Table|*.act|APE Palette|*.gpl";
            saveFileDialog1.Title = "Export Tileset Colors";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        tileset.Palette.Save(saveFileDialog1.FileName, PaletteFormat.PAL);
                        break;
                    case 2:
                        tileset.Palette.Save(saveFileDialog1.FileName, PaletteFormat.ACT);
                        break;
                    case 3:
                        tileset.Palette.Save(saveFileDialog1.FileName, PaletteFormat.GPL);
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
            if (++zoom > 8)
            {
                zoom = 8;
            }

            UpdateTileset(false);
            UpdateTilemap();

            lZoom.Text = $"Zoom: {zoom * 100}%";
        }

        private void mnuZoomOut_Click(object sender, EventArgs e)
        {
            if (--zoom <= 0)
            {
                zoom = 1;
            }

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

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            using (var dialog = new AboutDialog())
            {
                dialog.ShowDialog();
            }
        }
    }
}
