using System;
using System.Windows.Forms;

namespace TMC.Controls
{
    public class NumberBox : TextBox
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

        public int MinimumValue { get; set; } = 0;
        public int MaximumValue { get; set; } = int.MaxValue - 1;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Value < MinimumValue)
                Value = MinimumValue;
            if (Value > MaximumValue)
                Value = MaximumValue;
        }
    }
}
