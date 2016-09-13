using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMC
{
    public partial class OpenTilemapDialog : Form
    {
        Size[] friendlySizes;

        public OpenTilemapDialog(string filename)
        {
            InitializeComponent();

            textBox1.Text = File = filename;
            cFormat.SelectedIndex = 0;
        }

        private void cFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bluh();
        }

        void Bluh()
        {
            var file = new FileInfo(File);
            var sizes = new List<Size>();


            var l = (int)(Format == TilemapFormat.RotationScaling ? file.Length : file.Length / 2);
            for (int i = 1; i <= l; i++)
            {
                if (l % i == 0)
                    sizes.Add(new Size(i, l / i));
            }

            cSize.Items.Clear();
            foreach (var size in sizes)
                cSize.Items.Add($"{size.Width} x {size.Height}");

            friendlySizes = sizes.ToArray();
            cSize.SelectedIndex = 0;
        }

        public string File { get; private set; }

        public Size FriendlySize
        {
            get
            {
                return friendlySizes[cSize.SelectedIndex];
            }
        }

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
    }
}
