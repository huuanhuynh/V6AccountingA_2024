using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;
using V6Tools.V6Convert;
namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ASOTH6F5 : FilterBase
    {
        public ASOTH6F5()
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
           
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            ma_vt_filterLine.VvarTextBox.Text = row["MA_VT"].ToString().Trim();
            GIA2NumberTextBox.NumberTextBox.Value = ObjectAndString.ObjectToDecimal(row["GIA2"]);
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
        
            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                "MA_VT","GIA2"

            }, true);
            result.Add(new SqlParameter("@Advance2", keyf5));
            return result;
        }

      
    }
}
