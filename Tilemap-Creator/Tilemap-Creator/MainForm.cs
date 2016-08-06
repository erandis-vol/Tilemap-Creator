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
        Tileset tileset;
        Sprite tilesetSprite;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tilesetSprite?.Dispose();
            tileset?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tilesetSprite?.Dispose();
                tileset?.Dispose();

                // test making a tileset
                using (var bmp = new Bitmap("remilia.jpg"))
                using (var spr = new Sprite(bmp))
                {
                    // info about the loaded sprite
                    Console.WriteLine("sprite: size=({0}x{1}), colors={2}", spr.Width, spr.Height, spr.Palette.Length);

                    // create a new tileset
                    tileset = new Tileset(spr);
                    Console.WriteLine("tileset:", tileset.Size);
                    foreach (var size in tileset.PerfectSizes)
                    {
                        Console.WriteLine("> ({0} x {1})", size.Width, size.Height);
                    }

                    // smoosh into fixed size for now
                    tilesetSprite = tileset.Smoosh(640);

                    pictureBox1.Size = new Size(tilesetSprite.Width * 4, tilesetSprite.Height * 4);
                    pictureBox1.Image = tilesetSprite;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
