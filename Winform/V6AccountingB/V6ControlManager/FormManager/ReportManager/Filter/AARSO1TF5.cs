using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARSO1TF5 : FilterBase
    {
        public AARSO1TF5()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            Ma_kh_filterLine.VvarTextBox.Text = row["MA_KH"].ToString().Trim();
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
            //@Advance AS VARCHAR(max) = '', -- Điều kiện lọc danh mục khách hàng
            //@Ma_kh VARCHAR(50)   
          
            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            
            result.Add(new SqlParameter("@ma_kh",Ma_kh_filterLine.StringValue.Trim()));
            return result;
        }

        
    }
}
