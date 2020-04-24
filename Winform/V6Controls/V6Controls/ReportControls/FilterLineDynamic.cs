using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Controls;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ReportControls
{
    public partial class FilterLineDynamic : FilterLineBase
    {
        public V6ColorTextBox _textBox;
        public V6VvarTextBox _vtextBox;
        public V6LookupTextBox _lookupTextBox;
        public V6LookupProc _lookupProc;
        public V6NumberTextBox _numberTextBox;
        public V6DateTimeColor _dateTimeColor;
        public V6DateTimePicker _dateTimePick;
        public V6CheckBox _checkBox;
        public V6FormButton _button;
        public bool CheckNotEmpty;
        
        /// <summary>
        /// Sử dụng tùy lúc.
        /// </summary>
        public DefineInfo DefineInfo { get; set; }

        public FilterLineDynamic()
        {
            InitializeComponent();
            MyInit();
        }

        public FilterLineDynamic(string fieldName)
        {
            FieldName = fieldName.ToUpper();
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            
        }

        void FilterLineDynamic_GotFocus(object sender, EventArgs e)
        {
            OnGotFocus(e);
        }

        private void FilterLineDynamic_V6LostFocus(object sender)
        {
            OnV6LostFocus(sender);
        }

        protected void FilterLineDynamic_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void FilterLineDynamic_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        void FilterLineDynamic_LostFocus(object sender, EventArgs e)
        {
            OnLostFocus(e);
        }

        protected virtual void FilterLineDynamic_TextChanged(object sender, EventArgs e)
        {
            if (_textBox != null && _textBox.Text.Trim() == string.Empty)
            {
                IsSelected = false;
            }
            else if (_vtextBox != null && _vtextBox.Text.Trim() == string.Empty)
            {
                IsSelected = false;
            }
            else if (_lookupTextBox != null && _lookupTextBox.Text.Trim() == string.Empty)
            {
                IsSelected = false;
            }
            else if (_lookupProc != null && _lookupProc.Text.Trim() == string.Empty)
            {
                IsSelected = false;
            }
            else
            {
                IsSelected = true;
            }
            OnTextChanged(e);
        }

        /// <summary>
        /// AccessibleName của textbox bên trong.
        /// </summary>
        [DefaultValue(null)]
        public string AccessibleName2
        {
            get
            {
                if (_textBox != null) return _textBox.AccessibleName;
                if (_vtextBox != null) return _vtextBox.AccessibleName;
                if (_lookupTextBox != null) return _lookupTextBox.AccessibleName;
                if (_lookupProc != null) return _lookupProc.AccessibleName;
                if (_numberTextBox != null) return _numberTextBox.AccessibleName;
                if (_dateTimePick != null) return _dateTimePick.AccessibleName;
                if (_dateTimeColor != null) return _dateTimeColor.AccessibleName;
                if (_checkBox != null) return _checkBox.AccessibleName;
                return null;
            }
            set
            {
                if (_textBox != null) _textBox.AccessibleName = value;
                if (_vtextBox != null) _vtextBox.AccessibleName = value;
                if (_lookupTextBox != null) _lookupTextBox.AccessibleName = value;
                if (_lookupProc != null) _lookupProc.AccessibleName = value;
                if (_numberTextBox != null) _numberTextBox.AccessibleName = value;
                if (_dateTimePick != null) _dateTimePick.AccessibleName = value;
                if (_dateTimeColor != null) _dateTimeColor.AccessibleName = value;
                if (_checkBox != null) _checkBox.AccessibleName = value;
            }
        }

        public override object ObjectValue
        {
            get
            {
                if (_textBox != null) return _textBox.Text.Trim();
                if (_vtextBox != null) return _vtextBox.Text.Trim();
                if (_lookupTextBox != null) return _lookupTextBox.Value;
                if (_lookupProc != null) return _lookupProc.Value;
                if (_numberTextBox != null) return _numberTextBox.Value;
                if (_dateTimePick != null) return _dateTimePick.Date;
                if (_dateTimeColor != null) return _dateTimeColor.Value;
                if (_checkBox != null) return _checkBox.Checked;
                return null;
            }
        }

        public override string StringValue
        {
            get
            {
                if (_textBox != null) return _textBox.Text.Trim();
                if (_vtextBox != null) return _vtextBox.Text.Trim();
                if (_lookupTextBox != null) return _lookupTextBox.Value.ToString();
                if (_lookupProc != null) return _lookupProc.Value.ToString();
                if (_numberTextBox != null) return _numberTextBox.Value.ToString(CultureInfo.InvariantCulture);
                if (_dateTimePick != null) return _dateTimePick.YYYYMMDD;
                if (_dateTimeColor != null) return ObjectAndString.ObjectToString(_dateTimeColor.Value, "yyyyMMdd");
                if (_checkBox != null) return _checkBox.Checked?"1":"0";
                return null;
            }
        }

        /// <summary>
        /// Control chứa giá trị.
        /// </summary>
        public Control ValueControl
        {
            get
            {
                if (_textBox != null) return _textBox;
                if (_vtextBox != null) return _vtextBox;
                if (_lookupTextBox != null) return _lookupTextBox;
                if (_lookupProc != null) return _lookupProc;
                if (_numberTextBox != null) return _numberTextBox;
                if (_dateTimePick != null) return _dateTimePick;
                if (_dateTimeColor != null) return _dateTimeColor;
                if (_checkBox != null) return _checkBox;
                return null;
            }
        }

        public Type ValueType
        {
            get
            {
                Type type = typeof (string);
                if (_numberTextBox != null) type = typeof(decimal);
                else if (_dateTimePick != null) type = typeof(DateTime);
                else if (_dateTimeColor != null) type = typeof(DateTime);
                else if (_checkBox != null) type = typeof(bool);
                return type;
            }
        }

        #region ==== EVENTS ====

        public event Action<object, Control> ValueChanged;
        protected virtual void OnValueChanged(object sender, Control control)
        {
            var handler = ValueChanged;
            if (handler != null) handler(sender, control);
        }

        public event ControlEventHandle V6LostFocus;
        protected virtual void OnV6LostFocus(object sender)
        {
            var handler = V6LostFocus;
            if (handler != null) handler(sender);
        }

        #endregion events

        /// <summary>
        /// Tự kiểm tra check để lấy. Phần này luôn trả về chuỗi dù có check hay không.
        /// </summary>
        public override string Query
        {
            get
            {
                var tL = string.IsNullOrEmpty(TableLabel) ? "" : TableLabel + ".";
                var sValue = StringValue;
                var result = "";

                var oper = Operator;
                if (oper == "start") oper = "like";

                if (sValue.Contains(","))
                {
                    string[] sss = sValue.Split(',');
                    foreach (string s in sss)
                    {
                        result += string.Format(" or {3}{0} {1} {2}", FieldName, oper, FormatValue(s.Trim(), ValueType), tL);
                    }

                    if (result.Length > 4)
                    {
                        result = result.Substring(4);
                        result = string.Format("({0})", result);
                    }
                }
                else
                {
                    //if (_lookupTextBox != null)
                    //{
                    //    result = _lookupTextBox.Query;
                    //}
                    //if (ObjectAndString.IsNumber(ObjectValue))
                    //{
                    //    result = string.Format("{3}{0} {1} {2}", FieldName, "=",
                    //        ObjectAndString.ObjectToDecimal(ObjectValue).ToString(CultureInfo.InvariantCulture), tL);
                    //}
                    //else
                    {
                        result = string.Format("{3}{0} {1} {2} ", FieldName, oper, FormatValue(StringValue, ValueType), tL);
                    }
                }
                return result;
            }
        }

        [DefaultValue(true)]
        public bool ShowName { get { return _vtextBox != null && _vtextBox.ShowName; }
            set
            {
                if (_vtextBox != null) _vtextBox.ShowName = value;
            }
        }

        public V6ColorTextBox AddTextBox()
        {
            _textBox = new V6ColorTextBox();
            _textBox.Name = "txt" + FieldName;
            _textBox.Location = new Point(comboBox1.Right + 5, 1);
            _textBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_textBox);
            Operators.Clear();
            Operators.Add("start");
            Operators.Add("like");
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add("is null");
            Operators.Add("is not null");
            Operator = "start";
            _textBox.Click += FilterLineDynamic_Click;
            _textBox.TextChanged += FilterLineDynamic_TextChanged;
            _textBox.Enter += FilterLineDynamic_GotFocus;
            _textBox.Leave += FilterLineDynamic_LostFocus;
            _textBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _textBox.KeyDown += FilterLineDynamic_KeyDown;
            return _textBox;
        }
        public V6VvarTextBox AddVvarTextBox(string vVar, string filter)
        {
            _vtextBox = new V6VvarTextBox
            {
                Name = "txtVvar" + FieldName,
                VVar = vVar,
                Location = new Point(comboBox1.Right + 5, 1),
                Size = new Size(Width - comboBox1.Right - 5, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                CheckNotEmpty = false,
                CheckOnLeave = false,
                ShowName = true,
            };

            _vtextBox.SetInitFilter(filter);

            Controls.Add(_vtextBox);
            Operators.Clear();
            Operators.Add("start");
            Operators.Add("like"); Operators.Add("="); Operators.Add("<>");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "start";
            _vtextBox.Click += FilterLineDynamic_Click;
            _vtextBox.TextChanged += FilterLineDynamic_TextChanged;
            _vtextBox.Enter += FilterLineDynamic_GotFocus;
            _vtextBox.Leave += FilterLineDynamic_LostFocus;
            _vtextBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _vtextBox.KeyDown += FilterLineDynamic_KeyDown;
            return _vtextBox;
        }

        public V6LookupTextBox AddLookupTextBox(string ma_dm, string filter,
            string value_field, string text_field, string brother, string neighbor)
        {
            _lookupTextBox = new V6LookupTextBox
            {
                Name = "txtLookup" + FieldName,
                //VVar = vVar,
                Ma_dm = ma_dm,
                ValueField = value_field,
                ShowTextField = text_field,
                BrotherFields = brother,
                NeighborFields = neighbor,

                Location = new Point(comboBox1.Right + 5, 1),
                Size = new Size(Width - comboBox1.Right - 5, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                CheckNotEmpty = false,
                CheckOnLeave = false,
            };

            _lookupTextBox.SetInitFilter(filter);

            Controls.Add(_lookupTextBox);
            Operators.Clear();
            Operators.Add("start");
            Operators.Add("like");
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add("is null");
            Operators.Add("is not null");
            Operator = "start";
            _lookupTextBox.Click += FilterLineDynamic_Click;
            _lookupTextBox.TextChanged += FilterLineDynamic_TextChanged;
            _lookupTextBox.Enter += FilterLineDynamic_GotFocus;
            _lookupTextBox.Leave += FilterLineDynamic_LostFocus;
            _lookupTextBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _lookupTextBox.KeyDown += FilterLineDynamic_KeyDown;
            return _lookupTextBox;
        }
        
        public V6LookupProc AddLookupProc(string ma_dm, string filter,
            string value_field, string text_field, string brother, string neighbor)
        {
            _lookupProc = new V6LookupProc
            {
                Name = "txtLookupProc" + FieldName,
                //VVar = vVar,
                Ma_dm = ma_dm,
                ValueField = value_field,
                ShowTextField = text_field,
                BrotherFields = brother,
                NeighborFields = neighbor,

                Location = new Point(comboBox1.Right + 5, 1),
                Size = new Size(Width - comboBox1.Right - 5, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                CheckNotEmpty = false,
                CheckOnLeave = false,
            };

            _lookupProc.SetInitFilter(filter);

            Controls.Add(_lookupProc);
            Operators.Clear();
            Operators.Add("start");
            Operators.Add("like");
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add("is null");
            Operators.Add("is not null");
            Operator = "start";
            _lookupProc.Click += FilterLineDynamic_Click;
            _lookupProc.TextChanged += FilterLineDynamic_TextChanged;
            _lookupProc.Enter += FilterLineDynamic_GotFocus;
            _lookupProc.Leave += FilterLineDynamic_LostFocus;
            _lookupProc.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _lookupProc.KeyDown += FilterLineDynamic_KeyDown;
            return _lookupProc;
        }

        public void AddNumberTextBox()
        {
            _numberTextBox = new V6NumberTextBox();
            _numberTextBox.Name = "num" + FieldName;
            _numberTextBox.Location = new Point(comboBox1.Right + 5, 1);
            _numberTextBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _numberTextBox.Anchor = AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            _numberTextBox.DecimalPlaces = 0;
            _numberTextBox.ThousandSymbol = V6Options.M_NUM_SEPARATOR[0];

            Controls.Add(_numberTextBox);
            Operators.Clear();
            Operators.Add("="); Operators.Add("<>"); Operators.Add(">"); Operators.Add("<");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "=";
            _numberTextBox.Click += FilterLineDynamic_Click;
            _numberTextBox.TextChanged += FilterLineDynamic_TextChanged;
            _numberTextBox.StringValueChange += (o, e) =>
            {
                OnValueChanged(this, _numberTextBox);
            };
            _numberTextBox.Enter += FilterLineDynamic_GotFocus;
            _numberTextBox.Leave += FilterLineDynamic_LostFocus;
            _numberTextBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _numberTextBox.KeyDown += FilterLineDynamic_KeyDown;
        }
        
        public void AddNumberYear()
        {
            _numberTextBox = new NumberYear();
            _numberTextBox.Name = "num" + FieldName;
            _numberTextBox.Location = new Point(comboBox1.Right + 5, 1);
            _numberTextBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _numberTextBox.Anchor = AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            _numberTextBox.DecimalPlaces = 0;
            _numberTextBox.ThousandSymbol = V6Options.M_NUM_SEPARATOR[0];

            Controls.Add(_numberTextBox);
            Operators.Clear();
            Operators.Add("="); Operators.Add("<>"); Operators.Add(">"); Operators.Add("<");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "=";
            _numberTextBox.Click += FilterLineDynamic_Click;
            _numberTextBox.TextChanged += FilterLineDynamic_TextChanged;
            _numberTextBox.StringValueChange += (o, e) =>
            {
                OnValueChanged(this, _numberTextBox);
            };
            _numberTextBox.Enter += FilterLineDynamic_GotFocus;
            _numberTextBox.Leave += FilterLineDynamic_LostFocus;
            _numberTextBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _numberTextBox.KeyDown += FilterLineDynamic_KeyDown;
        }
        
        public void AddNumberMonth()
        {
            _numberTextBox = new NumberMonth();
            _numberTextBox.Name = "num" + FieldName;
            _numberTextBox.Location = new Point(comboBox1.Right + 5, 1);
            _numberTextBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _numberTextBox.Anchor = AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            _numberTextBox.DecimalPlaces = 0;
            _numberTextBox.ThousandSymbol = V6Options.M_NUM_SEPARATOR[0];

            Controls.Add(_numberTextBox);
            Operators.Clear();
            Operators.Add("="); Operators.Add("<>"); Operators.Add(">"); Operators.Add("<");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "=";
            _numberTextBox.Click += FilterLineDynamic_Click;
            _numberTextBox.TextChanged += FilterLineDynamic_TextChanged;
            _numberTextBox.StringValueChange += (o, e) =>
            {
                OnValueChanged(this, _numberTextBox);
            };
            _numberTextBox.Enter += FilterLineDynamic_GotFocus;
            _numberTextBox.Leave += FilterLineDynamic_LostFocus;
            _numberTextBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _numberTextBox.KeyDown += FilterLineDynamic_KeyDown;
        }

        public void AddDateTimePick()
        {
            _dateTimePick = new V6DateTimePicker();
            _dateTimePick.Name = "date" + FieldName;
            _dateTimePick.Format = DateTimePickerFormat.Custom;
            _dateTimePick.CustomFormat = @"dd/MM/yyyy";
            _dateTimePick.Location = new Point(comboBox1.Right + 5, 1);
            _dateTimePick.Size = new Size(Width - comboBox1.Right - 5, 20);
            _dateTimePick.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_dateTimePick);
            Operators.Clear();
            Operators.Add("="); Operators.Add("<>"); Operators.Add(">"); Operators.Add("<");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "=";
            _dateTimePick.Click += FilterLineDynamic_Click;
            _dateTimePick.TextChanged += FilterLineDynamic_TextChanged;
            _dateTimePick.ValueChanged += (o, e) =>
            {
                OnValueChanged(this, _dateTimePick);
            };
            _dateTimePick.Enter += FilterLineDynamic_GotFocus;
            _dateTimePick.Leave += FilterLineDynamic_LostFocus;
            _dateTimePick.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _dateTimePick.KeyDown += FilterLineDynamic_KeyDown;
        }
        
        public void AddDateTimeColor()
        {
            _dateTimeColor = new V6DateTimeColor();
            _dateTimeColor.Name = "date" + FieldName;
            //_dateTimeColor.Format = DateTimePickerFormat.Custom;
            //_dateTimeColor.CustomFormat = @"dd/MM/yyyy";
            _dateTimeColor.Location = new Point(comboBox1.Right + 5, 1);
            _dateTimeColor.Size = new Size(Width - comboBox1.Right - 5, 20);
            _dateTimeColor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_dateTimeColor);
            Operators.Clear();
            Operators.Add("="); Operators.Add("<>"); Operators.Add(">"); Operators.Add("<");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "=";
            _dateTimeColor.Click += FilterLineDynamic_Click;
            _dateTimeColor.TextChanged += FilterLineDynamic_TextChanged;
            //_dateTimeColor.ValueChanged += (o, e) =>
            //{
            //    OnValueChanged(this, _dateTimeColor);
            //};
            _dateTimeColor.Enter += FilterLineDynamic_GotFocus;
            _dateTimeColor.Leave += FilterLineDynamic_LostFocus;
            _dateTimeColor.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _dateTimeColor.KeyDown += FilterLineDynamic_KeyDown;
        }

        public void AddCheckBox()
        {
            _checkBox = new V6CheckBox();
            _checkBox.Name = "chk" + FieldName;
            _checkBox.Text = "";
            _checkBox.Location = new Point(comboBox1.Right + 5, 1);
            _checkBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_checkBox);
            
            comboBox1.Visible = false;
            checkBox1.Checked = true;
            checkBox1.Visible = false;

            _checkBox.Click += FilterLineDynamic_Click;
            _checkBox.TextChanged += FilterLineDynamic_TextChanged;
            _checkBox.CheckedChanged += (o, e) =>
            {
                OnValueChanged(this, _checkBox);
            };
            _checkBox.Enter += FilterLineDynamic_GotFocus;
            _checkBox.Leave += FilterLineDynamic_LostFocus;
            _checkBox.KeyDown += FilterLineDynamic_KeyDown;
        }

        public V6FormButton AddButton(string text)
        {
            _button = new V6FormButton();
            _button.Name = "btn" + FieldName;
            _button.UseVisualStyleBackColor = true;
            _button.AutoSize = true;
            _button.Text = text;
            _button.Location = new Point(0, 0);
            Controls.Add(_button);
            
            label1.Visible = false;
            comboBox1.Visible = false;
            checkBox1.Visible = false;

            _button.Click += FilterLineDynamic_Click;
            //_button.TextChanged += FilterLineDynamic_TextChanged;
            //_button.CheckedChanged += (o, e) =>
            //{
            //    OnValueChanged(this, _button);
            //};
            //_button.Enter += FilterLineDynamic_GotFocus;
            //_button.Leave += FilterLineDynamic_LostFocus;
            //_button.KeyDown += FilterLineDynamic_KeyDown;
            return _button;
        }

        public override void SetValue(object value)
        {
            if (_textBox != null)
            {
                _textBox.Text = ObjectAndString.ObjectToString(value);
            }
            else if (_vtextBox != null)
            {
                _vtextBox.Text = ObjectAndString.ObjectToString(value);
            }
            else if (_lookupTextBox != null)
            {
                _lookupTextBox.SetValue(value);
            }
            else if (_lookupProc != null)
            {
                _lookupProc.SetValue(value);
            }
            else if (_numberTextBox != null)
            {
                _numberTextBox.Value = ObjectAndString.ObjectToDecimal(value);
            }
            else if (_dateTimePick != null)
            {
                if ("" + value == "M_NGAY_CT1") _dateTimePick.SetValue(V6Setting.M_ngay_ct1);
                else if ("" + value == "M_NGAY_CT2") _dateTimePick.SetValue(V6Setting.M_ngay_ct2);
                else if (!string.IsNullOrEmpty("" + value))
                    _dateTimePick.SetValue(ObjectAndString.ObjectToFullDateTime(value));
            }
            else if (_dateTimeColor != null)
            {
                if ("" + value == "M_NGAY_CT1") _dateTimeColor.Value = V6Setting.M_ngay_ct1;
                else if ("" + value == "M_NGAY_CT2") _dateTimeColor.Value = V6Setting.M_ngay_ct2;
                else if (!string.IsNullOrEmpty("" + value)) _dateTimeColor.Value = ObjectAndString.ObjectToDate(value);
            }
            else if (_checkBox != null)
            {
                _checkBox.Checked = ObjectAndString.ObjectToBool(value);
            }
        }

        public void SetLimitChars(string limitChars)
        {
            if (_textBox != null)
            {
                _textBox.LimitCharacters = limitChars;
            }
            else if (_vtextBox != null)
            {
                _vtextBox.LimitCharacters = limitChars;
            }
            else if (_lookupTextBox != null)
            {
                _lookupTextBox.LimitCharacters = limitChars;
            }
            else if (_lookupProc != null)
            {
                _lookupProc.LimitCharacters = limitChars;
            }
            else if (_numberTextBox != null)
            {
                _numberTextBox.LimitCharacters = limitChars;
            }
            else if (_dateTimePick != null)
            {
                //_dateTimePick;
            }
        }

        public void SetNotEmpty(bool b)
        {
            CheckNotEmpty = b;
            if (b)
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                if (_vtextBox != null)
                {
                    _vtextBox.CheckOnLeave = true;
                    _vtextBox.CheckNotEmpty = true;
                }
                if (_lookupTextBox != null)
                {
                    _lookupTextBox.CheckOnLeave = true;
                    _lookupTextBox.CheckNotEmpty = true;
                }
                if (_lookupProc != null)
                {
                    _lookupProc.CheckOnLeave = true;
                    _lookupProc.CheckNotEmpty = true;
                }
            }
            else
            {
                checkBox1.Enabled = true;
                if (_vtextBox != null)
                {
                    _vtextBox.CheckOnLeave = false;
                    _vtextBox.CheckNotEmpty = false;
                }
                if (_lookupTextBox != null)
                {
                    _lookupTextBox.CheckOnLeave = false;
                    _lookupTextBox.CheckNotEmpty = false;
                }
                if (_lookupProc != null)
                {
                    _lookupProc.CheckOnLeave = false;
                    _lookupProc.CheckNotEmpty = false;
                }
            }
        }

        
    }
}
