﻿using System;
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
        }
        
        public CPNKetQuaPhieuNhapMua(V6InvoiceBase invoice, DataTable AM, DataTable AD)
        {
            InitializeComponent();
            _invoice = invoice;
            //MyInitBase(dataGridView1, dataGridView2, AM, AD);//???//
        }

        public bool MultiSelect;

        public void SetAM(DataTable am)
        {
            dataGridView1.DataSource = am.Copy();
            FormatGridView();
        }

        private void FormatGridView()
        {
            try
            {
                string grd_show = "", grd_format = "", grd_header = "";
                var data = V6BusinessHelper.Select("ALDM", "*", "ma_dm='AMAD73A'").Data;
                if (data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    grd_show = row["GRDS_V1"].ToString().Trim();
                    grd_format = row["GRDF_V1"].ToString().Trim();
                    grd_header = V6Setting.IsVietnamese ? row["GRDHV_V1"].ToString().Trim() : row["GRDHE_V1"].ToString().Trim();
                }
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, grd_show, grd_format, grd_header);

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
                string grd_show = "", grd_format = "", grd_header = "";
                var data = V6BusinessHelper.Select("ALDM", "*", "ma_dm='AMAD73ACT'").Data;
                if (data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    grd_show = row["GRDS_V1"].ToString().Trim();
                    grd_format = row["GRDF_V1"].ToString().Trim();
                    grd_header = V6Setting.IsVietnamese ? row["GRDHV_V1"].ToString().Trim() : row["GRDHE_V1"].ToString().Trim();
                }
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, grd_show, grd_format, grd_header);
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
                    if (dataGridView1.CurrentRow != null) dataGridView1.CurrentRow.ChangeSelect();
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
