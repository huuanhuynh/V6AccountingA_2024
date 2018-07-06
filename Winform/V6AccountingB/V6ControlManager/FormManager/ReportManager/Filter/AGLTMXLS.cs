using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.Add_Edit;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTMXLS : FilterBase
    {
        public AGLTMXLS()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = false;

            txtma_maubcX.Text = "GLTCX";
            txtma_maubcB.Text = "GLTCB";
            txtma_maubcC.Text = "GLTCC";
            
            txtNgay_ct1.Value = V6Setting.M_ngay_ct1;
            txtNgay_ct2.Value = V6Setting.M_ngay_ct2;
            txtNgay_ct3.Value = V6Setting.M_ngay_ct1.AddMonths(-1);
            txtNgay_ct4.Value = V6Setting.M_ngay_ct2.AddMonths(-1);


            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            SetHideFields("V");
            
        }

        private void AGLTMXLS_Load(object sender, EventArgs e)
        {
            LoadAlmaubc();
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields = new SortedDictionary<string, string> {{"TAG", "TAG"}};
            }
            else
            {
                
            }
            
        }

        private DataTable maubcDataX, maubcDataB, maubcDataBB;
        private void LoadAlmaubc()
        {
            try
            {
                maubcDataX = V6BusinessHelper.Select("ALMAUBC",
                    "ma_maubc,ten_maubc,ten_maubc2,file_maubc,UID", "ma_maubc='" + txtma_maubcX.Text.ToUpper() + "'",
                    "", "[ORDER]").Data;

                cboMaubc_X.ValueMember = "file_maubc";
                cboMaubc_X.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
                cboMaubc_X.DataSource = maubcDataX;
                cboMaubc_X.ValueMember = "file_maubc";
                cboMaubc_X.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";

                maubcDataB = V6BusinessHelper.Select("ALMAUBC",
                    "ma_maubc,ten_maubc,ten_maubc2,file_maubc,UID", "ma_maubc='" + txtma_maubcB.Text.ToUpper() + "'",
                    "", "[ORDER]").Data;

                cboMauCdkt_B.ValueMember = "file_maubc";
                cboMauCdkt_B.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
                cboMauCdkt_B.DataSource = maubcDataB;
                cboMauCdkt_B.ValueMember = "file_maubc";
                cboMauCdkt_B.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";

                maubcDataBB = V6BusinessHelper.Select("ALMAUBC",
                    "ma_maubc,ten_maubc,ten_maubc2,file_maubc,UID", "ma_maubc='" + txtma_maubcC.Text.ToUpper() + "'",
                    "", "[ORDER]").Data;

                cboMauKqkd_C.ValueMember = "file_maubc";
                cboMauKqkd_C.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
                cboMauKqkd_C.DataSource = maubcDataBB;
                cboMauKqkd_C.ValueMember = "file_maubc";
                cboMauKqkd_C.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            string maubc = "", maubc_B = "", maubc_C = "";
            if (cboMaubc_X.Items.Count > 0 && cboMaubc_X.SelectedIndex >= 0)
            {
                maubc = cboMaubc_X.SelectedValue.ToString().Trim();
            }
            if (cboMauCdkt_B.Items.Count > 0 && cboMauCdkt_B.SelectedIndex >= 0)
            {
                maubc_B = cboMauCdkt_B.SelectedValue.ToString().Trim();
            }
            if (cboMauKqkd_C.Items.Count > 0 && cboMauKqkd_C.SelectedIndex >= 0)
            {
                maubc_C = cboMauKqkd_C.SelectedValue.ToString().Trim();
            }

            if (maubc == "")
            {
                maubc = "GLTCC";
            }

            int luyke = chk_Luy_ke.Checked ? 1 : 0;
            var ma_dvcs = txtMaDvcs.IsSelected ? txtMaDvcs.StringValue.Trim() + "%" : "%";

            var result = new List<SqlParameter>
            {
                new SqlParameter("@Ngay_ct1", txtNgay_ct1.Value.ToString("yyyyMMdd")),
                new SqlParameter("@Ngay_ct2", txtNgay_ct2.Value.ToString("yyyyMMdd")),
                new SqlParameter("@Ngay_ct3", txtNgay_ct3.Value.ToString("yyyyMMdd")),
                new SqlParameter("@Ngay_ct4", txtNgay_ct4.Value.ToString("yyyyMMdd")),
                new SqlParameter("@Ma_dvcs", ma_dvcs),
                new SqlParameter("@Luyke", luyke),
                new SqlParameter("@Mau", maubc),
                new SqlParameter("@MauCDKT", maubc_B),
                new SqlParameter("@MauKQKD", maubc_C),
                new SqlParameter("@Advance", ""),
                new SqlParameter("@OutputCmd", "")
            };

            return result;
        }


        V6TableName CurrentTable = V6TableName.Almaubc;
        private void DoAdd()
        {
            try
            {
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {


                    if (maubcDataX != null)
                    {
                        var row0 = maubcDataX.Rows[cboMaubc_X.SelectedIndex];

                        var keys = new SortedDictionary<string, object>();
                        if (maubcDataX.Columns.Contains("UID"))
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
                        var f = new FormAddEdit(CurrentTable, V6Mode.Add, keys, _data);
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        var f = new FormAddEdit(CurrentTable);
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message);
            }
        }

        void f_InsertSuccess(SortedDictionary<string, object> dataDic)
        {
            try
            {
                var newRow = maubcDataX.NewRow();
                foreach (KeyValuePair<string, object> item in dataDic)
                {
                    if (maubcDataX.Columns.Contains(item.Key))
                        newRow[item.Key] = item.Value;
                }
                maubcDataX.Rows.Add(newRow);
                cboMaubc_X.SelectedIndex = maubcDataX.Rows.Count - 1;
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
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    //DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                    if (cboMaubc_X.SelectedIndex >= 0)
                    {
                        var row0 = maubcDataX.Rows[cboMaubc_X.SelectedIndex];
                        var keys = new SortedDictionary<string, object>();
                        if (maubcDataX.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
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
                        var f = new FormAddEdit(CurrentTable, V6Mode.Edit, keys, _data);
                        f.UpdateSuccessEvent += f_UpdateSuccess;
                        f.CallReloadEvent += FCallReloadEvent;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoEdit: " + ex.Message);
            }
        }

        private void FCallReloadEvent(object sender, EventArgs e)
        {

        }

        private void f_UpdateSuccess(SortedDictionary<string, object> dataDic)
        {
            try
            {
                var editRow = maubcDataX.Rows[cboMaubc_X.SelectedIndex];
                foreach (KeyValuePair<string, object> item in dataDic)
                {
                    if (maubcDataX.Columns.Contains(item.Key))
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
                if (cboMaubc_X.SelectedIndex >= 0)
                {
                    var row0 = maubcDataX.Rows[cboMaubc_X.SelectedIndex];
                    var ma_maubc = row0["file_maubc"].ToString().Trim();
                    var filter = "mau_bc='" + ma_maubc + "'";
                    BangCanDoiTaiChinhForm form = new BangCanDoiTaiChinhForm(filter);
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoEditDetails: " + ex.Message);
            }
        }

        private void btnThemMau_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", "Almaubc".ToUpper() + "6"))
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
            if (V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6"))
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
            if (V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoEditDetails();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ds == null || _ds.Tables.Count == 0)
                {
                    this.ShowInfoMessage(V6Text.NoData);
                    return;
                }

                string saveFile = V6ControlFormHelper.ChooseSaveFile(this, "Excel|*.xls");
                if (string.IsNullOrEmpty(saveFile)) return;

                string xlsTemplateFile = "Reports\\" + txtMauExport.Text.Trim();
                if (!xlsTemplateFile.EndsWith(".xls", true, CultureInfo.InvariantCulture) || !xlsTemplateFile.EndsWith(".xlsx", true, CultureInfo.InvariantCulture))
                {
                    xlsTemplateFile += ".xls";
                }
                xlsTemplateFile = Path.GetFullPath(xlsTemplateFile);
                //string ext = Path.GetExtension(xlsTemplateFile);
                //string saveAsFile = Path.GetFileNameWithoutExtension(xlsTemplateFile) + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;

                IDictionary<string, object> mappingData = new SortedDictionary<string, object>();
                //mappingData = _ds.Tables[0].ToDataSortedDictionary("NAME", "VALUE");
                IDictionary<string, object> addressData = new SortedDictionary<string, object>();
                //addressData = _ds.Tables[0].ToDataSortedDictionary("FCOLUMN", "FVALUE");

                DataTable data = _ds.Tables[0];
                foreach (DataRow row in data.Rows)
                {
                    string vType = row["VTYPE"].ToString().Trim();
                    if (string.IsNullOrEmpty(vType) || vType == "N")//Kiểu số
                    {
                        string NAME = row["NAME"].ToString().Trim().ToUpper();
                        string FCOLUMN = row["FCOLUMN"].ToString().Trim().ToUpper();
                        object value = row["Value"];
                        object fvalue = row["fvalue"];
                        mappingData.Add(NAME, value);
                        addressData.Add(FCOLUMN, fvalue);
                    }
                    else
                    {
                        string NAME = row["NAME"].ToString().Trim().ToUpper();
                        string FCOLUMN = row["FCOLUMN"].ToString().Trim().ToUpper();
                        object value = row[vType + "Value"];
                        object fvalue = row[vType + "fvalue"];
                        mappingData.Add(NAME, value);
                        addressData.Add(FCOLUMN, fvalue);
                    }
                }

                if (V6Tools.V6Export.ExportData.MappingDataToExcelFile(xlsTemplateFile, saveFile, mappingData, addressData))
                {
                    this.ShowInfoMessage(V6Text.ExportFinish + "\n" + saveFile);
                }
                else
                {
                    this.ShowInfoMessage(V6Text.ExportFail);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnExport_Click", ex);
            }
        }

    }
}
