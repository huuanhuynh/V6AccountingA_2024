using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class ChungTuQuickView : V6Control
    {
        public ChungTuQuickView(V6InvoiceBase invoice)
        {
            Invoice = invoice;
            InitializeComponent();
        }

        private readonly V6InvoiceBase Invoice;
        public event HandleData SelectedIndexChanged;
        public bool EnableChangeInvoice { get; set; }

        protected virtual void OnSelectedIndexChanged(SortedDictionary<string, object> data)
        {
            var handler = SelectedIndexChanged;
            if (handler != null) handler(data);
        }

        public void SetDataSource(object data)
        {
            //dataGridView1.DataSource = null;
            dataGridView1.DataSource = data;
            
            FormatGridView();
            //dataGridView1.Focus();
        }

        private void FormatGridView()
        {
            try
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Invoice.GRDS_Q1, Invoice.GRDF_Q1,
                    V6Setting.IsVietnamese ? Invoice.GRDHV_Q1 : Invoice.GRDHE_Q1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
        }

        private int _oldIndex;
        //private string _oldSttrec;
        private bool _lockChange = false;
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (_lockChange) return;
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != _oldIndex)
            {
                _oldIndex = dataGridView1.CurrentRow.Index;
                OnSelectedIndexChanged(dataGridView1.CurrentRow.ToDataDictionary());
            }
        }


        public void SetSelectedRow(string sttRec)
        {
            EnableChangeInvoice = false;
            try
            {
                if (dataGridView1.DataSource != null && dataGridView1.Columns.Contains("STT_REC"))
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        row.Selected = false;
                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["STT_REC"].Value.ToString().Trim() == sttRec)
                        {
                            row.Selected = true;
                            dataGridView1.CurrentCell = row.Cells["SO_CT"];
                            _oldIndex = row.Index;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ChungTuQuickView SetSelectedRow " + ex.Message, "V6ControlManager");
            }
            EnableChangeInvoice = true;
        }

    }
}
