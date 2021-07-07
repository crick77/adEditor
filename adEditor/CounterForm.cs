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
    public partial class CounterForm : Form
    {
        private TagElement te;

        public CounterForm()
        {
            InitializeComponent();
        }

        private void CounterForm_Load(object sender, EventArgs e)
        {
            te = (TagElement)this.Tag;
            int val = (te.data!=null) ? (int)te.data : -1;
            if (val!=-1)
            {
                nudValue.Value = val;
                nudValue.Select(0, nudValue.Text.Length);
                nudValue.Enabled = true;
                cbEnabled.Checked = true;
            }
            else
            {
                nudValue.Value = 1;
                nudValue.Enabled = false;
                cbEnabled.Checked = false;
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if(cbEnabled.Checked)
            {
                te.data = Convert.ToInt32(nudValue.Value);
            }
            else
            {
                te.data = -1;
            }
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {            
            Close();
        }

        private void nudValue_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                bOk_Click(sender, new EventArgs());
            }
        }

        private void cbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            nudValue.Enabled = cbEnabled.Checked;
        }
    }
}
