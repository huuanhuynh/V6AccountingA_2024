using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ReportControls
{
    public partial class FilterLineMauALL : FilterLineDynamic
    {
        public FilterLineMauALL()
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
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        private DataTable cboData = null;
        private void LoadDanhSach()
        {
            try
            {
                cboData = V6BusinessHelper.Select(TableName, "*", FieldMa+"='" + txtMa.Text.ToUpper() + "'", "", "["+ FieldOrder +"]").Data;

                cboMau.ValueMember = FieldValue;
                cboMau.DisplayMember = FieldDisplay;
                cboMau.DataSource = cboData;
                cboMau.ValueMember = FieldValue;
                cboMau.DisplayMember = FieldDisplay;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        /// <summary>
        /// Nhãn hiển thị.
        /// </summary>
        public override string Caption
        {
            get { return lblMau.Text; }
            set { lblMau.Text = value ?? ""; }
        }

        /// <summary>
        /// Là AccessibleName của control chứa value.
        /// </summary>
        [DefaultValue(null)]
        public new string AccessibleName2
        {
            get
            {
                return cboMau.AccessibleName;
            }
            set
            {
                cboMau.AccessibleName = value;
            }
        }

        public string TableName { get { return DefineInfo.TableName; } }
        public string FieldMa { get { return DefineInfo.FieldMa; } }
        public string FieldValue { get { return DefineInfo.FieldValue; } }
        public string FieldDisplay { get { return DefineInfo.FieldDisplay; } }
        public string FieldOrder { get { return DefineInfo.FieldOrder; } }
        public string TableNameCt { get { return DefineInfo.TableNameCt; } }
        public string FieldValueCt { get { return DefineInfo.FieldValueCt; } }
        public string FieldDisplayCt { get { return DefineInfo.FieldDisplayCt; } }
        public string FieldOrderCt { get { return DefineInfo.FieldOrderCt; } }

        /// <summary>
        /// Giá trị của textbox đã trim()
        /// </summary>
        public override string StringValue
        {
            get
            {
                return cboMau.SelectedValue.ToString();
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
            txtMa.Text = ("" + value).Trim();
            LoadDanhSach();
        }

        private void DoAdd()
        {
            try
            {
                if (cboData != null)
                {
                    var row0 = cboData.Rows[cboMau.SelectedIndex];

                    var keys = new SortedDictionary<string, object>();
                    if (cboData.Columns.Contains("UID"))
                        keys.Add("UID", row0["UID"]);

                    //if (KeyFields != null)
                    //    foreach (var keyField in KeyFields)
                    //    {
                    //        if (dataGridView1.Columns.Contains(keyField))
                    //        {
                    //            keys[keyField] = row.Cells[keyField].Value;
                    //        }
                    //    }

                    var _data = row0.ToDataDictionary();
                    var f = new FormAddEdit(TableName, V6Mode.Add, keys, _data);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl();
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
                }
                else
                {
                    var f = new FormAddEdit(TableName);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl();
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message);
            }
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, TableName);
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                V6ControlFormHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + TableName, ex);
            }
        }

        void f_InsertSuccess(IDictionary<string, object> dataDic)
        {
            try
            {
                var newRow = cboData.NewRow();
                foreach (KeyValuePair<string, object> item in dataDic)
                {
                    if (cboData.Columns.Contains(item.Key))
                        newRow[item.Key] = item.Value;
                }
                cboData.Rows.Add(newRow);
                cboMau.SelectedIndex = cboData.Rows.Count - 1;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".InsertSuccessHandler: " + ex.Message);
            }
        }

        private void DoEdit()
        {
            try
            {
                if (cboMau.SelectedIndex >= 0)
                {
                    var row0 = cboData.Rows[cboMau.SelectedIndex];
                    var keys = new SortedDictionary<string, object>();
                    if (cboData.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                        keys.Add("UID", row0["UID"]);

                    //if (KeyFields != null)
                    //    foreach (var keyField in KeyFields)
                    //    {
                    //        if (dataGridView1.Columns.Contains(keyField))
                    //        {
                    //            keys[keyField] = row.Cells[keyField].Value;
                    //        }
                    //    }

                    var _data = row0.ToDataDictionary();
                    var f = new FormAddEdit(TableName, V6Mode.Edit, keys, _data);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl();
                    f.UpdateSuccessEvent += f_UpdateSuccess;
                    f.CallReloadEvent += FCallReloadEvent;
                    f.ShowDialog(this);
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }

        private void FCallReloadEvent(object sender, EventArgs e)
        {

        }

        private void f_UpdateSuccess(IDictionary<string, object> dataDic)
        {
            try
            {
                var editRow = cboData.Rows[cboMau.SelectedIndex];
                foreach (KeyValuePair<string, object> item in dataDic)
                {
                    if (cboData.Columns.Contains(item.Key))
                        editRow[item.Key] = item.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".UpdateSuccessHandler: " + ex.Message);
            }
        }

        private void DoEditDetails()
        {
            try
            {
                if (cboMau.SelectedIndex >= 0)
                {
                    var row0 = cboData.Rows[cboMau.SelectedIndex];
                    var ma = row0[FieldValue].ToString().Trim();
                    var filter = FieldValueCt + "='" + ma + "'";

                    var ca = new CategoryView("itemId", "title", TableNameCt, filter, FieldOrderCt, null);
                    ca.ShowToForm(this, "title", true, true, false);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoEditDetails: " + ex.Message);
            }
        }

        private void btnThemMau_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", TableName.ToUpper() + "6"))
            {
                DoAdd();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnSuaTTMau_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", TableName.ToUpper() + "6"))
            {
                DoEdit();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnSuaCTMau_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", TableName.ToUpper() + "6"))
            {
                DoEditDetails();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }
    }
}
