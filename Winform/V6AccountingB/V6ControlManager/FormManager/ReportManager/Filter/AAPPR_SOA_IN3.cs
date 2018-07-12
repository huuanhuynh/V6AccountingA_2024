using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPPR_SOA_IN3 : FilterBase
    {
        public AAPPR_SOA_IN3()
        {
            InitializeComponent();
            
            MyInit();
            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
            lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
            lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
            lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");
        }

        private void MyInit()
        {
            F4 = false;
            F4 = true;
            F5 = false;
            F9 = false;
            F10 = true;

            dateNgay_ct1.Value = V6Setting.M_SV_DATE;
            dateNgay_ct2.Value = V6Setting.M_SV_DATE;

            TxtXtag.Text = "2";

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            TxtMa_ct.Text = "SOA";
            TxtMa_ct.Enabled = false;
            chkHoaDonDaIn.Checked=true;
            String1ValueChanged += AAPPR_SOA_IN1_String1ValueChanged;
        }

        void AAPPR_SOA_IN1_String1ValueChanged(string oldvalue, string newvalue)
        {
            
        }

        //public override string Kieu_post
        //{
        //    get
        //    {
        //        return cboKieuPost.SelectedValue.ToString().Trim();
        //    }
        //}

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ma_ct", TxtMa_ct.Text.Trim()));
            var and = radAnd.Checked;
            
            var cKey = "";
            

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_BP", "MA_KH", "MA_NX"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
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

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }
            switch (TxtXtag.Text.Trim())
            {
                case "0":
                    cKey = cKey + " and ( Xtag=' ' or Xtag IS NULL )";
                    break;
                case "1":
                    cKey = cKey + " and ( Xtag='1' OR Kieu_post='1' )";
                    break;
                case "2":
                    cKey = cKey + " and ( Xtag='2'  OR Kieu_post='2')";
                    break;
            }
            if (chkHoaDonDaIn.Checked)
            {
                cKey = cKey + " and [Sl_in] > 0";
            }
            else
            {
                cKey = cKey + " and [Sl_in] = 0";
            }
          

            result.Add(new SqlParameter("@advance", cKey));

          
            return result;
        }

        

        private void chkHoaDonDaIn_CheckedChanged(object sender, System.EventArgs e)
        {
            
        }

        
    }
}
