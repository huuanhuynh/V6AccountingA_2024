using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho.ChonDeNghiXuat
{
    public partial class IXY_PXK_KetQua : LocKetQuaBase
    {
        public IXY_PXK_KetQua()
        {
            InitializeComponent();
        }

        public IXY_PXK_KetQua(V6InvoiceBase invoice)
        {
            InitializeComponent();
            _invoice = invoice;
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
                var config = ConfigManager.GetAldmConfig("AMAD91A");

                var data = V6BusinessHelper.Select("ALDM", "*", "ma_dm='AMAD91A'").Data;
                if (data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    grd_show = row["GRDS_V1"].ToString().Trim();
                    grd_format = row["GRDF_V1"].ToString().Trim();
                    grd_header = V6Setting.IsVietnamese ? row["GRDHV_V1"].ToString().Trim() : row["GRDHE_V1"].ToString().Trim();
                }

                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, grd_show, grd_format, grd_header);
                
                if (!string.IsNullOrEmpty(config.FIELD))
                {
                    var field_valid = ObjectAndString.SplitString(config.FIELD);

                    dataGridView1.SetEditColumnParams(field_valid[0]);
                    dataGridView1.CongThuc_CellEndEdit_ApplyAllRow = false;
                    dataGridView1.ChangeColumnType(field_valid[0], typeof(V6NumberDataGridViewColumn), null);
                    if (!string.IsNullOrEmpty(config.CACH_TINH1))
                    {
                        dataGridView1.GanCongThuc(field_valid[0], config.CACH_TINH1);
                    }

                    if (!string.IsNullOrEmpty(config.FIELD2))
                    {
                        dataGridView1.SetValid(field_valid[0], field_valid[1], config.FIELD2);
                    }
                }
                //dataGridView1.GanCongThuc("SL_QD", "SO_LUONG=SL_QD*HS_QD1");
                //dataGridView1.ThemCongThuc("SL_QD", "TIEN_NT2=ROUND(SO_LUONG*GIA2,M_ROUND)");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            Refresh0(dataGridView1);
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
