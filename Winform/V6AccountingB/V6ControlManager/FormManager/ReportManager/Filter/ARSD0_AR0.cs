using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ARSD0_AR0 : FilterBase
    {
        public ARSD0_AR0()
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

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@mua_ban CHAR(1),
            //@ma_ct VARCHAR(50),
            //@ngay_ct_1 smalldatetime,
            //@ngay_ct_2 smalldatetime,
            //@ma_kh VARCHAR(50),
            //@ma_dvcs VARCHAR(50),
            //@m_tk_tk_vt VARCHAR(50)
            

            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@mua_ban", "B"));
            result.Add(new SqlParameter("@ma_ct", "AR0"));
            result.Add(new SqlParameter("@ngay_ct_1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@ngay_ct_2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@ma_kh", txtMa_kh.IsSelected ? txtMa_kh.StringValue : "" + "%"));
            result.Add(new SqlParameter("@Ma_dvcs", txtMaDvcs.IsSelected ? txtMaDvcs.StringValue : "" + "%"));
            result.Add(new SqlParameter("@m_tk_tk_vt", "111,112"));
            return result;
        }

    }
}
