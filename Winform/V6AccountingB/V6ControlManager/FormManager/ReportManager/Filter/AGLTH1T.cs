using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTH1T : FilterBase
    {
        public AGLTH1T()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;
            F7 = true;
            F9 = true;
            F10 = true;

            TxtTk_sc.Text = "2";
            txtbac_tk.Value = 0;
          
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            SetHideFields(RTien);
            LoadLanguage();
            Ready();
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
            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@Advance AS NVARCHAR(MAX),
            //@Bac_tk int=1,
            //@Tk_sc INT,
            //@User_id int,
            //@Loai_tk int

            var result = new List<SqlParameter>();

         
            
           
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            if (txtbac_tk.Value < 0)
                txtbac_tk.Value = 0;


            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
            

            var and = radAnd.Checked;

            string cKey;

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","TK"
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
            result.Add(new SqlParameter("@Tk_sc", TxtTk_sc.Text.Trim()));
            result.Add(new SqlParameter("@Bac_tk", txtbac_tk.Value));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            int loai_tk = 0;
            if (rdo_All.Checked) loai_tk = 1;

            result.Add(new SqlParameter("@Loai_tk", loai_tk));
            
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
