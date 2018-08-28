using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOVVTH2F5 : FilterBase
    {
        public ACOVVTH2F5()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
            SetParentRowEvent += ACOVVTH1F5_SetParentRowEvent;

            //txtMaDvcs.VvarTextBox.Text = V6LoginInfo.Madvcs;
            //if (V6LoginInfo.MadvcsCount <= 1)
            //{
            //    txtMaDvcs.Enabled = false;
            //}
            SetHideFields("V");
        }

        void ACOVVTH1F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            ma_vv_filterLine.VvarTextBox.Text = row["MA_VV"].ToString().Trim();
            ma_vt_filterLine.VvarTextBox.Text = row["MA_VT"].ToString().Trim();

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
                "MA_VV","MA_VT"

            }, true);
            //26/08/2018 Thieu key @Advance2
          result.Add(new SqlParameter("@Advance2", keyf5));
          return result;
        }

        
    }
}
