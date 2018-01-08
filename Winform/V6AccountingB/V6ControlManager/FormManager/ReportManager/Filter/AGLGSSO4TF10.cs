using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6Controls;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLGSSO4TF10 : FilterBase
    {
        public AGLGSSO4TF10()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            Ref_key = "TK";
           // SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
            SetParentAllRowEvent += AARCD1_F5_SetParentAllRowEvent;
            LoadLanguage();
        }

        void AARCD1_F5_SetParentRowEvent(System.Windows.Forms.DataGridViewRow row)
        {
           Tk_filterLine.VvarTextBox.Text = row.Cells["TK"].Value.ToString().Trim();
        }
        void AARCD1_F5_SetParentAllRowEvent(DataGridView dataGridView1)
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
                        var tk = (row.Cells["TK"].Value ?? "").ToString().Trim();
                        listtk = listtk + "," + tk;
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
            //@tk nvarchar(4000),
            //@ngay_ct1 smalldatetime,
            //@ngay_ct2 smalldatetime,
            //@Tk_sc INT,
            //@Bac_tk INT,
            //@Advance AS VARCHAR(8000),
            //@tk1 nvarchar(4000)

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            
            result.Add(new SqlParameter("@tk1",Tk_filterLine.StringValue.Trim()));
            return result;
        }

        
    }
}
