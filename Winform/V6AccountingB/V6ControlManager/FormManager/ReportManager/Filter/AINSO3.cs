﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINSO3 : FilterBase
    {
        public AINSO3()
        {
            InitializeComponent();

            F3 = true;
            F5 = false;

            TxtMakho.VvarTextBox.Text = (V6Setting.M_Ma_kho ?? "").Trim();

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);
            

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields.Add("TAG", "TAG");
              
            }
            else
            {
                
            }
            GridViewHideFields.Add("TIEN_NT_N", "TIEN_NT_N");
            GridViewHideFields.Add("TIEN_NT_X", "TIEN_NT_X");
            GridViewHideFields.Add("DU_DAU_NT", "DU_DAU_NT");
            GridViewHideFields.Add("DU_CUOI_NT", "DU_CUOI_NT");
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@Condition nvarchar(max),
            //@Ma_ct VARCHAR(50) = ''

            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("NGAY_CT1", dateNgay_ct1.Date);
            RptExtraParameters.Add("NGAY_CT2", dateNgay_ct2.Date);


            RptExtraParameters.Add("MA_KHO", TxtMakho.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");
            RptExtraParameters.Add("MA_VT", TxtMa_vt.Text.Trim());

            if (TxtMa_vt.Text.Trim() == "")
            {
                throw new Exception(V6Text.NoInput + lblMaVT.Text);
            }
            
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;
            


            var result = new List<SqlParameter>();


            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));


            var and = radAnd.Checked;

            var cKey = " MA_VT='" + TxtMa_vt.Text.Trim() + "'";
 
            
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KHO"
            }, and);
            
            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey =cKey+" AND "+ string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = cKey + " AND " + string.Format("(1=2 OR {0})", key0);
                }
            }
           

            result.Add(new SqlParameter("@Condition", cKey));
            result.Add(new SqlParameter("@Ma_ct", ""));

            int tinh_dc = 0;
            if (Chk_Tinh_dc.Checked) tinh_dc = 1;

            result.Add(new SqlParameter("@Tinh_dc", tinh_dc));


            return result;
        }

        
    }
}
