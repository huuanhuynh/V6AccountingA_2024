using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

namespace H_document.DocumentObjects
{
    public static class DocumentObjectHelper
    {
        public static float Distance(PointF point1, PointF point2)
        {
            var a = Math.Abs(point2.X - point1.X);
            var b = Math.Abs(point2.Y - point1.Y);
            return (float)Math.Sqrt(a * a + b * b);
        }

        public static void DrawTextInRectangleF(Graphics graphics, string text, Font font, Brush brush, RectangleF rec, StringFormat stringFormat)
        {
            graphics.DrawString(text, font, brush, rec, stringFormat);
        }

        /// <summary>
        /// Vẽ điểm resize không cần tính margin nữa.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pen"></param>
        /// <param name="point"></param>
        /// <param name="r"></param>
        public static void DrawMovePoint(Graphics graphics, Pen pen, PointF point, float r = 1)
        {
            var recF = new RectangleF(point.X - r, point.Y - r, 2 * r, 2 * r);
            graphics.DrawEllipse(pen, recF);
        }
        public static void DrawMovePoint(Graphics graphics, Pen pen, Margins margins, PointF point, float r=1)
        {
            var recF = new RectangleF(point.X + margins.Left - r, point.Y + margins.Top - r, 2 * r, 2 * r);
            graphics.DrawEllipse(pen, recF);
        }

        public static bool IsNear(PointF point1, PointF point2, float distance)
        {
            var distance0 = 1f;
            distance0 = Distance(point1, point2);
            return distance0 <= distance;
        }

        
    }
}
