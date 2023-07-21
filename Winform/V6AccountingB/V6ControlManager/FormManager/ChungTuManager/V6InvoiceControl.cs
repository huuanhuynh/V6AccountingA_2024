using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using GSM;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6ControlManager.FormManager.SoDuManager;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6ThuePostManager;
using V6Tools;
using V6Tools.V6Convert;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.BaoGia;

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
            _invoice = V6InvoiceBase.GetInvoiceBase(maCt);
            //MaCt = maCt;
            InitializeComponent();
            MyInit0();
        }
        
        public V6InvoiceControl(V6InvoiceBase invoice, string itemId)
        {
            _invoice = invoice;
            //MaCt = maCt;
            InitializeComponent();
            MyInit0();
        }

        private void MyInit0()
        {
            try
            {
                M_ROUND_NUM = V6Setting.RoundNum;
                M_ROUND = V6Setting.RoundTien;
                M_ROUND_NT = V6Setting.RoundTienNt;
                M_ROUND_SL = V6Setting.RoundSL;
                M_ROUND_GIA = V6Setting.RoundGia;
                M_ROUND_GIA_NT = V6Setting.RoundGiaNt;
                M_SOA_HT_KM_CK = V6Options.GetValue("M_SOA_HT_KM_CK");
                M_SOA_MULTI_VAT = V6Options.GetValue("M_SOA_MULTI_VAT");
                M_POA_MULTI_VAT = V6Options.GetValue("M_POA_MULTI_VAT");
                M_CAL_SL_QD_ALL = V6Options.GetValue("M_CAL_SL_QD_ALL");
                M_TYPE_SL_QD_ALL = V6Options.GetValue("M_TYPE_SL_QD_ALL");
                //var lbl = new Label();
                //lbl.Text = MaCt;
                //Controls.Add(lbl);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit0 " + _sttRec, ex);
            }
        }

        public void LoadAdvanceControls(string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(this, ma_ct, All_Objects);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
        }

        //public delegate void ChangeInvoice(string sttRec);
        public event Action<string> InvoiceChanged;
        /// <summary>
        /// Sự kiện để liên kết leftPanel.
        /// </summary>
        /// <param name="sttRec"></param>
        public virtual void OnInvoiceChanged(string sttRec)
        {
            CheckVvarTextBox();

            var handler = InvoiceChanged;
            if (handler != null) handler(sttRec);
        }

        public event Action<DataTable> AmChanged;
        public virtual void OnAmChanged(DataTable data)
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
        public V6InvoiceBase _invoice;
        /// <summary>
        /// Bảng dữ liệu chứng từ.
        /// </summary>
        public DataTable AM { get { return _am; } set { _am = value;  OnAmChanged(value);} }
        private DataTable _am;
        /// <summary>
        /// Dữ liệu AM hiện tại.
        /// </summary>
        public DataRow AM_current
        {
            get
            {
                if (_am == null || CurrentIndex < 0 || CurrentIndex >= _am.Rows.Count) return null;
                return _am.Rows[CurrentIndex];
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

        public ChungTuChungContainer ChungTuContainer { get; set; }
        public TabPage ParentTabPage { get; set; }
        /// <summary>
        /// Cờ thêm khi nhấn Shift trong chức năng chọn.
        /// </summary>
        public bool chon_accept_flag_add;
        /// <summary>
        /// Cờ kiểm soát lỗi, nếu có lỗi thì không được chạy tiếp chức năng khác. if (_fail) {show_message; return;}
        /// </summary>
        public bool _fail;
        //public string MaCt { get; set; }
        public string _sttRec0 = "";
        public string _sttRec02 = "";
        public string _sttRec03 = "";
        public string _mMaNt0 = "";
        public string _maNt = "VND";
        public bool co_chon_don_hang;
        public string _mavt_default_initfilter;
        /// <summary>
        /// Tên loại phiếu được chọn làm căn cứ viết code động.
        /// </summary>
        public string _chon_px = "";
        public string MA_NT { get { return _maNt; } }

        public int M_ROUND_NUM;
        public int M_ROUND;
        public int M_ROUND_NT;
        public int M_ROUND_SL;
        public int M_ROUND_GIA;
        public int M_ROUND_GIA_NT;
        public string M_SOA_HT_KM_CK;
        public string M_SOA_MULTI_VAT;
        public string M_POA_MULTI_VAT;
        /// <summary>
        /// <para>0:SL_QD=SO_LUONG1*HS_QD1(thùng):SL_QD2=Số lẻ(viên)</para>
        /// <para>1:SO_LUONG1=SL_QD*HS_QD1</para>
        /// <para>2:SL_QD=SO_LUONG1/HS_QD1(thùng)</para>
        /// </summary>
        public string M_CAL_SL_QD_ALL;
        /// <summary>
        /// <para>0I-> Nhập SLQD</para>
        /// <para>00:(M_CAL_SL_QD_ALL=0)</para>
        /// <para>1I-> Nhập So_luong1</para>
        /// <para>10(M_CAL_SL_QD_ALL=1)</para>
        /// <para>2I->Nhập SLQD</para>
        /// <para>20(M_CAL_SL_QD_ALL=2)</para>
        /// </summary>
        public string M_TYPE_SL_QD_ALL = null;

        /// <summary>
        /// List các control trong detail
        /// </summary>
        public Dictionary<string, AlctControls> detailControlList1 = new Dictionary<string, AlctControls>();
        public Dictionary<string, AlctControls> detailControlList2 = new Dictionary<string, AlctControls>();
        public Dictionary<string, AlctControls> detailControlList3 = new Dictionary<string, AlctControls>();
        /// <summary>
        /// List thứ tự field chi tiết.
        /// </summary>
        public List<string> _orderList = new List<string>();
        public List<string> _orderList2 = new List<string>();
        public List<string> _orderList3 = new List<string>();
        public List<string> _carryFields = new List<string>();
        public IDictionary<string, object> _carryRowData = new SortedDictionary<string, object>();

        public SortedDictionary<string, DataRow> _alct1Dic;
        public SortedDictionary<string, DataRow> _alct2Dic;
        public SortedDictionary<string, DataRow> _alct3Dic;

        public DataGridViewRow _gv1EditingRow;
        public DataGridViewRow _gv2EditingRow;
        public DataGridViewRow _gv3EditingRow;

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
        public bool IsAddOrEdit { get { return Mode == V6Mode.Add || Mode == V6Mode.Edit; } }
        /// <summary>
        /// Không phải đang sửa hoặc thêm mới chứng từ.
        /// </summary>
        public bool NotAddEdit
        {
            get
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return true;
                return false;
            }
        }

        public event EventHandler BillChanged;
        /// <summary>
        /// Gây ra sự kiện để cập nhập thông tin lên button
        /// </summary>
        public virtual void OnBillChanged()
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
        public virtual void OnChangeTable(ChangeTableEventArgs e)
        {
            var handler = ChangeTable;
            if (handler != null) handler(this, e);
        }

        public event EventHandler ViewNext;
        public virtual void OnViewNext()
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

        //private bool _AED_Finish;
        /// <summary>
        /// Add-Edit-Delete is running.
        /// </summary>
        public bool _AED_Running;
        /// <summary>
        /// Add-Edit-Delete successfull.
        /// </summary>
        public bool _AED_Success;
        public bool IsRunning
        {
            get { return _AED_Running || _executing; }
        }

        public bool ClickSuaOnLoad { get; set; }

        public virtual void ApGiaBan(bool auto = false)
        {
            ShowMainMessage("Cần override hàm ApGiaBan().");
        }
        public virtual void ApGiaMua(bool auto = false)
        {
            ShowMainMessage("Cần override hàm ApGiaMua().");
        }
        
        public void HuyBase()
        {
            if (IsRunning)
            {
                ShowMainMessage(V6Text.ProcessNotComplete);
                return;
            }
            Huy();
        }

        public virtual void Huy()
        {
            ShowMainMessage("Cần override hàm Huy().");
        }

        /// <summary>
        /// Goi cac ham enable khac (functionButton, navi, form)
        /// </summary>
        public void EnableControls()
        {
            EnableNavigationButtons();
            EnableFunctionButtons();
            EnableVisibleControls();
        }

        public virtual void EnableNavigationButtons()
        {
            throw new System.NotImplementedException("EnableNavigationButtons Cần override.");
        }

        public virtual void EnableFunctionButtons()
        {
            throw new System.NotImplementedException("EnableFunctionButtons Cần override.");
        }

        /// <summary>
        /// Ẩn hiện, set Readonly cho các control.
        /// </summary>
        public virtual void EnableVisibleControls()
        {
            throw new System.NotImplementedException("EnableVisibleControls Cần override.");
        }

        /// <summary>
        /// Khóa control theo quyền, trả về list name hoặc Aname của readonly để xử lý tác vụ khác.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="invoice"></param>
        /// <param name="mode">Mode chính, đang thêm mới hay sửa chứng từ.</param>
        /// <param name="modeDetail">Mode chi tiết, đang thêm mới chi tiết hay sửa.</param>
        /// <returns></returns>
        public List<string> SetControlReadOnlyHide(Control container, V6InvoiceBase invoice, V6Mode mode, V6Mode modeDetail)
        {
            if (AM == null) return null;
            List<string> add_readonly = new List<string>();
            List<string> edit_readonly = new List<string>();
            try // try 1
            {
                int sl_in = 0;
                if(Mode == V6Mode.Edit)
                    if(AM.Columns.Contains("SL_IN")) sl_in = ObjectAndString.ObjectToInt(AM_current["SL_IN"]);

                // Khóa theo điều kiện dữ liệu (tùy chỉnh trong procedure VPA_SET_EDIT_READONLY_ALL
                try // try 2
                {
                    SqlParameter[] plist =
                        {
                            new SqlParameter("@Ma_ct", _invoice.Mact),
                            new SqlParameter("@STT_REC", _sttRec),
                            new SqlParameter("@Mode",  mode == V6Mode.Add ? "M" : mode == V6Mode.Edit ? "S" : "V"),
                            new SqlParameter("@ModeDetail", modeDetail == V6Mode.Add ? "M" : modeDetail == V6Mode.Edit ? "S" : modeDetail == V6Mode.Delete? "X" : "V"),
                            new SqlParameter("@User_id", V6Login.UserId),
                        };
                    var data = V6BusinessHelper.ExecuteProcedure("VPA_SET_EDIT_READONLY_ALL", plist).Tables[0];
                    if (container is HD_Detail)
                    {
                        string L_FIELD_N = "L_FIELDS" + container.Name.Right(1);    // L_FIELDS+1 với 1 là stt detail control.
                        if (data.Columns.Contains(L_FIELD_N) && data.Rows.Count > 0)
                        {
                            edit_readonly.AddRange(ObjectAndString.SplitString(data.Rows[0][L_FIELD_N].ToString()));
                        }
                    }
                    else
                    {
                        if (data.Rows.Count > 0)
                            edit_readonly.AddRange(ObjectAndString.SplitString(data.Rows[0]["L_FIELDS0"].ToString()));
                    }
                }
                catch (Exception ex2)
                {
                    this.WriteExLog(GetType() + ".SetControlReadOnlyHide ex2 " + _sttRec, ex2);
                }

                //  Ẩn hiện theo quyền trong Alctct
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

                if (container is HD_Detail)
                {
                    if (modeDetail == V6Mode.Add)
                    {
                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, edit_readonly, false);
                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, add_readonly, true);
                    }
                    else if (modeDetail == V6Mode.Edit)
                    {
                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, add_readonly, false);
                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, edit_readonly, true);
                    }
                }
                else
                {
                    if (mode == V6Mode.Add)
                    {
                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, edit_readonly, false);
                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, add_readonly, true);
                    }
                    else if (mode == V6Mode.Edit)
                    {
                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, add_readonly, false);
                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(container, edit_readonly, true);
                    }
                }

                V6ControlFormHelper.SetListControlVisibleByAccessibleNames(container, invoice.GRD_HIDE, false);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetControlReadOnlyHide", ex);
            }
            return mode == V6Mode.Edit ? edit_readonly : add_readonly;
        }

        public override void SetStatus2Text()
        {
            if (V6Setting.NotLoggedIn || M_TYPE_SL_QD_ALL == null) return;
            string id = "ST2" + _invoice.Mact;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = V6Text.Text("STATUS2" + _invoice.Mact);
            }
            V6ControlFormHelper.SetStatusText2(text, id);
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
        public bool CheckPhanBo(DataGridView grid, decimal total)
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

        protected bool _ctrl_T = false;
        public override bool DoHotKey0(Keys keyData)
        {
            //SupperAccess(keyData);
            if (_ctrl_T)
            {
                _ctrl_T = false;
                TabPage select_tab = null;
                int index = -1;
                if (keyData == Keys.D1)
                {
                    index = 0;
                }
                else if (keyData == Keys.D2)
                {
                    index = 1;
                }
                else if (keyData == Keys.D3)
                {
                    index = 2;
                }
                else if (keyData == Keys.D4)
                {
                    index = 3;
                }
                else if (keyData == Keys.D5)
                {
                    index = 4;
                }
                else if (keyData == Keys.D6)
                {
                    index = 5;
                }
                else if (keyData == Keys.D7)
                {
                    index = 6;
                }
                else if (keyData == Keys.D8)
                {
                    index = 7;
                }
                else if (keyData == Keys.D9)
                {
                    index = 8;
                }

                var tabControl1 = this.GetControlByName("tabControl1") as TabControl;
                if (tabControl1 != null && index >= 0 && tabControl1.TabPages.Count > index)
                {
                    tabControl1.SelectedTab = tabControl1.TabPages[index];
                }
                return true;
            }

            if (false)
            {
                ShowMainMessage("false");
            }
            else if (keyData == (Keys.Control | Keys.T))
            {
                _ctrl_T = true;
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }
        private string _supper_access = "";
        private void SupperAccess(Keys keyData)
        {
            switch (_supper_access.Length)
            {
                case 0:
                    if (keyData == Keys.V) { _supper_access += "v"; return; } break;
                case 1:
                    if (keyData == Keys.D6 || keyData == Keys.NumPad6) { _supper_access += "6"; return; } break;
                case 2:
                    if (keyData == Keys.S) { _supper_access += "s"; return; } break;
                case 3:
                    if (keyData == Keys.O) { _supper_access += "o"; return; } break;
                case 4:
                    if (keyData == Keys.F) { _supper_access += "f"; return; } break;
                case 5:
                    if (keyData == Keys.T)
                    {
                        _supper_access += "t";
                        ShowMainMessage(_supper_access);
                        //SupperAccess
                        if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                        {
                            ViewFormVar();
                        }
                    } break;
            }
            _supper_access = "";
        }

        /// <summary>
        /// Cần viết OnAmChanged(AM); cuối hàm để dùng LeftPanel
        /// </summary>
        /// <param name="sttrec"></param>
        /// <param name="mode"></param>
        public virtual void ViewInvoice(string sttrec, V6Mode mode)
        {
            throw new NotImplementedException();
        }

        public virtual void DisableAllFunctionButtons(Button btnLuu, Button btnMoi, Button btnCopy, Button btnIn, Button btnSua, Button btnHuy, Button btnXoa, Button btnXem, Button btnTim, Button btnQuayRa)
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
        public void FixDataGridViewSize(params V6ColorDataGridView[] dgvs)
        {
            try
            {
                foreach (V6ColorDataGridView dgv in dgvs)
                {
                    var gpa = dgv.Parent;
                    if (gpa != null)
                    {
                        dgv.Top = 52;
                        if (HaveGridViewSummary(gpa))
                        {
                            dgv.Height = gpa.Height - 75;
                        }
                        else
                        {
                            dgv.Height = gpa.Height - 55;
                        }
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

        private bool HaveGridViewSummary(Control container)
        {
            if (container != null)
            {
                foreach (Control control in container.Controls)
                {
                    if (control is GridViewSummary) return true;
                }
            }
            return false;
        }
        
        public IDictionary<string, object> PreparingDataAM(V6InvoiceBase invoice)
        {
            var addDataAM = GetData();
            addDataAM["STT_REC"] = _sttRec;
            addDataAM["MA_CT"] = invoice.Mact;
            All_Objects["AM_DATA"] = addDataAM;
            return addDataAM;
        }

        public void LoadTagAndText(V6InvoiceBase invoice, ControlCollection detailPanelControls)
        {
            try
            {
                var tagData = invoice.LoadTag(m_itemId);
                V6ControlFormHelper.SetFormTagDictionary(this, tagData);
                V6ControlFormHelper.SetFormTextDictionaryByName(this, invoice.textData);
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
                this.WriteExLog(GetType() + ".LoadTagAndText " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Tạo mới stt_rec và gán vào biến _sttRec.
        /// </summary>
        /// <param name="maCt">Invoice.Mact</param>
        /// <returns>_sttRec</returns>
        public virtual string GetSttRec(string maCt)
        {
            V6ControlFormHelper.RemoveRunningList(_sttRec);
            _sttRec = V6BusinessHelper. GetNewSttRec(maCt);
            return _sttRec;
        }

        public void ResetSttRec0(int stt_rec0_length = 5)
        {
            SetStatusText("ResetSttRec0");
            int i = 0;
            if (AD != null)
                foreach (DataRow row in AD.Rows)
                {
                    i++;
                    row["STT_REC0"] = ("000000000" + i).Right(stt_rec0_length);
                }
            i = 0;
            if (AD2 != null)
                foreach (DataRow row in AD2.Rows)
                {
                    i++;
                    row["STT_REC0"] = ("000000000" + i).Right(stt_rec0_length);
                }
            i = 0;
            if (AD3 != null)
                foreach (DataRow row in AD3.Rows)
                {
                    i++;
                    row["STT_REC0"] = ("000000000" + i).Right(stt_rec0_length);
                }
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
        public string GetSoCt0InitFilter()
        {
            var result = "";
            if (!chon_accept_flag_add) return result;
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

        public string GetThuNo131InitFilter()
        {
            var result = "";
            if (!chon_accept_flag_add) return result;
            try
            {
                foreach (DataRow row in AD.Rows)
                {
                    result += " And MA_KH <> '" + row["MA_KH_I"].ToString().Trim() + "'";
                }
                if (result.Length > 4) result = result.Substring(4);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            return result;
        }

        public string GetAlVitriTonInitFilter()
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

        public string GetAlLoTonInitFilter()
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

        #region ==== GETTONROW ====
        private DataTable _dataViTri;

        public void GetTonRow(DataGridViewRow grow, HD_Detail detail1, DateTime ngay_ct)
        {
            try
            {
                var cell_MA_VT = grow.Cells["MA_VT"];
                var cell_MA_KHO_I = grow.Cells["MA_KHO_I"];
                var cell_MA_LO = grow.Cells["MA_LO"];
                V6VvarTextBox txtmavt = new V6VvarTextBox() {VVar = "MA_VT"};
                txtmavt.Text = cell_MA_VT.Value.ToString();
                V6VvarTextBox txtmakhoi = new V6VvarTextBox() {VVar = "MA_KHO"};
                txtmakhoi.Text = cell_MA_KHO_I.Value.ToString();

                GetTon13Row(grow, detail1, txtmavt, txtmakhoi, ngay_ct);
                if (txtmavt.VITRI_YN)
                {
                    if (txtmavt.LO_YN && txtmavt.DATE_YN)
                    {
                        GetViTriLoDateRow(grow, detail1, txtmavt, txtmakhoi, ngay_ct);
                    }
                    else
                    {
                        GetViTriRow(grow, detail1, txtmavt, txtmakhoi, ngay_ct);
                    }
                }
                else
                {
                    if (cell_MA_LO.Value.ToString().Trim() == "") GetLoDateRow(grow, detail1, txtmavt, txtmakhoi, ngay_ct);
                    else GetLoDate13Row(grow, detail1, txtmavt, txtmakhoi, ngay_ct);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(
                    string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        public void GetTonRow_A1(DataGridViewRow grow, DateTime ngay_ct)
        {
            try
            {
                V6VvarTextBox txtmavt = new V6VvarTextBox() {VVar = "MA_VT"};
                txtmavt.Text = STR(grow.Cells["MA_VT"]);
                V6VvarTextBox txtmakhoi = new V6VvarTextBox() {VVar = "MA_KHO"};
                txtmakhoi.Text = STR(grow.Cells["MA_KHO_I"]);

                GetTon13Row_A1(grow, txtmavt, txtmakhoi, ngay_ct);
                if (txtmavt.VITRI_YN)
                {
                    if (txtmavt.LO_YN && txtmavt.DATE_YN)
                    {
                        GetViTriLoDateRow_A1(grow, txtmavt, txtmakhoi, ngay_ct);
                    }
                    else
                    {
                        GetViTriRow_A1(grow, txtmavt, txtmakhoi, ngay_ct);
                    }
                }
                else
                {
                    if (STR(grow.Cells["MA_LO"]) == "") GetLoDateRow_A1(grow, txtmavt, txtmakhoi, ngay_ct);
                    else GetLoDate13Row_A1(grow, txtmavt, txtmakhoi, ngay_ct);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(
                    string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void GetTon13Row(DataGridViewRow row, HD_Detail detail1, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = row.Cells["STT_REC0"];
                var cell_MA_VT = row.Cells["MA_VT"];
                var cell_MA_KHO_I = row.Cells["MA_KHO_I"];
                var cell_SO_LUONG1 = row.Cells["SO_LUONG1"];
                //var cell_HE_SO1 = row.Cells["HE SO1"];
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                var cell_TON13 = row.Cells["TON13"];
                

                if ((txtmavt.LO_YN || txtmavt.DATE_YN) && (txtmakhoi.LO_YN || txtmakhoi.DATE_YN))
                    return;

                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();
                // Get ton kho theo ma_kho,ma_vt 18/01/2016
                //if (V6Options.M_CHK_XUAT == "0")
                {
                    _dataViTri = _invoice.GetStock(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                    if (_dataViTri != null && _dataViTri.Rows.Count > 0)
                    {
                        string sttRec0 = cell_STT_REC0.Value.ToString();
                        //Trừ dần
                        for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow data_row = _dataViTri.Rows[i];
                            string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                            string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();


                            //Neu dung maVt va maKhoI
                            if (maVt == data_maVt && maKhoI == data_maKhoI)
                            {
                                //- so luong
                                decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton00"]);
                                decimal new_soLuong = data_soLuong;

                                foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                                {
                                    string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                                    string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                                    string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();

                                    decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]);

                                    //Add 31-07-2016
                                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                                    if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                                    {
                                        if (maVt == c_maVt && maKhoI == c_maKhoI)
                                        {
                                            new_soLuong -= c_soLuong;
                                        }
                                    }
                                }

                                //if (new_soLuong < 0) new_soLuong = 0;
                                {
                                    cell_TON13.Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        cell_TON13.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        public void GetTon13Row_A1(DataGridViewRow grow, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                //var cell_STT_REC0 = grow.Cells["STT_REC0"];
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                var cell_TON13 = grow.Cells["TON13"];
                

                if ((txtmavt.LO_YN || txtmavt.DATE_YN) && (txtmakhoi.LO_YN || txtmakhoi.DATE_YN))
                    return;

                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();
                // Get ton kho theo ma_kho,ma_vt 18/01/2016
                //if (V6Options.M_CHK_XUAT == "0")
                {
                    _dataViTri = _invoice.GetStock(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                    if (_dataViTri != null && _dataViTri.Rows.Count > 0)
                    {
                        string sttRec0 = STR(grow.Cells["STT_REC0"]);
                        //Trừ dần
                        for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow data_row = _dataViTri.Rows[i];
                            string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                            string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();


                            //Neu dung maVt va maKhoI
                            if (maVt == data_maVt && maKhoI == data_maKhoI)
                            {
                                //- so luong
                                decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton00"]);
                                decimal new_soLuong = data_soLuong;

                                foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                                {
                                    string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                                    string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                                    string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();

                                    decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]);

                                    //Add 31-07-2016
                                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                                    //if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0)) // !!!
                                    if (grow.DataGridView.EditingControl != null && c_sttRec0 != sttRec0)
                                    {
                                        if (maVt == c_maVt && maKhoI == c_maKhoI)
                                        {
                                            new_soLuong -= c_soLuong;
                                        }
                                    }
                                }

                                //if (new_soLuong < 0) new_soLuong = 0;
                                {
                                    cell_TON13.Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        cell_TON13.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }



        public void GetViTriLoDateRow(DataGridViewRow row, HD_Detail detail1, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = row.Cells["STT_REC0"];
                var cell_TON13 = row.Cells["TON13"];
                var cell_TON13QD = row.Cells["TON13QD"];
                //var cell_HE_SO1 = row.Cells["HE SO1"];
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                var cell_MA_LO = row.Cells["MA_LO"];
                var cell_MA_VITRI = row.Cells["MA_VITRI"];
                var cell_HANSD = row.Cells["HSD"];
                string sttRec0 = cell_STT_REC0.Value.ToString().Trim();
                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!txtmavt.LO_YN || !txtmavt.DATE_YN || !txtmavt.VITRI_YN
                    || !txtmakhoi.LO_YN || !txtmakhoi.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = _invoice.GetViTriLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    cell_TON13.Value = 0;
                    cell_MA_LO.Value = "";
                    cell_HANSD.Value = null;
                    cell_TON13QD.Value = 0;
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                //for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                for (int i = 0; i <= _dataViTri.Rows.Count - 1; i++)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maLo == "" || data_maViTri == "") continue;
                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal data_soLuongQd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_qd"]);
                        decimal new_soLuong = data_soLuong;
                        decimal new_soLuongQd = data_soLuongQd;

                        foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                        {
                            string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                            string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row1["Ma_lo"].ToString().Trim().ToUpper();
                            string c_maViTri = row1["Ma_vitri"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]); //???
                            decimal c_soLuongQd = ObjectAndString.ObjectToDecimal(row1["sl_qd"]); //???

                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo &&
                                    data_maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                    new_soLuongQd -= c_soLuongQd;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            cell_TON13.Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                            cell_MA_LO.Value = data_row["Ma_lo"].ToString().Trim();
                            cell_MA_VITRI.Value = data_row["Ma_vitri"].ToString().Trim();
                            cell_HANSD.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            cell_TON13QD.Value = new_soLuongQd;
                            break;
                        }
                        else
                        {
                            ResetTonLoHsdRow(cell_TON13, cell_MA_LO, cell_HANSD,cell_TON13QD);
                            cell_MA_VITRI.Value = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        public void GetViTriLoDateRow_A1(DataGridViewRow grow, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                
                string sttRec0 = STR(grow.Cells["STT_REC0"]);
                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!txtmavt.LO_YN || !txtmavt.DATE_YN || !txtmavt.VITRI_YN
                    || !txtmakhoi.LO_YN || !txtmakhoi.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = _invoice.GetViTriLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    grow.Cells["TON13"].Value = 0;
                    grow.Cells["MA_LO"].Value = "";
                    grow.Cells["HSD"].Value = null;
                    grow.Cells["TON13QD"].Value = 0;
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                //for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                for (int i = 0; i <= _dataViTri.Rows.Count - 1; i++)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maLo == "" || data_maViTri == "") continue;
                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal data_soLuongQd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_qd"]);
                        decimal new_soLuong = data_soLuong;
                        decimal new_soLuongQd = data_soLuongQd;

                        foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                        {
                            string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                            string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row1["Ma_lo"].ToString().Trim().ToUpper();
                            string c_maViTri = row1["Ma_vitri"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]); //???
                            decimal c_soLuongQd = ObjectAndString.ObjectToDecimal(row1["sl_qd"]); //???

                            if ((grow.DataGridView.EditingControl != null && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo &&
                                    data_maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                    new_soLuongQd -= c_soLuongQd;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            SetCellValue(grow.Cells["TON13"],new_soLuong * HE_SO1M / HE_SO1T); // chia hệ số
                            grow.Cells["MA_LO"].Value = data_row["Ma_lo"].ToString().Trim();
                            grow.Cells["MA_VITRI"].Value = data_row["Ma_vitri"].ToString().Trim();
                            grow.Cells["HSD"].Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            grow.Cells["TON13QD"].Value = new_soLuongQd;
                            break;
                        }
                        else
                        {
                            ResetTonLoHsdRow(grow.Cells["TON13"], grow.Cells["MA_LO"], grow.Cells["HSD"], grow.Cells["TON13QD"]);
                            grow.Cells["MA_VITRI"].Value = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void GetViTriRow(DataGridViewRow row, HD_Detail detail1, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = row.Cells["STT_REC0"];
                var cell_TON13 = row.Cells["TON13"];
                //var cell_HE_SO1 = row.Cells["HE SO1"];
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                var cell_MA_LO = row.Cells["MA_LO"];
                var cell_MA_VITRI = row.Cells["MA_VITRI"];
                var cell_HANSD = row.Cells["HSD"];
                string sttRec0 = cell_STT_REC0.Value.ToString().Trim();
                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!txtmavt.VITRI_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = _invoice.GetViTri(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    cell_TON13.Value = 0;
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maViTri == "") continue;
                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal new_soLuong = data_soLuong;

                        foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                        {
                            string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                            string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maViTri = row1["Ma_vitri"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]); //???
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        //if (new_soLuong < 0) new_soLuong = 0;
                        {
                            cell_TON13.Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                            cell_MA_VITRI.Value = data_row["Ma_vitri"].ToString().Trim();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        public void GetViTriRow_A1(DataGridViewRow grow, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                
                string sttRec0 = STR(grow.Cells["STT_REC0"]);
                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!txtmavt.VITRI_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = _invoice.GetViTri(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    grow.Cells["TON13"].Value = 0;
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maViTri == "") continue;
                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal new_soLuong = data_soLuong;

                        foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                        {
                            string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                            string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maViTri = row1["Ma_vitri"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]); //???
                            if (grow.DataGridView.EditingControl != null && c_sttRec0 != sttRec0)
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        //if (new_soLuong < 0) new_soLuong = 0;
                        {
                            grow.Cells["TON13"].Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                            grow.Cells["MA_VITRI"].Value = data_row["Ma_vitri"].ToString().Trim();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void GetLoDateRow(DataGridViewRow row, HD_Detail detail1, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = row.Cells["STT_REC0"];
                var cell_TON13 = row.Cells["TON13"];
                var cell_TON13QD = row.Cells["TON13QD"];
                //var cell_HE_SO1 = row.Cells["HE SO1"];
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                var cell_MA_LO = row.Cells["MA_LO"];
                var cell_MA_VITRI = row.Cells["MA_VITRI"];
                var cell_HANSD = row.Cells["HSD"];
                string sttRec0 = cell_STT_REC0.Value.ToString().Trim();
                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!txtmavt.LO_YN || !txtmavt.DATE_YN
                    || !txtmakhoi.LO_YN || !txtmakhoi.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = _invoice.GetLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    ResetTonLoHsdRow(cell_TON13, cell_MA_LO, cell_HANSD, cell_TON13QD);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    if (data_maLo == "") continue;
                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal data_soLuongQd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_QD"]);
                        decimal new_soLuong = data_soLuong;
                        decimal new_soLuongQd = data_soLuongQd;

                        foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                            string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row1["Ma_lo"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]); //???
                            decimal c_soLuongQd = ObjectAndString.ObjectToDecimal(row1["sl_qd"]); //???

                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                    new_soLuongQd -= c_soLuongQd;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            cell_TON13.Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                            cell_MA_LO.Value = data_row["Ma_lo"].ToString().Trim();
                            cell_HANSD.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            cell_TON13QD.Value = new_soLuongQd;
                            break;
                        }
                        else
                        {
                            ResetTonLoHsdRow(cell_TON13, cell_MA_LO, cell_HANSD, cell_TON13QD);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        private void GetLoDateRow_A1(DataGridViewRow grow, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = grow.Cells["STT_REC0"];
                var cell_TON13 = grow.Cells["TON13"];
                var cell_TON13QD = grow.Cells["TON13QD"];
                //var cell_HE_SO1 = row.Cells["HE SO1"];
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                var cell_MA_LO = grow.Cells["MA_LO"];
                var cell_MA_VITRI = grow.Cells["MA_VITRI"];
                var cell_HANSD = grow.Cells["HSD"];
                string sttRec0 = cell_STT_REC0.Value.ToString().Trim();
                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!txtmavt.LO_YN || !txtmavt.DATE_YN
                    || !txtmakhoi.LO_YN || !txtmakhoi.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = _invoice.GetLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    ResetTonLoHsdRow(cell_TON13, cell_MA_LO, cell_HANSD, cell_TON13QD);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    if (data_maLo == "") continue;
                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal data_soLuongQd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_QD"]);
                        decimal new_soLuong = data_soLuong;
                        decimal new_soLuongQd = data_soLuongQd;

                        foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                            string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row1["Ma_lo"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]); //???
                            decimal c_soLuongQd = ObjectAndString.ObjectToDecimal(row1["sl_qd"]); //???

                            if (grow.DataGridView.EditingControl != null && c_sttRec0 != sttRec0)
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                    new_soLuongQd -= c_soLuongQd;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            cell_TON13.Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                            cell_MA_LO.Value = data_row["Ma_lo"].ToString().Trim();
                            cell_HANSD.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            cell_TON13QD.Value = new_soLuongQd;
                            break;
                        }
                        else
                        {
                            ResetTonLoHsdRow(cell_TON13, cell_MA_LO, cell_HANSD, cell_TON13QD);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void GetLoDate13Row(DataGridViewRow row, HD_Detail detail1, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = row.Cells["STT_REC0"];
                var cell_TON13 = row.Cells["TON13"];
                var cell_TON13QD = row.Cells["TON13QD"];
                //var cell_HE_SO1 = row.Cells["HE SO1"];
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                var cell_MA_LO = row.Cells["MA_LO"];
                var cell_MA_VITRI = row.Cells["MA_VITRI"];
                var cell_HANSD = row.Cells["HSD"];
                string sttRec0 = cell_STT_REC0.Value.ToString().Trim();
                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();
                string maLo = cell_MA_LO.Value.ToString().Trim().ToUpper();

                // Theo doi lo moi check
                if (!txtmavt.LO_YN || !txtmavt.DATE_YN || !txtmakhoi.LO_YN || !txtmakhoi.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "" || maLo == "") return;

                _dataViTri = _invoice.GetLoDate13(maVt, maKhoI, maLo, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    ResetTonLoHsdRow(cell_TON13, cell_MA_LO, cell_HANSD, cell_TON13QD);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();

                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI && maLo == data_maLo)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal data_soLuongQd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_QD"]);
                        decimal new_soLuong = data_soLuong;
                        decimal new_soLuongQd = data_soLuongQd;

                        foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                            string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row1["Ma_lo"].ToString().Trim().ToUpper();

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]); //???
                            decimal c_soLuongQd = ObjectAndString.ObjectToDecimal(row1["Sl_qd"]); //???
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                    new_soLuongQd -= c_soLuongQd;
                                }
                            }
                        }

                        //if (new_soLuong < 0) new_soLuong = 0;
                        {
                            cell_TON13.Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                            cell_HANSD.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            cell_TON13QD.Value = new_soLuongQd;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        private void GetLoDate13Row_A1(DataGridViewRow grow, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = grow.Cells["STT_REC0"];
                var cell_TON13 = grow.Cells["TON13"];
                var cell_TON13QD = grow.Cells["TON13QD"];
                //var cell_HE_SO1 = row.Cells["HE SO1"];
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(grow.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;
                var cell_MA_LO = grow.Cells["MA_LO"];
                var cell_MA_VITRI = grow.Cells["MA_VITRI"];
                var cell_HANSD = grow.Cells["HSD"];
                string sttRec0 = cell_STT_REC0.Value.ToString().Trim();
                string maVt = txtmavt.Text.Trim().ToUpper();
                string maKhoI = txtmakhoi.Text.Trim().ToUpper();
                string maLo = cell_MA_LO.Value.ToString().Trim().ToUpper();

                // Theo doi lo moi check
                if (!txtmavt.LO_YN || !txtmavt.DATE_YN || !txtmakhoi.LO_YN || !txtmakhoi.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "" || maLo == "") return;

                _dataViTri = _invoice.GetLoDate13(maVt, maKhoI, maLo, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    ResetTonLoHsdRow(cell_TON13, cell_MA_LO, cell_HANSD, cell_TON13QD);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();

                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI && maLo == data_maLo)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal data_soLuongQd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_QD"]);
                        decimal new_soLuong = data_soLuong;
                        decimal new_soLuongQd = data_soLuongQd;

                        foreach (DataRow row1 in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row1["Stt_rec0"].ToString().Trim();
                            string c_maVt = row1["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row1["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row1["Ma_lo"].ToString().Trim().ToUpper();

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row1["So_luong"]); //???
                            decimal c_soLuongQd = ObjectAndString.ObjectToDecimal(row1["Sl_qd"]); //???
                            if (grow.DataGridView.EditingControl != null && c_sttRec0 != sttRec0)
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                    new_soLuongQd -= c_soLuongQd;
                                }
                            }
                        }

                        //if (new_soLuong < 0) new_soLuong = 0;
                        {
                            cell_TON13.Value = new_soLuong * HE_SO1M / HE_SO1T; // chia hệ số
                            cell_HANSD.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            cell_TON13QD.Value = new_soLuongQd;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        #endregion ==== GETTONROW ====

        /// <summary>
        /// Di chuyển đến chi tiết tiếp theo, focus vào nút Sửa. Nếu dòng cuối cho focus vào Mới.
        /// </summary>
        /// <param name="dataGridView1">Gridview</param>
        /// <param name="detailControl">và detail_control tương ứng.</param>
        /// <param name="check">on-off</param>
        public void GotoNextDetailEdit(V6ColorDataGridView dataGridView1, HD_Detail detailControl, bool check)
        {
            if (!check)
            {
                detailControl.FocusButton = detailControl.btnMoi;
                return;
            }

            try
            {
                detailControl.FocusButton = detailControl.btnSua;
                var nextRow = dataGridView1.GotoNextRow();
                if (nextRow == null) detailControl.FocusButton = detailControl.btnMoi;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void HienThiTongSoDong(Label lblTongSoDong)
        {
            try
            {
                var tSoDong = AD == null ? 0 : AD.Rows.Count;
                lblTongSoDong.Text = tSoDong.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".HienThiTongSoDong", ex);
            }
        }

        /// <summary>
        /// Bật edit ở cột CP_NT, CP nếu lại pb = 0.
        /// </summary>
        /// <param name="loai_pb"></param>
        /// <param name="sua_tien"></param>
        /// <param name="dataGridView3ChiPhi"></param>
        /// <param name="columnsNgoaiTe">Các cột được edit trường hợp dùng Ngoại tệ.</param>
        /// <param name="columnsNguyenTe">Các cột được edit trường hợp không dùng Ngoại tệ.</param>
        public void SetGridViewChiPhiEditAble(string loai_pb, bool sua_tien, V6ColorDataGridView dataGridView3ChiPhi,
            string columnsNgoaiTe = "CP,CP_NT", string columnsNguyenTe = "CP_NT")
        {
            if (loai_pb == "0" && (Mode == V6Mode.Add || Mode == V6Mode.Edit))
            {
                if(MA_NT != _mMaNt0)
                    dataGridView3ChiPhi.SetEditColumn(sua_tien ? columnsNgoaiTe.Split(',') : columnsNguyenTe.Split(','));
                else
                    dataGridView3ChiPhi.SetEditColumn(columnsNguyenTe.Split(','));
            }
            else
            {
                dataGridView3ChiPhi.ReadOnly = true;
            }
        }

        /// <summary>
        /// Bật tắt chỉnh sửa trực tiếp trên dataGridView1
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="Invoice"></param>
        public void SetGridViewReadonly(V6ColorDataGridView dataGridView1, V6InvoiceBase Invoice)
        {
            try
            {
                if (Mode != V6Mode.Edit)
                {
                    dataGridView1.ReadOnly = true;
                    return;
                }

                dataGridView1.ReadOnly = false;
                //dataGridView1.SetEditColumn();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.ReadOnly = true;
                }
                //dataGridView1.SetEditColumn();//Alctct right. copy dataeditor sua nhieu dong.
                //invoice.GRD_READONLY
                // FIELD:E/R invoice extra info ADFIELDS

                string _showFields = null;
                if (Invoice.EXTRA_INFOR != null && Invoice.EXTRA_INFOR.ContainsKey("ADFIELDS"))
                {
                    _showFields = Invoice.EXTRA_INFOR["ADFIELDS"];
                }

                if (_showFields != null)
                {
                    var showFieldSplit = ObjectAndString.SplitString(_showFields);
                    foreach (string field in showFieldSplit)
                    {
                        if (field.Contains(":"))
                        {
                            var ss = field.Split(':');
                            DataGridViewColumn column = dataGridView1.Columns[ss[0]];

                            if (ss.Length > 2)
                            {
                                string NM_IP = ss[2].ToUpper(); // N2 hoac NM_IP_SL
                                if (NM_IP.StartsWith("N"))
                                {
                                    string newFormat = NM_IP.Length == 2
                                        ? NM_IP
                                        : V6Options.GetValueNull(NM_IP.Substring(1));
                                    column = dataGridView1.ChangeColumnType(ss[0],
                                        typeof (V6NumberDataGridViewColumn), newFormat);
                                }
                                else if (NM_IP.StartsWith("C")) // CVvar
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0],
                                        typeof (V6VvarDataGridViewColumn), null);
                                    ((V6VvarDataGridViewColumn) column).Vvar = NM_IP.Substring(1);
                                }
                                else if (NM_IP.StartsWith("D0")) // ColorDateTime
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0],
                                        typeof (V6DateTimeColorGridViewColumn), null);
                                }
                                else if (NM_IP.StartsWith("D1")) // DateTimePicker
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0],
                                        typeof (V6DateTimePickerGridViewColumn), null);
                                }
                            }
                            else
                            {
                                
                            }

                            if (ss[1].ToUpper() == "R" && column != null)
                            {
                                column.ReadOnly = true;
                            }
                            else if (ss[1].ToUpper() == "E" && column != null)
                            {
                                column.ReadOnly = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Gán dữ liệu mặc định theo chứng từ. (VPA_GetDefaultvalue)
        /// </summary>
        /// <param name="invoice"></param>
        public void SetDefaultData(V6InvoiceBase invoice)
        {
            try
            {
                LoadDefaultData(1, invoice.Mact, "", m_itemId, "nhom='00'");
                //var data = invoice.LoadDefaultData(V6Setting.Language, m_itemId);
                //var data0 = new SortedDictionary<string, object>();
                //data0.AddRange(data);
                //var controlDic = V6ControlFormHelper.SetFormDataDictionary(this, new SortedDictionary<string, object>(data0), false);
                //FixVvarBrothers(controlDic);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetDefaultData " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Điều khiển ẩn hiện các control chi tiết.
        /// </summary>
        /// <param name="detailControlList"></param>
        /// <param name="visible">true hiện / false ẩn</param>
        /// <param name="fieldList"></param>
        public void SetDetailControlVisible(IDictionary<string, AlctControls> detailControlList, bool visible, params string[] fieldList)
        {
            try
            {
                foreach (string field in fieldList)
                {
                    string FIELD = field.Trim().ToUpper();
                    if (detailControlList.ContainsKey(FIELD))
                    {
                        var item = detailControlList[FIELD];
                        if (item.IsVisible)
                        {
                            if (visible) item.DetailControl.VisibleTag();
                            else item.DetailControl.InvisibleTag();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetDetailControlVisible " + _sttRec, ex);
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

        public void SetDefaultDataDetail(V6InvoiceBase invoice, Control detailControl)
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

        public void SetDefaultDataHDDetail(V6InvoiceBase invoice, HD_Detail detailControl)
        {
            try
            {
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if (!shift_is_down) return;

                var data = invoice.LoadDefaultData(V6Setting.Language, m_itemId);
                var data0 = new SortedDictionary<string, object>();
                data0.AddRange(data);
                V6ControlFormHelper.SetFormDataDictionary(detailControl.panel0, new SortedDictionary<string, object>(data0), false);
                V6ControlFormHelper.SetFormDataDictionary(detailControl.panelControls, new SortedDictionary<string, object>(data0), false);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetDefaultDataDetail " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Gán dữ liệu mặc định khi bấm mới chi tiết. Cần override
        /// </summary>
        public virtual void SetDefaultDetail()
        {
            var detail1 = this.GetControlByName("detail1") as HD_Detail;
            SetDefaultDataHDDetail(_invoice, detail1);
        }

        /// <summary>
        /// <para>Gán InitFilter cho các control được chỉ định trong V6Options.M_V6_ADV_FILTER</para>
        /// <para>Field:TableName:1 => lấy initfilter từ V6Login theo TableName.</para>
        /// <para>Field:TableName:1 => nếu là 1 sẽ gắn thêm [Status] &lt;&gt; '0'</para>
        /// </summary>
        public void SetBaseInitFilterAll()
        {
            if (V6Setting.NotLoggedIn) return;
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

                        var old_filter = txt.Filter;
                        var adv_filter = V6Login.GetInitFilter(tableName, V6ControlFormHelper.FindFilterType(this));
                        if (status == "1")
                        {
                            var adv_filter_extra = "[Status] <> '0'";
                            if (!string.IsNullOrEmpty(adv_filter_extra))
                            {
                                adv_filter += (string.IsNullOrEmpty(adv_filter) ? "" : " and ") + adv_filter_extra;
                            }
                        }
                        
                        txt.BaseInitFilter = adv_filter;
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
                    var old_filter = txt.Filter;
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

        public void ShowViewInfoData(V6InvoiceBase invoice)
        {
            try
            {
                if (!string.IsNullOrEmpty(_sttRec))
                {
                    new InvoiceInfosViewForm(invoice, _sttRec, invoice.Mact).ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ShowViewInfoData " + _sttRec, ex);
            }
        }
        
        public void ShowViewInfoData2_TH(V6InvoiceBase invoice)
        {
            try
            {
                if (!string.IsNullOrEmpty(_sttRec))
                {
                    var f = new InvoiceInfosViewForm(invoice, _sttRec, invoice.Mact);
                    f.Data2_TH = true;
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ShowViewInfoData " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Hiển thị thông báo trên Container của form chứng từ.
        /// </summary>
        /// <param name="message"></param>
        public virtual void ShowParentMessage(string message)
        {
            try
            {
                var parent = Parent.Parent;
                for (int i = 0; i < 5; i++)
                {
                    if (parent is ChungTuChungContainer)
                    {
                        ((ChungTuChungContainer)parent).ShowMessage(message);
                        return;
                    }
                    else if (parent is HoaDonCafeContainer)
                    {
                        ((HoaDonCafeContainer)parent).ShowMessage(message);
                        return;
                    }
                    else if (parent is BaoGiaContainer)
                    {
                        ((BaoGiaContainer)parent).ShowMessage(message);
                        return;
                    }
                    else if (parent is V6Form)
                    {
                        if (!string.IsNullOrEmpty(message)) ((V6Form)parent).ShowMessage(message);
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
        /// <param name="actionControl">Control gây ra sự kiện.</param>
        /// <param name="_soLuong1">Số lượng nhập theo DVT đang chọn.</param>
        /// <param name="_sl_qd">Quy đổi ra sl theo DVT quy đổi (sẽ được tính toán ra).</param>
        /// <param name="_sl_qd2"></param>
        /// <param name="_hs_qd1">Hệ số nhân đổi DVT ra DVT qd. (1 viên = ?0.01 thùng)</param>
        /// <param name="_hs_qd2">100 viên / thùng</param>
        public void TinhSoluongQuyDoi_0(V6NumberTextBox _soLuong1
            , V6NumberTextBox _sl_qd, V6NumberTextBox _sl_qd2
            , V6NumberTextBox _hs_qd1, V6NumberTextBox _hs_qd2, Control actionControl)
        {
            try
            {
               
                if (M_CAL_SL_QD_ALL == "0")
                {
                    //Phần nguyên, (ví dụ 1.5 thùng)
                    if (_hs_qd1.Value != 0)
                    {
                        if (M_TYPE_SL_QD_ALL == "0E" && actionControl != null)
                        {
                            string ACN = actionControl.AccessibleName.ToUpper();
                            if (ACN == "SL_QD" || ACN == "GIA_NT21" || ACN == "GIA_NT01" || ACN == "GIA_NT1")
                            {
                                DoNothing();
                            }
                            else
                            {
                                var sl_qd = _soLuong1.Value * _hs_qd1.Value;
                                _sl_qd.Value = V6BusinessHelper.Vround(sl_qd, M_ROUND_SL);
                            }
                        }
                        else
                        {
                            var sl_qd = _soLuong1.Value * _hs_qd1.Value;
                            _sl_qd.Value = V6BusinessHelper.Vround(sl_qd, M_ROUND_SL);
                        }
                    }
                    else if (M_TYPE_SL_QD_ALL == "00")
                    {
                        _sl_qd.Value = 0;
                    }
                    //Phần lẻ (ví dụ 50 viên = 0.5 thùng bên trên)
                    //Tuanmh 26/02/2019 _hs_qd2.Value != 0 
                    if (_hs_qd2.Value != 0)
                    {
                        var tong = _sl_qd.Value*_hs_qd2.Value;
                        var sl_nguyen_thung = ((int) _sl_qd.Value)*_hs_qd2.Value;
                        _sl_qd2.Value = V6BusinessHelper.Vround(tong - sl_nguyen_thung, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi_0 " + _sttRec, ex);
            }
        }

        public void TinhSoluongQuyDoi_2(V6NumberTextBox _soLuong1
            , V6NumberTextBox _sl_qd, V6NumberTextBox _sl_qd2
            , V6NumberTextBox _hs_qd1, V6NumberTextBox _hs_qd2, Control actionControl)
        {
            try
            {
                if (M_CAL_SL_QD_ALL == "2")
                {
                    //Quy đổi chia
                    if (_hs_qd1.Value != 0)
                    {
                        if (M_TYPE_SL_QD_ALL == "2E" && actionControl != null)
                        {
                            string ACN = actionControl.AccessibleName.ToUpper();
                            if (ACN == "SL_QD" || ACN == "GIA_NT21" || ACN == "GIA_NT01" || ACN == "GIA_NT1")
                            {
                                DoNothing();
                            }
                            else
                            {
                                var sl_qd = _soLuong1.Value / _hs_qd1.Value;
                                _sl_qd.Value = V6BusinessHelper.Vround(sl_qd, M_ROUND_SL);
                            }
                        }
                        else
                        {
                            var sl_qd = _soLuong1.Value / _hs_qd1.Value;
                            _sl_qd.Value = V6BusinessHelper.Vround(sl_qd, M_ROUND_SL);
                        }
                    }
                    else if (M_TYPE_SL_QD_ALL == "20")
                    {
                        _sl_qd.Value = 0;
                    }
                    //Phần lẻ (ví dụ 50 viên = 0.5 thùng bên trên)
                    //Tuanmh 26/02/2019 _hs_qd2.Value != 0 
                    if (_hs_qd2.Value != 0)
                    {
                        var tong = _sl_qd.Value*_hs_qd2.Value;
                        var sl_nguyen_thung = ((int) _sl_qd.Value)*_hs_qd2.Value;
                        _sl_qd2.Value = V6BusinessHelper.Vround(tong - sl_nguyen_thung, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi_0 " + _sttRec, ex);
            }
        }

        public void TinhSoluongQuyDoi_1(V6NumberTextBox _soLuong1
            , V6NumberTextBox _sl_qd, V6NumberTextBox _sl_qd2
            , V6NumberTextBox _hs_qd1, V6NumberTextBox _hs_qd2, Control actionControl)
        {
            try
            {
                if (M_CAL_SL_QD_ALL == "1")
                {
                    if (_hs_qd1.Value != 0)
                    {
                        if (M_TYPE_SL_QD_ALL == "1E" && actionControl != null)
                        {
                            string ACN = actionControl.AccessibleName.ToUpper();
                            if (ACN == "SO_LUONG1" || ACN == "GIA_NT21" || ACN == "GIA_NT01" || ACN == "GIA_NT1")
                            {
                                DoNothing();
                            }
                            else
                            {
                                var soLuong1 = _sl_qd.Value * _hs_qd1.Value;
                                _soLuong1.Value = V6BusinessHelper.Vround(soLuong1, M_ROUND_SL);
                            }
                        }
                        else
                        {
                            var soLuong1 = _sl_qd.Value * _hs_qd1.Value;
                            _soLuong1.Value = V6BusinessHelper.Vround(soLuong1, M_ROUND_SL);
                        }
                    }
                    else if (M_TYPE_SL_QD_ALL == "10")
                    {
                        _soLuong1.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi_1 " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Tính số lượng quy đổi.
        /// </summary>
        /// <param name="row">Dòng chi tiết đang xử lý.</param>
        /// <param name="actionControl">Control gây ra sự kiện.</param>
        public void TinhSoluongQuyDoi_0_Row(DataGridViewRow row, string actionControl)
        {
            try
            {
                var _hs_qd1 = row.Cells["HS_QD1"];
                var _hs_qd2 = row.Cells["HS_QD2"];
                var _sl_qd = row.Cells["SL_QD"];
                var _sl_qd2 = row.Cells["SL_QD2"];
                var _soLuong1 = row.Cells["SO_LUONG1"];
                if (M_CAL_SL_QD_ALL == "0")
                {
                    //Phần nguyên, (ví dụ 1.5 thùng)
                    if (ObjectAndString.ObjectToDecimal(_hs_qd1.Value) != 0)
                    {
                        if (M_TYPE_SL_QD_ALL == "0E" && actionControl != null)
                        {
                            string ACN = actionControl.ToUpper();
                            if (ACN == "SL_QD" || ACN == "GIA_NT21" || ACN == "GIA_NT01" || ACN == "GIA_NT1")
                            {
                                DoNothing();
                            }
                            else
                            {
                                var sl_qd = ObjectAndString.ObjectToDecimal(_soLuong1.Value) * ObjectAndString.ObjectToDecimal(_hs_qd1.Value);
                                _sl_qd.Value = V6BusinessHelper.Vround(sl_qd, M_ROUND_SL);
                            }
                        }
                        else
                        {
                            var sl_qd = ObjectAndString.ObjectToDecimal(_soLuong1.Value) * ObjectAndString.ObjectToDecimal(_hs_qd1.Value);
                            _sl_qd.Value = V6BusinessHelper.Vround(sl_qd, M_ROUND_SL);
                        }
                    }
                    else if (M_TYPE_SL_QD_ALL == "00")
                    {
                        _sl_qd.Value = 0;
                    }
                    //Phần lẻ (ví dụ 50 viên = 0.5 thùng bên trên)
                    //Tuanmh 26/02/2019 _hs_qd2.Value != 0 
                    if (ObjectAndString.ObjectToDecimal(_hs_qd2.Value) != 0)
                    {
                        var tong = ObjectAndString.ObjectToDecimal(_sl_qd.Value) * ObjectAndString.ObjectToDecimal(_hs_qd2.Value);
                        var sl_nguyen_thung = ((int)ObjectAndString.ObjectToDecimal(_sl_qd.Value)) * ObjectAndString.ObjectToDecimal(_hs_qd2.Value);
                        _sl_qd2.Value = V6BusinessHelper.Vround(tong - sl_nguyen_thung, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi_0 " + _sttRec, ex);
            }
        }

        public void TinhSoluongQuyDoi_2_Row(DataGridViewRow row, string actionControl)
        {
            try
            {
                var _hs_qd1 = row.Cells["HS_QD1"];
                var _hs_qd2 = row.Cells["HS_QD2"];
                var _sl_qd = row.Cells["SL_QD"];
                var _sl_qd2 = row.Cells["SL_QD2"];
                var _soLuong1 = row.Cells["SO_LUONG1"];
                if (M_CAL_SL_QD_ALL == "2")
                {
                    //Quy đổi chia
                    if (ObjectAndString.ObjectToDecimal(_hs_qd1.Value) != 0)
                    {
                        if (M_TYPE_SL_QD_ALL == "2E" && actionControl != null)
                        {
                            string ACN = actionControl.ToUpper();
                            if (ACN == "SL_QD" || ACN == "GIA_NT21" || ACN == "GIA_NT01" || ACN == "GIA_NT1")
                            {
                                DoNothing();
                            }
                            else
                            {
                                var sl_qd = ObjectAndString.ObjectToDecimal(_soLuong1.Value) / ObjectAndString.ObjectToDecimal(_hs_qd1.Value);
                                _sl_qd.Value = V6BusinessHelper.Vround(sl_qd, M_ROUND_SL);
                            }
                        }
                        else
                        {
                            var sl_qd = ObjectAndString.ObjectToDecimal(_soLuong1.Value) / ObjectAndString.ObjectToDecimal(_hs_qd1.Value);
                            _sl_qd.Value = V6BusinessHelper.Vround(sl_qd, M_ROUND_SL);
                        }
                    }
                    else if (M_TYPE_SL_QD_ALL == "20")
                    {
                        _sl_qd.Value = 0;
                    }
                    //Phần lẻ (ví dụ 50 viên = 0.5 thùng bên trên)
                    //Tuanmh 26/02/2019 _hs_qd2.Value != 0 
                    if (ObjectAndString.ObjectToDecimal(_hs_qd2.Value) != 0)
                    {
                        var tong = ObjectAndString.ObjectToDecimal(_sl_qd.Value) * ObjectAndString.ObjectToDecimal(_hs_qd2.Value);
                        var sl_nguyen_thung = ((int)ObjectAndString.ObjectToDecimal(_sl_qd.Value)) * ObjectAndString.ObjectToDecimal(_hs_qd2.Value);
                        _sl_qd2.Value = V6BusinessHelper.Vround(tong - sl_nguyen_thung, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi_0 " + _sttRec, ex);
            }
        }

        public void TinhSoluongQuyDoi_1_Row(DataGridViewRow row, string actionControl)
        {
            try
            {
                var _hs_qd1 = row.Cells["HS_QD1"];
                var _hs_qd2 = row.Cells["HS_QD2"];
                var _sl_qd = row.Cells["SL_QD"];
                var _sl_qd2 = row.Cells["SL_QD2"];
                var _soLuong1 = row.Cells["SO_LUONG1"];
                if (M_CAL_SL_QD_ALL == "1")
                {
                    if (ObjectAndString.ObjectToDecimal(_hs_qd1.Value) != 0)
                    {
                        if (M_TYPE_SL_QD_ALL == "1E" && actionControl != null)
                        {
                            string ACN = actionControl.ToUpper();
                            if (ACN == "SO_LUONG1" || ACN == "GIA_NT21" || ACN == "GIA_NT01" || ACN == "GIA_NT1")
                            {
                                DoNothing();
                            }
                            else
                            {
                                var soLuong1 = ObjectAndString.ObjectToDecimal(_sl_qd.Value) * ObjectAndString.ObjectToDecimal(_hs_qd1.Value);
                                _soLuong1.Value = V6BusinessHelper.Vround(soLuong1, M_ROUND_SL);
                            }
                        }
                        else
                        {
                            var soLuong1 = ObjectAndString.ObjectToDecimal(_sl_qd.Value) * ObjectAndString.ObjectToDecimal(_hs_qd1.Value);
                            _soLuong1.Value = V6BusinessHelper.Vround(soLuong1, M_ROUND_SL);
                        }
                    }
                    else if (M_TYPE_SL_QD_ALL == "10")
                    {
                        _soLuong1.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhSoluongQuyDoi_1 " + _sttRec, ex);
            }
        }


        public decimal TinhTong(DataTable AD_table, string colName)
        {
            return V6BusinessHelper.TinhTong(AD_table, colName, true);
        }

        /// <summary>
        /// Lấy giá trị số của cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public decimal DEC(DataGridViewCell cell)
        {
            return ObjectAndString.ObjectToDecimal(cell.Value);
        }


        /// <summary>
        /// return grow.Cells[name].Value.ToString().Trim();
        /// </summary>
        /// <param name="grow"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string STR(DataGridViewRow grow, string name)
        {
            return ObjectAndString.ObjectToString(grow.Cells[name].Value).Trim();
        }

        public string STR(DataGridViewCell cell)
        {
            return ObjectAndString.ObjectToString(cell.Value).Trim();
        }

        /// <summary>
        /// Lấy mã vật tư trong gridview_row.
        /// </summary>
        /// <param name="grow"></param>
        /// <returns></returns>
        public string MA_VT(DataGridViewRow grow)
        {
            return ObjectAndString.ObjectToString(grow.Cells["MA_VT"].Value).Trim();
        }

        /// <summary>
        /// Tính ck_nt, ck khi nhập tiền hoặc số lượng ...
        /// </summary>
        /// <param name="nhap_ck_nt">Đang nhập tiền ck.</param>
        /// <param name="_ck_textbox"></param>
        /// <param name="_ck_nt_textbox"></param>
        /// <param name="txtTyGia"></param>
        /// <param name="_tienNt2"></param>
        /// <param name="_pt_cki"></param>
        public void TinhChietKhauChiTiet(bool nhap_ck_nt, V6NumberTextBox _ck_textbox, V6NumberTextBox _ck_nt_textbox,
            V6NumberTextBox txtTyGia, V6NumberTextBox _tienNt2, V6NumberTextBox _pt_cki)
        {
            try
            {
                if (nhap_ck_nt)
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
        public void TinhChietKhauChiTiet(DataGridViewRow grow, decimal ty_gia, bool nhap_ck_nt = false)
        {
            try
            {
                if (!nhap_ck_nt)
                {
                    SetCellValue(grow.Cells["CK_NT"], V6BusinessHelper.Vround(DEC(grow.Cells["TIEN_NT2"]) * DEC(grow.Cells["PT_CKI"]) / 100, M_ROUND_NT));
                }
                if (_maNt == _mMaNt0)
                {
                    SetCellValue(grow.Cells["CK"], grow.Cells["CK_NT"].Value);
                }
                else
                {
                    SetCellValue(grow.Cells["CK"], V6BusinessHelper.Vround(DEC(grow.Cells["CK_NT"]) * ty_gia, M_ROUND));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        /// <summary>
        /// Tính ck_nt, ck khi nhập tiền hoặc số lượng ...
        /// </summary>
        /// <param name="nhap_ck_nt">Đang nhập tiền ck.</param>
        /// <param name="cell_ck"></param>
        /// <param name="cell_ck_nt"></param>
        /// <param name="txtTyGia"></param>
        /// <param name="cell_tien_nt2"></param>
        /// <param name="cell_pt_cki"></param>
        public void TinhChietKhauChiTiet_row(bool nhap_ck_nt, DataGridViewCell cell_ck, DataGridViewCell cell_ck_nt,
            V6NumberTextBox txtTyGia, DataGridViewCell cell_tien_nt2, DataGridViewCell cell_pt_cki)
        {
            try
            {
                if (!nhap_ck_nt)
                    cell_ck_nt.Value = V6BusinessHelper.Vround(
                        ObjectAndString.ObjectToDecimal(cell_tien_nt2.Value)
                        * ObjectAndString.ObjectToDecimal(cell_pt_cki.Value)
                        / 100, M_ROUND_NT);

                if (_maNt == _mMaNt0)
                {
                    cell_ck.Value = cell_ck_nt.Value;
                }
                else
                {
                    cell_ck.Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(cell_ck_nt.Value) * txtTyGia.Value, M_ROUND);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhChietKhauChiTiet " + _sttRec, ex);
            }
        }
        
        public void TinhChietKhauChiTiet_row_NHAP_TIEN_NT0(bool nhapTien, DataGridViewRow row,
            Decimal txtTyGia_Value)
        {
            try
            {
                var _ck = row.Cells["CK"];
                var _ck_nt = row.Cells["CK_NT"];
                var _pt_cki = row.Cells["PT_CKI"];
                var _tien_nt0 = row.Cells["TIEN_NT0"];
                if (nhapTien)
                {
                    _ck.Value = _maNt == _mMaNt0 ? _ck_nt.Value : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(_ck_nt.Value) * txtTyGia_Value, M_ROUND);
                }
                else
                {
                    _ck_nt.Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(_tien_nt0.Value) * ObjectAndString.ObjectToDecimal(_pt_cki.Value) / 100, M_ROUND_NT);
                    _ck.Value = _maNt == _mMaNt0 ? _ck_nt.Value : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(_ck_nt.Value) * txtTyGia_Value, M_ROUND);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhChietKhauChiTietG " + _sttRec, ex);
            }
        }

        public void TinhChietKhauChiTiet_row_XUAT_TIEN_NT2(DataGridViewRow grow, Decimal txtTyGia_Value, bool nhapTien = false)
        {
            try
            {
                var _ck = grow.Cells["CK"];
                var _ck_nt = grow.Cells["CK_NT"];
                var _pt_cki = grow.Cells["PT_CKI"];
                var _tien_nt2 = grow.Cells["TIEN_NT2"];
                if (nhapTien)
                {
                    _ck.Value = _maNt == _mMaNt0 ? _ck_nt.Value : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(_ck_nt.Value) * txtTyGia_Value, M_ROUND);
                }
                else
                {
                    _ck_nt.Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(_tien_nt2.Value) * ObjectAndString.ObjectToDecimal(_pt_cki.Value) / 100, M_ROUND_NT);
                    _ck.Value = _maNt == _mMaNt0 ? _ck_nt.Value : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(_ck_nt.Value) * txtTyGia_Value, M_ROUND);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhChietKhauChiTietG " + _sttRec, ex);
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
        public void Tinh_TienThue_TheoThueSuat(decimal thueSuat, decimal tien, V6NumberTextBox txtTienThue, int round)
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

        public void Tinh_TienThue_TheoTienThueNt(decimal tienThueNt, decimal tyGia, V6NumberTextBox txtTienThue, int round)
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
        public void Tinh_TienThue_TheoTienThueNt_row(DataGridViewRow grow, decimal tienThueNt, decimal tyGia, string thue_field, int round)
        {
            try
            {
                SetCellValue(grow.Cells[thue_field], V6BusinessHelper.Vround(tienThueNt * tyGia, round));
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
        public void Tinh_TienThueNtVaTienThue_TheoThueSuat(decimal thueSuat, decimal tienNt, decimal tien, V6NumberTextBox txtTienThueNt, V6NumberTextBox txtTienThue)
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

        public void Tinh_TienThueNtVaTienThue_TheoThueSuat_Row(decimal thueSuat, decimal tienNt, decimal tien, DataGridViewRow row)
        {
            try
            {
                var thue_nt = row.Cells["THUE_NT"];
                var thue = row.Cells["THUE"];

                thue_nt.Value = V6BusinessHelper.Vround(tienNt * thueSuat / 100, M_ROUND_NT);
                thue.Value = _maNt == _mMaNt0 ? thue_nt.Value : V6BusinessHelper.Vround(tien * thueSuat / 100, M_ROUND);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Tính Gia_nt và Gia theo Tien_nt và Tien chia So_luong.
        /// </summary>
        /// <param name="row"></param>
        public void TinhGia_TheoTienSoLuong(DataRow row)
        {
            var so_luong = ObjectAndString.ObjectToDecimal(row["SO_LUONG"]);
            if (so_luong != 0)
            {
                decimal tien_nt = ObjectAndString.ObjectToDecimal(row["TIEN_NT"]);
                decimal tien = ObjectAndString.ObjectToDecimal(row["TIEN"]);
                if (AD.Columns.Contains("GIA_NT"))
                    row["GIA_NT"] = V6BusinessHelper.Vround((tien_nt / so_luong), M_ROUND_GIA_NT);
                if (AD.Columns.Contains("GIA"))
                    row["GIA"] = V6BusinessHelper.Vround((tien / so_luong), M_ROUND_GIA);
                if (_maNt == _mMaNt0)
                {
                    row["GIA"] = row["GIA_NT"];
                }
            }
        }

        /// <summary>
        /// Tính Gia_nt1 và Gia1 theo Tien_nt và Tien chia So_luong1.
        /// </summary>
        /// <param name="row"></param>
        public void TinhGia1_TheoTienSoLuong1(DataRow row)
        {
            var so_luong1 = ObjectAndString.ObjectToDecimal(row["SO_LUONG1"]);
            if (so_luong1 != 0)
            {
                decimal tien_nt = ObjectAndString.ObjectToDecimal(row["TIEN_NT"]);
                decimal tien = ObjectAndString.ObjectToDecimal(row["TIEN"]);
                if (AD.Columns.Contains("GIA_NT1"))
                    row["GIA_NT1"] = V6BusinessHelper.Vround((tien_nt / so_luong1), M_ROUND_GIA_NT);
                if (AD.Columns.Contains("GIA1"))
                    row["GIA1"] = V6BusinessHelper.Vround((tien / so_luong1), M_ROUND_GIA);
                if (_maNt == _mMaNt0)
                {
                    row["GIA1"] = row["GIA_NT1"];
                }
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
        public void TienNtChanged(decimal tienNt, decimal tyGia, decimal thueSuat, V6NumberTextBox txtTien,
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
        public void TienChanged(decimal tien, decimal thueSuat, V6NumberTextBox txtTienThue)
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

        public bool _autoloadtop_acted = false;
        protected void AutoLoadTop(ToolStripMenuItem timTopCuoiKyMenu)
        {
            try
            {
                int timer_count = 0;
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += delegate
                {
                    if (_autoloadtop_acted)
                    {
                        timer.Stop();
                        timer.Dispose();
                        return;
                    }
                    if (++timer_count == 3)
                    {
                        timTopCuoiKyMenu.PerformClick();
                        timer.Stop();
                        timer.Dispose();
                    }
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void V6InvoiceControl_Load(object sender, EventArgs e)
        {
            V6ControlsHelper.DisableLookup = false;
            SetBaseInitFilterAll();
            //Gán ngôn ngữ, Sửa AccessibleName trên Form
            //LoadLanguage() ;
        }

        /// <summary>
        /// Chạy ExistRowInTable cho các V6VvarTextBox trong tabAdvance.
        /// </summary>
        public void CheckVvarTextBox()
        {
            try
            {
                var v6TabControl1 = this.GetControlByName("tabControl1") as TabControl;
                if (v6TabControl1 != null)
                {
                    foreach (TabPage tabPage in v6TabControl1.TabPages)
                    {
                        if (tabPage.Text == "Advance")
                        {
                            Panel panel1 = tabPage.Controls[0] as Panel;
                            if (panel1 == null) return;
                            foreach (Control control in panel1.Controls)
                            {
                                var vT = control as V6VvarTextBox;
                                if (vT != null && !string.IsNullOrEmpty(vT.VVar))
                                {
                                    vT.ExistRowInTable();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CheckVvarTextBox", ex);
            }
        }

        public bool ValidateNgayCt(string maCt, DateTimePicker dateNgayCT)
        {
            try
            {
                string message;
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

        public bool ValidateMasterData(V6InvoiceBase Invoice)
        {
            var v6ValidConfig = ConfigManager.GetV6ValidConfig(Invoice.Mact, 1);
            
            if (v6ValidConfig != null && v6ValidConfig.HaveInfo)
            {
                var a_fields = v6ValidConfig.A_field.Split(',');
                foreach (string field in a_fields)
                {
                    var control = V6ControlFormHelper.GetControlByAccessibleName(this, field);
                    if (control is V6DateTimeColor)
                    {
                        if (((V6DateTimeColor)control).Value == null)
                        {
                            this.ShowWarningMessage(string.Format("{0}: [{1}] {2}", V6Text.Text("CHUANHAPGIATRI"), field, V6Text.FieldCaption(field)));
                            control.Focus();
                            return false;
                        }
                    }
                    else if (control is V6NumberTextBox)
                    {
                        if (((V6NumberTextBox)control).Value == 0)
                        {
                            this.ShowWarningMessage(string.Format("{0}: [{1}] {2}", V6Text.Text("CHUANHAPGIATRI"), field, V6Text.FieldCaption(field)));
                            control.Focus();
                            return false;
                        }
                    }
                    else if (control is TextBox)
                    {
                        if (string.IsNullOrEmpty(control.Text))
                        {
                            this.ShowWarningMessage(string.Format("{0}: [{1}] {2}", V6Text.Text("CHUANHAPGIATRI"), field, V6Text.FieldCaption(field)));
                            control.Focus();
                            return false;
                        }
                    }
                }

                //Trường dữ liệu cần nhập đúng
                string firstField = null, error = null;
                var a_fields3 = ObjectAndString.SplitStringBy(v6ValidConfig.A_field3, ';');
                foreach (string afield3 in a_fields3)
                {
                    // afield3 = Table|filter:*field-data?alvt.lo_yn=1,field2... // data?alvt.lo_yn=1
                    // Table|filter, Nếu không có | thì không có filer.
                    // *field-data?alvt.lo_yn=1 field_data_where trường kiểm tra, trường dữ liệu, filter
                    // nếu check count data where ? = 0 thì bỏ qua. Nếu có check tồn tại field = detail[data]
                    int index = afield3.IndexOf(':');
                    string table_filter = afield3.Substring(0, index);
                    var field_data_list = ObjectAndString.SplitStringBy(afield3.Substring(index + 1), ',');
                    string table = table_filter;
                    string init_filter = null;
                    if (table_filter.Contains("|"))
                    {
                        index = table_filter.IndexOf('|');
                        table = table_filter.Substring(0, index);
                        init_filter = table_filter.Substring(index + 1);
                    }

                    //foreach (DataRow row in AD.Rows) // Chỉ check 1row (detail2_data)
                    {
                        foreach (string field_data_where in field_data_list)
                        {
                            string star_field = "", data = ""; // *field, sau xử lý sẽ mất *
                            string description = null;
                            IDictionary<string, object> keys = new Dictionary<string, object>();
                            index = field_data_where.IndexOf('-');
                            star_field = field_data_where.Substring(0, index);
                            bool star = false; // bắt buộc có dữ liệu
                            if (star_field.StartsWith("*"))
                            {
                                star = true;
                                star_field = star_field.Substring(1);
                            }
                            string data_where = field_data_where.Substring(index + 1);
                            data = data_where;
                            index = data_where.IndexOf('?');
                            if (index > 0) // Nếu có ?
                            {
                                data = data_where.Substring(0, index);
                                var where = data_where.Substring(index + 1);

                                //check where table.whereclause
                                var checkwhere = where.Split('.');
                                string where_clause = "" + data + "='"
                                                      + V6ControlFormHelper.GetControlValue(GetControlByAccessibleName(data))
                                                      + "' and " + checkwhere[1];
                                int count = V6BusinessHelper.SelectCount(checkwhere[0], "*", where_clause);
                                if (count == 0) goto next_row;
                            }
                            // Check field_data valid in table
                            object o = V6ControlFormHelper.GetControlValue(GetControlByAccessibleName(data));
                            if (!star && o.ToString().Trim() == "") goto next_row; // bỏ qua không kiểm tra

                            keys.Add(star_field, o);
                            description += string.Format("\r\n{0}:{1}={2}", table, star_field, o);

                            bool exist = V6BusinessHelper.CheckDataExist(table, keys, init_filter);
                            if (!exist)
                            {
                                error += (error != null && error.Length > 1 ? "\r\n" : "") + V6Text.Wrong + description;
                                if (firstField == null) firstField = data;
                            }
                        }


                    next_row:
                        ;
                    }
                }

                if (error != null)
                {
                    var control = GetControlByAccessibleName(firstField);
                    if (control != null) control.Focus();
                    this.ShowWarningMessage(error);
                    return false;
                }
            }
            else
            {
                V6ControlFormHelper.SetStatusText(V6Text.Text("NoInfo") + " V6Valid!");
            }
            return true;
        }

        /// <summary>
        /// <para>Kiểm tra dữ liệu chi tiết hợp lệ quy định trong V6Valid.</para>
        /// <para>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</para>
        /// </summary>
        /// <param name="detail1"></param>
        /// <param name="Invoice"></param>
        /// <param name="DETAIL_DATA"></param>
        /// <param name="firstField"></param>
        /// <returns>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</returns>
        public string ValidateDetailData(HD_Detail detail1, V6InvoiceBase Invoice, IDictionary<string, object> DETAIL_DATA, out string firstField)
        {
            string error = null;
            firstField = null;
            try
            {
                string inv = "";
                All_Objects["detail1"] = detail1;
                All_Objects["DETAILDATA"] = DETAIL_DATA;
                inv += InvokeFormEvent(FormDynamicEvent.VALIDATEDETAILDATA);
                V6Tag invTag = new V6Tag(inv);
                if (invTag.Cancel)
                {
                    firstField = invTag.Field;
                    error += invTag.DescriptionLang(V6Setting.IsVietnamese);
                    return error;
                }

                var config = ConfigManager.GetV6ValidConfig(Invoice.Mact, 2);
                
                if (config != null && config.HaveInfo)
                {
                    //Trường bắt buột nhập dữ liệu.
                    var a_fields = ObjectAndString.SplitString(config.A_field);
                    foreach (string field in a_fields)
                    {
                        string FIELD = field.Trim().ToUpper();
                        string label = FIELD;
                        if (!DETAIL_DATA.ContainsKey(FIELD))
                        {
                            //error += string.Format("{0}: [{1}]\n", V6Text.NoData, FIELD);
                            continue;
                        }

                        V6ColumnStruct columnS = Invoice.ADStruct[FIELD];
                        object value = DETAIL_DATA[FIELD];
                        
                        if (ObjectAndString.IsDateTimeType(columnS.DataType))
                        {
                            if (value == null)
                            {
                                var lbl = detail1.GetControlByName("lbl" + FIELD);
                                if (lbl != null) label = lbl.Text;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                        else if (ObjectAndString.IsNumberType(columnS.DataType))
                        {
                            if (ObjectAndString.ObjectToDecimal(value) == 0)
                            {
                                var lbl = detail1.GetControlByName("lbl" + FIELD);
                                if (lbl != null) label = lbl.Text;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                        else // string
                        {
                            if (("" + value).Trim() == "")
                            {
                                var lbl = detail1.GetControlByName("lbl" + FIELD);
                                if (lbl != null) label = lbl.Text;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                    }

                    //Trường vvar
                    var a_field2s = ObjectAndString.SplitString(config.A_field2);
                    foreach (string field2 in a_field2s)
                    {
                        var vvar = GetControlByAccessibleName(field2) as V6VvarTextBox;
                        if (vvar != null)
                        {
                            if (vvar.CheckNotEmpty && vvar.CheckOnLeave && !vvar.ExistRowInTable(true))
                            {
                                error += V6Text.Wrong + " [" + field2 + "]\n";
                            }
                        }
                    }

                    //Trường dữ liệu cần nhập đúng
                    var a_fields3 = ObjectAndString.SplitStringBy(config.A_field3, ';');
                    foreach (string afield3 in a_fields3)
                    {
                        // afield3 = Table|filter:*field-data?alvt.lo_yn=1,field2... // data?alvt.lo_yn=1
                        // Table|filter, Nếu không có | thì không có filer.
                        // *field-data?alvt.lo_yn=1 field_data_where trường kiểm tra, trường dữ liệu, filter
                        // nếu check count data where ? = 0 thì bỏ qua. Nếu có check tồn tại field = detail[data]
                        int index = afield3.IndexOf(':');
                        string table_filter = afield3.Substring(0, index);
                        var field_data_list = ObjectAndString.SplitStringBy(afield3.Substring(index + 1), ',');
                        string table = table_filter;
                        string init_filter = null;
                        if (table_filter.Contains("|"))
                        {
                            index = table_filter.IndexOf('|');
                            table = table_filter.Substring(0, index);
                            init_filter = table_filter.Substring(index + 1);
                        }

                        //foreach (DataRow row in AD.Rows) // Chỉ check 1row (detail2_data)
                        {
                            foreach (string field_data_where in field_data_list)
                            {
                                string star_field = "", data = ""; // *field, sau xử lý sẽ mất *
                                string description = null;
                                IDictionary<string, object> keys = new Dictionary<string, object>();
                                index = field_data_where.IndexOf('-');
                                star_field = field_data_where.Substring(0, index);
                                bool star = false; // bắt buộc có dữ liệu
                                if (star_field.StartsWith("*"))
                                {
                                    star = true;
                                    star_field = star_field.Substring(1);
                                }
                                string data_where = field_data_where.Substring(index + 1);
                                data = data_where;
                                index = data_where.IndexOf('?');
                                if (index > 0) // Nếu có ?
                                {
                                    data = data_where.Substring(0, index);
                                    var where = data_where.Substring(index + 1);

                                    //check where table.whereclause
                                    var checkwhere = where.Split('.');
                                    string where_clause = "" + data + "='" + DETAIL_DATA[data.ToUpper()] + "' and " + checkwhere[1];
                                    int count = V6BusinessHelper.SelectCount(checkwhere[0], "*", where_clause);
                                    if (count == 0) goto next_row;
                                }
                                // Check field_data valid in table
                                object o = DETAIL_DATA[data.ToUpper()];
                                if (!star && o.ToString().Trim() == "") goto next_row; // bỏ qua không kiểm tra

                                keys.Add(star_field, o);
                                description += string.Format("\r\n{0}:{1}={2}", table, star_field, o);

                                bool exist = V6BusinessHelper.CheckDataExist(table, keys, init_filter);
                                if (!exist)
                                {
                                    error += (error != null && error.Length > 1 ? "\r\n" : "") + V6Text.Wrong + description;
                                    if (firstField == null) firstField = data;
                                }
                            }

                            
                        next_row:
                            ;
                        }
                    }
                }
                else
                {
                    SetStatusText(V6Text.Text("NoInfo") + " V6Valid!");
                }
            }
            catch (Exception ex)
            {
                //error += ex.Message;//Lỗi chương trình không liên quan lỗi nhập liệu
                this.WriteExLog(GetType() + ".ValidateData_Detail " + _sttRec, ex);
            }
            return error;
        }
        
        public string ValidateDetailData_Row(V6InvoiceBase Invoice, DataGridViewRow grow, out string firstField)
        {
            string error = null;
            firstField = null;
            var gridView = grow.DataGridView;
            try
            {
                IDictionary<string, object> DETAIL_DATA = grow.ToDataDictionary();
                string inv = "";
                //All_Objects["detail1"] = detail1;
                All_Objects["DETAILDATA"] = DETAIL_DATA;
                inv += InvokeFormEvent(FormDynamicEvent.VALIDATEDETAILDATA);
                V6Tag invTag = new V6Tag(inv);
                if (invTag.Cancel)
                {
                    firstField = invTag.Field;
                    error += invTag.DescriptionLang(V6Setting.IsVietnamese);
                    SET_ROW_VALIDATE(grow, false);
                    return error;
                }

                var config = ConfigManager.GetV6ValidConfig(Invoice.Mact, 2);
                
                if (config != null && config.HaveInfo)
                {
                    //Trường bắt buột nhập dữ liệu.
                    var a_fields = ObjectAndString.SplitString(config.A_field);
                    foreach (string field in a_fields)
                    {
                        string FIELD = field.Trim().ToUpper();
                        string label = FIELD;
                        if (!DETAIL_DATA.ContainsKey(FIELD))
                        {
                            //error += string.Format("{0}: [{1}]\n", V6Text.NoData, FIELD);
                            continue;
                        }

                        V6ColumnStruct columnS = Invoice.ADStruct[FIELD];
                        object value = DETAIL_DATA[FIELD];
                        
                        if (ObjectAndString.IsDateTimeType(columnS.DataType))
                        {
                            if (value == null)
                            {
                                var column = gridView.Columns[FIELD];
                                if (column != null) label = column.HeaderText;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                        else if (ObjectAndString.IsNumberType(columnS.DataType))
                        {
                            if (ObjectAndString.ObjectToDecimal(value) == 0)
                            {
                                var column = gridView.Columns[FIELD];
                                if (column != null) label = column.HeaderText;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                        else // string
                        {
                            if (("" + value).Trim() == "")
                            {
                                var column = gridView.Columns[FIELD];
                                if (column != null) label = column.HeaderText;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                    }

                    //Trường vvar
                    var a_field2s = ObjectAndString.SplitString(config.A_field2);
                    foreach (string field2 in a_field2s)
                    {
                        var vvar = GetControlByAccessibleName(field2) as V6VvarTextBox;
                        if (vvar != null)
                        {
                            if (vvar.CheckNotEmpty && vvar.CheckOnLeave && !vvar.ExistRowInTable(true))
                            {
                                error += V6Text.Wrong + " [" + field2 + "]\n";
                            }
                        }
                    }

                    //Trường dữ liệu cần nhập đúng
                    var a_fields3 = ObjectAndString.SplitStringBy(config.A_field3, ';');
                    foreach (string afield3 in a_fields3)
                    {
                        // afield3 = Table|filter:*field-data?alvt.lo_yn=1,field2... // data?alvt.lo_yn=1
                        int index = afield3.IndexOf(':');
                        string table_filter = afield3.Substring(0, index);
                        var field_data_list = ObjectAndString.SplitStringBy(afield3.Substring(index + 1), ',');
                        string table = table_filter;
                        string init_filter = null;
                        if (table_filter.Contains("|"))
                        {
                            index = table_filter.IndexOf('|');
                            table = table_filter.Substring(0, index);
                            init_filter = table_filter.Substring(index + 1);
                        }

                        //foreach (DataRow row in AD2.Rows) // Chỉ check 1row (detail2_data)
                        {
                            foreach (string field_data_where in field_data_list)
                            {
                                string star_field = "", data = ""; // *field, sau xử lý sẽ mất *
                                string description = null;
                                IDictionary<string, object> keys = new Dictionary<string, object>();
                                index = field_data_where.IndexOf('-');
                                star_field = field_data_where.Substring(0, index);
                                bool star = false; // bắt buộc có dữ liệu
                                if (star_field.StartsWith("*"))
                                {
                                    star = true;
                                    star_field = star_field.Substring(1);
                                }
                                string data_where = field_data_where.Substring(index + 1);
                                data = data_where;
                                index = data_where.IndexOf('?');
                                if (index > 0)
                                {
                                    data = data_where.Substring(0, index);
                                    var where = data_where.Substring(index + 1);

                                    //check where table.whereclause
                                    var checkwhere = where.Split('.');
                                    string where_clause = "" + data + "='" + DETAIL_DATA[data.ToUpper()] + "' and " + checkwhere[1];
                                    int count = V6BusinessHelper.SelectCount(checkwhere[0], "*", where_clause);
                                    if (count == 0) goto next_row;
                                }
                                // Check field_data valid in table
                                object o = DETAIL_DATA[data.ToUpper()];
                                if (!star && o.ToString().Trim() == "") goto next_row; // bỏ qua không kiểm tra

                                keys.Add(star_field, o);
                                description += string.Format("\r\n{0}:{1}={2}", table, star_field, o);

                                bool exist = V6BusinessHelper.CheckDataExist(table, keys, init_filter);
                                if (!exist)
                                {
                                    error += (error != null && error.Length > 1 ? "\r\n" : "") + V6Text.Wrong + description;
                                    if (firstField == null) firstField = data;
                                }
                            }

                            next_row:
                            ;
                        }
                    }
                }
                else
                {
                    SetStatusText(V6Text.Text("NoInfo") + " V6Valid!");
                }
            }
            catch (Exception ex)
            {
                //error += ex.Message;//Lỗi chương trình không liên quan lỗi nhập liệu
                this.WriteExLog(GetType() + ".ValidateData_Detail " + _sttRec, ex);
            }
            SET_ROW_VALIDATE(grow, string.IsNullOrEmpty(error));
            return error;
        }

        public bool IS_NOT_VALIDATE(DataGridViewRow grow)
        {
            return ObjectAndString.ObjectToBool(GetTagDicValue(grow.Tag, "NOTVALIDATED"));
        }

        public void SET_ROW_VALIDATE(DataGridViewRow grow, bool is_validated)
        {
            SetRowTagDicValue(grow, "NOTVALIDATED", !is_validated);
        }

        public object GetTagDicValue(object tag, string KEY)
        {
            if (tag is IDictionary<string, object>)
            {
                var tagData = (IDictionary<string, object>)tag;
                if (tagData.ContainsKey(KEY)) return tagData[KEY];
            }
            return null;
        }

        public void SetRowTagDicValue(DataGridViewRow grow, string KEY, object value)
        {
            IDictionary<string, object> tagData;

            if (grow.Tag is IDictionary<string, object>)
            {
                tagData = ((IDictionary<string, object>)grow.Tag);
            }
            else
            {
                tagData = new SortedDictionary<string, object>();
                grow.Tag = tagData;
            }

            tagData[KEY] = value;
        }

        /// <summary>
        /// <para>Kiểm tra dữ liệu chi tiết hợp lệ khi validate master.</para>
        /// <para>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</para>
        /// </summary>
        /// <returns>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</returns>
        public string ValidateDetailData_InMaster()
        {
            string error = null;
            try
            {
                var config = ConfigManager.GetV6ValidConfig(_invoice.Mact, 2);

                if (config != null && config.HaveInfo)
                {
                    //Trường dữ liệu cần nhập đúng
                    var a_fields3 = ObjectAndString.SplitStringBy(config.A_field3, ';');
                    foreach (string afield3 in a_fields3)
                    {
                        // afield3 = Table|filter:*field-data?alvt.lo_yn=1,field2... // data?alvt.lo_yn=1
                        int index = afield3.IndexOf(':');
                        string table_filter = afield3.Substring(0, index);
                        var field_data_list = ObjectAndString.SplitStringBy(afield3.Substring(index + 1), ',');
                        string table = table_filter;
                        string init_filter = null;
                        if (table_filter.Contains("|"))
                        {
                            index = table_filter.IndexOf('|');
                            table = table_filter.Substring(0, index);
                            init_filter = table_filter.Substring(index+1);
                        }
                        
                        foreach (DataRow row in AD.Rows)
                        {
                            foreach (string field_data_where in field_data_list)
                            {
                                string star_field = "", data = ""; // *field, sau xử lý sẽ mất *
                                string description = null;
                                IDictionary<string, object> keys = new Dictionary<string, object>();
                                index = field_data_where.IndexOf('-');
                                star_field = field_data_where.Substring(0, index);
                                bool star = false; // bắt buộc có dữ liệu
                                if (star_field.StartsWith("*"))
                                {
                                    star = true;
                                    star_field = star_field.Substring(1);
                                }
                                string data_where = field_data_where.Substring(index + 1);
                                data = data_where;
                                index = data_where.IndexOf('?');
                                if (index > 0)
                                {
                                    data = data_where.Substring(0, index);
                                    var where = data_where.Substring(index + 1);

                                    //check where table.whereclause
                                    var checkwhere = where.Split('.');
                                    string where_clause = "" + data + "='" + row[data] + "' and " + checkwhere[1];
                                    int count = V6BusinessHelper.SelectCount(checkwhere[0], "*", where_clause);
                                    if (count == 0) goto next_row;
                                }
                                // Check field_data valid in table
                                object o = row[data];
                                if (!star && o.ToString().Trim() == "") goto next_row; // bỏ qua không kiểm tra
                                
                                keys.Add(star_field, o);
                                description += string.Format("\r\n{0}:{1}={2}", table, star_field, o);
                                bool exist = V6BusinessHelper.CheckDataExist(table, keys, init_filter);
                                if (!exist)
                                {
                                    error += (error != null && error.Length > 1 ? "\r\n" : "") + V6Text.Wrong + description;
                                    //if (firstField == null) firstField = data;
                                }
                            }

                            next_row:
                            ;
                        }
                    }
                }
                else
                {
                    SetStatusText(V6Text.Text("NoInfo") + " V6Valid!");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateDetailData_InMaster " + _sttRec, ex);
            }
            return error;
        }
        
        /// <summary>
        /// <para>Kiểm tra dữ liệu chi tiết thuế hợp lệ quy định trong V6Valid.</para>
        /// <para>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</para>
        /// </summary>
        /// <param name="detail2"></param>
        /// <param name="Invoice"></param>
        /// <param name="DETAIL2_DATA"></param>
        /// <param name="firstField"></param>
        /// <returns></returns>
        public string ValidateDetail2Data(HD_Detail detail2, V6InvoiceBase Invoice, IDictionary<string, object> DETAIL2_DATA, out string firstField)
        {
            string error = "";
            firstField = null;
            try
            {
                string inv = "";
                All_Objects["detail2"] = detail2;
                All_Objects["DETAIL2DATA"] = DETAIL2_DATA;
                inv += InvokeFormEvent(FormDynamicEvent.VALIDATEDETAIL2DATA);
                V6Tag invTag = new V6Tag(inv);
                if (invTag.Cancel)
                {
                    firstField = invTag.Field;
                    error += invTag.DescriptionLang(V6Setting.IsVietnamese);
                    return error;
                }

                // Check thuế code cứng
                IDictionary<string, object> form_data = GetData();
                SqlParameter[] plist = {new SqlParameter("@status", Mode == V6Mode.Add ? "M" : "S"), 
new SqlParameter("@kieu_post", form_data["KIEU_POST"]), 
new SqlParameter("@SO_CT0", DETAIL2_DATA["SO_CT0"]),
new SqlParameter("@SO_SERI0", DETAIL2_DATA["SO_SERI0"]),
new SqlParameter("@MA_SO_THUE0", DETAIL2_DATA["MA_SO_THUE"]),
new SqlParameter("@MA_KH0", DETAIL2_DATA["MA_KH"]), // KHACH_HANG_0
new SqlParameter("@NGAY_CT0", DETAIL2_DATA["NGAY_CT0"]),
new SqlParameter("@MA_CT", _invoice.Mact),
new SqlParameter("@STT_REC", _sttRec),
new SqlParameter("@MODE", detail2.MODE == V6Mode.Add ? "M" : "S"),
new SqlParameter("@USER_ID", V6Login.UserId) };
                DataTable data30 = V6BusinessHelper.ExecuteProcedure("VPA_CHECK_EXIST_ARV30", plist).Tables[0];
                DataRow row30 = data30.Rows[0];
                //chk_yn varchar(1),		mess nvarchar(max),		mess2 nvarchar(max)
                string result = string.Format("Cancel:{0};Field:FIELD;DescriptionV:{1};DescriptionE:{2}",
                     row30["chk_yn"], row30["mess"], row30["mess2"]);
                //V6Message.Show(result, thisForm);
                V6Tag invTag2 = new V6Tag(result);
                if (invTag2.Cancel)
                {
                    firstField = invTag.Field;
                    error += invTag.DescriptionLang(V6Setting.IsVietnamese);
                    return error;
                }


                var config = ConfigManager.GetV6ValidConfig(Invoice.Mact, 4);
                
                if (config != null && config.HaveInfo)
                {
                    //Trường bắt buột nhập dữ liệu.
                    var a_fields = ObjectAndString.SplitString(config.A_field);
                    foreach (string field in a_fields)
                    {
                        string FIELD = field.Trim().ToUpper();
                        string label = FIELD;
                        if (!DETAIL2_DATA.ContainsKey(FIELD))
                        {
                            //error += string.Format("{0}: [{1}]\n", V6Text.NoData, FIELD);
                            continue;
                        }

                        V6ColumnStruct columnS = Invoice.AD2Struct[FIELD];
                        object value = DETAIL2_DATA[FIELD];
                        
                        if (ObjectAndString.IsDateTimeType(columnS.DataType))
                        {
                            if (value == null)
                            {
                                var lbl = detail2.GetControlByName("lbl" + FIELD);
                                if (lbl != null) label = lbl.Text;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                        else if (ObjectAndString.IsNumberType(columnS.DataType))
                        {
                            if (ObjectAndString.ObjectToDecimal(value) == 0)
                            {
                                var lbl = detail2.GetControlByName("lbl" + FIELD);
                                if (lbl != null) label = lbl.Text;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                        else // string
                        {
                            if (("" + value).Trim() == "")
                            {
                                var lbl = detail2.GetControlByName("lbl" + FIELD);
                                if (lbl != null) label = lbl.Text;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                    }

                    //Trường vvar
                    var a_field2s = ObjectAndString.SplitString(config.A_field2);
                    foreach (string field2 in a_field2s)
                    {
                        var vvar = GetControlByAccessibleName(field2) as V6VvarTextBox;
                        if (vvar != null)
                        {
                            if (vvar.CheckNotEmpty && vvar.CheckOnLeave && !vvar.ExistRowInTable(true))
                            {
                                error += V6Text.Wrong + " [" + field2 + "]\n";
                            }
                        }
                    }

                    //Trường dữ liệu cần nhập đúng
                    var a_fields3 = ObjectAndString.SplitStringBy(config.A_field3, ';');
                    foreach (string afield3 in a_fields3)
                    {
                        // afield3 = Table|filter:*field-data?alvt.lo_yn=1,field2... // data?alvt.lo_yn=1
                        int index = afield3.IndexOf(':');
                        string table_filter = afield3.Substring(0, index);
                        var field_data_list = ObjectAndString.SplitStringBy(afield3.Substring(index + 1), ',');
                        string table = table_filter;
                        string init_filter = null;
                        if (table_filter.Contains("|"))
                        {
                            index = table_filter.IndexOf('|');
                            table = table_filter.Substring(0, index);
                            init_filter = table_filter.Substring(index + 1);
                        }

                        //foreach (DataRow row in AD2.Rows) // Chỉ check 1row (detail2_data)
                        {
                            foreach (string field_data_where in field_data_list)
                            {
                                string star_field = "", data = ""; // *field, sau xử lý sẽ mất *
                                string description = null;
                                IDictionary<string, object> keys = new Dictionary<string, object>();
                                index = field_data_where.IndexOf('-');
                                star_field = field_data_where.Substring(0, index);
                                bool star = false; // bắt buộc có dữ liệu
                                if (star_field.StartsWith("*"))
                                {
                                    star = true;
                                    star_field = star_field.Substring(1);
                                }
                                string data_where = field_data_where.Substring(index + 1);
                                data = data_where;
                                index = data_where.IndexOf('?');
                                if (index > 0)
                                {
                                    data = data_where.Substring(0, index);
                                    var where = data_where.Substring(index + 1);

                                    //check where table.whereclause
                                    var checkwhere = where.Split('.');
                                    string where_clause = "" + data + "='" + DETAIL2_DATA[data.ToUpper()] + "' and " + checkwhere[1];
                                    int count = V6BusinessHelper.SelectCount(checkwhere[0], "*", where_clause);
                                    if (count == 0) goto next_row;
                                }
                                // Check field_data valid in table
                                object o = DETAIL2_DATA[data.ToUpper()];
                                if (!star && o.ToString().Trim() == "") goto next_row; // bỏ qua không kiểm tra

                                keys.Add(star_field, o);
                                description += string.Format("\r\n{0}:{1}={2}", table, star_field, o);
                                bool exist = V6BusinessHelper.CheckDataExist(table, keys, init_filter);
                                if (!exist)
                                {
                                    error += (error != null && error.Length > 1 ? "\r\n" : "") + V6Text.Wrong + description;
                                    if (firstField == null) firstField = data;
                                }
                            }

                            next_row:
                            ;
                        }
                    }
                }
                else
                {
                    SetStatusText(V6Text.Text("NoInfo") + " V6Valid!");
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

        public void XuLyHienThiChietKhau_PhieuNhap(bool ckChung, bool suaTienCk,
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

        /// <summary>
        /// //Lay thong tin gan du lieu 20170320
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="itemID"></param>
        /// <param name="controlName"></param>
        /// <param name="controlData"></param>
        public void SetDefaultDataReference(V6InvoiceBase invoice, string itemID, string controlName, DataRow controlData)
        {
            //Lay thong tin gan du lieu 20161129
            //var infos =   invoice.LoadDataReferenceInfo(V6Setting.Language, ItemID);
            var infosDic = GetDefaultDataAndTagInfoData(V6Setting.Language, 1, invoice.Mact, "", itemID, "nhom='02'");
            //SetDefaultDataInfoToForm(infosDic);
            
            //Chuẩn bị dữ liệu để gán lên form
            SortedDictionary<string, object> someData = new SortedDictionary<string, object>();
            foreach (KeyValuePair<string, DefaultValueAndTagInfo> item0 in infosDic)
            {
                var item = item0.Value;
                if (item.Type1 == "0")
                {
                    //Value null vẫn gán.
                }
                else if (item.Type1 == "2")
                {
                    //Kiểm tra value trên form theo Name. rỗng mới gán
                    var fValue = ObjectAndString.ObjectToString(V6ControlFormHelper.GetFormValue(this, item.AName));
                    if (!string.IsNullOrEmpty(fValue)) continue;
                }

                if (item.Value.StartsWith(controlName + "."))//Lấy dữ liệu theo trường nào đó trong Data
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
                        someData[item.AName.ToUpper()] = getValue;
                    }
                }
            }

            SetSomeData(someData);
        }

        /// <summary>
        /// Thêm chữ (Thêm) khi nhấn shift cho menuItem.
        /// </summary>
        /// <param name="items"></param>
        public void FixMenuChucNangItemShiftText(params ToolStripMenuItem[] items)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            foreach (ToolStripMenuItem item in items)
            {
                if (shift)
                {
                    item.Text = item.Text.WithEnd(V6Text.AddInParentheses);
                }
                else
                {
                    item.Text = item.Text.RemoveEnd(V6Text.AddInParentheses);
                }
            }
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
        protected void FixTyGia(DataTable detailData, DataRow row, decimal ty_gia, string fieldTien, string fieldTienNt, int round)
        {
            if (detailData.Columns.Contains(fieldTien) && detailData.Columns.Contains(fieldTienNt))
            {
                decimal temp = ObjectAndString.ObjectToDecimal(row[fieldTienNt]);
                if (temp != 0)
                    row[fieldTien] = V6BusinessHelper.Vround(temp * ty_gia, round);
            }
        }

        protected void FixTyGiaDetail(DataTable detailData, HD_Detail detailControl, decimal ty_gia, string fieldTien, string fieldTienNt, int round)
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
        private void FixMaNt(DataTable detailData, DataRow row, string fieldTienNt, int round)
        {
            if (detailData.Columns.Contains(fieldTienNt))
            {
                decimal temp = ObjectAndString.ObjectToDecimal(row[fieldTienNt]);
                if (temp != 0)
                    row[fieldTienNt] = V6BusinessHelper.Vround(temp, round);
            }
        }
        private void FixMaNtDetail(DataTable detailData, HD_Detail detailControl,string fieldTienNt, int round)
        {
            if ( detailData.Columns.Contains(fieldTienNt))
            {
                
                Control controlNT = detailControl.GetControlByAccessibleName(fieldTienNt);

                if (controlNT != null)
                {
                    decimal valueNT = ObjectAndString.ObjectToDecimal(V6ControlFormHelper.GetControlValue(controlNT));
                    if (valueNT != 0)
                    {
                        V6ControlFormHelper.SetControlValue(controlNT, V6BusinessHelper.Vround(valueNT, round));
                    }
                }
            }
        }

        public virtual void XuLyThayDoiTyGia(V6NumberTextBox txtTyGia, CheckBox chkSuaTien)
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
                        FixTyGia(AD, row, ty_gia, "TT", "TT_NT", M_ROUND);

                        FixTyGia(AD, row, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                    HD_Detail detailControl = GetControlByName("detail1") as HD_Detail;
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
                        FixTyGiaDetail(AD, detailControl, ty_gia, "TT", "TT_NT", M_ROUND);

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
                    HD_Detail detailControl = GetControlByName("detail2") as HD_Detail;
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
                    HD_Detail detailControl = GetControlByName("detail3") as HD_Detail;
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
        public void XuLyThayDoiMaNt(V6NumberTextBox txtTyGia, CheckBox chkSuaTien, string MaNt, string MaNt0)
        {
            try
            {

                // Tuanmh 04/01/2020 - xu ly khi (MaNt= MaNt0) 
                var ty_gia = txtTyGia.Value;

                if (MaNt != MaNt0)
                {
                    M_ROUND_NT = V6Setting.RoundTienNt;
                    M_ROUND = V6Setting.RoundTien;
                    M_ROUND_GIA_NT = V6Setting.RoundGiaNt;
                    M_ROUND_GIA = V6Setting.RoundGia;
                    return;
                }
                else
                {
                    M_ROUND = V6Setting.RoundTien;
                    M_ROUND_GIA = V6Setting.RoundGia;
                    M_ROUND_NT = M_ROUND;
                    M_ROUND_GIA_NT = M_ROUND_GIA;
                }

                if (ty_gia == 0 || chkSuaTien.Checked)
                {
                    return;
                }

                {

                    foreach (DataRow row in AD.Rows)
                    {
                        FixMaNt(AD, row, "Tien_nt", M_ROUND);
                        FixMaNt(AD, row, "Tien_nt2", M_ROUND);
                        FixMaNt(AD, row, "Tien1_nt", M_ROUND);
                        FixMaNt(AD, row, "Tien_vc_nt", M_ROUND);
                        FixMaNt(AD, row, "TIEN_NT0", M_ROUND);
                        FixMaNt(AD, row, "Thue_nt", M_ROUND);
                        FixMaNt(AD, row, "CP_NT", M_ROUND);
                        FixMaNt(AD, row, "GIA_NT", M_ROUND_GIA);
                        FixMaNt(AD, row, "GIA_NT01", M_ROUND_GIA);
                        FixMaNt(AD, row, "GIA_NT1", M_ROUND_GIA);
                        FixMaNt(AD, row, "GIA_NT2", M_ROUND_GIA);
                        FixMaNt(AD, row, "GIA_NT21", M_ROUND_GIA);

                        FixMaNt(AD, row, "GIA_NT0", M_ROUND_GIA);
                        FixMaNt(AD, row, "NK_NT", M_ROUND);
                        FixMaNt(AD, row, "CK_NT", M_ROUND);
                        FixMaNt(AD, row, "GG_NT", M_ROUND);

                        FixMaNt(AD, row, "PS_NO_NT", M_ROUND);
                        FixMaNt(AD, row, "PS_CO_NT", M_ROUND);
                    }
                    HD_Detail detailControl = GetControlByName("detail1") as HD_Detail;
                    if (detailControl != null &&
                        (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        //...
                        FixMaNtDetail(AD, detailControl, "Tien_nt", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "Tien_nt2", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "Tien1_nt", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "Tien_vc_nt", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "TIEN_NT0", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "Thue_nt", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "CP_NT", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "GIA_NT", M_ROUND_GIA);
                        FixMaNtDetail(AD, detailControl, "GIA_NT01", M_ROUND_GIA);
                        FixMaNtDetail(AD, detailControl, "GIA_NT1", M_ROUND_GIA);
                        FixMaNtDetail(AD, detailControl, "GIA_NT2", M_ROUND_GIA);
                        FixMaNtDetail(AD, detailControl, "GIA_NT21", M_ROUND_GIA);

                        FixMaNtDetail(AD, detailControl, "GIA_NT0", M_ROUND_GIA);
                        FixMaNtDetail(AD, detailControl, "NK_NT", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "CK_NT", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "GG_NT", M_ROUND);

                        FixMaNtDetail(AD, detailControl, "PS_NO_NT", M_ROUND);
                        FixMaNtDetail(AD, detailControl, "PS_CO_NT", M_ROUND);
                    }

                }

                if (AD2 != null)
                {
                    foreach (DataRow row in AD2.Rows)
                    {
                        FixMaNt(AD2, row, "t_tien_nt", M_ROUND);
                        FixMaNt(AD2, row, "t_thue_nt", M_ROUND);
                        FixMaNt(AD2, row, "t_tt_nt", M_ROUND);
                    }
                    HD_Detail detailControl = GetControlByName("detail2") as HD_Detail;
                    if (detailControl != null && (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        FixMaNtDetail(AD2, detailControl,  "t_tien_nt", M_ROUND);
                        FixMaNtDetail(AD2, detailControl,  "t_thue_nt", M_ROUND);
                        FixMaNtDetail(AD2, detailControl,  "t_tt_nt", M_ROUND);
                    }
                }

                if (AD3 != null)
                {
                    foreach (DataRow row in AD3.Rows)
                    {
                        FixMaNt(AD3, row,  "PS_NO_NT", M_ROUND);
                        FixMaNt(AD3, row,  "PS_CO_NT", M_ROUND);
                    }
                    HD_Detail detailControl = GetControlByName("detail3") as HD_Detail;
                    if (detailControl != null && (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        FixMaNtDetail(AD3, detailControl, "PS_NO_NT", M_ROUND);
                        FixMaNtDetail(AD3, detailControl, "PS_CO_NT", M_ROUND);
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
        public bool ValidateData_Master_CheckTon(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            try
            {
                if (V6Options.M_CHK_XUAT == "1") return true;

                var M_CHECK_SAVE_STOCK = V6Options.GetValue("M_CHECK_SAVE_STOCK");
                string message = "";
                foreach (char c in M_CHECK_SAVE_STOCK)
                {
                    if (AD.Rows.Count > 50)
                    {
                        switch (c)
                        {
                            case '1':
                                message = CheckMakhoMavt1(Invoice, ngayCt, maKhoX);
                                if (!string.IsNullOrEmpty(message)) goto ThongBao;
                                break;
                            case '2':
                                message = CheckMakhoMavtMalo1(Invoice, ngayCt, maKhoX);
                                if (!string.IsNullOrEmpty(message)) goto ThongBao;
                                break;
                            case '3':
                                message = CheckMakhoMavtMaloMavitri1(Invoice, ngayCt, maKhoX);
                                if (!string.IsNullOrEmpty(message)) goto ThongBao;
                                break;
                        }
                    }
                    else
                    {
                        switch (c)
                        {
                            case '1':
                                message = CheckMakhoMavt50(Invoice, ngayCt, maKhoX);
                                if (!string.IsNullOrEmpty(message)) goto ThongBao;
                                break;
                            case '2':
                                message = CheckMakhoMavtMalo50(Invoice, ngayCt, maKhoX);
                                if (!string.IsNullOrEmpty(message)) goto ThongBao;
                                break;
                            case '3':
                                message = CheckMakhoMavtMaloMavitri50(Invoice, ngayCt, maKhoX);
                                if (!string.IsNullOrEmpty(message)) goto ThongBao;
                                break;
                        }
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
                ShowParentMessage(string.Format("{0}: {1}", V6Text.Text("LOICHECKTON"), ex.Message));
                return true;
            }
        }

        private string CheckMakhoMavtMaloMavitri1(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            string message = "";
            int message_count = 0;
            #region === KiemTra3 Kho,VatTu,Lo,Vitri ===
            
            foreach (DataRow row in AD.Rows)
            {
                string c_mavt = row["Ma_vt"].ToString().Trim();
                string c_makho = maKhoX ?? row["Ma_kho_i"].ToString().Trim();
                string c_malo = row["Ma_lo"].ToString().Trim();
                string c_mavitri = row["Ma_vitri"].ToString().Trim();
                //string c_mavt_makho_malo_mavitri = c_mavt + "~" + c_makho + "~" + c_malo + "~" + c_mavitri;
                decimal c_soluong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                if (c_soluong == 0) continue;
                //lay thong tin lodate cua mavt
                bool lo = false, date = false, vitri = false;
                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select("Alvt", key, "*").Data;
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
                    var data1 = Invoice.GetViTriLoDateAll(c_mavt, c_makho, c_malo, c_mavitri, _sttRec, ngayCt.Date);
                    foreach (DataRow row1 in data1.Rows)
                    {
                        var data_mavt = row1["Ma_vt"].ToString().Trim();
                        var data_makho = row1["Ma_kho"].ToString().Trim();
                        var data_malo = row1["Ma_lo"].ToString().Trim();
                        var data_mavitri = row1["Ma_vitri"].ToString().Trim();
                        var data_soluong = ObjectAndString.ObjectToDecimal(row1["Ton_dau"]);
                        if (c_mavt == data_mavt && c_makho == data_makho && c_malo == data_malo &&
                            c_mavitri == data_mavitri)
                        {
                            if (data_soluong < c_soluong)
                            {
                                message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Vitri:{5}  Tồn:{0}  Xuất:{1}\n",
                                    data_soluong, c_soluong, c_makho, c_mavt, c_malo, c_mavitri);
                                message_count++;
                            }
                        }
                    }
                }
                if (message_count == 5) return message + "...";
            } // end for AD
            
            return message;
            #endregion kho,vt,lo
        }

        private string CheckMakhoMavtMaloMavitri50(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
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
                if (c_soluong == 0) continue;
                //lay thong tin lodate cua mavt
                bool lo = false, date = false, vitri = false;
                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select("Alvt", key, "*").Data;
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


        private string CheckMakhoMavtMalo1(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            string message = "";
            int message_count = 0;
            #region === KiemTra2 Kho,VatTu,Lo ===
            
            foreach (DataRow row in AD.Rows)
            {
                string c_mavt = row["Ma_vt"].ToString().Trim();
                string c_makho = maKhoX ?? row["Ma_kho_i"].ToString().Trim();
                string c_malo = row["Ma_lo"].ToString().Trim();
                decimal c_soluong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                if (c_soluong == 0) continue;
                //lay thong tin lodate cua mavt
                bool lo = false, date = false;
                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select("Alvt", key, "*").Data;
                if (lodate_data.Rows.Count == 1)
                {
                    DataRow row0 = lodate_data.Rows[0];
                    lo = row0["Lo_yn"].ToString().Trim() == "1";
                    date = row0["Date_yn"].ToString().Trim() == "1";
                }
                else
                {
                    //tuanmh 03/11/2017
                    continue; // Next AD row.
                }
                if (lo && date)
                {
                    var data1 = Invoice.GetLoDateAll(c_mavt, c_makho, c_malo, _sttRec, ngayCt);
                    //Kiểm tra


                    if (data1 == null || data1.Rows.Count == 0)
                    {
                        message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                            0, c_soluong, c_makho, c_mavt, c_malo);
                        message_count++;
                    }
                    else
                    {
                        bool found = false;
                        foreach (DataRow row1 in data1.Rows)
                        {
                            var data_mavt = row1["Ma_vt"].ToString().Trim();
                            var data_makho = row1["Ma_kho"].ToString().Trim();
                            var data_malo = row1["Ma_lo"].ToString().Trim();
                            var data_soluong = ObjectAndString.ObjectToDecimal(row1["Ton_dau"]);
                            if (c_mavt == data_mavt && c_makho == data_makho && c_malo == data_malo)
                            {
                                found = true;
                                if (data_soluong < c_soluong)
                                {
                                    message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                                        data_soluong, c_soluong, c_makho, c_mavt, c_malo);
                                    message_count++;
                                }
                            }
                        }

                        if (!found)
                        {
                            message += string.Format("Kho:{2}  Vật tư:{3}  Lô:{4}  Tồn:{0}  Xuất:{1}\n",
                                0, c_soluong, c_makho, c_mavt, c_malo);
                            message_count++;
                        }
                    }
                }
                if (message_count == 5) return message + "...";
            } // end for AD

            return message;
            #endregion kho,vt,lo
        }

        private string CheckMakhoMavtMalo50(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
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
                if (c_soluong == 0) continue;
                //lay thong tin lodate cua mavt
                bool lo = false, date = false;
                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select("Alvt", key, "*").Data;
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

        

        private string CheckMakhoMavt1(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            string message = "";
            int message_count = 0;
            #region === Check Makho, Mavt ===

            foreach (DataRow row in AD.Rows)
            {
                string c_mavt = row["Ma_vt"].ToString().Trim();
                string c_makho = maKhoX ?? row["Ma_kho_i"].ToString().Trim();
                //string c_mavt_makho = c_mavt + "~" + c_makho;
                decimal c_soluong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                if (c_soluong == 0) continue;

                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select("Alvt", key, "*").Data;
                if (lodate_data.Rows.Count != 1)
                {
                    continue; // Bỏ qua không kiểm tra.
                    //DataRow row0 = lodate_data.Rows[0];
                    //lo = row0["Lo_yn"].ToString().Trim() == "1";
                    //date = row0["Date_yn"].ToString().Trim() == "1";
                }

                //Get dữ liệu tồn
                var data1 = Invoice.GetStockAll(c_mavt, c_makho, _sttRec, ngayCt);
                foreach (DataRow row1 in data1.Rows)
                {
                    var data_mavt = row1["Ma_vt"].ToString().Trim();
                    var data_makho = row1["Ma_kho"].ToString().Trim();
                    var data_soluong = ObjectAndString.ObjectToDecimal(row1["Ton00"]);
                    //
                    if (c_soluong < 0) // gõ âm thoải mái.
                    {
                        DoNothing();
                    }
                    else if (c_mavt == data_mavt && c_makho == data_makho)
                    {
                        if (data_soluong < c_soluong)
                        {
                            message += string.Format("Kho:{2}  Vật tư:{3}  Tồn:{0}  Xuất:{1}\n",
                                data_soluong, c_soluong, c_makho, c_mavt);
                            message_count++;
                        }
                    }
                }
                if (message_count == 5) return message + "...";
            } // end for AD

            return message;
            #endregion makho, mavt
        }

        private string CheckMakhoMavt50(V6InvoiceBase Invoice, DateTime ngayCt, string maKhoX)
        {
            if (AD.Rows.Count > 100) throw new Exception("AD>100");
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
                if (c_soluong == 0) continue;

                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("MA_VT", c_mavt);
                key.Add("VT_TON_KHO", 1);
                var lodate_data = V6BusinessHelper.Select("Alvt", key, "*").Data;
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
                    if (item.Value < 0) // gõ âm thoải mái.
                    {
                        DoNothing();
                    }
                    else if (c_mavt == data_mavt && c_makho == data_makho)
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
        public string _sttRec_In = "";
        public bool _print_flag = false;
        public int _print_flag_tick_count = 0;
        public void BasePrint(V6InvoiceBase Invoice, string sttRec_In, V6PrintMode printMode,
            decimal tongThanhToan_Value, decimal tongThanhToanNT_Value, bool closeAfterPrint, int sec = 3)
        {
            try
            {
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                bool check_print = CheckPrint(Invoice);
                if (!check_print)
                {
                    return;
                }

                if (IsViewingAnInvoice)
                {
                    if (V6Login.UserRight.AllowPrint("", Invoice.CodeMact))
                    {
                        if (printMode == V6PrintMode.DoNoThing)
                        {
                            printMode = V6PrintMode.DoNoThing;
                            if (Invoice.PrintMode == "1") printMode = V6PrintMode.AutoPrint;
                            if (Invoice.PrintMode == "2") printMode = V6PrintMode.AutoClickPrint;
                            if (Invoice.PrintMode == "3") printMode = V6PrintMode.AutoExportT;
                            if (!string.IsNullOrEmpty(Invoice.PrintModeCT))
                            {
                                if (Invoice.PrintModeCT == "0") printMode = V6PrintMode.DoNoThing;
                                if (Invoice.PrintModeCT == "1") printMode = V6PrintMode.AutoPrint;
                                if (Invoice.PrintModeCT == "2") printMode = V6PrintMode.AutoClickPrint;
                                if (Invoice.PrintModeCT == "3") printMode = V6PrintMode.AutoExportT;
                            }
                        }

                        var program = Invoice.PrintReportProcedure;
                        var repFile = Invoice.Alct["FORM"].ToString().Trim();
                        var repTitle = Invoice.Alct["TIEU_DE_CT"].ToString().Trim();
                        var repTitle2 = Invoice.Alct["TIEU_DE2"].ToString().Trim();

                        if (Invoice.AlctConfig.XtraReport != shift_is_down)
                        {
                            var inDX = new InChungTuDX(Invoice, program, program, repFile, repTitle, repTitle2,
                                "", "", "", sttRec_In);
                            inDX.TTT = tongThanhToan_Value;
                            inDX.TTT_NT = tongThanhToanNT_Value;
                            inDX.MA_NT = _maNt;
                            inDX.Dock = DockStyle.Fill;
                            inDX.PrintSuccess += (sender, stt_rec, albcConfig) =>
                            {
                                if (albcConfig.ND51 > 0) Invoice.IncreaseSl_inAM(stt_rec, AM_current);
                                if (!sender.IsDisposed) sender.Dispose();
                            };
                            inDX.PrintMode = printMode;
                            inDX.Close_after_print = closeAfterPrint;
                            inDX.ShowToForm(this, Invoice.PrintTitle, true);
                        }
                        else
                        {
                            var c = new InChungTuViewBase(Invoice, program, program, repFile, repTitle, repTitle2,
                                "", "", "", sttRec_In);
                            c.TTT = tongThanhToan_Value;
                            c.TTT_NT = tongThanhToanNT_Value;
                            c.MA_NT = _maNt;
                            c.Dock = DockStyle.Fill;
                            c.PrintSuccess += (sender, stt_rec, albcConfig) =>
                            {
                                if (albcConfig.ND51 > 0) Invoice.IncreaseSl_inAM(stt_rec, AM_current);
                                if (!sender.IsDisposed) sender.Dispose();
                            };
                            c.PrintMode = printMode;
                            c.Close_after_print = closeAfterPrint;
                            c.ShowToForm(this, Invoice.PrintTitle, true);
                        }
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
            SetStatus2Text();
        }

        public void InPhieuHachToan(V6InvoiceBase Invoice, string sttRec_In, decimal tongThanhToan_Value, decimal tongThanhToanNT_Value)
        {
            try
            {
                string program = "APRINT_HACHTOAN";
                string repFile = "APRINT_HACHTOAN";
                var repTitle = "PHIẾU HẠCH TOÁN";
                var repTitle2 = "GENERAL VOUCHER";
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if ((Invoice.AlctConfig.XtraReport && !shift_is_down) || (!Invoice.AlctConfig.XtraReport && shift_is_down))
                {
                    var c = new ReportR_DX(Invoice.CodeMact, program, program, repFile,
                        repTitle, repTitle2, "", "", "");

                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@STT_REC", sttRec_In));
                    plist.Add(new SqlParameter("@isInvoice", "0"));
                    plist.Add(new SqlParameter("@ReportFile", repFile));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    c.FilterControl.InitFilters = plist;

                    //Tạo Extra parameters.
                    SortedDictionary<string, object> parameterData = new SortedDictionary<string, object>();
                    decimal TTT = tongThanhToan_Value;
                    decimal TTT_NT = tongThanhToanNT_Value;
                    string LAN = c.LAN;
                    //string MA_NT = _maNt;
                    parameterData.Add("SOTIENVIETBANGCHU", V6BusinessHelper.MoneyToWords(TTT, LAN, V6Options.M_MA_NT0));
                    parameterData.Add("SOTIENVIETBANGCHUNT", V6BusinessHelper.MoneyToWords(TTT_NT, LAN, MA_NT));
                    c.FilterControl.RptExtraParameters = parameterData;
                    c.PrintMode = V6PrintMode.AutoLoadData;
                    c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
                }
                else
                {
                    var c = new ReportRViewBase(Invoice.CodeMact, program, program, repFile,
                        repTitle, repTitle2, "", "", "");

                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@STT_REC", sttRec_In));
                    plist.Add(new SqlParameter("@isInvoice", "0"));
                    plist.Add(new SqlParameter("@ReportFile", repFile));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    c.FilterControl.InitFilters = plist;

                    //Tạo Extra parameters.
                    SortedDictionary<string, object> parameterData = new SortedDictionary<string, object>();
                    decimal TTT = tongThanhToan_Value;
                    decimal TTT_NT = tongThanhToanNT_Value;
                    string LAN = c.LAN;
                    //string MA_NT = _maNt;
                    parameterData.Add("SOTIENVIETBANGCHU", V6BusinessHelper.MoneyToWords(TTT, LAN, V6Options.M_MA_NT0));
                    parameterData.Add("SOTIENVIETBANGCHUNT", V6BusinessHelper.MoneyToWords(TTT_NT, LAN, MA_NT));
                    c.FilterControl.RptExtraParameters = parameterData;
                    c.PrintMode = V6PrintMode.AutoLoadData;
                    c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
                }
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, sttRec_In, ex.Message));
            }
        }
        
        public void InPhieuThuTien(V6InvoiceBase Invoice, string sttRec_In, decimal tongThanhToan_Value, decimal tongThanhToanNT_Value)
        {
            try
            {
                string program = "APRINT_" + Invoice.Mact + "_TA1";
                string repFile = "APRINT_" + Invoice.Mact + "_TA1";
                var repTitle = "PHIẾU THU TIỀN";
                var repTitle2 = "RECEIPTS VOUCHER";

                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if ((Invoice.AlctConfig.XtraReport && !shift_is_down) || (!Invoice.AlctConfig.XtraReport && shift_is_down))
                {
                    var c = new ReportR_DX(Invoice.CodeMact, program, program, repFile,
                        repTitle, repTitle2, "", "", "");

                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@STT_REC", sttRec_In));
                    plist.Add(new SqlParameter("@isInvoice", "0"));
                    plist.Add(new SqlParameter("@ReportFile", repFile));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    c.FilterControl.InitFilters = plist;

                    //Tạo Extra parameters.
                    SortedDictionary<string, object> parameterData = new SortedDictionary<string, object>();
                    decimal TTT = tongThanhToan_Value;
                    decimal TTT_NT = tongThanhToanNT_Value;
                    string LAN = c.LAN;
                    //string MA_NT = _maNt;
                    parameterData.Add("SOTIENVIETBANGCHU", V6BusinessHelper.MoneyToWords(TTT, LAN, V6Options.M_MA_NT0));
                    parameterData.Add("SOTIENVIETBANGCHUNT", V6BusinessHelper.MoneyToWords(TTT_NT, LAN, MA_NT));
                    c.FilterControl.RptExtraParameters = parameterData;
                    c.PrintMode = V6PrintMode.AutoLoadData;
                    c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
                }
                else
                {
                    var c = new ReportRViewBase(Invoice.CodeMact, program, program, repFile,
                        repTitle, repTitle2, "", "", "");

                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@STT_REC", sttRec_In));
                    plist.Add(new SqlParameter("@isInvoice", "0"));
                    plist.Add(new SqlParameter("@ReportFile", repFile));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    c.FilterControl.InitFilters = plist;

                    //Tạo Extra parameters.
                    SortedDictionary<string, object> parameterData = new SortedDictionary<string, object>();
                    decimal TTT = tongThanhToan_Value;
                    decimal TTT_NT = tongThanhToanNT_Value;
                    string LAN = c.LAN;
                    //string MA_NT = _maNt;
                    parameterData.Add("SOTIENVIETBANGCHU", V6BusinessHelper.MoneyToWords(TTT, LAN, V6Options.M_MA_NT0));
                    parameterData.Add("SOTIENVIETBANGCHUNT", V6BusinessHelper.MoneyToWords(TTT_NT, LAN, MA_NT));
                    c.FilterControl.RptExtraParameters = parameterData;
                    c.PrintMode = V6PrintMode.AutoLoadData;
                    c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
                }
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, sttRec_In, ex.Message));
            }
        }

        public bool CheckEditAll(V6InvoiceBase Invoice, string status, string kieupost, string soct_sophieu, string ma_sonb,
            string ma_dvcs, string makh, string manx, DateTime ngayct_date, decimal tongThanhToan_Value, string mode_E_D)
        {
            //Tuanmh 24/07/2016 Check Debit Amount
            DataTable DataCheck_Edit_All =
                Invoice.GetCheck_Edit_All(status, kieupost, soct_sophieu, ma_sonb, _sttRec, ma_dvcs, makh,
                    manx, ngayct_date, Invoice.Mact, tongThanhToan_Value, mode_E_D, V6Login.UserId);

            bool check_edit = true;

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
                        check_edit = true;
                        break;
                    case "01":
                    case "02":
                    case "03":

                        if (message != "") this.ShowWarningMessage(message);
                        if (chk_yn == "0")
                        {
                            check_edit = false;
                        }
                        break;

                    case "06":
                    case "07":
                    case "08":
                        // Save but mess
                        if (message != "") this.ShowInfoMessage(message);
                        check_edit = true;
                        break;
                }
            }
            return check_edit;
        }

        /// <summary>
        /// VPA_CHECK_PRINT_ALL Kiểm tra được phép in hay không.
        /// </summary>
        /// <param name="Invoice"></param>
        /// <returns></returns>
        public bool CheckPrint(V6InvoiceBase Invoice)
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

        public void XemPhieuNhapView(DateTime ngayCT, string maCT, string maKho, string maVt)
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

        public void ResetTonLoHsd(V6NumberTextBox _ton13, V6VvarTextBox _maLo, V6DateTimeColor _hanSd, V6NumberTextBox _ton13Qd)
        {
            _ton13.Value = 0;
            _maLo.Clear();
            _hanSd.Value = null;
            if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") _ton13Qd.Value = 0;
        }
        public void ResetTonLoHsdRow(DataGridViewCell _ton13, DataGridViewCell _maLo, DataGridViewCell _hanSd, DataGridViewCell _ton13Qd)
        {
            _ton13.Value = 0;
            _maLo.Value = "";
            _hanSd.Value = null;
            if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") 
                _ton13Qd.Value = 0;
        }

        public string GetCA()
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@dS", V6BusinessHelper.GetServerDateTime()),
            };
            var ca = V6BusinessHelper.ExecuteFunctionScalar("VFA_GET_CA_FROM_ALTD", plist);
            return "" + ca;
        }

        public string GetFilterSKSM(DataTable dataSKSM, string sttRec0, string maVt, string maKhoI, HD_Detail detail1)
        {
            try
            {
                var list_SKSM = "";
                if (maVt == "" || maKhoI == "") return list_SKSM;

                foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                {

                    string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                    string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                    string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                    string c_sk = row["SO_KHUNG"].ToString().Trim().ToUpper();
                    string c_sm = row["SO_MAY"].ToString().Trim().ToUpper();

                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                    //decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                    if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                    {
                        if (maVt == c_maVt && maKhoI == c_maKhoI)
                        {
                            //or_sksm = 0;
                            list_SKSM += string.Format(" and (SO_KHUNG<>'{0}' and SO_MAY<>'{1}')", c_sk, c_sm);
                        }
                    }
                }

                if (list_SKSM.Length > 4)
                {
                    list_SKSM = list_SKSM.Substring(4);
                    return "(" + list_SKSM + ")";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            return "(1=1)";
        }

        public string GetFilterSKSM_PXDC(DataTable dataSKSM, string sttRec0, string maVt, string maKhoX, HD_Detail detail1)
        {
            try
            {
                var list_SKSM = "";
                if (maVt == "" || maKhoX == "") return list_SKSM;

                foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                {

                    string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                    string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                    string c_maKhoI = maKhoX;
                    string c_sk = row["SO_KHUNG"].ToString().Trim().ToUpper();
                    string c_sm = row["SO_MAY"].ToString().Trim().ToUpper();

                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                    //decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                    if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                    {
                        if (maVt == c_maVt && maKhoX == c_maKhoI)
                        {
                            //or_sksm = 0;
                            list_SKSM += string.Format(" and (SO_KHUNG<>'{0}' and SO_MAY<>'{1}')", c_sk, c_sm);
                        }
                    }
                }

                if (list_SKSM.Length > 4)
                {
                    list_SKSM = list_SKSM.Substring(4);
                    return "(" + list_SKSM + ")";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            return "(1=1)";
        }
        
        
        
        public void CreateFormProgram(V6InvoiceBase Invoice)
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
                    DataSet ds = ObjectAndString.XmlStringToDataSet(xml);
                    if (ds == null || ds.Tables.Count <= 0) continue;

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
                    DataSet ds = ObjectAndString.XmlStringToDataSet(xml);
                    //ds.ReadXml(new StringReader(xml));
                    if (ds == null || ds.Tables.Count <= 0) goto Build;
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
                Form_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + Invoice.Mact, using_text, method_text);
                // Get Event_program infos
                var ms = Form_program.GetMethods();
                for (int i = 0; i < ms.Length; i++)
                {
                    var m = ms[i];
                    Name_Methods[m.Name] = m;
                }

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
        public object InvokeFormEvent(string eventName)
        {
            try
            {
                //SetStatusText("InvokeFormEvent " + eventName);
                if (Event_Methods.ContainsKey(eventName))
                {
                    var method_name = Event_Methods[eventName];
                    return V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                SetStatusText("InvokeFormEvent " + eventName + " " + ex1.Message);
                this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
            }
            return null;
        }


        public void InvokeFormEventFixCopyData()
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
        /// <param name="many">Thanh thế hết giá trị cho các cột được cấu hình Alct.Extra_info.CT_REPLACE bằng giá trị của dòng đang đứng.</param>
        public void ChucNang_ThayThe(V6InvoiceBase invoice, bool many = false)
        {
            try
            {
                //Hien form chuc nang co options *-1 or input
                if (NotAddEdit) return;
                
                var detail1 = GetControlByName("detail1") as HD_Detail;
                if (detail1 == null)
                {
                    ShowParentMessage(V6Text.Text("UNKNOWNOBJECT") + ": [detail1].");
                    return;
                }
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    return;
                }

                var dataGridView1 = GetControlByName("dataGridView1") as DataGridView;
                if (dataGridView1 == null)
                {
                    ShowParentMessage(V6Text.Text("UNKNOWNOBJECT") + ": [dataGridView1].");
                    return;
                }
                if (dataGridView1.CurrentRow == null)
                {
                    ShowParentMessage(V6Text.NoData);
                    return;
                }

                if (!invoice.EXTRA_INFOR.ContainsKey("CT_REPLACE"))
                {
                    ShowParentMessage(V6Text.NoDefine + "EXTRA_INFOR[CT_REPLACE]");
                    return;
                }
                var listFieldCanReplace = ObjectAndString.SplitString(invoice.EXTRA_INFOR["CT_REPLACE"]);

                if (many)
                {
                    IDictionary<string, object> data = new Dictionary<string, object>();
                    if (dataGridView1.CurrentRow != null)
                    {
                        foreach (string field in listFieldCanReplace)
                        {
                            data[field.ToUpper()] = dataGridView1.CurrentRow.Cells[field].Value;
                        }

                        V6ControlFormHelper.UpdateDKlistAll(data, listFieldCanReplace, AD, dataGridView1.CurrentRow.Index);
                    }
                }
                else // Thay thế giá trị của cột đang đứng từ dòng hiện tại trở xuống bằng giá trị mới.
                {
                    int field_index = dataGridView1.CurrentCell.ColumnIndex;
                    string FIELD = dataGridView1.CurrentCell.OwningColumn.DataPropertyName.ToUpper();
                    
                    
                    if (!listFieldCanReplace.Contains(FIELD))
                    {
                        ShowParentMessage(V6Text.NoDefine + " CT_REPLACE:" + FIELD);
                        return;
                    }

                    V6ColorTextBox textBox = detail1.GetControlByAccessibleName(FIELD) as V6ColorTextBox;
                    Type valueType = dataGridView1.CurrentCell.OwningColumn.ValueType;

                    //Check
                    if (textBox == null)
                    {
                        ShowParentMessage(V6Text.Text("UNKNOWNOBJECT"));
                        return;
                    }

                    ChucNangThayTheForm f =
                        new ChucNangThayTheForm(
                            ObjectAndString.IsNumberType(dataGridView1.CurrentCell.OwningColumn.ValueType), textBox);
                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        if (f.ChucNangDaChon == f._ThayThe)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Index < dataGridView1.CurrentRow.Index) continue;

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
                                if (row.Index < dataGridView1.CurrentRow.Index) continue;

                                var newValue = ObjectAndString.ObjectToDecimal(row.Cells[field_index].Value)*-1;
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
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChucNang_ThayThe " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Chức năng sửa dữ liệu AD bằng cách gõ trực tiếp.
        /// </summary>
        /// <param name="invoice"></param>
        public void ChucNang_SuaNhieuDong(V6InvoiceBase invoice)
        {
            try
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;

                string adFields = invoice.EXTRA_INFOR.ContainsKey("ADFIELDS") ? invoice.EXTRA_INFOR["ADFIELDS"] : "";
                if (string.IsNullOrEmpty(adFields))
                {
                    adFields = "V6NOEDIT:R"; // tắt edit khi chưa có cấu hình.
                }

                string tableName = invoice.Mact + "_REPLACE";
                var f = new DataEditorForm(this, AD, tableName, adFields, null, V6Text.Edit + " " + V6TableHelper.V6TableCaption(tableName, V6Setting.Language), false, false, true, false);
                f.SetHideFields(invoice.GRD_HIDE);
                f.SetReadOnlyFields(invoice.GRD_READONLY);
                All_Objects["dataGridView"] = f.DataGridView;
                InvokeFormEvent(FormDynamicEvent.SUANHIEUDONG);
                f.ShowDialog(this);

                TinhTongThanhToan("SuaNhieuDong");
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang_SuaNhieuDong " + _sttRec, ex);
            }
        }

        public void AfterReplace(Dictionary<string, object> allObjects)
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
        public void ResetAMADbyConfig(V6InvoiceBase invoice)
        {
            var m_ma_hd = ("" + invoice.Alct["M_MA_HD"]).Trim();
            if (m_ma_hd == "") return;

            var sss = ObjectAndString.SplitStringBy(m_ma_hd, ';');
            foreach (string s in sss)
            {
                var ss = ObjectAndString.Split2(s, ':');
                if (ss.Length > 1)
                {
                    var fields_values = ObjectAndString.SplitString(ss[1]);
                    if (ss[0].ToUpper() == "AM")
                    {
                        foreach (string field_value in fields_values)
                        {
                            var f_v = ObjectAndString.Split2(field_value, ':');
                            string FIELD = f_v[0].Trim().ToUpper();
                            string configValue = f_v[1];

                            if (invoice.AMStruct.ContainsKey(FIELD))
                            {
                                Control c = GetControlByAccessibleName(FIELD);
                                if (c != null) V6ControlFormHelper.SetControlValue(c, configValue);
                            }
                        }
                    }
                    else if (ss[0].ToUpper() == "AD")
                    {
                        foreach (string field_value in fields_values)
                        {
                            var f_v = ObjectAndString.Split2(field_value, ':');
                            string FIELD = f_v[0].Trim().ToUpper();
                            string configValue = f_v[1];

                            if (invoice.ADStruct.ContainsKey(FIELD) && AD.Columns.Contains(FIELD))
                            {
                                object resetValue = configValue;
                                V6ColumnStruct struct0 = invoice.ADStruct[FIELD];
                                if (!struct0.AllowNull)
                                {
                                    switch (struct0.sql_data_type_string)
                                    {
                                        case "date":
                                        case "smalldatetime":
                                        case "datetime":
                                            resetValue = ObjectAndString.ObjectToDate(configValue);
                                            if (resetValue == null) resetValue = V6Setting.M_SV_DATE;
                                            break;
                                        case "bit":
                                            resetValue = ObjectAndString.ObjectToBool(configValue);
                                            break;
                                        case "bigint":
                                            resetValue = ObjectAndString.ObjectToInt64(configValue);
                                            break;
                                        case "smallint":
                                        case "int":
                                        case "tinyint":
                                            resetValue = ObjectAndString.ObjectToInt(configValue);
                                            break;
                                        case "numeric":
                                        case "decimal":
                                        case "smallmoney":
                                        case "money":
                                            resetValue = ObjectAndString.ObjectToDecimal(configValue);
                                            break;

                                        default:
                                            resetValue = configValue;
                                            break;
                                    }
                                }

                                foreach (DataRow dataRow in AD.Rows)
                                {
                                    dataRow[field_value] = resetValue;
                                }
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gán lại các giá trị mới như stt_rec, date04...
        /// </summary>
        public void ResetAllADDefaultValue()
        {
            var svDate = V6BusinessHelper.GetServerDateTime();
            var time = ObjectAndString.ObjectToString(svDate, "HH:mm:ss");
            var date = svDate.Date;
            foreach (DataTable table in new[] { AD, AD2, AD3 })
            {
                if (table == null) continue;
                foreach (DataRow dataRow in table.Rows)
                {
                    dataRow["STT_REC"] = _sttRec;
                    if (table.Columns.Contains("STT_RECDH")) dataRow["STT_RECDH"] = DBNull.Value;
                    if (table.Columns.Contains("STT_REC0DH")) dataRow["STT_REC0DH"] = DBNull.Value;
                    if (table.Columns.Contains("TIME04")) dataRow["TIME04"] = time;
                    if (table.Columns.Contains("DATE04")) dataRow["DATE04"] = date;
                    if (table.Columns.Contains("USER_ID04")) dataRow["USER_ID04"] = V6Login.UserId;
                    if (table.Columns.Contains("TIME24")) dataRow["TIME24"] = time;
                    if (table.Columns.Contains("DATE24")) dataRow["DATE24"] = date;
                    if (table.Columns.Contains("USER_ID24")) dataRow["USER_ID24"] = V6Login.UserId;
                }
            }
        }

        public void UpdateDateTime4(DataGridViewRow row)
        {
            var gridview = row.DataGridView;
            var svDate = V6BusinessHelper.GetServerDateTime();
            var time = ObjectAndString.ObjectToString(svDate, "HH:mm:ss");
            var date = svDate.Date;

            if (gridview.Columns.Contains("TIME24")) row.Cells["TIME24"].Value = time;
            if (gridview.Columns.Contains("DATE24")) row.Cells["DATE24"].Value = date;
            if (gridview.Columns.Contains("USER_ID24")) row.Cells["USER_ID24"].Value = V6Login.UserId;
        }

        public void ViewLblKieuPost(Label lblKieuPostColor, V6ColorComboBox cboKieuPost, bool view)
        {
            try
            {
                lblKieuPostColor.Visible = view;
                lblKieuPostColor.Text = cboKieuPost.Text;

                if (!view) return;

                TextBox maXuLy_TextBox = GetControlByAccessibleName("MA_XULY") as TextBox;

                if (maXuLy_TextBox is V6LookupProc)
                {
                    var maXuLy_Proc = maXuLy_TextBox as V6LookupProc;
                    if (maXuLy_Proc.Data != null)
                    {
                        var ten_xuly = " (" + (V6Setting.IsVietnamese ? maXuLy_Proc.Data["TEN_XULY"] : maXuLy_Proc.Data["TEN_XULY2"]) + ")";
                        lblKieuPostColor.Text += ten_xuly;
                    }
                }
                else if (maXuLy_TextBox is V6VvarTextBox)
                {
                    var maXuLy_Vvar = maXuLy_TextBox as V6VvarTextBox;
                    if (maXuLy_Vvar.Data != null)
                    {
                        var ten_xuly = " (" + (V6Setting.IsVietnamese ? maXuLy_Vvar.Data["TEN_XULY"] : maXuLy_Vvar.Data["TEN_XULY2"]) + ")";
                        lblKieuPostColor.Text += ten_xuly;
                    }
                }
                //if (txtMaXuLy_TextBox != null && txtMaXuLy_TextBox.Data != null)
                //{
                //    var ten_xuly = " (" + (V6Setting.IsVietnamese ? txtMaXuLy_TextBox.Data["TEN_XULY"] : txtMaXuLy_TextBox.Data["TEN_XULY2"]) + ")";
                //    lblKieuPostColor.Text += ten_xuly;
                //}

                lblKieuPostColor.ForeColor = cboKieuPost.SelectedItemTextColor;
                //if (cboKieuPost.SelectedValue == null) return;
                //var selectedRow = ((DataRowView)cboKieuPost.SelectedItem).Row;
                //var color_name = selectedRow["ColorV"].ToString().Trim();
                //if (color_name != "")
                //{
                //    var color = ObjectAndString.StringToColor(color_name);
                //    lblKieuPostColor.ForeColor = color;
                //}
                //else
                //{
                //    lblKieuPostColor.ForeColor = Color.Black;
                //}
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ViewLblKieuPost", ex);
            }
        }

        /// <summary>
        /// ADSELECTMORE d.XXXX Gán giá trị liên quan của ma_vt
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="ma_vt_data"></param>
        public void SetADSelectMoreControlValue(V6InvoiceBase invoice, DataRow ma_vt_data)
        {
            try
            {
                if (string.IsNullOrEmpty(invoice.ADSELECTMORE)) return;

                var d_list = ObjectAndString.SplitString(invoice.ADSELECTMORE);
                foreach (string d_ in d_list)
                {
                    string D_ = d_.ToUpper().Trim();
                    if (D_.StartsWith("D."))
                    {
                        string FIELD = D_.Substring(2);
                        if (All_Objects.ContainsKey(FIELD))
                        {
                            SetControlValue(All_Objects[FIELD] as Control, ma_vt_data[FIELD]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetADSelectMoreControlValue", ex);
            }
        }
        
        public void SetADSelectMoreControlValue(V6InvoiceBase invoice, IDictionary<string, object> ma_vt_data)
        {
            try
            {
                if (string.IsNullOrEmpty(invoice.ADSELECTMORE)) return;

                var d_list = ObjectAndString.SplitString(invoice.ADSELECTMORE);
                foreach (string d_ in d_list)
                {
                    string D_ = d_.ToUpper().Trim();
                    if (D_.StartsWith("D."))
                    {
                        string FIELD = D_.Substring(2).ToUpper();
                        if (All_Objects.ContainsKey(FIELD))
                        {
                            SetControlValue(All_Objects[FIELD] as Control, ma_vt_data[FIELD]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetADSelectMoreControlValue", ex);
            }
        }


        protected void GoToFirstFocus(Control defaultControl)
        {
            if (_invoice.EXTRA_INFOR.ContainsKey("TABINDEX"))
            {
                var list = ObjectAndString.SplitString(_invoice.EXTRA_INFOR["TABINDEX"]);
                Control c = GetControlByAccessibleName(list[0]);
                if (c != null)
                {
                    c.Focus();
                    return;
                }
            }
            defaultControl.Focus();
        }

        public void ReorderGroup1TabIndex()
        {
            if (_invoice.EXTRA_INFOR.ContainsKey("TABINDEX"))
            {
                var list = ObjectAndString.SplitString(_invoice.EXTRA_INFOR["TABINDEX"]);
                for (int i = list.Length-1; i >= 0; i--)
                {
                    Control c = GetControlByAccessibleName(list[i]);
                    if (c != null) c.TabIndex = i;
                }
            }
        }

        public void LoadAlnt(ComboBox cboMaNt)
        {
            try
            {
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = V6Setting.IsVietnamese ? "Ten_nt" : "Ten_nt2";
                cboMaNt.DataSource = _invoice.Alnt;
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = V6Setting.IsVietnamese ? "Ten_nt" : "Ten_nt2";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void LoadAlpost(ComboBox cboKieuPost)
        {
            try
            {
                cboKieuPost.ValueMember = "kieu_post";
                cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
                cboKieuPost.DataSource = _invoice.AlPost;
                cboKieuPost.ValueMember = "kieu_post";
                cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        protected void LoadAlimtype(ComboBox cbo)
        {
            try
            {
                cbo.ValueMember = "IMTYPE";
                cbo.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
                cbo.DataSource = _invoice.AlImtype;
                cbo.ValueMember = "IMTYPE";
                cbo.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Hiển thị dữ liệu lên control chi tiết. (Chỉ có hiệu lực khi control chi tiết đang ở mode view or lock.
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="detail1"></param>
        public void ViewCurrentRowToDetail(V6ColorDataGridView dataGridView1, HD_Detail detail1)
        {
            if (detail1. IsViewOrLock)
            {
                detail1.SetData( dataGridView1.CurrentRow.ToDataDictionary());
            }
        }

        public V6ColorTextBox txtCustomInfo;
        
        public void CreateCustomInfoTextBox(GroupBox group4, TextBox topLeftBase, Control bottomLeftBase)
        {
            try
            {
                txtCustomInfo = new V6ColorTextBox()
                {
                    Name = "txtCustomeInfo",
                    Width = 100,
                    Height = 23,
                    Left = topLeftBase.Right + 5,
                    Top = topLeftBase.Top,
                    ReadOnly = true,
                    Multiline = true,
                    Visible = false,
                    Tag = "readonly",
                };
                txtCustomInfo.TextChanged += delegate
                {
                    txtCustomInfo.Visible = txtCustomInfo.Text != "";
                    txtCustomInfo.Height = 23;
                };
                txtCustomInfo.BorderStyle = BorderStyle.FixedSingle;
                txtCustomInfo.Font = new Font(txtCustomInfo.Font, FontStyle.Bold);
                //txtCustomInfo.Text = "V6Soft";
                group4.Controls.Add(txtCustomInfo);
                group4.SizeChanged += (sender, args) =>
                {
                    if (group4.Width > txtCustomInfo.Left + 100) txtCustomInfo.Width = group4.Width - txtCustomInfo.Left - 10;
                };
                txtCustomInfo.BringToFront();
                txtCustomInfo.GotFocus += (sender, args) =>
                {
                    txtCustomInfo.Height = bottomLeftBase.Bottom - topLeftBase.Top;
                };
                txtCustomInfo.MouseMove += delegate
                {
                    txtCustomInfo.Height = bottomLeftBase.Bottom - topLeftBase.Top;
                };
                txtCustomInfo.Leave += (sender, args) =>
                {
                    txtCustomInfo.Height = 23;
                };
                txtCustomInfo.MouseLeave += delegate
                {
                    if (!txtCustomInfo.Focused) txtCustomInfo.Height = 23;
                };
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "CreateCustomeInfoTextBox", ex);
            }
        }

        /// <summary>
        /// Tải thông tin.
        /// </summary>
        public void LoadCustomInfo(DateTime ngayCT, string ma_kh)
        {
            string text = "";
            try
            {
                //txtCustomInfo.Text = "";
                //VPA_GET_SOA_MA_KH_INFOR
                SqlParameter[] plist =
                {
                    new SqlParameter("@Ngay_ct", ngayCT.Date),
                    new SqlParameter("@Ma_kh", ma_kh),
                    new SqlParameter("@Ma_ct", _invoice.Mact),
                    new SqlParameter("@Stt_rec", _sttRec),
                    new SqlParameter("@Lan", V6Setting.Language),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@Advance", ""),
                };
                var data = V6BusinessHelper.ExecuteProcedure("VPA_GET_VC_MA_KH_INFOR", plist).Tables[0];
                if (data.Rows.Count > 0)
                {
                    text = data.Rows[0]["Mess1"].ToString();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadCustomInfo", ex);
            }
            txtCustomInfo.Text = text;
        }

        /// <summary>
        /// <para>Gọi hàm động Event F9. Nếu ko có định nghĩa Event động sẽ gọi hàm cứng XulyF9 (override).</para>
        /// <para>Trong code động vẫ ncó thể gọi thisForm.XuLyF9 để gọi hàm cứng.</para>
        /// </summary>
        protected void XuLyF9Base()
        {
            if (!IsHaveInvoice) return;
            if (!V6Login.IsAdmin && !ObjectAndString.ObjectToBool(_invoice.Alctct["R_F9"]))
            {
                this.ShowWarningMessage(V6Text.NoRight);
                return;
            }
            if (Event_Methods.ContainsKey(FormDynamicEvent.F9))
            {
                InvokeFormEvent(FormDynamicEvent.F9);
            }
            else
            {
                XuLyF9();
            }
        }

        public virtual void XuLyF9()
        {
            ShowMainMessage(V6Text.NoDefine + FormDynamicEvent.F9);
        }

        /// <summary>
        /// Ẩn control AM theo Alctct.GRD_HIDE
        /// </summary>
        protected void HideControlByGRD_HIDE()
        {
            try
            {
                if (_invoice.Alctct == null) return;
                foreach (string field in _invoice.GRD_HIDE)
                {
                    Control c = GetControlByAccessibleName(field);
                    if (c != null) c.InvisibleTag();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".HideControlByGRD_HIDE", ex);
            }
        }

        public void TinhSoLuongTheoSoLuong1(V6NumberTextBox soLuong, V6NumberTextBox soLuong1, decimal he_so1T_Value, decimal he_so1M_Value)
        {
            soLuong.Value = soLuong1.Value * he_so1T_Value / he_so1M_Value;
        }

        protected void ExportXml()
        {
            try
            {
                if (!IsViewingAnInvoice) return;
                string save = V6ControlFormHelper.ChooseSaveFile(this, "Xml|*.xml", "InvoiceData" + _invoice.Mact);
                if (string.IsNullOrEmpty(save)) return;

                DataSet ds = new DataSet("InvoiceData" + _invoice.Mact);
                DataTable am = new DataTable("AM");
                DataTable ad = new DataTable("AD");
                ds.Tables.Add(am);
                ds.Tables.Add(ad);
                am.AddRow(AM_current, true);
                ad.AddRowByTable(AD, true);
                //if (AD2 != null)
                //{
                //    DataTable ad2 = new DataTable("AD2");
                //    ds.Tables.Add(ad2);
                //    ad2.AddRowByTable(AD2, true);
                //}
                //if (AD3 != null)
                //{
                //    DataTable ad3 = new DataTable("AD3");
                //    ds.Tables.Add(ad3);
                //    ad3.AddRowByTable(AD3, true);
                //}
                // Remove UID STT_REC STT_REC0 ...
                foreach (DataTable tb in ds.Tables)
                {
                    if (tb.Columns.Contains("UID")) tb.Columns.Remove(tb.Columns["UID"]);
                    if (tb.Columns.Contains("STT_REC")) tb.Columns.Remove(tb.Columns["STT_REC"]);
                    if (tb.Columns.Contains("STT_REC0")) tb.Columns.Remove(tb.Columns["STT_REC0"]);
                }

                ds.WriteXml(save);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ExportXml", ex);
            }
        }

        protected void ImportXml()
        {
            try
            {
                if (NotAddEdit) return;
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                string save = V6ControlFormHelper.ChooseOpenFile(this, "Xml|*.xml");
                if (string.IsNullOrEmpty(save)) return;

                DataSet ds = new DataSet("InvoiceData");
                ds.ReadXml(save);
                // Remove UID STT_REC STT_REC0 ...
                foreach (DataTable tb in ds.Tables)
                {
                    if (tb.Columns.Contains("UID")) tb.Columns.Remove(tb.Columns["UID"]);
                    if (tb.Columns.Contains("STT_REC")) tb.Columns.Remove(tb.Columns["STT_REC"]);
                    if (tb.Columns.Contains("STT_REC0")) tb.Columns.Remove(tb.Columns["STT_REC0"]);
                }
                DataTable am = ds.Tables["AM"];
                DataTable ad = ds.Tables["AD"];
                
                V6ControlFormHelper.SetFormDataRow(this, am.Rows[0]);
                if (!shift_is_down) AD.Rows.Clear();
                int addCount = 0, failCount = 0; _message = "";
                foreach (DataRow row in ad.Rows)
                {
                    var newData = row.ToDataDictionary();
                    if (XuLyThemDetail(newData))
                    {
                        addCount++;
                        All_Objects["data"] = newData;
                        InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    }
                    else failCount++;
                }
                //if (ds.Tables.Contains("AD2")) { }
                V6ControlFormHelper.ShowMainMessage(string.Format("Succeed {0}. Failed: {1}{2}", addCount, failCount, _message));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ImportXml", ex);
            }
        }

        public virtual bool XuLyThemDetail(IDictionary<string, object> toDataDictionary)
        {
            throw new NotImplementedException();
        }

        protected void GET_AM_OLD_EXTRA()
        {
            try
            {
                if (AM_old == null) return;
                if (_invoice.EXTRA_INFOR.ContainsKey("AM_OLD"))
                {
                    var amFields = ObjectAndString.SplitString(_invoice.EXTRA_INFOR["AM_OLD"]);
                    var amSome = new Dictionary<string, object>();
                    foreach (string field in amFields)
                    {
                        if (AM_old.Table.Columns.Contains(field)) amSome[field.ToUpper()] = AM_old[field];
                    }
                    SetSomeData(amSome);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GET_AM_OLD_EXTRA", ex);
            }
        }

        public void chonAlvt_AcceptData(DataTable table, HD_Detail detail1, V6VvarTextBox _maVt, decimal tygia, V6ColorDataGridView dataGridView1)
        {
            var add_count = 0;
            _message = "";
            detail1.MODE = V6Mode.View;
            dataGridView1.UnLock();
            if (table.Columns.Contains("MA_VT") && table.Columns.Contains("MA_KHO_I")
                && table.Columns.Contains("SO_LUONG1"))
            {
                if (table.Rows.Count > 0)
                {
                    bool flag_add = chon_accept_flag_add;
                    chon_accept_flag_add = false;
                    if (!flag_add)
                    {
                        AD.Rows.Clear();
                    }
                }

                int i = -1;
                foreach (DataRow row in table.Rows)
                {
                    i++;
                    var data = row.ToDataDictionary(_sttRec);
                    var cMaVt = data["MA_VT"].ToString().Trim();
                    var cMaKhoI = ("" + data["MA_KHO_I"]).Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

                    //{ Tuanmh 31/08/2016 Them thong tin ALVT
                    _maVt.Text = cMaVt;
                    var datavt = _maVt.Data;


                    if (datavt != null)
                    {
                        //Nếu dữ liệu không (!) chứa mã nào thì thêm vào dữ liệu cho mã đó.
                        if (!data.ContainsKey("TEN_VT")) data.Add("TEN_VT", (datavt["TEN_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT1")) data.Add("DVT1", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT")) data.Add("DVT", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("TK_VT")) data.Add("TK_VT", (datavt["TK_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("HE_SO1T")) data.Add("HE_SO1T", 1);
                        if (!data.ContainsKey("HE_SO1M")) data.Add("HE_SO1M", 1);
                        if (!data.ContainsKey("SO_LUONG")) data.Add("SO_LUONG", data["SO_LUONG1"]);
                        if (!data.ContainsKey("TIEN_NT0")) data.Add("TIEN_NT0", 0);
                        if (!data.ContainsKey("GIA_NT01")) data.Add("GIA_NT01", 0);

                        var __tien_nt0 = ObjectAndString.ToObject<decimal>(data["TIEN_NT0"]);
                        var __gia_nt0 = ObjectAndString.ObjectToDecimal(data["GIA_NT01"]);
                        var __tien0 = V6BusinessHelper.Vround(__tien_nt0 * tygia, M_ROUND);
                        var __gia0 = V6BusinessHelper.Vround(__gia_nt0 * tygia, M_ROUND_GIA);

                        if (!data.ContainsKey("TIEN0")) data.Add("TIEN0", __tien0);

                        if (!data.ContainsKey("TIEN_NT")) data.Add("TIEN_NT", data["TIEN_NT0"]);
                        if (!data.ContainsKey("TIEN")) data.Add("TIEN", __tien0);
                        if (!data.ContainsKey("GIA01")) data.Add("GIA01", __gia0);
                        if (!data.ContainsKey("GIA0")) data.Add("GIA0", __gia0);
                        if (!data.ContainsKey("GIA")) data.Add("GIA", __gia0);
                        if (!data.ContainsKey("GIA1")) data.Add("GIA1", __gia0);
                        if (!data.ContainsKey("GIA_NT0")) data.Add("GIA_NT0", data["GIA_NT01"]);
                        if (!data.ContainsKey("GIA_NT")) data.Add("GIA_NT", data["GIA_NT01"]);
                        if (!data.ContainsKey("GIA_NT1")) data.Add("GIA_NT1", data["GIA_NT01"]);
                    }

                    if (exist && exist2)
                    {
                        if (XuLyThemDetail(data))
                        {
                            add_count++;
                            All_Objects["data"] = data;
                            InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                        }
                    }
                    else
                    {
                        if (!exist) _message += string.Format("Dòng {0}: Vật tư {1}  {2}. ", i + 1, cMaVt, V6Text.NotExist);
                        if (!exist2) _message += string.Format("Dòng {0}: Mã kho {1} {2}. ", i + 1, cMaKhoI, V6Text.Empty);
                    }
                }
                ShowParentMessage(string.Format(V6Text.Added + "[{0}].", add_count) + _message);
            }
            else
            {
                ShowParentMessage(V6Text.Text("LACKINFO"));
            }
        }

        protected void InHoaDonDienTu()
        {
            if (!IsViewingAnInvoice) return;

            //bool ctrl_is_down = (ModifierKeys & Keys.Control) == Keys.Control;
            //if (ctrl_is_down)
            //{ btnTestViewXml_Click(sender, e); return; }

            try
            {
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;

                var row = AM_current;
                string MA_SONB = row["MA_SONB"].ToString().Trim();
                IDictionary<string, object> key = new Dictionary<string, object>();
                key["MA_SONB"] = MA_SONB;
                string brand = ("" + V6BusinessHelper.SelectOneValue("ALSONB", "S1", key)).Trim();
                //string brand = "3"; // 1:Viettel 2:Vnpt 3:Bkav 4:Vnpt_token 5:SoftDreams 6:ThaiSon 7:Monet 8:Minvoice 9...
                if (string.IsNullOrEmpty(brand))
                {
                    this.ShowWarningMessage(V6Text.CheckInfor + " " + MA_SONB);
                    return;
                }

                string v6_partner_infor = ("" + row["PART_INFOS"]).Trim();
                if (string.IsNullOrEmpty(v6_partner_infor))
                {
                    this.ShowWarningMessage("Hóa đơn chưa chuyển!");
                    return;
                }

                string mode = V6Options.V6OptionValues["M_HDDT_TYPE_PRINT"];
                if (shift_is_down)
                {
                    mode = mode == "1" ? "2" : "1";
                }

                string return_file_name = "";
                //string tableName = "V6MAPINFO";
                //string keys = "UID,MA_TD1"; //+ma_td1   1:VIETTEL    2:VNPT    3:BKAV
                //var map_table = V6BusinessHelper.Select(tableName, "*", "LOAI = 'AAPPR_SOA2' and (MA_TD1='" + FilterControl.String1 + "' or ma_td1='0' or ma_td1='') order by GROUPNAME,GC_TD1").Data;
                SqlParameter[] plist0 =
                {
                    new SqlParameter("@Loai", "AAPPR_" + _invoice.Mact + "2"),
                    new SqlParameter("@MA_TD1", brand), // Nhánh hóa đơn điện tử.
                    new SqlParameter("@Ma_ct", (row["Ma_ct"] ?? "").ToString()),
                    new SqlParameter("@Stt_rec", (row["Stt_rec"] ?? "").ToString()),
                    new SqlParameter("@Ma_dvcs", row["MA_DVCS"].ToString()),
                    new SqlParameter("@User_ID", V6Login.UserId),
                    new SqlParameter("@Advance", ""),
                };
                var map_table = V6BusinessHelper.ExecuteProcedure("VPA_GET_V6MAPINFO", plist0).Tables[0];

                string invoiceNo = row["SO_SERI"].ToString().Trim() + row["SO_CT"].ToString().Trim();
                DateTime ngay_ct = ObjectAndString.ObjectToFullDateTime(row["NGAY_CT"]);
                string v6_partner_id = row["V6PARTNER_ID"].ToString().Trim();
                string pattern = row["MA_MAUHD"].ToString().Trim();
                string fkey_hd = row["fkey_hd"].ToString().Trim();

                var pmparams = new PostManagerParams
                {
                    DataSet = map_table.DataSet,
                    Branch = brand, // FilterControl.String1,                 // Nhánh hóa đơn điện tử.
                    InvoiceNo = invoiceNo,
                    InvoiceDate = ngay_ct,
                    V6PartnerID = v6_partner_id,
                    Pattern = pattern,
                    Fkey_hd = fkey_hd,
                    //strIssueDate = "nouse",
                    Mode = mode,
                };
                string error;
                return_file_name = PostManager.PowerDownloadPDF(pmparams, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    this.ShowErrorMessage(error);
                    return;
                }

                string ext = Path.GetExtension(return_file_name).ToLower();
                if (ext == ".pdf")
                {
                    PDF_ViewPrintForm view = new PDF_ViewPrintForm(return_file_name);
                    view.ShowDialog(this);
                }
                else if (ext == ".html")
                {
                    HtmlViewerForm view = new HtmlViewerForm(return_file_name, return_file_name, false);
                    view.ShowDialog(this);
                }
            }
            catch (WebException ex)
            {
                this.ShowErrorMessage(V6Text.Text("NETWORK_ERROR"));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnTestViewPdf_Click", ex);
            }
        }

        /// <summary>
        /// Thêm vào lịch sử thay đổi dòng chi tiết.
        /// </summary>
        /// <param name="stt_rec0">Mã dòng</param>
        /// <param name="controlList1"></param>
        /// <param name="oldData">Dữ liệu cũ. null nếu thêm dòng.</param>
        /// <param name="newData">Dữ liệu mới. null nếu xóa.</param>
        protected void UpdateDetailChangeLog(string stt_rec0, Dictionary<string, AlctControls> controlList1, IDictionary<string, object> oldData, IDictionary<string, object> newData)
        {
            // Tuanmh 30/10/2020 Ignore
            //return;
            if (Mode != V6Mode.Edit) return;                                    // Chỉ chạy mode Edit.
            if (!V6Options.SaveEditLogInvoice || !_invoice.WRITE_LOG) return;   // Cho phép lưu log trong setting.
            if (controlList1 == null) return;
            try
            {
                if (oldData == null) // add
                {
                    SortedDictionary<string, object> newData1 = new SortedDictionary<string, object>();
                    foreach (KeyValuePair<string, AlctControls> item in controlList1)
                    {
                        string KEY = item.Key.ToUpper();
                        if (item.Value.IsVisible && item.Value.DetailControl != null && item.Value.DetailControl.Enabled && newData.ContainsKey(KEY))
                        {
                            if (!ObjectAndString.IsNoValue(newData[KEY]))
                            {
                                newData1[KEY] = newData[KEY];
                            }
                        }
                    }

                    editLogData["ADD_" + stt_rec0] = new OldNewData() {OldData = null, NewData = newData1};
                }
                else if (newData == null) // delete
                {
                    SortedDictionary<string, object> oldData1 = new SortedDictionary<string, object>();
                    foreach (KeyValuePair<string, AlctControls> item in controlList1)
                    {
                        string KEY = item.Key.ToUpper();
                        if (item.Value.IsVisible && item.Value.DetailControl != null && item.Value.DetailControl.Enabled && oldData.ContainsKey(KEY))
                        {
                            if (!ObjectAndString.IsNoValue(oldData[KEY]))
                            {
                                oldData1[KEY] = oldData[KEY];
                            }
                        }
                    }

                    editLogData["DELETE_" + stt_rec0] = new OldNewData() {OldData = oldData1, NewData = null};
                }
                else // edit
                {
                    if (editLogData.ContainsKey("EDIT_" + stt_rec0))
                    {
                        IDictionary<string, object> newData1 = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, AlctControls> item in controlList1)
                        {
                            if (item.Value.IsVisible && item.Value.DetailControl != null && item.Value.DetailControl.Enabled)
                            {
                                string FIELD = item.Key.ToUpper();
                                newData1[FIELD] = newData[FIELD];
                            }
                        }

                        editLogData["EDIT_" + stt_rec0].NewData = newData1;
                    }
                    else
                    {
                        IDictionary<string, object> oldData1 = new Dictionary<string, object>();
                        IDictionary<string, object> newData1 = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, AlctControls> item in controlList1)
                        {
                            if (item.Value.IsVisible && item.Value.DetailControl != null && item.Value.DetailControl.Enabled)
                            {
                                string FIELD = item.Key.ToUpper();
                                oldData1[FIELD] = oldData[FIELD];
                                newData1[FIELD] = newData[FIELD];
                            }
                        }

                        editLogData["EDIT_" + stt_rec0] = new OldNewData() {OldData = oldData1, NewData = newData1};
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".UpdateDetailChangeLog", ex);
            }
        }

        public void InitEditLog()
        {
            if (!V6Options.SaveEditLogInvoice || !_invoice.WRITE_LOG) return;   // Cho phép lưu log trong setting.
            editLogData = new Dictionary<string, OldNewData>();
        }

        /// <summary>
        /// stt_rec0 add?edit?delete data
        /// </summary>
        private Dictionary<string, OldNewData> editLogData = new Dictionary<string, OldNewData>();

        private class OldNewData
        {
            public IDictionary<string, object> OldData;
            public IDictionary<string, object> NewData;
        }

        private string GetDetailInfo()
        {
            string result = "";
            foreach (KeyValuePair<string, OldNewData> item in editLogData)
            {
                result += "~" + item.Key + " " + V6ControlFormHelper.CompareDifferentData(item.Value.OldData, item.Value.NewData);
            }
            if (result.Length > 1) result = result.Substring(1);
            return result;
        }

        /// <summary>
        /// Save Edit history.
        /// </summary>
        /// <param name="data_old">Dữ liệu trước đó.</param>
        /// <param name="data_new">Dữ liệu mới</param>
        protected void SaveEditLog(IDictionary<string, object> data_old, IDictionary<string, object> data_new)
        {
            try
            {
                if (V6Options.SaveEditLogInvoice && _invoice.WRITE_LOG)
                {
                    string info = V6ControlFormHelper.CompareDifferentData(data_old, data_new);
                    string detailInfo = GetDetailInfo();
                    V6BusinessHelper.WriteV6InvoiceHistory(ItemID, MethodBase.GetCurrentMethod().Name, string.IsNullOrEmpty(CodeForm) ? "N" : CodeForm[0].ToString(),
                        ObjectAndString.ObjectToString(data_old["UID"]), _invoice.Mact, _sttRec, info, detailInfo);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SaveEditHistory", ex);
            }
        }


        public void SetColumnInitFilter(V6ColorDataGridView dataGridView, string field, string filter)
        {
            var ma_kho_column = dataGridView.Columns[field] as V6VvarDataGridViewColumn;
            if (ma_kho_column != null)
            {
                ma_kho_column.InitFilter = filter;
            }
        }

        /// <summary>
        /// Nếu ma_vt.GIA_TON == 5 gán sl_td1 = ma_vt.sl_td3, gia_nt = ma_vt.sl_td1.
        /// </summary>
        /// <param name="txt_ma_vt"></param>
        /// <param name="txt_sl_td1"></param>
        /// <param name="txt_gia_nt"></param>
        public void GetGiaVonCoDinh(V6VvarTextBox txt_ma_vt, V6NumberTextBox txt_sl_td1, V6NumberTextBox txt_gia_nt)
        {
            try
            {
                if (txt_ma_vt.GIA_TON != 5) return;
                if (_maNt != _mMaNt0) txt_sl_td1.Value = ObjectAndString.ObjectToDecimal(txt_ma_vt.Data["SL_TD3"]);
                else txt_sl_td1.Value = 1;

                txt_gia_nt.Value = ObjectAndString.ObjectToDecimal(txt_ma_vt.Data["SL_TD1"]);

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// EXTRA_INFOR ENTER2TAB
        /// </summary>
        /// <param name="dataGridViews"></param>
        protected void SetGridViewFlag(params V6ColorDataGridView[] dataGridViews)
        {
            try
            {
                if (_invoice.EXTRA_INFOR.ContainsKey("ENTER2TAB"))
                {
                    bool e2t = ObjectAndString.ObjectToBool(_invoice.EXTRA_INFOR["ENTER2TAB"]);
                    foreach (V6ColorDataGridView gridView in dataGridViews)
                    {
                        gridView.enter_to_tab = e2t;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// ReportR45db2SelectorForm
        /// </summary>
        /// <param name="program"></param>
        public void XuLyKhac(string program)
        {
            try
            {
                if (NotAddEdit) return;
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                chon_accept_flag_add = shift;

                ReportR45db2SelectorForm r45Selector = new ReportR45db2SelectorForm(_invoice, program);
                r45Selector.All_Objects["parentForm"] = this;
                if (r45Selector.ShowDialog(this) == DialogResult.OK)
                {
                    ChonEventArgs chonE = new ChonEventArgs();
                    chonE.AD2AM = r45Selector.AD2AM;
                    chonExcel_AcceptData(r45Selector.dataGridView1.GetSelectedData(), chonE);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public virtual void chonExcel_AcceptData(List<IDictionary<string, object>> getSelectedData, ChonEventArgs chonE)
        {
            throw new NotImplementedException();
        }


        public override void ShowAlinitAddEdit(Control control)
        {
            V6Mode v6mode = V6Mode.Add;
            IDictionary<string, object> keys0 = new Dictionary<string, object>();
            IDictionary<string, object> keys = null;
            
            keys0["LOAI"] = 1;
            keys0["MA_CT_ME"] = _invoice.Mact;
            keys0["NHOM"] = "00";
            keys0["NAMETAG"] = control.Name.ToUpper();
            if(!string.IsNullOrEmpty(control.AccessibleName)) keys0["NAMEVAL"] = control.AccessibleName.ToUpper();
            // Lấy dữ liệu mặc định của Form parent.
            var defaultData = V6BusinessHelper.GetDefaultValueData(1, _invoice.Mact, "", ItemID, "nhom='00'");
            DataRow dataRow = null;
            foreach (DataRow row in defaultData.Rows)
            {
                if (row["NAMETAG"].ToString().Trim().ToUpper() == control.Name.ToUpper())
                {
                    dataRow = row;
                    break;
                }

                if (!string.IsNullOrEmpty(control.AccessibleName) && row["NAMEVAL"].ToString().Trim().ToUpper() == control.AccessibleName.ToUpper())
                {
                    dataRow = row;
                    break;
                }
            }

            if (dataRow != null) // nếu tồn tại dữ liệu.
            {
                v6mode = V6Mode.Edit;
                keys = new Dictionary<string, object>();
                keys["UID"] = dataRow["UID"];
            }
            else
            {
                v6mode = V6Mode.Add;
                keys0["KIEU"] = "0";
            }
            V6ControlFormHelper.CallShowAlinitAddEdit(v6mode, keys, keys0);
        }
    }
}
