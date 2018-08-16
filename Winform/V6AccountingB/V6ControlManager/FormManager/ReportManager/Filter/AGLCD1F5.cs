using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLCD1F5 : FilterBase
    {
        public AGLCD1F5()
        {
            InitializeComponent();
            
            F3 = true;
            F5 = true;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            
            var tk = row["TK"].ToString().Trim();
            
            lineTaiKhoan.VvarTextBox.Text = tk;

            //var parent = V6ControlFormHelper.FindParent<ReportRViewBase>(this);
            //var rViewBase = parent as ReportRViewBase;
            //if (rViewBase != null) rViewBase.txtReportTitle.Text += " - "+tk;
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@Ngay_ct1 SmallDateTime, --Từ ngày
            //@Ngay_ct2 SmallDateTime, --Đến ngày
            //@BuTru char(1), 
            //@Advance AS VARCHAR(8000) = '' 
            //@Advance2 AS VARCHAR(8000) - Theo TK
            //@Tk

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            
          
            var advance2 = GetFilterStringByFields(new List<string>()
            {
                "TK"
            }, true);

            
            result.Add(new SqlParameter("@Advance2", advance2));
            result.Add(new SqlParameter("@Tk", lineTaiKhoan.StringValue));

            return result;
        }

        
    }
}
