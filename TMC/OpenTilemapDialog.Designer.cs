namespace TMC
{
    partial class OpenTilemapDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenTilemapDialog));
            this.r4bpp = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cSize = new System.Windows.Forms.ComboBox();
            this.open = new System.Windows.Forms.OpenFileDialog();
            this.r8bpp = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // r4bpp
            // 
            this.r4bpp.AutoSize = true;
            this.r4bpp.Checked = true;
            this.r4bpp.Location = new System.Drawing.Point(60, 38);
            this.r4bpp.Name = "r4bpp";
            this.r4bpp.Size = new System.Drawing.Size(55, 17);
            this.r4bpp.TabIndex = 15;
            this.r4bpp.Text = "4 BPP";
            this.r4bpp.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Format:";
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(12, 12);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(219, 20);
            this.txtFile.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(237, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(194, 88);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 17;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOpen
            // 
            this.bOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpen.Location = new System.Drawing.Point(113, 88);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(75, 23);
            this.bOpen.TabIndex = 16;
            this.bOpen.Text = "Open";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Size:";
            // 
            // cSize
            // 
            this.cSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSize.FormattingEnabled = true;
            this.cSize.Location = new System.Drawing.Point(60, 61);
            this.cSize.Name = "cSize";
            this.cSize.Size = new System.Drawing.Size(171, 21);
            this.cSize.TabIndex = 19;
            // 
            // open
            // 
            this.open.FileName = "openFileDialog1";
            // 
            // r8bpp
            // 
            this.r8bpp.AutoSize = true;
            this.r8bpp.Location = new System.Drawing.Point(121, 38);
            this.r8bpp.Name = "r8bpp";
            this.r8bpp.Size = new System.Drawing.Size(55, 17);
            this.r8bpp.TabIndex = 20;
            this.r8bpp.Text = "8 BPP";
            this.r8bpp.UseVisualStyleBackColor = true;
            // 
            // OpenTilemapDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 123);
            this.Controls.Add(this.r8bpp);
            this.Controls.Add(this.cSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.r4bpp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpenTilemapDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Tilemap...";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OpenTilemapDialog_FormClosed);
            this.Load += new System.EventHandler(this.OpenTilemapDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton r4bpp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cSize;
        private System.Windows.Forms.OpenFileDialog open;
        private System.Windows.Forms.RadioButton r8bpp;
    }
}