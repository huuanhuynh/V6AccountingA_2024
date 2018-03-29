﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL.Loc
{
    public partial class TimHoaDonDichVuCoSLForm : V6Form
    {
        private readonly HoaDonDichVuCoSLControl _formChungTu;
        private readonly V6Invoice82 _invoice;
        private LocKetQuaHoaDonDichVuCoSL _locKetQua;
        //private bool __ready = false;
        private bool _viewMode;
        
        public bool ViewMode
        {
            get
            {
                return _viewMode;
            }
            set
            {
                _viewMode = value;
            }
        }
        public TimHoaDonDichVuCoSLForm()
        {
            InitializeComponent();
        }

        public TimHoaDonDichVuCoSLForm(HoaDonDichVuCoSLControl formChungTu)
        {
            InitializeComponent();
            _formChungTu = formChungTu;
            _invoice = _formChungTu.Invoice;
            
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                InitTuyChon();
                InitLocKetQua();

                locThongTin1.CreateDynamicFilter(_formChungTu.Invoice.AMStruct, _formChungTu.Invoice.ADV_AM);
                locThongTinChiTiet1.CreateDynamicFilter2(_formChungTu.Invoice.ADStruct, _formChungTu.Invoice.ADV_AD);

                _locKetQua.OnSelectAMRow += locKetQua_OnSelectAMRow;
                _locKetQua.AcceptSelectEvent += delegate { btnNhan.PerformClick(); };

                LoadLanguage();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MyInit: " + ex.Message);
            }
        }

        private void InitLocKetQua()
        {
            try
            {
                _locKetQua =  new LocKetQuaHoaDonDichVuCoSL(_invoice, _formChungTu.AM, _formChungTu.AD)
                { Dock = DockStyle.Fill, Visible = false };
                
                panel1.Controls.Add(_locKetQua);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".InitLocKetQua: " + ex.Message);
            }
        }

        void locKetQua_OnSelectAMRow(int index, string mact, string sttrec, decimal ttt_nt, decimal ttt, string mant)
        {
            try
            {
                _formChungTu.LoadAD(sttrec);
                _locKetQua.SetAD(_formChungTu.AD);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".OnSelectAM: " + ex.Message);
            }
        }

        private void SetValueAndShowLocKetQua()
        {
            _locKetQua.SetAM(tempAM);
            ChungTu.ViewSearchSumary(this, tempAM, lblDocSoTien, _formChungTu.Invoice.Mact, _formChungTu.MA_NT);
            Refresh0();
            _locKetQua.SetAD(_formChungTu.AD);

            ShowLocKetQua();
        }

        private void ShowLocKetQua()
        {
            locThoiGian1.Visible = false;
            locThongTin1.Visible = false;
            locThongTinChiTiet1.Visible = false;
            grbTuyChon.Visible = false;

            _locKetQua.Visible = true;
            _locKetQua.dataGridView1.Focus();
        }

        public void HideLocKetQua()
        {
            _locKetQua.Visible = false;
            locThoiGian1.Visible = true;
            locThongTin1.Visible = true;
            locThongTinChiTiet1.Visible = true;
            grbTuyChon.Visible = true;
        }

        
        private void Nhan()
        {
            try
            {
                if (_locKetQua != null && _locKetQua.Visible)
                {
                    _formChungTu.AM = tempAM;
                    _formChungTu.ResetADTables();
                    _formChungTu.ViewInvoice(_locKetQua.CurrentSttRec, V6Mode.View);
                    Hide();
                }
                else
                {
                    SearchThread();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Tìm chứngt từ lỗi:\n" + ex.Message);
            }
        }


        private void SearchThread()
        {
            //ReadyFor
            CheckForIllegalCrossThreadCalls = false;
            Timer timerCheckSearch = new Timer {Interval = 500};
            timerCheckSearch.Tick += checkSearch_Tick;
            flagSearchFinish = false;
            flagSearchSuccess = false;
            btnNhan.Enabled = false;

            new Thread(DoSearch)
            {
                IsBackground = true
            }
            .Start();

            timerCheckSearch.Start();
        }
        private bool flagSearchFinish;
        private bool flagSearchSuccess;
        private string exMessage = "";
        private DataTable tempAM;

        private void DoSearch()
        {
            try
            {
                var stru = _formChungTu.Invoice.AMStruct;
                var where0Time = locThoiGian1.GetFilterSql(stru, "", "like");

                var where1AM = locThongTin1.GetFilterSql(_formChungTu.Invoice, stru, "", "start");
                var w1 = GetAMFilterSql_TuyChon();
                if (w1.Length > 0)
                    where1AM += (where1AM.Length > 0 ? " and " : "") + w1;

                var stru2 = _formChungTu.Invoice.ADStruct;
                var where2AD = locThongTinChiTiet1.GetFilterSql(stru2, "", "like");

                var w3NhomVt = GetNhVtFilterSql_TuyChon("", "like");

                var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
                var w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "like");

                tempAM = _formChungTu.Invoice.SearchAM(where0Time, where1AM, where2AD, w3NhomVt, w4Dvcs);
                if (tempAM != null && tempAM.Rows.Count > 0)
                {
                    flagSearchSuccess = true;
                }
                else
                {
                    exMessage = V6Text.NoInvoiceFound;
                }


            }
            catch (Exception ex)
            {
                exMessage = ex.Message;
                flagSearchSuccess = false;
            }
            flagSearchFinish = true;
        }


        private void InitTuyChon()
        {
            txtNhomVT1.SetInitFilter("loai_nh=1");
            txtNhomVT2.SetInitFilter("loai_nh=2");
            txtNhomVT3.SetInitFilter("loai_nh=3");

            txtMaDVCS.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDVCS.Enabled = false;
                txtMaDVCS.ReadOnly = true;
            }

            chkNSD.Checked = _formChungTu.Invoice.M_LOC_NSD;
            if (chkNSD.Checked) chkNSD.Enabled = false;
            ChungTu.SetTxtStatusProperties(_formChungTu.Invoice, txtTrangThai, lblStatusDescription);
        }

        public string GetAMFilterSql_TuyChon()
        {
            var result = "";

            if (chkNSD.Checked)
            {
                result += "user_id2 = " + V6Login.UserId;
            }
            if (txtTrangThai.Text.Trim() != "")
            {
                result += string.Format("{0}[kieu_post]='{1}'", result.Length > 0 ? " and " : "", txtTrangThai.Text.Trim());
            }

            return result;
        }

        public string GetNhVtFilterSql_TuyChon(string tableLable, string oper = "=", bool and = true)
        {
            var result = "";

            var keys = new SortedDictionary<string, object>();
            if (txtNhomVT1.Text.Trim() != "")
                keys.Add("NH_VT1", txtNhomVT1.Text.Trim());
            if (txtNhomVT2.Text.Trim() != "")
                keys.Add("NH_VT2", txtNhomVT2.Text.Trim());
            if (txtNhomVT3.Text.Trim() != "")
                keys.Add("NH_VT3", txtNhomVT3.Text.Trim());

            if (keys.Count > 0)
            {
                var struAlvt = V6BusinessHelper.GetTableStruct("ALVT");
                result = SqlGenerator.GenWhere2(struAlvt, keys, oper, and, tableLable);
            }
            return result;
        }

        public string GetDvcsFilterSql_TuyChon(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var keys = new SortedDictionary<string, object>
            {
                {"MA_DVCS", txtMaDVCS.Text.Trim()}
            };
            var result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);
            return result;
        }

        void checkSearch_Tick(object sender, EventArgs e)
        {
            if (flagSearchFinish)
            {
                try
                {
                    ((Timer)sender).Stop();
                    if (flagSearchSuccess)
                    {
                        SetValueAndShowLocKetQua();
                    }
                    else
                    {
                        this.ShowInfoMessage(exMessage);
                    }
                }
                catch
                {
                    // ignored
                }
                btnNhan.Enabled = true;
                ((Timer)sender).Dispose();
            }
            else
            {
                //lblStatus
            }
        }

        

        private void Huy()
        {
            if (!_viewMode && _locKetQua != null && _locKetQua.Visible)
            {
                HideLocKetQua();
            }
            else
            {
                Hide();
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void TimHoaDonDichVuCoSLForm_Load(object sender, EventArgs e)
        {
            if (_formChungTu.AM != null && _viewMode)
            {
                ShowLocKetQua();
            }
            else
            {
                HideLocKetQua();
            }
            InitTuyChon();
        }

        public void Refresh0()
        {
            _locKetQua.Refresh0(_locKetQua.dataGridView1);
        }

        private void TimHoaDonDichVuCoSLForm_Activated(object sender, EventArgs e)
        {
            locThoiGian1.Focus();
        }

        private void TimHoaDonDichVuCoSLForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

    }
}