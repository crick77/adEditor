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
    public partial class StringEditForm : Form
    {
        private TagElement te;

        public StringEditForm()
        {
            InitializeComponent();
        }

        private void StringEditForm_Shown(object sender, EventArgs e)
        {
            te = (TagElement)this.Tag;
            gbEdit.Text = te.name;
            tbValue.Text = (string)te.data;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            te.data = tbValue.Text;
            this.Tag = te;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbValue_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                bSave_Click(this, new EventArgs());
            }
        }
    }
}
