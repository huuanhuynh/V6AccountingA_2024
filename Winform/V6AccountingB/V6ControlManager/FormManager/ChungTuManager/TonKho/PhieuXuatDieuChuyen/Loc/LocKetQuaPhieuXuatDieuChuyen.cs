using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatDieuChuyen.Loc
{
    public partial class LocKetQuaPhieuXuatDieuChuyen : LocKetQuaBase
    {
        public LocKetQuaPhieuXuatDieuChuyen()
        {
            InitializeComponent();
            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
        }

        public LocKetQuaPhieuXuatDieuChuyen(V6InvoiceBase invoice, DataTable AM, DataTable AD)
        {
            InitializeComponent();
            _invoice = invoice;
            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
            MyInitBase(dataGridView1, dataGridView2, AM, AD);
        }

        void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            CurrentIndex = -1;
            dataGridView2.DataSource = null;
        }
        
        public void SetAM(DataTable am)
        {
            dataGridView1.DataSource = am;
        }

        public void SetAD(DataTable ad)
        {
            dataGridView2.DataSource = ad;
        }
        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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
