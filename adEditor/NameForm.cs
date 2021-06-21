﻿using System;
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
    }
}
