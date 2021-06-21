using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adEditor
{
    public partial class DataVideoForm : Form
    {
        string tfile;
        TagElement te;

        public DataVideoForm()
        {
            InitializeComponent();
        }

        private void DataVideoForm_Load(object sender, EventArgs e)
        {
            te = (TagElement)this.Tag;
            if (te.data != null)
            {
                tfile = System.IO.Path.GetTempFileName();
                File.Move(tfile, Path.ChangeExtension(tfile, te.extension));
                tfile = Path.ChangeExtension(tfile, te.extension);
                byte[] b = (byte[])te.data;
                File.WriteAllBytes(tfile, b);
                axWindowsMediaPlayer.Visible = true;
                axWindowsMediaPlayer.URL = tfile;
                axWindowsMediaPlayer.Ctlcontrols.play();
            }
            else
            {
                axWindowsMediaPlayer.Visible = false;
            }
        }

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Video files (*.avi;*.mp4;*.mpg)|*.avi;*.mp4;*.mpg|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] b = File.ReadAllBytes(ofd.FileName);
                te.data = b;
                te.extension = Path.GetExtension(ofd.FileName);
            }
        }

        private void DataVideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(tfile!=null) File.Delete(tfile);
        }
    }
}
