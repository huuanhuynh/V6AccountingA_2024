using System;

namespace H_Controls.Controls.Lookup.Filter
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
            DateTimeColorTextBox1.TextChanged += delegate
            {
                IsSelected = true;
            };
        }

        public DateTimeColor DateTimeTextBox
        {
            get { return DateTimeColorTextBox1; }
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
                        DateTimeColorTextBox1.Value != null ?
                        ((DateTime)DateTimeColorTextBox1.Value).ToString("yyyyMMdd") : "");
                }
                return "";
            }
        }

        public DateTime? Value
        {
            get
            {
                return DateTimeColorTextBox1.Value;
            }
        }
    }
}
