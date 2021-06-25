
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
            this.bOk = new System.Windows.Forms.Button();
            this.videoView = new LibVLCSharp.WinForms.VideoView();
            this.bPlay = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bPause = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.videoView)).BeginInit();
            this.SuspendLayout();
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bLoad.Location = new System.Drawing.Point(13, 249);
            this.bLoad.Margin = new System.Windows.Forms.Padding(2);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(80, 28);
            this.bLoad.TabIndex = 1;
            this.bLoad.Text = "&Load video";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOk.Location = new System.Drawing.Point(465, 249);
            this.bOk.Margin = new System.Windows.Forms.Padding(2);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(71, 28);
            this.bOk.TabIndex = 2;
            this.bOk.Text = "&Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // videoView
            // 
            this.videoView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoView.BackColor = System.Drawing.Color.Black;
            this.videoView.Location = new System.Drawing.Point(13, 13);
            this.videoView.MediaPlayer = null;
            this.videoView.Name = "videoView";
            this.videoView.Size = new System.Drawing.Size(617, 220);
            this.videoView.TabIndex = 3;
            this.videoView.Text = "Video Player";
            // 
            // bPlay
            // 
            this.bPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bPlay.Location = new System.Drawing.Point(223, 249);
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(41, 23);
            this.bPlay.TabIndex = 4;
            this.bPlay.Text = "&Play";
            this.bPlay.UseVisualStyleBackColor = true;
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStop.Location = new System.Drawing.Point(341, 249);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(41, 23);
            this.bStop.TabIndex = 5;
            this.bStop.Text = "&Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(559, 249);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(71, 28);
            this.bCancel.TabIndex = 6;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bPause
            // 
            this.bPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bPause.Location = new System.Drawing.Point(283, 249);
            this.bPause.Name = "bPause";
            this.bPause.Size = new System.Drawing.Size(41, 23);
            this.bPause.TabIndex = 7;
            this.bPause.Text = "| |";
            this.bPause.UseVisualStyleBackColor = true;
            this.bPause.Click += new System.EventHandler(this.bPause_Click);
            // 
            // DataVideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 291);
            this.ControlBox = false;
            this.Controls.Add(this.bPause);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bPlay);
            this.Controls.Add(this.videoView);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.bLoad);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DataVideoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Video data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataVideoForm_FormClosing);
            this.Load += new System.EventHandler(this.DataVideoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Button bOk;
        private LibVLCSharp.WinForms.VideoView videoView;
        private System.Windows.Forms.Button bPlay;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bPause;
    }
}