using System;
using System.ComponentModel;
using System.Windows.Forms;
using V6Controls;
using V6Tools.V6Convert;

namespace V6ReportControls
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

        public V6DateTimePicker DateTimeTextBox
        {
            get { return v6DateTimeTextBox1; }
        }

        public override string StringValue
        {
            get
            {
                if (",=,<>,>,>=,<,<=,".Contains(","+Operator+","))
                {   
                    return string.Format("'{0}'", v6DateTimeTextBox1.YYYYMMDD);
                }
                return "";
            }
        }

        public override Control InputControl
        {
            get
            {
                return v6DateTimeTextBox1;
            }
        }

        public override object ObjectValue
        {
            get { return Value; }
        }

        public DateTime Value
        {
            get
            {
                return v6DateTimeTextBox1.Date;
            }
        }

        public override void SetValue(object value)
        {
            v6DateTimeTextBox1.SetValue(ObjectAndString.ObjectToFullDateTime(value));
        }

    }
}
