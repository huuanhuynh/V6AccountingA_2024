using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARTTBK7 : FilterBase
    {
        public AARTTBK7()
        {
            InitializeComponent();
            txtTk.VvarTextBox.SetInitFilter("tk_cn=1");

            F3 = true;
            F5 = false;

            TxtKieu_bc.Text = "0";
            txtTk.VvarTextBox.Text = (V6Setting.M_TK_CN ?? "131").Trim();

            ctDenSo.Enabled = false;
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

            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");
            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");
                _hideFields.Add("STT_REC0", "STT_REC0");
                _hideFields.Add("STT_REC_TT", "STT_REC_TT");
                _hideFields.Add("MA_TT", "MA_TT");
                _hideFields.Add("MA_GD", "MA_GD");

                _hideFields.Add("T_TT_NT0", "T_TT_NT0");
                _hideFields.Add("T_TT_NT", "T_TT_NT");
                _hideFields.Add("DA_TT_NT", "DA_TT_NT");
                _hideFields.Add("CON_PT_NT", "CON_PT_NT");
                _hideFields.Add("T_TIEN_NT2", "T_TIEN_NT2");
                _hideFields.Add("T_THUE_NT", "T_THUE_NT");

                               

            }
            else
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");
                _hideFields.Add("STT_REC0", "STT_REC0");
                _hideFields.Add("STT_REC_TT", "STT_REC_TT");
                _hideFields.Add("MA_TT", "MA_TT");
                _hideFields.Add("MA_GD", "MA_GD");

            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@Ngay_ct1 SmallDateTime,
            //@Ngay_ct2 SmallDateTime,
            //@cKey nvarchar(MAX)

            var result = new List<SqlParameter>();

            if (txtTk.VvarTextBox.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
            }

            V6Setting.M_TK = txtTk.VvarTextBox.Text;
            V6Setting.M_TK_CN = txtTk.VvarTextBox.Text;
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;


            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
          
            
            var and = radAnd.Checked;
            
            string cKey;
            string cKey_hd;
            string cKey2;
            
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KH","MA_BP","MA_NVIEN","TK"
            }, and);

            var key0_hd = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KH","TK","MA_HTTT"
            }, and);

            //"MA_BP","MA_NVIEN"

            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);

            var key1_hd = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);

            var key2_hd = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3", "NH_VT4", "NH_VT5", "NH_VT6","MA_VT"
                
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

            if (!string.IsNullOrEmpty(key0_hd))
            {
                if (and)
                {
                    cKey_hd = string.Format("(1=1 AND {0})", key0_hd);
                }
                else
                {
                    cKey_hd = string.Format("(1=2 OR {0})", key0_hd);
                }
            }
            else
            {
                cKey_hd = "1=1";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }
            if (!string.IsNullOrEmpty(key1_hd))
            {
                cKey_hd = cKey_hd + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1_hd);
            }

            if (!string.IsNullOrEmpty(key2_hd))
            {
                cKey_hd = cKey_hd + string.Format(" and stt_rec in (select DISTINCT stt_rec from ARI70 where ma_ct IN ('SOA','SOF') AND ma_vt in (select ma_vt from alvt where {0} ))", key2_hd);
            }

            // {Key2: ARA00 TK AND MA_CT IN 
            if (filterLinema_ct.IsSelected || filterLinetk_thu.IsSelected)
            {
                var key2 = GetFilterStringByFields(new List<string>()
                {
                    "TK#","MA_CT#","MA_DVCS"
                }, and);

                key2 = key2.Replace("#", "");

                if (!string.IsNullOrEmpty(key2))
                {
                    if (and)
                    {
                        cKey2 = string.Format("(1=1 AND {0})", key2);
                    }
                    else
                    {
                        cKey2 = string.Format("(1=2 OR {0})", key2);
                    }
                }
                else
                {
                    cKey2 = "1=1";
                }


                if ((!string.IsNullOrEmpty(cKey2)) && (cKey2 != "1=1"))
                {
                    cKey = cKey +
                           string.Format(
                               " and stt_rec in (select DISTINCT stt_rec from ARA00 where ({0}) and ngay_ct>= '{1}' and ngay_ct<='{2}')",
                               key2, dateNgay_ct1.Value.ToString("yyyyMMdd"), dateNgay_ct2.Value.ToString("yyyyMMdd"));
                }
            }

            //}

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

            

            result.Add(new SqlParameter("@cKey", cKey));
            result.Add(new SqlParameter("@cKey_hd", cKey_hd));
            result.Add(new SqlParameter("@Kieu_bc", TxtKieu_bc.Text.Trim()));
            return result;
        }

        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }



   
    }
}
