using System;

namespace H_Controls.Controls.Lookup.Filter
{
    public partial class FilterLineDateTimeTextBox : FilterLineBase
    {
        public FilterLineDateTimeTextBox()
        {
            InitializeComponent();
            Operators.Clear();
            Operators.Add("="); Operators.Add("<>");
            Operators.Add(">"); Operators.Add(">=");
            Operators.Add("<"); Operators.Add("<="); 
            Operators.Add("is null"); Operators.Add("is not null");
            Operator = "=";
            _dateTimeTextBox1.TextChanged += delegate
            {
                IsSelected = true;
            };
        }

        public DateTimePick DateTimeTextBox
        {
            get { return _dateTimeTextBox1; }
        }

        public override string StringValue
        {
            get
            {
                if (",=,<>,>,>=,<,<=,".Contains(","+Operator+","))
                {   
                    return string.Format("'{0}'",_dateTimeTextBox1.Value.ToString("yyyyMMdd"));
                }
                return "";
            }
        }

        public DateTime Value
        {
            get
            {
                return _dateTimeTextBox1.Value;
            }
        }
    }
}
