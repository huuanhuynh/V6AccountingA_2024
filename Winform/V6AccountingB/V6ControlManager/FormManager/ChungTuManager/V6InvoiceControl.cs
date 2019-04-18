using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Controls.GridView;
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
            _invoice = new V6InvoiceBase(maCt);
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
        public virtual void OnInvoiceChanged(string sttRec)
        {
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
        
        public bool chon_accept_flag_add;

        //public string MaCt { get; set; }
        public string _sttRec0 = "";
        public string _sttRec02 = "";
        public string _sttRec03 = "";
        public string _mMaNt0 = "";
        public string _maNt = "VND";
        public bool co_chon_don_hang;
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
        public string M_TYPE_SL_QD_ALL;

        /// <summary>
        /// List thứ tự field chi tiết.
        /// </summary>
        public List<string> _orderList = new List<string>();
        public List<string> _orderList2 = new List<string>();
        public List<string> _orderList3 = new List<string>();

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
            throw new System.NotImplementedException("Cần override.");
        }

        public virtual void EnableFunctionButtons()
        {
            throw new System.NotImplementedException("Cần override.");
        }

        /// <summary>
        /// Ẩn hiện, set Readonly cho các control.
        /// </summary>
        public virtual void EnableVisibleControls()
        {
            throw new System.NotImplementedException("Cần override.");
        }

        public void SetControlReadOnlyHide(Control container, V6InvoiceBase invoice, V6Mode mode)
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
        
        public IDictionary<string, object> PreparingDataAM(V6InvoiceBase invoice)
        {
            var addDataAM = GetData();
            addDataAM["STT_REC"] = _sttRec;
            addDataAM["MA_CT"] = invoice.Mact;
            return addDataAM;
        }

        public void LoadTag(V6InvoiceBase invoice, ControlCollection detailPanelControls)
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
        public virtual string GetSttRec(string maCt)
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
        public string GetSoCt0InitFilter()
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

        public void GetTonRow(DataGridViewRow row, HD_Detail detail1, DateTime ngay_ct)
        {
            try
            {
                var cell_MA_VT = row.Cells["MA_VT"];
                var cell_MA_KHO_I = row.Cells["MA_KHO_I"];
                var cell_MA_LO = row.Cells["MA_LO"];
                V6VvarTextBox txtmavt = new V6VvarTextBox() {VVar = "MA_VT"};
                txtmavt.Text = cell_MA_VT.Value.ToString();
                txtmavt.RefreshLoDateYnValue();
                V6VvarTextBox txtmakhoi = new V6VvarTextBox() {VVar = "MA_KHO"};
                txtmakhoi.Text = cell_MA_KHO_I.Value.ToString();
                txtmakhoi.RefreshLoDateYnValue();

                GetTon13Row(row, detail1, txtmavt, txtmakhoi, ngay_ct);
                if (txtmavt.VITRI_YN)
                {
                    if (txtmavt.LO_YN && txtmavt.DATE_YN)
                    {
                        GetViTriLoDateRow(row, detail1, txtmavt, txtmakhoi, ngay_ct);
                    }
                    else
                    {
                        GetViTriRow(row, detail1, txtmavt, txtmakhoi, ngay_ct);
                    }
                }
                else
                {
                    if (cell_MA_LO.Value.ToString().Trim() == "") GetLoDateRow(row, detail1, txtmavt, txtmakhoi, ngay_ct);
                    else GetLoDate13Row(row, detail1, txtmavt, txtmakhoi, ngay_ct);
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
                var cell_HE_SO1 = row.Cells["HE_SO1"];
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
                                    cell_TON13.Value = new_soLuong / ObjectAndString.ObjectToDecimal(cell_HE_SO1.Value);
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
                var cell_HE_SO1 = row.Cells["HE_SO1"];
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
                            cell_TON13.Value = new_soLuong / ObjectAndString.ObjectToDecimal(cell_HE_SO1.Value);
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

        public void GetViTriRow(DataGridViewRow row, HD_Detail detail1, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = row.Cells["STT_REC0"];
                var cell_TON13 = row.Cells["TON13"];
                var cell_HE_SO1 = row.Cells["HE_SO1"];
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
                            cell_TON13.Value = new_soLuong / ObjectAndString.ObjectToDecimal(cell_HE_SO1.Value);
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

        private void GetLoDateRow(DataGridViewRow row, HD_Detail detail1, V6VvarTextBox txtmavt, V6VvarTextBox txtmakhoi, DateTime dateNgayCT)
        {
            try
            {
                var cell_STT_REC0 = row.Cells["STT_REC0"];
                var cell_TON13 = row.Cells["TON13"];
                var cell_TON13QD = row.Cells["TON13QD"];
                var cell_HE_SO1 = row.Cells["HE_SO1"];
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
                            cell_TON13.Value = new_soLuong / ObjectAndString.ObjectToDecimal(cell_HE_SO1.Value);
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
                var cell_HE_SO1 = row.Cells["HE_SO1"];
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
                            cell_TON13.Value = new_soLuong / ObjectAndString.ObjectToDecimal(cell_HE_SO1.Value);
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

        /// <summary>
        /// Gán dữ liệu mặc định khi bấm mới chi tiết. Cần override
        /// </summary>
        public virtual void SetDefaultDetail()
        {
            
        }

        /// <summary>
        /// <para>Gán InitFilter cho các control được chỉ định trong V6Options.M_V6_ADV_FILTER</para>
        /// <para>Field:TableName:1 => lấy initfilter từ V6Login theo TableName.</para>
        /// <para>Field:TableName:1 => nếu là 1 sẽ gắn thêm [Status] &lt;&gt; '0'</para>
        /// </summary>
        public void SetInitFilterAll()
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

        public void ShowViewInfoData(V6InvoiceBase invoice)
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

        public void TinhChietKhauChiTiet(bool nhapTien, V6NumberTextBox _ck_textbox, V6NumberTextBox _ck_nt_textbox,
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
        
        public void TinhChietKhauChiTietRow(bool nhapTien, DataGridViewRow row,
            Decimal txtTyGia_Value)
        {
            try
            {
                var _ck = row.Cells["CK"];
                var _ck_nt = row.Cells["CK_NT"];
                var _pt_cki = row.Cells["PT_CKI"];
                var _tien_nt2 = row.Cells["TIEN_NT2"];
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
                thue.Value = _maNt == _mMaNt0 ? thue_nt.Value : V6BusinessHelper.Vround(tien * thueSuat / 100, M_ROUND_NT);
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


        private void V6InvoiceControl_Load(object sender, EventArgs e)
        {
            SetInitFilterAll();
            LoadLanguage();
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
            var v6validConfig = ConfigManager.GetV6ValidConfig(Invoice.Mact, 1);
            
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
        /// <param name="data"></param>
        /// <param name="firstField"></param>
        /// <returns>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</returns>
        public string ValidateDetailData(HD_Detail detail1, V6InvoiceBase Invoice, IDictionary<string, object> data, out string firstField)
        {
            string error = "";
            firstField = null;
            try
            {
                var config = ConfigManager.GetV6ValidConfig(Invoice.Mact, 2);
                
                if (config != null && config.HaveInfo)
                {
                    //Trường bắt buột nhập dữ liệu.
                    var a_fields = ObjectAndString.SplitString(config.A_field);
                    foreach (string field in a_fields)
                    {
                        string FIELD = field.Trim().ToUpper();
                        string label = FIELD;
                        if (!data.ContainsKey(FIELD))
                        {
                            //error += string.Format("{0}: [{1}]\n", V6Text.NoData, FIELD);
                            continue;
                        }

                        V6ColumnStruct columnS = Invoice.ADStruct[FIELD];
                        object value = data[FIELD];
                        
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
        
        /// <summary>
        /// <para>Kiểm tra dữ liệu chi tiết thuế hợp lệ quy định trong V6Valid.</para>
        /// <para>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</para>
        /// </summary>
        /// <param name="detail3"></param>
        /// <param name="Invoice"></param>
        /// <param name="data"></param>
        /// <param name="firstField"></param>
        /// <returns></returns>
        public string ValidateDetail2Data(HD_Detail detail3, V6InvoiceBase Invoice, IDictionary<string, object> data, out string firstField)
        {
            string error = "";
            firstField = null;
            try
            {
                var config = ConfigManager.GetV6ValidConfig(Invoice.Mact, 4);
                
                if (config != null && config.HaveInfo)
                {
                    //Trường bắt buột nhập dữ liệu.
                    var a_fields = ObjectAndString.SplitString(config.A_field);
                    foreach (string field in a_fields)
                    {
                        string FIELD = field.Trim().ToUpper();
                        string label = FIELD;
                        if (!data.ContainsKey(FIELD))
                        {
                            //error += string.Format("{0}: [{1}]\n", V6Text.NoData, FIELD);
                            continue;
                        }

                        V6ColumnStruct columnS = Invoice.ADStruct[FIELD];
                        object value = data[FIELD];
                        
                        if (ObjectAndString.IsDateTimeType(columnS.DataType))
                        {
                            if (value == null)
                            {
                                var lbl = detail3.GetControlByName("lbl" + FIELD);
                                if (lbl != null) label = lbl.Text;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                        else if (ObjectAndString.IsNumberType(columnS.DataType))
                        {
                            if (ObjectAndString.ObjectToDecimal(value) == 0)
                            {
                                var lbl = detail3.GetControlByName("lbl" + FIELD);
                                if (lbl != null) label = lbl.Text;
                                error += V6Text.NoInput + " [" + label + "]\n";
                                if (firstField == null) firstField = FIELD;
                            }
                        }
                        else // string
                        {
                            if (("" + value).Trim() == "")
                            {
                                var lbl = detail3.GetControlByName("lbl" + FIELD);
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
            var infos = invoice.LoadDataReferenceInfo(V6Setting.Language, ItemID);
            //Duyet txtmahttt
            //from TK_NO to MA_NX
            //data[to] = from
            //Chuẩn bị dữ liệu để gán lên form
            SortedDictionary<string, object> someData = new SortedDictionary<string, object>();
            foreach (DefaultValueInfo item in infos)
            {
                if (!string.IsNullOrEmpty(item.AName)) continue;
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

        public void XuLyThayDoiTyGia(V6NumberTextBox txtTyGia, CheckBox chkSuaTien)
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
                if (V6Options.M_CHK_XUAT == "0") return true;

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
        public V6PrintMode _print_flag = V6PrintMode.DoNoThing;
        public int _print_flag_tick_count = 0;
        public void BasePrint(V6InvoiceBase Invoice, string sttRec_In, V6PrintMode printMode,
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

        /// <summary>
        /// No_use
        /// </summary>
        public Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        public Type Event_program;
        public Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        
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
        public object InvokeFormEvent(string eventName)
        {
            try
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
        public void ChucNang_ThayThe(V6InvoiceBase invoice)
        {
            try
            {
                //Hien form chuc nang co options *-1 or input
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;

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

                int field_index = dataGridView1.CurrentCell.ColumnIndex;
                string FIELD = dataGridView1.CurrentCell.OwningColumn.DataPropertyName.ToUpper();
                V6ColorTextBox textBox = detail1.GetControlByAccessibleName(FIELD) as V6ColorTextBox;
                Type valueType = dataGridView1.CurrentCell.OwningColumn.ValueType;

                //Check
                if (textBox == null)
                {
                    ShowParentMessage(V6Text.Text("UNKNOWNOBJECT"));
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
                                object resetValue;
                                V6ColumnStruct struct0 = invoice.ADStruct[FIELD];
                                if (struct0.AllowNull)
                                {
                                    resetValue = DBNull.Value;
                                }
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

        public void ViewLblKieuPost(Label lblKieuPostColor, ComboBox cboKieuPost)
        {
            try
            {
                lblKieuPostColor.Text = cboKieuPost.Text;
                
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

                if (cboKieuPost.SelectedValue == null) return;
                var selectedRow = ((DataRowView) cboKieuPost.SelectedItem).Row;
                var color_name = selectedRow["ColorV"].ToString().Trim();
                if (color_name != "")
                {
                    var color = ObjectAndString.StringToColor(color_name);
                    lblKieuPostColor.ForeColor = color;
                }
                else
                {
                    lblKieuPostColor.ForeColor = Color.Black;
                }
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

        
    }
}
