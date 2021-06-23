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

        public DataPDFForm()
        {
            InitializeComponent();
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF files (*.pdf)|*.pdf";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] b = File.ReadAllBytes(ofd.FileName);
                te.data = b;
                te.extension = Path.GetExtension(ofd.FileName);
                showDoc();
            }
        }

        private void DataPDFForm_Load(object sender, EventArgs e)
        {           
            te = (TagElement)this.Tag;
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
            if (te.data != null)
            {
                var stream = new MemoryStream((byte[])te.data);

                // Load PDF Document into WinForms Control
                pdfRenderer.Load(PdfDocument.Load(stream));
            }
        }
    }
}   
