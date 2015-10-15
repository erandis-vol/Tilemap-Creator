using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TMC.Imaging;

namespace TMC
{
    public partial class PaletteDialog : Form
    {
        private Palette palette;
        private bool small;

        public PaletteDialog(Palette palette)
        {
            InitializeComponent();

            this.palette = palette;
            this.small = palette.Length <= 16;
        }

        private void PaletteDialog_Load(object sender, EventArgs e)
        {

        }

        private void pPalette_Paint(object sender, PaintEventArgs e)
        {
            // Two types of rendering, yo
            // A sixteen color palette renders larger
            if (small)
            {
                for (int c = 0; c < 16; c++)
                {
                    int x = c % 4;
                    int y = c / 4;

                    using (Brush brush = new SolidBrush(palette[c]))
                    {
                        e.Graphics.FillRectangle(brush, x * 64, y * 64, 64, 64);
                    }
                }
            }
            else
            {
                for (int c = 0; c < 256; c++)
                {
                    int x = c % 8;
                    int y = c / 8;

                    using (Brush brush = new SolidBrush(palette[c]))
                    {
                        e.Graphics.FillRectangle(brush, x * 32, y * 32, 32, 32);
                    }
                }
            }
            /*else
            {
                e.Graphics.DrawString("How did you get a palette larger than 256 colors? D:", this.Font, Brushes.Red, 0, 0);
            }*/
        }

        /// <summary>
        /// Gets the modified Palette.
        /// </summary>
        public Palette Palette
        {
            get { return palette; }
        }
    }
}
