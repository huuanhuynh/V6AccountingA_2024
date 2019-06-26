using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls
{
    /// <summary>
    /// Summary description for UserControl1.
    /// </summary>
    public class V6TabControl : TabControl
    {        

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container _components;
        
        #region ==== Color Properties - not use yet ====
        // Dùng để tùy chỉnh màu sắc khi tạo control.
        private Color
            colorBackInActive1 = Color.DimGray,
            colorBackInActive2 = Color.LightGray,
            colorBackInActive3 = Color.LightGray,
            colorBackInActive4 = Color.LightGray,
            colorBoderInActive = Color.LightGray,
            
           
            colorBackActive1 = Color.LightCyan,
            colorBackActive2 = Color.LightSkyBlue,
            colorBackActive3 = Color.LightSkyBlue,
            colorBackActive4 = Color.LightSkyBlue,
            colorBoderActive = Color.LightSkyBlue
            ;
        
        #endregion ==== Color Properties ====
        
        public V6TabControl()
        {
            ControlAdded += V6TabControl_ControlAdded;
            
            InitializeComponent();
        }
        
        void V6TabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is TabPage)
            {
                e.Control.BackColor = Color.FromArgb(246, 243, 226);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_components != null)
                    _components.Dispose();
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
            _components = new System.ComponentModel.Container();
            //SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.ItemSize = new System.Drawing.Size(230, 24);
        }
        #endregion

        

        /// <summary>
        /// Vẽ mấy thứ trên thẻ Tab
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            
            if (e.Bounds != RectangleF.Empty)
            {
                Graphics g = e.Graphics;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                for (int nIndex = 0; nIndex < TabCount; nIndex++)
                {
                    //inActive tab
                    RectangleF recBounds;
                    if (nIndex == SelectedIndex)//Active tab
                    {
                        #region ==== Active Tab ====
                        recBounds = GetTabRect(nIndex);
                        var path = new GraphicsPath();
                        path.AddRectangle(recBounds);
                        using (var brush = new LinearGradientBrush(recBounds, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                        {
                            var colorBlend = new ColorBlend(3)
                            {
                                Colors = new[]
                                {
                                    colorBackActive1, colorBackActive2,
                                    colorBackActive3, colorBackActive4
                                },
                                Positions = new[] { 0f, .4f, 0.5f, 1f }
                            };

                            brush.InterpolationColors = colorBlend;                 //Gán màu cho cây cọ
                            e.Graphics.FillPath(brush, path);                       //Vẽ lên activeTab

                            using (var pen = new Pen(colorBoderActive))
                            {
                                e.Graphics.DrawPath(pen, path);                      //Vẽ khung chữ nhật trên nút Tab
                            }
                        }
                        path.Dispose();

                        //----------------------------
                        // clear bottom lines
                        Pen pen2 = new Pen(colorBackActive4);

                        switch (Alignment)
                        {
                            case TabAlignment.Top:
                                g.DrawLine(pen2, recBounds.Left + 1, recBounds.Bottom,
                                                recBounds.Right - 1, recBounds.Bottom);
                                g.DrawLine(pen2, recBounds.Left + 1, recBounds.Bottom + 1,
                                                recBounds.Right - 1, recBounds.Bottom + 1);
                                break;

                            case TabAlignment.Bottom:
                                g.DrawLine(pen2, recBounds.Left + 1, recBounds.Top,
                                                   recBounds.Right - 1, recBounds.Top);
                                g.DrawLine(pen2, recBounds.Left + 1, recBounds.Top - 1,
                                                   recBounds.Right - 1, recBounds.Top - 1);
                                g.DrawLine(pen2, recBounds.Left + 1, recBounds.Top - 2,
                                                   recBounds.Right - 1, recBounds.Top - 2);
                                break;
                        }
                        pen2.Dispose();

                        //Tab HeaderText
                        DrawTabHearderText(e.Graphics, recBounds, nIndex);
                        #endregion ==== Active Tab ====
                    }
                    else
                    {
                        #region === InActive Tab ====
                        recBounds = GetTabRect(nIndex);
                        var path = new GraphicsPath();
                        path.AddRectangle(recBounds);
                        using (var brush = new LinearGradientBrush(recBounds, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.Vertical))
                        {
                            var colorBlend = new ColorBlend(3)
                            {
                                Colors = new[]
                                {
                                    colorBackInActive1,
                                    colorBackInActive2,
                                    colorBackInActive3,
                                    colorBackInActive4
                                },
                                Positions = new[] {0f, .4f, 0.5f, 1f}
                            };

                            brush.InterpolationColors = colorBlend;

                            e.Graphics.FillPath(brush, path);//vẽ màu nền
                            using (var pen = new Pen(colorBoderInActive))
                            {
                                e.Graphics.DrawPath(pen, path);//Ve khung
                            }
                        }
                        
                        path.Dispose();

                        //Tab HeaderText
                        recBounds.Y += 3;
                        DrawTabHearderText(e.Graphics, recBounds, nIndex);
                        #endregion === InActive Tab ====
                    }
                    
                    //----------------------------
                    // draw border
                    //int nDelta = SystemInformation.Border3DSize.Width;

                    //Rectangle TabArea = this.DisplayRectangle;
                    ////Pen border = new Pen(Color.LightSkyBlue);
                    ////tabArea.Inflate(nDelta, nDelta);
                    ////e.Graphics.DrawRectangle(border, tabArea);
                    ////border.Dispose();
                    ////----------------------------
                    //// draw background to cover flat border areas
                    //if (this.SelectedTab != null)
                    //{
                    //    TabPage tabPage = this.SelectedTab;
                    //    Color color = tabPage.BackColor;
                    //    Pen border = new Pen(colorBackActive4);

                    //    TabArea.Offset(1, 1);
                    //    TabArea.Width -= 2;
                    //    TabArea.Height -= 2;

                    //    e.Graphics.DrawRectangle(border, TabArea);
                    //    TabArea.Width -= 1;
                    //    TabArea.Height -= 1;
                    //    e.Graphics.DrawRectangle(border, TabArea);

                    //    border.Dispose();
                    //}
                }
            }

        }

        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    //base.OnPaintBackground(pevent);
        //    if (DesignMode)
        //    {
        //        //' If this is in the designer let's put a nice gradient on the back
        //        //' By default the tabcontrol has a fixed grey background. Yuck!
        //        var backBrush = new LinearGradientBrush(Bounds, SystemColors.ControlLightLight,
        //            SystemColors.ControlLight, LinearGradientMode.Vertical);
        //        pevent.Graphics.FillRectangle(backBrush, Bounds);
        //        backBrush.Dispose();
        //    }
        //    else{
        //        //' At runtime we want a transparent background.
        //        //' So let's paint the containing control (there has to be one).
        //        InvokePaintBackground(Parent, pevent);
        //        InvokePaint(Parent, pevent);
        //    }
        //}

        private void DrawTabHearderText(Graphics graphics, RectangleF tabTextArea, int nIndex)
        {
            var cTab = TabPages[nIndex];
            var num = ObjectAndString.ObjectToInt(cTab.AccessibleDefaultActionDescription);
            string str = TabPages[nIndex].Text;
            //var stringFormat = new StringFormat {Alignment = StringAlignment.Near};
            //tabTextArea.X += 5.0f;// Đưa chữ qua phải một chút
            //tabTextArea.Y += 5.0f;// Đưa chữ xuống dưới một chút
            //graphics.DrawString(str, Font, new SolidBrush(TabPages[nIndex].ForeColor), tabTextArea, stringFormat);
            graphics.DrawString(str, Font, new SolidBrush(TabPages[nIndex].ForeColor),
                new PointF(tabTextArea.X, tabTextArea.Y+5));
            //tabTextArea.X -= 5.0f;// Đưa về vị trí củ
            //tabTextArea.Y -= 5.0f;// Đưa về vị trí củ
            //return tabTextArea;
            //Vẽ status
            if (V6Setting.ViewMenuStatus && num > 0)
            {
                //RectangleF statusRec = new RectangleF(tabTextArea.Right - 10, tabTextArea.Top + 1, 8, 8);
                //graphics.FillEllipse(Brushes.Red, statusRec);
                PointF[] points =
                {
                    new PointF(tabTextArea.Right - 10, tabTextArea.Top + 1), 
                    new PointF(tabTextArea.Right - 2, tabTextArea.Top + 1), 
                    new PointF(tabTextArea.Right - 2, tabTextArea.Top + 9), 
                };
                graphics.FillPolygon(Brushes.Red, points);
            }
        }

        //private void RePaintBackground(object sender, PaintEventArgs e)
        //{
        //    UserControl parent = sender as UserControl;
        //    if (parent != null)
        //    {
        //        var g = parent.CreateGraphics();
        //        Rectangle lasttabrect = GetTabRect(TabPages.Count - 1);
        //        RectangleF emptyspacerect = new RectangleF(
        //                lasttabrect.X + lasttabrect.Width + Left,
        //                Top + lasttabrect.Y,
        //                Width - (lasttabrect.X + lasttabrect.Width),
        //                lasttabrect.Height);
        //        Brush b = Brushes.BlueViolet; // the color you want
        //        g.FillRectangle(b, emptyspacerect);
        //    }
        //}

    }

}
