using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace V6Tools.V6Convert
{
    public class Picture
    {
        public static byte[] ToJpegByteArray(Image image)
        {
            return ToByteArray(image, ImageFormat.Jpeg);
        }
        
        public static byte[] ToBmpByteArray(Image image)
        {
            return ToByteArray(image, ImageFormat.Bmp);
        }
        
        public static byte[] ToByteArray(Image image, ImageFormat imageFormat)
        {
            if (image == null) return null;
            MemoryStream ms = new MemoryStream();
            image.Save(ms, imageFormat);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static Image ToImage(object objectData)
        {
            Image picture = null;
            if (objectData is Image) picture = (Image)objectData;
            else if (objectData is byte[]) picture = ByteArrayToImage((byte[]) objectData);
            else
            {
                picture = ByteArrayToImage(Encoding.ASCII.GetBytes(objectData.ToString()));
            }

            return picture;
        }

        /// <summary>
        /// Tải hình ảnh từ file, không giữ file, nếu lỗi trả về null.
        /// </summary>
        /// <param name="path">đường dẫn đến file hình ảnh</param>
        /// <returns></returns>
        public static Image LoadCopyImage(string path)
        {
            try
            {
                using (Image im = Image.FromFile(path))
                {
                    Bitmap bm = new Bitmap(im);
                    return bm;
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

    }
}
