using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Init;
using V6Structs;

namespace V6ReportControls
{
    public partial class FilterGroup : UserControl
    {
        
        public FilterGroup()
        {
            InitializeComponent();
        }

        //[DefaultValue(false)]
        //public virtual bool IsSelected
        //{
        //    get { return checkBox1.Checked; }
        //    set { checkBox1.Checked = value; }
        //}

        //public ComboBox.ObjectCollection Operators
        //{
        //    get { return comboBox1.Items; }
        //}

        //public string FieldCaption
        //{
        //    get { return label1.Text; }
        //    set { label1.Text = value ?? ""; }
        //}

        //public string FieldName
        //{
        //    get; set;
        //}

        [Browsable(true)]
        [Description("Nhãn hiển thị, cẩn thận nhầm data.")]
        public override string Text
        {
            get { return FieldCaption; }// set { var Fieldc =value; }
        }

        ///// <summary>
        ///// Dấu so sánh sql, có thêm start : like 'value%'
        ///// </summary>
        //[DefaultValue("start")]
        //public string Operator
        //{
        //    get
        //    {
        //        return comboBox1.SelectedItem.ToString();
        //    }
        //    set
        //    {
        //        switch (value.ToLower())
        //        {
        //            case "=":
        //            case "start":
        //            case "like":
        //            case "<>":
        //            case ">":
        //            case ">=":
        //            case "<":
        //            case "<=":
        //            case "is null":
        //            case "is not null":
        //                comboBox1.SelectedItem = value.ToLower();
        //                break;
        //        }
        //    }
        //}

        public virtual object ObjectValue
        {
            get { return lblGroupStringVT.Text; }
        }

        /// <summary>
        /// GenGroup
        /// </summary>
        public virtual string StringValue
        {
            get { return lblGroupStringVT.Text; }
        }

        public DefineInfo DefineInfo { get; set; }
        /// <summary>
        /// Dòng chữ trên groupBox.
        /// </summary>
        public string FieldCaption
        {
            get { return groupBoxNhom.Text; }
            set { groupBoxNhom.Text = value ?? ""; }
        }

        ///// <summary>
        ///// Nếu không check trả về rỗng.
        ///// </summary>
        //public virtual string StringValueCheck
        //{
        //    get { return IsSelected ? StringValue : ""; }
        //}

        ///// <summary>
        ///// Tự kiểm tra check để lấy. Phần này luôn trả về chuỗi dù có check hay không.
        ///// </summary>
        //public virtual string Query
        //{
        //    get
        //    {
        //        var oper = Operator;
        //        if (oper == "start") oper = "like";
        //        var result = string.Format("{0} {1} {2}", FieldName, oper, StringValue);
        //        return result;
        //    }
        //}

        ///// <summary>
        ///// Có thêm tableLabel vd: a.ma_vt = 'abc'
        ///// </summary>
        ///// <param name="tableLabel"></param>
        ///// <returns></returns>
        //public virtual string GetQuery(string tableLabel = null)
        //{
        //    var tL = string.IsNullOrEmpty(tableLabel) ? "" : tableLabel + ".";
        //    var result = string.Format("{0}{1}", tL, Query);
        //    return result;
        //}

        ///// <summary>
        ///// Trả về query nếu được check
        ///// </summary>
        //public string QueryCheck
        //{
        //    get
        //    {
        //        return GetQueryCheck();
        //    }
        //}

        //public string GetQueryCheck(string tableLabel = null)
        //{
        //    if (IsSelected) return GetQuery(tableLabel);
        //    return "";
        //}

        //private void label1_Click(object sender, MouseEventArgs e)
        //{
        //    if (checkBox1.Enabled && e.Button == MouseButtons.Left)
        //    checkBox1.Checked = !checkBox1.Checked;
        //}

        //private void label1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    OnMouseUp(e);
        //}

        //private void comboBox1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    OnMouseUp(e);
        //}

        private void NH_KH1_Leave(object sender, EventArgs e)
        {
            var current = sender as TextBox;
            if (current != null)
            {
                if (current.Text.Trim() == "") current.Text = "0";
            }
        }

        private void NH_KH_TextChanged(object sender, System.EventArgs e)
        {
            var current = sender as TextBox;
            if (current != null)
            {
                if (current.Text.Trim() == "") return;
                foreach (Control control in groupBoxNhom.Controls)
                {
                    if (control != current && control.Text == current.Text)
                    {
                        control.Text = "0";
                    }
                }
            }

            //String2 =
            //    V6BusinessHelper.GenGroup("TEN_NH", NH_VT1.Text, NH_VT2.Text, NH_VT3.Text, NH_VT4.Text, NH_VT5.Text, NH_VT6.Text);
            //if (String2.Length > 0) String2 += ",";
            //String2 += "TEN_VT";

            var paramDic = new Dictionary<string, string>();
            foreach (V6ColorTextBox txt in txtList)
            {
                paramDic.Add(txt.Name, txt.Text);
            }

            var lblGroupStringVTText = GenGroup(paramDic);

            //var lblGroupStringKHText =
            //    V6BusinessHelper.GenGroup("NH_KH", NH_KH1.Text, NH_KH2.Text, NH_KH3.Text, NH_KH4.Text, NH_KH5.Text, NH_KH6.Text);

            lblGroupStringVT.Text = lblGroupStringVTText;

            //String1 = lblGroupStringVT.Text;
        }

        private string GenGroup(Dictionary<string, string> names_groups)
        {
            var result = "";
            var dic = new SortedDictionary<string, string>();
            foreach (KeyValuePair<string, string> item in names_groups)
            {
                var groupName = item.Key;
                var groupSort = ("00" + item.Value).Right(2);
                if (groupSort != "00") dic[groupSort] = groupName;
            }
            //for (int i = 0; i < groups.Length; i++)
            //{
            //    var groupName = names[i];
            //    var groupSort = groups[i];
            //    if (groupSort != "0") dic[groupSort] = groupName;
            //}
            foreach (KeyValuePair<string, string> item in dic)
            {
                result += "," + item.Value;
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

        List<V6ColorTextBox> txtList = new List<V6ColorTextBox>();
        public void GenControls(string controlDescriptions)
        {
            try
            {
                var lines = controlDescriptions.Split('/');
                int lblX = 5, lblY = 0;
                object testSender = null;
                for (int i = 0; i < lines.Length; i++)
                {
                    //Tạo control từng dòng.
                    string lineDescription = lines[i];
                    var fields = lineDescription.Split(',');

                    for (int j = 0; j < fields.Length; j++)
                    {
                        var FIELD_text = fields[j].Split(':');
                        string FIELD = FIELD_text[0];
                        string text = "";
                        if (FIELD_text.Length > 1) text = FIELD_text[1];

                        int x = 5 + j*25;
                        int y = 15 + i*25;
                        lblY = y + 25;

                        var txtNH = new V6ColorTextBox();
                        groupBoxNhom.Controls.Add(txtNH);
                        txtList.Add(txtNH);
                        txtNH.AccessibleName = "NH_" + FIELD;
                        txtNH.BackColor = System.Drawing.Color.White;
                        txtNH.BackColorDisabled = System.Drawing.SystemColors.Control;
                        txtNH.EnterColor = System.Drawing.Color.PaleGreen;
                        txtNH.ForeColor = System.Drawing.SystemColors.WindowText;
                        txtNH.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
                        txtNH.HoverColor = System.Drawing.Color.Yellow;
                        txtNH.LeaveColor = System.Drawing.Color.White;
                        txtNH.LimitCharacters = "0123456789";
                        txtNH.Location = new System.Drawing.Point(x, y);
                        txtNH.Margin = new System.Windows.Forms.Padding(4);
                        txtNH.MaxLength = 2;
                        txtNH.Name = FIELD;
                        txtNH.Size = new System.Drawing.Size(20, 20);
                        txtNH.TabIndex = 8;
                        txtNH.Text = text;
                        txtNH.Leave += NH_KH1_Leave;
                        txtNH.TextChanged += NH_KH_TextChanged;

                        if (text != "")
                        {
                            testSender = txtNH;
                        }
                    }
                }
                //lbl1
                this.lblGroupStringVT = new Label();
                this.groupBoxNhom.Controls.Add(this.lblGroupStringVT);
                this.lblGroupStringVT.Location = new System.Drawing.Point(lblX, lblY);
                this.lblGroupStringVT.Name = "lblGroupStringVT";
                this.lblGroupStringVT.Size = new System.Drawing.Size(283, 44);
                //this.lblGroupStringVT.TabIndex = 7;
                NH_KH_TextChanged(testSender, new EventArgs());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GenControls", ex);
            }
        }

        private Label lblGroupStringVT;
    }
}
