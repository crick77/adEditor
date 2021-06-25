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
        byte[] data;
        string extension;

        public DataVideoForm()
        {
            InitializeComponent();

            Core.Initialize();
            _libVLC = new LibVLC();
            _mp = new MediaPlayer(_libVLC);
            videoView.MediaPlayer = _mp;
        }

        private void DataVideoForm_Load(object sender, EventArgs e)
        {
            te = (TagElement)this.Tag;
            data = (byte[])te.data;
            extension = te.extension;

            bPlay.Enabled = bPause.Enabled = bStop.Enabled = (data != null);

            showVideo();
        }

        private void showVideo()
        {
            if (data != null)
            {
                var stream = new MemoryStream(data);
                MediaInput mi = new StreamMediaInput(stream);
                Media media = new Media(_libVLC, mi, null);
                videoView.MediaPlayer.Play(media);
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
                data = File.ReadAllBytes(ofd.FileName);
                extension = Path.GetExtension(ofd.FileName).Substring(1).ToUpper(); ;

                bPlay.Enabled = bPause.Enabled = bStop.Enabled = (data != null);

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
            if (!videoView.MediaPlayer.IsPlaying)
            {
                videoView.MediaPlayer.Play();
            }
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            if (videoView.MediaPlayer.IsPlaying)
            {
                videoView.MediaPlayer.Stop();
                videoView.MediaPlayer.Position = 0;
            }            
        }

        private void bPause_Click(object sender, EventArgs e)
        {
            if (videoView.MediaPlayer.IsPlaying)
            {
                videoView.MediaPlayer.Pause();
            }
            else
            {
                videoView.MediaPlayer.Play();
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            te.data = data;
            te.extension = extension;

            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
