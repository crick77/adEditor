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
using System.IO.Pipes;
using System.Threading;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace adEditor
{    
    public partial class MainForm : Form
    {
        private TreeNode dataNode = null;
        private TreeNode infoNode = null;
        private TreeNode openCountNode = null;
        private TreeNode dataFieldCountNode = null;
        private bool dirty;
        private static string privateKey;
        private SHA1Managed sha1 = new SHA1Managed();
        private readonly byte[] iv = new byte[16] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        public MainForm()
        {
            InitializeComponent();

            clearDirty();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
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
            
            TreeNode info = new TreeNode("Information");
            info.ImageIndex = 0;
            info.Tag = new TagElement("-");

            TreeNode n = new TreeNode("Version: 1.0");
            n.ImageIndex = n.SelectedImageIndex = 3;
            n.Tag = new TagElement("S", "Version", 0, versionToByte("1.0"));
            info.Nodes.Add(n);

            DateTime now = DateTime.Now;
            n = new TreeNode("Created: "+now.ToString("dd-MM-yyyy HH:mm:ss"));
            n.Tag = new TagElement("D", "Created", 0, now);
            info.Nodes.Add(n);

            string s = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            n = new TreeNode("Owner: " + s);
            n.Tag = new TagElement("S", "Owner", 64, s, true);
            info.Nodes.Add(n);

            uint openCount = 0;
            n = new TreeNode("Open count: "+ openCount);
            n.Tag = new TagElement("-", "OpenCount", 0, openCount);
            info.Nodes.Add(n);

            int expireCount = -1;
            n = new TreeNode("Expire count: " + (expireCount < 0 ? "NO EXPIRE" : expireCount.ToString()));
            n.Tag = new TagElement("VC", "Expire count: ", 0, expireCount, true);
            info.Nodes.Add(n);
            
            n = new TreeNode("Expire date: NO EXPIRE");
            n.Tag = new TagElement("VD", "Expire date: ", 0, null, true);
            info.Nodes.Add(n);

            n = new TreeNode("Allow resharing: NO");
            n.Tag = new TagElement("W", "Allow resharing: ", 0, false, true);
            info.Nodes.Add(n);

            n = new TreeNode("Data field #: 0");
            n.Tag = new TagElement("-", "Data field #: ", 0, 0);
            info.Nodes.Add(n);
            dataFieldCountNode = n;

            TreeNode data = new TreeNode("Data");
            data.Tag = new TagElement("+D");
            data.ImageIndex = data.SelectedImageIndex = 1;

            infoNode = info;
            dataNode = data;

            root.Nodes.Add(info);
            root.Nodes.Add(data);            

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

            addToolStripMenuItem.Enabled = dataEn;            
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
                        DataTextForm dtf = new DataTextForm(te.viewable);
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
                        TagElement countTe = (TagElement)openCountNode.Tag;
                        this.Cursor = Cursors.WaitCursor;
                        DataImageForm di = new DataImageForm(te.viewable, (uint)countTe.data);
                        this.Cursor = Cursors.Arrow;
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
                        DataVideoForm dvf = new DataVideoForm(te.viewable);
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
                        DataPDFForm dpf = new DataPDFForm(te.viewable);
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
                            int v = (int)te.data;
                            selected.Text = te.name + ((v < 0) ? "NO EXPIRE" : v.ToString());
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
                            selected.Text = te.name + ((te.data == null) ? "NO EXPIRE" : te.data);
                            setDirty();
                        }
                        df.Dispose();
                        break;
                    }
                //case "OR": // onread
                //    {
                //        GuardsForm gf = populateGuardsForm();
                //        gf.Text = "onRead event";
                //        gf.Tag = te.data;                        
                //        gf.ShowDialog();
                //        if(gf.DialogResult == DialogResult.OK)
                //        {
                //            te.data = gf.Tag;
                //            computerVarRef();
                //            setDirty();
                //        }
                //        gf.Dispose();
                //        break;
                //    }
                //case "OC": // oncopy
                //    {
                //        GuardsForm gf = populateGuardsForm();
                //        gf.Text = "onCopy event";
                //        gf.Tag = te.data;                        
                //        gf.ShowDialog();
                //        if (gf.DialogResult == DialogResult.OK)
                //        {
                //            te.data = gf.Tag;
                //            computerVarRef();
                //            setDirty();
                //        }
                //        gf.Dispose();
                //        break;
                //    }
                //case "OS": // onshare
                //    {
                //        GuardsForm gf = populateGuardsForm();
                //        gf.Text = "onShare event";
                //        gf.Tag = te.data;                       
                //        gf.ShowDialog();
                //        if (gf.DialogResult == DialogResult.OK)
                //        {
                //            te.data = gf.Tag;
                //            computerVarRef();
                //            setDirty();                            
                //        }
                //        gf.Dispose();
                //        break;
                //    }
                case "W": // switch
                    {
                        bool v = (bool)te.data;
                        v = !v;
                        te.data = v;
                        selected.Text = te.name + (v ? "YES" : "NO");
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

            te = (TagElement)dataFieldCountNode.Tag;
            int count = (int)te.data;
            te.data = count+1;
            dataFieldCountNode.Text = te.name + te.data;

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
                        
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagElement te = (TagElement)treeViewItem.SelectedNode.Tag;
            if(!te.removable) return;            

            if (MessageBox.Show("Sure to remove?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                treeViewItem.Nodes.Remove(treeViewItem.SelectedNode);

                te = (TagElement)dataFieldCountNode.Tag;
                int count = (int)te.data;
                te.data = count - 1;
                dataFieldCountNode.Text = te.name + te.data;
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
                PubKeyForm pbf = new PubKeyForm();
                if (pbf.ShowDialog() == DialogResult.Cancel) return;
                string publicKey = (string)pbf.Tag;
                pbf.Dispose();

                int writtenSize = 0;
                Stream s = new FileStream(saveFileDialog.FileName, FileMode.Create);

                ActiveDataHeader adh = new ActiveDataHeader();
                // put magic
                adh.magic = Encoding.UTF8.GetBytes("*AD*");
                // put version
                TagElement te = (TagElement)infoNode.Nodes[0].Tag;
                adh.version = (byte)te.data;
                // put creation time
                te = (TagElement)infoNode.Nodes[1].Tag;
                adh.createTime = ((DateTime)te.data).Ticks;
                // put owner
                te = (TagElement)infoNode.Nodes[2].Tag;
                adh.owner = padStringToByteArray((string)te.data, 64);
                // put datacount
                te = (TagElement)infoNode.Nodes[7].Tag;
                adh.dataCount = Convert.ToInt16((int)te.data);

                // clear hashes and empty fiedls
                adh.headerHash = new byte[20].Initialize(0);
                adh.dataHash = new byte[20].Initialize(0);
                adh.nextHeaderLen = 0;
                
                // convert string publickey into byte array
                string sExp = publicKey.Substring(0, 4);
                string sMod = publicKey.Substring(4);
                //get the object back from the stream
                var pubKey = new RSAParameters();
                pubKey.Exponent = Convert.FromBase64String(sExp);
                pubKey.Modulus = Convert.FromBase64String(sMod);
                // concatenate Exponent+Modulus
                byte[] bPubKey = Combine(pubKey.Exponent, pubKey.Modulus);                
                adh.publicKey = bPubKey;

                // Perform encryption
                var csp = new RSACryptoServiceProvider(2048);
                csp.ImportParameters(pubKey);

                // generate a symmetric key and encrypt with public key
                var aes = Aes.Create();
                aes.GenerateKey();
                adh.symmetricKey = csp.Encrypt(aes.Key, false);;

                // put end magic                
                adh.magic2 = Encoding.UTF8.GetBytes("DFB");
                
                // Actually there is no EXTRA HEADER, so go directly to GUARDHEADER

                ActiveDataGuardHeader10 adgh10 = new ActiveDataGuardHeader10();
                adgh10.magic = Encoding.UTF8.GetBytes("GD10");
                adgh10.openCount = 0;
                te = (TagElement)infoNode.Nodes[4].Tag;
                adgh10.counter = (int)te.data;
                te = (TagElement)infoNode.Nodes[5].Tag;
                adgh10.expireDate = (te.data!=null) ? (long)te.data : 0L;
                te = (TagElement)infoNode.Nodes[6].Tag;
                adgh10.flags = (bool)te.data ? (short)1 : (short)0;

                byte[] guardHeaderBuff = ActiveDataGuardHeader10ToBytes(adgh10);

                // combine data nodes
                byte[] dataBuffer = new byte[0];
                foreach (TreeNode n in dataNode.Nodes)
                {
                    te = (TagElement)n.Tag;

                    byte[] data = (byte[])te.data;
                    ActiveDataDataBlock addb = new ActiveDataDataBlock();
                    addb.name = padStringToByteArray(te.name, 32);
                    addb.extension = padStringToByteArray(te.extension, 24);
                    addb.type = padStringToByteArray(te.type, 2);
                    addb.flag = Convert.ToInt16(te.flag);
                    addb.dataLen = (uint)data.Length;

                    dataBuffer = Combine(dataBuffer, ActiveDataBlockToBytes(addb), data);
                }

                // combine buffers and compute hash
                byte[] headersBuff = Combine(ActiveDataHeaderToBytes(adh), guardHeaderBuff);
                adh.headerHash = sha1.ComputeHash(headersBuff);
                adh.dataHash = sha1.ComputeHash(dataBuffer);
                // regenerate byte arrays of header
                byte[] headerBuff = ActiveDataHeaderToBytes(adh);

                // Encrypt with symmetric key the guard block and data bloc
                var crypto = new AesCryptographyService();
                guardHeaderBuff = crypto.Encrypt(guardHeaderBuff, aes.Key, iv);
                dataBuffer = crypto.Encrypt(dataBuffer, aes.Key, iv);

                // save all
                s.Write(headerBuff, 0, headerBuff.Length);
                s.Write(guardHeaderBuff, 0, guardHeaderBuff.Length);
                s.Write(dataBuffer, 0, dataBuffer.Length);
                writtenSize += headerBuff.Length + guardHeaderBuff.Length + dataBuffer.Length;
                
                s.Close();
                dirty = false;

                MessageBox.Show("File saved! "+ writtenSize + " bytes written.");
                Text = "adEditor - " + saveFileDialog.FileName;
            }            
        }
       
        private byte[] ActiveDataHeaderToBytes(ActiveDataHeader adh)
        {
            int length = Marshal.SizeOf(adh);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(adh, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }

        private byte[] ActiveDataGuardHeader10ToBytes(ActiveDataGuardHeader10 adgh10)
        {
            int length = Marshal.SizeOf(adgh10);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(adgh10, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }

        private byte[] ActiveDataBlockToBytes(ActiveDataDataBlock adb)
        {
            int length = Marshal.SizeOf(adb);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(adb, ptr, true);
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
            byte ve = (byte)((Convert.ToByte(parts[0]) << 4) & 0xF0);
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
            return MessageBox.Show("Unsaved changes were detected. Do you want to save them?", "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);                
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
            af.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {                
                KeyForm kf = new KeyForm(openFileDialog.FileName, "private");
                if (kf.ShowDialog() == DialogResult.OK)
                {
                    privateKey = (string)kf.Tag;
                    kf.Dispose();
                }
                else
                {
                    kf.Dispose();
                    MessageBox.Show("Operation aborted.");
                    return;
                }

                //Thread t = new Thread(ServerThread);
                //t.Start();

                //Thread.Sleep(5000);

                byte[] bytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);

                //t.Join(250);
                //t = null;

                MessageBox.Show("Read all file, bytes: " + bytes.Length);
                Text = "adEditor - " + openFileDialog.FileName;

                byte[] buff = new byte[Marshal.SizeOf(typeof(ActiveDataHeader))];
                Array.Copy(bytes, 0, buff, 0, buff.Length);
                GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
                ActiveDataHeader adh = (ActiveDataHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ActiveDataHeader));
                handle.Free();

                // check magic
                if (!string.Equals(Encoding.UTF8.GetString(adh.magic), "*AD*") && !string.Equals(Encoding.UTF8.GetString( adh.magic2), "DFB"))
                {
                    MessageBox.Show("File is corrupted or is not an ActiveData file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    return;
                }

                // check version, actualli ony 1.0 can be handled
                if(adh.version!=0x10) {
                    MessageBox.Show("File is a newer format and cannot be handled with this editor. Please update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // fill the tree
                treeViewItem.Nodes.Clear();

                TreeNode root = new TreeNode("ActiveData");
                root.Tag = new TagElement("-");
                TreeNode info = new TreeNode("Information");
                info.ImageIndex = 0;
                info.Tag = new TagElement("-");

                string ver = ((adh.version & 0xF0) >> 4) + "." + (adh.version & 0x0F);
                TreeNode n = new TreeNode("Version: " + ver);
                n.ImageIndex = n.SelectedImageIndex = 3;
                n.Tag = new TagElement("-", "Version", 0, ver);
                info.Nodes.Add(n);

                DateTime dt = new DateTime(adh.createTime);
                string c = dt.ToString("dd-MM-yyyy HH:mm:ss");
                n = new TreeNode("Created: " + c);
                n.Tag = new TagElement("-", "Created", 0, dt);
                info.Nodes.Add(n);

                c = Encoding.UTF8.GetString(adh.owner);
                n = new TreeNode("Owner: " + c);
                n.Tag = new TagElement("-", "Owner", 64, c);
                info.Nodes.Add(n);
                
                n = new TreeNode("Data field #: " + adh.dataCount);
                n.Tag = new TagElement("-", "Owner", 0, adh.dataCount);
                info.Nodes.Add(n);

                // Read GuardBlock
                int prevBuffersize = buff.Length;
                buff = new byte[Marshal.SizeOf(typeof(ActiveDataGuardHeader10))];
                Array.Copy(bytes, prevBuffersize, buff, 0, buff.Length);
                handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
                ActiveDataGuardHeader10 adgh10 = (ActiveDataGuardHeader10)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ActiveDataGuardHeader10));
                handle.Free();

                var csp = new RSACryptoServiceProvider(2048);
                var pvtKey = new RSAParameters();
                pvtKey.Exponent = Convert.FromBase64String(privateKey.Substring(0, 4));
                pvtKey.Modulus = Convert.FromBase64String(privateKey.Substring(4));

                // generate a symmetric key and encrypt with public key
                var aes = Aes.Create();
                aes.GenerateKey();
                adh.symmetricKey = csp.Encrypt(aes.Key, false); ;
                byte[] prvKey = Convert.FromBase64String(privateKey);
                RSAParameters privateK = new RSAParameters();
                Array.Copy(prvKey, 0, privateK.D, 0, 256);
                Array.Copy(prvKey, privateK.D.Length, privateK.Modulus, 0, 256);
                Array.Copy(prvKey, privateK.D.Length+privateK.Modulus.Length, privateK.DP, 0, 128);
                Array.Copy(prvKey, privateK.D.Length + privateK.Modulus.Length+privateK.DP.Length, privateK.DQ, 0, 128);               
                Array.Copy(prvKey, privateK.D.Length + privateK.Modulus.Length + privateK.DP.Length+privateK.DQ.Length, privateK.P, 0, 128);
                Array.Copy(prvKey, privateK.D.Length + privateK.Modulus.Length + privateK.DP.Length+privateK.DQ.Length+privateK.P.Length, privateK.Q, 0, 128);

                /*
                TreeNode data = new TreeNode("Data");
                data.Tag = new TagElement("D");
                data.ImageIndex = data.SelectedImageIndex = 1;

                byte[] mdHash = new byte[20];
                byte[] dHash = new byte[20];
                for (int i = 0; i < 20; ++i)
                {
                    mdHash[i] = adf.metaDataHash[i];
                    dHash[i] = adf.dataHash[i];
                    adf.metaDataHash[i] = 0;
                    adf.dataHash[i] = 0;
                }
                adf.metaDataHash = sha1.ComputeHash(ActiveDataFileToBytes(adf));
                if(!equalHash(adf.metaDataHash, mdHash))
                {
                    MessageBox.Show("The file is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    treeViewItem.Nodes.Clear();
                    return;
                }

                long dataSize = 0;
                for(int i=0;i<adf.dataCount;i++)
                {
                    BufferSize = Marshal.SizeOf(typeof(ActiveDataFields));
                    buff = new byte[BufferSize];
                    Array.Copy(bytes, 2048+dataSize, buff, 0, BufferSize);
                    handle = GCHandle.Alloc(buff, GCHandleType.Pinned);

                    ActiveDataFields adFld = (ActiveDataFields)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ActiveDataFields));

                    handle.Free();

                    string fieldName = byteArrayToString(adFld.fieldName);
                    string[] extension = byteArrayToString(adFld.extension).Split('|');
                    TreeNode n2 = new TreeNode(fieldName + ": " + extension[0]+" ("+adFld.fieldSize+" characters).");
                    byte[] _b = new byte[adFld.fieldSize];
                    Array.Copy(bytes, 2048 + dataSize+BufferSize, _b, 0, adFld.fieldSize);
                    TagElement te = new TagElement(extension[1], fieldName, 0, _b, true);
                    te.viewable = true;
                    te.flag = adFld.flag;
                    n2.Tag = te;

                    data.Nodes.Add(n2);

                    dataSize = dataSize + BufferSize + adFld.fieldSize;
                }
                */
                root.Nodes.Add(info);
                //root.Nodes.Add(data);
                treeViewItem.Nodes.Add(root);
                treeViewItem.ExpandAll();
                
                
                
            }
        }

        private bool equalHash(byte[] h1, byte[] h2)
        {
            if (h1.Length != h2.Length) return false;
            for(int i = 0;i<h1.Length;++i)
            {
                if (h1[i] != h2[i]) return false;
            }
            return true;
        }

        private void setDirty()
        {
            dirty = true;
            if(!Text.EndsWith("*")) Text = Text + "*";
        }
        private void clearDirty()
        {
            dirty = false;
            if (Text.EndsWith("*")) Text = Text.Substring(0, Text.Length - 1);
        }

        private string byteArrayToString(byte[] b)
        {
            string s = Encoding.UTF8.GetString(b);
            string s2 = "";
            foreach(char c in s.ToCharArray())
            {
                if (c == '\0') return s2;
                s2 += c;
            }
            return s2;
        }

        private byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        private static void ServerThread(object data)
        {
            NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("ad_pipe", PipeDirection.InOut, 1);

            // Wait for a client to connect
            pipeServer.WaitForConnection();

            try
            {
                // Read the request from the client. Once the client has
                // written to the pipe its security token will be available.

                StreamString ss = new StreamString(pipeServer);

                // Verify our identity to the connected client using a
                // string that the client anticipates.

                ss.WriteString("HLO!");
                string filename = ss.ReadString();
                ss.WriteString(privateKey);                
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            pipeServer.Close();
            pipeServer.Dispose();
        }
    }

    // Defines the data protocol for reading and writing strings on our stream
    public class StreamString
    {
        private Stream ioStream;
        private UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            int len = 0;

            len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue)
            {
                len = (int)UInt16.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }

    public class AesCryptographyService
    {
        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, encryptor);
                }
            }
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, decryptor);
                }
            }
        }

        private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }
    }
}
