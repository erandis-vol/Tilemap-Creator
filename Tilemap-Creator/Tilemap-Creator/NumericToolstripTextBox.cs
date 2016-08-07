using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMC
{
    public class NumericToolStripTextBox : ToolStripTextBox
    {
        public int Value
        {
            get
            {
                int result;
                if (int.TryParse(Text, out result))
                    return result;

                return 0;
            }
            set
            {
                Text = value.ToString();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b')
                ;
            else
                e.Handled = true;

            base.OnKeyPress(e);
        }
    }
}
