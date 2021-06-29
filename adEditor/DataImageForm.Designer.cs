
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataImageForm));
            this.pBox = new System.Windows.Forms.PictureBox();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bLoad = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.cbDegrade = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pBox
            // 
            this.pBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("pBox.InitialImage")));
            this.pBox.Location = new System.Drawing.Point(2, 1);
            this.pBox.Margin = new System.Windows.Forms.Padding(2);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(398, 248);
            this.pBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBox.TabIndex = 0;
            this.pBox.TabStop = false;
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOk.Location = new System.Drawing.Point(233, 259);
            this.bOk.Margin = new System.Windows.Forms.Padding(2);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(71, 28);
            this.bOk.TabIndex = 1;
            this.bOk.Text = "&Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(326, 259);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(63, 28);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bLoad.Location = new System.Drawing.Point(11, 259);
            this.bLoad.Margin = new System.Windows.Forms.Padding(2);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(71, 28);
            this.bLoad.TabIndex = 3;
            this.bLoad.Text = "&Load";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // ofd
            // 
            this.ofd.Filter = "Image files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            this.ofd.FilterIndex = 0;
            this.ofd.RestoreDirectory = true;
            this.ofd.Title = "Open Image";
            // 
            // cbDegrade
            // 
            this.cbDegrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDegrade.AutoSize = true;
            this.cbDegrade.Location = new System.Drawing.Point(94, 266);
            this.cbDegrade.Name = "cbDegrade";
            this.cbDegrade.Size = new System.Drawing.Size(121, 17);
            this.cbDegrade.TabIndex = 4;
            this.cbDegrade.Text = "&Degrade each open";
            this.cbDegrade.UseVisualStyleBackColor = true;
            this.cbDegrade.CheckedChanged += new System.EventHandler(this.cbDegrade_CheckedChanged);
            // 
            // DataImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 293);
            this.ControlBox = false;
            this.Controls.Add(this.cbDegrade);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.pBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DataImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Image data";
            this.Load += new System.EventHandler(this.DataImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pBox;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.CheckBox cbDegrade;
    }
}