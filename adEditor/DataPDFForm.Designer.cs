
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
            this.bZoonIn = new System.Windows.Forms.Button();
            this.pdfRenderer = new PdfiumViewer.PdfRenderer();
            this.bZoomOut = new System.Windows.Forms.Button();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(21, 555);
            this.bLoad.Margin = new System.Windows.Forms.Padding(2);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(64, 29);
            this.bLoad.TabIndex = 1;
            this.bLoad.Text = "&Load file";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bZoonIn
            // 
            this.bZoonIn.Location = new System.Drawing.Point(246, 492);
            this.bZoonIn.Margin = new System.Windows.Forms.Padding(2);
            this.bZoonIn.Name = "bZoonIn";
            this.bZoonIn.Size = new System.Drawing.Size(67, 28);
            this.bZoonIn.TabIndex = 3;
            this.bZoonIn.Text = "Zoom +";
            this.bZoonIn.UseVisualStyleBackColor = true;
            this.bZoonIn.Click += new System.EventHandler(this.bZoonIn_Click);
            // 
            // pdfRenderer
            // 
            this.pdfRenderer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfRenderer.Location = new System.Drawing.Point(12, 12);
            this.pdfRenderer.Name = "pdfRenderer";
            this.pdfRenderer.Page = 0;
            this.pdfRenderer.Rotation = PdfiumViewer.PdfRotation.Rotate0;
            this.pdfRenderer.Size = new System.Drawing.Size(732, 451);
            this.pdfRenderer.TabIndex = 5;
            this.pdfRenderer.Text = "pdfRenderer";
            this.pdfRenderer.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitHeight;
            // 
            // bZoomOut
            // 
            this.bZoomOut.Location = new System.Drawing.Point(406, 492);
            this.bZoomOut.Margin = new System.Windows.Forms.Padding(2);
            this.bZoomOut.Name = "bZoomOut";
            this.bZoomOut.Size = new System.Drawing.Size(67, 28);
            this.bZoomOut.TabIndex = 6;
            this.bZoomOut.Text = "Zoom -";
            this.bZoomOut.UseVisualStyleBackColor = true;
            this.bZoomOut.Click += new System.EventHandler(this.bZoomOut_Click);
            // 
            // bOk
            // 
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOk.Location = new System.Drawing.Point(528, 555);
            this.bOk.Margin = new System.Windows.Forms.Padding(2);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(64, 29);
            this.bOk.TabIndex = 7;
            this.bOk.Text = "&Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(657, 555);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(64, 29);
            this.bCancel.TabIndex = 8;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.pdf";
            this.openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            this.openFileDialog.FilterIndex = 0;
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Title = "Open PDF File";
            // 
            // DataPDFForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 595);
            this.ControlBox = false;
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.bZoomOut);
            this.Controls.Add(this.pdfRenderer);
            this.Controls.Add(this.bZoonIn);
            this.Controls.Add(this.bLoad);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DataPDFForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PDF Data";
            this.Load += new System.EventHandler(this.DataPDFForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Button bZoonIn;
        private PdfiumViewer.PdfRenderer pdfRenderer;
        private System.Windows.Forms.Button bZoomOut;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}