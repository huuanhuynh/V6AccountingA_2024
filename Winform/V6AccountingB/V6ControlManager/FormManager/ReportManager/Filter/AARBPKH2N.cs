using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARBPKH2N: FilterBase
    {
        public AARBPKH2N()
        {
            InitializeComponent();

            txtTuNam.Value = V6Setting.M_Nam_bd;
            txtTaiKhoan.Text = "131";
            Ready();
        }
        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>
            {
                new SqlParameter("@cAcct", txtTaiKhoan.Text),
                new SqlParameter("@nYear", (int) txtTuNam.Value),
                new SqlParameter("@dCur", V6Setting.M_SV_DATE.Date),
                new SqlParameter("@tCur", V6BusinessHelper.GetServerDateTime().ToString("HH:mm:ss")),
                new SqlParameter("@uCur", V6Login.UserId)
            };

            return result;
        }
        
        private void txtTuNam_TextChanged(object sender, EventArgs e)
        {
            txtDenNam.Value = txtTuNam.Value + 1;
            Number2 = txtDenNam.Value;
        }
    }
}
