using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMC
{
    public enum ColorMode
    {
        ColorTrue,
        Color16,
        Color256
    }

    public struct ColorSwap
    {
        public Color Old, New;
    }

    public partial class PaletteDialog : Form
    {
        private ColorMode colorMode;
        private Color[] palette;

        List<ColorSwap> colorSwaps;

        public PaletteDialog(Color[] palette, ColorMode colorMode)
        {
            InitializeComponent();

            this.colorMode = colorMode;
            this.palette = palette;
            colorSwaps = new List<ColorSwap>();
        }

        private void PaletteDialog_Load(object sender, EventArgs e)
        {
            Text += " (" + (colorMode == ColorMode.Color16 ? "16 colors" : "256 colors") + ")";
        }

        private void pPalette_Paint(object sender, PaintEventArgs e)
        {
            if (palette == null) return;

            e.Graphics.FillRectangle(Brushes.Black, 0, 0, 256, 256);

            if (colorMode == ColorMode.Color256)
            {
                for (int i = 0; i < 256; i++)
                {
                    int x = i % 16; int y = i / 16;
                    e.Graphics.FillRectangle(new SolidBrush(palette[i]), x * 16, y * 16, 16, 16);
                }
            }
            else if (colorMode == ColorMode.Color16)
            {
                for (int i = 0; i < 16; i++)
                {
                    int x = i % 16; int y = i / 16;
                    e.Graphics.FillRectangle(new SolidBrush(palette[i]), x * 16, y * 16, 16, 16);
                }
            }
        }

        private void pPalette_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int x = e.X / 16; int y = e.Y / 16;

            int index = x + y * 16;

            if (colorMode == ColorMode.Color16 && index > 15) return;

            colorDialog.Color = palette[index];
            if (colorDialog.ShowDialog() != DialogResult.OK) return;

            ColorSwap cs = new ColorSwap();
            cs.Old = palette[index];
            cs.New = colorDialog.Color;
            colorSwaps.Add(cs);

            palette[index] = colorDialog.Color;
            pPalette.Invalidate();
        }

        public ColorSwap[] ColorSwaps
        {
            get { return colorSwaps.ToArray(); }
        }
    }
}
