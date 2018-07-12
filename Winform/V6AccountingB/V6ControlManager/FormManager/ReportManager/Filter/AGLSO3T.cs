using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLSO3T : FilterBase
    {
        public AGLSO3T()
        {
            InitializeComponent();
           
            F3 = true;
            F5 = false;

            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;

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
            //@ngay_ct1 char(8),
            //@ngay_ct2 char(8),
            //@condition varchar(max)

            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;
                    
            
            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));

                                  
           
            var and = radAnd.Checked;

            string cKey;

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
                    cKey =" AND "+ string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = " AND " + string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = " AND 1=1";
            }

            // TxtTk.Text="111,112"
            if (TxtTk_no.Text != "")
            {
                var ss = TxtTk_no.Text.Split(',');
                var orString = "";
                foreach (string s in ss)
                {
                    orString += string.Format(" OR TK Like '{0}%'", s.Trim());
                }
                orString = orString.Substring(4);
                cKey = cKey + " AND (ISNULL(PS_NO,0)<>0) ";
                cKey = cKey + string.Format(" AND ({0})", orString);


                if (TxtTk_Co.Text != "")
                {
                    var ss2 = TxtTk_Co.Text.Split(',');
                    var orString2 = "";
                    foreach (string s in ss2)
                    {
                        orString2 += string.Format(" OR TK_DU Like '{0}%'", s.Trim());
                    }
                    orString2 = orString2.Substring(4);
                    cKey = cKey + string.Format(" AND ({0})", orString2);
                }


            }
            else
            {
                if (TxtTk_Co.Text != "")
                {
                    var ss = TxtTk_Co.Text.Split(',');
                    var orString = "";
                    foreach (string s in ss)
                    {
                        orString += string.Format(" OR TK Like '{0}%'", s.Trim());
                    }
                    orString = orString.Substring(4);
                    cKey = cKey + " AND (ISNULL(PS_CO,0)<>0) ";
                    cKey = cKey + string.Format(" AND ({0})", orString);
                }
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }

            result.Add(new SqlParameter("@condition", cKey));
            return result;

            
            
            
        }

        
    }
}
