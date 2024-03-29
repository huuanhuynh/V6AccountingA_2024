﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLCTPB : FilterBase
    {
        public AGLCTPB()
        {
            InitializeComponent();

            F4 = true;
            F7 = false;
            F8 = true;
            F9 = true;
            
            txtNam.Value = V6Setting.M_SV_DATE.Year;
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@Type AS VARCHAR(8),
            //@Year AS INT,
            //@Period1 AS INT = 0,
            //@Period2 AS INT = 0,
            //@Stt_recs VARCHAR(MAX) = '', 
            //@User_id INT = 1, 
            //@Ma_dvcs VARCHAR(50) = ''
            Number3 = txtNam.Value;
            var result = new List<SqlParameter>();
            int zero = 0;
            result.Add(new SqlParameter("@Type","VIEW"));
            result.Add(new SqlParameter("@Year", (int)txtNam.Value));
            result.Add(new SqlParameter("@Period1", zero));
            result.Add(new SqlParameter("@Period2", zero));
            result.Add(new SqlParameter("@Stt_recs", ("")));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            result.Add(new SqlParameter("@Ma_dvcs", ""));

            return result;

            
            
        }

        private void txtNam_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception)
            {

            }
        }

        
        
    }
}
