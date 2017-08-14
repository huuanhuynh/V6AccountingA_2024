using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    /// <summary>
    /// Control cơ bản dùng cho các control Chứng từ.
    /// </summary>
    public partial class V6InvoiceControl : V6FormControl
    {
        public V6InvoiceControl()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Khởi tạo control rỗng.
        /// </summary>
        /// <param name="maCt">Chưa dùng làm gì.</param>
        public V6InvoiceControl(string maCt)
        {
            MaCt = maCt;
            InitializeComponent();
            EmptyInit();
        }

        private void EmptyInit()
        {
            var lbl = new Label();
            lbl.Text = MaCt;
            Controls.Add(lbl);
        }

        //public delegate void ChangeInvoice(string sttRec);
        public event Action<string> InvoiceChanged;
        protected virtual void OnInvoiceChanged(string sttRec)
        {
            var handler = InvoiceChanged;
            if (handler != null) handler(sttRec);
        }

        public event Action<DataTable> AmChanged;
        protected virtual void OnAmChanged(DataTable data)
        {
            try
            {
                var handler = AmChanged;
                if (handler != null) handler(data);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".OnAmChanged", ex);
            }
        }

        public int CurrentIndex = -1;
        public DataTable AM { get { return am; } set { am = value;  OnAmChanged(value);} }
        private DataTable am;
        /// <summary>
        /// Nếu trước đó đã có hiển thị chứng từ mà bấm (mới) biến này sẽ lưu lại.
        /// </summary>
        protected DataRow AM_old;
        /// <summary>
        /// Luôn là bảng copy
        /// </summary>
        public DataTable AD, AD2, AD3;
        /// <summary>
        /// Dic này dùng sttrec không upper làm key (mặc định dữ liệu đã upper, kệ)
        /// </summary>
        public SortedDictionary<string, DataTable> ADTables, AD2Tables, AD3Tables;

        public void ResetADTables()
        {
            try
            {
                if (ADTables != null) ADTables.Clear();
                if (AD2Tables != null) AD2Tables.Clear();
                if (AD3Tables != null) AD3Tables.Clear();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ResetADTables", ex);
            }
        }

        public TabPage ParentTabPage { get; set; }

        /// <summary>
        /// Không dùng làm gì. Chỉ dùng trong EmptyControl.
        /// </summary>
        public string MaCt { get; set; }
        protected string _sttRec0 = "";
        protected string _sttRec02 = "";
        protected string _sttRec03 = "";
        protected string _mMaNt0 = "";
        protected string _maNt = "VND";
        protected bool co_chon_don_hang;
        public string MA_NT { get { return _maNt; } }
        
        protected int M_ROUND_NUM = V6Setting.RoundNum;
        protected int M_ROUND = V6Setting.RoundTien;
        protected int M_ROUND_NT = V6Setting.RoundTienNt;
        protected int M_ROUND_GIA = V6Setting.RoundGia;
        protected int M_ROUND_GIA_NT = V6Setting.RoundGiaNt;

        /// <summary>
        /// List thứ tự field chi tiết.
        /// </summary>
        protected List<string> _orderList = new List<string>();
        protected List<string> _orderList2 = new List<string>();
        protected List<string> _orderList3 = new List<string>();

        protected SortedDictionary<string, DataRow> _alct1Dic;
        protected SortedDictionary<string, DataRow> _alct2Dic;
        protected SortedDictionary<string, DataRow> _alct3Dic;

        protected DataGridViewRow _gv1EditingRow;
        protected DataGridViewRow _gv2EditingRow;
        protected DataGridViewRow _gv3EditingRow;

        private List<Control> _carryControls = new List<Control>();

        public bool IsHaveInvoice
        {
            get
            {
                return (AM != null && AM.Rows.Count > 0);
            }
        }
        public bool IsViewingAnInvoice
        {
            get
            {
                return (AM != null && AM.Rows.Count > CurrentIndex
                    && !string.IsNullOrEmpty(_sttRec) && CurrentIndex != -1);
            }
        }

        public event EventHandler BillChanged;
        /// <summary>
        /// Gây ra sự kiện để cập nhập thông tin lên button
        /// </summary>
        protected virtual void OnBillChanged()
        {
            var handler = BillChanged;
            if (handler != null) handler(this, new EventArgs());
        }

        public delegate void ChangeTableEventHandler(object sender, ChangeTableEventArgs e);
        public class ChangeTableEventArgs : EventArgs
        {
            //public string KhuFrom { get; set; }
            //public string KhuTo { get; set; }
            //public string TableFrom { get; set; }
            //public string TableTo { get; set; }
        }
        public event ChangeTableEventHandler ChangeTable;
        protected virtual void OnChangeTable(ChangeTableEventArgs e)
        {
            var handler = ChangeTable;
            if (handler != null) handler(this, e);
        }

        public event EventHandler ViewNext;
        protected virtual void OnViewNext()
        {
            var handler = ViewNext;
            if (handler != null) handler(this, new EventArgs());
        }

        

        /// <summary>
        /// Đổi Mode, Thay đổi trạng thái control Enable
        /// </summary>
        public V6Mode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                EnableControls();
                OnBillChanged();
            }
        }
        private V6Mode _mode = V6Mode.Init;

        /// <summary>
        /// Goi cac ham enable khac (functionButton, navi, form)
        /// </summary>
        protected void EnableControls()
        {
            EnableNavigationButtons();
            EnableFunctionButtons();
            EnableFormControls();
        }

        protected virtual void EnableNavigationButtons()
        {
            throw new System.NotImplementedException("Cần override.");
        }

        protected virtual void EnableFunctionButtons()
        {
            throw new System.NotImplementedException("Cần override.");
        }

        protected virtual void EnableFormControls()
        {
            throw new System.NotImplementedException("Cần override.");
        }

        public void CallViewInvoice(string sttrec, V6Mode mode)
        {
            ViewInvoice(sttrec, mode);
        }

        /// <summary>
        /// Kiểm tra phân bổ
        /// </summary>
        /// <param name="grid">GridView3ChiPhi</param>
        /// <param name="total">var total = TxtT_cp_nt.Value;</param>
        /// <returns></returns>
        protected bool CheckPhanBo(DataGridView grid, decimal total)
        {
            try
            {
                decimal sum = 0;
                foreach (DataGridViewRow row in grid.Rows)
                {
                    sum += ObjectAndString.ObjectToDecimal(row.Cells["CP_NT"].Value);
                }
                if (sum != total) return false;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CheckPhanBo", ex);
            }
            return true;
        }

        public virtual void ViewInvoice(string sttrec, V6Mode mode)
        {
            throw new NotImplementedException();
        }

        protected virtual void DisableAllFunctionButtons(Button btnLuu, Button btnMoi, Button btnCopy, Button btnIn, Button btnSua, Button btnHuy, Button btnXoa, Button btnXem, Button btnTim, Button btnQuayRa)
        {
            btnLuu.Enabled = false;
            btnMoi.Enabled = false;
            btnCopy.Enabled = false;
            btnIn.Enabled = false;
            btnSua.Enabled = false;
            btnHuy.Enabled = false;
            btnXoa.Enabled = false;
            btnXem.Enabled = false;
            btnTim.Enabled = false;
            btnQuayRa.Enabled = false;
        }

        /// <summary>
        /// Chỉnh lại kích thước của gridview có phần details.
        /// </summary>
        /// <param name="dgvs"></param>
        protected void FixDataGridViewSize(params V6ColorDataGridView[] dgvs)
        {
            try
            {
                foreach (V6ColorDataGridView dgv in dgvs)
                {
                    var gpa = dgv.Parent;
                    if (gpa != null)
                    {
                        dgv.Top = 52;
                        dgv.Height = gpa.Height - 55;
                        dgv.Left = 2;
                        dgv.Width = gpa.Width - 5;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixDataGridViewSize", ex);
            }
        }

        //protected void EnableButtonsWhenSave(Button btnLuu, Button btnMoi, Button btnCopy, Button btnIn, Button btnSua, Button btnHuy, Button btnXoa, Button btnXem, Button btnTim, Button btnQuayRa)
        //{
        //    EnableFunctionButtons();
        //}

        protected SortedDictionary<string, object> PreparingDataAM(V6InvoiceBase invoice)
        {
            var addDataAM = GetData();
            addDataAM["STT_REC"] = _sttRec;
            addDataAM["MA_CT"] = invoice.Mact;
            return addDataAM;
        }

        protected void LoadTag(V6InvoiceBase invoice, Control detailPanelControl)
        {
            try
            {
                var tagData = invoice.LoadTag(m_itemId);
                V6ControlFormHelper.SetFormTagDictionary(this, tagData);
                V6ControlFormHelper.SetFormTagDictionary(detailPanelControl, tagData);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadTag", ex);
            }
        }

        /// <summary>
        /// Tạo mới stt_rec và gán vào biến _sttRec.
        /// </summary>
        /// <param name="maCt">Invoice.Mact</param>
        /// <returns>_sttRec</returns>
        protected virtual string GetSttRec(string maCt)
        {
            V6ControlFormHelper.RemoveRunningList(_sttRec);
            _sttRec = V6BusinessHelper. GetNewSttRec(maCt);
            return _sttRec;
        }

        public TabPage GetParentTabPage()
        {
            try
            {
                var parent = Parent;
                for (int i = 0; i < 5; i++)
                {
                    if (parent is TabPage)
                    {
                        return (TabPage)parent;
                    }
                    else
                    {
                        parent = parent.Parent;
                    }
                }
            }
            catch
            {
                // ignored
            }
            return null;
        }

        /// <summary>
        /// Stt_rec &lt;&gt; ADrow["STT_REC_TT"]
        /// </summary>
        /// <returns></returns>
        protected string GetSoCt0InitFilter()
        {
            var result = "";
            try
            {
                foreach (DataRow row in AD.Rows)
                {
                    result += " And Stt_rec <> '" + row["STT_REC_TT"].ToString().Trim() + "'";
                }
                if (result.Length > 4) result = result.Substring(4);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GetSoCt0InitFilter: " + ex.Message);
            }
            return result;
        }
        protected string GetAlVitriTonInitFilter()
        {
            var result = "";
            try
            {
                //foreach (DataRow row in AD.Rows)
                //{
                //    result += " And Stt_rec <> '" + row["STT_REC_TT"].ToString().Trim() + "'";
                //}
                //if (result.Length > 4) result = result.Substring(4);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GetAlVitriTonInitFilter: " + ex.Message);
            }
            return result;
        }

        public void HienThiTongSoDong(Label lblTongSoDong)
        {
            var tSoDong = AD == null ? 0 : AD.Rows.Count;
            lblTongSoDong.Text = tSoDong.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Bật edit ở cột CP_NT, CP nếu lại pb = 0.
        /// </summary>
        /// <param name="loai_pb"></param>
        /// <param name="sua_tien"></param>
        /// <param name="dataGridView3ChiPhi"></param>
        protected void SetGridViewChiPhiEditAble(string loai_pb, bool sua_tien, V6ColorDataGridView dataGridView3ChiPhi)
        {
            if (loai_pb == "0" && (Mode == V6Mode.Add || Mode == V6Mode.Edit))
            {
                dataGridView3ChiPhi.SetEditColumn(sua_tien ? "CP,CP_NT".Split(',') : "CP_NT".Split(','));
            }
            else
            {
                dataGridView3ChiPhi.ReadOnly = true;
            }
        }

        /// <summary>
        /// Gán dữ liệu mặc định theo chứng từ. (VPA_GetDefaultvalue)
        /// </summary>
        /// <param name="invoice"></param>
        protected void SetDefaultData(V6InvoiceBase invoice)
        {
            try
            {
                var data = invoice.LoadDefaultData(V6Setting.Language, m_itemId);
                var data0 = new SortedDictionary<string, object>();
                data0.AddRange(data);
                var controlDic = V6ControlFormHelper.SetFormDataDictionary(this, new SortedDictionary<string, object>(data0), false);
                FixVvarBrothers(controlDic);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetDefaultData", ex);
            }
        }

        /// <summary>
        /// Gán dữ liệu liên quan của các vVar textbox.
        /// </summary>
        /// <param name="controlDic"></param>
        private void FixVvarBrothers(SortedDictionary<string, Control> controlDic)
        {
            if (!V6Setting.Fixinvoicevvar) return;
            try
            {
                foreach (KeyValuePair<string, Control> item in controlDic)
                {
                    if (item.Value is V6VvarTextBox)
                    {
                        ((V6VvarTextBox) item.Value).ExistRowInTable();
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixVvarBrothers", ex);
            }
        }

        protected void SetDefaultDataDetail(V6InvoiceBase invoice, Control detailControl)
        {
            try
            {
                var data = invoice.LoadDefaultData(V6Setting.Language, m_itemId);
                var data0 = new SortedDictionary<string, object>();
                data0.AddRange(data);
                V6ControlFormHelper.SetFormDataDictionary(detailControl, new SortedDictionary<string, object>(data0), false);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetDefaultDataDetail", ex);
            }
        }

        /// <summary>
        /// Gán dữ liệu mặc định khi bấm mới chi tiết. Cần override
        /// </summary>
        protected virtual void SetDefaultDetail()
        {
            
        }

        protected void SetInitFilterAll()
        {
            try
            {
                //MA_KH
                var temp_control = V6ControlFormHelper.GetControlByAccesibleName(this, "MA_KH");
                if (temp_control is V6VvarTextBox)
                {
                    var txt = temp_control as V6VvarTextBox;
                    var init_filter = V6Login.GetInitFilter("ALKH");
                    var init_filter_user = "[Status] <> '0'";
                    if (!string.IsNullOrEmpty(init_filter_user))
                    {
                        init_filter += (string.IsNullOrEmpty(init_filter) ? "" : " and ") + init_filter_user;
                    }
                    txt.SetInitFilter(init_filter);
                }
                
                //MA_KH_I
                temp_control = V6ControlFormHelper.GetControlByAccesibleName(this, "MA_KH_I");
                if (temp_control is V6VvarTextBox)
                {
                    var txt = temp_control as V6VvarTextBox;
                    var init_filter = V6Login.GetInitFilter("ALKH");
                    var init_filter_user = "[Status] <> '0'";
                    if (!string.IsNullOrEmpty(init_filter_user))
                    {
                        init_filter += (string.IsNullOrEmpty(init_filter) ? "" : " and ") + init_filter_user;
                    }
                    txt.SetInitFilter(init_filter);
                }

                //MA_VT
                temp_control = V6ControlFormHelper.GetControlByAccesibleName(this, "MA_VT");
                if (temp_control is V6VvarTextBox)
                {
                    var txt = temp_control as V6VvarTextBox;
                    var init_filter = V6Login.GetInitFilter("ALVT");
                    var init_filter_extra = "[Status] <> '0'";
                    if (!string.IsNullOrEmpty(init_filter_extra))
                    {
                        init_filter += (string.IsNullOrEmpty(init_filter) ? "" : " and ") + init_filter_extra;
                    }
                    txt.SetInitFilter(init_filter);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetInitFilterAll", ex);
            }
        }

        protected void ShowViewInfoData(V6InvoiceBase invoice)
        {
            try
            {
                if ((V6Login.IsAdmin || V6Login.Level != "05") && !string.IsNullOrEmpty(_sttRec))
                    new ViewInfoData(invoice, _sttRec, invoice.Mact).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ShowViewInfoData " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Hàm cần override để thay đổi container, Hiển thị thông báo phía trên của form chứng từ.
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ShowParentMessage(string message)
        {
            try
            {
                var parent = Parent.Parent;
                for (int i = 0; i < 5; i++)
                {
                    if (parent is ChungTuChungContainer)
                    {
                        ((ChungTuChungContainer)parent)
                            .ShowMessage(message);
                        return;
                    }
                    else
                    {
                        parent = parent.Parent;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowParentMessage", ex);
            }
        }

        protected void TinhSoluongQuyDoi(V6NumberTextBox _soLuong1
            , V6NumberTextBox _sl_qd, V6NumberTextBox _sl_qd2
            , V6NumberTextBox _hs_qd1, V6NumberTextBox _hs_qd2)
        {
            try
            {
                _sl_qd.Value = _soLuong1.Value * _hs_qd1.Value;
                _sl_qd2.Value = V6BusinessHelper.Vround(
                    (_sl_qd.Value * _hs_qd2.Value) - (((int)_sl_qd.Value) * _hs_qd2.Value), 1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi", ex);
            }
        }


        protected decimal TinhTong(DataTable AD_table, string colName)
        {
            return V6BusinessHelper.TinhTong(AD_table, colName);
        }

        
        protected void TinhChietKhauChiTiet(bool nhapTien, V6NumberTextBox _ck_textbox, V6NumberTextBox _ck_nt_textbox,
            V6NumberTextBox txtTyGia, V6NumberTextBox _tienNt2, V6NumberTextBox _pt_cki)
        {
            try
            {
                if (nhapTien)
                {
                    _ck_textbox.Value = V6BusinessHelper.Vround(_ck_nt_textbox.Value * txtTyGia.Value, M_ROUND);
                    if (_maNt == _mMaNt0)
                    {
                        _ck_textbox.Value = _ck_nt_textbox.Value;
                    }
                }
                else
                {
                    _ck_nt_textbox.Value = V6BusinessHelper.Vround(_tienNt2.Value * _pt_cki.Value / 100, M_ROUND_NT);
                    _ck_textbox.Value = V6BusinessHelper.Vround(_ck_nt_textbox.Value * txtTyGia.Value, M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        _ck_textbox.Value = _ck_nt_textbox.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhChietKhauChiTiet", ex);
            }
        }


        #region ==== Tính toán trong chi tiết ====

        /// <summary>
        /// Tính lại tiền thuế nt hoặc thuế theo thuế suất và tiền tương ứng nhập vào
        /// </summary>
        /// <param name="thueSuat">vd: 10 là 10%</param>
        /// <param name="tien"></param>
        /// <param name="txtTienThue"></param>
        /// <param name="round"></param>
        protected void Tinh_TienThue_TheoThueSuat(decimal thueSuat, decimal tien, V6NumberTextBox txtTienThue, int round)
        {
            try
            {
                txtTienThue.Value = V6BusinessHelper.Vround(tien * thueSuat / 100, round);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
        protected void Tinh_TienThue_TheoTienThueNt(decimal tienThueNt, decimal tyGia, V6NumberTextBox txtTienThue, int round)
        {
            try
            {
                txtTienThue.Value = V6BusinessHelper.Vround(tienThueNt * tyGia, round);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected void Tinh_TienThueNtVaTienThue_TheoThueSuat(decimal thueSuat, decimal tienNt, decimal tien, V6NumberTextBox txtTienThueNt, V6NumberTextBox txtTienThue)
        {
            try
            {
                Tinh_TienThue_TheoThueSuat(thueSuat, tienNt, txtTienThueNt, M_ROUND_NT);
                Tinh_TienThue_TheoThueSuat(thueSuat, tien, txtTienThue, M_ROUND);

                if (_maNt == _mMaNt0)
                {
                    txtTienThue.Value = txtTienThueNt.Value;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Xử lý khi thay đổi giá trị tienNt (trong chi tiết).
        /// </summary>
        /// <param name="tienNt">giá trị tienNt sau khi thay đổi.</param>
        /// <param name="tyGia">tỷ giá đổi tienNt ra tien.</param>
        /// <param name="thueSuat"></param>
        /// <param name="txtTien"></param>
        /// <param name="txtTienThueNt"></param>
        /// <param name="txtTienThue"></param>
        protected void TienNtChanged(decimal tienNt, decimal tyGia, decimal thueSuat, V6NumberTextBox txtTien,
            V6NumberTextBox txtTienThueNt, V6NumberTextBox txtTienThue)
        {
            try
            {
                txtTien.Value = V6BusinessHelper.Vround(tienNt * tyGia, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    txtTien.Value = tienNt;
                }
                Tinh_TienThueNtVaTienThue_TheoThueSuat(thueSuat, tienNt, txtTien.Value, txtTienThueNt, txtTienThue);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Tính lại tiền thuế sau khi thay đổi tiền.
        /// </summary>
        /// <param name="tien">Giá trị Tiền sau khi thay đổi.</param>
        /// <param name="thueSuat">Thuế suất theo %. vd 10 là 10%.</param>
        /// <param name="txtTienThue">TextBox tiền thuế.</param>
        protected void TienChanged(decimal tien, decimal thueSuat, V6NumberTextBox txtTienThue)
        {
            try
            {
                Tinh_TienThue_TheoThueSuat(thueSuat, tien, txtTienThue, M_ROUND);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #endregion Tính toán trong chi tiết ====================================================


        protected bool ValidateNgayCt(string maCt, DateTimePicker dateNgayCT)
        {
            try
            {
                string message = "";
                if (V6BusinessHelper.CheckNgayCt(maCt, dateNgayCT.Value, out message))
                {
                    ShowParentMessage("");
                    return true;
                }

                ShowParentMessage(message);
                dateNgayCT.Focus();
                return false;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ValidateNgayCt", ex);
            }
            return false;
        }

        private void V6InvoiceControl_Load(object sender, EventArgs e)
        {
            SetInitFilterAll();
            LoadLanguage();
        }

        protected void XuLyHienThiChietKhau_PhieuNhap(bool ckChung, bool suaTienCk,
            V6NumberTextBox _pt_cki_textbox, V6NumberTextBox _ckNt_textbox,
            V6NumberTextBox txtTongCkNt, CheckBox chkSuaPtck)
        {
            chkSuaPtck.Enabled = ckChung;

            if (ckChung)
            {
                _pt_cki_textbox.DisableTag();
                _ckNt_textbox.DisableTag();

                txtTongCkNt.ReadOnly = !suaTienCk;
            }
            else
            {
                txtTongCkNt.ReadOnly = true;
                _pt_cki_textbox.EnableTag(); _pt_cki_textbox.ReadOnlyTag(false);

                if (suaTienCk)
                {
                    _ckNt_textbox.EnableTag(); _ckNt_textbox.ReadOnlyTag(false);
                }
                else
                {
                    _ckNt_textbox.DisableTag();
                }
            }
        }

        protected void SetDefaultDataReference(V6InvoiceBase invoice, string itemID, string controlName, DataRow controlData)
        {
            //Lay thong tin gan du lieu 20161129
            var infos = invoice.LoadDataReferenceInfo(V6Setting.Language, ItemID);
            //Duyet txtmahttt
            //from TK_NO to MA_NX
            //data[to] = from
            //Chuẩn bị dữ liệu để gán lên form
            SortedDictionary<string, object> someData = new SortedDictionary<string, object>();
            foreach (DefaultValueInfo item in infos)
            {
                if (item.Type1 == "0")
                {
                    //Value null vẫn gán. không làm gì hết.
                }
                else if (item.Type1 == "2")
                {
                    //Kiểm tra value trên form theo Name. rỗng mới gán
                    var fValue = V6ControlFormHelper.GetFormValue(this, item.Name).ToString().Trim();
                    if (!string.IsNullOrEmpty(fValue)) continue;
                }

                if (item.Value.StartsWith(controlName + "."))//Lấy dữ liệu theo trường nào đó trong txtMaHttt.Data
                {
                    var getField = item.Value.Split('.')[1].Trim();
                    if (controlData.Table.Columns.Contains(getField))
                    {
                        var getValue = ("" + controlData[getField]).Trim();
                        if (item.Type1 == "1")
                        {
                            //Value khác null mới gán
                            if (string.IsNullOrEmpty(getValue)) continue;
                        }
                        someData[item.Name.ToUpper()] = getValue;
                    }
                }
            }
            SetSomeData(someData);
        }

        private void FixTyGia(DataRow row, decimal ty_gia, string fieldTien, string fieldTienNt, int round)
        {
            if (AD.Columns.Contains(fieldTien) && AD.Columns.Contains(fieldTienNt))
            {
                decimal temp = ObjectAndString.ObjectToDecimal(row[fieldTienNt]);
                if (temp != 0)
                    row[fieldTien] = V6BusinessHelper.Vround(temp * ty_gia, round);
            }
        }
        protected void XuLyThayDoiTyGia(V6NumberTextBox txtTyGia, CheckBox chkSua_Tien)
        {
            try
            {
                var ty_gia = txtTyGia.Value;

                // Tuanmh 25/05/2017
                if (ty_gia == 0 || chkSua_Tien.Checked) return;

                decimal temp;

                foreach (DataRow row in AD.Rows)
                {
                    FixTyGia(row, ty_gia, "Tien", "Tien_nt", M_ROUND);
                    FixTyGia(row, ty_gia, "Tien2", "Tien_nt2", M_ROUND);
                    FixTyGia(row, ty_gia, "Tien1", "Tien1_nt", M_ROUND);
                    FixTyGia(row, ty_gia, "Tien_vc", "Tien_vc_nt", M_ROUND);
                    FixTyGia(row, ty_gia, "Tien0", "TIEN_NT0", M_ROUND);
                    FixTyGia(row, ty_gia, "Thue", "Thue_nt", M_ROUND);
                    FixTyGia(row, ty_gia, "CP", "CP_NT", M_ROUND);
                    FixTyGia(row, ty_gia, "GIA01", "GIA_NT01", M_ROUND_GIA);
                    FixTyGia(row, ty_gia, "GIA1", "GIA_NT1", M_ROUND_GIA);
                    FixTyGia(row, ty_gia, "GIA2", "GIA_NT2", M_ROUND_GIA);
                    FixTyGia(row, ty_gia, "GIA21", "GIA_NT21", M_ROUND_GIA);

                    FixTyGia(row, ty_gia, "GIA0", "GIA_NT0", M_ROUND_GIA);
                    FixTyGia(row, ty_gia, "NK", "NK_NT", M_ROUND);
                    FixTyGia(row, ty_gia, "CK", "CK_NT", M_ROUND);
                    FixTyGia(row, ty_gia, "GG", "GG_NT", M_ROUND);

                    FixTyGia(row, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                    FixTyGia(row, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);

                }

                if(AD2 != null)
                foreach (DataRow row in AD2.Rows)
                {
                    FixTyGia(row, ty_gia, "t_tien", "t_tien_nt", M_ROUND);
                    FixTyGia(row, ty_gia, "t_thue", "t_thue_nt", M_ROUND);
                    FixTyGia(row, ty_gia, "t_tt", "t_tt_nt", M_ROUND);
                }

                if (AD3 != null)
                    foreach (DataRow row in AD3.Rows)
                    {
                        FixTyGia(row, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGia(row, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyThayDoiTyGia: " + ex.Message);
            }
        }

        #region ==== ValidateData_Master_CheckTon ====

        /// <summary>
        /// Kiểm tra tồn kho trước khi lưu. Trả về true là ok. 123
        /// </summary>
        /// <param name="Invoice"></param>
        /// <param name="ngayCt">Ngày ct đang nhập trên form.</param>
        /// <param name="maKhoX">Mã kho xuất trên form, nếu dùng Ma_kho_i để null</param>
        /// <returns></returns>
        protected bool ValidateData_Master_CheckTon(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            try
            {
                if (V6Options.M_CHK_XUAT != "0") return true;

                var M_CHECK_SAVE_STOCK = V6Options.V6OptionValues["M_CHECK_SAVE_STOCK"];
                string message = "";
                foreach (char c in M_CHECK_SAVE_STOCK)
                {
                    switch (c)
                    {
                        case '1':
                            message = CheckMakhoMavt(Invoice, ngayCt, maKhoX);
                            if (!string.IsNullOrEmpty(message)) goto ThongBao;
                            break;
                        case '2':
                            message = CheckMakhoMavtMalo(Invoice, ngayCt, maKhoX);
                            if (!string.IsNullOrEmpty(message)) goto ThongBao;
                            break;
                        case '3':
                            message = CheckMakhoMavtMaloMavitri(Invoice, ngayCt, maKhoX);
                            if (!string.IsNullOrEmpty(message)) goto ThongBao;
                            break;
                        default:
                            break;
                    }
                }

            ThongBao:
                if (!string.IsNullOrEmpty(message))
                {
                    this.ShowWarningMessage(message);
                    return false;
                }
                //Kiểm tra ok.
                return true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "ValidateData_Master_CheckTon", ex);
                ShowParentMessage("Có lỗi khi check tồn: " + ex.Message);
                return true;
            }
        }

        private string CheckMakhoMavtMaloMavitri(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            #region === KiemTra3 Kho,VatTu,Lo,Vitri ===
            //Reset biến
            Dictionary<string, decimal> mavt_makho_malo_mavitri__soluong = new Dictionary<string, decimal>();
            var mavt_list = new List<string>();
            var makho_list = new List<string>();
            var malo_list = new List<string>();
            var mavitri_list = new List<string>();
            string mavt_in = "", makho_in = "", malo_in = "", mavitri_in = "";
            foreach (DataRow row in AD.Rows)
            {
                string c_mavt = row["Ma_vt"].ToString().Trim();
                string c_makho = maKhoX ?? row["Ma_kho_i"].ToString().Trim();
                string c_malo = row["Ma_lo"].ToString().Trim();
                string c_mavitri = row["Ma_vitri"].ToString().Trim();
                string c_mavt_makho_malo_mavitri = c_mavt + "~" + c_makho + "~" + c_malo + "~" + c_mavitri;
                decimal c_soluong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                //lay thong tin lodate cua mavt
                bool lo = false, date = false, vitri = false;
                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select(V6TableName.Alvt, key, "*").Data;
                if (lodate_data.Rows.Count == 1)
                {
                    DataRow row0 = lodate_data.Rows[0];
                    lo = row0["Lo_yn"].ToString().Trim() == "1";
                    date = row0["Date_yn"].ToString().Trim() == "1";
                    vitri = row0["Vitri_yn"].ToString().Trim() == "1";
                }
                if (lo && date && vitri)
                {
                    if (!mavt_list.Contains(c_mavt))
                    {
                        mavt_list.Add(c_mavt);
                        mavt_in += ",'" + c_mavt + "'";
                    }
                    if (!makho_list.Contains(c_makho))
                    {
                        makho_list.Add(c_makho);
                        makho_in += ",'" + c_makho + "'";
                    }
                    if (!malo_list.Contains(c_malo))
                    {
                        malo_list.Add(c_malo);
                        malo_in += ",'" + c_malo + "'";
                    }
                    if (!mavitri_list.Contains(c_mavitri))
                    {
                        mavitri_list.Add(c_mavitri);
                        mavitri_in += ",'" + c_mavitri + "'";
                    }

                    if (mavt_makho_malo_mavitri__soluong.ContainsKey(c_mavt_makho_malo_mavitri))
                    {
                        mavt_makho_malo_mavitri__soluong[c_mavt_makho_malo_mavitri] += c_soluong;
                    }
                    else
                    {
                        mavt_makho_malo_mavitri__soluong[c_mavt_makho_malo_mavitri] = c_soluong;
                    }
                }
            }
            if (mavt_in.Length > 0) mavt_in = mavt_in.Substring(1);
            if (makho_in.Length > 0) makho_in = makho_in.Substring(1);
            if (malo_in.Length > 0) malo_in = malo_in.Substring(1);
            if (mavitri_in.Length > 0) mavitri_in = mavitri_in.Substring(1);
            
            if (string.IsNullOrEmpty(mavt_in) || string.IsNullOrEmpty(makho_in) || string.IsNullOrEmpty(malo_in))
            {
                return null;
            }
            var data = Invoice.GetViTriLoDateAll(mavt_in, makho_in, malo_in, mavitri_in, _sttRec, ngayCt.Date);
            //Kiểm tra
            string message = "";

            foreach (KeyValuePair<string, decimal> item in mavt_makho_malo_mavitri__soluong)
            {
                var ss = item.Key.Split('~');
                var c_mavt = ss[0];
                var c_makho = ss[1];
                var c_malo = ss[2];
                var c_mavitri = ss[3];

                foreach (DataRow row in data.Rows)
                {
                    var data_mavt = row["Ma_vt"].ToString().Trim();
                    var data_makho = row["Ma_kho"].ToString().Trim();
                    var data_malo = row["Ma_lo"].ToString().Trim();
                    var data_mavitri = row["Ma_vitri"].ToString().Trim();
                    var data_soluong = ObjectAndString.ObjectToDecimal(row["Ton_dau"]);
                    if (c_mavt == data_mavt && c_makho == data_makho && c_malo == data_malo && c_mavitri == data_mavitri)
                    {
                        if (data_soluong < item.Value)
                        {
                            message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Vitri:{5}  Tồn:{0}  Xuất:{1}\n",
                                data_soluong, item.Value, c_makho, c_mavt, c_malo, c_mavitri);
                        }
                        goto NextItem;
                    }
                }

                //else //Có dữ liệu nhưng không trùng mã...
                message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Vitri:{5}  Tồn:{0}  Xuất:{1}\n",
                            0, item.Value, c_makho, c_mavt, c_malo, c_mavitri);

            NextItem:
                DoNothing();
            }
            return message;
            #endregion kho,vt,lo
        }

        private string CheckMakhoMavtMalo(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            #region === KiemTra2 Kho,VatTu,Lo ===
            //Reset biến
            Dictionary<string, decimal> mavt_makho_malo__soluong = new Dictionary<string, decimal>();
            var mavt_list = new List<string>();
            var makho_list = new List<string>();
            var malo_list = new List<string>();
            string mavt_in = "", makho_in = "", malo_in = "";
            foreach (DataRow row in AD.Rows)
            {
                string c_mavt = row["Ma_vt"].ToString().Trim();
                string c_makho = maKhoX ?? row["Ma_kho_i"].ToString().Trim();
                string c_malo = row["Ma_lo"].ToString().Trim();
                string c_mavt_makho_malo = c_mavt + "~" + c_makho + "~" + c_malo;
                decimal c_soluong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                //lay thong tin lodate cua mavt
                bool lo = false, date = false;
                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select(V6TableName.Alvt, key, "*").Data;
                if (lodate_data.Rows.Count == 1)
                {
                    DataRow row0 = lodate_data.Rows[0];
                    lo = row0["Lo_yn"].ToString().Trim() == "1";
                    date = row0["Date_yn"].ToString().Trim() == "1";
                }
                if (lo && date)
                {
                    if (!mavt_list.Contains(c_mavt))
                    {
                        mavt_list.Add(c_mavt);
                        mavt_in += ",'" + c_mavt + "'";
                    }
                    if (!makho_list.Contains(c_makho))
                    {
                        makho_list.Add(c_makho);
                        makho_in += ",'" + c_makho + "'";
                    }
                    if (!malo_list.Contains(c_malo))
                    {
                        malo_list.Add(c_malo);
                        malo_in += ",'" + c_malo + "'";
                    }

                    if (mavt_makho_malo__soluong.ContainsKey(c_mavt_makho_malo))
                    {
                        mavt_makho_malo__soluong[c_mavt_makho_malo] += c_soluong;
                    }
                    else
                    {
                        mavt_makho_malo__soluong[c_mavt_makho_malo] = c_soluong;
                    }
                }
            }
            if (mavt_in.Length > 0) mavt_in = mavt_in.Substring(1);
            if (makho_in.Length > 0) makho_in = makho_in.Substring(1);
            if (malo_in.Length > 0) malo_in = malo_in.Substring(1);

            if (string.IsNullOrEmpty(mavt_in) || string.IsNullOrEmpty(makho_in) || string.IsNullOrEmpty(malo_in))
            {
                return null;
            }
            var data = Invoice.GetLoDateAll(mavt_in, makho_in, malo_in, _sttRec, ngayCt);
            //Kiểm tra
            string message = "";

            foreach (KeyValuePair<string, decimal> item in mavt_makho_malo__soluong)
            {
                var ss = item.Key.Split('~');
                var c_mavt = ss[0];
                var c_makho = ss[1];
                var c_malo = ss[2];

                //if(data.Rows.Count > 0)
                foreach (DataRow row in data.Rows)
                {
                    var data_mavt = row["Ma_vt"].ToString().Trim();
                    var data_makho = row["Ma_kho"].ToString().Trim();
                    var data_malo = row["Ma_lo"].ToString().Trim();
                    var data_soluong = ObjectAndString.ObjectToDecimal(row["Ton_dau"]);
                    if (c_mavt == data_mavt && c_makho == data_makho && c_malo == data_malo)
                    {
                        if (data_soluong < item.Value)
                        {
                            message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                                data_soluong, item.Value, c_makho, c_mavt, c_malo);
                        }

                        goto NextItem;
                    }
                }

                message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                                0, item.Value, c_makho, c_mavt, c_malo);

            NextItem:
                DoNothing();
            }

            return message;
            #endregion kho,vt,lo
        }

        private string CheckMakhoMavt(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            #region === Check Makho, Mavt ===
            Dictionary<string, decimal> mavt_makho__soluong = new Dictionary<string, decimal>();
            // tạo key in
            List<string> mavt_list = new List<string>();
            List<string> makho_list = new List<string>();
            string mavt_in = "", makho_in = "";
            foreach (DataRow row in AD.Rows)
            {
                string c_mavt = row["Ma_vt"].ToString().Trim();
                string c_makho = maKhoX ?? row["Ma_kho_i"].ToString().Trim();
                string c_mavt_makho = c_mavt + "~" + c_makho;
                decimal c_soluong = ObjectAndString.ObjectToDecimal(row["So_luong"]);

                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select(V6TableName.Alvt, key, "*").Data;
                if (lodate_data.Rows.Count != 1)
                {
                    continue; // Bỏ qua không kiểm tra.
                    //DataRow row0 = lodate_data.Rows[0];
                    //lo = row0["Lo_yn"].ToString().Trim() == "1";
                    //date = row0["Date_yn"].ToString().Trim() == "1";
                }

                if (!mavt_list.Contains(c_mavt))
                {
                    mavt_list.Add(c_mavt);
                    mavt_in += ",'" + c_mavt + "'";
                }
                if (!makho_list.Contains(c_makho))
                {
                    makho_list.Add(c_makho);
                    makho_in += ",'" + c_makho + "'";
                }

                if (mavt_makho__soluong.ContainsKey(c_mavt_makho))
                {
                    mavt_makho__soluong[c_mavt_makho] += c_soluong;
                }
                else
                {
                    mavt_makho__soluong[c_mavt_makho] = c_soluong;
                }
            }

            if (mavt_in.Length > 0) mavt_in = mavt_in.Substring(1);
            if (makho_in.Length > 0) makho_in = makho_in.Substring(1);
            //Get dữ liệu tồn
            var data = Invoice.GetStockAll(mavt_in, makho_in, _sttRec, ngayCt);
            //Kiểm tra
            string message = "";
            foreach (KeyValuePair<string, decimal> item in mavt_makho__soluong)
            {
                var ss = item.Key.Split('~');
                var c_mavt = ss[0];
                var c_makho = ss[1];
                foreach (DataRow row in data.Rows)
                {
                    var data_mavt = row["Ma_vt"].ToString().Trim();
                    var data_makho = row["Ma_kho"].ToString().Trim();
                    var data_soluong = ObjectAndString.ObjectToDecimal(row["Ton00"]);
                    if (c_mavt == data_mavt && c_makho == data_makho)
                    {
                        if (data_soluong < item.Value)
                        {
                            message += string.Format("Kho:{2}  Vật tư:{3}  Tồn:{0}  Xuất:{1}\n",
                                data_soluong, item.Value, c_makho, c_mavt);
                        }

                        goto NextItem;
                    }
                }
                // Nếu không gặp được bộ mã trùng item.Key
                message += string.Format("Kho:{2}  Vật tư:{3}  Tồn:{0}  Xuất:{1}\n",
                                0, item.Value, c_makho, c_mavt);

            NextItem:
                DoNothing();
            }
            return message;
            #endregion makho, mavt
        }
        #endregion CheckTon

        protected void XemPhieuNhapView(DateTime ngayCT, string maCT, string maKho, string maVt)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@nXT", 1),
                    new SqlParameter("@Type", 0),
                    new SqlParameter("@Ngay_ct", ngayCT.Date),
                    new SqlParameter("@Ma_ct", maCT),
                    new SqlParameter("@Stt_rec", _sttRec),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@M_lan", V6Login.SelectedLanguage),
                    new SqlParameter("@Advance", string.Format("Ma_kho='{0}' and Ma_vt='{1}'", maKho, maVt)),
                    new SqlParameter("@OutputInsert", ""),
                };
                var data0 = V6BusinessHelper.ExecuteProcedure("VPA_Get_STOCK_IN_VIEWF5", plist);
                if (data0 == null || data0.Tables.Count == 0)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }

                var data = data0.Tables[0];
                FilterView f = new FilterView(data, "MA_KH", "STOCK_IN_VIEWF5", null, null);
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XemPhieuNhapView", ex);
            }
        }

    }
}
