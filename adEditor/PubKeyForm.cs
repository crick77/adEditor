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
    public partial class PubKeyForm : Form
    {
        public PubKeyForm()
        {
            InitializeComponent();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Tag = tbPubKey.Text;
            Close();
        }

        private void tbPubKey_TextChanged(object sender, EventArgs e)
        {
            bOk.Enabled = (tbPubKey.Text.Length == 348);
        }

        private void PubKeyForm_Load(object sender, EventArgs e)
        {
            tbPubKey.Text = "";
            tbPubKey_TextChanged(sender, new EventArgs());
        }
    }
}
