using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
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

        protected void SetDefaultData(V6InvoiceBase invoice)
        {
            try
            {
                var data = invoice.LoadDefaultData(V6Setting.Language, m_itemId);
                var data0 = new SortedDictionary<string, object>();
                data0.AddRange(data);
                V6ControlFormHelper.SetFormDataDictionary(this, new SortedDictionary<string, object>(data0), false);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetDefaultData", ex);
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
        /// Gán dữ liệu mặc định khi bấm mới chi tiết.
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
                    new ViewInfoData(invoice, _sttRec, invoice.Mact).ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ShowViewInfoData " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Hàm cần override
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
    }
}
