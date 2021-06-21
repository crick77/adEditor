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
using Microsoft.Web.WebView2.Core;

namespace adEditor
{
    public partial class DataPDFForm : Form
    {
        TagElement te;
        string tfile;

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
            }
        }

        private async void DataPDFForm_Load(object sender, EventArgs e)
        {
            await webView2.EnsureCoreWebView2Async();
            webView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            await webView2.CoreWebView2.ExecuteScriptAsync("window.addEventListener('contextmenu', window => {window.preventDefault();alert('x');});");
            te = (TagElement)this.Tag;
            if (te.data != null)
            {
                tfile = System.IO.Path.GetTempFileName();
                File.Move(tfile, Path.ChangeExtension(tfile, te.extension));
                tfile = Path.ChangeExtension(tfile, te.extension);
                byte[] b = (byte[])te.data;
                File.WriteAllBytes(tfile, b);
                webView2.CoreWebView2.Navigate("file:///" + tfile);
                //webView2.Enabled = false;
                webView2.Visible = true;
            }
            else
            {
                webView2.Visible = false;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string ScrollToTopString = @"window.focus(); window.scrollBy(0, 100);";
            await webView2.CoreWebView2.ExecuteScriptAsync(ScrollToTopString);
        }
    }
}   
