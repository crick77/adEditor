
namespace adEditor
{
    partial class DataVideoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataVideoForm));
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.bLoad = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(12, 12);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(776, 350);
            this.axWindowsMediaPlayer.TabIndex = 0;
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(209, 392);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(160, 48);
            this.bLoad.TabIndex = 1;
            this.bLoad.Text = "Load video";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(457, 386);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(142, 54);
            this.bClose.TabIndex = 2;
            this.bClose.Text = "&Close";
            this.bClose.UseVisualStyleBackColor = true;
            // 
            // DataVideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 464);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.Name = "DataVideoForm";
            this.Text = "DataVideoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataVideoForm_FormClosing);
            this.Load += new System.EventHandler(this.DataVideoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Button bClose;
    }
}