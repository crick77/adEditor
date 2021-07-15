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
using Microsoft.Win32;

namespace adEditor
{    
    public partial class MainForm : Form
    {
        private TreeNode dataNode = null;
        private TreeNode infoNode = null;
        private TreeNode openCountNode = null;
        private TreeNode dataFieldCountNode = null;
        private bool dirty;
        private Thread pipeThread;
        private static RSAParameters privateKey;
        private static RSAParameters publicKey;
        private static string pipeResult;
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
            root.ImageIndex = root.SelectedImageIndex = 15;
            root.Tag = new TagElement("-");
            
            TreeNode info = new TreeNode("Information");
            info.ImageIndex = info.SelectedImageIndex = 0;
            info.Tag = new TagElement("-");

            TreeNode n = new TreeNode("Version: 1.0");
            n.ImageIndex = n.SelectedImageIndex = 3;
            n.Tag = new TagElement("S", "Version", 0, versionToByte("1.0"));
            info.Nodes.Add(n);

            DateTime now = DateTime.Now;
            n = new TreeNode("Created: "+now.ToString("dd-MM-yyyy HH:mm:ss"));
            n.ImageIndex = n.SelectedImageIndex = 9;
            n.Tag = new TagElement("D", "Created", 0, now);
            info.Nodes.Add(n);

            string s = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            n = new TreeNode("Owner: " + s);
            n.ImageIndex = n.SelectedImageIndex = 12;
            n.Tag = new TagElement("S", "Owner", 64, s, true);
            info.Nodes.Add(n);

            uint openCount = 0;
            n = new TreeNode("Open count: "+ openCount);
            n.ImageIndex = n.SelectedImageIndex = 11;
            n.Tag = new TagElement("-", "OpenCount", 0, openCount);
            info.Nodes.Add(n);

            int expireCount = -1;
            n = new TreeNode("Expire count: UNLIMITED");
            n.ImageIndex = n.SelectedImageIndex = 8;
            n.Tag = new TagElement("VC", "Expire count: ", 0, expireCount, true);
            info.Nodes.Add(n);
            
            n = new TreeNode("Expire date: NO EXPIRE");
            n.ImageIndex = n.SelectedImageIndex = 10;
            n.Tag = new TagElement("VD", "Expire date: ", 0, null, true);
            info.Nodes.Add(n);

            n = new TreeNode("Allow resharing: NO");
            n.ImageIndex = n.SelectedImageIndex = 14;
            n.Tag = new TagElement("W", "Allow resharing: ", 0, false, true);
            info.Nodes.Add(n);

            n = new TreeNode("Data field #: 0");
            n.ImageIndex = n.SelectedImageIndex = 13;
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
            saveToolStripMenuItem.Enabled = true;
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

        private void PDFToolStripMenuItem_Click(object sender, EventArgs e)
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
                string _publicKey = (string)pbf.Tag;
                pbf.Dispose();

                // get a stream from the string
                var sr = new System.IO.StringReader(_publicKey);
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream

                //get the object back from the stream
                var pubKey = (RSAParameters)xs.Deserialize(sr);
                // concatenate Exponent+Modulus
                byte[] bPubKey = Combine(pubKey.Exponent, pubKey.Modulus);
                
                int writtenSize = 0;
                Stream s = new FileStream(saveFileDialog.FileName, FileMode.Create);
                                                
                // Encrypt with symmetric key the guard block and data bloc
                // Perform encryption
                var csp = new RSACryptoServiceProvider(2048);
                csp.ImportParameters(pubKey);

                // generate a symmetric key and encrypt with public key
                var aes = Aes.Create();
                aes.GenerateKey();
                var crypto = new AesCryptographyService();                                

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
                adh.nextHeaderLen = 0;

                adh.publicKey = bPubKey;
                adh.symmetricKey = csp.Encrypt(aes.Key, false);

                // put end magic                
                adh.magic2 = Encoding.UTF8.GetBytes("DFB");
                
                // Actually there is no EXTRA HEADER, so go directly to GUARDHEADER

                ActiveDataGuardHeader10 adgh10 = new ActiveDataGuardHeader10();
                adgh10.magic = Encoding.UTF8.GetBytes("GD10");
                adgh10.openCount = 0;
                te = (TagElement)infoNode.Nodes[4].Tag;
                adgh10.counter = (int)te.data;
                te = (TagElement)infoNode.Nodes[5].Tag;
                adgh10.expireDate = (te.data!=null) ? ((DateTime)te.data).Ticks : 0L;
                te = (TagElement)infoNode.Nodes[6].Tag;
                adgh10.flags = (bool)te.data ? (short)1 : (short)0;

                byte[] guardHeaderBuff = ActiveDataGuardHeader10ToBytes(adgh10);
                
                // combine buffers and compute hash
                byte[] headersBuff = Combine(ActiveDataHeaderToBytes(adh), guardHeaderBuff);
                adh.headerHash = sha1.ComputeHash(headersBuff);
                // regenerate byte arrays of header
                byte[] headerBuff = ActiveDataHeaderToBytes(adh);

                guardHeaderBuff = crypto.Encrypt(guardHeaderBuff, aes.Key, iv);

                // save all
                s.Write(headerBuff, 0, headerBuff.Length);
                s.Write(guardHeaderBuff, 0, guardHeaderBuff.Length);

                // combine data nodes and save
                writtenSize += headerBuff.Length + guardHeaderBuff.Length;
                foreach (TreeNode n in dataNode.Nodes)
                {
                    TagElement _te = (TagElement)n.Tag;

                    byte[] data = crypto.Encrypt((byte[])_te.data, aes.Key, iv);
                    ActiveDataDataBlock addb = new ActiveDataDataBlock();
                    addb.name = padStringToByteArray(_te.name, 32);
                    addb.extension = padStringToByteArray(_te.extension, 24);
                    addb.type = padStringToByteArray(_te.type, 2);
                    addb.flag = Convert.ToInt16(_te.flag);
                    addb.dataHash = new byte[20].Initialize(0);
                    addb.dataLen = (uint)data.Length;

                    byte[] addbBuf = ActiveDataBlockToBytes(addb);
                    byte[] dataBuffer = Combine(addbBuf, data);
                    addb.dataHash = sha1.ComputeHash(headersBuff);

                    addbBuf = ActiveDataBlockToBytes(addb);
                    dataBuffer = Combine(addbBuf, data);
                    s.Write(dataBuffer, 0, dataBuffer.Length);
                    writtenSize += dataBuffer.Length;
                }
                                                
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
            if(pipeThread!=null)
            {
                var pipeClient = new NamedPipeClientStream(".", "ad_pipe", PipeDirection.InOut, PipeOptions.None, System.Security.Principal.TokenImpersonationLevel.Impersonation);
                pipeClient.Connect();

                var ss = new StreamString(pipeClient);
                string msg = ss.ReadString();
                if(string.Equals(msg, "HLO!"))
                {
                    ss.WriteString("?EXIT?");
                    pipeClient.Close();
                    pipeClient.Dispose();
                }
            }
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
                // start pipe thread
                pipeResult = null;
                pipeThread = new Thread(ServerThread);
                pipeThread.Start();                
                
                try
                {
                    byte[] bytes;
                    try
                    {
                        // request I/O
                        bytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);                        
                    }
                    catch (Exception)
                    {
                        // in case of exception, wait pipe to finish
                        pipeThread.Join(500);
                        pipeThread = null;

                        switch (pipeResult)
                        {
                            case "DATE_EXPIRED":
                                {
                                    MessageBox.Show("You're trying to open the file past the allowed date. Sorry.", "Halt!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    break;
                                }
                            case "COUNTER_EXPIRED":
                                {
                                    MessageBox.Show("Maximum number of opening reached. Sorry.", "Halt!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    break;
                                }
                            case "WRONG_KEY":
                                {
                                    throw new CryptographicException();
                                }
                            default:
                                {
                                    MessageBox.Show("Cannot read the file, there were some problem.", "Halt!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    break;
                                }
                        }
                        return;
                    }

                    MessageBox.Show("Read all file, bytes: " + bytes.Length);
                    Text = "adEditor - " + openFileDialog.FileName;

                    byte[] buff = new byte[Marshal.SizeOf(typeof(ActiveDataHeader))];
                    int mainHeadeLen = buff.Length;
                    Array.Copy(bytes, 0, buff, 0, buff.Length);
                    GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
                    ActiveDataHeader adh = (ActiveDataHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ActiveDataHeader));
                    handle.Free();

                    // check magic
                    if (!string.Equals(Encoding.UTF8.GetString(adh.magic), "*AD*") && !string.Equals(Encoding.UTF8.GetString(adh.magic2), "DFB"))
                    {
                        MessageBox.Show("File is corrupted or is not an ActiveData file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // check version, actualli ony 1.0 can be handled
                    if (adh.version != 0x10)
                    {
                        MessageBox.Show("File is a newer format and cannot be handled with this editor. Please update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // fill the tree
                    treeViewItem.Nodes.Clear();

                    TreeNode root = new TreeNode("ActiveData");
                    root.ImageIndex = root.SelectedImageIndex = 15;
                    root.Tag = new TagElement("-");

                    TreeNode info = new TreeNode("Information");
                    info.ImageIndex = info.SelectedImageIndex = 0;
                    info.Tag = new TagElement("-");

                    string ver = ((adh.version & 0xF0) >> 4) + "." + (adh.version & 0x0F);
                    TreeNode n = new TreeNode("Version: " + ver);
                    n.ImageIndex = n.SelectedImageIndex = 3;
                    n.Tag = new TagElement("-", "Version", 0, adh.version);
                    info.Nodes.Add(n);

                    DateTime dt = new DateTime(adh.createTime);
                    string c = dt.ToString("dd-MM-yyyy HH:mm:ss");
                    n = new TreeNode("Created: " + c);
                    n.ImageIndex = n.SelectedImageIndex = 9;
                    n.Tag = new TagElement("-", "Created", 0, dt);
                    info.Nodes.Add(n);

                    c = Encoding.UTF8.GetString(adh.owner);
                    n = new TreeNode("Owner: " + c);
                    n.ImageIndex = n.SelectedImageIndex = 12;
                    n.Tag = new TagElement("-", "Owner", 64, c);
                    info.Nodes.Add(n);

                    n = new TreeNode("Data field #: " + adh.dataCount);
                    n.ImageIndex = n.SelectedImageIndex = 13;
                    n.Tag = new TagElement("-", "Owner", 0, adh.dataCount);
                    info.Nodes.Add(n);

                    // Create crypto objects
                    var csp = new RSACryptoServiceProvider(2048);
                    var crypto = new AesCryptographyService();
                    
                    // Dectype symmetri key
                    csp.ImportParameters(privateKey);
                    byte[] symmetricKey = csp.Decrypt(adh.symmetricKey, false);

                    // Read GuardBlock
                    int prevBuffersize = buff.Length;
                    // The buffer for GuardBlock is 22 bytes + PCSK#17 padding to 32 bytes
                    buff = new byte[32];
                    Array.Copy(bytes, prevBuffersize, buff, 0, buff.Length);
                    // decrypt buffer
                    buff = crypto.Decrypt(buff, symmetricKey, iv);

                    // convert it back to struct
                    handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
                    ActiveDataGuardHeader10 adgh10 = (ActiveDataGuardHeader10)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ActiveDataGuardHeader10));
                    handle.Free();

                    if (!string.Equals(Encoding.UTF8.GetString(adgh10.magic), "GD10"))
                    {
                        MessageBox.Show("Some headers are corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    n = new TreeNode("Open count: " + adgh10.openCount);
                    n.ImageIndex = n.SelectedImageIndex = 11;
                    n.Tag = new TagElement("-", "OpenCount", 0, adgh10.openCount);
                    info.Nodes.Add(n);

                    n = new TreeNode("Remaining open count: " + ((adgh10.counter < 0) ? "UNLIMITED" : adgh10.counter.ToString()));
                    n.ImageIndex = n.SelectedImageIndex = 8;
                    n.Tag = new TagElement("-", "RemainingCount", 0, adgh10.openCount);
                    info.Nodes.Add(n);

                    n = new TreeNode("Expire date: " + ((adgh10.expireDate == 0) ? "NO EXPIRE" : new DateTime(adgh10.expireDate).ToString()));
                    n.ImageIndex = n.SelectedImageIndex = 10;
                    n.Tag = new TagElement("-", "ExpireDate", 0, new DateTime(adgh10.expireDate));
                    info.Nodes.Add(n);
                    infoNode = info;

                    TreeNode data = new TreeNode("Data");
                    data.Tag = new TagElement("D");
                    data.ImageIndex = data.SelectedImageIndex = 1;

                    byte[] dataBuffer = Extract(bytes, mainHeadeLen + 32, bytes.Length - mainHeadeLen - 32);

                    for (int i = 0; i < adh.dataCount; i++)
                    {
                        buff = new byte[Marshal.SizeOf(typeof(ActiveDataDataBlock))];
                        Array.Copy(dataBuffer, 0, buff, 0, buff.Length);
                        handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
                        ActiveDataDataBlock addb = (ActiveDataDataBlock)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ActiveDataDataBlock));
                        handle.Free();

                        string dataName = byteArrayToString(addb.name);
                        string extension = byteArrayToString(addb.extension);
                        string type = byteArrayToString(addb.type);

                        byte[] _b = new byte[addb.dataLen];
                        Array.Copy(dataBuffer, buff.Length, _b, 0, _b.Length);
                        _b = crypto.Decrypt(_b, symmetricKey, iv);
                        TagElement te = new TagElement(type, dataName, 0, _b, true);
                        te.extension = extension;
                        te.viewable = true;
                        te.flag = Convert.ToUInt32(addb.flag);

                        TreeNode n2 = new TreeNode(dataName + ": " + extension + " (" + _b.Length + " characters/bytes).");
                        switch (te.type)
                        {
                            case "I":
                                {
                                    n2.ImageIndex = n.SelectedImageIndex = 4;
                                    break;
                                }
                            case "P":
                                {
                                    n2.ImageIndex = n.SelectedImageIndex = 6;
                                    break;
                                }
                            case "V":
                                {
                                    n2.ImageIndex = n.SelectedImageIndex = 5;
                                    break;
                                }
                            case "T":
                                {
                                    n2.ImageIndex = n.SelectedImageIndex = 2;
                                    break;
                                }
                            default: // unknown data type
                                {
                                    n2.ImageIndex = n.SelectedImageIndex = 16;
                                    break;
                                }                        
                        } // switch
                        
                        n2.Tag = te;

                        data.Nodes.Add(n2);

                        dataBuffer = Extract(dataBuffer, (int)(buff.Length + addb.dataLen), (int)(dataBuffer.Length - buff.Length - addb.dataLen));
                    }
                    dataNode = data;

                    root.Nodes.Add(info);
                    root.Nodes.Add(data);
                    treeViewItem.Nodes.Add(root);
                    treeViewItem.ExpandAll();

                    // enable sharing if allowed and we have at least 1 opening left
                    shareToolStripMenuItem.Enabled = ((adgh10.flags & 1) == 1) && (adgh10.counter>0);
                }
                catch(CryptographicException)
                {
                    MessageBox.Show("The file cannot be decrypted. It this file really for you?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
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

        private byte[] Extract(byte[] source, int start, int len)
        {
            byte[] b = new byte[len];
            Array.Copy(source, start, b, 0, len);
            return b;
        }

        private static void ServerThread(object data)
        {
            NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("ad_pipe", PipeDirection.InOut, 1);

            // Wait for a client to connect
            pipeServer.WaitForConnection();
            Console.WriteLine("Client connected!!");

            try
            {
                // Read the request from the client. Once the client has
                // written to the pipe its security token will be available.
                StreamString ss = new StreamString(pipeServer);

                // Verify our identity to the connected client using a
                // string that the client anticipates.

                ss.WriteString("HLO!");
                string filename = ss.ReadString();
                if(string.Equals(filename, "?EXIT?"))
                {
                    Console.WriteLine("EXIT request received. Performing a graceful exit...");
                    pipeServer.Close();
                    pipeServer.Dispose();
                    return;
                }
                Console.WriteLine("HLO written, filename: "+filename);
                
                var sw = new System.IO.StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, privateKey);

                ss.WriteString(sw.ToString());   
                pipeResult = ss.ReadString();                
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            pipeServer.Close();
            pipeServer.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\adEditor");
            if (key == null || key.ValueCount<2)
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\adEditor", false);

                key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\adEditor");
                var csp = new RSACryptoServiceProvider(2048);

                // private key
                var privKey = csp.ExportParameters(true);

                string privKeyString;
                {
                    //we need some buffer
                    var sw = new System.IO.StringWriter();
                    //we need a serializer
                    var _xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                    //serialize the key into the stream
                    _xs.Serialize(sw, privKey);
                    //get the string from the stream
                    privKeyString = sw.ToString();
                }

                //and the public key ...
                var pubKey = csp.ExportParameters(false);

                string pubKeyString;
                {
                    //we need some buffer
                    var sw = new System.IO.StringWriter();
                    //we need a serializer
                    var _xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                    //serialize the key into the stream
                    _xs.Serialize(sw, pubKey);
                    //get the string from the stream
                    pubKeyString = sw.ToString();
                }

                key.SetValue("pub", pubKeyString);
                key.SetValue("priv", privKeyString);
            }
            
            //get a stream from the string
            var sr = new System.IO.StringReader(key.GetValue("priv").ToString());
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            privateKey = (RSAParameters)xs.Deserialize(sr);

            sr = new System.IO.StringReader(key.GetValue("pub").ToString());
            //get the object back from the stream
            publicKey = (RSAParameters)xs.Deserialize(sr);

            key.Close();
        }

        private void showPublicKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyForm kf = new KeyForm();
            var sw = new System.IO.StringWriter();
            //we need a serializer
            var _xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //serialize the key into the stream
            _xs.Serialize(sw, publicKey);
            //get the string from the stream
            kf.Tag = sw.ToString();
            kf.ShowDialog();
            kf.Dispose();
        }
        private void shareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PubKeyForm pbf = new PubKeyForm();
                if (pbf.ShowDialog() == DialogResult.Cancel) return;
                string _publicKey = (string)pbf.Tag;
                pbf.Dispose();

                // get a stream from the string
                var sr = new System.IO.StringReader(_publicKey);
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                
                //get the object back from the stream
                var pubKey = (RSAParameters)xs.Deserialize(sr);
                // concatenate Exponent+Modulus
                byte[] bPubKey = Combine(pubKey.Exponent, pubKey.Modulus);

                int writtenSize = 0;
                Stream s = new FileStream(saveFileDialog.FileName, FileMode.Create);

                // Encrypt with symmetric key the guard block and data bloc
                // Perform encryption
                var csp = new RSACryptoServiceProvider(2048);
                csp.ImportParameters(pubKey);

                // generate a symmetric key and encrypt with public key
                var aes = Aes.Create();
                aes.GenerateKey();
                var crypto = new AesCryptographyService();

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
                te = (TagElement)infoNode.Nodes[3].Tag;
                adh.dataCount = Convert.ToInt16(te.data);

                // clear hashes and empty fiedls
                adh.headerHash = new byte[20].Initialize(0);
                adh.nextHeaderLen = 0;

                adh.publicKey = bPubKey;
                adh.symmetricKey = csp.Encrypt(aes.Key, false);

                // put end magic                
                adh.magic2 = Encoding.UTF8.GetBytes("DFB");

                // Actually there is no EXTRA HEADER, so go directly to GUARDHEADER

                ActiveDataGuardHeader10 adgh10 = new ActiveDataGuardHeader10();
                adgh10.magic = Encoding.UTF8.GetBytes("GD10");
                te = (TagElement)infoNode.Nodes[4].Tag;
                adgh10.openCount = (int)te.data;
                te = (TagElement)infoNode.Nodes[5].Tag;
                adgh10.counter = (int)te.data;
                te = (TagElement)infoNode.Nodes[6].Tag;
                adgh10.expireDate = (te.data != null) ? ((DateTime)te.data).Ticks : 0L;
                // Lock anyfurther sharing
                adgh10.flags = 0;

                byte[] guardHeaderBuff = ActiveDataGuardHeader10ToBytes(adgh10);

                // combine buffers and compute hash
                byte[] headersBuff = Combine(ActiveDataHeaderToBytes(adh), guardHeaderBuff);
                adh.headerHash = sha1.ComputeHash(headersBuff);
                // regenerate byte arrays of header
                byte[] headerBuff = ActiveDataHeaderToBytes(adh);

                guardHeaderBuff = crypto.Encrypt(guardHeaderBuff, aes.Key, iv);

                // save all
                s.Write(headerBuff, 0, headerBuff.Length);
                s.Write(guardHeaderBuff, 0, guardHeaderBuff.Length);

                // combine data nodes and save
                writtenSize += headerBuff.Length + guardHeaderBuff.Length;
                foreach (TreeNode n in dataNode.Nodes)
                {
                    TagElement _te = (TagElement)n.Tag;

                    byte[] data = crypto.Encrypt((byte[])_te.data, aes.Key, iv);
                    ActiveDataDataBlock addb = new ActiveDataDataBlock();
                    addb.name = padStringToByteArray(_te.name, 32);
                    addb.extension = padStringToByteArray(_te.extension, 24);
                    addb.type = padStringToByteArray(_te.type, 2);
                    addb.flag = Convert.ToInt16(_te.flag);
                    addb.dataHash = new byte[20].Initialize(0);
                    addb.dataLen = (uint)data.Length;

                    byte[] addbBuf = ActiveDataBlockToBytes(addb);
                    byte[] dataBuffer = Combine(addbBuf, data);
                    addb.dataHash = sha1.ComputeHash(headersBuff);

                    addbBuf = ActiveDataBlockToBytes(addb);
                    dataBuffer = Combine(addbBuf, data);
                    s.Write(dataBuffer, 0, dataBuffer.Length);
                    writtenSize += dataBuffer.Length;
                }

                s.Close();
                
                MessageBox.Show("File shared! " + writtenSize + " bytes written.");                
            }
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
