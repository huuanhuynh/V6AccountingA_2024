using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuXuatTraLaiNCC;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuXuatTraLaiNCC.ChonPhieuNhap;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonPhieuNhap
{
    public partial class CPN_HoaDonForm : V6Form
    {
        //private readonly PhieuXuatTraLaiNCCControl _PhieuXuatTraLaiNCCForm;
        private CPN_KetQua_HoaDon _locKetQua;
        private V6Invoice74 Invoice = new V6Invoice74();
        private string _ma_dvcs, _ma_kh;
        private DateTime _ngayCt;
        //private bool __ready = false;
        private bool _viewMode;
        //private List<string> _orderListAD;
        public delegate void AcceptSelectDataList(List<SortedDictionary<string, object>> selectedDataList);
        public event AcceptSelectDataList AcceptSelectEvent;

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
        public CPN_HoaDonForm()
        {
            InitializeComponent();
        }

        public CPN_HoaDonForm(DateTime ngayCt, string ma_dvcs, string ma_kh)
        {
            InitializeComponent();
            //_PhieuXuatTraLaiNCCForm = PhieuXuatTraLaiNCCForm;
            //_orderListAD = orderListAD;
            _ngayCt = ngayCt;
            _ma_dvcs = ma_dvcs;
            _ma_kh = ma_kh;
            MyInit();
        }

        private void MyInit()
        {
            InitTuyChon();
            _locKetQua =  new CPN_KetQua_HoaDon(Invoice)
            { Dock = DockStyle.Fill, Visible = false };
            panel1.Controls.Add(_locKetQua);

            locThongTin1.CreateDynamicFilter(Invoice.AMStruct, Invoice.ADV_AM);
            locThongTinChiTiet1.CreateDynamicFilter2(Invoice.ADStruct, Invoice.ADV_AD);

            locThongTin1.maKhach.Text = _ma_kh;
            txtMaDVCS.Text = _ma_dvcs;

            _locKetQua.AcceptSelectEvent += delegate { btnNhan.PerformClick(); };
            Ready();
        }

        private void SetValueAndShowLocKetQua()
        {
            _locKetQua.SetAM(tAM);
            Refresh0();
            //_locKetQua.SetAD(_PhieuXuatTraLaiNCCForm.AD, _orderListAD);

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
                    //Goi delegate
                    var data = _locKetQua.dataGridView1.GetSelectedData();
                    if (data.Count > 0)
                    {
                        OnAcceptSelectEvent(data);
                        Close();
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Setting.IsVietnamese? "Chưa chọn dòng nào!\nDùng phím cách(space) hoặc Ctrl+A, Ctrl+U để bỏ chọn hết!"
                            : "No row selected!\nPlease use SpaceBar or Ctrl+A, Ctrl+U for unselect all!");
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

        private void DoSearch()
        {
            try
            {
                var stru = Invoice.AMStruct;
                var where0Time = locThoiGian1.GetFilterSql(stru, "", "like");
                var where1AM = locThongTin1.GetFilterSql(stru, "", "like");
                var w1 = GetAMFilterSql_TuyChon();
                if (w1.Length > 0)
                    where1AM += (where1AM.Length > 0 ? " and " : "") + w1;

                var stru2 = Invoice.ADStruct;
                var where2AD = locThongTinChiTiet1.GetFilterSql(stru2, "", "like");

                var w3NhomVt = GetNhVtFilterSql_TuyChon("", "like");

                var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
                var w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "like");

                tAM = Invoice.SearchPhieuNhap_HoaDon(_ngayCt, where0Time, where1AM, where2AD, w3NhomVt, w4Dvcs);
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
            ChungTu.SetTxtStatusProperties(Invoice, txtTrangThai, lblStatusDescription);
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

        private void CPN_XuatTraLaiNCC_Form_Load(object sender, EventArgs e)
        {
            HideLocKetQua();
        }

        public void Refresh0()
        {
            _locKetQua.Refresh0(_locKetQua.dataGridView1);
        }

        protected virtual void OnAcceptSelectEvent(List<SortedDictionary<string, object>> selecteddatalist)
        {
            var handler = AcceptSelectEvent;
            if (handler != null) handler(selecteddatalist);
        }
    }
}
