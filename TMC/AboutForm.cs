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

namespace TMC
{
    public partial class AboutForm : Form
    {
        private string[] info = { "[b]About Tilemap Creator", "Copyright (c) 2014 eien no itari", "", "", "", "", "[b]Credits", "- itari (programming)", "- GBATEK (GBA/DS information)", "- tinke (DS information)", "", "", "[b]Changelog", "v1.0 (Oct. 12, 2013)", "- initial release (Tileset Creator)", "", "v1.1 (Oct. 13, 2013)", "- fixed an issue with tileset display", "", "v2.0 (Oct. 17, 2013)", "- now Tilemap Creator!", "- tilemap import", "- tilemap save/load", "", "v2.5 (Oct. 24, 2013)", "- improve palettemap view", "- tile flipping implemented", "- \"trace\" mode", "- tileset indexing (16-color)", "", "v3.0 (Nov. 9, 2013)", "- new GUI", "- tileset indexing (256-color)", "- \"trace\" removed", "", "v3.4 (Dec. 14, 2013)", "- \"area draw\"", "- 8 bpp tilemaps now supported", "", "v3.4.1 (Feb. 7, 2014)", "- TMC is now on github!", "", "v4.0 (??. ??, 2014)", "- new GUI (again!)", "- support for NDS images/tilemaps", "- improved image handling", "- ???", "", "", "", "", "Thanks for the continued support!", "[b]I love you!" };
        private int index = -8;

        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {

        }

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            index += 1;
            if (index >= info.Length) index = -8;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Draw scene
            Font norm = new Font(pictureBox1.Font, FontStyle.Regular);
            Font bold = new Font(pictureBox1.Font, FontStyle.Bold);
            float space = bold.Size + 4;

            int y = -1;
            for (int i = index; i < index + 8; i++)
            {
                y++;

                if (i >= info.Length) break;
                if (i < 0) continue;

                if (info[i] == "") continue;

                // draw
                if (info[i].StartsWith("[b]"))
                    e.Graphics.DrawString(info[i].Substring(3), bold, Brushes.Black, 39, y * space);
                else
                    e.Graphics.DrawString(info[i], norm, Brushes.Black, 39, y * space);
            }

            // icon
            e.Graphics.DrawIcon(this.Icon, 3, 3);
        }

    }
}
