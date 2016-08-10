namespace TMC
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tilemapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rModePalette = new System.Windows.Forms.RadioButton();
            this.rModeTilemap = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pTileset = new TMC.InterpolatedPictureBox();
            this.tTilesetHeight = new TMC.NumberBox();
            this.cTilesetWidth = new TMC.NumberComboBox();
            this.pTilemap = new TMC.InterpolatedPictureBox();
            this.tTilemapHeight = new TMC.NumberBox();
            this.tTilemapWidth = new TMC.NumberBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTilemap)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tilemapToolStripMenuItem,
            this.tilesetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tilemapToolStripMenuItem
            // 
            this.tilemapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem1});
            this.tilemapToolStripMenuItem.Name = "tilemapToolStripMenuItem";
            this.tilemapToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.tilemapToolStripMenuItem.Text = "Tilemap";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(120, 26);
            this.openToolStripMenuItem1.Text = "Open";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(120, 26);
            this.saveToolStripMenuItem1.Text = "Save";
            // 
            // tilesetToolStripMenuItem
            // 
            this.tilesetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.createToolStripMenuItem});
            this.tilesetToolStripMenuItem.Name = "tilesetToolStripMenuItem";
            this.tilesetToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.tilesetToolStripMenuItem.Text = "Tileset";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(127, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(127, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(127, 26);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 606);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(982, 24);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1MinSize = 256;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2MinSize = 256;
            this.splitContainer1.Size = new System.Drawing.Size(982, 578);
            this.splitContainer1.SplitterDistance = 473;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pTileset);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 478);
            this.panel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tTilesetHeight);
            this.panel1.Controls.Add(this.cTilesetWidth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 100);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "X";
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pTilemap);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 100);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(505, 478);
            this.panel4.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tTilemapHeight);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.tTilemapWidth);
            this.panel3.Controls.Add(this.rModePalette);
            this.panel3.Controls.Add(this.rModeTilemap);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(505, 100);
            this.panel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "X";
            // 
            // rModePalette
            // 
            this.rModePalette.Appearance = System.Windows.Forms.Appearance.Button;
            this.rModePalette.AutoSize = true;
            this.rModePalette.Location = new System.Drawing.Point(85, 67);
            this.rModePalette.Name = "rModePalette";
            this.rModePalette.Size = new System.Drawing.Size(90, 27);
            this.rModePalette.TabIndex = 1;
            this.rModePalette.Text = "Edit Palette";
            this.rModePalette.UseVisualStyleBackColor = true;
            this.rModePalette.CheckedChanged += new System.EventHandler(this.rMode_CheckedChanged);
            // 
            // rModeTilemap
            // 
            this.rModeTilemap.Appearance = System.Windows.Forms.Appearance.Button;
            this.rModeTilemap.AutoSize = true;
            this.rModeTilemap.Checked = true;
            this.rModeTilemap.Location = new System.Drawing.Point(3, 67);
            this.rModeTilemap.Name = "rModeTilemap";
            this.rModeTilemap.Size = new System.Drawing.Size(76, 27);
            this.rModeTilemap.TabIndex = 0;
            this.rModeTilemap.TabStop = true;
            this.rModeTilemap.Text = "Edit Tiles";
            this.rModeTilemap.UseVisualStyleBackColor = true;
            this.rModeTilemap.CheckedChanged += new System.EventHandler(this.rMode_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pTileset
            // 
            this.pTileset.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pTileset.Location = new System.Drawing.Point(0, 0);
            this.pTileset.Name = "pTileset";
            this.pTileset.Size = new System.Drawing.Size(16, 16);
            this.pTileset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pTileset.TabIndex = 1;
            this.pTileset.TabStop = false;
            this.pTileset.Paint += new System.Windows.Forms.PaintEventHandler(this.pTileset_Paint);
            // 
            // tTilesetHeight
            // 
            this.tTilesetHeight.Location = new System.Drawing.Point(141, 7);
            this.tTilesetHeight.MaximumValue = 2048;
            this.tTilesetHeight.MinimumValue = 1;
            this.tTilesetHeight.Name = "tTilesetHeight";
            this.tTilesetHeight.Size = new System.Drawing.Size(100, 22);
            this.tTilesetHeight.TabIndex = 5;
            this.tTilesetHeight.Text = "1";
            this.tTilesetHeight.Value = 1;
            // 
            // cTilesetWidth
            // 
            this.cTilesetWidth.FormattingEnabled = true;
            this.cTilesetWidth.Location = new System.Drawing.Point(12, 6);
            this.cTilesetWidth.MaximumValue = 2147483646;
            this.cTilesetWidth.MinimumValue = 1;
            this.cTilesetWidth.Name = "cTilesetWidth";
            this.cTilesetWidth.Size = new System.Drawing.Size(100, 24);
            this.cTilesetWidth.TabIndex = 0;
            this.cTilesetWidth.Text = "1";
            this.cTilesetWidth.Value = 1;
            // 
            // pTilemap
            // 
            this.pTilemap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pTilemap.Location = new System.Drawing.Point(0, 0);
            this.pTilemap.Name = "pTilemap";
            this.pTilemap.Size = new System.Drawing.Size(16, 16);
            this.pTilemap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pTilemap.TabIndex = 1;
            this.pTilemap.TabStop = false;
            this.pTilemap.Paint += new System.Windows.Forms.PaintEventHandler(this.pTilemap_Paint);
            // 
            // tTilemapHeight
            // 
            this.tTilemapHeight.Location = new System.Drawing.Point(310, 5);
            this.tTilemapHeight.MaximumValue = 2147483646;
            this.tTilemapHeight.MinimumValue = 1;
            this.tTilemapHeight.Name = "tTilemapHeight";
            this.tTilemapHeight.Size = new System.Drawing.Size(100, 22);
            this.tTilemapHeight.TabIndex = 4;
            this.tTilemapHeight.Text = "1";
            this.tTilemapHeight.Value = 1;
            // 
            // tTilemapWidth
            // 
            this.tTilemapWidth.Location = new System.Drawing.Point(181, 5);
            this.tTilemapWidth.MaximumValue = 2147483646;
            this.tTilemapWidth.MinimumValue = 1;
            this.tTilemapWidth.Name = "tTilemapWidth";
            this.tTilemapWidth.Size = new System.Drawing.Size(100, 22);
            this.tTilemapWidth.TabIndex = 2;
            this.tTilemapWidth.Text = "1";
            this.tTilemapWidth.Value = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 630);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Tilemap Creator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTilemap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilemapToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private InterpolatedPictureBox pTileset;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private InterpolatedPictureBox pTilemap;
        private System.Windows.Forms.RadioButton rModePalette;
        private System.Windows.Forms.RadioButton rModeTilemap;
        private NumberBox tTilemapWidth;
        private System.Windows.Forms.Label label1;
        private NumberBox tTilemapHeight;
        private System.Windows.Forms.Label label2;
        private NumberBox tTilesetHeight;
        private NumberComboBox cTilesetWidth;
    }
}

