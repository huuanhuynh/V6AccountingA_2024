using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FileTool;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Controls.Forms.Viewer;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

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
            
            txtNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            txtNgay_ct2.SetValue(V6Setting.M_ngay_ct2);
            txtNgay_ct3.SetValue(V6Setting.M_ngay_ct1.AddMonths(-1));
            txtNgay_ct4.SetValue(V6Setting.M_ngay_ct2.AddMonths(-1));


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
            if (V6Login.IsAdmin) chkHienTatCa.Enabled = true;
            Ready();
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields = new SortedDictionary<string, string> {{"TAG", "TAG"}};
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
                    "*", (chkHienTatCa.Checked ? "" : "[status]='1' and ") + "ma_maubc='" + txtma_maubcX.Text.ToUpper() + "'",
                    "", "[ORDER]").Data;

                cboMaubc_X.ValueMember = "file_maubc";
                cboMaubc_X.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
                cboMaubc_X.DataSource = maubcDataX;
                cboMaubc_X.ValueMember = "file_maubc";
                cboMaubc_X.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";

                maubcDataB = V6BusinessHelper.Select("ALMAUBC",
                    "*", "ma_maubc='" + txtma_maubcB.Text.ToUpper() + "'",
                    "", "[ORDER]").Data;

                cboMauCdkt_B.ValueMember = "file_maubc";
                cboMauCdkt_B.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
                cboMauCdkt_B.DataSource = maubcDataB;
                cboMauCdkt_B.ValueMember = "file_maubc";
                cboMauCdkt_B.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";

                maubcDataBB = V6BusinessHelper.Select("ALMAUBC",
                    "*", "ma_maubc='" + txtma_maubcC.Text.ToUpper() + "'",
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
                new SqlParameter("@Ngay_ct1", txtNgay_ct1.YYYYMMDD),
                new SqlParameter("@Ngay_ct2", txtNgay_ct2.YYYYMMDD),
                new SqlParameter("@Ngay_ct3", txtNgay_ct3.YYYYMMDD),
                new SqlParameter("@Ngay_ct4", txtNgay_ct4.YYYYMMDD),
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
                        var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, keys, _data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(FindParent<V6FormControl>());
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, null, null);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(FindParent<V6FormControl>());
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

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, CurrentTable.ToString());
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + ma_ct, ex);
            }
        }

        void f_InsertSuccess(IDictionary<string, object> dataDic)
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
                        var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Edit, keys, _data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(FindParent<V6FormControl>());
                        f.UpdateSuccessEvent += f_UpdateSuccess;
                        f.CallReloadEvent += FCallReloadEvent;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.NoSelection);
                    }
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
                    var parentData = row0.ToDataDictionary();
                    parentData["MAU_BC"] = parentData["FILE_MAUBC"];
                    BangCanDoiTaiChinhForm form = new BangCanDoiTaiChinhForm(filter, parentData);
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

        private void btnXemMau_Click(object sender, EventArgs e)
        {
            try
            {
                if (radExcel.Checked)
                {
                    string name = txtMauExport.Text.Trim().ToLower();
                    if (!name.EndsWith(".xls") && !name.EndsWith(".xlsx")) name += ".xls";
                    V6ControlFormHelper.OpenExcelTemplate(name, V6Setting.V6ReportsFolder);
                }
                else if (radWord.Checked)
                {
                    string name = txtMauExport.Text.Trim().ToLower();
                    if (!name.EndsWith(".doc") && !name.EndsWith(".docx")) name += ".docx";
                    V6ControlFormHelper.OpenWordTemplate(name, V6Setting.V6ReportsFolder);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnXemMau_Click", ex);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (radExcel.Checked)
            {
                ExportExcel(sender, e);
            }
            else if (radWord.Checked)
            {
                ExportWord(sender, e);
            }
        }

        private void ExportExcel(object sender, EventArgs e)
        {
            try
            {
                if (_ds == null || _ds.Tables.Count == 0)
                {
                    this.ShowInfoMessage(V6Text.NoData);
                    return;
                }

                string saveFile = V6ControlFormHelper.ChooseSaveFile(this, "Excel|*.xls;*.xlsx", cboMaubc_X.Text);
                if (string.IsNullOrEmpty(saveFile)) return;

                string xlsTemplateFile = V6Login.StartupPath + "\\Reports\\" + txtMauExport.Text.Trim();
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
                        if (mappingData.ContainsKey(NAME))
                        {
                            ShowMainMessage(V6Text.DataExist + "NAME=" + NAME);
                        }
                        else
                        {
                            mappingData.Add(NAME, value);
                        }
                        if (addressData.ContainsKey(FCOLUMN))
                        {
                            ShowMainMessage(V6Text.DataExist + "FCOLUMN=" + FCOLUMN);
                        }
                        else
                        {
                            addressData.Add(FCOLUMN, fvalue);
                        }
                    }
                    else
                    {
                        string NAME = row["NAME"].ToString().Trim().ToUpper();
                        string FCOLUMN = row["FCOLUMN"].ToString().Trim().ToUpper();
                        object value = row[vType + "Value"];
                        object fvalue = row[vType + "fvalue"];
                        if (mappingData.ContainsKey(NAME))
                        {
                            ShowMainMessage(V6Text.DataExist + "NAME=" + NAME);
                        }
                        else
                        {
                            mappingData.Add(NAME, value);
                        }
                        if (addressData.ContainsKey(FCOLUMN))
                        {
                            ShowMainMessage(V6Text.DataExist + FCOLUMN);
                        }
                        else
                        {
                            addressData.Add(FCOLUMN, fvalue);
                        }
                    }
                }

                if (V6Tools.V6Export.ExportData.MappingDataToExcelFile(xlsTemplateFile, saveFile, mappingData, addressData))
                {
                    if (V6Options.AutoOpenExcel)
                    {
                        V6ControlFormHelper.OpenFileProcess(saveFile);
                    }
                    else
                    {
                        this.ShowInfoMessage(V6Text.ExportFinish + "\n" + saveFile);
                    }
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

        private void ExportWord(object sender, EventArgs e)
        {
            try
            {
                if (_ds == null || _ds.Tables.Count == 0)
                {
                    this.ShowInfoMessage(V6Text.NoData);
                    return;
                }

                string saveFile = V6ControlFormHelper.ChooseSaveFile(this, "Excel|*.docx;*.doc", cboMaubc_X.Text);
                if (string.IsNullOrEmpty(saveFile)) return;

                string xlsTemplateFile = V6Login.StartupPath + "\\Reports\\" + txtMauExport.Text.Trim();
                if (!xlsTemplateFile.EndsWith(".doc", true, CultureInfo.InvariantCulture) || !xlsTemplateFile.EndsWith(".docx", true, CultureInfo.InvariantCulture))
                {
                    xlsTemplateFile += ".docx";
                }
                xlsTemplateFile = Path.GetFullPath(xlsTemplateFile);
                //string ext = Path.GetExtension(xlsTemplateFile);
                //string saveAsFile = Path.GetFileNameWithoutExtension(xlsTemplateFile) + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;

                IDictionary<string, string> mappingData = new SortedDictionary<string, string>();
                //mappingData = _ds.Tables[0].ToDataSortedDictionary("NAME", "VALUE");
                //IDictionary<string, object> addressData = new SortedDictionary<string, object>();
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
                        if (mappingData.ContainsKey(NAME))
                        {
                            ShowMainMessage(V6Text.DataExist + "NAME=" + NAME);
                        }
                        else
                        {
                            mappingData.Add("{@" + NAME + "}", ObjectAndString.ObjectToString(value));
                        }
                    }
                    else
                    {
                        string NAME = row["NAME"].ToString().Trim().ToUpper();
                        string FCOLUMN = row["FCOLUMN"].ToString().Trim().ToUpper();
                        object value = row[vType + "Value"];
                        object fvalue = row[vType + "fvalue"];
                        if (mappingData.ContainsKey(NAME))
                        {
                            ShowMainMessage(V6Text.DataExist + "NAME=" + NAME);
                        }
                        else
                        {
                            mappingData.Add("{@" + NAME + "}", ObjectAndString.ObjectToString(value));
                        }
                    }
                }

                File.Copy(xlsTemplateFile, saveFile, true);

                WordUtility wf = new WordUtility();
                //wf.ReplaceText(xlsTemplateFile, saveFile, mappingData);
                wf.ReplaceTextInterop(saveFile, mappingData);

                {
                    if (V6Options.AutoOpenExcel)
                    {
                        V6ControlFormHelper.OpenFileProcess(saveFile);
                    }
                    else
                    {
                        this.ShowInfoMessage(V6Text.ExportFinish + "\n" + saveFile);
                    }
                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnExport_Click", ex);
            }
        }

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadAlmaubc();
        }

        private void cboMaubc_X_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaubc_X.SelectedValue == null) return;
            //if (_radioRunning || _updateDataRow) return;
            try
            {
                txtMauExport.Text = cboMaubc_X.SelectedValue.ToString().Trim();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".cboMaubc_X_SelectedIndexChanged", ex);
            }
        }

        private void btnMapping_Click(object sender, EventArgs e)
        {
            var data = V6BusinessHelper.SelectTable(txtMauExport.Text);
            IDictionary<string, object> defaultData = new Dictionary<string, object>();
            defaultData["MAU_BC"] = txtMauExport.Text;
            defaultData["NAME"] = "NAME";
            var f = new DataEditorForm(this, data, txtMauExport.Text, null, "UID", "title",
                true, true, false, true, defaultData);
            f.ShowDialog(this);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        

    }
}
