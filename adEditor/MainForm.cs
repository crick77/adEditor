using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace adEditor
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CounterVar
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 22)]
        public string varName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string varGuid;

        [MarshalAs(UnmanagedType.U1)]
        public byte refCount;

        [MarshalAs(UnmanagedType.U2)]
        public short value;

        /*public string varName
        {
            get { return new string(_varName); }
            set
            {
                if (value.Length == 22)
                {
                    _varName = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }

        public string varGuid
        {
            get { return new string(_varGuid); }
            set
            {
                if (value.Length == 36)
                {
                    _varGuid = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }*/
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DateVar
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 22)]
        public string varName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string varGuid;

        [MarshalAs(UnmanagedType.U1)]
        public byte refCount;

        [MarshalAs(UnmanagedType.U8)]
        public long value;

        /*public string varName
        {
            get { return new string(_varName); }
            set
            {
                if (value.Length == 22)
                {
                    _varName = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }

        public string varGuid
        {
            get { return new string(_varGuid); }
            set
            {
                if (value.Length == 36)
                {
                    _varGuid = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }*/
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Event
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public char[] eventName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string varGuid;

        [MarshalAs(UnmanagedType.U2)]
        public short decreaseQty;

        /*public string eventName
        {
            get { return new string(_eventName); }
            set
            {
                if (value.Length == 2)
                {
                    _eventName = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }

        public string varGuid
        {
            get { return new string(_varGuid); }
            set
            {
                if (value.Length == 36)
                {
                    _varGuid = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }*/
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ActiveDataFile
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] magic;

        [MarshalAs(UnmanagedType.U1)]
        public byte version;

        [MarshalAs(UnmanagedType.U8)]
        public long createTime;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string owner;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string metaDataHash;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string dataHash;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public CounterVar[] counter;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public DateVar[] datetime;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public Event[] events;

        /*public string owner
        {
            get { return new string(_owner); }
            set
            {
                if (value.Length == 64)
                {
                    _owner = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }

        public string metaDataHash
        {
            get { return new string(_metaDataHash); }
            set
            {
                if (value.Length == 20)
                {
                    _metaDataHash = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }

        public string dataHash
        {
            get { return new string(_dataHash); }
            set
            {
                if (value.Length == 20)
                {
                    _dataHash = value.ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Wrong string length.");
                }
            }
        }*/
    }

    public partial class MainForm : Form
    {
        private TreeNode varNode = null;
        private TreeNode eventNode = null;
        private TreeNode metadataNode = null;
        private TreeNode dataNode = null;
        
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
            metadataNode = metaData;

            TreeNode n = new TreeNode("Version: 1.0");
            n.ImageIndex = n.SelectedImageIndex = 3;
            n.Tag = new TagElement("S", "Version", 0, "1.0");
            metaData.Nodes.Add(n);
            DateTime now = DateTime.Now;
            string c = now.ToString("dd-MM-yyyy HH:mm:ss");
            n = new TreeNode("Created: "+c);
            n.Tag = new TagElement("D", "Created", 0, now);
            metaData.Nodes.Add(n);

            c = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            n = new TreeNode("Owner: " + c);
            n.Tag = new TagElement("S", "Owner", 64, c, true);
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
            dataNode = data;

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
            bool removeEn = false;
            bool dataEn = false;
            bool varEn = false;
            bool eventEn = false;

            if (treeViewItem.SelectedNode != null)
            {
                TreeNode selected = treeViewItem.SelectedNode;
                TagElement te = (TagElement)selected.Tag;

                editEn = te.editable;
                removeEn = te.removable;
                dataEn = te.type == "+D";
                varEn = te.type == "+V";
                eventEn = te.type == "+E";
            }
                        
            editToolStripMenuItem.Enabled = editEn;

            dataToolStripMenuItem.Enabled = dataEn;
            varToolStripMenuItem.Enabled = varEn;
            eventsToolStripMenuItem.Enabled = eventEn;
            addToolStripMenuItem.Enabled = (dataEn || varEn || eventEn);

            removeToolStripMenuItem.Enabled = removeEn;
            renameToolStripMenuItem.Enabled = removeEn;
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
                case "VD": // Value-Date
                    {
                        DateForm df = new DateForm();
                        if (te != null)
                        {
                            df.Tag = te;
                        }
                        df.ShowDialog();
                        if (df.DialogResult==DialogResult.OK && df.Tag != null)
                        {
                            te = (TagElement)df.Tag;
                            selected.Text = te.name + "= " + te.data;
                        }
                        df.Dispose();
                        break;
                    }
                case "OR": // onread
                    {
                        GuardsForm gf = populateGuardsForm();
                        gf.Text = "onRead event";
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
                case "OC": // oncopy
                    {
                        GuardsForm gf = populateGuardsForm();
                        gf.Text = "onCopy event";
                        if (te != null)
                        {
                            gf.Tag = te.data;
                        }
                        gf.ShowDialog();
                        if (gf.DialogResult == DialogResult.OK)
                        {
                            te.data = gf.Tag;

                            computerVarRef();
                        }
                        gf.Dispose();
                        break;
                    }
                case "OS": // onshare
                    {
                        GuardsForm gf = populateGuardsForm();
                        gf.Text = "onShare event";
                        if (te != null)
                        {
                            gf.Tag = te.data;
                        }
                        gf.ShowDialog();
                        if (gf.DialogResult == DialogResult.OK)
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
            addDataNode(treeViewItem.SelectedNode, "S", 32, "text", 2, true);
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(treeViewItem.SelectedNode, "I", 32, "image", 4);
        }

        private TreeNode addDataNode(TreeNode selected, string type, int maxlen, string descr, int iconIndex, bool canShow = false, string sep = ":", object data = null)
        {
            NameForm nf = new NameForm(maxlen);
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
            TagElement te = new TagElement(type, fName, maxlen, data, true, true);
            tn.Text = fName + (canShow ? sep+" ": "") + (data!=null ? data : "");
            tn.Tag = te;
            tn.ImageIndex = tn.SelectedImageIndex = iconIndex;
            selected.Nodes.Add(tn);
            selected.ExpandAll();

            return tn;
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(treeViewItem.SelectedNode, "V", 32, "video", 5);
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(treeViewItem.SelectedNode, "P", 32, "PDF", 6);
        }

        private void onreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode n in eventNode.Nodes)
            {
                if (n.Text == "onRead")
                {
                    treeViewItem.SelectedNode = n;
                    editToolStripMenuItem_Click(null, null);
                    return;
                }
            }

            GuardsForm gf = populateGuardsForm();
            gf.Text = "onRead event";
            gf.ShowDialog();
            if(gf.DialogResult == DialogResult.OK && gf.Tag!=null)
            {
                Guard g = (Guard)gf.Tag;
                
                TreeNode tn = new TreeNode("onRead");
                tn.Tag = new TagElement("OR", "onRead", 0, g, true, true);
                treeViewItem.SelectedNode.Nodes.Add(tn);
                treeViewItem.SelectedNode.ExpandAll();

                computerVarRef();               
            }
            gf.Dispose();
        }

        private void addCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefData rd = new RefData((decimal)1);
            addDataNode(treeViewItem.SelectedNode, "VC", 22, "Counter", 8, true, "=", rd);
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
                    gf.addCounterVar(_te.name, rd.guid, (decimal)rd.data);
                }
                if(_te.type == "VD")
                {
                    RefData rd = (RefData)_te.data;
                    gf.addExpireVar(_te.name, rd.guid, (DateTime)rd.data);
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

        private void addDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefData rd = new RefData(DateTime.Now);
            addDataNode(treeViewItem.SelectedNode, "VD", 22, "Date/Time", 8, true, "=", rd);
        }

        private void onCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode n in eventNode.Nodes)
            {
                if (n.Text == "onCopy")
                {
                    treeViewItem.SelectedNode = n;
                    editToolStripMenuItem_Click(null, null);
                    return;
                }
            }

            GuardsForm gf = populateGuardsForm();
            gf.Text = "onCopy event";
            gf.ShowDialog();
            if (gf.DialogResult == DialogResult.OK && gf.Tag != null)
            {
                Guard g = (Guard)gf.Tag;

                TreeNode tn = new TreeNode("onCopy");
                tn.Tag = new TagElement("OC", "onCopy", 0, g, true, true);
                treeViewItem.SelectedNode.Nodes.Add(tn);
                treeViewItem.SelectedNode.ExpandAll();

                computerVarRef();
            }
            gf.Dispose();
        }

        private void onShareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode n in eventNode.Nodes)
            {
                if (n.Text == "onShare")
                {
                    treeViewItem.SelectedNode = n;
                    editToolStripMenuItem_Click(null, null);
                    return;
                }
            }

            GuardsForm gf = populateGuardsForm();
            gf.Text = "onShare event";
            gf.ShowDialog();
            if (gf.DialogResult == DialogResult.OK && gf.Tag != null)
            {
                Guard g = (Guard)gf.Tag;

                TreeNode tn = new TreeNode("onShare");
                tn.Tag = new TagElement("OS", "onShare", 0, g, true, true);
                treeViewItem.SelectedNode.Nodes.Add(tn);
                treeViewItem.SelectedNode.ExpandAll();

                computerVarRef();
            }
            gf.Dispose();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagElement te = (TagElement)treeViewItem.SelectedNode.Tag;
            if(!te.removable)
            {
                MessageBox.Show("Selectected item cannot be removed.", "Attention");
                return;
            }

            if(te.data is RefData)
            {
                RefData rd = (RefData)te.data;
                if(rd.refCount>0)
                {
                    MessageBox.Show("Item is referenced "+rd.refCount+" times. Please remove reference and retry.", "Attention");
                    return;
                }
            }

            if (MessageBox.Show("Sure to remove?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                treeViewItem.Nodes.Remove(treeViewItem.SelectedNode);
                computerVarRef();
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagElement te = (TagElement)treeViewItem.SelectedNode.Tag;
            if(te!=null)
            {
                NameForm nf = new NameForm(te.maxLen);
                nf.Tag = te.name;
                nf.ShowDialog();
                
                if(nf.DialogResult == DialogResult.OK && nf.Tag!=null)
                {
                    bool done = true;
                    TreeNode parent = treeViewItem.SelectedNode.Parent;
                    string fName = (string)nf.Tag;
                    foreach (TreeNode n in parent.Nodes)
                    {
                        if (n != treeViewItem.SelectedNode)
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

                    if(done)
                    {
                        te.name = fName;
                        treeViewItem.SelectedNode.Text = fName + (te.data != null ? ": "+te.data : "");
                    }
                }
            }
        }

        private void treeViewItem_DoubleClick(object sender, EventArgs e)
        {
            TagElement te = (TagElement)treeViewItem.SelectedNode.Tag;
            if(te.editable)
            {
                editToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataNode.Nodes.Count<1)
            {
                if (MessageBox.Show("You're going to save an empty ActiveData file. Is that you want?", "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes) return;
            }

            if(saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                Stream s = new FileStream(saveFileDialog.FileName, FileMode.Create);

                ActiveDataFile adf = new ActiveDataFile();
                adf.magic = Encoding.UTF8.GetBytes("*AD*");
                adf.version = ((1 << 4) + (0 & 0x0F));
                TagElement te = (TagElement)metadataNode.Nodes[1].Tag;
                adf.createTime = ((DateTime)te.data).Ticks;
                te = (TagElement)metadataNode.Nodes[2].Tag;
                adf.owner = padString((string)te.data, 64);

                adf.metaDataHash = "12345678901234567890";
                adf.dataHash = "12345678901234567890";

                adf.counter = new CounterVar[6];
                adf.datetime = new DateVar[6];
                int idxC = 0, idxD = 0;
                foreach(TreeNode n in varNode.Nodes)
                {
                    te = (TagElement)n.Tag;
                    RefData rd = (RefData)te.data;
                    if (te.type == "VC")
                    {
                        adf.counter[idxC] = new CounterVar();
                        adf.counter[idxC].varName = padString(te.name, 22);
                        adf.counter[idxC].varGuid = rd.guid;
                        adf.counter[idxC].refCount = Convert.ToByte(rd.refCount);
                        adf.counter[idxC].value = Convert.ToInt16((decimal)rd.data);
                        idxC++;
                    }

                    if (te.type == "VD")
                    {
                        adf.datetime[idxD] = new DateVar();
                        adf.datetime[idxD].varName = padString(te.name, 22);
                        adf.datetime[idxD].varGuid = rd.guid;
                        adf.datetime[idxD].refCount = Convert.ToByte(rd.refCount);
                        adf.datetime[idxD].value = ((DateTime)rd.data).Ticks;
                        idxD++;
                    }
                }

                adf.events = new Event[3];
                int idx = 0;
                foreach (TreeNode n in eventNode.Nodes)
                {
                    te = (TagElement)n.Tag;
                    Guard g = (Guard)te.data;

                    adf.events[idx] = new Event();
                    adf.events[idx].eventName = te.type.ToCharArray();
                    if(g.decreaseQty==0) // dateguard
                    {
                        adf.events[idx].decreaseQty = 0;
                        adf.events[idx].varGuid = g.varDateGuid;
                    }
                    else
                    {
                        adf.events[idx].decreaseQty = Convert.ToInt16(g.decreaseQty);
                        adf.events[idx].varGuid = g.varCounterGuid;
                    }
                    
                    idx++;
                }

                byte[] newBuffer = ActiveDataFileToBytes(adf);
                s.Write(newBuffer, 0, newBuffer.Length);                               
                                
                // pad fill                
                s.Close();

                MessageBox.Show("File saved! "+ newBuffer.Length + " bytes written.");
            }
        }

        private int writeInt(Stream s, int value)
        {
            byte[] b = BitConverter.GetBytes(value);
            s.Write(b, 0, b.Length);
            return b.Length;
        }

        private int writeLong(Stream s, long value)
        {
            byte[] b = BitConverter.GetBytes(value);
            s.Write(b, 0, b.Length);
            return b.Length;
        }

        private int writeByte(Stream s, byte b)
        {
            s.WriteByte(b);
            return sizeof(byte);
        }

        private int writeString(Stream s, string t)
        {
            byte[] b = Encoding.UTF8.GetBytes(t);
            s.Write(b, 0, b.Length);
            return b.Length;
        }

        private int writePad(Stream s, byte b, int len)
        {
            byte[] bb = new byte[len];
            for (int i = 0; i < len; i++) bb[i] = b;
            s.Write(bb, 0, len);
            return len;
        }

        private byte[] ActiveDataFileToBytes(ActiveDataFile adf)
        {
            int length = Marshal.SizeOf(adf);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(adf, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }

        private string padString(string s, int pad)
        {
            return s.PadRight(pad, (char)0).Substring(0, pad);            
        }
    }
}
