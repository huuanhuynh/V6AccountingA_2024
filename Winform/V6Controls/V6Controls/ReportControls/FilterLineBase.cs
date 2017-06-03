﻿using System.ComponentModel;
using System.Windows.Forms;

namespace V6ReportControls
{
    public partial class FilterLineBase : UserControl
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
            set { checkBox1.Checked = value; }
        }

        public ComboBox.ObjectCollection Operators
        {
            get { return comboBox1.Items; }
        }

        public string FieldCaption
        {
            get { return label1.Text; }
            set { label1.Text = value ?? ""; }
        }

        public string FieldName
        {
            get; set;
        }

        [Browsable(true)]
        [Description("Nhãn hiển thị, cẩn thận nhầm data.")]
        public override string Text
        {
            get { return label1.Text; } set { label1.Text = value??""; }
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

        public virtual object ObjectValue
        {
            get { return "N'objectbase'"; }
        }

        public virtual string StringValue
        {
            get{ return "N'base'"; }
        }

        /// <summary>
        /// Nếu không check trả về rỗng.
        /// </summary>
        public virtual string StringValueCheck
        {
            get { return IsSelected ? StringValue : ""; }
        }

        /// <summary>
        /// Tự kiểm tra check để lấy. Phần này luôn trả về chuỗi dù có check hay không.
        /// </summary>
        public virtual string Query
        {
            get
            {
                var oper = Operator;
                if (oper == "start") oper = "like";
                var result = string.Format("{0} {1} {2}", FieldName, oper, StringValue);
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

    }
}
