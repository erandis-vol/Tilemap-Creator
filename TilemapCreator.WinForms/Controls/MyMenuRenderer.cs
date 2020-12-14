using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TilemapCreator.Controls
{
    public class MyMenuRenderer : ToolStripRenderer
    {
        //private static readonly Color HighlightColor = Color.FromArgb(201, 222, 245);
        protected static readonly Color HoverColor = Color.FromArgb(216, 230, 242);
        protected static readonly Color HoverOutlineColor = Color.FromArgb(192, 220, 243);
        protected static readonly Color SelectedColor = Color.FromArgb(192, 220, 243);
        protected static readonly Color SelectedOutlineColor = Color.FromArgb(144, 200, 246);

        protected override void Initialize(ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);

            toolStrip.BackColor = SystemColors.Control;
            toolStrip.ForeColor = SystemColors.ControlText;
        }

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);

            item.BackColor = SystemColors.Control;
            item.ForeColor = SystemColors.ControlText;
            if (item is ToolStripSeparator)
                item.Margin = new Padding(0, 0, 0, 1);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            var bounds = e.AffectedBounds;
            e.Graphics.FillRectangle(SystemBrushes.Control, bounds);
            //e.Graphics.DrawLine(SystemPens.ControlLight, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            var rect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            e.Graphics.DrawRectangle(SystemPens.ControlLight, rect);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            // TODO
            base.OnRenderItemCheck(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            var rect = new Rectangle(30, 3, e.Item.Width - 28, 1);
            e.Graphics.FillRectangle(SystemBrushes.ControlLight, rect);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            e.Item.ForeColor = e.Item.Enabled ? e.Item.ForeColor : SystemColors.GrayText;
            if (e.Item.Enabled)
            {
                // Normal item
                var rect = new Rectangle(2, 0, e.Item.Width - 2, e.Item.Height);
                var outlineRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);

                if (e.Item.Selected)
                {
                    using var b = new SolidBrush(HoverColor);
                    e.Graphics.FillRectangle(b, rect);

                    using var p = new Pen(HoverOutlineColor);
                    e.Graphics.DrawRectangle(p, outlineRect);
                }
                else
                {
                    using var b = new SolidBrush(e.Item.BackColor);
                    e.Graphics.FillRectangle(b, rect);
                }

                // Header item on open menu
                //if (e.Item.GetType() == typeof(ToolStripMenuItem))
                if (e.Item is ToolStripMenuItem menuItem)
                {
                    if (menuItem.DropDown.Visible && e.Item.IsOnDropDown == false)
                    {
                        using var b = new SolidBrush(SelectedColor);
                        e.Graphics.FillRectangle(b, rect);

                        using var p = new Pen(SelectedOutlineColor);
                        e.Graphics.DrawRectangle(p, outlineRect);
                    }
                }
            }
        }
    }
}
