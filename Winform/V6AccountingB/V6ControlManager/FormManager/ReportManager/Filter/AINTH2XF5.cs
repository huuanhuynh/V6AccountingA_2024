using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINTH2XF5 : FilterBase
    {
        public AINTH2XF5()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
            SetParentRowEvent += ASOTH2F5_SetParentRowEvent;

            //txtMaDvcs.VvarTextBox.Text = V6LoginInfo.Madvcs;
            //if (V6LoginInfo.MadvcsCount <= 1)
            //{
            //    txtMaDvcs.Enabled = false;
            //}
            SetHideFields("V");
        }

        void ASOTH2F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            if(String1=="1")
                ma_kh_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            else if(String1 == "2")
                ma_vv_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            else if (String1 == "3")
                ma_bp_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            else
            {
                ma_nx_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            }

        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");

                GridViewHideFields.Add("GIA_NT2", "GIA_NT2");
                GridViewHideFields.Add("GIA_NT", "GIA_NT");
                GridViewHideFields.Add("TT_NT", "TT_NT");
                GridViewHideFields.Add("TIEN_NT2", "TIEN_NT2");
                
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
            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

            
            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                String1=="1"?"MA_KH":
                String1=="2"?"MA_VV":
                String1=="3"?"MA_BP": "MA_NX"
                
            }, true);

            result.Add(new SqlParameter("@Advance2", keyf5));
            return result;
        }

        
    }
}
