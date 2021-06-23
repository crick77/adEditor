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
            RefData rd = (RefData)te.data;
            DateTime dt = (DateTime)rd.data;
            if (dtPicker.MinDate > dt)
            {
                dt = dtPicker.MinDate;
                MessageBox.Show("Date/time value updated to now.");
            }
            dtPicker.Value = dt;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            RefData rd = (RefData)te.data;
            rd.data = dtPicker.Value;
            Close();
        }
    }
}
