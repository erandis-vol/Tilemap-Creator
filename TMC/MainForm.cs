using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace TMC
{
    public partial class MainForm : Form
    {
        #region Palette
        private Color[] palette = new Color[] { Color.FromArgb(160, 214, 54, 64), Color.FromArgb(160, 188, 122, 54), Color.FromArgb(160, 48, 180, 196), Color.FromArgb(160, 100, 186, 182), Color.FromArgb(160, 190, 96, 224), Color.FromArgb(160, 148, 184, 80), Color.FromArgb(160, 240, 148, 32), Color.FromArgb(160, 32, 240, 148), Color.FromArgb(160, 254, 210, 64), Color.FromArgb(160, 196, 54, 24), Color.FromArgb(160, 78, 170, 110), Color.FromArgb(160, 182, 224, 188), Color.FromArgb(160, 162, 74, 74), Color.FromArgb(160, 192, 182, 96), Color.FromArgb(160, 80, 148, 184), Color.FromArgb(160, 148, 32, 240) };
        #endregion
        
        private Tilemap tileMap;
        private Tileset tileSet;

        private bool mc = false;

        // tilemap stuff
        private Point mousePosTM = new Point(0, 0);
        private bool onTM = false;
        private bool draggingTM = false;
        private Point areaSelectStartTM = new Point(0, 0);

        // tileset
        private Point mousePosTS = new Point(0, 0);
        private bool onTS = false;
        private int widthTS = 1;

        private ColorMode colorModeTS;
        private Color[] paletteTS;

        private int selectedTile = 0, selectedPal = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mnuTilemap.Enabled = false;
            mnuSaveTS.Enabled = false;

            mnuPalette.Enabled = false;
            mnuEditPal.Enabled = false;
            mnuExportPal.Enabled = false;

            //mnuOptions.Visible = false;

            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            pnlPalette.Visible = false;
            cPalette.SelectedIndex = 0;

            paletteTS = null;
            colorModeTS = ColorMode.ColorTrue;

            //paletteBox1.ColorMode = ColorMode.Color16;
            //paletteBox1.Palette = Helper.GenerateGreyscalePalette(16);

            pPreview.Image = new Bitmap(32, 32);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(tileSet != null) tileSet.Dispose();
        }

        private void mnuOpenTM_Click(object sender, EventArgs e)
        {
            OpenTilemapDialog od = new OpenTilemapDialog();

            if (od.ShowDialog() != DialogResult.OK) return;

            // ...
            mc = true;

            string ext = Path.GetExtension(od.FileName).ToLower();

            if (ext == ".rmp")
            {
                tileMap = Tilemap.FromSphereMap(od.FileName);
            }
            else
            {
                tileMap = Tilemap.FromRaw(od.FileName, od.SelectedSize.Width, od.BPP);
            }

            txtWidthTM.Value = tileMap.Width;
            txtHeightTM.Value = tileMap.Height;

            // bpp mode
            mnu4Bpp.Checked = (od.BPP == BitDepth.BPP4);
            mnu8Bpp.Checked = (od.BPP == BitDepth.BPP8);
            ChangeMode();

            pTilemap.Image = tileMap.Draw(tileSet);

            mc = false;
        }

        private void mnuSaveTM_Click(object sender, EventArgs e)
        {
            BitDepth bpp = (mnu4Bpp.Checked ? BitDepth.BPP4 : BitDepth.BPP8);
            SaveTilemapDialog sd = new SaveTilemapDialog(bpp);
            //sd.ShowDialog();
            //MessageBox.Show("Dialog: " + sd.DialogResult);

            if (sd.ShowDialog() != DialogResult.OK) return;

            tileMap.Save(sd.FileName, bpp,
                (sd.AddExtra ? sd.Extra : -1));
            MessageBox.Show("Saved!");
        }

        private void mnu4Bpp_Click(object sender, EventArgs e)
        {
            mnu4Bpp.Checked = !mnu4Bpp.Checked;
            mnu8Bpp.Checked = !mnu4Bpp.Checked;
            ChangeMode();
        }

        private void mnu8Bpp_Click(object sender, EventArgs e)
        {
            mnu8Bpp.Checked = !mnu8Bpp.Checked;
            mnu4Bpp.Checked = !mnu8Bpp.Checked;
            ChangeMode();
        }

        private void mnuClearTM_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < tileMap.Height; y++)
            {
                for (int x = 0; x < tileMap.Width; x++)
                {
                    if (rPM.Checked)
                        tileMap[x, y].Palette = 0;
                    else
                        tileMap[x, y].Tile = 0;
                }
            }

            if (rTM.Checked) pTilemap.Image = tileMap.Draw(tileSet);
            pTilemap.Invalidate();
        }

        private void mnuFillTM_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < tileMap.Height; y++)
            {
                for (int x = 0; x < tileMap.Width; x++)
                {
                    if (rPM.Checked)
                        tileMap[x, y].Palette = selectedPal;
                    else
                        tileMap[x, y].Tile = selectedTile;
                }
            }

            if (rTM.Checked) pTilemap.Image = tileMap.Draw(tileSet);
            pTilemap.Invalidate();
        }   

        private void mnuImportTS_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Images|*.bmp;*.png";
            openFileDialog.Title = "Import Tileset...";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            mc = true;

            Stopwatch watch = Stopwatch.StartNew();

            tileMap = Helper.ImageToTilemap(openFileDialog.FileName, out tileSet);

            watch.Stop();
            MessageBox.Show("Time to create: " + watch.Elapsed.TotalSeconds.ToString() + " s");

            txtWidthTM.Value = tileMap.Width;
            txtHeightTM.Value = tileMap.Height;

            cSizeTS.Items.Clear();
            foreach (Size s in tileSet.Sizes)
            {
                cSizeTS.Items.Add(s.Width.ToString());// + " x " + s.Height);
            }
            cSizeTS.SelectedIndex = 0;

            int height = tileSet.Sizes[0].Height;
            lHeightTS.Text = "x " + height;

            widthTS = tileSet.Sizes[0].Width;
            selectedTile = 0;
            pTileset.Image = tileSet.Draw(widthTS);
            pTilemap.Image = tileMap.Draw(tileSet);
            UpdateTilePreview();

            groupBox1.Text = "Tileset (Not Indexed)";
            //groupBox1.Text = translationSettings["TS", "00"] + " (" + translationSettings["TS", "00a"] + ")";

            mnuSaveTS.Enabled = true;
            mnuTilemap.Enabled = true;
            mnuPalette.Enabled = true;

            paletteTS = null;
            colorModeTS = ColorMode.ColorTrue;
            mnuEditPal.Enabled = false;
            mnuExportPal.Enabled = false;

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            //watch.Stop();
            //statusBar1.Text = "Time elapsed: " + watch.Elapsed.TotalSeconds.ToString() + " s";

            mc = false;
        }

        private void mnuOpenTS_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Images|*.bmp;*.png";
            openFileDialog.Title = "Open Tileset...";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            mc = true;

            tileMap = new Tilemap(30, 20);
            tileSet = new Tileset(openFileDialog.FileName);

            cSizeTS.Items.Clear();
            foreach (Size s in tileSet.Sizes)
            {
                cSizeTS.Items.Add(s.Width.ToString());// + " x " + s.Height);
            }
            cSizeTS.SelectedIndex = 0;
            
            int height = tileSet.Sizes[0].Height;
            lHeightTS.Text = "x " + height;

            widthTS = tileSet.Sizes[0].Width;
            selectedTile = 0;
            pTileset.Image = tileSet.Draw(widthTS);
            pTilemap.Image = tileMap.Draw(tileSet);
            UpdateTilePreview();

            groupBox1.Text = "Tileset (Not Indexed)";
            //groupBox1.Text = translationSettings["TS", "00"] + " (" + translationSettings["TS", "00a"] + ")";

            mnuSaveTS.Enabled = true;
            mnuTilemap.Enabled = true;
            mnuPalette.Enabled = true;

            paletteTS = null;
            colorModeTS = ColorMode.ColorTrue;
            mnuEditPal.Enabled = false;
            mnuExportPal.Enabled = false;

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            mc = false;
        }

        private void mnuSaveTS_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";
            saveFileDialog.Title = "Save Tileset As...";
            saveFileDialog.Filter = "Bitmaps|*.bmp";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            
            tileSet.Save(saveFileDialog.FileName, widthTS, colorModeTS, paletteTS);
        }

        private void mnuIndexPal_Click(object sender, EventArgs e)
        {
            paletteTS = Helper.GetBitmapPalette(tileSet.Draw(widthTS));
            colorModeTS = ColorMode.Color16;
            
            // yeah...
            for (int i = 0; i < tileSet.Length; i++)
            {
                Bitmap b = tileSet[i];
                Helper.MatchBitmapToPalette(ref b, paletteTS);
                tileSet[i] = b;
            }

            groupBox1.Text = "Tileset (Indexed)";
            //groupBox1.Text = translationSettings["TS", "00"] + " (" + translationSettings["TS", "00b"] + ")";
            
            // refresh
            pTileset.Image = tileSet.Draw(widthTS);
            pTilemap.Image = tileMap.Draw(tileSet);

            // unlock
            mnuEditPal.Enabled = true;
            mnuExportPal.Enabled = true;
        }

        private void mnuIndexPal2_Click(object sender, EventArgs e)
        {
            paletteTS = Helper.GetBitmapPalette(tileSet.Draw(widthTS), 256);
            colorModeTS = ColorMode.Color256;

            // yeah...
            for (int i = 0; i < tileSet.Length; i++)
            {
                Bitmap b = tileSet[i];
                Helper.MatchBitmapToPalette(ref b, paletteTS);
                tileSet[i] = b;
            }

            groupBox1.Text = "Tileset (Indexed)";
            //groupBox1.Text = translationSettings["TS", "00"] + " (" + translationSettings["TS", "00a"] + ")";

            // refresh
            pTileset.Image = tileSet.Draw(widthTS);
            pTilemap.Image = tileMap.Draw(tileSet);

            // unlock
            mnuEditPal.Enabled = true;
            mnuExportPal.Enabled = true;
        }  

        private void mnuExportPal_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";
            saveFileDialog.Title = "Export Tileset Palette...";
            saveFileDialog.Filter = "PaintShop Palette|*.pal|Adobe Color Table|*.act";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            string ext = Path.GetExtension(saveFileDialog.FileName).ToLower();
            if (ext == ".pal")
            {
                Helper.SavePAL(saveFileDialog.FileName, paletteTS);
            }
            else if (ext == ".act")
            {
                Helper.SaveACT(saveFileDialog.FileName, paletteTS);
            }
        }

        private void mnuEditPal_Click(object sender, EventArgs e)
        {
            PaletteDialog pd = new PaletteDialog(paletteTS, colorModeTS);
            pd.ShowDialog();

            if (pd.ColorSwaps.Length > 0)
            {
                // edit palette
                for (int i = 0; i < tileSet.Length; i++)
                {
                    Bitmap b = tileSet[i];
                    Helper.PerformColorSwaps(ref b, pd.ColorSwaps);
                    tileSet[i] = b;
                }

                // refresh
                pTileset.Image = tileSet.Draw(widthTS);
                pTilemap.Image = tileMap.Draw(tileSet);
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        private void ChangeMode()
        {
            mc = true;
            if (mnu4Bpp.Checked) // 4BPP
            {
                rPM.Visible = true;
                
                rTM.Checked = true;
                rPM.Checked = false;

                chkFlipX.Visible = true;
                chkFlipY.Visible = true;
                chkFlipX.Checked = false;
                chkFlipY.Checked = false;
            }
            else // 8 BPP
            {
                rPM.Visible = false;

                rTM.Checked = true;
                rPM.Checked = false;

                chkFlipX.Visible = false;
                chkFlipY.Visible = false;
                chkFlipX.Checked = false;
                chkFlipY.Checked = false;
            }
            
            pTilemap.Invalidate();

            mc = false;
        }

        private void cTSSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            UpdateTilesetSize();
            
            //Size size = tileSet.Sizes[cSizeTS.SelectedIndex];

            //groupBox1.Text = "Tileset (" + size.Width + " x " + size.Height + ")";
            //pTileset.Image = tileSet.Draw(size.Width);
        }

        private void cSizeTS_TextUpdate(object sender, EventArgs e)
        {
            //MessageBox.Show("Woot!");
            if (mc) return;

            UpdateTilesetSize();
        }

        private void UpdateTilesetSize()
        {
            //MessageBox.Show("Size: " + Convert.ToInt32(cSizeTS.SelectedText));
            //MessageBox.Show("'" + cSizeTS.Text + "'");

            int width;
            if (!int.TryParse(cSizeTS.Text, out width)) return;

            if (width > tileSet.Length) return;

            int height = tileSet.Length / width;
            if (tileSet.Length % width > 0) height++;

            lHeightTS.Text = "x " + height;

            widthTS = width;
            pTileset.Image = tileSet.Draw(widthTS);
        }

        private void cPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPal = cPalette.SelectedIndex;
        }

        private void txtWidthTM_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (txtWidthTM.Value > 0)
            {
                //tileMap.Resize(txtWidthTM.Value, txtHeightTM.Value);
                //pTilemap.Image = tileMap.Draw(tileSet);
            }
        }

        private void txtHeightTM_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (txtWidthTM.Value > 0)
            {
                //tileMap.Resize(txtWidthTM.Value, txtHeightTM.Value);
                //pTilemap.Image = tileMap.Draw(tileSet);
            }
        }

        private void bResizeTM_Click(object sender, EventArgs e)
        {
            if (txtWidthTM.Value > 0 && txtHeightTM.Value > 0)
            {
                tileMap.Resize(txtWidthTM.Value, txtHeightTM.Value);
                pTilemap.Image = tileMap.Draw(tileSet);
            }
        }

        private void chkPM_CheckedChanged(object sender, EventArgs e)
        {
            if (mc) return;
            
            //MessageBox.Show("LKALKSDJ");
            pTilemap.Invalidate();
            pnlPalette.Visible = rPM.Checked;

            // yeah...
            groupBox2.Text = (rPM.Checked ? "Palettemap" : "Tilemap");
            mnuActionsTM.Text = "Actions (" + (rPM.Checked ? "Palettemap" : "Tilemap") + ")";
        }

        private void pTilemap_Paint(object sender, PaintEventArgs e)
        {
            if (rPM.Checked && tileMap != null)
            {
                Font f = new Font("Gothic MS", 6);
                for (int x = 0; x < tileMap.Width; x++)
                {
                    for (int y = 0; y < tileMap.Height; y++)
                    {
                        //int index = x + y * tileMap.Width;
                        int pal = tileMap[x,y].Palette;
                        e.Graphics.FillRectangle(new SolidBrush(palette[pal]), x * 8, y * 8, 8, 8);

                        e.Graphics.DrawString(pal.ToString("X"), f, Brushes.Black, x * 8 + 1f, y * 8);
                    }
                }
            }

            if (pTilemap.Width > 240) e.Graphics.DrawLine(Pens.Black, 240, 0, 240, 160);
            if (pTilemap.Height > 160) e.Graphics.DrawLine(Pens.Black, 0, 160, 240, 160);

            if (onTM)
            {
                if (draggingTM)
                {
                    Pen yellow = new Pen(Color.Yellow);
                    yellow.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawLine(yellow, areaSelectStartTM.X * 8, areaSelectStartTM.Y * 8, mousePosTM.X * 8, areaSelectStartTM.Y * 8);
                    e.Graphics.DrawLine(yellow, areaSelectStartTM.X * 8, areaSelectStartTM.Y * 8, areaSelectStartTM.X * 8, mousePosTM.Y * 8);
                    e.Graphics.DrawLine(yellow, mousePosTM.X * 8, mousePosTM.Y * 8, areaSelectStartTM.X * 8, mousePosTM.Y * 8);
                    e.Graphics.DrawLine(yellow, mousePosTM.X * 8, mousePosTM.Y * 8, mousePosTM.X * 8, areaSelectStartTM.Y * 8);
                    
                }
                else
                    e.Graphics.DrawRectangle(Pens.Yellow, mousePosTM.X * 8, mousePosTM.Y * 8, 7, 7);
            }
        }

        private void pTilemap_MouseDown(object sender, MouseEventArgs e)
        {
            pTilemap_Click(e);
        }

        private void pTilemap_MouseMove(object sender, MouseEventArgs e)
        {
            pTilemap_Click(e);
        }

        private void pTilemap_Click(MouseEventArgs e)
        {
            if (tileSet == null || tileMap == null) return;
            
            if (!pTilemap.Bounds.Contains(e.Location)) return;
            
            int x = e.X / 8;
            int y = e.Y / 8;

            //statusBar1.Text = "(" + x + ", " + y + ") - Tile: " + tileMap[x, y].Tile + " - Palette: " + tileMap[x, y].Palette + " - X-Flip: " + tileMap[x, y].Flip.X + " - Y-Flip: " + tileMap[x, y].Flip.Y;

            statusBar1.Panels[0].Text = "(" + x + ", " + y + ")";
            statusBar1.Panels[1].Text = "Tile: " + tileMap[x, y].Tile;
            statusBar1.Panels[2].Text = "Palette: " + tileMap[x, y].Palette;
            statusBar1.Panels[3].Text = "X-Flip: " + tileMap[x, y].Flip.X;
            statusBar1.Panels[4].Text = "Y-Flip: " + tileMap[x, y].Flip.Y;


            if (e.Button == MouseButtons.Left)
            {
                //int index = x + y * tileMap.Width;
                if (mousePosTM.X != x || mousePosTM.Y != y)
                {
                    mousePosTM = new Point(x, y);
                }

                if (rPM.Checked) // Palettemap
                {
                    tileMap[x, y].Palette = selectedPal;
                    pTilemap.Invalidate();
                }
                else // Tilemap
                {
                    tileMap[x, y].Tile = selectedTile;

                    tileMap[x, y].Flip.X = chkFlipX.Checked;
                    tileMap[x, y].Flip.Y = chkFlipY.Checked;

                    Graphics g = Graphics.FromImage(pTilemap.Image);
                    g.DrawImage(Helper.FlipBitmap(tileSet[selectedTile], chkFlipX.Checked, chkFlipY.Checked), x * 8, y * 8);

                    pTilemap.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right && draggingTM)
            {
                if (mousePosTM.X != x || mousePosTM.Y != y)
                {
                    mousePosTM = new Point(x, y);
                    pTilemap.Invalidate();
                }
            }
            else
            {                
                if (mousePosTM.X != x || mousePosTM.Y != y)
                {
                    mousePosTM = new Point(x, y);
                    pTilemap.Invalidate();
                }

                if (e.Button == MouseButtons.Right)
                {
                    draggingTM = true;
                    //areaSelectTM = new Rectangle(mousePosTM, new Size(1, 1));
                    areaSelectStartTM = mousePosTM;
                }
            }
        }

        private void pTilemap_MouseUp(object sender, MouseEventArgs e)
        {
            if (draggingTM)
            {
                draggingTM = false;
                Point start = new Point(0, 0);
                Point end = new Point(0, 0);

                // y
                if (mousePosTM.X >= areaSelectStartTM.X)
                {
                    start.X = areaSelectStartTM.X;
                    end.X = mousePosTM.X;
                }
                else
                {
                    start.X = mousePosTM.X;
                    end.X = areaSelectStartTM.X;
                }
                // y
                if (mousePosTM.Y >= areaSelectStartTM.Y)
                {
                    start.Y = areaSelectStartTM.Y;
                    end.Y = mousePosTM.Y;
                }
                else
                {
                    start.Y = mousePosTM.Y;
                    end.Y = areaSelectStartTM.Y;
                }

                if (rPM.Checked)
                {
                    for (int y = start.Y; y < end.Y; y++)
                    {
                        for (int x = start.X; x < end.X; x++)
                        {
                            tileMap[x, y].Palette = selectedPal;
                        }
                    }
                }
                else
                {
                    Graphics g = Graphics.FromImage(pTilemap.Image);
                    for (int y = start.Y; y < end.Y; y++)
                    {
                        for (int x = start.X; x < end.X; x++)
                        {
                            tileMap[x, y].Tile = selectedTile;
                            tileMap[x, y].Flip.X = chkFlipX.Checked;
                            tileMap[x, y].Flip.Y = chkFlipY.Checked;

                            g.DrawImage(Helper.FlipBitmap(tileSet[selectedTile], chkFlipX.Checked, chkFlipY.Checked), x * 8, y * 8);
                        }
                    }
                }

                pTilemap.Invalidate();
            }
        }   

        private void pTilemap_MouseEnter(object sender, EventArgs e)
        {
            onTM = true;
        }

        private void pTilemap_MouseLeave(object sender, EventArgs e)
        {
            onTM = false;
            pTilemap.Invalidate();
        }

        private void pTileset_Paint(object sender, PaintEventArgs e)
        {
            if (tileSet != null)
            {
                //int width = tileSet.Sizes[cSizeTS.SelectedIndex].Width;

                e.Graphics.DrawRectangle(Pens.Red, (selectedTile % widthTS) * 8, (selectedTile / widthTS) * 8, 7, 7);
            }

            if (onTS) e.Graphics.DrawRectangle(Pens.Yellow, mousePosTS.X * 8, mousePosTS.Y * 8, 7, 7);
        }

        private void pTileset_MouseUp(object sender, MouseEventArgs e)
        {
            if (tileSet == null) return;

            //if (!pTileset.Bounds.Contains(e.Location)) return;

            int width = widthTS;

            int x = e.X / 8;
            int y = e.Y / 8;

            if (x + y * width > tileSet.Length) return;

            selectedTile = x + y * width;
            
            UpdateTilePreview();
            pTileset.Invalidate();
        }

        private void pTileset_MouseMove(object sender, MouseEventArgs e)
        {
            if (tileSet == null) return;

            int x = e.X / 8;
            int y = e.Y / 8;

            //statusBar1.Text = "(" + x + ", " + y + ")";

            statusBar1.Panels[0].Text = "(" + x + ", " + y + ")";
            statusBar1.Panels[1].Text = "Tile: " + (x + y * widthTS);
            statusBar1.Panels[2].Text = "";
            statusBar1.Panels[3].Text = "";
            statusBar1.Panels[4].Text = "";

            if (x != mousePosTS.X || y != mousePosTS.Y)
            {
                mousePosTS = new Point(x, y);
                pTileset.Invalidate();
            }
        }
        
        private void pTileset_MouseEnter(object sender, EventArgs e)
        {
            onTS = true;
        }

        private void pTileset_MouseLeave(object sender, EventArgs e)
        {
            onTS = false;
            pTileset.Invalidate();
        }

        private void chkFlipX_CheckedChanged(object sender, EventArgs e)
        {
            if (mc) return;

            UpdateTilePreview();
        }

        private void chkFlipY_CheckedChanged(object sender, EventArgs e)
        {
            if (mc) return;

            UpdateTilePreview();
        }

        private void UpdateTilePreview()
        {
            Bitmap t = Helper.FlipBitmap(tileSet[selectedTile], chkFlipX.Checked, chkFlipY.Checked);
            Graphics g = Graphics.FromImage(pPreview.Image);

            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    g.FillRectangle(new SolidBrush(t.GetPixel(x, y)), x * 4, y * 4, 4, 4);
                }

            pPreview.Invalidate();

            lTileInfo.Text = "Tile: " + selectedTile;
            //lTileInfo.Text = translationSettings["TS", "02"] + " " + selectedTile;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {

        }
        
    }
}
