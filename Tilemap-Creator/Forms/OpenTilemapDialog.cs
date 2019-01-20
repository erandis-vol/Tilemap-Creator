using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TMC.Core;

namespace TMC.Forms
{
    public partial class OpenTilemapDialog : Form
    {
        private List<Size> sizes = new List<Size>();
        private FileInfo selectedFile;

        public OpenTilemapDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (selectedFile != null && selectedFile.Exists)
            {
                cmbFormat.SelectedIndex = 0;
            }

            base.OnLoad(e);
        }

        private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSizes();
        }

        private void UpdateSizes()
        {
            sizes.Clear();

            // Calculate the length of the tilemap
            int length = (int)selectedFile.Length;
            if (SelectedFormat != TilemapFormat.RotationScaling)
                length /= 2;

            // Calculates possible sizes for the given tilemap
            for (int i = 1; i <= length; i++)
            {
                if (length % i == 0)
                    sizes.Add(new Size(i, length / i));

                // This second calculation lets us catch
                // tilemaps that may not be perfectly sized
                if ((length - 8) % i == 0)
                    sizes.Add(new Size(i, (length - 8) / i));
            }

            cmbSize.Items.Clear();
            cmbSize.Items.AddRange(sizes.Select(x => $"{x.Width} x {x.Height}").ToArray());
            cmbSize.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets or sets the selected file.
        /// </summary>
        public string SelectedFile
        {
            get => selectedFile.FullName;
            set
            {
                if (selectedFile == null || selectedFile.FullName != value)
                {
                    selectedFile = new FileInfo(value);
                    txtFileName.Text = selectedFile.FullName;

                    UpdateSizes();
                }
            }
        }

        /// <summary>
        /// Gets the selected size.
        /// </summary>
        public Size SelectedSize => sizes[cmbSize.SelectedIndex];

        /// <summary>
        /// Gets the selected format.
        /// </summary>
        public TilemapFormat SelectedFormat
        {
            get
            {
                if (cmbFormat.SelectedIndex == 0)
                {
                    return TilemapFormat.Text4;
                }
                else if (cmbFormat.SelectedIndex == 1)
                {
                    return TilemapFormat.Text8;
                }
                else if (cmbFormat.SelectedIndex == 2)
                {
                    return TilemapFormat.RotationScaling;
                }
                else
                {
                    return TilemapFormat.Text4;
                }
            }
        }
    }
}
