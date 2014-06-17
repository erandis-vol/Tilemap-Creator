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
        Tileset tileset;
   
        public Form1()
        {
            InitializeComponent();
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

                // do stuff~
                tileset = otf.Tileset;
            }
        }
    }
}
