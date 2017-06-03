namespace H_Controls.Controls.Lookup.Filter
{
    public partial class FilterLineVvarTextBox : FilterLineBase
    {
        public FilterLineVvarTextBox()
        {
            InitializeComponent();
            Operators.Add("start");
            Operator = "start";
            LookupTextBox1.TextChanged += delegate {
                IsSelected = true;
            };
        }

        public LookupTextBox VvarTextBox
        {
            get { return LookupTextBox1; }
        }

        public string TableName
        {
            get { return LookupTextBox1.TableName; }
            set { LookupTextBox1.TableName = value; }
        }

        public override string FieldName
        {
            get { return LookupTextBox1.AccessibleName; }
            set { LookupTextBox1.AccessibleName = value; }
        }

        public override string StringValue
        {
            get
            {
                return LookupTextBox1.Text.Trim();
            }
        }

        private string FormatValue(string value)
        {
            if (",=,<>,>,>=,<,<=,".Contains("," + Operator + ","))
                return string.Format("'{0}'", value.Replace("'", "''"));
            else if (Operator == "like")
                return string.Format("'%{0}%'", value.Replace("'", "''"));
            else if (Operator == "start")
                return string.Format("'{0}%'", value.Replace("'", "''"));
            return "";
        }
        public override string Query
        {
            get
            {
                var sValue = StringValue;
                var result = "";

                var oper = Operator;
                if (oper == "start") oper = "like";

                if (sValue.Contains(","))
                {
                    string[] sss = sValue.Split(',');
                    foreach (string s in sss)
                    {
                        result += string.Format(" or {0} {1} {2}", FieldName, oper, FormatValue(s));
                    }

                    if (result.Length > 4)
                    {
                        result = result.Substring(4);
                        result = string.Format("({0})", result);
                    }
                }
                else
                {
                    result = string.Format("{0} {1} {2}", FieldName, oper, FormatValue(StringValue));
                }
                return result;
            }
        }

        /// <summary>
        /// tableLable không còn tác dụng nếu dùng dấu ,
        /// </summary>
        /// <param name="tableLable"></param>
        /// <returns></returns>
        public override string GetQuery(string tableLable = null)
        {
            if(StringValue.Contains(","))
                return Query;
            return base.GetQuery(tableLable);
        }
    }
}
