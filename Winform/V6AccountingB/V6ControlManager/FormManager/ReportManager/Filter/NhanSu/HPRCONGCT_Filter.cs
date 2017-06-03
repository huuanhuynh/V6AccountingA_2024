using System.Collections.Generic;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Init;
using V6SqlConnect;

namespace V6ControlManager.FormManager.ReportManager.Filter.NhanSu
{
    public partial class HPRCONGCT_Filter: FilterBase
    {
        public HPRCONGCT_Filter()
        {
            InitializeComponent();
            F9 = true;
            
            dateNgay_ct1.Value = V6Setting.M_SV_DATE;
            dateNgay_ct2.Value = V6Setting.M_SV_DATE;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
        }
        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            Date1 = dateNgay_ct1.Value.Date;
            result.Add(new SqlParameter("@dWork", Date1));
            
            //result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@nUserID", V6Login.UserId));
            return result;
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }
    }
}
