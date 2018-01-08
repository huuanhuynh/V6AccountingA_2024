using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using HaUtility.Converter;

namespace H_document.DocumentObjects
{
    public class DocumentObject// : ICloneable
    {
        public DocumentObject()
        {
            _objectLocation = new PointF(0, 0);
            _objectSize = new SizeF(30, 5);
        }

        protected DocumentObject(DocumentObject copy)
        {
            Name = copy.Name;
            ObjectType = copy.ObjectType;
            ObjectSize = copy.ObjectSize;
            _objectLocation = copy.LocationF;
        }

        public DocumentObject(SortedDictionary<string, string> data)
        {
            if (data == null) return;
            if (data.ContainsKey(Field.Location))
            {
                string[] ss = PrimitiveTypes.ObjectToString(data[Field.Location]).Split(';');
                _objectLocation = new PointF(
                    PrimitiveTypes.ToObject<float>(ss[0]),
                    PrimitiveTypes.ToObject<float>(ss[1]));
            }
            if (data.ContainsKey(Field.Size))
            {
                string[] ss = PrimitiveTypes.ObjectToString(data[Field.Size]).Split(';');
                _objectSize = new SizeF(
                    PrimitiveTypes.ToObject<float>(ss[0]),
                    PrimitiveTypes.ToObject<float>(ss[1]));
            }
            //if (data.ContainsKey("TEXT")) Text = data["TEXT"];
        }

        public Color BaseColor = Color.Black;
        [DisplayName(@"Màu chữ")]
        public Color ForceColor { get { return _forceColor; } set { _forceColor = value; } }
        private Color _forceColor = Color.Black;

        [DisplayName("Loại đối tượng")]
        public DocumentObjectType ObjectType { get; protected set; }
        
        #region ==== Location properties ====
        
        /// <summary>
        /// Vị trí góc trên bên trái của đối tượng (mm)
        /// </summary>
        [Browsable(false)]
        public PointF LocationF { get { return _objectLocation; } set { _objectLocation = value; } }

        protected PointF _objectLocation = new PointF(0f,0f);
        /// <summary>
        /// Tọa độ trái (mm)
        /// </summary>
        [CategoryAttribute("Vị trí")]
        [DisplayName(@"Tọa độ trái (mm)")]
        public float LeftF { get { return _objectLocation.X; } set { _objectLocation.X = value; } }
        /// <summary>
        /// Tọa độ trên (mm)
        /// </summary>
        [CategoryAttribute("Vị trí")]
        [DisplayName(@"Tọa độ trên (mm)")]
        public float TopF { get { return _objectLocation.Y; } set { _objectLocation.Y = value; } }
        /// <summary>
        /// tọa độ dưới (mm)
        /// </summary>
        [CategoryAttribute("Vị trí")]
        [Description("Vị trí dưới của đối tượng tính theo milimet.")]
        [DisplayName(@"Tọa độ dưới (mm)")]
        public float BottomF { get { return _objectLocation.Y + HeightF; } set { if(value-TopF>0.1f) HeightF = value - TopF; } }
        /// <summary>
        /// Tọa độ phải (mm)
        /// </summary>
        [CategoryAttribute("Vị trí")]
        [DisplayName(@"Tọa độ phải (mm)")]
        public float RightF { get { return _objectLocation.X + WidthF; } set { if(value - LeftF>0.1f) WidthF = value - LeftF; } }
        #endregion location

        #region ==== ObjectSize properties ====
        
        [Browsable(false)]
        public SizeF ObjectSize { get { return _objectSize; } set { _objectSize = value; } }
        private SizeF _objectSize = new SizeF(5f, 5f);
        /// <summary>
        /// Chiều rộng (mm)
        /// </summary>
        [CategoryAttribute("Kích thước")]
        [DisplayName(@"Rộng (mm)")]
        public virtual float WidthF { get { return _objectSize.Width; } set { _objectSize.Width = value; } }
        /// <summary>
        /// Chiều cao (mm)
        /// </summary>
        [CategoryAttribute("Kích thước")]
        [DisplayName(@"Cao (mm)")]
        public virtual float HeightF { get { return _objectSize.Height; } set { _objectSize.Height = value; } }
        #endregion size

        [Browsable(false)]
        public RectangleF ObjectRectangleF { get { return new RectangleF(_objectLocation, _objectSize); } }
        [Browsable(false)]
        public Rectangle ObjectRectangle { get { return new Rectangle((int)LeftF,(int)TopF,(int)WidthF,(int)HeightF);} }
        [Browsable(false)]
        public List<PointF> ResizePoints
        {
            get
            {
                var result = new List<PointF>();
                //Point 1 góc dưới bên phải
                result.Add(BottomRightResizePoint);
                //Point 2 bên dưới
                result.Add(BottomCenterResizePoint);
                //Point 3 bên phải
                result.Add(RightCenterResizePoint);
                return result;
            }
        }

        [Browsable(false)]
        public PointF BottomRightResizePoint
        {
            get{ return new PointF(RightF, BottomF); }
            set
            {
                RightF = value.X;
                BottomF = value.Y;
            }
        }
        [Browsable(false)]
        public PointF BottomCenterResizePoint
        {
            get { return new PointF(LeftF + WidthF/2, BottomF); }
            set { BottomF = value.Y; }
        }
        [Browsable(false)]
        public PointF RightCenterResizePoint
        {
            get { return new PointF(RightF, TopF + HeightF/2); }
            set { RightF = value.X; }
        }

        [Browsable(false)]
        public PointF StartMovePoint { get; set; }

        /// <summary>
        /// Move or Resize
        /// </summary>
        [Browsable(false)]
        public HMode DesignMode { get; set; }

        /// <summary>
        /// Hàm kế thừa cần gọi lại base.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="margins"></param>
        /// <param name="parameters"></param>
        /// <param name="drawMode"></param>
        /// <param name="selectedObjects"></param>
        public virtual void DrawToGraphics(Graphics graphics, Margins margins, SortedDictionary<string, object> parameters, HMode drawMode, DocumentObject[] selectedObjects)
        {
            //Hàm sẽ được override
            if (selectedObjects != null && selectedObjects.Contains(this))
            //if (this == selectedObjects)
            {
                Pen pen = new Pen(Color.Red, 0.1f);
                pen.DashStyle = DashStyle.Dot;
                graphics.DrawRectangle(pen, LeftF+margins.Left, TopF+margins.Top, WidthF, HeightF);
                if (drawMode == HMode.Design)
                {
                    //Vẽ ô di chuyển
                    DrawMovePoints(graphics, margins, 1f);
                }
            }
        }

        private void DrawMovePoints(Graphics graphics, Margins margins, float r)
        {
            Pen pen = new Pen(BaseColor, 0.2f);
            foreach (PointF point in ResizePoints)
            {
                DocumentObjectHelper.DrawMovePoint(graphics, pen, margins, point, r);
            }
        }

        

        /// <summary>
        /// Đơn vị của point là milimet.
        /// </summary>
        /// <param name="point">Đã trừ margins</param>
        /// <returns></returns>
        public bool Containt(PointF point)
        {
            if (point.X < LeftF || point.Y < TopF) return false;
            if (point.X > RightF || point.Y > BottomF) return false;

            return true;
        }

        /// <summary>
        /// Gán trạng thái cho selected object.
        /// </summary>
        /// <param name="mouseDownPoint">Đã trừ margins</param>
        public virtual void SetStatus(PointF mouseDownPoint)
        {
            ResizePointType = PointType.None;
            DesignMode = HMode.None;
            if (DocumentObjectHelper.IsNear(mouseDownPoint, BottomRightResizePoint, 1f))
            {
                ResizePointType = PointType.BottomRightResizePoint;
                DesignMode = HMode.Resize;
            }
            else if (DocumentObjectHelper.IsNear(mouseDownPoint, BottomCenterResizePoint, 1f))
            {
                ResizePointType = PointType.BottomCenterResizePoint;
                DesignMode = HMode.Resize;
            }
            else if (DocumentObjectHelper.IsNear(mouseDownPoint, RightCenterResizePoint, 1f))
            {
                ResizePointType = PointType.RightCenterResizePoint;
                DesignMode = HMode.Resize;
            }
            else if (Containt(mouseDownPoint))
            {
                DesignMode = HMode.Move;
            }
        }
        
        /// <summary>
        /// Lấy trạng thái cho selected object.
        /// </summary>
        /// <param name="mouseDownPoint">Đã trừ margins</param>
        public virtual PointType GetStatus(PointF mouseDownPoint)
        {
            ResizePointType = PointType.None;
            DesignMode = HMode.None;
            if (DocumentObjectHelper.IsNear(mouseDownPoint, BottomRightResizePoint, 1f))
            {
                ResizePointType = PointType.BottomRightResizePoint;
                //DesignMode = Mode.Resize;
                //return 1;
            }
            else if (DocumentObjectHelper.IsNear(mouseDownPoint, BottomCenterResizePoint, 1f))
            {
                ResizePointType = PointType.BottomCenterResizePoint;
                //DesignMode = Mode.Resize;
                //return 2;
            }
            else if (DocumentObjectHelper.IsNear(mouseDownPoint, RightCenterResizePoint, 1f))
            {
                ResizePointType = PointType.RightCenterResizePoint;
                //DesignMode = Mode.Resize;
                //return 3;
            }
            else if (Containt(mouseDownPoint))
            {
                //DesignMode = Mode.Move;
                //return 0;
                return PointType.MoveItem;
            }
            return ResizePointType;
        }

        [Browsable(false)]
        public PointType ResizePointType { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Thay đổi kích thước của đối tượng.
        /// </summary>
        /// <param name="newPoint">Vị trí mới của điểm resize đã trừ margins</param>
        public virtual void SetSelectedResizePointLocation(PointF newPoint)
        {
            switch (ResizePointType)
            {
                case PointType.None:
                    break;
                case PointType.BottomRightResizePoint:
                    BottomRightResizePoint = newPoint;
                    break;
                case PointType.BottomCenterResizePoint:
                    BottomCenterResizePoint = newPoint;
                    break;
                case PointType.RightCenterResizePoint:
                    RightCenterResizePoint = newPoint;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual DocumentObject Clone()
        {
            return new DocumentObject(this);
        }

        /// <summary>
        /// Điểm resize lúc bắt đầu.
        /// </summary>
        public PointF SelectedResizePointStartLocation = new PointF(0,0);
        public void ResizeByPoint(PointType pointType, SizeF distant)
        {
            //ResizePointType = pointType;
            var newLocation = SelectedResizePointStartLocation + distant;
            SetSelectedResizePointLocation(newLocation);
        }
    }

    class FancyStringEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (svc != null)
            {
                using (var frm = new Form { Text = "Nhập nội dung" })
                using (var txt = new TextBox { Text = (string)value, Dock = DockStyle.Fill, Multiline = true })
                using (var ok = new Button { Text = "OK", Dock = DockStyle.Bottom })
                {
                    frm.Controls.Add(txt);
                    frm.Controls.Add(ok);
                    frm.AcceptButton = ok;
                    ok.DialogResult = DialogResult.OK;
                    if (svc.ShowDialog(frm) == DialogResult.OK)
                    {
                        value = txt.Text;
                    }
                }
            }
            return value;
        }
    }

    public enum DocumentObjectType
    {
        Text,
        Line,
        Box,
        Picture,
        Details
    }
}
