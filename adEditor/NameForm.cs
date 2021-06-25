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
        private int maxLen;

        public NameForm(int maxLen)
        {
            InitializeComponent();

            this.maxLen = maxLen;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if(tbName.Text.Length<1)
            {
                MessageBox.Show("Empty name is not allowed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Tag = tbName.Text;
            bOk.DialogResult = DialogResult.OK;
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

        private void NameForm_Load(object sender, EventArgs e)
        {
            tbName.MaxLength = maxLen;
            if(Tag!=null)
            {
                tbName.Text = (string)Tag;
            }
        }
    }
}
