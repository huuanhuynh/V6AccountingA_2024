using System.Globalization;

namespace H_Controls.Controls.Lookup.Filter
{
    public partial class FilterLineNumberTextBox : FilterLineBase
    {
        public FilterLineNumberTextBox()
        {
            InitializeComponent();
            NumberTextBox1.TextChanged += delegate
            {
                IsSelected = true;
            };
        }

        public NumberTextBox NumberTextBox
        {
            get { return NumberTextBox1; }
        }

        public override string StringValue
        {
            get
            {
                if (",=,<>,>,>=,<,<=,".Contains(","+Operator+","))
                {
                    return NumberTextBox1.Value.ToString(CultureInfo.InvariantCulture);
                }
                if (Operator == "like")
                {
                    return NumberTextBox1.Value.ToString(CultureInfo.InvariantCulture);
                }
                return "";
            }
        }
    }
}
