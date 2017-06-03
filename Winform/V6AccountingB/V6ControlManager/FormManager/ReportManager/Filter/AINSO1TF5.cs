using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINSO1TF5 : FilterBase
    {
        public AINSO1TF5()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
            String1ValueChanged += AINSO1TF5_String1ValueChanged;
        }

        private void AINSO1TF5_String1ValueChanged(string oldvalue, string newvalue)
        {
            TxtMakho.VvarTextBox.Text = newvalue;
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            ma_vt_filterLine.VvarTextBox.Text = row["MA_VT"].ToString().Trim();
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
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("NGAY_CT1", V6Setting.M_ngay_ct1);
            RptExtraParameters.Add("NGAY_CT2", V6Setting.M_ngay_ct2);


            RptExtraParameters.Add("MA_KHO", TxtMakho.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");
            RptExtraParameters.Add("MA_VT", ma_vt_filterLine.VvarTextBox.Text.Trim());

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

            result.Add(new SqlParameter("@ma_vt", ma_vt_filterLine.StringValue.Trim()));

            return result;
        }

        
    }
}
