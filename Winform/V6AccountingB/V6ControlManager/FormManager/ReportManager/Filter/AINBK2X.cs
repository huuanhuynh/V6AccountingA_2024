using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINBK2X : FilterBase
    {
        public AINBK2X()
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

            if (TxtMa_vt.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn mã vật tư!");
            }

                      //@ngay_ct1 char(8),
                      //@ngay_ct2 char(8),
                      //@item nvarchar(50),
                      //@advance nvarchar(max)
              result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@item", TxtMa_vt.Text.Trim()));
            var and = radAnd.Checked;
            var cKey = "";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_KHO","MA_DVCS"
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
            cKey = cKey + " AND (NXT=2)";

            result.Add(new SqlParameter("@advance", cKey));
            return result;
        }

        
    }
}
