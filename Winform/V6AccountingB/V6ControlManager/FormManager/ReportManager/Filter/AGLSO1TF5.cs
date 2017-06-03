using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLSO1TF5 : FilterBase
    {
        public AGLSO1TF5()
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
               
                //@tk varchar(4000),
                //@ngay_ct1 varchar(8),
                //@ngay_ct2 varchar(8),
                //@gop_tk int,
                //@Advance VARCHAR(8000),
                //@tk1 VARCHAR(50)       

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            
            result.Add(new SqlParameter("@tk1",Tk_filterLine.StringValue.Trim()));
            return result;
        }

        
    }
}
