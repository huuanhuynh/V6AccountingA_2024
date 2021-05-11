using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ATOBCGIAM : FilterBase
    {
        public ATOBCGIAM()
        {
            InitializeComponent();
            
            txtThang1.Value =V6Setting.M_ngay_ct1.Month;
            txtThang2.Value = V6Setting.M_ngay_ct2.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            lineLoai_CC0.VvarTextBox.Text = "01";

            txtMa_tg_cc.VvarTextBox.SetInitFilter("Loai_tg_cc='G'");
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            txtNam.Value = V6Setting.M_NAM;
            txtThang1.Value = V6Setting.M_KY1;
            txtThang2.Value = V6Setting.M_KY2;  
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


            //@nam as int,
            //@tuKy as int,
            //@denKy as int,
            //@ma_bpts varchar(50),
            //@condition as nvarchar(max)
            V6Setting.M_NAM = (int)txtNam.Value;
            V6Setting.M_KY1 = (int)txtThang1.Value;
            V6Setting.M_KY2 = (int)txtThang2.Value;
            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@nam", (int)txtNam.Value));
            result.Add(new SqlParameter("@tuKy", (int)txtThang1.Value));
            result.Add(new SqlParameter("@denKy", (int)txtThang2.Value));


            result.Add(new SqlParameter("@ma_bpts", TxtMa_bp.Text.Trim()));
            
            

            var and = radAnd.Checked;

            var cKey = "1=1";
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "NH_CC1","NH_CC2","NH_CC3","SO_THE_CC","LOAI_CC","LOAI_CC0","MA_DVCS"
            }, and);
           
            if (!string.IsNullOrEmpty(key0))
            {
                cKey =cKey+" AND "+ string.Format(" SO_THE_CC  in (select SO_THE_CC  from ALCC where {0} )", key0);
            }

            result.Add(new SqlParameter("@condition", cKey));
            
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
