using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuNhapKho.Loc
{
    public partial class LocKetQuaPhieuNhapKho : LocKetQuaBase
    {
        public LocKetQuaPhieuNhapKho()
        {
            InitializeComponent();
        }
        
        public LocKetQuaPhieuNhapKho(V6InvoiceBase invoice, DataTable AM, DataTable AD)
        {
            InitializeComponent();
            _invoice = invoice;
            MyInitBase(dataGridView1, dataGridView2, AM, AD);
        }

        public void SetAM(DataTable am)
        {
            dataGridView1.DataSource = am.Copy();
        }

        public void SetAD(DataTable ad)
        {
            dataGridView2.TableSource = ad.Copy();
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
                _cancel_SelectionChange = true;
                OnAcceptSelectEvent();
            }
        }
    }
}
