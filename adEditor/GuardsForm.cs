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
    public partial class GuardsForm : Form
    {
        private TagElement te;

        public GuardsForm()
        {
            InitializeComponent();
        }

        private void cbDecrease_CheckedChanged(object sender, EventArgs e)
        {
            nudCounter.Enabled = cbDecrease.Checked;
            cbVars.Enabled = cbDecrease.Checked;
        }

        private void GuardsForm_Load(object sender, EventArgs e)
        {
            
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (te != null)
            {
                Guard g = null;
                if (cbDecrease.Checked)
                {
                    g = new Guard();
                    g.decreaseQty = (int)nudCounter.Value;
                    g.varIndex = cbVars.SelectedIndex;
                }
                if (cbExpire.Checked)
                {
                    g = (g != null) ? g : new Guard();
                    g.dateIndex = cbDates.SelectedIndex;
                }

                te.data = g;
                this.Tag = te;
            }
            Close();
        }

        public void addVar(string varName, decimal max)
        {
            ComboBoxVar cbv = new ComboBoxVar(varName, max);
            cbVars.Items.Add(cbv);
        }

        public void addExpire(string varName, DateTime date)
        {
            ComboBoxVar cbv = new ComboBoxVar(varName, date);
            cbDates.Items.Add(cbv);
        }

        private void cbExpire_CheckedChanged(object sender, EventArgs e)
        {
            cbDates.Enabled = cbExpire.Checked;
        }

        private void cbVars_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal maxVal = (decimal)((ComboBoxVar)cbVars.SelectedItem).varData;
            nudCounter.Maximum = maxVal;
        }

        private void GuardsForm_Shown(object sender, EventArgs e)
        {
            cbVars.Enabled = false;
            cbDates.Enabled = false;
            cbDecrease.Enabled = false;
            cbExpire.Enabled = false;
            nudCounter.Enabled = false;

            te = (TagElement)this.Tag;
            if (te != null)
            {
                Guard g = (Guard)te.data;
                if (g != null)
                {
                    if (g.varIndex >= 0)
                    {
                        cbDecrease.Checked = true;
                        cbDecrease.Enabled = true;
                        cbVars.Enabled = true;
                        cbVars.SelectedIndex = g.varIndex;
                        decimal maxVal = (decimal)((ComboBoxVar)cbVars.SelectedItem).varData;
                        nudCounter.Enabled = true;
                        nudCounter.Value = g.decreaseQty;
                        nudCounter.Maximum = maxVal;
                    }
                    else
                    {
                        cbDecrease.Checked = false;
                        cbVars.Enabled = false;
                        nudCounter.Enabled = false;
                    }

                    if (g.dateIndex >= 0)
                    {
                        cbExpire.Checked = true;
                        cbExpire.Enabled = true;
                        cbDates.SelectedIndex = g.dateIndex;
                        cbDates.Enabled = true;
                    }
                    else
                    {
                        cbExpire.Checked = false;
                        cbDates.Enabled = false;
                    }

                    return;
                }
            }

            cbDecrease.Enabled = cbVars.Items.Count > 0;
            if (cbVars.Items.Count > 0)
            {
                cbVars.SelectedIndex = 0;
                decimal maxVal = (decimal)((ComboBoxVar)cbVars.SelectedItem).varData;
                nudCounter.Maximum = maxVal;
            }
            cbExpire.Enabled = cbDates.Items.Count > 0;
            if (cbDates.Items.Count > 0)
            {
                cbDates.SelectedIndex = 0;
            }
        }
    }
}
