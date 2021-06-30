using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;

namespace adEditor
{
    public partial class DataPDFForm : Form
    {
        TagElement te;
        byte[] data;
        string extension;

        public DataPDFForm(bool viewable)
        {
            InitializeComponent();

            data = null;
            extension = null;
            bOk.Enabled = bLoad.Enabled = !viewable;
        }

        private void bLoad_Click(object sender, EventArgs e)
        {            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                data = File.ReadAllBytes(openFileDialog.FileName);                
                extension = Path.GetExtension(openFileDialog.FileName).Substring(1).ToUpper();

                bZoonIn.Enabled = bZoomOut.Enabled = (data != null);

                showDoc();
            }
        }

        private void DataPDFForm_Load(object sender, EventArgs e)
        {           
            te = (TagElement)this.Tag;
            data = (byte[])te.data;
            extension = te.extension;

            bZoonIn.Enabled = bZoomOut.Enabled = (data != null);

            showDoc();
        }        

        private void bZoonIn_Click(object sender, EventArgs e)
        {
            pdfRenderer.ZoomIn();            
        }

        private void bZoomOut_Click(object sender, EventArgs e)
        {
            pdfRenderer.ZoomOut();
        }

        private void showDoc()
        {
            if (data != null)
            {
                var stream = new MemoryStream(data);
                pdfRenderer.Load(PdfDocument.Load(stream));
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            te.data = data;
            te.extension = extension;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}   
