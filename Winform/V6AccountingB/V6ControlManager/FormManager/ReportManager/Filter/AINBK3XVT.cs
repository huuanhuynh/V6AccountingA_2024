﻿using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINBK3XVT : FilterBase
    {
        public AINBK3XVT()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);
            ctDenSo.Enabled = false;
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

            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

            SetHideFields(RTien);
        }
        public void SetHideFields(string Loaitien)
        {
            if (Loaitien == "VN")
            {
                GridViewHideFields = new SortedDictionary<string, string>
                {
                    {"TAG", "TAG"},
                    {"STT_REC", "STT_REC"},
                    {"STT_REC0", "STT_REC0"},
                    {"STT_REC_TT", "STT_REC_TT"},
                    {"MA_TT", "MA_TT"},
                    {"MA_GD", "MA_GD"},
                    {"PT_NT", "PT_NT"},
                    {"TIEN_NT2", "TIEN_NT2"},
                    {"TIEN_NT", "TIEN_NT"},
                    {"CK_NT", "CK_NT"},
                    {"GG_NT", "GG_NT"},
                    {"LAI_NT", "LAI_NT"},
                    {"TEN_VT2", "TEN_VT2"},
                    {"GIA_NT2", "GIA_NT2"},
                    {"GIA_NT21", "GIA_NT21"},
                    {"GIA_NT", "GIA_NT"},
                    {"THUE_NT", "THUE_NT"}
                };
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
            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

                //@ngay_ct1 SMALLDATETIME,
                //@ngay_ct2 SMALLDATETIME,
	            //@condition as nvarchar(max),
	            //@pGroup as nvarchar(max)
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@pGroup", "MA_VT"));


            var and = radAnd.Checked;
            
            var cKey = "";
          

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_KHO","MA_DVCS","MA_BP", "MA_KH", "MA_VV", "MA_NX", "MA_VT","MA_NVIEN","MA_CT","MA_GD"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
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

            if (!string.IsNullOrEmpty(key2))
            {
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }

            cKey = cKey + " AND (NXT=2)";


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
            

            result.Add(new SqlParameter("@condition", cKey));
            return result;
        }

        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }

        
    }
}
