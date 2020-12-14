using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TilemapCreator.Controls
{
    public class BorderedPanel : Panel
    {
        public BorderedPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            BorderColor = SystemColors.ControlLight;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = ClientRectangle;
            var outlineRect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            using var b = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(b, rect);

            using var p = new Pen(BorderColor);
            e.Graphics.DrawRectangle(p, outlineRect);
        }

        /// <summary>
        /// Gets or sets border color for the control.
        /// </summary>
        [Category("Appearance")]
        public Color BorderColor { get; set; }
    }
}
