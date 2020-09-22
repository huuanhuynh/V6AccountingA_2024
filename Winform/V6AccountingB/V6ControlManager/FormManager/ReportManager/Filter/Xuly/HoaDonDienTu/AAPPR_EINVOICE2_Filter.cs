using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPPR_EINVOICE2_Filter : FilterBase
    {
        public AAPPR_EINVOICE2_Filter()
        {
            InitializeComponent();
            
            F3 = true;
            F6 = true;
            F9 = true;
            
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            cboSendType.SelectedIndex = 0;
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            SetHideFields(RTien);
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
            String1 = (cboSendType.SelectedIndex + 1).ToString();
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@nhom_ct", chkNhomCt.Checked?1:0));
            result.Add(new SqlParameter("@loai", txtLoai.Text.Trim()));
            result.Add(new SqlParameter("@MA_TD1", String1));

            var and = radAnd.Checked;
            var cKey = "";


            var key0 = GetFilterStringByFields(new List<string>()
            {
               "MA_DVCS", "MA_SONB", "MA_MAUHD", "SO_SERI", "MA_KH", "MA_CT", "TK_THUE_CO", "MA_THUE", "MA_XULY", "STATUS_IN"
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

            result.Add(new SqlParameter("@Condition", cKey));
               
            
            return result;
        }

        private void ChonFileExcelSaveAs()
        {
            var save = new SaveFileDialog
            {
                Filter = @"Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
            };
            if (txtFileName.Text != "") save.FileName = txtFileName.Text;

            if (save.ShowDialog(this) == DialogResult.OK)
            {
                txtFileName.Text = save.FileName;
            }
        }

        private void XuatExcelHTKK()
        {
            if (_ds == null || _ds.Tables.Count == 0)
            {
                this.ShowInfoMessage(V6Text.NoData);
                return;
            }
            if (txtFileName.Text == "")
            {
                this.ShowInfoMessage("Chưa chọn file lưu.");
                return;
            }

            var reportRviewBase = (ReportRViewBase) this.Parent.Parent.Parent;
            if (reportRviewBase._executing)
            {
                this.ShowMessage(V6Text.DataLoading);
            }

            V6ControlFormHelper.ExportExcelTemplateHTKK(_ds.Tables[0], _ds.Tables[1],
                reportRviewBase.ReportDocumentParameters,
                reportRviewBase.MAU, reportRviewBase.LAN, reportRviewBase.ReportFile,
                reportRviewBase.ExcelTemplateFileFullHTKK, txtFileName.Text
                );
        }
        
        private void XuatExcelTaxOnline()
        {
            if (_ds == null || _ds.Tables.Count == 0)
            {
                this.ShowInfoMessage(V6Text.NoData);
                return;
            }
            if (txtFileName.Text == "")
            {
                this.ShowInfoMessage("Chưa chọn file lưu.");
                return;
            }

            var reportRviewBase = (ReportRViewBase) this.Parent.Parent.Parent;
            if (reportRviewBase._executing)
            {
                this.ShowMessage(V6Text.DataLoading);
            }

            V6ControlFormHelper.ExportExcelTemplateONLINE(_ds.Tables[0], _ds.Tables[1],
                reportRviewBase.ReportDocumentParameters,
                reportRviewBase.MAU, reportRviewBase.LAN, reportRviewBase.ReportFile,
                reportRviewBase.ExcelTemplateFileFullONLINE, txtFileName.Text
                );
        }

        private void btnChuyenExcelHTKK_Click(object sender, EventArgs e)
        {
            XuatExcelHTKK();
        }

        private void btnChuyenExcelTaxOnline_Click(object sender, EventArgs e)
        {
            XuatExcelTaxOnline();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            ChonFileExcelSaveAs();
        }

        private void btnSuaChiTieu_Click(object sender, EventArgs e)
        {
            try
            {
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                bool ctrl_is_down = (ModifierKeys & Keys.Control) == Keys.Control;
                if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK) return;
                string tableName = "V6MAPINFO";
                if (shift_is_down) tableName = "V6MAPINFO1";
                string keys = "UID,MA_TD1";//+ma_td1   1:VIETTEL    2:VNPT    3:BKAV
                var data = V6BusinessHelper.Select(tableName, "*", "LOAI = 'AAPPR_IXB2' and (MA_TD1='" + String1 + "' or ma_td1='0' or ma_td1='') order by GROUPNAME,GC_TD1").Data;
                if (ctrl_is_down) // Xuất Excel
                {
                    if (data.Columns.Contains("UID")) data.Columns.Remove("UID");
                    string fileName = V6ControlFormHelper.ExportExcel_ChooseFile(this, data, tableName + "_" + String1 + cboSendType.Text);

                    if (V6Options.AutoOpenExcel)
                    {
                        V6ControlFormHelper.OpenFileProcess(fileName);
                    }
                    else
                    {
                        this.ShowInfoMessage(V6Text.ExportFinish);
                    }
                }
                else
                {
                    IDictionary<string, object> defaultData = new Dictionary<string, object>();
                    defaultData.Add("LOAI", "AAPPR_IXB2");
                    V6ControlFormHelper.ShowDataEditorForm(this, data, tableName, null, keys, false, false, true, true,
                        defaultData);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "btnSuaChiTieu_Click", ex);
            }
        }

        private void cboSendType_SelectedIndexChanged(object sender, EventArgs e)
        {
            String1 = (cboSendType.SelectedIndex + 1).ToString();
        }

        
    }
}
