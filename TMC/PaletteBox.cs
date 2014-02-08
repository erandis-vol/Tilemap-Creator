using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMC
{
    
    
    public partial class PaletteBox : UserControl
    {
        private ColorMode colorMode = ColorMode.Color256;
        private Color[] palette = null;//Helper.GenerateGreyscalePalette(256);
        
        public PaletteBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (palette == null)
            {
                e.Graphics.FillRectangle(new SolidBrush(BackColor), Bounds);
                //return;
            }
            else
            {
                if (colorMode == ColorMode.Color16)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        int x = i % 4; int y = i / 4;
                        e.Graphics.FillRectangle(new SolidBrush(palette[i]), x * 32, y * 32, 32, 32);
                    }
                }
                else if (colorMode == ColorMode.Color256)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        int x = i % 16; int y = i / 16;
                        e.Graphics.FillRectangle(new SolidBrush(palette[i]), x * 8, y * 8, 8, 8);
                    }
                }
            }
        }

        public ColorMode ColorMode
        {
            get { return colorMode; }
            set { colorMode = value; }
        }

        public Color[] Palette
        {
            get { return palette; }
            set { palette = value; Invalidate(); }
        }
    }
}
