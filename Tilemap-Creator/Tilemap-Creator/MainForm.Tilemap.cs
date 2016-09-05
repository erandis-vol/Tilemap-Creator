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

        Point tilemapMouseCurrent = new Point(-1, -1);
        bool tilemapHasMouse = false;

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
                    var font = new Font("Arial", 5.5f, FontStyle.Regular);

                    for (int y = 0; y < tilemap.Height; y++)
                    {
                        for (int x = 0; x < tilemap.Width; x++)
                        {
                            g.FillRectangle(brushes[tilemap[x, y].PaletteIndex],
                                x * Tileset.TileSize, y * Tileset.TileSize,
                                Tileset.TileSize, Tileset.TileSize);

                            g.DrawString(tilemap[x, y].PaletteIndex.ToString("X"), font,
                                Brushes.Black, 1 + x * Tileset.TileSize, y * Tileset.TileSize);
                        }
                    }

                    for (int i = 0; i < 16; i++)
                        brushes[i].Dispose();
                    font.Dispose();
                }
            }
        }

        void RedrawTilemap(int boundsX, int boundsY, int boundsWidth, int boundsHeight)
        {
            // redraws a portion of the tilemap onto the existing image

            using (var g = Graphics.FromImage(tilemapImage))
            {
                for (int y = boundsY; y < boundsY + boundsHeight; y++)
                {
                    for (int x = boundsX; x < boundsX + boundsWidth; x++)
                    {
                        if (x >= tilemap.Width || y >= tilemap.Height)
                            break;

                        var tile = tilemap[x, y];
                        g.DrawImageFlipped(tileset[tile.TilesetIndex],
                            x * Tileset.TileSize, y * Tileset.TileSize,
                            tile.FlipX, tile.FlipY);
                    }
                }

                if (rModePalette.Checked)
                {
                    var brushes = new SolidBrush[16];
                    for (int i = 0; i < 16; i++)
                        brushes[i] = new SolidBrush(palettemapColors[i]);
                    var font = new Font("Arial", 5.5f, FontStyle.Regular);

                    for (int y = boundsY; y < boundsY + boundsHeight; y++)
                    {
                        for (int x = boundsX; x < boundsX + boundsWidth; x++)
                        {
                            if (x >= tilemap.Width || y >= tilemap.Height)
                                break;

                            g.FillRectangle(brushes[tilemap[x, y].PaletteIndex],
                                x * Tileset.TileSize, y * Tileset.TileSize,
                                Tileset.TileSize, Tileset.TileSize);

                            g.DrawString(tilemap[x, y].PaletteIndex.ToString("X"), font,
                                Brushes.Black, 1 + x * Tileset.TileSize, y * Tileset.TileSize);
                        }
                    }

                    for (int i = 0; i < 16; i++)
                        brushes[i].Dispose();
                    font.Dispose();
                }
            }

            // below not needed because image should already be set for pTilemap
            // pTilemap.Image = tilemapImage;
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

            // draw cursor based on tileset/palette selection
            if (tilemapMouseCurrent.X >= 0 && tilemapMouseCurrent.Y >= 0)
            {
                if (rModeTilemap.Checked)
                {
                    e.Graphics.DrawRectangle
                        (
                        Pens.Red,
                        tilemapMouseCurrent.X * zoom * Tileset.TileSize,
                        tilemapMouseCurrent.Y * zoom * Tileset.TileSize,
                        tilesetSelection.Width * zoom * Tileset.TileSize - 1,
                        tilesetSelection.Height * zoom * Tileset.TileSize - 1
                        );
                }
                else
                {
                    e.Graphics.DrawRectangle
                        (
                        Pens.Red,
                        tilemapMouseCurrent.X * zoom * Tileset.TileSize,
                        tilemapMouseCurrent.Y * zoom * Tileset.TileSize,
                        zoom * Tileset.TileSize - 1,
                        zoom * Tileset.TileSize - 1
                        );
                }
            }
        }

        private void pTilemap_MouseDown(object sender, MouseEventArgs e)
        {
            pTilemap_MouseMove(sender, e);
        }

        private void pTilemap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= pTilemap.Width || e.Y >= pTilemap.Height)
                return;

            // update tilemap mouse position
            tilemapMouseCurrent.X = e.X / (zoom * Tileset.TileSize);
            tilemapMouseCurrent.Y = e.Y / (zoom * Tileset.TileSize);

            lPosition.Text = rModeTilemap.Checked ?
                $"Tilemap: ({tilemapMouseCurrent.X}, {tilemapMouseCurrent.Y})" :
                $"Palettemap: ({tilemapMouseCurrent.X}, {tilemapMouseCurrent.Y})";

            // set new tiles
            if (e.Button == MouseButtons.Left)
            {
                if (rModeTilemap.Checked)
                {
                    var tilesPerRow = cTilesetWidth.Value;

                    // set selection rectangle
                    for (int x = 0; x < tilesetSelection.Width; x++)
                    {
                        for (int y = 0; y < tilesetSelection.Height; y++)
                        {
                            // tilemap position
                            var mapX = tilemapMouseCurrent.X + x;
                            var mapY = tilemapMouseCurrent.Y + y;

                            if (mapX >= tilemap.Width || mapY >= tilemap.Height)
                                break;

                            // tileset position -- accounts for X/Y flipping
                            var setX = tilesetSelection.X + (chkTilesetFlipX.Checked ? tilesetSelection.Width - 1 - x : x);
                            var setY = tilesetSelection.Y + (chkTilesetFlipY.Checked ? tilesetSelection.Height - 1 - y : y);

                            // tile at position
                            var t = setX + setY * tilesPerRow;

                            // ilegal tiles default to 0
                            if (t >= tileset.Size)
                                t = 0;

                            // set selection
                            tilemap[mapX, mapY].TilesetIndex = t;
                            tilemap[mapX, mapY].FlipX = chkTilesetFlipX.Checked;
                            tilemap[mapX, mapY].FlipY = chkTilesetFlipY.Checked;
                        }
                    }                  
                }
                else
                {
                    // set palette selection
                    tilemap[tilemapMouseCurrent.X, tilemapMouseCurrent.Y].PaletteIndex = paletteSelection;
                }

                // redraw just the portion of the tilemap that was edited
                RedrawTilemap(tilemapMouseCurrent.X, tilemapMouseCurrent.Y,
                        tilesetSelection.Width, tilesetSelection.Height);
            }
            // TODO: get tile at X, Y -- overrides selection
            else if (e.Button == MouseButtons.Right)
            {

            }

            pTilemap.Invalidate();
        }

        private void pTilemap_MouseLeave(object sender, EventArgs e)
        {
            tilemapMouseCurrent.X = -1;
            pTilemap.Invalidate();
        }

        private void rMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ignore) return;

            // TODO: change selection
            UpdateTileset(false);
            UpdateTilemap();
        }

        private void bTilemapResize_Click(object sender, EventArgs e)
        {
            if (tilemap == null || tileset == null)
                return;

            if (tilemap.Width != tTilemapWidth.Value || tilemap.Height != tTilemapHeight.Value)
            {
                tilemap.Resize(tTilemapWidth.Value, tTilemapHeight.Value);

                UpdateTilemap();
            }
        }
    }
}
