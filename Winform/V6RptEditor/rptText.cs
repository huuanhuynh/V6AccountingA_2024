using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class rptText: Label
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

        #region ==== BackGroundColor ====
        [Category("_crProperty")]
        [Description("Màu nền")]
        public Color BackGroundColor
        {
            get { return _obj.Border.BackgroundColor; }
            set
            {
                if (value != _obj.Border.BackgroundColor)
                {
                    _obj.Border.BackgroundColor = value;
                    BackColor = value;//Đổi hiển thị màu
                    Tag = value;//Đổi biến lưu dùng cho hover
                    try
                    {
                        BackGroundColorChanged(this, new EventArgs());
                    }
                    catch { }
                }
            }
        }
        public delegate void OnBackGroundColorChanged(object sender, EventArgs e);
        public event OnBackGroundColorChanged BackGroundColorChanged;
        #endregion

        #region ==== CanGrow ====
        [Category("_crProperty")]
        [Description("Cho phép nhiều dòng")]
        public bool CanGrow
        {
            get { return _obj.ObjectFormat.EnableCanGrow; }
            set
            {

                if (value != _obj.ObjectFormat.EnableSuppress)
                {
                    _obj.ObjectFormat.EnableSuppress = value;
                    if (value)
                    {
                        this.ForeColor = Color.LightGray;
                    }
                    else
                    {
                        this.ForeColor = _obj.Color;
                    }
                    if (CanGrowChanged != null)
                        CanGrowChanged(this, new EventArgs());
                }
            }
        }
        public delegate void OnCanGrowChanged(object sender, EventArgs e);
        public event OnCanGrowChanged CanGrowChanged;
        #endregion

        #region ==== TextColor ====
        [Category("_crProperty")]
        [Description("Màu chữ")]
        public Color TextColor
        {
            get { return _obj.Color; }
            set
            {
                if (value != _obj.Color)
                {
                    _obj.Color = value;
                    if(!Suppress)
                        ForeColor = value;//Đổi hiển thị màu
                    if(TextColorChanged!=null)
                        TextColorChanged(this, new EventArgs());
                }
            }
        }
        public delegate void OnTextColorChanged(object sender, EventArgs e);
        public event OnTextColorChanged TextColorChanged;
        #endregion

        #region ==== Suppress (Ẩn) ====
        [Category("_crProperty")]
        [Description("Ẩn đi")]
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
                        this.ForeColor = Color.LightGray;
                    }
                    else
                    {
                        this.ForeColor = _obj.Color;
                    }
                    if(SuppressChanged!=null)
                    SuppressChanged(this, new EventArgs());
                }
            }
        }
        public delegate void OnSuppressChanged(object sender, EventArgs e);
        public event OnSuppressChanged SuppressChanged;
        #endregion

        #region ==== HorizontalAlignment (Text Align) ====
        [Category("_crProperty")]
        [Description("Canh lề ngang (trái, phải, giữa)")]
        public CrystalDecisions.Shared.Alignment HorizontalAlignment
        {
            get { return _obj.ObjectFormat.HorizontalAlignment; }
            set
            {
                if (value != _obj.ObjectFormat.HorizontalAlignment)
                {
                    _obj.ObjectFormat.HorizontalAlignment = value;
                    switch (_obj.ObjectFormat.HorizontalAlignment)
                    {
                        case CrystalDecisions.Shared.Alignment.Decimal:
                            TextAlign = ContentAlignment.TopRight;
                            break;
                        case CrystalDecisions.Shared.Alignment.DefaultAlign:
                            TextAlign = ContentAlignment.TopLeft;
                            break;
                        case CrystalDecisions.Shared.Alignment.HorizontalCenterAlign:
                            TextAlign = ContentAlignment.TopCenter;
                            break;
                        case CrystalDecisions.Shared.Alignment.Justified:
                            TextAlign = ContentAlignment.TopRight;//xài đỡ
                            break;
                        case CrystalDecisions.Shared.Alignment.LeftAlign:
                            TextAlign = ContentAlignment.TopLeft;
                            break;
                        case CrystalDecisions.Shared.Alignment.RightAlign:
                            TextAlign = ContentAlignment.TopRight;
                            break;
                        default:
                            break;
                    }

                }
            }
        }
        #endregion
        //public override Point Location { get; set; }

        TextObject _obj;
        public rptText(TextObject obj)
        {
            _obj = obj;
            this.Name = obj.Name;
            
            #region === getSize ===
            this.AutoSize = false;
            
            int lbw = 0, rbw = 0, tbw = 0, bbw = 0;
            if (LeftLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) lbw = 0;
            else if (LeftLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) lbw = 5;
            else lbw = 1;

            if (RightLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) rbw = 0;
            else if (RightLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) rbw = 5;
            else rbw = 1;

            if (TopLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) tbw = 0;
            else if (TopLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) tbw = 5;
            else tbw = 1;

            if (BottomLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) bbw = 0;
            else if (BottomLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) bbw = 5;
            else bbw = 1;
            
            this.Size = new Size(Twip.TwipToPixelX(obj.Width) + lbw + rbw, Twip.TwipToPixelY(obj.Height) + tbw + bbw);
            #endregion
            #region === getLocation ===
            this.Location = new Point(Twip.TwipToPixelX(obj.Left)-lbw, Twip.TwipToPixelY(obj.Top)-tbw);
            #endregion
            this.BackColor = obj.Border.BackgroundColor;
            this.Tag = this.BackColor;
            this.Text = obj.Text;
            this.Font = obj.Font;
            #region === getLineStyletoPadding ===
            int pT = 0, pL = 0;
            switch (TopLineStyle)
            {   
                case CrystalDecisions.Shared.LineStyle.DoubleLine:
                    pT = 5;
                    break;
                case CrystalDecisions.Shared.LineStyle.NoLine:
                    pT = 0;
                    break;
                default:
                    pT = 1;
                    break;
            }
            switch (LeftLineStyle)
            {
                case CrystalDecisions.Shared.LineStyle.DoubleLine:
                    pL = 5;
                    break;
                case CrystalDecisions.Shared.LineStyle.NoLine:
                    pL = 0;
                    break;
                default:
                    pL = 1;
                    break;
            }
            this.Padding = new Padding(pL, pT, 0, 0);
            #endregion
            //this.textColor = obj.Color;
            
            if (Suppress)
                this.ForeColor = Color.LightGray;
            else this.ForeColor = obj.Color;
            switch (obj.ObjectFormat.HorizontalAlignment)
            {
                case CrystalDecisions.Shared.Alignment.Decimal:
                    TextAlign = ContentAlignment.TopRight;
                    break;
                case CrystalDecisions.Shared.Alignment.DefaultAlign:
                    TextAlign = ContentAlignment.TopLeft;
                    break;
                case CrystalDecisions.Shared.Alignment.HorizontalCenterAlign:
                    TextAlign = ContentAlignment.TopCenter;
                    break;
                case CrystalDecisions.Shared.Alignment.Justified:
                    TextAlign = ContentAlignment.TopRight;//xài đỡ
                    break;
                case CrystalDecisions.Shared.Alignment.LeftAlign:
                    TextAlign = ContentAlignment.TopLeft;
                    break;
                case CrystalDecisions.Shared.Alignment.RightAlign:
                    TextAlign = ContentAlignment.TopRight;
                    break;
                default:
                    break;
            }
            
            
            this.ApplyEvent();
        }

        private void ApplyEvent()
        {
            ControlManager.ControlMoverOrResizer.Init(this);
            this.SizeChanged += _SizeChanged;
            this.TextChanged += _TextChanged;
            this.LocationChanged += _LocationChanged;
            this.MouseClick += rptText_MouseClick;
            this.FontChanged += _FontChanged;
            //this.TextColorChanged += rptText_TextColorChanged;
            //this.BackGroundColorChanged += rptField_BackGroundColorChanged;
        }

        

        //void rptText_TextColorChanged(object sender, EventArgs e)
        //{
        //    _obj.Color = textColor;
        //}

        //void rptField_BackGroundColorChanged(object sender, EventArgs e)
        //{
        //    _obj.Border.BackgroundColor = this.BackGroundColor;
        //}

        void _FontChanged(object sender, EventArgs e)
        {
            _obj.ApplyFont(Font);
        }

        void rptText_MouseClick(object sender, MouseEventArgs e)
        {
            FormRptEditor.SetPropertySelectedObject(this, this.Name);
        }

        void _LocationChanged(object sender, EventArgs e)
        {
            if (_obj != null)
            {
                int lbw = 0, rbw = 0, tbw = 0, bbw = 0;
                if (LeftLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) lbw = 0;
                else if (LeftLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) lbw = 5;
                else lbw = 1;

                //if (RightLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) rbw = 0;
                //else if (RightLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) rbw = 5;
                //else rbw = 1;

                if (TopLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) tbw = 0;
                else if (TopLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) tbw = 5;
                else tbw = 1;

                //if (BottomLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) bbw = 0;
                //else if (BottomLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) bbw = 5;
                //else bbw = 1;
                if (this.Top < 0) this.Top = 0;
                if (this.Left < 0) this.Left = 0;
                _obj.Top = Twip.PixelToTwipY(this.Top + tbw);
                _obj.Left = Twip.PixelToTwipX(this.Left + lbw);
            }
        }

        void _TextChanged(object sender, EventArgs e)
        {
            if (FormRptEditor._rpt != null)
            {
                TextObject rptobj = (TextObject) FormRptEditor._rpt.ReportDefinition.ReportObjects[this.Name];
                rptobj.Text = this.Text;
            }
        }

        void _SizeChanged(object sender, EventArgs e)
        {
            if (FormRptEditor._rpt != null)
            {
                int lbw = 0, rbw = 0, tbw = 0, bbw = 0;
                if (LeftLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) lbw = 0;
                else if (LeftLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) lbw = 5;
                else lbw = 1;
                
                if (RightLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) rbw = 0;
                else if (RightLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) rbw = 5;
                else rbw = 1;
                
                if (TopLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) tbw = 0;
                else if (TopLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) tbw = 5;
                else tbw = 1;
                
                if (BottomLineStyle == CrystalDecisions.Shared.LineStyle.NoLine) bbw = 0;
                else if (BottomLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine) bbw = 5;
                else bbw = 1;
                int objW = this.Width - lbw - rbw;
                int objH = this.Height - tbw - bbw;
                _obj.Width = Twip.PixelToTwipX(objW);
                _obj.Height = Twip.PixelToTwipY(objH);
            }
        }

        #region ==== Border ====

        #region ==== BorderColor ====
        [Description("Màu viền")]
        [Category("_crProperty")]
        public Color BorderColor
        {
            get { return _obj.Border.BorderColor; }
            set
            {
                _obj.Border.BorderColor = value;
                Invalidate();
                //OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle(new Point(0,0), this.Size)));
            }
        }
        #endregion

        [Description("Kiểu đường viền trên")]
        [Category("_crProperty")]
        public CrystalDecisions.Shared.LineStyle TopLineStyle
        {
            get { return _obj.Border.TopLineStyle; }
            set
            {
                if (value != CrystalDecisions.Shared.LineStyle.BlankLine
                    && value != CrystalDecisions.Shared.LineStyle.FirstInvalidLineStyle)
                {
                    int oldLineW = 0;
                    if(_obj.Border.TopLineStyle == CrystalDecisions.Shared.LineStyle.NoLine)
                        oldLineW = 0;
                    else if(_obj.Border.TopLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine)
                        oldLineW = 5;
                    else oldLineW = 1;
                    
                    _obj.Border.TopLineStyle = value;
                    //Kiểm tra dịch chuyển vị trí cho phù hợp với border
                    int newLineW = 0;
                    int newPaddingT = Padding.Top;
                    if (_obj.Border.TopLineStyle == CrystalDecisions.Shared.LineStyle.NoLine)
                    { newLineW = 0; newPaddingT = 0; }
                    else if (_obj.Border.TopLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine)
                    { newLineW = 5; newPaddingT = 5; }
                    else
                    { newLineW = 1; newPaddingT = 1; }

                    //Nếu là đường này thì pading =
                    Padding p = new Padding(Padding.Left, newPaddingT, Padding.Right, Padding.Bottom);
                    Padding = p;
                    this.Top -= (newLineW - oldLineW);
                    this.Height += (newLineW - oldLineW);
                    Invalidate();
                }
            }
        }
        [Description("Kiểu đường viền dưới")]
        [Category("_crProperty")]
        public CrystalDecisions.Shared.LineStyle BottomLineStyle
        {
            get { return _obj.Border.BottomLineStyle; }
            set
            {
                if (value != CrystalDecisions.Shared.LineStyle.BlankLine
                    && value != CrystalDecisions.Shared.LineStyle.FirstInvalidLineStyle)
                {
                    int oldLineW = 0;
                    if (_obj.Border.BottomLineStyle == CrystalDecisions.Shared.LineStyle.NoLine)
                        oldLineW = 0;
                    else if (_obj.Border.BottomLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine)
                        oldLineW = 5;
                    else oldLineW = 1;

                    _obj.Border.BottomLineStyle = value;
                    //Kiểm tra dịch chuyển vị trí cho phù hợp với border
                    int newLineW = 0;
                    int newPaddingB = Padding.Top;
                    if (_obj.Border.BottomLineStyle == CrystalDecisions.Shared.LineStyle.NoLine)
                    { newLineW = 0; newPaddingB = 0; }
                    else if (_obj.Border.BottomLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine)
                    { newLineW = 5; newPaddingB = 5; }
                    else
                    { newLineW = 1; newPaddingB = 1; }

                    //Nếu là đường này thì pading =
                    Padding p = new Padding(Padding.Left, Padding.Top, Padding.Right, newPaddingB);
                    Padding = p;
                    //this.Top -= (newLineW - oldLineW);
                    this.Height += (newLineW - oldLineW);
                    Invalidate();
                }
            }
        }
        [Description("Kiểu đường viền trái")]
        [Category("_crProperty")]
        public CrystalDecisions.Shared.LineStyle LeftLineStyle
        {
            get { return _obj.Border.LeftLineStyle; }
            set
            {
                if (value != CrystalDecisions.Shared.LineStyle.BlankLine
                       && value != CrystalDecisions.Shared.LineStyle.FirstInvalidLineStyle)
                {
                    int oldLineW = 0;
                    if (_obj.Border.LeftLineStyle == CrystalDecisions.Shared.LineStyle.NoLine)
                        oldLineW = 0;
                    else if (_obj.Border.LeftLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine)
                        oldLineW = 5;
                    else oldLineW = 1;

                    _obj.Border.LeftLineStyle = value;
                    //Kiểm tra dịch chuyển vị trí cho phù hợp với border
                    int newLineW = 0;
                    int newPaddingL = Padding.Top;
                    if (_obj.Border.LeftLineStyle == CrystalDecisions.Shared.LineStyle.NoLine)
                    { newLineW = 0; newPaddingL = 0; }
                    else if (_obj.Border.LeftLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine)
                    { newLineW = 5; newPaddingL = 5; }
                    else
                    { newLineW = 1; newPaddingL = 1; }

                    //Nếu là đường này thì pading =
                    Padding p = new Padding(newPaddingL, Padding.Top, Padding.Right, Padding.Bottom);
                    Padding = p;
                    this.Left -= (newLineW - oldLineW);
                    this.Width += (newLineW - oldLineW);
                    Invalidate();
                }
            }
        }
        [Description("Kiểu đường viền phải")]
        [Category("_crProperty")]
        public CrystalDecisions.Shared.LineStyle RightLineStyle
        {
            get { return _obj.Border.RightLineStyle; }
            set
            {
                if (value != CrystalDecisions.Shared.LineStyle.BlankLine
                       && value != CrystalDecisions.Shared.LineStyle.FirstInvalidLineStyle)
                {
                    int oldLineW = 0;
                    if (_obj.Border.RightLineStyle == CrystalDecisions.Shared.LineStyle.NoLine)
                        oldLineW = 0;
                    else if (_obj.Border.RightLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine)
                        oldLineW = 5;
                    else oldLineW = 1;

                    _obj.Border.RightLineStyle = value;
                    //Kiểm tra dịch chuyển vị trí cho phù hợp với border
                    int newLineW = 0;
                    int newPaddingR = Padding.Top;
                    if (_obj.Border.RightLineStyle == CrystalDecisions.Shared.LineStyle.NoLine)
                    { newLineW = 0; newPaddingR = 0; }
                    else if (_obj.Border.RightLineStyle == CrystalDecisions.Shared.LineStyle.DoubleLine)
                    { newLineW = 5; newPaddingR = 5; }
                    else
                    { newLineW = 1; newPaddingR = 1; }

                    //Nếu là đường này thì pading =
                    Padding p = new Padding(Padding.Left, Padding.Top, newPaddingR, Padding.Bottom);
                    Padding = p;
                    //this.Left -= (newLineW - oldLineW);
                    this.Width += (newLineW - oldLineW);
                    Invalidate();
                }
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ButtonBorderStyle
                l = ButtonBorderStyle.None,
                t = ButtonBorderStyle.None,
                r = ButtonBorderStyle.None,
                b = ButtonBorderStyle.None;
            bool lD = false, tD = false, rD = false, bD = false;
            switch (LeftLineStyle)
            {   
                case CrystalDecisions.Shared.LineStyle.DashLine:
                    l = ButtonBorderStyle.Dashed;
                    break;
                case CrystalDecisions.Shared.LineStyle.DotLine:
                    l = ButtonBorderStyle.Dotted;
                    break;
                case CrystalDecisions.Shared.LineStyle.DoubleLine:
                    l = ButtonBorderStyle.Solid; lD = true;
                    break;
                case CrystalDecisions.Shared.LineStyle.SingleLine:
                    l = ButtonBorderStyle.Solid;
                    break;
                default:
                    break;
            }
            switch (TopLineStyle)
            {
                case CrystalDecisions.Shared.LineStyle.DashLine:
                    t = ButtonBorderStyle.Dashed;
                    break;
                case CrystalDecisions.Shared.LineStyle.DotLine:
                    t = ButtonBorderStyle.Dotted;
                    break;
                case CrystalDecisions.Shared.LineStyle.DoubleLine:
                    t = ButtonBorderStyle.Solid; tD = true;
                    break;
                case CrystalDecisions.Shared.LineStyle.SingleLine:
                    t = ButtonBorderStyle.Solid;
                    break;
            }
            switch (RightLineStyle)
            {
                case CrystalDecisions.Shared.LineStyle.DashLine:
                    r = ButtonBorderStyle.Dashed;
                    break;
                case CrystalDecisions.Shared.LineStyle.DotLine:
                    r = ButtonBorderStyle.Dotted;
                    break;
                case CrystalDecisions.Shared.LineStyle.DoubleLine:
                    r = ButtonBorderStyle.Solid; rD = true;
                    break;
                case CrystalDecisions.Shared.LineStyle.SingleLine:
                    r = ButtonBorderStyle.Solid;
                    break;
            }
            switch (BottomLineStyle)
            {
                case CrystalDecisions.Shared.LineStyle.DashLine:
                    b = ButtonBorderStyle.Dashed;
                    break;
                case CrystalDecisions.Shared.LineStyle.DotLine:
                    b = ButtonBorderStyle.Dotted;
                    break;
                case CrystalDecisions.Shared.LineStyle.DoubleLine:
                    b = ButtonBorderStyle.Solid; bD = true;
                    break;
                case CrystalDecisions.Shared.LineStyle.SingleLine:
                    b = ButtonBorderStyle.Solid;
                    break;
            }
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                BorderColor, 1, l,
                BorderColor, 1, t,
                BorderColor, 1, r,
                BorderColor, 1, b);
            //If double draw more
            if (lD)
            {
                e.Graphics.DrawLine(new Pen(BorderColor),
                    3+1, 0, 3+1, Height);
            }
            if (tD)
            {
                e.Graphics.DrawLine(new Pen(BorderColor),
                    0, 3+1, Width, 3+1);
            }
            if (rD)
            {
                e.Graphics.DrawLine(new Pen(BorderColor),
                    Width - 5, 0, Width - 5, Height);
            }
            if (bD)
            {
                e.Graphics.DrawLine(new Pen(BorderColor),
                    0, Height - 5, Width, Height - 5);
            }

            if (this == FormRptEditor._classStatic.ObjForPGrid)
            {
                int thick = 1;
                Rectangle smallRectangle = new Rectangle(ClientRectangle.Left + thick, ClientRectangle.Top + thick,
                    ClientRectangle.Width - thick * 2, ClientRectangle.Height - thick * 2);
                ControlPaint.DrawBorder(e.Graphics, smallRectangle, Color.Black, ButtonBorderStyle.Dotted);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // rptText
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.ResumeLayout(false);

        }
    }
}
