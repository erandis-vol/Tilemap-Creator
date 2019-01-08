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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuTileset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreateTileset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpenTileset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveTileset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveTilesetAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPalette = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReduceColors = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSwapColors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportColors = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTilemap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewTilemap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpenTilemap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveTilemap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveTilemapAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAllowFlipping = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.lTile = new System.Windows.Forms.ToolStripStatusLabel();
            this.lPalette = new System.Windows.Forms.ToolStripStatusLabel();
            this.lFlip = new System.Windows.Forms.ToolStripStatusLabel();
            this.lZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pTileset = new TMC.Controls.InterpolatedPictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkTilesetFlipY = new System.Windows.Forms.CheckBox();
            this.chkTilesetFlipX = new System.Windows.Forms.CheckBox();
            this.lTilesetSelection = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tTilesetHeight = new TMC.Controls.NumberBox();
            this.cTilesetWidth = new TMC.Controls.NumberComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pTilemap = new TMC.Controls.InterpolatedPictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.bTilemapRight = new System.Windows.Forms.Button();
            this.bTilemapLeft = new System.Windows.Forms.Button();
            this.bTilemapDown = new System.Windows.Forms.Button();
            this.bTilemapUp = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bTilemapResize = new System.Windows.Forms.Button();
            this.tTilemapHeight = new TMC.Controls.NumberBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tTilemapWidth = new TMC.Controls.NumberBox();
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
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTileset,
            this.mnuTilemap,
            this.mnuView,
            this.mnuSettings,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(736, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuTileset
            // 
            this.mnuTileset.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreateTileset,
            this.toolStripSeparator1,
            this.mnuOpenTileset,
            this.mnuSaveTileset,
            this.mnuSaveTilesetAs,
            this.toolStripSeparator2,
            this.mnuPalette});
            this.mnuTileset.Name = "mnuTileset";
            this.mnuTileset.Size = new System.Drawing.Size(53, 20);
            this.mnuTileset.Text = "Tileset";
            // 
            // mnuCreateTileset
            // 
            this.mnuCreateTileset.Name = "mnuCreateTileset";
            this.mnuCreateTileset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuCreateTileset.Size = new System.Drawing.Size(146, 22);
            this.mnuCreateTileset.Text = "Create";
            this.mnuCreateTileset.Click += new System.EventHandler(this.mnuCreateTileset_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // mnuOpenTileset
            // 
            this.mnuOpenTileset.Name = "mnuOpenTileset";
            this.mnuOpenTileset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenTileset.Size = new System.Drawing.Size(146, 22);
            this.mnuOpenTileset.Text = "Open";
            this.mnuOpenTileset.Click += new System.EventHandler(this.mnuOpenTileset_Click);
            // 
            // mnuSaveTileset
            // 
            this.mnuSaveTileset.Name = "mnuSaveTileset";
            this.mnuSaveTileset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSaveTileset.Size = new System.Drawing.Size(146, 22);
            this.mnuSaveTileset.Text = "Save";
            this.mnuSaveTileset.Click += new System.EventHandler(this.mnuSaveTileset_Click);
            // 
            // mnuSaveTilesetAs
            // 
            this.mnuSaveTilesetAs.Name = "mnuSaveTilesetAs";
            this.mnuSaveTilesetAs.Size = new System.Drawing.Size(146, 22);
            this.mnuSaveTilesetAs.Text = "Save As...";
            this.mnuSaveTilesetAs.Click += new System.EventHandler(this.mnuSaveTilesetAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // mnuPalette
            // 
            this.mnuPalette.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReduceColors,
            this.mnuSwapColors,
            this.toolStripSeparator3,
            this.mnuExportColors});
            this.mnuPalette.Name = "mnuPalette";
            this.mnuPalette.Size = new System.Drawing.Size(146, 22);
            this.mnuPalette.Text = "Colors";
            // 
            // mnuReduceColors
            // 
            this.mnuReduceColors.Name = "mnuReduceColors";
            this.mnuReduceColors.Size = new System.Drawing.Size(113, 22);
            this.mnuReduceColors.Text = "Reduce";
            this.mnuReduceColors.Click += new System.EventHandler(this.mnuReduceColors_Click);
            // 
            // mnuSwapColors
            // 
            this.mnuSwapColors.Name = "mnuSwapColors";
            this.mnuSwapColors.Size = new System.Drawing.Size(113, 22);
            this.mnuSwapColors.Text = "Swap";
            this.mnuSwapColors.Click += new System.EventHandler(this.mnuSwapColors_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(110, 6);
            // 
            // mnuExportColors
            // 
            this.mnuExportColors.Name = "mnuExportColors";
            this.mnuExportColors.Size = new System.Drawing.Size(113, 22);
            this.mnuExportColors.Text = "Export";
            this.mnuExportColors.Click += new System.EventHandler(this.mnuExportColors_Click);
            // 
            // mnuTilemap
            // 
            this.mnuTilemap.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewTilemap,
            this.toolStripSeparator5,
            this.mnuOpenTilemap,
            this.mnuSaveTilemap,
            this.mnuSaveTilemapAs});
            this.mnuTilemap.Name = "mnuTilemap";
            this.mnuTilemap.Size = new System.Drawing.Size(62, 20);
            this.mnuTilemap.Text = "Tilemap";
            // 
            // mnuNewTilemap
            // 
            this.mnuNewTilemap.Name = "mnuNewTilemap";
            this.mnuNewTilemap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNewTilemap.Size = new System.Drawing.Size(180, 22);
            this.mnuNewTilemap.Text = "New";
            this.mnuNewTilemap.Click += new System.EventHandler(this.mnuNewTilemap_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuOpenTilemap
            // 
            this.mnuOpenTilemap.Name = "mnuOpenTilemap";
            this.mnuOpenTilemap.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.mnuOpenTilemap.Size = new System.Drawing.Size(180, 22);
            this.mnuOpenTilemap.Text = "Open";
            this.mnuOpenTilemap.Click += new System.EventHandler(this.mnuOpenTilemap_Click);
            // 
            // mnuSaveTilemap
            // 
            this.mnuSaveTilemap.Name = "mnuSaveTilemap";
            this.mnuSaveTilemap.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mnuSaveTilemap.Size = new System.Drawing.Size(180, 22);
            this.mnuSaveTilemap.Text = "Save";
            this.mnuSaveTilemap.Click += new System.EventHandler(this.mnuSaveTilemap_Click);
            // 
            // mnuSaveTilemapAs
            // 
            this.mnuSaveTilemapAs.Name = "mnuSaveTilemapAs";
            this.mnuSaveTilemapAs.Size = new System.Drawing.Size(180, 22);
            this.mnuSaveTilemapAs.Text = "Save As...";
            this.mnuSaveTilemapAs.Click += new System.EventHandler(this.mnuSaveTilemapAs_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuZoomIn,
            this.mnuZoomOut,
            this.toolStripSeparator4,
            this.mnuGrid,
            this.mnuStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "View";
            // 
            // mnuZoomIn
            // 
            this.mnuZoomIn.Name = "mnuZoomIn";
            this.mnuZoomIn.ShortcutKeyDisplayString = "Ctrl++";
            this.mnuZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.mnuZoomIn.Size = new System.Drawing.Size(180, 22);
            this.mnuZoomIn.Text = "Zoom In";
            this.mnuZoomIn.Click += new System.EventHandler(this.mnuZoomIn_Click);
            // 
            // mnuZoomOut
            // 
            this.mnuZoomOut.Name = "mnuZoomOut";
            this.mnuZoomOut.ShortcutKeyDisplayString = "Ctrl+-";
            this.mnuZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.mnuZoomOut.Size = new System.Drawing.Size(180, 22);
            this.mnuZoomOut.Text = "Zoom Out";
            this.mnuZoomOut.Click += new System.EventHandler(this.mnuZoomOut_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuGrid
            // 
            this.mnuGrid.Checked = true;
            this.mnuGrid.CheckOnClick = true;
            this.mnuGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuGrid.Name = "mnuGrid";
            this.mnuGrid.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.mnuGrid.Size = new System.Drawing.Size(180, 22);
            this.mnuGrid.Text = "Grid";
            this.mnuGrid.Click += new System.EventHandler(this.mnuGrid_Click);
            // 
            // mnuStatusBar
            // 
            this.mnuStatusBar.Checked = true;
            this.mnuStatusBar.CheckOnClick = true;
            this.mnuStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuStatusBar.Name = "mnuStatusBar";
            this.mnuStatusBar.Size = new System.Drawing.Size(180, 22);
            this.mnuStatusBar.Text = "Status bar";
            this.mnuStatusBar.Click += new System.EventHandler(this.mnuStatusBar_Click);
            // 
            // mnuSettings
            // 
            this.mnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAllowFlipping});
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(61, 20);
            this.mnuSettings.Text = "Settings";
            // 
            // mnuAllowFlipping
            // 
            this.mnuAllowFlipping.Checked = true;
            this.mnuAllowFlipping.CheckOnClick = true;
            this.mnuAllowFlipping.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuAllowFlipping.Name = "mnuAllowFlipping";
            this.mnuAllowFlipping.Size = new System.Drawing.Size(180, 22);
            this.mnuAllowFlipping.Text = "Allow Tile Flipping";
            this.mnuAllowFlipping.Click += new System.EventHandler(this.mnuAllowFlip_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuAbout.Size = new System.Drawing.Size(237, 22);
            this.mnuAbout.Text = "About Tilemap Creator";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lPosition,
            this.lTile,
            this.lPalette,
            this.lFlip,
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
            // lTile
            // 
            this.lTile.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lTile.Name = "lTile";
            this.lTile.Size = new System.Drawing.Size(42, 19);
            this.lTile.Text = "Tile: 0";
            // 
            // lPalette
            // 
            this.lPalette.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lPalette.Name = "lPalette";
            this.lPalette.Size = new System.Drawing.Size(59, 19);
            this.lPalette.Text = "Palette: 0";
            // 
            // lFlip
            // 
            this.lFlip.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lFlip.Name = "lFlip";
            this.lFlip.Size = new System.Drawing.Size(65, 19);
            this.lFlip.Text = "Flip: None";
            // 
            // lZoom
            // 
            this.lZoom.Name = "lZoom";
            this.lZoom.Size = new System.Drawing.Size(73, 19);
            this.lZoom.Text = "Zoom: 200%";
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
            this.panel2.Location = new System.Drawing.Point(0, 109);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 355);
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
            this.panel1.Size = new System.Drawing.Size(242, 109);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTilesetFlipY);
            this.groupBox2.Controls.Add(this.chkTilesetFlipX);
            this.groupBox2.Controls.Add(this.lTilesetSelection);
            this.groupBox2.Location = new System.Drawing.Point(11, 49);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(182, 56);
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
            this.groupBox1.Location = new System.Drawing.Point(11, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(182, 43);
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
            this.tTilesetHeight.Location = new System.Drawing.Point(102, 17);
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
            this.panel4.Location = new System.Drawing.Point(0, 109);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(491, 355);
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
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.rModePalette);
            this.panel3.Controls.Add(this.rModeTilemap);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(491, 109);
            this.panel3.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.bTilemapRight);
            this.groupBox4.Controls.Add(this.bTilemapLeft);
            this.groupBox4.Controls.Add(this.bTilemapDown);
            this.groupBox4.Controls.Add(this.bTilemapUp);
            this.groupBox4.Location = new System.Drawing.Point(188, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(168, 77);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Shift";
            // 
            // bTilemapRight
            // 
            this.bTilemapRight.Location = new System.Drawing.Point(114, 34);
            this.bTilemapRight.Name = "bTilemapRight";
            this.bTilemapRight.Size = new System.Drawing.Size(48, 23);
            this.bTilemapRight.TabIndex = 3;
            this.bTilemapRight.Text = "Right";
            this.bTilemapRight.UseVisualStyleBackColor = true;
            this.bTilemapRight.Click += new System.EventHandler(this.bTilemapRight_Click);
            // 
            // bTilemapLeft
            // 
            this.bTilemapLeft.Location = new System.Drawing.Point(6, 34);
            this.bTilemapLeft.Name = "bTilemapLeft";
            this.bTilemapLeft.Size = new System.Drawing.Size(48, 23);
            this.bTilemapLeft.TabIndex = 2;
            this.bTilemapLeft.Text = "Left";
            this.bTilemapLeft.UseVisualStyleBackColor = true;
            this.bTilemapLeft.Click += new System.EventHandler(this.bTilemapLeft_Click);
            // 
            // bTilemapDown
            // 
            this.bTilemapDown.Location = new System.Drawing.Point(60, 48);
            this.bTilemapDown.Name = "bTilemapDown";
            this.bTilemapDown.Size = new System.Drawing.Size(48, 23);
            this.bTilemapDown.TabIndex = 1;
            this.bTilemapDown.Text = "Down";
            this.bTilemapDown.UseVisualStyleBackColor = true;
            this.bTilemapDown.Click += new System.EventHandler(this.bTilemapDown_Click);
            // 
            // bTilemapUp
            // 
            this.bTilemapUp.Location = new System.Drawing.Point(60, 19);
            this.bTilemapUp.Name = "bTilemapUp";
            this.bTilemapUp.Size = new System.Drawing.Size(48, 23);
            this.bTilemapUp.TabIndex = 0;
            this.bTilemapUp.Text = "Up";
            this.bTilemapUp.UseVisualStyleBackColor = true;
            this.bTilemapUp.Click += new System.EventHandler(this.bTilemapUp_Click);
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
            this.rModePalette.Location = new System.Drawing.Point(66, 82);
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
            this.rModeTilemap.Location = new System.Drawing.Point(2, 82);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuTileset;
        private System.Windows.Forms.ToolStripMenuItem mnuTilemap;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenTileset;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveTileset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateTileset;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenTilemap;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveTilemap;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private TMC.Controls.InterpolatedPictureBox pTileset;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private TMC.Controls.InterpolatedPictureBox pTilemap;
        private System.Windows.Forms.RadioButton rModePalette;
        private System.Windows.Forms.RadioButton rModeTilemap;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private TMC.Controls.NumberBox tTilesetHeight;
        private TMC.Controls.NumberComboBox cTilesetWidth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lTilesetSelection;
        private System.Windows.Forms.CheckBox chkTilesetFlipY;
        private System.Windows.Forms.CheckBox chkTilesetFlipX;
        private System.Windows.Forms.GroupBox groupBox3;
        private TMC.Controls.NumberBox tTilemapHeight;
        private System.Windows.Forms.Label label1;
        private TMC.Controls.NumberBox tTilemapWidth;
        private System.Windows.Forms.Button bTilemapResize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuPalette;
        private System.Windows.Forms.ToolStripStatusLabel lPosition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuExportColors;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuZoomIn;
        private System.Windows.Forms.ToolStripMenuItem mnuZoomOut;
        private System.Windows.Forms.ToolStripStatusLabel lZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuStatusBar;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuAllowFlipping;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuNewTilemap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button bTilemapRight;
        private System.Windows.Forms.Button bTilemapLeft;
        private System.Windows.Forms.Button bTilemapDown;
        private System.Windows.Forms.Button bTilemapUp;
        private System.Windows.Forms.ToolStripMenuItem mnuSwapColors;
        private System.Windows.Forms.ToolStripStatusLabel lTile;
        private System.Windows.Forms.ToolStripStatusLabel lPalette;
        private System.Windows.Forms.ToolStripStatusLabel lFlip;
        private System.Windows.Forms.ToolStripMenuItem mnuReduceColors;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveTilesetAs;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveTilemapAs;
    }
}

