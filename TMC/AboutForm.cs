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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("About {0}", Application.ProductName);

            // Draw Icon
            Bitmap ico = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage(ico);
            g.DrawIcon(Icon, 0, 0);
            pIcon.Image = ico;

            //label1.Text = Application.ProductName;
            //label2.Text = Application.ProductVersion;
            label3.Text = "Copyright © " + Application.CompanyName + " 2014";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
