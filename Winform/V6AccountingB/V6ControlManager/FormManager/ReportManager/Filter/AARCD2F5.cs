using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARCD2F5 : FilterBase
    {
        public AARCD2F5()
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
            TK_filterLine.VvarTextBox.Text = row["TK"].ToString().Trim();
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@Ngay_ct1	varchar(8),
            //@Ngay_ct2	varchar(8),
            //@Advance	nvarchar(max),
            //@Group	nvarchar(max)='',	
            //@Advance2 AS VARCHAR(8000) = '' -- Điều kiện lọc 1 ma khach hang


            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

           
            
            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                "MA_KH", "TK"
                
            }, true);

            result.Add(new SqlParameter("@Advance2", keyf5));
            return result;
        }

        
    }
}
