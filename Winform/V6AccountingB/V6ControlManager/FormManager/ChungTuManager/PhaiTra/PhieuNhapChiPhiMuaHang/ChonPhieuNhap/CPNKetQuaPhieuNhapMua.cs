using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapChiPhiMuaHang.ChonPhieuNhap
{
    public partial class CPNKetQuaPhieuNhapMua : LocKetQuaBase
    {
        public CPNKetQuaPhieuNhapMua()
        {
            InitializeComponent();
            MyInit();
        }
        
        public CPNKetQuaPhieuNhapMua(V6InvoiceBase invoice, DataTable AM, DataTable AD)
        {
            InitializeComponent();
            _invoice = invoice;
            //MyInitBase(dataGridView1, dataGridView2, AM, AD);//???//
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig("AMAD73A");
                //if (_aldmConfig.HaveInfo) gridViewSummary1.NoSumColumns = _aldmConfig.GRDT_V1;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        public bool MultiSelect { get; set; }

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
                FormatGridViewAD();
            }
            catch (Exception)
            {

            }
        }

        public void SetAD(DataTable ad)
        {
            dataGridView2.TableSource = ad.Copy();
            FormatGridViewAD();
        }

        private void FormatGridViewAD()
        {
            try
            {
                var config = ConfigManager.GetAldmConfig("AMAD73ACT");
                if (!config.HaveInfo) return;
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, config.GRDS_V1, config.GRDF_V1, config.GRDH_LANG_V1);
            }
            catch (Exception)
            {
                
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (_cancel_SelectionChange)
            {
                _cancel_SelectionChange = false;
                return;
            }
            Refresh0(dataGridView1);
        }
        
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnAcceptSelectEvent();
            }
            else if (MultiSelect)
            {
                if (e.KeyCode == Keys.Space)
                {
                    if (dataGridView1.CurrentRow != null) dataGridView1.ChangeSelect(dataGridView1.CurrentRow);
                }
                else if (e.KeyData == (Keys.Control | Keys.A))
                {
                    dataGridView1.SelectAllRow();
                }
                else if (e.KeyData == (Keys.Control | Keys.U))
                {
                    dataGridView1.UnSelectAllRow();
                }
            }
        }
    }
}
