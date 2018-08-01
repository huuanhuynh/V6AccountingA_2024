using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ASOBK2 : FilterBase
    {
        public ASOBK2()
        {
            InitializeComponent();

            F3 = true;
            F5 = false;

            TxtMakho.VvarTextBox.Text = (V6Setting.M_Ma_kho ?? "").Trim();

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
              
            }
            else
            {
                
            }
            GridViewHideFields.Add("STT_REC", "STT_REC");
            GridViewHideFields.Add("STT_REC0", "STT_REC0");
            
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@Condition nvarchar(max),
            //@Ma_ct VARCHAR(50) = ''

            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("NGAY_CT1", dateNgay_ct1.Date);
            RptExtraParameters.Add("NGAY_CT2", dateNgay_ct2.Date);


            RptExtraParameters.Add("MA_KHO", TxtMakho.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");
            RptExtraParameters.Add("MA_VT", TxtMa_vt.Text.Trim());

            if (TxtMa_vt.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn mã vật tư!");
            }
            
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;
            


            var result = new List<SqlParameter>();


            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@MaVt", TxtMa_vt.Text.Trim()));


            var and = radAnd.Checked;

            var cKey = "1=1";
 
            
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KHO","MA_KH","MA_BP","MA_VV","MA_NX","MA_NVIEN"
            }, and);
            
            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey =cKey+" AND "+ string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = cKey + " AND " + string.Format("(1=2 OR {0})", key0);
                }
            }
           
             var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);


             if (!string.IsNullOrEmpty(key1))
             {
                 cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
             }

            result.Add(new SqlParameter("@advance", cKey));
            


          

            return result;
        }

        
    }
}
