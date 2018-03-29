﻿using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL.Loc
{
    public partial class LocKetQuaHoaDonDichVuCoSL : LocKetQuaBase
    {
        public LocKetQuaHoaDonDichVuCoSL()
        {
            InitializeComponent();
            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
        }
        
        public LocKetQuaHoaDonDichVuCoSL(V6InvoiceBase invoice, DataTable AM, DataTable AD)
        {
            InitializeComponent();
            _invoice = invoice;
            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
            MyInitBase(dataGridView1, dataGridView2, AM, AD);
        }
        
        void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            CurrentIndex = -1;
            dataGridView2.TableSource = null;
        }

        public void SetAM(DataTable am81)
        {
            dataGridView1.DataSource = am81.Copy();
        }

        public void SetAD(DataTable ad81)
        {
            dataGridView2.TableSource = ad81;
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