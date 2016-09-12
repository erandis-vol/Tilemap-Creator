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
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowTileFlippingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.lZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pTileset = new TMC.InterpolatedPictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkTilesetFlipY = new System.Windows.Forms.CheckBox();
            this.chkTilesetFlipX = new System.Windows.Forms.CheckBox();
            this.lTilesetSelection = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tTilesetHeight = new TMC.NumberBox();
            this.cTilesetWidth = new TMC.NumberComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pTilemap = new TMC.InterpolatedPictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bTilemapResize = new System.Windows.Forms.Button();
            this.tTilemapHeight = new TMC.NumberBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tTilemapWidth = new TMC.NumberBox();
            this.rModePalette = new System.Windows.Forms.RadioButton();
            this.rModeTilemap = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTilemap)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tilemapToolStripMenuItem,
            this.tilesetToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(736, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tilemapToolStripMenuItem
            // 
            this.tilemapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem1});
            this.tilemapToolStripMenuItem.Name = "tilemapToolStripMenuItem";
            this.tilemapToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.tilemapToolStripMenuItem.Text = "Tilemap";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // tilesetToolStripMenuItem
            // 
            this.tilesetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.toolStripSeparator1,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.colorsToolStripMenuItem});
            this.tilesetToolStripMenuItem.Name = "tilesetToolStripMenuItem";
            this.tilesetToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.tilesetToolStripMenuItem.Text = "Tileset";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.createToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPaletteToolStripMenuItem,
            this.toolStripSeparator3,
            this.exportToolStripMenuItem});
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            this.colorsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.colorsToolStripMenuItem.Text = "Colors";
            // 
            // editPaletteToolStripMenuItem
            // 
            this.editPaletteToolStripMenuItem.Name = "editPaletteToolStripMenuItem";
            this.editPaletteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.editPaletteToolStripMenuItem.Text = "View";
            this.editPaletteToolStripMenuItem.Click += new System.EventHandler(this.editPaletteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(104, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.toolStripSeparator4,
            this.gridToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.zoomOutToolStripMenuItem.Text = "Zoom Out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(126, 6);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Checked = true;
            this.gridToolStripMenuItem.CheckOnClick = true;
            this.gridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.statusBarToolStripMenuItem.Text = "Status bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.statusBarToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allowTileFlippingToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // allowTileFlippingToolStripMenuItem
            // 
            this.allowTileFlippingToolStripMenuItem.Checked = true;
            this.allowTileFlippingToolStripMenuItem.CheckOnClick = true;
            this.allowTileFlippingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allowTileFlippingToolStripMenuItem.Name = "allowTileFlippingToolStripMenuItem";
            this.allowTileFlippingToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.allowTileFlippingToolStripMenuItem.Text = "Allow Tile Flipping";
            this.allowTileFlippingToolStripMenuItem.Click += new System.EventHandler(this.allowTileFlippingToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lPosition,
            this.lZoom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 488);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(736, 24);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lPosition
            // 
            this.lPosition.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lPosition.Name = "lPosition";
            this.lPosition.Size = new System.Drawing.Size(29, 19);
            this.lPosition.Text = "0, 0";
            // 
            // lZoom
            // 
            this.lZoom.Name = "lZoom";
            this.lZoom.Size = new System.Drawing.Size(35, 19);
            this.lZoom.Text = "200%";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1MinSize = 242;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2MinSize = 256;
            this.splitContainer1.Size = new System.Drawing.Size(736, 464);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pTileset);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 112);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 352);
            this.panel2.TabIndex = 2;
            // 
            // pTileset
            // 
            this.pTileset.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pTileset.Location = new System.Drawing.Point(0, 0);
            this.pTileset.Margin = new System.Windows.Forms.Padding(2);
            this.pTileset.Name = "pTileset";
            this.pTileset.Size = new System.Drawing.Size(12, 13);
            this.pTileset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pTileset.TabIndex = 1;
            this.pTileset.TabStop = false;
            this.pTileset.Paint += new System.Windows.Forms.PaintEventHandler(this.pTileset_Paint);
            this.pTileset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pTileset_MouseDown);
            this.pTileset.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pTileset_MouseMove);
            this.pTileset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pTileset_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 112);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTilesetFlipY);
            this.groupBox2.Controls.Add(this.chkTilesetFlipX);
            this.groupBox2.Controls.Add(this.lTilesetSelection);
            this.groupBox2.Location = new System.Drawing.Point(9, 48);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(181, 56);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selection";
            // 
            // chkTilesetFlipY
            // 
            this.chkTilesetFlipY.AutoSize = true;
            this.chkTilesetFlipY.Location = new System.Drawing.Point(58, 34);
            this.chkTilesetFlipY.Margin = new System.Windows.Forms.Padding(2);
            this.chkTilesetFlipY.Name = "chkTilesetFlipY";
            this.chkTilesetFlipY.Size = new System.Drawing.Size(52, 17);
            this.chkTilesetFlipY.TabIndex = 2;
            this.chkTilesetFlipY.Text = "Flip Y";
            this.chkTilesetFlipY.UseVisualStyleBackColor = true;
            // 
            // chkTilesetFlipX
            // 
            this.chkTilesetFlipX.AutoSize = true;
            this.chkTilesetFlipX.Location = new System.Drawing.Point(4, 34);
            this.chkTilesetFlipX.Margin = new System.Windows.Forms.Padding(2);
            this.chkTilesetFlipX.Name = "chkTilesetFlipX";
            this.chkTilesetFlipX.Size = new System.Drawing.Size(52, 17);
            this.chkTilesetFlipX.TabIndex = 1;
            this.chkTilesetFlipX.Text = "Flip X";
            this.chkTilesetFlipX.UseVisualStyleBackColor = true;
            // 
            // lTilesetSelection
            // 
            this.lTilesetSelection.AutoSize = true;
            this.lTilesetSelection.Location = new System.Drawing.Point(4, 15);
            this.lTilesetSelection.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lTilesetSelection.Name = "lTilesetSelection";
            this.lTilesetSelection.Size = new System.Drawing.Size(70, 13);
            this.lTilesetSelection.TabIndex = 0;
            this.lTilesetSelection.Text = "(0, 0) to (0, 0)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tTilesetHeight);
            this.groupBox1.Controls.Add(this.cTilesetWidth);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(181, 42);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "X";
            // 
            // tTilesetHeight
            // 
            this.tTilesetHeight.Location = new System.Drawing.Point(101, 17);
            this.tTilesetHeight.Margin = new System.Windows.Forms.Padding(2);
            this.tTilesetHeight.MaximumValue = 2048;
            this.tTilesetHeight.MinimumValue = 1;
            this.tTilesetHeight.Name = "tTilesetHeight";
            this.tTilesetHeight.ReadOnly = true;
            this.tTilesetHeight.Size = new System.Drawing.Size(76, 20);
            this.tTilesetHeight.TabIndex = 8;
            this.tTilesetHeight.Text = "1";
            this.tTilesetHeight.Value = 1;
            // 
            // cTilesetWidth
            // 
            this.cTilesetWidth.FormattingEnabled = true;
            this.cTilesetWidth.Location = new System.Drawing.Point(4, 17);
            this.cTilesetWidth.Margin = new System.Windows.Forms.Padding(2);
            this.cTilesetWidth.MaximumValue = 2147483646;
            this.cTilesetWidth.MinimumValue = 1;
            this.cTilesetWidth.Name = "cTilesetWidth";
            this.cTilesetWidth.Size = new System.Drawing.Size(76, 21);
            this.cTilesetWidth.TabIndex = 7;
            this.cTilesetWidth.Text = "1";
            this.cTilesetWidth.Value = 1;
            this.cTilesetWidth.SelectedIndexChanged += new System.EventHandler(this.cTilesetWidth_SelectedIndexChanged);
            this.cTilesetWidth.TextChanged += new System.EventHandler(this.cTilesetWidth_TextChanged);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pTilemap);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 112);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(491, 352);
            this.panel4.TabIndex = 2;
            // 
            // pTilemap
            // 
            this.pTilemap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pTilemap.Location = new System.Drawing.Point(0, 0);
            this.pTilemap.Margin = new System.Windows.Forms.Padding(2);
            this.pTilemap.Name = "pTilemap";
            this.pTilemap.Size = new System.Drawing.Size(12, 13);
            this.pTilemap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pTilemap.TabIndex = 1;
            this.pTilemap.TabStop = false;
            this.pTilemap.Paint += new System.Windows.Forms.PaintEventHandler(this.pTilemap_Paint);
            this.pTilemap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pTilemap_MouseDown);
            this.pTilemap.MouseLeave += new System.EventHandler(this.pTilemap_MouseLeave);
            this.pTilemap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pTilemap_MouseMove);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.rModePalette);
            this.panel3.Controls.Add(this.rModeTilemap);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(491, 112);
            this.panel3.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bTilemapResize);
            this.groupBox3.Controls.Add(this.tTilemapHeight);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tTilemapWidth);
            this.groupBox3.Location = new System.Drawing.Point(2, 2);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(181, 64);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Size";
            // 
            // bTilemapResize
            // 
            this.bTilemapResize.Location = new System.Drawing.Point(4, 40);
            this.bTilemapResize.Margin = new System.Windows.Forms.Padding(2);
            this.bTilemapResize.Name = "bTilemapResize";
            this.bTilemapResize.Size = new System.Drawing.Size(173, 19);
            this.bTilemapResize.TabIndex = 8;
            this.bTilemapResize.Text = "Resize";
            this.bTilemapResize.UseVisualStyleBackColor = true;
            this.bTilemapResize.Click += new System.EventHandler(this.bTilemapResize_Click);
            // 
            // tTilemapHeight
            // 
            this.tTilemapHeight.Location = new System.Drawing.Point(101, 17);
            this.tTilemapHeight.Margin = new System.Windows.Forms.Padding(2);
            this.tTilemapHeight.MaximumValue = 2147483646;
            this.tTilemapHeight.MinimumValue = 1;
            this.tTilemapHeight.Name = "tTilemapHeight";
            this.tTilemapHeight.Size = new System.Drawing.Size(76, 20);
            this.tTilemapHeight.TabIndex = 7;
            this.tTilemapHeight.Text = "1";
            this.tTilemapHeight.Value = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "X";
            // 
            // tTilemapWidth
            // 
            this.tTilemapWidth.Location = new System.Drawing.Point(4, 17);
            this.tTilemapWidth.Margin = new System.Windows.Forms.Padding(2);
            this.tTilemapWidth.MaximumValue = 2147483646;
            this.tTilemapWidth.MinimumValue = 1;
            this.tTilemapWidth.Name = "tTilemapWidth";
            this.tTilemapWidth.Size = new System.Drawing.Size(76, 20);
            this.tTilemapWidth.TabIndex = 5;
            this.tTilemapWidth.Text = "1";
            this.tTilemapWidth.Value = 1;
            // 
            // rModePalette
            // 
            this.rModePalette.Appearance = System.Windows.Forms.Appearance.Button;
            this.rModePalette.AutoSize = true;
            this.rModePalette.Location = new System.Drawing.Point(63, 85);
            this.rModePalette.Margin = new System.Windows.Forms.Padding(2);
            this.rModePalette.Name = "rModePalette";
            this.rModePalette.Size = new System.Drawing.Size(71, 23);
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
            this.rModeTilemap.Location = new System.Drawing.Point(2, 85);
            this.rModeTilemap.Margin = new System.Windows.Forms.Padding(2);
            this.rModeTilemap.Name = "rModeTilemap";
            this.rModeTilemap.Size = new System.Drawing.Size(60, 23);
            this.rModeTilemap.TabIndex = 0;
            this.rModeTilemap.TabStop = true;
            this.rModeTilemap.Text = "Edit Tiles";
            this.rModeTilemap.UseVisualStyleBackColor = true;
            this.rModeTilemap.CheckedChanged += new System.EventHandler(this.rMode_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 512);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Tilemap Creator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pTilemap)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private NumberBox tTilesetHeight;
        private NumberComboBox cTilesetWidth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lTilesetSelection;
        private System.Windows.Forms.CheckBox chkTilesetFlipY;
        private System.Windows.Forms.CheckBox chkTilesetFlipX;
        private System.Windows.Forms.GroupBox groupBox3;
        private NumberBox tTilemapHeight;
        private System.Windows.Forms.Label label1;
        private NumberBox tTilemapWidth;
        private System.Windows.Forms.Button bTilemapResize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lPosition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allowTileFlippingToolStripMenuItem;
    }
}

