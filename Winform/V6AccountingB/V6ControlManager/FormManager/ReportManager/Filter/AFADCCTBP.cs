using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AFADCCTBP: FilterBase
    {
        public AFADCCTBP()
        {
            InitializeComponent();

            F3 = true;
            F4 = true;
            F8 = true;
            
        
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            txtNam.Value = V6Setting.M_NAM;
            txtThang1.Value = V6Setting.M_KY1;
            txtThang1.Value = V6Setting.M_KY2;
            txtThang1.Value = V6Setting.M_ngay_ct1.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;

        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@Nam INT, 
            //@Ky1 INT,
            //@Ky2 INT,
            //@User_id INT,
            //@Dien_giai NVARCHAR(MAX),
            //@Ma_dvcs VARCHAR(50),
            //@Ma_dvcs0 VARCHAR(50),
            //@Action VARCHAR(50)
            V6Setting.M_NAM = (int)txtNam.Value;
            V6Setting.M_KY1 = (int)txtThang1.Value;
            V6Setting.M_KY2 = (int)txtThang1.Value;
            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@Nam", (int)txtNam.Value));
            result.Add(new SqlParameter("@Ky1", (int)txtThang1.Value));
            result.Add(new SqlParameter("@Ky2", (int)txtThang1.Value));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            result.Add(new SqlParameter("@Dien_giai", ""));
            result.Add(new SqlParameter("@So_the_ts",TxtSo_the_ts.Text.Trim()));
            result.Add(new SqlParameter("@Ma_dvcs", txtMaDvcs.IsSelected ? txtMaDvcs.StringValue : "" + "%"));
            result.Add(new SqlParameter("@Ma_dvcs0", txtMaDvcs.IsSelected ? txtMaDvcs.StringValue : ""));



            result.Add(new SqlParameter("@Action", "F2"));



            return result;

            
            
        }

        private void txtThang12_TextChanged(object sender, EventArgs e)
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

        private void txtMaDvcs_Load(object sender, EventArgs e)
        {

        }
    }
}
