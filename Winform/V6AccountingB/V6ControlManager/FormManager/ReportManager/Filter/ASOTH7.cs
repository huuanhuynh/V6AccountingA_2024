using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ASOTH7: FilterBase
    {
        public ASOTH7()
        {
            InitializeComponent();
            F3 = false;
            F5 = false;
            
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;
            dateNgay_ct3.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct4.Value = V6Setting.M_ngay_ct2;


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

            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            //@ngay_ct1 smalldatetime,
            //@ngay_ct2 smalldatetime,
            //@ngay_ct3 smalldatetime,
            //@ngay_ct4 smalldatetime,
            //@condition as nvarchar(max)

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;


            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct3", dateNgay_ct3.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct4", dateNgay_ct4.Value.ToString("yyyyMMdd")));

            var and = radAnd.Checked;
            
            var cKey = "";
            

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS", "MA_KHO","MA_BP", "MA_KH", "MA_VV", "MA_NX", "MA_VT","MA_NVIEN"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
               "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey ="(MA_CT ='SOA') AND "+ string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = "(MA_CT ='SOA') AND " + string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "(MA_CT ='SOA')";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }

            if (!string.IsNullOrEmpty(key2))
            {
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }


            result.Add(new SqlParameter("@condition", cKey));

          
            return result;
        }

        
    }
}
