using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTH1TF5 : FilterBase
    {
        public AGLTH1TF5()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
            LoadLanguage();
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            Tk_filterLine.VvarTextBox.Text = row["TK"].ToString().Trim();
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@Advance AS NVARCHAR(MAX),
            //@Bac_tk int=1,
            //@Tk_sc INT,
            //@User_id int,
            //@Loai_tk int,
            //@Tk varchar(16)	        

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            
            result.Add(new SqlParameter("@tk",Tk_filterLine.StringValue.Trim()));
            return result;
        }

        
    }
}
