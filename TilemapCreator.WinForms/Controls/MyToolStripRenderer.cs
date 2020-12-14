using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TilemapCreator.Controls
{
    public class MyToolStripRenderer : MyMenuRenderer
    {
        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);

            if (item is ToolStripSeparator separator && !separator.IsOnDropDown)
            {
                item.Margin = new Padding(0, 0, 0, 2);
            }

            if (item is ToolStripButton)
            {
                item.AutoSize = false;
                item.Size = new Size(24, 24);
            }
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            if (e.ToolStrip is ToolStripOverflow)
            {
                var rect = new Rectangle(e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
                e.Graphics.DrawRectangle(Pens.Red, rect);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (!(e.ToolStrip is ToolStrip))
                base.OnRenderToolStripBorder(e);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            var rect = new Rectangle(0, 1, e.Item.Width, e.Item.Height - 2);
            var outlineRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);

            if (e.Item.Selected || e.Item.Pressed)
            {
                using var b = new SolidBrush(e.Item.Pressed ? SelectedColor : HoverColor);
                e.Graphics.FillRectangle(b, rect);

                using var p = new Pen(e.Item.Pressed ? SelectedOutlineColor : HoverOutlineColor);
                e.Graphics.DrawRectangle(p, outlineRect);
            }

            if (e.Item is ToolStripButton button)
            {
                if (button.Checked)
                {
                    using var b = new SolidBrush(SelectedColor);
                    e.Graphics.FillRectangle(b, rect);

                    using var p = new Pen(SelectedOutlineColor);
                    e.Graphics.DrawRectangle(p, outlineRect);
                }
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (e.Item is ToolStripSeparator separator && separator.IsOnDropDown)
            {
                base.OnRenderSeparator(e);
                return;
            }

            var rect = new Rectangle(3, 3, 2, e.Item.Height - 4);

            using (var p = new Pen(SystemColors.Control))
            {
                e.Graphics.DrawLine(p, rect.Left, rect.Top, rect.Left, rect.Height);
            }

            using (var p = new Pen(SystemColors.ControlLightLight))
            {
                e.Graphics.DrawLine(p, rect.Left + 1, rect.Top, rect.Left + 1, rect.Height);
            }
        }
    }
}
