﻿using System;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL.ChonPhieuNhap
{
    public partial class CPN_KetQua_HoaDonDichVuCoSL : LocKetQuaBase
    {
        public CPN_KetQua_HoaDonDichVuCoSL()
        {
            InitializeComponent();
        }
        
        public CPN_KetQua_HoaDonDichVuCoSL(V6InvoiceBase invoice)
        {
            InitializeComponent();
            _invoice = invoice;
            //MyInitBase(dataGridView1, null, AM, AD);
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig("AMAD81K");
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
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
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
