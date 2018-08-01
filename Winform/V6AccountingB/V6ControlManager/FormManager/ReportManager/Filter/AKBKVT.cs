using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AKBKVT : FilterBase
    {
        public AKBKVT()
        {
            InitializeComponent();

            Txttype.Text = "*";
            
            F3 = true;

            TxtMakho.VvarTextBox.Text = (V6Setting.M_Ma_kho ?? "").Trim();
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

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
            if (lang == "V")
            {
                GridViewHideFields = new SortedDictionary<string, string>();
                GridViewHideFields.Add("TAG", "TAG");
              
            }
            else
            {
                
            }
            GridViewHideFields.Add("TIEN_NT_N", "TIEN_NT_N");
            GridViewHideFields.Add("TIEN_NT_X", "TIEN_NT_X");
            GridViewHideFields.Add("DU_DAU_NT", "DU_DAU_NT");
            GridViewHideFields.Add("DU_CUOI_NT", "DU_CUOI_NT");
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

           //@StartDate varchar(8),
           //@EndDate varchar(8),
           //@Type varchar(1),
           //@Condition nvarchar(max)

            if (TxtMakho.StringValue != "")
            {
                V6Setting.M_Ma_kho = TxtMakho.StringValue;
            }
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("NGAY_CT1", dateNgay_ct1.Date);
            RptExtraParameters.Add("NGAY_CT2", dateNgay_ct2.Date);


            RptExtraParameters.Add("MA_KHO", TxtMakho.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");
            //_parameters.Add("MA_VT", TxtMa_vt.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");

            var result = new List<SqlParameter>();

                        
            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@Type", Txttype.Text.Trim()));


            var and = radAnd.Checked;
            
            var cKey = "";
           

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KHO","MA_VT","MA_BPHT","MA_BP", "MA_KH", "MA_VV","MA_SP","MA_PHI","MA_KU","MA_CT","MA_LO","MA_SONB"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3", "NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
            }, and);

            var key2 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);
            
          
            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey += string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey += string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key1);
            }
            if (!string.IsNullOrEmpty(key2))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key2);
            }
          


            result.Add(new SqlParameter("@Condition", cKey));

            return result;
        }

        
    }
}
