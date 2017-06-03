using System.Windows.Forms;

namespace H_Controls.Controls.Lookup.Filter
{
    public partial class FilterLineBase : BaseUserControl
    {
        public virtual bool IsSelected
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }
        public FilterLineBase()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
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

        public virtual string FieldName
        {
            get; set;
        }

        /// <summary>
        /// Dấu so sánh sql, có thêm start : like 'value%'
        /// </summary>
        protected string Operator
        {
            get
            {
                return comboBox1.SelectedItem.ToString();
            }
            set
            {
                comboBox1.SelectedItem = value;
            }
        }

        public virtual string StringValue
        {
            get
            {
                return "'base'";
            }
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
        /// <param name="tableLable"></param>
        /// <returns></returns>
        public virtual string GetQuery(string tableLable = null)
        {
            var tL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
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
                if (IsSelected)
                {
                    return Query;
                }
                return "";
            }
        }

        private void label1_Click(object sender, System.EventArgs e)
        {
            checkBox1.Checked = !checkBox1.Checked;
        }

    }
}
