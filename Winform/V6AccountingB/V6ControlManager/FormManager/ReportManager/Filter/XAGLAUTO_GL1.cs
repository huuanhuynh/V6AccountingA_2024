using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class XAGLAUTO_GL1: FilterBase
    {
        public XAGLAUTO_GL1()
        {
            InitializeComponent();
            
            txtMaCt.SetInitFilter("MA_CT in ('CA1','BN1')");
            txtMaSoNB.SetInitFilter("dbo.VFV_InList0('GL1', MA_CTNB, ',') = 1");
            txtTK.Text = "336";
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            if (txtMaDvcs.Text.Trim() == "" || txtTK.Text.Trim() == "" || txtMaSoNB.Text.Trim() == "" || txtMaCt.Text.Trim() == "")
            {
                throw new Exception(V6Text.CheckInfor);
            }


            var result = new List<SqlParameter>();
            //@tk_336_from CHAR(16) = '3361',
            //@ma_dvcs_from CHAR(8) = 'HN',
            //@ma_ct_from CHAR(3) = 'CA1',
            //@Ma_sonb_GL1 VARCHAR(16),
            //@ngay_ct1 SMALLDATETIME,
            //@ngay_ct2 SMALLDATETIME,
            //@Save_voucher CHAR(1),
            //@Deleto CHAR(1)
            result.Add(new SqlParameter("@tk_336_from", txtTK.Text.Trim()));
            result.Add(new SqlParameter("@ma_dvcs_from", txtMaDvcs.Text.Trim()));
            result.Add(new SqlParameter("@ma_ct_from", txtMaCt.Text.Trim()));
            result.Add(new SqlParameter("@Ma_sonb_GL1", txtMaSoNB.Text));
            result.Add(new SqlParameter("@ngay_ct1", v6ColorDateTimePick1.YYYYMMDD));
            result.Add(new SqlParameter("@ngay_ct2", v6ColorDateTimePick2.YYYYMMDD));
            result.Add(new SqlParameter("@Save_voucher", "1"));
            result.Add(new SqlParameter("@Deleto", chkXoaDuLieuCu.Checked ? "1" : "0"));
            
            return result;
        }

        private void XAGLAUTO_GL1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
