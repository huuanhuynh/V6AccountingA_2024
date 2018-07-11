using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOVVTH2 : FilterBase
    {
        public ACOVVTH2()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = false;
            F5 = true;

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

            Txtnh_vv1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vv2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vv3.VvarTextBox.SetInitFilter("loai_nh=3");

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
            if (lang == "V")
            {
                _hideFields = new SortedDictionary<string, string>();
                _hideFields.Add("TAG", "TAG");
            }
            else
            {

            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {


                    //@StartDate varchar(8),
                    //@EndDate varchar(8),
                    //@condition varchar(4000)


            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;
       
            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));





            var and = radAnd.Checked;

            var cKey = "";
            //foreach (Control c in groupBox1.Controls)
            //{
            //    var line = c as FilterLineBase;
            //    if (line != null)
            //    {
            //        cKey += line.IsSelected ? ((and?"\nand ": "\nor  ") + line.Query) : "";
            //    }
            //}

            //if (cKey.Length > 0)
            //{
            //    if (and)
            //    {
            //        cKey = string.Format("(1=1 {0})", cKey);
            //    }
            //    else
            //    {
            //        cKey = string.Format("(1=2 {0})", cKey);
            //    }
            //}
            //else
            //{
            //    cKey = "1=1";
            //}

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KH","MA_VV","MA_KHO","MA_VT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
             "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6",
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6","MA_QG", "MA_NSX", "TK_VT"
            }, and);
            var key3 = GetFilterStringByFields(new List<string>()
            {
                "NH_VV1", "NH_VV2", "NH_VV3",
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
            if (!string.IsNullOrEmpty(key3))
            {
                cKey = cKey + string.Format(" and ma_vv in (select ma_vv from alvv where {0} )", key3);
            }

            result.Add(new SqlParameter("@advance", cKey));
            return result;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
