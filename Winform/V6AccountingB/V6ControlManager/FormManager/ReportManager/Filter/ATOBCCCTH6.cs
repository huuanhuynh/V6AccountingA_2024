using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ATOBCCCTH6 : FilterBase
    {
        public ATOBCCCTH6()
        {
            InitializeComponent();

            txtThang1.Value = V6Setting.M_ngay_ct1.Month;
            txtdau_cuoi.Value = 2;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            txtNam.Value = V6Setting.M_NAM;
            txtThang1.Value = V6Setting.M_KY2;
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

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


            //@nNam int,
            //@nKy int,
            //@cDau_cuoi int,
            // @ma_bpts varchar(50),
            //@cKey nvarchar(MAX)  )     
            V6Setting.M_NAM = (int)txtNam.Value;

            if (txtdau_cuoi.Value == 1)
            {
                V6Setting.M_KY1 = (int)txtThang1.Value;
            }
            else
            {
                V6Setting.M_KY2 = (int)txtThang1.Value;
            }
            V6Setting.M_NAM = (int)txtNam.Value;
            V6Setting.M_KY1 = (int)txtThang1.Value;
            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@nNam", (int)txtNam.Value));
            result.Add(new SqlParameter("@nKy", (int)txtThang1.Value));
            result.Add(new SqlParameter("@cDau_cuoi", (int)txtdau_cuoi.Value));
            result.Add(new SqlParameter("@ma_bpts", TxtMa_bp.Text.Trim()));


            var and = radAnd.Checked;

            var cKey = "1=1";
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "NH_CC1","NH_CC2","NH_CC3","SO_THE_CC","LOAI_CC","LOAI_CC0","MA_DVCS"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                cKey = cKey + " AND " + string.Format(" SO_THE_CC  in (select SO_THE_CC  from ALCC where {0} )", key0);
            }

            result.Add(new SqlParameter("@cKey", cKey));

            return result;
        }

        private void txtThang12_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void txtThang1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
        }
    }
}
