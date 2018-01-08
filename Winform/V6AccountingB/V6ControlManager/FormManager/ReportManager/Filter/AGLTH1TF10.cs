using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6Controls;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTH1TF10 : FilterBase
    {
        public AGLTH1TF10()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
           // SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
            SetParentAllRowEvent += AARCD1_F5_SetParentAllRowEvent;
            LoadLanguage();
        }

        void AARCD1_F5_SetParentRowEvent(System.Windows.Forms.DataGridViewRow row)
        {
           Tk_filterLine.VvarTextBox.Text = row.Cells["tk"].Value.ToString().Trim();
        }
        void AARCD1_F5_SetParentAllRowEvent(System.Windows.Forms.DataGridView dataGridView1)
        {

            var listtk = "";
            int i = 0;

            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        var tk = (row.Cells["tk"].Value ?? "").ToString().Trim();

                        listtk = listtk + "," + tk;
                        dataGridView1.Rows.Remove(row);
                        i--;
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
                }
            }

            
            Tk_filterLine.VvarTextBox.Text = listtk.Substring(1);


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
