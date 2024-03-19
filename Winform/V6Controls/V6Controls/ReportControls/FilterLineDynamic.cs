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
        public ComboBox _comboBox;
        public V6VvarTextBox _vtextBox;
        public V6LookupTextBox _lookupTextBox;
        public V6LookupProc _lookupProc;
        public V6NumberTextBox _numberTextBox;
        public V6DateTimeColor _dateTimeColor;
        public V6DateTimePicker _dateTimePick;
        public V6CheckBox _checkBox;
        public V6FormButton _button;
        public V6QRTextBox _qrTextBox;
        public RichTextBox _richTextBox;

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
            if (_comboBox != null && _comboBox.Text.Trim() == string.Empty)
            {
                IsSelected = false;
            }
            else if (_richTextBox != null && _richTextBox.Text.Trim() == string.Empty)
            {
                IsSelected = false;
            }
            else if (_qrTextBox != null && _qrTextBox.Text.Trim() == string.Empty)
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
            else if (_numberTextBox != null && _numberTextBox.Value == 0)
            {
                IsSelected = false;
            }
            else if (_dateTimeColor != null && _dateTimeColor.Value == null)
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
                if (_comboBox != null) return _comboBox.AccessibleName;
                if (_richTextBox != null) return _richTextBox.AccessibleName;
                if (_qrTextBox != null) return _qrTextBox.AccessibleName;
                if (_vtextBox != null) return _vtextBox.AccessibleName;
                if (_lookupTextBox != null) return _lookupTextBox.AccessibleName;
                if (_lookupProc != null) return _lookupProc.AccessibleName;
                if (_numberTextBox != null) return _numberTextBox.AccessibleName;
                if (_dateTimePick != null) return _dateTimePick.AccessibleName;
                if (_dateTimeColor != null) return _dateTimeColor.AccessibleName;
                if (_checkBox != null) return _checkBox.AccessibleName;
                if (_button != null) return _button.AccessibleName;
                return null;
            }
            set
            {
                if (_textBox != null) _textBox.AccessibleName = value;
                if (_comboBox != null) _comboBox.AccessibleName = value;
                if (_richTextBox != null) _richTextBox.AccessibleName = value;
                if (_qrTextBox != null) _qrTextBox.AccessibleName = value;
                if (_vtextBox != null) _vtextBox.AccessibleName = value;
                if (_lookupTextBox != null) _lookupTextBox.AccessibleName = value;
                if (_lookupProc != null) _lookupProc.AccessibleName = value;
                if (_numberTextBox != null) _numberTextBox.AccessibleName = value;
                if (_dateTimePick != null) _dateTimePick.AccessibleName = value;
                if (_dateTimeColor != null) _dateTimeColor.AccessibleName = value;
                if (_checkBox != null) _checkBox.AccessibleName = value;
                if (_button != null) _button.AccessibleName = value;
            }
        }

        public override Control InputControl
        {
            get
            {
                if (_textBox != null) return _textBox;
                if (_comboBox != null) return _comboBox;
                if (_richTextBox != null) return _richTextBox;
                if (_qrTextBox != null) return _qrTextBox;
                if (_vtextBox != null) return _vtextBox;
                if (_lookupTextBox != null) return _lookupTextBox;
                if (_lookupProc != null) return _lookupProc;
                if (_numberTextBox != null) return _numberTextBox;
                if (_dateTimePick != null) return _dateTimePick;
                if (_dateTimeColor != null) return _dateTimeColor;
                if (_checkBox != null) return _checkBox;
                if (_button != null) return _button;
                return null;
            }
        }

        public override object ObjectValue
        {
            get
            {
                if (_textBox != null) return _textBox.Text.Trim();
                if (_comboBox != null) return _comboBox.Text.Trim();
                if (_richTextBox != null) return _richTextBox.Text.Trim();
                if (_qrTextBox != null) return _qrTextBox.Text.Trim();
                if (_vtextBox != null) return _vtextBox.Text.Trim();
                if (_lookupTextBox != null) return _lookupTextBox.Value;
                if (_lookupProc != null) return _lookupProc.Value;
                if (_numberTextBox != null) return _numberTextBox.Value;
                if (_dateTimePick != null) return _dateTimePick.Date;
                if (_dateTimeColor != null) return _dateTimeColor.Value;
                if (_checkBox != null) return _checkBox.Checked;
                if (_button != null) return _button.Text;
                return null;
            }
        }

        public override string StringValue
        {
            get
            {
                if (_textBox != null) return _textBox.Text.Trim();
                if (_comboBox != null) return _comboBox.Text.Trim();
                if (_richTextBox != null) return _richTextBox.Text.Trim();
                if (_qrTextBox != null) return _qrTextBox.Text.Trim();
                if (_vtextBox != null) return _vtextBox.Text.Trim();
                if (_lookupTextBox != null) return _lookupTextBox.Value == null ? null : _lookupTextBox.Value.ToString();
                if (_lookupProc != null) return _lookupProc.Value.ToString();
                if (_numberTextBox != null) return _numberTextBox.Value.ToString(CultureInfo.InvariantCulture);
                if (_dateTimePick != null) return _dateTimePick.YYYYMMDD;
                if (_dateTimeColor != null) return ObjectAndString.ObjectToString(_dateTimeColor.Value, "yyyyMMdd");
                if (_checkBox != null) return _checkBox.Checked?"1":"0";
                if (_button != null) return _button.Text;
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
                if (_comboBox != null) return _comboBox;
                if (_richTextBox != null) return _richTextBox;
                if (_qrTextBox != null) return _qrTextBox;
                if (_vtextBox != null) return _vtextBox;
                if (_lookupTextBox != null) return _lookupTextBox;
                if (_lookupProc != null) return _lookupProc;
                if (_numberTextBox != null) return _numberTextBox;
                if (_dateTimePick != null) return _dateTimePick;
                if (_dateTimeColor != null) return _dateTimeColor;
                if (_checkBox != null) return _checkBox;
                if (_button != null) return _button;
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
        /// Tự kiểm tra check để lấy. Phần này luôn trả về chuỗi dù có check hay không. Field = value and Field = value2.
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

                if (_vtextBox != null && (sValue.Contains(",") || sValue.Contains("|")))
                {
                    string[] sss = sValue.Split(',', '|');
                    foreach (string s in sss)
                    {
                        if (oper == "<>")
                        {
                            result += string.Format(" and {3}{0} {1} {2}", FieldName, oper, FormatValue(s.Trim(), ValueType), tL);
                        }
                        else
                        {
                            result += string.Format(" or {3}{0} {1} {2}", FieldName, oper, FormatValue(s.Trim(), ValueType), tL);
                        }
                    }

                    if (result.Length > 4)
                    {
                        result = result.Substring(4);
                        result = string.Format("({0})", result);
                    }
                }
                else
                {
                    result = string.Format("{3}{0} {1} {2} ", FieldName, oper, FormatValue(StringValue, ValueType), tL);
                }
                return result;
            }
        }

        private string Query_R(string tableLabel = null)
        {
            var tL = string.IsNullOrEmpty(tableLabel) ? "" : tableLabel + ".";
            var sValue = StringValue;
            var result = "";

            var oper = Operator;
            if (oper == "start") oper = "like";

            if (sValue.Contains(",") || sValue.Contains("|"))
            {
                string[] sss = sValue.Split(',', '|');
                foreach (string s in sss) // !!!!! thêm kiểu   and (ma_kho in (select alkho where xxxxx))
                {
                    if (oper == "<>")
                    {
                        result += string.Format(" and {3}{0} {1} {2}", _vtextBox.LookupInfo.vValue, oper, FormatValue(s.Trim(), typeof(string)), tL);
                    }
                    else
                    {
                        result += string.Format(" or {3}{0} {1} {2}", _vtextBox.LookupInfo.vValue, oper, FormatValue(s.Trim(), typeof(string)), tL);
                    }
                }

                if (result.Length > 4)
                {
                    result = result.Substring(4);
                    result = string.Format("({3} in (select {2} from {1} where {0}))",
                        result, _vtextBox.LookupInfo.TableName, _vtextBox.LookupInfo.vValue, FieldName);
                }
            }
            else
            {
                result = string.Format("{3}{0} {1} {2}", FieldName, oper, FormatValue(StringValue, typeof(string)), tL);
            }
            return result;
        }

        public override string GetQuery_R(string tableLabel = null)
        {
            if (_vtextBox != null && (StringValue.Contains(",") || StringValue.Contains("|")))
                return GetQuery0_R(tableLabel);
            return base.GetQuery(tableLabel);
        }

        /// <summary>
        /// trường hợp đặc biệt vvar danh sách ,,,
        /// </summary>
        /// <param name="tableLabel"></param>
        /// <returns></returns>
        public string GetQuery0_R(string tableLabel = null)
        {
            var tL = string.IsNullOrEmpty(tableLabel) ? "" : tableLabel + ".";
            var result = string.Format("{0}{1}", tL, Query_R(tableLabel));
            return result;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="comboBoxType">1 DropDownList, 0 DropDown</param>
        /// <returns></returns>
        public ComboBox AddComboBox(string items, string comboBoxType)
        {
            _comboBox = new ComboBox();
            _comboBox.Name = "txt" + FieldName;
            _comboBox.Location = new Point(comboBox1.Right + 5, 1);
            _comboBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _comboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _comboBox.Items.AddRange(ObjectAndString.SplitString(items, false));
            _comboBox.DropDownStyle = comboBoxType == "1" ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDown;
            Controls.Add(_comboBox);
            Operators.Clear();
            Operators.Add("start");
            Operators.Add("like");
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add("is null");
            Operators.Add("is not null");
            Operator = "start";
            _comboBox.Click += FilterLineDynamic_Click;
            _comboBox.TextChanged += FilterLineDynamic_TextChanged;
            _comboBox.Enter += FilterLineDynamic_GotFocus;
            _comboBox.Leave += FilterLineDynamic_LostFocus;
            //_comboBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _comboBox.KeyDown += FilterLineDynamic_KeyDown;
            return _comboBox;
        }
        public RichTextBox AddRichTextBox()
        {
            //checkBox1.Visible = false;
            //label1.Visible = false;
            comboBox1.Visible = false;
            
            _richTextBox = new RichTextBox();
            _richTextBox.Name = "txt" + FieldName;
            _richTextBox.Location = new Point(checkBox1.Left, checkBox1.Bottom + 5);
            _richTextBox.Size = new Size(Width - 5, 60);
            _richTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_richTextBox);
            Height = _richTextBox.Height + _richTextBox.Top;
            //Operators.Clear();
            //Operators.Add("start");
            //Operators.Add("like");
            //Operators.Add("=");
            //Operators.Add("<>");
            //Operators.Add("is null");
            //Operators.Add("is not null");
            //Operator = "start";
            _richTextBox.Click += FilterLineDynamic_Click;
            _richTextBox.TextChanged += FilterLineDynamic_TextChanged;
            _richTextBox.Enter += FilterLineDynamic_GotFocus;
            _richTextBox.Leave += FilterLineDynamic_LostFocus;
            //_richTextBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _richTextBox.KeyDown += FilterLineDynamic_KeyDown;
            return _richTextBox;
        }
        public V6QRTextBox AddQRTextBox()
        {
            _qrTextBox = new V6QRTextBox();
            _qrTextBox.Name = "txt" + FieldName;
            _qrTextBox.Location = new Point(comboBox1.Right + 5, 1);
            _qrTextBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _qrTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_qrTextBox);
            Operators.Clear();
            Operators.Add("start");
            Operators.Add("like");
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add("is null");
            Operators.Add("is not null");
            Operator = "start";
            _qrTextBox.Click += FilterLineDynamic_Click;
            _qrTextBox.TextChanged += FilterLineDynamic_TextChanged;
            _qrTextBox.Enter += FilterLineDynamic_GotFocus;
            _qrTextBox.Leave += FilterLineDynamic_LostFocus;
            _qrTextBox.V6LostFocus += FilterLineDynamic_V6LostFocus;
            _qrTextBox.KeyDown += FilterLineDynamic_KeyDown;
            return _qrTextBox;
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
            Operators.Add("like");
            Operators.Add("=");
            Operators.Add("<>");
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
            Operators.Add("="); Operators.Add("<>");
            Operators.Add(">");
            Operators.Add(">=");
            Operators.Add("<");
            Operators.Add("<=");
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
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add(">");
            Operators.Add(">=");
            Operators.Add("<");
            Operators.Add("<=");
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
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add(">");
            Operators.Add(">=");
            Operators.Add("<");
            Operators.Add("<=");
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
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add(">");
            Operators.Add(">=");
            Operators.Add("<");
            Operators.Add("<=");
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
            _dateTimeColor.Size = new Size(Width - comboBox1.Right - 30, 20);
            _dateTimeColor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_dateTimeColor);
            Operators.Clear();
            Operators.Add("=");
            Operators.Add("<>");
            Operators.Add(">");
            Operators.Add(">=");
            Operators.Add("<");
            Operators.Add("<=");
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

            DateSelectButton dsb = new DateSelectButton();
            Controls.Add(dsb);
            dsb.ReferenceControl = _dateTimeColor;
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
            else if (_comboBox != null)
            {
                SetControlValue(_comboBox, value);
            }
            else if (_richTextBox != null)
            {
                _richTextBox.Text = ObjectAndString.ObjectToString(value);
            }
            else if (_qrTextBox != null)
            {
                _qrTextBox.Text = ObjectAndString.ObjectToString(value);
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
            else if (_button != null)
            {
                _button.Text = value.ToString();
            }
        }

        public void SetLimitChars(string limitChars)
        {
            if (_textBox != null)
            {
                _textBox.LimitCharacters = limitChars;
            }
            //else if (_comboBox != null)
            //{
            //    _comboBox.LimitCharacters = limitChars;
            //}
            else if (_qrTextBox != null)
            {
                _qrTextBox.LimitCharacters = limitChars;
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

        public void SetMaxLength(int maxLength)
        {
            if (_textBox != null)
            {
                _textBox.MaxLength = maxLength;
            }
            else if (_comboBox != null)
            {
                _comboBox.MaxLength = maxLength;
            }
            else if (_qrTextBox != null)
            {
                _qrTextBox.MaxLength = maxLength;
            }
            else if (_vtextBox != null)
            {
                _vtextBox.MaxLength = maxLength;
            }
            else if (_lookupTextBox != null)
            {
                _lookupTextBox.MaxLength = maxLength;
            }
            else if (_lookupProc != null)
            {
                _lookupProc.MaxLength = maxLength;
            }
            else if (_numberTextBox != null)
            {
                _numberTextBox.MaxNumLength = maxLength;
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

        internal void SetReadonly(bool b)
        {
            if (b)
            {
                if (_vtextBox != null)
                {
                    _vtextBox.CheckOnLeave = false;
                    _vtextBox.ReadOnlyTag();
                }
                if (_lookupTextBox != null)
                {
                    _lookupTextBox.CheckOnLeave = false;
                    _lookupTextBox.ReadOnlyTag();
                }
                if (_lookupProc != null)
                {
                    _lookupProc.CheckOnLeave = false;
                    _lookupProc.ReadOnlyTag();
                }

                if (_textBox != null)
                {
                    _textBox.ReadOnlyTag();
                }
                if (_comboBox != null)
                {
                    _comboBox.ReadOnlyTag();
                }
                if (_richTextBox != null)
                {
                    _richTextBox.ReadOnlyTag();
                }
                if (_qrTextBox != null)
                {
                    _qrTextBox.ReadOnlyTag();
                }
                if (_numberTextBox != null)
                {
                    _numberTextBox.ReadOnlyTag();
                }
                if (_dateTimeColor != null)
                {
                    _dateTimeColor.ReadOnlyTag();
                }
                if (_dateTimePick != null)
                {
                    _dateTimePick.ReadOnlyTag();
                }
                if (_checkBox != null)
                {
                    _checkBox.ReadOnlyTag();
                }
                if (_button != null)
                {
                    _button.ReadOnlyTag();
                }
                
            }
            else
            {
                if (_vtextBox != null)
                {
                    _vtextBox.CheckOnLeave = true;
                    _vtextBox.ReadOnlyTag(false);
                }
                if (_lookupTextBox != null)
                {
                    _lookupTextBox.CheckOnLeave = true;
                    _lookupTextBox.ReadOnlyTag(false);
                }
                if (_lookupProc != null)
                {
                    _lookupProc.CheckOnLeave = true;
                    _lookupProc.ReadOnlyTag(false);
                }

                if (_textBox != null)
                {
                    _textBox.ReadOnlyTag(false);
                }
                if (_comboBox != null)
                {
                    _comboBox.ReadOnlyTag(false);
                }
                if (_richTextBox != null)
                {
                    _richTextBox.ReadOnlyTag(false);
                }
                if (_qrTextBox != null)
                {
                    _qrTextBox.ReadOnlyTag(false);
                }
                if (_numberTextBox != null)
                {
                    _numberTextBox.ReadOnlyTag(false);
                }
                if (_dateTimeColor != null)
                {
                    _dateTimeColor.ReadOnlyTag(false);
                }
                if (_dateTimePick != null)
                {
                    _dateTimePick.ReadOnlyTag(false);
                }
                if (_checkBox != null)
                {
                    _checkBox.ReadOnlyTag(false);
                }
                if (_button != null)
                {
                    _button.ReadOnlyTag(false);
                }
            }
        }
    }
}
