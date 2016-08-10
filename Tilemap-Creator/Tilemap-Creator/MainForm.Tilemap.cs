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
        Tilemap tilemap;
        Bitmap tilemapImage;

        // updates Tilemap image (forced redraw)
        void UpdateTilemap()
        {
            if (tilemap == null || tileset == null) return;
            ignore = true;

            // set size info
            tTilemapWidth.Value = tilemap.Width;
            tTilemapHeight.Value = tilemap.Height;

            // draw tilemap image :D
            DrawTilemap();

            // finished
            pTilemap.Size = new Size(tilemapImage.Width * zoom, tilemapImage.Height * zoom);
            pTilemap.Image = tilemapImage;
            ignore = false;
        }

        void DrawTilemap()
        {
            // recreate Tilemap image
            tilemapImage?.Dispose();
            tilemapImage = new Bitmap(tilemap.Width * Tileset.TileSize, tilemap.Height * Tileset.TileSize);

            // render Tilemap fully
            using (var g = Graphics.FromImage(tilemapImage))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                for (int y = 0; y < tilemap.Height; y++)
                {
                    for (int x = 0; x < tilemap.Width; x++)
                    {
                        var tile = tilemap[x, y];
                        g.DrawImageFlipped(tileset[tile.TilesetIndex],
                            x * Tileset.TileSize, y * Tileset.TileSize,
                            tile.FlipX, tile.FlipY);
                    }
                }

                // render Palettemap on top
                if (rModePalette.Checked)
                {
                    var brushes = new SolidBrush[16];
                    for (int i = 0; i < 16; i++)
                        brushes[i] = new SolidBrush(palettemapColors[i]);
                    var font = new Font("Arial", 5f, FontStyle.Regular);

                    for (int y = 0; y < tilemap.Height; y++)
                    {
                        for (int x = 0; x < tilemap.Width; x++)
                        {
                            g.FillRectangle(brushes[tilemap[x, y].PaletteIndex],
                                x * Tileset.TileSize, y * Tileset.TileSize,
                                Tileset.TileSize, Tileset.TileSize);

                            g.DrawString(tilemap[x, y].PaletteIndex.ToString("x"), font,
                                Brushes.Black, 1 + x * Tileset.TileSize, 1 + y * Tileset.TileSize);
                        }
                    }

                    for (int i = 0; i < 16; i++)
                        brushes[i].Dispose();
                    font.Dispose();
                }
            }
        }

        private void pTilemap_Paint(object sender, PaintEventArgs e)
        {
            if (tileset == null || tilemap == null) return;

            // draw grid
            using (var pen = new Pen(new SolidBrush(gridColor), 1f))
            using (var penS = new Pen(new SolidBrush(gridColorS), 1f))
            {
                pen.DashPattern = new[] { 2f, 2f };
                penS.DashPattern = new[] { 2f, 2f };

                int f = zoom * Tileset.TileSize;

                for (int x = 1; x < pTilemap.Width / f; x++)
                {
                    e.Graphics.DrawLine(x % 30 == 0 ? penS : pen, x * f, 0, x * f, pTilemap.Height);
                }

                for (int y = 1; y < pTilemap.Height / f; y++)
                {
                    e.Graphics.DrawLine(y % 20 == 0 ? penS : pen, 0, y * f, pTilemap.Width, y * f);
                }
            }
        }

        private void rMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ignore) return;

            // TODO: change selection
            UpdateTileset();
            UpdateTilemap();
        }
    }
}
