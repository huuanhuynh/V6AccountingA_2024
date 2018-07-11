using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOVVSD1DK : FilterBase
    {
        public ACOVVSD1DK()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = false;
            F5 = false;

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

            Txtnh_vv1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vv2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vv3.VvarTextBox.SetInitFilter("loai_nh=3");
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


                    //@du_kh_yn int,
                    //@date varchar(12),
                    //@filter nvarchar(max),
                    //@condition nvarchar(max)


            var result = new List<SqlParameter>();

            //if (TxtTk.Text.Trim() == "")
            //{
            //    throw new Exception("Chưa chọn tài khoản!");
            //}
            // result.Add(new SqlParameter("@Tk", TxtTk.Text.Trim()));


            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;
            int Detail = 1;

            if (rdo_khonggoptk.Checked)
                Detail = 0;
            else
                Detail = 1;
            result.Add(new SqlParameter("@du_kh_yn", Detail));
            result.Add(new SqlParameter("@Date", dateNgay_ct2.Value.ToString("yyyyMMdd")));

            var and = radAnd.Checked;

            var cKey = "";
            var cKey_Filter = "1=1";
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
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_VV"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                //"NH_VT1","NH_VT2","NH_VT3", "MA_QG", "MA_NSX", "TK_VT"
                ""
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
                //  cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }
            if (!string.IsNullOrEmpty(key1))
            {
                cKey_Filter = cKey_Filter + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key1);
            }

            result.Add(new SqlParameter("@Filter", cKey_Filter));
            result.Add(new SqlParameter("@Condition", cKey));
            return result;
        }
    }
}
