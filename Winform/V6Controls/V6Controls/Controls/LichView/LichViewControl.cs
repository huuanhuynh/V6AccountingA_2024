using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;
using V6Tools;

namespace V6Controls.Controls.LichView
{
    public partial class LichViewControl : V6Control
    {
        public LichViewControl()
        {
            InitializeComponent();
            FocusDate = DateTime.Now;
            Month = FocusDate.Month;
            Year = FocusDate.Year;
            
            MyInit();
        }

        private void MyInit()
        {
            //ResetSize();
            Click += LichView_Click;
            MouseMove += LichView_MouseMove;
            MouseLeave += LichViewControl_MouseLeave;
            Resize += LichViewControl_Resize;
        }

        void LichViewControl_MouseLeave(object sender, EventArgs e)
        {
            HoverCell = null;
        }

        /// <summary>
        /// _isResizing is used as a signal, so this method is not called recusively
        /// this prevents a stack overflow
        /// </summary>
        private bool _isResizing;
        void LichViewControl_Resize(object sender, EventArgs e)
        {
            // only set sizes automatically at runtime
            if (!DesignMode)
            {
                if (!_isResizing)
                {
                    _isResizing = true;
                    //ResetSize();
                    Invalidate();
                    _isResizing = false;
                }
            }
        }

        void LichView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                MouseLocation = e.Location;
                var g = this.CreateGraphics();
                //Kiểm tra xem nút nào đang được trỏ chuột.
                bool haveHoverCell = false;
                if(DataSource != null)
                foreach (KeyValuePair<int, LichViewCellData> item in DataSource)
                {
                    var cellData = item.Value;
                    if (cellData.Rectangle.Contains(MouseLocation))
                    {
                        haveHoverCell = true;
                        cellData.IsHover = true;
                        DrawCell(g, cellData);
                        HoverCell = cellData;
                        //if (can_break) break;
                    }
                    else if (cellData.IsHover)
                    {
                        //Redraw old hover cell
                        var oldCell = cellData;
                        oldCell.IsHover = false;
                        DrawCell(g, oldCell);
                    }
                }
                if (!haveHoverCell) HoverCell = null;
                if (FocusDate.Year == Year && FocusDate.Month == Month)
                {
                    if (DataSource != null)
                    if (DataSource.ContainsKey(FocusDate.Day))
                    {
                        DrawToDay(g, DataSource[FocusDate.Day]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MouseMove", ex);
            }
        }

        [Description("Thay đổi ô rê chuột.")]
        public event Action<LichViewEventArgs> HoverCellChanged;
        protected virtual void OnHoverCellChanged(LichViewEventArgs eventArgs)
        {
            try
            {
                var handler = HoverCellChanged;
                if (handler != null) handler(eventArgs);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".OnHoverCellChanged", ex);
            }
        }
        public LichViewCellData HoverCell
        {
            get
            {
                return _hoverCell;
            }
            private set
            {
                try
                {
                    if (!Equals(value, _hoverCell))
                    {
                        _hoverCell = value;

                        var arg = new LichViewEventArgs()
                        {
                            CellData = _hoverCell,
                            MouseLocation = MouseLocation,
                            IsClickDetail1 = value != null && value.Detail1Rectangle.Contains(MouseLocation),
                            IsClickDetail2 = value != null && value.Detail2Rectangle.Contains(MouseLocation),
                            IsClickDetail3 = value != null && value.Detail3Rectangle.Contains(MouseLocation),
                        };
                        OnHoverCellChanged(arg);
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".SetHoverCell", ex);
                }
            }
        }

        private LichViewCellData _hoverCell = null;

        void LichView_Click(object sender, EventArgs e)
        {
            //Xác định MouseLocation đang ở trên vị trí nào?
            bool mouse_on_next_button = false, mouse_on_previous_button = false;
            //Rectangle NextButtonRectangle = new Rectangle();
            if (NextButtonRectangle.Contains(MouseLocation))
            {
                mouse_on_next_button = true;
                goto XuLy;
            }
            if (PreviousButtonRectangle.Contains(MouseLocation))
            {
                mouse_on_previous_button = true;
                goto XuLy;
            }
            //Xử lý click trên vị trí đó.
            XuLy:
            if (mouse_on_next_button && ShowNextPrevious)
            {
                OnClickNextEvent(new LichViewEventArgs());
            }
            else if (mouse_on_previous_button && ShowNextPrevious)
            {
                OnClickPreviousEvent(new LichViewEventArgs());
            }
            else// if (mouse_on_a_cell)
            {
                bool mouse_on_a_cell = false;
                LichViewCellData clickCellData = null;
                foreach (KeyValuePair<int, LichViewCellData> item in DataSource)
                {
                    if (item.Value.Rectangle.Contains(MouseLocation))
                    {
                        clickCellData = item.Value;
                        mouse_on_a_cell = true;
                        break;
                    }
                }

                if(mouse_on_a_cell) OnClickCellEvent(this,
                    new LichViewEventArgs()
                    {
                        CellData=clickCellData,
                        MouseLocation = MouseLocation,
                        IsClickDetail1 = clickCellData.Detail1Rectangle.Contains(MouseLocation),
                        IsClickDetail2 = clickCellData.Detail2Rectangle.Contains(MouseLocation),
                        IsClickDetail3 = clickCellData.Detail3Rectangle.Contains(MouseLocation),
                    });
            }
        }

        #region ==== Properties ====
        /// <summary>
        /// Ngày hiện tại, có kẻ khung viền.
        /// </summary>
        public DateTime FocusDate;
        
        /// <summary>
        /// Dữ liệu
        /// </summary>
        public IDictionary<int, LichViewCellData> DataSource {
            get { return _dataSource; }
            set
            {
                _dataSource = value ?? new Dictionary<int, LichViewCellData>();
            } }
        protected IDictionary<int, LichViewCellData> _dataSource = new Dictionary<int, LichViewCellData>();
        public IDictionary<string, object> RowData { get; set; } 
        /// <summary>
        /// Màu viền. Không dùng chọn Color.Transparent
        /// </summary>
        [Category("ColorSetting")]
        public Color BorderColor { get { return _borderColor; } set { _borderColor = value; } }
        private Color _borderColor = Color.Black;
        [Category("ColorSetting")]
        public Color DetailColor { get { return _detailColor; } set { _detailColor = value; } }
        private Color _detailColor = Color.Orange;
        [Category("ColorSetting")]
        public Color HoverBackColor { get { return _hoverBackColor; } set { _hoverBackColor = value; } }
        private Color _hoverBackColor = Color.Aqua;
        [Category("ColorSetting")]
        public Color SatudayColor { get { return _satudayColor; } set { _satudayColor = value; } }
        private Color _satudayColor = Color.Blue;
        [Category("ColorSetting")]
        public Color SundayColor { get { return _sundayColor; } set { _sundayColor = value; } }
        private Color _sundayColor = Color.Red;

        /// <summary>
        /// Độ dày của đường viền
        /// </summary>
        [DefaultValue(1)]
        public int BorderWidth { get { return _borderWidth; } set { _borderWidth = value; } }
        protected int _borderWidth = 1;
        [DefaultValue(1)]
        public int Month { get; protected set; }
        [DefaultValue(2017)]
        public int Year { get; protected set; }
        /// <summary>
        /// Ngày 1 của tháng đang hiển thị.
        /// </summary>
        public DateTime ViewDate1 { get { return new DateTime(Year, Month, 1);} }
        /// <summary>
        /// Ngày cuối của tháng đang hiển thị (31 tượng trưng nhưng tháng 2 vẫn là 28 hoặc 29).
        /// </summary>
        public DateTime ViewDate31 { get { return new DateTime(Year, Month,  DateTime.DaysInMonth(Year, Month));} }
        protected Point MouseLocation { get; set; }
        public int HeaderHeight { get; set; }
        public int FooterHeight { get; set; }
        public string FooterText { get; set; }

        protected Rectangle PreviousButtonRectangle
        {
            get { return new Rectangle(BorderWidth+2, BorderWidth+2, col_width - 4, HeaderHeight/2); }
        }

        protected Rectangle NextButtonRectangle
        {
            get { return new Rectangle(6*col_width + 3, BorderWidth+2, col_width - 4, HeaderHeight/2); }
        }

        protected Rectangle HeaderTitleRectangle
        {
            get
            {
                return new Rectangle(BorderWidth + col_width, BorderWidth, Width - 2*col_width - 2*BorderWidth,
                    HeaderHeight/2);
            }
        }

        protected Rectangle FooterRectangle
        {
            get
            {
                return new Rectangle(BorderWidth, Height - BorderWidth - FooterHeight,
                    Width - 2 * BorderWidth, FooterHeight);
            }
        }

        [DefaultValue(true)]
        public bool ShowAmLich
        {
            get { return _showAL; }
            set
            {
                _showAL = value;
                Invalidate();
            }
        }
        protected bool _showAL = true;

        [DefaultValue(false)]
        public bool ShowNextPrevious { get; set; }
        

        #endregion properties

        #region ==== Events ====
        public event Action<LichViewEventArgs> ClickNextEvent;
        protected virtual void OnClickNextEvent(LichViewEventArgs eventArgs)
        {
            var handler = ClickNextEvent;
            if (handler != null) handler(eventArgs);
        }

        public event Action<LichViewEventArgs> ClickPreviousEvent;
        protected virtual void OnClickPreviousEvent(LichViewEventArgs eventArgs)
        {
            var handler = ClickPreviousEvent;
            if (handler != null) handler(eventArgs);
        }

        public event Action<LichViewControl, LichViewEventArgs> ClickCellEvent;
        protected virtual void OnClickCellEvent(LichViewControl sender, LichViewEventArgs eventArgs)
        {
            try
            {
                var handler = ClickCellEvent;
                if (handler != null) handler(sender, eventArgs);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".OnClickCellEvent", ex);
            }
        }
        #endregion events

        /// <summary>
        /// Nạp dữ liệu cho lịch hiển thị
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="focusDate"></param>
        /// <param name="data">Dữ liệu thêm trên lịch theo ngày (trong tháng).</param>
        /// <param name="rowData"></param>
        /// <param name="footerText"></param>
        public void SetData(int year, int month, DateTime focusDate,
            IDictionary<int, LichViewCellData> data,
            IDictionary<string, object> rowData,
            string footerText)
        {
            Year = year;
            Month = month;
            FocusDate = focusDate;
            DataSource = data;
            RowData = rowData;
            FooterText = footerText;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ResetVar();
            //base.OnPaint(e);
            DrawBoder(e);
            DrawHeader(e);
            DrawBody(new Point(BorderWidth, BorderWidth + HeaderHeight), e);
            DrawFooter(e);
        }

        private void DrawBoder(PaintEventArgs e)
        {
            if (_borderColor == Color.Transparent) return;
            Pen p = new Pen(_borderColor, BorderWidth);
            e.Graphics.DrawRectangle(p, 0, 0, Width-1, Height-1);
        }

        #region ==== DrawHeader ====
        private void DrawHeader(PaintEventArgs e)
        {
            //DrawHeaderBackColor();
            DrawHeaderBorder(e);
            
            DrawHeaderTitleAndButtons(e);
            DrawDayOfWeek(e);
        }

        

        public string[] DayOfWeekNames { get { return V6Setting.IsVietnamese ? _dayNamesV : _dayNamesE; }}
        private string[] _dayNamesV = {"Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy", "Chủ nhật"};
        private string[] _dayNamesE = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        private void DrawDayOfWeek(PaintEventArgs e)
        {
            int recHeight = HeaderHeight/2;
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            var brush = new SolidBrush(ForeColor);

            for (int i = 0; i < 7; i++)
            {
                if (i == 5) brush = new SolidBrush(_satudayColor);
                if (i == 6) brush = new SolidBrush(_sundayColor);
                RectangleF layoutRec = new RectangleF(col_width*i, recHeight, col_width, recHeight);
                e.Graphics.DrawString(DayOfWeekNames[i], Font, brush, layoutRec, format);
            }
            
        }

        private void DrawHeaderBorder(PaintEventArgs e)
        {
            
        }

        private void DrawHeaderTitleAndButtons(PaintEventArgs e)
        {
            var brush = new SolidBrush(Color.Blue);
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            
            //Title
            Font titleFont = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);
            string text = V6Setting.IsVietnamese
                ? string.Format("Tháng {0} năm {1}", Month, Year)
                : string.Format("{0}, {1}",CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month), Year);
            e.Graphics.DrawString(text, titleFont, brush, HeaderTitleRectangle, format);

            //Buttons
            if (ShowNextPrevious)
            {
                e.Graphics.FillRectangle(brush, PreviousButtonRectangle);
                e.Graphics.FillRectangle(brush, NextButtonRectangle);

                brush = new SolidBrush(Color.White);
                e.Graphics.DrawString("<", Font, brush, PreviousButtonRectangle, format);
                e.Graphics.DrawString(">", Font, brush, NextButtonRectangle, format);
            }
        }

        

        #endregion ==== DrawHeader ====

        private void ResetVar()
        {
            //Xác định cột cho ngày 1
            if (Month <= 0) Month = 1;
            if (Month > 12) Month = 12;
            if (Year <= 0) Year = 2018;
            day_in_month = DateTime.DaysInMonth(Year, Month);
            ngay_dau_thang = new DateTime(Year, Month, 1);
            
            first_day_of_week = ngay_dau_thang.DayOfWeek;
            int emptyCol = (int)first_day_of_week - 1;
            if (emptyCol == -1) emptyCol = 6;
            int temp_day = day_in_month + emptyCol;
            week_in_month = temp_day/7;
            if (temp_day%7 > 0) week_in_month++;

            col_width = (Width - BorderWidth * 2) / 7;
            row_height = (Height - BorderWidth * 2 - HeaderHeight - FooterHeight) / week_in_month;
        }

        private void DrawBody(Point basePoint, PaintEventArgs e)
        {
            try
            {
                LichViewCellData todayCellData = null;
                int col = (int)first_day_of_week - 1;
                if (col == -1) col = 6;
                int row = 0;
                for (int i = 1; i <= day_in_month; i++)
                {
                    int x = basePoint.X + col*col_width;
                    int y = basePoint.Y + row*row_height;
                    Point cellBasePoint = new Point(x, y);
                    LichViewCellData cellData = DataSource == null || !DataSource.ContainsKey(i) ? null : DataSource[i];
                    if (cellData == null) // Can xem lai !!!!!
                    {
                        cellData = new LichViewCellData(0, new DateTime(Year, Month, i))
                        {
                            Col = col,
                            Row = row,
                            Day = i
                        };
                    }
                    else
                    {
                        cellData.Col = col;
                        cellData.Row = row;
                    }
                    if (i == FocusDate.Day)
                    {
                        todayCellData = cellData;
                    }
                    Rectangle cellRectangle = new Rectangle(cellBasePoint, new Size(col_width, row_height));
                    cellData.Rectangle = cellRectangle;
                    DrawCell(e.Graphics, cellData);

                    col++;
                    if (col == 7)
                    {
                        col = 0;
                        row++;
                    }
                }
                DrawToDay(e.Graphics, todayCellData);

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DrawBody", ex);
            }
        }

        private void DrawToDay(Graphics g, LichViewCellData todayCellData)
        {
            if (todayCellData != null)
            {
                var pen = new Pen(Color.Blue, 1);
                pen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(pen, todayCellData.Rectangle.X + 2, todayCellData.Rectangle.Y + 2,
                    todayCellData.Rectangle.Width - 4, todayCellData.Rectangle.Height - 4);
            }
        }

        private int col_width = 10;
        private int row_height = 10;
        private int day_in_month = 31;
        private int week_in_month = 5;
        private DayOfWeek first_day_of_week;
        private DateTime ngay_dau_thang;
        private void DrawCell(Graphics g, LichViewCellData cellData)//, int day)//, int col, int row)
        {
            var basePoint = cellData.Rectangle.Location;
            //Draw backColor
            Brush brush = new SolidBrush(BackColor);

            var hoverRec = cellData.Rectangle;
            if (cellData.IsHover)
            {
                brush = new SolidBrush(_hoverBackColor);
                Pen detailBorderPen = new Pen(Color.Blue, 1);
                Color tran_color = Color.FromArgb(100, 0, 0, 255);
                Brush detail_brush = new SolidBrush(tran_color);
                g.FillRectangle(brush, hoverRec);
                //Draw detail hover border.
                if (cellData.Detail1Rectangle.Contains(MouseLocation))
                {
                    g.FillRectangle(detail_brush, cellData.Detail1Rectangle);
                }
                else if (cellData.Detail2Rectangle.Contains(MouseLocation))
                {
                    g.FillRectangle(detail_brush, cellData.Detail2Rectangle);
                }
                else if (cellData.Detail3Rectangle.Contains(MouseLocation))
                {
                    g.FillRectangle(detail_brush, cellData.Detail3Rectangle);
                }
            }
            else
            {
                g.FillRectangle(brush, hoverRec);
            }

            var pen = new Pen(Color.Black, 1);
            g.DrawRectangle(pen, cellData.Rectangle);// cellRectangle);
            
            brush = new SolidBrush(cellData.Col == 5 ? _satudayColor : (cellData.Col == 6 ? _sundayColor : ForeColor));
            //Draw day
            float emSize = (float)col_width/9;
            if (emSize < 5.5) emSize = 5.5f;
            Font dayFont = new Font(Font.FontFamily, emSize);
            g.DrawString("" + cellData.Day, dayFont, brush, cellData.Rectangle);// basePoint);
            //Draw LunarDate
            if (_showAL)
            {
                var size = g.MeasureString("" + cellData.Day, dayFont);
                LunarDate lunarDate = new LunarDate(cellData.Date);
                //In đỏ ngày tết
                if(lunarDate.LunarMonth == 1)
                {
                    if (lunarDate.LunarDay == 1 || lunarDate.LunarDay == 3 || lunarDate.LunarDay == 3)
                    {
                        brush = new SolidBrush(Color.Red);
                    }
                }
                Rectangle lunarRec = new Rectangle(cellData.Rectangle.X,
                    (int) (cellData.Rectangle.Y + Math.Ceiling(size.Height)),
                    Width, (int) (Height - size.Height));
                emSize = (float) col_width/15;
                if (emSize < 4.5) emSize = 4.5f;
                Font lunarFont = new Font(Font.FontFamily, emSize);
                var lunarDay = "" + lunarDate.LunarDay;
                if (cellData.Date.Day == 1 || lunarDate.LunarDay == 1)
                {
                    lunarDay += "/" + lunarDate.LunarMonth;
                    if (lunarDate.IsLeap)
                    {
                        lunarDay += "N";
                    }
                }
                g.DrawString(lunarDay, lunarFont, brush, lunarRec);
            }

            //Draw Data
            //Test
            brush = new SolidBrush(DetailColor);
            
            //if (DataSource != null && DataSource.ContainsKey(day)) // Nếu có cellData
            if (cellData.Key != 0)
            {
                //LichViewCellData cellData = DataSource[day];
                //cellData.Rectangle = cellRectangle;
                float oneDetailHeight = (float)row_height/3;
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                RectangleF layoutRec = new RectangleF(basePoint.X + 20, basePoint.Y, col_width - 20, oneDetailHeight);
                g.DrawString(cellData.Detail1, Font, brush, layoutRec, format);
                cellData.Detail1Rectangle = layoutRec;
                layoutRec = new RectangleF(basePoint.X, basePoint.Y + oneDetailHeight, col_width, oneDetailHeight);
                g.DrawString(cellData.Detail2, Font, new SolidBrush(cellData.Detail2Color), layoutRec, format);
                cellData.Detail2Rectangle = layoutRec;
                layoutRec = new RectangleF(basePoint.X, basePoint.Y + oneDetailHeight*2, col_width, oneDetailHeight);
                g.DrawString(cellData.Detail3, Font, brush, layoutRec, format);
                cellData.Detail3Rectangle = layoutRec;
            }
        }

        private void DrawFooter(PaintEventArgs e)
        {
            if (FooterHeight <= 0) return;
            if (FooterText != null)
            {
                Font titleFont = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);
                Brush brush = new SolidBrush(Color.Blue);
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(FooterText, titleFont, brush, FooterRectangle, format);
            }
        }
    }

    public class LichViewEventArgs : EventArgs
    {
        /// <summary>
        /// Dữ liệu của ô được click
        /// </summary>
        public LichViewCellData CellData { get; set; }
        /// <summary>
        /// Vị trí chuột trên control lichView.
        /// </summary>
        public Point MouseLocation { get; set; }
        /// <summary>
        /// Có phải đã bấm lên Detail1 hay không.
        /// </summary>
        public bool IsClickDetail1 { get; set; }
        /// <summary>
        /// Có phải đã bấm lên Detail2 hay không.
        /// </summary>
        public bool IsClickDetail2 { get; set; }
        /// <summary>
        /// Có phải đã bấm lên Detail3 hay không.
        /// </summary>
        public bool IsClickDetail3 { get; set; }
    }

    
}
