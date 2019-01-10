using System;
using System.Windows.Forms;
using TMC.Core;

namespace TMC.Forms
{
    public partial class SaveTilemapDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveTilemapDialog"/> class.
        /// </summary>
        public SaveTilemapDialog()
        {
            InitializeComponent();
            cmbFormat.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets or sets the selected file.
        /// </summary>
        public string SelectedFile
        {
            get => txtFile.Text;
            set => txtFile.Text = value;
        }

        /// <summary>
        /// Gets or sets the selected format.
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

                // Unreachbale:
                throw new NotImplementedException();
            }
            set
            {
                if (value == TilemapFormat.Text4)
                {
                    cmbFormat.SelectedIndex = 0;
                }
                else if (value == TilemapFormat.Text8)
                {
                    cmbFormat.SelectedIndex = 1;
                }
                else if (value == TilemapFormat.RotationScaling)
                {
                    cmbFormat.SelectedIndex = 2;
                }

                // Unreachbale:
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets the selected padding.
        /// </summary>
        public int SelectedPadding
        {
            get => txtPadding.Value;
            set => txtPadding.Value = value;
        }
    }
}
