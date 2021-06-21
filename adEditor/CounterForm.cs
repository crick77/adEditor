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
            RefData rd = (RefData)te.data;
            nudValue.Value = (decimal)rd.data;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            RefData rd = (RefData)te.data;
            rd.data = nudValue.Value;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            te = null;
            Close();
        }
    }
}
