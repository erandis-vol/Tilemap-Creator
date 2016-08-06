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
                // test zooming the picturebox
                original = new Bitmap("remilia.jpg");
                sprite = new Sprite(original);

                pictureBox1.Size = new Size(original.Width, original.Height);
                pictureBox2.Size = new Size(original.Width * 2, original.Height * 2);

                pictureBox1.Image = original;

                sprite.Lock();
                sprite.SwapColors(0, sprite.Palette.Length - 1, false);
                sprite.Unlock();

                pictureBox2.Image = sprite;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
