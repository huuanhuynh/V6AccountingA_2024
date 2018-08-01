using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPPR_IND: FilterBase
    {
        public AAPPR_IND()
        {
            InitializeComponent();
            F3 = true;
            F4 = true;
            F7 = true;
            F8 = true;
            F9 = true;
            F10 = true;
            dateNgay_ct1.SetValue(V6Setting.M_SV_DATE);
            dateNgay_ct2.SetValue(V6Setting.M_SV_DATE);

            TxtXtag.Text = "2";
            
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            TxtMa_ct.Text = "IND,IXB";
            TxtMa_ct.Enabled = false;
            chkDaXuLy.Checked = false;

            cboKieuPost.ValueMember = "kieu_post";
            cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
            cboKieuPost.DataSource = V6BusinessHelper.Select("AlPost", "Kieu_post,Ten_post,Ten_post2",
                                "dbo.VFV_InList0(ma_ct,@mact,',')=1", "", "Kieu_post",
                                new SqlParameter("@mact", "IND")).Data;
            cboKieuPost.ValueMember = "kieu_post";
            cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
            

            cboKieuPost.SelectedValue = "2";
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
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@ma_ct", TxtMa_ct.Text.Trim()));
            var and = radAnd.Checked;
            
            var cKey = "";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_BP", "MA_NVIEN", "MA_NX", "MA_XULY", "MA_KH"
            }, and, "a");

            //var key0Data = new Dictionary<string, object>();
            //if (txtMaDvcs.IsSelected) key0Data.Add("MA_DVCS", txtMaDvcs.StringValue);
            //if (lineMaBoPhan.IsSelected) key0Data.Add("MA_BP", lineMaBoPhan.StringValue);
            //if (lineMaNvien.IsSelected) key0Data.Add("MA_NVIEN", lineMaNvien.StringValue);
            //if (lineMa_xuly.IsSelected) key0Data.Add("MA_XULY", lineMa_xuly.StringValue);
            //if (lineMaKh.IsSelected) key0Data.Add("MA_KH", lineMaKh.StringValue);
            //if (lineMaNX.IsSelected) key0Data.Add("MA_NX", lineMaNX.StringValue);

            //var key0 = SqlGenerator.GenWhere2(key0Data, "like", true, "a", false);

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
                cKey = cKey + string.Format(" and a.ma_kh in (select ma_kh from alkh where {0} )", key1);
            }

            
            if (chkDaXuLy.Checked)
            {
                cKey = cKey + " and ISNULL(a.Ma_xuly,'')<>''";
            }
            else
            {
                cKey = cKey + " and ISNULL(a.Ma_xuly,'')=''";
            }
            //switch (TxtXtag.Text.Trim())
            //{
            //    case "0":
            //        cKey = cKey + " and ( Xtag=' ' or Xtag IS NULL )";
            //        break;
            //    case "1":
            //        cKey = cKey + " and ( Xtag='1')";
            //        break;
            //    case "2":
            //        cKey = cKey + " and ( Xtag='2')";
            //        break;
            //}
          

            result.Add(new SqlParameter("@advance", cKey));

          
            return result;
        }

        private void lineMa_xuly_Leave(object sender, System.EventArgs e)
        {
            if (lineMa_xuly.IsSelected)
            {
                chkDaXuLy.Checked = true;
            }
            else
            {
                chkDaXuLy.Checked = false;    
            }
        }

    }
}
