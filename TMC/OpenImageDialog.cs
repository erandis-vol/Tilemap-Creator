using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TMC.Imaging;

namespace TMC
{
    public partial class OpenImageDialog : Form
    {
        private string imagePath;
        private Pixelmap img16Color = null, img256Color = null;
        private Pixelmap imgResult = null;

        private bool open = false;
        private bool mc = false;

        public OpenImageDialog(string file)
        {
            InitializeComponent();

            imagePath = file;
        }

        private void OpenImageDialog_Load(object sender, EventArgs e)
        {
            lblFile.Text = "File: " + imagePath;
            //pictureBox1.Image = new Bitmap(imagePath);
        }

        private void OpenImageDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Clone our result
            // Fun times for everyone
            // ^_^
            if (open)
            {
                imgResult = (r16Color.Checked ? img16Color : img256Color).Clone();
            }

            // Dispose of these stupid things
            if (img16Color != null) img16Color.Dispose();
            if (img256Color != null) img256Color.Dispose();

            // Handle dialog result stuff
            if (open) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }

        private void OpenImageDialog_Shown(object sender, EventArgs e)
        {
            // First, disable everything
            mc = true;
            bOpen.Enabled = false;
            bCancel.Enabled = false;

            r16Color.Enabled = false;
            r256Color.Enabled = false;

            this.Cursor = Cursors.WaitCursor;
            mc = false;

            // And then do the loading thing
            backgroundWorker1.RunWorkerAsync();
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            open = true;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // First, disable everything~

            // Load the image thingy
            string ext = Path.GetExtension(imagePath).ToLower();
            if (ext == ".ncgr")
            {
                using (Pixelmap temp = new Pixelmap(imagePath, PixelmapFormat.NCGR))
                {
                    if (temp.BitsPerPixel == 4) img16Color = temp.Clone();
                    else img256Color = temp.Clone();
                }
            }
            else // Assume Bitmap ;_;
            {
                using (Bitmap bmp = new Bitmap(imagePath))
                {
                    if (bmp.PixelFormat == PixelFormat.Format4bppIndexed)
                    {
                        img16Color = new Pixelmap(bmp);
                    }
                    else if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                    {
                        img256Color = new Pixelmap(bmp);
                    }
                    else
                    {
                        img16Color = new Pixelmap(bmp, 16);
                        img256Color = new Pixelmap(bmp, 256);
                    }
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Do formatting stuff
            mc = true;
            if (img16Color != null && img256Color != null)
            {
                r16Color.Enabled = true;
                r256Color.Enabled = true;

                r16Color.Checked = true;
                r256Color.Checked = false;

                pictureBox1.Image = img16Color.Render();
            }
            else if (img16Color != null)
            {
                r16Color.Enabled = true;
                r256Color.Enabled = false;

                r16Color.Checked = true;
                r256Color.Checked = false;

                pictureBox1.Image = img16Color.Render();
            }
            else if (img256Color != null)
            {
                r16Color.Enabled = false;
                r256Color.Enabled = true;

                r256Color.Checked = true;

                pictureBox1.Image = img256Color.Render();
            }

            bOpen.Enabled = true;
            bCancel.Enabled = true;
            this.Cursor = Cursors.Default;
            mc = false;
        }

        private void rColor_CheckedChanged(object sender, EventArgs e)
        {
            // This thing
            if (mc) return;

            //! Render depending on radiobutton
            try
            {
                pictureBox1.Image = r16Color.Checked ? img16Color.Render() : img256Color.Render();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
            }
        }

        public Pixelmap Image
        {
            get
            {
                if (open)
                    return imgResult;
                else
                    return null;
            }
        }
    }
}
