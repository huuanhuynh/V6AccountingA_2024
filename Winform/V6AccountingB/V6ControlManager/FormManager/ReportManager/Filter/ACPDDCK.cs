using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACPDDCK : FilterBase
    {
        public ACPDDCK()
        {
            InitializeComponent();

            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;

            F4 = false;
            F7 = false;
            F8 = false;
            
           

        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
                //@ngay_ct1 varchar(8),
                //@ngay_ct2 varchar(8),
                //@ma_bpht varchar(50),
                //@tinhgia_dc	int,	--tinh lai gia dieu chinh 0/1
                //@User_id int,
                //@advance NVARCHAR(MAX)

            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ma_bpht",txtMa_bpht.StringValue));
            result.Add(new SqlParameter("@tinhgia_dc", chkdc_ck.Checked?1:0));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            result.Add(new SqlParameter("@advance",""));

            return result;

            
            
        }

        
        
    }
}
