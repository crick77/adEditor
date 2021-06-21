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
    public partial class DataTextForm : Form
    {
        private TagElement te;

        public DataTextForm()
        {
            InitializeComponent();
        }

        private void DataTextForm_Load(object sender, EventArgs e)
        {
            this.te = (TagElement)this.Tag;
            tbText.Text = (string)te.data;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            te.data = tbText.Text;
            this.Tag = te;
            Close();
        }
    }
}
