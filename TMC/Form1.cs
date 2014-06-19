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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Imaging = TMC.Imaging;

namespace TMC
{
    public partial class Form1 : Form
    {
        private Tileset tileset;

        private bool mc;
   
        public Form1()
        {
            InitializeComponent();

            tileset = null;
            mc = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LockTilesetControls();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        // Lock & Unlock
        private void LockTilesetControls()
        {
            groupBox1.Enabled = false;
            mnuTilesetSave.Enabled = false;
        }

        private void UnlockTilesetControls()
        {
            groupBox1.Enabled = true;
            mnuTilesetSave.Enabled = true;
        }

        private void mnuOpenTileset_Click(object sender, EventArgs e)
        {
            open.FileName = "";
            open.Filter = "Supported Images|*.bmp;*.png;*.ncgr|Bitmap|*.bmp;*.png|Nitro Character Graphic|*.ncgr";
            open.Title = "Open Tileset";

            if (open.ShowDialog() == DialogResult.OK)
            {
                OpenTilesetForm otf = new OpenTilesetForm(open.FileName);
                if (otf.ShowDialog() != DialogResult.OK) return;

                // Check if a good tileset was loaded
                if (otf.Tileset == null) return;

                mc = true;

                // Do stuff~
                tileset = null;
                tileset = otf.Tileset;

                // Show the perfect sizes...
                int[] goodWidths = tileset.CalculatePerfectWidths();
                cTilesetWidth.Items.Clear();
                for (int i = 0; i < goodWidths.Length; i++) cTilesetWidth.Items.Add(goodWidths[i].ToString());
                cTilesetWidth.SelectedIndex = goodWidths.Length / 2;

                // Do tileset info.
                lTilesetInfo.Text = "Tiles: " + tileset.Length +
                    "\nBit Depth: " + tileset.GetBitDepthDescription();

                mc = false;

                // Draw tileset...
                UpdateTilesetStuff();
                UnlockTilesetControls();
            }
        }

        private void mnuTilesetSave_Click(object sender, EventArgs e)
        {
            save.FileName = "";
            save.Filter = "Supported Images|*.bmp;*.ncgr|Bitmap|*.bmp|Nitro Character Graphic|*.ncgr";
            save.Title = "Save Tileset";

            if (save.ShowDialog() == DialogResult.OK)
            {
                // Get that
                int drawWidth;
                if (!int.TryParse(cTilesetWidth.Text, out drawWidth)) return;

                // Smoosh tileset
                Imaging.Pixelmap tset = tileset.Smoosh(drawWidth);

                // Determine how to save
                string ext = Path.GetExtension(save.FileName).ToLower();
                if (ext == ".bmp")
                {
                    tset.Save(save.FileName, Imaging.PixelmapFormat.Bitmap);
                }
                else if (ext == ".ncgr")
                {
                    tset.Save(save.FileName, Imaging.PixelmapFormat.NCGR);
                }

                MessageBox.Show("Tileset save successfully!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            // Show AboutForm dialog...
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        private void UpdateTilesetStuff()
        {
            // Saftey check
            if (mc || tileset == null) return;

            // Get that
            int drawWidth;
            if (!int.TryParse(cTilesetWidth.Text, out drawWidth)) return;

            // Do stuff
            lTilesetHeight.Text = "x " + (tileset.Length / drawWidth);
            pTileset.Image = tileset.Draw(drawWidth, 1); // fixed width for testing
        }

        private void cTilesetWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTilesetStuff();
        }

        private void cTilesetWidth_TextUpdate(object sender, EventArgs e)
        {
            UpdateTilesetStuff();
        }
    }
}
