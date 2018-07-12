using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AVGLGSSO5AF5 : FilterBase
    {
        public AVGLGSSO5AF5()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = false;
            F9 = false;
            F10 = false;

            SetHideFields(RTien);
            SetParentRowEvent += AVGLGSSO5AF5_SetParentRowEvent;
        }

        void AVGLGSSO5AF5_SetParentRowEvent(IDictionary<string, object> row)
        {
            lineKhoaCTGS.VvarTextBox.Text = row["KHOA_CTGS"].ToString().Trim();
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

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //ngay_ct1
            //ngay_ct2
            //so_ct1
            //so_ct2
            //ct_goc
            //Advance
            //cLan
            //User_id
            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            result.Add(new SqlParameter("@Khoa_ctgs0", lineKhoaCTGS.StringValue));
            return result;
            
        }
    }
}
