using System.Drawing;
using System.Drawing.Drawing2D;
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

        /// <summary>
        /// Thay đổi kích thước image nếu lớn quá.
        /// </summary>
        /// <param name="image">Hình ảnh cần thay đổi kích thước.</param>
        /// <param name="maxW">Chiều rộng tối đa.</param>
        /// <param name="maxH">Chiều cao tối đa.</param>
        /// <param name="stretch">Co giãn theo kích thước mới.</param>
        /// <returns></returns>
        public static Image ResizeDownImage(Image image, int maxW, int maxH, bool stretch = false)
        {
            double resizeWidth = image.Width;
            double resizeHeight = image.Height;
            bool have = false;

            if (stretch)
            {
                if (resizeWidth > maxW)
                {
                    resizeWidth = maxW;
                    have = true;
                }
                if (resizeHeight > maxH)
                {
                    resizeHeight = maxH;
                    have = true;
                }
            }
            else
            {
                double aspect = resizeWidth / resizeHeight;

                if (resizeWidth > maxW)
                {
                    have = true;
                    resizeWidth = maxW;
                    resizeHeight = resizeWidth / aspect;
                }
                if (resizeHeight > maxH)
                {
                    have = true;
                    aspect = resizeWidth / resizeHeight;
                    resizeHeight = maxH;
                    resizeWidth = resizeHeight * aspect;
                }
            }

            if (have)
                return ResizeImage(image, (int)resizeWidth, (int)resizeHeight);
            else
                return image;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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
                    Bitmap bm = new Bitmap((Image)im.Clone());
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
