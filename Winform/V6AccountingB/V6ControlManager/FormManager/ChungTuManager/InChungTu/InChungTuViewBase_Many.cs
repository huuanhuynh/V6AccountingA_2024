using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter;
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
using V6Tools.V6Export;
using V6ControlManager.FormManager.ReportManager.DXreport;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu
{
    public partial class InChungTuViewBase_Many : V6FormControl
    {
        #region Biến toàn cục

        public decimal TTT { get; set; }
        public decimal TTT_NT { get; set; }
        public string MA_NT { get; set; }

        private ReportDocument _rpDoc10, _rpDoc20, _rpDoc30, _rpDoc40;

        private string _reportProcedure;
        // _reportFile = ma_file
        private V6InvoiceBase Invoice;
        private string _program, _Ma_File, _reportTitle, _reportTitle2;
        private string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        public string Report_Stt_rec{get; set; }
        /// <summary>
        /// Advance filter get albc
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
                var AlbcData = V6BusinessHelper.Select("ALBC", AlbcKeys, "*").Data;
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
                //gridViewSummary1.Visible = FilterControl.ViewSum;
                
                var lineList = FilterControl.GetFilterLineList();
                foreach (KeyValuePair<string, FilterLineBase> item in lineList)
                {
                    All_Objects[item.Key] = item.Value;
                }
                All_Objects["thisForm"] = this;
                SetStatus2Text();
                //gridViewSummary1.Visible = FilterControl.ViewSum;

                InvokeFormEvent(FormDynamicEvent.AFTERADDFILTERCONTROL);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateFormControls", ex);
            }
        }

        private DataSet _ds;
        private DataTable _tbl1_AD, _tbl2_AM, _tbl2, _tbl3;
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

        private string MAU
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
                if (cboMauIn.DataSource != null && cboMauIn.SelectedItem is DataRowView && cboMauIn.SelectedIndex >= 0)
                {
                    return ((DataRowView)cboMauIn.SelectedItem).Row;
                }
                return null;
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

        private int SelectedSoLien
        {
            get
            {
                var row = MauInSelectedRow;
                int result = 0;
                if (row != null)
                {
                    result = ObjectAndString.ObjectToInt(row["SO_LIEN"]);
                }
                if (result <= 0) result = Invoice_SoLien;
                return result;
            }
        }
        
        private int ROW_MAX
        {
            get
            {
                var row = MauInSelectedRow;
                int result = 0;
                if (row != null && row.Table.Columns.Contains("ROW_MAX"))
                {
                    result = ObjectAndString.ObjectToInt(row["ROW_MAX"]);
                }
                if (result <= 0) result = 0;
                return result;
            }
        }

        private int Invoice_SoLien
        {
            get { return Invoice.SoLien == 0 ? 1 : Invoice.SoLien; }
        }

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
                var result = txtReportTitle.Text;
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["Title"].ToString().Trim();
                }
                return result;
            }
        }

        private bool IsInvoice
        {
            get
            {
                var result = false;
                if (MauInSelectedRow != null)
                {
                    result = (ObjectAndString.ObjectToInt(MauInSelectedRow["ND51"]) & 1) > 0;
                }
                return result;
            }
        }
        
        private bool IsPlus
        {
            get
            {
                var result = false;
                if (MauInSelectedRow != null)
                {
                    result = (ObjectAndString.ObjectToInt(MauInSelectedRow["ND51"]) & 2) > 0;
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

        public string ReportFileFull_1
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_1.rpt";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_1.rpt";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFull_2
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_2.rpt";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_2.rpt";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFull_3
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_3.rpt";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_3.rpt";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFull_4
        {
            get
            {
                var result = V6Login.StartupPath + @"\Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_4.rpt";
                if (!File.Exists(result))
                {
                    result = V6Login.StartupPath + @"\Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_4.rpt";//_reportFile gốc
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
                var result = Path.Combine(V6Login.StartupPath, @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".xls");
                if (File.Exists(result + "x")) result += "x";
                if (!File.Exists(result))
                {
                    result = Path.Combine(V6Login.StartupPath, @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + ".xls");
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
                var result = Path.Combine(V6Login.StartupPath, @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_view.xls");
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
                var result = Path.Combine(V6Login.StartupPath, @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "HTKK.xls");
                if (!File.Exists(result))
                {
                    result = Path.Combine(V6Login.StartupPath, @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "HTKK.xls");
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
                var result = Path.Combine(V6Login.StartupPath, @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "ONLINE.xls");
                if (!File.Exists(result))
                {
                    result = Path.Combine(V6Login.StartupPath, @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "ONLINE.xls");
                }
                return result;
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
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".getDefaultPrinter", ex);
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
                catch (Exception)
                {

                }
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

        public delegate void PrintSuccessDelegate(InChungTuViewBase_Many sender, string stt_rec, AlbcConfig albcConfig);
        public event PrintSuccessDelegate PrintSuccess;
        protected virtual void CallPrintSuccessEvent()
        {
            var handler = PrintSuccess;

            if (handler != null)
            {
                AlbcConfig config = new AlbcConfig(MauInSelectedRow.ToDataDictionary());
                handler(this, Report_Stt_rec, config);
            }
        }
        
        public InChungTuViewBase_Many(V6InvoiceBase invoice,
            string program, string reportProcedure,
            string reportFile, string reportTitle, string reportTitle2,
            string reportFileF5, string reportTitleF5, string reportTitle2F5, DataTable report_data, string current_report_stt_rec)
        {
            Invoice = invoice;
            _program = program;
            _reportProcedure = reportProcedure;
            _Ma_File = reportFile;
            _reportTitle = reportTitle;
            _reportTitle2 = reportTitle2;

            _reportFileF5 = reportFileF5;
            _reportTitleF5 = reportTitleF5;
            _reportTitle2F5 = reportTitle2F5;

            V6ControlFormHelper.AddLastAction(GetType() + " " + invoice.Mact + " " + program);
            
            InitializeComponent();
            dataGridView_Many.DataSource = report_data.Copy();
            Report_Stt_rec = current_report_stt_rec;
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
                InvokeFormEvent(FormDynamicEvent.INIT);
                Disposed += InChungTuViewBase_Many_Disposed;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
            }
        }

        private void CheckRightReport()
        {
            bool no_print = false;
            if (!V6Login.UserRight.AllowPrint(ItemID, Invoice.CodeMact))
            {
                no_print = true;
                crystalReportViewer1.ShowPrintButton = false;
                crystalReportViewer1.ShowExportButton = false;
                contextMenuStrip1.Items.Remove(exportToPdfMenu);
            }
            if (!V6Login.UserRight.AllowView(ItemID, Invoice.CodeMact))
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

        private int _viewer_focus_count = 0;
        

        private void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_Ma_File, Invoice.Mact, Report_Stt_rec, Advance);
            
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
        
        private int MauTuIn
        {
            get
            {
                if (MauInData.Rows.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    return ObjectAndString.ObjectToInt(MauInSelectedRow["MAU_TU_IN"]);
                }
                return 0;
            }
        }

        private void MyInit2()
        {
            try
            {
                if (Invoice.ExtraInfo_PrintNT && MA_NT != V6Options.M_MA_NT0)
                {
                    rNgoaiTe.Checked = true;
                }

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
                //
                GetSumCondition();
                txtReportTitle.Text = ReportTitle;

                numSoLien.Value = SelectedSoLien;
                if (numSoLien.Value > 0) numSoLien.Enabled = true;

                string key3 = "1";
                var menuRow = V6Menu.GetRowByMact(Invoice.Mact);
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
                            //if (!key3.Contains("2")) exportToExcelViewMenu.Visible = false;
                            if (!key3.Contains("3")) exportToExcelMenu.Visible = false;
                            //if (!key3.Contains("4")) exportToXmlMenu.Visible = false;
                            if (!key3.Contains("5")) printGridMenu.Visible = false;
                            //if (!key3.Contains("6")) viewDataMenu.Visible = false;
                            if (!key3.Contains("7")) exportToPdfMenu.Visible = false;
                            if (!key3.Contains("8")) viewInvoiceInfoMenu.Visible = false;
                        }
                    }
                }

                if (key3.Length > 0)
                    switch (key3[0])
                    {
                        case '1': DefaultMenuItem = exportToExcelTemplateMenu; break;
                        //case '2': DefaultMenuItem = exportToExcelView; break;
                        case '3': DefaultMenuItem = exportToExcelMenu; break;
                        //case '4': DefaultMenuItem = exportToXmlMenu; break;
                        case '5': DefaultMenuItem = printGridMenu; break;
                        //case '6': DefaultMenuItem = viewDataMenu; break;
                        case '7': DefaultMenuItem = exportToPdfMenu; break;
                        //case '8': DefaultMenuItem = viewInvoiceInfoMenu; break;
                    }

                InvokeFormEvent(FormDynamicEvent.INIT2);
                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2 " + ReportFileFull, ex);
            }
        }
        private ToolStripMenuItem DefaultMenuItem = null;

        private void GetSumCondition()
        {
            try
            {
                //gridViewSummary1.NoSumColumns = Report_GRDT_V1;
                if (MauInSelectedRow != null)
                {
                    //gridViewSummary1.SumCondition = new Condition()
                    //{
                    //    FIELD = MauInSelectedRow["FIELD_S"].ToString().Trim(),
                    //    OPER = MauInSelectedRow["OPER_S"].ToString().Trim(),
                    //    VALUE = MauInSelectedRow["VALUE_S"].ToString().Trim()
                    //};
                    //if (!string.IsNullOrEmpty(gridViewSummary1.SumConditionString)) toolTipV6FormControl.SetToolTip(gridViewSummary1, gridViewSummary1.SumConditionString);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetSumCondition", ex);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyInit2();
            MakeReport(Report_Stt_rec);
            //MakeReport(PrintMode, PrinterName, (int)numSoLien.Value, _printCopy);
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
            catch (Exception)
            {
                // ignored
            }
        }
        
        public InChungTuFilterBase FilterControl { get; set; }
        private void AddFilterControl(string program)
        {
            FilterControl = InFilter.GetFilterControl(program);
            FilterControl.SetFieldValue(Report_Stt_rec);
            
            panel1.Controls.Add(FilterControl);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (ActiveControl == crystalReportViewer1
                    || ActiveControl == crystalReportViewer2
                    || ActiveControl == crystalReportViewer3
                    || ActiveControl == crystalReportViewer4
                    || ActiveControl == btnIn)
                {
                    btnIn_Click(btnIn, null);
                    return true;
                }
            }
            else if (keyData == (Keys.Control | Keys.E))
            {
                btnExport3.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void btnNhan_Click(object sender, EventArgs e)
        {
            btnNhanImage = btnNhan.Image;
            MakeReport(V6PrintMode.DoNoThing, null, (int) numSoLien.Value);
        }

        /// <summary>
        /// Tính toán đường chéo sẽ hiện trên report 10x2=20
        /// </summary>
        /// <param name="t">Bảng dữ liệu</param>
        /// <param name="field">Trường tính toán ký tự</param>
        /// <param name="lengOfName"></param>
        /// <param name="twLineHeight"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        private int CalculateCrossLine(DataTable t, string field, int lengOfName, int twLineHeight, float fontSize)
        {
            int lineDroppedHeight = 600;//Font size 10. Chiều cao tương đối của ô text khi rớt xuống 1 dòng
            int dropLineHeightBase = 300;//Font size 10. Phần cao thêm khi rớt tiếp dòng thứ 2

            lineDroppedHeight += (int)((fontSize - 10) * 50);
            dropLineHeightBase += (int)((fontSize - 10) * 50);

            var dropLineHeight1 = lineDroppedHeight - twLineHeight;//Tính phần cao thêm khi rớt dòng thứ 1.
            //Mỗi dòng drop sẽ nhân với DropLineHeight
            //var dropCount = 0;
            var dropHeight = 0;
            foreach (DataRow r in t.Rows)
            {
                try
                {
                    var len = r[field].ToString().Trim().Length;
                    if (len > 1) len--;
                    
                    var dropExtra = len / lengOfName;
                    if (dropExtra > 0)
                    {
                        dropHeight += dropLineHeight1;
                        if (dropExtra > 1) dropHeight += dropLineHeightBase*(dropExtra-1);
                        //if (dropCount > 2) dropHeight += dropLineHeightBase;
                        //if (dropCount > 3) dropHeight += dropLineHeightBase;
                        //if (dropCount > 4) dropHeight += dropLineHeightBase;
                    }
                }
                catch
                {
                    // ignored
                }
            }
            

            //var dropHeight = dropLineHeight * dropCount;
            //số 2 ở đây là vì mỗi dòng có 2 cross line
            var dropCount = dropHeight/(twLineHeight/2);
            if (dropCount == 0 && dropHeight > 0) dropCount = 1;
            return t.Rows.Count * 2 + dropCount;
        }
        
        private void GenerateProcedureParameters()
        {
            try
            {
                _pList = new List<SqlParameter>();
                _pList.AddRange(FilterControl.GetFilterParameters());

                _pList.Add(new SqlParameter("@isInvoice", IsInvoice?"1":"0"));
                _pList.Add(new SqlParameter("@ReportFile", ReportFile));
                
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("GenerateProcedureParameters: " + ex.Message);
            }
        }

        private void SetCrossLineAll(ReportDocument rpDoc, ReportDocument rpDoc2, ReportDocument rpDoc3, ReportDocument rpDoc4)
        {
            try
            {
                SetCrossLineRpt(rpDoc);
                // SUBREPORT
                for (int i = 0; i < rpDoc.Subreports.Count; i++)
                {
                    SetCrossLineRpt(rpDoc.Subreports[i]);
                }

                if (MauTuIn == 1 && _soLienIn >= 2 && rpDoc2 != null)
                {
                    SetCrossLineRpt(rpDoc2);
                    // SUBREPORT
                    for (int i = 0; i < rpDoc2.Subreports.Count; i++)
                    {
                        SetCrossLineRpt(rpDoc2.Subreports[i]);
                    }
                }
                if (MauTuIn == 1 && _soLienIn >= 3 && rpDoc3 != null)
                {
                    SetCrossLineRpt(rpDoc3);
                    // SUBREPORT
                    for (int i = 0; i < rpDoc3.Subreports.Count; i++)
                    {
                        SetCrossLineRpt(rpDoc3.Subreports[i]);
                    }
                }
                if (MauTuIn == 1 && _soLienIn >= 4 && rpDoc4 != null)
                {
                    SetCrossLineRpt(rpDoc4);
                    // SUBREPORT
                    for (int i = 0; i < rpDoc4.Subreports.Count; i++)
                    {
                        SetCrossLineRpt(rpDoc4.Subreports[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetCrossLine", ex);
            }
        }

        private Dictionary<string, ReportObject> GetRprObjects(ReportDocument rpt)
        {
            var result = new Dictionary<string, ReportObject>();
            foreach (ReportObject o in rpt.ReportDefinition.ReportObjects)
            {
                result[o.Name] = o;
            }
            return result;
        }

        private void SetCrossLineRpt(ReportDocument rpt)
        {
            int flag = 0;
            var checkField = "TEN_VT";
            data_overflow = false;
            try
            {
                if (!IsInvoice) return;

                var Khung = rpt.ReportDefinition.ReportObjects["Khung"];
                var DuongNgang = rpt.ReportDefinition.ReportObjects["DuongNgang"] as TextObject;
                
                flag = 1;
                //var Section1 = rpt.ReportDefinition.Sections["ReportHeaderSection1"];
                //var Section2 = rpt.ReportDefinition.Sections["Section3"];
                //var h1 = Section1.Height;
                //var h2 = Section2.Height;
                
                //Biến chung
                int boxTop = Khung.Top; // 6500;
                int boxHeight = Khung.Height; // 3840;
                int boxLeft = Khung.Left;
                int boxWidth = Khung.Width;
                int lineHeight = DuongNgang.Height;
                int halfLineHeight = lineHeight/2; // boxHeight/20;//192, 20 is maxLine
                int dropMax = 40;
                try
                {
                    dropMax = ObjectAndString.ObjectToInt(Invoice.Alct["drop_Max"]);
                    if (dropMax < 1) dropMax = 40;
                    //Lấy lại thông tin dropMax theo albc (cboMauin)
                    if (MauInSelectedRow != null && MauInSelectedRow.Table.Columns.Contains("DROP_MAX"))
                    {
                        var dropMaxT = ObjectAndString.ObjectToInt(MauInSelectedRow["DROP_MAX"]);
                        if (dropMaxT > 5) dropMax = dropMaxT;
                    }
                    //Lấy lại checkField (khác MA_VT)
                    if (MauInSelectedRow != null && MauInSelectedRow.Table.Columns.Contains("FIELD_MAX"))
                    {
                        var checkFieldT = MauInSelectedRow["FIELD_MAX"].ToString().Trim();
                        if (checkFieldT.Length > 0) checkField = checkFieldT;
                    }
                }
                catch
                {
                    flag = 2;
                }

                if (!_tbl1_AD.Columns.Contains(checkField))
                {
                    checkField = _tbl1_AD.Columns.Contains("DIEN_GIAII") ? "DIEN_GIAII" : _tbl1_AD.Columns[0].ColumnName;
                }
                flag = 3;
                float fontSize = ((TextObject) DuongNgang).Font.Size;
                int crossLineNum = CalculateCrossLine(_tbl1_AD, checkField, dropMax, lineHeight, fontSize)
                                       + (int)numCrossAdd.Value;
                if (ROW_MAX > 0 && crossLineNum > ROW_MAX * 2)
                {
                    data_overflow = true;
                }
                else
                {
                    data_overflow = false;
                }
                var top = boxTop + (halfLineHeight * crossLineNum); //3840/20=192
                var height = boxHeight - (top - boxTop);
                flag = 5;

                string duongNgangText = ((TextObject) DuongNgang).Text + "";
                duongNgangText = duongNgangText.Trim();
                if (duongNgangText == "")
                {
                    //Kiểu cũ DuongCheo = WordObject
                    var DuongCheo = rpt.ReportDefinition.ReportObjects["DuongCheo"];
                    if (height < 150) // Hide lowCrossline.
                    {
                        height = 10;
                        DuongNgang.Width = Khung.Width;// DuongNgang.Width + DuongCheo.Width;
                        DuongCheo.Width = 10;
                    }

                    DuongNgang.Height = 10;
                    DuongNgang.Top = top + 30;

                    DuongCheo.Height = height;
                    DuongCheo.Top = top;

                    flag = 9;
                }
                else
                {
                    
                    if (height < 150) // Hide lowCrossline.
                    {
                        height = 10;
                        //DuongNgang.Width = DuongNgang.Width + DuongCheo.Width;
                        //DuongCheo.Width = 10;
                    }

                    DuongNgang.Height = 10;
                    DuongNgang.Top = top;

                    //Tính toán vị trí 500 anh em đường chéo DC1000 đến DC1499
                    int numofdc = 500;
                    int dc_left_base = DuongNgang.Width < 1000 ? boxLeft : DuongNgang.Left + DuongNgang.Width;
                    int dc_with = boxLeft + boxWidth - dc_left_base;
                    for (int i = 000; i < numofdc; i++)
                    {
                        string dc_name = "Text" + (1000 + i);
                        var dc1xxx = rpt.ReportDefinition.ReportObjects[dc_name];
                        int dc_left = 0, dc_top = 0;
                        dc_left = dc_left_base + i * dc_with / numofdc;
                        dc_top = top + i * height / numofdc;
                        dc1xxx.Left = dc_left;
                        dc1xxx.Top = dc_top;
                    }

                    flag = 9;
                }
            }
            catch(Exception ex)
            {
                if(flag == 3)
                    ShowMainMessage(string.Format(V6Text.Text("CHECKDROPLINEFIELD0"), checkField));
                this.WriteExLog(GetType() + ".SetCrossLineRpt", ex);
            }
        }

        private IDictionary<string, object> ReportDocumentParameters; 
        /// <summary>
        /// Lưu ý: chạy sau khi add dataSource để tránh l ỗi nhập parameter value
        /// </summary>
        private void SetAllReportParams(ReportDocument rpDoc, ReportDocument rpDoc2, ReportDocument rpDoc3, ReportDocument rpDoc4)
        {
            ReportDocumentParameters = new SortedDictionary<string, object>();
            
            ReportDocumentParameters.Add("Decimals", 0);
            ReportDocumentParameters.Add("ThousandsSeparator", V6Options.M_NUM_SEPARATOR);
            ReportDocumentParameters.Add("DecimalSymbol", V6Options.M_NUM_POINT);
            ReportDocumentParameters.Add("DecimalsSL", V6Options.M_IP_R_SL);
            ReportDocumentParameters.Add("DecimalsDG", V6Options.M_IP_R_GIA);
            ReportDocumentParameters.Add("DecimalsDGNT", V6Options.M_IP_R_GIANT);
            ReportDocumentParameters.Add("DecimalsTT", V6Options.M_IP_R_TIEN);
            ReportDocumentParameters.Add("DecimalsTTNT", V6Options.M_IP_R_TIENNT);

            ReportDocumentParameters.Add("Mau?", 0);
            ReportDocumentParameters.Add("BanSao?", false);
            ReportDocumentParameters.Add("ViewInfo", MauTuIn==1);
            ReportDocumentParameters.Add(
                "Info",
                "In bởi  Phần mềm V6 Accounting2016.NET - Cty phần mềm V6 (www.v6corp.com) - MST: 0303180249 - ĐT: 028.62570563"
            );
            ReportDocumentParameters.Add("ViewCrossLine", true);


            //ReportDocumentParameters.Add("CrossLineNum", crossLineNum + numCrossAdd.Value);

            ReportDocumentParameters.Add("SoTienVietBangChu", V6BusinessHelper.MoneyToWords(TTT, LAN, V6Options.M_MA_NT0));
            ReportDocumentParameters.Add("SoTienVietBangChuNT", V6BusinessHelper.MoneyToWords(TTT_NT, LAN, MA_NT));
            
            //ReportDocumentParameters.Add("ChuoiMaHoa", V6BusinessHelper.GetChuoiMaHoa(""));

            ReportDocumentParameters.Add("Title", txtReportTitle.Text.Trim());
            // V6Soft
            ReportDocumentParameters.Add("M_TEN_CTY", V6Soft.V6SoftValue["M_TEN_CTY"].ToUpper());
            ReportDocumentParameters.Add("M_TEN_TCTY", V6Soft.V6SoftValue["M_TEN_TCTY"].ToUpper());
            ReportDocumentParameters.Add("M_DIA_CHI", V6Soft.V6SoftValue["M_DIA_CHI"]);


            ReportDocumentParameters.Add("M_TEN_CTY2", V6Soft.V6SoftValue["M_TEN_CTY2"].ToUpper());
            ReportDocumentParameters.Add("M_TEN_TCTY2", V6Soft.V6SoftValue["M_TEN_TCTY2"].ToUpper());
            ReportDocumentParameters.Add("M_DIA_CHI2", V6Soft.V6SoftValue["M_DIA_CHI2"]);
            // V6option
            ReportDocumentParameters.Add("M_MA_THUE", V6Options.GetValue("M_MA_THUE"));
            ReportDocumentParameters.Add("M_RTEN_VSOFT", V6Options.GetValue("M_RTEN_VSOFT"));

            ReportDocumentParameters.Add("M_TEN_NLB", txtM_TEN_NLB.Text.Trim());
            ReportDocumentParameters.Add("M_TEN_NLB2", txtM_TEN_NLB2.Text.Trim());
            ReportDocumentParameters.Add("M_TEN_KHO_BD", V6Options.GetValue("M_TEN_KHO_BD"));
            ReportDocumentParameters.Add("M_TEN_KHO2_BD", V6Options.GetValue("M_TEN_KHO2_BD"));
            ReportDocumentParameters.Add("M_DIA_CHI_BD", V6Options.GetValue("M_DIA_CHI_BD"));
            ReportDocumentParameters.Add("M_DIA_CHI2_BD", V6Options.GetValue("M_DIA_CHI2_BD"));

            ReportDocumentParameters.Add("M_TEN_GD", V6Options.GetValue("M_TEN_GD"));
            ReportDocumentParameters.Add("M_TEN_GD2", V6Options.GetValue("M_TEN_GD2"));
            ReportDocumentParameters.Add("M_TEN_KTT", V6Options.GetValue("M_TEN_KTT"));
            ReportDocumentParameters.Add("M_TEN_KTT2", V6Options.GetValue("M_TEN_KTT2"));

            ReportDocumentParameters.Add("M_SO_QD_CDKT", V6Options.GetValue("M_SO_QD_CDKT"));
            ReportDocumentParameters.Add("M_SO_QD_CDKT2", V6Options.GetValue("M_SO_QD_CDKT2"));
            ReportDocumentParameters.Add("M_NGAY_QD_CDKT", V6Options.GetValue("M_NGAY_QD_CDKT"));
            ReportDocumentParameters.Add("M_NGAY_QD_CDKT2", V6Options.GetValue("M_NGAY_QD_CDKT2"));

            ReportDocumentParameters.Add("M_RFONTNAME", V6Options.GetValue("M_RFONTNAME"));
            ReportDocumentParameters.Add("M_R_FONTSIZE", V6Options.GetValue("M_R_FONTSIZE"));
            

            V6Login.SetCompanyInfo(ReportDocumentParameters);

            if (FilterControl.RptExtraParameters != null)
            {
                ReportDocumentParameters.AddRange(FilterControl.RptExtraParameters, true);
            }

            var rptExtraParametersD = FilterControl.GetRptParametersD(Extra_para, LAN);
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
                    // SUBREPORT
                    for (int i = 0; i < rpDoc.Subreports.Count; i++)
                    {
                        rpDoc.SetParameterValue(item.Key, item.Value, rpDoc.Subreports[i].Name);
                    }
                }
                catch (Exception ex)
                {
                    errors += "rpDoc " + item.Key + ": " + ex.Message + "\n";
                }
            }

            if (MauTuIn == 1 && _soLienIn >= 2 && rpDoc2 != null)
            {
                foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
                {
                    try
                    {
                        rpDoc2.SetParameterValue(item.Key, item.Value);
                        // SUBREPORT
                        for (int i = 0; i < rpDoc2.Subreports.Count; i++)
                        {
                            rpDoc2.SetParameterValue(item.Key, item.Value, rpDoc2.Subreports[i].Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        errors += "rpDoc2 " + item.Key + ": " + ex.Message + "\n";
                    }
                }
            }
            if (MauTuIn == 1 && _soLienIn >= 3 && rpDoc3 != null)
            {
                foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
                {
                    try
                    {
                        rpDoc3.SetParameterValue(item.Key, item.Value);
                        // SUBREPORT
                        for (int i = 0; i < rpDoc3.Subreports.Count; i++)
                        {
                            rpDoc3.SetParameterValue(item.Key, item.Value, rpDoc3.Subreports[i].Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        errors += "rpDoc3 " + item.Key + ": " + ex.Message + "\n";
                    }
                }
            }
            if (MauTuIn == 1 && _soLienIn >= 4 && rpDoc4 != null)
            {
                foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
                {
                    try
                    {
                        rpDoc4.SetParameterValue(item.Key, item.Value);
                        // SUBREPORT
                        for (int i = 0; i < rpDoc4.Subreports.Count; i++)
                        {
                            rpDoc4.SetParameterValue(item.Key, item.Value, rpDoc4.Subreports[i].Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        errors += "rpDoc4 " + item.Key + ": " + ex.Message + "\n";
                    }
                }
            }

            if (errors != "")
            {
                V6ControlFormHelper.WriteToLog(GetType() + ".SetAllReportParams\r\nFile: " + ReportFileFull, errors);
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
                if (FilterControl is FilterDanhMuc)
                {
                    proc = "VPA_R_AL_ALL";
                }
                else
                {
                    proc = _reportProcedure;
                }
                _ds = V6BusinessHelper.ExecuteProcedure(proc, _pList.ToArray());
                //12/06/2018 Chuyển FilterControl.LoadDataFinish(_ds); từ timer về đây.
                FilterControl.LoadDataFinish(_ds);
                if (_ds.Tables.Count > 0)
                {
                    _tbl1_AD = _ds.Tables[0];
                    _tbl1_AD.TableName = "DataTable1";
                }
                if (_ds.Tables.Count > 1)
                {
                    _tbl2_AM = _ds.Tables[1];
                    _tbl2_AM.TableName = "DataTable2";
                    if (_tbl2_AM.Rows.Count > 0) FilterControl.Call1(_tbl2_AM.Rows[0]);
                }
                else
                {
                    _tbl2_AM = null;
                }
                
                _executesuccess = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadData Error\n" + ex.Message, "InChungTuViewBase_Many");
                _tbl1_AD = null;
                _tbl2_AM = null;
                _ds = null;
                _executesuccess = false;
            }
            _executing = false;
        }

        public string PrinterName { get; set; }
        private int _soLienIn = 1, _printCopy = 1;
        
        public int PrintCopies
        {
            get { return _printCopy; }
            set { _printCopy = value; }
        }

        public bool Close_after_print { get; set; }
        /// <summary>
        /// 0 DoNoThing 1 AutoPrint 2 AutoClickPrint 3 AutoClickExport
        /// </summary>
        public V6PrintMode PrintMode { get; set; }

        public void MakeReport(string stt_rec)
        {
            try
            {
                // gán 1 số thông số trước khi MakeReport
                Report_Stt_rec = stt_rec;
                // set current row cho gridview_many
                foreach (DataGridViewRow row in dataGridView_Many.Rows)
                {
                    if (row.Cells["STT_REC"].Value.ToString() == Report_Stt_rec)
                    {
                        dataGridView_Many.CurrentCell = row.Cells["SO_CT"];
                        break;
                    }
                }
                // set filter value
                FilterControl.SetFieldValue(Report_Stt_rec);
                // do make report
                MakeReport(PrintMode, PrinterName, (int)numSoLien.Value, _printCopy);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
        }

        /// <summary>
        /// Make report bằng current row.
        /// </summary>
        public void MakeReport()
        {
            try
            {
                DataGridViewRow row = dataGridView_Many.CurrentRow;
                // gán 1 số thông số trước khi MakeReport
                Report_Stt_rec = row.Cells["STT_REC"].Value.ToString().Trim();
                // set filter value
                FilterControl.SetFieldValue(Report_Stt_rec);
                // do make report
                MakeReport(PrintMode, PrinterName, (int)numSoLien.Value, _printCopy);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printMode">In luôn hoặc không?</param>
        /// <param name="printerName">Nếu printMode=AutoPrint thì bắt buộc có printerName</param>
        /// <param name="soLien"></param>
        /// <param name="printCopy"></param>
        public void MakeReport(V6PrintMode printMode, string printerName,
            int soLien, int printCopy = 1)
        {
            //if (_dataLoading)
            //{
            //    return false;
            //}
            try
            {
                //_forcePrint = forcePrint;
                PrintMode = printMode;
                PrinterName = printerName;
                _soLienIn = soLien;
                if (_soLienIn < 1) _soLienIn = 1;
                _printCopy = printCopy;

                GenerateProcedureParameters(); //Add các key khác

                LoadData();

                try
                {
                    dataGridView1.SetFrozen(0);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _tbl1_AD;
                    dataGridView1.DataSource = _tbl2_AM;
                    FormatGridView();
                    
                    ViewReport();
                    if (PrintMode == V6PrintMode.AutoPrint)
                    {
                        Print(PrinterName, _rpDoc10, _rpDoc20, _rpDoc30, _rpDoc40);
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
                    //if (!dataGridView1.IsDisposed) dataGridView1.Focus();
                    //btnIn.Focus();
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(GetType() + ".MakeReport " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message, "InChungTuViewBase_Many");
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
                    //FilterControl.LoadDataFinish(_ds); // Chuyển về vị trí sau khi gán _ds
                    //if (_tbl1.Rows.Count > 0) FilterControl.Call1(_tbl1.Rows[0]);
                    All_Objects["_ds"] = _ds;
                    InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                    dataGridView1.SetFrozen(0);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _tbl1_AD;
                    
                    FormatGridView();
                    ViewReport();
                    if (PrintMode == V6PrintMode.AutoPrint)
                    {
                        Print(PrinterName, _rpDoc10, _rpDoc20, _rpDoc30, _rpDoc40);
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

                    //dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    _executesuccess = false;
                    this.ShowErrorMessage(GetType() + ".MakeReport " + ex.Message);
                }
            }
            else if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
            }
        }

        private void FormatGridView()
        {
            try
            {
                //V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                        _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
                }

                //Header
                var fieldList = (from DataColumn column in _tbl1_AD.Columns select column.ColumnName).ToList();

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
                V6ControlFormHelper.FormatGridViewHideColumns(dataGridView1, Invoice.Mact);

                
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

        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        void InChungTuViewBase_Many_Disposed(object sender, EventArgs e)
        {
            try
            {
                //_rpDoc.Close();
                //if (_rpDoc2 != null) _rpDoc2.Close();
                //if (_rpDoc3 != null) _rpDoc3.Close();
                //if (_rpDoc4 != null) _rpDoc4.Close();

                //_rpDoc.Dispose();
                //if (_rpDoc2 != null) _rpDoc2.Dispose();
                //if (_rpDoc3 != null) _rpDoc3.Dispose();
                //if (_rpDoc4 != null) _rpDoc4.Dispose();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Disposed", ex);
            }
        }
        
        #endregion Linh tinh

        private void exportToExcelMenu_Click(object sender, EventArgs e)
        {
            if (_tbl1_AD == null)
            {
                ShowMainMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = @"Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    FileName = GetExportFileName()
                };
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var setting = new ExportExcelSetting();
                        setting.data = _tbl1_AD;
                        setting.saveFile = save.FileName;
                        setting.title = Name;
                        setting.isDrawLine = true;
                        V6Tools.V6Export.ExportData.ToExcel(setting);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorMessage(GetType() + ".ExportFail: " + ex.Message, "InChungTuViewBase_Many");
                        return;
                    }
                    if (V6Options.AutoOpenExcel)
                    {
                        V6ControlFormHelper.OpenFileProcess(save.FileName);
                    }
                    else
                    {
                        this.ShowInfoMessage(V6Text.ExportFinish);
                    }
                    if (PrintMode == V6PrintMode.AutoExportT)
                    {
                        btnHuy.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ExportFail\n" + ex.Message, "InChungTuViewBase_Many");
            }
        }


        private void rbtTienTe_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady || string.IsNullOrEmpty(MA_NT)) return;
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
                    MakeReport(V6PrintMode.DoNoThing, null, (int)numSoLien.Value);
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
                if (!IsReady || string.IsNullOrEmpty(MA_NT)) return;
                if (((RadioButton)sender).Checked)
                {
                    _radioRunning = true;
                    _radioChange = true;
                    txtReportTitle.Text = ReportTitle;// (rTiengViet.Checked ? ReportTitle : rEnglish.Checked ? ReportTitle : (ReportTitle+2 ));
                    SetFormReportFilter();
                    if (MauInView.Count > 0 && cboMauIn.SelectedIndex >= 0)
                    {
                        txtReportTitle.Text = ReportTitle;
                    }

                    if (ReloadData == "1")
                        MakeReport(V6PrintMode.DoNoThing, null, (int)numSoLien.Value);
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
            if (_tbl1_AD == null)
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
                this.ShowErrorMessage(GetType() + ".PrintGrid\n" + ex.Message, "InChungTuViewBase_Many");
            }
        }
        

        void ViewReport()
        {
            if (_ds == null) return;
            
            CleanUp();
            ReportDocument rpDoc = null, rpDoc2 = null, rpDoc3 = null, rpDoc4 = null;
            FixReportViewerToolbarButton(true);

            if ((MauTuIn == 1 && V6Login.IsAdmin) || (MauTuIn == 1 && ObjectAndString.ObjectToBool(Invoice.Alctct["R_INLIEN"])))
            {
                btnIn.ContextMenuStrip = menuBtnIn;
                inLien1Menu.Visible = (_soLienIn >= 1);
                inLien2Menu.Visible = (_soLienIn >= 2);
                inLien3Menu.Visible = (_soLienIn >= 3);
                inLien4Menu.Visible = (_soLienIn >= 4);
            }
            else
            {
                btnIn.ContextMenuStrip = null;
            }
            
            if (MauTuIn == 1)
            {
                //Hoa don 3 lien 123
                rpDoc = new ReportDocument();
                rpDoc2 = new ReportDocument();
                rpDoc3 = new ReportDocument();
                rpDoc4 = new ReportDocument();

                try
                {
                    if (File.Exists(ReportFileFull_1)) rpDoc.Load(ReportFileFull_1);
                    else this.ShowWarningMessage(V6Text.NotExist + ": " + ReportFileFull_1);
                    rpDoc.SetDataSource(_ds.Copy());
                    // SUBREPORT
                    for (int i = 0; i < rpDoc.Subreports.Count; i++)
                    {
                        rpDoc.Subreports[i].SetDataSource(_ds.Copy());
                    }
                }
                catch (Exception e1)
                {
                    this.ShowErrorMessage(GetType() + ".ViewReport rpDoc.Load: " + e1.Message);
                }

                if (_soLienIn >= 2)
                {
                    try
                    {
                        if (File.Exists(ReportFileFull_2)) rpDoc2.Load(ReportFileFull_2);
                        else this.ShowWarningMessage(V6Text.NotExist + ": " + ReportFileFull_2);
                        rpDoc2.SetDataSource(_ds.Copy());
                        // SUBREPORT
                        for (int i = 0; i < rpDoc2.Subreports.Count; i++)
                        {
                            rpDoc2.Subreports[i].SetDataSource(_ds.Copy());
                        }
                    }
                    catch (Exception e2)
                    {
                        rpDoc2 = null;
                        this.ShowErrorMessage(GetType() + ".ViewReport rpDoc2.Load: " + e2.Message);
                    }
                }
                if (_soLienIn >= 3)
                {
                    try
                    {
                        if (File.Exists(ReportFileFull_3)) rpDoc3.Load(ReportFileFull_3);
                        else this.ShowWarningMessage(V6Text.NotExist + ": " + ReportFileFull_3);
                        rpDoc3.SetDataSource(_ds.Copy());
                        // SUBREPORT
                        for (int i = 0; i < rpDoc3.Subreports.Count; i++)
                        {
                            rpDoc3.Subreports[i].SetDataSource(_ds.Copy());
                        }
                    }
                    catch (Exception e3)
                    {
                        rpDoc3 = null;
                        this.ShowErrorMessage(GetType() + ".ViewReport rpDoc3.Load: " + e3.Message);
                    }
                }

                if (_soLienIn >= 4)
                {
                    try
                    {
                        if (File.Exists(ReportFileFull_4)) rpDoc4.Load(ReportFileFull_4);
                        else this.ShowWarningMessage(V6Text.NotExist + ": " + ReportFileFull_4);
                        rpDoc4.SetDataSource(_ds.Copy());
                        // SUBREPORT
                        for (int i = 0; i < rpDoc4.Subreports.Count; i++)
                        {
                            rpDoc4.Subreports[i].SetDataSource(_ds.Copy());
                        }
                    }
                    catch (Exception e4)
                    {
                        rpDoc4 = null;
                        this.ShowErrorMessage(GetType() + ".ViewReport rpDoc4.Load: " + e4.Message);
                    }
                }


                SetAllReportParams(rpDoc, rpDoc2, rpDoc3, rpDoc4);
                SetCrossLineAll(rpDoc, rpDoc2, rpDoc3, rpDoc4);
                var infos = EXTRA_INFOR;
                if (infos.ContainsKey("RPTHIDE"))
                {
                    var names = ObjectAndString.SplitString(infos["RPTHIDE"]);
                    RPTHIDE(rpDoc, names);
                    RPTHIDE(rpDoc2, names);
                    RPTHIDE(rpDoc3, names);
                    RPTHIDE(rpDoc4, names);
                }

                crystalReportViewer1.ReportSource = rpDoc;
                crystalReportViewer1.Zoom(Invoice.ExtraInfo_PrintVCzoom);
                
                _rpDoc10 = rpDoc;
                if (_soLienIn >= 2 && rpDoc2 != null)
                {
                    crystalReportViewer2.ReportSource = rpDoc2;
                    crystalReportViewer2.Zoom(Invoice.ExtraInfo_PrintVCzoom);
                    _rpDoc20 = rpDoc2;
                }
                if (_soLienIn >= 3 && rpDoc3 != null)
                {
                    crystalReportViewer3.ReportSource = rpDoc3;
                    crystalReportViewer3.Zoom(Invoice.ExtraInfo_PrintVCzoom);
                    _rpDoc30 = rpDoc3;
                }
                if (_soLienIn >= 4 && rpDoc4 != null)
                {
                    crystalReportViewer4.ReportSource = rpDoc4;
                    crystalReportViewer4.Zoom(Invoice.ExtraInfo_PrintVCzoom);
                    _rpDoc40 = rpDoc4;
                }

                crystalReportViewer1.Visible = true;
                crystalReportViewer2.Visible = false;
                crystalReportViewer3.Visible = false;
                crystalReportViewer4.Visible = false;
                
            }
            else
            {
                if (!IsInvoice)
                {
                    FixReportViewerToolbarButton(false);
                }
                
                rpDoc = new ReportDocument();
                if (File.Exists(ReportFileFull)) rpDoc.Load(ReportFileFull);
                else this.ShowWarningMessage(V6Text.NotExist + ": " + ReportFileFull);

                rpDoc.SetDataSource(_ds);
                
                SetAllReportParams(rpDoc, rpDoc2, rpDoc3, rpDoc4);
                SetCrossLineAll(rpDoc, rpDoc2, rpDoc3, rpDoc4);
                
                var infos = EXTRA_INFOR;
                if (infos.ContainsKey("RPTHIDE"))
                {
                    var names = ObjectAndString.SplitString(infos["RPTHIDE"]);
                    RPTHIDE(rpDoc, names);
                    RPTHIDE(rpDoc2, names);
                    RPTHIDE(rpDoc3, names);
                    RPTHIDE(rpDoc4, names);
                }

                crystalReportViewer1.ReportSource = rpDoc;
                crystalReportViewer1.Zoom(Invoice.ExtraInfo_PrintVCzoom);
                
                _rpDoc10 = rpDoc;
                
                crystalReportViewer1.Visible = true;
                crystalReportViewer2.Visible = false;
                crystalReportViewer3.Visible = false;
                crystalReportViewer4.Visible = false;
            }
            //btnIn.Focus();
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

                // SUBREPORT
                for (int i = 0; i < rpDoc.Subreports.Count; i++)
                {
                    var all_objects_sub = new SortedDictionary<string, ReportObject>();
                    foreach (ReportObject o in rpDoc.Subreports[i].ReportDefinition.ReportObjects)
                    {
                        all_objects_sub[o.Name.ToUpper()] = o;
                    }
                
                    foreach (string name in names)
                    {
                        string NAME = name.ToUpper();
                        if (all_objects_sub.ContainsKey(NAME))
                        {
                            all_objects_sub[NAME].ObjectFormat.EnableSuppress = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".RPTHIDE", ex);
            }
        }

        private void FixReportViewerToolbarButton(bool isLock)
        {
            if (isLock)
            {
                Lock(crystalReportViewer1, crystalReportViewer2, crystalReportViewer3, crystalReportViewer4);
            }
            else
            {
                Open(crystalReportViewer1, crystalReportViewer2, crystalReportViewer3, crystalReportViewer4);
            }
        }

        private void Lock(params CrystalReportViewer[] crViewers)
        {
            foreach (CrystalReportViewer crViewer in crViewers)
            {
                if (crViewer != null)
                {
                    crViewer.ShowExportButton = false;
                    crViewer.ShowPrintButton = false;
                }
            }
        }

        private void Open(params CrystalReportViewer[] crViewers)
        {
            foreach (CrystalReportViewer crViewer in crViewers)
            {
                if (crViewer != null)
                {
                    crViewer.ShowExportButton = true;
                    crViewer.ShowPrintButton = true;
                }
            }
        }

        private void Print(string printerName, ReportDocument rpDoc, ReportDocument rpDoc2, ReportDocument rpDoc3, ReportDocument rpDoc4)
        {
            int intDaGuiDenMayIn = 0;
            if (_printCopy < 1) _printCopy = 1;
            bool printerOnline = PrinterStatus.CheckPrinterOnline(printerName);
            
            if (printerOnline)
            {
                try
                {
                    if (MauTuIn == 1)
                    {
                        try
                        {
                            if (V6ControlFormHelper.PrinterSettings != null)
                            {
                                V6ControlFormHelper.SetCrystalReportPrinterOptions(NOPRINTER, V6ControlFormHelper.PrinterSettings, rpDoc);
                            }

                            if (NOPRINTER)
                            {
                                if (rpDoc.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(printerName);
                            }
                            else if (!NOPRINTER) PrinterStatus.SetDefaultPrinter(printerName);
                            rpDoc.PrintToPrinter(1, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0);
                            intDaGuiDenMayIn++;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format(V6Text.Text("PRINTXERRORSTOP"), 1) + ex.Message);
                        }

                        if (_soLienIn > 1)
                            try
                            {
                                if (V6ControlFormHelper.PrinterSettings != null)
                                {
                                    V6ControlFormHelper.SetCrystalReportPrinterOptions(NOPRINTER, V6ControlFormHelper.PrinterSettings, rpDoc2);
                                }

                                if (NOPRINTER)
                                {
                                    if (rpDoc2.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(printerName);
                                }
                                else if (!NOPRINTER) PrinterStatus.SetDefaultPrinter(printerName);
                                rpDoc2.PrintToPrinter(1, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0);
                                intDaGuiDenMayIn++;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(string.Format(V6Text.Text("PRINTXERRORSTOP"), 2) + ex.Message);
                            }
                        if (_soLienIn > 2)
                        {
                            try
                            {
                                if (V6ControlFormHelper.PrinterSettings != null)
                                {
                                    V6ControlFormHelper.SetCrystalReportPrinterOptions(NOPRINTER, V6ControlFormHelper.PrinterSettings, rpDoc3);
                                }
                                if (NOPRINTER)
                                {
                                    if (rpDoc3.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(printerName);
                                }
                                else if (!NOPRINTER) PrinterStatus.SetDefaultPrinter(printerName);
                                rpDoc3.PrintToPrinter(1, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0);
                                intDaGuiDenMayIn++;
                            }
                            catch (Exception ex)
                            {
                                this.ShowErrorMessage(GetType() + string.Format(V6Text.Text("PRINTXERROR"), 3) + ex.Message);
                            }
                        }
                        if (_soLienIn > 3)
                        {
                            try
                            {
                                if (V6ControlFormHelper.PrinterSettings != null)
                                {
                                    V6ControlFormHelper.SetCrystalReportPrinterOptions(NOPRINTER, V6ControlFormHelper.PrinterSettings, rpDoc4);
                                }
                                if (NOPRINTER)
                                {
                                    if (rpDoc4.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(printerName);
                                }
                                else if (!NOPRINTER) PrinterStatus.SetDefaultPrinter(printerName);
                                rpDoc4.PrintToPrinter(1, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0);
                                intDaGuiDenMayIn++;
                            }
                            catch (Exception ex)
                            {
                                this.ShowErrorMessage(GetType() + string.Format(V6Text.Text("PRINTXERROR"), 4) + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            if (V6ControlFormHelper.PrinterSettings != null)
                            {
                                V6ControlFormHelper.SetCrystalReportPrinterOptions(NOPRINTER, V6ControlFormHelper.PrinterSettings, rpDoc);
                            }

                            if (NOPRINTER)
                            {
                                if (rpDoc.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(printerName);
                            }
                            else if (!NOPRINTER) PrinterStatus.SetDefaultPrinter(printerName);
                            rpDoc.PrintToPrinter(_soLienIn*_printCopy, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0);
                            intDaGuiDenMayIn = _soLienIn;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(V6Text.Text("PRINTERRORSTOP") + ex.Message);
                        }
                    }
                    
                    //if (!xemMau)
                    //    timer1.Start();
                    if (intDaGuiDenMayIn == _soLienIn)
                    {
                        //xong = true;
                        CallPrintSuccessEvent();
                        if (Close_after_print)
                        {
                            if (!IsDisposed) Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(string.Format("{0} {1} {2}", GetType(), V6Text.Text("PRINTERROR"), ex.Message));
                }
            }
            else
            {
                //isInHoaDonClicked = false;
                btnIn.Enabled = true;
                this.ShowErrorMessage(string.Format("{0} {1}", GetType(), V6Text.Text("PRINTERAE")));
            }
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Bottom < panelCRview.Top)
            {
                //Phóng lớn dataGridView
                dataGridView1.BringToFront();
                //gridViewSummary1.BringToFront();
                dataGridView1.Height = Height - grbDieuKienLoc.Top -5;
                dataGridView1.Width = Width - 10;
                dataGridView1.Top = grbDieuKienLoc.Top;
                dataGridView1.Left = grbDieuKienLoc.Left;

                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            else
            {
                dataGridView1.Top = grbDieuKienLoc.Top;
                dataGridView1.Left = grbDieuKienLoc.Right + 5;
                dataGridView1.Height = panelCRview.Top - grbDieuKienLoc.Top - 5;
                dataGridView1.Width = panelCRview.Width;
                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            }
        }

        private void crystalReportViewer1_DoubleClick(object sender, EventArgs e)
        {
            if (panelCRview.Top > dataGridView1.Bottom)
            {
                panelCRview.BringToFront();
                panelCRview.Height = panelCRview.Bottom - grbDieuKienLoc.Top;
                panelCRview.Width = panelCRview.Right - grbDieuKienLoc.Left;
                panelCRview.Top = grbDieuKienLoc.Top;
                panelCRview.Left = grbDieuKienLoc.Left;
            }
            else
            {
                panelCRview.Left = grbDieuKienLoc.Right + 5;
                panelCRview.Top = dataGridView1.Bottom + 5;
                panelCRview.Height = Height - panelCRview.Top - 10;// panelCRview.Bottom - dataGridView1.Bottom - 5;
                panelCRview.Width = dataGridView1.Width;
            }
        }
        
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        protected override void ClearMyVars()
        {
            List<ReportDocument> list = new List<ReportDocument>() {_rpDoc10, _rpDoc20, _rpDoc30, _rpDoc40};
            foreach (ReportDocument rpDoc in list)
            {
                if (rpDoc != null)
                {
                    rpDoc.Close();
                    rpDoc.Dispose();
                }
            }
        }

        private string GetExportFileName()
        {
            string result = ChuyenMaTiengViet.ToUnSign(ReportTitle);
            if (EXTRA_INFOR.ContainsKey("EXPORT")) result = EXTRA_INFOR["EXPORT"];
            // Value
            if (_tbl2_AM != null && _tbl2_AM.Rows.Count > 0)
            {
                var am_data = _tbl2_AM.Rows[0].ToDataDictionary();
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

        public override void SetStatus2Text()
        {
            string id = "ST2PRINT" + Invoice.Mact;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = V6Text.Text("PRINT" + Invoice.Mact);
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }
        
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (FilterControl._hideFields != null && FilterControl._hideFields.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
            }
        }

        private void exportToExcelTemplateMenu_Click(object sender, EventArgs e)
        {
            var setting = new ExportExcelSetting();
            if (EXTRA_INFOR.ContainsKey("EXPORTEXCELFILTER"))
                setting.data = V6BusinessHelper.Filter(_tbl1_AD, EXTRA_INFOR["EXPORTEXCELFILTER"]);
            else setting.data = _tbl1_AD;
            setting.data2 = _tbl2_AM;
            setting.reportParameters = ReportDocumentParameters;
            setting.albcConfigData = _albcConfig.DATA;
            setting.xlsTemplateFile = ExcelTemplateFileFull;
            setting.saveFile = GetExportFileName();
            string exportFile = V6ControlFormHelper.ExportExcelTemplate_ChooseFile(this, setting);
            if (PrintMode == V6PrintMode.AutoExportT && !string.IsNullOrEmpty(exportFile))
            {
                btnHuy.PerformClick();
            }
        }

        private void chkCrossModify_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCrossModify.Checked)
            {
                numCrossAdd.Enabled = true;
                //btnRefresh.Enabled = true;
            }
            else
            {
                numCrossAdd.Enabled = false;
                //btnRefresh.Enabled = false;
                numCrossAdd.Value = 0;
            }
        }
        
        private string _oldDefaultPrinter;
        /// <summary>
        /// Cờ tràn dữ liệu cho mẫu hóa đơn khi tính toán vượt ROW_MAX.
        /// </summary>
        private bool data_overflow = false;
        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsInvoice && data_overflow)
                {
                    this.ShowWarningMessage(V6Text.OverFlow);
                    return;
                }

                // Kiểm tra pagecount > 1 trường hợp dùng ROW_MAX
                if (IsInvoice && ROW_MAX > 0)
                {
                    var pv = (PageView)crystalReportViewer1.Controls[0];
                    var pagecount = pv.GetLastPageNumber();
                    if (pagecount > 1)
                    {
                        this.ShowWarningMessage(V6Text.OverFlow);
                        return;
                    }
                }

                _soLienIn = (int) numSoLien.Value;
                
                if (string.IsNullOrEmpty(PrinterName))
                {
                    _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();

                    var printerst = V6ControlFormHelper.ChoosePrinter(this, DefaultPrinter);
                    if (printerst != null)
                    {
                        var selectedPrinter = printerst.PrinterName;
                        _printCopy = printerst.Copies;
                        V6ControlFormHelper.SetCrystalReportPrinterOptions(NOPRINTER, printerst, _rpDoc10, _rpDoc20, _rpDoc30, _rpDoc40);
                        V6BusinessHelper.WriteOldSelectPrinter(selectedPrinter);
                        //printting = true;
                        Print(selectedPrinter, _rpDoc10, _rpDoc20, _rpDoc30, _rpDoc40);
                        PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);

                        if (!string.IsNullOrEmpty(selectedPrinter) && selectedPrinter != DefaultPrinter)
                        {
                            print_one = true;
                            DefaultPrinter = selectedPrinter;
                        }
                    }
                    else
                    {
                        //printting = false;
                    }
                }
                else
                {
                    Print(PrinterName, _rpDoc10, _rpDoc20, _rpDoc30, _rpDoc40);
                }
                
            }
            catch (Exception ex)
            {
                PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                this.ShowErrorException(GetType() + ".Print_Click", ex);
            }
        }

        private void btnInLien_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsInvoice && data_overflow)
                {
                    this.ShowWarningMessage(V6Text.OverFlow);
                    return;
                }

                // Kiểm tra pagecount > 1 trường hợp dùng ROW_MAX
                if (IsInvoice && ROW_MAX > 0)
                {
                    var pv = (PageView)crystalReportViewer1.Controls[0];
                    var pagecount = pv.GetLastPageNumber();
                    if (pagecount > 1)
                    {
                        this.ShowWarningMessage(V6Text.OverFlow);
                        return;
                    }
                }

                _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();

                
                var printerst = V6ControlFormHelper.ChoosePrinter(this, DefaultPrinter);
                if (printerst != null)
                {
                    var selectedPrinter = printerst.PrinterName;
                    _printCopy = printerst.Copies;
                    
                    var rpDoc = _rpDoc10;
                    if (sender == inLien1Menu) rpDoc = _rpDoc10;
                    if (sender == inLien2Menu) rpDoc = _rpDoc20;
                    if (sender == inLien3Menu) rpDoc = _rpDoc30;
                    if (sender == inLien4Menu) rpDoc = _rpDoc40;

                    V6ControlFormHelper.SetCrystalReportPrinterOptions(NOPRINTER, printerst, rpDoc);
                    if (NOPRINTER)
                    {
                        if (_rpDoc10.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(selectedPrinter);
                    }
                    else if (!NOPRINTER) PrinterStatus.SetDefaultPrinter(selectedPrinter);
                    V6BusinessHelper.WriteOldSelectPrinter(selectedPrinter);
                    rpDoc.PrintToPrinter(_printCopy, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0);
                    if (NOPRINTER)
                    {
                        if (rpDoc.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                    }
                    else if (!NOPRINTER) PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);

                    if (!string.IsNullOrEmpty(selectedPrinter) && selectedPrinter != DefaultPrinter)
                    {
                        print_one = true;
                        DefaultPrinter = selectedPrinter;
                    }
                }
            }
            catch (Exception ex)
            {
                if (NOPRINTER)
                {
                    if (_rpDoc10.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                else if (!NOPRINTER) PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name + ((ToolStripMenuItem)sender).Name, ex);
            }
        }

        private bool _updateDataRow = false;
        private void cboMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady || string.IsNullOrEmpty(MA_NT)) return;
            if (_radioRunning || _updateDataRow) return;

            _albcConfig = new AlbcConfig(MauInSelectedRow.ToDataDictionary());
            GetSumCondition();

            txtReportTitle.Text = ReportTitle;
            numSoLien.Value = SelectedSoLien;
            if (ReloadData == "1")
            {
                MakeReport(V6PrintMode.DoNoThing, null, (int) numSoLien.Value);
            }
            else
            {
                _soLienIn = (int) numSoLien.Value;
                //FormatGridView();
                ViewReport();
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
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message, "InChungTuViewBase_Many");
            }
        }

        private void btnSuaTTMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, AlbcKeys, null);
                f2.AfterInitControl += f_AfterInitControl;
                f2.InitFormControl(this);
                SetStatus2Text();
                f2.ShowDialog(this);

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
            _updateDataRow = false;
        }

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadComboboxSource();
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

        private void btnLt_Click(object sender, EventArgs e)
        {
            var soLienIn = numSoLien.Value;
            if (soLienIn == 4)
            {
                if (crystalReportViewer1.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = false;
                    crystalReportViewer4.Visible = true;
                }
                else if (crystalReportViewer2.Visible)
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = false;
                    crystalReportViewer4.Visible = false;
                }
                else if (crystalReportViewer3.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = true;
                    crystalReportViewer3.Visible = false;
                    crystalReportViewer4.Visible = false;
                }
                else if (crystalReportViewer4.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = true;
                    crystalReportViewer4.Visible = false;
                }
                else
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = false;
                    crystalReportViewer4.Visible = false;
                }
            }
            else if (soLienIn == 3)
            {
                if (crystalReportViewer1.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = true;
                }
                else if (crystalReportViewer2.Visible)
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = false;
                }
                else if (crystalReportViewer3.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = true;
                    crystalReportViewer3.Visible = false;
                }
                else
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = false;
                }
                crystalReportViewer4.Visible = false;
            }
            else if (soLienIn == 2)
            {
                if (crystalReportViewer1.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = true;
                }
                else if (crystalReportViewer2.Visible)
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                }
                else
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                }
                crystalReportViewer3.Visible = false;
                crystalReportViewer4.Visible = false;
            }
            else if (soLienIn == 1)
            {
                crystalReportViewer1.Visible = true;
                crystalReportViewer2.Visible = false;
                crystalReportViewer3.Visible = false;
                crystalReportViewer4.Visible = false;
            }
        }

        private void btnLs_Click(object sender, EventArgs e)
        {
            var soLienIn = numSoLien.Value;
            if (soLienIn == 4)
            {
                if (crystalReportViewer1.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = true;
                    crystalReportViewer3.Visible = false;
                    crystalReportViewer4.Visible = false;
                }
                else if (crystalReportViewer2.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = true;
                    crystalReportViewer4.Visible = false;
                }
                else if (crystalReportViewer3.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = false;
                    crystalReportViewer4.Visible = true;
                }
                else// if (crystalReportViewer4.Visible)
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = false;
                    crystalReportViewer4.Visible = false;
                }
            }
            else if (soLienIn == 3)
            {
                if (crystalReportViewer1.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = true;
                    crystalReportViewer3.Visible = false;
                }
                else if (crystalReportViewer2.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = true;
                }
                else //if (crystalReportViewer3.Visible)
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                    crystalReportViewer3.Visible = false;
                }
                crystalReportViewer4.Visible = false;
            }
            else if (soLienIn == 2)
            {
                if (crystalReportViewer1.Visible)
                {
                    crystalReportViewer1.Visible = false;
                    crystalReportViewer2.Visible = true;
                }
                else if (crystalReportViewer2.Visible)
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                }
                else
                {
                    crystalReportViewer1.Visible = true;
                    crystalReportViewer2.Visible = false;
                }
                crystalReportViewer3.Visible = false;
                crystalReportViewer4.Visible = false;
            }
            else if (soLienIn == 1)
            {
                crystalReportViewer1.Visible = true;
                crystalReportViewer2.Visible = false;
                crystalReportViewer3.Visible = false;
                crystalReportViewer4.Visible = false;
            }
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            //btnIn.Focus();
        }

        private void exportToPdfMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_rpDoc10 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                string export_file = GetExportFileName();
                V6ControlFormHelper.ExportRptToPdf_As(this, _rpDoc10, export_file);
                if (MauTuIn == 1)
                {
                    if (_soLienIn >= 2 && _rpDoc20 != null)
                    {
                        V6ControlFormHelper.ExportRptToPdf_As(this, _rpDoc20, export_file + "_2");
                    }
                    if (_soLienIn >= 3 && _rpDoc30 != null)
                    {
                        V6ControlFormHelper.ExportRptToPdf_As(this, _rpDoc30, export_file + "_3");
                    }
                    if (_soLienIn >= 4 && _rpDoc40 != null)
                    {
                        V6ControlFormHelper.ExportRptToPdf_As(this, _rpDoc40, export_file + "_4");
                    }
                }
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

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
            if (_albcConfig != null && _albcConfig.HaveInfo)
            {
                V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                    _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
            }
        }

        bool flag_next_pre;
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                flag_next_pre = true;
                if (dataGridView_Many.CurrentRow.Index < dataGridView_Many.RowCount - 1)
                {
                    dataGridView_Many.CurrentCell = dataGridView_Many.Rows[dataGridView_Many.CurrentRow.Index + 1].Cells["SO_CT"];
                    MakeReport();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
            flag_next_pre = false;
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            try
            {
                flag_next_pre = true;
                if (dataGridView_Many.CurrentRow.Index > 0)
                {
                    dataGridView_Many.CurrentCell = dataGridView_Many.Rows[dataGridView_Many.CurrentRow.Index - 1].Cells["SO_CT"];
                    MakeReport();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
            flag_next_pre = false;
        }

        private int old_index = -1;
        private void dataGridView_Many_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsReady) return;
                if (dataGridView_Many.CurrentRow.Index != old_index)
                {
                    old_index = dataGridView_Many.CurrentRow.Index;
                    if (!flag_next_pre)
                    {
                        MakeReport();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
        }

        
    }

    
}
