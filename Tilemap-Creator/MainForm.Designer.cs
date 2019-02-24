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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.lTilesetSelection = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cmbTilesetWidth = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtTilesetHeight = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTilesetFlipX = new System.Windows.Forms.ToolStripButton();
            this.btnTilesetFlipY = new System.Windows.Forms.ToolStripButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pTilemap = new TMC.Controls.InterpolatedPictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rModePalette = new System.Windows.Forms.RadioButton();
            this.rModeTilemap = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.textTilemapWidth = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.textTilemapHeight = new System.Windows.Forms.ToolStripTextBox();
            this.btnTilemapResize = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTilemapShiftLeft = new System.Windows.Forms.ToolStripButton();
            this.btnTilemapShiftUp = new System.Windows.Forms.ToolStripButton();
            this.btnTilemapShiftDown = new System.Windows.Forms.ToolStripButton();
            this.btnTilemapShiftRight = new System.Windows.Forms.ToolStripButton();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItemSave = new System.Windows.Forms.MenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemTileset = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemTilemap = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemPalette = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItemEdit = new System.Windows.Forms.MenuItem();
            this.menuItemUndo = new System.Windows.Forms.MenuItem();
            this.menuItemRedo = new System.Windows.Forms.MenuItem();
            this.menuItemView = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTilemap)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 531);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1MinSize = 242;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2MinSize = 256;
            this.splitContainer1.Size = new System.Drawing.Size(800, 531);
            this.splitContainer1.SplitterDistance = 247;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pTileset);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(247, 479);
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
            this.panel1.Controls.Add(this.lTilesetSelection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 27);
            this.panel1.TabIndex = 1;
            // 
            // lTilesetSelection
            // 
            this.lTilesetSelection.AutoSize = true;
            this.lTilesetSelection.Location = new System.Drawing.Point(2, 7);
            this.lTilesetSelection.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lTilesetSelection.Name = "lTilesetSelection";
            this.lTilesetSelection.Size = new System.Drawing.Size(70, 13);
            this.lTilesetSelection.TabIndex = 9;
            this.lTilesetSelection.Text = "(0, 0) to (0, 0)";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbTilesetWidth,
            this.toolStripLabel2,
            this.txtTilesetHeight,
            this.toolStripSeparator7,
            this.btnTilesetFlipX,
            this.btnTilesetFlipY});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(247, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cmbTilesetWidth
            // 
            this.cmbTilesetWidth.AutoToolTip = true;
            this.cmbTilesetWidth.Name = "cmbTilesetWidth";
            this.cmbTilesetWidth.Size = new System.Drawing.Size(75, 25);
            this.cmbTilesetWidth.Text = "1";
            this.cmbTilesetWidth.ToolTipText = "Tileset Width";
            this.cmbTilesetWidth.SelectedIndexChanged += new System.EventHandler(this.cmbTilesetWidth_SelectedIndexChanged);
            this.cmbTilesetWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTilesetWidth_KeyPress);
            this.cmbTilesetWidth.TextChanged += new System.EventHandler(this.cmbTilesetWidth_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(15, 22);
            this.toolStripLabel2.Text = "×";
            // 
            // txtTilesetHeight
            // 
            this.txtTilesetHeight.Name = "txtTilesetHeight";
            this.txtTilesetHeight.ReadOnly = true;
            this.txtTilesetHeight.Size = new System.Drawing.Size(40, 25);
            this.txtTilesetHeight.Text = "1";
            this.txtTilesetHeight.ToolTipText = "Tileset Height";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTilesetFlipX
            // 
            this.btnTilesetFlipX.CheckOnClick = true;
            this.btnTilesetFlipX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTilesetFlipX.Image = ((System.Drawing.Image)(resources.GetObject("btnTilesetFlipX.Image")));
            this.btnTilesetFlipX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTilesetFlipX.Name = "btnTilesetFlipX";
            this.btnTilesetFlipX.Size = new System.Drawing.Size(23, 22);
            this.btnTilesetFlipX.Text = "Flip X";
            this.btnTilesetFlipX.ToolTipText = "Flip X";
            // 
            // btnTilesetFlipY
            // 
            this.btnTilesetFlipY.CheckOnClick = true;
            this.btnTilesetFlipY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTilesetFlipY.Image = ((System.Drawing.Image)(resources.GetObject("btnTilesetFlipY.Image")));
            this.btnTilesetFlipY.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTilesetFlipY.Name = "btnTilesetFlipY";
            this.btnTilesetFlipY.Size = new System.Drawing.Size(23, 22);
            this.btnTilesetFlipY.Text = "Flip Y";
            this.btnTilesetFlipY.ToolTipText = "Flip Y";
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pTilemap);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 52);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(550, 479);
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
            this.panel3.Controls.Add(this.rModePalette);
            this.panel3.Controls.Add(this.rModeTilemap);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(550, 27);
            this.panel3.TabIndex = 1;
            // 
            // rModePalette
            // 
            this.rModePalette.Appearance = System.Windows.Forms.Appearance.Button;
            this.rModePalette.AutoSize = true;
            this.rModePalette.Location = new System.Drawing.Point(79, 2);
            this.rModePalette.Margin = new System.Windows.Forms.Padding(2);
            this.rModePalette.Name = "rModePalette";
            this.rModePalette.Size = new System.Drawing.Size(91, 23);
            this.rModePalette.TabIndex = 1;
            this.rModePalette.Text = "Edit Palettemap";
            this.rModePalette.UseVisualStyleBackColor = true;
            this.rModePalette.CheckedChanged += new System.EventHandler(this.rMode_CheckedChanged);
            // 
            // rModeTilemap
            // 
            this.rModeTilemap.Appearance = System.Windows.Forms.Appearance.Button;
            this.rModeTilemap.AutoSize = true;
            this.rModeTilemap.Checked = true;
            this.rModeTilemap.Location = new System.Drawing.Point(0, 2);
            this.rModeTilemap.Margin = new System.Windows.Forms.Padding(2);
            this.rModeTilemap.Name = "rModeTilemap";
            this.rModeTilemap.Size = new System.Drawing.Size(75, 23);
            this.rModeTilemap.TabIndex = 0;
            this.rModeTilemap.TabStop = true;
            this.rModeTilemap.Text = "Edit Tilemap";
            this.rModeTilemap.UseVisualStyleBackColor = true;
            this.rModeTilemap.CheckedChanged += new System.EventHandler(this.rMode_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textTilemapWidth,
            this.toolStripLabel1,
            this.textTilemapHeight,
            this.btnTilemapResize,
            this.toolStripSeparator6,
            this.btnTilemapShiftLeft,
            this.btnTilemapShiftUp,
            this.btnTilemapShiftDown,
            this.btnTilemapShiftRight});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(550, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // textTilemapWidth
            // 
            this.textTilemapWidth.MaxLength = 3;
            this.textTilemapWidth.Name = "textTilemapWidth";
            this.textTilemapWidth.Size = new System.Drawing.Size(40, 25);
            this.textTilemapWidth.Text = "1";
            this.textTilemapWidth.ToolTipText = "Tilemap Width";
            this.textTilemapWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTilemapWidth_KeyPress);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(15, 22);
            this.toolStripLabel1.Text = "×";
            // 
            // textTilemapHeight
            // 
            this.textTilemapHeight.MaxLength = 3;
            this.textTilemapHeight.Name = "textTilemapHeight";
            this.textTilemapHeight.Size = new System.Drawing.Size(40, 25);
            this.textTilemapHeight.Text = "1";
            this.textTilemapHeight.ToolTipText = "Tilemap Height";
            this.textTilemapHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTilemapHeight_KeyPress);
            // 
            // btnTilemapResize
            // 
            this.btnTilemapResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTilemapResize.Image = ((System.Drawing.Image)(resources.GetObject("btnTilemapResize.Image")));
            this.btnTilemapResize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTilemapResize.Name = "btnTilemapResize";
            this.btnTilemapResize.Size = new System.Drawing.Size(23, 22);
            this.btnTilemapResize.Text = "Resize Tilemap";
            this.btnTilemapResize.Click += new System.EventHandler(this.btnTilemapResize_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTilemapShiftLeft
            // 
            this.btnTilemapShiftLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTilemapShiftLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnTilemapShiftLeft.Image")));
            this.btnTilemapShiftLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTilemapShiftLeft.Name = "btnTilemapShiftLeft";
            this.btnTilemapShiftLeft.Size = new System.Drawing.Size(23, 22);
            this.btnTilemapShiftLeft.Text = "Shift Tilemap Left";
            this.btnTilemapShiftLeft.Click += new System.EventHandler(this.btnTilemapShiftLeft_Click);
            // 
            // btnTilemapShiftUp
            // 
            this.btnTilemapShiftUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTilemapShiftUp.Image = ((System.Drawing.Image)(resources.GetObject("btnTilemapShiftUp.Image")));
            this.btnTilemapShiftUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTilemapShiftUp.Name = "btnTilemapShiftUp";
            this.btnTilemapShiftUp.Size = new System.Drawing.Size(23, 22);
            this.btnTilemapShiftUp.Text = "Shift Tilemap Up";
            this.btnTilemapShiftUp.Click += new System.EventHandler(this.btnTilemapShiftUp_Click);
            // 
            // btnTilemapShiftDown
            // 
            this.btnTilemapShiftDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTilemapShiftDown.Image = ((System.Drawing.Image)(resources.GetObject("btnTilemapShiftDown.Image")));
            this.btnTilemapShiftDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTilemapShiftDown.Name = "btnTilemapShiftDown";
            this.btnTilemapShiftDown.Size = new System.Drawing.Size(23, 22);
            this.btnTilemapShiftDown.Text = "Shift Tilemap Down";
            this.btnTilemapShiftDown.Click += new System.EventHandler(this.btnTilemapShiftDown_Click);
            // 
            // btnTilemapShiftRight
            // 
            this.btnTilemapShiftRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTilemapShiftRight.Image = global::TMC.Properties.Resources.arrow;
            this.btnTilemapShiftRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTilemapShiftRight.Name = "btnTilemapShiftRight";
            this.btnTilemapShiftRight.Size = new System.Drawing.Size(23, 22);
            this.btnTilemapShiftRight.Text = "Shift Tilemap Right";
            this.btnTilemapShiftRight.Click += new System.EventHandler(this.btnTilemapShiftRight_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemTileset,
            this.menuItemTilemap,
            this.menuItemPalette,
            this.menuItemEdit,
            this.menuItemView,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.menuItem1,
            this.menuItemExit});
            this.menuItemFile.Text = "&File";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 0;
            this.menuItemOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItemOpen.Text = "&Open";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Index = 1;
            this.menuItemSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Index = 2;
            this.menuItemSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.menuItemSaveAs.Text = "Save &As...";
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 3;
            this.menuItem1.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 4;
            this.menuItemExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemTileset
            // 
            this.menuItemTileset.Index = 1;
            this.menuItemTileset.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            this.menuItemTileset.Text = "Tile&set";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Import";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Export";
            // 
            // menuItemTilemap
            // 
            this.menuItemTilemap.Index = 2;
            this.menuItemTilemap.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem5});
            this.menuItemTilemap.Text = "Tile&map";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "Import";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "Export";
            // 
            // menuItemPalette
            // 
            this.menuItemPalette.Index = 3;
            this.menuItemPalette.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7});
            this.menuItemPalette.Text = "&Palette";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "Import";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "Export";
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Index = 4;
            this.menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemUndo,
            this.menuItemRedo});
            this.menuItemEdit.Text = "&Edit";
            // 
            // menuItemUndo
            // 
            this.menuItemUndo.Index = 0;
            this.menuItemUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.menuItemUndo.Text = "Undo";
            // 
            // menuItemRedo
            // 
            this.menuItemRedo.Index = 1;
            this.menuItemRedo.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.menuItemRedo.Text = "Redo";
            // 
            // menuItemView
            // 
            this.menuItemView.Index = 5;
            this.menuItemView.Text = "&View";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 6;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAbout});
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 0;
            this.menuItemAbout.Text = "&About Tilemap Creator";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 555);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tilemap Creator";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pTilemap)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private TMC.Controls.InterpolatedPictureBox pTileset;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private TMC.Controls.InterpolatedPictureBox pTilemap;
        private System.Windows.Forms.ToolStripStatusLabel lPosition;
        private System.Windows.Forms.ToolStripStatusLabel lZoom;
        private System.Windows.Forms.ToolStripStatusLabel lTile;
        private System.Windows.Forms.ToolStripStatusLabel lPalette;
        private System.Windows.Forms.ToolStripStatusLabel lFlip;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rModePalette;
        private System.Windows.Forms.RadioButton rModeTilemap;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripTextBox textTilemapWidth;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox textTilemapHeight;
        private System.Windows.Forms.ToolStripButton btnTilemapResize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnTilemapShiftLeft;
        private System.Windows.Forms.ToolStripButton btnTilemapShiftUp;
        private System.Windows.Forms.ToolStripButton btnTilemapShiftDown;
        private System.Windows.Forms.ToolStripButton btnTilemapShiftRight;
        private System.Windows.Forms.ToolStripComboBox cmbTilesetWidth;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnTilesetFlipX;
        private System.Windows.Forms.ToolStripButton btnTilesetFlipY;
        private System.Windows.Forms.ToolStripTextBox txtTilesetHeight;
        private System.Windows.Forms.Label lTilesetSelection;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemTileset;
        private System.Windows.Forms.MenuItem menuItemTilemap;
        private System.Windows.Forms.MenuItem menuItemPalette;
        private System.Windows.Forms.MenuItem menuItemEdit;
        private System.Windows.Forms.MenuItem menuItemView;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.MenuItem menuItemUndo;
        private System.Windows.Forms.MenuItem menuItemRedo;
        private System.Windows.Forms.MenuItem menuItemOpen;
        private System.Windows.Forms.MenuItem menuItemSave;
        private System.Windows.Forms.MenuItem menuItemSaveAs;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
    }
}

