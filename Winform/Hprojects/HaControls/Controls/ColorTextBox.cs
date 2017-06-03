using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace H_Controls.Controls
{
    public class ColorTextBox:TextBox
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public ColorTextBox()
        {
            InitializeComponent();
            MyInit();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ColorTextBox
            // 
            MouseLeave += ColorTextBox_MouseLeave;

            KeyDown += ColorTextBox_KeyDown;
            KeyPress += LimitTextBox_KeyPress;
            Enter += ColorTextBox_Enter;
            Leave += ColorTextBox_LostFocus;
            MouseEnter += ColorTextBox_MouseEnter;
            ReadOnlyChanged += ColorTextBox_ReadOnlyChanged;
            EnabledChanged += ColorTextBox_EnabledChanged;
            //TextChanged += ColorTextBox_TextChanged;

            ResumeLayout(false);

        }
        

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
        public const int ECM_FIRST = 0x1500;
        public const int EM_SETCUEBANNER = ECM_FIRST + 1;

        //thêm thuộc tính mới
        protected Color
            _enterColor = Color.PaleGreen,
            _enterColorReadOnly = Color.AntiqueWhite,
            _leaveColor = Color.White,
            _leaveColorReadOnly = Color.AntiqueWhite,
            _disableColor = Color.WhiteSmoke,
            _hoverColor = Color.Yellow;


        protected bool _enableColorEffect = true;
        protected bool _enableColorEffectOnMouseEnter;
        protected bool _upper, _lower;
        
        public void Upper()
        {
            var sls = SelectionStart;
            _upper = true;
            _lower = false;
            Text = Text.ToUpper();
            SelectionStart = sls;
        }

        public void Lower()
        {
            var sls = SelectionStart;
            _upper = false;
            _lower = true;
            Text = Text.ToLower();
            SelectionStart = sls;
        }

        public string LimitCharacters
        {
            get;
            set;
        }

        public void SetLimitCharacters(string s)
        {
            LimitCharacters = s;
        }

        void LimitTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !string.IsNullOrEmpty(LimitCharacters))
            {
                var c = e.KeyChar;
                //var c = (char)e.KeyCode;
                if (!LimitCharacters.Contains(c)) e.Handled = true;
                else
                {
                    if (MaxLength == 1)
                    {
                        Text = c.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Bật tắt chức năng đổi màu khi di chuột hay vào control.
        /// </summary>
        [Category("H")]
        public bool EnableColorEffect
        {
            get { return _enableColorEffect; }
            set { _enableColorEffect = value; }
        }
        [Category("H")]
        public bool EnableColorEffectOnMouseEnter
        {
            get { return _enableColorEffectOnMouseEnter; }
            set { _enableColorEffectOnMouseEnter = value; }
        }
        private string _cuetext = "";
        [Category("H")]
        public string GrayTitle
        {
            get
            {
                return _cuetext;
            }
            set
            {
                _cuetext = value;
                SetCueText(_cuetext);
            }
        }
        public void SetCueText(string cueText)
        {
            SendMessage(Handle, EM_SETCUEBANNER, IntPtr.Zero, cueText);
        }
        protected string gotfocustext = "";

        [Category("H")]
        public string GotFocusText
        {
            get { return gotfocustext; }
        }

        protected string lostfocustext = "";

        [Category("H")]
        public string LostFocusText
        {
            get { return lostfocustext; }
        }

        [Category("H")]
        public virtual bool HaveValueChanged
        {
            get
            {
                return gotfocustext.Trim()!=lostfocustext.Trim();
            }
        }

        [Description("Màu nền khi chuột chạy ngang.(Không còn dùng nữa)")]
        //[DefaultValue(Color.White)]
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
        //[DefaultValue(Color.PaleGreen)]
        public Color EnterColor
        {
            get { return _enterColor; }
            set { _enterColor = value; }
        }

        private bool _looking;
        /// <summary>
        /// Ready = false khi lookup, true khi lookup xong
        /// </summary>
        protected bool Looking
        {
            get { return _looking; }
            set { _looking = value; }
        }

        /// <summary>
        /// Biến chuỗi giá trị thành chuỗi sử dụng trong chuỗi sql. (tự bọc dấu ' )
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Operator">like|start|=|...</param>
        /// <returns></returns>
        public virtual string FormatStringValue(string value, string Operator = "like")
        {
            if (",=,<>,>,>=,<,<=,".Contains("," + Operator + ","))
                return string.Format("'{0}'", value.Replace("'", "''"));
            else if (Operator == "like")
                return string.Format("'%{0}%'", value.Replace("'", "''"));
            else if (Operator == "start")
                return string.Format("'{0}%'", value.Replace("'", "''"));
            return "";
        }

        /// <summary>
        /// If value not containt(",") return "AccessibleName like 'ABC'";
        /// else return "AccessibleName like 'ABC' or AccessibleName like 'DEF'"
        /// </summary>
        public virtual string Query
        {
            get
            {
                return GetQuery(Text);
            }
        }

        /// <summary>
        /// Lấy chuỗi truy vấn dùng cho Where trong sql
        /// </summary>
        /// <param name="text">Giá trị kiểu chuỗi</param>
        /// <param name="oper">các dấu so sánh trong sql.
        /// nếu dùng like sẽ dùng % ở 2 đầu.
        /// nếu dùng start chỉ dùng % ở sau.</param>
        /// <returns></returns>
        public virtual string GetQuery(string text, string oper = "like")
        {
            var sValue = text.Trim();
            var result = "";
            
            var oper1 = oper == "start"? "like" : oper;

            if (sValue.Contains(","))
            {
                string[] sss = sValue.Split(',');
                foreach (string s in sss)
                {
                    result += string.Format(" or {0} {1} {2}", AccessibleName, oper1, FormatStringValue(s, oper));
                }

                if (result.Length > 4)
                {
                    result = result.Substring(4);
                    result = string.Format("({0})", result);
                }
            }
            else
            {
                result = string.Format("{0} {1} {2}", AccessibleName, oper1, FormatStringValue(sValue, oper1));
            }
            return result;
        }

        /// <summary>
        /// Set trạng thái đang lookup
        /// </summary>
        /// <param name="b"></param>
        public void SetLooking(bool b)
        {
            _looking = b;
        }

        //============================================================================
        void ColorTextBox_TextChanged(object sender, EventArgs e)
        {
            var sls = SelectionStart;
            if (_upper) Text = Text.ToUpper();
            else if (_lower) Text = Text.ToLower();
            SelectionStart = sls;
        }

        void ColorTextBox_ReadOnlyChanged(object sender, EventArgs e)
        {
            BackColor = Enabled
                ? (ReadOnly ? _leaveColorReadOnly : _leaveColor)
                : _disableColor;
        }

        void ColorTextBox_EnabledChanged(object sender, EventArgs e)
        {
            BackColor = Enabled
                ? (ReadOnly ? _leaveColorReadOnly : _leaveColor)
                : _disableColor;
        }


        private void MyInit()
        {
            //BackColor = LeaveColor;
            //PreviousColor = BackColor;
        }
        //==========================================================================
        public void Quyen()
        {
        }
        //==========================================================================
        #region ==== Events ====
        public delegate void LostFocusDelegate(object sender);

        public event LostFocusDelegate LostFocusChange;
        public event LostFocusDelegate LostFocusNoChange;

        public void CallDoLostFocusChange()
        {
            if (LostFocusChange != null) LostFocusChange(this);
        }
        public void CallDoLostFocusNoChange()
        {
            if (LostFocusNoChange != null) LostFocusNoChange(this);
        }

        protected virtual void ColorTextBox_Enter(object sender, EventArgs e)
        {
            if (_enableColorEffect)
            {
                BackColor = ReadOnly ? _enterColorReadOnly : _enterColor;
            }
            gotfocustext = Text;
        }

        protected virtual void ColorTextBox_LostFocus(object sender, EventArgs e)
        {
            if (_enableColorEffect)
            {
                BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            }

            if (!ReadOnly)
            {
                lostfocustext = Text;
                if (LostFocusText != GotFocusText) CallDoLostFocusChange();
                else CallDoLostFocusNoChange();
            }
        }        
        
        private bool _detroysenkey = false;
        public void DestroySendkey()
        {
            _detroysenkey = true;
        }

        [DefaultValue(true)]
        public bool UseSendTabOnEnter { get { return _tab; } set { _tab = value; } }
        private bool _tab = true;

        protected virtual void ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_detroysenkey) return;

            if (e.KeyCode == Keys.Enter)
            {
                if (UseSendTabOnEnter)
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void ColorTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (_enableColorEffect && _enableColorEffectOnMouseEnter && !Focused)
            {
                BackColor = _hoverColor;
            }
        }

        private void ColorTextBox_MouseLeave(object sender, EventArgs e)
        {
            if (_enableColorEffect && _enableColorEffectOnMouseEnter && !Focused)
            {
                BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            }
        }
        #endregion events

        

        /// <summary>
        /// Gán text bằng hàm này để xảy ra sự kiện LostFocusChange
        /// </summary>
        /// <param name="text">Giá trị mới</param>
        public virtual void ChangeText(string text)
        {
            var oldValue = Text;
            Text = text;
            if (oldValue != Text) CallDoLostFocusChange();
            else CallDoLostFocusNoChange();
        }
    }
}
