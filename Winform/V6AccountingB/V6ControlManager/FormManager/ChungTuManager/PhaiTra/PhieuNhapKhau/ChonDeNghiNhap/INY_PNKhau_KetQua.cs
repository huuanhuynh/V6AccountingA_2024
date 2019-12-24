using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapKhau.ChonDeNghiNhap
{
    public partial class INY_PNKhau_KetQua : LocKetQuaBase
    {
        public INY_PNKhau_KetQua()
        {
            InitializeComponent();
        }

        public INY_PNKhau_KetQua(V6InvoiceBase invoice)
        {
            InitializeComponent();
            _invoice = invoice;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig("AMAD91A");
                if (_aldmConfig.HaveInfo) gridViewSummary1.NoSumColumns = _aldmConfig.GRDT_V1;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
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
                if (!_aldmConfig.HaveInfo) return;

                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1, _aldmConfig.GRDH_LANG_V1);

                if (!string.IsNullOrEmpty(_aldmConfig.FIELD))
                {
                    var field_valid = ObjectAndString.SplitString(_aldmConfig.FIELD);

                    dataGridView1.SetEditColumnParams(field_valid[0]);
                    dataGridView1.CongThuc_CellEndEdit_ApplyAllRow = false;
                    dataGridView1.ChangeColumnType(field_valid[0], typeof(V6NumberDataGridViewColumn), null);
                    if (!string.IsNullOrEmpty(_aldmConfig.CACH_TINH1))
                    {
                        dataGridView1.GanCongThuc(field_valid[0], _aldmConfig.CACH_TINH1);
                    }

                    if (!string.IsNullOrEmpty(_aldmConfig.FIELD2))
                    {
                        dataGridView1.SetValid(field_valid[0], field_valid[1], _aldmConfig.FIELD2);
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
