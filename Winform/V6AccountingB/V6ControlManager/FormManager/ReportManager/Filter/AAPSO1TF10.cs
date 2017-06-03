﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6Controls;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPSO1TF10 : FilterBase
    {
        public AAPSO1TF10()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            Ref_key = "MA_KH";
           // SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
            SetParentAllRowEvent += AARCD1_F5_SetParentAllRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(DataGridViewRow row)
        {
           Ma_kh_filterLine.VvarTextBox.Text = row.Cells["MA_KH"].Value.ToString().Trim();
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
                        var tk = (row.Cells["MA_KH"].Value ?? "").ToString().Trim();
                        listtk = listtk + "," + tk;
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".AARCD1_F5_SetParentAllRowEvent", ex);
                }
            }
            
            Ma_kh_filterLine.VvarTextBox.Text = listtk.Substring(1);
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
                //@Advance AS VARCHAR(max) = '',
                //@ma_kh varchar(max)   

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            
            result.Add(new SqlParameter("@ma_kh",Ma_kh_filterLine.StringValue.Trim()));
            return result;
        }

        
    }
}
