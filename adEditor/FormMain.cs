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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void mainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewItem.Nodes.Clear();

            TreeNode root = new TreeNode("ActiveData");
            root.Tag = new TagElement("-");
            TreeNode metaData = new TreeNode("Meta-Data");
            metaData.Tag = new TagElement("-");

            TreeNode n = new TreeNode("Version: 1.0");
            n.Tag = new TagElement("S", "Version", "1.0");
            metaData.Nodes.Add(n);
            string c = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            n = new TreeNode("Created: "+c);
            n.Tag = new TagElement("D", "Created", c);
            metaData.Nodes.Add(n);

            c = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            n = new TreeNode("Owner: " + c);
            n.Tag = new TagElement("S", "Owner", c, true);
            metaData.Nodes.Add(n);

            TreeNode data = new TreeNode("Data");
            data.Tag = new TagElement("-");
            root.Nodes.Add(metaData);
            root.Nodes.Add(data);

            treeViewItem.Nodes.Add(root);
            treeViewItem.ExpandAll();

        }

        private void treeViewItem_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {            
        }

        private void menuItem_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            MessageBox.Show(e.ClickedItem.Text);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            bool en = (treeViewItem.SelectedNode != null);
            if (en) {
                TreeNode selected = treeViewItem.SelectedNode;
                TagElement te = (TagElement)selected.Tag;
                en = te.editable;
            }            

            editToolStripMenuItem.Enabled = en;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selected = treeViewItem.SelectedNode;
            TagElement te = (TagElement)selected.Tag;
            if (te.type=="S")
            {
                StringEditForm sef = new StringEditForm();
                sef.Tag = te;
                sef.ShowDialog();
                selected.Tag = sef.Tag;
                te = (TagElement)sef.Tag;
                selected.Text = (string)te.data;
            }
            
        }
    }
}
