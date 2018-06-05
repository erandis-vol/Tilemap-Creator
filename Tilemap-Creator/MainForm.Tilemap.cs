using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMC.Core;

namespace TMC
{
    partial class MainForm
    {
        Tilemap tilemap;
        FastBitmap tilemapImage;

        Point tilemapMouseCurrent = new Point(-1, -1);

        private static Brush[] palettemapBrushes;
        private static Font palettemapFont;

        // Updates Tilemap image (forced redraw)
        private void UpdateTilemap()
        {
            if (tilemap == null || tileset == null) return;
            ignore = true;

            // set size info
            tTilemapWidth.Value = tilemap.Width;
            tTilemapHeight.Value = tilemap.Height;

            // draw tilemap
            if (tilemapImage == null)
            {
                tilemapImage = new FastBitmap(tilemap.Width * 8, tilemap.Height * 8);
            }
            if (tilemapImage.Width != tilemap.Width * 8 ||
                tilemapImage.Height != tilemap.Height * 8)
            {
                tilemapImage.Dispose();
                tilemapImage = new FastBitmap(tilemap.Width * 8, tilemap.Height * 8);
            }

            tilemap.Draw(tilemapImage, tileset);

            // finished
            pTilemap.Size = new Size(tilemapImage.Width * zoom, tilemapImage.Height * zoom);
            pTilemap.Image = tilemapImage;
            ignore = false;
        }

        private void DrawPalettemap(Graphics g)
        {
            DrawPalettemap(g, 0, 0, tilemap.Width, tilemap.Height);
        }

        private void DrawPalettemap(Graphics g, int boundsX, int boundsY, int boundsWidth, int boundsHeight)
        {
            if (!rModePalette.Checked) return;

            if (palettemapBrushes == null)
            {
                palettemapBrushes = new SolidBrush[palettemapColors.Length];
                for (int i = 0; i < palettemapColors.Length; i++)
                    palettemapBrushes[i] = new SolidBrush(palettemapColors[i]);
            }

            if (palettemapFont == null)
                palettemapFont = new Font("Arial", 5.5f, FontStyle.Regular);

            for (int y = boundsY; y < boundsY + boundsHeight; y++)
            {
                for (int x = boundsX; x < boundsX + boundsWidth; x++)
                {
                    if (x < 0 || y < 0 || x >= tilemap.Width || y >= tilemap.Height)
                        continue;

                    g.FillRectangle(
                        palettemapBrushes[tilemap[x, y].Palette & 0xF],
                        x * 8 * zoom,
                        y * 8 * zoom,
                        8 * zoom,
                        8 * zoom
                    );

                    g.DrawString(
                        tilemap[x, y].Palette.ToString("X"),
                        palettemapFont,
                        Brushes.Black,
                        1 + x * 8 * zoom,
                        y * 8 * zoom
                    );
                }
            }
        }

        private void pTilemap_Paint(object sender, PaintEventArgs e)
        {
            if (tileset == null || tilemap == null) return;

            // Draw palettemap
            DrawPalettemap(
                e.Graphics,
                e.ClipRectangle.X / 8,
                e.ClipRectangle.Y / 8,
                e.ClipRectangle.Width / 8,
                e.ClipRectangle.Height / 8
            );

            // Draw grid
            if (mnuGrid.Checked)
            {
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

            // Draw cursor based on tileset/palette selection
            if (tilemapMouseCurrent.X >= 0 && tilemapMouseCurrent.Y >= 0)
            {
                if (rModeTilemap.Checked)
                {
                    e.Graphics.DrawRectangle(
                        Pens.Red,
                        tilemapMouseCurrent.X * zoom * 8,
                        tilemapMouseCurrent.Y * zoom * 8,
                        tilesetSelection.Width * zoom * 8 - 1,
                        tilesetSelection.Height * zoom * 8 - 1
                    );
                }
                else
                {
                    e.Graphics.DrawRectangle(
                        Pens.Red,
                        tilemapMouseCurrent.X * zoom * 8,
                        tilemapMouseCurrent.Y * zoom * 8,
                        zoom * 8 - 1,
                        zoom * 8 - 1
                    );
                }
            }
        }

        private void pTilemap_MouseDown(object sender, MouseEventArgs e)
        {
            pTilemap_MouseMove(sender, e);
        }

        private void pTilemap_MouseMove(object sender, MouseEventArgs me)
        {
            if (me.X < 0 || me.Y < 0 || me.X >= pTilemap.Width || me.Y >= pTilemap.Height)
                return;

            // update tilemap mouse position
            tilemapMouseCurrent.X = me.X / (zoom * Tileset.TileSize);
            tilemapMouseCurrent.Y = me.Y / (zoom * Tileset.TileSize);

            lPosition.Text = rModeTilemap.Checked ?
                $"Tilemap: ({tilemapMouseCurrent.X}, {tilemapMouseCurrent.Y})" :
                $"Palettemap: ({tilemapMouseCurrent.X}, {tilemapMouseCurrent.Y})";

            if (tilemap == null)
                return;

            var mousedTile = tilemap[tilemapMouseCurrent];
            lTile.Text = $"Tile: {mousedTile.Index:X3}";
            lPalette.Text = $"Palette: {mousedTile.Palette:X}";
            lFlip.Text = "Flip: " + (mousedTile.FlipX ? mousedTile.FlipY ? "XY" : "X" : mousedTile.FlipY ? "Y" : "None");

            // Set tiles starting from X, Y
            if (me.Button == MouseButtons.Left)
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

                            if (mapX < 0 || mapX >= tilemap.Width || mapY < 0 || mapY >= tilemap.Height)
                                continue;

                            // tileset position -- accounts for X/Y flipping
                            var setX = tilesetSelection.X + (chkTilesetFlipX.Checked ? tilesetSelection.Width - 1 - x : x);
                            var setY = tilesetSelection.Y + (chkTilesetFlipY.Checked ? tilesetSelection.Height - 1 - y : y);

                            // tile at position
                            var tile = setX + setY * tilesPerRow;

                            // ilegal tiles default to 0
                            if (tile >= tileset.Length)
                                tile = 0;

                            // set selection
                            tilemap[mapX, mapY].Index = (short)tile;
                            if (mnuAllowFlipping.Checked)
                            {
                                tilemap[mapX, mapY].FlipX = chkTilesetFlipX.Checked;
                                tilemap[mapX, mapY].FlipY = chkTilesetFlipY.Checked;
                            }
                        }
                    }                  
                }
                else
                {
                    // set palette selection
                    tilemap[tilemapMouseCurrent.X, tilemapMouseCurrent.Y].Palette = (byte)paletteSelection;
                }

                // redraw just the portion of the tilemap that was edited
                tilemap.Draw(tilemapImage, tileset, tilemapMouseCurrent.X, tilemapMouseCurrent.Y,
                        tilesetSelection.Width, tilesetSelection.Height);
            }
            // Get tile at X, Y -- overrides selection
            else if (me.Button == MouseButtons.Right)
            {
                if (rModeTilemap.Checked)
                {
                    var t = tilemap[tilemapMouseCurrent];
                    var w = cTilesetWidth.Value;

                    tilesetSelection = new Rectangle(t.Index % w, t.Index / w, 1, 1);

                    if (mnuAllowFlipping.Checked)
                    {
                        chkTilesetFlipX.Checked = t.FlipX;
                        chkTilesetFlipY.Checked = t.FlipY;
                    }
                }
                else
                {
                    paletteSelection = tilemap[tilemapMouseCurrent].Palette;
                }

                lTilesetSelection.Text = rModeTilemap.Checked ?
                        $"({tilesetSelection.X}, {tilesetSelection.Y}) to ({tilesetSelection.X + tilesetSelection.Width - 1}, {tilesetSelection.Y + tilesetSelection.Height - 1})" :
                        $"{paletteSelection}";

                pTileset.Invalidate();
            }
            // Flood fill
            // https://rosettacode.org/wiki/Bitmap/Flood_fill#C.23
            else if (me.Button == MouseButtons.Middle)
            {
                var dst = rModeTilemap.Checked ?
                    (tilesetSelection.X + tilesetSelection.Y * cTilesetWidth.Value) : paletteSelection;
                var src = rModeTilemap.Checked ?
                    tilemap[tilemapMouseCurrent].Index : tilemap[tilemapMouseCurrent].Palette;

                var queue = new Queue<Point>();

                // Fills all tiles going out from X, Y with the first tile selected
                if (src != dst)
                {
                    if (rModeTilemap.Checked)
                    {
                        // create a queue to hold points
                        queue.Enqueue(tilemapMouseCurrent);

                        // queues all tiles left and right of x,y and such
                        while (queue.Count > 0)
                        {
                            var n = queue.Dequeue();
                            if (tilemap[n].Index != src)
                                continue;

                            Point w = n, e = new Point(n.X + 1, n.Y);
                            while (w.X >= 0 && tilemap[w].Index == src)
                            {
                                tilemap[w].Index = (short)dst;
                                if (w.Y > 0 && tilemap[w.X, w.Y - 1].Index == src)
                                    queue.Enqueue(new Point(w.X, w.Y - 1));
                                if (w.Y < tilemap.Height - 1 && tilemap[w.X, w.Y + 1].Index == src)
                                    queue.Enqueue(new Point(w.X, w.Y + 1));
                                w.X--;
                            }

                            while (e.X <= tilemap.Width - 1 && tilemap[e].Index == src)
                            {
                                tilemap[e].Index = (short)dst;
                                if (e.Y > 0 && tilemap[e.X, e.Y - 1].Index == src)
                                    queue.Enqueue(new Point(e.X, e.Y - 1));
                                if (e.Y < tilemap.Height - 1 && tilemap[e.X, e.Y + 1].Index == src)
                                    queue.Enqueue(new Point(e.X, e.Y + 1));
                                e.X++;
                            }
                        }
                    }
                    else
                    {
                        // create a queue to hold points
                        queue.Enqueue(tilemapMouseCurrent);

                        // queues all tiles left and right of x,y and such
                        while (queue.Count > 0)
                        {
                            var n = queue.Dequeue();
                            if (tilemap[n].Palette != src)
                                continue;

                            Point w = n, e = new Point(n.X + 1, n.Y);
                            while (w.X >= 0 && tilemap[w].Palette == src)
                            {
                                tilemap[w].Palette = (byte)dst;
                                if (w.Y > 0 && tilemap[w.X, w.Y - 1].Palette == src)
                                    queue.Enqueue(new Point(w.X, w.Y - 1));
                                if (w.Y < tilemap.Height - 1 && tilemap[w.X, w.Y + 1].Palette == src)
                                    queue.Enqueue(new Point(w.X, w.Y + 1));
                                w.X--;
                            }

                            while (e.X <= tilemap.Width - 1 && tilemap[e].Palette == src)
                            {
                                tilemap[e].Palette = (byte)dst;
                                if (e.Y > 0 && tilemap[e.X, e.Y - 1].Palette == src)
                                    queue.Enqueue(new Point(e.X, e.Y - 1));
                                if (e.Y < tilemap.Height - 1 && tilemap[e.X, e.Y + 1].Palette == src)
                                    queue.Enqueue(new Point(e.X, e.Y + 1));
                                e.X++;
                            }
                        }
                    }

                    // Redraw entire tilemap (unknown amount of tiles changed)
                    tilemap.Draw(tilemapImage, tileset, 0, 0, tilemap.Width, tilemap.Height);
                }
            }

            //end:
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

        private void bTilemapUp_Click(object sender, EventArgs e)
        {
            if (tilemap == null)
                return;

            tilemap.ShiftUp();
            UpdateTilemap();
        }

        private void bTilemapDown_Click(object sender, EventArgs e)
        {
            if (tilemap == null)
                return;

            tilemap.ShiftDown();
            UpdateTilemap();
        }

        private void bTilemapLeft_Click(object sender, EventArgs e)
        {
            if (tilemap == null)
                return;

            tilemap.ShiftLeft();
            UpdateTilemap();
        }

        private void bTilemapRight_Click(object sender, EventArgs e)
        {
            if (tilemap == null)
                return;

            tilemap.ShiftRight();
            UpdateTilemap();
        }
    }
}
