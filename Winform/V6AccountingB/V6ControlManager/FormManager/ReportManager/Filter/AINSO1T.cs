using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINSO1T : FilterBase
    {
        public AINSO1T()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;
            F7 = true;
            F9 = true;
            F10 = true;


         
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

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
            GridViewHideFields = new SortedDictionary<string, string>();
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
                    {"T_TT_NT0", "T_TT_NT0"},
                    {"T_TT_NT", "T_TT_NT"},
                    {"DA_TT_NT", "DA_TT_NT"},
                    {"CON_PT_NT", "CON_PT_NT"},
                    {"T_TIEN_NT2", "T_TIEN_NT2"},
                    {"T_THUE_NT", "T_THUE_NT"}
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

            //@Ngay_ct1 SmallDateTime, --Từ ngày
            //@Ngay_ct2 SmallDateTime, --Đến ngày
            //@Advance AS VARCHAR(max) = ''-- Điều kiện lọc
              String1 = TxtMakho.StringValue;

              var result = new List<SqlParameter>();

         
            
           
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            int tinh_dc = 0;
            if (Chk_Tinh_dc.Checked) tinh_dc = 1;
            result.Add(new SqlParameter("@Tinh_dc", tinh_dc));
            var and = radAnd.Checked;

            var cKey = "";
           
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KHO","MA_VT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6","MA_QG", "MA_NSX", "TK_VT"
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
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key1);
            }

            if (!string.IsNullOrEmpty(key2))
            {
                //  cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }


            result.Add(new SqlParameter("@Advance", cKey));
            return result;
            
        }

        private void rbtLAN_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rTiengViet.Checked) ObjectDictionary["VEBC"] = "V";
            if (rEnglish.Checked) ObjectDictionary["VEBC"] = "E";
            if (rBothLang.Checked) ObjectDictionary["VEBC"] = "B";
            if (rCurrent.Checked) ObjectDictionary["VEBC"] = "C";
        }

        private void rTienViet_CheckedChanged(object sender, System.EventArgs e)
        {
            ObjectDictionary["VN_FC"] = rTienViet.Checked ? "VN" : "FC";
        }
        
    }
}
