using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using H_DatabaseAccess;

namespace H_Controls.Controls
{
    public class DateTimePick:DateTimePicker
    {
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
            _numberKeyPressed = (e.Modifiers == Keys.None && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode != Keys.Back && e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)));
            _selectionComplete = false;
            base.OnKeyDown(e);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmReflect + WmNotify)
            {
                var hdr = (NmHdr)m.GetLParam(typeof(NmHdr));
                if (hdr.Code == -759) //date chosen (by keyboard)
                    _selectionComplete = true;
            }
            base.WndProc(ref m);
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

        private string _gotfocustext = "";
        [Category("H")]
        public string GotFocusText { get { return _gotfocustext; } }
        private string _lostfocustext = "";
        [Category("H")]
        public string LostFocusText { get { return _lostfocustext; } }
        [Category("H")]
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

        public string TextTitle { get; set; }

        public virtual string Query
        {
            get { return SqlGenerator.GetQuery(Value.ToString("yyyyMMdd"), AccessibleName, "="); }
        }
        //============================================================================
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public DateTimePick()
        {
            InitializeComponent();
            myInit();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ColorDateTimePick
            // 
            this.CustomFormat = "dd/MM/yyyy";
            this.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Click += new System.EventHandler(this.ColorTextBox_Click);
            this.Enter += new System.EventHandler(this.ColorTextBox_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ColorTextBox_KeyDown);
            this.Leave += new System.EventHandler(this.ColorTextBox_Leave);
            this.MouseLeave += new System.EventHandler(this.ColorTextBox_MouseLeave);
            this.ResumeLayout(false);

        }
        private void myInit()
        {
            //this.BackColor = LeaveColor;
            //this.PreviousColor = BackColor;
        }
        //==========================================================================
        public void Quyen()
        {
            
        }
        //==========================================================================
        private void ColorTextBox_Enter(object sender, EventArgs e)
        {
            this.BackColor = _enterColor;
            this._previousColor = BackColor;
            this._gotfocustext = this.Text;
        }
        private void ColorTextBox_Leave(object sender, EventArgs e)
        {
            this.BackColor = _leaveColor;
            this._previousColor = BackColor;
            this._lostfocustext = this.Text;
        }        

        private void ColorTextBox_Click(object sender, EventArgs e)
        {
            
        }
        private bool _detroysenkey = false;
        public void DestroySendkey()
        {
            _detroysenkey = true;
        }
        protected virtual void ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_detroysenkey) return;

            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ColorTextBox_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = _previousColor;
        }
    }
}
