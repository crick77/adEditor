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
using System.Security.Cryptography;

namespace adEditor
{    
    public partial class MainForm : Form
    {
        private TreeNode varNode = null;
        private TreeNode eventNode = null;
        private TreeNode metadataNode = null;
        private TreeNode dataNode = null;
        private bool dirty;
        private SHA1Managed sha1 = new SHA1Managed();

        public MainForm()
        {
            InitializeComponent();

            clearDirty();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = checkEmpty();
                if (dr == DialogResult.Cancel) return;
                if (dr == DialogResult.Yes) saveToolStripMenuItem_Click(sender, new EventArgs());
            }

            treeViewItem.Nodes.Clear();

            TreeNode root = new TreeNode("ActiveData");
            root.Tag = new TagElement("-");
            TreeNode metaData = new TreeNode("Meta-Data");
            metaData.ImageIndex = 0;
            metaData.Tag = new TagElement("-");
            metadataNode = metaData;

            TreeNode n = new TreeNode("Version: 1.0");
            n.ImageIndex = n.SelectedImageIndex = 3;
            n.Tag = new TagElement("S", "Version", 0, "1.3");
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

            clearDirty();
            Text = "adEditor - New";
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
            if (!te.editable) return;
            switch (te.type)
            {
                case "S":
                    {
                        StringEditForm sef = new StringEditForm(te.maxLen);
                        sef.Tag = te;
                        sef.ShowDialog();
                        if (sef.DialogResult == DialogResult.OK)
                        {                            
                            te = (TagElement)sef.Tag;
                            selected.Text = te.name + ": " + (string)te.data;
                            setDirty();
                        }
                        sef.Dispose();
                        break;
                    }

                case "T":
                    {
                        DataTextForm dtf = new DataTextForm();
                        dtf.Tag = te;
                        dtf.ShowDialog();
                        if (dtf.DialogResult == DialogResult.OK)
                        {
                            te = (TagElement)dtf.Tag;
                            string txt = Encoding.UTF8.GetString((byte[])te.data);
                            selected.Text = te.name + ": " + ellipsis(txt)+" ("+txt.Length+" characters).";
                            setDirty();
                        }
                        dtf.Dispose();
                        break;
                    }
                case "I":
                    {
                        DataImageForm di = new DataImageForm();
                        di.Tag = te;                        
                        di.ShowDialog();
                        if(di.DialogResult == DialogResult.OK)
                        {
                            te = (TagElement)di.Tag;
                            byte[] d = (byte[])te.data;
                            selected.Text = te.name + ": " + te.extension+" ("+d.Length+" bytes).";
                            setDirty();
                        }
                        di.Dispose();
                        break;
                    }
                case "V":
                    {
                        DataVideoForm dvf = new DataVideoForm();
                        dvf.Tag = te;                        
                        dvf.ShowDialog();
                        if (dvf.DialogResult == DialogResult.OK)
                        {
                            te = (TagElement)dvf.Tag;
                            byte[] d = (byte[])te.data;
                            selected.Text = te.name + ": " + te.extension + " (" + d.Length + " bytes).";
                            setDirty();
                        }
                        dvf.Dispose();
                        break;
                    }
                case "P":
                    {
                        DataPDFForm dpf = new DataPDFForm();
                        dpf.Tag = te;                        
                        dpf.ShowDialog();
                        if (dpf.DialogResult == DialogResult.OK)
                        {
                            te = (TagElement)dpf.Tag;
                            byte[] d = (byte[])te.data;
                            selected.Text = te.name + ": " + te.extension + " (" + d.Length + " bytes).";
                            setDirty();
                        }
                        dpf.Dispose();
                        break;
                    }
                case "VC": // Value-Counter
                    {
                        CounterForm cf = new CounterForm();
                        cf.Tag = te;                        
                        cf.ShowDialog();
                        if (cf.DialogResult == DialogResult.OK)
                        {
                            te = (TagElement)cf.Tag;
                            selected.Text = te.name + "= " + te.data;
                            setDirty();                            
                        }
                        cf.Dispose();
                        break;
                    }
                case "VD": // Value-Date
                    {
                        DateForm df = new DateForm();
                        df.Tag = te;                        
                        df.ShowDialog();
                        if (df.DialogResult == DialogResult.OK)
                        {
                            te = (TagElement)df.Tag;
                            selected.Text = te.name + "= " + te.data;
                            setDirty();
                        }
                        df.Dispose();
                        break;
                    }
                case "OR": // onread
                    {
                        GuardsForm gf = populateGuardsForm();
                        gf.Text = "onRead event";
                        gf.Tag = te.data;                        
                        gf.ShowDialog();
                        if(gf.DialogResult == DialogResult.OK)
                        {
                            te.data = gf.Tag;
                            computerVarRef();
                            setDirty();
                        }
                        gf.Dispose();
                        break;
                    }
                case "OC": // oncopy
                    {
                        GuardsForm gf = populateGuardsForm();
                        gf.Text = "onCopy event";
                        gf.Tag = te.data;                        
                        gf.ShowDialog();
                        if (gf.DialogResult == DialogResult.OK)
                        {
                            te.data = gf.Tag;
                            computerVarRef();
                            setDirty();
                        }
                        gf.Dispose();
                        break;
                    }
                case "OS": // onshare
                    {
                        GuardsForm gf = populateGuardsForm();
                        gf.Text = "onShare event";
                        gf.Tag = te.data;                       
                        gf.ShowDialog();
                        if (gf.DialogResult == DialogResult.OK)
                        {
                            te.data = gf.Tag;
                            computerVarRef();
                            setDirty();                            
                        }
                        gf.Dispose();
                        break;
                    }
            } // switch
        }

        private string ellipsis(string s)
        {
            return (s != null && s.Length < 16) ? s : s.Substring(0, 16) + "...";
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(dataNode, "T", 32, "text", 2, true);
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(dataNode, "I", 32, "image", 4);
        }

        private TreeNode addDataNode(TreeNode selected, string type, int maxlen, string descr, int iconIndex, bool canShow = false, string sep = ":", object data = null)
        {
            NameForm nf = new NameForm(maxlen);
            nf.Text = "Add "+descr+" variable";
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
            addDataNode(dataNode, "V", 32, "video", 5);
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDataNode(dataNode, "P", 32, "PDF", 6);
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
                eventNode.Nodes.Add(tn);
                eventNode.ExpandAll();

                computerVarRef();               
            }
            gf.Dispose();
        }

        private void addCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if(countVars("VC") == 6)
            {
                MessageBox.Show("You cannot add more than 6 counter vars.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RefData rd = new RefData((decimal)1);
            addDataNode(varNode, "VC", 22, "Counter", 8, true, "=", rd);
        }

        private int countVars(string type)
        {
            int count = 0;
            foreach (TreeNode n in varNode.Nodes)
            {
                TagElement te = (TagElement)n.Tag;
                if (te.type == type) count++;
            }

            return count;
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
            if (countVars("VD") == 6)
            {
                MessageBox.Show("You cannot add more than 6 date/time vars.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RefData rd = new RefData(DateTime.Now);
            addDataNode(varNode, "VD", 22, "Date/Time", 8, true, "=", rd);
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
                eventNode.Nodes.Add(tn);
                eventNode.ExpandAll();

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
                eventNode.Nodes.Add(tn);
                eventNode.ExpandAll();

                computerVarRef();
            }
            gf.Dispose();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagElement te = (TagElement)treeViewItem.SelectedNode.Tag;
            if(!te.removable) return;            

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
            if(te!=null && te.removable)
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
            if(dataNode!=null && dataNode.Nodes.Count<1)
            {
                if (MessageBox.Show("You're going to save an empty ActiveData file. Is that you want?", "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes) return;
            }

            if(saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                int writtenSize = 0;
                Stream s = new FileStream(saveFileDialog.FileName, FileMode.Create);

                ActiveDataFile adf = new ActiveDataFile();
                adf.magic = Encoding.UTF8.GetBytes("*AD*");
                TagElement te = (TagElement)metadataNode.Nodes[0].Tag;
                adf.version = versionToByte((string)te.data);
                te = (TagElement)metadataNode.Nodes[1].Tag;
                adf.createTime = ((DateTime)te.data).Ticks;
                te = (TagElement)metadataNode.Nodes[2].Tag;
                adf.owner = padStringToByteArray((string)te.data, 64);

                adf.metaDataHash = padStringToByteArray("12345678901234567890");
                adf.dataHash = padStringToByteArray("12345678901234567890");

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
                        adf.counter[idxC].varName = padStringToByteArray(te.name, 22);
                        adf.counter[idxC].varGuid = padStringToByteArray(rd.guid);
                        adf.counter[idxC].refCount = Convert.ToByte(rd.refCount);
                        adf.counter[idxC].value = Convert.ToInt16((decimal)rd.data);
                        idxC++;
                    }

                    if (te.type == "VD")
                    {
                        adf.datetime[idxD] = new DateVar();
                        adf.datetime[idxD].varName = padStringToByteArray(te.name, 22);
                        adf.datetime[idxD].varGuid = padStringToByteArray(rd.guid);
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
                    adf.events[idx].eventName = padStringToByteArray(te.type);
                    if(g.decreaseQty==0) // dateguard
                    {
                        adf.events[idx].decreaseQty = 0;
                        adf.events[idx].varGuid = padStringToByteArray(g.varDateGuid);
                    }
                    else
                    {
                        adf.events[idx].decreaseQty = Convert.ToInt16(g.decreaseQty);
                        adf.events[idx].varGuid = padStringToByteArray(g.varCounterGuid);
                    }
                    
                    idx++;
                }

                adf.magic2 = Encoding.UTF8.GetBytes("DF");
                adf.dataCount = Convert.ToInt16(dataNode.Nodes.Count); 
                // compute hash of metadata and reinsert it
                adf.metaDataHash = sha1.ComputeHash(ActiveDataFileToBytes(adf));
                // compute for length
                byte[] headerBuff = ActiveDataFileToBytes(adf);

                // pad fill                
                int headerSizePad = 2048 - headerBuff.Length;
                byte[] padBuffer = new byte[headerSizePad].Initialize(0);

                // compute data fields size
                byte[][] dataBuffer = new byte[dataNode.Nodes.Count*2][];
                int i = 0;
                foreach(TreeNode n in dataNode.Nodes)
                {
                    te = (TagElement)n.Tag;

                    byte[] data = (byte[])te.data;
                    ActiveDataFields adfld = new ActiveDataFields();
                    adfld.fieldName = padStringToByteArray(te.name, 32);
                    adfld.extension = padStringToByteArray(te.extension, 32);
                    adfld.fieldSize = (uint)data.Length;

                    // compute size of buffer
                    dataBuffer[i] = ActiveDataFieldToBytes(adfld);
                    dataBuffer[i + 1] = data;
                    i+=2;
                }

                // combine array
                byte[] dataCombined = new byte[dataBuffer.Sum(a => a.Length)];
                int offset = 0;
                foreach (byte[] array in dataBuffer)
                {
                    System.Buffer.BlockCopy(array, 0, dataCombined, offset, array.Length);
                    offset += array.Length;
                }

                // update header data hash
                adf.dataHash = sha1.ComputeHash(dataCombined);
                // regenerate new buffer
                headerBuff = ActiveDataFileToBytes(adf);

                // Write down the sequence of buffers to file
                s.Write(headerBuff, 0, headerBuff.Length);
                writtenSize += headerBuff.Length;
                s.Write(padBuffer, 0, padBuffer.Length);
                writtenSize += padBuffer.Length;
                s.Write(dataCombined, 0, dataCombined.Length);
                writtenSize += dataCombined.Length;

                s.Close();
                dirty = false;

                MessageBox.Show("File saved! "+ writtenSize + " bytes written.");
                Text = "adEditor - " + saveFileDialog.FileName;
            }            
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

        private byte[] ActiveDataFieldToBytes(ActiveDataFields adf)
        {
            int length = Marshal.SizeOf(adf);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(adf, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }

        private byte[] padStringToByteArray(string s, int pad)
        {
            return Encoding.UTF8.GetBytes(s.PadRight(pad, (char)0).Substring(0, pad));
        }
        private byte[] padStringToByteArray(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        private byte versionToByte(string v)
        {
            string[] parts = v.Split('.');
            byte ve = (byte)(Convert.ToByte(parts[0]) << 4);
            byte sv = (byte)(Convert.ToByte(parts[1]) & 0x0F);
            return (byte)(ve + sv); 
        }

        private string byteToVersion(byte v)
        {
            string ve = ""+(v & 0xF0);
            string sv = "" + (v & 0x0F);
            return ve + "." + sv;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = checkEmpty();
                if (dr == DialogResult.Yes) saveToolStripMenuItem_Click(sender, new EventArgs());
                e.Cancel = dr == DialogResult.Cancel;
            }            
        }

        private DialogResult checkEmpty()
        {
            return MessageBox.Show("Unsaved changed were detected. Do you want to save it?", "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);                
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
            af.Dispose();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                MessageBox.Show("Read all file, bytes: " + bytes.Length);
                Text = "adEditor - " + openFileDialog.FileName;
            }
        }

        private void setDirty()
        {
            dirty = true;
            Text = Text + "*";
        }
        private void clearDirty()
        {
            dirty = false;
            if (Text.EndsWith("*")) Text = Text.Substring(0, Text.Length - 1);
        }
    }
}
