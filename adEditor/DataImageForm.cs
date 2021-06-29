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
        uint flag;
        uint openCounter;

        public DataImageForm(uint openCounter)
        {
            InitializeComponent();

            this.openCounter = openCounter;
        }
        
        private void bOk_Click(object sender, EventArgs e)
        {
            te.data = data;
            te.extension = extension;
            te.flag = flag;

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
                flag = te.flag;
                cbDegrade.Checked = (flag == 1);

                if (data != null)
                    showImage();
                else
                    pBox.Image = pBox.InitialImage;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void showImage()
        {
            Bitmap b = new Bitmap(byteArrayToImage(data));
            if (cbDegrade.Checked && openCounter>0)
            {
                b = Blur(b, new Rectangle(0, 0, b.Width, b.Height), (int)openCounter);
            }
            pBox.Image = b;
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

        // superic github
        private Bitmap Blur(Bitmap image, Rectangle rectangle, Int32 blurSize)
        {
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    Int32 avgR = 0, avgG = 0, avgB = 0;
                    Int32 blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (Int32 x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (Int32 y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for (Int32 x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (Int32 y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            return blurred;
        }

        private void cbDegrade_CheckedChanged(object sender, EventArgs e)
        {
            flag = (uint)((cbDegrade.Checked) ? 1 : 0);
        }
    }
}
