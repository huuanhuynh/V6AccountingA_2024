using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic.PowerPacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class rptLine0 : LineShape
    {
        #region ==== Object Name ====
        [Category("_crProperty")]
        [Description("Tên đối tượng")]
        public string ObjectName
        {
            get
            {
                return _obj.Name;
            }
        }
        #endregion
        #region ==== LineColor ====
        [Description("Màu viền")]
        [Category("_crProperty")]
        public Color LineColor
        {
            get { return _obj.LineColor; }
            set
            {
                _obj.LineColor = value;
                Invalidate();
                //OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle(new Point(0,0), this.Size)));
            }
        }
        #endregion

        #region ==== LineThickness ====
        [Description("độ dày")]
        [Category("_crProperty")]
        public float LineThickness
        {
            get { return Twip.TwipToPixelXF(_obj.LineThickness); }
            set
            {
                _obj.LineThickness = Twip.PixelToTwipX(value);
                this.BorderWidth = Twip.Làm_tròn(value);
                Invalidate();
            }
        }
        #endregion

        

        #region ==== Suppress (Ẩn) ====
        [Category("_crProperty")]
        [Description("Ẩn đi")]
        [DefaultValue(false)]
        public bool Suppress
        {
            get { return _obj.ObjectFormat.EnableSuppress; }
            set
            {
                if (value != _obj.ObjectFormat.EnableSuppress)
                {
                    _obj.ObjectFormat.EnableSuppress = value;
                    if (value)
                    {
                        //oldColor = this.BorderColor;
                        this.BorderColor = Color.LightGray;
                    }
                    else
                    {
                        this.BorderColor = LineColor;
                    }
                    if (SuppressChanged != null)
                    SuppressChanged(this, new EventArgs());
                }
            }
        }
        public delegate void OnEnableSuppressChanged(object sender, EventArgs e);
        public event OnEnableSuppressChanged SuppressChanged;
        #endregion

        #region ==== CloseAtPageBreak ====
        [Description("Tự kéo dài xuống cuối vùng. Không thể hiện khi thiết kế. Thể hiện khi in hoặc xem trước khi in.")]
        [Category("_crProperty")]
        [DefaultValue(false)]
        public bool CloseAtPageBreak
        {
            get { return _obj.ObjectFormat.EnableCloseAtPageBreak; }
            set
            {
                //enableCloseAtPageBreak = value;
                if (value != _obj.ObjectFormat.EnableCloseAtPageBreak)
                {
                    _obj.ObjectFormat.EnableCloseAtPageBreak = value;
                    if(CloseAtPageBreakChanged!=null)
                    CloseAtPageBreakChanged(this, new EventArgs());
                }
            }
        }
        public delegate void OnCloseAtPageBreakChanged(object sender, EventArgs e);
        public event OnCloseAtPageBreakChanged CloseAtPageBreakChanged;
        #endregion

        bool Hline = true;
        LineObject _obj;
        public rptLine0(LineObject obj)
        {
            _obj = obj;
            Name = obj.Name;
            StartPoint = new Point(Twip.TwipToPixelX(obj.Left), Twip.TwipToPixelY(obj.Top));
            EndPoint = new Point(Twip.TwipToPixelX(obj.Left + obj.Width), Twip.TwipToPixelY(obj.Top + obj.Height));
            if (obj.Top == obj.Bottom) Hline = true;
            else Hline = false;
            this.Cursor = Cursors.SizeAll;
            this.BorderWidth = Twip.TwipToPixelX(obj.LineThickness);
            this.BorderColor = obj.LineColor;
            
            if (Suppress)
                this.BorderColor = Color.LightGray;
            
            ApplyEvent();
        }

        public rptLine0()
        {
            ApplyEvent();
        }

        public void ApplyEvent()
        {
            this.MouseDown += rptLine_MouseDown;
            this.MouseClick += rptLine_MouseClick;
            //this.MouseDown += rptOleDC_MouseDown;
            this.MouseMove += rptLine_MouseMove;
        }

        void rptLine_MouseMove(object sender, MouseEventArgs e)
        {
            if(FormRptEditor.selectedLineName == Name)
            if (nearPoint(new Point(0,0),e.X,e.Y) 
                || nearPoint(new Point(Math.Abs(EndPoint.X-StartPoint.X)
                    ,Math.Abs(EndPoint.Y - StartPoint.Y)),e.X,e.Y))
            {
                if (Hline) this.Cursor = Cursors.SizeWE;
                else this.Cursor = Cursors.SizeNS;//Viline
            }
            else this.Cursor = Cursors.SizeAll;
            
        }
        bool nearPoint(Point p, int x, int y)
        {
            if (Math.Abs(p.X - x) < 4 && Math.Abs(p.Y - y) < 4) return true;

            return false;
        }

        void rptLine_MouseDown(object sender, MouseEventArgs e)
        {
            FormRptEditor.selectedLineName = this.Name;
        }

        //[DefaultValue(false)]
        //public bool selected { get; set; }
        void rptLine_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (FormRptEditor.focusedLine0 != null)
                FormRptEditor.focusedLine0.Cursor = Cursors.Hand;
            this.Cursor = Cursors.SizeAll;
            FormRptEditor.focusedLine0 = this;
            FormRptEditor.focusedLineDC = null;
            FormRptEditor.SetPropertySelectedObject(this, this.Name);
        }

        public void UpdateChage()
        {
            if (FormRptEditor._rpt != null)
            {
                LineObject lineObj = (LineObject) FormRptEditor._rpt.ReportDefinition.ReportObjects[this.Name];
                
                if (Hline)
                {
                    //đường ngang. cho left và right bằng left mong muốn
                    lineObj.Left = Twip.PixelToTwipX(this.X1);
                    lineObj.Right = lineObj.Left;
                    //Thay đổi top và bottom tới top mong muốn
                    lineObj.Top = Twip.PixelToTwipY(this.Y1);
                    lineObj.Bottom = lineObj.Top;
                    //Thay đổi right theo mong muốn
                    lineObj.Right = Twip.PixelToTwipX(this.X2);
                }
                else
                {
                    //đường dọc. cho top và bottom bằng top mong muốn
                    lineObj.Top = Twip.PixelToTwipY(this.Y1);
                    lineObj.Bottom = lineObj.Top;
                    //Đổi left = right = left mong muốn
                    lineObj.Left = Twip.PixelToTwipX(this.X1);
                    lineObj.Right = lineObj.Left;

                    lineObj.Bottom = Twip.PixelToTwipX(this.Y2);
                }
            }
        }
                
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {   
            base.OnPaint(e);
            if (Focused)
            {
                SolidBrush b = new SolidBrush(Color.Red);
                //vẽ 2 cục 2 đầu
                e.Graphics.FillRectangle(b,
                    StartPoint.X-1, StartPoint.Y-1, 3, 3);
                e.Graphics.FillRectangle(b,
                    EndPoint.X - 2, EndPoint.Y - 2, 3, 3);
            }
            if (this == FormRptEditor._classStatic.ObjForPGrid)
            {
                Pen pen = new Pen(Color.Red);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLine(pen, StartPoint, EndPoint);
            }
        }
        
    }
}
