using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using HaUtility.Helper;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter;
using V6ControlManager.FormManager.ReportManager.DXreport;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using PrintDialog = System.Windows.Forms.PrintDialog;
using PrinterStatus = V6Tools.PrinterStatus;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu
{
    public partial class InChungTuDX : V6FormControl
    {
        #region Biến toàn cục

        public decimal TTT { get; set; }
        public decimal TTT_NT { get; set; }
        public string MA_NT { get; set; }

        private XtraReport _repx10, _repx20, _repx30, _repx40;

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
        private DataTable _tbl_AD, _tbl2_AM, _tbl2, _tbl3;
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
            _extraInfor.AddRange(ObjectAndString.StringToStringDictionary("" + MauInSelectedRow["EXTRA_INFOR"]));
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

        public string ReportFileFullDX
        {
            get
            {
                var result = @"ReportsDX\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".repx";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"ReportsDX\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + ".repx";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFullDX_1
        {
            get
            {
                var result = @"ReportsDX\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_1.repx";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"ReportsDX\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_1.repx";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFullDX_2
        {
            get
            {
                var result = @"ReportsDX\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_2.repx";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"ReportsDX\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_2.repx";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFullDX_3
        {
            get
            {
                var result = @"ReportsDX\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_3.repx";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"ReportsDX\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_3.repx";//_reportFile gốc
                }
                return result;
            }
        }

        public string ReportFileFullDX_4
        {
            get
            {
                var result = @"ReportsDX\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_4.repx";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"ReportsDX\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_4.repx";//_reportFile gốc
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
                var result = @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".xls";
                if (!File.Exists(result))
                {
                    result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + ".xls";
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
                var result = @"Reports\"
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
                var result = @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "HTKK.xls";
                if (!File.Exists(result))
                {
                    result = @"Reports\"
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
                var result = @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "ONLINE.xls";
                if (!File.Exists(result))
                {
                    result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "ONLINE.xls";
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

        public delegate void PrintSuccessDelegate(InChungTuDX sender, string stt_rec, AlbcConfig albcConfig);
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
        
        public InChungTuDX(V6InvoiceBase invoice,
            string program, string reportProcedure,
            string reportFile, string reportTitle, string reportTitle2,
            string reportFileF5, string reportTitleF5, string reportTitle2F5, string report_stt_rec)
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

            Report_Stt_rec = report_stt_rec;

            V6ControlFormHelper.AddLastAction(GetType() + " " + invoice.Mact + " " + program);
            
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
                InvokeFormEvent(FormDynamicEvent.INIT);
                Disposed += InChungTuDX_Disposed;
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
                //documentViewer1.ShowPrintButton = false; !!!!
                //documentViewer1.ShowExportButton = false; !!!!
                contextMenuStrip1.Items.Remove(exportToPdfMenu);
            }
            if (!V6Login.UserRight.AllowView(ItemID, Invoice.CodeMact))
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

                        if (!key3.Contains("E")) btnSuaMau.Enabled = false;
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
                this.WriteExLog(GetType() + ".Init2 " + ReportFileFullDX, ex);
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

        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            MyInit2();
            MakeReport(PrintMode, PrinterName, (int)numSoLien.Value, _printCopy);
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
                if (ActiveControl == documentViewer1
                    || ActiveControl == documentViewer2
                    || ActiveControl == documentViewer3
                    || ActiveControl == documentViewer4
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
		
        ///// <summary>
        ///// Tính toán đường chéo sẽ hiện trên report
        ///// </summary>
        ///// <param name="t">Bảng dữ liệu</param>
        ///// <param name="field">Trường tính toán ký tự</param>
        ///// <param name="lengOfName"></param>
        ///// <param name="lineHeight"></param>
        ///// <param name="fontSize"></param>
        ///// <returns></returns>
        //private int CalculateCrossLine(DataTable t, string field, int lengOfName, float lineHeight, float fontSize)
        //{
        //    float lineDroppedHeight = lineHeight + lineHeight/2;//Font size 10. Chiều cao tương đối của ô text khi rớt xuống 1 dòng
        //    float dropLineHeightBase = lineHeight * 0.7f;//Font size 10. Phần cao thêm khi rớt tiếp dòng thứ 2

        //    lineDroppedHeight += ((fontSize - 10) * lineHeight/10);
        //    dropLineHeightBase += ((fontSize - 10) * lineHeight/10);

        //    float dropLineHeight1 = lineDroppedHeight - lineHeight;//Tính phần cao thêm khi rớt dòng thứ 1.
        //    //Mỗi dòng drop sẽ nhân với DropLineHeight
        //    //var dropCount = 0;
        //    float dropHeight = 0;
        //    foreach (DataRow r in t.Rows)
        //    {
        //        try
        //        {
        //            var len = r[field].ToString().Trim().Length;
        //            if (len > 1) len--;
                    
        //            var dropExtra = len / lengOfName;
        //            if (dropExtra > 0)
        //            {
        //                dropHeight += dropLineHeight1;
        //                if (dropExtra > 1) dropHeight += dropLineHeightBase*(dropExtra-1);
        //            }
        //        }
        //        catch
        //        {
        //            // ignored
        //        }
        //    }
            

        //    //var dropHeight = dropLineHeight * dropCount;
        //    //số 2 ở đây là vì mỗi dòng có 2 cross line
        //    int dropCount = (int)(dropHeight/(lineHeight/2));
        //    if (dropCount == 0 && dropHeight > 0) dropCount = 1;
        //    return t.Rows.Count * 2 + dropCount;
        //}
        
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

        private void SetCrossLineAll(XtraReport rpDoc, XtraReport rpDoc2, XtraReport rpDoc3, XtraReport rpDoc4)
        {
            try
            {
                if (!IsInvoice) return;

                SetCrossLineRepx_BackGround(rpDoc);

                if (MauTuIn == 1 && _soLienIn >= 2 && rpDoc2 != null)
                {
                    SetCrossLineRepx_BackGround(rpDoc2);
                }
                if (MauTuIn == 1 && _soLienIn >= 3 && rpDoc3 != null)
                {
                    SetCrossLineRepx_BackGround(rpDoc3);
                }
                if (MauTuIn == 1 && _soLienIn >= 4 && rpDoc4 != null)
                {
                    SetCrossLineRepx_BackGround(rpDoc4);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetCrossLine", ex);
            }
        }

        //private Dictionary<string, ReportObject> GetRprObjects(XtraReport rpt)
        //{
        //    var result = new Dictionary<string, ReportObject>();
        //    foreach (ReportObject o in rpt.ReportDefinition.ReportObjects)
        //    {
        //        result[o.Name] = o;
        //    }
        //    return result;
        //}

        private void SetCrossLineRepx_BackGround(XtraReport repx)
        {
            int flag = 0;
            
            try
            {
                if (!IsInvoice) return;

                flag = 1;

                // Lấy background image
                var image = repx.Watermark.Image;
                if (image == null)
                {
                    image = new Bitmap(repx.PageWidth, repx.PageHeight);
                    repx.Watermark.Image = image;
                    repx.Watermark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
                }
                // tính toán và vẽ lên image
                float p_LeftX, p_LeftY, p_CenterX, p_CenterY, p_RightX, p_RightY;
                bool draw = GetCrossLinePoints(repx, out p_LeftX, out p_LeftY, out p_CenterX, out p_CenterY, out p_RightX, out p_RightY);
                if (repx.Pages.Count > 1)
                {
                    data_overflow = true;
                }
                else
                {
                    data_overflow = false;
                }

                if (!draw) return;

                Graphics graphics = Graphics.FromImage(image);
                float rate = (float)image.Height / (repx.PageHeight);
                Point p_Begin = new Point((int)(p_LeftX * rate), (int)(p_LeftY * rate)+2);
                Point p_Center = new Point((int)(p_CenterX * rate), (int)(p_CenterY * rate)+2);
                Point p_End = new Point((int)(p_RightX * rate), (int)(p_RightY * rate));
                
                if(p_CenterX > 0)
                {
                    // Vẽ đường ngang
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    HDrawing.DrawLine(graphics, p_Begin, p_Center);
                    // Vẽ đường chéo
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    HDrawing.DrawLine(graphics, p_Center, p_End);
                }
                else
                {
                    // Vẽ đường chéo
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    HDrawing.DrawLine(graphics, p_Begin, p_End);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetCrossLineRpt " + Report_Stt_rec, ex);
            }
        }

        /// <summary>
        /// Lấy các điểm vẽ đường chéo lên report. Trả về bool có phải vẽ đường chéo hay không?
        /// </summary>
        /// <param name="repx"></param>
        /// <param name="p_BeginX"></param>
        /// <param name="p_BeginY"></param>
        /// <param name="p_CenterX"></param>
        /// <param name="p_CenterY"></param>
        /// <param name="p_RightX"></param>
        /// <param name="p_RightY"></param>
        private bool GetCrossLinePoints(XtraReport repx, out float p_BeginX, out float p_BeginY, out float p_CenterX, out float p_CenterY, out float p_RightX, out float p_RightY)
        {
            bool p_FoundEndLabel = false;
            p_BeginX = 0;
            p_BeginY = 0;
            p_CenterX = 0;
            p_CenterY = 0;
            p_RightX = 0;
            p_RightY = 0;

            try
            {
                repx.CreateDocument();
                
                p_RightX = (repx.PageWidth - repx.Margins.Right);
                p_RightY = (repx.PageHeight - repx.Margins.Bottom - repx.Bands[BandKind.PageFooter].HeightF);   // Cuối chi tiết.

                IEnumerator en = repx.Pages.GetEnumerator();
                
                PSPage page;
                while (en.MoveNext())
                {
                    page = (PSPage)en.Current;
                    foreach (Brick br in page.Bricks)
                    {

                        if (br.GetType() == typeof(LabelBrick))
                        {
                            LabelBrick lbr = (LabelBrick)br;
                            XRLabel label = lbr.BrickOwner as XRLabel;

                            if (label.Name.ToUpper() == "LBLTONGTIEN")
                            {
                                //p_RightY += label.TopF;   // Dòng này sẽ làm đường chéo tiến sát đỉnh ô tổng tiền.
                                p_FoundEndLabel = true;
                                goto END;
                                break;
                            }

                            if (label.Name.ToUpper() == "DETAILLEFT")
                            {
                                p_BeginX = repx.Margins.Left + lbr.Rect.Left / 3;
                                p_BeginY = repx.Margins.Top + lbr.Rect.Bottom / 3;
                            }
                            if (label.Name.ToUpper() == "DETAILCENTER")
                            {
                                p_CenterX = repx.Margins.Left + lbr.Rect.Left / 3;
                                p_CenterY = repx.Margins.Top + lbr.Rect.Bottom / 3;
                            }
                            if (label.Name.ToUpper() == "DETAILRIGHT")
                            {
                                p_RightX = repx.Margins.Left + lbr.Rect.Right / 3;
                            }

                            if (p_FoundEndLabel)
                                break;

                        }

                    }
                    if (p_FoundEndLabel)
                        break;
                }

                //die Höhe muß durch 3 geteilt werden, da die Position des Bricks, die wieter oben b
                //berechnet wird in DocumentUnits.Document = 1/300 inch gemessen wird und PageHeight in
                //ReportUnits = 1/100 inch
            END: ;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetCrossLinePoints", ex);
            }

            return p_FoundEndLabel;
        }

        private IDictionary<string, object> ReportDocumentParameters; 
        /// <summary>
        /// Lưu ý: chạy sau khi add dataSource để tránh lỗi nhập parameter value
        /// </summary>
        private void SetAllReportParams(XtraReport repx1, XtraReport repx2, XtraReport repx3, XtraReport repx4)
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
                {"IsTemplate", 0},
                {"IsCopy", false},
                {"ViewInfo", MauTuIn == 1},
                {
                    "Info",
                    "In bởi  Phần mềm V6 Accounting2016.NET - Cty phần mềm V6 (www.v6corp.com) - MST: 0303180249 - ĐT: 028.62570563"
                },
                {"ViewCrossLine", true},
                {"SoTienVietBangChu", V6BusinessHelper.MoneyToWords(TTT, LAN, V6Options.M_MA_NT0)},
                {"SoTienVietBangChuNT", V6BusinessHelper.MoneyToWords(TTT_NT, LAN, MA_NT)},
                {"Title", txtReportTitle.Text.Trim()},
                {"M_TEN_CTY", V6Soft.V6SoftValue["M_TEN_CTY"].ToUpper()},
                {"M_TEN_TCTY", V6Soft.V6SoftValue["M_TEN_TCTY"].ToUpper()},
                {"M_DIA_CHI", V6Soft.V6SoftValue["M_DIA_CHI"]},
                {"M_TEN_CTY2", V6Soft.V6SoftValue["M_TEN_CTY2"].ToUpper()},
                {"M_TEN_TCTY2", V6Soft.V6SoftValue["M_TEN_TCTY2"].ToUpper()},
                {"M_DIA_CHI2", V6Soft.V6SoftValue["M_DIA_CHI2"]},
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
                {"M_RTFONT", V6Options.GetValue("M_RTFONT")},
                {"M_RSFONT", V6Options.GetValue("M_RSFONT")},
                {"M_R_FONTSIZE", V6Options.GetValue("M_R_FONTSIZE")}
            };

            V6Login.SetCompanyInfo(ReportDocumentParameters);

            if (FilterControl.RptExtraParameters != null)
            {
                ReportDocumentParameters.AddRange(FilterControl.RptExtraParameters, true);
            }

            var extraParametersD = FilterControl.GetRptParametersD(Extra_para, LAN);
            if (extraParametersD != null)
            {
                ReportDocumentParameters.AddRange(extraParametersD, true);
            }

            string errors = "";
            foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
            {
                try
                {
                    if (repx1.Parameters[item.Key] != null)
                    {
                        repx1.Parameters[item.Key].Value = item.Value;
                    }
                    else
                    {
                        // missing parameters warning!
                        //errors += "\n" + item.Key + ":\t " + V6Text.NotExist;
                        // Auto create Paramter for easy edit.
                        repx1.Parameters.Add(new Parameter()
                        {
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
                    errors += "repx1 " + item.Key + ": " + ex.Message + "\n";
                }
            }
            
            if (MauTuIn == 1 && _soLienIn >= 2 && repx2 != null)
            {
                foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
                {
                    try
                    {
                        if (repx2.Parameters[item.Key] != null)
                        {
                            repx2.Parameters[item.Key].Value = item.Value;
                        }
                        else
                        {
                            // missing parameters warning!
                            //errors += "\n" + item.Key + ":\t " + V6Text.NotExist;
                            // Auto create Paramter for easy edit.
                            repx2.Parameters.Add(new Parameter()
                            {
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
                        errors += "repx2 " + item.Key + ": " + ex.Message + "\n";
                    }
                }
            }

            if (MauTuIn == 1 && _soLienIn >= 3 && repx3 != null)
            {
                foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
                {
                    try
                    {
                        if (repx3.Parameters[item.Key] != null)
                        {
                            repx3.Parameters[item.Key].Value = item.Value;
                        }
                        else
                        {
                            // missing parameters warning!
                            //errors += "\n" + item.Key + ":\t " + V6Text.NotExist;
                            // Auto create Paramter for easy edit.
                            repx3.Parameters.Add(new Parameter()
                            {
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
                        errors += "repx3 " + item.Key + ": " + ex.Message + "\n";
                    }
                }
            }

            if (MauTuIn == 1 && _soLienIn >= 4 && repx4 != null)
            {
                foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
                {
                    try
                    {
                        if (repx4.Parameters[item.Key] != null)
                        {
                            repx4.Parameters[item.Key].Value = item.Value;
                        }
                        else
                        {
                            // missing parameters warning!
                            //errors += "\n" + item.Key + ":\t " + V6Text.NotExist;
                            // Auto create Paramter for easy edit.
                            repx4.Parameters.Add(new Parameter()
                            {
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
                        errors += "repx4 " + item.Key + ": " + ex.Message + "\n";
                    }
                }
            }

            if (errors != "")
            {
                V6ControlFormHelper.AddLastError(GetType() + ".SetAllReportParams\r\nFile: "
                    + ReportFileFullDX + "\r\nError: " + errors);
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
                    _tbl_AD = _ds.Tables[0];
                    _tbl_AD.TableName = "DataTable1";
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
                this.ShowErrorMessage(GetType() + ".LoadData Error\n" + ex.Message, "InChungTuDX");
                _tbl_AD = null;
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
                    dataGridView1.DataSource = _tbl_AD;
                    dataGridView1.DataSource = _tbl2_AM;
                    FormatGridView();
                    
                    ViewReport();
                    if (PrintMode == V6PrintMode.AutoPrint)
                    {
                        Print(PrinterName, _repx10, _repx20, _repx30, _repx40);
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
                this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message, "InChungTuDX");
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
                    dataGridView1.DataSource = _tbl_AD;
                    
                    FormatGridView();
                    ViewReport();
                    if (PrintMode == V6PrintMode.AutoPrint)
                    {
                        Print(PrinterName, _repx10, _repx20, _repx30, _repx40);
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
                //VPA_GetFormatGridView]@Codeform VARCHAR(50),@Type VARCHAR(20)
                string FIELDV, OPERV, BOLD_YN, COLOR_YN, COLORV;
                object VALUEV;
                V6BusinessHelper.GetFormatGridView(_program, "REPORT", out FIELDV, out OPERV, out VALUEV, out BOLD_YN,
                    out COLOR_YN, out COLORV);
                //Color.MediumAquamarine
                V6ControlFormHelper.FormatGridView(dataGridView1, FIELDV, OPERV, VALUEV, BOLD_YN == "1", COLOR_YN == "1",
                    ObjectAndString.StringToColor(COLORV));


                //Header
                var fieldList = (from DataColumn column in _tbl_AD.Columns select column.ColumnName).ToList();

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

        void InChungTuDX_Disposed(object sender, EventArgs e)
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
            if (_tbl_AD == null)
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
                        V6Tools.V6Export.ExportData.ToExcel(_tbl_AD, save.FileName, Name, true);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorMessage(GetType() + ".ExportFail: " + ex.Message, "InChungTuDX");
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
                this.ShowErrorMessage(GetType() + ".ExportFail\n" + ex.Message, "InChungTuDX");
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
            if (_tbl_AD == null)
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
                this.ShowErrorMessage(GetType() + ".PrintGrid\n" + ex.Message, "InChungTuDX");
            }
        }


        void ViewReport()
        {
            if (_ds == null) return;
            
            CleanUp();
            XtraReport repx1 = null, repx2 = null, repx3 = null, repx4 = null;
            //documentViewer1.DisplayToolbar = false;
            //documentViewer2.DisplayToolbar = false;
            //documentViewer3.DisplayToolbar = false;
            //documentViewer4.DisplayToolbar = false;
            
            if (MauTuIn == 1)
            {
                //Hoa don 3 lien 123
                repx1 = null;
                repx2 = null;
                repx3 = null;
                repx4 = null;

                try
                {
                    repx1 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX_1);
                    repx1.PrintingSystem.ShowMarginsWarning = false;
                    repx1.DataSource = _ds;
                }
                catch (Exception e1)
                {
                    this.ShowErrorMessage(GetType() + ".ViewReport rpDoc.Load: " + e1.Message);
                }

                if (_soLienIn >= 2)
                {
                    try
                    {
                        repx2 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX_2);
                        repx2.PrintingSystem.ShowMarginsWarning = false;
                        repx2.DataSource = _ds.Copy();
                    }
                    catch (Exception e2)
                    {
                        repx2 = null;
                        this.ShowErrorMessage(GetType() + ".ViewReport rpDoc2.Load: " + e2.Message);
                    }
                }
                if (_soLienIn >= 3)
                {
                    try
                    {
                        repx3 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX_3);
                        repx3.PrintingSystem.ShowMarginsWarning = false;
                        repx3.DataSource = _ds.Copy();
                    }
                    catch (Exception e3)
                    {
                        repx3 = null;
                        this.ShowErrorMessage(GetType() + ".ViewReport rpDoc3.Load: " + e3.Message);
                    }
                }

                if (_soLienIn >= 4)
                {
                    try
                    {
                        repx4 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX_4);
                        repx4.PrintingSystem.ShowMarginsWarning = false;
                        repx4.DataSource = _ds.Copy();
                    }
                    catch (Exception e4)
                    {
                        repx4 = null;
                        this.ShowErrorMessage(GetType() + ".ViewReport rpDoc4.Load: " + e4.Message);
                    }
                }


                SetAllReportParams(repx1, repx2, repx3, repx4);
                SetCrossLineAll(repx1, repx2, repx3, repx4);
                
                if (EXTRA_INFOR.ContainsKey("RPTHIDE"))
                {
                    var names = ObjectAndString.SplitString(EXTRA_INFOR["RPTHIDE"]);
                    RPTHIDE(repx1, names);
                    RPTHIDE(repx2, names);
                    RPTHIDE(repx3, names);
                    RPTHIDE(repx4, names);
                }

                documentViewer1.Zoom = DXreportManager.GetExtraReportZoom(documentViewer1, repx1, Invoice.ExtraInfo_PrintVCzoom);
                documentViewer1.DocumentSource = repx1;
                
                _repx10 = repx1;
                _repx10.CreateDocument();
                if (_soLienIn >= 2 && repx2 != null)
                {
                    documentViewer2.DocumentSource = repx2;
                    documentViewer2.Zoom = Invoice.ExtraInfo_PrintVCzoom_DX;
                    _repx20 = repx2;
                    _repx20.CreateDocument();
                }
                if (_soLienIn >= 3 && repx3 != null)
                {
                    documentViewer3.DocumentSource = repx3;
                    documentViewer3.Zoom = Invoice.ExtraInfo_PrintVCzoom_DX;
                    _repx30 = repx3;
                    _repx30.CreateDocument();
                }
                if (_soLienIn >= 4 && repx4 != null)
                {
                    documentViewer4.DocumentSource = repx4;
                    documentViewer4.Zoom = Invoice.ExtraInfo_PrintVCzoom_DX;
                    _repx40 = repx4;
                    _repx40.CreateDocument();
                }

                documentViewer1.Show();
                documentViewer1.Visible = true;
                documentViewer2.Visible = false;
                documentViewer3.Visible = false;
                documentViewer4.Visible = false;
                
            }
            else
            {
                if (!IsInvoice)
                {
                    FixReportViewerToolbarButton(false);
                }

                repx1 = null;
                repx1 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX);
                repx1.PrintingSystem.ShowMarginsWarning = false;
                repx1.DataSource = _ds;
                
                
                SetAllReportParams(repx1, repx2, repx3, repx4);
                SetCrossLineAll(repx1, repx2, repx3, repx4);
                if (EXTRA_INFOR.ContainsKey("RPTHIDE"))
                {
                    var names = ObjectAndString.SplitString(EXTRA_INFOR["RPTHIDE"]);
                    RPTHIDE(repx1, names);
                    RPTHIDE(repx2, names);
                    RPTHIDE(repx3, names);
                    RPTHIDE(repx4, names);
                }
                documentViewer1.Zoom = DXreportManager.GetExtraReportZoom(documentViewer1, repx1, Invoice.ExtraInfo_PrintVCzoom);
                documentViewer1.DocumentSource = repx1;
                
                if (_repx10 != null) _repx10.Dispose();
                _repx10 = repx1;
                repx1.CreateDocument();
                //documentViewer1.Show();
                documentViewer1.Visible = true;
                documentViewer2.Visible = false;
                documentViewer3.Visible = false;
                documentViewer4.Visible = false;
            }
            //btnIn.Focus();
        }

        private void RPTHIDE(XtraReport rpDoc, IList<string> names)
        {
            try
            {
                if (rpDoc == null) return;
                if (names == null) return;

                var all_objects = new SortedDictionary<string, XRControl>();
                foreach (Band band in rpDoc.Bands)
                {
                    foreach (SubBand subBand in band.SubBands)
                    {
                        foreach (var control in subBand.Controls)
                        {
                            all_objects[(control as XRControl).Name] = control as XRControl;
                        }
                    }

                    foreach (var control in band.Controls)
                    {
                        all_objects[(control as XRControl).Name] = control as XRControl;
                    }
                }  

                foreach (string name in names)
                {
                    string NAME = name.ToUpper();
                    if (all_objects.ContainsKey(NAME))
                    {
                        all_objects[NAME].Visible = false;
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
                Lock(documentViewer1, documentViewer2, documentViewer3, documentViewer4);
            }
            else
            {
                Open(documentViewer1, documentViewer2, documentViewer3, documentViewer4);
            }
        }

        private void Lock(params DocumentViewer[] crViewers)
        {
            foreach (DocumentViewer crViewer in crViewers)
            {
                if (crViewer != null)
                {
                    //crViewer.ShowExportButton = false; !!!!
                    //crViewer.ShowPrintButton = false; !!!!
                }
            }
        }

        private void Open(params DocumentViewer[] crViewers)
        {
            foreach (DocumentViewer crViewer in crViewers)
            {
                if (crViewer != null)
                {
                    //crViewer.ShowExportButton = true; !!!!
                    //crViewer.ShowPrintButton = true; !!!!
                }
            }
        }

        private void Print(string printerName, XtraReport rpDoc, XtraReport rpDoc2, XtraReport rpDoc3, XtraReport rpDoc4)
        {
            int intDaGuiDenMayIn = 0;
            if (_printCopy < 1) _printCopy = 1;
            bool printerOnline = PrinterStatus.CheckPrinterOnline(printerName);
            //var setPrinterOk = PrinterStatus.SetDefaultPrinter(printerName);
            //var printerError = string.Compare("Error", PrinterStatus.getDefaultPrinterProperties("Status"), StringComparison.OrdinalIgnoreCase) == 0;
			
			if (printerOnline)
            {
                try
                {
                    if (MauTuIn == 1)
                    {
                        try
                        {
                            if (IsInvoice)// In 1 trang
                            {
                                //rpDoc.PrintToPrinter(1, false, 1, 1);
                                var printTool = new ReportPrintTool(rpDoc); // In 1 trang ??
                                printTool.PrintingSystem.ShowMarginsWarning = false;
                                printTool.PrinterSettings.Copies = 1;
                                printTool.PrinterSettings.FromPage = 1;
                                printTool.PrinterSettings.ToPage = 1;
                                printTool.Print(printerName);
                            }
                            else
                            {
                                //rpDoc.PrintToPrinter(1, false, 0, 0);
                                var printTool = new ReportPrintTool(rpDoc);
                                printTool.PrintingSystem.ShowMarginsWarning = false;
                                printTool.PrinterSettings.Copies = 1;
                                printTool.Print(printerName);
                            }
                            intDaGuiDenMayIn++;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format(V6Text.Text("PRINTXERRORSTOP"), 1) + ex.Message);
                        }

                        if (_soLienIn > 1)
                            try
                            {
                                if (IsInvoice)
                                {
                                    //rpDoc2.PrintToPrinter(1, false, 1, 1);
                                    var printTool = new ReportPrintTool(rpDoc2);
                                    printTool.PrintingSystem.ShowMarginsWarning = false;
                                    printTool.PrinterSettings.Copies = 1;
                                    printTool.PrinterSettings.FromPage = 1;
                                    printTool.PrinterSettings.ToPage = 1;
                                    printTool.Print(printerName);
                                }
                                else
                                {
                                    //rpDoc2.PrintToPrinter(1, false, 0, 0);
                                    var printTool = new ReportPrintTool(rpDoc2);
                                    printTool.PrintingSystem.ShowMarginsWarning = false;
                                    printTool.PrinterSettings.Copies = 1;
                                    printTool.Print(printerName);
                                }
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
                                if (IsInvoice)
                                {
                                    //rpDoc3.PrintToPrinter(1, false, 1, 1);
                                    var printTool = new ReportPrintTool(rpDoc3);
                                    printTool.PrintingSystem.ShowMarginsWarning = false;
                                    printTool.PrinterSettings.Copies = 1;
                                    printTool.PrinterSettings.FromPage = 1;
                                    printTool.PrinterSettings.ToPage = 1;
                                    printTool.Print(printerName);
                                }
                                else
                                {
                                    //rpDoc3.PrintToPrinter(1, false, 0, 0);
                                    var printTool = new ReportPrintTool(rpDoc3);
                                    printTool.PrintingSystem.ShowMarginsWarning = false;
                                    printTool.PrinterSettings.Copies = 1;
                                    printTool.Print(printerName);
                                }
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
                                if (IsInvoice)
                                {
                                    //rpDoc4.PrintToPrinter(1, false, 1, 1);
                                    var printTool = new ReportPrintTool(rpDoc4);
                                    printTool.PrintingSystem.ShowMarginsWarning = false;
                                    printTool.PrinterSettings.Copies = 1;
                                    printTool.PrinterSettings.FromPage = 1;
                                    printTool.PrinterSettings.ToPage = 1;
                                    printTool.Print(printerName);
                                }
                                else
                                {
                                    //rpDoc4.PrintToPrinter(1, false, 0, 0);
                                    var printTool = new ReportPrintTool(rpDoc4);
                                    printTool.PrintingSystem.ShowMarginsWarning = false;
                                    printTool.PrinterSettings.Copies = 1;
                                    printTool.Print(printerName);
                                }
                                //if (rpDoc4.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(printerName);
                                //rpDoc4.PrintToPrinter(1, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0);
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
                            if (IsInvoice) // Print page 1 only.
                            {
                                //rpDoc.PrintToPrinter(_soLienIn, false, 1, 1);
                                var printTool = new ReportPrintTool(rpDoc);
                                printTool.PrintingSystem.ShowMarginsWarning = false;
                                printTool.PrinterSettings.Copies = (short)_soLienIn;
                                printTool.PrinterSettings.FromPage = 1;
                                printTool.PrinterSettings.ToPage = 1;
                                printTool.Print(printerName);
                            }
                            else
                            {
                                //rpDoc.PrintToPrinter(_soLienIn*_printCopy, false, 0, 0);
                                var printTool = new ReportPrintTool(rpDoc);
                                printTool.PrintingSystem.ShowMarginsWarning = false;
                                printTool.PrinterSettings.Copies = (short)_soLienIn;
                                printTool.Print(printerName);
                            }
                            //if (rpDoc.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(printerName);
                            //rpDoc.PrintToPrinter(_soLienIn*_printCopy, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0);
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

        private void documentViewer1_DoubleClick(object sender, EventArgs e)
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
            List<XtraReport> list = new List<XtraReport>() { _repx10, _repx20, _repx30, _repx40 };
            foreach (XtraReport rpDoc in list)
            {
                if (rpDoc != null)
                {
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
            string exportFile = V6ControlFormHelper.ExportExcelTemplate_ChooseFile(this, _tbl_AD, _tbl2_AM,
                ReportDocumentParameters, MAU, LAN, ReportFile, ExcelTemplateFileFull, GetExportFileName());
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
                    var pagecount = documentViewer1.Document.Pages.Count;
                    if (pagecount > 1)
                    {
                        this.ShowWarningMessage(V6Text.OverFlow);
                        return;
                    }
                }

                _soLienIn = (int) numSoLien.Value;
                //if (!IsInvoice)
                //{
                //    documentViewer1.PrintReport();
                //    return;
                //}

                var dfp = DefaultPrinter;
                if (string.IsNullOrEmpty(PrinterName))
                {
                    _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();
                    //ReportDocument.PrintingSystem.ShowMarginsWarning = false;
                    PrintDialog p = new PrintDialog();
                    p.PrinterSettings.PrinterName = dfp;
                    p.AllowCurrentPage = false;
                    p.AllowPrintToFile = false;
                    p.AllowSelection = false;
                    p.AllowSomePages = false;
                    p.PrintToFile = false;
                    p.UseEXDialog = true; //Fix win7

                    DialogResult dr = p.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        var selectedPrinter = p.PrinterSettings.PrinterName;
                        _printCopy = p.PrinterSettings.Copies;

                        V6BusinessHelper.WriteOldSelectPrinter(selectedPrinter);
                        //printting = true;
                        Print(selectedPrinter, _repx10, _repx20, _repx30, _repx40);
                        PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);

                        if (!string.IsNullOrEmpty(selectedPrinter) && selectedPrinter != dfp)
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
                    Print(PrinterName, _repx10, _repx20, _repx30, _repx40);
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
                    var pv = (PageView)documentViewer1.Controls[0];
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
                    
                    var rpDoc = _repx10;
                    if (sender == inLien1Menu) rpDoc = _repx10;
                    if (sender == inLien2Menu) rpDoc = _repx20;
                    if (sender == inLien3Menu) rpDoc = _repx30;
                    if (sender == inLien4Menu) rpDoc = _repx40;
                    
                    //V6ControlFormHelper.SetCrystalReportPrinterOptions(printerst, rpDoc); !!!!
                    //if (_repx10.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(selectedPrinter); !!!!
                    V6BusinessHelper.WriteOldSelectPrinter(selectedPrinter);
                    //rpDoc.PrintToPrinter(_printCopy, false, IsInvoice ? 1 : 0, IsInvoice ? 1 : 0); !!!!
                    //if (rpDoc.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter); !!!!

                    if (!string.IsNullOrEmpty(selectedPrinter) && selectedPrinter != DefaultPrinter)
                    {
                        print_one = true;
                        DefaultPrinter = selectedPrinter;
                    }
                }
            }
            catch (Exception ex)
            {
                //if (_repx10.PrintOptions.NoPrinter) PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter); !!!!
                
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
            try
            {
                if (_ds == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                if (MauTuIn == 1)
                {
                    Dictionary<string, XtraReport> file_repx = new Dictionary<string, XtraReport>();
                    XtraReport x1 = null, x2 = null, x3 = null, x4 = null;
                    x1 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX_1);
                    if (x1 != null)
                    {
                        x1.DataSource = _ds.Copy();
                        file_repx.Add(ReportFileFullDX_1, x1);
                    }
                    if (_soLienIn >= 2)
                    {
                        x2 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX_2);
                        if (x2 != null)
                        {
                            x2.DataSource = _ds.Copy();
                            file_repx.Add(ReportFileFullDX_2, x2);
                        }
                    }
                    if (_soLienIn >= 3)
                    {
                        x3 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX_3);
                        if (x3 != null)
                        {
                            x3.DataSource = _ds.Copy();
                            file_repx.Add(ReportFileFullDX_3, x3);
                        }
                    }
                    if (_soLienIn >= 4)
                    {
                        x4 = DXreportManager.LoadV6XtraReportFromFile(ReportFileFullDX_4);
                        if (x4 != null)
                        {
                            x4.DataSource = _ds.Copy();
                            file_repx.Add(ReportFileFullDX_4, x4);
                        }
                    }

                    SetAllReportParams(x1, x2, x3, x4);
                    XtraEditorForm1 form1 = new XtraEditorForm1(file_repx);
                    form1.Show(this);
                    SetStatus2Text();
                }
                else
                {
                    DXreportManager.EditRepx(ReportFileFullDX, _ds, ReportDocumentParameters, this);
                }
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message);
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

        private void btnLt_Click(object sender, EventArgs e)
        {
            var soLienIn = numSoLien.Value;
            if (soLienIn == 4)
            {
                if (documentViewer1.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = false;
                    documentViewer4.Visible = true;
                }
                else if (documentViewer2.Visible)
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = false;
                    documentViewer4.Visible = false;
                }
                else if (documentViewer3.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = true;
                    documentViewer3.Visible = false;
                    documentViewer4.Visible = false;
                }
                else if (documentViewer4.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = true;
                    documentViewer4.Visible = false;
                }
                else
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = false;
                    documentViewer4.Visible = false;
                }
            }
            else if (soLienIn == 3)
            {
                if (documentViewer1.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = true;
                }
                else if (documentViewer2.Visible)
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = false;
                }
                else if (documentViewer3.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = true;
                    documentViewer3.Visible = false;
                }
                else
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = false;
                }
                documentViewer4.Visible = false;
            }
            else if (soLienIn == 2)
            {
                if (documentViewer1.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = true;
                }
                else if (documentViewer2.Visible)
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                }
                else
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                }
                documentViewer3.Visible = false;
                documentViewer4.Visible = false;
            }
            else if (soLienIn == 1)
            {
                documentViewer1.Visible = true;
                documentViewer2.Visible = false;
                documentViewer3.Visible = false;
                documentViewer4.Visible = false;
            }
        }

        private void btnLs_Click(object sender, EventArgs e)
        {
            var soLienIn = numSoLien.Value;
            if (soLienIn == 4)
            {
                if (documentViewer1.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = true;
                    documentViewer3.Visible = false;
                    documentViewer4.Visible = false;
                }
                else if (documentViewer2.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = true;
                    documentViewer4.Visible = false;
                }
                else if (documentViewer3.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = false;
                    documentViewer4.Visible = true;
                }
                else// if (documentViewer4.Visible)
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = false;
                    documentViewer4.Visible = false;
                }
            }
            else if (soLienIn == 3)
            {
                if (documentViewer1.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = true;
                    documentViewer3.Visible = false;
                }
                else if (documentViewer2.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = true;
                }
                else //if (documentViewer3.Visible)
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                    documentViewer3.Visible = false;
                }
                documentViewer4.Visible = false;
            }
            else if (soLienIn == 2)
            {
                if (documentViewer1.Visible)
                {
                    documentViewer1.Visible = false;
                    documentViewer2.Visible = true;
                }
                else if (documentViewer2.Visible)
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                }
                else
                {
                    documentViewer1.Visible = true;
                    documentViewer2.Visible = false;
                }
                documentViewer3.Visible = false;
                documentViewer4.Visible = false;
            }
            else if (soLienIn == 1)
            {
                documentViewer1.Visible = true;
                documentViewer2.Visible = false;
                documentViewer3.Visible = false;
                documentViewer4.Visible = false;
            }

            var dv = documentViewer1.Visible ? documentViewer1 :
                documentViewer2.Visible ? documentViewer2 :
                documentViewer3.Visible ? documentViewer3 :
                documentViewer4;
            documentViewer1_ZoomChanged(dv, null);
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            //btnIn.Focus();
        }

        private void exportToPdfMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx10 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx10, "PDF", ReportTitle);
                if (MauTuIn == 1)
                {
                    if (_soLienIn >= 2 && _repx20 != null)
                    {
                        DXreportManager.ExportRepxToPdfInThread_As(this, _repx20, "PDF", ReportTitle);
                    }
                    if (_soLienIn >= 3 && _repx30 != null)
                    {
                        DXreportManager.ExportRepxToPdfInThread_As(this, _repx30, "PDF", ReportTitle);
                    }
                    if (_soLienIn >= 4 && _repx40 != null)
                    {
                        DXreportManager.ExportRepxToPdfInThread_As(this, _repx40, "PDF", ReportTitle);
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
            V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
        }

        

		private void exportReportToXlsxMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx10 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx10, "EXCEL", ReportTitle);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "", ex);
            }
        }

        private void exportReportToXlsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx10 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx10, "XLS", ReportTitle);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "", ex);
            }
        }

        private void exportReportToDocxMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_repx10 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx10, "DOCX", ReportTitle);
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
                if (_repx10 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx10, "HTML", ReportTitle);
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
                if (_repx10 == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                DXreportManager.ExportRepxToPdfInThread_As(this, _repx10, "IMAGE", ReportTitle);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "", ex);
            }
        }

        private void documentViewer1_ZoomChanged(object sender, EventArgs e)
        {
            V6ControlsHelper.ShowV6Tooltip(panelCRview, string.Format("{0} {1}%", V6Text.Zoom, ((DocumentViewer)sender).Zoom * 100));
        }
    }

}
