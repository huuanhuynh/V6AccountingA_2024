using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6ReportControls;
using V6RptEditor;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using System.Globalization;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6Tools.V6Export;
using V6ControlManager.FormManager.ReportManager.DXreport;

namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    public partial class ReportTreeView44 : V6FormControl
    {
        #region Biến toàn cục
        private ReportDocument _rpDoc0;

        private string _reportProcedure;
        /// <summary>
        /// reportFile
        /// </summary>
        public string _Ma_File;
        //private string _program, _reportFile, _reportTitle, _reportTitle2;
        private string _program, _reportTitle, _reportTitle2;
        private string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        /// <summary>
        /// Advance filter get albc, nhận từ filter cha để lọc.
        /// </summary>
        public string Advance = "";

        private DataTable MauInData;
        private DataView MauInView;
        public AlbcConfig _albcConfig = new AlbcConfig();

        /// <summary>
        /// Danh sách event_method của Form_program.
        /// </summary>
        private Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        private Type Form_program;
        private Dictionary<string, object> All_Objects = new Dictionary<string, object>();

        private string ReloadData
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["Reload_data"].ToString().Trim();
                }
                return result;
            }
        }

        private object InvokeFormEvent(string eventName)
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(eventName))
                {
                    var method_name = Event_Methods[eventName];
                    return V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
            }
            return null;
        }

        private void CreateFormProgram()
        {
            try
            {
                var AlbcData = V6BusinessHelper.Select(V6TableName.Albc, AlbcKeys, "*").Data;
                if (AlbcData.Rows.Count == 0) return;

                var dataRow = AlbcData.Rows[0];
                var xml = dataRow["MMETHOD"].ToString().Trim();
                if (xml == "") return;
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xml));
                if (ds.Tables.Count <= 0) return;

                var data = ds.Tables[0];

                string using_text = "";
                string method_text = "";
                foreach (DataRow event_row in data.Rows)
                {
                    var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                    var method_name = event_row["method"].ToString().Trim();
                    Event_Methods[EVENT_NAME] = method_name;

                    using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                    method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                }
                Form_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "M" + _program, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
            }
        }

        private void CreateFormControls()
        {
            try
            {
                txtM_TEN_NLB.Text = V6Options.GetValueNull("M_TEN_NLB");
                txtM_TEN_NLB2.Text = V6Options.GetValueNull("M_TEN_NLB2");

                var M_COMPANY_BY_MA_DVCS = V6Options.ContainsKey("M_COMPANY_BY_MA_DVCS") ? V6Options.GetValue("M_COMPANY_BY_MA_DVCS").Trim() : "";
                if (M_COMPANY_BY_MA_DVCS == "1" && V6Login.MadvcsCount == 1)
                {
                    var dataRow = V6Setting.DataDVCS;
                    var GET_FIELD = "TEN_NLB";
                    if (dataRow.Table.Columns.Contains(GET_FIELD))
                        txtM_TEN_NLB.Text = V6Setting.DataDVCS[GET_FIELD].ToString();
                    GET_FIELD = "TEN_NLB2";
                    if (dataRow.Table.Columns.Contains(GET_FIELD))
                        txtM_TEN_NLB2.Text = V6Setting.DataDVCS[GET_FIELD].ToString();
                }

                //{ Tuanmh Get name 27/08/2018
                var TEN_NLB_LOGIN = V6Login.XmlInfo.TEN_NLB_LOGIN.Trim();
                var TEN_NLB_LOGIN2 = V6Login.XmlInfo.TEN_NLB_LOGIN2.Trim();
                if (TEN_NLB_LOGIN != "")
                {
                    txtM_TEN_NLB.Text = TEN_NLB_LOGIN;
                }
                if (TEN_NLB_LOGIN2 != "")
                {
                    txtM_TEN_NLB2.Text = TEN_NLB_LOGIN2;
                }
                //}

                FilterControl = QuickReportManager.AddFilterControl44Base(_program, _reportProcedure, _Ma_File, panel1, toolTipV6FormControl);
                All_Objects["thisForm"] = this;
                InvokeFormEvent(FormDynamicEvent.AFTERADDFILTERCONTROL);
                //QuickReportManager.MadeFilterControls(FilterControl, _program, All_Objects, toolTipV6FormControl);
                FilterControl.MadeFilterControls(_program, All_Objects, toolTipV6FormControl);

                //AddFilterControl(_program);
                SetStatus2Text();
                //FilterControl.Call1(imageList1);
                //treeListViewAuto1.SetGroupAndNameFieldList(
                //    FilterControl.String1.Split(','),
                //    FilterControl.String2.Split(','));

                var lineList = FilterControl.GetFilterLineList();
                foreach (KeyValuePair<string, FilterLineBase> item in lineList)
                {
                    All_Objects[item.Key] = item.Value;
                }

                SetStatus2Text();



                InvokeFormEvent(FormDynamicEvent.AFTERADDFILTERCONTROL);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateFormControls", ex);
            }
        }

        public DataSet DataSet
        {
            get { return _ds; }
            set { _ds = value; }
        }
        private DataSet _ds;
        private DataTable _tbl1, _tbl2, _tbl3;
        private DataView _view1, _view2, _view3;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        private List<SqlParameter> _pList;

        public V6PrintMode PrintMode = V6PrintMode.DoNoThing;
        /// <summary>
        /// Tên file excel tự động xuất.
        /// </summary>
        public string AutoExportExcel = null;
        public string PrinterName { get; set; }
        private int _printCopy = 1;

        public int PrintCopies
        {
            get { return _printCopy; }
            set { _printCopy = value; }
        }

        public delegate void PrintReportSuccessDelegate(Control sender);
        public event PrintReportSuccessDelegate PrintSuccess;
        protected virtual void CallPrintSuccessEvent()
        {
            var handler = PrintSuccess;

            if (handler != null)
            {
                handler(this);
            }
        }

        /// <summary>
        /// MA_FILE, MAU, LAN, REPORT
        /// </summary>
        private SortedDictionary<string, object> AlbcKeys
        {
            get
            {
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _Ma_File},
                        {"MAU", MAU},
                        {"LAN", LAN},
                        {"REPORT", ReportFile}
                    };
                return keys;
            }
        }

        public string MAU
        {
            get { return rTienViet.Checked ? "VN" : "FC"; }
            set {
                switch (value)
                {
                    case "VN":
                        rTienViet.Checked = true;
                        break;
                    case "FC":
                        rNgoaiTe.Checked = true;
                        break;
                }
            }
        }

        public string LAN
        {
            get
            {
                return rTiengViet.Checked ? "V" : rEnglish.Checked ? "E" : rBothLang.Checked ? "B" : V6Login.SelectedLanguage;
            }
            set 
            {
                switch (value)
                {
                    case "V":
                        rTiengViet.Checked = true;
                        break;
                    case "E":
                        rEnglish.Checked = true;
                        break;
                    case "B":
                        rBothLang.Checked = true;
                        break;
                    default:
                        rCurrent.Checked = true;
                        break;
                }
            }
        }

        public DataRow MauInSelectedRow
        {
            get
            {
                if (cboMauIn.SelectedIndex == -1) return null;
                var data = MauInView.ToTable();
                return data.Rows[cboMauIn.SelectedIndex];
            }
        }

        #region ===== EXTRA_INFOR =====
        public SortedDictionary<string, string> EXTRA_INFOR
        {
            get
            {
                //if (_extraInfor == null || _extraInfor.Count == 0)
                {
                    GetExtraInfor();
                }
                return _extraInfor;
            }
        }

        private SortedDictionary<string, string> _extraInfor = null;

        private void GetExtraInfor()
        {
            _extraInfor = new SortedDictionary<string, string>();
            if (MauInSelectedRow == null) return;
            _extraInfor.AddRange(ObjectAndString.StringToStringDictionary("" + MauInSelectedRow["EXTRA_INFOR"]));
        }

        /// <summary>
        /// MauIn (Albc) EXTRA_INFOR NOPRINTER
        /// </summary>
        private bool NOPRINTER
        {
            get
            {
                if (EXTRA_INFOR.ContainsKey("NOPRINTER")) return ObjectAndString.ObjectToBool(EXTRA_INFOR["NOPRINTER"]);
                return false;
            }
        }
        #endregion EXTRA_INFOR
        
        public string ReportFile
        {
            get
            {
                var result = _Ma_File;
                if (MauInSelectedRow != null)
                {
                    result = cboMauIn.SelectedValue.ToString().Trim();
                }
                return result;
            }
        }
        
        
        

        public string RPT_DIR
        {
            get
            {
                string result = "";
                if (MauInSelectedRow != null)
                if (MauInData != null && (MauInData.Columns.Contains("RPT_DIR") && MauInSelectedRow["RPT_DIR"] != null))
                {
                    string rpt_dir = MauInSelectedRow["RPT_DIR"].ToString().Trim();
                    if (rpt_dir != "") result += rpt_dir + @"\";
                }
                return result;
            }
        }

        public string ReportFileFull
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".rpt";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + ".rpt";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFullF7
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "F7.rpt";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "F7.rpt";//_reportFile gốc
                }
                return result;
            }
        }

        /// <summary>
        /// File excel mẫu.
        /// </summary>
        public string ExcelTemplateFileFull
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".xls";
                if (File.Exists(result + "x")) result += "x";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + ".xls";
                    if (File.Exists(result + "x")) result += "x";
                }
                return result;
            }
        }

        /// <summary>
        /// File excel mẫu (view).
        /// </summary>
        public string ExcelTemplateFileView
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_view.xls";
                if (!File.Exists(result))
                {
                    result = ExcelTemplateFileFull;
                }
                return result;
            }
        }
        
        /// <summary>
        /// File excel mẫu HTKK.
        /// </summary>
        public string ExcelTemplateFileFullHTKK
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "HTKK.xls";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "HTKK.xls";
                }
                return result;
            }
        }
        
        /// <summary>
        /// File excel mẫu ONLINE.
        /// </summary>
        public string ExcelTemplateFileFullONLINE
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "ONLINE.xls";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "ONLINE.xls";
                }
                return result;
            }
        }

        private bool print_one;
        private string DefaultPrinter
        {
            get
            {
                var result = "";
                try
                {
                    if (MauInSelectedRow != null)
                    {
                        var y_n = (MauInSelectedRow["Printer_yn"] ?? "").ToString().Trim();
                        if (y_n == "1" || print_one)
                            result = (MauInSelectedRow["Printer_def"] ?? "").ToString().Trim();
                    }
                }
                catch (Exception)
                {
                    
                }
                return result;
            }
            set
            {
                try
                {
                    var y_n = false;
                    var udata = new SortedDictionary<string, object>();
                    udata.Add("PRINTER_DEF", value);

                    foreach (DataRow row in MauInData.Rows)
                    {
                        if (row["Report"].ToString().Trim().ToUpper() == ReportFile.ToUpper())
                        {
                            row["PRINTER_DEF"] = value;
                            y_n = (row["Printer_yn"] ?? "").ToString().Trim() == "1";
                            break;
                        }
                    }
                    if(y_n) V6BusinessHelper.Update(V6TableName.Albc, udata, AlbcKeys);
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".SetDefaultPrinter", ex);
                }
            }
        }

        #endregion 
        
        public ReportTreeView44(string itemId, string program, string reportProcedure,
            string reportFile, string reportTitle, string reportTitle2,
            string reportFileF5, string reportTitleF5, string reportTitle2F5)
        {
            m_itemId = itemId;
            Name = itemId;
            _program = program;
            _reportProcedure = reportProcedure;
            _Ma_File = reportFile;
            _reportTitle = reportTitle;
            _reportTitle2 = reportTitle2;

            _reportFileF5 = reportFileF5;
            _reportTitleF5 = reportTitleF5;
            _reportTitle2F5 = reportTitle2F5;

            V6ControlFormHelper.AddLastAction(GetType() + " " + program);
            
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                if (V6Login.IsAdmin) chkHienTatCa.Enabled = true;
                rCurrent.Text = V6Login.SelectedLanguageName;
                if (V6Login.SelectedLanguage == "V" || V6Login.SelectedLanguage == "E") rCurrent.Visible = false;
                CreateFormProgram();
                CreateFormControls();
                CheckRightReport();
                if (V6Options.M_R_FONTSIZE > 8)
                {
                    treeListViewAuto1.Font = new Font(treeListViewAuto1.Font.FontFamily, V6Options.M_R_FONTSIZE);
                }

                menuCopy.Click += menuCopy_Click;
                menuCopyValue.Click += menuCopy_Click;
                menuCopyAll.Click += menuCopy_Click;

                InvokeFormEvent(FormDynamicEvent.INIT);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
            }
        }

        Point _point;
        void LabelSummary_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _point = e.Location;
                copyMenuStrip1.Show(this, new Point(lblSummary.Left + _point.X, lblSummary.Top + _point.Y));
            }
        }

        void menuCopy_Click(object sender, EventArgs e)
        {
            try
            {
                string textPart = "";
                // tính toán vị trí.
                if (_copyValues.Count == 0)
                {
                    Clipboard.Clear();
                    return;
                }
                int onePartW = lblSummary.Width / _copyValues.Count;
                int index = _point.X / onePartW;
                var item = _copyValues[index];

                if (sender == menuCopy)
                {
                    Clipboard.SetText(item.stringValue);
                }
                else if (sender == menuCopyValue)
                {
                    string text = item.decimalValue.ToString(CultureInfo.InstalledUICulture);
                    Clipboard.SetText(text);
                }
                else if (sender == menuCopyAll)
                {
                    Clipboard.SetText(lblSummary.Text);
                }
                return;
            }
            catch (Exception ex)
            {
                Clipboard.SetText("ERR:" + ex.Message);
                return;
            }
            Clipboard.Clear();
        }

        private void CheckRightReport()
        {
            bool no_print = false;
            if (!V6Login.UserRight.AllowPrint(ItemID, ItemID))
            {
                no_print = true;
                crystalReportViewer1.ShowPrintButton = false;
                crystalReportViewer1.ShowExportButton = false;
                contextMenuStrip1.Items.Remove(exportToPdfMenu);
            }
            if (!V6Login.UserRight.AllowView(ItemID, ItemID))
            {
                crystalReportViewer1.InvisibleTag();
                if (no_print)
                {
                    while (contextMenuStrip1.Items.Count > 0)
                    {
                        contextMenuStrip1.Items.RemoveAt(0);
                    }
                }
            }
        }

        private void MyInit2()
        {
            try
            {
                if (V6Setting.ReportLanguage == "V")
                {
                    rTiengViet.Checked = true;
                }
                else if (V6Setting.ReportLanguage == "E")
                {
                    rEnglish.Checked = true;
                }
                else if (V6Setting.ReportLanguage == "B")
                {
                    rBothLang.Checked = true;
                }
                else
                {
                    rCurrent.Checked = true;
                }
                LoadComboboxSource();
                LoadDefaultData(4, "", _Ma_File, m_itemId, "");

                string key3 = "1";
                var menuRow = V6Menu.GetRowByMact(ItemID);
                if (menuRow != null)
                {
                    key3 = ("" + menuRow["Key3"]).Trim().ToUpper();
                    if (key3 == "") key3 = "1";
                }

                if (!V6Login.IsAdmin)
                {
                    if (menuRow != null)
                    {
                        var user_acc = V6Login.UserInfo["USER_ACC"].ToString().Trim();
                        if (user_acc != "1")
                        {
                            if (!key3.Contains("1")) exportToExcelTemplateMenu.Visible = false;
                            if (!key3.Contains("2")) exportToExcelViewMenu.Visible = false;
                            if (!key3.Contains("3")) exportToExcelMenu.Visible = false;
                            if (!key3.Contains("4")) exportToXmlMenu.Visible = false;
                            if (!key3.Contains("5")) printGridMenu.Visible = false;
                            if (!key3.Contains("6")) viewDataMenu.Visible = false;
                            if (!key3.Contains("7")) exportToPdfMenu.Visible = false;
                            if (!key3.Contains("8")) viewInvoiceInfoMenu.Visible = false;
                        }
                    }
                }

                if (key3.Length > 0)
                    switch (key3[0])
                    {
                        case '1': DefaultMenuItem = exportToExcelTemplateMenu; break;
                        case '2': DefaultMenuItem = exportToExcelViewMenu; break;
                        case '3': DefaultMenuItem = exportToExcelMenu; break;
                        case '4': DefaultMenuItem = exportToXmlMenu; break;
                        case '5': DefaultMenuItem = printGridMenu; break;
                        case '6': DefaultMenuItem = viewDataMenu; break;
                        case '7': DefaultMenuItem = exportToPdfMenu; break;
                        //case '8': DefaultMenuItem = viewInvoiceInfoMenu; break;
                    }

                txtReportTitle.Text = _albcConfig.TITLE;

                InvokeFormEvent(FormDynamicEvent.INIT2);
                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2 " + ReportFileFull, ex);
            }
        }
        private ToolStripMenuItem DefaultMenuItem = null;

        private void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_Ma_File, "", "", Advance);
            
            if (MauInData.Rows.Count > 0)
            {
                MauInView = new DataView(MauInData);
                SetFormReportFilter();
                cboMauIn.ValueMember = "report";
                cboMauIn.DisplayMember = V6Setting.IsVietnamese ? "caption" : "caption2";
                cboMauIn.DataSource = MauInView;
                cboMauIn.ValueMember = "report";
                cboMauIn.DisplayMember = V6Setting.IsVietnamese ? "caption" : "caption2";
                _albcConfig = new AlbcConfig(MauInSelectedRow.ToDataDictionary());
                //GetSumCondition();
            }
            else
            {
                cboMauIn.Enabled = false;
                btnSuaTTMauBC.Enabled = false;
                //btnThemMauBC.Enabled = false;
            }
        }

        private void SetFormReportFilter()
        {
            try
            {
                MauInView.RowFilter = "mau='" + MAU + "'" + " and lan='" + LAN + "'"
                    + (chkHienTatCa.Checked ? "" : " and status='1'");

                if (MauInView.Count == 0)
                {
                    cboMauIn.Enabled = false;
                    btnSuaTTMauBC.Enabled = false;
                    //btnThemMauBC.Enabled = false;
                }
                else
                {
                    V6ControlFormHelper.EnableControls(cboMauIn, btnSuaTTMauBC);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void GetFormReportTitle(int selectedIndex)
        {
            try
            {
                DataTable data = MauInView.ToTable();
                DataRow row = data.Rows[selectedIndex];
                string title = row["title"].ToString();
                txtReportTitle.Text = title;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyInit2();
            if (_ds != null && _ds.Tables.Count > 0)
            {
                SetTBLdata();
                ViewFooter();
                ShowReport();
            }
            else if (PrintMode != V6PrintMode.DoNoThing)
            {
                try
                {
                    btnNhanImage = btnNhan.Image;
                    FormManagerHelper.HideMainMenu();
                    MakeReport2(PrintMode, PrinterName, _printCopy);
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message);
                }
            }
        }

        
        public FilterBase FilterControl { get; set; }
        public IDictionary<string, object> SelectedRowData { get; set; }
        
        private void AddFilterControl(string program)
        {
            FilterControl = Filter.Filter.GetFilterControl(program, _reportProcedure, _Ma_File, toolTipV6FormControl);
            panel1.Controls.Add(FilterControl);
            FilterControl.String1ValueChanged += FilterControl_String1ValueChanged;
            FilterControl.Check1ValueChanged += FilterControl_Check1ValueChanged;
            FilterControl.Focus();
        }

        void FilterControl_Check1ValueChanged(bool oldvalue, bool newvalue)
        {
            try
            {
                treeListViewAuto1.ViewName = !newvalue;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ChangeView: " + ex.Message);
            }
        }

        void FilterControl_String1ValueChanged(string oldvalue, string newvalue)
        {
            try
            {
                string FilterControl_String1 = _pList[0].Value.ToString();
                string FilterControl_String2 = _pList[1].Value.ToString();
                FilterControl_Call1(imageList1, FilterControl_String1);
                treeListViewAuto1.SetGroupAndNameFieldList_ResetView(
                    FilterControl_String1.Split(','),
                    FilterControl_String2.Split(','));
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ChangeGroup: " + ex.Message);
            }
        }

        /// <summary>
        /// Update Image list.
        /// </summary>
        /// <param name="image_list"></param>
        /// <param name="groupFields"></param>
        private void FilterControl_Call1(ImageList image_list, string groupFields)
        {
            var lever_count = ObjectAndString.SplitString(groupFields).Length;
            if (image_list != null)
            {
                if (lever_count > 0) image_list.Images[lever_count - 1] = Properties.Resources.Box16;
                if (lever_count > 1) image_list.Images[lever_count - 2] = Properties.Resources.House16;
                if (lever_count > 2) image_list.Images[lever_count - 3] = Properties.Resources.TreeFolderYellowk16;
                if (lever_count > 3) image_list.Images[lever_count - 4] = Properties.Resources.Add16;
                if (lever_count > 4) image_list.Images[lever_count - 5] = Properties.Resources.Add16;
                if (lever_count > 5) image_list.Images[lever_count - 6] = Properties.Resources.Add16;
            }
        }
        
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (_executing)
            {
                return;
            }

            try
            {
                btnNhanImage = btnNhan.Image;
                FormManagerHelper.HideMainMenu();
                MakeReport2(V6PrintMode.DoNoThing, null, 1);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message);
            }
        }
       
        private bool GenerateProcedureParameters()
        {
            try
            {
                _pList = new List<SqlParameter>();
                var tList = FilterControl.GetFilterParameters();
                foreach (SqlParameter p in tList)
                {
                    _pList.Add(new SqlParameter(p.ParameterName, p.Value));
                }
                //_pList.Add(new SqlParameter("@cKey", "1=1" + (sKey.Length>0?" And " +sKey:"")));
                return true;
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("GenerateProcedureParameters: " + ex.Message);
                return false;
            }
        }

        public IDictionary<string, object> ReportDocumentParameters;

        /// <summary>
        /// Lưu ý: chạy sau khi add dataSource để tránh lỗi nhập parameter value
        /// </summary>
        /// <param name="rpDoc"></param>
        private void SetAllReportParams(ReportDocument rpDoc)
        {
            ReportDocumentParameters = new SortedDictionary<string, object>
            {
                {"Decimals", 0},
                {"ThousandsSeparator", V6Options.M_NUM_SEPARATOR},

                {"DecimalSymbol", V6Options.M_NUM_POINT},
                {"DecimalsSL", V6Options.M_IP_R_SL},
                {"DecimalsDG", V6Options.M_IP_R_GIA},
                {"DecimalsDGNT", V6Options.M_IP_R_GIANT},
                {"DecimalsTT", V6Options.M_IP_R_TIEN},
                {"DecimalsTTNT", V6Options.M_IP_R_TIENNT},
                {"Title", txtReportTitle.Text.Trim()},
                // V6Soft
                {"M_TEN_CTY", V6Soft.V6SoftValue["M_TEN_CTY"].ToUpper()},
                {"M_TEN_TCTY", V6Soft.V6SoftValue["M_TEN_TCTY"].ToUpper()},
                {"M_DIA_CHI", V6Soft.V6SoftValue["M_DIA_CHI"]},
                {"M_TEN_CTY2", V6Soft.V6SoftValue["M_TEN_CTY2"].ToUpper()},
                {"M_TEN_TCTY2", V6Soft.V6SoftValue["M_TEN_TCTY2"].ToUpper()},
                {"M_DIA_CHI2", V6Soft.V6SoftValue["M_DIA_CHI2"]},
                // V6option
                {"M_MA_THUE", V6Options.GetValue("M_MA_THUE")},
                {"M_RTEN_VSOFT", V6Options.GetValue("M_RTEN_VSOFT")},

                {"M_TEN_NLB", txtM_TEN_NLB.Text.Trim()},
                {"M_TEN_NLB2", txtM_TEN_NLB2.Text.Trim()},
                {"M_TEN_KHO_BD", V6Options.GetValue("M_TEN_KHO_BD")},
                {"M_TEN_KHO2_BD", V6Options.GetValue("M_TEN_KHO2_BD")},
                {"M_DIA_CHI_BD", V6Options.GetValue("M_DIA_CHI_BD")},
                {"M_DIA_CHI2_BD", V6Options.GetValue("M_DIA_CHI2_BD")},

                {"M_TEN_GD", V6Options.GetValue("M_TEN_GD")},
                {"M_TEN_GD2", V6Options.GetValue("M_TEN_GD2")},
                {"M_TEN_KTT", V6Options.GetValue("M_TEN_KTT")},
                {"M_TEN_KTT2", V6Options.GetValue("M_TEN_KTT2")},

                {"M_SO_QD_CDKT", V6Options.GetValue("M_SO_QD_CDKT")},
                {"M_SO_QD_CDKT2", V6Options.GetValue("M_SO_QD_CDKT2")},
                {"M_NGAY_QD_CDKT", V6Options.GetValue("M_NGAY_QD_CDKT")},
                {"M_NGAY_QD_CDKT2", V6Options.GetValue("M_NGAY_QD_CDKT2")},

                {"M_RFONTNAME", V6Options.GetValue("M_RFONTNAME")},
                {"M_R_FONTSIZE", V6Options.GetValue("M_R_FONTSIZE")},
            };

            V6Login.SetCompanyInfo(ReportDocumentParameters);

            if (FilterControl.RptExtraParameters != null)
            {
                ReportDocumentParameters.AddRange(FilterControl.RptExtraParameters, true);
            }

            var rptExtraParametersD = FilterControl.GetRptParametersD(_albcConfig.EXTRA_PARA, LAN);
            if (rptExtraParametersD != null)
            {
                ReportDocumentParameters.AddRange(rptExtraParametersD, true);
            }

            foreach (var item in edited_paras)
            {
                if (ReportDocumentParameters.ContainsKey(item.Key))
                    ReportDocumentParameters[item.Key] = item.Value;
            }

            string errors = "";
            foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
            {
                try
                {
                    rpDoc.SetParameterValue(item.Key, item.Value);
                }
                catch (Exception ex)
                {
                    errors += "\n" + item.Key + ": " + ex.Message;
                }
            }
            if (errors != "")
            {
                this.ShowErrorMessage(GetType() + ".SetAllReportParams: " + ReportFileFull + " " + errors);
            }
        }


        #region ==== LoadData MakeReport ====
        
        void LoadData()
        {
            object beforeLoadData = InvokeFormEvent(FormDynamicEvent.BEFORELOADDATA);

            try
            {
                if (beforeLoadData != null && !ObjectAndString.ObjectToBool(beforeLoadData))
                {
                    _message = V6Text.CheckInfor;
                    _executing = false;
                    return;
                }

                _executing = true;
                _executesuccess = false;
                var proc = "";
                if (!string.IsNullOrEmpty(FilterControl.ProcedureName))
                {
                    proc = FilterControl.ProcedureName;
                }
                else if (FilterControl is FilterDanhMuc)
                {
                    proc = "VPA_R_AL_ALL";
                }
                else
                {
                    proc = _reportProcedure;
                }
                _ds = V6BusinessHelper.ExecuteProcedure(proc, _pList.ToArray());
                SetTBLdata();

                _executesuccess = true;
            }
            catch (Exception ex)
            {
                _message = V6Text.Text("QUERY_FAILED") + "\n";
                if (ex.Message.StartsWith("Could not find stored procedure")) _message += V6Text.NotExist + ex.Message.Substring(31);
                else _message += ex.Message;

                _tbl1 = null;
                _tbl2 = null;
                _ds = null;
                _executesuccess = false;
            }
            _executing = false;
        }

        /// <summary>
        /// GenerateProcedureParameters();//Add các key
        /// var tLoadData = new Thread(LoadData);
        /// tLoadData.Start();
        /// timerViewReport.Start();
        /// </summary>
        private void MakeReport2(V6PrintMode printMode, string printerName, int printCopy = 1)
        {
            PrintMode = printMode;
            PrinterName = printerName;
            _printCopy = printCopy;

            if (GenerateProcedureParameters()) //Add các key khác
            {
                _executesuccess = false;
                _executing = true;
                var tLoadData = new Thread(LoadData);
                CheckForIllegalCrossThreadCalls = false;
                tLoadData.Start();
                timerViewReport.Start();
            }
        }	

        private void SetTBLdata()
        {
            if (_ds.Tables.Count > 0)
            {
                _tbl1 = _ds.Tables[0];
                _tbl1.TableName = "DataTable1";
                _view1 = new DataView(_tbl1);
            }
            if (_ds.Tables.Count > 1)
            {
                _tbl2 = _ds.Tables[1];
                _tbl2.TableName = "DataTable2";
                _view2 = new DataView(_tbl2);
            }
            else
            {
                _tbl2 = null;
            }

            if (_ds.Tables.Count > 2)
            {
                _tbl3 = _ds.Tables[2];
                _tbl3.TableName = "DataTable3";
                _view3 = new DataView(_tbl3);
            }
            else
            {
                _tbl3 = null;
            }
        }

        /// <summary>
        /// hàm mới: gán check Tiền Việt / Ngoại tệ
        /// </summary>
        /// <param name="VN_FC"></param>
        public virtual void SetLoaiTien(string VN_FC)
        {
            try
            {
                var rTienViet = GetControlByName("rTienViet") as RadioButton;
                var rNgoaiTe = GetControlByName("rNgoaiTe") as RadioButton;
                if (VN_FC == "FC")
                {
                    rTienViet.Checked = false;
                    rNgoaiTe.Checked = true;
                }
                else if (VN_FC == "VN")
                {
                    rTienViet.Checked = true;
                    rNgoaiTe.Checked = false;
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// cờ không gán lại ở Form_load
        /// </summary>
        bool _setVEBC = false;
        /// <summary>
        /// hàm mới: gán check loại report Tiếng Việt, Eng, Cả 2 hoặc đã chọn lúc login.
        /// </summary>
        /// <param name="VEBC"></param>
        public virtual void SetLanguage(string VEBC)
        {
            _setVEBC = true;
            try
            {
                //var rTiengViet = GetControlByName("rTiengViet") as RadioButton;
                //var rEnglish = GetControlByName("rEnglish") as RadioButton;
                //var rBothLang = GetControlByName("rBothLang") as RadioButton;
                //var rCurrent = GetControlByName("rCurrent") as RadioButton;
                if (VEBC == "V")
                {
                    rTiengViet.Checked = true;
                    rEnglish.Checked = false;
                    rBothLang.Checked = false;
                    rCurrent.Checked = false;
                }
                else if (VEBC == "E")
                {
                    rTiengViet.Checked = false;
                    rEnglish.Checked = true;
                    rBothLang.Checked = false;
                    rCurrent.Checked = false;
                }
                else if (VEBC == "B")
                {
                    rTiengViet.Checked = false;
                    rEnglish.Checked = false;
                    rBothLang.Checked = true;
                    rCurrent.Checked = false;
                }
                else if (VEBC == "C")
                {
                    rTiengViet.Checked = false;
                    rEnglish.Checked = false;
                    rBothLang.Checked = false;
                    rCurrent.Checked = true;
                }
            }
            catch
            {

            }
        }

        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                
                try
                {
                    ShowReport();
                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();
                    _executesuccess = false;
                    V6Message.Show(ex.Message);
                }
            }
            else if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                this.ShowErrorMessage(_message);
            }
        }

        private void Print(string printerName, ReportDocument rpDoc)
        {
            bool printerOnline = PrinterStatus.CheckPrinterOnline(printerName);
            
            if (printerOnline)
            {
                try
                {
                    V6ControlFormHelper.SetCrystalReportPrinterOptions(NOPRINTER, V6ControlFormHelper.PrinterSettings, rpDoc);
                    V6ControlFormHelper.PrintRptToPrinter(NOPRINTER, rpDoc, printerName, _printCopy, 0, 0);
                    CallPrintSuccessEvent();
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(GetType() + ".In lỗi!\n" + ex.Message, "V6Soft");
                }
            }
            else
            {
                btnIn.Enabled = true;
                this.ShowErrorMessage(GetType() + ".Không thể truy cập máy in!", "V6Soft");
            }
        }

        #endregion ==== LoadData MakeReport ====
        

        #region Linh tinh        

        public bool IsRunning
        {
            get { return _executing || _radioRunning; }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                ShowMainMessage(V6Text.ProcessNotComplete);
                return;
            }
            Dispose();
        }
        
        #endregion Linh tinh

        private void rbtTienTe_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;
            if (rTienViet.Checked)
            {
                
            }
            else
            {
                
            }
            SetFormReportFilter();

            if (_radioChange)
            {
                _radioChange = false;
            }
            else
            {
                if (ReloadData == "1")
                    MakeReport2(PrintMode, PrinterName, _printCopy);
                else
                    ViewReport();
            }
        }

        private bool _radioChange = false;
        private bool _radioRunning;
        private void rbtLanguage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsReady) return;

                if (((RadioButton)sender).Checked)
                {
                    _radioRunning = true;
                    _radioChange = true;
                    txtReportTitle.Text = rTiengViet.Checked ? _reportTitle : rEnglish.Checked ? _reportTitle2 : _reportTitle + "/" + _reportTitle2;
                    SetFormReportFilter();
                    if (MauInView.Count > 0 && cboMauIn.SelectedIndex >= 0)
                    {
                        txtReportTitle.Text = _albcConfig.TITLE;
                    }

                    if (_albcConfig.RELOAD_DATA == "1")
                        MakeReport2(PrintMode, PrinterName, _printCopy);
                    else
                        ViewReport();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".rbtLanguage_CheckedChanged", ex);
            }
            _radioRunning = false;
        }

        void ShowReport()
        {
            try
            {
                FilterControl.LoadDataFinish(_ds);
                All_Objects["_ds"] = _ds;
                InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);

                string FilterControl_String1 = _pList[0].Value.ToString();
                string FilterControl_String2 = _pList[1].Value.ToString();
                treeListViewAuto1.SetGroupAndNameFieldList(
                    FilterControl_String1.Split(','),
                    FilterControl_String2.Split(','));
                treeListViewAuto1.SetData(_tbl3, _albcConfig.GRDS_V1, _albcConfig.GRDH_LANG_V1, _albcConfig.GRDF_V1);
                //treeListViewAuto1.ExpandAll();
                //treeListViewAuto1.ViewName = true;
                FilterControl_Call1(imageList1, FilterControl_String1);

                ViewReport();
                if (PrintMode == V6PrintMode.AutoPrint)
                {
                    Print(PrinterName, _rpDoc0);
                    if (!IsDisposed) Dispose();
                }
                else if (PrintMode == V6PrintMode.AutoClickPrint)
                {
                    btnIn.PerformClick();
                }
                else if (PrintMode == V6PrintMode.AutoExportT)
                {
                    if (btnExport3.Visible && btnExport3.Enabled)
                        btnExport3.PerformClick();
                }
                else if (PrintMode == V6PrintMode.AutoLoadData)
                {
                    //Done;
                }

                treeListViewAuto1.Focus();
            }
            catch (Exception ex)
            {
                timerViewReport.Stop();
                _executesuccess = false;
                this.ShowErrorException(GetType() + ".ShowReport", ex);
            }
        }


        void ViewReport()
        {
			if (_ds == null) return;
            if (_ds.Tables[0].Rows.Count == 0)
			{
                this.ShowInfoMessage(V6Text.NoData, 500);
                return;
            }
            try
            {
                if (!V6Login.UserRight.AllowPrint(ItemID, ItemID))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }
                if (_ds == null) return;
                CleanUp();
                var rpDoc = new ReportDocument();
                rpDoc.Load(ReportFileFull);

                rpDoc.SetDataSource(_ds);

                SetAllReportParams(rpDoc);
                var infos = EXTRA_INFOR;
                if (infos.ContainsKey("RPTHIDE"))
                {
                    var names = ObjectAndString.SplitString(infos["RPTHIDE"]);
                    RPTHIDE(rpDoc, names);
                }

                crystalReportViewer1.ReportSource = rpDoc;
                _rpDoc0 = rpDoc;
                //crystalReportViewer1.Show();
                crystalReportViewer1.Zoom(1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewReport " + ReportFileFull, ex);
            }
        }

        private void RPTHIDE(ReportDocument rpDoc, IList<string> names)
        {
            try
            {
                if (rpDoc == null) return;
                var all_objects = new SortedDictionary<string, ReportObject>();
                foreach (ReportObject o in rpDoc.ReportDefinition.ReportObjects)
                {
                    all_objects[o.Name.ToUpper()] = o;
                }

                foreach (string name in names)
                {
                    string NAME = name.ToUpper();
                    if (all_objects.ContainsKey(NAME))
                    {
                        all_objects[NAME].ObjectFormat.EnableSuppress = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".RPTHIDE", ex);
            }
        }

        public void MoveLblSummary()
        {
            lblSummary.Left = treeListViewAuto1.Left;
            lblSummary.Top = treeListViewAuto1.Bottom;// + (gridViewSummary1.Visible ? 26 : 0);
            if (treeListViewAuto1.Anchor == full_Anchor) lblSummary.Anchor = bottom_Anchor;
            else if (treeListViewAuto1.Anchor == top_Anchor) lblSummary.Anchor = top_Anchor;
        }

        private void ViewFooter()
        {
            try
            {
                var tbl2_row = _tbl2.Rows[0];
                string config_string = ""; // [rten_dau_ky]:DAU_KY:N2,[rten_cuoi_ky]:CUOI_KY:N2
                if (EXTRA_INFOR.ContainsKey("FOOTER"))
                {
                    config_string = EXTRA_INFOR["FOOTER"];
                    lblSummary.Visible = true;
                    MoveLblSummary();
                }
                else
                {
                    return;
                }
                var configs = ObjectAndString.SplitString(config_string);
                string viewText = "";
                _copyValues = new List<CopyValueItem>();
                foreach (string config in configs)
                {
                    var sss = config.Split(':');
                    string value_field = sss[1];
                    if (!_tbl2.Columns.Contains(value_field)) continue;

                    int decimal_place = 2;
                    if (sss.Length > 2 && sss[2].Length>1)
                    {
                        decimal_place = ObjectAndString.ObjectToInt(sss[2].Substring(1));
                    }
                    string field_header_template = sss[0];
                    // Text and [Field] and [Another_Field]
                    int i = 0;
                    while (i < field_header_template.Length && field_header_template.IndexOf("[", i, StringComparison.Ordinal) >= 0)
                    {
                        int i0 = field_header_template.IndexOf("[", i, StringComparison.Ordinal);
                        int i1 = field_header_template.IndexOf("]", i, StringComparison.Ordinal);
                        if (i1 < i0)
                        {
                            i++;
                            continue;
                        }
                        string field = field_header_template.Substring(i0 + 1, i1 - i0 - 1);
                        if (_tbl2.Columns.Contains(field))
                        {
                            field_header_template = field_header_template.Replace("[" + field + "]",
                                ObjectAndString.ObjectToString(tbl2_row[field]));
                        }
                        i = i1;
                    }

                    decimal value = ObjectAndString.ObjectToDecimal(tbl2_row[value_field]);
                    string stringValue = ObjectAndString.NumberToString(value, decimal_place, V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR);
                    var item = new CopyValueItem
                    {
                        decimalValue = value,
                        stringValue = stringValue
                    };
                    _copyValues.Add(item);
                    viewText += string.Format("   {0} {1}", field_header_template, stringValue);
                }

                if (viewText.Length > 3) viewText = viewText.Substring(3);
                lblSummary.Text = viewText;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ViewFooter", ex);
            }
        }

        List<CopyValueItem> _copyValues = new List<CopyValueItem>();
        private class CopyValueItem
        {
            public decimal decimalValue { get; set; }
            public string stringValue { get; set; }
        }
        
        private void dataGridView1_CellDoubleClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (treeListViewAuto1.Bottom < crystalReportViewer1.Top)
            {
                //Phóng lớn dataGridView
                treeListViewAuto1.BringToFront();
                //gridViewSummary1.BringToFront();
                treeListViewAuto1.Height = Height - grbDieuKienLoc.Top - 5;
                treeListViewAuto1.Width = Width - 5;
                treeListViewAuto1.Top = grbDieuKienLoc.Top;
                treeListViewAuto1.Left = grbDieuKienLoc.Left;

                treeListViewAuto1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
				
				lblSummary.Left = treeListViewAuto1.Left;
                lblSummary.Top = treeListViewAuto1.Bottom + 26;
                crystalReportViewer1.Visible = false;
            }
            else
            {
                treeListViewAuto1.Top = grbDieuKienLoc.Top;
                treeListViewAuto1.Left = grbDieuKienLoc.Right + 5;
                treeListViewAuto1.Height = crystalReportViewer1.Top - grbDieuKienLoc.Top - 5;
                treeListViewAuto1.Width = crystalReportViewer1.Width;
                treeListViewAuto1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                lblSummary.Left = treeListViewAuto1.Left;
                lblSummary.Top = treeListViewAuto1.Bottom + 26;
                crystalReportViewer1.Visible = true;
            }
        }

        private void crystalReportViewer1_DoubleClick(object sender, EventArgs e)
        {
            if (crystalReportViewer1.Top > treeListViewAuto1.Bottom)
            {
                crystalReportViewer1.BringToFront();
                crystalReportViewer1.Height = crystalReportViewer1.Bottom - grbDieuKienLoc.Top;
                crystalReportViewer1.Width = crystalReportViewer1.Right - grbDieuKienLoc.Left;
                crystalReportViewer1.Top = grbDieuKienLoc.Top;
                crystalReportViewer1.Left = grbDieuKienLoc.Left;
            }
            else
            {
                crystalReportViewer1.Left = grbDieuKienLoc.Right + 5;
                crystalReportViewer1.Top = treeListViewAuto1.Bottom + 5;
                crystalReportViewer1.Height = Height - crystalReportViewer1.Top - 10;
                crystalReportViewer1.Width = treeListViewAuto1.Width;
            }
        }

        private void XuLyHienThiFormSuaChungTuF3()
        {
            try
            {       
                if (treeListViewAuto1.IsDetailSelected) // Kiem tra dong chon chi tiet.
                {
                    var currentSelectedData = treeListViewAuto1.SelectedItemData;
                    if (currentSelectedData.ContainsKey("STT_REC") && currentSelectedData.ContainsKey("MA_CT"))
                    {
                        var selectedMaCt = currentSelectedData["MA_CT"].ToString().Trim();
                        var selectedSttRec = currentSelectedData["STT_REC"].ToString().Trim();
                        if (selectedMaCt == "INF")// phiếu nhập điều chuyển
                        {
                            selectedMaCt = "IXB"; // phiếu xuất điều chuyển
                            selectedSttRec = selectedSttRec.Left(10) + selectedMaCt;
                        }
                        else if (",AP1,POA,POB,POC,".Contains(selectedMaCt) && treeListViewAuto1.SelectedItemData.ContainsKey("STT_REC_PN"))
                        {
                            selectedSttRec = treeListViewAuto1.SelectedItemData["STT_REC_PN"].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt))
                        {
                            if (!V6Login.UserRight.AllowView("", "00" + selectedMaCt))
                            {
                                ShowMainMessage(V6Text.NoRight);
                                return;
                            }

                            AlctConfig alctConfig = ConfigManager.GetAlctConfig(selectedMaCt);
                            if (alctConfig.TableNameAM != "" && alctConfig.TableNameAD != "")
                            {
                                var hoaDonForm = ChungTuF3.GetChungTuControl(selectedMaCt, Name, selectedSttRec);
                                if (V6Options.M_SUA_BC == "1")
                                {
                                    hoaDonForm.ClickSuaOnLoad = true;
                                }
                                hoaDonForm.ShowToForm(this, V6Setting.IsVietnamese ? alctConfig.TEN_CT : alctConfig.TEN_CT2, true);
                                SetStatus2Text();
                            }
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.EditDenied);
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ReportTreeView44 XuLyHienThiFormSuaChungTu:\n" + ex.Message);
            }
        }

        private void XuLyXemChiTietF5()
        {
            try
            {
                if (treeListViewAuto1.IsDetailSelected)
                {
                    var oldKeys = FilterControl.GetFilterParameters();

                    var view = new ReportTreeView44(m_itemId, _program + "F5", _program + "F5", _reportFileF5,
                        _reportTitleF5, _reportTitle2F5, "", "", "");
                    view.MAU = MAU;
                    view.LAN = LAN;
                    view.CodeForm = CodeForm;
                    view.Advance = FilterControl.Advance;
                    view.FilterControl.String1 = FilterControl.String1;
                    view.FilterControl.String2 = FilterControl.String2;
                    view.FilterControl.ParentFilterData = FilterControl.FilterData;
                    view.Dock = DockStyle.Fill;
                    view.FilterControl.InitFilters = oldKeys;

                    view.FilterControl.SetParentRow(treeListViewAuto1.SelectedItemData);

                    view.PrintMode = V6PrintMode.AutoLoadData;
                    view.ShowToForm(this, "Chi tiết", true);

                    SetStatus2Text();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyXemChiTiet " + ex.Message);
            }
        }

        private void XuLyVeDoThiF7()
        {
            try
            {
                new ChartReportForm(FilterControl, ReportFileFullF7, _tbl1, _tbl2.Copy(), ReportDocumentParameters)
                    .ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyVeDoThiF7: " + ex.Message);
            }
            SetStatus2Text();
        }

        protected override void ClearMyVars()
        {
            List<ReportDocument> list = new List<ReportDocument>() { _rpDoc0 };
            foreach (ReportDocument rpDoc in list)
            {
                if (rpDoc != null)
                {
                    rpDoc.Close();
                    rpDoc.Dispose();
                }
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.E))
            {
                btnExport3.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            else if (keyData == Keys.F3 && FilterControl.F3)
            {
                XuLyHienThiFormSuaChungTuF3();
            }
            else if (keyData == Keys.F7 && FilterControl.F7)
            {
                XuLyVeDoThiF7();
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }

        public override void SetStatus2Text()
        {
            FilterControl.SetStatus2Text(_reportProcedure);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && FilterControl.F5)
            {
                if(treeListViewAuto1.Focused) XuLyXemChiTietF5();
            }
        }

        private void ReportTreeView44_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                SetStatus2Text();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!V6Login.UserRight.AllowPrint(ItemID, ItemID))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }
                if (_ds == null)
                {
                    this.ShowErrorMessage(V6Text.NoData);
                    return;
                }

                crystalReportViewer1.PrintReport();
                //return;

                //var dfp = DefaultPrinter;
                //var selectedPrinter = V6ControlFormHelper.PrintRpt(this, _rpDoc, dfp);
                //if (!string.IsNullOrEmpty(selectedPrinter) && selectedPrinter != dfp)
                //{
                //    print_one = true;
                //    DefaultPrinter = selectedPrinter;
                //}
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnIn_Click " + V6Text.Text("LOIIN"), ex);
            }
        }

        private bool _updateDataRow = false;
        private void cboMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;
            if (_radioRunning || _updateDataRow) return;

            _albcConfig = new AlbcConfig(MauInSelectedRow.ToDataDictionary());
            txtReportTitle.Text = _albcConfig.TITLE;
            if (_albcConfig.RELOAD_DATA == "1")
                MakeReport2(PrintMode, PrinterName, _printCopy);
            else
            {
                //FormatGridView();
                ViewReport();
            }
        }

        private void btnSuaTTMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, AlbcKeys, null);
                f2.AfterInitControl += f_AfterInitControl;
                f2.InitFormControl(this);
                f2.ShowDialog(this);
                SetStatus2Text();
                if (f2.UpdateSuccess)
                {
                    _updateDataRow = true;
                    //cap nhap thong tin
                    var data = f2.FormControl.DataDic;
                    V6ControlFormHelper.UpdateDataRow(MauInSelectedRow, data);
                    _albcConfig = new AlbcConfig(data);
                    _updateDataRow = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnSuaTTMauBC_Click: " + ex.Message);
            }
            SetStatus2Text();
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, "Albc");
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, All_Objects);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
        }

        private void btnThemMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                SortedDictionary<string, object> data0 = null;
                var viewt = new DataView(MauInData);
                viewt.RowFilter = "mau='" + MAU + "'" + " and lan='" + LAN + "'";
                if (MauInData == null || viewt.Count == 0)
                {
                    data0 = new SortedDictionary<string, object>();
                    data0.AddRange(AlbcKeys);
                    data0["CAPTION"] = _reportTitle;
                    data0["CAPTION2"] = _reportTitle2;
                    data0["TITLE"] = txtReportTitle.Text;
                    data0["FirstAdd"] = "1";
                }
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Add, AlbcKeys, data0);
                f2.AfterInitControl += f_AfterInitControl;
                f2.InitFormControl(this);
                f2.ShowDialog(this);
                SetStatus2Text();
                if (f2.InsertSuccess)
                {
                    var data = f2.FormControl.DataDic;
                    //cap nhap thong tin
                    LoadComboboxSource();
                    //Chọn cái mới.
                    var reportFileNew = data["REPORT"].ToString().Trim();
                    var dataV = MauInView.ToTable();
                    for (int i = 0; i < dataV.Rows.Count; i++)
                    {
                        if (dataV.Rows[i]["Report"].ToString().Trim().ToUpper() == reportFileNew.ToUpper())
                        {
                            cboMauIn.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ThemMauBC_Click: " + ex.Message);
            }
            SetStatus2Text();
        }

        private void btnSuaMau_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var f = new FormRptEditor();
                f.rptPath = ReportFileFull;
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message);
            }
        }

        private void thisForm_MouseClick(object sender, MouseEventArgs e)
        {
            V6Form_MouseClick(sender, e);
        }

        private void btnSuaLine_Click(object sender, EventArgs e)
        {
            bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
            if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                if (ctrl) // đảo PAGE trong V6MENU để đảo kiểu report Crystal hay DX.
                {
                    string new_value = "1";
                    if (MenuButton.UseXtraReport) new_value = "0";
                    string sql = "UPDATE V6MENU SET PAGE = '" + new_value + "' WHERE ITEMID = '" + ItemID + "'";
                    int result = V6BusinessHelper.ExecuteSqlNoneQuery(sql);
                    MenuButton.Xtra = new_value;
                    ShowMainMessage("V6MENU " + V6Text.Updated + " " + result);
                    return;
                }

                var title = V6Setting.IsVietnamese ? "Sửa báo cáo động" : "Edit dynamic report";
                var f = new DanhMucView(ItemID, title, "Alreport", "ma_bc='" + _program + "'",
                    V6TableHelper.GetDefaultSortField(V6TableName.Alreport), new AldmConfig());
                f.EnableAdd = false;
                f.EnableCopy = false;
                f.EnableDelete = false;
                f.EnableFullScreen = false;

                f.ShowToForm(this, title);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnSuaLine_Click: " + ex.Message);
            }
        }

        private string GetExportFileName()
        {
            string result = ChuyenMaTiengViet.ToUnSign(_albcConfig.TITLE);
            if (EXTRA_INFOR.ContainsKey("EXPORT")) result = EXTRA_INFOR["EXPORT"];
            // Value
            if (_tbl2 != null && _tbl2.Rows.Count > 0)
            {
                var am_data = _tbl2.Rows[0].ToDataDictionary();
                var regex = new Regex("{(.+?)}");
                foreach (Match match in regex.Matches(result))
                {
                    var matchGroup0 = match.Groups[0].Value;
                    var matchContain = match.Groups[1].Value;
                    var matchColumn = matchContain.ToUpper();
                    var matchFormat = "";
                    if (matchContain.Contains(":"))
                    {
                        int _2dotIndex = matchContain.IndexOf(":", StringComparison.InvariantCulture);
                        matchColumn = matchContain.Substring(0, _2dotIndex).ToUpper();
                        matchFormat = matchContain.Substring(_2dotIndex + 1);
                    }
                    if (am_data.ContainsKey(matchColumn)
                        && am_data[matchColumn] is DateTime && matchFormat == "")
                    {
                        matchFormat = "yyyMMdd";
                    }
                    result = result.Replace(matchGroup0, am_data.ContainsKey(matchColumn) ? ObjectAndString.ObjectToString(am_data[matchColumn], matchFormat).Trim() : "");
                }
            }
            // remove any invalid character from the filename.  
            result = Regex.Replace(result.Trim(), "[^A-Za-z0-9_. ]+", "");
            return result;
        }

        private void exportToExcelMenu_Click(object sender, EventArgs e)
        {
            if (_tbl1 == null)
            {
                ShowMainMessage(V6Text.NoData);
                return;
            }
            try
            {
                var setting = new ExportExcelSetting();
                setting.BOLD_YN = ObjectAndString.ObjectToBool(_albcConfig.BOLD_YN);
                setting.BOLD_CONDITION = new Condition(_albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV);
                setting.data = _tbl1;
                setting.title = txtReportTitle.Text;
                string fileName = V6ControlFormHelper.ExportExcel_ChooseFile(this, setting, GetExportFileName());

                if (V6Options.AutoOpenExcel)
                {
                    V6ControlFormHelper.OpenFileProcess(fileName);
                }
                else
                {
                    this.ShowInfoMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".exportToExcelMenu_Click " + ReportFileFull, ex);
            }
        }

        private void exportToExcelTemplateMenu_Click(object sender, EventArgs e)
        {
            var setting = new ExportExcelSetting();
            if (EXTRA_INFOR.ContainsKey("EXPORTEXCELFILTER"))
                setting.data = V6BusinessHelper.Filter(_tbl1, EXTRA_INFOR["EXPORTEXCELFILTER"]);
            else setting.data = _tbl1;
            setting.data2 = _tbl2;
            setting.reportParameters = ReportDocumentParameters;
            setting.albcConfigData = _albcConfig.DATA;
            setting.xlsTemplateFile = ExcelTemplateFileFull;
            setting.saveFile = GetExportFileName();
            V6ControlFormHelper.ExportExcelTemplate_ChooseFile(this, setting);
        }

        private void exportToExcelView_Click(object sender, EventArgs e)
        {
            try
            {
                string excelColumns = _albcConfig.GRDS_V1;
                string excelHeaders = _albcConfig.GRDH_LANG_V1;
                if (string.IsNullOrEmpty(excelColumns) || string.IsNullOrEmpty(excelHeaders))
                {
                    exportToExcelMenu_Click(sender, e);
                }
                else
                {
                    DataTable data = _tbl1;
                    //if (dataGridView1.DataSource is DataView)
                    //{
                    //    data = ((DataView)dataGridView1.DataSource).ToTable();
                    //}
                    var setting = new ExportExcelSetting();
                    setting.data = data;
                    setting.data2 = _tbl2;
                    setting.reportParameters = ReportDocumentParameters;
                    setting.albcConfigData = _albcConfig.DATA;
                    V6ControlFormHelper.ExportExcelTemplateD(this, "V", setting,
                        ReportFile, ExcelTemplateFileView, _albcConfig.TITLE, excelColumns, excelHeaders);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ExportToExcelView " + ex.Message);
            }
        }

        private void exportToXmlMenu_Click(object sender, EventArgs e)
        {
            if (_tbl1 == null)
            {
                ShowMainMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Xml files (*.xml)|*.xml",
                    FileName = ChuyenMaTiengViet.ToUnSign(GetExportFileName())
                };
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        V6Tools.V6Export.ExportData.ToXmlFile(_tbl1, save.FileName);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorMessage(GetType() + ".ExportFail: " + ex.Message);
                        return;
                    }
                    this.ShowInfoMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Error!\n" + ex.Message);
            }
        }

        SortedDictionary<string, object> edited_paras = new SortedDictionary<string, object>();
        private void btnEditPara_Click(object sender, EventArgs e)
        {
            try
            {
                // init select bang danh sach 
                var data = new SortedDictionary<string, object>();
                if (edited_paras.Count > 0) data = edited_paras;
                else DXreportManager.AddBaseParameters(data);

                var f = new FormEditDataDynamic("ALTTPARA", data);

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (var item in f.Data)
                    {
                        edited_paras[item.Key] = item.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("btnEditPara_Click: " + ex.Message);
            }
        }

        private void viewDataMenu_Click(object sender, EventArgs e)
        {
            treeListViewAuto1.ViewDataToNewForm();
        }

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadComboboxSource();
        }

        
        private void panel1_Leave(object sender, EventArgs e)
        {
            //btnNhan.Focus();
        }

        private void exportToPdfMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_rpDoc0 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                V6ControlFormHelper.ExportRptToPdf_As(this, _rpDoc0, GetExportFileName());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Export PDF", ex);
            }
        }
        
        private void btnExport3_Click(object sender, EventArgs e)
        {
            if (DefaultMenuItem != null && DefaultMenuItem.Enabled)
                DefaultMenuItem.PerformClick();
        }

        private void viewInvoiceInfoMenu_Click(object sender, EventArgs e)
        {
            try
            {
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                var rowData = treeListViewAuto1.SelectedItemData;
                if (rowData == null || !rowData.ContainsKey("MA_CT") || !rowData.ContainsKey("STT_REC")) return;
                string ma_ct = rowData["MA_CT"].ToString().Trim();
                string stt_rec = rowData["STT_REC"].ToString().Trim();
                if (ma_ct == String.Empty || stt_rec == String.Empty) return;
                var f = new InvoiceInfosViewForm(V6InvoiceBase.GetInvoiceBase(ma_ct), stt_rec, ma_ct);
                f.Data2_TH = shift_is_down;
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".viewInvoiceInfoMenu_Click", ex);
            }
        }

        private void viewListInfoMenu_Click(object sender, EventArgs e)
        {
            try
            {
                var row_data = treeListViewAuto1.SelectedItemData;
                if (row_data == null || row_data.ContainsKey("MA_DM") || !row_data.ContainsKey("UID")) return;
                string ma_dm = ObjectAndString.ObjectToString(row_data["MA_DM"]);
                new DanhMucInfosViewForm(ma_dm, row_data).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".viewListInfoMenu_Click", ex);
            }
        }

        public override void ShowAlinitAddEdit(Control control)
        {
            V6Mode v6mode = V6Mode.Add;
            IDictionary<string, object> keys0 = new Dictionary<string, object>();
            IDictionary<string, object> keys = null;

            keys0["LOAI"] = 4;
            //keys0["MA_CT_ME"] = _invoice.Mact;
            keys0["MA_DM"] = _Ma_File;
            keys0["NHOM"] = "00";
            keys0["NAMETAG"] = control.Name.ToUpper();
            if (!string.IsNullOrEmpty(control.AccessibleName)) keys0["NAMEVAL"] = control.AccessibleName.ToUpper();
            // Lấy dữ liệu mặc định của Form parent.
            var defaultData = V6BusinessHelper.GetDefaultValueData(4, "", _Ma_File, m_itemId, "");
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
