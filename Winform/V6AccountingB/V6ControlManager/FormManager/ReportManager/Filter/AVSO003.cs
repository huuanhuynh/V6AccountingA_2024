using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AVSO003 : FilterBase
    {
        public AVSO003()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
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

            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

            chkGiamTru.Checked=true;

            SetHideFields(RTien);
        }
        public void SetHideFields(string Loaitien)
        {
            if (Loaitien == "VN")
            {
                _hideFields = new SortedDictionary<string, string>
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
            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;


            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@giam_tru", chkGiamTru.Checked ? 1 : 0));

            var and = radAnd.Checked;
            
            var cKey = "";
          

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_KHO","MA_DVCS","MA_BP", "MA_KH", "MA_VV", "MA_NX", "MA_VT","MA_NVIEN","MA_HTTT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3", "NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
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


            result.Add(new SqlParameter("@advance", cKey));
            return result;
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        
    }
}
