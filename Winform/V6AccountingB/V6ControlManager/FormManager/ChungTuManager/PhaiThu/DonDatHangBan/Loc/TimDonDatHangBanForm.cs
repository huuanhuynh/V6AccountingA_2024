using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.DonDatHangBan.Loc
{
    public partial class TimDonDatHangBanForm : V6Form
    {
        private readonly DonDatHangBanControl _formChungTu;
        private V6Invoice91 _invoice;
        private LocKetQuaDonDatHangBan _locKetQua;
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
        public TimDonDatHangBanForm()
        {
            InitializeComponent();
        }

        public TimDonDatHangBanForm(DonDatHangBanControl formChungTu)
        {
            InitializeComponent();
            _formChungTu = formChungTu;
            _invoice = formChungTu.Invoice;
            MyInit();
        }

        private void MyInit()
        {
            dateNgayCt1.SetValue(V6Setting.M_ngay_ct1);
            dateNgayCt2.SetValue(V6Setting.M_ngay_ct2);
            MyInit_TT();
            MyInit_TTCT();
            InitTuyChon();
            InitLocKetQua();

            CreateDynamicFilter_TT(_invoice.AMStruct, _invoice.ADV_AM);
            CreateDynamicFilter2_TTCT(_invoice.ADStruct, _invoice.ADV_AD);
            
            _locKetQua.OnSelectAMRow += locKetQua_OnSelectAMRow;
            _locKetQua.AcceptSelectEvent += delegate { btnNhan.PerformClick(); };

            LoadDefaultData(4, "SOH", "SEARCH_SOH", ItemID);
        }

        private void MyInit_TT()
        {
            try
            {
                soTienTu.DecimalPlaces = V6Options.M_ROUND_NT;
                soTienDen.DecimalPlaces = V6Options.M_ROUND_NT;

                txtNhomKH1.SetInitFilter("LOAI_NH = 1");
                txtNhomKH2.SetInitFilter("LOAI_NH = 2");
                txtNhomKH3.SetInitFilter("LOAI_NH = 3");
                txtNhomKH4.SetInitFilter("LOAI_NH = 4");
                txtNhomKH5.SetInitFilter("LOAI_NH = 5");
                txtNhomKH6.SetInitFilter("LOAI_NH = 6");
                txtNhomKH7.SetInitFilter("LOAI_NH = 7");
                txtNhomKH8.SetInitFilter("LOAI_NH = 8");
                txtNhomKH9.SetInitFilter("LOAI_NH = 9");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void InitLocKetQua()
        {
            try
            {
                _locKetQua = new LocKetQuaDonDatHangBan(_invoice, _formChungTu.AM, _formChungTu.AD)
                {Dock = DockStyle.Fill, Visible = false};
                panel1.Controls.Add(_locKetQua);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".InitLocKetQua", ex);
            }
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

            chkNSD.Checked = _invoice.M_LOC_NSD;
            if (chkNSD.Checked && V6Login.Level == "05") chkNSD.Enabled = false;
            ChungTu.SetTxtStatusProperties(_invoice, txtTrangThai, lblStatusDescription);
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
            ShowLocKetQua();
            _locKetQua.SetAM(tempAM);
            ChungTu.ViewSearchSumary(this, tempAM, lblDocSoTien, _invoice.Mact, _formChungTu.MA_NT);
        }

        private void ShowLocKetQua()
        {
            grbThoiGian.Visible = false;
            grbThongTin.Visible = false;
            grbThongTinCT.Visible = false;
            grbTuyChon.Visible = false;

            _locKetQua.Visible = true;
            _locKetQua.BringToFront();
            _locKetQua.dataGridView1.Focus();
        }

        public void HideLocKetQua()
        {
            _locKetQua.Visible = false;
            grbThoiGian.Visible = true;
            grbThongTin.Visible = true;
            grbThongTinCT.Visible = true;
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

        /// <summary>
        /// 1 tìm top cuối kỳ.
        /// </summary>
        private string flag_search_topcuoiky = "";
        public void SearchTopCuoiKy()
        {
            SearchThread_TopCuoiKy();
        }

        private void SearchThread_TopCuoiKy()
        {
            //ReadyFor
            CheckForIllegalCrossThreadCalls = false;
            Timer timerCheckSearch = new Timer { Interval = 500 };
            timerCheckSearch.Tick += checkSearch_Tick;
            flagSearchFinish = false;
            flagSearchSuccess = false;
            btnNhan.Enabled = false;
            PrepareThread_TopCuoiKy();
            flag_search_topcuoiky = "1";
            new Thread(DoSearch)
                {
                    IsBackground = true
                }
                .Start();

            timerCheckSearch.Start();
        }

        private void PrepareThread_TopCuoiKy()
        {
            var stru = _invoice.AMStruct;
            _where0Time = string.Format("ngay_ct <= '{0:yyyyMMdd}'", V6Setting.M_Ngay_ck);
            _where1AM = GetFilterSql_ThongTin(stru, "", chkTTstart.Checked ? "start" : "like");
            var w1 = GetAMFilterSql_TuyChon();
            if (w1.Length > 0)
                _where1AM += (_where1AM.Length > 0 ? " and " : "") + w1;

            var stru2 = _invoice.ADStruct;
            _where2AD = GetFilterSql_TTCT(stru2, "", "like");
            _w3NhomVt = GetNhVtFilterSql_TuyChon("", chkTuyChonStart.Checked ? "start" : "like");
            var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
            _w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "start");
            var option = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MADVCS2MAKHO"));
            if (option.Contains(_invoice.Mact))
            {
                _w4Dvcs_2 = _invoice.GetMaDvcsFilterByMaKho(maKhach.Text, txtMaDVCS.Text);
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
        private DataTable tempAM;

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

        private string _where0Time = "", _where1AM = "", _where2AD = "", _w3NhomVt = "", _w4Dvcs = "", _w4Dvcs_2 = "";

        private void PrepareThread()
        {
            var stru = _invoice.AMStruct;
            _where0Time = GetFilterSql_ThoiGian(stru, "", chkThoiGianStart.Checked ? "start" : "like");
            _where1AM = GetFilterSql_ThongTin(stru, "", chkTTstart.Checked ? "start" : "like");
            var w1 = GetAMFilterSql_TuyChon();
            if (w1.Length > 0)
                _where1AM += (_where1AM.Length > 0 ? " and " : "") + w1;

            var stru2 = _invoice.ADStruct;
            _where2AD = GetFilterSql_TTCT(stru2, "", "like");
            _w3NhomVt = GetNhVtFilterSql_TuyChon("", chkTuyChonStart.Checked ? "start" : "like");
            var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
            _w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "start");
            var option = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MADVCS2MAKHO"));
            if (option.Contains(_invoice.Mact))
            {
                _w4Dvcs_2 = _invoice.GetMaDvcsFilterByMaKho(maKhach.Text, txtMaDVCS.Text);
            }
        }

        private void DoSearch()
        {
            try
            {
                if (flag_search_topcuoiky == "1")
                {
                    tempAM = _invoice.SearchAM_TopCuoiKy(_where0Time, _where1AM, _where2AD, _w3NhomVt, _w4Dvcs, _w4Dvcs_2);
                }
                else
                {
                    tempAM = _invoice.SearchAM(_where0Time, _where1AM, _where2AD, _w3NhomVt, _w4Dvcs, _w4Dvcs_2);
                }

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

        public string GetFilterSql_ThoiGian(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            V6Setting.M_ngay_ct1 = dateNgayCt1.Date;
            V6Setting.M_ngay_ct2 = dateNgayCt2.Date;

            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(grbThoiGian);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            var dateFilter = string.Format("{0}ngay_ct BETWEEN '{1}' AND '{2}'",
                tableLable.Length > 0 ? tableLable + "." : "",
                dateNgayCt1.YYYYMMDD,
                dateNgayCt2.YYYYMMDD
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

            var keys = V6ControlFormHelper.GetFormDataDictionary(grbThongTin);
            var result2 = SqlGenerator.GenWhere2_oper(tableStruct, keys, oper, and, tableLable);
            if (result2.Length > 0)
            {
                if (result.Length > 0)
                    result += and_or + "(" + result2 + ")";
                else result = "(" + result2 + ")";
            }

            //advance
            var rAdvance = panelFilterTT.GetQueryString(tableStruct, tableLable, and);
            var keys_Adv = panelFilterTT.GetQueryKeys();//Test get key Dic
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

            string where_nhkh = GetNhKhFilterSql(tbL, "like", true);
            if (where_nhkh.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "")
                    + tbL
                    + string.Format(" MA_KH in (Select Ma_kh from ALKH where {0})", where_nhkh);
            }

            return result;
        }

        public string GetNhKhFilterSql(string tableLable, string oper = "=", bool and = true)
        {
            var result = "";

            var keys = new SortedDictionary<string, object>();
            if (txtNhomKH1.Text.Trim() != "")
                keys.Add("NH_KH1", txtNhomKH1.Text.Trim());
            if (txtNhomKH2.Text.Trim() != "")
                keys.Add("NH_KH2", txtNhomKH2.Text.Trim());
            if (txtNhomKH3.Text.Trim() != "")
                keys.Add("NH_KH3", txtNhomKH3.Text.Trim());
            if (txtNhomKH4.Text.Trim() != "")
                keys.Add("NH_KH4", txtNhomKH4.Text.Trim());
            if (txtNhomKH5.Text.Trim() != "")
                keys.Add("NH_KH5", txtNhomKH5.Text.Trim());
            if (txtNhomKH6.Text.Trim() != "")
                keys.Add("NH_KH6", txtNhomKH6.Text.Trim());
            if (txtNhomKH7.Text.Trim() != "")
                keys.Add("NH_KH7", txtNhomKH4.Text.Trim());
            if (txtNhomKH8.Text.Trim() != "")
                keys.Add("NH_KH8", txtNhomKH5.Text.Trim());
            if (txtNhomKH9.Text.Trim() != "")
                keys.Add("NH_KH9", txtNhomKH6.Text.Trim());

            if (keys.Count > 0)
            {
                var struAlvt = V6BusinessHelper.GetTableStruct("ALKH");
                result = SqlGenerator.GenWhere2(struAlvt, keys, oper, and, tableLable);
            }
            return result;
        }

        public void CreateDynamicFilter_TT(V6TableStruct amStruct, string advAM)
        {
            panelFilterTT.AddMultiFilterLine(amStruct, advAM);
        }

        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }

        private void MyInit_TTCT()
        {
            txtMaSanPham.SetInitFilter("Loai_vt=55");
        }

        public string GetFilterSql_TTCT(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var tLable = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(grbThongTinCT);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            if (result.Length > 0)
            {
                result = "(" + result + ")";
            }

            //advance
            var rAdvance = panelFilterTTCT.GetQueryString(tableStruct, tableLable, and);
            if (rAdvance.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "") + rAdvance;
            }

            return result;
        }

        public void CreateDynamicFilter2_TTCT(V6TableStruct adStruct, string advAD)
        {
            panelFilterTTCT.AddMultiFilterLine(adStruct, advAD);
        }

        public string GetAMFilterSql_TuyChon()
        {
            var result = "";

            if (!V6Login.IsAdmin && chkNSD.Checked)
            {
                result += string.Format("(user_id2 = {0} or user_id0 = {0})", V6Login.UserId);
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

        private void TimDonDatHangBanForm_Load(object sender, EventArgs e)
        {
            if (_formChungTu.AM != null && _viewMode)
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

        private void TimDonDatHangBanForm_Activated(object sender, EventArgs e)
        {
            dateNgayCt1.Focus();
        }

        private void TimDonDatHangBanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void UpdateAM(string sttRec, IDictionary<string, object> data, V6Mode mode)
        {
            try
            {
                DataTable dt = _locKetQua.dataGridView1.DataSource as DataTable;
                if (dt == null) return;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["STT_REC"].ToString().Trim() == sttRec)
                    {
                        if (mode == V6Mode.Delete) dt.Rows.Remove(row);
                        else V6ControlFormHelper.UpdateDataRow(row, data);
                        return;
                    }
                }

                dt.AddRow(data);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".UpdateAM", ex);
            }
            ChungTu.ViewSearchSumary(this, tempAM, lblDocSoTien, _invoice.Mact, _formChungTu.MA_NT);
        }

        private void TimDonDatHangBanForm_VisibleChanged(object sender, EventArgs e)
        {
            txtMaDVCS.Text = V6Login.Madvcs;
        }
    }
}
