using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
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
            MyInit0();
        }
        
        public V6InvoiceControl(string maCt, string itemId)
        {
            MaCt = maCt;
            InitializeComponent();
        }
        
        private void MyInit0()
        {
            var lbl = new Label();
            lbl.Text = MaCt;
            Controls.Add(lbl);
        }

        protected void LoadAdvanceControls(string ma_ct)
        {
            try
            {
                V6ControlFormHelper.CreateAdvanceFormControls(this, ma_ct, All_Objects);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
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
                this.WriteExLog(GetType() + ".OnAmChanged " + _sttRec, ex);
            }
        }

        public int CurrentIndex = -1;
        public DataTable AM { get { return am; } set { am = value;  OnAmChanged(value);} }
        private DataTable am;
        /// <summary>
        /// Dữ liệu AM hiện tại.
        /// </summary>
        public DataRow AM_current
        {
            get
            {
                if (am == null || CurrentIndex < 0 || CurrentIndex >= am.Rows.Count) return null;
                return am.Rows[CurrentIndex];
            }
        }

        /// <summary>
        /// Nếu trước đó đã có hiển thị chứng từ mà bấm (mới) biến này sẽ lưu lại.
        /// </summary>
        public DataRow AM_old;
        /// <summary>
        /// Chi tiết / chi tiết thuế / bổ sung. Luôn là bảng copy
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
                this.WriteExLog(GetType() + ".ResetADTables " + _sttRec, ex);
            }
        }

        public TabPage ParentTabPage { get; set; }

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
        public string M_SOA_HT_KM_CK = V6Options.GetValue("M_SOA_HT_KM_CK");
        public string M_SOA_MULTI_VAT = V6Options.GetValue("M_SOA_MULTI_VAT");
        public string M_CAL_SL_QD_ALL = V6Options.GetValue("M_CAL_SL_QD_ALL");

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
            EnableVisibleControls();
        }

        protected virtual void EnableNavigationButtons()
        {
            throw new System.NotImplementedException("Cần override.");
        }

        protected virtual void EnableFunctionButtons()
        {
            throw new System.NotImplementedException("Cần override.");
        }

        /// <summary>
        /// Ẩn hiện, set Readonly cho các control.
        /// </summary>
        protected virtual void EnableVisibleControls()
        {
            throw new System.NotImplementedException("Cần override.");
        }

        protected void SetControlReadOnlyHide(Control container, V6InvoiceBase invoice, V6Mode mode)
        {
            try //  Ẩn hiện theo quyền trong Alctct
            {
                if (AM == null) return;
                List<string> add_readonly = new List<string>();
                List<string> edit_readonly = new List<string>();
                int sl_in = 0;
                if(Mode == V6Mode.Edit)
                    if(AM.Columns.Contains("SL_IN")) sl_in = ObjectAndString.ObjectToInt(AM_current["SL_IN"]);

                foreach (string s in invoice.GRD_READONLY)
                {
                    if (s.Contains(":"))
                    {
                        var ss = s.Split(':');
                        if (ss.Length > 1)
                        {
                            if(ss[1].Contains("M")) add_readonly.Add(ss[0]);
                            if(ss[1].Contains("S")) edit_readonly.Add(ss[0]);
                            if(sl_in > 0 && ss[1].Contains("I")) edit_readonly.Add(ss[0]);
                        }
                    }
                    else
                    {
                        add_readonly.Add(s);
                        edit_readonly.Add(s);
                    }
                }
                
                if (mode == V6Mode.Add)
                {
                    V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, edit_readonly, false);
                    V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, add_readonly, true);
                }
                else if (mode == V6Mode.Edit)
                {
                    V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, add_readonly, false);
                    V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, edit_readonly, true);
                    //V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, in_readonly, true);
                }

                //V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, invoice.GRD_READONLY, true);
                V6ControlFormHelper.SetListControlVisibleByAccessibleNames(container, invoice.GRD_HIDE, false);
            }
            catch (Exception ex2)
            {
                this.WriteExLog(GetType() + ".EnableFormControls ex2", ex2);
            }
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
                this.WriteExLog(GetType() + ".CheckPhanBo " + _sttRec, ex);
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
                this.WriteExLog(GetType() + ".FixDataGridViewSize " + _sttRec, ex);
            }
        }
        
        protected SortedDictionary<string, object> PreparingDataAM(V6InvoiceBase invoice)
        {
            var addDataAM = GetData();
            addDataAM["STT_REC"] = _sttRec;
            addDataAM["MA_CT"] = invoice.Mact;
            return addDataAM;
        }

        protected void LoadTag(V6InvoiceBase invoice, ControlCollection detailPanelControls)
        {
            try
            {
                var tagData = invoice.LoadTag(m_itemId);
                V6ControlFormHelper.SetFormTagDictionary(this, tagData);
                if (detailPanelControls != null)
                {
                    foreach (Control control in detailPanelControls)
                    {
                        V6ControlFormHelper.SetFormTagDictionary(control, tagData);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadTag " + _sttRec, ex);
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            return result;
        }

        protected string GetAlLoTonInitFilter()
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                this.WriteExLog(GetType() + ".SetDefaultData " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Gán dữ liệu liên quan của các vVar textbox.
        /// </summary>
        /// <param name="controlDic"></param>
        private void FixVvarBrothers(SortedDictionary<string, Control> controlDic)
        {
            if (!V6Setting.FixInvoiceVvar) return;
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
                this.WriteExLog(GetType() + ".FixVvarBrothers " + _sttRec, ex);
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
                this.WriteExLog(GetType() + ".SetDefaultDataDetail " + _sttRec, ex);
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
            if (V6Setting.IsDesignTime) return;
            try
            {
                string M_V6_ADV_FILTER = V6Options.GetValue("M_V6_ADV_FILTER");
                string[] infos = ObjectAndString.SplitString(M_V6_ADV_FILTER);//"MA_SONB:ALSONB:1;");

                foreach (string info in infos)
                {
                    var sss = info.Split(':');
                    string NAME = "", tableName = "", status = "";
                    if (sss.Length > 1)
                    {
                        NAME = sss[0].ToUpper();
                        tableName = sss[1];
                        status = sss.Length > 2 ? sss[2] : "";
                    }
                    else
                    {
                        continue;
                    }


                    var tempControl = V6ControlFormHelper.GetControlByAccessibleName(this, NAME);
                    if (tempControl is V6VvarTextBox)
                    {
                        var txt = tempControl as V6VvarTextBox;

                        var old_filter = txt.InitFilter;
                        var adv_filter = V6Login.GetInitFilter(tableName, V6ControlFormHelper.FindFilterType(this));
                        if (status == "1")
                        {
                            var adv_filter_extra = "[Status] <> '0'";
                            if (!string.IsNullOrEmpty(adv_filter_extra))
                            {
                                adv_filter += (string.IsNullOrEmpty(adv_filter) ? "" : " and ") + adv_filter_extra;
                            }
                        }
                        //var new_filter = old_filter;
                        //if (string.IsNullOrEmpty(new_filter))
                        if (string.IsNullOrEmpty(old_filter))
                        {
                            //new_filter = adv_filter;
                            txt.SetInitFilter(adv_filter);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(adv_filter))
                            {
                                //new_filter = string.Format("({0}) and ({1})", old_filter, adv_filter);
                                txt.AddInitFilter(adv_filter);
                            }
                        }
                        //txt.SetInitFilter(new_filter);
                    }
                }

                return;
                //MA_KH
                var temp_control = V6ControlFormHelper.GetControlByAccessibleName(this, "MA_KH");
                if (temp_control is V6VvarTextBox)
                {
                    var txt = temp_control as V6VvarTextBox;
                    var init_filter = V6Login.GetInitFilter("ALKH", V6ControlFormHelper.FindFilterType(this));
                    var init_filter_user = "[Status] <> '0'";
                    if (!string.IsNullOrEmpty(init_filter_user))
                    {
                        init_filter += (string.IsNullOrEmpty(init_filter) ? "" : " and ") + init_filter_user;
                    }
                    txt.SetInitFilter(init_filter);
                }
                
                //MA_KH_I
                temp_control = V6ControlFormHelper.GetControlByAccessibleName(this, "MA_KH_I");
                if (temp_control is V6VvarTextBox)
                {
                    var txt = temp_control as V6VvarTextBox;
                    var init_filter = V6Login.GetInitFilter("ALKH", V6ControlFormHelper.FindFilterType(this));
                    var init_filter_user = "[Status] <> '0'";
                    if (!string.IsNullOrEmpty(init_filter_user))
                    {
                        init_filter += (string.IsNullOrEmpty(init_filter) ? "" : " and ") + init_filter_user;
                    }
                    txt.SetInitFilter(init_filter);
                }

                //MA_VT
                temp_control = V6ControlFormHelper.GetControlByAccessibleName(this, "MA_VT");
                if (temp_control is V6VvarTextBox)
                {
                    var txt = temp_control as V6VvarTextBox;
                    var init_filter = V6Login.GetInitFilter("ALVT", V6ControlFormHelper.FindFilterType(this));
                    var init_filter_extra = "[Status] <> '0'";
                    if (!string.IsNullOrEmpty(init_filter_extra))
                    {
                        init_filter += (string.IsNullOrEmpty(init_filter) ? "" : " and ") + init_filter_extra;
                    }
                    txt.SetInitFilter(init_filter);
                }

                //MA_SONB
                temp_control = V6ControlFormHelper.GetControlByAccessibleName(this, "MA_SONB");
                if (temp_control is V6VvarTextBox)
                {
                    var txt = temp_control as V6VvarTextBox;
                    var old_filter = txt.InitFilter;
                    var adv_filter = V6Login.GetInitFilter("ALSONB", V6ControlFormHelper.FindFilterType(this));
                    var adv_filter_extra = "[Status] <> '0'";
                    if (!string.IsNullOrEmpty(adv_filter_extra))
                    {
                        adv_filter += (string.IsNullOrEmpty(adv_filter) ? "" : " and ") + adv_filter_extra;
                    }
                    var new_filter = old_filter;
                    if (string.IsNullOrEmpty(new_filter))
                    {
                        new_filter = adv_filter;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(adv_filter))
                        {
                            new_filter = string.Format("({0}) and ({1})", old_filter, adv_filter);
                        }
                    }
                    txt.SetInitFilter(new_filter);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetInitFilterAll " + _sttRec, ex);
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
                this.WriteExLog(GetType() + ".ShowParentMessage " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Tính số lượng quy đổi.
        /// </summary>
        /// <param name="_soLuong1">Số lượng nhập theo DVT đang chọn.</param>
        /// <param name="_sl_qd">Quy đổi ra sl theo DVT quy đổi (sẽ được tính toán ra).</param>
        /// <param name="_sl_qd2"></param>
        /// <param name="_hs_qd1">Hệ số nhân đổi DVT ra DVT qd. (1 viên = ?0.01 thùng)</param>
        /// <param name="_hs_qd2">100 viên / thùng</param>
        //protected void TinhSoluongQuyDoi(V6NumberTextBox _soLuong1
        //    , V6NumberTextBox _sl_qd, V6NumberTextBox _sl_qd2
        //    , V6NumberTextBox _hs_qd1, V6NumberTextBox _hs_qd2)
        //{
        //    try
        //    {
        //        if (M_CAL_SL_QD_ALL == "0")
        //        {
        //            //Phần nguyên, (ví dụ 1.5 thùng)
        //            _sl_qd.Value = _soLuong1.Value*_hs_qd1.Value;
        //            //Phần lẻ (ví dụ 50 viên = 0.5 thùng bên trên)
        //            var tong = _sl_qd.Value*_hs_qd2.Value;
        //            var sl_nguyen_thung = ((int) _sl_qd.Value)*_hs_qd2.Value;
        //            _sl_qd2.Value = V6BusinessHelper.Vround(tong - sl_nguyen_thung, 1);
        //        }
        //        else if (M_CAL_SL_QD_ALL == "1")
        //        {
        //            _soLuong1.Value = _sl_qd.Value*_hs_qd1.Value;
        //        }
        //        else
        //        {
        //            //this.ShowParentMessage();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi " + _sttRec, ex);
        //    }
        //}

        protected void TinhSoluongQuyDoi_0(V6NumberTextBox _soLuong1
            , V6NumberTextBox _sl_qd, V6NumberTextBox _sl_qd2
            , V6NumberTextBox _hs_qd1, V6NumberTextBox _hs_qd2)
        {
            try
            {
                if (M_CAL_SL_QD_ALL == "0")
                {
                    //Phần nguyên, (ví dụ 1.5 thùng)
                    _sl_qd.Value = _soLuong1.Value*_hs_qd1.Value;
                    //Phần lẻ (ví dụ 50 viên = 0.5 thùng bên trên)
                    var tong = _sl_qd.Value*_hs_qd2.Value;
                    var sl_nguyen_thung = ((int) _sl_qd.Value)*_hs_qd2.Value;
                    _sl_qd2.Value = V6BusinessHelper.Vround(tong - sl_nguyen_thung, 1);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi_0 " + _sttRec, ex);
            }
        }
        protected void TinhSoluongQuyDoi_1(V6NumberTextBox _soLuong1
            , V6NumberTextBox _sl_qd, V6NumberTextBox _sl_qd2
            , V6NumberTextBox _hs_qd1, V6NumberTextBox _hs_qd2)
        {
            try
            {
                if (M_CAL_SL_QD_ALL == "1")
                {
                    _soLuong1.Value = _sl_qd.Value*_hs_qd1.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi_1 " + _sttRec, ex);
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
                this.ShowErrorException(GetType() + ".TinhChietKhauChiTiet " + _sttRec, ex);
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

        /// <summary>
        /// Tính và gán vào control
        /// </summary>
        /// <param name="thueSuat"></param>
        /// <param name="tienNt"></param>
        /// <param name="tien"></param>
        /// <param name="txtTienThueNt">control thue_nt</param>
        /// <param name="txtTienThue">control thue</param>
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


        private void V6InvoiceControl_Load(object sender, EventArgs e)
        {
            SetInitFilterAll();
            LoadLanguage();
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

                ShowMainMessage(message);
                dateNgayCT.Focus();
                return false;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ValidateNgayCt " + _sttRec, ex);
            }
            return false;
        }

        protected bool ValidateMasterData(V6InvoiceBase Invoice)
        {
            var v6validConfig = V6ControlsHelper.GetV6ValidConfig(Invoice.Mact, 1);
            
            if (v6validConfig != null && v6validConfig.HaveInfo)
            {
                var a_fields = v6validConfig.A_field.Split(',');
                foreach (string field in a_fields)
                {
                    var control = V6ControlFormHelper.GetControlByAccessibleName(this, field);
                    if (control is V6DateTimeColor)
                    {
                        if (((V6DateTimeColor)control).Value == null)
                        {
                            this.ShowWarningMessage("Chưa nhập giá trị: " + field);
                            control.Focus();
                            return false;
                        }
                    }
                    else if (control is V6NumberTextBox)
                    {
                        if (((V6NumberTextBox)control).Value == 0)
                        {
                            this.ShowWarningMessage("Chưa nhập giá trị: " + field);
                            control.Focus();
                            return false;
                        }
                    }
                    else if (control is TextBox)
                    {
                        if (string.IsNullOrEmpty(control.Text))
                        {
                            this.ShowWarningMessage("Chưa nhập giá trị: " + field);
                            control.Focus();
                            return false;
                        }
                    }
                }
            }
            else
            {
                V6ControlFormHelper.SetStatusText("No V6Valid info!");
            }
            return true;
        }
        
        /// <summary>
        /// <para>Kiểm tra dữ liệu chi tiết hợp lệ quy định trong V6Valid.</para>
        /// <para>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</para>
        /// </summary>
        /// <param name="Invoice"></param>
        /// <param name="data"></param>
        /// <returns>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</returns>
        protected string ValidateDetailData(V6InvoiceBase Invoice, SortedDictionary<string, object> data)
        {
            string error = "";
            try
            {
                var config = V6ControlsHelper.GetV6ValidConfig(Invoice.Mact, 2);
                
                if (config != null && config.HaveInfo)
                {
                    var a_fields = ObjectAndString.SplitString(config.A_field);
                    foreach (string field in a_fields)
                    {
                        string FIELD = field.Trim().ToUpper();
                        if (!data.ContainsKey(FIELD))
                        {
                            //error += string.Format("{0}: [{1}]\n", V6Text.NoData, FIELD);
                            continue;
                        }

                        V6ColumnStruct columnS = Invoice.ADStruct[FIELD];
                        object value = data[FIELD];
                        if (ObjectAndString.IsDateTimeType(columnS.DataType))
                        {
                            if (value == null) error += "Chưa nhập giá trị: [" + FIELD + "]\n";
                        }
                        else if (ObjectAndString.IsNumberType(columnS.DataType))
                        {
                            if (ObjectAndString.ObjectToDecimal(value) == 0) error += "Chưa nhập giá trị: [" + FIELD + "]\n";
                        }
                        else // string
                        {
                            if (string.IsNullOrEmpty("" + value)) error += "Chưa nhập giá trị: [" + FIELD + "]\n";
                        }

                    }
                }
                else
                {
                    ShowMainMessage("No V6Valid info!");
                }
            }
            catch (Exception ex)
            {
                //error += ex.Message;//Lỗi chương trình không liên quan lỗi nhập liệu
                this.WriteExLog(GetType() + ".ValidateData_Detail " + _sttRec, ex);
            }
            return error;
        }

        public virtual void TinhTongThanhToan(string debug) { }

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
                    var fValue = ObjectAndString.ObjectToString(V6ControlFormHelper.GetFormValue(this, item.Name));
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

        /// <summary>
        /// Tính lại fieldTien theo NT có làm tròn.
        /// </summary>
        /// <param name="detailData">Bảng dữ liệu chi tiết.</param>
        /// <param name="row">Dòng dữ liệu cần thay đổi.</param>
        /// <param name="ty_gia"></param>
        /// <param name="fieldTien"></param>
        /// <param name="fieldTienNt"></param>
        /// <param name="round">Làm tròn</param>
        private void FixTyGia(DataTable detailData, DataRow row, decimal ty_gia, string fieldTien, string fieldTienNt, int round)
        {
            if (detailData.Columns.Contains(fieldTien) && detailData.Columns.Contains(fieldTienNt))
            {
                decimal temp = ObjectAndString.ObjectToDecimal(row[fieldTienNt]);
                if (temp != 0)
                    row[fieldTien] = V6BusinessHelper.Vround(temp * ty_gia, round);
            }
        }
        private void FixTyGiaDetail(DataTable detailData, HD_Detail detailControl, decimal ty_gia, string fieldTien, string fieldTienNt, int round)
        {
            if (detailData.Columns.Contains(fieldTien) && detailData.Columns.Contains(fieldTienNt))
            {
                Control control = detailControl.GetControlByAccessibleName(fieldTien);
                Control controlNT = detailControl.GetControlByAccessibleName(fieldTienNt);
                
                if (control != null && controlNT != null)
                {
                    decimal valueNT = ObjectAndString.ObjectToDecimal(V6ControlFormHelper.GetControlValue(controlNT));
                    if (valueNT != 0)
                    {
                        V6ControlFormHelper.SetControlValue(control, V6BusinessHelper.Vround(valueNT*ty_gia, round));
                    }
                }
            }
        }
        protected void XuLyThayDoiTyGia(V6NumberTextBox txtTyGia, CheckBox chkSuaTien)
        {
            try
            {
                var ty_gia = txtTyGia.Value;

                // Tuanmh 25/05/2017
                if (ty_gia == 0 || chkSuaTien.Checked) return;

                {
                    foreach (DataRow row in AD.Rows)
                    {
                        FixTyGia(AD, row, ty_gia, "Tien", "Tien_nt", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "Tien2", "Tien_nt2", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "Tien1", "Tien1_nt", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "Tien_vc", "Tien_vc_nt", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "Tien0", "TIEN_NT0", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "Thue", "Thue_nt", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "CP", "CP_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "GIA", "GIA_NT", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia, "GIA01", "GIA_NT01", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia, "GIA1", "GIA_NT1", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia, "GIA2", "GIA_NT2", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia, "GIA21", "GIA_NT21", M_ROUND_GIA);

                        FixTyGia(AD, row, ty_gia, "GIA0", "GIA_NT0", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia, "NK", "NK_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "CK", "CK_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "GG", "GG_NT", M_ROUND);

                        FixTyGia(AD, row, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                    HD_Detail detailControl = this.GetControlByName("detail1") as HD_Detail;
                    if (detailControl != null && (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        //...
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Tien", "Tien_nt", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Tien2", "Tien_nt2", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Tien1", "Tien1_nt", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Tien_vc", "Tien_vc_nt", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Tien0", "TIEN_NT0", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Thue", "Thue_nt", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "CP", "CP_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GIA", "GIA_NT", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GIA01", "GIA_NT01", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GIA1", "GIA_NT1", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GIA2", "GIA_NT2", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GIA21", "GIA_NT21", M_ROUND_GIA);

                        FixTyGiaDetail(AD, detailControl, ty_gia, "GIA0", "GIA_NT0", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "NK", "NK_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "CK", "CK_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GG", "GG_NT", M_ROUND);

                        FixTyGiaDetail(AD, detailControl, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                }

                if (AD2 != null)
                {
                    foreach (DataRow row in AD2.Rows)
                    {
                        FixTyGia(AD2, row, ty_gia, "t_tien", "t_tien_nt", M_ROUND);
                        FixTyGia(AD2, row, ty_gia, "t_thue", "t_thue_nt", M_ROUND);
                        FixTyGia(AD2, row, ty_gia, "t_tt", "t_tt_nt", M_ROUND);
                    }
                    HD_Detail detailControl = this.GetControlByName("detail2") as HD_Detail;
                    if (detailControl != null && (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        FixTyGiaDetail(AD2, detailControl, ty_gia, "t_tien", "t_tien_nt", M_ROUND);
                        FixTyGiaDetail(AD2, detailControl, ty_gia, "t_thue", "t_thue_nt", M_ROUND);
                        FixTyGiaDetail(AD2, detailControl, ty_gia, "t_tt", "t_tt_nt", M_ROUND);
                    }
                }

                if (AD3 != null)
                {
                    foreach (DataRow row in AD3.Rows)
                    {
                        FixTyGia(AD3, row, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGia(AD3, row, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                    HD_Detail detailControl = this.GetControlByName("detail3") as HD_Detail;
                    if (detailControl != null && (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        FixTyGiaDetail(AD3, detailControl, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGiaDetail(AD3, detailControl, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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

                var M_CHECK_SAVE_STOCK = V6Options.GetValue("M_CHECK_SAVE_STOCK");
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
                this.WriteExLog(GetType() + "ValidateData_Master_CheckTon " + _sttRec, ex);
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
                else
                {
                    //tuanmh 03/11/2017
                    continue;
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
                //message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Vitri:{5}  Tồn:{0}  Xuất:{1}\n",
                //            0, item.Value, c_makho, c_mavt, c_malo, c_mavitri);
                message = "";

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
                else
                {
                    //tuanmh 03/11/2017
                    continue;
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

                if (data == null || data.Rows.Count == 0)
                {
                    message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                                0, item.Value, c_makho, c_mavt, c_malo);
                }
                else
                {
                    bool found = false;
                    foreach (DataRow row in data.Rows)
                    {
                        var data_mavt = row["Ma_vt"].ToString().Trim();
                        var data_makho = row["Ma_kho"].ToString().Trim();
                        var data_malo = row["Ma_lo"].ToString().Trim();
                        var data_soluong = ObjectAndString.ObjectToDecimal(row["Ton_dau"]);
                        if (c_mavt == data_mavt && c_makho == data_makho && c_malo == data_malo)
                        {
                            found = true;
                            if (data_soluong < item.Value)
                            {
                                message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                                    data_soluong, item.Value, c_makho, c_mavt, c_malo);
                            }

                            goto NextItem;
                        }
                    }

                    if (!found)
                    {
                        message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                                0, item.Value, c_makho, c_mavt, c_malo);
                    }
                }
                //message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                //                0, item.Value, c_makho, c_mavt, c_malo);
                

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

            // Loi 03/11/2017
            if (string.IsNullOrEmpty(mavt_in) || string.IsNullOrEmpty(makho_in))
            {
                return null;
            }

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
                    //
                    if (data_soluong != 0 && c_mavt == data_mavt && c_makho == data_makho)
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
                //message += string.Format("Kho:{2}  Vật tư:{3}  Tồn:{0}  Xuất:{1}\n",
                //                0, item.Value, c_makho, c_mavt);
                message = "";
            NextItem:
                DoNothing();
            }
            return message;
            #endregion makho, mavt
        }
        #endregion CheckTon

        /// <summary>
        /// Biến tạm: stt_rec để in sau khi lưu thành công.
        /// </summary>
        protected string _sttRec_In = "";
        protected V6PrintMode _print_flag = V6PrintMode.DoNoThing;
        protected void BasePrint(V6InvoiceBase Invoice, string sttRec_In, V6PrintMode printMode,
            decimal tongThanhToan_Value, decimal tongThanhToanNT_Value, bool closeAfterPrint, int sec = 3)
        {
            try
            {
                bool check_print = CheckPrint(Invoice);
                if (!check_print)
                {
                    return;
                }

                if (IsViewingAnInvoice)
                {
                    if (V6Login.UserRight.AllowPrint("", Invoice.CodeMact))
                    {
                        var program = Invoice.PrintReportProcedure;
                        var repFile = Invoice.Alct["FORM"].ToString().Trim();
                        var repTitle = Invoice.Alct["TIEU_DE_CT"].ToString().Trim();
                        var repTitle2 = Invoice.Alct["TIEU_DE2"].ToString().Trim();

                        var c = new InChungTuViewBase(Invoice, program, program, repFile, repTitle, repTitle2,
                            "", "", "", sttRec_In);
                        c.TTT = tongThanhToan_Value;
                        c.TTT_NT = tongThanhToanNT_Value;
                        c.MA_NT = _maNt;
                        c.Dock = DockStyle.Fill;
                        c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                        {
                            if (hoadon_nd51 > 0) Invoice.IncreaseSl_inAM(stt_rec, AM_current);
                            if (!sender.IsDisposed) sender.Dispose();
                        };
                        c.PrintMode = printMode;
                        c.Close_after_print = closeAfterPrint;
                        c.ShowToForm(this, Invoice.PrintTitle, true);
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Kiểm tra được phép in hay không.
        /// </summary>
        /// <param name="Invoice"></param>
        /// <returns></returns>
        protected bool CheckPrint(V6InvoiceBase Invoice)
        {
            bool check_print = true;
            try
            {
                var amRow = AM.Rows[CurrentIndex];
                DataTable DataCheck_Edit_All = Invoice.GetCheck_Print_All(amRow["KIEU_POST"].ToString().Trim(),
                    amRow["KIEU_POST"].ToString().Trim(),
                    amRow["SO_CT"].ToString().Trim(), _sttRec, ObjectAndString.ObjectToFullDateTime(amRow["NGAY_CT"]),
                    Invoice.Mact, V6Login.UserId);

                if (DataCheck_Edit_All != null && DataCheck_Edit_All.Rows.Count > 0)
                {
                    var chksave_all = DataCheck_Edit_All.Rows[0]["chksave_all"].ToString();
                    var chk_yn = DataCheck_Edit_All.Rows[0]["chk_yn"].ToString();
                    var mess = DataCheck_Edit_All.Rows[0]["mess"].ToString().Trim();
                    var mess2 = DataCheck_Edit_All.Rows[0]["mess2"].ToString().Trim();
                    var message = V6Setting.IsVietnamese ? mess : mess2;

                    switch (chksave_all)
                    {
                        case "00":
                        case "04":
                            // Save: OK --Loai_kh in ALKH
                            // Save: OK --Thau
                            break;
                        case "01":
                        case "02":
                        case "03":

                            if (message != "") this.ShowWarningMessage(message);
                            if (chk_yn == "0")
                            {
                                check_print = false;
                            }
                            break;

                        case "06":
                        case "07":
                        case "08":
                            // Save but mess
                            if (message != "") this.ShowWarningMessage(message);
                            check_print = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CheckPrint " + _sttRec, ex);
            }
            return check_print;
        }

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
                this.WriteExLog(GetType() + ".XemPhieuNhapView " + _sttRec, ex);
            }
        }

        protected void ResetTonLoHsd(V6NumberTextBox _ton13, V6VvarTextBox _maLo, V6DateTimeColor _hanSd)
        {
            _ton13.Value = 0;
            _maLo.Clear();
            _hanSd.Value = null;
        }

        protected string GetCA()
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@dS", V6BusinessHelper.GetServerDateTime()),
            };
            var ca = V6BusinessHelper.ExecuteFunctionScalar("VFA_GET_CA_FROM_ALTD", plist);
            return "" + ca;
        }

        /// <summary>
        /// No_use
        /// </summary>
        protected Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        protected Type Event_program;
        protected Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        protected void CreateFormProgram(V6InvoiceBase Invoice)
        {
            try
            {


                //DMETHOD
                if (!Invoice.Alct1.Columns.Contains("DMETHOD"))
                {
                    this.ShowWarningMessage("No column name [DMETHOD] in [Alct1]");
                    return;
                }

                string using_text = "";
                string method_text = "";
                foreach (DataRow dataRow in Invoice.Alct1.Rows)
                {
                    var xml = dataRow["DMETHOD"].ToString().Trim();
                    if (xml == "") continue;
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xml));
                    if (ds.Tables.Count <= 0) continue;
                    var data = ds.Tables[0];
                    foreach (DataRow event_row in data.Rows)
                    {
                        var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                        var method_name = event_row["method"].ToString().Trim();
                        Event_Methods[EVENT_NAME] = method_name;

                        using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                        method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                    }
                }
                //MMETHOD
                if (Invoice.Alct.Table.Columns.Contains("MMETHOD"))
                {
                    var xml = Invoice.Alct["MMETHOD"].ToString().Trim();
                    if (xml == "") goto Build;
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xml));
                    if (ds.Tables.Count <= 0) goto Build;
                    var data = ds.Tables[0];
                    foreach (DataRow event_row in data.Rows)
                    {
                        var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                        var method_name = event_row["method"].ToString().Trim();
                        Event_Methods[EVENT_NAME] = method_name;

                        using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                        method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                    }
                }

            Build:
                Event_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + Invoice.Mact, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0 " + _sttRec, ex);
            }
        }

        /// <summary>
        /// <para>Gọi hàm viết code động theo Event được định nghĩa trước.</para>
        /// <para>Hàm không quăng lỗi, lỗi được ghi log.</para>
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        protected object InvokeFormEvent(string eventName)
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(eventName))
                {
                    var method_name = Event_Methods[eventName];
                    return V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
            }
            return null;
        }


        protected void InvokeFormEventFixCopyData()
        {
            try
            {
                All_Objects["AD"] = AD;
                All_Objects["AD2"] = AD2;
                All_Objects["AD3"] = AD3;
                InvokeFormEvent(FormDynamicEvent.FIXCOPYDATA);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".InvokeFormEventFixCopyData " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Chức năng sửa hàng loạt một cột dữ liệu.
        /// </summary>
        /// <param name="invoice"></param>
        protected void ChucNang_ThayThe(V6InvoiceBase invoice)
        {
            try
            {
                //Hien form chuc nang co options *-1 or input
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    var detail1 = GetControlByName("detail1") as HD_Detail;
                    if (detail1 == null)
                    {
                        ShowParentMessage("Không xác định được control: [detail1].");
                        return;
                    }

                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        this.ShowWarningMessage(V6Text.DetailNotComplete);
                    }
                    else
                    {
                        var dataGridView1 = GetControlByName("dataGridView1") as DataGridView;
                        if (dataGridView1 == null)
                        {
                            ShowParentMessage("Không xác định được control: [dataGridView1].");
                            return;
                        }

                        int field_index = dataGridView1.CurrentCell.ColumnIndex;
                        string FIELD = dataGridView1.CurrentCell.OwningColumn.DataPropertyName.ToUpper();
                        V6ColorTextBox textBox = detail1.GetControlByAccessibleName(FIELD) as V6ColorTextBox;
                        Type valueType = dataGridView1.CurrentCell.OwningColumn.ValueType;

                        //Check
                        if (textBox == null)
                        {
                            ShowParentMessage("Không xác định được control.");
                            return;
                        }
                        
                        ChucNangThayTheForm f = new ChucNangThayTheForm(ObjectAndString.IsNumberType(dataGridView1.CurrentCell.OwningColumn.ValueType), textBox);
                        if (f.ShowDialog(this) == DialogResult.OK)
                        {
                            if (f.ChucNangDaChon == f._ThayThe)
                            {
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    object newValue = ObjectAndString.ObjectTo(valueType, f.Value);
                                    if (ObjectAndString.IsDateTimeType(valueType) && newValue != null)
                                    {
                                        DateTime newDate = (DateTime) newValue;
                                        if (newDate < new DateTime(1700, 1, 1))
                                        {
                                            newValue = null;
                                        }
                                    }

                                    SortedDictionary<string, object> newData = new SortedDictionary<string, object>();
                                    newData.Add(FIELD, newValue);
                                    V6ControlFormHelper.UpdateGridViewRow(row, newData);
                                }
                            }
                            else // Đảo ngược
                            {
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    var newValue = ObjectAndString.ObjectToDecimal(row.Cells[field_index].Value) * -1;
                                    SortedDictionary<string, object> newData = new SortedDictionary<string, object>();
                                    newData.Add(FIELD, newValue);
                                    V6ControlFormHelper.UpdateGridViewRow(row, newData);
                                }
                            }

                            All_Objects["replaceField"] = FIELD;
                            All_Objects["dataGridView1"] = dataGridView1;
                            All_Objects["detail1"] = detail1;
                            if (Event_Methods.ContainsKey(FormDynamicEvent.AFTERREPLACE))
                            {
                                InvokeFormEvent(FormDynamicEvent.AFTERREPLACE);
                            }
                            else
                            {
                                AfterReplace(All_Objects);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChucNang_ThayThe " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Chức năng sửa dữ liệu AD bằng cách gõ trực tiếp.
        /// </summary>
        /// <param name="invoice"></param>
        protected void ChucNang_SuaNhieuDong(V6InvoiceBase invoice)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                string adFields = invoice.EXTRA_INFOR.ContainsKey("ADFIELDS") ? invoice.EXTRA_INFOR["ADFIELDS"] : "";
                //V6ControlFormHelper.ShowDataEditorForm(AD, invoice.Mact + "_REPLACE", adFields, null, false, false, true, false);
                string tableName = invoice.Mact + "_REPLACE";
                var f = new DataEditorForm(AD, tableName, adFields, null, V6Text.Edit + " " + V6TableHelper.V6TableCaption(tableName, V6Setting.Language), false, false, true, false);
                All_Objects["dataGridView"] = f.DataGridView;
                InvokeFormEvent(FormDynamicEvent.SUANHIEUDONG);
                f.ShowDialog(this);
            }
        }

        protected void AfterReplace(Dictionary<string, object> allObjects)
        {
            try
            {
                var dataGridView1 = allObjects["dataGridView1"] as DataGridView;
                if (dataGridView1 == null)
                {
                    this.ShowWarningMessage(GetType() + ".AfterReplace dataGridView1 null", 500);
                    return;
                }
                var replaceField = allObjects["replaceField"] as string;
                //var detail1 = allObjects["detail1"] as HD_Detail;

                if (replaceField == "MA_VT" && dataGridView1.Columns.Contains("TEN_VT"))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var txt = new V6VvarTextBox();
                        txt.VVar = "MA_VT";
                        txt.Text = row.Cells["MA_VT"].Value.ToString();
                        if (txt.Data != null)
                        {
                            var ten_vt = txt.Data["TEN_VT"].ToString().Trim();
                            row.Cells["TEN_VT"].Value = ten_vt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AfterReplace " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Gán lại giá trị mặc định cho một số trường định nghĩa trong ALCT.M_MA_HD
        /// </summary>
        /// <param name="invoice"></param>
        protected void ResetAMADbyConfig(V6InvoiceBase invoice)
        {
            var m_ma_hd = ("" + invoice.Alct["M_MA_HD"]).Trim();
            if (m_ma_hd == "") return;

            var sss = ObjectAndString.SplitString(m_ma_hd);
            foreach (string s in sss)
            {
                var ss = s.Split(':');
                if (ss.Length > 1)
                {
                    var fields = ObjectAndString.SplitString(ss[1]);
                    if (ss[0].ToUpper() == "AM")
                    {
                        foreach (string field in fields)
                        {
                            string FIELD = field.Trim().ToUpper();
                            if (invoice.AMStruct.ContainsKey(FIELD))
                            {
                                Control c = GetControlByAccessibleName(FIELD);
                                if (c != null) V6ControlFormHelper.SetControlValue(c, null);
                            }
                        }
                    }
                    else if (ss[0].ToUpper() == "AD")
                    {
                        foreach (string field in fields)
                        {
                            string FIELD = field.Trim().ToUpper();

                            if (invoice.ADStruct.ContainsKey(FIELD) && AD.Columns.Contains(FIELD))
                            {
                                object resetValue = null;
                                V6ColumnStruct struct0 = invoice.ADStruct[FIELD];
                                if (struct0.AllowNull) resetValue = DBNull.Value;
                                else
                                {
                                    switch (struct0.sql_data_type_string)
                                    {
                                        case "date":
                                        case "smalldatetime":
                                        case "datetime":
                                            resetValue = V6Setting.M_SV_DATE;
                                            break;
                                        case "bit":
                                            resetValue = false;
                                            break;
                                        case "bigint":
                                        case "numeric":
                                        case "smallint":
                                        case "decimal":
                                        case "smallmoney":
                                        case "int":
                                        case "tinyint":
                                        case "money":
                                            resetValue = 0;
                                            break;

                                        default:
                                            resetValue = "";
                                            break;
                                    }
                                }
                                foreach (DataRow dataRow in AD.Rows)
                                {
                                    dataRow[field] = resetValue;
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
