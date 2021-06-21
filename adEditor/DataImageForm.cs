using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adEditor
{
    public partial class DataImageForm : Form
    {
        public DataImageForm()
        {
            InitializeComponent();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                pBox.Image = Image.FromFile(ofd.FileName);
                this.Tag = pBox.Image;
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Tag = null;
            Close();
        }

        private void DataImage_Load(object sender, EventArgs e)
        {
            if(this.Tag!=null)
            {
                pBox.Image = (Image)this.Tag;
            }
        }
    }
}
