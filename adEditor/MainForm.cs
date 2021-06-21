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
    public partial class MainForm : Form
    {
        private TreeNode varNode = null;
        private TreeNode eventNode = null;
        public MainForm()
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
            metaData.ImageIndex = 0;
            metaData.Tag = new TagElement("-");

            TreeNode n = new TreeNode("Version: 1.0");
            n.ImageIndex = n.SelectedImageIndex = 3;
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

            n = new TreeNode("Guards");
            n.Tag = new TagElement(false);
            n.ImageIndex = n.SelectedImageIndex = 7;
            TreeNode g = n;
            metaData.Nodes.Add(n);            

            n = new TreeNode("Variables");
            n.Tag = new TagElement("+V");
            n.ImageIndex = n.SelectedImageIndex = 8;
            g.Nodes.Add(n);
            varNode = n;

            n = new TreeNode("Events");
            n.Tag = new TagElement("+E");
            n.ImageIndex = n.SelectedImageIndex = 9;
            g.Nodes.Add(n);
            eventNode = n;

            TreeNode data = new TreeNode("Data");
            data.Tag = new TagElement("+D");
            data.ImageIndex = data.SelectedImageIndex = 1;
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
            bool editEn = false;
            bool addEn = false;
            bool varEn = false;
            bool eventEn = false;

            if (treeViewItem.SelectedNode != null)
            {
                TreeNode selected = treeViewItem.SelectedNode;
                TagElement te = (TagElement)selected.Tag;

                editEn = te.editable;
                addEn = te.type == "+D";
                varEn = te.type == "+V";
                eventEn = te.type == "+E";
            }
                        
            editToolStripMenuItem.Enabled = editEn;
            addToolStripMenuItem.Enabled = addEn;
            varToolStripMenuItem.Enabled = varEn;
            eventsToolStripMenuItem.Enabled = eventEn;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selected = treeViewItem.SelectedNode;
            TagElement te = (TagElement)selected.Tag;
            switch (te.type)
            {
                case "S":
                    {
                        StringEditForm sef = new StringEditForm();
                        sef.Tag = te;
                        sef.ShowDialog();
                        selected.Tag = sef.Tag;
                        te = (TagElement)sef.Tag;
                        selected.Text = te.name + ": " + (string)te.data;
                        sef.Dispose();
                        break;
                    }
                case "I":
                    {
                        DataImageForm di = new DataImageForm();
                        if(te!=null)
                        {
                            di.Tag = te.data;
                        }
                        di.ShowDialog();
                        if(di.Tag!=null)
                        {
                            te.data = di.Tag;
                        }
                        di.Dispose();
                        break;
                    }
                case "V":
                    {
                        DataVideoForm dvf = new DataVideoForm();
                        if (te != null)
                        {
                            dvf.Tag = te;
                        }
                        dvf.ShowDialog();
                        if (dvf.Tag != null)
                        {
                            te = (TagElement)dvf.Tag;
                        }
                        dvf.Dispose();
                        break;
                    }
                case "P":
                    {
                        DataPDFForm dpf = new DataPDFForm();
                        if (te != null)
                        {
                            dpf.Tag = te;
                        }
                        dpf.ShowDialog();
                        if (dpf.Tag != null)
                        {
                            te = (TagElement)dpf.Tag;
                        }
                        dpf.Dispose();
                        break;
                    }
                case "VC": // Value-Counter
                    {
                        CounterForm cf = new CounterForm();
                        if (te != null)
                        {
                            cf.Tag = te;
                        }
                        cf.ShowDialog();
                        if (cf.Tag != null)
                        {
                            te = (TagElement)cf.Tag;
                            selected.Text = te.name + "= " + te.data;
                        }
                        cf.Dispose();
                        break;
                    }
                case "OR": // onread
                    {
                        GuardsForm gf = populateGuardsForm();
                        if (te != null)
                        {
                            gf.Tag = te.data;
                        }
                        gf.ShowDialog();
                        if(gf.DialogResult == DialogResult.OK)
                        {
                            te.data = gf.Tag;

                            computerVarRef();
                        }
                        gf.Dispose();
                        break;
                    }
            } // switch
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(treeViewItem.SelectedNode, "S", "text", 2, true);
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(treeViewItem.SelectedNode, "I", "image", 4);
        }

        private TreeNode addDataNode(TreeNode selected, string type, string descr, int iconIndex, bool canShow = false, string sep = ":", object data = null)
        {
            NameForm nf = new NameForm();
            nf.Text = "Add "+descr+" field";
            bool done = false;
            string fName = null;
            while (!done)
            {
                nf.ShowDialog();
                if (nf.Tag == null)
                {
                    nf.Dispose();
                    return null;
                }
                fName = (string)nf.Tag;
                done = true;
                foreach (TreeNode n in selected.Nodes)
                {
                    TagElement _te = (TagElement)n.Tag;
                    if (_te.name.ToUpper() == fName.ToUpper())
                    {
                        MessageBox.Show("A field with supplied name exists yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        done = false;
                        break;
                    }
                }
            }

            nf.Dispose();
            TreeNode tn = new TreeNode();
            TagElement te = new TagElement(type, fName, data, true);
            tn.Text = fName + (canShow ? sep+" ": "") + (data!=null ? data : "");
            tn.Tag = te;
            tn.ImageIndex = tn.SelectedImageIndex = iconIndex;
            selected.Nodes.Add(tn);
            selected.ExpandAll();

            return tn;
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(treeViewItem.SelectedNode, "V", "video", 5);
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(treeViewItem.SelectedNode, "P", "PDF", 6);
        }

        private void onreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardsForm gf = populateGuardsForm();

            gf.ShowDialog();
            if(gf.DialogResult == DialogResult.OK && gf.Tag!=null)
            {
                Guard g = (Guard)gf.Tag;
                if (treeViewItem.SelectedNode.Nodes.Count==0)
                {
                    TreeNode tn = new TreeNode("onRead");
                    tn.Tag = new TagElement("OR", "onRead", g, true);
                    treeViewItem.SelectedNode.Nodes.Add(tn);
                    treeViewItem.SelectedNode.ExpandAll();

                    computerVarRef();
                }

            }
            gf.Dispose();
        }

        private void guardsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefData rd = new RefData((decimal)1);
            TreeNode n = addDataNode(treeViewItem.SelectedNode, "VC", "Counter", 8, true, "=", rd);
        }

        private GuardsForm populateGuardsForm()
        {
            GuardsForm gf = new GuardsForm();

            foreach (TreeNode n in varNode.Nodes)
            {
                TagElement _te = (TagElement)n.Tag;
                if (_te.type == "VC")
                {
                    RefData rd = (RefData)_te.data;
                    gf.addVar(_te.name, rd.guid, (decimal)rd.data);
                }
            }

            return gf;
        }

        private void computerVarRef()
        {
            foreach(TreeNode v in varNode.Nodes)
            {               
                int refCount = 0;
                
                TagElement tev = (TagElement)v.Tag;
                RefData rd = (RefData)tev.data;
                foreach (TreeNode g in eventNode.Nodes)
                {
                    TagElement teg = (TagElement)g.Tag;
                    Guard guard = (Guard)teg.data;
                    if (guard.varDateGuid == rd.guid || guard.varCounterGuid == rd.guid) refCount++;                    
                }

                rd.refCount = refCount;
                v.Text = tev.name + "= " + tev.data;
            }
        }
    }
}
