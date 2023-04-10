using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ATOTINHPB: FilterBase
    {
        public ATOTINHPB()
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
            V6Setting.M_NAM = (int)txtNam.Value;
            V6Setting.M_KY2 = (int)txtKy.Value;
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@nCycle", (int)txtKy.Value));
            result.Add(new SqlParameter("@nYear", (int)txtNam.Value));
            result.Add(new SqlParameter("@nUserID", V6Login.UserId));
            

            //var and = radAnd.Checked;

            //var cKey = "";
            //var key0 = GetFilterStringByFields(new List<string>()
            //{
            //    "NH_VT1","NH_VT2","NH_VT3","TK_VT"
            //}, and);
           
            //if (!string.IsNullOrEmpty(key0))
            //{
            //    cKey = string.Format(" ma_vt in (select ma_kh from alkh where {0} )", key0);
            //}
            
            //result.Add(new SqlParameter("@Advance", cKey));
            
            return result;
        }

        private void txtThang12_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox) sender;
                //if (txt.Value < 1) txt.Value = 1;
                //if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {
                
            }
        }

        
    }
}
