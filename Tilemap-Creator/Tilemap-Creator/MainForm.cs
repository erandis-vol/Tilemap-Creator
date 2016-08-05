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
    public partial class MainForm : Form
    {
        Bitmap original;
        Sprite sprite;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            original?.Dispose();
            sprite?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // test the Sprite class
                // slooooow
                original = new Bitmap("1234posts.png");
                sprite = new Sprite(original);

                pictureBox1.Image = original;
                pictureBox2.Image = sprite.Draw(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
