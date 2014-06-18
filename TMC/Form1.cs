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

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void mnuOpenTileset_Click(object sender, EventArgs e)
        {
            open.FileName = "";
            open.Filter = "Supported Images|*.bmp;*.png";
            open.Title = "Open Tileset";

            if (open.ShowDialog() == DialogResult.OK)
            {
                OpenTilesetForm otf = new OpenTilesetForm(open.FileName);
                if (otf.ShowDialog() != DialogResult.OK) return;

                // Check if a good tileset was loaded
                if (otf.Tileset == null) return;

                // Do stuff~
                tileset = otf.Tileset;

                // Show the perfect sizes...
                int[] goodWidths = tileset.CalculatePerfectWidths();
                cTilesetWidth.Items.Clear();
                for (int i = 0; i < goodWidths.Length; i++) cTilesetWidth.Items.Add(goodWidths[i].ToString());
                cTilesetWidth.SelectedIndex = goodWidths.Length / 2;

                // Draw tileset...
                UpdateTilesetStuff();
            }
        }

        private void mnuTilesetSave_Click(object sender, EventArgs e)
        {
            // Pass~
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
            pTileset.Image = tileset.Draw(drawWidth, 2); // fixed width for testing
        }
    }
}
