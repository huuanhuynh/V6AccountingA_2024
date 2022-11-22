using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTHUEBK2TT156_Filter : FilterBase
    {
        public AGLTHUEBK2TT156_Filter()
        {
            InitializeComponent();
            
            F3 = true;
            F5 = false;

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

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
            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@nhom_ct int,
            //@Condition varchar(50)

            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;


            
            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@nhom_ct", chkNhomCt.Checked?1:0));
            result.Add(new SqlParameter("@loai", txtLoai.Text.Trim()));


            var and = radAnd.Checked;
            var cKey = "";


            var key0 = GetFilterStringByFields(new List<string>()
            {
               "MA_DVCS","MA_KH","MA_CT","TK_THUE_CO","MA_THUE","MA_SONB"
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

            var reportRviewBase0 = this.Parent.Parent.Parent;
            if (reportRviewBase0 is ReportRViewBase)
            {
                var reportRviewBase = (ReportRViewBase)reportRviewBase0;
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
            else if (reportRviewBase0 is ReportR_DX)
            {
                var reportRviewBase = (ReportR_DX)reportRviewBase0;
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

            var reportRviewBase0 = this.Parent.Parent.Parent;
            if (reportRviewBase0 is ReportRViewBase)
            {
                var reportRviewBase = (ReportRViewBase)reportRviewBase0;
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
            else if (reportRviewBase0 is ReportR_DX)
            {
                var reportRviewBase = (ReportR_DX)reportRviewBase0;
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
        }

        private void btnChuyenExcelHTKK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFileName.Text == "")
                {
                    if (!Directory.Exists("C:\\V6EXCEL")) Directory.CreateDirectory("C:\\V6EXCEL");
                    txtFileName.Text = string.Format("C:\\V6EXCEL\\" + "HTKK_MUAVAO_{0:yyyyMMdd}_{1:yyyyMMdd}.xls",
                        dateNgay_ct1.Value, dateNgay_ct2.Value);
                }
                XuatExcelHTKK();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void btnChuyenExcelTaxOnline_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFileName.Text == "")
                {
                    if (!Directory.Exists("C:\\V6EXCEL")) Directory.CreateDirectory("C:\\V6EXCEL");
                    txtFileName.Text = string.Format("C:\\V6EXCEL\\" + "HTKK_BANRA_{0:yyyyMMdd}_{1:yyyyMMdd}.xls",
                        dateNgay_ct1.Value, dateNgay_ct2.Value);
                }
                XuatExcelTaxOnline();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + MethodBase.GetCurrentMethod().Name, ex);
            }
            
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            ChonFileExcelSaveAs();
        }

        
    }
}
