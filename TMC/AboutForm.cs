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
        //private string[] info = { "#About Tilemap Creator", "Copyright (c) 2013 - 2015 Hopeless Masquerade", "", "", "", "", "#Credits", "- Hopeless Masquerade (programming)", "- Lin (FastPixel.cs)", "- GBATEK (GBA/DS information)", "- tinke (DS information)", "", "", "#Changelog", "v1.0 (Oct. 12, 2013)", "- initial release (Tileset Creator)", "", "v1.1 (Oct. 13, 2013)", "- fixed an issue with tileset display", "", "v2.0 (Oct. 17, 2013)", "- now Tilemap Creator!", "- tilemap import", "- tilemap save/load", "", "v2.5 (Oct. 24, 2013)", "- improve palettemap view", "- tile flipping implemented", "- \"trace\" mode", "- tileset indexing (16-color)", "", "v3.0 (Nov. 9, 2013)", "- new GUI", "- tileset indexing (256-color)", "- \"trace\" removed", "", "v3.4 (Dec. 14, 2013)", "- \"area draw\"", "- 8 bpp tilemaps now supported", "", "v3.4.1 (Feb. 7, 2014)", "- TMC is now on github!", "", "v4.0 (??. ??, 2015)", "- complete rewrite", "- support for NDS images/tilemaps", "- improved image handling", "- ???", "", "", "", "", "#Thanks for the continued support!" };

        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
