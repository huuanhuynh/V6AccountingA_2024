using System.Collections.Generic;
using System.Data.SqlClient;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLCD2F5F5 : FilterBase
    {
        public AGLCD2F5F5()
        {
            InitializeComponent();
            
            F3 = true;
            F5 = false;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            
            var tk_du = row["TK_DU"].ToString().Trim();

            filterLineVvarTextBox1.VvarTextBox.Text = tk_du;

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
            //@noco char(1),
            //@Advance AS VARCHAR(8000) = '',
            //@Advance2 AS VARCHAR(8000) = '',
            //@Advance3  AS VARCHAR(8000) = ''

            //@Advance AS VARCHAR(8000) = '111',
            //@Advance2 AS VARCHAR(8000) = '1114',
            //@Advance3 AS VARCHAR(8000) = '131',
            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            
          
            var advance3 = GetFilterStringByFields(new List<string>()
            {
                "TK_DU"
            }, true);
            result.Add(new SqlParameter("@Advance3", advance3));
            


            return result;
        }

        
    }
}
