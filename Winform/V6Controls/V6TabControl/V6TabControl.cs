using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
namespace V6TabControlLib
{
    /// <summary>
    /// Summary description for UserControl1.
    /// </summary>
    public class V6TabControl : System.Windows.Forms.TabControl
    {        

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        //public delegate void OnHeaderCloseDelegate(object sender,CloseEventArgs e);
        public delegate void OnHeaderCloseDelegate(object sender, CloseEventArgs e);
        public delegate void OnAddClickDelegate(V6TabPage v6Tabpage, AddEventArgs a);

        
        public event OnHeaderCloseDelegate OnClose;
        public event OnAddClickDelegate OnAddClick;

        private RectangleF addButtonArea;
        private RectangleF closeButtonArea;

        private bool allowCloseButton = true;
        private bool allowAddButton = true;
        private bool allowInActiveAddButton = false;

        #region ==== Color Properties - not use yet ====
        // Dùng để tùy chỉnh màu sắc khi tạo control.
        private Color
            clrBackInActive1 = Color.Black,
            clrBackInActive2,
            clrBackInActive3,

            clrCloseInActive1,
            clrCloseInActive2,
            clrCloseInActive3,
            clrCloseInActiveS,

            clrAddInActive1,
            clrAddInActive2,
            clrAddInActive3,
            clrAddInActiveS,

            clrBackActive1 = Color.Black,
            clrBackActive2,
            clrBackActive3,

            clrCloseActive1,
            clrCloseActive2,
            clrCloseActive3,
            clrCloseActiveS,

            clrAddActive1,
            clrAddActive2,
            clrAddActive3,
            clrAddActiveS,

            clrBackMouseHover1 = Color.Black,
            clrBackMouseHover2,
            clrBackMouseHover3,

            clrCloseMouseHover1,
            clrCloseMouseHover2,
            clrCloseMouseHover3,
            clrCloseMouseHoverS,

            clrAddMouseHover1,
            clrAddMouseHover2,
            clrAddMouseHover3,
            clrAddMouseHoverS
            ;
        
        #endregion ==== Color Properties ====

        #region ==== Bool Properties ====
        [Description("Cho hiện nút thêm trên cả các thẻ không hoạt động.")]
        [DefaultValue(false)]
        public bool AllowInActiveAddButton
        {
            get { return allowInActiveAddButton; }
            set { allowInActiveAddButton = value; }
        }
        private bool confirmOnClose = true;

        [Description("Xác nhận trước khi đóng thẻ!")]
        [DefaultValue(true)]
        public bool ConfirmOnClose
        {
            get{return this.confirmOnClose;}
            set{this.confirmOnClose = value;}
        }
        [Description("Cho hiện nút close trên mỗi thẻ.")]
        [DefaultValue(true)]
        public bool AllowCloseButton
        {
            get { return allowCloseButton; }
            set { allowCloseButton = value; }
        }
        [Description("Cho hiện nút add trên thẻ.")]
        [DefaultValue(true)]
        public bool AllowAddButton
        {
            get { return allowAddButton; }
            set { allowAddButton = value; }
        }
        #endregion ==== Bool Properties ====

        public V6TabControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            this.TabStop = false;
            this.OnClose += new OnHeaderCloseDelegate(V6TabControl_OnClose);
            this.OnAddClick += new OnAddClickDelegate(V6TabControl_OnAddClick);
            // TODO: Add any initialization after the InitComponent call

        }

        void V6TabControl_OnAddClick(V6TabPage v6Tabpage, AddEventArgs a)
        {
            v6Tabpage.Text = "NewTab " + this.TabCount;
            this.TabPages.Add(v6Tabpage);
            this.SelectedTab = v6Tabpage;
        }

        void V6TabControl_OnClose(object sender, CloseEventArgs e)
        {
            this.Controls.Remove(this.TabPages[e.TabIndex]);
        }
        
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            //SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            this.TabStop = false;
            this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            //this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemSize = new System.Drawing.Size(230, 24);
            //this.Controls.Add(this.btnClose); 
        }
        #endregion
        private Stream GetContentFromResource(string filename)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream("MyControlLibrary." + filename);
            return stream;

        }

        private RectangleF GetAddButtonArea(RectangleF tabTextArea)
        {
            return new RectangleF(tabTextArea.Right - 45, tabTextArea.Top + 2, tabTextArea.Height - 4, tabTextArea.Height - 4);
        }
        private RectangleF GetCloseButtonArea(RectangleF tabTextArea)
        {   
            return new RectangleF(tabTextArea.Right - 23, tabTextArea.Top + 2, tabTextArea.Height - 4, tabTextArea.Height - 4);
        }

        private void PaintX_Mark(Color color, int weight, Graphics graphics, RectangleF closeButtonArea)
        {
            using (Pen pen = new Pen(color, weight))
            {
                graphics.DrawLine(pen, closeButtonArea.Left + 5, closeButtonArea.Top + 5, closeButtonArea.Right - 5, closeButtonArea.Bottom - 5);
                graphics.DrawLine(pen, closeButtonArea.Right - 5, closeButtonArea.Top + 5, closeButtonArea.Left + 5, closeButtonArea.Bottom - 5);
            }
        }
        private void PaintAddMark(Color color, int weight, Graphics graphics, RectangleF addButtonArea)
        {
            using (Pen pen = new Pen(color, weight))
            {
                graphics.DrawLine(pen, addButtonArea.Left + 5, (float)(addButtonArea.Top + addButtonArea.Bottom) / 2, addButtonArea.Right - 5, (float)(addButtonArea.Top + addButtonArea.Bottom) / 2);
                graphics.DrawLine(pen, (float)(addButtonArea.Left + addButtonArea.Right) / 2, addButtonArea.Top + 5, (float)(addButtonArea.Left + addButtonArea.Right) / 2, addButtonArea.Bottom - 5);
            }
        }

        /// <summary>
        /// Vẽ mấy thứ trên thẻ Tab
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Bounds != RectangleF.Empty)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                RectangleF tabTextArea = RectangleF.Empty;

                for (int nIndex = 0; nIndex < this.TabCount; nIndex++)
                {
                    //inActive tab
                    if (nIndex != this.SelectedIndex)
                    {
                        #region === InActive Tab ====
                        tabTextArea = (RectangleF)this.GetTabRect(nIndex);
                        GraphicsPath _Path = new GraphicsPath();
                        _Path.AddRectangle(tabTextArea);
                        using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                        {
                            ColorBlend _ColorBlend = new ColorBlend(3);
                            _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                      Color.FromArgb(255,SystemColors.ControlLight),SystemColors.ControlDark,
                                                      SystemColors.ControlLightLight};

                            _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            _Brush.InterpolationColors = _ColorBlend;

                            e.Graphics.FillPath(_Brush, _Path);//vẽ màu nền
                            using (Pen pen = new Pen(SystemColors.ActiveBorder))
                            {
                                e.Graphics.DrawPath(pen, _Path);//Ve khung
                            }
                            #region ==== CloseButton ====
                            if (allowCloseButton)
                            {
                                //Tạo mẫu cọ InActive
                                _ColorBlend.Colors = new Color[]{  SystemColors.ActiveBorder, 
                                                        SystemColors.ActiveBorder,SystemColors.ActiveBorder,
                                                        SystemColors.ActiveBorder};

                                _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                                _Brush.InterpolationColors = _ColorBlend;
                                //Vẽ nút [x]
                                DrawCloseButton(e.Graphics, tabTextArea, _Brush);
                            }
                            #endregion ==== CloseButton ====
                            #region ==== AddButton ====
                            /*
                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓█████             █████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                          */
                            //if (nIndex == (this.TabCount - 1))//Nếu đã đến tab cuối cùng.
                            if (allowCloseButton && allowAddButton && allowInActiveAddButton)//Nếu đến tab đang mở (ActiveTab)
                            {
                                //ColorBlend _ColorBlend = new ColorBlend(3);
                                _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                      Color.FromArgb(255,SystemColors.ControlLight),SystemColors.ControlDark,
                                                      SystemColors.ControlLightLight};

                                _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                                _Brush.InterpolationColors = _ColorBlend;
                                DrawAddButton(e.Graphics, tabTextArea, _Brush);
                            }
                            #endregion ==== AddButton ====
                            #region ==== MenuButton - đã bỏ ====
                            //if (CanDrawMenuButton(nIndex))
                            //{
                            // _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            // _Brush.InterpolationColors = _ColorBlend;
                            // _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            // // assign the color blend to the pathgradientbrush
                            // _Brush.InterpolationColors = _ColorBlend;

                            //  e.Graphics.FillRectangle(_Brush, tabTextArea.X + tabTextArea.Width - 43, 4, tabTextArea.Height - 3, tabTextArea.Height - 5);
                            //// e.Graphics.DrawRectangle(SystemPens.GradientInactiveCaption, tabTextArea.X + tabTextArea.Width - 37, 7, 13, 13);
                            // e.Graphics.DrawRectangle(new Pen(Color.White), tabTextArea.X + tabTextArea.Width - 41, 6, tabTextArea.Height - 7, tabTextArea.Height - 9);
                            // using (Pen pen = new Pen(Color.White, 2))
                            // {
                            //     e.Graphics.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 36, 11, tabTextArea.X + tabTextArea.Width - 33, 16);
                            //     e.Graphics.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 33, 16, tabTextArea.X + tabTextArea.Width - 30, 11);
                            // }
                            //}
                            #endregion ==== MenuButton ====
                        }
                        
                        _Path.Dispose();
                        #endregion === InActive Tab ====
                    }
                    else//Active tab
                    {
                        #region ==== Active Tab ====
                        tabTextArea = (RectangleF)this.GetTabRect(nIndex);
                        GraphicsPath _Path = new GraphicsPath();
                        _Path.AddRectangle(tabTextArea);
                        using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                        {
                            ColorBlend _ColorBlend = new ColorBlend(3);
                            //_ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                            //                            Color.FromArgb(255,SystemColors.Control),SystemColors.ControlLight,
                            //                            SystemColors.Control};
                            //  Bạc Modify => Giống cục vàng 9999
                            _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                      Color.Yellow ,Color.Orange,
                                                      SystemColors.Control};
                            _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };//
                            _Brush.InterpolationColors = _ColorBlend;                 //Gán màu cho cây cọ
                            e.Graphics.FillPath(_Brush, _Path);                       //Vẽ lên activeTab
                            using (Pen pen = new Pen(SystemColors.ActiveBorder))
                            {
                                e.Graphics.DrawPath(pen, _Path);                      //Vẽ khung chữ nhật trên nút Tab
                            }
                            #region ==== CloseButton ====
                            if (allowCloseButton)
                            {
                                //Drawing Close Button//Vẽ nút X, tạo màu
                                _ColorBlend.Colors = new Color[]{Color.FromArgb(255,231,164,152), 
                                                      Color.FromArgb(255,231,164,152),Color.FromArgb(255,197,98,79),
                                                      Color.FromArgb(255,197,98,79)};
                                _Brush.InterpolationColors = _ColorBlend;

                                DrawCloseButton(e.Graphics, tabTextArea, _Brush);
                            }
                            #endregion ==== CloseButton ====
                            #region ==== AddButton ====
                            /*
                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓█████             █████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                          */
                            //if (nIndex == (this.TabCount - 1))//Nếu đã đến tab cuối cùng.
                            if (allowCloseButton && allowAddButton)//
                            {
                                //ColorBlend _ColorBlend = new ColorBlend(3);
                                _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                  Color.FromArgb(255,Color.LightBlue),Color.Green,
                                                  Color.DarkGreen};

                                _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                                _Brush.InterpolationColors = _ColorBlend;
                                DrawAddButton(e.Graphics, tabTextArea, _Brush);
                            }
                            #endregion ==== AddButton ====
                            #region ==== menuButton không còn sử dụng ====
                            //if (CanDrawMenuButton(nIndex))//không sử dụng, return false
                            //{
                            //    //Drawing menu button//phần này đã không còn sử dụng
                            //    _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                            //                           Color.FromArgb(255,SystemColors.ControlLight),SystemColors.ControlDark,
                            //                           SystemColors.ControlLightLight};
                            //    _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            //    _Brush.InterpolationColors = _ColorBlend;
                            //    _ColorBlend.Colors = new Color[]{Color.FromArgb(255,170,213,243), 
                            //                            Color.FromArgb(255,170,213,243),Color.FromArgb(255,44,137,191),
                            //                            Color.FromArgb(255,44,137,191)};
                            //    _Brush.InterpolationColors = _ColorBlend;
                            //    e.Graphics.FillRectangle(_Brush, tabTextArea.X + tabTextArea.Width - 43, 4, tabTextArea.Height - 3, tabTextArea.Height - 5);
                            //    e.Graphics.DrawRectangle(Pens.White, tabTextArea.X + tabTextArea.Width - 41, 6, tabTextArea.Height - 7, tabTextArea.Height - 9);
                            //    using (Pen pen = new Pen(Color.White, 2))
                            //    {
                            //        e.Graphics.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 36, 11, tabTextArea.X + tabTextArea.Width - 33, 16);
                            //        e.Graphics.DrawLine(pen, tabTextArea.X + tabTextArea.Width - 33, 16, tabTextArea.X + tabTextArea.Width - 30, 11);
                            //    }
                            //}
                            #endregion ==== menuButton không còn sử dụng ====
                        }
                        _Path.Dispose();
                        #endregion ==== Active Tab ====
                    }
                    //Tab HeaderText
                    DrawTabHearderText(e.Graphics, tabTextArea, nIndex);
                }
            }
        }

        private void DrawTabHearderText(Graphics graphics, RectangleF tabTextArea, int nIndex)
        {
            string str = this.TabPages[nIndex].Text;
            StringFormat stringFormat = new StringFormat();
            //stringFormat.Alignment = StringAlignment.Center;//Old
            stringFormat.Alignment = StringAlignment.Near;
            tabTextArea.X += 5.0f;// Đưa chữ qua phải một chút
            tabTextArea.Y += 5.0f;// Đưa chữ xuống dưới một chút
            graphics.DrawString(str, this.Font, new SolidBrush(this.TabPages[nIndex].ForeColor), tabTextArea, stringFormat);
            //tabTextArea.X -= 5.0f;// Đưa về vị trí củ
            //tabTextArea.Y -= 5.0f;// Đưa về vị trí củ
            //return tabTextArea;
        }

        private void DrawCloseButton(Graphics graphics, RectangleF tabTextArea, LinearGradientBrush _Brush)
        {
            //Lấy vùng vẽ cho closeButton
            closeButtonArea = GetCloseButtonArea(tabTextArea);
            //Vẽ một hình chữ nhật ■ ở cuối thẻ tab
            graphics.FillRectangle(_Brush, closeButtonArea);
            //Vẽ một đường chữ nhật □ màu trắng nhỏ hơn ■ đè lên hình trên => ◙
            graphics.DrawRectangle(Pens.White, closeButtonArea.Left + 2, closeButtonArea.Top + 2, closeButtonArea.Width - 4, closeButtonArea.Height - 4);
            /*(Sau khi vẽ hai hình chữ nhật)
            ███████████████████████████
            ██                       ██
            ██  ███████████████████  ██
            ██  ███████████████████  ██
            ██  ███████████████████  ██
            ██  ███████████████████  ██
            ██  ███████████████████  ██
            ██  ███████████████████  ██
            ██  ███████████████████  ██
            ██                       ██
            ███████████████████████████
             */
            //Vẽ chữ X màu trắng, độ dày 2
            PaintX_Mark(Color.White, 2, graphics, closeButtonArea);
            /*(Sau khi vẽ dấu X)
            ███████████████████████████
            ██                       ██
            ██  ███████████████████  ██
            ██  ██   █████████   ██  ██
            ██  █████   ███   █████  ██
            ██  ████████   ████████  ██
            ██  █████   ███   █████  ██
            ██  ██   █████████   ██  ██
            ██  ███████████████████  ██
            ██                       ██
            ███████████████████████████
            */            
        }

        private void DrawAddButton(Graphics graphics, RectangleF tabTextArea, LinearGradientBrush _Brush)
        {
            DrawAddButton(graphics, tabTextArea, _Brush, Color.White);
        }
        private void DrawAddButton(Graphics graphics, RectangleF tabTextArea, LinearGradientBrush _Brush, Color x_color)
        {
            //Tạo vùng vẽ
            addButtonArea = GetAddButtonArea(tabTextArea);
            GraphicsPath _Path = new GraphicsPath();
            _Path.AddRectangle(addButtonArea);
            
            

            graphics.FillPath(_Brush, _Path);//vẽ màu nền
            using (Pen pen = new Pen(SystemColors.ActiveBorder))
            {
                graphics.DrawPath(pen, _Path);
            }
            //Vẽ dấu + màu trắng, độ dày 2
            PaintAddMark(x_color, 2, graphics, addButtonArea);
            
        }
        
        private bool CanDrawMenuButton(int nIndex)//Vô hiệu hóa, return false
        {
            //if(((TabPageEx)this.TabPages[nIndex]).Menu != null)
            //  return true;
            return false;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF tabTextArea = RectangleF.Empty;
            for (int nIndex = 0; nIndex < this.TabCount; nIndex++)
            {
                if (nIndex != this.SelectedIndex)//InActive
                {
                    tabTextArea = (RectangleF)this.GetTabRect(nIndex);
                    GraphicsPath _Path = new GraphicsPath();
                    _Path.AddRectangle(tabTextArea);
                    using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                    {
                        ColorBlend _ColorBlend = new ColorBlend(3);
                        #region ==== CloseButton ====
                        if (allowCloseButton)
                        {
                            _ColorBlend.Colors = new Color[]{  SystemColors.ActiveBorder, 
                                                        SystemColors.ActiveBorder,SystemColors.ActiveBorder,
                                                        SystemColors.ActiveBorder};

                            _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            _Brush.InterpolationColors = _ColorBlend;

                            DrawCloseButton(g, tabTextArea, _Brush);
                        }
                        #endregion ==== CloseButton ====
                        #region ==== Add Button ====
                        if (allowCloseButton && allowAddButton && allowInActiveAddButton)
                        {
                            _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                      Color.FromArgb(255,SystemColors.ControlLight),SystemColors.ControlDark,
                                                      SystemColors.ControlLightLight};

                            _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            _Brush.InterpolationColors = _ColorBlend;
                            DrawAddButton(g, tabTextArea, _Brush);
                        }
                        #endregion ==== Add Button ====
                    }
                    #region ========= Thêm phần vẽ nút thêm [+] ========== InActive
                    ///*
                    //      ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                    //      ▓▓███████████████████████▓▓
                    //      ▓▓███████████████████████▓▓
                    //      ▓▓██████████   ██████████▓▓
                    //      ▓▓██████████   ██████████▓▓
                    //      ▓▓█████             █████▓▓
                    //      ▓▓██████████   ██████████▓▓
                    //      ▓▓██████████   ██████████▓▓
                    //      ▓▓███████████████████████▓▓
                    //      ▓▓███████████████████████▓▓
                    //      ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                    //      */
                    ////if (nIndex == (this.TabCount - 1))//Nếu đã đến tab cuối cùng.
                    //if (allowAddButton && nIndex == this.SelectedIndex)//Nếu đến tab đang mở (ActiveTab)
                    //{
                    //    //Tạo vùng vẽ
                    //    RectangleF addButtonArea = GetAddButtonArea(tabTextArea);
                    //    //RectangleF addButtonArea = new RectangleF(tabTextArea.Right - 50, tabTextArea.Top, 25, tabTextArea.Height);
                    //    _Path = new GraphicsPath();
                    //    _Path.AddRectangle(addButtonArea);
                    //    using (LinearGradientBrush _Brush = new LinearGradientBrush(addButtonArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                    //    {
                    //        ColorBlend _ColorBlend = new ColorBlend(3);
                    //        _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                    //                                  Color.FromArgb(255,SystemColors.ControlLight),SystemColors.ControlDark,
                    //                                  SystemColors.ControlLightLight};

                    //        _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                    //        _Brush.InterpolationColors = _ColorBlend;

                    //        g.FillPath(_Brush, _Path);//vẽ màu nền
                    //        using (Pen pen = new Pen(SystemColors.ActiveBorder))
                    //        {
                    //            g.DrawPath(pen, _Path);
                    //        }
                    //        PaintAddMark(Color.Gray, 2, g, addButtonArea);
                    //    }
                    //}
                    #endregion ========= Thêm phần vẽ nút thêm [+] ==========
                    _Path.Dispose();

                }
                else
                {                                      //Active

                    tabTextArea = (RectangleF)this.GetTabRect(nIndex);
                    GraphicsPath _Path = new GraphicsPath();
                    _Path.AddRectangle(tabTextArea);
                    using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                    {
                        ColorBlend _ColorBlend = new ColorBlend(3);
                        #region ==== CloseButton ====
                        if (allowCloseButton)
                        {
                            _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };

                            _ColorBlend.Colors = new Color[]{Color.FromArgb(255,231,164,152), 
                                                      Color.FromArgb(255,231,164,152),Color.FromArgb(255,197,98,79),
                                                      Color.FromArgb(255,197,98,79)};
                            _Brush.InterpolationColors = _ColorBlend;
                            //Vẽ một hình chữ nhật ■ ở cuối thẻ tab 
                            DrawCloseButton(g, tabTextArea, _Brush);

                        }
                        #endregion ==== CloseButton ====
                        #region ==== Add Button ====
                        if (allowCloseButton && allowAddButton)
                        {
                            _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                      Color.FromArgb(255,Color.LightBlue),Color.Green,
                                                      Color.DarkGreen};

                            _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            _Brush.InterpolationColors = _ColorBlend;
                            DrawAddButton(g, tabTextArea, _Brush);
                        }
                        #endregion ==== Add Button ====
                    }
                    #region ========= Thêm phần vẽ nút thêm [+] ========== Mouseleave Active Tab
                    ///*
                    //      ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                    //      ▓▓███████████████████████▓▓
                    //      ▓▓███████████████████████▓▓
                    //      ▓▓██████████   ██████████▓▓
                    //      ▓▓██████████   ██████████▓▓
                    //      ▓▓█████             █████▓▓
                    //      ▓▓██████████   ██████████▓▓
                    //      ▓▓██████████   ██████████▓▓
                    //      ▓▓███████████████████████▓▓
                    //      ▓▓███████████████████████▓▓
                    //      ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                    //      */
                    ////if (nIndex == (this.TabCount - 1))//Nếu đã đến tab cuối cùng.
                    //if (allowAddButton && nIndex == this.SelectedIndex)//Nếu đến tab đang mở (ActiveTab)
                    //{
                    //    //Tạo vùng vẽ
                    //    RectangleF addButtonArea = GetAddButtonArea(tabTextArea);
                    //    //RectangleF addButtonArea = new RectangleF(tabTextArea.Right - 50, tabTextArea.Top, 25, tabTextArea.Height);
                    //    _Path = new GraphicsPath();
                    //    _Path.AddRectangle(addButtonArea);
                    //    using (LinearGradientBrush _Brush = new LinearGradientBrush(addButtonArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                    //    {
                    //        ColorBlend _ColorBlend = new ColorBlend(3);
                    //        //_ColorBlend.Colors = new Color[]{Color.LightBlue, 
                    //        //                             Color.Green, SystemColors.ControlDark,
                    //        //                             SystemColors.ControlLightLight};
                    //        _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                    //                                  Color.FromArgb(255,Color.LightBlue),Color.Green,
                    //                                  Color.DarkGreen};

                    //        _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                    //        _Brush.InterpolationColors = _ColorBlend;

                    //        g.FillPath(_Brush, _Path);//vẽ màu nền
                    //        using (Pen pen = new Pen(SystemColors.InactiveBorder))
                    //        {
                    //            g.DrawPath(pen, _Path);
                    //        }
                    //        PaintAddMark(Color.White, 2, g, addButtonArea);
                    //    }
                    //}
                    #endregion ========= Thêm phần vẽ nút thêm [+] ==========
                    _Path.Dispose();
                }
            }
            g.Dispose();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

            if (!DesignMode)
            {
                Graphics g = CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                for (int nIndex = 0; nIndex < this.TabCount; nIndex++)
                {
                    RectangleF tabTextArea = (RectangleF)this.GetTabRect(nIndex);
                    GraphicsPath _Path = new GraphicsPath();
                    _Path.AddRectangle(tabTextArea);
                    Point pt = new Point(e.X, e.Y);
                    #region ==== Mouse move on tab ===
                    //if (tabTextArea.Contains(pt))
                    //{
                    //    using (LinearGradientBrush _Brush = new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                    //    {
                    //        ColorBlend _ColorBlend = new ColorBlend(3);
                    //        _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight,
                    //            Color.FromArgb(255,SystemColors.ControlLight),
                    //            //SystemColors.ControlDark,
                    //            Color.Yellow,
                    //            SystemColors.ControlLightLight};

                    //        _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                    //        _Brush.InterpolationColors = _ColorBlend;

                    //        g.FillPath(_Brush, _Path);//vẽ màu nền
                    //        using (Pen pen = new Pen(SystemColors.ActiveBorder))
                    //        {
                    //            g.DrawPath(pen, _Path);//Ve khung
                    //        }
                    //    }
                    //}
                    #endregion ==== Mouse move on tab ===
                    //Lấy vùng cho nút Close
                    //tabTextArea =
                    //    new RectangleF(tabTextArea.X + tabTextArea.Width - 22, 4, tabTextArea.Height - 4,
                    //                   tabTextArea.Height - 5);
                    closeButtonArea = GetCloseButtonArea(tabTextArea);
                    addButtonArea = GetAddButtonArea(tabTextArea);
                    
                    //Nếu vị trí con trỏ nằm trên vùng nút close
                    if(allowCloseButton)
                    if (closeButtonArea.Contains(pt))
                    {
                        using (
                            LinearGradientBrush _Brush =
                                new LinearGradientBrush(tabTextArea, SystemColors.Control,
                                                        SystemColors.ControlLight,
                                                        LinearGradientMode.Vertical))
                        {
                            ColorBlend _ColorBlend = new ColorBlend(3);
                            #region ==== CloseButton ====                            
                            _ColorBlend.Colors = new Color[]
                            {
                                Color.FromArgb(255, 252, 193, 183),
                                Color.FromArgb(255, 252, 193, 183), Color.FromArgb(255, 210, 35, 2),
                                Color.FromArgb(255, 210, 35, 2)
                            };
                            _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            _Brush.InterpolationColors = _ColorBlend;

                            DrawCloseButton(g, tabTextArea, _Brush);                            
                            #endregion ==== CloseButton ====
                        }
                    }
                    else                    
                    {
                        if (nIndex != SelectedIndex)//InActive Close Tab Button Mouse leave
                        {
                            using (LinearGradientBrush _Brush =
                                      new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight,
                                          LinearGradientMode.Vertical))
                            {
                                ColorBlend _ColorBlend = new ColorBlend(3);
                                _ColorBlend.Colors = new Color[]
                                  {
                                      SystemColors.ActiveBorder,
                                      SystemColors.ActiveBorder, SystemColors.ActiveBorder,
                                      SystemColors.ActiveBorder
                                  };
                                _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                                _Brush.InterpolationColors = _ColorBlend;
                                DrawCloseButton(g, tabTextArea, _Brush);                                
                            }
                        }
                        else // Active Close Tab button mouse leave
                        {
                            _Path = new GraphicsPath();
                            _Path.AddRectangle(tabTextArea);
                            using (LinearGradientBrush _Brush =
                                      new LinearGradientBrush(tabTextArea, SystemColors.Control, SystemColors.ControlLight,
                                          LinearGradientMode.Vertical))
                            {
                                ColorBlend _ColorBlend = new ColorBlend(3);
                                _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };

                                _ColorBlend.Colors = new Color[]{Color.FromArgb(255,231,164,152), 
                                                      Color.FromArgb(255,231,164,152),Color.FromArgb(255,197,98,79),
                                                      Color.FromArgb(255,197,98,79)};
                                _Brush.InterpolationColors = _ColorBlend;
                                DrawCloseButton(g, tabTextArea, _Brush);
                            }
                        }
                    }
                    #region ========= Thêm phần vẽ nút thêm [+] ========== MouseMove InActive
                    /*
                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓█████             █████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                          */

                    //Nếu đã đến tab cuối cùng và con trỏ vào
                    //if (nIndex == (this.TabCount - 1))
                    if(allowCloseButton && allowAddButton)
                    if (nIndex == this.SelectedIndex)//Nếu đến tab đang mở (ActiveTab)
                    {                        
                        if (addButtonArea.Contains(pt))
                        {
                            _Path = new GraphicsPath();
                            _Path.AddRectangle(addButtonArea);
                            using (LinearGradientBrush _Brush = new LinearGradientBrush(addButtonArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                            {
                                ColorBlend _ColorBlend = new ColorBlend(3);                                
                                _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                      Color.FromArgb(255,Color.LightBlue),Color.Green,
                                                      Color.LightGreen};

                                _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                                _Brush.InterpolationColors = _ColorBlend;

                                DrawAddButton(g, tabTextArea, _Brush);
                                //g.FillPath(_Brush, _Path);//vẽ màu nền
                                //using (Pen pen = new Pen(Color.Green))
                                //{
                                //    g.DrawPath(pen, _Path);
                                //}
                                //PaintAddMark(Color.White, 2, g, addButtonArea);
                            }
                        }
                        
                        else//Quay về màu bình thường của Add button ActiveTab
                        {
                            _Path = new GraphicsPath();
                            _Path.AddRectangle(addButtonArea);
                            using (LinearGradientBrush _Brush = new LinearGradientBrush(addButtonArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                            {
                                ColorBlend _ColorBlend = new ColorBlend(3);
                                //_ColorBlend.Colors = new Color[]{Color.LightBlue, 
                                //                             Color.Green, SystemColors.ControlDark,
                                //                             SystemColors.ControlLightLight};
                                _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                      Color.FromArgb(255,Color.LightBlue),Color.Green,
                                                      Color.DarkGreen};
                                
                                _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                                _Brush.InterpolationColors = _ColorBlend;
                                DrawAddButton(g, tabTextArea, _Brush);
                                //g.FillPath(_Brush, _Path);//vẽ màu nền
                                //using (Pen pen = new Pen(SystemColors.InactiveBorder))
                                //{
                                //    g.DrawPath(pen, _Path);
                                //}
                                //PaintAddMark(Color.White, 2, g, addButtonArea);
                            }
                        }
                    }
                    else if (allowInActiveAddButton) //InActive Tab Add Button
                    {
                        _Path = new GraphicsPath();
                        _Path.AddRectangle(addButtonArea);
                        using (LinearGradientBrush _Brush = new LinearGradientBrush(addButtonArea, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                        {
                            ColorBlend _ColorBlend = new ColorBlend(3);
                            _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                  Color.FromArgb(255,SystemColors.ControlLight),SystemColors.ControlDark,
                                                  SystemColors.ControlLightLight};

                            _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                            _Brush.InterpolationColors = _ColorBlend;
                            if (addButtonArea.Contains(pt))
                            {
                                //ColorBlend _ColorBlend = new ColorBlend(3);
                                _ColorBlend.Colors = new Color[]{SystemColors.ControlLightLight, 
                                                      Color.FromArgb(255,Color.LightBlue),Color.Green,
                                                      Color.LightGreen};

                                _ColorBlend.Positions = new float[] { 0f, .4f, 0.5f, 1f };
                                _Brush.InterpolationColors = _ColorBlend;
                                DrawAddButton(g, tabTextArea, _Brush, Color.White);
                            }
                            else
                            {
                                DrawAddButton(g, tabTextArea, _Brush, Color.White);
                            }
                        }
                    }
                    #endregion ========= Thêm phần vẽ nút thêm [+] ==========
                    
                }
                g.Dispose();
            }
        }
        int oldActiveTabIndex = 0;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                RectangleF tabTextArea = (RectangleF)this.GetTabRect(SelectedIndex);
                //tabTextArea =
                //    tabTextArea =
                //    new RectangleF(tabTextArea.X + tabTextArea.Width - 22, 4, tabTextArea.Height - 3,
                //                   tabTextArea.Height - 5);
                closeButtonArea = GetCloseButtonArea(tabTextArea);
                addButtonArea = GetAddButtonArea(tabTextArea);
                Point pt = new Point(e.X, e.Y);
                if (closeButtonArea.Contains(pt) && allowCloseButton)
                {
                    if (confirmOnClose)
                    {
                        if (V6Controls.V6ConfirmDialogs.Show(
                                "Bạn đang đóng thẻ " 
                                + this.TabPages[SelectedIndex].Text.TrimEnd()
                                + ". Bạn thật sự muốn đóng phải không?"
                                , "Đóng thẻ"
                                , MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    //Fire Event to Client
                    if (OnClose != null)
                    {
                        OnClose(this, new CloseEventArgs(SelectedIndex));
                    }
                }
                #region ========= Thêm phần vẽ nút thêm [+] ==========
                /*
                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓█████             █████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓██████████   ██████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓███████████████████████▓▓
                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                          */
                if (allowCloseButton && allowAddButton)
                {
                    if (addButtonArea.Contains(pt))
                    {
                        if(allowInActiveAddButton)
                            OnAddClick(new V6TabPage(), new AddEventArgs());
                        else if(oldActiveTabIndex == this.SelectedIndex)
                            OnAddClick(new V6TabPage(), new AddEventArgs());
                    }
                }
                oldActiveTabIndex = this.SelectedIndex;
                #endregion ========= Thêm phần vẽ nút thêm [+] ==========
            }
        }
    }

   public class CloseEventArgs:EventArgs
   {
      private int nTabIndex = -1;
      public CloseEventArgs(int nTabIndex)
      {
         this.nTabIndex = nTabIndex;
      }
      /// <summary>
      /// Get/Set the tab index value where the close button is clicked
      /// </summary>
      public int TabIndex 
      {
         get
         {
            return this.nTabIndex;
         }
         set
         {
            this.nTabIndex = value;
         }
      }
   
   }
    //Bắt chước làm AddEvent
   public class AddEventArgs : EventArgs
   {
   }
}
