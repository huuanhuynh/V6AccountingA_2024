﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls
{
    /// <summary>
    /// DateTimePicker có thêm tính năng.
    /// <para>Thuộc tính ReadOnly.</para>
    /// <para>Khi ReadOnly chỉ có thể dùng hàm SetData để thay đổi giá trị.</para>
    /// </summary>
    public class V6DateTimePicker:DateTimePicker
    {
        ///// <summary>
        ///// Thuộc tính che, chỉ cả ngày và giờ khi UseTime=true.
        ///// </summary>
        ////[DefaultValue(typeof(DateTime),"Now")] 
        //public new DateTime Value
        //{
        //    get { return UseTime ? base.Value : base.Value.Date; }
        //    set { base.Value = UseTime ? value : value.Date; }
        //}
        //thêm thuộc tính mới
        private Color
            _enterColor = Color.PaleGreen,
            _leaveColor = Color.White,
            _hoverColor = Color.Yellow,
            _previousColor = Color.White;

        private const int WmKeyup = 0x0101;
        private const int WmKeydown = 0x0100;
        private const int WmReflect = 0x2000;
        private const int WmNotify = 0x004e;

        [StructLayout(LayoutKind.Sequential)]
        private struct NmHdr
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int Code;
        }

        private bool _numberKeyPressed;
        private bool _selectionComplete;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (ReadOnly && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Tab)
            {
                e.Handled = true;
                return;
            }
            _numberKeyPressed = (e.Modifiers == Keys.None && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode != Keys.Back && e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)));
            _selectionComplete = false;
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (ReadOnly)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyPress(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (_numberKeyPressed && _selectionComplete &&
                (e.Modifiers == Keys.None && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode != Keys.Back && e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))))
            {
                Message m = new Message();
                m.HWnd = Handle;
                m.LParam = IntPtr.Zero;
                m.WParam = new IntPtr((int)Keys.Right); //right arrow key
                m.Msg = WmKeydown;
                base.WndProc(ref m);
                m.Msg = WmKeyup;
                base.WndProc(ref m);
                _numberKeyPressed = false;
                _selectionComplete = false;
            }
        }


        protected override void WndProc(ref Message m)
        {
            //Flag for right arrow key.
            if (m.Msg == WmReflect + WmNotify)
            {
                var hdr = (NmHdr)m.GetLParam(typeof(NmHdr));
                if (hdr.Code == -759) //date chosen (by keyboard)
                {
                    _selectionComplete = true;
                }
            }
            //Disable Enable property.
            //switch (m.Msg)
            //{
            //    case Win32.WM_ENABLE:
            //        // Prevent the message from reaching the control,
            //        // so the colors don't get changed by the default procedure.
            //        return; // <-- suppress WM_ENABLE message
            //}

            base.WndProc(ref m);
        }

        public event ControlEventHandle V6LostFocus;
        public event ControlEventHandle V6LostFocusNoChange;
        public void CallDoV6LostFocus()
        {
            if (V6LostFocus != null) V6LostFocus(this);
        }
        public void CallDoV6LostFocusNoChange()
        {
            if (V6LostFocusNoChange != null) V6LostFocusNoChange(this);
        }
        

        private string _gotfocustext = "";
        [Category("V6")]
        public string GotFocusText { get { return _gotfocustext; } }
        private string _lostfocustext = "";
        [Category("V6")]
        public string LostFocusText { get { return _lostfocustext; } }
        [Category("V6")]
        public virtual bool HaveValueChanged
        {
            get
            {
                return _gotfocustext.Trim()!=_lostfocustext.Trim();
            }
        }

        [Description("Màu nền khi chuột chạy ngang.(Không còn dùng nữa)")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; }
        }

        [Description("Màu nền khi ra khỏi.")]
        public Color LeaveColor
        {
            get { return _leaveColor; }
            set { _leaveColor = value; }
        }

        [Description("Màu nền khi vào.")]
        public Color EnterColor
        {
            get { return _enterColor; }
            set { _enterColor = value; }
        }

        [DefaultValue(null)]
        public string TextTitle { get; set; }
        [Category("V6")]
        [DefaultValue(false)]
        [Description("Có sử dụng thời gian trong Value(Thuộc tính che).")]
        public bool UseTime { get; set; }

        public virtual string Query
        {
            get { return SqlGenerator.GetQuery(Date.ToString("yyyyMMdd"), AccessibleName, "="); }
        }
        
        /// <summary>
        /// Giá trị ngày tháng không có ngày giờ. (ngày giờ bằng 0, mặc định).
        /// </summary>
        public DateTime Date { get { return Value.Date; } }
        /// <summary>
        /// Giá trị chuổi theo format dd/MM/yyyy
        /// </summary>
        public string StringValue { get { return Date.ToString("dd/MM/yyyy"); } }
        public string YYYYMMDD { get { return Date.ToString("yyyyMMdd"); } }
        //============================================================================
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public V6DateTimePicker()
        {
            InitializeComponent();
            MyInit();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // V6DateTimePick
            // 
            this.CustomFormat = "dd/MM/yyyy";
            this.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Click += new System.EventHandler(this.V6ColorTextBox_Click);
            this.Enter += new System.EventHandler(this.V6ColorTextBox_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendTab_KeyDown);
            this.KeyUp += V6DateTimePick_KeyUp;
            this.Leave += new System.EventHandler(this.V6ColorTextBox_Leave);
            this.MouseLeave += new System.EventHandler(this.V6ColorTextBox_MouseLeave);
            this.ValueChanged += V6DateTimePick_ValueChanged;
            this.LostFocus += V6DateTimePick_LostFocus;

            this.ResumeLayout(false);

        }

        void V6DateTimePick_LostFocus(object sender, EventArgs e)
        {
            if (!ReadOnly)
            {
                _lostfocustext = Text;
                if (LostFocusText != GotFocusText) CallDoV6LostFocus();
                else CallDoV6LostFocusNoChange();
            }
        }

        /// <summary>
        /// Gán giá trị kể cả khi readonly.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(DateTime value)
        {
            _oldValue = value;
            Value = value;
        }

        private DateTime _oldValue = DateTime.Now;
        void V6DateTimePick_ValueChanged(object sender, EventArgs e)
        {
            if (ReadOnly && Value != _oldValue)
            {
                Value = _oldValue;
                V6ControlFormHelper.SetStatusText(AccessibleName + " " + V6Text.ReadOnly + "!!!!!!!!!!!!!!!!!!!!!!!");
                return;
            }
            
            _oldValue = Value;

            if (_selectDatePartIndex > -1)
            {
                BackgroundWorker bk = new BackgroundWorker();
                bk.DoWork += bk_DoWork;
                bk.RunWorkerCompleted += bk_RunWorkerCompleted;
            }
        }

        void bk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var i = _selectDatePartIndex;
            _selectDatePartIndex = -1;
            SelectDatePart(i);
        }

        void bk_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);
        }
        
        private void MyInit()
        {
            
        }
        //==========================================================================
        
        #region ==== Change Disable BackColor and ForeColor ====

        /// <summary>
        /// Cờ readonly
        /// </summary>
        [DefaultValue(false)]
        public bool ReadOnly { get; set; }

        #endregion change disable backcolor and forecolor
        //==========================================================================

        [DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
       public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

       private const int MOUSEEVENTF_LEFTDOWN = 0x02;
       private const int MOUSEEVENTF_LEFTUP = 0x04;
       private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
       private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public void DoScreenMouseClick(int X, int Y)
        {
            //Call the imported function with the cursor's current position
            //;int X = Cursor.Position.X;
            //int Y = Cursor.Position.Y;
            if (X < 0) X = 1;
            if (Y < 0) Y = 1;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint) X, (uint) Y, 0, 0);
        }

        /// <summary>
        /// Thực hiện một click chuột lên control.
        /// </summary>
        /// <param name="p">Tọa độ sẽ click lên control (sai số 1,1)</param>
        protected void DoVirtualMouseClick(Point p)
        {
            //var p = new Point(9, 9);    // Khởi tạo vị trí
            var sp = PointToScreen(p);  // Tìm vị trí trên màn hình
            var op = Cursor.Position;   // Lưu vị trí trỏ chuột
            Cursor.Position = sp;       // Đưa trỏ chuột đến vị trí vừa tìm
            DoScreenMouseClick(1, 1);         // Thực hiện click
            Cursor.Position = op;       // Đưa chuột về vị trí cũ
        }

        //Click chuột vào vị trí đầu tiên
        private void V6ColorTextBox_Enter(object sender, EventArgs e)
        {
            _input_history = "";
            this.BackColor = _enterColor;
            this._previousColor = BackColor;
            this._gotfocustext = this.Text;

            DoVirtualMouseClick(new Point(9, 9));

            //SendKeys.Send("{RIGHT 2}");
        }

        private void V6ColorTextBox_Leave(object sender, EventArgs e)
        {
            this.BackColor = _leaveColor;
            this._previousColor = BackColor;
            this._lostfocustext = this.Text;
        }        

        private void V6ColorTextBox_Click(object sender, EventArgs e)
        {
            
        }
        private bool _detroysenkey = false;
        public bool Carry;

        public void DestroySendkey()
        {
            _detroysenkey = true;
        }
        protected virtual void SendTab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Home)
            {
                e.Handled = true;
                SelectDatePart(0);
                Reset_InputHistory();
            }

            if (!_detroysenkey && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private string _input_history = "";

        private void Reset_InputHistory()
        {
            _input_history = "";
        }
        void V6DateTimePick_KeyUp(object sender, KeyEventArgs e)
        {
            if (ReadOnly || _input_history.Length > 1)
            {
                return;
            }
            
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                _input_history += "99";
                return;
            }

            KeysConverter kc = new KeysConverter();
            string now_digit = kc.ConvertToString(e.KeyData).Right(1);

            if (_input_history.Length == 1)
            {
                var num = ObjectAndString.ObjectToInt(_input_history + now_digit);
                if (num > DateTime.DaysInMonth(Value.Year, Value.Month))
                {
                    if (num > 31) num = 31;
                    Value = new DateTime(Value.Year, 1, num);
                    SelectDatePartAfterValueChanged(1);
                }
            }

            if (char.IsDigit(Convert.ToChar(now_digit)))
            {
                _input_history += now_digit;
            }
        }

        private int _selectDatePartIndex = -1;
        private void SelectDatePartAfterValueChanged(int i)
        {
            _selectDatePartIndex = i;
        }

        /// <summary>
        /// Di chuyển selection theo vị trí, vd 0:ngày 1:tháng 2:năm...
        /// </summary>
        /// <param name="i">index</param>
        public void SelectDatePart(int i)
        {
            DoVirtualMouseClick(new Point(9, 9));
            if(i>0) SendKeys.Send("{/}");
            if(i>1) SendKeys.Send("{/}");
            if(i>2) SendKeys.Send("{/}");
            if(i>3) SendKeys.Send("{/}");
            if(i>4) SendKeys.Send("{/}");
            if(i>5) SendKeys.Send("{/}");
            if(i>6) SendKeys.Send("{/}");
        }

        private void V6ColorTextBox_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = _previousColor;
        }

        
    }
}