using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLCTKC: FilterBase
    {
        public AGLCTKC()
        {
            InitializeComponent();

            F4 = true;
            F7 = false;
            F8 = true;

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
            //@Period1 AS INT,
            //@Period2 AS INT,
            //@NumList VARCHAR(MAX),
            //@User_id AS INT,
            //@Ma_dvcs VARCHAR(50) = ''

            var result = new List<SqlParameter>();
            int zero = 0;
            result.Add(new SqlParameter("@Type","VIEW"));
            result.Add(new SqlParameter("@Year", (int)txtNam.Value));
            result.Add(new SqlParameter("@Period1", zero));
            result.Add(new SqlParameter("@Period2", zero));
            result.Add(new SqlParameter("@NumList", ""));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            result.Add(new SqlParameter("@Ma_dvcs", ""));

            return result;

            
            
        }

        private void txtThang12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox) sender;
                if (txt.Value < 1) txt.Value = 1;
                if (txt.Value > 12) txt.Value = 12;
                
            }
            catch (Exception)
            {
                
            }
        }

        private void txtKy2_V6LostFocus(object sender)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 1;
                if (txt.Value > 12) txt.Value = 12;

                

            }
            catch (Exception)
            {

            }
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
