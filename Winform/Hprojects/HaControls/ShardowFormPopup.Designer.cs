using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace H_Controls
{
    partial class ShardowFormPopup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lblCloseButton = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 40;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lblCloseButton
            // 
            this.lblCloseButton.BackColor = System.Drawing.Color.Red;
            this.lblCloseButton.ForeColor = Color.Gray;
            this.lblCloseButton.Location = new System.Drawing.Point(237, 9);
            this.lblCloseButton.Name = "lblCloseButton";
            this.lblCloseButton.Size = new System.Drawing.Size(35, 17);
            this.lblCloseButton.TabIndex = 0;
            this.lblCloseButton.Text = "X";
            this.lblCloseButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShardowFormPopup
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lblCloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShardowFormPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.move_window);
            //this.ResumeLayout(false);
        }
               
        
        #endregion

        #region ===== Method =====

        #region ===== MyInit =====
        void MyInitForDesignView()
        {
            //this.SuspendLayout();
            //lblCloseButton = new Label();
            lblCloseButton.BackColor = closeButtonColor;
            lblCloseButton.ForeColor = closeButtonXcolor;
            this.ResumeLayout();
            lblCloseButton.Size = new Size(closeButtonSizeW, closeButtonSizeH);
            lblCloseButton.Location = new Point(this.Width - borderSize - lblCloseButton.Width - 2, this.borderSize + 2);
            lblCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            
            lblCloseButton.MouseEnter += lblCloseButton_MouseEnter;
            lblCloseButton.MouseHover += lblCloseButton_MouseHover;
            lblCloseButton.MouseLeave += lblCloseButton_MouseLeave;
            lblCloseButton.MouseDown += lblCloseButton_MouseDown;
            lblCloseButton.MouseUp += lblCloseButton_MouseUp;
            lblCloseButton.MouseClick += lblCloseButton_MouseClick;
            
        }
        public void MyInit(Control c)
        {
            //Chỉnh lại màu sắc khi run
            lblCloseButton.BackColor = closeButtonColor;
            lblCloseButton.ForeColor = closeButtonXcolor;
            this.ResumeLayout();

            this.StartPosition = FormStartPosition.Manual;
            Point p = c.PointToScreen(new Point(0, c.Height));
            this.Left = p.X;
            this.Top = p.Y;

            if (this.Right > scw) timer1.Start();
            if (this.Bottom > sch) timer2.Start();
            
        }
        #endregion

        #region ===== Move in screen =====
        float i = 1;
        float i2 = 1;
        int scw = Screen.PrimaryScreen.Bounds.Width;
        int sch = Screen.PrimaryScreen.Bounds.Height;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Nếu vẫn lấn phải
            if (this.Right > scw)
            {
                this.Left -= (int)i; //Đưa về trái
                i *= 1.5f;           //Tốc độ tăng dần
            }
            // Nếu quay lại còn 1 pixel
            else if (scw - this.Right == 1)
            {
                this.Left += 1;     //Thì thêm 1
            }
            // Nếu về quá nhiều thì giảm 1 nữa
            else if (this.Right < scw)
            {
                this.Left += (scw - this.Right) / 2;
            }
            // Nếu đã về đủ
            else if (this.Right == scw)
            {
                timer1.Stop();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Bottom > sch)
            {
                this.Top -= (int)i2;
                i2 *= 1.5f;
            }
            else if (sch - this.Bottom == 1)
            {
                this.Top += 1;
            }
            else if (this.Bottom < sch)
            {
                this.Top += (sch - this.Bottom) / 2;
            }
            else if (this.Bottom == sch)
            {
                timer2.Stop();
            }
        }
        #endregion

        #region ===== Mouse Event =====
        void lblCloseButton_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
            Dispose(false);
        }

        void lblCloseButton_MouseUp(object sender, MouseEventArgs e)
        {
            lblCloseButton.BackColor = closeButtonColorHover;
            lblCloseButton.ForeColor = closeButtonXcolorHover;
        }

        void lblCloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            lblCloseButton.BackColor = closeButtonColorDown;
            lblCloseButton.ForeColor = closeButtonXcolorDown;
        }

        void lblCloseButton_MouseLeave(object sender, EventArgs e)
        {
            lblCloseButton.BackColor = closeButtonColor;
            lblCloseButton.ForeColor = closeButtonXcolor;
        }

        void lblCloseButton_MouseHover(object sender, EventArgs e)
        {
            lblCloseButton.BackColor = closeButtonColorHover;
            lblCloseButton.ForeColor = closeButtonXcolorHover;
        }

        void lblCloseButton_MouseEnter(object sender, EventArgs e)
        {
            lblCloseButton.BackColor = closeButtonColorHover;
            lblCloseButton.ForeColor = closeButtonXcolorHover;
        }
        #endregion

        #region ===== Paint =====
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            MyOnPaint(e.Graphics);
        }
        void MyOnPaint(Graphics g)
        {
            SolidBrush borderBrush = new SolidBrush(borderColor);
            Pen borderPen = new Pen(borderBrush);
            Rectangle topRectangle = new Rectangle(0, 0, Width, borderSize);
            Rectangle lefRectangle = new Rectangle(0, 0, borderSize, Height);
            Rectangle rightRectangle = new Rectangle(Width - borderSize, 0, borderSize, Height);
            Rectangle bottomRectangle = new Rectangle(0, Height - borderSize, Width, borderSize);

            g.FillRectangles(borderBrush, new Rectangle[4] { topRectangle, lefRectangle, rightRectangle, bottomRectangle });

        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            lblCloseButton.Location = new Point(this.Width - borderSize - lblCloseButton.Width - 2, this.borderSize + 2);
        }
        #endregion

        #endregion

        #region ===== Properties =====
        int borderSize = 2;
        public int BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; }
        }

        private int closeButtonSizeW = 35;
        [Description("Kích thước nút Đóng [X] Rộng")]
        public int CloseButtonSizeW
        {
            get { return closeButtonSizeW; }
        }
        private int closeButtonSizeH = 17;
        [Description("Kích thước nút Đóng [X] Cao")]
        public int CloseButtonSizeH
        {
            get { return closeButtonSizeH; }
        }

        bool allowCloseButton = true;
        [Description("Chưa xài tới")]
        public bool AllowCloseButton
        {
            get { return allowCloseButton; }
            //set { allowCloseButton = value; }
        }

        [Description("Màu viền của form")]
        Color borderColor = Color.Black;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        [Description("Màu ban đầu của nút đóng")]
        Color closeButtonColor = Color.White;
        public Color CloseButtonColor
        {
            get { return closeButtonColor; }
            set { closeButtonColor = value; }
        }

        [Description("Màu của nút đóng khi đưa chuột ngang qua")]
        Color closeButtonColorHover = Color.Gray;
        public Color CloseButtonColorHover
        {
            get { return closeButtonColorHover; }
            set { closeButtonColorHover = value; }
        }

        [Description("Màu của nút đóng khi nhấn chuột xuống")]
        Color closeButtonColorDown = Color.Black;
        public Color CloseButtonColorDown
        {
            get { return closeButtonColorDown; }
            set { closeButtonColorDown = value; }
        }

        [Description("Màu trực tiếp của nút đóng")]
        public Color CloseButtonXColorFore
        {
            get { return lblCloseButton.ForeColor; }
            set { lblCloseButton.ForeColor = value; }
        }

        [Description("Màu chữ X của nút Đóng khi để bình thường")]
        Color closeButtonXcolor = Color.LightGray;
        public Color CloseButtonXcolor
        {
            get { return closeButtonXcolor; }
            set { closeButtonXcolor = value; }
        }

        [Description("Màu chữ X của nút Đóng khi di chuyen chuot qua")]
        Color closeButtonXcolorHover = Color.White;
        public Color CloseButtonXcolorHover
        {
            get { return closeButtonXcolorHover; }
            set { closeButtonXcolorHover = value; }
        }

        [Description("Màu chữ X của nút Đóng khi nhan chuot xuong")]
        Color closeButtonXcolorDown = Color.Gray;
        public Color CloseButtonXcolorDown
        {
            get { return closeButtonXcolorDown; }
            set { closeButtonXcolorDown = value; }
        }
        #endregion

        #region ===== Variables =====
        private Label lblCloseButton = new Label();
        private Timer timer1;
        private Timer timer2;
        #endregion
    
    }
}