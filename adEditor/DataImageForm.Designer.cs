
namespace adEditor
{
    partial class DataImageForm
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
            this.pBox = new System.Windows.Forms.PictureBox();
            this.pBoxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            this.pBoxMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pBox
            // 
            this.pBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBox.ContextMenuStrip = this.pBoxMenuStrip;
            this.pBox.Location = new System.Drawing.Point(3, 2);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(796, 476);
            this.pBox.TabIndex = 0;
            this.pBox.TabStop = false;
            // 
            // pBoxMenuStrip
            // 
            this.pBoxMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.pBoxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem});
            this.pBoxMenuStrip.Name = "pBoxMenuStrip";
            this.pBoxMenuStrip.Size = new System.Drawing.Size(230, 42);
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(229, 38);
            this.loadImageToolStripMenuItem.Text = "Load image...";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(207, 498);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(142, 54);
            this.bOk.TabIndex = 1;
            this.bOk.Text = "&Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(475, 498);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(126, 54);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // DataImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 564);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.pBox);
            this.Name = "DataImage";
            this.Text = "DataImage";
            this.Load += new System.EventHandler(this.DataImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            this.pBoxMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pBox;
        private System.Windows.Forms.ContextMenuStrip pBoxMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCancel;
    }
}