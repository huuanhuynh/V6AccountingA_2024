﻿using System;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class ReportR45SelectorForm2 : V6Form
    {
        public ReportR45SelectorForm2()
        {
            InitializeComponent();
        }

        public ReportR45SelectorForm2(V6InvoiceBase invoice)
        {
            InitializeComponent();
            _invoice = invoice;

            MyInit();
        }

        private V6InvoiceBase _invoice = null;
        private ReportR45ViewBase _r45;
        public V6ColorDataGridView dataGridView1;
        
        private void MyInit()
        {
            try
            {
                string program = "A" + _invoice.Mact + "_XULYKHAC2";
                _r45 = new ReportR45ViewBase(ItemID, program, program, program, "", "", "", "", "");
                _r45.FilterControl.groupBox1.AutoSize = true;
                _r45.FilterControl.groupBox1.Height = 10;
                
                _r45.Dock = DockStyle.Fill;
                _r45.btnHuy.Visible = false;
                _r45.btnIn.Visible = false;
                _r45.btnNhan.Text = V6Text.Text("FILTER");
                _r45.btnNhan.Image = global::V6ControlManager.Properties.Resources.Search24;
                panel1.Controls.Add(_r45);
                _r45.Disposed += _r45_Disposed;
                dataGridView1 = _r45.dataGridView1;
                dataGridView1.Space_Bar = true;
                dataGridView1.Control_A = true;
                dataGridView1.KeyDown += dataGridView1_KeyDown;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("MyInit", ex);
            }
        }

        void _r45_Disposed(object sender, EventArgs e)
        {
            Dispose();
        }

        private void SelectMultiIDForm_Load(object sender, EventArgs e)
        {

        }

        private void SelectMultiIDForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                this.ShowInfoMessage(V6Text.NoData);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        //public override bool DoHotKey0(Keys keyData)
        //{
        //    if (keyData == Keys.Enter)
        //    {
        //        btnNhan.PerformClick();
        //        return true;
        //    }
        //    return base.DoHotKey0(keyData);
        //}

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_KeyDown", ex);
            }
        }
        

        private void FixSize()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixSize", ex);
            }
        }

        private void ReportR45SelectorForm2_SizeChanged(object sender, EventArgs e)
        {
            FixSize();
        }
    }
}