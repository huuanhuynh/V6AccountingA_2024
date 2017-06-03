// Copyright 2006 Herre Kuijpers - <herre@xs4all.nl>
//
// This source file(s) may be redistributed, altered and custimized
// by any means PROVIDING the authors name and all copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;

namespace V6Controls
{
    public partial class MenuControl : UserControl
    {
        /// <summary>
        /// the OutlookBarButtons class contains the list of buttons
        /// it manages adding and removing buttons, and updates the Outlookbar control
        /// respectively. Note that this is a class, not a control!
        /// </summary>
        #region OutlookBarButtons list
        public class OutlookBarButtons : CollectionBase
        {
            //protected ArrayList List;
            protected MenuControl parenT;
            public MenuControl Parent
            {
                get { return parenT; }
            }

            internal OutlookBarButtons(MenuControl parent)
            {
                parenT = parent;
            }

            public MenuButton this[int index]
            {
                get { return (MenuButton)List[index]; }
            }

            public void Add(MenuButton item)
            {
                //if (List.Count == 0) Parent.SelectedButton = item;
                List.Add(item);
                item.Parent = Parent;
                Parent.ButtonlistChanged();
            }

            public MenuButton Add(string text, Image image)
            {
                MenuButton b = new MenuButton(parenT)
                {
                    Text = text,
                    Image = image
                };
                Add(b);
                return b;
            }

            public MenuButton Add(string text)
            {
                return Add(text, null);
            }

            //public MenuButton Add()
            //{
            //    return Add();
            //}

            

            public void Remove(MenuButton button)
            {
                List.Remove(button);
                Parent.ButtonlistChanged();
            }

            public int IndexOf(object value)
            {
                return List.IndexOf(value);
            }

            #region handle CollectionBase events
            protected override void OnInsertComplete(int index, object value)
            {
                MenuButton b = (MenuButton)value;
                b.Parent = parenT;
                Parent.ButtonlistChanged();
                base.OnInsertComplete(index, value);
            }

            protected override void OnSetComplete(int index, object oldValue, object newValue)
            {
                MenuButton b = (MenuButton)newValue;
                b.Parent = parenT;
                Parent.ButtonlistChanged();
                base.OnSetComplete(index, oldValue, newValue);
            }

            protected override void OnClearComplete()
            {
                Parent.ButtonlistChanged();
                base.OnClearComplete();
            }
            #endregion handle CollectionBase events
        }
        #endregion OutlookBarButtons list

        #region MenuControl property definitions

        /// <summary>
        /// buttons contains the list of clickable OutlookBarButtons
        /// </summary>
        protected OutlookBarButtons buttons;

        /// <summary>
        /// this variable remembers which button is currently selected
        /// </summary>
        protected MenuButton selectedButton;

        /// <summary>
        /// this variable remembers the button index over which the mouse is moving
        /// </summary>
        protected int hoveringButtonIndex = -1;

        public int ViewStatusMode = 0;

        /// <summary>
        /// property to set the buttonHeigt
        /// default is 30
        /// </summary>
        protected int buttonHeight;
        [Description("Specifies the height of each button on the MenuControl"), Category("Layout")]
        public int ButtonHeight
        {
            get { return buttonHeight; }
            set {
                if (value > 18)
                    buttonHeight = value;
                else
                    buttonHeight = 18;
            }
        }
        
        protected Color gradientButtonDark = Color.FromArgb(178, 193, 140);
        [Description("Dark gradient color of the button"), Category("Appearance")]
        public Color GradientButtonNormalDark
        {
            get { return gradientButtonDark; }
            set { gradientButtonDark = value; }
        }

        protected Color gradientButtonLight = Color.FromArgb(234, 240, 207);
        [Description("Light gradient color of the button"), Category("Appearance")]
        public Color GradientButtonNormalLight
        {
            get { return gradientButtonLight; }
            set { gradientButtonLight = value; }
        }

        protected Color gradientButtonHoverDark = Color .FromArgb(247, 192, 91);
        [Description("Dark gradient color of the button when the mouse is moving over it"), Category("Appearance")]
        public Color GradientButtonHoverDark
        {
            get { return gradientButtonHoverDark; }
            set { gradientButtonHoverDark = value; }
        }

        protected Color gradientButtonHoverLight = Color.FromArgb(255, 255, 220);
        [Description("Light gradient color of the button when the mouse is moving over it"), Category("Appearance")]
        public Color GradientButtonHoverLight
        {
            get { return gradientButtonHoverLight; }
            set { gradientButtonHoverLight = value; }
        }

        protected Color gradientButtonSelectedDark = Color.FromArgb(239, 150, 21);
        [Description("Dark gradient color of the seleced button"), Category("Appearance")]
        public Color GradientButtonSelectedDark
        {
            get { return gradientButtonSelectedDark; }
            set { gradientButtonSelectedDark = value; }
        }

        protected Color gradientButtonSelectedLight = Color.FromArgb(251, 230, 148);
        [Description("Light gradient color of the seleced button"), Category("Appearance")]
        public Color GradientButtonSelectedLight
        {
            get { return gradientButtonSelectedLight; }
            set { gradientButtonSelectedLight = value; }
        }


        /// <summary>
        /// when a button is selected programatically, it must update the control
        /// and repaint the buttons
        /// </summary>
        [Browsable(false)]
        public MenuButton SelectedButton
        {
            get { return selectedButton; }
            set {                
                // assign new selected button
                PaintSelectedButton(selectedButton, value);

                // assign new selected button
                selectedButton = value; 
            }
        }

        /// <summary>
        /// readonly list of buttons
        /// </summary>
        //[Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public OutlookBarButtons Buttons
        {
            get { return buttons; }
        }

        #endregion MenuControl property definitions

        #region MenuControl events


        [Serializable]
        public class ButtonClickEventArgs : MouseEventArgs
        {
            public ButtonClickEventArgs(MenuButton button, MouseEventArgs evt) : base(evt.Button, evt.Clicks, evt.X, evt.Y, evt.Delta)
            {
                SelectedButton = button;
            }
           
            public MenuButton SelectedButton { get; set; }
        }

        public delegate void ButtonClickEventHandler(object sender, ButtonClickEventArgs e);

        public new event ButtonClickEventHandler Click;

        #endregion MenuControl events

        #region MenuControl functions

        public MenuControl()
        {
            InitializeComponent();
            buttons = new OutlookBarButtons(this);
            buttonHeight = 30; // set default to 30
        }

        private void PaintSelectedButton(MenuButton prevButton,MenuButton newButton)
        {
            if (prevButton == newButton)
                return; // no change so return immediately

            // find the indexes of the previous and new button
            var selIdx = buttons.IndexOf(prevButton);
            var valIdx = buttons.IndexOf(newButton);

            // now reset selected button
            // mouse is leaving control, so unhighlight anythign that is highlighted
            Graphics g = Graphics.FromHwnd(Handle);
            if (selIdx >= 0)
                // un-highlight current hovering button
                buttons[selIdx].PaintButton(g, 1, selIdx * (buttonHeight + 1) + 1, false, false);

            if (valIdx >= 0)
                // highlight newly selected button
                buttons[valIdx].PaintButton(g, 1, valIdx * (buttonHeight + 1) + 1, true, false);
            g.Dispose();
        }

        /// <summary>
        /// returns the button given the coordinates relative to the Outlookbar control
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public MenuButton HitTest(int x, int y)
        {
            int index = (y - 1) / (buttonHeight + 1);
            if (index >= 0 && index < buttons.Count)
                return buttons[index];
            else
                return null;
        }

        /// <summary>
        /// this function will setup the control to cope with changes in the buttonlist 
        /// that is, addition and removal of buttons
        /// </summary>
        private void ButtonlistChanged()
        {
            if (!DesignMode) // only set sizes automatically at runtime
                MaximumSize = new Size(0, buttons.Count * (buttonHeight + 1) + 1);

            Invalidate();
        }

        public void ClickButton(MenuButton button)
        {
            foreach (MenuButton bt in buttons)
            {
                if (bt == button)
                {
                    SelectedButton = bt;
                    var bce = new ButtonClickEventArgs(selectedButton, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 0));
                    if (Click != null) // only invoke on left mouse click
                        Click.Invoke(this, bce);
                }
            }
        }
        public void ClickIndex(int index)
        {
            if (index >= 0 && index < Buttons.Count)
            {
                SelectedButton = buttons[index];
                var bce = new ButtonClickEventArgs(selectedButton, new MouseEventArgs(MouseButtons.Left,1,1,1,0) );
                if (Click != null) // only invoke on left mouse click
                    Click.Invoke(this, bce);
            }
        }

        #endregion MenuControl functions

        #region MenuControl control event handlers

        private void OutlookBar_Load(object sender, EventArgs e)
        {
            // initiate the render style flags of the control
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer |
                ControlStyles.Selectable |
                ControlStyles.UserMouse,
                true
                );
        }

        private void OutlookBar_Paint(object sender, PaintEventArgs e)
        {
            int top = 1;
            foreach (MenuButton b in Buttons)
            {
                if (b.Visible)
                {
                    b.PaintButton(e.Graphics, 1, top, b.Equals(selectedButton), false);
                    top += b.Height + 1;
                }
            }
        }

        private void OutlookBar_Click(object sender, EventArgs e)
        {
            V6ControlsHelper.DisableLookup = true;
            if (!(e is MouseEventArgs)) return;

            // case to MouseEventArgs so position and mousebutton clicked can be used
            MouseEventArgs mea = (MouseEventArgs) e;

            // only continue if left mouse button was clicked
            if (mea.Button != MouseButtons.Left) return;
            
            int index = (mea.Y - 1) / (buttonHeight + 1);

            if (index < 0 || index >= buttons.Count)
                return;

            MenuButton button = buttons[index];
            if (button == null) return;
            if (!button.Enabled) return;

            // ok, all checks passed so assign the new selected button
            // and raise the event
            SelectedButton = button;

            ButtonClickEventArgs bce = new ButtonClickEventArgs(selectedButton, mea);
            if (Click != null) // only invoke on left mouse click
                Click.Invoke(this, bce);
        }

        private void OutlookBar_DoubleClick(object sender, EventArgs e)
        {
            //TODO: only if you intend to support a doubleclick
            // this can be implemented exactly like the click event
        }


        private void OutlookBar_MouseLeave(object sender, EventArgs e)
        {
            V6ControlsHelper.DisableLookup = false;
            // mouse is leaving control, so unhighlight anything that is highlighted
            if (hoveringButtonIndex >= 0)
            {
                // so we need to change the hoveringButtonIndex to the new index
                Graphics g = Graphics.FromHwnd(Handle);
                MenuButton b1 = buttons[hoveringButtonIndex];

                // un-highlight current hovering button
                b1.PaintButton(g, 1, hoveringButtonIndex * (buttonHeight + 1) + 1, b1.Equals(selectedButton), false);
                hoveringButtonIndex = -1;
                g.Dispose();
            }
        }

        private void OutlookBar_MouseMove(object sender, MouseEventArgs e)
        {
            //V6ControlFormHelper.DisableLookup = true;
            if (e.Button == MouseButtons.None)
            {
                // determine over which button the mouse is moving
                int index = (e.Location.Y - 1) / (buttonHeight + 1);
                if (index >= 0 && index < buttons.Count)
                {
                    if (hoveringButtonIndex == index )
                        return; // nothing changed so we're done, current button stays highlighted

                    // so we need to change the hoveringButtonIndex to the new index
                    Graphics g = Graphics.FromHwnd(Handle);

                    if (hoveringButtonIndex >= 0)
                    {
                        MenuButton b1 = buttons[hoveringButtonIndex];

                        // un-highlight current hovering button
                        b1.PaintButton(g, 1, hoveringButtonIndex * (buttonHeight + 1) + 1, b1.Equals(selectedButton), false);
                    }
                    
                    // highlight new hovering button
                    MenuButton b2 = buttons[index];
                    b2.PaintButton(g, 1, index * (buttonHeight + 1) + 1, b2.Equals(selectedButton), true);
                    hoveringButtonIndex = index; // set to new index
                    g.Dispose();

                }
                else
                {
                    // no hovering button, so un-highlight all.
                    if (hoveringButtonIndex >= 0)
                    {
                        // so we need to change the hoveringButtonIndex to the new index
                        Graphics g = Graphics.FromHwnd(Handle);
                        MenuButton b1 = buttons[hoveringButtonIndex];

                        // un-highlight current hovering button
                        b1.PaintButton(g, 1, hoveringButtonIndex * (buttonHeight + 1) + 1, b1.Equals(selectedButton), false);
                        hoveringButtonIndex = -1;
                        g.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// _isResizing is used as a signal, so this method is not called recusively
        /// this prevents a stack overflow
        /// </summary>
        private bool _isResizing;
        private void OutlookBar_Resize(object sender, EventArgs e)
        {
            // only set sizes automatically at runtime
            if (!DesignMode)
            {
                if (!_isResizing)
                {
                    _isResizing = true;
                    if ((Height - 1) % (buttonHeight + 1) > 0)
                        Height = ((Height - 1) / (buttonHeight + 1)) * (buttonHeight + 1) + 1;
                    Invalidate();
                    _isResizing = false;
                }
            }
        }

        #endregion MenuControl control event handlers

    }

    /// <summary>
    /// OutlookbarButton represents a button on the Outlookbar
    /// ItemID; CodeForm; ReportFile; ReportTitle; eportTitle2;
    /// ReportFileF5; ReportTitleF5; ReportTitle2F5;
    /// </summary>
    #region MenuButton
    public class MenuButton // : IComponent
    {
        private bool _enabled = true;

        [Description("Indicates wether the button is _enabled"), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        private bool _visible = true;
        [Description("Indicates wether the button is _enabled"), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        protected Image image;
        [Description("The image that will be displayed on the button"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                parenT.Invalidate();
            }
        }

        protected string info = "";
        [Description("User-defined data to be associated with the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        protected string codeform = "";
        [Description("control"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CodeForm
        {
            get { return codeform; }
            set { codeform = value; }
        }

        /// <summary>
        /// v2ID, jobID, itemID
        /// </summary>
        protected string itemID = "";
        [Description("Mã control"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        /// <summary>
        /// Tên file help , không chứa phần mở rộng và đường dẫn.
        /// </summary>
        public string Key1;
        public string Key2, Key3, Key4;

        protected string userControlViewName = "";
        [Description("User-defined data to be associated with the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserControlViewName
        {
            get { return userControlViewName; }
            set { userControlViewName = value; }
        }

        protected string accessibleDescription = "";
        [Description("ID lấy ngôn ngữ"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AccessibleDescription
        {
            get
            {
                return accessibleDescription;
            }
            set { accessibleDescription = value; }
        }

        protected string exe = "";
        [Description("Tên tập tin thực thi."), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Exe
        {
            get { return exe; }
            set { exe = value; }
        }

        protected string maCT = "";
        [Description("Tên tập tin thực thi."), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string MaChungTu
        {
            get { return maCT; }
            set { maCT = value; }
        }

        protected string nhatKy = "";
        [Description("Tên tập tin thực thi."), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string NhatKy
        {
            get { return nhatKy; }
            set { nhatKy = value; }
        }

        protected string tablename = "";
        [Description("User-defined data to be associated with the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TableName
        {
            get { return tablename; }
            set { tablename = value; }
        }

        protected string listtable = "";
        [Description("User-defined data to be associated with the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ListTable
        {
            get { return listtable; }
            set { listtable = value; }
        }

        protected string sortfield = "";
        [Description("User-defined data to be associated with the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string SortField
        {
            get { return sortfield; }
            set { sortfield = value; }
        }

        /// <summary>
        /// Định nghĩa các trường khóa, cách nhau bởi dấu phẩy nếu có nhiều.
        /// </summary>
        protected string keyfield = "";
        [Description("Định nghĩa các trường khóa, cách nhau bởi dấu phẩy nếu có nhiều."), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string KeyFields
        {
            get { return keyfield; }
            set { keyfield = value; }
        }

        protected object tag;
        [Description("User-defined data to be associated with the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        protected string hotkey = "";
        [Description("Dùng để định nghĩa 1 hotkey (chưa xài)"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string HotKey
        {
            get { return hotkey; }
            set { hotkey = value; }
        }

        public MenuButton()
        {
            parenT = new MenuControl(); // set it to a dummy outlookbar control
            text = "";
            ViewStatusNumber = V6Setting.ViewMenuStatus;
            //Test
            //StatusNumber = 5;
        }

        public MenuButton(MenuControl parent)
        {
            parenT = parent;
            text = "";
            ViewStatusNumber = V6Setting.ViewMenuStatus;
            //Test
            //StatusNumber = 5;
        }

        protected MenuControl parenT;


        internal MenuControl Parent
        {
            get { return parenT; }
            set { parenT = value; }
        }

        protected string text;
        [Description("The text that will be displayed on the button"), Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                parenT.Invalidate();
            }
        }

        protected int height;
        public int Height
        {
            get { return parenT == null ? 30 : parenT.ButtonHeight; }

        }

        public int Width
        {
            get { return parenT == null ? 60 : parenT.Width - 2; }
        }

        private bool ViewStatusNumber = false;
        public int StatusNumber { get; set; }

        public string ReportFile { get; set; }
        public string ReportTitle { get; set; }
        public string ReportTitle2 { get; set; }

        public string ReportFileF5 { get; set; }
        public string ReportTitleF5 { get; set; }
        public string ReportTitle2F5 { get; set; }

        /// <summary>
        /// the outlook button will paint itself on its container (the MenuControl)
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isSelected"></param>
        /// <param name="isHovering"></param>
        public void PaintButton(Graphics graphics, int x, int y, bool isSelected, bool isHovering)
        {
            Brush br;
            Rectangle rect = new Rectangle(0, y, Width, Height);
            
            if (_enabled)
            {
                if (isSelected)
                {
                    if (isHovering)
                    {
                        br = new LinearGradientBrush(rect, parenT.GradientButtonSelectedDark,
                            parenT.GradientButtonSelectedLight, 90f);
                        V6ControlFormHelper.SetStatusText(Text);
                    }
                    else
                    {
                        br = new LinearGradientBrush(rect, parenT.GradientButtonSelectedLight,
                            parenT.GradientButtonSelectedDark, 90f);
                    }
                }
                else
                {
                    if (isHovering)
                    {
                        br = new LinearGradientBrush(rect, parenT.GradientButtonHoverLight,
                            parenT.GradientButtonHoverDark, 90f);
                        V6ControlFormHelper.SetStatusText(Text);
                    }
                    else
                    {
                        br = new LinearGradientBrush(rect, parenT.GradientButtonNormalLight,
                            parenT.GradientButtonNormalDark, 90f);
                    }
                }
            }
            else
                br = new LinearGradientBrush(rect, parenT.GradientButtonNormalLight, parenT.GradientButtonNormalDark, 90f);

            //Tô màu bằng br đã tạo
            graphics.FillRectangle(br, x, y, Width, Height);
            //Vẽ số trạng thái trên góc trên bên phải
            if (ViewStatusNumber && StatusNumber > 0)
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                if (parenT.ViewStatusMode == 3)
                {
                    RectangleF statusRec = new RectangleF(x + Width - 10, y + 1, 8, 8);
                    graphics.FillEllipse(Brushes.Red, statusRec);
                }
                else
                {
                    var statusFont = new Font(parenT.Font.FontFamily, 7);
                    graphics.DrawString("" + StatusNumber, statusFont, Brushes.Black, x + Width - 12, y + 1);
                }
            }
            br.Dispose();

            if (text.Length > 0)
                graphics.DrawString(Text, parenT.Font, Brushes.Black, 36, y + Height / 2 - parenT.Font.Height / 2);

            if (image != null)
            {
                graphics.DrawImage(image, 36 / 2 - image.Width / 2, y + Height / 2 - image.Height / 2, image.Width, image.Height);
            }
        }
    }
    #endregion

}
