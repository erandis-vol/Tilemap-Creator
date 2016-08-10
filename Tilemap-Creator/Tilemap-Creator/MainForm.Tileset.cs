using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMC
{
    partial class MainForm
    {
        Tileset tileset;
        Sprite tilesetImage;
        
        Color[] palettemapColors = new Color[]
        {
            Color.FromArgb(128, Color.White),
            Color.FromArgb(128, Color.Yellow),
            Color.FromArgb(128, Color.Red),
            Color.FromArgb(128, Color.Gray),
            Color.FromArgb(128, Color.Cyan),
            Color.FromArgb(128, Color.Blue),
            Color.FromArgb(128, Color.Magenta),
            Color.FromArgb(128, Color.LightYellow),
            Color.FromArgb(128, Color.Teal),
            Color.FromArgb(128, Color.LightSteelBlue),
            Color.FromArgb(128, Color.Violet),
            Color.FromArgb(128, Color.Orange),
            Color.FromArgb(128, Color.LightGray),
            Color.FromArgb(128, Color.SandyBrown),
            Color.FromArgb(128, Color.Purple),
            Color.FromArgb(128, Color.LightPink),
        };
        Bitmap palettesetImage;

        // updates Tileset size and image
        void UpdateTileset()
        {
            if (tileset == null) return;

            ignore = true;
            if (rModeTilemap.Checked)
            {

                // get Tileset size
                int width = cTilesetWidth.Value;
                if (width <= 0) width = 1;

                int height = (tileset.Size / width) + (tileset.Size % width > 0 ? 1 : 0);

                // update height text
                tTilesetHeight.Value = height;

                // update Tileset image
                tilesetImage?.Dispose();
                tilesetImage = tileset.Smoosh(width);

                pTileset.Size = new Size(tilesetImage.Width * zoom, tilesetImage.Height * zoom);
                pTileset.Image = tilesetImage;
            }
            else
            {
                pTileset.Size = new Size(palettesetImage.Width * zoom, palettesetImage.Height * zoom);
                pTileset.Image = palettesetImage;
            }
            ignore = false;
        }

        void DrawPalette()
        {
            palettesetImage?.Dispose();
            palettesetImage = new Bitmap(4 * 8, 4 * 8);

            using (var g = Graphics.FromImage(palettesetImage))
            using (var font = new Font("Arial", 5.5f, FontStyle.Regular))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                for (int i = 0; i < 16; i++)
                {
                    using (var b = new SolidBrush(palettemapColors[i]))
                    {
                        g.FillRectangle(b, i % 4 * 8, i / 4 * 8, 8, 8);
                        g.DrawString(i.ToString("X"), font, Brushes.Black, 1 + i % 4 * 8, i / 4 * 8);
                    }
                }
            }
        }

        private void pTileset_Paint(object sender, PaintEventArgs e)
        {
            if (tileset == null) return;

            // draw grid
            using (var pen = new Pen(new SolidBrush(gridColor), 1f))
            {
                pen.DashPattern = new[] { 2f, 2f };

                int f = zoom * Tileset.TileSize;

                for (int x = 1; x < pTileset.Width / f; x++)
                {
                    e.Graphics.DrawLine(pen, x * f, 0, x * f, pTileset.Height);
                }

                for (int y = 1; y < pTileset.Height / f; y++)
                {
                    e.Graphics.DrawLine(pen, 0, y * f, pTileset.Width, y * f);
                }
            }
        }

        private void cTilesetWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore) return;

            UpdateTileset();
        }

        private void cTilesetWidth_TextChanged(object sender, EventArgs e)
        {
            if (ignore) return;

            UpdateTileset();
        }
    }
}
