
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
            this.pBox.Location = new System.Drawing.Point(2, 1);
            this.pBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(398, 248);
            this.pBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBox.TabIndex = 0;
            this.pBox.TabStop = false;
            // 
            // pBoxMenuStrip
            // 
            this.pBoxMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.pBoxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem});
            this.pBoxMenuStrip.Name = "pBoxMenuStrip";
            this.pBoxMenuStrip.Size = new System.Drawing.Size(146, 26);
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.loadImageToolStripMenuItem.Text = "Load image...";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOk.Location = new System.Drawing.Point(104, 259);
            this.bOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(71, 28);
            this.bOk.TabIndex = 1;
            this.bOk.Text = "&Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bCancel.Location = new System.Drawing.Point(238, 259);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(63, 28);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // DataImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 293);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.pBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DataImageForm";
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