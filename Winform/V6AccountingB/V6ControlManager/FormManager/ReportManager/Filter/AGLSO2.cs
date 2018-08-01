using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLSO2 : FilterBase
    {
        public AGLSO2()
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

            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
            lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
            lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
            lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");

            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
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
            //@StartDate	varchar(50),
            //@EndDate	varchar(50),
            //@LoaiBC		int,--0: theo tk; 1: theo tk_du
            //@bo_nkcd	int,
            //@Advance nvarchar(max),
            //@User_id INT = 1

            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            var loaibc = 0;
            var bo_nkcd = 0;

            if (rdo_khonggoptk.Checked)
            {
                loaibc = 1;
            }
            if (chk_bo_nkcd.Checked)
            {
                bo_nkcd = 1;
            }


            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@LoaiBC", loaibc));
            result.Add(new SqlParameter("@bo_nkcd", bo_nkcd));
                                  
           
            var and = radAnd.Checked;

            string cKey="1=1";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_BP", "MA_KH","MA_NVIEN"

            }, and);

            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);


            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey = cKey + " AND " + string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = cKey + " AND " + string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = " 1=1";
            }

           
            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }

            result.Add(new SqlParameter("@Advance", cKey));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));

            return result;

            
            
            
        }

        
    }
}
