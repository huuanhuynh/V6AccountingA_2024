using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.ReportManager.DXreport;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using System.Globalization;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6Tools.V6Export;

namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    public partial class ReportR_DX : V6FormControl
    {
        #region Biến toàn cục
        private XtraReport _repx0;

        public string _reportProcedure;
        public string _program, _Ma_File, _reportTitle, _reportTitle2;
        public string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        /// <summary>
        /// Advance filter get albc, nhận từ filter cha để lọc.
        /// </summary>
        public string Advance = "";

        public DataTable MauInData;
        public DataView MauInView;
        public AlbcConfig _albcConfig = new AlbcConfig();
        public AlreportConfig _alreportConfig;

        public bool Close_after_print { get; set; }
        /// <summary>
        /// 0 DoNoThing 1 AutoPrint 2 AutoClickPrint 3 AutoClickExport
        /// </summary>
        public V6PrintMode PrintMode { get; set; }
        public string PrinterName { get; set; }
        private int _printCopy = 1;
        public int PrintCopies
        {
            get { return _printCopy; }
            set { _printCopy = value; }
        }

        /// <summary>
        /// Danh sách event_method của Form_program.
        /// </summary>
        private Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        private Type Form_program;
        private Dictionary<string, object> All_Objects = new Dictionary<string, object>();

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
                _albcConfig = ConfigManager.GetAlbcConfig(MAU, LAN, _Ma_File, ReportFile);
                if (_albcConfig.NoInfo) return;
                if (_albcConfig.MMETHOD.Trim() == "") return;

                var ds = ObjectAndString.XmlStringToDataSet(_albcConfig.MMETHOD);
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
                var TEN_NLB_LOGIN= V6Login.XmlInfo.TEN_NLB_LOGIN.Trim();
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
                foreach (KeyValuePair<string,FilterLineBase> item in lineList)
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

        public DataSet DataSet
        {
            get { return _ds; }
            set { _ds = value; }
        }
        private DataSet _ds;
        public DataTable _tbl1, _tbl2, _tbl3;
        public DataView _view1, _view2, _view3;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        public List<SqlParameter> _pList;

        /// <summary>
        /// Tên file excel tự động xuất.
        /// </summary>
        public string AutoExportExcelFileName = null;
        public string AutoExportWordFileName = null;
        
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
                GetExtraInfor();
                return _extraInfor;
            }
        }

        private SortedDictionary<string, string> _extraInfor = null;

        private void GetExtraInfor()
        {
            _extraInfor = new SortedDictionary<string, string>();
            if (_albcConfig.NoInfo) return;
            _extraInfor = _albcConfig.EXTRA_INFOR;
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
                var result = "";
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

        public string ReportFileFullDX
        {
            get
            {
                var result = V6Login.StartupPath + @"\ReportsDX\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".repx";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\ReportsDX\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + ".repx";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFullDXF7
        {
            get
            {
                var result = V6Login.StartupPath + @"\ReportsDX\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "F7.repx";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\ReportsDX\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "F7.repx";//_reportFile gốc
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

        public string WordTemplateFileFull
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".doc";
                if (File.Exists(result + "x")) result += "x";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + ".doc";
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
        private string Report_GRDT_V2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDT_V2"].ToString().Trim();
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
        
        public ReportR_DX(string itemId, string program, string reportProcedure,
            string reportFile, string reportTitle, string reportTitle2,
            string reportFileF5, string reportTitleF5, string reportTitle2F5)
        {
            V6ControlFormHelper.AddLastAction(GetType() + " " + program);
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
                    dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, V6Options.M_R_FONTSIZE);
                }
                dataGridView1.Height = documentViewer1.Top - grbDieuKienLoc.Top - SummaryHeight - gridViewTopFilter1.Height;

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
                    Clipboard.SetText("");
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
            Clipboard.SetText("");
        }

        private void CheckRightReport()
        {
            bool no_print = false;
            if (!V6Login.UserRight.AllowPrint(ItemID, ItemID))
            {
                no_print = true;
                //documentViewer1.ShowPrintButton = false; !!!!!
                //documentViewer1.ShowExportButton = false; !!!!!
                contextMenuStrip1.Items.Remove(exportToPdfMenu);
            }
            if (!V6Login.UserRight.AllowView(ItemID, ItemID))
            {
                documentViewer1.InvisibleTag();
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
                if (!_setVEBC)
                {
                    SetLanguage(V6Setting.ReportLanguage);
                }
                //if (V6Setting.ReportLanguage == "V")
                //{
                //    rTiengViet.Checked = true;
                //}
                //else if (V6Setting.ReportLanguage == "E")
                //{
                //    rEnglish.Checked = true;
                //}
                //else if (V6Setting.ReportLanguage == "B")
                //{
                //    rBothLang.Checked = true;
                //}
                //else
                //{
                //    rCurrent.Checked = true;
                //}
                LoadComboboxSource();
                LoadDefaultData(4, "", _Ma_File, m_itemId, "");
                //Gán điều kiện sum cho gridViewSummary1
                GetSumCondition();

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

                            if (!key3.Contains("A")) exportEXCELXtraMenu.Enabled = false;
                            if (!key3.Contains("B")) exportEXCELDataMenu.Enabled = false;
                            if (!key3.Contains("C")) exportToWordMenu.Enabled = false;
                            
                        }
                        if (!key3.Contains("E")) btnSuaMau.Enabled = false;
                        
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
                
                txtReportTitle.Text = ReportTitle;

                if (EXTRA_INFOR.ContainsKey("ENTER2TAB"))
                {
                    dataGridView1.enter_to_tab = ObjectAndString.ObjectToBool(EXTRA_INFOR["ENTER2TAB"]);
                    //dataGridView2.enter_to_tab = dataGridView1.enter_to_tab;
                }

                InvokeFormEvent(FormDynamicEvent.INIT2);
                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2 " + ReportFileFullDX, ex);
            }
        }

        private ToolStripMenuItem DefaultMenuItem = null;

        private void GetSumCondition()
        {
            try
            {
                gridViewSummary1.NoSumColumns = _albcConfig.GRDT_V1;
                if (_albcConfig.HaveInfo)
                {
                    gridViewSummary1.SumCondition = new Condition(
                        _albcConfig.FIELD_S,
                        _albcConfig.OPER_S,
                        _albcConfig.VALUE_S
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
                    V6ControlFormHelper.EnableControls(cboMauIn, btnSuaTTMauBC);
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

        public void Form_Load(object sender, EventArgs e)
        {
            MyInit2();
            if (_ds != null && _ds.Tables.Count > 0)
            {
                SetTBLdata();
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
            FilterControl.Focus();
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
       
        public bool GenerateProcedureParameters()
        {
            try
            {
                _pList = new List<SqlParameter>();
                var tList = FilterControl.GetFilterParametersNew();
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

        public IDictionary<string, object> ReportDocumentParameters; 
        /// <summary>
        /// Lưu ý: chạy sau khi add dataSource để tránh lỗi nhập parameter value
        /// </summary>
        /// <param name="repx"></param>
        private void SetAllReportParams(XtraReport repx)
        {
            ReportDocumentParameters = new SortedDictionary<string, object>();
            DXreportManager.AddBaseParameters(ReportDocumentParameters);
            V6Login.SetCompanyInfo(ReportDocumentParameters);
            ReportDocumentParameters["Title"] = txtReportTitle.Text;
            ReportDocumentParameters["M_TEN_NLB"] = txtM_TEN_NLB.Text;
            ReportDocumentParameters["M_TEN_NLB2"] = txtM_TEN_NLB2.Text;
            foreach (var item in edited_paras)
            {
                ReportDocumentParameters[item.Key] = item.Value;
            }

            if (FilterControl.RptExtraParameters != null)
            {
                ReportDocumentParameters.AddRange(FilterControl.RptExtraParameters, true);
            }

            var rptExtraParametersD = FilterControl.GetRptParametersD(_albcConfig.EXTRA_PARA, LAN);
            if (rptExtraParametersD != null)
            {
                ReportDocumentParameters.AddRange(rptExtraParametersD, true);
            }

            string errors = "";
            foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
            {
                try
                {
                    if (repx.Parameters[item.Key] != null)
                    {
                        repx.Parameters[item.Key].Value = item.Value;
                    }
                    else
                    {
                        // missing parameters warning!
                        //errors += "\n" + item.Key + ":\t " + V6Text.NotExist;
                        // Auto create Paramter for easy edit.
                        repx.Parameters.Add(new Parameter() {
                            Name = item.Key,
                            Value = item.Value,
                            Visible = false,
                            Type = item.Value.GetType(),
                            Description = item.Key,
                        });
                    }
                }
                catch (Exception ex)
                {
                    errors += "\n" + item.Key + ": " + ex.Message;
                }
            }

            if (errors != "")
            {
                this.ShowErrorMessage(GetType() + ".SetAllReportParams: " + ReportFileFullDX + " " + errors);
            }
        }


        #region ==== LoadData MakeReport ====
        
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

        public void LoadData()
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
                if (FilterControl is FilterDanhMuc)
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
                ii = 0;
                InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                ViewFooter();
                ShowReport();
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

        private void Print(string printerName, XtraReport repx)
        {
            bool printerOnline = PrinterStatus.CheckPrinterOnline(printerName);
            
            if (printerOnline)
            {
                try
                {
                    var printTool = new ReportPrintTool(repx);
                    printTool.PrintingSystem.ShowMarginsWarning = false;
                    printTool.Print(printerName);

                    //if (!xemMau)
                    //    timer1.Start();

                    //xong = true;
                    CallPrintSuccessEvent();
                    if (Close_after_print)
                    {
                        if (!IsDisposed) Dispose();
                    }
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

                if (_albcConfig.HaveInfo)
                {
                    int frozen = ObjectAndString.ObjectToInt(_albcConfig.FROZENV);
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
                        txtReportTitle.Text = ReportTitle;
                    }

                    if (ReloadData == "1")
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

        void ShowReport()
        {
            try
            {
                FilterControl.LoadDataFinish(_ds);
                All_Objects["_ds"] = _ds;
                InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                dataGridView1.SetFrozen(0);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _tbl1;
                
                //V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                        _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
                }
                
                FormatGridView();
                gridViewTopFilter1.MadeFilterItems();

                ViewReport();
                if (PrintMode == V6PrintMode.AutoPrint)
                {
                    Print(PrinterName, _repx0);
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

                if (!string.IsNullOrEmpty(AutoExportExcelFileName))
                {
                    var setting = new ExportExcelSetting();
                    setting.data = _tbl1;
                    setting.data2 = _tbl2;
                    setting.reportParameters = ReportDocumentParameters;
                    setting.albcConfigData = _albcConfig.DATA;
                    setting.xlsTemplateFile = ExcelTemplateFileFull;
                    setting.saveFile = AutoExportExcelFileName;
                    V6ControlFormHelper.ExportExcelTemplate(this, setting);
                    if (!IsDisposed) Dispose();
                }
                else if (!string.IsNullOrEmpty(AutoExportWordFileName))
                {

                }
                
                dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                timerViewReport.Stop();
                _executesuccess = false;
                this.ShowErrorException(GetType() + ".ShowReport", ex);
            }
        }

        /// <summary>
        /// Hiển thị report  khi đã có data.
        /// </summary>
        public void ViewReport()
        {
            if (_ds == null) return;
            if (_ds.Tables[0].Rows.Count == 0)
            {
                this.ShowInfoMessage(V6Text.NoData, 500);
                return;
            }
            try
            {
                CleanUp();
                XtraReport x = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX);
                x.PrintingSystem.ShowMarginsWarning = false;
                x.DataSource = _ds;
                SetAllReportParams(x);
                documentViewer1.Zoom = DXreportManager.GetExtraReportZoom(documentViewer1, x, _albcConfig.EXTRA_INFOR_PRINTVCZOOM);
                documentViewer1.DocumentSource = x;
                x.CreateDocument();
                _repx0 = x;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewReport " + ReportFileFullDX, ex);
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
                    dataGridView1.Height = documentViewer1.Top - grbDieuKienLoc.Top - SummaryHeight - gridViewTopFilter1.Height;
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
            if (documentViewer1.Visible)
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
                documentViewer1.Visible = false;
            }
            else // Thu nhỏ dataGridView
            {
                dataGridView1.Top = grbDieuKienLoc.Top + gridViewTopFilter1.Height;
                dataGridView1.Left = grbDieuKienLoc.Right + 5;
                dataGridView1.Height = documentViewer1.Top - grbDieuKienLoc.Top - SummaryHeight - gridViewTopFilter1.Height;
                dataGridView1.Width = documentViewer1.Width;
                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                lblSummary.Left = dataGridView1.Left;
                lblSummary.Top = dataGridView1.Bottom + 26;
                documentViewer1.Visible = true;
            }
        }

        private void documentViewer1_DoubleClick(object sender, EventArgs e)
        {
            if (documentViewer1.Top > dataGridView1.Bottom)
            {
                documentViewer1.BringToFront();
                documentViewer1.Height = documentViewer1.Bottom - grbDieuKienLoc.Top;
                documentViewer1.Width = documentViewer1.Right - grbDieuKienLoc.Left;
                documentViewer1.Top = grbDieuKienLoc.Top;
                documentViewer1.Left = grbDieuKienLoc.Left;
            }
            else
            {
                documentViewer1.Left = grbDieuKienLoc.Right + 5;
                documentViewer1.Top = dataGridView1.Bottom + SummaryHeight;
                documentViewer1.Height = Height - documentViewer1.Top - 10;
                documentViewer1.Width = dataGridView1.Width;
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
                        else if (",AP1,POA,POB,POC,".Contains(selectedMaCt) && dataGridView1.Columns.Contains("STT_REC_PN"))
                        {
                            selectedSttRec = currentRow.Cells["STT_REC_PN"].Value.ToString().Trim();
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
                this.ShowErrorMessage(GetType() + ".ReportR_DX XuLyHienThiFormSuaChungTu:\n" + ex.Message);
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

                var view = new ReportR_DX(m_itemId, _program + "F5", _program + "F5",
                    FilterControl.ReportFileF5??_reportFileF5,
                    FilterControl.ReportTitleF5??_reportTitleF5,
                    FilterControl.ReportTitle2F5??_reportTitle2F5,
                    (FilterControl.ReportFileF5 ?? _reportFileF5) + "F5",
                    "", "");
                view.MAU = MAU;
                view.LAN = LAN;
                view.CodeForm = CodeForm;
                view.Advance = FilterControl.Advance;
                view.FilterControl.String1 = FilterControl.String1;
                view.FilterControl.String2 = FilterControl.String2;
                view.FilterControl.ParentFilterData = FilterControl.FilterData;

                view.Dock = DockStyle.Fill;
                view.FilterControl.InitFilters = oldKeys;

                view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());

                view.PrintMode = V6PrintMode.AutoLoadData;
                view.ShowToForm(this, "Chi tiết", true);
                
                SetStatus2Text();
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
                new ChartReportDXForm(FilterControl, ReportFileFullDXF7, _tbl1, _tbl2.Copy(), ReportDocumentParameters)
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
            List<XtraReport> list = new List<XtraReport>() { _repx0 };
            foreach (XtraReport rpDoc in list)
            {
                if (rpDoc != null)
                {
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
                if(dataGridView1.Focused) XuLyXemChiTietF5();
            }
        }

        

        private void ReportR_DX_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                SetStatus2Text();
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (FilterControl.GridViewHideFields != null && FilterControl.GridViewHideFields.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
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

                if (GetAOs("INLIENTUC") == "1")
                {
                    var printerst = V6ControlFormHelper.ChoosePrinter(this, PrinterName);
                    if (printerst != null)
                    {
                        PrinterName = printerst.PrinterName;
                        PrintCopies = printerst.Copies;
                        V6BusinessHelper.WriteOldSelectPrinter(PrinterName);
                        Close_after_print = true;
                        Print(PrinterName, _repx0);
                    }
                }
                else
                {
                    //in thường
                    var printTool = new ReportPrintTool(_repx0);
                    printTool.PrintingSystem.ShowMarginsWarning = false;
                    printTool.PrintDialog();
                }
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
            if (MauInSelectedRow == null) return;
            
            _albcConfig = new AlbcConfig(MauInSelectedRow.ToDataDictionary());

            GetSumCondition();

            txtReportTitle.Text = ReportTitle;
            if (ReloadData == "1")
                MakeReport2(PrintMode, PrinterName, _printCopy);
            else
            {
                FormatGridView();
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
                f2.FormControl.All_Objects["IS_DX"] = "1";
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
            try
            {
                if (_ds == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.EditRepx(ReportFileFullDX, _ds, ReportDocumentParameters, this);
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
                string excelColumns = Report_GRDSV1;
                string excelHeaders = V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1;
                if (string.IsNullOrEmpty(excelColumns) || string.IsNullOrEmpty(excelHeaders))
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
                        ReportFile, ExcelTemplateFileView, ReportTitle, excelColumns, excelHeaders);
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

        private void viewDataMenu_Click(object sender, EventArgs e)
        {
            dataGridView1.ViewDataToNewForm();
        }

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadComboboxSource();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            SaveSelectedCellLocation(dataGridView1);
            if(dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            SelectedRowData = dataGridView1.CurrentRow.ToDataDictionary();
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            //btnNhan.Focus();
        }
        
        private void exportToPdfMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx0 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx0, "PDF", GetExportFileName());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Export PDF", ex);
            }
        }

        private void exportToWordMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx0 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx0, "DOCX", ReportTitle);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "", ex);
            }
        }

        private void exportEXCELXtraMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx0 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx0, "XLSX", ReportTitle);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "", ex);
            }
        }

        private void exportEXCELDataMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx0 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx0, "XLSX_RAW", ReportTitle);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "", ex);
            }
        }

        

        private void exportReportToHtmlMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx0 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx0, "HTML", ReportTitle);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "", ex);
            }
        }

        private void exportReportToImageMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx0 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx0, "IMAGE", ReportTitle);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "", ex);
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
                this.ShowErrorMessage(GetType() + ".btnSuaLine_Click: " + ex.Message);
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

        private void dataGridView1_FilterChange()
        {
            //V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
            if (_albcConfig != null && _albcConfig.HaveInfo)
            {
                V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                    _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
            }
        }

        private void documentViewer1_ZoomChanged(object sender, EventArgs e)
        {
            V6ControlsHelper.ShowV6Tooltip(documentViewer1, string.Format("{0} {1}%", V6Text.Zoom, documentViewer1.Zoom * 100));
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
