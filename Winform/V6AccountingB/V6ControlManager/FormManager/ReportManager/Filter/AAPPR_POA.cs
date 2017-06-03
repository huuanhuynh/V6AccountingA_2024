using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Init;
using V6SqlConnect;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPPR_POA: FilterBase
    {
        public AAPPR_POA()
        {
            InitializeComponent();
            F3 = true;
            F4 = true;
            F5 = false;
            F9 = true;
            
            dateNgay_ct1.Value = V6Setting.M_SV_DATE;
            dateNgay_ct2.Value = V6Setting.M_SV_DATE;

            TxtXtag.Text = "0";
            
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            TxtMa_ct.Text = "POA,POB";
            TxtMa_ct.Enabled = false;

            cboKieuPost.ValueMember = "kieu_post";
            cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
            cboKieuPost.DataSource = V6BusinessHelper.Select("AlPost", "Kieu_post,Ten_post,Ten_post2",
                                "dbo.VFV_InList0(ma_ct,@mact,',')=1", "", "Kieu_post",
                                new SqlParameter("@mact", "POA")).Data;
            cboKieuPost.ValueMember = "kieu_post";
            cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
            

            cboKieuPost.SelectedValue = "2";
            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
        }

        public override string Kieu_post
        {
            get
            {
                return cboKieuPost.SelectedValue.ToString().Trim();
            }
        }

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
            
            //var key0 = GetFilterStringByFields(new List<string>()
            //{
            //    "MA_DVCS","MA_BP", "MA_KH", "MA_NX"
            //}, and);
            var key0Data = new Dictionary<string, object>();
            if(txtMaDvcs.IsSelected) key0Data.Add("MA_DVCS", txtMaDvcs.StringValue);
            if (lineMaBoPhan.IsSelected) key0Data.Add("MA_BP", lineMaBoPhan.StringValue);
            if (lineMaKh.IsSelected) key0Data.Add("MA_KH", lineMaKh.StringValue);
            if (lineMaNX.IsSelected) key0Data.Add("MA_NX", lineMaNX.StringValue);
            
            var key0 = SqlGenerator.GenWhere2(key0Data, "like", true, "a", false);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
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
                cKey = cKey + string.Format(" and a.ma_kh in (select ma_kh from alkh where {0} )", key1);
            }
            switch (TxtXtag.Text.Trim())
            {
                case "0":
                    cKey = cKey + " and ( Xtag=' ' or Xtag IS NULL )";
                    break;
                case "1":
                    cKey = cKey + " and ( Xtag='1')";
                    break;
                case "2":
                    cKey = cKey + " and ( Xtag='2')";
                    break;
            }
          

            result.Add(new SqlParameter("@advance", cKey));

          
            return result;
        }

    }
}
