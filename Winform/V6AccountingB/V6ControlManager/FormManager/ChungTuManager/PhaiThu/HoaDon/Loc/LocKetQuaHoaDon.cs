using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.Loc
{
    public partial class LocKetQuaHoaDon : LocKetQuaBase
    {
        public LocKetQuaHoaDon()
        {
            InitializeComponent();
        }
        
        public LocKetQuaHoaDon(V6InvoiceBase invoice, DataTable AM, DataTable AD)
        {
            InitializeComponent();
            _invoice = invoice;
            _aldmConfig = ConfigManager.GetAldmConfig("SEARCH_" + _invoice.Mact);
            MyInitBase(dataGridView1, dataGridView2, AM, AD);
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
