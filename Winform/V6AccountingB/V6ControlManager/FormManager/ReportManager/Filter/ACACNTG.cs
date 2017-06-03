using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACACNTG : FilterBase
    {
        public ACACNTG()
        {
            InitializeComponent();
            
            txtThang1.Value =V6Setting.M_ngay_ct1.Month;
            txtThang2.Value = V6Setting.M_ngay_ct2.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            TxtTk.Text = "111";

            dateNgay_ct1.Value = new DateTime((int)txtNam.Value, (int)txtThang1.Value, (int)1);
            var _ngay_new12 = new DateTime((int)txtNam.Value, (int)txtThang2.Value + 1, (int)1);
            dateNgay_ct2.Value = _ngay_new12.AddDays(-1);
            

            txtAp_tg_gd.Value = 0;
            txtKieu_tg_gs.Value = 1;
            txtLoai_cl.Value = 1;

        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@Ngay_ct1 SMALLDATETIME, 
            //@Ngay_ct2 SMALLDATETIME,
            //@Tk VARCHAR(50),
            //@Ma_dvcs VARCHAR(50),
            //@Ap_tg_gd INT,
            //@Ty_gia NUMERIC(16, 2),
            //@Kieu_tg_gs Int,
            //@Loai_cl CHAR(1)

            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));

            if (txtKieu_tg_gs.Value==1)
                result.Add(new SqlParameter("@Tk",""));
            else
                result.Add(new SqlParameter("@Tk", TxtTk.Text.Trim()));

            result.Add(new SqlParameter("@Ma_dvcs", txtMaDvcs.VvarTextBox.Text.Trim()));
            result.Add(new SqlParameter("@Ap_tg_gd",(int) txtAp_tg_gd.Value));
            result.Add(new SqlParameter("@Ty_gia", txtTyGia.Value));
            result.Add(new SqlParameter("@Kieu_tg_gs", (int)txtKieu_tg_gs.Value));
            result.Add(new SqlParameter("@Loai_cl", txtLoai_cl.Value));

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

        private void Make_date12(object sender)
            {
            try
            {
               
                dateNgay_ct1.Value = new DateTime((int) txtNam.Value, (int) txtThang1.Value, (int) 1);
                var _ngay_new= new DateTime((int) txtNam.Value, (int) txtThang2.Value+1, (int) 1);
                 dateNgay_ct2.Value = _ngay_new.AddDays(-1);

               

            }
            catch (Exception)
            {
                
            }
        }

        private void txtKieu_tg_gs_TextChanged(object sender, EventArgs e)
        {

            if (txtKieu_tg_gs.Value == 1)
            {
                TxtTk.Enabled = false;
                txtTyGia.Enabled = false;
            }
            else
            {
                TxtTk.Enabled = true;
                txtTyGia.Enabled = true;
                
            }
        }

        
    }
}
