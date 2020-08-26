using System;
using System.ComponentModel;
using System.Globalization;
using V6Controls;
using V6Tools.V6Convert;

namespace V6ReportControls
{
    public partial class FilterLineNumberTextBox : FilterLineBase
    {
        public FilterLineNumberTextBox()
        {
            InitializeComponent();
            v6NumberTextBox1.TextChanged += delegate
            {
                if (v6NumberTextBox1.Value == 0) IsSelected = false;
                else IsSelected = true;
            };
        }

        [DefaultValue(null)]
        public string AccessibleName2
        {
            get
            {
                if (v6NumberTextBox1 != null) return v6NumberTextBox1.AccessibleName;
                return null;
            }
            set
            {
                if (v6NumberTextBox1 != null) v6NumberTextBox1.AccessibleName = value;
            }
        }

        public V6NumberTextBox NumberTextBox
        {
            get { return v6NumberTextBox1; }
        }

        public override string StringValue
        {
            get
            {
                if (",=,<>,>,>=,<,<=,".Contains(","+Operator+","))
                {
                    return v6NumberTextBox1.Value.ToString(CultureInfo.InvariantCulture);
                }
                if (Operator == "like")
                {
                    return v6NumberTextBox1.Value.ToString(CultureInfo.InvariantCulture);
                }
                return "";
            }
        }


        public override object ObjectValue
        {
            get { return Value; }
        }

        public Decimal Value
        {
            get
            {
                return v6NumberTextBox1.Value;
            }
        }

        public override void SetValue(object value)
        {
            v6NumberTextBox1.Value = ObjectAndString.ObjectToDecimal(value);
        }
    }
}
