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
    public partial class SaveTilemapDialog : Form
    {
        public SaveTilemapDialog(string filename, TilemapFormat suggestedFormat)
        {
            InitializeComponent();

            switch (suggestedFormat)
            {
                case TilemapFormat.GBA4:
                case TilemapFormat.GBA8:
                    cFormat.SelectedIndex = 0;
                    break;
            }
            cBitDepth.SelectedIndex = 0;

            textBox1.Text = File = filename;
        }

        public string File { get; private set; }
        public TilemapFormat Format
        {
            get
            {
                var bpp = 4 << cBitDepth.SelectedIndex;
                Console.WriteLine($"Bit Depth {bpp}");
                //var format = cFormat.SelectedIndex;

                //if (format == 0)
                return bpp == 4 ? TilemapFormat.GBA4 : TilemapFormat.GBA8;
            }
        }
        public int ExtraBytes
        {
            get { return txtExtra.Value; }
        }
    }
}
