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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuTilemap = new System.Windows.Forms.MenuItem();
            this.mnuOpenTM = new System.Windows.Forms.MenuItem();
            this.mnuSaveTM = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mnuModeTM = new System.Windows.Forms.MenuItem();
            this.mnu4Bpp = new System.Windows.Forms.MenuItem();
            this.mnu8Bpp = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuActionsTM = new System.Windows.Forms.MenuItem();
            this.mnuClearTM = new System.Windows.Forms.MenuItem();
            this.mnuFillTM = new System.Windows.Forms.MenuItem();
            this.mnuTileset = new System.Windows.Forms.MenuItem();
            this.mnuImportTS = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mnuOpenTS = new System.Windows.Forms.MenuItem();
            this.mnuSaveTS = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuPalette = new System.Windows.Forms.MenuItem();
            this.mnuIndexPal = new System.Windows.Forms.MenuItem();
            this.mnuIndexPal2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuExportPal = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuEditPal = new System.Windows.Forms.MenuItem();
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bResizeTM = new System.Windows.Forms.Button();
            this.rPM = new System.Windows.Forms.RadioButton();
            this.rTM = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlPalette = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cPalette = new System.Windows.Forms.ComboBox();
            this.lHeightTS = new System.Windows.Forms.Label();
            this.chkFlipY = new System.Windows.Forms.CheckBox();
            this.chkFlipX = new System.Windows.Forms.CheckBox();
            this.pPreview = new System.Windows.Forms.PictureBox();
            this.lTileInfo = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cSizeTS = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel4 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel5 = new System.Windows.Forms.StatusBarPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pTileset = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pTilemap = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.txtHeightTM = new TMC.NumericTextBox();
            this.txtWidthTM = new TMC.NumericTextBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlPalette.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel5)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTilemap)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuTilemap,
            this.mnuTileset,
            this.mnuAbout});
            // 
            // mnuTilemap
            // 
            this.mnuTilemap.Index = 0;
            this.mnuTilemap.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuOpenTM,
            this.mnuSaveTM,
            this.menuItem7,
            this.mnuModeTM,
            this.menuItem1,
            this.mnuActionsTM});
            this.mnuTilemap.Text = "Tilemap";
            // 
            // mnuOpenTM
            // 
            this.mnuOpenTM.Index = 0;
            this.mnuOpenTM.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftO;
            this.mnuOpenTM.Text = "Open...";
            this.mnuOpenTM.Click += new System.EventHandler(this.mnuOpenTM_Click);
            // 
            // mnuSaveTM
            // 
            this.mnuSaveTM.Index = 1;
            this.mnuSaveTM.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.mnuSaveTM.Text = "Save As...";
            this.mnuSaveTM.Click += new System.EventHandler(this.mnuSaveTM_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 2;
            this.menuItem7.Text = "-";
            // 
            // mnuModeTM
            // 
            this.mnuModeTM.Index = 3;
            this.mnuModeTM.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnu4Bpp,
            this.mnu8Bpp});
            this.mnuModeTM.Text = "Mode";
            // 
            // mnu4Bpp
            // 
            this.mnu4Bpp.Checked = true;
            this.mnu4Bpp.Index = 0;
            this.mnu4Bpp.Text = "4 BPP";
            this.mnu4Bpp.Click += new System.EventHandler(this.mnu4Bpp_Click);
            // 
            // mnu8Bpp
            // 
            this.mnu8Bpp.Index = 1;
            this.mnu8Bpp.Text = "8 BPP";
            this.mnu8Bpp.Click += new System.EventHandler(this.mnu8Bpp_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // mnuActionsTM
            // 
            this.mnuActionsTM.Index = 5;
            this.mnuActionsTM.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuClearTM,
            this.mnuFillTM});
            this.mnuActionsTM.Text = "Actions (Tilemap)";
            // 
            // mnuClearTM
            // 
            this.mnuClearTM.Index = 0;
            this.mnuClearTM.Text = "Clear";
            this.mnuClearTM.Click += new System.EventHandler(this.mnuClearTM_Click);
            // 
            // mnuFillTM
            // 
            this.mnuFillTM.Index = 1;
            this.mnuFillTM.Text = "Fill with Selected";
            this.mnuFillTM.Click += new System.EventHandler(this.mnuFillTM_Click);
            // 
            // mnuTileset
            // 
            this.mnuTileset.Index = 1;
            this.mnuTileset.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuImportTS,
            this.menuItem6,
            this.mnuOpenTS,
            this.mnuSaveTS,
            this.menuItem2,
            this.mnuPalette});
            this.mnuTileset.Text = "Tileset";
            // 
            // mnuImportTS
            // 
            this.mnuImportTS.Index = 0;
            this.mnuImportTS.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.mnuImportTS.Text = "Import...";
            this.mnuImportTS.Click += new System.EventHandler(this.mnuImportTS_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            this.menuItem6.Text = "-";
            // 
            // mnuOpenTS
            // 
            this.mnuOpenTS.Index = 2;
            this.mnuOpenTS.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.mnuOpenTS.Text = "Open...";
            this.mnuOpenTS.Click += new System.EventHandler(this.mnuOpenTS_Click);
            // 
            // mnuSaveTS
            // 
            this.mnuSaveTS.Index = 3;
            this.mnuSaveTS.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.mnuSaveTS.Text = "Save As...";
            this.mnuSaveTS.Click += new System.EventHandler(this.mnuSaveTS_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.Text = "-";
            // 
            // mnuPalette
            // 
            this.mnuPalette.Index = 5;
            this.mnuPalette.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuIndexPal,
            this.mnuIndexPal2,
            this.menuItem3,
            this.mnuExportPal,
            this.menuItem4,
            this.mnuEditPal});
            this.mnuPalette.Text = "Palette";
            // 
            // mnuIndexPal
            // 
            this.mnuIndexPal.Index = 0;
            this.mnuIndexPal.Text = "Index (16 Color)";
            this.mnuIndexPal.Click += new System.EventHandler(this.mnuIndexPal_Click);
            // 
            // mnuIndexPal2
            // 
            this.mnuIndexPal2.Index = 1;
            this.mnuIndexPal2.Text = "Index (256 Color)";
            this.mnuIndexPal2.Click += new System.EventHandler(this.mnuIndexPal2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "-";
            // 
            // mnuExportPal
            // 
            this.mnuExportPal.Index = 3;
            this.mnuExportPal.Text = "Export";
            this.mnuExportPal.Click += new System.EventHandler(this.mnuExportPal_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.Text = "-";
            // 
            // mnuEditPal
            // 
            this.mnuEditPal.Index = 5;
            this.mnuEditPal.Text = "Edit";
            this.mnuEditPal.Click += new System.EventHandler(this.mnuEditPal_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Index = 2;
            this.mnuAbout.Text = "?";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 118);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bResizeTM);
            this.groupBox2.Controls.Add(this.rPM);
            this.groupBox2.Controls.Add(this.rTM);
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Controls.Add(this.txtHeightTM);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtWidthTM);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(211, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tilemap";
            // 
            // bResizeTM
            // 
            this.bResizeTM.Location = new System.Drawing.Point(200, 19);
            this.bResizeTM.Name = "bResizeTM";
            this.bResizeTM.Size = new System.Drawing.Size(65, 46);
            this.bResizeTM.TabIndex = 20;
            this.bResizeTM.Text = "Resize";
            this.bResizeTM.UseVisualStyleBackColor = true;
            this.bResizeTM.Click += new System.EventHandler(this.bResizeTM_Click);
            // 
            // rPM
            // 
            this.rPM.AutoSize = true;
            this.rPM.Location = new System.Drawing.Point(74, 78);
            this.rPM.Name = "rPM";
            this.rPM.Size = new System.Drawing.Size(78, 17);
            this.rPM.TabIndex = 19;
            this.rPM.Text = "Palettemap";
            this.rPM.UseVisualStyleBackColor = true;
            this.rPM.CheckedChanged += new System.EventHandler(this.chkPM_CheckedChanged);
            // 
            // rTM
            // 
            this.rTM.AutoSize = true;
            this.rTM.Checked = true;
            this.rTM.Location = new System.Drawing.Point(6, 78);
            this.rTM.Name = "rTM";
            this.rTM.Size = new System.Drawing.Size(62, 17);
            this.rTM.TabIndex = 18;
            this.rTM.TabStop = true;
            this.rTM.Text = "Tilemap";
            this.rTM.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.Location = new System.Drawing.Point(6, 71);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(259, 1);
            this.panel4.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Height:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Width:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlPalette);
            this.groupBox1.Controls.Add(this.lHeightTS);
            this.groupBox1.Controls.Add(this.chkFlipY);
            this.groupBox1.Controls.Add(this.chkFlipX);
            this.groupBox1.Controls.Add(this.pPreview);
            this.groupBox1.Controls.Add(this.lTileInfo);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.cSizeTS);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tileset";
            // 
            // pnlPalette
            // 
            this.pnlPalette.Controls.Add(this.label4);
            this.pnlPalette.Controls.Add(this.cPalette);
            this.pnlPalette.Location = new System.Drawing.Point(6, 53);
            this.pnlPalette.Name = "pnlPalette";
            this.pnlPalette.Size = new System.Drawing.Size(188, 41);
            this.pnlPalette.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Palette:";
            // 
            // cPalette
            // 
            this.cPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cPalette.FormattingEnabled = true;
            this.cPalette.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"});
            this.cPalette.Location = new System.Drawing.Point(49, 0);
            this.cPalette.Name = "cPalette";
            this.cPalette.Size = new System.Drawing.Size(139, 21);
            this.cPalette.TabIndex = 19;
            this.cPalette.SelectedIndexChanged += new System.EventHandler(this.cPalette_SelectedIndexChanged);
            // 
            // lHeightTS
            // 
            this.lHeightTS.AutoSize = true;
            this.lHeightTS.Location = new System.Drawing.Point(99, 22);
            this.lHeightTS.Name = "lHeightTS";
            this.lHeightTS.Size = new System.Drawing.Size(21, 13);
            this.lHeightTS.TabIndex = 23;
            this.lHeightTS.Text = "x 0";
            // 
            // chkFlipY
            // 
            this.chkFlipY.AutoSize = true;
            this.chkFlipY.Location = new System.Drawing.Point(65, 69);
            this.chkFlipY.Name = "chkFlipY";
            this.chkFlipY.Size = new System.Drawing.Size(52, 17);
            this.chkFlipY.TabIndex = 22;
            this.chkFlipY.Text = "Y-Flip";
            this.chkFlipY.UseVisualStyleBackColor = true;
            this.chkFlipY.CheckedChanged += new System.EventHandler(this.chkFlipY_CheckedChanged);
            // 
            // chkFlipX
            // 
            this.chkFlipX.AutoSize = true;
            this.chkFlipX.Location = new System.Drawing.Point(7, 69);
            this.chkFlipX.Name = "chkFlipX";
            this.chkFlipX.Size = new System.Drawing.Size(52, 17);
            this.chkFlipX.TabIndex = 21;
            this.chkFlipX.Text = "X-Flip";
            this.chkFlipX.UseVisualStyleBackColor = true;
            this.chkFlipX.CheckedChanged += new System.EventHandler(this.chkFlipX_CheckedChanged);
            // 
            // pPreview
            // 
            this.pPreview.Location = new System.Drawing.Point(161, 53);
            this.pPreview.Name = "pPreview";
            this.pPreview.Size = new System.Drawing.Size(33, 33);
            this.pPreview.TabIndex = 20;
            this.pPreview.TabStop = false;
            // 
            // lTileInfo
            // 
            this.lTileInfo.AutoSize = true;
            this.lTileInfo.Location = new System.Drawing.Point(7, 53);
            this.lTileInfo.Name = "lTileInfo";
            this.lTileInfo.Size = new System.Drawing.Size(36, 13);
            this.lTileInfo.TabIndex = 19;
            this.lTileInfo.Text = "Tile: 0";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel5.Location = new System.Drawing.Point(7, 46);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(187, 1);
            this.panel5.TabIndex = 18;
            // 
            // cSizeTS
            // 
            this.cSizeTS.FormattingEnabled = true;
            this.cSizeTS.Location = new System.Drawing.Point(43, 19);
            this.cSizeTS.Name = "cSizeTS";
            this.cSizeTS.Size = new System.Drawing.Size(50, 21);
            this.cSizeTS.TabIndex = 1;
            this.cSizeTS.SelectedIndexChanged += new System.EventHandler(this.cTSSize_SelectedIndexChanged);
            this.cSizeTS.TextUpdate += new System.EventHandler(this.cSizeTS_TextUpdate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size:";
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 419);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanel2,
            this.statusBarPanel3,
            this.statusBarPanel4,
            this.statusBarPanel5});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(624, 22);
            this.statusBar1.TabIndex = 1;
            this.statusBar1.Text = "statusBar1";
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Text = "(0,0)";
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Text = "Tile: 0";
            // 
            // statusBarPanel3
            // 
            this.statusBarPanel3.Name = "statusBarPanel3";
            this.statusBarPanel3.Text = "Palette: 0";
            // 
            // statusBarPanel4
            // 
            this.statusBarPanel4.Name = "statusBarPanel4";
            this.statusBarPanel4.Text = "X-Flip: False";
            // 
            // statusBarPanel5
            // 
            this.statusBarPanel5.Name = "statusBarPanel5";
            this.statusBarPanel5.Text = "Y-Flip: False";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 301);
            this.panel2.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.AutoScroll = true;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.pTileset);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(180, 301);
            this.panel7.TabIndex = 1;
            // 
            // pTileset
            // 
            this.pTileset.Location = new System.Drawing.Point(0, 0);
            this.pTileset.Name = "pTileset";
            this.pTileset.Size = new System.Drawing.Size(128, 128);
            this.pTileset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pTileset.TabIndex = 1;
            this.pTileset.TabStop = false;
            this.pTileset.Paint += new System.Windows.Forms.PaintEventHandler(this.pTileset_Paint);
            this.pTileset.MouseEnter += new System.EventHandler(this.pTileset_MouseEnter);
            this.pTileset.MouseLeave += new System.EventHandler(this.pTileset_MouseLeave);
            this.pTileset.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pTileset_MouseMove);
            this.pTileset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pTileset_MouseUp);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pTilemap);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(180, 118);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(444, 301);
            this.panel3.TabIndex = 3;
            // 
            // pTilemap
            // 
            this.pTilemap.Location = new System.Drawing.Point(0, 0);
            this.pTilemap.Name = "pTilemap";
            this.pTilemap.Size = new System.Drawing.Size(128, 128);
            this.pTilemap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pTilemap.TabIndex = 1;
            this.pTilemap.TabStop = false;
            this.pTilemap.Paint += new System.Windows.Forms.PaintEventHandler(this.pTilemap_Paint);
            this.pTilemap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pTilemap_MouseDown);
            this.pTilemap.MouseEnter += new System.EventHandler(this.pTilemap_MouseEnter);
            this.pTilemap.MouseLeave += new System.EventHandler(this.pTilemap_MouseLeave);
            this.pTilemap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pTilemap_MouseMove);
            this.pTilemap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pTilemap_MouseUp);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // txtHeightTM
            // 
            this.txtHeightTM.Location = new System.Drawing.Point(54, 45);
            this.txtHeightTM.Name = "txtHeightTM";
            this.txtHeightTM.Size = new System.Drawing.Size(140, 20);
            this.txtHeightTM.TabIndex = 3;
            this.txtHeightTM.Text = "20";
            this.txtHeightTM.Value = 20;
            this.txtHeightTM.TextChanged += new System.EventHandler(this.txtHeightTM_TextChanged);
            // 
            // txtWidthTM
            // 
            this.txtWidthTM.Location = new System.Drawing.Point(54, 19);
            this.txtWidthTM.Name = "txtWidthTM";
            this.txtWidthTM.Size = new System.Drawing.Size(140, 20);
            this.txtWidthTM.TabIndex = 1;
            this.txtWidthTM.Text = "30";
            this.txtWidthTM.Value = 30;
            this.txtWidthTM.TextChanged += new System.EventHandler(this.txtWidthTM_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tilemap Creator 3.5";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlPalette.ResumeLayout(false);
            this.pnlPalette.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel5)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTilemap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuTilemap;
        private System.Windows.Forms.MenuItem mnuTileset;
        private System.Windows.Forms.MenuItem mnuAbout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MenuItem mnuOpenTM;
        private System.Windows.Forms.MenuItem mnuSaveTM;
        private System.Windows.Forms.MenuItem mnuImportTS;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem mnuOpenTS;
        private System.Windows.Forms.MenuItem mnuSaveTS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox pTilemap;
        private System.Windows.Forms.ComboBox cSizeTS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private NumericTextBox txtWidthTM;
        private NumericTextBox txtHeightTM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rPM;
        private System.Windows.Forms.RadioButton rTM;
        private System.Windows.Forms.Button bResizeTM;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lTileInfo;
        private System.Windows.Forms.CheckBox chkFlipY;
        private System.Windows.Forms.CheckBox chkFlipX;
        private System.Windows.Forms.PictureBox pPreview;
        private System.Windows.Forms.Label lHeightTS;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox pTileset;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mnuPalette;
        private System.Windows.Forms.MenuItem mnuEditPal;
        private System.Windows.Forms.MenuItem mnuIndexPal;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem mnuExportPal;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.StatusBarPanel statusBarPanel1;
        private System.Windows.Forms.StatusBarPanel statusBarPanel2;
        private System.Windows.Forms.StatusBarPanel statusBarPanel3;
        private System.Windows.Forms.StatusBarPanel statusBarPanel4;
        private System.Windows.Forms.StatusBarPanel statusBarPanel5;
        private System.Windows.Forms.MenuItem mnuIndexPal2;
        private System.Windows.Forms.MenuItem mnuActionsTM;
        private System.Windows.Forms.MenuItem mnuClearTM;
        private System.Windows.Forms.MenuItem mnuFillTM;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Panel pnlPalette;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cPalette;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem mnuModeTM;
        private System.Windows.Forms.MenuItem mnu4Bpp;
        private System.Windows.Forms.MenuItem mnu8Bpp;
    }
}

