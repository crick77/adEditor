
namespace adEditor
{
    partial class DataPDFForm
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
            this.webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView2)).BeginInit();
            this.SuspendLayout();
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(157, 478);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(129, 55);
            this.bLoad.TabIndex = 1;
            this.bLoad.Text = "&Load file";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // webView2
            // 
            this.webView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView2.CreationProperties = null;
            this.webView2.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2.Location = new System.Drawing.Point(12, 12);
            this.webView2.Name = "webView2";
            this.webView2.Size = new System.Drawing.Size(776, 442);
            this.webView2.TabIndex = 2;
            this.webView2.ZoomFactor = 1D;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(359, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 53);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataPDFForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 564);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.webView2);
            this.Controls.Add(this.bLoad);
            this.Name = "DataPDFForm";
            this.Text = "DataPDFForm";
            this.Load += new System.EventHandler(this.DataPDFForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bLoad;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private System.Windows.Forms.Button button1;
    }
}