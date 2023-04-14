using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLQTTTNTT156 : FilterBase
    {
        public AGLQTTTNTT156()
        {
            InitializeComponent();
            
            F3 = true;
            F5 = false;

            txtThang1.Value = V6Setting.M_ngay_ct1.Month;
            txtThang2.Value = V6Setting.M_ngay_ct2.Month;
            txtNam1.Value = V6Setting.M_ngay_ct2.Year;
            txtNam2.Value = V6Setting.M_ngay_ct2.Year;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            txtma_maubc.Text = "GLQTTTNTT";

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            SetHideFields(RTien);
            LoadAlmaubc();
            if (V6Login.IsAdmin) chkHienTatCa.Enabled = true;
        }

        private DataTable maubcData;
        private void LoadAlmaubc()
        {
            try
            {
                //ma_maubc,ten_maubc,ten_maubc2,file_maubc
                maubcData = V6BusinessHelper.Select("ALMAUBC",
                   "*", (chkHienTatCa.Checked ? "" : "[status]='1' and ") + "ma_maubc='" + txtma_maubc.Text.ToUpper() + "'",
                   "", "[ORDER]").Data;

                cboMaubc.ValueMember = "file_maubc";
                cboMaubc.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
                cboMaubc.DataSource = maubcData;
                cboMaubc.ValueMember = "file_maubc";
                cboMaubc.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }
        public override void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
            try
            {
                
                //reportRviewBase.re
            }
            catch (Exception)
            {
                
            }
        }

        public void SetHideFields(string Loaitien)
        {
            if (Loaitien == "VN")
            {
                GridViewHideFields = new SortedDictionary<string, string>
                {
                    {"TAG", "TAG"},
                    {"STT_REC", "STT_REC"},
                    {"STT_REC0", "STT_REC0"},
                    {"STT_REC_TT", "STT_REC_TT"},
                    {"MA_TT", "MA_TT"},
                    {"MA_GD", "MA_GD"},
                    {"T_TT_NT0", "T_TT_NT0"},
                    {"T_TT_NT", "T_TT_NT"},
                    {"DA_TT_NT", "DA_TT_NT"},
                    {"CON_PT_NT", "CON_PT_NT"},
                    {"T_TIEN_NT2", "T_TIEN_NT2"},
                    {"T_THUE_NT", "T_THUE_NT"}
                };
            }
            else 
            {
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
                GridViewHideFields.Add("MA_TT", "MA_TT");
                GridViewHideFields.Add("MA_GD", "MA_GD");

            }
            
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            // @Period1 int,
            // @Year1 int,
            // @Period2 int,
            // @Year2 int,
            // @Mau VARCHAR(50),
            // @Advance VARCHAR(8000) = ''


            if (txtThang1.Value<=0 || txtThang1.Value >12 || txtThang2.Value <= 0 || txtThang2.Value > 12)
            {
                throw new Exception(V6Text.Text("SAITHANG"));
            }


            string maubc = "";
            if (cboMaubc.Items.Count > 0 && cboMaubc.SelectedIndex >= 0)
            {
                maubc = cboMaubc.SelectedValue.ToString().Trim();
            }

            if (maubc == "")
            {
                maubc = "GLQTTTNTT156";
            }

            var result = new List<SqlParameter>();

     



            result.Add(new SqlParameter("@Period1", (int)txtThang1.Value));
            result.Add(new SqlParameter("@Year1", (int)txtNam1.Value));
            result.Add(new SqlParameter("@Period2", (int)txtThang2.Value));
            result.Add(new SqlParameter("@Year2", (int)txtNam2.Value));
            result.Add(new SqlParameter("@Mau", maubc));


            var and = radAnd.Checked;
            var cKey = "";


            var key0 = GetFilterStringByFields(new List<string>()
            {
               "MA_DVCS"
            }, and);
           if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey = string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }

            result.Add(new SqlParameter("@Advance", cKey));


            return result;
        }

        private void txtThang2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                //if (txt.Value < 1) txt.Value = 1;
                //if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
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


                    if (maubcData != null)
                    {
                        var row0 = maubcData.Rows[cboMaubc.SelectedIndex];

                        var keys = new SortedDictionary<string, object>();
                        if (maubcData.Columns.Contains("UID"))
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
                var newRow = maubcData.NewRow();
                foreach (KeyValuePair<string, object> item in dataDic)
                {
                    if (maubcData.Columns.Contains(item.Key))
                        newRow[item.Key] = item.Value;
                }
                maubcData.Rows.Add(newRow);
                cboMaubc.SelectedIndex = maubcData.Rows.Count - 1;
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

                    if (cboMaubc.SelectedIndex >= 0)
                    {
                        var row0 = maubcData.Rows[cboMaubc.SelectedIndex];
                        var keys = new SortedDictionary<string, object>();
                        if (maubcData.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
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
                var editRow = maubcData.Rows[cboMaubc.SelectedIndex];
                foreach (KeyValuePair<string, object> item in dataDic)
                {
                    if (maubcData.Columns.Contains(item.Key))
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
                if (cboMaubc.SelectedIndex >= 0)
                {
                    var row0 = maubcData.Rows[cboMaubc.SelectedIndex];
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

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadAlmaubc();
        }

        private void txtALINFOR_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMaubc.SelectedIndex == -1) return;

                string tableName = "ALINFOR";
                DataTable data = V6BusinessHelper.Select(tableName, "*",
                    "MA_INFOR = '" + cboMaubc.SelectedValue + "' and STATUS = '1' order by stt", "", "").Data;
                V6ControlFormHelper.ShowDataEditorForm(this, data, tableName, null, "UID", false, false, false, true, null);
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message, this);
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            string saveFile = V6ControlFormHelper.ChooseSaveFile(this, "XML files (*.xml)|*.xml", txtFileName.Text);

            if (!string.IsNullOrEmpty(saveFile))
            {
                txtFileName.Text = saveFile;
            }
        }

        string procName = null;
        private void btnKetXuatXmlHTKK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFileName.Text == "")
                {
                    if (!Directory.Exists("C:\\V6EXCEL")) Directory.CreateDirectory("C:\\V6EXCEL");
                    txtFileName.Text = string.Format("C:\\V6EXCEL\\" + "TOKHAI_QTTTNDN_{0:00}_{1:0000}_{2:00}_{3:0000}.xml",
                        txtThang1.Value, txtNam1.Value, txtThang2.Value, txtNam2.Value);
                }

                string xmlTemplateFile = V6Login.StartupPath + "\\Reports\\XML\\" + cboMaubc.SelectedValue.ToString().Trim() + ".xml";

                if (procName == null)
                {
                    var parent = FindParent<ReportR_DX>() as ReportR_DX;
                    if (parent != null)
                    {
                        procName = parent._reportProcedure + "_XML";
                    }
                    else
                    {
                        var p = FindParent<ReportRViewBase>() as ReportRViewBase;
                        if (p != null) procName = p._reportProcedure + "_XML";
                    }
                }

                var plist = GetFilterParameters();
                var data = V6BusinessHelper.ExecuteProcedure(procName, plist.ToArray()).Tables[0];
                string xml = File.ReadAllText(xmlTemplateFile);
                foreach (DataRow row in data.Rows)
                {
                    //      [KEY_MAP]=mst,
                    //      [var_type]='C'
                    //      [Value_C]='0303180249'
                    //      [Value_N]=Null
                    //      [Value_D]=Null
                    //      [Value_DT]=Null
                    //      [DATA_TYPE]=Null
                    string xmlKey = row["KEY_MAP"].ToString().Trim();
                    string var_type = row["var_type"].ToString().Trim().ToUpper();
                    string xmlValue = "";
                    if (var_type == "C") xmlValue = GetValue(row["Value_C"], row["DATA_TYPE"].ToString()).ToString();
                    else if (var_type == "N") xmlValue = GetValue(row["Value_N"], row["DATA_TYPE"].ToString()).ToString();
                    else if (var_type == "D") xmlValue = ObjectAndString.ObjectToString
                        (GetValue(row["Value_D"], row["DATA_TYPE"].ToString()), "yyyy-MM-dd");
                    else if (var_type == "T") xmlValue = ObjectAndString.ObjectToString
                        (GetValue(row["Value_DT"], row["DATA_TYPE"].ToString()), "yyyy-MM-dd HH:mm:ss");
                    
                    ReplaceXmlValue(ref xml, xmlKey, xmlValue);
                }

                File.WriteAllText(txtFileName.Text, xml);
                ShowMainMessage(V6Text.Finish);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private object GetValue(object fieldValue, string configDATATYPE)
        {
            switch (configDATATYPE.ToUpper())
            {
                case "BOOL":
                    if (fieldValue is bool)
                    {
                        return fieldValue;
                    }
                    else
                    {
                        return fieldValue != null &&
                            (fieldValue.ToString() == "1" ||
                                fieldValue.ToString().ToLower() == "true" ||
                                fieldValue.ToString().ToLower() == "yes");
                    }
                case "BOOLS":
                    if (fieldValue is bool)
                    {
                        return fieldValue;
                    }
                    else
                    {
                        if ((fieldValue + "").Trim() == "") return null;

                        return fieldValue.ToString() == "1" ||
                               fieldValue.ToString().ToLower() == "true" ||
                               fieldValue.ToString().ToLower() == "yes";
                    }
                case "DATE":
                case "DATETIME":
                    return ObjectAndString.ObjectToDate(fieldValue);
                    break;
                case "N2C":
                    return V6BusinessHelper.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                case "N2CE":
                    return V6BusinessHelper.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", "VND");
                //case "N2CMANT":
                //    return V6BusinessHelper.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", row["MA_NT"].ToString().Trim());
                //case "N2CMANTE":
                //    return V6BusinessHelper.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim());
                //case "N2C0VNDE":
                //    {
                //        string ma_nt = row["MA_NT"].ToString().Trim().ToUpper();
                //        if (ma_nt != "VND")
                //        {
                //            return V6BusinessHelper.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim());
                //        }
                //        else
                //        {
                //            return "";
                //        }
                //    }
                case "DECIMAL":
                case "MONEY":
                case "NUMBER":
                    return ObjectAndString.ObjectToDecimal(fieldValue);
                case "INT":
                    return ObjectAndString.ObjectToInt(fieldValue);
                case "INT64":
                case "LONG":
                    return ObjectAndString.ObjectToInt64(fieldValue);
                //case "UPPER": // Chỉ dùng ở exe gọi bằng Foxpro.
                //    return (fieldValue + "").ToUpper();
                case "INTSTRING": // Đưa kiểu số về chuỗi nguyên (không lấy phần thập phân).
                    return ObjectAndString.ObjectToInt(fieldValue).ToString();
                case "STRING":
                    return "" + fieldValue;
                default:    // Kiểu nguyên mẫu của dữ liệu.
                    return fieldValue;
            }
        }

        private void ReplaceXmlValue(ref string xml, string xmlKey, string xmlValue)
        {
            int startIndex = xml.IndexOf("<" + xmlKey + ">", StringComparison.InvariantCulture) + xmlKey.Length + 2;
            int endIndex = xml.IndexOf("</" + xmlKey + ">", StringComparison.InvariantCulture);
            xml = xml.Substring(0, startIndex)
                  + xmlValue
                  + xml.Substring(endIndex);
        }
    }
}
