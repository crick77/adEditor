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
    public partial class NameForm : Form
    {
        public NameForm()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            this.Tag = tbName.Text;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Tag = null;
            Close();
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bOk_Click(this, new EventArgs());
            }
        }
    }
}
