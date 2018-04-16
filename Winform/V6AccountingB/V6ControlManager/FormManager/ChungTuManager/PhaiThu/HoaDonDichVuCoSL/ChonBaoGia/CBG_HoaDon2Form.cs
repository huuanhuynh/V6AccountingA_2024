using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL.ChonDonHang;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL.ChonBaoGia
{
    public partial class CBG_HoaDonDichVuCoSLForm : V6Form
    {
        private V6Invoice93 Invoice = new V6Invoice93();
        private CBG_HoaDonDichVuCoSLKetQua _locKetQua;
        private string _ma_dvcs, _ma_kh;
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
        public CBG_HoaDonDichVuCoSLForm()
        {
            InitializeComponent();
        }

        public CBG_HoaDonDichVuCoSLForm(string ma_dvcs, string ma_kh)
        {
            InitializeComponent();
            _ma_dvcs = ma_dvcs;
            _ma_kh = ma_kh;
            MyInit();
        }

        private void MyInit()
        {
            InitTuyChon();
            _locKetQua =  new CBG_HoaDonDichVuCoSLKetQua(Invoice)
            { Dock = DockStyle.Fill, Visible = false };
            panel1.Controls.Add(_locKetQua);

            panelFilter1.AddMultiFilterLine(Invoice.AMStruct, Invoice.ADV_AM);
            panelFilter2.AddMultiFilterLine(Invoice.ADStruct, Invoice.ADV_AD);

            maKhach.Text = _ma_kh;
            txtMaDVCS.Text = _ma_dvcs;

            //_locKetQua.OnSelectAMRow += locKetQua_OnSelectAMRow;
            _locKetQua.AcceptSelectEvent += delegate { btnNhan.PerformClick(); };

            v6ColorDateTimePick1.Value = V6Setting.M_ngay_ct1;
            v6ColorDateTimePick2.Value = V6Setting.M_ngay_ct2;
        }

        private void InitTuyChon()
        {
            txtNhomVT1.SetInitFilter("loai_nh=1");
            txtNhomVT2.SetInitFilter("loai_nh=2");
            txtNhomVT3.SetInitFilter("loai_nh=3");
            txtNhomVT4.SetInitFilter("loai_nh=4");
            txtNhomVT5.SetInitFilter("loai_nh=5");
            txtNhomVT6.SetInitFilter("loai_nh=6");

            txtMaDVCS.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDVCS.Enabled = false;
                txtMaDVCS.ReadOnly = true;
            }

            chkNSD.Checked = Invoice.M_LOC_NSD;
            if (chkNSD.Checked) chkNSD.Enabled = false;
            ChungTu.SetTxtStatusProperties(Invoice, txtTrangThai, lblStatusDescription);
        }
        
        private void SetValueAndShowLocKetQua()
        {
            _locKetQua.SetAM(tAM);
            Refresh0();
            //_locKetQua.SetAD(_HangTraLaiForm.AD, _orderListAD);

            ShowLocKetQua();
        }
        private void ShowLocKetQua()
        {
            grbThoiGian.Visible = false;
            grbThongTin.Visible = false;
            grbThongTinChiTiet.Visible = false;
            grbTuyChon.Visible = false;

            _locKetQua.Visible = true;
            _locKetQua.dataGridView1.Focus();
        }

        public void HideLocKetQua()
        {
            _locKetQua.Visible = false;
            grbThoiGian.Visible = true;
            grbThongTin.Visible = true;
            grbThongTinChiTiet.Visible = true;
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
                this.ShowErrorMessage(GetType() + ".Tìm chứng từ lỗi:\n" + ex.Message, "HangTraLai.ChonPhieuXuat");
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

        public string GetFilterSql_ThoiGian(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            V6Setting.M_ngay_ct1 = v6ColorDateTimePick1.Value;
            V6Setting.M_ngay_ct2 = v6ColorDateTimePick2.Value;

            var result = "";
            SortedDictionary<string, object> keys = V6ControlFormHelper.GetFormDataDictionary(grbThoiGian);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            var dateFilter = string.Format("{0}ngay_ct BETWEEN '{1}' AND '{2}'",
                tableLable.Length > 0 ? tableLable + "." : "",
                v6ColorDateTimePick1.Value.ToString("yyyyMMdd"),
                v6ColorDateTimePick2.Value.ToString("yyyyMMdd")
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

        public string GetFilterSql_ThongTin(V6TableStruct tableStruct, string tableLable = null,
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
                    result += string.Format("{0} ({1}so_ct >= '{2}' and {1}so_ct <= '{3}')",
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

            SortedDictionary<string, object> keys = V6ControlFormHelper.GetFormDataDictionary(grbThongTin);
            var result2 = SqlGenerator.GenWhere2_oper(tableStruct, keys, oper, and, tableLable);
            if (result2.Length > 0)
            {
                if (result.Length > 0)
                    result += and_or + "(" + result2 + ")";
                else result = "(" + result2 + ")";
            }

            //advance
            var rAdvance = panelFilter1.GetQueryString(tableStruct, tableLable, and);
            if (rAdvance.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "") + " " + rAdvance;
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


        public string GetFilterSql_TTChiTiet(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var tLable = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            var result = "";
            SortedDictionary<string, object> keys = V6ControlFormHelper.GetFormDataDictionary(grbThongTinChiTiet);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            if (result.Length > 0)
            {
                result = "(" + result + ")";
            }

            //advance
            var rAdvance = panelFilter2.GetQueryString(tableStruct, tableLable, and);
            if (rAdvance.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "") + rAdvance;
            }

            return result;
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
                result += (result.Length > 0 ? " and " : "") + "kieu_post='" + txtTrangThai.Text.Trim() + "'";
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
            if (txtNhomVT4.Text.Trim() != "")
                keys.Add("NH_VT4", txtNhomVT4.Text.Trim());
            if (txtNhomVT5.Text.Trim() != "")
                keys.Add("NH_VT5", txtNhomVT5.Text.Trim());
            if (txtNhomVT6.Text.Trim() != "")
                keys.Add("NH_VT6", txtNhomVT6.Text.Trim());

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

        private void DoSearch()
        {
            try
            {
                var stru = Invoice.AMStruct;
                var where0Time = GetFilterSql_ThoiGian(stru, "", "like");
                var where1AM = GetFilterSql_ThongTin(stru, "", "like");
                var w1 = GetAMFilterSql_TuyChon();
                if (w1.Length > 0)
                    where1AM += (where1AM.Length > 0 ? " and " : "") + w1;

                var stru2 = Invoice.ADStruct;
                var where2AD = GetFilterSql_TTChiTiet(stru2, "", "like");

                var w3NhomVt = GetNhVtFilterSql_TuyChon("", "like");

                var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
                var w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "like");

                tAM = Invoice.SearchBaoGia(where0Time, where1AM, where2AD, w3NhomVt, w4Dvcs);
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

        private void Form_Load(object sender, EventArgs e)
        {
            
        }

        private void CDH_PNMForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) v6ColorDateTimePick1.Focus();
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
