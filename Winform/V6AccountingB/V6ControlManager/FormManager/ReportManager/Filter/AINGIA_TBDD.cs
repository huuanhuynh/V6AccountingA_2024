using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINGIA_TBDD : FilterBase
    {
        public AINGIA_TBDD()
        {
            InitializeComponent();
            
            txtThang1.Value =V6Setting.M_ngay_ct1.Month;
            txtThang2.Value = V6Setting.M_ngay_ct2.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;


            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

            txtDk_cl.Value = 1;
            txtTinh_giatb.Value = 1;
            
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@Period1 int,
            //@Year1 int,
            //@Period2 int,
            //@Year2 int,
            //@Ma_kho VARCHAR(50),
            //@Ma_vt VARCHAR(50),
            //@Dk_cl int,
            //@Tinh_giatb int,
            //@Advance VARCHAR(255) = ''

            Number1 = txtThang1.Value;
            Number2 = txtThang2.Value;
            Number3 = txtNam.Value;

            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@Period1", (int)txtThang1.Value));
            result.Add(new SqlParameter("@Year1", (int)txtNam.Value));
            result.Add(new SqlParameter("@Period2", (int)txtThang2.Value));
            result.Add(new SqlParameter("@Year2", (int)txtNam.Value));

            result.Add(new SqlParameter("@Ma_kho",TxtMakho.StringValue==""?"%":TxtMakho.StringValue));
            result.Add(new SqlParameter("@Ma_vt", TxtMa_vt.StringValue==""?"%":TxtMa_vt.StringValue));

            result.Add(new SqlParameter("@Dk_cl", (int)txtDk_cl.Value));
            result.Add(new SqlParameter("@Tinh_giatb", (int)txtTinh_giatb.Value));

            var and = radAnd.Checked;
            var cKey = "";
            var cKey_SD = "";

            var key0 = GetFilterStringByFields(new List<string>()
            {
               "MA_KHO","MA_VT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                 "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6","TK_VT"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey += string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey += string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key1);
            }

            cKey_SD += cKey;

            result.Add(new SqlParameter("@Advance", cKey));
            
            return result;
        }

        private void txtThang12_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox) sender;
                if (txt.Value < 1) txt.Value = 1;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {
                
            }
        }

        
    }
}
