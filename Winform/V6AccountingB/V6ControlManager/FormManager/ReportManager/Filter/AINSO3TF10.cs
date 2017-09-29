using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6Controls;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINSO3TF10 : FilterBase
    {
        public AINSO3TF10()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            Ref_key = "MA_VT";
           // SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
            SetParentAllRowEvent += AARCD1_F5_SetParentAllRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(System.Windows.Forms.DataGridViewRow row)
        {
           Ma_vt_filterLine.VvarTextBox.Text = row.Cells["MA_VT"].Value.ToString().Trim();
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
                        var tk = (row.Cells["MA_VT"].Value ?? "").ToString().Trim();

                        listtk = listtk + "," + tk;
                        dataGridView1.Rows.Remove(row);
                        i--;
                    }
                }
                catch (Exception ex)
                {
                    
                }

            }

            
            Ma_vt_filterLine.VvarTextBox.Text = listtk.Substring(1);


        }
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
               
                //@Ngay_ct1 SmallDateTime, --Từ ngày
                //@Ngay_ct2 SmallDateTime, --Đến ngày
                //@Advance AS VARCHAR(max) = '',
                //@ma_kh varchar(max)   

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            
            result.Add(new SqlParameter("@ma_vt",Ma_vt_filterLine.StringValue.Trim()));
            return result;
        }

        
    }
}
