using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace H_Controls.Controls.Lookup.Filter
{
    public partial class FilterLineDynamic : FilterLineBase
    {
        private ColorTextBox _textBox;
        private NumberTextBox _numberTextBox;
        private DateTimePick _dateTimePick;

        public FilterLineDynamic()
        {
            InitializeComponent();
        }

        //public string FieldHeader
        //{
        //    get { return label1.Text; }
        //    set { label1.Text = value ?? ""; }
        //}

        public override string StringValue
        {
            get
            {
                if (_textBox != null) return _textBox.Text.Trim();
                if (_numberTextBox != null) return _numberTextBox.Value.ToString(CultureInfo.InvariantCulture);
                if (_dateTimePick != null) return _dateTimePick.Value.ToString("yyyyMMdd");
                
                return "";
            }
        }

        /// <summary>
        /// 
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
                    return string.Format("'{0}'", value.Replace("'", "''"));
                else if(Operator == "like")
                    return string.Format("'%{0}%'", value.Replace("'", "''"));
                else if(Operator == "start")
                    return string.Format("'{0}%'", value.Replace("'", "''"));
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
                return string.Format("'{0}'", value.Replace("'", "''"));
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
            _textBox = new ColorTextBox();
            _textBox.Size = new Size(100,20);
            _textBox.Location = new Point(comboBox1.Right + 5, 1);
            Controls.Add(_textBox);
            Operators.Clear();
            Operators.Add("like");
            Operators.Add("start"); Operators.Add("="); Operators.Add("<>");
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "like";
            _textBox.TextChanged+= delegate {
                IsSelected = true;
            };
        }
        public void AddNumberTextBox()
        {
            _numberTextBox = new NumberTextBox();
            _numberTextBox.Size = new Size(100, 20);
            _numberTextBox.Location = new Point(comboBox1.Right + 5, 1);
            _numberTextBox.DecimalPlaces = 0;
            _numberTextBox.ThousandSymbol = ' ';
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
            _dateTimePick = new DateTimePick();
            _dateTimePick.Size = new Size(200, 20);
            _dateTimePick.Format = DateTimePickerFormat.Custom;
            _dateTimePick.CustomFormat = @"dd/MM/yyyy";
            _dateTimePick.Location = new Point(comboBox1.Right + 5, 1);
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
    }
}
