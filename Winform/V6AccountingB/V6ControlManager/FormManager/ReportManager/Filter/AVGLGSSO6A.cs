using System.Collections.Generic;
using System.Data.SqlClient;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AVGLGSSO6A : FilterBase
    {
        public AVGLGSSO6A()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;
            F6 = true;
            F7 = true;
            F9 = true;
            F10 = false;

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            lineMaDVCS.VvarTextBox.Text = V6Login.Madvcs;
            
            if (V6Login.MadvcsCount <= 1)
            {
                lineMaDVCS.Enabled = false;
            }

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

        public override void UpdateValues()
        {
            ObjectDictionary["VN_FC"] = rTienViet.Checked ? "VN" : "FC";
            if (rTiengViet.Checked) ObjectDictionary["VEBC"] = "V";
            if (rEnglish.Checked) ObjectDictionary["VEBC"] = "E";
            if (rBothLang.Checked) ObjectDictionary["VEBC"] = "B";
            if (rCurrent.Checked) ObjectDictionary["VEBC"] = "C";
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            UpdateValues();
            //ngay_ct1
            //ngay_ct2
            //so_ct1
            //so_ct2
            //ct_goc
            //Advance
            //cLan
            //User_id
            var result = new List<SqlParameter>();
            
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@so_ct1", ctTuSo.Text.Trim()));
            result.Add(new SqlParameter("@so_ct2", ctDenSo.Text.Trim()));
            result.Add(new SqlParameter("@ct_goc", ""));

            var and = radAnd.Checked;

            var cKey = "";
           
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS"
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
            
            result.Add(new SqlParameter("@Advance", cKey));
            ReportRViewBase parent = V6ControlFormHelper.FindParent<ReportRViewBase>(this) as ReportRViewBase;
            result.Add(parent != null ? new SqlParameter("@cLan", parent.LAN) : new SqlParameter("@cLan", "V"));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            return result;
            
        }


        private void rTienViet_CheckedChanged(object sender, System.EventArgs e)
        {
            ObjectDictionary["VN_FC"] = rTienViet.Checked ? "VN" : "FC";
        }

        private void rbtLAN_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rTiengViet.Checked) ObjectDictionary["VEBC"] = "V";
            if (rEnglish.Checked) ObjectDictionary["VEBC"] = "E";
            if (rBothLang.Checked) ObjectDictionary["VEBC"] = "B";
            if (rCurrent.Checked) ObjectDictionary["VEBC"] = "C";
        }

    }
}
