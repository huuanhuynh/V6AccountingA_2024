using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTH1 : FilterBase
    {
        public AGLTH1()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;

            TxtTk.Text = (V6Setting.M_TK ?? "111").Trim();
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);


            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
           
            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields = new SortedDictionary<string, string>();

                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
                GridViewHideFields.Add("MA_TT", "MA_TT");
                GridViewHideFields.Add("MA_GD", "MA_GD");

                GridViewHideFields.Add("T_TT_NT0", "T_TT_NT0");
                GridViewHideFields.Add("T_TT_NT", "T_TT_NT");
                GridViewHideFields.Add("DA_TT_NT", "DA_TT_NT");
                GridViewHideFields.Add("CON_PT_NT", "CON_PT_NT");
                GridViewHideFields.Add("T_TIEN_NT2", "T_TIEN_NT2");
                GridViewHideFields.Add("T_THUE_NT", "T_THUE_NT");

                               

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

                // @Tk varchar(16),
                //@Loai_tk int,
                //@StartDate varchar(8),
                //@EndDate varchar(8),
                //@Advance nvarchar(max)
            var result = new List<SqlParameter>();

            
            if (TxtTk.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
            }

            V6Setting.M_TK = TxtTk.Text;
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            result.Add(new SqlParameter("@Tk", TxtTk.Text.Trim()));
            int loai_tk = 0;
            if (rdo_All.Checked) loai_tk = 1;

            result.Add(new SqlParameter("@Loai_tk", loai_tk));
            
            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
                                              
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

            result.Add(new SqlParameter("@advance", cKey));
            return result;
        }

        
    }
}
