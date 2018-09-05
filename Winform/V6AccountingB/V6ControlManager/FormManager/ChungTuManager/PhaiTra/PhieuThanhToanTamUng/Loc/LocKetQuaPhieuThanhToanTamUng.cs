using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuThanhToanTamUng.Loc
{
    public partial class LocKetQuaPhieuThanhToanTamUng : LocKetQuaBase
    {
        public LocKetQuaPhieuThanhToanTamUng()
        {
            InitializeComponent();
            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
        }
        
        public LocKetQuaPhieuThanhToanTamUng(V6InvoiceBase invoice, DataTable AM, DataTable AD)
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
            dataGridView3.DataSource = null;
        }

        public void SetAM(DataTable am)
        {
            dataGridView1.DataSource = am.Copy();
        }

        public void SetAD(DataTable ad, DataTable ad2)
        {
            dataGridView2.DataSource = ad;

            dataGridView3.DataSource = ad2;
            FormatGridView3();
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
        
        private void FormatGridView3()
        {
            dataGridView3.AutoGenerateColumns = dataGridView3.Columns.Count == 0;
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.HeaderText = CorpLan2.GetFieldHeader(column.DataPropertyName);
                switch (column.DataPropertyName.ToUpper())
                {
                    case "MA_CT":
                        column.DisplayIndex = 0;
                        break;
                    case "MA_KH":
                        column.DisplayIndex = 1;
                        break;
                    case "TEN_KH":
                        column.DisplayIndex = 2;
                        break;

                    case "UID":
                    case "STT_REC_TT":
                    case "STT_REC":
                    case "STT_RECDH":
                    case "STT_REC0DH":
                    case "STT_REC0":
                        column.Visible = false;
                        break;
                    case "SO_LUONG1":
                    case "SO_LUONG":
                    case "T_SO_LUONG":
                        column.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
                        break;
                    case "GIA":
                    case "GIA2":
                    case "GIA21":
                    case "GIA0":
                        column.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIA"];
                        break;
                    case "GIA_NT":
                        column.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIANT"];
                        break;
                    //case "TIEN":
                    //case "TIEN2":
                    //    e.Column.DefaultCellStyle.Format = V6Options.V6OptionValue["M_IP_R_TIEN"];
                    //    break;
                    case "TIEN_NT2":
                        column.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIENNT"];
                        break;
                }
            }
            dataGridView3.AutoGenerateColumns = true;
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
