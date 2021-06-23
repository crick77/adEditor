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
        private Guard g;

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
            if(!cbDecrease.Checked && !cbExpire.Checked)
            {
                MessageBox.Show("Must choose at least one of the two guards!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            g = null;
            if (cbDecrease.Checked)
            {
                g = new Guard();
                g.decreaseQty = (int)nudCounter.Value;
                ComboBoxVar cbv = (ComboBoxVar)cbVars.SelectedItem;
                g.varCounterGuid = cbv.varGuid;
            }
            if (cbExpire.Checked)
            {
                g = (g != null) ? g : new Guard();
                ComboBoxVar cbv = (ComboBoxVar)cbDates.SelectedItem;
                g.varDateGuid = cbv.varGuid;
            }
            
            this.Tag = g;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        public void addCounterVar(string varName, string varGuid, decimal max)
        {
            ComboBoxVar cbv = new ComboBoxVar(varName, varGuid, max);
            cbVars.Items.Add(cbv);
        }

        public void addExpireVar(string varName, string varGuid, DateTime date)
        {
            ComboBoxVar cbv = new ComboBoxVar(varName, varGuid, date);
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

            g = (Guard)this.Tag;
            if (g != null)
            {
                if (g.varCounterGuid != null)
                {
                    cbDecrease.Checked = true;
                    cbDecrease.Enabled = true;
                    cbVars.Enabled = true;                                        
                    nudCounter.Enabled = true;
                    nudCounter.Value = g.decreaseQty;                    

                    cbVars.SelectedIndex = 0;
                    for (int idx = 0;idx < cbVars.Items.Count;idx++)
                    {
                        ComboBoxVar cbv = (ComboBoxVar)cbVars.Items[idx];
                        if (cbv.varGuid == g.varCounterGuid)
                        {
                            cbVars.SelectedIndex = idx;
                            decimal maxVal = (decimal)((ComboBoxVar)cbVars.SelectedItem).varData;
                            nudCounter.Maximum = maxVal;
                            break;
                        }
                    }
                }
                else
                {
                    cbDecrease.Checked = false;
                    cbVars.Enabled = false;
                    nudCounter.Enabled = false;
                }

                if (g.varDateGuid != null)
                {
                    cbExpire.Checked = true;
                    cbExpire.Enabled = true;                    
                    cbDates.SelectedIndex = 0;
                    for (int idx = 0; idx < cbDates.Items.Count; idx++)
                    {
                        ComboBoxVar cbv = (ComboBoxVar)cbDates.Items[idx];
                        if (cbv.varGuid == g.varDateGuid)
                        {
                            cbDates.SelectedIndex = idx;
                            break;
                        }
                    }
                    cbDates.Enabled = true;
                }
                else
                {
                    cbExpire.Checked = false;
                    cbDates.Enabled = false;
                }

                return;
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

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
