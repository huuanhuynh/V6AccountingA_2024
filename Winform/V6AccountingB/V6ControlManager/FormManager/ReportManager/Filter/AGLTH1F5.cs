using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTH1F5 : FilterBase
    {
        public AGLTH1F5()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            Tk_du_filterLine.VvarTextBox.Text = row["TK_DU"].ToString().Trim();
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

          
            //@Tk varchar(16),
            //@Loai_tk int,
            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@Ma_dvcs varchar(50),
            //@Advance2  VARCHAR(100)

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

          //  result.Add(new SqlParameter("@ma_dvcs", txtMaDvcs.Text.Trim() + "%"));

            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                "TK_DU",
                
            }, true);
           
            result.Add(new SqlParameter("@Advance2", keyf5));
            return result;
        }

        
    }
}
