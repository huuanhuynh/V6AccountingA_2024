using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapChiPhiMuaHang.ChonPhieuNhap
{
    public partial class CPN_PhieuNhapChiPhiMuaHangForm : V6Form
    {
        private readonly PhieuNhapChiPhiMuaHangControl _hoaDonForm;
        private CPNKetQuaPhieuNhapMua _locKetQua;
        public delegate void AcceptSelectDataList(List<IDictionary<string, object>> selectedDataList, bool multiSelect, IDictionary<string, object> amData);
        public event AcceptSelectDataList AcceptSelectEvent;
        protected virtual void OnAcceptSelectEvent(List<IDictionary<string, object>> selecteddatalist, bool multiSelect, IDictionary<string, object> amData)
        {
            var handler = AcceptSelectEvent;
            if (handler != null) handler(selecteddatalist, multiSelect, amData);
        }
        //private bool __ready = false;
        //private List<string> _orderList;
        //private List<string> _orderList2;
        private bool _viewMode, _multiSelect;
        
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
        
        public CPN_PhieuNhapChiPhiMuaHangForm()
        {
            InitializeComponent();
        }

        public CPN_PhieuNhapChiPhiMuaHangForm(PhieuNhapChiPhiMuaHangControl hoaDonForm, bool multiSelect)//, List<string> orderList, List<string> orderList2)
        {
            InitializeComponent();
            _hoaDonForm = hoaDonForm;
            _multiSelect = multiSelect;
            MyInit();
        }

        private void MyInit()
        {
            InitLocKetQua();
            _locKetQua.OnSelectAMRow += locKetQua_OnSelectAMRow;
            _locKetQua.AcceptSelectEvent += delegate { btnNhan.PerformClick(); };
            _locKetQua.MultiSelect = _multiSelect;

            v6ColorDateTimePick1.SetValue(V6Setting.M_ngay_ct1);
            v6ColorDateTimePick2.SetValue(V6Setting.M_ngay_ct2);
        }

        private void InitLocKetQua()
        {
            try
            {
                _locKetQua = new CPNKetQuaPhieuNhapMua(_hoaDonForm.Invoice, _hoaDonForm.AM, _hoaDonForm.AD)
                {Dock = DockStyle.Fill, Visible = false};
                panel1.Controls.Add(_locKetQua);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".InitLocKetQua", ex);
            }
        }

        void locKetQua_OnSelectAMRow(int index, string mact, string sttrec, decimal ttt_nt, decimal ttt, string mant)
        {
            try
            {
                var ad = _hoaDonForm.Invoice.LoadAD_PhieuNhap(mact, sttrec);
                _locKetQua.SetAD(ad);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".OnSelectAM: " + ex.Message);
            }
        }

        private void SetValueAndShowLocKetQua()
        {
            _locKetQua.SetAM(tAM);
            ChungTu.ViewSearchSumary(this, _hoaDonForm.AM, lblDocSoTien, _hoaDonForm.Invoice.Mact, _hoaDonForm.MA_NT);
            //Refresh0();
            ShowLocKetQua();
        }
        private void ShowLocKetQua()
        {
            locThoiGian1.Visible = false;
            locThongTin1.Visible = false;

            _locKetQua.Visible = true;
            _locKetQua.dataGridView1.Focus();
        }

        public void HideLocKetQua()
        {
            _locKetQua.Visible = false;
            
            locThoiGian1.Visible = true;
            locThongTin1.Visible = true;
        }

        private void Nhan()
        {
            try
            {
                if (_locKetQua != null && _locKetQua.Visible)
                {
                    var listData = new List<IDictionary<string, object>>();
                    var amData = new SortedDictionary<string,object>();
                    
                    if (_multiSelect)
                    {
                        foreach (DataGridViewRow row in _locKetQua.dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                //Load AD_
                                var mact = row.Cells["Ma_ct"].Value.ToString().Trim();
                                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                                var dataAD = _hoaDonForm.Invoice.LoadAD_PhieuNhap(mact, sttRec);
                                var data0 = dataAD.ToListDataDictionary();
                                listData.AddRange(data0);
                            }
                        }
                    }
                    else
                    {
                        var row = _locKetQua.dataGridView1.CurrentRow;
                        if (row != null)
                        {
                            foreach (DataGridViewColumn column in _locKetQua.dataGridView1.Columns)
                            {
                                string FIELD = column.DataPropertyName.ToUpper();
                                amData[FIELD] = row.Cells[FIELD].Value;
                            }
                            listData = _locKetQua.dataGridView2.GetData();
                        }
                    }

                    if (listData.Count > 0)
                    {
                        // Tuanmh 22/03/2016 Cp_nt,Cp=0     //
                        foreach (IDictionary<string, object> dic in listData)
                        {
                            dic["CP_NT"] = 0;
                            dic["CP"] = 0;
                        }
                        OnAcceptSelectEvent(listData, _multiSelect, amData);
                        Close();
                    }
                    else
                    {
                        if(_multiSelect)
                        this.ShowWarningMessage(V6Setting.IsVietnamese
                            ? "Chưa chọn dòng nào!\nDùng phím cách(space) hoặc Ctrl+A, Ctrl+U để bỏ chọn hết!"
                            : "No row selected!\nPlease use SpaceBar or Ctrl+A, Ctrl+U for unselect all!");
                        else this.ShowWarningMessage(V6Setting.IsVietnamese
                            ? V6Text.NoData
                            : "No data!");
                    }
                }
                else
                {
                    SearchThread();
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Tìm chứng từ lỗi:\n" + ex.Message);
            }
        }
        
        private void SearchThread()
        {
            //ReadyFor
            CheckForIllegalCrossThreadCalls = false;
            Timer timerCheckSearch = new Timer();
            timerCheckSearch.Interval = 500;
            timerCheckSearch.Tick += checkSearch_Tick;
            flagSearchFinish = false;
            flagSearchSuccess = false;
            btnNhan.Enabled = false;
            PrepareThread();
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
        public DataTable tAM;

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

        private string _where0Time = "", _where1AM = "", _where2AD = "", _w3NhomVt = "", _w4Dvcs = "";

        private void PrepareThread()
        {
            var stru = _hoaDonForm.Invoice.AMStruct;
            _where0Time = locThoiGian1GetFilterSql(stru, "", "like");
            _where1AM = locThongTin1GetFilterSql(stru, "", "like");
        }

        private void DoSearch()
        {
            try
            {
                tAM = _hoaDonForm.Invoice.SearchPhieuNhap(_where0Time, _where1AM);
                //    .SearchAM(where0Time, where1AM, where2AD, w3NhomVt, w4Dvcs);
                if (tAM != null && tAM.Rows.Count > 0)
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

        public string locThoiGian1GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            V6Setting.M_ngay_ct1 = v6ColorDateTimePick1.Date;
            V6Setting.M_ngay_ct2 = v6ColorDateTimePick2.Date;

            var result = "";
            SortedDictionary<string, object> keys = V6ControlFormHelper.GetFormDataDictionary(locThoiGian1);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            var dateFilter = string.Format("{0}ngay_ct BETWEEN '{1}' AND '{2}'",
                tableLable.Length > 0 ? tableLable + "." : "",
                v6ColorDateTimePick1.YYYYMMDD,
                v6ColorDateTimePick2.YYYYMMDD
                );
            if (result.Length > 0)
            {
                result = dateFilter + " and (" + result + ")";
            }
            else
            {
                result = dateFilter;
            }

            return result;
        }

        public string locThongTin1GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            var and_or = and ? " AND " : " OR ";
            var tu_so = ctTuSo.Text.Trim().Replace("'", "");
            var den_so = ctDenSo.Text.Trim().Replace("'", "");

            var result = "";
            //so chung tu
            if (chkLike.Checked)
            {
                if (tu_so != "")
                {
                    result += (result.Length > 0 ? and_or : "")
                        + tbL
                        + string.Format("so_ct like '%{0}'",
                        tu_so + ((tu_so.Contains("_") || tu_so.Contains("%")) ? "" : "%"));
                }
            }
            else
            {
                if (tu_so != "" && den_so == "")
                {
                    result += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                        result.Length > 0 ? and_or : "",
                        tbL,
                        tu_so);
                }
                else if (tu_so == "" && den_so != "")
                {
                    result += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                       result.Length > 0 ? and_or : "",
                       tbL,
                       den_so);
                }
                else if (tu_so != "" && den_so != "")
                {
                    result += string.Format("{0} (LTrim(RTrim({1}so_ct)) >= '{2}' and LTrim(RTrim({1}so_ct)) <= '{3}')",
                        result.Length > 0 ? and_or : "",
                        tbL,
                        tu_so, den_so)
                    ;
                }
            }

            if (soTienTu.Value != 0)
            {

                if (soTienDen.Value != 0)
                {
                    result += (result.Length > 0 ? and_or : "")
                              + tbL + "T_TT_NT >=" + soTienTu.Value.ToString(CultureInfo.InvariantCulture);

                    result += (result.Length > 0 ? and_or : "")
                              + tbL + "T_TT_NT <=" + soTienDen.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    result += (result.Length > 0 ? and_or : "")
                   + tbL + "T_TT_NT =" + soTienTu.Value.ToString(CultureInfo.InvariantCulture);

                }
            }
            else
            {
                if (soTienDen.Value != 0)
                {
                    result += (result.Length > 0 ? and_or : "")
                              + tbL + "T_TT_NT <=" + soTienDen.Value.ToString(CultureInfo.InvariantCulture);
                }
            }
            SortedDictionary<string, object> keys = V6ControlFormHelper.GetFormDataDictionary(locThongTin1);
            var result2 = SqlGenerator.GenWhere2_oper(tableStruct, keys, oper, and, tableLable);
            if (result2.Length > 0)
            {
                if (result.Length > 0)
                    result += string.Format("{0}({1})",
                        and_or, result2);
                else result = "(" + result2 + ")";
            }
            // dien_giai
            if (dienGiai.Text.Trim().Length > 0)
            {
                result += (result.Length > 0 ? and_or : "")
                    + tbL
                    + string.Format("dien_giai like N'%{0}%'",
                    dienGiai.Text.Replace("'", "''"));
            }

            return result;
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

        private void TimHoaDonForm_Load(object sender, EventArgs e)
        {
            if (_hoaDonForm.AM != null && _viewMode)
            {
                ShowLocKetQua();
            }
            else
            {
                HideLocKetQua();
            }
        }

        public void Refresh0()
        {
            _locKetQua.Refresh0(_locKetQua.dataGridView1);
        }
    }
}
