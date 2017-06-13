using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6Controls;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ReportControls
{
    public partial class FilterLineDynamic : FilterLineBase
    {
        private V6ColorTextBox _textBox;
        public V6VvarTextBox _vtextBox;
        private V6NumberTextBox _numberTextBox;
        private V6DateTimePick _dateTimePick;
        public V6CheckBox _checkBox;
        public bool CheckNotEmpty;
        
        /// <summary>
        /// Sử dụng tùy lúc.
        /// </summary>
        public DefineInfo DefineInfo { get; set; }

        public FilterLineDynamic()
        {
            InitializeComponent();
        }

        [DefaultValue(null)]
        public string AccessibleName2
        {
            get
            {
                if (_textBox != null) return _textBox.AccessibleName;
                if (_vtextBox != null) return _vtextBox.AccessibleName;
                if (_numberTextBox != null) return _numberTextBox.AccessibleName;
                if (_dateTimePick != null) return _dateTimePick.AccessibleName;

                return null;
            }
            set
            {
                if (_textBox != null) _textBox.AccessibleName = value;
                if (_vtextBox != null) _vtextBox.AccessibleName = value;
                if (_numberTextBox != null) _numberTextBox.AccessibleName = value;
                if (_dateTimePick != null) _dateTimePick.AccessibleName = value;
            }
        }

        public override object ObjectValue
        {
            get
            {
                if (_textBox != null) return _textBox.Text.Trim();
                if (_vtextBox != null) return _vtextBox.Text.Trim();
                if (_numberTextBox != null) return _numberTextBox.Value;
                if (_dateTimePick != null) return _dateTimePick.Value;
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
                if (_numberTextBox != null) return _numberTextBox.Value.ToString(CultureInfo.InvariantCulture);
                if (_dateTimePick != null) return _dateTimePick.Value.ToString("yyyyMMdd");
                if (_checkBox != null) return _checkBox.Checked?"1":"0";
                return "";
            }
        }

        /// <summary>
        /// Định dạng lại giá trị cho phù hợp với query.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string FormatValue(string value)
        {
            Type type = typeof(string);
            
            if (_numberTextBox != null) type = typeof (decimal);
            else if (_dateTimePick != null) type = typeof (DateTime);

            if (type == typeof (string))
            {
                if (",=,<>,>,>=,<,<=,".Contains("," + Operator + ","))
                    return string.Format("N'{0}'", value.Replace("'", "''"));
                else if(Operator == "like")
                    return string.Format("N'%{0}%'", value.Replace("'", "''"));
                else if(Operator == "start")
                    return string.Format("N'{0}%'", value.Replace("'", "''"));
                return "";
            }
            else if (type == typeof (decimal))
            {
                return value;
            }
            else if (type == typeof (DateTime))
            {
                return string.Format("'{0}'", value);
            }
            else
            {
                return string.Format("N'{0}'", value.Replace("'", "''"));
            }
        }
        /// <summary>
        /// Tự kiểm tra check để lấy. Phần này luôn trả về chuỗi dù có check hay không.
        /// </summary>
        public override string Query
        {
            get
            {
                var sValue = StringValue;
                var result = "";

                var oper = Operator;
                if (oper == "start") oper = "like";

                if (sValue.Contains(","))
                {
                    string[] sss = sValue.Split(',');
                    foreach (string s in sss)
                    {
                        result += string.Format(" or {0} {1} {2}", FieldName, oper, FormatValue(s));
                    }

                    if (result.Length > 4)
                    {
                        result = result.Substring(4);
                        result = string.Format("({0})", result);
                    }
                }
                else
                {
                    result = string.Format("{0} {1} {2} ", FieldName, oper, FormatValue(StringValue));
                }
                return result;
            }
        }
        
        public void AddTextBox()
        {
            _textBox = new V6ColorTextBox();
            _textBox.Location = new Point(comboBox1.Right + 5, 1);
            _textBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_textBox);
            Operators.Clear();
            Operators.Add("start");
            Operators.Add("like"); Operators.Add("="); Operators.Add("<>");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "start";
            _textBox.TextChanged+= delegate {
                IsSelected = true;
            };
        }
        public V6VvarTextBox AddVvarTextBox(string vVar, string filter)
        {
            _vtextBox = new V6VvarTextBox
            {
                VVar = vVar,
                Location = new Point(comboBox1.Right + 5, 1),
                Size = new Size(Width - comboBox1.Right - 5, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                CheckNotEmpty = false,
                CheckOnLeave = false,
            };

            _vtextBox.SetInitFilter(filter);

            Controls.Add(_vtextBox);
            Operators.Clear();
            Operators.Add("start");
            Operators.Add("like"); Operators.Add("="); Operators.Add("<>");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "start";
            _vtextBox.TextChanged+= delegate {
                IsSelected = true;
            };
            return _vtextBox;
        }
        public void AddNumberTextBox()
        {
            _numberTextBox = new V6NumberTextBox();
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
            _numberTextBox.TextChanged += delegate
            {
                IsSelected = true;
            };
        }
        public void AddDateTimePick()
        {
            _dateTimePick = new V6DateTimePick();
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
            _dateTimePick.TextChanged += delegate
            {
                IsSelected = true;
            };
        }
        public void AddCheckBox()
        {
            _checkBox = new V6CheckBox();
            _checkBox.Text = "";
            _checkBox.Location = new Point(comboBox1.Right + 5, 1);
            _checkBox.Size = new Size(Width - comboBox1.Right - 5, 20);
            _checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_checkBox);
            
            comboBox1.Visible = false;
            checkBox1.Visible = false;
            
        }

        public void SetValue(string stringValue)
        {
            if (_textBox != null)
            {
                _textBox.Text = stringValue;
            }
            else if (_vtextBox != null)
            {
                _vtextBox.Text = stringValue;
            }
            else if (_numberTextBox != null)
            {
                _numberTextBox.Value = ObjectAndString.ObjectToDecimal(stringValue);
            }
            else if (_dateTimePick != null)
            {
                if (stringValue == "M_NGAY_CT1") _dateTimePick.Value = V6Setting.M_ngay_ct1;
                else if (stringValue == "M_NGAY_CT2") _dateTimePick.Value = V6Setting.M_ngay_ct2;
                else if (!string.IsNullOrEmpty(stringValue)) _dateTimePick.Value = ObjectAndString.ObjectToFullDateTime(stringValue);
            }
            else if (_checkBox != null)
            {
                _checkBox.Checked = stringValue == "1";
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
            }
            else
            {
                checkBox1.Enabled = true;
                if (_vtextBox != null)
                {
                    _vtextBox.CheckOnLeave = false;
                    _vtextBox.CheckNotEmpty = false;
                }
            }
        }
    }
}
