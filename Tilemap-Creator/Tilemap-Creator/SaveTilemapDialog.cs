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
        public SaveTilemapDialog(string filename)
        {
            InitializeComponent();

            cFormat.SelectedIndex = 0;
            textBox1.Text = File = filename;
        }

        public string File { get; private set; }

        public TilemapFormat Format
        {
            get
            {
                switch (cFormat.SelectedIndex)
                {
                    case 0:
                        return TilemapFormat.Text4;
                    case 1:
                        return TilemapFormat.Text8;
                    case 2:
                        return TilemapFormat.RotationScaling;

                    default:    // should never happen
                        throw new Exception();
                }
            }
        }

        public int ExtraBytes
        {
            get { return txtExtra.Value; }
        }
    }
}
