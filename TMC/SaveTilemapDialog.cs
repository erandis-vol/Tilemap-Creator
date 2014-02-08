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
        private bool success = false;

        public SaveTilemapDialog(BitDepth bpp)
        {
            InitializeComponent();

            // Display that
            if (bpp == BitDepth.BPP4)
            {
                r4bpp.Checked = true;
                r8bpp.Checked = false;
            }
            else
            {
                r4bpp.Checked = false;
                r8bpp.Checked = true;
            }

            r4bpp.AutoCheck = false;
            r8bpp.AutoCheck = false;
        }

        private void SaveDialog_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            bSave.Enabled = false;
        }

        private void SaveDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.DialogResult = (success ? DialogResult.OK : DialogResult.Cancel);
            if (success) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save.FileName = "";
            save.Filter = "Tilemaps|*.raw";
            save.Title = "Save Tilemap...";

            if (save.ShowDialog() != DialogResult.OK) return;

            textBox1.Text = save.FileName;
            bSave.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            success = true;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Properties

        public string FileName
        {
            get { return textBox1.Text; }
        }

        public bool AddExtra
        {
            get { return checkBox1.Checked; }
        }

        public int Extra
        {
            get { return (textBox2.TextLength > 0 ? Convert.ToInt32(textBox2.Text) : -1); }
        }

        #endregion
    
    }
}
