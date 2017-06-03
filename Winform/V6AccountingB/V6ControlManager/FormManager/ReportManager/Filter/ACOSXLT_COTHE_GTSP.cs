using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOSXLT_COTHE_GTSP : FilterBase
    {
        public ACOSXLT_COTHE_GTSP()
        {
            InitializeComponent();
            
            txtThang1.Value =V6Setting.M_ngay_ct1.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            txtNam.Value = V6Setting.M_NAM;
            txtThang1.Value = V6Setting.M_KY2;
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@nam NUMERIC(4, 0),
            //@thang NUMERIC(2, 0),
            //@ma_bpht VARCHAR(50),
            //@ma_sp VARCHAR(50)
            //@Condition nvarchar(max),
            V6Setting.M_NAM = (int)txtNam.Value;

            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@nam", (int)txtNam.Value));
            result.Add(new SqlParameter("@thang", (int)txtThang1.Value));
            result.Add(new SqlParameter("@ma_bpht", txtMa_bpht.Text));
            result.Add(new SqlParameter("@ma_sp", txtMa_sp.Text));

            var and = radAnd.Checked;

            string cKey;
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey = " AND " + string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = " AND " + string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "";
            }

            result.Add(new SqlParameter("@Condition", cKey));

            return result;
        }

        private void txtThang12_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox) sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {
                
            }
        }

      
    }
}
