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
    public class rptLineDC : LineShape
    {
        //bool moveStartPoint=false, moveEndPoint=false, moveLine=false;

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
                        this.BorderColor = Color.LightGray;
                    }
                    else
                    {
                        this.BorderColor = BorderColor;
                    }
                    if (SuppressChanged != null)
                        SuppressChanged(this, new EventArgs());
                }
            }
        }
        public delegate void OnSuppressChanged(object sender, EventArgs e);
        public event OnSuppressChanged SuppressChanged;
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
                    if (CloseAtPageBreakChanged != null)
                        CloseAtPageBreakChanged(this, new EventArgs());
                }
            }
        }
        public delegate void OnCloseAtPageBreakChanged(object sender, EventArgs e);
        public event OnCloseAtPageBreakChanged CloseAtPageBreakChanged;
        #endregion

        PictureObject _obj;
        public rptLineDC(PictureObject obj)
        {
            _obj = obj;
            Name = obj.Name;
            StartPoint = new Point(Twip.TwipToPixelX(obj.Left), Twip.TwipToPixelY(obj.Top));
            EndPoint = new Point(Twip.TwipToPixelX(obj.Left + obj.Width), Twip.TwipToPixelY(obj.Top + obj.Height));
            this.BorderColor = obj.Border.BorderColor;
            Cursor = Cursors.Hand;
            ApplyEvent();
        }

        public void ApplyEvent()
        {
            this.MouseDown += rptLineDC_MouseDown;
            this.MouseClick += rptLineDC_MouseClick;

            this.MouseMove += rptLine_MouseMove;
        }

        void rptLine_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormRptEditor.selectedLineName == Name)
            if (nearPoint(new Point(0, 0), e.X, e.Y)
                || nearPoint(new Point(Math.Abs(EndPoint.X - StartPoint.X)
                    , Math.Abs(EndPoint.Y - StartPoint.Y)), e.X, e.Y))
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            else this.Cursor = Cursors.SizeAll;

        }
        bool nearPoint(Point p, int x, int y)
        {
            if (Math.Abs(p.X - x) < 4 && Math.Abs(p.Y - y) < 4) return true;

            return false;
        }

        void rptLineDC_MouseDown(object sender, MouseEventArgs e)
        {
            FormRptEditor.selectedLineName = this.Name;
        }

        //public bool selected { get; set; }
        void rptLineDC_MouseClick(object sender, MouseEventArgs e)
        {
            
            if(FormRptEditor.focusedLineDC != null)
                FormRptEditor.focusedLineDC.Cursor = Cursors.Hand;
            this.Cursor = Cursors.SizeAll;
            FormRptEditor.focusedLineDC = this;
            FormRptEditor.focusedLine0 = null;
            FormRptEditor.SetPropertySelectedObject(this, this.Name);
        }
        
        public void UpdateChage()
        {
            if (FormRptEditor._rpt != null)
            {
                ReportObject rptobj = FormRptEditor._rpt.ReportDefinition.ReportObjects[this.Name];
                rptobj.Top = Twip.PixelToTwipY(this.Y1);
                rptobj.Left = Twip.PixelToTwipX(this.X1);

                rptobj.Width = Twip.PixelToTwipX(this.X2 - X1);
                rptobj.Height = Twip.PixelToTwipX(this.Y2 - Y1);
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
