﻿using System.ComponentModel;
using V6Controls;

namespace V6ReportControls
{
    public partial class FilterLineVvarTextBox : FilterLineBase
    {
        public FilterLineVvarTextBox()
        {
            InitializeComponent();
            Operators.Add("start");
            Operator = "start";
            v6VvarTextBox1.TextChanged += delegate {
                if (v6VvarTextBox1.Text.Trim() == string.Empty)
                {
                    IsSelected = false;
                }
                else
                {
                    IsSelected = true;
                }
            };
        }

        [DefaultValue(null)]
        public string AccessibleName2
        {
            get
            {
                if (v6VvarTextBox1 != null) return v6VvarTextBox1.AccessibleName;
                return null;
            }
            set
            {
                if (v6VvarTextBox1 != null) v6VvarTextBox1.AccessibleName = value;
            }
        }

        public V6VvarTextBox VvarTextBox
        {
            get { return v6VvarTextBox1; }
        }

        public string Vvar
        {
            get { return v6VvarTextBox1.VVar; }
            set { v6VvarTextBox1.VVar = value; }
        }
        
        /// <summary>
        /// Giá trị của textbox đã trim()
        /// </summary>
        public override string StringValue
        {
            get
            {
                return v6VvarTextBox1.Text.Trim();
            }
        }

        public override object ObjectValue
        {
            get { return StringValue; }
        }

        private string FormatValue(string value)
        {
            if (",=,<>,>,>=,<,<=,".Contains("," + Operator + ","))
                return string.Format("N'{0}'", value.Replace("'", "''"));
            else if (Operator == "like")
                return string.Format("N'%{0}%'", value.Replace("'", "''"));
            else if (Operator == "start")
                return string.Format("N'{0}%'", value.Replace("'", "''"));
            return "";
        }
        public override string Query
        {
            get
            {
                return GetQuery0();
            }
        }

        /// <summary>
        /// Lấy câu query theo dữ liệu nhập vào.
        /// </summary>
        /// <param name="tableLabel">tên bảng ví dụ: [TableA] hoặc a</param>
        /// <returns></returns>
        public override string GetQuery(string tableLabel = null)
        {
            if(StringValue.Contains(","))
                return Query;
            return base.GetQuery(tableLabel);
        }

        private string GetQuery0(string tableLabel = null)
        {
            var tL = string.IsNullOrEmpty(tableLabel) ? "" : tableLabel + ".";
            var sValue = StringValue;
            var result = "";

            var oper = Operator;
            if (oper == "start") oper = "like";

            if (sValue.Contains(","))
            {
                string[] sss = sValue.Split(',');
                foreach (string s in sss)
                {
                    if (oper == "<>")
                    {
                        result += string.Format(" and {3}{0} {1} {2}", FieldName, oper, FormatValue(s), tL);
                    }
                    else
                    {
                        result += string.Format(" or {3}{0} {1} {2}", FieldName, oper, FormatValue(s), tL);
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
                result = string.Format("{3}{0} {1} {2}", FieldName, oper, FormatValue(StringValue), tL);
            }
            return result;
        }

    }
}
