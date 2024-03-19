using System;
using System.ComponentModel;
using System.Windows.Forms;
using V6Controls.Forms;

namespace V6ReportControls
{
    public partial class FilterLineBase : V6Control
    {
        
        public FilterLineBase()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        [DefaultValue(false)]
        public virtual bool IsSelected
        {
            get { return checkBox1.Checked; }
            set
            {
                if (checkBox1.Enabled || value)
                {
                    checkBox1.Checked = value;
                }
            }
        }

        public ComboBox.ObjectCollection Operators
        {
            get { return comboBox1.Items; }
        }

        /// <summary>
        /// Text của label.
        /// </summary>
        public virtual string Caption
        {
            get { return label1.Text; }
            set { label1.Text = value ?? ""; }
        }

        public string FieldName
        {
            get; set;
        }

        /// <summary>
        /// Nhãn đánh dấu tên bảng, dùng tùy lúc
        /// </summary>
        [DefaultValue(null)]
        public string TableLabel
        {
            get; set;
        }

        [Browsable(true)]
        [Description("Nhãn hiển thị, cẩn thận nhầm data.")]
        public override string Text
        {
            get { return Caption; } set { Caption = value??""; }
        }

        /// <summary>
        /// Dấu so sánh sql, có thêm start : like 'value%'
        /// </summary>
        [DefaultValue("start")]
        public string Operator
        {
            get
            {
                return comboBox1.SelectedItem.ToString();
            }
            set
            {
                switch (value.ToLower())
                {
                    case "=":
                    case "start":
                    case "like":
                    case "<>":
                    case ">":
                    case ">=":
                    case "<":
                    case "<=":
                    case "is null":
                    case "is not null":
                        comboBox1.SelectedItem = value.ToLower();
                        break;
                }
            }
        }

        public virtual Control InputControl
        {
            get
            {
                return null;
            }
        }

        public virtual object ObjectValue
        {
            get { return "N'objectbase'"; }
        }

        public virtual string StringValue
        {
            get{ return "N'base'"; }
        }

        public virtual void SetValue(object value)
        {
            throw new NotImplementedException("No override!");
        }

        /// <summary>
        /// Định dạng lại giá trị cho phù hợp với query.
        /// </summary>
        /// <param name="value">StringValue</param>
        /// <param name="type">Kiểu dữ liệu typeof(string) hoặc typeof(decimal) hoặc typeof(DateTime)</param>
        /// <returns></returns>
        public string FormatValue(string value, Type type)
        {
            //Type type = typeof(string);

            //if (_numberTextBox != null) type = typeof(decimal);
            //else if (_dateTimePick != null) type = typeof(DateTime);
            //else if (_dateTimeColor != null) type = typeof(DateTime);

            if (type == typeof(string))
            {
                if (",=,<>,>,>=,<,<=,".Contains("," + Operator + ","))
                    return string.Format("N'{0}'", value.Replace("'", "''"));
                else if (Operator == "like")
                    return string.Format("N'%{0}%'", value.Replace("'", "''"));
                else if (Operator == "start")
                    return string.Format("N'{0}%'", value.Replace("'", "''"));
                return "";
            }
            else if (type == typeof(decimal))
            {
                return value;
            }
            else if (type == typeof(DateTime))
            {
                return string.Format("'{0}'", value);
            }
            else if (type == typeof(bool))
            {
                return value.Replace("'", "''");
            }
            else
            {
                return string.Format("N'{0}'", value.Replace("'", "''"));
            }
        }

        /// <summary>
        /// Nếu không check trả về rỗng.
        /// </summary>
        public virtual string StringValueCheck
        {
            get { return IsSelected ? StringValue : ""; }
        }

        /// <summary>
        /// [Field] =(oper) value. Tự kiểm tra check để lấy. Phần này luôn trả về chuỗi dù có check hay không.
        /// </summary>
        public virtual string Query
        {
            get
            {
                var oper = Operator;
                if (oper == "start") oper = "like";
                var result = string.Format("[{0}] {1} {2}", FieldName, oper, FormatValue(StringValue, ObjectValue.GetType()));
                return result;
            }
        }
        /// <summary>
        /// Có thêm tableLabel vd: a.ma_vt = 'abc'
        /// </summary>
        /// <param name="tableLabel"></param>
        /// <returns></returns>
        public virtual string GetQuery(string tableLabel = null)
        {
            var tL = string.IsNullOrEmpty(tableLabel) ? "" : tableLabel + ".";
            var result = string.Format("{0}{1}", tL, Query);
            return result;
        }

        public virtual string GetQuery_R(string tableLabel = null)
        {
            return GetQuery(tableLabel);
        }

        /// <summary>
        /// Trả về query nếu được check
        /// </summary>
        public string QueryCheck
        {
            get
            {
                return GetQueryCheck();
            }
        }

        public string GetQueryCheck(string tableLabel = null)
        {
            if (IsSelected) return GetQuery(tableLabel);
            return "";
        }

        private void label1_Click(object sender, MouseEventArgs e)
        {
            if (checkBox1.Enabled && e.Button == MouseButtons.Left)
            checkBox1.Checked = !checkBox1.Checked;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void comboBox1_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void FilterLineBase_Load(object sender, EventArgs e)
        {
            this.Name = "line" + FieldName;
        }
    }
}
