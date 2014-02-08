using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TMC
{
    public class NumericTextBox : TextBox
    {
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //if (e.KeyChar >= '0' && e.KeyChar <= '9') { }
            if (char.IsDigit(e.KeyChar)) { }
            else if (e.KeyChar == '\b') { }
            else e.Handled = true;
        }

        public int Value
        {
            get
            {
                if (TextLength > 0) return Convert.ToInt32(Text);
                else return 0;
            }
            set { Text = value.ToString(); }
        }
    }
}
