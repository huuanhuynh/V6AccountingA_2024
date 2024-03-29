﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AFABTPBTSN : FilterBase
    {
        public AFABTPBTSN()
        {
            InitializeComponent();

            F4 = true;
            F7 = true;
            F8 = true;
            
            txtKy1.Value =V6Setting.M_ngay_ct1.Month;
            txtKy2.Value = V6Setting.M_ngay_ct2.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            txtKy1.Value = V6Setting.M_KY1;
            txtKy2.Value = V6Setting.M_KY2;
            txtNam.Value = V6Setting.M_NAM;
            Change_DienGiai();
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
            //@Nam INT, 
            //@Ky1 INT,
            //@Ky2 INT,
            //@User_id INT,
            //@Dien_giai NVARCHAR(MAX),
            //@Ma_dvcs VARCHAR(50),
            //@Ma_dvcs0 VARCHAR(50),
            //@Action VARCHAR(50)
            Number1 = txtKy1.Value;
            Number2 = txtKy2.Value;
            Number3 = txtNam.Value;
            V6Setting.M_NAM = (int)txtNam.Value;
            V6Setting.M_KY1 = (int)txtKy1.Value;
            V6Setting.M_KY2 = (int)txtKy2.Value;
            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@Nam", (int)txtNam.Value));
            result.Add(new SqlParameter("@Ky1", (int)txtKy1.Value));
            result.Add(new SqlParameter("@Ky2", (int)txtKy2.Value));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            result.Add(new SqlParameter("@Dien_giai", txtDien_giai.Text.Trim()));
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
                //if (txt.Value < 0) txt.Value = 0;
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

                Change_DienGiai();
            }
            catch (Exception)
            {

            }
        }

        private void Change_DienGiai()
        {
            try
            {
                if (txtKy1.Value > txtKy2.Value)
                    txtKy2.Value = txtKy1.Value;

                if (txtKy1.Value == txtKy2.Value)
                    txtDien_giai.Text = string.Format("{0} {1} {2} {3}", V6Text.Text("BTPBKHTSCDCK"), txtKy1.Value.ToString("#"), V6Text.Text("year"), txtNam.Value.ToString("#"));
                else
                    txtDien_giai.Text = string.Format("{0} {1} {2} {3} {4} {5}", V6Text.Text("BTPBKHTSCDTK"), txtKy1.Value.ToString("#"), V6Text.Text("DENKY"), txtKy2.Value.ToString("#"), V6Text.Text("year"), txtNam.Value.ToString("#"));

                
            }
            catch (Exception)
            {

            }
        }
        private void txtNam_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Change_DienGiai();

            }
            catch (Exception)
            {

            }
        }

        
        
    }
}
