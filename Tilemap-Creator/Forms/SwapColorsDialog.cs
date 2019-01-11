using System;
using System.Drawing;
using System.Windows.Forms;
using TMC.Core;

namespace TMC.Forms
{
    public partial class SwapColorsDialog : Form
    {
        int start = -1;
        int mouse = -1;

        public SwapColorsDialog(Palette palette)
        {
            InitializeComponent();

            Palette = new Palette(palette);
            pPalette.Height = ((Palette.Length / 16) + (Palette.Length % 16 != 0 ? 1 : 0)) * 16;
        }

        private void pPalette_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Palette.Length; i++)
            {
                var x = i % 16;
                var y = i / 16;

                e.Graphics.FillRectangle(new SolidBrush(Palette[i]), x * 16, y * 16, 16, 16);
            }

            if (mouse != -1)
            {
                var x = mouse % 16;
                var y = mouse / 16;

                if (start >= 0 && start < Palette.Length)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Palette[start]), x * 16, y * 16, 16, 16);
                    e.Graphics.DrawRectangle(new Pen(GetSelectorColor(Palette[start])), x * 16, y * 16, 15, 15);
                }
                else if (mouse >= 0 && mouse < Palette.Length)
                {
                    e.Graphics.DrawRectangle(new Pen(GetSelectorColor(Palette[mouse])), x * 16, y * 16, 15, 15);
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Black, x * 16, y * 16, 15, 15);
                }
            }
        }

        private void pPalette_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= pPalette.Width || e.Y >= pPalette.Height)
                return;

            var index = e.X / 16 + e.Y / 16 * 16;
            if (index >= 0 && index < Palette.Length)
                start = (e.X / 16) + (e.Y / 16) * 16;
        }

        private void pPalette_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= pPalette.Width || e.Y >= pPalette.Height)
                return;

            var end = (e.X / 16) + (e.Y / 16) * 16;
            if (start == end || end < 0 || end >= Palette.Length)
                return;

            var t = Palette[start];
            Palette[start] = Palette[end];
            Palette[end] = t;

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

        private Color GetSelectorColor(Color color) => color.GetBrightness() <= 0.64f ? Color.White : Color.Black;

        public Palette Palette { get; }
    }
}
