using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL.ChonPhieuXuat
{
    public partial class CPX_HoaDonDichVuCoSLKetQua : LocKetQuaBase
    {
        public CPX_HoaDonDichVuCoSLKetQua()
        {
            InitializeComponent();
            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
        }

        public CPX_HoaDonDichVuCoSLKetQua(V6InvoiceBase invoice)
        {
            InitializeComponent();
            _invoice = invoice;
            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
        }

        void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            CurrentIndex = -1;
        }

        public void SetAM(DataTable am)
        {
            dataGridView1.DataSource = am.Copy();
            FormatGridView();
        }


        private void FormatGridView()
        {
            try
            {
                string grd_show = "", grd_format = "", grd_header = "";
                var data = V6BusinessHelper.Select("ALDM", "*", "ma_dm='AMAD84A'").Data;
                if (data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    grd_show = row["GRDS_V1"].ToString().Trim();
                    grd_format = row["GRDF_V1"].ToString().Trim();
                    grd_header = V6Setting.IsVietnamese ? row["GRDHV_V1"].ToString().Trim() : row["GRDHE_V1"].ToString().Trim();
                }

                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, grd_show, grd_format, grd_header);

            }
            catch (Exception)
            {

            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnAcceptSelectEvent();
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    dataGridView1.CurrentRow.ChangeSelect();
                }

            }
            else if (e.KeyData == (Keys.Control | Keys.A))
            {
                e.Handled = true;
                dataGridView1.SelectAllRow();
            }
            else if (e.KeyData == (Keys.Control | Keys.U))
            {
                dataGridView1.UnSelectAllRow();
            }
        }
        /// <summary>
        /// Hiện (tải) lại chi tiết.
        /// </summary>
        public override void Refresh0(DataGridView grid1)
        {
            
        }
    }
}
