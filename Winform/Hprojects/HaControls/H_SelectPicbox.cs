using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace H_Controls
{
    public partial class H_SelectPicbox : UserControl
    {
        public H_SelectPicbox()
        {
            InitializeComponent();
            PicImage = new PictureBox();

            PicImage.Dock = DockStyle.Fill;
            PicImage.SizeMode = PictureBoxSizeMode.StretchImage;
            PicImage.BackColor = Color.Black;
            this.Controls.Add(PicImage);
            PicImage.SendToBack();

            _leftPercent = SelectBoxLeft_ByPercent;
            _rightPercent = SelectBoxRight_ByPercent;
            _topPercent = SelectBoxTop_ByPercent;
            _bottonPercent = SelectBoxBottom_ByPercent;
        }

        public delegate void SelectBoxResize(int left_percent, int right_percent, int top_percent, int bottom_percent);
        public SelectBoxResize SelectBoxResize_Event;

        private PictureBox PicImage;
        private Image X_InnerImage;
        #region ===== Properties =====
        [Description("Ẩn hiện khung chọn")]
        [DefaultValue(true)]
        [Browsable(true)]
        public bool ShowSelectBox 
        {
            get { return lineTop.Visible; }
            set
            {
                lineTop.Visible = lineBottom.Visible = lineLeft.Visible = lineRight.Visible = value;
            }
        
        }
        public Point SelectBoxLocation
        {
            get { return lineLeft.Location; }
            set
            {
                Point oldP = new Point(lineLeft.Location.X, lineLeft.Location.Y);
                Point newP = value;
                //Nếu di chuyển trái
                if (newP.X < oldP.X)
                {
                    SelectBoxLeft = newP.X;
                    SelectBoxRight += newP.X - oldP.X;
                }
                else//nếu di chuyển phải
                {
                    SelectBoxRight += newP.X - oldP.X;
                    SelectBoxLeft = newP.X;
                }
                //Nếu di chuyển lên
                if (newP.Y < oldP.Y)
                {
                    SelectBoxTop = newP.Y;
                    SelectBoxBottom += newP.Y - oldP.Y;
                }
                else
                {
                    SelectBoxBottom += newP.Y - oldP.Y;
                    SelectBoxTop = newP.Y;
                }
            }
        }
        /// <summary>
        /// Độ dày viền khung chọn
        /// </summary>
        [Description("Độ dày viền khung chọn")]
        [Browsable(true)]
        public int SelectBoxBorderWidth
        {
            get { return lineLeft.Width; }
            set
            {
                lineLeft.Width = value;
                lineRight.Width = value;
                lineTop.Height = value;
                lineBottom.Height = value;
                
                lineRight.Height = lineBottom.Bottom - lineTop.Top;
                lineRight.Top = lineTop.Top;

                lineBottom.Width = lineRight.Right - lineLeft.Left;
                lineBottom.Left = lineLeft.Left;
            }
        }

        
        /// <summary>
        /// Tọa độ trái khung chọn
        /// </summary>
        [Description("Tọa độ trái khung chọn")]
        [Browsable(true)]
        public int SelectBoxLeft
        {
            get { return lineLeft.Left; }
            set 
            {
                if (value < 0) value = 0;
                else if (value > this.Width - SelectBoxBorderWidth*2)
                    value = this.Width - SelectBoxBorderWidth*2;

                if (value + SelectBoxBorderWidth > lineRight.Left)
                    this.SelectBoxRight = value + SelectBoxBorderWidth;

                lineLeft.Left = value;

                lineTop.Width = lineRight.Right - value;
                lineTop.Left = value;
                lineBottom.Width = lineTop.Width;
                lineBottom.Left = value;

                
            }
        }
        /// <summary>
        /// Tọa độ trái khung chọn theo phần trăm
        /// </summary>
        [Description("Tọa độ trái khung chọn theo phần trăm")]
        [Browsable(true)]
        public int SelectBoxLeft_ByPercent
        {
            get { return SelectBoxLeft*100/Width; }
            set {
                if (value >= 0 && value <= 100)
                {
                    SelectBoxLeft = Width * value / 100;
                }
            }
        }
        /// <summary>
        /// Bề rộng khung chọn
        /// </summary>
        [Description("Bề rộng khung chọn")]
        [Browsable(true)]
        public int SelectBoxWidth
        {
            get { return SelectBoxRight - SelectBoxLeft; }
            set
            {
                if (value > 2 * SelectBoxBorderWidth)
                {
                    SelectBoxRight = SelectBoxLeft + value;
                }
            }
        }
        /// <summary>
        /// Bề rộng khung chọn theo phần trăm
        /// </summary>
        [Description("Bề rộng khung chọn theo phần trăm")]
        [Browsable(true)]
        public int SelectBoxWidth_ByPercent
        {
            get { return SelectBoxWidth * 100 / Width; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    SelectBoxWidth = Width * value / 100;
                }
            }
        }
        /// <summary>
        /// Chiều cao khung chọn
        /// </summary>
        [Description("Chiều cao khung chọn")]
        [Browsable(true)]
        public int SelectBoxHeight
        {
            get { return SelectBoxBottom - SelectBoxTop; }
            set
            {
                if (value > 2 * SelectBoxBorderWidth)
                {
                    SelectBoxBottom = SelectBoxTop + value;
                }
            }
        }
        /// <summary>
        /// Chiều cao khung chọn theo phần trăm
        /// </summary>
        [Description("Chiều cao khung chọn theo phần trăm")]
        [Browsable(true)]
        public int SelectBoxHeight_ByPercent
        {
            get { return SelectBoxHeight * 100 / Height; }
            set
            {
                SelectBoxHeight = value * Height / 100;
            }
        }
        
        /// <summary>
        /// Tọa độ điểm phải khung chọn
        /// </summary>
        [Description("Tọa độ điểm phải khung chọn")]
        [Browsable(true)]
        public int SelectBoxRight
        {
            get { return lineRight.Right; }
            set
            {
                if (value < SelectBoxBorderWidth * 2) value = SelectBoxBorderWidth * 2;
                else if (value > this.Width)
                    value = this.Width;

                if (value - SelectBoxBorderWidth < lineLeft.Right)
                    this.SelectBoxLeft = value - SelectBoxBorderWidth * 2;

                lineRight.Left = value - SelectBoxBorderWidth;

                lineTop.Width = lineRight.Right - lineLeft.Left;
                lineBottom.Width = lineTop.Width;

                
            }
        }
        /// <summary>
        /// Tọa độ phải khung chọn theo phần trăm
        /// </summary>
        [Description("Tọa độ phải khung chọn theo phần trăm")]
        [Browsable(true)]
        public int SelectBoxRight_ByPercent
        {
            get { return (SelectBoxRight * 100) / Width; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    SelectBoxRight = value * Width / 100;
                }
            }
        }

        /// <summary>
        /// Tọa độ trên của khung chọn
        /// </summary>
        [Description("Tọa độ trên của khung chọn")]
        [Browsable(true)]
        public int SelectBoxTop
        {
            get { return lineTop.Top; }
            set
            {
                if (value < 0) value = 0;
                else if (value > this.Height - SelectBoxBorderWidth*2)
                    value = this.Height - SelectBoxBorderWidth*2;

                if (value + SelectBoxBorderWidth > lineBottom.Top)
                    this.SelectBoxBottom = value + SelectBoxBorderWidth;

                lineTop.Top = value;

                lineLeft.Height = lineBottom.Bottom - value;
                lineLeft.Top = value;
                lineRight.Height = lineLeft.Height;
                lineRight.Top = value;

                
            }
        }
        /// <summary>
        /// Tọa độ trên của khung chọn theo phần trăm
        /// </summary>
        [Description("Tọa độ trên của khung chọn theo phần trăm")]
        [Browsable(true)]
        public int SelectBoxTop_ByPercent
        {
            get { return SelectBoxTop * 100 / Height; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    SelectBoxTop = value * Height / 100;
                }
            }
        }

        /// <summary>
        /// Tọa độ dưới của khung chọn
        /// </summary>
        [Description("Tọa độ dưới của khung chọn")]
        [Browsable(true)]
        public int SelectBoxBottom
        {
            get { return lineBottom.Bottom; }
            set
            {
                if (value < SelectBoxBorderWidth * 2) value = SelectBoxBorderWidth * 2;
                else if (value > this.Height - SelectBoxBorderWidth)
                    value = this.Height;// -SelectBoxBorderWidth;

                if (value - SelectBoxBorderWidth < lineTop.Bottom)
                    this.SelectBoxTop = value - SelectBoxBorderWidth * 2;

                lineBottom.Top = value - SelectBoxBorderWidth;

                lineLeft.Height = lineBottom.Bottom - lineTop.Top;
                lineRight.Height = lineLeft.Height;

                
            }
        }
        /// <summary>
        /// Tọa độ dưới của khung chọn theo phần trăm
        /// </summary>
        [Description("Tọa độ dưới của khung chọn theo phần trăm")]
        [Browsable(true)]
        public int SelectBoxBottom_ByPercent
        {
            get { return SelectBoxBottom * 100 / Height; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    SelectBoxBottom = value * Height / 100;
                }
            }
        }

        /// <summary>
        /// Lấy tọa độ vùng trong khung chọn
        /// </summary>
        [Description("Lấy vùng chọn chỉ bên trong khung chọn")]
        public Rectangle SelectBoxInside
        {
            get
            {
                return new Rectangle(lineLeft.Right, lineTop.Bottom,
                    lineTop.Width - SelectBoxBorderWidth * 2,
                    lineLeft.Height - SelectBoxBorderWidth * 2);
            }
            //set;
        }

        /// <summary>
        /// Lấy tọa độ khung chọn tính cả viền
        /// </summary>
        [Description("Lấy vùng chọn cả phần viền khung chọn")]
        public Rectangle SelectBoxOutside
        {
            get
            {
                return new Rectangle(lineLeft.Left, lineTop.Top,
                   lineTop.Width, lineLeft.Height);
            }
        }

        

        /// <summary>
        /// Phần hình ảnh được chọn bên trong khung chọn
        /// </summary>
        public Bitmap SelectImageInsideSelectBox
        {
            get { return SelectImageClone(SelectBoxInside); }
        }
        /// <summary>
        /// Phần hình ảnh được chọn có cả phần ảnh dưới viền khung chọn
        /// </summary>
        public Bitmap SelectImageOutsideSelectBox
        {
            get { return SelectImageClone(SelectBoxOutside); }
        }
        
        private Bitmap SelectImageClone(Rectangle select)
        {
            if (this.H_PicPreview.Image != null)
            {
                Bitmap b = new Bitmap(this.H_PicPreview.Image);
                return H_CloneImage(b, select);
                //return b.Clone(select, System.Drawing.Imaging.PixelFormat.DontCare);
            }
            else return null;
        }
        private Bitmap H_CloneImage(Bitmap b, Rectangle select)
        {
           // if (b == null) return null;
            Rectangle newRectangle = new Rectangle();
            float scaleH = ((float)b.Width) / this.Width;
            float scaleV = ((float)b.Height) / this.Height;
            newRectangle.X = (int)(select.X * scaleH);
            newRectangle.Y = (int)(select.Y * scaleV);
            newRectangle.Width = (int)(select.Width * scaleH);
            newRectangle.Height = (int)(select.Height * scaleV);

            return b.Clone(newRectangle, System.Drawing.Imaging.PixelFormat.DontCare);
        }

        /// <summary>
        /// Lấy một phần ảnh của ảnh truyền vào
        /// tương đương tỷ lệ của khung chọn tính cả viền
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Bitmap H_CloneImageOutside(Bitmap b)
        {
            return H_CloneImage(b,this.SelectBoxOutside);
        }
        /// <summary>
        /// Lấy một phần hình ảnh của ảnh được truyền vào
        /// tương đương tỷ lệ phần được chọn bên trong khung chọn
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Bitmap H_CloneImage(Bitmap b)
        {
            return H_CloneImage(b, this.SelectBoxInside);
        }

        #endregion


        public PictureBox H_PicPreview
        {
            get {
                return this.pictureBox1;
            }
        }
        public void H_AddPicture(Image img)
        {
            PicImage.Image = img; 
            PicImage.BringToFront();
            X_InnerImage = img;
        }

        public void H_RemovePicture()
        {
            //this.Controls.Remove(PicImage);
            PicImage.SendToBack();
        }
        public Image H_Image
        {
            get
            {
                return X_InnerImage;
            }
            set
            {
                if (PicImage != null)
                {
                    PicImage.Image = value;
                    X_InnerImage = value;
                }
            }
        }
        

        #region ===== Resize SelectBox =====
       

        private Label _movingLine = null;
        private bool _drawselect = false;
        private bool _moveselect = false;
        private Point _oldMovingPoint = new Point();
        private Point _oldMousePos = new Point();
        private Point _oldSelectBoxLocation = new Point();

        private int _oldSelectBoxRight = 0;
        private int _oldSelectBoxBottom = 0;
        //private Point _downPoint = new Point(); 
        //public bool AllowDrawToSelect { get; set; }

       
        private void timer1_Tick(object sender, EventArgs e)
        {
            _SelectBoxResize(Cursor.Position);
        }

        private void _SelectBoxResize(Point newMousePos)
        {
            if (_movingLine != null)
            {
                int newX = _oldMovingPoint.X + (newMousePos.X - _oldMousePos.X);
                int newY = _oldMovingPoint.Y + (newMousePos.Y - _oldMousePos.Y);
                if (_movingLine == lineLeft)
                {
                    //Di chuyển đường trái nằm khoảng giữa 0 và đường phải.
                    //Nếu điểm trái cũ cộng thêm khoảng di chuyển nằm giữa...
                    this.SelectBoxLeft = newX;
                }
                else if (_movingLine == lineRight)
                {
                    //Di chuyển đường phải nằm khoảng giữa đường trái và this width
                    this.SelectBoxRight = newX;
                }
                else if (_movingLine == lineTop)
                {
                    //Di chuyển đường trên nằm khoảng giữa 0 và đường dưới.
                    this.SelectBoxTop = newY;
                }
                else if (_movingLine == lineBottom)
                {
                    //Di chuyển đường dưới nằm khoảng giữa đường trên và this height
                    this.SelectBoxBottom = newY;
                }

                if (SelectBoxResize_Event != null)
                {
                    SelectBoxResize_Event(
                        SelectBoxLeft_ByPercent,
                        SelectBoxRight_ByPercent,
                        SelectBoxTop_ByPercent,
                        SelectBoxBottom_ByPercent);
                }
            }
            else if (_drawselect && ShowSelectBox)
            {
                //int newRight = lineLeft.Left + (newMousePos.X - _oldMousePos.X);
                //int newBottom = lineTop.Top + (newMousePos.Y - _oldMousePos.Y);
                int newRight = _oldSelectBoxRight + (newMousePos.X - _oldMousePos.X);
                int newBottom = _oldSelectBoxBottom + (newMousePos.Y - _oldMousePos.Y);
                
                this.SelectBoxRight = newRight;
                this.SelectBoxBottom = newBottom;

                if (SelectBoxResize_Event != null)
                {
                    SelectBoxResize_Event(
                        SelectBoxLeft_ByPercent,
                        SelectBoxRight_ByPercent,
                        SelectBoxTop_ByPercent,
                        SelectBoxBottom_ByPercent);
                }
            }
            else if (_moveselect && ShowSelectBox)
            {
                //Tính toán newlocation
                Point newL = new Point(
                    _oldSelectBoxLocation.X + newMousePos.X - _oldMousePos.X,
                    _oldSelectBoxLocation.Y + newMousePos.Y - _oldMousePos.Y);
                SelectBoxLocation = newL;

                if (SelectBoxResize_Event != null)
                {
                    SelectBoxResize_Event(
                        SelectBoxLeft_ByPercent,
                        SelectBoxRight_ByPercent,
                        SelectBoxTop_ByPercent,
                        SelectBoxBottom_ByPercent);
                }
            }

            
            //_leftPercent = SelectBoxLeft_ByPercent;
            //_rightPercent = SelectBoxRight_ByPercent;
            //_topPercent = SelectBoxTop_ByPercent;
            //_bottonPercent = SelectBoxBottom_ByPercent;
        }
        private void line_MouseDown(object sender, MouseEventArgs e)
        {
            //Bật resize
            _movingLine = (Label)sender;
            _oldMovingPoint = _movingLine.Location;
            _oldMousePos = Cursor.Position;
            timer1.Start();
            //debugCount++;
        }
        private void line_MouseUp(object sender, MouseEventArgs e)
        {
            //Tắt resize            
            _movingLine = null;
            _drawselect = false;
            timer1.Stop();

            
        }
        //int debugCount = 0;
        private void transparentBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (!_drawselect)
                {
                    //Tắt resize
                    _movingLine = null;
                    _drawselect = true;
                    _moveselect = false;
                    _oldMousePos = Cursor.Position;

                    Cursor.Position = new Point(
                        Cursor.Position.X + SelectBoxBorderWidth * 2,
                        Cursor.Position.Y + SelectBoxBorderWidth * 2);

                    this.SelectBoxLeft = e.X;
                    this.SelectBoxTop = e.Y;
                    this.SelectBoxRight = e.X;
                    this.SelectBoxBottom = e.Y;

                    _oldSelectBoxRight = SelectBoxRight;
                    _oldSelectBoxBottom = SelectBoxBottom;

                    timer1.Start();
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (!_moveselect)
                {
                    //Tắt resize
                    _movingLine = null;
                    _drawselect = false;
                    _moveselect = true;
                    _oldMousePos = Cursor.Position;
                    _oldSelectBoxLocation = new Point(SelectBoxLocation.X, SelectBoxLocation.Y);
                    transpCtrl1.Cursor = Cursors.SizeAll;

                    timer1.Start();
                }
            }
        }
        private void transparentBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_drawselect)
            {
                this.SelectBoxRight = e.X;
                this.SelectBoxBottom = e.Y;
            }
            else if (_moveselect)
            {
                transpCtrl1.Cursor = Cursors.Default;
            }
            {
                _movingLine = null;
                _drawselect = false;
                _moveselect = false;
                timer1.Stop();
            }
        }

        #endregion

        private void H_SelectPicbox_Load(object sender, EventArgs e)
        {
            //pictureBox1.BringToFront();
           //pictureBox1.MouseDown +=pictureBox1_MouseDown;
           //pictureBox1.MouseUp +=pictureBox1_MouseUp;
        }

        //bool _sauto = true;
        //public bool SelectBoxAutoSize
        //{
        //    get { return _sauto; }
        //    set { _sauto = value; }
        //}
        private int _leftPercent, _rightPercent, _topPercent, _bottonPercent;
        private void H_SelectPicbox_SizeChanged(object sender, EventArgs e)
        {
            //SelectBoxLeft_ByPercent = _leftPercent;
            //SelectBoxRight_ByPercent = _rightPercent;
            //SelectBoxTop_ByPercent = _topPercent;
            //SelectBoxBottom_ByPercent = _bottonPercent;
        }

        public void SetSelectBoxSize_ByPercent(int l, int r, int t, int bt)
        {
            this.SelectBoxLeft_ByPercent = l;
            this.SelectBoxRight_ByPercent = r;
            this.SelectBoxTop_ByPercent = t;
            this.SelectBoxBottom_ByPercent = bt;

            if (SelectBoxResize_Event != null)
            {
                SelectBoxResize_Event(
                    SelectBoxLeft_ByPercent,
                    SelectBoxRight_ByPercent,
                    SelectBoxTop_ByPercent,
                    SelectBoxBottom_ByPercent);
            }
        }

        //public void ResetDrawSelectBoxEvent()
        //{
        //    pictureBox1.MouseDown +=pictureBox1_MouseDown;
        //    pictureBox1.MouseUp +=pictureBox1_MouseUp;
        //}
    }

    
}
