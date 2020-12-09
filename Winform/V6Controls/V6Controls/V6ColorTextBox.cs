using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using V6Controls.Forms;
using V6Controls.Functions;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls
{
    [DebuggerDisplay("{this}")]
    public class V6ColorTextBox : TextBox
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public V6ColorTextBox()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            //BackColor = LeaveColor;
            //PreviousColor = BackColor;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // V6ColorTextBox
            // 
            this.ReadOnlyChanged += new System.EventHandler(this.V6ColorTextBox_ReadOnlyChanged);
            this.EnabledChanged += new System.EventHandler(this.V6ColorTextBox_EnabledChanged);
            this.TextChanged += new System.EventHandler(this.V6ColorTextBox_TextChanged);
            this.VisibleChanged += new System.EventHandler(this.V6ColorTextBox_VisibleChanged);
            this.Enter += new System.EventHandler(this.V6ColorTextBox_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.V6ColorTextBox_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.V6ColorTextBox_KeyPress);
            this.Leave += new System.EventHandler(this.V6ColorTextBox_LostFocus);
            this.MouseEnter += new System.EventHandler(this.V6ColorTextBox_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.V6ColorTextBox_MouseLeave);
            this.ResumeLayout(false);

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
        public bool _upper, _lower;

        public virtual DataRow Data { get; protected set; }
        public bool LO_YN
        {
            get
            {
                if (Data != null && Data.Table.Columns.Contains("LO_YN")) return ObjectAndString.ObjectToBool(Data["LO_YN"]);
                return false;
            }
        }
        public bool SKSM_YN
        {
            get
            {
                if (Data != null && Data.Table.Columns.Contains("SKSM_YN")) return ObjectAndString.ObjectToBool(Data["SKSM_YN"]);
                return false;
            }
        }
        public bool DATE_YN
        {
            get
            {
                if (Data != null && Data.Table.Columns.Contains("DATE_YN")) return ObjectAndString.ObjectToBool(Data["DATE_YN"]);
                return false;
            }
        }
        public bool VITRI_YN
        {
            get
            {
                if (Data != null && Data.Table.Columns.Contains("VITRI_YN")) return ObjectAndString.ObjectToBool(Data["VITRI_YN"]);
                return false;
            }
        }
        public bool REPL_YN
        {
            get
            {
                if (Data != null && Data.Table.Columns.Contains("REPL_YN")) return ObjectAndString.ObjectToBool(Data["REPL_YN"]);
                return false;
            }
        }
        
        //{Tuanmh 02/08/2016
        public bool VT_TON_KHO
        {
            get
            {
                if (Data != null && Data.Table.Columns.Contains("VT_TON_KHO")) return ObjectAndString.ObjectToBool(Data["VT_TON_KHO"]);
                return false;
            }
        }
        public int GIA_TON
        {
            get
            {
                if (Data != null && Data.Table.Columns.Contains("GIA_TON")) return ObjectAndString.ObjectToInt(Data["GIA_TON"]);
                return 0;
            }
        }
        //}

        /// <summary>
        /// Cho phép ký tự xuống dòng.
        /// </summary>
        [DefaultValue(false)]
        [Description("Cho phép ký tự xuống dòng trong trường không không dùng multiline.")]
        public bool AllowMultiLine
        {
            get { return _allowMultiLine; }
            set { _allowMultiLine = value; }
        }

        protected bool _allowMultiLine = false;

        /// <summary>
        /// Bật chức năng mang theo giá trị được gán cuối cùng
        /// </summary>
        [DefaultValue(false)]
        [Description("Bật chức năng mang theo giá trị được gán cuối cùng và được dùng bởi 2 hàm SetCarryValues UseCarryValues trong V6ControlFormHelper.")]
        public bool Carry { get; set; }
        /// <summary>
        /// Giá trị đang mang theo - kiểu string
        /// </summary>
        protected string CarryString;
        /// <summary>
        /// Mang theo giá trị hiện tại của control.
        /// </summary>
        public virtual void CarryValue()
        {
            if(Carry)
            CarryString = Text;
        }
        /// <summary>
        /// Gán lại giá trị lên control bằng value đã mang theo.
        /// </summary>
        public virtual void UseCarry()
        {
            if(Carry)
            Text = CarryString;
        }
        
        /// <summary>
        /// Bật tính năng tự động Upper khi ra khỏi.
        /// </summary>
        public void Upper()
        {
            var sls = SelectionStart;
            _upper = true;
            _lower = false;
            Text = Text.ToUpper();
            SelectionStart = sls;
        }

        /// <summary>
        /// Bật tính năng tự động Lower khi ra khỏi.
        /// </summary>
        public void Lower()
        {
            var sls = SelectionStart;
            _upper = false;
            _lower = true;
            Text = Text.ToLower();
            SelectionStart = sls;
        }

        /// <summary>
        /// Tắt tính năng tự động Upper hay Lower khi ra khỏi.
        /// </summary>
        public void DisableUpperLower()
        {
            _upper = false;
            _lower = false;
        }

        protected string _filterType = null;
        /// <summary>
        /// Lấy FilterType của form chứa nó.
        /// </summary>
        /// <returns></returns>
        public string GetFilterType()
        {
            if (_filterType == null)
            {
                _filterType = V6ControlFormHelper.FindFilterType(this);
            }
            return _filterType;
        }

        [Category("V6")]
        [DefaultValue(null)]
        [Description("Những ký tự được phép sử dụng khi gõ. Đối với kiểu số các số cách nhau bởi ;")]
        public virtual string LimitCharacters
        {
            get { return _lmChars; }
            set { _lmChars = value; }
        }
        private string _lmChars;

        [Category("V6")]
        [DefaultValue(null)]
        [Description("Những ký tự KHÔNG được sử dụng khi gõ.")]
        public string LimitCharacters0
        {
            get
            {
                if(UseLimitCharacters0) LoadLitmit0Option();
                return _lmChars0;
            }
            set { _lmChars0 = value; }
        }
        private string _lmChars0;
        /// <summary>
        /// Bật tính năng sử dụng LimitCharacters0 lưu trong V6Options.
        /// </summary>
        [DefaultValue(false)]
        [Description("Bật tính năng sử dụng LimitCharacters0 lưu trong V6Options.")]
        public bool UseLimitCharacters0 { get; set; }

        /// <summary>
        /// Bật TabStop khi readonly.
        /// </summary>
        [DefaultValue(false)]
        [Description("Bật TabStop khi readonly.")]
        public bool ReadOnlyTabStop { get; set; }

        /// <summary>
        /// Gán chuỗi ký tự được phép sử dụng khi gõ.
        /// </summary>
        /// <param name="s"></param>
        public void SetLimitCharacters(string s)
        {
            LimitCharacters = s;
        }
        /// <summary>
        /// Gán chuỗi ký tự KHÔNG được sử dụng khi gõ.
        /// </summary>
        /// <param name="s"></param>
        public void SetLimitCharacters0(string s)
        {
            LimitCharacters0 = s;
        }

        private void LoadLitmit0Option()
        {
            if (_lmChars0 == null)
            {
                try
                {
                    LimitCharacters0 = V6Options.GetValue("M_V6_LIMITCHARS0");
                }
                catch (Exception)
                {
                    
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this is V6VvarTextBox || this is V6LookupTextBox)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            //if (keyData == Keys.Enter && UseSendTabOnEnter)
            //{
            //    //SendKeys.Send("{TAB}");
            //    SelectNextControl(this, true, true, true, true);
            //    //if (!b)
            //    //    SendKeys.Send("{TAB}");
            //    return false;
            //}

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void V6ColorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ReadOnly || !Enabled)
            {
                return;
            }

            //Xử lý phím Enter trường hợp Multiline.
            if (Multiline && e.KeyChar == '\r')
            {
                e.Handled = true;
                if ((ModifierKeys & Keys.Shift) != 0) AddNewLine();

                return;
            }

            if (!char.IsControl(e.KeyChar))// && !string.IsNullOrEmpty(LimitCharacters))
            {
                var c = e.KeyChar;
                //var c = (char)e.KeyCode;
                if ((!string.IsNullOrEmpty(_lmChars) && !_lmChars.Contains(c))
                    || (UseLimitCharacters0 && !string.IsNullOrEmpty(LimitCharacters0) && _lmChars0.Contains(c))
                    )
                {
                    e.Handled = true;
                }
                else if (!(this is V6CheckTextBox))
                {
                    if (MaxLength == 1)
                    {
                        Text = c.ToString();
                    }
                    else if (TextLength > 0 && SelectionStart == MaxLength)
                    {
                        Text = Text.Substring(0, TextLength - 1) + c;
                    }
                }
            }
        }

        /// <summary>
        /// Bật tắt chức năng đổi màu khi di chuột hay vào control.
        /// </summary>
        [Category("V6")]
        [DefaultValue(true)]
        public bool EnableColorEffect
        {
            get { return _enableColorEffect; }
            set { _enableColorEffect = value; }
        }
        [Category("V6")]
        [DefaultValue(false)]
        public bool EnableColorEffectOnMouseEnter
        {
            get { return _enableColorEffectOnMouseEnter; }
            set { _enableColorEffectOnMouseEnter = value; }
        }

        public event EventHandler GrayTextChanged;
        protected virtual void OnGrayTextChanged()
        {
            var handler = GrayTextChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected void Do_Enter_ColorEffect()
        {
            if (!DesignMode && _enableColorEffect)
            {
                BackColor = ReadOnly ? _enterColorReadOnly : _enterColor;
            }
        }
        
        protected void Do_MouseEnter_ColorEffect()
        {
            if (_enableColorEffect && _enableColorEffectOnMouseEnter && !Focused)
            {
                BackColor = _hoverColor;
            }
        }

        protected void Do_MouseLeave_ColorEffect()
        {
            if (_enableColorEffect && _enableColorEffectOnMouseEnter && !Focused)
            {
                BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            }
        }

        protected void Do_LostFocus_ColorEffect()
        {
            if (!DesignMode && _enableColorEffect)
            {
                BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            }
        }


        protected string _cuetext = "";
        [Category("V6")]
        [DefaultValue("")]
        public string GrayText
        {
            get
            {
                return _cuetext;
            }
            set
            {
                _cuetext = value;
                SetCueText(_cuetext);
                OnGrayTextChanged();
            }
        }
        public void SetCueText(string cueText)
        {
            SendMessage(Handle, EM_SETCUEBANNER, IntPtr.Zero, cueText);
        }
        protected string gotfocustext = "";

        [Category("V6")]
        public string GotFocusText
        {
            get { return gotfocustext; }
        }

        public void ResetFocusText()
        {
            gotfocustext = Text;
        }

        protected string lostfocustext = "";

        [Category("V6")]
        public string LostFocusText
        {
            get { return Text; }
        }

        [Category("V6")]
        public virtual bool HaveValueChanged
        {
            get
            {
                return gotfocustext.Trim()!=Text.Trim();
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
        /// Khi bằng true thì tạm tắt sự kiện V6LostFocus để lookup.
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
                return string.Format("N'{0}'", value.Replace("'", "''"));
            else if (Operator == "like")
                return string.Format("N'%{0}%'", value.Replace("'", "''"));
            else if (Operator == "start")
                return string.Format("N'{0}%'", value.Replace("'", "''"));
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
                return GetQuery();
            }
        }

        /// <summary>
        /// Lấy chuỗi truy vấn dùng cho Where trong sql
        /// </summary>
        /// <param name="oper">các dấu so sánh trong sql.
        /// nếu dùng like sẽ dùng % ở 2 đầu.
        /// nếu dùng start chỉ dùng % ở sau.</param>
        /// <param name="value">Giá trị kiểu text, mặc định null sẽ lấy text của TextBox</param>
        /// <returns></returns>
        public virtual string GetQuery(string oper = "like", string value = null)
        {
            var sValue = Text.Trim();
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

        protected virtual void DrawGrayText()
        {
            if (Text == "")
            {
                Graphics g = CreateGraphics();
                Brush bTextColor = new SolidBrush(Color.LightGray);
                g.DrawString(_cuetext, Font, bTextColor, g.VisibleClipBounds,
                    new StringFormat() {LineAlignment = StringAlignment.Center});
            }
        }

        //============================================================================
        void V6ColorTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        void V6ColorTextBox_ReadOnlyChanged(object sender, EventArgs e)
        {
            BackColor = Enabled
                ? (ReadOnly ? _leaveColorReadOnly : _leaveColor)
                : _disableColor;
            TabStop = ReadOnlyTabStop || !ReadOnly;
        }

        void V6ColorTextBox_EnabledChanged(object sender, EventArgs e)
        {
            BackColor = Enabled
                ? (ReadOnly ? _leaveColorReadOnly : _leaveColor)
                : _disableColor;
        }


        
        #region ==== Change Disable BackColor and ForeColor ====

        private Color _ForeColorBackup;
        private Color _BackColorBackup;
        private bool _ColorsSaved = false;

        private bool _SettingColors = false;
        private Color _BackColorDisabled = SystemColors.Control;

        private Color _ForeColorDisabled = SystemColors.WindowText;

        private void V6ColorTextBox_VisibleChanged(object sender, System.EventArgs e)
        {
            if (!this._ColorsSaved && this.Visible)
            {
                // Save the ForeColor/BackColor so we can switch back to them later
                _ForeColorBackup = this.ForeColor;
                _BackColorBackup = this.BackColor;
                _ColorsSaved = true;

                // If the window starts out in a Disabled state...
                if (!this.Enabled)
                {
                    // Force the TextBox to initialize properly in an Enabled state,
                    // then switch it back to a Disabled state
                    this.Enabled = true;
                    this.Enabled = false;
                }

                SetColors();
                // Change to the Enabled/Disabled colors specified by the user
            }
        }

        protected override void OnForeColorChanged(System.EventArgs e)
        {
            base.OnForeColorChanged(e);

            // If the color is being set from OUTSIDE our control,
            // then save the current ForeColor and set the specified color
            if (!_SettingColors)
            {
                _ForeColorBackup = this.ForeColor;
                SetColors();
            }
        }

        protected override void OnBackColorChanged(System.EventArgs e)
        {
            base.OnBackColorChanged(e);

            // If the color is being set from OUTSIDE our control,
            // then save the current BackColor and set the specified color
            if (!_SettingColors)
            {
                _BackColorBackup = this.BackColor;
                SetColors();
            }
        }

        public static bool IsRunTime
        {
            get { return LicenseManager.UsageMode != LicenseUsageMode.Designtime; }
        }

        private void SetColors()
        {
            // Don't change colors until the original ones have been saved,
            // since we would lose what the original Enabled colors are supposed to be
            if (IsRunTime && _ColorsSaved)
            {
                _SettingColors = true;
                if (this.Enabled)
                {
                    this.ForeColor = this._ForeColorBackup;
                    this.BackColor = this._BackColorBackup;
                }
                else
                {
                    this.ForeColor = this.ForeColorDisabled;
                    this.BackColor = this.BackColorDisabled;
                }
                _SettingColors = false;
            }
        }

        protected override void OnEnabledChanged(System.EventArgs e)
        {
            base.OnEnabledChanged(e);

            SetColors();
            // change colors whenever the Enabled() state changes
        }

        [Category("V6")]
        [Description("Màu nền khi Enabled=false.")]
        public Color BackColorDisabled
        {
            get { return _BackColorDisabled; }
            set
            {
                if (!value.Equals(Color.Empty))
                {
                    _BackColorDisabled = value;
                }
                SetColors();
            }
        }

        [Category("V6")]
        [Description("Màu chữ khi Enabled=false.")]
        public Color ForeColorDisabled
        {
            get { return _ForeColorDisabled; }
            set
            {
                if (!value.Equals(Color.Empty))
                {
                    _ForeColorDisabled = value;
                }
                SetColors();
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams cp = null;
                // If the window starts out in a disabled state...
                if (!this.Enabled)
                {
                    // Prevent window being initialized in a disabled state:
                    this.Enabled = true;
                    // temporary ENABLED state
                    cp = base.CreateParams;
                    // create window in ENABLED state
                    this.Enabled = false;
                    // toggle it back to DISABLED state 
                }
                else
                {
                    cp = base.CreateParams;
                }
                return cp;
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_ENABLE:
                    // Prevent the message from reaching the control,
                    // so the colors don't get changed by the default procedure.
                    return; // <-- suppress WM_ENABLE message

            }
            // Trap WM_PASTE:
            // Xử lý dữ liệu clipboard trước khi dán
            if (m.Msg == Win32.WM_PASTE)
            {
                if (Clipboard.ContainsText())
                {
                    var clipboard = Clipboard.GetText();
                    try
                    {
                        if (!string.IsNullOrEmpty(LimitCharacters0))
                        {
                            foreach (char c in _lmChars0)
                            {
                                clipboard = clipboard.Replace("" + c, "");
                            }
                        }
                        if (!string.IsNullOrEmpty(LimitCharacters))
                        {
                            for (int i = clipboard.Length - 1; i >= 0; i--)
                            {
                                char c = clipboard[i];
                                if (!_lmChars.Contains(c))
                                {
                                    clipboard = clipboard.Remove(i, 1);
                                }
                            }
                        }
                        Paste(clipboard);
                    }
                    catch (Exception ex)
                    {
                        this.WriteExLog(GetType() + ".WndProc WM_PASTE", ex);
                    }
                }
                return;
            }
            base.WndProc(ref m);
        }

        #endregion change disable backcolor and forecolor
        
        #region ==== Events ====
        /// <summary>
        /// Sự kiện xảy ra khi có dữ liệu thay đổi và leave.
        /// </summary>
        public event ControlEventHandle V6LostFocus;
        public event ControlEventHandle V6LostFocusNoChange;

        protected void DoCharacterCasing()
        {
            if (_shift_f3) return;
            if (_upper) Text = Text.ToUpper();
            else if (_lower) Text = Text.ToLower();
        }

        protected void DoRemoveMultiLine()
        {
            if (!Text.Contains("\n")) return;
            if (!Multiline && !_allowMultiLine)
            {
                Text = Text.Replace("\r\n", "").Replace("\n", "");
            }
        }

        /// <summary>
        /// Gọi sự kiện Leave. Các code .Levae += của textbox được gọi sẽ được kích hoạt.
        /// </summary>
        public void CallLeave()
        {
            OnLeave(new EventArgs());
        }

        public void CallDoV6LostFocus()
        {
            if (V6LostFocus != null) V6LostFocus(this);
        }
        public void CallDoV6LostFocusNoChange()
        {
            if (V6LostFocusNoChange != null) V6LostFocusNoChange(this);
        }

        protected virtual void V6ColorTextBox_Enter(object sender, EventArgs e)
        {
            gotfocustext = Text;
            Do_Enter_ColorEffect();
        }

        protected virtual void V6ColorTextBox_LostFocus(object sender, EventArgs e)
        {
            DoRemoveMultiLine();
            DoCharacterCasing();
            Do_LostFocus_ColorEffect();

            if (!ReadOnly)
            {
                lostfocustext = Text;
                if (LostFocusText != GotFocusText) CallDoV6LostFocus();
                else CallDoV6LostFocusNoChange();
            }
        }

        [Category("V6")]
        [DefaultValue(true)]
        public bool UseSendTabOnEnter { get { return _tab; } set { _tab = value; } }
        private bool _tab = true;
        
        private bool _detroysenkey;
        public void DestroySendkey()
        {
            _detroysenkey = true;
        }

        protected virtual void V6ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_detroysenkey) return;
            if (ReadOnly || !Enabled)
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (_tab && (!Multiline || (ModifierKeys & Keys.Shift) == 0))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                return;
            }

            if (e.KeyData == Keys.Enter)
            {
                if (_tab && (!Multiline || (ModifierKeys & Keys.Shift) == 0))
                {
                    SendKeys.Send("{TAB}");
                }
            }
            else if (ShiftF3 && e.KeyData == (Keys.Shift | Keys.F3))
            {
                Text = TextFunctions.ChangeCase(Text);
            }
            else if (ShiftF3 && e.KeyData == (Keys.Shift | Keys.F4))
            {
                Text = TextFunctions.ChangeToUnicode(Text);
            }
            else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.I))
            {
                var message = string.Format("{0}({1}), Aname({2}), Adescription({3}),",
                    GetType(), Name, AccessibleName, AccessibleDescription);
                V6ControlFormHelper.SetStatusText(message);
                Clipboard.SetText(message);
            }
        }

        [Category("V6")]
        [DefaultValue(false)]
        [Description("Bật tắt chức năng UPPER bằng phím bấm. (Nếu được bật sẽ tắt tính năng auto cast UPPER hoặc lower của VvarTextBox)")]
        public bool ShiftF3 { get { return _shift_f3; } set { _shift_f3 = value; } }
        protected bool _shift_f3 = false;

        private void V6ColorTextBox_MouseEnter(object sender, EventArgs e)
        {
            Do_MouseEnter_ColorEffect();
        }

        private void V6ColorTextBox_MouseLeave(object sender, EventArgs e)
        {
            Do_MouseLeave_ColorEffect();
        }
        #endregion events

        [Category("V6")]
        [DefaultValue(false)]
        [Description("Cờ sinh ra sự kiện khi sử dụng hàm Set[Some]DataDictionary hoặc SetControlValue lên control.")]
        public bool UseChangeTextOnSetFormData { get; set; }
        /// <summary>
        /// Gán text bằng hàm này để xảy ra sự kiện V6LostFocus
        /// </summary>
        /// <param name="text">Giá trị mới</param>
        public virtual void ChangeText(string text)
        {
            var oldValue = Text;
            Text = text;
            if (oldValue != Text) CallDoV6LostFocus();
            else CallDoV6LostFocusNoChange();
        }

        public override string ToString()
        {
            return string.Format("{0}:{1} type:{2}", string.IsNullOrEmpty(AccessibleName) ? Name : AccessibleName, Text, GetType());
        }
        
        private void AddNewLine()
        {
            int st = SelectionStart;
            Text = Text.Insert(SelectionStart, Environment.NewLine);
            SelectionStart = st + 2;
        }
    }
}
