using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINCDHSDGF5 : FilterBase
    {
        public AINCDHSDGF5()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            SetParentRowEvent += AINCDHSDG_F5_SetParentRowEvent;
        }

        void AINCDHSDG_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            ma_vt_filterLine.VvarTextBox.Text = row["MA_VT"].ToString().Trim();
            ma_lo_filterLine.VvarTextBox.Text = row["MA_LO"].ToString().Trim();
            TxtMakho.VvarTextBox.Text = row["MA_KHO"].ToString().Trim();
            
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("NGAY_CT1", V6Setting.M_ngay_ct1);
            RptExtraParameters.Add("NGAY_CT2", V6Setting.M_ngay_ct2);
          //  RptExtraParameters.Add("MA_LO", ma_lo_filterLine.VvarTextBox.Text.Trim());
            RptExtraParameters.Add("MA_VT", ma_vt_filterLine.VvarTextBox.Text.Trim());
            RptExtraParameters.Add("MA_KHO", TxtMakho.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");

            if (ma_lo_filterLine.StringValue == "" || ma_vt_filterLine.StringValue == "" || TxtMakho.StringValue == "")
            {
                throw new Exception("Không phải dòng chi tiết!");
            }

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
           
            
            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                "MA_VT","MA_LO","MA_KHO"
                
            }, true);

            result.Add(new SqlParameter("@Condition2", keyf5));
            return result;
        }

        
    }
}
