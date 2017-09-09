using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class HPAYROLLCALC : FilterBase
    {
        public HPAYROLLCALC()
        {
            InitializeComponent();
            
            txtThang.Value =V6Setting.M_ngay_ct1.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;


         
        }   

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@nMonth int,
            //@Year int,
            //@nUserID int,
            //@nDubugMode int,

            int dubugMode = 0;
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@nMonth", (int)txtThang.Value));
            result.Add(new SqlParameter("@nYear", (int)txtNam.Value));
            result.Add(new SqlParameter("@nUserID", V6Login.UserId));
            result.Add(new SqlParameter("@nDubugMode", dubugMode));
            return result;
        }

        private void txtThang12_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox) sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
                Number1 = txtThang.Value;
            }
            catch (Exception)
            {
                
            }

        }
        private void txtNam_TextChanged(object sender, EventArgs e)
        {
            Number2 = txtNam.Value;
        }

    }
}
