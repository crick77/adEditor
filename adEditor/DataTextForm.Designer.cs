
namespace adEditor
{
    partial class DataTextForm
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
            this.tbText = new System.Windows.Forms.TextBox();
            this.bOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(117, 54);
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbText.ShortcutsEnabled = false;
            this.tbText.Size = new System.Drawing.Size(1242, 157);
            this.tbText.TabIndex = 0;
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(597, 246);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(207, 59);
            this.bOk.TabIndex = 1;
            this.bOk.Text = "Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // DataTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 356);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.tbText);
            this.Name = "DataTextForm";
            this.Text = "Text data";
            this.Load += new System.EventHandler(this.DataTextForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Button bOk;
    }
}