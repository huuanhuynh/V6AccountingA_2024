using System;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using V6Tools.V6Convert;

namespace V6Controls
{
    /// <summary>
    /// Kiểu số
    /// </summary>
    public class V6NumberTextBox : V6ColorTextBox
    {
        #region ==== Properties ====

        //public override string Text { get; private set; }

        protected int _decimals = 3;

        [Description("Số chữ số phần lẽ (phần thập phân)")]
        [DefaultValue(3)]
        [Category("V6")]
        public int DecimalPlaces
        {
            get { return _decimals; }
            set
            {
                _decimals = value;
                Write();
            }
        }
        /// <summary>
        /// String value (Decimal Format)
        /// </summary>
        private string _stringValue = "";         //stringDecimalValue : 1234567.9012 //Dùng system decimal separator???
        public delegate void StringValueChangeDelegate(object sender, StringValueChangeEventArgs e);
        /// <summary>
        /// Xảy ra khi value change, dùng cho sự kiện value change luôn.
        /// </summary>
        public event StringValueChangeDelegate StringValueChange;

        /// <summary>
        /// Giá trị kiểu chuổi 1234.45 - Dấu thập phân theo hệ thống.
        /// </summary>
        [Category("V6")]
        public string StringValue
        {
            get
            {
                var s = _stringValue==""?"0":_stringValue;
                if(DecimalPlaces>0)
                {
                    if(s.Contains(_system_decimal_symbol))
                    {
                        if (s.IndexOf(_system_decimal_symbol, StringComparison.Ordinal) == 0)
                            s = 0 + s;
                    }
                    else
                    {
                        s += _system_decimal_symbol;
                    }

                    //Bỏ số 0 đứng trước
                    while (s.IndexOf(_system_decimal_symbol, StringComparison.Ordinal)>1 && s.StartsWith("0"))
                    {
                        s = s.Substring(1);
                    }

                    //Thêm số 0 vào sau.
                    while (s.Length - s.IndexOf(_system_decimal_symbol, StringComparison.Ordinal) - 1 < _decimals)
                    {
                        s += "0";
                    }

                    if (s.Contains(_system_decimal_symbol) && s.Length - s.IndexOf(_system_decimal_symbol, StringComparison.Ordinal) - 1 > _decimals)
                    {
                        s = s.Substring(0, s.IndexOf(_system_decimal_symbol, StringComparison.Ordinal) + _decimals + 1);
                    }
                }
                return s;
            }
            protected set
            {
                var old_value = _stringValue;
                _stringValue = value;
                if (StringValueChange != null)
                    StringValueChange(this,new StringValueChangeEventArgs {OldValue = old_value, NewValue = value});
            }
        }

        protected decimal gotFocusValue = 0;
        [Category("V6")]
        public decimal GotFocusValue
        {
            get
            {
                return gotFocusValue;
                //decimal d = 0;
                //decimal.TryParse(GotFocusText.Replace(ThousandSymbol.ToString(),""), out d);
                //return d;
            }
        }
        protected decimal lostFocusValue = 0;
        [Category("V6")]
        public decimal LostFocusValue
        {
            get
            {
                return lostFocusValue;
                //decimal d = 0;
                //decimal.TryParse(LostFocusText.Replace(ThousandSymbol.ToString(),""), out d);
                //return d;
            }
        }

        public override string Query
        {
            get { return GetQuery(StringValue, "="); }
        }

        //public override string FormatStringValue(string value, string Operator = "like")
        //{
        //    if (",=,<>,>,>=,<,<=,".Contains("," + Operator + ","))
        //        return string.Format("{0}", value.Replace("'", "''"));
        //    else if (Operator == "like")
        //        return string.Format("N'%{0}%'", value.Replace("'", "''"));
        //    else if (Operator == "start")
        //        return string.Format("N'{0}%'", value.Replace("'", "''"));
        //    return "";
        //}

        public override string GetQuery(string text, string oper = "like")
        {
            var oper1 = (oper == "start" || oper == "like") ? "=" : oper;
            return string.Format("{0} {1} {2}", AccessibleName, oper1, text);
        }

        protected override void V6ColorTextBox_Enter(object sender, EventArgs e)
        {
            base.V6ColorTextBox_Enter(sender,e);
            gotFocusValue = Value;
        }

        protected override void V6ColorTextBox_LostFocus(object sender, EventArgs e)
        {
            if (!DesignMode && _enableColorEffect)
            {
                BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            }

            if (!ReadOnly)
            {
                lostfocustext = Text;
                lostFocusValue = Value;
                if (LostFocusValue != GotFocusValue) CallDoV6LostFocus();
            }
        
        }

        readonly string _system_decimal_symbol = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        [Category("V6")]
        public decimal Value
        {
            get
            {
                decimal d;
                decimal.TryParse(_stringValue, out d);
                if (_isNegative) d = -d;
                return d;
            }
            set
            {
                if (value < 0)
                {
                    _isNegative = true;
                    value = -value;
                }
                else
                {
                    _isNegative = false;
                }
                var ts = value != 0 ? value.ToString(CultureInfo.CurrentCulture) : "0";
                StringValue = ts;
            }
        }
        [Category("V6")]
        public override bool HaveValueChanged
        {
            get { return GotFocusValue != LostFocusValue; }
        }

        private char _decimalSymbol = ',';        //Decimal
        [Description("Dấu cách phần thập phân (phần lẽ).")]
        [DefaultValue(',')]
        [Category("V6")]
        public char DecimalSymbol
        {
            get { return _decimalSymbol; }
            set {
                if (value != '\0')
                    _decimalSymbol = value;
                else
                    _decimalSymbol = '.';
            }
        }
        private char _thousandSymbol = ' ';        //Thousand
        [Description("Dấu cách phần ngàn.")]
        [DefaultValue(' ')]
        [Category("V6")]
        public char ThousandSymbol
        {
            get { return _thousandSymbol; }
            set { _thousandSymbol = value; }
        }
        
        private bool _isNegative;
        [Description("Có phải số âm hay không?")]
        [DefaultValue(false)]
        [Category("V6")]
        public bool IsNegative
        {
            get { return _isNegative; }
            set { _isNegative = value; }
        }

        /// <summary>
        /// Độ dài chuỗi số không kể dấu . và cách 3. (Chưa xử lý)
        /// </summary>
        [DefaultValue(0)]
        public int MaxNumLength { get; set; }
        /// <summary>
        /// Độ dài phần thập phân. (Chưa xử lý)
        /// Ref to Decimal places
        /// </summary>
        [DefaultValue(0)]
        public int MaxNumDecimal { get; set; }
        /// <summary>
        /// Lấy giá trị NumberFormat cho Decimals từ V6OptionValues theo name
        /// </summary>
        [DefaultValue(null)]
        public string NumberFormatName { get; set; }

        #endregion ==== Properties ====       

        public V6NumberTextBox()
        {
            InitializeComponent();
            StringValueChange += V6CurrencyTextBox_SValueChange;
            Write();
        }

        void V6CurrencyTextBox_SValueChange(object sender, StringValueChangeEventArgs e)
        {
            Write();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // V6CurrencyTextBox
            // 
            TextAlign = HorizontalAlignment.Right;
            KeyDown += V6CurrencyTextBox2_KeyDown;
            KeyPress += V6CurrencyTextBox2_KeyPress;
            GotFocus += V6NumberTextBox_GotFocus;
            LostFocus += V6NumberTextBox_LostFocus;
            
            ResumeLayout(false);
        }

        public override void UseCarry()
        {
            if (Carry)
            {
                try
                {
                    Value = ObjectAndString.StringToDecimal(CarryString);// decimal.Parse(CarryString);
                }
                catch
                {
                    // ignored
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32.WM_PASTE && Clipboard.ContainsText())
            {
                var clipboard = Clipboard.GetText().Replace("\n", "").Replace(" ","");
                clipboard = clipboard.Replace(_thousandSymbol.ToString(), "");
                clipboard = clipboard.Replace(DecimalSymbol.ToString(), _system_decimal_symbol);
                
                decimal newValue = 0;
                if (decimal.TryParse(clipboard, out newValue))
                {
                    Value = newValue;
                }
                return;
            }
            base.WndProc(ref m);
        }

        void V6NumberTextBox_GotFocus(object sender, EventArgs e)
        {
            SelectAll();
        }

        void V6NumberTextBox_LostFocus(object sender, EventArgs e)
        {
            CorrectValueFromLimitChar();
        }

        public override void Refresh()
        {
            Write();
        }

        /// <summary>
        /// Cập nhập hiển thị lên textBox
        /// </summary>
        public void Write()
        {
            var writeText = AddThousandMark(LayChuoiPhanNguyen()) + GetDecimalTextValue();
            //Loại bỏ phần lẽ
            if (writeText == "0") _stringValue = "0";

            Text = ((_isNegative?"-":"") + writeText);
            DrawGrayText();
        }

        private void CorrectValueFromLimitChar()
        {
            if (string.IsNullOrEmpty(LimitCharacters)) return;

            var limits = ObjectAndString.SplitStringBy(LimitCharacters, ';');
            foreach (string limit in limits)
            {
                if (IsEqual(limit))
                {
                    return;
                }
            }
            
            Value = ObjectAndString.ObjectToDecimal(limits[0]);
        }

        public bool IsEqual(object value)
        {
            return ObjectAndString.ObjectToDecimal(value) == Value;
        }

        protected override void DrawGrayText()
        {
            if (Value == 0)
            {
                Graphics g = CreateGraphics();
                Brush bTextColor = new SolidBrush(Color.LightGray);
                g.DrawString(_cuetext, Font, bTextColor, g.VisibleClipBounds,
                    new StringFormat() { LineAlignment = StringAlignment.Center });
            }
        }

        /// <summary>
        /// Xử lý nút Backspace
        /// </summary>
        /// <param name="i">Vị trí con trỏ trên chuỗi số
        /// (không tính dấu cách phần ngàn và những thứ không liên quan)</param>
        private void DoBack()
        {
            try
            {
                int dotIndex = StringValue.IndexOf(_system_decimal_symbol, StringComparison.Ordinal);
                //var sls = SelectionStart;
                var right = TextLength - SelectionStart;

                var i = GetRealSelectionIndex();
                var l = GetRealSelectionLength();
                if (l > 0)
                {
                    StringValue = StringValue.Remove(i, l);
                }
                else if (i > 0 && i != dotIndex+1)
                {
                    StringValue = StringValue.Remove(i - 1, 1);
                }

                //var i2 = GetRealSelectionIndex(sls);
                //while (!Equals(i2, i))
                //{
                //    if (i2 < i) i2 = GetRealSelectionIndex(++sls);
                //    else i2 = GetRealSelectionIndex(--sls);
                //}
                if (dotIndex > 0 && i > dotIndex)
                {
                    var temp = TextLength - right;
                    if (temp > 0) temp--;
                    SelectionStart = temp;
                }
                else
                {
                    SelectionStart = TextLength - right;
                }

                if (Value == 0) SelectionStart = 1;
            }
            catch
            { }
        }

        /// <summary>
        /// Xử lý nút Delete
        /// </summary>
        private void DoDelete()//Cần xử lý kỹ sls
        {
            try
            {
                int dotIndex = StringValue.IndexOf(_system_decimal_symbol, StringComparison.Ordinal);
                var sls = SelectionStart;
                int beforeLength = TextLength;
                int right = TextLength - SelectionStart;
                
                var i = GetRealSelectionIndex();
                var L = GetRealSelectionLength();

                if (L > 0)
                {
                    StringValue = _stringValue.Remove(i, L);//L
                }
                else if (i < _stringValue.Length && _stringValue.Length > 0)// && i != dotIndex)
                {
                    if(_stringValue[i].ToString() != _system_decimal_symbol)//Không được xóa dấu .
                    StringValue = _stringValue.Remove(i, 1);
                }

                var afterLength = TextLength;
                if (beforeLength - afterLength == 2)
                {
                    sls--;
                }
                //Giữ nguyên vị trí sls
                //var i2 = GetRealSelectionIndex(sls);
                //while (!Equals(i2, i))
                //{
                //    if (i2 < i) i2 = GetRealSelectionIndex(++sls);
                //    else i2 = GetRealSelectionIndex(--sls);
                //}
                if (sls > TextLength) sls = TextLength;
                SelectionStart = sls;

                //if (Value == 0 && TextLength == 1) SelectionStart = 1;
                //else
                //{
                //    SelectionStart = TextLength - right;
                //}
            }
            catch
            { }            
        }

        /// <summary>
        /// Thêm số vào chuỗi giá trị (StringValue).
        /// </summary>
        /// <param name="s"></param>
        private void InsertStringValue(char s)
        {
            if (true || StringValue.Length < MaxLength || (MaxLength==1 && !string.IsNullOrEmpty(LimitCharacters)))
            {
                int dotIndex = StringValue.IndexOf(_system_decimal_symbol, StringComparison.Ordinal);
                //int sls = SelectionStart;
                int right = TextLength - SelectionStart;
                int i = GetRealSelectionIndex();
                int l = GetRealSelectionLength();

                if (MaxLength == 1)// && !string.IsNullOrEmpty(LimitCharacters))
                {
                    StringValue = "";
                }
                else if (TextLength >= MaxLength && dotIndex<0)
                {
                    if (i <= StringValue.Length)
                    {
                        if (l > 0)//có bôi đen thì xử lý
                        {
                            StringValue = StringValue.Remove(i, l);
                            right = StringValue.Length - i;

                            if ((i == 0 || i == 1) && StringValue.StartsWith("0"))
                            {
                                StringValue = s + StringValue.Substring(1);
                                //if (right > 0) right--;
                            }
                            else
                            {
                                StringValue = StringValue.Insert(i, s.ToString());
                            }

                            var sls = TextLength - right + ((dotIndex > 0 && i > dotIndex) ? 1 : 0);
                            if (sls <= 0) sls = 1;
                            SelectionStart = sls;    
                        }                       
                    }                    
                    return;
                }
                else if ((dotIndex > 0 && DecimalPlaces > 0) && ((TextLength >= MaxLength && i<=dotIndex) || SelectionStart == TextLength))
                {
                    return;
                }
                
                
                try
                {
                    if (i <= StringValue.Length)
                    {
                        if (l > 0)//có bôi đen
                        {
                            StringValue = StringValue.Remove(i, l);
                            right = StringValue.Length-i;
                        }

                        if ((i==0||i==1) && StringValue.StartsWith("0"))
                        {
                            StringValue = s + StringValue.Substring(1);
                            //if (right > 0) right--;
                        }
                        else
                        {
                            StringValue = StringValue.Insert(i, s.ToString());
                        }
                    }
                    
                    var sls =TextLength - right + ((dotIndex>0&& i > dotIndex) ? 1 : 0);
                    if (sls <= 0) sls = 1;
                    SelectionStart = sls;                    
                }
                catch { } 
            }
        }

        /// <summary>
        /// Lấy chuỗi số sau dấu thập phân
        /// </summary>
        /// <returns>Chuỗi số sau dấu thập phân.</returns>
        private string GetDecimalTextValue()
        {
            string returnString = "";
            if (_decimals == 0) return returnString;

            if (_stringValue.Contains(_system_decimal_symbol))
            {
                returnString = _stringValue.Substring(_stringValue.IndexOf(_system_decimal_symbol, StringComparison.Ordinal) + 1);
                if (returnString.Length > _decimals)
                {
                    returnString = returnString.Substring(0, _decimals);
                }
            }
            while (returnString.Length < _decimals)
            {
                returnString += "0";
            }
            returnString = _decimalSymbol + returnString;

            return returnString;
        }
        
        /// <summary>
        /// Lấy chuỗi số trước dấu thập phân.
        /// </summary>
        /// <returns>Chuỗi số trước dấu thập phân.</returns>
        private string LayChuoiPhanNguyen()
        {
            var returnString = _stringValue.Contains(_system_decimal_symbol) ?
                _stringValue.Substring(0, _stringValue.IndexOf(_system_decimal_symbol, StringComparison.Ordinal))
                : _stringValue;
            if (returnString.Length == 0)
                returnString = "0";
            return returnString;
        }
        //Them dau cach phan ngan
        private string AddThousandMark(string rawTextValue)
        {
            string returnString = rawTextValue;
            string thuosandMark = "";
            if (_thousandSymbol != '\0')
            {
                thuosandMark = _thousandSymbol.ToString();
            }
            for (int i = 3; i < rawTextValue.Length; i += 3)
            {
                returnString = returnString.Insert(rawTextValue.Length - i, thuosandMark);
            }
            return returnString;
        }
        
        /// <summary>
        /// Get Selection Start
        /// Trả về vị trí con trỏ trong StringValue
        /// </summary>
        /// <returns></returns>
        private int GetRealSelectionIndex(int i = -1)
        {
            if(i==-1) i = SelectionStart;
            
            int returnInt = 0;
            //Lấy chuỗi tạm đến vị trí con trỏ
            string s = Text.Substring(0, i);
            //Xử lý bỏ những thứ không cần thiết trong chuỗi
            //ký hiệu tiền tệ, dấu cách phần ngàn, khoảng trắng)
            //if (CurrencySymbolAtFirst && _currencySymbol.Length > 0 && s.Length >= _currencySymbol.Length)            
            //    s = s.Substring(_currencySymbol.Length);// .Replace(mCSymbol, "");
            if (s.Length > 0 && _thousandSymbol.ToString().Length>0)
                s = s.Replace(_thousandSymbol.ToString(), "");
            if (s.Length > 0)
                s = s.Replace(" ", "");
            if (s.Length > 0)
                s = s.Replace("-", "");
            
            returnInt = s.Length;
            if (returnInt > StringValue.Length) returnInt = StringValue.Length;
            return returnInt;
        }

        private int GetRealSelectionLength()
        {
            int i = SelectionStart;
            int l = SelectionLength;
            if (l == 0) return 0;
            int r = 0;
            string s = Text.Substring(i, l);

            //if (CurrencySymbolAtFirst &&
            //    _currencySymbol.Length > 0 && s.Length >= _currencySymbol.Length)
            //    s = s.Substring(_currencySymbol.Length);
            if (s.Length > 0)
                s = s.Replace(""+_thousandSymbol ,"");
            if (s.Length > 0)
                s = s.Replace(" ", "");
            if (s.Length > 0)
                s = s.Replace("-", "");

            r = s.Length;
            return r;
        }

        //==================== Xử lý sự kiện =========================


        private void V6CurrencyTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //chặn sự kiện của phím để dùng xử lý riêng
            e.Handled = true;
            if (ReadOnly)
            {
                if (e.KeyData == Keys.Enter) SendKeys.Send("{TAB}");
                return;
            }
            //int sls = SelectionStart;
            if (e.KeyCode == Keys.Back)
            {
                DoBack();
            }
            else if(e.KeyCode == Keys.Delete)
            {
                DoDelete();
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                if (SelectionStart > 0)
                {
                    SelectionStart--;
                }
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                if (SelectionStart < Text.Length)
                {
                    SelectionStart++;
                }
            }
            else if(e.KeyCode == Keys.Home)
            {
                SelectionStart = 0;
            }
            else if (e.KeyCode == Keys.End)
            {
                SelectionStart = Text.Length;
            }
            //else if(e.KeyCode == Keys.OemMinus)
            //{
                
            //}

        }

        private void V6CurrencyTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ReadOnly) return;

            string t = e.KeyChar.ToString();
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;

                //if (!string.IsNullOrEmpty(LimitCharacters)) // Đổi chức năng LimitCharacters thành correct value.
                //{
                //    var c = e.KeyChar;
                //    if (LimitCharacters.IndexOf(c) >= 0)
                //    {
                //        InsertStringValue(e.KeyChar);
                //    }
                //}
                //else
                {
                    InsertStringValue(e.KeyChar);
                }
            }
            else if (e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == _decimalSymbol || e.KeyChar == _system_decimal_symbol[0])
            {
                e.Handled = true;
                if (!_stringValue.Contains(_system_decimal_symbol))
                {
                    InsertStringValue(_system_decimal_symbol[0]);
                }
                SelectionStart = Text.IndexOf(_decimalSymbol) + 1;
            }
            else if (e.KeyChar == '-')
            {
                e.Handled = true;
                if (_isNegative && SelectionStart == 0)
                    SelectionStart = 1;
                _isNegative = !_isNegative;
                Write();
            }
            else if (e.KeyChar != 3 && e.KeyChar != 22)
            {
                e.Handled = true;
            }
            
        }

        public void ChangeValue(decimal value)
        {
            var oldValue = Value;
            Value = value;
            if (oldValue != Value) CallDoV6LostFocus();
            else CallDoV6LostFocusNoChange();
        }
    }
    /*
     *  Anhh
     */
    public class StringValueChangeEventArgs : EventArgs
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
