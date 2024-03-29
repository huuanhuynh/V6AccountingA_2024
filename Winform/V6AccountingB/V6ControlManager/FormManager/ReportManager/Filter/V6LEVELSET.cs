﻿using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class V6LEVELSET: FilterBase
    {
        public V6LEVELSET()
        {
            InitializeComponent();
            F3 = true;
            F4 = false;
            F5 = false;
            F9 = true;
            F8 = true;
            
            dateNgay_ct1.SetValue(V6Setting.M_SV_DATE);
            dateNgay_ct2.SetValue(V6Setting.M_SV_DATE);

            TxtXkieu_post.Text = "2";
            ctDenSo.Enabled = false;
            chkKhoaPhieu.Checked = false;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            TxtMa_ct.Text = "SOA";
            //TxtMa_ct.Enabled = false;

            //Get login user level
            string loginLevel = V6Login.Level;

            cboLevel.ValueMember = "ma_level";
            cboLevel.DisplayMember = V6Setting.IsVietnamese ? "ten_level" : "ten_level2";
            cboLevel.DataSource = V6BusinessHelper.Select("AlLevel", "ma_level,ten_level,ten_level2",
                                    " RIGHT(ma_level,1)>='" + loginLevel.Trim().Right(1) + "'", "", "ma_level").Data;
            cboLevel.ValueMember = "ma_level";
            cboLevel.DisplayMember = V6Setting.IsVietnamese ? "ten_level" : "ten_level2";


            cboLevel1.ValueMember = "ma_level1";
            cboLevel1.DisplayMember = V6Setting.IsVietnamese ? "ten_level" : "ten_level2";
            cboLevel1.DataSource = V6BusinessHelper.Select("AlLevel", "ma_level as ma_level1,ten_level,ten_level2",
                                    " RIGHT(ma_level,1)>='" + loginLevel.Trim().Right(1)+"'", "", "ma_level1").Data;
            cboLevel1.ValueMember = "ma_level1";
            cboLevel1.DisplayMember = V6Setting.IsVietnamese ? "ten_level" : "ten_level2";
            
            
            cboLevel.SelectedValue = loginLevel;
            cboLevel1.SelectedValue = loginLevel;

            cboLevel.Enabled = false;

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
        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            String1 = TxtMa_ct.Text;
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
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

            switch (TxtXkieu_post.Text.Trim())
            {
                case "0":
                    cKey = cKey + " and ( Xtag=' ' or Xtag IS NULL )";
                    break;
                case "1":
                    cKey = cKey + " and ( Kieu_post='1' )";
                    break;
                case "2":
                    cKey = cKey + " and ( Kieu_post='2')";
                    break;
                case "5":
                    cKey = cKey + " and ( Kieu_post='5')";
                    break;
            }

            if (chkKhoaPhieu.Checked)
            {

                cKey = cKey + " and ( ISNULL(Xtag,'')<>'')";
                switch (cboLevel1.SelectedValue.ToString().Trim())
                {
                    case "01":
                        cKey = cKey + " and ( Xtag='1')";
                        break;
                    case "02":
                        cKey = cKey + " and ( Xtag='2')";
                        break;
                    case "03":
                        cKey = cKey + " and ( Xtag='3')";
                        break;
                    case "04":
                        cKey = cKey + " and ( Xtag='4')";
                        break;
                    case "05":
                        cKey = cKey + " and ( ISNULL(Xtag,'')='')";
                        break;
                }

            }
            else
            {
                cKey = cKey + " and ( ISNULL(Xtag,'')='')";
            }



            // Tu so den so
            var tu_so = ctTuSo.Text.Trim().Replace("'", "");
            var den_so = ctDenSo.Text.Trim().Replace("'", "");
            var invoice = new V6AccountingBusiness.Invoices.V6Invoice81();
            var and_or = " and ";
            var tbL = "";
            //so chung tu
            if (chkLike.Checked)
            {
                if (tu_so != "")
                {
                    cKey += (cKey.Length > 0 ? and_or : "")
                       + string.Format("so_ct like '%{0}'",
                        tu_so + ((tu_so.Contains("_") || tu_so.Contains("%")) ? "" : "%"));
                }
            }
            else
            {
                var dinh_dang = invoice.Alct["DinhDang"].ToString().Trim();
                if (!string.IsNullOrEmpty(dinh_dang))
                {
                    if (tu_so != "") tu_so = (dinh_dang + tu_so).Right(dinh_dang.Length);
                    if (den_so != "") den_so = (dinh_dang + den_so).Right(dinh_dang.Length);
                }
                if (tu_so != "" && den_so == "")
                {
                    cKey += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                        cKey.Length > 0 ? and_or : "",
                        tbL,
                        tu_so);
                }
                else if (tu_so == "" && den_so != "")
                {
                    cKey += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                       cKey.Length > 0 ? and_or : "",
                       tbL,
                       den_so);
                }
                else if (tu_so != "" && den_so != "")
                {
                    cKey += string.Format("{0} (LTrim(RTrim({1}so_ct)) >= '{2}' and LTrim(RTrim({1}so_ct)) <= '{3}')",
                        cKey.Length > 0 ? and_or : "",
                        tbL,
                        tu_so, den_so)
                    ;
                }
            }


            result.Add(new SqlParameter("@advance", cKey));

          
            return result;
        }

        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }

        private void TxtXkieu_post_TextChanged(object sender, System.EventArgs e)
        {
            Kieu_post = TxtXkieu_post.Text;
        }

    }
}
