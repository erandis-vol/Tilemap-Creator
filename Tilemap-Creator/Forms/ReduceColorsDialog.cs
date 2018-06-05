using System;
using System.Windows.Forms;

namespace TMC.Forms
{
    public partial class ReduceColorsDialog : Form
    {
        public ReduceColorsDialog()
        {
            InitializeComponent();
        }

        private void OnRadioChecked(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = radioButton1.Checked;
        }

        public int Colors
        {
            get
            {
                if (radioButton1.Checked)
                    return (int)numericUpDown1.Value;
                else if (radioButton2.Checked)
                    return 256;
                else
                    return 16;
            }
        }
    }
}
