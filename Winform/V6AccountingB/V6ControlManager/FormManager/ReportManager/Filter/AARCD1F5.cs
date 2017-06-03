using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARCD1F5 : FilterBase
    {
        public AARCD1F5()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            ma_kh_filterLine.VvarTextBox.Text = row["MA_KH"].ToString().Trim();
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
               
            //@Tk VARCHAR(50), -- Tài khoản công nợ
            //@Ngay_ct1 SmallDateTime, --Từ ngày
            //@Ngay_ct2 SmallDateTime, --Đến ngày
            //@Advance AS VARCHAR(8000) 
            //@Advance2 AS VARCHAR(8000) - Theo ma kh

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

           
            
            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                "MA_KH"
                
            }, true);

            result.Add(new SqlParameter("@Advance2", keyf5));
            return result;
        }

        
    }
}
