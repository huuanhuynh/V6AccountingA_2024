using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using V6AccountingBusiness;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AFATINHKH : FilterBase
    {
        public AFATINHKH()
        {
            InitializeComponent();
            
            txtKy.Value =V6Setting.M_ngay_ct1.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            txtNam.Value = V6Setting.M_NAM;
            txtKy.Value = V6Setting.M_KY2;
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@nCycle NUMERIC(2), 
            //@nYear NUMERIC(4), 
            //@nUserID INT
            Number2 = txtKy.Value;
            Number3 = txtNam.Value;
            int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)txtKy.Value, (int)txtNam.Value);
            if (check == 1)
            {
                this.ShowWarningMessage(V6Text.CheckLock);
                throw new Exception(V6Text.CheckLock);
            }
            V6Setting.M_NAM = (int)txtNam.Value;
            V6Setting.M_KY2 = (int)txtKy.Value;
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@nCycle", (int)txtKy.Value));
            result.Add(new SqlParameter("@nYear", (int)txtNam.Value));
            result.Add(new SqlParameter("@nUserID", V6Login.UserId));
            
            V6BusinessHelper.WriteV6UserLog(ItemID, GetType() + "." + MethodBase.GetCurrentMethod().Name,
               string.Format("TinhGia_TB nCycle:{0} nYear:{1} nUserID:{2}", txtKy.Value, txtNam.Value, V6Login.UserId));

            return result;
        }

        private void txtThang12_TextChanged(object sender, System.EventArgs e)
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

        
    }
}
