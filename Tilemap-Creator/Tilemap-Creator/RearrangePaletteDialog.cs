using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TMC
{
    public partial class RearrangePaletteDialog : Form
    {
        Color[] palette;
        int start = -1;
        int mouse = -1;

        public RearrangePaletteDialog(Color[] palette)
        {
            InitializeComponent();

            this.palette = new Color[palette.Length];
            palette.CopyTo(this.palette, 0);

            pPalette.Height = ((palette.Length / 16) + (palette.Length % 16 != 0 ? 1 : 0)) * 16;
        }

        private void pPalette_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < palette.Length; i++)
            {
                int x = i % 16;
                int y = i / 16;

                e.Graphics.FillRectangle(new SolidBrush(palette[i]), x * 16, y * 16, 16, 16);
            }

            if (mouse != -1)
            {
                int x = mouse % 16;
                int y = mouse / 16;

                if (start != -1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(palette[start]), x * 16, y * 16, 16, 16);
                    e.Graphics.DrawRectangle(new Pen(Invert(palette[start])), x * 16, y * 16, 15, 15);
                }
                else
                {
                    e.Graphics.DrawRectangle(new Pen(Invert(palette[mouse])), x * 16, y * 16, 15, 15);
                }
            }
        }

        private void pPalette_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= pPalette.Width || e.Y >= pPalette.Height)
                return;

            start = (e.X / 16) + (e.Y / 16) * 16;
        }

        private void pPalette_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= pPalette.Width || e.Y >= pPalette.Height)
                return;

            int end = (e.X / 16) + (e.Y / 16) * 16;
            if (start == end)
                return;

            var t = palette[start];
            palette[start] = palette[end];
            palette[end] = t;

            start = -1;
            pPalette.Invalidate();
        }

        private void pPalette_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= pPalette.Width || e.Y >= pPalette.Height)
                mouse = -1;
            else
                mouse = (e.X / 16) + (e.Y / 16) * 16;

            pPalette.Invalidate();
        }

        Color Invert(Color c)
        {
            return Color.FromArgb(c.ToArgb() ^ 0xFFFFFF);
        }

        public Color[] Palette
        {
            get { return palette; }
            //set { palette = value; }
        }
    }
}
