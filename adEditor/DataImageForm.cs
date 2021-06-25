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

namespace adEditor
{
    public partial class DataImageForm : Form
    {
        TagElement te;
        byte[] data;
        string extension;

        public DataImageForm()
        {
            InitializeComponent();
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

        private void DataImage_Load(object sender, EventArgs e)
        {
            te = (TagElement)this.Tag;
            if(te!=null)
            {
                data = (byte[])te.data;
                extension = te.extension;

                if (data != null)
                    showImage();
                else
                    pBox.Image = pBox.InitialImage;
            }
        }

        private void showImage()
        {
            pBox.Image = byteArrayToImage(data);
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image i = Image.FromFile(ofd.FileName);
                extension = formatToExt(i);

                data = imageToByteArray(i);
                showImage();
            }
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public System.Drawing.Imaging.ImageFormat extToFormat(string ext)
        {
            switch(ext.ToUpper())
            {
                case "JPG": return System.Drawing.Imaging.ImageFormat.Jpeg;
                case "PNG": return System.Drawing.Imaging.ImageFormat.Png;
                case "GIF": return System.Drawing.Imaging.ImageFormat.Gif;
                case "TIFF": return System.Drawing.Imaging.ImageFormat.Tiff;
                case "WMF": return System.Drawing.Imaging.ImageFormat.Wmf;
                case "EMF": return System.Drawing.Imaging.ImageFormat.Emf;
                case "EXIF": return System.Drawing.Imaging.ImageFormat.Exif;
                case "ICON": return System.Drawing.Imaging.ImageFormat.Icon;
                default: return System.Drawing.Imaging.ImageFormat.Bmp;
            }
        }

        public string formatToExt(Image img)
        {
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)) return "JPG";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)) return "PNG";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)) return "GIF";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)) return "TIFF";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Wmf)) return "WMF";            
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf)) return "EMF";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif)) return "EXIF";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon)) return "ICON";

            return "BMP";
        }
    }
}
