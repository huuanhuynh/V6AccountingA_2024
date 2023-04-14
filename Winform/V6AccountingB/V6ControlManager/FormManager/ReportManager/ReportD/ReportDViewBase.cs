using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6ReportControls;
using V6RptEditor;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Export;
using V6ControlManager.FormManager.ReportManager.DXreport;

namespace V6ControlManager.FormManager.ReportManager.ReportD
{
    /// <summary>
    /// Codeform bắt đầu bằng 9????
    /// Dùng cho các report có số cột dữ liệu không cố định (ví dụ từ tháng đến tháng).
    /// </summary>
    public partial class ReportDViewBase : V6FormControl
    {
        #region Biến toàn cục
        public ReportDocument _rpDoc0;

        public string _reportProcedure;
        public string _program, _Ma_File, _reportTitle, _reportTitle2;
        public string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        /// <summary>
        /// Advance filter get albc
        /// </summary>
        public string Advance = "";

        public DataTable MauInData;
        public DataView MauInView;
        public AlbcConfig _albcConfig = new AlbcConfig();

        /// <summary>
        /// Danh sách event_method của Form_program.
        /// </summary>
        public Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        public Type Form_program;
        public Dictionary<string, object> All_Objects = new Dictionary<string, object>();

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
                var AlbcData = V6BusinessHelper.Select(V6TableName.Alreport, AlbcKeys, "*").Data;
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
                AddFilterControl(_program);
                gridViewSummary1.Visible = FilterControl.ViewSum;

                var lineList = FilterControl.GetFilterLineList();
                foreach (KeyValuePair<string, FilterLineBase> item in lineList)
                {
                    All_Objects[item.Key] = item.Value;
                }
                All_Objects["thisForm"] = this;
                SetStatus2Text();
                gridViewSummary1.Visible = FilterControl.ViewSum;

                InvokeFormEvent(FormDynamicEvent.AFTERADDFILTERCONTROL);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateFormControls", ex);
            }
        }

        private DataSet _ds;
        private DataTable _tbl1, _tbl2, _tblv;
        private DataTable[] _tbls;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        private List<SqlParameter> _pList;

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
            set { rTienViet.Checked = value == "VN"; }
        }

        public string LAN
        {
            get { return rTiengViet.Checked ? "V" : rEnglish.Checked ? "E" : rBothLang.Checked ? "B" : V6Login.SelectedLanguage; }
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

        #endregion EXTRA_INFOR

        private string Extra_para
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["Extra_para"].ToString().Trim();
                }
                return result;
            }
        }

        private string ReportFile
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
        private string ReportCaption
        {
            get
            {
                var result = _reportTitle;
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["Caption"].ToString().Trim();
                }
                return result;
            }
        }
        private string ReportCaption2
        {
            get
            {
                var result = _reportTitle2;
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["Caption2"].ToString().Trim();
                }
                return result;
            }
        }
        private string ReportTitle
        {
            get
            {
                var result = V6Setting.IsVietnamese ? _reportTitle : _reportTitle2;
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["Title"].ToString().Trim();
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
        private string ExcelTemplateFileFull
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
                    if (y_n) V6BusinessHelper.Update(V6TableName.Albc, udata, AlbcKeys);
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".DefaultPrinter ", ex);
                }
            }
        }

        private string Report_GRDSV1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDS_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDSV2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDS_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDFV1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDF_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDFV2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDF_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHV_V1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDHV_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHE_V1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDHE_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHV_V2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDHV_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHE_V2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDHE_V2"].ToString().Trim();
                }
                return result;
            }
        }

        private string Report_GRDT_V1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDT_V1"].ToString().Trim();
                }
                return result;
            }
        }
        
        private int F_START
        {
            get
            {
                var result = 2;
                if (MauInSelectedRow != null)
                {
                    result = ObjectAndString.ObjectToInt(MauInSelectedRow["FSTART"]);
                    if (result == 0)
                    {
                        result = FilterControl.fstart;
                    }
                }
                return result;
            }
        }
        private int F_FIXCOLUMN
        {
            get
            {
                var result = 5;
                if (MauInSelectedRow != null)
                {
                    result = ObjectAndString.ObjectToInt(MauInSelectedRow["FFIXCOLUMN"]);
                    if (result == 0)
                    {
                        result = FilterControl.ffixcolumn;
                    }
                }
                return result;
            }
        }

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
        #endregion 
        
        public ReportDViewBase(string itemId, string program, string reportProcedure,
            string reportFile, string reportTitle, string reportTitle2,
            string reportFileF5, string reportTitleF5, string reportTitle2F5)
        {
            V6ControlFormHelper.AddLastAction(GetType() + " " + program);
            m_itemId = itemId;
            _program = program;
            _reportProcedure = reportProcedure;
            _Ma_File = reportFile;
            _reportTitle = reportTitle;
            _reportTitle2 = reportTitle2;

            _reportFileF5 = reportFileF5;
            _reportTitleF5 = reportTitleF5;
            _reportTitle2F5 = reportTitle2F5;

            //_tStruct = ReportHelper.GetTableStruct(_program);
            
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
                    dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, V6Options.M_R_FONTSIZE);
                }
                dataGridView1.Height = crystalReportViewer1.Top - grbDieuKienLoc.Top - SummaryHeight - gridViewTopFilter1.Height;
                InvokeFormEvent(FormDynamicEvent.INIT);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
            }
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
                txtReportTitle.Text = ReportTitle;
                LoadDefaultData(4, "", _Ma_File, m_itemId);
                //Gán điều kiện sum cho gridViewSummary1
                GetSumCondition();
                SetToolTip(txtNumber, V6Text.Text("NSVNEDNBC"));

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

                InvokeFormEvent(FormDynamicEvent.INIT2);
                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2", ex);
            }
        }
        private ToolStripMenuItem DefaultMenuItem = null;

        private void GetSumCondition()
        {
            try
            {
                gridViewSummary1.NoSumColumns = Report_GRDT_V1;
                if (MauInSelectedRow != null)
                {
                    gridViewSummary1.SumCondition = new Condition
                    (
                        MauInSelectedRow["FIELD_S"].ToString().Trim(),
                        MauInSelectedRow["OPER_S"].ToString().Trim(),
                        MauInSelectedRow["VALUE_S"].ToString().Trim()
                    );
                    if (!string.IsNullOrEmpty(gridViewSummary1.SumConditionString)) toolTipV6FormControl.SetToolTip(gridViewSummary1, gridViewSummary1.SumConditionString);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetSumCondition", ex);
            }
        }

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
                    cboMauIn.Enabled = true;
                    btnSuaTTMauBC.Enabled = true;
                    btnThemMauBC.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetFormReportFilter", ex);
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
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetFormReportTitle", ex);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyInit2();
        }

        
        public FilterBase FilterControl { get; set; }
        private void AddFilterControl(string program)
        {
            FilterControl = Filter.Filter.GetFilterControl(program, _reportProcedure, _Ma_File, toolTipV6FormControl);
            panel1.Controls.Add(FilterControl);
        }
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (_executing)
            {
                return;
            }

            try
            {
                FormManagerHelper.HideMainMenu();
                btnNhanImage = btnNhan.Image;
                MakeReport();
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
                _pList.AddRange(FilterControl.GetFilterParameters());
                //_pList.Add(new SqlParameter("@cKey", "1=1" + (sKey.Length>0?" And " +sKey:"")));
                return true;
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("GenerateProcedureParameters: " + ex.Message);
                return false;
            }
        }

        private IDictionary<string, object> ReportDocumentParameters; 
        /// <summary>
        /// Lưu ý: chạy sau khi add dataSource để tránh lỗi nhập parameter value
        /// </summary>
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

                {"M_RFONTNAME", V6Options.GetValue("M_RFONTNAME")},
                {"M_R_FONTSIZE", V6Options.GetValue("M_R_FONTSIZE")},
            };

            V6Login.SetCompanyInfo(ReportDocumentParameters);

            foreach (var item in edited_paras)
            {
                if (ReportDocumentParameters.ContainsKey(item.Key))
                    ReportDocumentParameters[item.Key] = item.Value;
            }

            try
            {
                var index = 0;
                var RnameList = new string[]{};
                if(_tbl2.Columns.Contains("RName_List"))
                    RnameList = _tbl2.Rows[0]["RName_List"].ToString().Split(';');

                for (int i = F_START; i < F_START+F_FIXCOLUMN; i++)
                {
                    //phần này tùy biến trong mỗi chương trình
                    //tên các cột động trong báo cáo!
                    //(cho hiển thị tên cột giống trong bảng dữ liệu)
                    if (i < _tbls[current_report_index].Columns.Count)
                    {
                        var nameListIndex = current_report_index * F_FIXCOLUMN + index;
                        if (RnameList.Length > nameListIndex)
                        {                            
                            var fieldTitle = RnameList[nameListIndex];
                            ReportDocumentParameters.Add("p" + index, fieldTitle);
                        }
                        else
                        {
                            ReportDocumentParameters.Add("p" + index, _tbls[current_report_index].Columns[i].ColumnName);
                        }
                    }
                    else
                    {
                        ReportDocumentParameters.Add("p" + index, " ");
                    }

                    index++;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(GetType() + ".SetAllReportParam " + ReportFileFull + " " + ex.Message, "V6ControlManager");
            }

            if (FilterControl.RptExtraParameters != null)
            {
                ReportDocumentParameters.AddRange(FilterControl.RptExtraParameters, true);
            }

            var rptExtraParametersD = FilterControl.GetRptParametersD(Extra_para, LAN);
            if (rptExtraParametersD != null)
            {
                ReportDocumentParameters.AddRange(rptExtraParametersD, true);
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

            //SetReportParams2();
        }

        //private void SetReportParams2()
        //{
        //    if (FilterControl.RptExtraParameters != null)
        //    foreach (KeyValuePair<string, object> key_value_pair in FilterControl.RptExtraParameters)
        //    {
        //        _rpDoc.SetParameterValue(key_value_pair.Key, key_value_pair.Value);
        //    }
        //}

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
                _ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, _pList.ToArray());
                if (_ds.Tables.Count > 0)
                {
                    _tbl1 = _ds.Tables[0];
                    _tbl1.TableName = "DataTable1";
                    _tbls = TachBang(_tbl1, F_START, F_FIXCOLUMN);
                }
                if (_ds.Tables.Count > 1)
                {
                    _tbl2 = _ds.Tables[1];
                    _tbl2.TableName = "DataTable2";
                }
                else
                {
                    _tbl2 = null;
                }
                
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

        private int sobangtach;
        private int current_report_index;
        DataTable[] TachBang(DataTable bigTable, int soCotLap, int soCotData)
        {
            //try
            {
                sobangtach = 0;
                int socot = bigTable.Columns.Count;
                int sodong = bigTable.Rows.Count;

                int i = 0;
                int j = 0;

                for (i = soCotLap; i < socot; i += soCotData)
                {
                    sobangtach++;
                }

                DataTable[] returnTable = new DataTable[sobangtach];
                /**Định dạng mỗi bảng tách gồm soCotLap + soCotData cột
                 * 
                 **/
                for (i = 0; i < sobangtach; i++)
                {
                    returnTable[i] = new DataTable();
                    for (j = 0; j < soCotLap + soCotData; j++)
                    {
                        if (j < soCotLap)
                        {
                            var column = bigTable.Columns[j];
                            returnTable[i].Columns.Add(column.ColumnName, column.DataType);
                        }
                        if (j >= soCotLap)
                        {   //(j+i*soCotData) la index tren bigTable
                            if (j + i*soCotData < socot)
                            {
                                var column = bigTable.Columns[j + i*soCotData];
                                returnTable[i].Columns.Add(column.ColumnName, column.DataType);
                            }
                            else
                                returnTable[i].Columns.Add("~" + (j + 1 - soCotLap));
                        }
                    }
                }
                //Duyet qua bigtable
                for (i = 0; i < sobangtach; i++)
                {
                    for (int dong = 0; dong < sodong; dong++)
                    {
                        DataRow dr = returnTable[i].NewRow();
                        for (j = 0; j < soCotLap + soCotData; j++)
                        {
                            if (j < soCotLap)
                            {
                                dr[j] = bigTable.Rows[dong][j];
                            }
                            if (j >= soCotLap)
                            {
                                if ((j + i * soCotData) < socot)
                                    dr[j] = bigTable.Rows[dong][j + i * soCotData];
                                else
                                    dr[j] = null;
                            }

                        }
                        returnTable[i].Rows.Add(dr);
                    }
                }

                return returnTable;
            }
        }

        /// <summary>
        /// GenerateProcedureParameters();//Add các key
        /// var tLoadData = new Thread(LoadData);
        /// tLoadData.Start();
        /// timerViewReport.Start();
        /// </summary>
        private void MakeReport()
        {
            if (GenerateProcedureParameters()) //Add các key khác
            {
                _executing = true;
                var tLoadData = new Thread(LoadData);
                tLoadData.Start();
                timerViewReport.Start();
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
            if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else if (_executesuccess)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;

                try
                {
                    FilterControl.LoadDataFinish(_ds);
                    All_Objects["_ds"] = _ds;
                    InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                    ViewFooter();
                    dataGridView1.TableSource = _tbl1;
                    gridViewTopFilter1.RelocationAll();

                    try
                    {
                        //V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
                        if (_albcConfig != null && _albcConfig.HaveInfo)
                        {
                            V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                                _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
                        }
                    }
                    catch
                    {
                        // ignored
                    }

                    FormatGridView();
                    gridViewTopFilter1.MadeFilterItems();
                    ViewReportIndex();

                    dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    _executesuccess = false;
                    timerViewReport.Stop();
                    this.ShowErrorMessage(GetType() + ".TimerView: " + ex.Message);
                }
            }
            else //Tải dữ liệu lỗi
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                this.ShowErrorMessage(_message);
            }
        }

        private void FormatGridView()
        {
            try
            {
                //Header
                var fieldList = (from DataColumn column in _tbl1.Columns select column.ColumnName).ToList();

                var fieldDic = CorpLan2.GetFieldsHeader(fieldList);
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (fieldDic.ContainsKey(dataGridView1.Columns[i].DataPropertyName.ToUpper()))
                    {
                        dataGridView1.Columns[i].HeaderText =
                            fieldDic[dataGridView1.Columns[i].DataPropertyName.ToUpper()];
                    }
                }
                //Format
                var f = dataGridView1.Columns["so_luong"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
                }
                f = dataGridView1.Columns["TIEN2"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
                }
                f = dataGridView1.Columns["GIA2"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
                }

                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1,
                    V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);
                if (FilterControl != null) FilterControl.FormatGridView(dataGridView1);
                if (MauInSelectedRow != null)
                {
                    int frozen = ObjectAndString.ObjectToInt(MauInSelectedRow["FROZENV"]);
                    dataGridView1.SetFrozen(frozen);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
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
                    MakeReport();
                else
                    ViewReport();
            }
        }
        
        private bool _radioChange;
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
                        txtReportTitle.Text = ReportTitle;
                    }

                    if (ReloadData == "1")
                        MakeReport();
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

        private void printGrid_Click(object sender, EventArgs e)
        {
            if (_tbl1 == null)
            {
                ShowMainMessage(V6Text.NoData);
                return;
            }
            try
            {
                V6ControlFormHelper.PrintGridView(dataGridView1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".PrintGrid", ex);
            }
        }
        

        void ViewReport()
        {
            if (_ds == null) return;
            try
            {
                CleanUp();
                var rpDoc = new ReportDocument();
                rpDoc.Load(ReportFileFull);

                _tblv = ConvertTable(_tbls[current_report_index], F_START, F_FIXCOLUMN);
                var new_ds = new DataSet();
                _tblv.TableName = "DataTable1";
                new_ds.Tables.Add(_tblv);
                new_ds.Tables.Add(_tbl2.Copy());
                rpDoc.SetDataSource(new_ds);

                SetAllReportParams(rpDoc);

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
                    dataGridView1.Height = crystalReportViewer1.Top - grbDieuKienLoc.Top - SummaryHeight - gridViewTopFilter1.Height;
                }
                else
                {
                    return;
                }
                var configs = ObjectAndString.SplitString(config_string);
                string viewText = "";
                foreach (string config in configs)
                {
                    var sss = config.Split(':');
                    string value_field = sss[1];
                    if (!_tbl2.Columns.Contains(value_field)) continue;

                    int decimal_place = 2;
                    if (sss.Length > 2 && sss[2].Length > 1)
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

                    viewText += string.Format("   {0} {1}", field_header_template, ObjectAndString.NumberToString
                        (tbl2_row[sss[1]], decimal_place, V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR));
                }

                if (viewText.Length > 3) viewText = viewText.Substring(3);
                lblSummary.Text = viewText;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ViewFooter", ex);
            }
        }

        void ViewReportIndex(int currentReport = 0)
        {
            current_report_index = currentReport;
            if (sobangtach == 0)
            {
                V6ControlFormHelper.ShowMessage(V6Text.NoData, this);
                return;
            }
            //if (ReloadData == "1")
            //    MakeReport();
            //else
                ViewReport();

            txtNumber.Text = (current_report_index +1).ToString();
            EnableNavigationsButton();
        }

        /// <summary>
        /// Đổi tên các cột data thành DataX++ và thêm các cột trống
        /// </summary>
        /// <param name="table">Bảng dữ liệu</param>
        /// <param name="startColumn">Bắt đầu dữ liệu</param>
        /// <param name="fixColumn">Số cột data</param>
        /// <returns></returns>
        public DataTable ConvertTable(DataTable table, int startColumn, int fixColumn)
        {
            try
            {
                int socot = table.Columns.Count;
                int sodong = table.Rows.Count;
                DataTable tableForReport = new DataTable();
                //tao cot cho bang dich
                for (int i = 0; i < startColumn; i++)
                {
                    tableForReport.Columns.Add(table.Columns[i].ColumnName, table.Columns[i].DataType);
                }
                var index = 0;
                for (int i = startColumn; i < startColumn + fixColumn; i++)
                {
                    tableForReport.Columns.Add("Data" + index);
                    index++;
                    if (table.Columns.Count > i)
                        tableForReport.Columns[i].DataType = table.Columns[i].DataType;
                    else
                        tableForReport.Columns[i].DataType = table.Columns[table.Columns.Count - 1].DataType;//Cột rỗng

                }

                //tableForReport = table;
                if (tableForReport.Columns.Count < table.Columns.Count)
                    socot = tableForReport.Columns.Count;
                for (int i2 = 0; i2 < sodong; i2++)
                {
                    DataRow dr = tableForReport.NewRow();
                    //lay du lieu cho dong
                    for (int i3 = 0; i3 < socot; i3++)
                    {
                        if (i3 < table.Columns.Count)
                            dr[i3] = table.Rows[i2][i3];
                    }
                    tableForReport.Rows.Add(dr);
                }
                return tableForReport;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ConvertTable" + ex.Message, "ReportDView");
                return null;
            }
        }
        
        private void EnableNavigationsButton()
        {
            //nút đầu
            btnFirst.Enabled = current_report_index != 0;
            //nút cuối
            btnLast.Enabled = current_report_index != sobangtach-1;
            //nút lui
            btnPrevious.Enabled = current_report_index > 0;
            //nút tới
            btnNext.Enabled = current_report_index < sobangtach-1;
        }

        private int SummaryHeight
        {
            get
            {
                int summaryHeight = 0;
                if (gridViewSummary1.Visible) summaryHeight += gridViewSummary1.Height + 5;
                if (lblSummary.Visible) summaryHeight += lblSummary.Height + 5;
                if (summaryHeight == 0) summaryHeight = 5;
                return summaryHeight;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (crystalReportViewer1.Visible)
            {
                // Phóng lớn dataGridView
                dataGridView1.BringToFront();
                gridViewSummary1.BringToFront();
                dataGridView1.Height = Height - grbDieuKienLoc.Top - SummaryHeight - gridViewTopFilter1.Height;
                dataGridView1.Width = Width - 5;
                dataGridView1.Top = grbDieuKienLoc.Top + gridViewTopFilter1.Height;
                dataGridView1.Left = grbDieuKienLoc.Left;

                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

                lblSummary.Left = dataGridView1.Left;
                lblSummary.Top = dataGridView1.Bottom + 26;
                crystalReportViewer1.Visible = false;
            }
            else // Thu nhỏ dataGridView
            {
                dataGridView1.Top = grbDieuKienLoc.Top + gridViewTopFilter1.Height;
                dataGridView1.Left = grbDieuKienLoc.Right + 5;
                dataGridView1.Height = crystalReportViewer1.Top - grbDieuKienLoc.Top - SummaryHeight - gridViewTopFilter1.Height;
                dataGridView1.Width = crystalReportViewer1.Width;
                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                lblSummary.Left = dataGridView1.Left;
                lblSummary.Top = dataGridView1.Bottom + 26;
                crystalReportViewer1.Visible = true;
            }
        }

        private void crystalReportViewer1_DoubleClick(object sender, EventArgs e)
        {
            if (crystalReportViewer1.Top > dataGridView1.Bottom)
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
                crystalReportViewer1.Top = dataGridView1.Bottom + SummaryHeight;
                crystalReportViewer1.Height = Height - crystalReportViewer1.Top - 10;
                crystalReportViewer1.Width = dataGridView1.Width;
            }
        }

        private void XuLyHienThiFormSuaChungTuF3()
        {
            try
            {       
                if (dataGridView1.CurrentRow != null)
                {
                    var currentRow = dataGridView1.CurrentRow;
                    if (dataGridView1.Columns.Contains("Stt_rec") && dataGridView1.Columns.Contains("Ma_ct"))
                    {
                        var selectedMaCt = currentRow.Cells["Ma_ct"].Value.ToString().Trim();
                        var selectedSttRec = currentRow.Cells["Stt_rec"].Value.ToString().Trim();
                        if (selectedMaCt == "INF")// phiếu nhập điều chuyển
                        {
                            selectedMaCt = "IXB"; // phiếu xuất điều chuyển
                            selectedSttRec = selectedSttRec.Left(10) + selectedMaCt;
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
                this.ShowErrorMessage(GetType() + ".ReportRViewBase XuLyHienThiFormSuaChungTu:\n" + ex.Message);
            }
        }

        private void XuLyXemChiTietF5()
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    ShowMainMessage(V6Text.NoSelection);
                    return;
                }

                var oldKeys = FilterControl.GetFilterParametersNew();
                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = true,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,

                    Program = _program + "F5",
                    ReportProcedure = _program + "F5",
                    ReportFile = _reportFileF5,
                    ReportCaption = _reportTitleF5,
                    ReportCaption2 = _reportTitle2F5,
                    ReportFileF5 = "",
                    FilterControlInitFilters = oldKeys,
                    FilterControlString1 = FilterControl.String1,
                    FilterControlString2 = FilterControl.String2,
                    FilterControlFilterData = FilterControl.FilterData,
                    ParentRowData = dataGridView1.CurrentRow.ToDataDictionary(),
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);
                
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyXemChiTiet " + ex.Message);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3 && FilterControl.F3)
            {
                e.Handled = true;
                XuLyHienThiFormSuaChungTuF3();
            }
            if (e.KeyCode == Keys.F5 && FilterControl.F5)
            {
                if(dataGridView1.Focused) XuLyXemChiTietF5();
            }
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

        public override void SetStatus2Text()
        {
            FilterControl.SetStatus2Text(_reportProcedure);
        }

        private void ReportRViewBase_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (FilterControl.GridViewHideFields != null && FilterControl.GridViewHideFields.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
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
                ShowMainMessage(string.Format("{0}: {1}", V6Text.Text("LOIIN"), ex.Message));
                this.WriteExLog(GetType() + ".btnIn_Click", ex);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (sobangtach == 0 || current_report_index == 1)
                return;
            current_report_index = 0;
            
            ViewReportIndex(current_report_index);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if(current_report_index>0)
            {
                ViewReportIndex(current_report_index - 1);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (current_report_index < sobangtach)
            {
                ViewReportIndex(current_report_index + 1);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (current_report_index != sobangtach - 1)
            {
                ViewReportIndex(sobangtach - 1);
            }
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var num = Convert.ToInt32(txtNumber.Text)-1;
                if (num != current_report_index && num >= 0 && num < sobangtach)
                {
                    ViewReportIndex(num);
                }
                else
                {
                    ViewReportIndex(current_report_index);
                }
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            var num = Convert.ToInt32(txtNumber.Text);
            if (num != current_report_index && num >= 0 && num < sobangtach)
            {
                ViewReportIndex(num - 1);
            }
            else
            {
                ViewReportIndex(current_report_index);
            }
        }

        private void XuLyVeDoThiF7()
        {
            try
            {
                new ChartReportForm(FilterControl, ReportFileFullF7, _tbl1, _tbl2.Copy(), ReportDocumentParameters)
                    .ShowDialog(this);
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyVeDoThiF7: " + ex.Message);
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

        private bool _updateDataRow = false;
        private void cboMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;
            if (_radioRunning || _updateDataRow) return;

            _albcConfig = new AlbcConfig(MauInSelectedRow.ToDataDictionary());
            GetSumCondition();

            txtReportTitle.Text = ReportTitle;
            if (ReloadData == "1")
                MakeReport();
            else
                ViewReport();
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
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, "Albc");
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + ma_ct, ex);
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
        }

        private void btnSuaMau_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var f = new FormRptEditor();
                f.rptPath = ReportFileFull;
                f.ShowDialog(this);
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message);
            }
        }

        private string GetExportFileName()
        {
            string result = ChuyenMaTiengViet.ToUnSign(ReportTitle);
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
                this.ShowErrorMessage(GetType() + ".ExportFail\n" + ex.Message);
            }
        }

        private void exportToExcelTemplateMenu_Click(object sender, EventArgs e)
        {
            string excelColumns = "";
            string excelHeaders = "";
            if (_tbl2 != null && _tbl2.Rows.Count>0)
            {
                if (_tbl2.Columns.Contains("GRDS_V1")) excelColumns = _tbl2.Rows[0]["GRDS_V1"].ToString();
                var f = V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1";
                if (_tbl2.Columns.Contains(f)) excelHeaders = _tbl2.Rows[0][f].ToString();
            }

            var setting = new ExportExcelSetting();
            if (EXTRA_INFOR.ContainsKey("EXPORTEXCELFILTER"))
                setting.data = V6BusinessHelper.Filter(_tbl1, EXTRA_INFOR["EXPORTEXCELFILTER"]);
            else setting.data = _tbl1;
            setting.data2 = _tbl2;
            setting.reportParameters = ReportDocumentParameters;
            setting.albcConfigData = _albcConfig.DATA;
            V6ControlFormHelper.ExportExcelTemplateD(this, "R", setting,
                ReportFile, ExcelTemplateFileFull, GetExportFileName(), excelColumns, excelHeaders);
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

        private void viewDataMenu_Click(object sender, EventArgs e)
        {
            dataGridView1.ViewDataToNewForm();
        }

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadComboboxSource();
        }

        private void exportToExcelView_Click(object sender, EventArgs e)
        {
            try
            {
                string showFields = "";
                //string formatStrings = "";
                string headerString = "";
                if (_ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
                {
                    var data = _ds.Tables[1];
                    if (data.Columns.Contains("GRDS_V1")) showFields = data.Rows[0]["GRDS_V1"].ToString();
                    //if (data.Columns.Contains("GRDF_V1")) formatStrings = data.Rows[0]["GRDF_V1"].ToString();
                    var f = V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1";
                    if (data.Columns.Contains(f)) headerString = data.Rows[0][f].ToString();
                }

                if (string.IsNullOrEmpty(showFields) || string.IsNullOrEmpty(headerString))
                {
                    exportToExcelMenu_Click(sender, e);
                }
                else
                {
                    DataTable data = _tbl1;
                    if (dataGridView1.DataSource is DataView)
                    {
                        data = ((DataView)dataGridView1.DataSource).ToTable();
                    }

                    var setting = new ExportExcelSetting();
                    setting.data = data;
                    setting.data2 = _tbl2;
                    setting.reportParameters = ReportDocumentParameters;
                    setting.albcConfigData = _albcConfig.DATA;
                    V6ControlFormHelper.ExportExcelTemplateD(this, "V", setting,
                        ReportFile, ExcelTemplateFileView, ReportTitle, showFields, headerString);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ExportToExcelView " + ex.Message);
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

        private void btnSuaLine_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
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
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message);
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
                if (dataGridView1.CurrentRow == null || !dataGridView1.Columns.Contains("MA_CT") || !dataGridView1.Columns.Contains("STT_REC")) return;
                var row = dataGridView1.CurrentRow;
                string ma_ct = row.Cells["MA_CT"].Value.ToString().Trim();
                string stt_rec = row.Cells["STT_REC"].Value.ToString().Trim();
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
                if (dataGridView1.CurrentRow == null || !dataGridView1.Columns.Contains("MA_DM") || !dataGridView1.Columns.Contains("UID")) return;

                var row_data = dataGridView1.CurrentRow.ToDataDictionary();
                string ma_dm = ObjectAndString.ObjectToString(row_data["MA_DM"]);
                new DanhMucInfosViewForm(ma_dm, row_data).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".viewListInfoMenu_Click", ex);
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
            if (_albcConfig != null && _albcConfig.HaveInfo)
            {
                V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                    _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
            }
        }
    }
}
