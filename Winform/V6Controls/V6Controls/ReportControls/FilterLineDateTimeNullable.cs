using System;
using System.ComponentModel;
using V6Controls;

namespace V6ReportControls
{
    public partial class FilterLineDateTimeNullable : FilterLineBase
    {
        public FilterLineDateTimeNullable()
        {
            InitializeComponent();
            Operators.Clear();
            Operators.Add("="); Operators.Add("<>");
            Operators.Add(">"); Operators.Add(">=");
            Operators.Add("<"); Operators.Add("<="); 
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "=";
            v6DateTimeTextBox1.TextChanged += delegate
            {
                IsSelected = true;
            };
        }

        [DefaultValue(null)]
        public string AccessibleName2
        {
            get
            {
                if (v6DateTimeTextBox1 != null) return v6DateTimeTextBox1.AccessibleName;
                return null;
            }
            set
            {
                if (v6DateTimeTextBox1 != null) v6DateTimeTextBox1.AccessibleName = value;
            }
        }

        public V6DateTimeColor DateTimeTextBox
        {
            get { return v6DateTimeTextBox1; }
        }

        /// <summary>
        /// yyyyMMDD
        /// </summary>
        public override string StringValue
        {
            get
            {
                if (",=,<>,>,>=,<,<=,".Contains(","+Operator+","))
                {
                    return string.Format("'{0}'",
                        v6DateTimeTextBox1.Value != null ?
                        ((DateTime)v6DateTimeTextBox1.Value).ToString("yyyyMMdd") : "");
                }
                return "";
            }
        }

        public override object ObjectValue
        {
            get { return Value; }
        }

        public DateTime? Value
        {
            get
            {
                return v6DateTimeTextBox1.Value;
            }
        }
    }
}
