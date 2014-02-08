using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TMC
{
    public partial class OpenTilemapDialog : Form
    {
        private bool success = false;
        //int fileLength = 0;
        private Size[] sizes;
        
        public OpenTilemapDialog()
        {
            InitializeComponent();
        }

        private void OpenTilemapDialog_Load(object sender, EventArgs e)
        {
            bOpen.Enabled = false;
        }

        private void OpenTilemapDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = (success ? DialogResult.OK : DialogResult.Cancel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open.FileName = "";
            open.Title = "Open Tilemap...";
            open.Filter = "GBA RAW Tilemap|*.raw|Sphere Map File|*.rmp";

            if (open.ShowDialog() != DialogResult.OK) return;

            txtFile.Text = open.FileName;

            FileInfo f = new FileInfo(open.FileName);
            if (f.Extension.ToLower() == ".rmp")
            {
                r4bpp.Visible = false;
                label1.Text = "Format: Sphere Map";
                //nWidth.Enabled = false;
                cSize.Enabled = false;
            }
            else
            {
                r4bpp.Visible = true;
                label1.Text = "Format:";
                cSize.Enabled = true;

                cSize.Items.Clear();

                // Calculate sizes...
                int len = (int)f.Length / 2;
                List<Size> s = new List<Size>();
                for (int i = 1; i < f.Length; i++)
                {
                    if (len % i == 0)
                    {
                        cSize.Items.Add(i.ToString() + " x " + (len / i).ToString());
                        s.Add(new Size(i, len / i));
                    }
                    else if ((len - 8) % i == 0)
                    {
                        cSize.Items.Add(i.ToString() + " x " + ((len - 8) / i).ToString());
                        s.Add(new Size(i, (len - 8) / i));
                    }
                }
                sizes = s.ToArray();

                cSize.SelectedIndex = 0;
            }

            bOpen.Enabled = true;
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            success = true;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            success = false;
            Close();
        }

        public string FileName
        {
            get { return txtFile.Text; }
        }

        public Size[] TilemapSizes
        {
            get { return sizes; }
        }

        public Size SelectedSize
        {
            get { return sizes[cSize.SelectedIndex]; }
        }

        public BitDepth BPP
        {
            get { return (r4bpp.Checked ? BitDepth.BPP4 : BitDepth.BPP8); }
        }
    }
}
