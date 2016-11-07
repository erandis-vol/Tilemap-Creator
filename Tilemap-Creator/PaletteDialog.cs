using System.Drawing;
using System.Windows.Forms;

namespace TMC
{
    public partial class PaletteDialog : Form
    {
        Color[] palette;
        Panel[] paletteBoxes;

        public PaletteDialog(Color[] palette)
        {
            InitializeComponent();

            this.palette = new Color[palette.Length];
            palette.CopyTo(this.palette, 0);

            // fill the flow thing with panels
            paletteBoxes = new Panel[palette.Length];
            for (int i = 0; i < palette.Length; i++)
            {
                var p = new Panel();
                p.Size = new Size(32, 32);
                p.BackColor = palette[i];
                p.Tag = i;
                p.MouseClick += PaletteBox_MouseClick;

                paletteBoxes[i] = p;
            }

            flowColors.Controls.AddRange(paletteBoxes);

        }

        private void PaletteDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            //flowColors.Dispose();
        }

        private void PaletteBox_MouseClick(object sender, MouseEventArgs e)
        {
            var paletteBox = (Panel)sender;
            var index = (int)paletteBox.Tag;

            colorDialog1.Color = palette[index];
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                palette[index] = colorDialog1.Color;
                paletteBoxes[index].BackColor = palette[index];
            }
        }

        public Color[] Palette { get { return palette; } }
    }
}
