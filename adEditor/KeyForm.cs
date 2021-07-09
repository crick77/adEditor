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
    public partial class KeyForm : Form
    {
        public KeyForm(string fname, string type)
        {
            InitializeComponent();

            Text = "Enter your " + type + " key for file " + fname;
            if(type.ToUpper().Equals("PRIVATE"))
            {
                tbKey.MaxLength = 1540;
            }
            else
            {
                tbKey.MaxLength = 348;
            }
            tbKey.Focus();
            tbKey_TextChanged(null, new EventArgs());
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Tag = tbKey.Text;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Tag = null;
            Close();
        }

        private void tbKey_TextChanged(object sender, EventArgs e)
        {
            bOk.Enabled = (tbKey.Text.Length == tbKey.MaxLength);
        }
    }
}
