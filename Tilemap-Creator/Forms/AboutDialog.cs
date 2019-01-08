using System.Diagnostics;
using System.Windows.Forms;

namespace TMC.Forms
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://github.com/erandis-vol/Tilemap-Creator");
        }
    }
}
