using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter.NhanSu
{
    public partial class ERLISTNV_Filter: FilterBase
    {
        public ERLISTNV_Filter()
        {
            InitializeComponent();
            //F9 = true;
            
            dateNgay_ct1.Value = V6Setting.M_SV_DATE;
            dateNgay_ct2.Value = V6Setting.M_SV_DATE;

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
            Date1 = dateNgay_ct1.Value.Date;
            Date2 = dateNgay_ct2.Value.Date;
            result.Add(new SqlParameter("@Ngay_ct1", Date1));
            result.Add(new SqlParameter("@Ngay_ct2", Date2));
            result.Add(new SqlParameter("@User_ID", V6Login.UserId));

            ReportRViewBase parent = (ReportRViewBase) FindParent<ReportRViewBase>();

            result.Add(new SqlParameter("@Lang", parent.LAN));
            result.Add(new SqlParameter("@Advance", ""));
            return result;
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        public override void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            try
            {
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".FormatGridView", ex);
            }
        }
    }
}
