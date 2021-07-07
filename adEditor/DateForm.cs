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
    public partial class DateForm : Form
    {
        private TagElement te;

        public DateForm()
        {
            InitializeComponent();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DateForm_Load(object sender, EventArgs e)
        {
            dtPicker.MinDate = DateTime.Now;
            te = (TagElement)this.Tag;
            
            if(te.data!=null) { 
                DateTime dt = (DateTime)te.data;                        
                if (dtPicker.MinDate > dt)
                {
                    dt = dtPicker.MinDate;
                    MessageBox.Show("Date/time value updated to current date/time.");
                }

                dtPicker.Value = dt;
                cbEnabled.Checked = true;
                dtPicker.Enabled = true;
            }
            else
            {
                cbEnabled.Checked = false;
                dtPicker.MinDate = DateTime.Now;
                dtPicker.Enabled = false;
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (cbEnabled.Checked)
            {
                te.data = dtPicker.Value;
            }
            else
            {
                te.data = null;
            }
            Close();
        }

        private void cbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            dtPicker.Enabled = cbEnabled.Checked;
        }
    }
}
