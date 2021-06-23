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
using LibVLCSharp.Shared;

namespace adEditor
{
    public partial class DataVideoForm : Form
    {       
        TagElement te;
        LibVLC _libVLC;
        MediaPlayer _mp;

        public DataVideoForm()
        {
            InitializeComponent();
            Core.Initialize();
            _libVLC = new LibVLC();
            _mp = new MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mp;
        }

        private void DataVideoForm_Load(object sender, EventArgs e)
        {
            te = (TagElement)this.Tag;
            showVideo();
        }

        private void showVideo()
        {
            if (te.data != null)
            {
                var stream = new MemoryStream((byte[])te.data);
                MediaInput mi = new StreamMediaInput(stream);
                Media media = new Media(_libVLC, mi, null);
                videoView1.MediaPlayer.Play(media);
                media.Dispose();
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

                showVideo();
            }
        }

        private void DataVideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mp.Stop();
            _mp.Dispose();
            _libVLC.Dispose();
        }

        private void bPlay_Click(object sender, EventArgs e)
        {
            if(videoView1.MediaPlayer.IsPlaying)
            {
                videoView1.MediaPlayer.Position = 0;
            }
            else
            {
                videoView1.MediaPlayer.Stop();
                videoView1.MediaPlayer.Position = 0;                
                videoView1.MediaPlayer.Play();
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
