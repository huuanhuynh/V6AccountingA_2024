﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.ReportManager.Filter;
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

namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    public partial class ReportRView0Base : V6FormControl
    {
        #region Biến toàn cục
        
        private string _reportProcedure;
        //private string _program, _reportFile, _reportTitle, _reportTitle2;
        private string _program, _Ma_File, _reportTitle, _reportTitle2;
        private string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        /// <summary>
        /// Advance filter get albc, nhận từ filter cha để lọc.
        /// </summary>
        public string Advance = "";

        private DataTable MauInData;
        private DataView MauInView;

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
                SetStatus2Text();
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
        private DataTable _tbl1, _tbl2, _tbl3;
        private DataView _view1, _view2, _view3;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        private List<SqlParameter> _pList;

        //public bool AutoPrint { get; set; }
        public bool AutoClickNhan { get; set; }
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
            string s = MauInSelectedRow["EXTRA_INFOR"].ToString().Trim();
            if (s != "")
            {
                var sss = s.Split(';');
                foreach (string ss in sss)
                {
                    int indexOf = ss.IndexOf(":", StringComparison.Ordinal);
                    if (indexOf > 0)
                    {
                        _extraInfor[ss.Substring(0, indexOf).ToUpper()] = ss.Substring(indexOf + 1);
                    }
                }
            }
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

        public string ReportFileFull
        {
            get
            {
                var result = @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".rpt";
                if (!File.Exists(result))
                {
                    result = @"Reports\"
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
                var result = @"Reports\"
                    + RPT_DIR
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "F7.rpt";
                if (!File.Exists(result))
                {
                    result = @"Reports\"
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
        //public ReportRView0Base()
        //{
        //    InitializeComponent();
        //}

        public ReportRView0Base(string itemId, string program, string reportProcedure,
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
                //CheckRightReport();
                if (V6Options.M_R_FONTSIZE > 8)
                {
                    dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, V6Options.M_R_FONTSIZE);
                }
                InvokeFormEvent(FormDynamicEvent.INIT);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
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
                
                txtReportTitle.Text = ReportTitle;

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
                gridViewSummary1.NoSumColumns = Report_GRDT_V1;
                if (MauInSelectedRow != null)
                {
                    gridViewSummary1.SumCondition = new Condition()
                    {
                        FIELD = MauInSelectedRow["FIELD_S"].ToString().Trim(),
                        OPER = MauInSelectedRow["OPER_S"].ToString().Trim(),
                        VALUE = MauInSelectedRow["VALUE_S"].ToString().Trim()
                    };
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

                GetSumCondition();
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
                    //cboMauIn.Enabled = true;
                    //btnSuaTTMauBC.Enabled = true;
                    //btnThemMauBC.Enabled = true;
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
                ShowReport();
            }
            else if (AutoClickNhan)
            {
                btnNhan.PerformClick();
            }
        }

        
        public FilterBase FilterControl { get; set; }
        public IDictionary<string, object> SelectedRowData { get; set; }
        
        private void AddFilterControl(string program)
        {
            FilterControl = Filter.Filter.GetFilterControl(program, _reportProcedure);
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
                MakeReport2();
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

        public IDictionary<string, object> ReportDocumentParameters; 
        

        #region ==== LoadData MakeReport ====
        
        /// <summary>
        /// GenerateProcedureParameters();//Add các key
        /// var tLoadData = new Thread(LoadData);
        /// tLoadData.Start();
        /// timerViewReport.Start();
        /// </summary>
        private void MakeReport2()
        {
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

        void LoadData()
        {
            object beforeLoadData = InvokeFormEvent(FormDynamicEvent.BEFORELOADDATA);

            try
            {
                if (beforeLoadData != null && !(bool)beforeLoadData)
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
                    MakeReport2();
                //else
                //    ViewReport();
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
                        MakeReport2();
                    //else
                    //    ViewReport();
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
                ShowTopLeftMessage(V6Text.NoData);
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
                
                V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
                FormatGridView();
                gridViewTopFilter1.MadeFilterItems();

                //ViewReport();
                
                dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                timerViewReport.Stop();
                _executesuccess = false;
                this.ShowErrorException(GetType() + ".ShowReport", ex);
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

                    viewText += string.Format("   {0} {1}", field_header_template, ObjectAndString.NumberToString
                        (tbl2_row[value_field], decimal_place, V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR));
                }

                if (viewText.Length > 3) viewText = viewText.Substring(3);
                lblSummary.Text = viewText;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ViewFooter", ex);
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
                this.ShowErrorMessage(GetType() + ".ReportRView0Base XuLyHienThiFormSuaChungTu:\n" + ex.Message);
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

                var view = new ReportRView0Base(m_itemId, _program + "F5", _program + "F5",
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

                view.AutoClickNhan = true;
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
                new ChartReportForm(FilterControl, ReportFileFullF7, _tbl1, _tbl2.Copy(), ReportDocumentParameters)
                    .ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyVeDoThiF7: " + ex.Message);
            }
            SetStatus2Text();
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
            FilterControl.SetStatus2Text();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5 && FilterControl.F5)
            {
                if(dataGridView1.Focused) XuLyXemChiTietF5();
            }
        }

        

        private void ReportRView0Base_VisibleChanged(object sender, EventArgs e)
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
            V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
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

                printGrid_Click(null, null);

                print_one = true;
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

            GetSumCondition();

            txtReportTitle.Text = ReportTitle;
            if (ReloadData == "1")
                MakeReport2();
            //else
                //ViewReport();
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
                ShowTopLeftMessage(V6Text.NoData);
                return;
            }
            try
            {
                //V6Tools.V6Export.ExportData.ToExcel(_tbl1, save.FileName, txtReportTitle.Text, true);
                string fileName = V6ControlFormHelper.ExportExcel_ChooseFile(this, _tbl1, GetExportFileName(), txtReportTitle.Text);

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
            V6ControlFormHelper.ExportExcelTemplate_ChooseFile(this, _tbl1, _tbl2, ReportDocumentParameters,
                MAU, LAN, ReportFile, ExcelTemplateFileFull, GetExportFileName());
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
                    V6ControlFormHelper.ExportExcelTemplateD(this, data, _tbl2, "V", ReportDocumentParameters,
                        MAU, LAN, ReportFile, ExcelTemplateFileView, ReportTitle, excelColumns, excelHeaders);
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
                ShowTopLeftMessage(V6Text.NoData);
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
                //if (_rpDoc0 == null)
                //{
                //    ShowMainMessage(V6Text.NoData);
                //    return;
                //}
                //V6ControlFormHelper.ExportRptToPdf_As(this, _rpDoc0, GetExportFileName());
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
                if (dataGridView1.CurrentRow == null || !dataGridView1.Columns.Contains("MA_CT") || !dataGridView1.Columns.Contains("STT_REC")) return;
                
                var row = dataGridView1.CurrentRow;
                string ma_ct = row.Cells["MA_CT"].Value.ToString().Trim();
                string stt_rec = row.Cells["STT_REC"].Value.ToString().Trim();
                if (ma_ct == String.Empty || stt_rec == String.Empty) return;
                new InvoiceInfosViewForm(V6InvoiceBase.GetInvoiceBase(ma_ct), stt_rec, ma_ct).ShowDialog(this);
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

    }
}