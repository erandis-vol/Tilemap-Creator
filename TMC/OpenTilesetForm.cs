// Copyright (c) 2014 itari
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at:
// 
//  http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TMC.Imaging;

namespace TMC
{
    public partial class OpenTilesetForm : Form
    {
        private string imgPath;
        private bool preIndexed;
        private Pixelmap img16Color, img256Color;
        private Tileset result16, result256;

        private bool open = false;
        private bool error = false;

        private bool mc = false;

        public OpenTilesetForm(string filePath)
        {
            InitializeComponent();

            preIndexed = false;
            img16Color = null;
            img256Color = null;

            result16 = null;
            result256 = null;

            imgPath = filePath;
            label1.Text = "File: " + Path.GetFileName(filePath);
        }

        private void OpenTilesetForm_Load(object sender, EventArgs e)
        {

        }

        private void OpenTilesetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (open) DialogResult = DialogResult.OK;
            else if (error) DialogResult = DialogResult.Abort;
            else DialogResult = DialogResult.Cancel;
        }

        private void OpenTilesetForm_Shown(object sender, EventArgs e)
        {
            Text = "Loading Image...";
            progressBar1.Visible = true;

            button1.Enabled = false;
            button2.Enabled = false;

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;

            backgroundWorker1.RunWorkerAsync();
        }

        private void OpenTilesetForm_Resize(object sender, EventArgs e)
        {
            //CenterImage();
        }

        private void CenterImage()
        {
            // Calculate center of panel and subtract center of image...
            int x = panel2.Bounds.Width / 2 - pictureBox1.Width / 2;
            int y = panel2.Bounds.Height / 2 - pictureBox1.Height / 2;
            pictureBox1.Location = new Point(x, y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            open = true;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open = false;
            Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadImage();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // not needed
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (error)
            {
                Close();
                return; // Yeah... Not sure why this works, but it does.
            }

            mc = true;

            if (img16Color != null) comboBox1.Items.Add("16 Colors (4 BPP)");
            if (img256Color != null) comboBox1.Items.Add("256 Colors (8BPP)");
            comboBox1.SelectedIndex = 0;

            int[] widths = (img16Color == null ? result256.CalculatePerfectWidths() : result16.CalculatePerfectWidths());
            for (int k = 0; k < widths.Length; k++) comboBox2.Items.Add(widths[k].ToString());
            comboBox2.SelectedIndex = widths.Length / 2;
            label4.Text = "x " + ((img16Color == null ? result256.Length : result16.Length) / widths[widths.Length / 2]);

            comboBox3.SelectedIndex = 0;

            if (img16Color != null) pictureBox1.Image = result16.Draw(widths[widths.Length / 2], 1);
            else pictureBox1.Image = result256.Draw(widths[widths.Length / 2], 1);
            //CenterImage(); -- not yet~

            progressBar1.Visible = false;
            Text = "Open Tileset?";

            button1.Enabled = true;
            button2.Enabled = true;

            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;

            mc = false;
        }

        private void LoadImage()
        {
            try
            {
                // Open image
                string ext = Path.GetExtension(imgPath).ToLower();
                if (ext == ".png" || ext == ".bmp")
                {
                    // The bigger the image, the longer the wait.
                    Bitmap bmp = new Bitmap(imgPath);
                    if (bmp.PixelFormat == PixelFormat.Format4bppIndexed)
                    {
                        preIndexed = true;
                        img16Color = new Pixelmap(bmp);
                    }
                    else if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                    {
                        preIndexed = true;
                        img256Color = new Pixelmap(bmp);
                    }
                    else
                    {
                        // This will take the most time~   
                        img16Color = new Pixelmap(bmp, PaletteGenerationMethod.First, 16);
                        img256Color = new Pixelmap(bmp, PaletteGenerationMethod.First, 256);
                    }
                    bmp.Dispose();
                }
                else if (ext == ".ncgr")
                {
                    Pixelmap temp = Helper.LoadNCGR(imgPath);

                    preIndexed = true;
                    if (temp.GetColorMode() == Imaging.ColorMode.Color16)
                    {
                        img16Color = temp;
                    }
                    else
                    {
                        img256Color = temp;
                    }
                }
                else
                {
                    throw new Exception("Unsupported image format!");
                }

                // Load tilesets
                if (img16Color != null) result16 = new Tileset(img16Color);
                if (img256Color != null) result256 = new Tileset(img256Color);
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTilesetPreview();
        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            UpdateTilesetPreview();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTilesetPreview();
        }

        private void UpdateTilesetPreview()
        {
            if (mc) return;

            // Get width
            int w;
            if (!int.TryParse(comboBox2.Text, out w)) return;
            label4.Text = "x " + ((img16Color == null ? result256.Length : result16.Length) / w);

            if (preIndexed)
            {
                // Draw appropriate color depth
                if (img16Color != null)
                {
                    pictureBox1.Image = result16.Draw(w, GetZoom());
                }
                else
                {
                    pictureBox1.Image = result256.Draw(w, GetZoom());
                }
            }
            else
            {
                // Draw chosen color depth
                if (comboBox1.SelectedIndex == 0)
                {
                    pictureBox1.Image = result16.Draw(w, GetZoom());
                }
                else
                {
                    pictureBox1.Image = result256.Draw(w, GetZoom());
                }
            }

            // BOOM!
            //CenterImage();
            // NO!
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTilesetPreview();
        }

        private int GetZoom()
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0: return 1;
                case 1: return 2;
                case 2: return 4;
                case 3: return 8;
                case 4: return 11;
                case 5: return 16;
                default: return 1;
            }
        }

        public Tileset Tileset
        {
            get
            {
                if (preIndexed) // It was already good~
                {
                    if (img16Color != null) return result16;
                    else return result256;
                }
                else // Not indexed, the user indexed it.
                {
                    if (comboBox1.SelectedIndex == 0) return result16;
                    else return result256;
                }
            }
        }
    }
}
