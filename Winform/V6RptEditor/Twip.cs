using System.Drawing;

namespace V6RptEditor
{
    /// <summary>
    /// Chuyển đổi đơn vị Twip, pixel, mm và inch
    /// Cần chạy hàm GetTwipRate(Graphics g) trước
    /// để tăng độ chính xác cho mỗi loại màn hình
    /// Giá trị mặc định áp dụng cho màn hình LG FLATRON L1942SE
    /// (19inch - 1280 x 1024, 96dpi)
    /// </summary>
    public static class Twip
    {
        public static float twipPerPixelX = 15, twipPerPixelY = 15;
        public static float twipPerMM = 56.69f, dpiX = 96, dpiY = 96;
        public static float twipPerInch = 1440;

        public static void GetTwipRate(Graphics g)
        {    
            try
            {
                twipPerPixelX = twipPerInch / g.DpiX;
                twipPerPixelY = twipPerInch / g.DpiY;
                dpiX = g.DpiX; dpiY = g.DpiY;
                twipPerMM = twipPerInch / 2.54f / 10;
            }
            finally
            {
                g.Dispose();
            }
        }

        public static int PixelToTwipX(int pixel)
        {
            return (int)(pixel * twipPerPixelX);
        }
        public static int PixelToTwipX(float pixel)
        {
            return (int)(pixel * twipPerPixelX);
        }
        public static int PixelToTwipY(int pixel)
        {
            return (int)(pixel * twipPerPixelY);
        }
        public static int TwipToPixelX(int twip)
        {
            return Làm_tròn(twip / twipPerPixelX);
        }
        public static float TwipToPixelXF(int twip)
        {
            return twip / twipPerPixelX;
        }
        public static int TwipToPixelY(int twip)
        {
            return Làm_tròn(twip / twipPerPixelY);
        }

        public static int Làm_tròn(float value)
        {
            int n = (int)value;
            if (value - n > 0.5f) n++;
            return n;
        }
    }
}
