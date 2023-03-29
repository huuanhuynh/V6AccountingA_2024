using System;
using System.Collections.Generic;
using System.Data;
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

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapKhau.Loc
{
    public partial class TimPhieuNhapKhauForm : V6Form
    {
        public DataTable _formChungTu_AM;
        public DataTable _formChungTu_AD;
        public DataTable _formChungTu_AD2;
        private V6Mode _mode;
        public V6Invoice72 _invoice;

        public LocKetQuaPhieuNhapKhau _locKetQua;
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
        public TimPhieuNhapKhauForm()
        {
            InitializeComponent();
        }

        public TimPhieuNhapKhauForm(V6Invoice72 invoice, V6Mode mode)
        {
            InitializeComponent();
            _mode = mode;
            _invoice = invoice;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                InitTuyChon();
                InitLocKetQua();
                V6ControlFormHelper.CreateFormProgram(this, _locKetQua._aldmConfig, All_Objects, Event_Methods, out Event_program);

                locThongTin1.CreateDynamicFilter(_invoice.AMStruct, _invoice.ADV_AM);
                locThongTinChiTiet1.CreateDynamicFilter2(_invoice.ADStruct, _invoice.ADV_AD);

                _locKetQua.OnSelectAMRow += locKetQua_OnSelectAMRow;
                _locKetQua.AcceptSelectEvent += delegate { btnNhan.PerformClick(); };

                LoadDefaultData(4, "POB", "SEARCH_POB", ItemID);
                if (_locKetQua._aldmConfig.HaveInfo)
                {
                    Text = V6Setting.IsVietnamese ? _locKetQua._aldmConfig.TITLE : _locKetQua._aldmConfig.TITLE2;
                }
                else
                {
                    Text = Text + " " + _locKetQua._aldmConfig.ma_dm + " aldm.TITLE";
                }
                V6ControlFormHelper.ApplyDynamicFormControlEvents_ByName(this, Event_program, All_Objects);
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
                _locKetQua = new LocKetQuaPhieuNhapKhau(_invoice, _formChungTu_AM, _formChungTu_AD)
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
                _formChungTu_AD = _invoice.LoadAD(sttrec);
                _formChungTu_AD2 = _invoice.LoadAD2(sttrec);
                _locKetQua.SetAD(_formChungTu_AD, _formChungTu_AD2);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".OnSelectAM: " + ex.Message);
            }
        }

        private void SetValueAndShowLocKetQua()
        {
            _locKetQua.SetAM(tempAM);
            ChungTu.ViewSearchSumary(this, tempAM, lblDocSoTien, _invoice.Mact, V6Options.M_MA_NT0);
            ShowLocKetQua();
        }

        private void ShowLocKetQua()
        {
            locThoiGian1.Visible = false;
            locThongTin1.Visible = false;
            locThongTinChiTiet1.Visible = false;
            grbTuyChon.Visible = false;

            _locKetQua.Visible = true;
            _locKetQua.BringToFront();
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
            _where1AM = locThongTin1.GetFilterSql(stru, "", chkTTstart.Checked ? "start" : "like");
            var w1 = GetAMFilterSql_TuyChon();
            if (w1.Length > 0)
                _where1AM += (_where1AM.Length > 0 ? " and " : "") + w1;

            var stru2 = _invoice.ADStruct;
            _where2AD = locThongTinChiTiet1.GetFilterSql(stru2, "", chkTTCTstart.Checked ? "start" : "like");
            _w3NhomVt = GetNhVtFilterSql_TuyChon("", chkTuyChonStart.Checked ? "start" : "like");
            var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
            _w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "start");
            var option = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MADVCS2MAKHO"));
            if (option.Contains(_invoice.Mact))
            {
                _w4Dvcs_2 = _invoice.GetMaDvcsFilterByMaKho(locThongTin1.maKhach.Text, txtMaDVCS.Text);
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
            _where0Time = locThoiGian1.GetFilterSql(stru, "", chkThoiGianStart.Checked ? "start" : "like");
            _where1AM = locThongTin1.GetFilterSql(stru, "", chkTTstart.Checked ? "start" : "like");
            var w1 = GetAMFilterSql_TuyChon();
            if (w1.Length > 0)
                _where1AM += (_where1AM.Length > 0 ? " and " : "") + w1;

            var stru2 = _invoice.ADStruct;
            _where2AD = locThongTinChiTiet1.GetFilterSql(stru2, "", chkTTCTstart.Checked ? "start" : "like");
            _w3NhomVt = GetNhVtFilterSql_TuyChon("", chkTuyChonStart.Checked ? "start" : "like");
            var struDvcs = V6BusinessHelper.GetTableStruct("ALDVCS");
            _w4Dvcs = GetDvcsFilterSql_TuyChon(struDvcs, "", "start");
            var option = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MADVCS2MAKHO"));
            if (option.Contains(_invoice.Mact))
            {
                _w4Dvcs_2 = _invoice.GetMaDvcsFilterByMaKho(locThongTin1.maKhach.Text, txtMaDVCS.Text);
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
            if (ViewMode && _formChungTu_AM != null)
            {
                ShowLocKetQua();
            }
            else
            {
                HideLocKetQua();
            }
            InvokeFormEvent(FormDynamicEvent.INIT2);
        }

        public void Refresh0()
        {
            _locKetQua.Refresh0(_locKetQua.dataGridView1);
        }

        private void TimPhieuNhapKhauForm_Activated(object sender, EventArgs e)
        {
            locThoiGian1.Focus();
        }

        private void TimPhieuNhapKhauForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
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

        private void TimPhieuNhapKhauForm_VisibleChanged(object sender, EventArgs e)
        {
            txtMaDVCS.Text = V6Login.Madvcs;
        }
    }
}
