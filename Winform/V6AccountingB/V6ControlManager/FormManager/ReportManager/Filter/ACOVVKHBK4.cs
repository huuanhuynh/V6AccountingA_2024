using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOVVKHBK4 : FilterBase
    {
        public ACOVVKHBK4()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = false;
            F5 = false;

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

            Txtnh_vv1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vv2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vv3.VvarTextBox.SetInitFilter("loai_nh=3");
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
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {


                    //@StartDate varchar(8),
                    //@EndDate varchar(8),
                    //@Tk_cn varchar(50),
                    //@Detail int,
                    //@Condition nvarchar(Max) = ''


            var result = new List<SqlParameter>();

            //if (TxtTk.Text.Trim() == "")
            //{
            //    throw new Exception(V6Text.Text("CHUACHONTK"));
            //}
            // result.Add(new SqlParameter("@Tk", TxtTk.Text.Trim()));


            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;
            if (TxtTkcn.Text.Trim() == "")
            {
                throw new Exception(V6Text.Text("CHUACHONTK"));
            }
            result.Add(new SqlParameter("@Tk_cn", TxtTkcn.Text.Trim()));
            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));





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
                "TK","MA_DVCS","MA_KH","MA_VV"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9","NH_VV"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
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
                cKey = cKey + string.Format(" and ma_vv in (select ma_vv from alvv where {0} )", key2);
            }

            int Detail=1;

            if (rdo_khonggoptk.Checked)
                Detail = 1;
            else
                Detail = 0;

            result.Add(new SqlParameter("@Detail", Detail));
            result.Add(new SqlParameter("@Condition", cKey));
            return result;
        }
    }
}
