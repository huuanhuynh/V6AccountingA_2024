using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace H
{
    public class HDrawing
    {
        public static Point[] ClonePolygon(Point[] ps)
        {
            Point[] clonePs = new Point[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                clonePs[i] = new Point(ps[i].X, ps[i].Y);
            }
            return clonePs;
        }

        /// <summary>
        /// Chuyển thành màu trong suốt
        /// </summary>
        /// <param name="c"></param>
        /// <param name="anpha">Độ mờ, giá trị từ 0 đến 255.</param>
        /// <returns></returns>
        public static Color Color2Transparent(Color c, int anpha)
        {
            if (anpha < 0) anpha = 0;
            if (anpha > 255) anpha = 255;
            return Color.FromArgb(anpha, c.R, c.G, c.B);
        }



        #region ==== Draw ====

        public static void DrawLine(Graphics gp, Point p1, Point p2)
        {
            DrawLine(gp, p1, p2, Color.Black);
        }

        public static void DrawLine(Graphics gp, Point p1, Point p2, Color c)
        {
            DrawLine(gp, p1, p2, c, 1);
        }

        public static void DrawLine(Graphics gp, Point p1, Point p2, Color c, int width)
        {
            gp.DrawLine(new Pen(c,width), p1, p2);
        }

        public static void DrawLine(Graphics gp, Point p1, Point p2, Pen p)
        {
            gp.DrawLine(p, p1, p2);
        }

        public static void DrawLineTransparent(Graphics gp, Point p1, Point p2, Color c, int width)
        {
            DrawLine(gp, p1, p2, new Pen(Color2Transparent(c, 128), width));
        }

        public static void DrawPolygon(Graphics gp, Point[] points, Color c, int width)
        {
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (points.Length > 2)
            {
                Pen p = new Pen(c, width);
                gp.DrawPolygon(p, points);
            }
            else
            {
                DrawMyPolygon(gp, points, c, width);
            }
        }
        public static void DrawMyPolygon(Graphics g, Point[] points, Color c, int width)
        {
            int len = points.Length;
            if (len > 0)
            {
                Pen pen = new Pen(c, width);

                for (int i = 1; i < len; i++)
                {
                    DrawLine(g, points[i - 1], points[i], pen);
                }
                //last line
                DrawLine(g, points[len - 1], points[0], pen);
            }
        }

        public static void DrawPolygon(Graphics gp, Point[] points, Pen p)
        {
            if(points.Length>2)
                gp.DrawPolygon(p, points);
        }

        public static void DrawPolygonTransparent(Graphics gp, Point[] points, Color c, int width)
        {
            if (points.Length > 2)
            {
                Pen p = new Pen(Color2Transparent(c, 128), width);
                gp.DrawPolygon(p, points);
            }
        }

        #endregion draw


        #region ==== Fill ====

        public static void FillPolygon(Graphics gp, Point[] ps, Color c)
        {
            Brush b = new SolidBrush(c);
            gp.FillPolygon(b, ps);
        }

        public static void FillPolygonTransparent(Graphics gp, Point[] ps, Color c)
        {
            if (ps.Length > 2)
            {
                Brush b = new SolidBrush(HDrawing.Color2Transparent(c, 128));
                gp.FillPolygon(b, ps);
            }
        }

        #endregion fill


        #region ==== IS ====

        public static bool IsInPolygon1(Point[] poly, Point p)
        {
            Point p1, p2;


            bool inside = false;


            if (poly.Length < 3)
            {
                return inside;
            }


            var oldPoint = new Point(
                poly[poly.Length - 1].X, poly[poly.Length - 1].Y);


            for (int i = 0; i < poly.Length; i++)
            {
                var newPoint = new Point(poly[i].X, poly[i].Y);


                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;

                    p2 = newPoint;
                }

                else
                {
                    p1 = newPoint;

                    p2 = oldPoint;
                }


                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }


                oldPoint = newPoint;
            }


            return inside;
        }

        public static bool IsNearPoint(Point p1, Point p2)
        {
            if ((Math.Abs(p1.X - p2.X) <= 3)
                && (Math.Abs(p1.Y - p2.Y) <= 3))
                return true;
            return false;
        }

        public static int IsNearPolygonPoint(Point[] points, Point p)
        {
            //foreach (Point point in points)
            //{
            //    if (IsNearPoint(point, p)) return true;
            //}
            for (int i = 0; i < points.Length; i++)
            {
                if (IsNearPoint(points[i], p)) return i;
            }
            return -1;
        }

        #endregion is

        public static Image resizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap( width, height );
            Graphics g = Graphics.FromImage( (Image ) b );
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawImage( img, 0, 0, width, height );
            g.Dispose();
            return (Image ) b ;
        }
        //- See more at: http://expressmagazine.net/development/472/resize-image-thay-doi-kich-thuoc-anh-c#sthash.LsjCmuLq.dpuf
        public static Image Resize(Image img, float percentage)
        {
            //lấy kích thước ban đầu của bức ảnh
            int originalW = img.Width; int originalH = img.Height;
            //tính kích thước cho ảnh mới theo tỷ lệ đưa vào 
            int resizedW = (int)(originalW * percentage); int resizedH = (int)(originalH * percentage);
            //tạo 1 ảnh Bitmap mới theo kích thước trên
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            //tạo 1 graphic mới từ Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //vẽ lại ảnh ban đầu lên bmp theo kích thước mới
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            //giải phóng tài nguyên mà graphic đang giữ
            graphic.Dispose();
            //return the image
            return (Image)bmp;
        }
        //- See more at: http://expressmagazine.net/development/472/resize-image-thay-doi-kich-thuoc-anh-c#sthash.LsjCmuLq.dpuf

    }
}
