using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilemapCreator.Forms
{
    public partial class OpenTilesetDialog : Form
    {
        private bool _loading;
        private Tileset? _tileset;
        private string? _tilesetFileName;
        private Palette? _tilesetPalette;
        private Palette? _palette;
        private string? _paletteFileName;

        public OpenTilesetDialog()
        {
            InitializeComponent();
        }

        private void buttonOpenTileset_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Open Tileset",
                Filter = "All Supported Files|*.bmp;*.png;*.bin;*.raw;"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var filename = dialog.FileName;
            var format = Path.GetExtension(filename)?.ToUpper() switch
            {
                ".BMP" => TilesetFormat.Bmp,
                ".PNG" => TilesetFormat.Png,
                ".BIN" => TilesetFormat.Gba,
                ".RAW" => TilesetFormat.Gba,
                _ => throw new ArgumentException("Unsupported file format.", nameof(filename))
            };

            _loading = true;
            Cursor = Cursors.WaitCursor;
            progressBar1.Visible = true;

            // TODO: load in task
            if (LoadTileset(filename, format))
            {
                textBox1.Text = filename;

                // check for a previously loaded palette, which may no longer work
                if (_tilesetPalette != null && _palette != null && _tilesetPalette.Length > _palette.Length)
                {
                    Debug.WriteLine("Loaded palette has too few colors, clearing out.");
                    _palette = null;
                    _paletteFileName = null;
                    textBox2.Clear();
                }

                RefreshPreview();
            }
            else
            {
                textBox1.Clear();
            }

            _loading = false;
            Cursor = Cursors.Default;
            progressBar1.Visible = false;
        }

        private void buttonOpenPalette_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Open Palette",
                Filter = "All Supported Files|*.pal;*.act|Paintshop Palette Files|*.pal|Adobe Color Table Files|*.act"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var filename = dialog.FileName;
            var format = Path.GetExtension(filename)?.ToUpper() switch
            {
                ".PAL" => PaletteFormat.Pal,
                ".ACT" => PaletteFormat.Act,
                ".BIN" => PaletteFormat.Gba,
                ".RAW" => PaletteFormat.Gba,
                _ => throw new NotImplementedException()
            };

            if (LoadPalette(filename, format))
            {
                textBox2.Text = filename;
                RefreshPreview();
            }
            else
            {
                textBox2.Clear();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            buttonOpenPalette.Enabled = checkBox1.Checked;
            textBox2.Enabled = checkBox1.Checked;
            if (_palette != null)
            {
                RefreshPreview();
            }
        }

        // loads a tileset and (possibly) the embeded palette
        private bool LoadTileset(string filename, TilesetFormat format)
        {
            // load the tileset and palette
            Tileset tileset;
            Palette? palette;
            try
            {
                (tileset, palette) = Tileset.Load(filename, format, default);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION:");
                Debug.WriteLine(ex);
                return false;
            }

            Debug.Assert(tileset != null, "tileset is null");
            Debug.Assert(palette != null, "palette is null"); // it will be when gba is implemented

            // tileset loaded successfully, we can proceed
            _tileset = tileset;
            _tilesetPalette = palette;
            _tilesetFileName = filename;

            // determine valid dimensions, pick the one which often may match the original size
            var tilesetSizes = tileset.GetSuggestedDimensions();
            comboPreviewWidth.Items.Clear();
            for (int i = 0; i < tilesetSizes.Length; i++)
                comboPreviewWidth.Items.Add(tilesetSizes[i]);
            comboPreviewWidth.SelectedIndex = tilesetSizes.Length / 2;

            return true;
        }

        // loads a palette
        private bool LoadPalette(string filename, PaletteFormat format)
        {
            try
            {
                var palette = Palette.Load(filename, format);

                // check that palette size is compatible with tileset
                if (_tilesetPalette != null && palette.Length < _tilesetPalette.Length)
                {
                    MessageBox.Show("Palette does not contain enough colors for the tileset.",
                    "Invalid Palette", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                _palette = palette;
                _paletteFileName = filename;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load palette file:" + Environment.NewLine + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void RefreshPreview()
        {
            // TODO: look into some way to avoid unnecessary refreshing
            if (_tileset is null)
                return;

            // determine palette to use
            var palette = _tilesetPalette;
            if (checkBox1.Checked && _palette != null)
                palette = _palette;
            if (palette is null)
                return;

            // determine width
            if (!int.TryParse(comboPreviewWidth.Text, out var width) || width <= 0)
                width = 1;

            // deterimine height
            var height = Math.Max(1, _tileset.Length / width);
            if (width * height != _tileset.Length)
                height++;
            textPreviewHeight.Text = height.ToString();

            // draw the preview
            var image = new Bitmap(width * 8, height * 8);
            using (var fb = image.FastLock())
            {
                _tileset.Draw(fb, width, palette);
            }

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = image;
        }

        private void comboPreviewWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboPreviewWidth_TextChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                RefreshPreview();
            }
        }

        public Tileset? GetTileset() => _tileset;

        public Palette? GetPalette() => _palette ?? _tilesetPalette;
    }
}
