﻿using System;
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
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonBaoGia
{
    public partial class CBG_HoaDonForm : V6Form
    {
        private V6Invoice93 Invoice = new V6Invoice93();
        private CBG_HoaDonKetQua _locKetQua;
        private string _ma_dvcs, _ma_kh, _loai_ct_chon;
        //private bool __ready = false;
        private bool _viewMode;
        //private List<string> _orderListAD;
        //public delegate void AcceptSelectDataList(List<IDictionary<string, object>> selectedDataList);
        public event ChonAcceptSelectDataList AcceptSelectEvent;

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
        public CBG_HoaDonForm()
        {
            InitializeComponent();
        }

        public CBG_HoaDonForm(string ma_dvcs, string ma_kh)
        {
            InitializeComponent();
            _ma_dvcs = ma_dvcs;
            _ma_kh = ma_kh;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                InitTuyChon();
                InitLocKetQua();
                V6ControlFormHelper.CreateFormProgram(this, _locKetQua._aldmConfig, All_Objects, Event_Methods, out Form_program);

                panelFilter1.AddMultiFilterLine(Invoice.AMStruct, Invoice.ADV_AM);
                panelFilter2.AddMultiFilterLine(Invoice.ADStruct, Invoice.ADV_AD);

                maKhach.Text = _ma_kh;
                maKhach.ReadOnly = !string.IsNullOrEmpty(_ma_kh);
                txtMaDVCS.Text = _ma_dvcs;

                //_locKetQua.OnSelectAMRow += locKetQua_OnSelectAMRow;
                _locKetQua.AcceptSelectEvent += delegate { btnNhan.PerformClick(); };

                v6ColorDateTimePick1.SetValue(V6Setting.M_ngay_ct1);
                v6ColorDateTimePick2.SetValue(V6Setting.M_ngay_ct2);
                LoadDefaultData(4, Invoice.Mact, "SEARCH_SOA_" + Invoice.Mact, ItemID);
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
        }

        private void InitLocKetQua()
        {
            try
            {
                _locKetQua = new CBG_HoaDonKetQua(Invoice)
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

            //chkNSD.Checked = Invoice.M_LOC_NSD;
            //if (chkNSD.Checked && V6Login.Level == "05") chkNSD.Enabled = false;
            ChungTu.SetTxtStatusProperties(Invoice, txtTrangThai, lblStatusDescription);
        }
        
        private void SetValueAndShowLocKetQua()
        {
            ShowLocKetQua();
            _locKetQua.SetAM(tAM) ;
            Refresh0();
        }

        private void ShowLocKetQua()
        {
            grbThoiGian.Visible = false;
            grbThongTin.Visible = false;
            grbThongTinChiTiet.Visible = false;
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
                        string AD2AM_string = null;
                        if (_locKetQua._aldmConfig.HaveInfo && _locKetQua._aldmConfig.EXTRA_INFOR.ContainsKey("AD2AM"))
                        {
                            AD2AM_string += _locKetQua._aldmConfig.EXTRA_INFOR["AD2AM"];
                        }
                        else
                        {
                            V6ControlFormHelper.SetStatusText(V6Text.NoDefine + "_aldmConfig.EXTRA_INFOR.AD2AM");
                        }
                        ChonEventArgs e = new ChonEventArgs()
                        {
                            Loai_ct = _loai_ct_chon,
                            AD2AM = AD2AM_string
                        };
                        OnAcceptSelectEvent(data, e);
                        Close();
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Setting.IsVietnamese? "Chưa chọn dòng nào!\nDùng phím cách(space) hoặc Ctrl+A để chọn, Ctrl+U để bỏ chọn hết!"
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

        private string _where0Time = "", _where1AM = "", _where2AD = "", _w3NhomVt = "", _w4Dvcs = "", _w4Dvcs_2 = "";

        private void PrepareThread()
        {
            var stru = Invoice.AMStruct;
            _where0Time = GetFilterSql_ThoiGian(stru, "", chkThoiGianStart.Checked ? "start" : "like");
            _where1AM = GetFilterSql_ThongTin(stru, "", chkTTstart.Checked ? "start" : "like");
            var w1 = GetAMFilterSql_TuyChon();
            if (w1.Length > 0)
                _where1AM += (_where1AM.Length > 0 ? " and " : "") + w1;

            var stru2 = Invoice.ADStruct;
            _where2AD = GetFilterSql_ThongTinCT(stru2, "", chkTTCTstart.Checked ? "start" : "like");
            _w3NhomVt = GetNhVtFilterSql_TuyChon("", chkTuyChonStart.Checked ? "start" : "like");
            var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
            _w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "start");
            var option = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MADVCS2MAKHO"));
            if (option.Contains(Invoice.Mact))
            {
                _w4Dvcs_2 = Invoice.GetMaDvcsFilterByMaKho(maKhach.Text, txtMaDVCS.Text);
            }
        }

        private void DoSearch()
        {
            try
            {
                tAM = Invoice.SearchBaoGia(_where0Time, _where1AM, _where2AD, _w3NhomVt, _w4Dvcs, _w4Dvcs_2, out _loai_ct_chon);
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

        public string GetFilterSql_ThoiGian(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            V6Setting.M_ngay_ct1 = v6ColorDateTimePick1.Date;
            V6Setting.M_ngay_ct2 = v6ColorDateTimePick2.Date;

            var result = "";
            SortedDictionary<string, object> keys = V6ControlFormHelper.GetFormDataDictionary(grbThoiGian);
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


        public string GetFilterSql_ThongTinCT(V6TableStruct tableStruct, string tableLable,
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
            InvokeFormEvent(FormDynamicEvent.INIT2);
        }

        private void CDH_PNMForm_VisibleChanged(object sender, EventArgs e)
        {
            txtMaDVCS.Text = V6Login.Madvcs;
            if (Visible) v6ColorDateTimePick1.Focus();
        }

        public void Refresh0()
        {
            _locKetQua.Refresh0(_locKetQua.dataGridView1);
        }

        protected virtual void OnAcceptSelectEvent(List<IDictionary<string, object>> selecteddatalist, ChonEventArgs e)
        {
            var handler = AcceptSelectEvent;
            if (handler != null) handler(selecteddatalist, e);
        }
    }
}
