namespace TMC
{
    partial class OpenImageDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.r16Color = new System.Windows.Forms.RadioButton();
            this.r256Color = new System.Windows.Forms.RadioButton();
            this.bOpen = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.r256Color);
            this.panel1.Controls.Add(this.r16Color);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 48);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bOpen);
            this.panel2.Controls.Add(this.bCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 392);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(501, 36);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackgroundImage = global::TMC.Properties.Resources.transparent_dark;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(501, 344);
            this.panel3.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 9);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(47, 13);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "File: ???";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Colors:";
            // 
            // r16Color
            // 
            this.r16Color.AutoSize = true;
            this.r16Color.Location = new System.Drawing.Point(57, 25);
            this.r16Color.Name = "r16Color";
            this.r16Color.Size = new System.Drawing.Size(108, 17);
            this.r16Color.TabIndex = 2;
            this.r16Color.TabStop = true;
            this.r16Color.Text = "16 Colors (4 BPP)";
            this.r16Color.UseVisualStyleBackColor = true;
            this.r16Color.CheckedChanged += new System.EventHandler(this.rColor_CheckedChanged);
            // 
            // r256Color
            // 
            this.r256Color.AutoSize = true;
            this.r256Color.Location = new System.Drawing.Point(171, 25);
            this.r256Color.Name = "r256Color";
            this.r256Color.Size = new System.Drawing.Size(114, 17);
            this.r256Color.TabIndex = 3;
            this.r256Color.TabStop = true;
            this.r256Color.Text = "256 Colors (4 BPP)";
            this.r256Color.UseVisualStyleBackColor = true;
            this.r256Color.CheckedChanged += new System.EventHandler(this.rColor_CheckedChanged);
            // 
            // bOpen
            // 
            this.bOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpen.Location = new System.Drawing.Point(333, 6);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(75, 23);
            this.bOpen.TabIndex = 3;
            this.bOpen.Text = "Open";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(414, 6);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // OpenImageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 428);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "OpenImageDialog";
            this.Text = "Open Image";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OpenImageDialog_FormClosed);
            this.Load += new System.EventHandler(this.OpenImageDialog_Load);
            this.Shown += new System.EventHandler(this.OpenImageDialog_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton r256Color;
        private System.Windows.Forms.RadioButton r16Color;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}