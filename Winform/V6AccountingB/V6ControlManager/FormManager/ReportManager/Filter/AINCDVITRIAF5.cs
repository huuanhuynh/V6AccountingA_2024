using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINCDVITRIAF5 : FilterBase
    {
        public AINCDVITRIAF5()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            ma_vt_filterLine.VvarTextBox.Text = row["MA_VT"].ToString().Trim();
            TxtMavitri.VvarTextBox.Text = row["MA_VITRI"].ToString().Trim();
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("NGAY_CT1", V6Setting.M_SV_DATE);
            RptExtraParameters.Add("NGAY_CT2", V6Setting.M_SV_DATE);


            RptExtraParameters.Add("MA_KHO", "");
            RptExtraParameters.Add("MA_VT", ma_vt_filterLine.VvarTextBox.Text.Trim());
            RptExtraParameters.Add("MA_VITRI", TxtMavitri.VvarTextBox.Text.Trim());


            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
           
            
            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                "MA_VT","MA_VITRI"
                
            }, true);

            result.Add(new SqlParameter("@Condition2", keyf5));
            return result;
        }

        
    }
}
