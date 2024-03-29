﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AFASUAKH : FilterBase
    {
        public AFASUAKH()
        {
            InitializeComponent();

            F3 = F8 = true;
            
            txtThang1.Value =V6Setting.M_ngay_ct1.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            txtNam.Value = V6Setting.M_NAM;
            txtThang1.Value = V6Setting.M_KY2;
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
            // @nam int,
            // @ky int,
            // @ma_bpts varchar(50),
            // @advance nvarchar(max)
            V6Setting.M_NAM = (int)txtNam.Value;
            V6Setting.M_KY2 = (int)txtThang1.Value;

            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@nam", (int)txtNam.Value));
            result.Add(new SqlParameter("@ky", (int)txtThang1.Value));
            

            result.Add(new SqlParameter("@ma_bpts", TxtMa_bp.Text.Trim()));
            
            

            var and = radAnd.Checked;

            var cKey = "1=1";
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "NH_TS1","NH_TS2","NH_TS3","SO_THE_TS","LOAI_TS","MA_DVCS"
            }, and);
           
            if (!string.IsNullOrEmpty(key0))
            {
                cKey =cKey+" AND "+ string.Format(" SO_THE_TS  in (select SO_THE_TS  from ALTS where {0} )", key0);
            }

            result.Add(new SqlParameter("@advance", cKey));
            
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

        
    }
}
