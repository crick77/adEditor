using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace adEditor
{
    public partial class DataTextForm : Form
    {
        private TagElement te;        

        public DataTextForm(bool viewable)
        {
            InitializeComponent();

            bOk.Enabled = bLoadFromFile.Enabled = !viewable;
            tbText.ReadOnly = viewable;
        }

        private void DataTextForm_Load(object sender, EventArgs e)
        {
            this.te = (TagElement)this.Tag;
            if (te != null)
            {
                byte[] d = (byte[])te.data;
                if(d!=null) tbText.Text = Encoding.UTF8.GetString(d, 0, d.Length);
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            te.data = Encoding.UTF8.GetBytes(tbText.Text);
            te.extension = "TXT";
            
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bLoadFromFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                if(tbText.TextLength!=0 && MessageBox.Show("There is something already in the data field, sure to overwrite?", "Are you seure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.No) return;
                
                tbText.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
            }
        }
    }
}
