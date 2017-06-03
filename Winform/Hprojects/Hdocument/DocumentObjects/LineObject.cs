using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Printing;
using System.Linq;
using HaUtility.Helper;

namespace H_document.DocumentObjects
{
    public class LineObject : DocumentObject
    {
        //public override DocumentObjectType ObjectType { get{return DocumentObjectType.Line;} }

        /// <summary>
        /// Khởi tạo đối tượng dòng kẻ
        /// </summary>
        public LineObject()
        {
            ObjectType = DocumentObjectType.Line;
        }
        public LineObject(LineObject copy)
            : base(copy)
        {
            _drawLines = copy.DrawLines;
            LineWeight = copy.LineWeight;
        }

        public override DocumentObject Clone()
        {
            return new LineObject(this);
        }
        public LineObject(SortedDictionary<string, string> data):base(data)
        {
            if (data == null) return;
            if (data.ContainsKey(Field.DrawLines))
            {
                try
                {
                    int t = int.Parse(data[Field.DrawLines]);
                    DrawLines = (DrawLine) t;
                }
                catch (Exception ex)
                {
                    new Logger("", "Hdocument").WriteLog0("LineObject ctor " + ex.Message);
                }
            }
        }

        public override void DrawToGraphics(Graphics g, Margins margins, SortedDictionary<string, object> parameters, Mode drawMode, DocumentObject[] selectedObjects)
        {
            var pen = new Pen(BaseColor,_lineWeight);
            //Vẽ các đoạn thẳng
            var newPointA = new PointF(TopLeftF.X + margins.Left, TopLeftF.Y + margins.Top);
            var newPointB = new PointF(RightF + margins.Left, TopF + margins.Top);
            var newPointC = new PointF(BottomRightF.X + margins.Left, BottomRightF.Y + margins.Top);
            var newPointD = new PointF(LeftF + margins.Left, BottomF + margins.Top);

            if ((DrawLines&DrawLine.AB) == DrawLine.AB) g.DrawLine(pen, newPointA, newPointB);
            if ((DrawLines&DrawLine.AD) == DrawLine.AD) g.DrawLine(pen, newPointA, newPointD);
            if ((DrawLines&DrawLine.DC) == DrawLine.DC) g.DrawLine(pen, newPointD, newPointC);
            if ((DrawLines&DrawLine.BC) == DrawLine.BC) g.DrawLine(pen, newPointB, newPointC);
            if ((DrawLines&DrawLine.AC) == DrawLine.AC) g.DrawLine(pen, newPointA, newPointC);
            if ((DrawLines&DrawLine.BD) == DrawLine.BD) g.DrawLine(pen, newPointB, newPointD);

            //Vẽ các chữ cái. designMode + selected
            if (selectedObjects != null && selectedObjects.Contains(this) && drawMode == Mode.Design)
            {
                g.DrawString("A", new Font("Arial", 8.25f, FontStyle.Regular), new SolidBrush(Color.Red),
                    new RectangleF(margins.Left + LeftF - 1, margins.Top + TopF - 3, 3, 3));
                g.DrawString("B", new Font("Arial", 8.25f, FontStyle.Regular), new SolidBrush(Color.Red),
                    new RectangleF(margins.Left + RightF - 1, margins.Top + TopF - 3, 3, 3));
                g.DrawString("C", new Font("Arial", 8.25f, FontStyle.Regular), new SolidBrush(Color.Red),
                    new RectangleF(margins.Left + RightF - 1, margins.Top + BottomF + 1, 3, 3));
                g.DrawString("D", new Font("Arial", 8.25f, FontStyle.Regular), new SolidBrush(Color.Red),
                    new RectangleF(margins.Left + LeftF - 1, margins.Top + BottomF, 3, 3));
            }

            base.DrawToGraphics(g, margins, parameters, drawMode, selectedObjects);
        }

        public override string ToString()
        {
            return "Line: " + DrawLines + "" + LocationF + "/" + ObjectSize;
        }
        [Browsable(false)]
        public PointF TopLeftF
        {
            get { return _objectLocation; }
            set { _objectLocation = value; }
        }
        [Browsable(false)]
        public PointF BottomRightF
        {
            get { return new PointF(RightF, BottomF); }
            set {
                if(value.X>=LeftF) RightF = value.X;
                if(value.Y>=TopF) BottomF = value.Y;
            }
        }

        [Category("Đường kẻ")]
        [Description("Độ đậm nhạt của đường kẻ tính bằng mm.")]
        [DisplayName(@"Độ dày đường kẻ")]
        public LineMode LineWeight
        {
            get
            {
                if (_lineWeight < 0.15f) return LineMode.Siêu_nhỏ;
                if(_lineWeight < 0.25) return LineMode.Nhỏ;
                if(_lineWeight < 0.65) return LineMode.Vừa;
                if(_lineWeight < 1f) return LineMode.To;
                if(_lineWeight < 2f) return LineMode.Rất_to;
                return LineMode.Siêu_to;
            }
            set
            {
                switch (value)
                {
                    case LineMode.Siêu_nhỏ:
                        _lineWeight = 0.1f;
                        break;
                    case LineMode.Nhỏ:
                        _lineWeight = 0.2f;
                        break;
                    case LineMode.Vừa:
                        _lineWeight = 0.6f;
                        break;
                    case LineMode.To:
                        _lineWeight = 0.9f;
                        break;
                    case LineMode.Rất_to:
                        _lineWeight = 1.9f;
                        break;
                    case LineMode.Siêu_to:
                        _lineWeight = 2.5f;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value", value, null);
                }
            }
        }
        private float _lineWeight = 0.1f;
        
        [Category("Đường kẻ")]
        [Description("Các đoạn thẳng trên hình chữ nhật ABCD, quy tắc trên->dưới, trái->phải (vd AB,AC,BD không gọi CA,DB)")]
        [DefaultValue(DrawLine.AB)]
        [DisplayName(@"Các đường kẻ")]
        public DrawLine DrawLines { get { return _drawLines; } set { _drawLines = value; } }
        protected DrawLine _drawLines = DrawLine.AB;

        [Category("Đường kẻ")]
        [DisplayName(@"Màu đường kẻ")]
        public Color LineColor { get { return _lineColor; } set { _lineColor = value; } }
        private Color _lineColor = Color.Black;

    }

    [Flags]//Có thể dùng | để gán nhiều giá trị.
    [Editor("System.Windows.Forms.Design.AnchorEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    public enum DrawLine
    {
        AB = 1,
        DC = 2,
        AD = 4,
        BC = 8,
        AC = 16,
        BD = 32,
        None = 0,
    }
}
