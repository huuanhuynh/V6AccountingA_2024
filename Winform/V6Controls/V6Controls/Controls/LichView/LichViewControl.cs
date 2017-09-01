using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;

namespace V6Controls.Controls.LichView
{
    public partial class LichViewControl : V6Control
    {
        public LichViewControl()
        {
            InitializeComponent();
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
            MyInit();
        }

        private void MyInit()
        {
            //ResetSize();
            Click += LichView_Click;
            MouseMove += LichView_MouseMove;
            Resize += LichViewControl_Resize;
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
            MouseLocation = e.Location;
            //Invalidate();
        }

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
                OnClickNextEvent(new LichViewEventArgs{});
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

                if(mouse_on_a_cell) OnClickCellEvent(
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
        protected DateTime CurrentDate { get; set; }
        /// <summary>
        /// Dữ liệu
        /// </summary>
        public IDictionary<int, LichViewCellData> DataSource { get; set; }
        /// <summary>
        /// Màu viền. Không dùng chọn Color.Transparent
        /// </summary>
        public Color BorderColor { get { return _borderColor; } set { _borderColor = value; } }
        private Color _borderColor = Color.Black;

        public Color DetailColor { get { return _detailColor; } set { _detailColor = value; } }
        private Color _detailColor = Color.Orange;

        public Color HoverBackColor { get { return _hoverBackColor; } set { _hoverBackColor = value; } }
        private Color _hoverBackColor = Color.Aqua;

        public Color SatudayColor { get { return _satudayColor; } set { _satudayColor = value; } }
        private Color _satudayColor = Color.Blue;
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

        protected Point MouseLocation { get; set; }
        public int HeaderHeight { get; set; }
        public int FooterHeight { get; set; }

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

        public event Action<LichViewEventArgs> ClickCellEvent;
        protected virtual void OnClickCellEvent(LichViewEventArgs eventArgs)
        {
            var handler = ClickCellEvent;
            if (handler != null) handler(eventArgs);
        }
        #endregion events

        public void SetData(int year, int month, DateTime currentDate, IDictionary<int, LichViewCellData> data)
        {
            Year = year;
            Month = month;
            CurrentDate = currentDate;
            DataSource = data;

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
            if (Year <= 0) Year = 2017;
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
                int col = (int)first_day_of_week - 1;
                if (col == -1) col = 6;
                int row = 0;
                for (int i = 1; i <= day_in_month; i++)
                {
                    int x = basePoint.X + col*col_width;
                    int y = basePoint.Y + row*row_height;
                    Point cellbasepoint = new Point(x, y);
                    DrawCell(cellbasepoint, e, i, col, row);

                    col++;
                    if (col == 7)
                    {
                        col = 0;
                        row++;
                    }
                }
                //DrawToDay
                if (CurrentDate.Year == Year && CurrentDate.Month == Month)
                {
                    if (DataSource.ContainsKey(CurrentDate.Day))
                    {
                        var cellData = DataSource[CurrentDate.Day];
                        var pen = new Pen(Color.Blue, 2);
                        pen.DashStyle = DashStyle.Dash;
                        e.Graphics.DrawRectangle(pen, cellData.Rectangle.X + 2, cellData.Rectangle.Y + 2,
                            cellData.Rectangle.Width - 4, cellData.Rectangle.Height - 4);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DrawBody", ex);
            }
        }

        private int col_width = 10;
        private int row_height = 10;
        private int day_in_month = 31;
        private int week_in_month = 5;
        private DayOfWeek first_day_of_week;
        private DateTime ngay_dau_thang;
        private void DrawCell(Point basePoint, PaintEventArgs e, int day, int col, int row)
        {
            //Draw backColor
            Brush brush = new SolidBrush(_hoverBackColor);
            Rectangle cellRectangle = new Rectangle(basePoint, new Size(col_width, row_height));
            var pen = new Pen(Color.Black, 1);
            e.Graphics.DrawRectangle(pen, cellRectangle);
            //Hover...
            //if (rec.Contains(MouseLocation))
            //{
            //    e.Graphics.FillEllipse(brush, rec);
            //}
            brush = new SolidBrush(col == 5 ? _satudayColor : (col == 6 ? _sundayColor : ForeColor));
            //Draw day
            float emSize = (float)col_width/9;
            System.Drawing.Font dayFont = new Font(Font.FontFamily, emSize);
            e.Graphics.DrawString("" + day, dayFont, brush, basePoint);
            //Draw Data
            //Test
            brush = new SolidBrush(DetailColor);
            //var bottomRightRec = new Rectangle(basePoint.X + 9, basePoint.Y + 9, col_width - 9, row_height - 9);
            //e.Graphics.FillRectangle(brush, bottomRightRec);
            if (DataSource != null && DataSource.ContainsKey(day))
            {
                LichViewCellData cellData = DataSource[day];
                cellData.Rectangle = cellRectangle;
                float oneDetailHeight = (float)row_height/3;
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                RectangleF layoutRec = new RectangleF(basePoint.X + 20, basePoint.Y, col_width - 20, oneDetailHeight);
                e.Graphics.DrawString(cellData.Detail1, Font, brush, layoutRec, format);
                cellData.Detail1Rectangle = layoutRec;
                layoutRec = new RectangleF(basePoint.X, basePoint.Y + oneDetailHeight, col_width, oneDetailHeight);
                e.Graphics.DrawString(cellData.Detail2, Font, brush, layoutRec, format);
                cellData.Detail2Rectangle = layoutRec;
                layoutRec = new RectangleF(basePoint.X, basePoint.Y + oneDetailHeight*2, col_width, oneDetailHeight);
                e.Graphics.DrawString(cellData.Detail3, Font, brush, layoutRec, format);
                cellData.Detail3Rectangle = layoutRec;
            }
        }

        private void DrawFooter(PaintEventArgs e)
        {
            if (FooterHeight <= 0) return;
        }
    }

    public class LichViewEventArgs : EventArgs
    {
        /// <summary>
        /// Dữ liệu của ô được click
        /// </summary>
        public LichViewCellData CellData { get; set; }
        /// <summary>
        /// Vị trí chuột trên control.
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
