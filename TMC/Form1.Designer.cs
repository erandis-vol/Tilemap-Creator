// Copyright (c) 2014 itari
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at:
// 
//  http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace TMC
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuTilemap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTileset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.open = new System.Windows.Forms.OpenFileDialog();
            this.pTileset = new System.Windows.Forms.PictureBox();
            this.mnuOpenTileset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTilesetSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lTilesetHeight = new System.Windows.Forms.Label();
            this.cTilesetWidth = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTilemap,
            this.mnuTileset,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(597, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuTilemap
            // 
            this.mnuTilemap.Name = "mnuTilemap";
            this.mnuTilemap.Size = new System.Drawing.Size(62, 20);
            this.mnuTilemap.Text = "Tilemap";
            // 
            // mnuTileset
            // 
            this.mnuTileset.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenTileset,
            this.mnuTilesetSave});
            this.mnuTileset.Name = "mnuTileset";
            this.mnuTileset.Size = new System.Drawing.Size(53, 20);
            this.mnuTileset.Text = "Tileset";
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 375);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(597, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(597, 351);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pTileset);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(199, 257);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lTilesetHeight);
            this.panel1.Controls.Add(this.cTilesetWidth);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 94);
            this.panel1.TabIndex = 0;
            // 
            // open
            // 
            this.open.FileName = "openFileDialog1";
            // 
            // pTileset
            // 
            this.pTileset.Location = new System.Drawing.Point(0, 0);
            this.pTileset.Name = "pTileset";
            this.pTileset.Size = new System.Drawing.Size(16, 16);
            this.pTileset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pTileset.TabIndex = 0;
            this.pTileset.TabStop = false;
            // 
            // mnuOpenTileset
            // 
            this.mnuOpenTileset.Image = global::TMC.Properties.Resources.folder_open_image;
            this.mnuOpenTileset.Name = "mnuOpenTileset";
            this.mnuOpenTileset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenTileset.Size = new System.Drawing.Size(163, 22);
            this.mnuOpenTileset.Text = "Open";
            this.mnuOpenTileset.Click += new System.EventHandler(this.mnuOpenTileset_Click);
            // 
            // mnuTilesetSave
            // 
            this.mnuTilesetSave.Image = global::TMC.Properties.Resources.disks;
            this.mnuTilesetSave.Name = "mnuTilesetSave";
            this.mnuTilesetSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuTilesetSave.Size = new System.Drawing.Size(163, 22);
            this.mnuTilesetSave.Text = "Save As...";
            this.mnuTilesetSave.Click += new System.EventHandler(this.mnuTilesetSave_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::TMC.Properties.Resources.information;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuAbout.Size = new System.Drawing.Size(152, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // lTilesetHeight
            // 
            this.lTilesetHeight.AutoSize = true;
            this.lTilesetHeight.Location = new System.Drawing.Point(105, 6);
            this.lTilesetHeight.Name = "lTilesetHeight";
            this.lTilesetHeight.Size = new System.Drawing.Size(21, 13);
            this.lTilesetHeight.TabIndex = 8;
            this.lTilesetHeight.Text = "x ?";
            // 
            // cTilesetWidth
            // 
            this.cTilesetWidth.FormattingEnabled = true;
            this.cTilesetWidth.Location = new System.Drawing.Point(48, 3);
            this.cTilesetWidth.Name = "cTilesetWidth";
            this.cTilesetWidth.Size = new System.Drawing.Size(51, 21);
            this.cTilesetWidth.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Size:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 397);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tilemap Creator 4.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pTileset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuTilemap;
        private System.Windows.Forms.ToolStripMenuItem mnuTileset;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pTileset;
        private System.Windows.Forms.OpenFileDialog open;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenTileset;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuTilesetSave;
        private System.Windows.Forms.Label lTilesetHeight;
        private System.Windows.Forms.ComboBox cTilesetWidth;
        private System.Windows.Forms.Label label3;
    }
}

