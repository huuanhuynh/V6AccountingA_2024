﻿using System.ComponentModel;
using System.Windows.Forms;
using V6Controls;

namespace V6ReportControls
{
    public partial class FilterLineLookupProc : FilterLineBase
    {
        public FilterLineLookupProc()
        {
            InitializeComponent();
            Operators.Add("start");
            Operator = "start";
            lookupProc1.TextChanged += delegate {
                if (lookupProc1.Text.Trim() == string.Empty)
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
                if (lookupProc1 != null) return lookupProc1.AccessibleName;
                return null;
            }
            set
            {
                if (lookupProc1 != null) lookupProc1.AccessibleName = value;
            }
        }

        public V6LookupProc LookupProc
        {
            get { return lookupProc1; }
        }

        public string MA_DM
        {
            get { return lookupProc1.Ma_dm; }
            set { lookupProc1.Ma_dm = value; }
        }

        [Category("V6")]
        [Description("Tên trường lấy dữ liệu.")]
        [DefaultValue(null)]
        public string ValueField
        {
            get { return lookupProc1.ValueField; }
            set { lookupProc1.ValueField = value; }
        }

        [Category("V6")]
        [Description("Tên trường dữ liệu hiển thị. Bị đè bởi LookupInfo.F_NAME")]
        [DefaultValue(null)]
        public string ShowTextField
        {
            get { return lookupProc1.ShowTextField; }
            set { lookupProc1.ShowTextField = value; }
        }

        /// <summary>
        /// Giá trị của textbox đã trim()
        /// </summary>
        public override string StringValue
        {
            get
            {
                return lookupProc1.Text.Trim();
            }
        }

        public override Control InputControl
        {
            get
            {
                return lookupProc1;
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
                        result += string.Format(" and {3}{0} {1} {2}", FieldName, oper, FormatValue(s.Trim()), tL);
                    }
                    else
                    {
                        result += string.Format(" or {3}{0} {1} {2}", FieldName, oper, FormatValue(s.Trim()), tL);
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

        public override void SetValue(object value)
        {
            lookupProc1.SetValue(value);
        }

    }
}
