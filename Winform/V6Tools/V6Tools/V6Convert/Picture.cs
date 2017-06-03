using System.Drawing;
using System.IO;

namespace V6Tools.V6Convert
{
    public class Picture
    {
        public static byte[] ToJpegByteArray(Image image)
        {
            if (image == null) return null;// new byte[0];
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static Image LoadCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }

    }
}
