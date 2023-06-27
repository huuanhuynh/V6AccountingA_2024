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
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi.Loc
{
    public partial class TimPhieuChiForm : V6Form
    {
        public DataTable _formChungTu_AM;
        public DataTable _formChungTu_AD;
        public DataTable _formChungTu_AD2;
        private V6Mode _mode;
        private readonly V6Invoice51 _invoice;
        public LocKetQuaPhieuChi _locKetQua;
        private bool _viewMode;
        private List<string> _orderListAD2;

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
        public TimPhieuChiForm()
        {
            InitializeComponent();
        }

        public TimPhieuChiForm(V6Invoice51 invoice, V6Mode mode, List<string> orderListAD2)
        {
            InitializeComponent();
            _mode = mode;
            _invoice = invoice;
            _orderListAD2 = orderListAD2;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                dateNgayCt1.SetValue(V6Setting.M_ngay_ct1);
                dateNgayCt2.SetValue(V6Setting.M_ngay_ct2);
                MyInit_ThongTin();
                MyInit_TTCT();
                InitTuyChon();
                InitLocKetQua();
                V6ControlFormHelper.CreateFormProgram(this, _locKetQua._aldmConfig, All_Objects, Event_Methods, out Form_program);

                CreateDynamicFilter_ThongTin(_invoice.AMStruct, _invoice.ADV_AM);
                CreateDynamicFilter2_ThongTinCT(_invoice.ADStruct, _invoice.ADV_AD);

                LoadDefaultData(4, _invoice.Mact, "SEARCH_" + _invoice.Mact, ItemID);
                if (_locKetQua._aldmConfig.HaveInfo)
                {
                    Text = V6Setting.IsVietnamese ? _locKetQua._aldmConfig.TITLE : _locKetQua._aldmConfig.TITLE2;
                }
                else
                {
                    Text = Text + " " + _locKetQua._aldmConfig.ma_dm + " aldm.TITLE";
                }
                V6ControlFormHelper.ApplyDynamicFormControlEvents_ByName(this, Form_program, All_Objects);
                InvokeFormEvent(FormDynamicEvent.INIT);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }

            Ready();
        }

        private void InitLocKetQua()
        {
            try
            {
                _locKetQua = new LocKetQuaPhieuChi(_invoice, _formChungTu_AM, _formChungTu_AD)
                {
                    Dock = DockStyle.Fill,
                    Visible = false
                };
                panel1.Controls.Add(_locKetQua);
                _locKetQua.OnSelectAMRow += locKetQua_OnSelectAMRow;
                _locKetQua.AcceptSelectEvent += delegate { btnNhan.PerformClick(); };
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
                _formChungTu_AD = _invoice.LoadAD(sttrec);
                _formChungTu_AD2 = _invoice.LoadAD2(sttrec);
                _locKetQua.SetAD(_formChungTu_AD, _formChungTu_AD2, _orderListAD2);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".OnSelectAM: " + ex.Message);
            }
        }

        private void SetValueAndShowLocKetQua()
        {
            ShowLocKetQua();
            _locKetQua.SetAM(tempAM) ;
            ChungTu.ViewSearchSumary(this, tempAM, lblDocSoTien, _invoice.Mact, V6Options.M_MA_NT0);
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
                    _formChungTu_AM = tempAM;
                    DialogResult = DialogResult.OK;
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
            _where0Time = string.Format("ngay_ct <= '{0:yyyyMMdd}' and ngay_ct >= '{1:yyyyMMdd}'", V6Setting.M_Ngay_ck, V6Setting.M_Ngay_dk);
            _where1AM = GetFilterSql_ThongTin(stru, "", chkTTstart.Checked ? "start" : "like");
            var w1 = GetAMFilterSql_TuyChon();
            if (w1.Length > 0)
                _where1AM += (_where1AM.Length > 0 ? " and " : "") + w1;

            var stru2 = _invoice.ADStruct;
            _where2AD = GetFilterSql_ThongTinCT(stru2, "", chkTTCTstart.Checked ? "start" : "like");
            _w3NhomVt = GetNhVtFilterSql_TuyChon("", chkTuyChonStart.Checked ? "start" : "like");
            var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
            _w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "start");
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
            flag_search_topcuoiky = "0";
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
            _where2AD = GetFilterSql_ThongTinCT(stru2, "", chkTTCTstart.Checked ? "start" : "like");
            _w3NhomVt = GetNhVtFilterSql_TuyChon("", chkTuyChonStart.Checked ? "start" : "like");
            var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
            _w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "start");
        }

        private void DoSearch()
        {
            try
            {
                if (flag_search_topcuoiky == "1")
                {
                    tempAM = _invoice.SearchAM_TopCuoiKy(_where0Time, _where1AM, _w4Dvcs, _where2AD, _w3NhomVt);
                }
                else
                {
                    tempAM = _invoice.SearchAM(_where0Time, _where1AM, _w4Dvcs, _where2AD, _w3NhomVt);
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
                tableLable.Length>0?tableLable+".":"",
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

        public void Focus1()
        {
            dateNgayCt1.Focus();
        }

        private void MyInit_ThongTin()
        {
            try
            {
                txtMaDVCS.Text = V6Login.Madvcs;
                if (V6Login.MadvcsCount <= 1)
                {
                    txtMaDVCS.Enabled = false;
                }
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
                this.WriteExLog(GetType() + ".MyInit_ThongTin", ex);
            }
        }

        public string GetFilterSql_ThongTin(V6TableStruct tableStruct, string tableLable,
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
                    result += " " + and_or + " (" + result2 + ")";
                else result = "(" + result2 + ")";
            }

            //advance
            var rAdvance = panelFilterTT.GetQueryString(tableStruct, tableLable, and);
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

            string where_nhkh = GetNhKhFilterSql_ThongTin(tbL, "like", true);
            if (where_nhkh.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "")
                    + tbL
                    + string.Format(" MA_KH in (Select Ma_kh from ALKH where {0})", where_nhkh);
            }

            return result;
        }

        public string GetNhKhFilterSql_ThongTin(string tableLable, string oper = "=", bool and = true)
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

        public void CreateDynamicFilter_ThongTin(V6TableStruct amStruct, string advAM)
        {
            panelFilterTT.AddMultiFilterLine(amStruct, advAM);
        }

        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }

        private void grbThongTin_VisibleChanged(object sender, EventArgs e)
        {
            txtMaDVCS.Text = V6Login.Madvcs;
        }

        private void MyInit_TTCT()
        {
            txtMaSanPham.SetInitFilter("Loai_vt=55");
        }
        public string GetFilterSql_ThongTinCT(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(grbThongTinCT);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            //advance
            var rAdvance = panelFilterTTCT.GetQueryString(tableStruct, tableLable, and);
            if (rAdvance.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "") + rAdvance;
            }

            return result;
        }

        public void CreateDynamicFilter2_ThongTinCT(V6TableStruct adStruct, string advAD)
        {
            panelFilterTTCT.AddMultiFilterLine(adStruct, advAD);
        }

        private void InitTuyChon()
        {
            chkNSD.Checked = _invoice.M_LOC_NSD;
            if (chkNSD.Checked && V6Login.Level == "05") chkNSD.Enabled = false;
            ChungTu.SetTxtStatusProperties(_invoice, txtTrangThai, lblStatusDescription);
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

            return result;
        }

        public string GetDvcsFilterSql_TuyChon(V6TableStruct tableStruct, string tableLable, string oper = "=", bool and = true)
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
            if (_mode == V6Mode.Select)
            {
                DialogResult = DialogResult.Cancel;
            }
            else if (!_viewMode && _locKetQua != null && _locKetQua.Visible)
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
            if (_formChungTu_AM != null && _viewMode)
            {
                ShowLocKetQua();
            }
            else
            {
                HideLocKetQua();
            }
            InitTuyChon();
            InvokeFormEvent(FormDynamicEvent.INIT2);
        }

        private void TimPhieuChiForm_KeyDown(object sender, KeyEventArgs e)
        {
            btnHuy.PerformClick();
        }

        private void TimPhieuChiForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                if(grbThoiGian.Visible) Focus1();
            }
        }

        public void Refresh0()
        {
            _locKetQua.Refresh0(_locKetQua.dataGridView1);
        }

        private void TimPhieuChiForm_Activated(object sender, EventArgs e)
        {
            Focus1();
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
            ChungTu.ViewSearchSumary(this, tempAM, lblDocSoTien, _invoice.Mact, V6Options.M_MA_NT0);
        }
    }
}
