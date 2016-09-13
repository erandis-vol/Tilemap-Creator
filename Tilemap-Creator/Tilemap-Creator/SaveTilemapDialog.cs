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

            switch (suggestedFormat & TilemapFormat.Format)
            {
                case TilemapFormat.GBA:
                    cFormat.SelectedIndex = 0;
                    break;
            }
            cBitDepth.SelectedIndex = ((int)(suggestedFormat & TilemapFormat.BitDepth) & 0xF0 >> 4) / 8;

            textBox1.Text = File = filename;
        }

        public string File { get; private set; }
        public TilemapFormat Format
        {
            get
            {
                return (TilemapFormat)cFormat.SelectedIndex |
                    (cBitDepth.SelectedIndex == 0 ? TilemapFormat.BPP4 : TilemapFormat.BPP8); ;
            }
        }
        public int ExtraBytes
        {
            get { return txtExtra.Value; }
        }
    }
}
