using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TMC.Imaging;

namespace TMC
{
    public partial class MainForm : Form
    {
        private int zoom = 2;
        private readonly float[] dashPattern = new float[] { 2, 2 };

        private Tileset tileset = null;
        private int tilesPerRow = -1;
        private Rectangle tilesetSelection = new Rectangle(0, 0, 1, 1);
        private Point mouseTileset = new Point(0, 0), oldMouseTileset = new Point(0, 0);
        private bool mouseOnTileset = false, mouseSelectingTileset = false;

        private Tilemap tilemap = null;
        private bool mouseOnTilemap = false;
        private Point mouseTilemap = new Point(0, 0);

        private bool mc = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tilemap = new Tilemap(30, 20);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // TODO: disposal code
            if (tileset != null) tileset.Dispose();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // You can initilize a new tilemap anytime, yo
            // TODO: ask to save old one

            tilemap = new Tilemap(30, 20);

            UpdateTilemap();
        }


        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Supported Files|*.bmp;*.png;*.gif;*.tiff;*.ncgr";
            openFileDialog.Title = "Open Tileset";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Bitmap bmp = new Bitmap(openFileDialog.FileName);
                //Pixelmap pmp = new Pixelmap(bmp, 256);

                //pTileset.Image = bmp;
                //pTilemap.Image = pmp.Render(2);

                using (var oid = new OpenImageDialog(openFileDialog.FileName))
                {
                    if (oid.ShowDialog() != DialogResult.OK) return;

                    tileset = new Tileset(oid.Image);
                    int[] perfectWidths = tileset.CalculatePerfectWidths();
                    tilesPerRow = perfectWidths[perfectWidths.Length / 2];

                    mc = true;
                    cTilesetSizes.Items.Clear();
                    for (int i = 0; i < perfectWidths.Length; i++)
                    {
                        cTilesetSizes.Items.Add(perfectWidths[i].ToString());
                    }
                    cTilesetSizes.SelectedIndex = perfectWidths.Length / 2;
                    mc = false;

                    UpdateTileset();
                    UpdateTilemap();
                }

                
            }
            else
            {
                // TODO
            }
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";
            saveFileDialog.Filter = "Bitmaps|*.bmp|NCGR Files|*.ncgr";
            saveFileDialog.Title = "";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            using (Pixelmap smoosh = tileset.SmooshTiles(tilesPerRow))
            {
                if (saveFileDialog.FilterIndex == 1)
                {
                    //xMessageBox.Show("BMP!");
                    smoosh.Save(saveFileDialog.FileName, PixelmapFormat.BitmapIndexed);
                }
                else if (saveFileDialog.FilterIndex == 2)
                {
                    //xMessageBox.Show("NCGR!");
                    smoosh.Save(saveFileDialog.FileName, PixelmapFormat.NCGR);
                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tileset == null) return;

            saveFileDialog.Title = "Export Tileset Palette";
            saveFileDialog.Filter = "TMC Palette|*.tmcp|Windows Palette|*.pal|Adobe Color Table|*.act|NCLR Palette|*.nclr";
            saveFileDialog.FileName = "";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (saveFileDialog.FilterIndex == 1) // tmpc
            {
                tileset[0].Palette.Save(saveFileDialog.FileName, PaletteFormat.TMCP);
            }
            else if (saveFileDialog.FilterIndex == 2) // pal
            {
                tileset[0].Palette.Save(saveFileDialog.FileName, PaletteFormat.PAL);
            }
            else if (saveFileDialog.FilterIndex == 3) // act
            {
                tileset[0].Palette.Save(saveFileDialog.FileName, PaletteFormat.ACT);
            }
            else if (saveFileDialog.FilterIndex == 4) // nclr
            {
                tileset[0].Palette.Save(saveFileDialog.FileName, PaletteFormat.NCLR);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tileset == null) return;

            using (PaletteDialog pd = new PaletteDialog(tileset[0].Palette))
            {
                pd.ShowDialog();
                // TODO: edit palettes ^_^

                if (tileset[0].Palette.IsSameAs(pd.Palette))
                {
                    MessageBox.Show("Cool!\nNo changes made!");
                }
            }
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoom < 12)
            {
                zoom++;
                lblZoom.Text = string.Format("Zoom: {0}%", zoom * 100);

                UpdateTileset();
                UpdateTilemap();
            }
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoom > 1)
            {
                zoom--;
                lblZoom.Text = string.Format("Zoom: {0}%", zoom * 100);

                UpdateTileset();
                UpdateTilemap();
            }
        }

        private void UpdateTileset()
        {
            // Basically, redraw and shit
            if (tileset == null || tilesPerRow == -1) return;

            // TODO: palettemap mode stuffz
            tilesetSelection = new Rectangle(0, 0, 1, 1);
            
            pTileset.Image = tileset.Render(tilesPerRow, zoom);

            lblTilesetHeight.Text = "x " + (tileset.Count / tilesPerRow + (tileset.Count % tilesPerRow == 0 ? 0 : 1));
            
            //pTilemap.Image = tileset.SmooshTiles(tilesPerRow).RenderFlipped(true, true, 2);

        }

        private void UpdateTilemap()
        {
            // Safety checks and stuff
            if (tilemap == null) return;
            if (tileset == null || tilesPerRow == -1) return;

            pTilemap.Image = tilemap.Render(tileset, zoom);
        }

        private void cTilesetSizes_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            int i;
            if (!int.TryParse(cTilesetSizes.Text.Trim(), out i)) i = 1;

            tilesPerRow = i;
            UpdateTileset();
        }

        private void cTilesetSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            int i;
            if (!int.TryParse(cTilesetSizes.Text.Trim(), out i)) i = 1;

            tilesPerRow = i;
            UpdateTileset();
        }

        private void pTileset_Paint(object sender, PaintEventArgs e)
        {
            if (tileset == null) return;

            // Draw selection
            using (Pen lPen = new Pen(Brushes.Yellow, 1))
            {
                lPen.DashPattern = dashPattern;

                e.Graphics.DrawRectangle(lPen,
                    tilesetSelection.X * 8 * zoom,
                    tilesetSelection.Y * 8 * zoom,
                    tilesetSelection.Width * 8 * zoom - 1,
                    tilesetSelection.Height * 8 * zoom - 1);
            }

            if (mouseOnTileset)
                using (Pen mPen = new Pen(Brushes.Red, 1))
                {
                    mPen.DashPattern = dashPattern;

                    if (mouseSelectingTileset)
                    {
                        //! block select
                        e.Graphics.DrawLine(mPen,
                            oldMouseTileset.X * 8 * zoom,
                            oldMouseTileset.Y * 8 * zoom,
                        oldMouseTileset.X * 8 * zoom,
                        (mouseTileset.Y + 1) * 8 * zoom);
                        e.Graphics.DrawLine(mPen,
                            oldMouseTileset.X * 8 * zoom,
                            oldMouseTileset.Y * 8 * zoom,
                            (mouseTileset.X + 1) * 8 * zoom,
                            oldMouseTileset.Y * 8 * zoom);
                        e.Graphics.DrawLine(mPen,
                            (mouseTileset.X + 1) * 8 * zoom,
                            (mouseTileset.Y + 1) * 8 * zoom,
                            oldMouseTileset.X * 8 * zoom,
                            (mouseTileset.Y + 1) * 8 * zoom);
                        e.Graphics.DrawLine(mPen,
                            (mouseTileset.X + 1) * 8 * zoom,
                            (mouseTileset.Y + 1) * 8 * zoom,
                            (mouseTileset.X + 1) * 8 * zoom,
                            oldMouseTileset.Y * 8 * zoom);
                    }
                    else
                    {
                        //! square cursor
                        e.Graphics.DrawRectangle(mPen,
                            mouseTileset.X * 8 * zoom,
                            mouseTileset.Y * 8 * zoom,
                            8 * zoom - 1,
                            8 * zoom - 1);
                    }
                }
        }

        private void pTileset_MouseEnter(object sender, EventArgs e)
        {
            mouseOnTileset = true;
            pTileset.Invalidate();
        }

        private void pTileset_MouseLeave(object sender, EventArgs e)
        {
            mouseOnTileset = false;
            pTileset.Invalidate();
        }

        private void pTileset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseSelectingTileset = true;
                oldMouseTileset = new Point(e.X / (8 * zoom), e.Y / (8 * zoom));
                pTileset.Invalidate();
            }
        }

        private void pTileset_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (mouseSelectingTileset)
                {
                    mouseSelectingTileset = false;
                    Point start = new Point(0, 0);
                    Point end = new Point(0, 0);

                    // x
                    if (mouseTileset.X >= oldMouseTileset.X)
                    {
                        start.X = oldMouseTileset.X;
                        end.X = mouseTileset.X + 1;
                    }
                    else
                    {
                        start.X = mouseTileset.X + 1;
                        end.X = oldMouseTileset.X;
                    }
                    // y
                    if (mouseTileset.Y >= oldMouseTileset.Y)
                    {
                        start.Y = oldMouseTileset.Y;
                        end.Y = mouseTileset.Y + 1;
                    }
                    else
                    {
                        start.Y = mouseTileset.Y + 1;
                        end.Y = oldMouseTileset.Y;
                    }

                    //selectionStartTS = start;
                    //selectionSizeTS = new Size(end.X - start.X, end.Y - start.Y);
                    tilesetSelection = new Rectangle(start, new Size(end.X - start.X, end.Y - start.Y));

                    if (tilesetSelection.Width == 0) tilesetSelection.Width = 1;
                    if (tilesetSelection.Height == 0) tilesetSelection.Height = 1;
                }
                else
                {
                    //selectionStartTS = mousePosTS;
                    //selectionSizeTS = new Size(1, 1);
                    tilesetSelection = new Rectangle(mouseTileset, new Size(1, 1));
                }

                int tile = tilesetSelection.X + (tilesetSelection.Y * tilesPerRow);
                lblTile.Text = "First Tile: " + tile;
            }
        }

        private void pTileset_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / (8 * zoom);
            int y = e.Y / (8 * zoom);

            if (x < 0 || y < 0) return;
            if (e.X >= pTileset.Width || e.Y >= pTileset.Height) return;

            mouseTileset = new Point(x, y);
            pTileset.Invalidate();
        }

        private void pTilemap_Paint(object sender, PaintEventArgs e)
        {
            if (tilemap == null || tileset == null) return;

            if (mouseOnTilemap)
                using (Pen pen = new Pen(Brushes.Black))
                {
                    pen.DashPattern = dashPattern;

                    e.Graphics.DrawRectangle(pen, mouseTilemap.X * 8 * zoom, mouseTilemap.Y * 8 * zoom, tilesetSelection.Width * 8 * zoom - 1, tilesetSelection.Height * 8 * zoom - 1);
                }
        }

        private void pTilemap_MouseEnter(object sender, EventArgs e)
        {
            mouseOnTilemap = true;
            pTilemap.Invalidate();
        }

        private void pTilemap_MouseLeave(object sender, EventArgs e)
        {
            mouseOnTilemap = false;
            pTilemap.Invalidate();
        }

        private void pTilemap_MouseDown(object sender, MouseEventArgs e)
        {
            pTilemap_MouseMove(sender, e);
        }

        private void pTilemap_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pTilemap_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / (8 * zoom);
            int y = e.Y / (8 * zoom);
            mouseTilemap = new Point(x, y);

            if (x < 0 || y < 0) return;
            if (e.X >= pTilemap.Width || e.Y >= pTilemap.Height) return;

            if (e.Button == MouseButtons.Left)
            {
                // TODO: palettemap

                // loop selection
                // normal direction
                // if flipy, go from top to bottom instead
                // flipx, go from right to left instead
                
                for (int yy = 0; yy < tilesetSelection.Height; yy++)
                {
                    for (int xx = 0; xx < tilesetSelection.Width; xx++)
                    {
                        if (y + yy >= tilemap.Height || x + xx >= tilemap.Width)
                            continue;

                        //? This is the only thing that actually needs to be changed, I think
                        int tile = (tilesetSelection.X + xx) + ((tilesetSelection.Y + yy) * tilesPerRow);
                        if (chkFlipX.Checked && chkFlipY.Checked)
                        {
                            tile = (tilesetSelection.X + (tilesetSelection.Width - 1 - xx)) + ((tilesetSelection.Y + (tilesetSelection.Height - 1 - yy)) * tilesPerRow);
                        }
                        else if (chkFlipX.Checked)
                        {
                            tile = (tilesetSelection.X + (tilesetSelection.Width - 1 - xx)) + ((tilesetSelection.Y + yy) * tilesPerRow);
                        }
                        else if (chkFlipY.Checked)
                        {
                            tile = (tilesetSelection.X + xx) + ((tilesetSelection.Y + (tilesetSelection.Height - 1 - yy)) * tilesPerRow);
                        }
                        //else
                        //{
                            // pass
                        //}
                        
                        
                        tilemap[x + xx, y + yy].Value = (tile < tileset.Count ? tile : 0);
                        //? change this?
                        tilemap[x + xx, y + yy].FlipX = chkFlipX.Checked;
                        tilemap[x + xx, y + yy].FlipY = chkFlipY.Checked;
                    }
                }

                // redraw or w/e
                pTilemap.Image = tilemap.Render(tileset, zoom);
            }
            else if (mouseOnTilemap)
            {
                pTilemap.Invalidate();
            }
            //pTilemap.Invalidate();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //groupBox1.Width = e.SplitX - 3;
            //groupBox2.Location = new Point(e.SplitX + 3, groupBox2.Location.Y);
        }

        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            //groupBox1.Width = e.SplitX - 3;
            //groupBox2.Location = new Point(e.SplitX + 3, groupBox2.Location.Y);
        }
    }
}
