namespace TMC
{
    partial class PaletteDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteDialog));
            this.pPalette = new System.Windows.Forms.PictureBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pPalette)).BeginInit();
            this.SuspendLayout();
            // 
            // pPalette
            // 
            this.pPalette.Location = new System.Drawing.Point(12, 12);
            this.pPalette.Name = "pPalette";
            this.pPalette.Size = new System.Drawing.Size(256, 256);
            this.pPalette.TabIndex = 0;
            this.pPalette.TabStop = false;
            this.pPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.pPalette_Paint);
            this.pPalette.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pPalette_MouseDoubleClick);
            // 
            // PaletteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 280);
            this.Controls.Add(this.pPalette);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Palette";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PaletteDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pPalette)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pPalette;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}