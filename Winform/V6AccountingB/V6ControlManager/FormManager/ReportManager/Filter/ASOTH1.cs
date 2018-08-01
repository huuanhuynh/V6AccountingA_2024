using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ASOTH1: FilterBase
    {
        public ASOTH1()
        {
            InitializeComponent();
            F3 = false;
            F5 = true;
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            String2 = "TEN_VT";
            String1 = "MA_VT";

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            chkGiamTru.Checked = true;
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


            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@giam_tru", chkGiamTru.Checked ? 1 : 0));



            var and = radAnd.Checked;
            
            var cKey = "";
          

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_KHO","MA_DVCS","MA_BP", "MA_KH", "MA_VV", "MA_NX", "MA_VT","MA_NVIEN"
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
                    cKey =  string.Format("(1=1 AND {0})", key0);
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
            if (chk_tang.Checked)
            {
                cKey = cKey + " and (isnull(tang,'')<> '' )";
            }

            result.Add(new SqlParameter("@advance", cKey));
            result.Add(new SqlParameter("@Group", lblGroupString.Text));

            return result;
        }
        private void NH_KH_TextChanged(object sender, System.EventArgs e)
        {
            var current = sender as TextBox;
            if (current != null)
                foreach (Control control in groupBoxNhom.Controls)
                {
                    if (control != current && control.Text == current.Text)
                    {
                        control.Text = "0";
                    }
                }

            String2 = V6BusinessHelper.GenGroup(
                "TEN_NH", NH_VT1.Text, NH_VT2.Text, NH_VT3.Text, NH_VT4.Text, NH_VT5.Text, NH_VT6.Text);
            if (String2.Length > 0) String2 += ",";
            String2 += "TEN_VT";

            lblGroupString.Text = V6BusinessHelper.GenGroup(
                "NH_VT", NH_VT1.Text, NH_VT2.Text, NH_VT3.Text, NH_VT4.Text, NH_VT5.Text, NH_VT6.Text);
            String1 = lblGroupString.Text + (lblGroupString.Text.Length > 0 ? "," : "") + "MA_VT";
        }

        
        
    }
}
