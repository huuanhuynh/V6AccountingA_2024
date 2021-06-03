using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using V6Controls;

namespace V6ReportControls
{
    public partial class FilterLineDSNS : FilterLineDynamic
    {
        public FilterLineDSNS()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                IsSelected = true;
                label1.Visible = false;
                comboBox1.Visible = false;
                //btnChonDSNS.Click += FilterLineDynamic_Click;
                txtDSNS.TextChanged += FilterLineDynamic_TextChanged;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        protected override void FilterLineDynamic_TextChanged(object sender, EventArgs e)
        {
            if (txtDSNS.Text.Trim() == string.Empty)
            {
                IsSelected = false;
            }
            else
            {
                IsSelected = true;
            }
            OnTextChanged(e);
        }

        /// <summary>
        /// Nhãn hiển thị.
        /// </summary>
        public override string Caption
        {
            get { return btnChonDSNS.Text; }
            set { btnChonDSNS.Text = value ?? ""; }
        }

        /// <summary>
        /// Là AccessibleName của control chứa value.
        /// </summary>
        [DefaultValue(null)]
        public new string AccessibleName2
        {
            get
            {
                return txtDSNS.AccessibleName;
            }
            set
            {
                txtDSNS.AccessibleName = value;
            }
        }

        public bool EnabledTxt { get { return !txtDSNS.ReadOnly; } set { txtDSNS.ReadOnly = !value; } }

        /// <summary>
        /// Giá trị của textbox đã trim()
        /// </summary>
        public override string StringValue
        {
            get
            {
                return txtDSNS.Text;
            }
        }

        public override Control InputControl
        {
            get
            {
                return txtDSNS;
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
            txtDSNS.Text = ("" + value).Trim();
        }

        private void btnChonDSNS_Click(object sender, EventArgs e)
        {
            try
            {
                V6Controls.Forms.ToChucTreeSelectForm f = new V6Controls.Forms.ToChucTreeSelectForm(StringValue);

                f.AcceptDataSelect += delegate(string idList, List<IDictionary<string, object>> dataList)
                {
                    SetValue(idList);
                    OnClick(e);
                };
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnChonDSNS_Click", ex);
            }
        }
    }
}
