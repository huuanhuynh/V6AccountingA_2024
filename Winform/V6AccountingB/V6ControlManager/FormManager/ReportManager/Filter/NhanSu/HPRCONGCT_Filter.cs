using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter.NhanSu
{
    public partial class HPRCONGCT_Filter: FilterBase
    {
        public HPRCONGCT_Filter()
        {
            InitializeComponent();
            Check1 = true;
            F3 = true;
            F5 = true;
            F9 = true;
            
            dateNgay_ct1.SetValue(V6Setting.M_SV_DATE);
            dateNgay_ct2.SetValue(V6Setting.M_SV_DATE);

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
            var result = new List<SqlParameter>();
            Check1 = radTheoNgay.Checked;
            Date1 = dateNgay_ct1.Date;
            
            result.Add(new SqlParameter("@dWork", Date1));
            result.Add(new SqlParameter("@nUserID", V6Login.UserId));
            result.Add(new SqlParameter("@cType", radTheoNgay.Checked ? "0" : "1"));
            return result;
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        public override void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            string showFields = "";
            string formatStrings = "";
            string headerString = "";
            if (_ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
            {
                var data = _ds.Tables[1];
                if (data.Columns.Contains("GRDS_V1")) showFields = data.Rows[0]["GRDS_V1"].ToString();
                if (data.Columns.Contains("GRDF_V1")) formatStrings = data.Rows[0]["GRDF_V1"].ToString();
                var f = V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1";
                if (data.Columns.Contains(f)) headerString = data.Rows[0][f].ToString();
            }
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
        }

    }
}
