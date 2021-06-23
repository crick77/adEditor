
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
            this.bLoad = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.videoView1 = new LibVLCSharp.WinForms.VideoView();
            this.bPlay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bLoad.Location = new System.Drawing.Point(27, 204);
            this.bLoad.Margin = new System.Windows.Forms.Padding(2);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(80, 25);
            this.bLoad.TabIndex = 1;
            this.bLoad.Text = "Load video";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bClose
            // 
            this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bClose.Location = new System.Drawing.Point(295, 201);
            this.bClose.Margin = new System.Windows.Forms.Padding(2);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(71, 28);
            this.bClose.TabIndex = 2;
            this.bClose.Text = "&Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // videoView1
            // 
            this.videoView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoView1.BackColor = System.Drawing.Color.Black;
            this.videoView1.Location = new System.Drawing.Point(13, 13);
            this.videoView1.MediaPlayer = null;
            this.videoView1.Name = "videoView1";
            this.videoView1.Size = new System.Drawing.Size(375, 170);
            this.videoView1.TabIndex = 3;
            this.videoView1.Text = "videoView";
            // 
            // bPlay
            // 
            this.bPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bPlay.Location = new System.Drawing.Point(160, 204);
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(75, 23);
            this.bPlay.TabIndex = 4;
            this.bPlay.Text = "&Play";
            this.bPlay.UseVisualStyleBackColor = true;
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // DataVideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 241);
            this.Controls.Add(this.bPlay);
            this.Controls.Add(this.videoView1);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bLoad);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DataVideoForm";
            this.Text = "DataVideoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataVideoForm_FormClosing);
            this.Load += new System.EventHandler(this.DataVideoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Button bClose;
        private LibVLCSharp.WinForms.VideoView videoView1;
        private System.Windows.Forms.Button bPlay;
    }
}