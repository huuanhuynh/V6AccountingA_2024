using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter;
using V6ControlManager.FormManager.ReportManager;
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

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu
{
    public partial class InChungTuViewBase : V6FormControl
    {
        #region Biến toàn cục

        public decimal TTT { get; set; }
        public decimal TTT_NT { get; set; }
        public string MA_NT { get; set; }

        DataGridViewPrinter _myDataGridViewPrinter;
        private ReportDocument rpDoc, rpDoc2, rpDoc3, rpDoc4;

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
                    method_text += event_row["content"];
                    method_text += "\n";
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
                var M_COMPANY_BY_MA_DVCS = V6Options.V6OptionValues.ContainsKey("M_COMPANY_BY_MA_DVCS") ? V6Options.V6OptionValues["M_COMPANY_BY_MA_DVCS"].Trim() : "";
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

                InvokeFormEvent(QuickReportManager.FormEvent.AFTERADDFILTERCONTROL);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateFormControls", ex);
            }
        }

        private DataSet _ds;
        private DataTable _tbl, _tbl1, _tbl2, _tbl3;
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
        }

        internal string LAN
        {
            get { return rTiengViet.Checked ? "V" : rEnglish.Checked ? "E" : "B"; }
        }

        private DataRow SelectedRow
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

        private int SelectedSoLien
        {
            get
            {
                var row = SelectedRow;
                int result = 0;
                if (row != null)
                {
                    result = ObjectAndString.ObjectToInt(row["SO_LIEN"]);
                }
                if (result <= 0) result = Invoice_SoLien;
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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["Extra_para"].ToString().Trim();
                }
                return result;
            }
        }

        private string ReportFile
        {
            get
            {
                var result = _Ma_File;
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["Caption"].ToString().Trim();
                }
                return result;
            }
        }
        private string ReportCaption2
        {
            get
            {
                var result = _reportTitle2;
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["Caption2"].ToString().Trim();
                }
                return result;
            }
        }
        private string ReportTitle
        {
            get
            {
                var result = txtReportTitle.Text;
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["Title"].ToString().Trim();
                }
                return result;
            }
        }

        private bool IsInvoice
        {
            get
            {
                var result = false;
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = ObjectAndString.ObjectToInt(data.Rows[cboMauIn.SelectedIndex]["ND51"]) == 1;
                }
                return result;
            }
        }

        private string ReportFileFull
        {
            get
            {
                var result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".rpt";//ReportFile co su thay doi khi chon o combobox
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
        private string ReportFileFull_1
        {
            get
            {
                var result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_1.rpt";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_1.rpt";//_reportFile gốc
                }
                return result;
            }
        }
        private string ReportFileFull_2
        {
            get
            {
                var result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_2.rpt";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_2.rpt";//_reportFile gốc
                }
                return result;
            }
        }
        private string ReportFileFull_3
        {
            get
            {
                var result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_3.rpt";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _Ma_File + "_3.rpt";//_reportFile gốc
                }
                return result;
            }
        }
        private string ReportFileFull_4
        {
            get
            {
                var result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "_4.rpt";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"Reports\"
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
                var result = @"Reports\"
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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDS_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDSV2
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDS_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDFV1
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDF_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDFV2
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDF_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHV_V1
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDHV_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHE_V1
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDHE_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHV_V2
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDHV_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHE_V2
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDHE_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDT_V1
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDT_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDT_V2
        {
            get
            {
                var result = "";
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["GRDT_V2"].ToString().Trim();
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
                    if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                    {
                        var data = MauInView.ToTable();
                        var y_n = (data.Rows[cboMauIn.SelectedIndex]["Printer_yn"] ?? "").ToString().Trim();
                        if (y_n == "1" || print_one)
                            result = (data.Rows[cboMauIn.SelectedIndex]["Printer_def"] ?? "").ToString().Trim();
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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["Reload_data"].ToString().Trim();
                }
                return result;
            }
        }
        #endregion 

        public delegate void PrintSuccessDelegate(InChungTuViewBase sender, string stt_rec, int mau_tu_in);
        public event PrintSuccessDelegate PrintSuccess;
        protected virtual void CallPrintSuccessEvent()
        {
            var handler = PrintSuccess;

            if (handler != null)
            {
                int hoadon_nd51 = MauInView == null || cboMauIn.SelectedIndex < 0
                    ? 0
                    : ObjectAndString.ObjectToInt(MauInView.ToTable().Rows[cboMauIn.SelectedIndex]["ND51"]);
                handler(this, Report_Stt_rec, hoadon_nd51);
            }
        }

        //public ReportRViewBase()
        //{
        //    InitializeComponent();
        //}

        public InChungTuViewBase(V6InvoiceBase invoice,
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
            CreateFormProgram();
            CreateFormControls();
            InvokeFormEvent(QuickReportManager.FormEvent.INIT);
        }

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
                    return ObjectAndString.ObjectToInt(MauInView.ToTable().Rows[cboMauIn.SelectedIndex]["MAU_TU_IN"]);
                }
                return 0;
            }
        }

        private void MyInit2()
        {
            try
            {
                LoadComboboxSource();
                if (V6Setting.IsVietnamese)
                {
                    rTiengViet.Checked = true;
                }
                else
                {
                    rEnglish.Checked = true;
                }
                txtReportTitle.Text = ReportTitle;

                numSoLien.Value = SelectedSoLien;
                
                if (numSoLien.Value > 0) numSoLien.Enabled = true;

                if (!V6Login.IsAdmin)
                {
                    exportToExcel.Visible = false;
                    //viewDataToolStripMenuItem.Visible = false;
                }

                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2 " + ReportFileFull, ex);
            }
        }

        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            MyInit2();
            MakeReport(AutoPrint, PrinterName, (int)numSoLien.Value, _printCopy);
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

        
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            btnNhanImage = btnNhan.Image;
            MakeReport(false, null, (int) numSoLien.Value);
        }

        //private const int nameLineLength = 40;
        private const int lineDroppedHeight = 600;//500
        //private const int dropLineHeight = 15;//500-320

        /// <summary>
        /// Tính toán đường chéo sẽ hiện trên report
        /// </summary>
        /// <param name="t">Bảng dữ liệu</param>
        /// <param name="field">Trường tính toán ký tự</param>
        /// <param name="lengOfName"></param>
        /// <param name="twLineHeight"></param>
        /// <returns></returns>
        private int CalculateCrossLine(DataTable t, string field, int lengOfName, int twLineHeight)
        {
            var dropLineHeightBase = 300;
            var dropLineHeight1 = lineDroppedHeight - twLineHeight;
            //Mỗi dòng drop sẽ nhân với DropLineHeight
            //var dropCount = 0;
            var dropHeight = 0;
            foreach (DataRow r in t.Rows)
            {
                try
                {
                    var len = r[field].ToString().Trim().Length;
                    if (len > 1) len--;
                    
                    var dropCount = len / lengOfName;
                    if (dropCount > 0)
                    {
                        dropHeight += dropLineHeight1;
                        if (dropCount > 1) dropHeight += dropLineHeightBase*(dropCount-1);
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
            return t.Rows.Count * 2 + dropHeight / (twLineHeight / 2);
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

        private void SetCrossLine()
        {
            try
            {
                SetCrossLineRpt(rpDoc);

                if (MauTuIn == 1 && _soLienIn >= 2 && rpDoc2 != null)
                {
                    SetCrossLineRpt(rpDoc2);
                }
                if (MauTuIn == 1 && _soLienIn >= 3 && rpDoc3 != null)
                {
                    SetCrossLineRpt(rpDoc3);
                }
                if (MauTuIn == 1 && _soLienIn >= 4 && rpDoc4 != null)
                {
                    SetCrossLineRpt(rpDoc4);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetCrossLine", ex);
            }
        }

        private void SetCrossLineRpt(ReportDocument rpt)
        {
            int flag = 0;
            var checkField = "TEN_VT";
            try
            {
                if (!IsInvoice) return;

                var Khung = rpt.ReportDefinition.ReportObjects["Khung"];
                var DuongNgang = rpt.ReportDefinition.ReportObjects["DuongNgang"];
                var DuongCheo = rpt.ReportDefinition.ReportObjects["DuongCheo"];
                flag = 1;
                //var Section1 = rpt.ReportDefinition.Sections["ReportHeaderSection1"];
                //var Section2 = rpt.ReportDefinition.Sections["Section3"];
                //var h1 = Section1.Height;
                //var h2 = Section2.Height;

                

                int boxTop = Khung.Top;// 6500;
                int boxHeight = Khung.Height;// 3840;
                int lineHeight = DuongNgang.Height;
                
                int halfLineHeight = lineHeight/2;// boxHeight/20;//192, 20 is maxLine

                int dropMax = 40;
                try
                {
                    dropMax = ObjectAndString.ObjectToInt(Invoice.Alct.Rows[0]["drop_Max"]);
                    if (dropMax < 1) dropMax = 40;
                    //Lấy lại thông tin dropMax theo albc (cboMauin)
                    if (SelectedRow != null && SelectedRow.Table.Columns.Contains("DROP_MAX"))
                    {
                        var dropMaxT = ObjectAndString.ObjectToInt(SelectedRow["DROP_MAX"]);
                        if (dropMaxT > 5) dropMax = dropMaxT;
                    }
                    //Lấy lại checkField (khác MA_VT)
                    if (SelectedRow != null && SelectedRow.Table.Columns.Contains("FIELD_MAX"))
                    {
                        var checkFieldT = SelectedRow["FIELD_MAX"].ToString().Trim();
                        if (checkFieldT.Length > 0) checkField = checkFieldT;
                    }
                }
                catch
                {
                    flag = 2;
                }
                
                
                if (!_tbl.Columns.Contains(checkField))
                {
                    checkField = _tbl.Columns.Contains("DIEN_GIAII") ? "DIEN_GIAII" : _tbl.Columns[0].ColumnName;
                }
                flag = 3;
                int crossLineNum = CalculateCrossLine(_tbl, checkField, dropMax, lineHeight)
                    + (int)numCrossAdd.Value;
                var top = boxTop + (halfLineHeight * crossLineNum);//3840/20=192
                var height = boxHeight - (top - boxTop);
                flag = 5;
                //if (height <= 0) height = 10;
                if (height < 150) // Hide lowCrossline.
                {
                    height = 10;
                    DuongNgang.Width = DuongNgang.Width + DuongCheo.Width;
                    DuongCheo.Width = 10;
                }

                DuongNgang.Height = 10;
                DuongNgang.Top = top + 30;
                
                DuongCheo.Height = height;
                DuongCheo.Top = top;
                
                flag = 9;
            }
            catch(Exception ex)
            {
                if(flag == 3)
                ShowTopMessage("Kiểm tra thông tin trường tính toán drop_line [" + checkField + "]");
                this.WriteExLog(GetType() + ".SetCrossLineRpt", ex);
            }
        }

        private SortedDictionary<string, object> ReportDocumentParameters; 
        /// <summary>
        /// Lưu ý: chạy sau khi add dataSource để tránh lỗi nhập parameter value
        /// </summary>
        private void SetAllReportParams()
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
                "In bởi  Phần mềm V6 Accounting2016.NET - Cty phần mềm V6 (www.v6corp.com) - MST: 0303180249 - ĐT: 08.62570563"
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
            ReportDocumentParameters.Add("M_MA_THUE", V6Options.V6OptionValues["M_MA_THUE"]);
            ReportDocumentParameters.Add("M_RTEN_VSOFT", V6Options.V6OptionValues["M_RTEN_VSOFT"]);

            ReportDocumentParameters.Add("M_TEN_NLB", txtM_TEN_NLB.Text.Trim());
            ReportDocumentParameters.Add("M_TEN_NLB2", txtM_TEN_NLB2.Text.Trim());
            ReportDocumentParameters.Add("M_TEN_KHO_BD", V6Options.V6OptionValues["M_TEN_KHO_BD"]);
            ReportDocumentParameters.Add("M_TEN_KHO2_BD", V6Options.V6OptionValues["M_TEN_KHO2_BD"]);
            ReportDocumentParameters.Add("M_DIA_CHI_BD", V6Options.V6OptionValues["M_DIA_CHI_BD"]);
            ReportDocumentParameters.Add("M_DIA_CHI2_BD", V6Options.V6OptionValues["M_DIA_CHI2_BD"]);

            ReportDocumentParameters.Add("M_TEN_GD", V6Options.V6OptionValues["M_TEN_GD"]);
            ReportDocumentParameters.Add("M_TEN_GD2", V6Options.V6OptionValues["M_TEN_GD2"]);
            ReportDocumentParameters.Add("M_TEN_KTT", V6Options.V6OptionValues["M_TEN_KTT"]);
            ReportDocumentParameters.Add("M_TEN_KTT2", V6Options.V6OptionValues["M_TEN_KTT2"]);

            ReportDocumentParameters.Add("M_SO_QD_CDKT", V6Options.V6OptionValues["M_SO_QD_CDKT"]);
            ReportDocumentParameters.Add("M_SO_QD_CDKT2", V6Options.V6OptionValues["M_SO_QD_CDKT2"]);
            ReportDocumentParameters.Add("M_NGAY_QD_CDKT", V6Options.V6OptionValues["M_NGAY_QD_CDKT"]);
            ReportDocumentParameters.Add("M_NGAY_QD_CDKT2", V6Options.V6OptionValues["M_NGAY_QD_CDKT2"]);

            ReportDocumentParameters.Add("M_RFONTNAME", V6Options.V6OptionValues["M_RFONTNAME"]);
            ReportDocumentParameters.Add("M_R_FONTSIZE", V6Options.V6OptionValues["M_R_FONTSIZE"]);
            

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

            string errors = "";
            foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
            {
                try
                {
                    rpDoc.SetParameterValue(item.Key, item.Value);
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
                    }
                    catch (Exception ex)
                    {
                        errors += "rpDoc4 " + item.Key + ": " + ex.Message + "\n";
                    }
                }
            }

            if (errors != "")
            {
                V6ControlFormHelper.AddLastError(GetType() + ".SetAllReportParams\r\nFile: "
                    + ReportFileFull + "\r\nError: " + errors);
            }

        }

        #region ==== LoadData MakeReport ====
        bool _dataLoaded;
        bool _dataLoading;

        
        void LoadData()
        {
            object beforeLoadData = InvokeFormEvent(QuickReportManager.FormEvent.BEFORELOADDATA);

            try
            {
                if (beforeLoadData != null && !(bool)beforeLoadData)
                {
                    _message = V6Text.CheckInfor;
                    Data_Loading = false;
                    return;
                }

                _dataLoading = true;
                _dataLoaded = false;
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
                if (_ds.Tables.Count > 0)
                {
                    _tbl = _ds.Tables[0];
                    _tbl.TableName = "DataTable1";
                }
                if (_ds.Tables.Count > 1)
                {
                    _tbl1 = _ds.Tables[1];
                    _tbl1.TableName = "DataTable2";
                    if (_tbl1.Rows.Count > 0) FilterControl.Call1(_tbl1.Rows[0]);
                }
                else
                {
                    _tbl1 = null;
                }
                
                _dataLoaded = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadData Error\n" + ex.Message, "InChungTuViewBase");
                _tbl = null;
                _tbl1 = null;
                _ds = null;
                _dataLoaded = false;
            }
            _dataLoading = false;
        }

        private bool _forcePrint;
        public string PrinterName { get; set; }
        private int _soLienIn = 1, _printCopy = 1;

        public int PrintCopies
        {
            get { return _printCopy; }
            set { _printCopy = value; }
        }

        //public bool xong { get; set; }
        public bool AutoPrint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forcePrint">In luôn</param>
        /// <param name="printerName">Nếu forcePrint=true thì bắt buộc có printerName</param>
        /// <param name="soLien"></param>
        /// <param name="printCopy"></param>
        public void MakeReport(bool forcePrint, string printerName,
            int soLien, int printCopy = 1)
        {
            //if (_dataLoading)
            //{
            //    return false;
            //}
            try
            {
                _forcePrint = forcePrint;
                PrinterName = printerName;
                _soLienIn = soLien;
                if (_soLienIn < 1) _soLienIn = 1;
                _printCopy = printCopy;

                GenerateProcedureParameters(); //Add các key khác

                LoadData();

                try
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _tbl;
                    dataGridView1.DataSource = _tbl1;
                    FormatGridView();

                    ViewReport();
                    if (_forcePrint)
                    {
                        Print(PrinterName);
                        if (!IsDisposed) Dispose();
                    }
                    if (!dataGridView1.IsDisposed) dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(GetType() + ".MakeReport " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message, "InChungTuViewBase");
            }
        }

        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_dataLoaded)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                try
                {
                    FilterControl.LoadDataFinish(_ds);
                    InvokeFormEvent(QuickReportManager.FormEvent.AFTERLOADDATA);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _tbl;
                    //FormHelper.SetGridHeaderTextAndFormat(dataGridView1, m_XmlConfig.m_GridFormatDictionary, MainForm.myMessage, MainForm.CurrentLang);
                    FormatGridView();

                    
                    ViewReport();
                    if (_forcePrint)
                    {
                        Print(PrinterName);
                        Dispose();
                    }

                    dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    _dataLoaded = false;
                    this.ShowErrorMessage(GetType() + ".MakeReport " + ex.Message);
                }
            }
            else if (_dataLoading)
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
            //VPA_GetFormatGridView]@Codeform VARCHAR(50),@Type VARCHAR(20)
            string FIELDV, OPERV, BOLD_YN, COLOR_YN, COLORV;
            object VALUEV;
            V6BusinessHelper.GetFormatGridView(_program, "REPORT", out FIELDV, out OPERV, out VALUEV, out BOLD_YN, out COLOR_YN, out COLORV);
            //Color.MediumAquamarine
            V6ControlFormHelper.FormatGridView(dataGridView1, FIELDV, OPERV, VALUEV, BOLD_YN == "1", COLOR_YN == "1", Color.FromName(COLORV));


            //Header
            var fieldList = (from DataColumn column in _tbl.Columns select column.ColumnName).ToList();

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
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
            }
            f = dataGridView1.Columns["TIEN2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
            }
            f = dataGridView1.Columns["GIA2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIA"];
            }

            //V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Invoice.GRDS_AD, Invoice.GRDF_AD,
            //            V6Setting.IsVietnamese ? Invoice.GRDHV_AD : Invoice.GRDHE_AD);
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1, V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView1, Invoice.Mact);
        }

        #endregion ==== LoadData MakeReport ====
        

         #region Linh tinh        

        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
        #endregion Linh tinh

        private void exportToExcel_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                ShowTopMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog {Filter = @"Excel files (*.xls)|*.xls|Xlsx|*.xlsx"};
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {   
                        V6Tools.V6Export.Data_Table.ToExcel(_tbl, save.FileName, Name, true);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorMessage(GetType() + ".ExportFail: " + ex.Message, "InChungTuViewBase");
                        return;
                    }
                    this.ShowInfoMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ExportFail\n" + ex.Message, "InChungTuViewBase");
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
                    MakeReport(false, null, (int)numSoLien.Value);
                else
                    ViewReport();
            }
        }

        private bool _radioChange = false;

        private void rbtLanguage_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady || string.IsNullOrEmpty(MA_NT)) return;
            _radioChange = true;
            txtReportTitle.Text = ReportTitle;// (rTiengViet.Checked ? ReportTitle : rEnglish.Checked ? ReportTitle : (ReportTitle+2 ));
            SetFormReportFilter();
            if (((RadioButton) sender).Checked)
            {
                if (ReloadData == "1")
                    MakeReport(false, null, (int)numSoLien.Value);
                else
                    ViewReport();
            }
        }
        

        private bool SetupThePrinting()
        {
            PrintDialog myPrintDialog = new PrintDialog();
            myPrintDialog.AllowCurrentPage = false;
            myPrintDialog.AllowPrintToFile = false;
            myPrintDialog.AllowSelection = false;
            myPrintDialog.AllowSomePages = false;
            myPrintDialog.PrintToFile = false;
            myPrintDialog.ShowHelp = false;
            myPrintDialog.ShowNetwork = false;

            if (myPrintDialog.ShowDialog(this) != DialogResult.OK)
                return false;

            MyPrintDocument.DocumentName = Text;
            MyPrintDocument.PrinterSettings = myPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = myPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

            _myDataGridViewPrinter = new DataGridViewPrinter(dataGridView1, MyPrintDocument,
                this.ShowConfirmMessage("PrintAlignmentCenter") == DialogResult.Yes,
                true, Text, new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }
        private void MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = _myDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more)
                e.HasMorePages = true;
        }
        private void printGrid_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                ShowTopMessage(V6Text.NoData);
                return;
            }
            try
            {
                MyPrintDocument.PrintPage += MyPrintDocument_PrintPage;
                if (SetupThePrinting())
                {
                    MyPrintDocument.Print();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".PrintGrid\n" + ex.Message, "InChungTuViewBase");
            }
        }
        

        void ViewReport()
        {
            if (_ds == null) return;
            crystalReportViewer1.DisplayToolbar = false;
            crystalReportViewer2.DisplayToolbar = false;
            crystalReportViewer3.DisplayToolbar = false;
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
                    rpDoc.SetDataSource(_ds);
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
                    }
                    catch (Exception e4)
                    {
                        rpDoc4 = null;
                        this.ShowErrorMessage(GetType() + ".ViewReport rpDoc4.Load: " + e4.Message);
                    }
                }
                

                SetAllReportParams();
                SetCrossLine();

                crystalReportViewer1.ReportSource = rpDoc;
                if (_soLienIn >= 2 && rpDoc2 != null)
                crystalReportViewer2.ReportSource = rpDoc2;
                if (_soLienIn >= 3 && rpDoc3 != null)
                crystalReportViewer3.ReportSource = rpDoc3;
                //if (_soLienIn >= 4 && rpDoc4 != null)
                //    crystalReportViewer4.ReportSource = rpDoc4;

                crystalReportViewer1.Show();
                crystalReportViewer1.Zoom(1);
                crystalReportViewer1.Visible = true;
                crystalReportViewer2.Visible = false;
                crystalReportViewer3.Visible = false;
            }
            else
            {
                if (!IsInvoice)
                {
                    crystalReportViewer1.DisplayToolbar = true;
                    crystalReportViewer2.DisplayToolbar = true;
                    crystalReportViewer3.DisplayToolbar = true;
                }

                rpDoc = new ReportDocument();
                if (File.Exists(ReportFileFull)) rpDoc.Load(ReportFileFull);
                else this.ShowWarningMessage(V6Text.NotExist + ": " + ReportFileFull);

                rpDoc.SetDataSource(_ds);

                SetAllReportParams();
                SetCrossLine();

                crystalReportViewer1.ReportSource = rpDoc;
                crystalReportViewer1.Show();
                crystalReportViewer1.Zoom(1);
                crystalReportViewer1.Visible = true;
                crystalReportViewer2.Visible = false;
                crystalReportViewer3.Visible = false;
            }
        }

        private void Print(string printerName)
        {
            int intDaGuiDenMayIn = 0;
            bool printerOnline = PrinterStatus.CheckPrinterOnline(printerName);
            var setPrinterOk = PrinterStatus.SetDefaultPrinter(printerName);
            var printerError = string.Compare("Error", PrinterStatus.getDefaultPrinterProperties("Status"), StringComparison.OrdinalIgnoreCase) == 0;

            if (setPrinterOk && printerOnline && !printerError)
            {
                try
                {
                    if (MauTuIn == 1)
                    {
                        try
                        {
                            rpDoc.PrintToPrinter(1, false, 1, 1);
                            intDaGuiDenMayIn++;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("In liên 1 lỗi:\nNgừng in!\n" + ex.Message);
                        }

                        if (_soLienIn > 1)
                            try
                            {
                                rpDoc2.PrintToPrinter(1, false, 1, 1);
                                intDaGuiDenMayIn++;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("In liên 2 lỗi:\nNgừng in!\n" + ex.Message);
                            }
                        if (_soLienIn > 2)
                        {
                            try
                            {
                                rpDoc3.PrintToPrinter(1, false, 1, 1);
                                intDaGuiDenMayIn++;
                            }
                            catch (Exception ex)
                            {
                                this.ShowErrorMessage(GetType() + ".In liên 3 lỗi:\n" + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            if (IsInvoice)
                            {
                                rpDoc.PrintToPrinter(_soLienIn, false, 1, 1);
                            }
                            else
                            {
                                rpDoc.PrintToPrinter(_soLienIn*_printCopy, false, 0, 0);
                            }
                            intDaGuiDenMayIn = _soLienIn;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("In lỗi:\nNgừng in!\n" + ex.Message);
                        }

                        //if (_soLienIn > 1)
                        //    try
                        //    {
                        //        rpDoc.PrintToPrinter(1, false, 1, 1);
                        //        intDaGuiDenMayIn++;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw new Exception("In liên 2 lỗi:\nNgừng in!\n" + ex.Message);
                        //    }
                        //if (_soLienIn > 2)
                        //{
                        //    try
                        //    {
                        //        rpDoc.PrintToPrinter(1, false, 1, 1);
                        //        intDaGuiDenMayIn++;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        this.ShowErrorMessage(GetType() + ".In liên 3 lỗi:\n" + ex.Message);
                        //    }
                        //}
                    }
                    
                    //if (!xemMau)
                    //    timer1.Start();
                    if (intDaGuiDenMayIn == _soLienIn)
                    {
                        //xong = true;
                        CallPrintSuccessEvent();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(GetType() + ".In lỗi!\n" + ex.Message, "V6Soft");
                }
            }
            else
            {
                //isInHoaDonClicked = false;
                btnIn.Enabled = true;
                this.ShowErrorMessage(GetType() + ".Không thể truy cập máy in!", "V6Soft");
            }
            //reset default printer
            //try { V6Tools.PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter); }
            //catch { }
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

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("In chứng từ.");
        }
        
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (FilterControl._hideFields != null && FilterControl._hideFields.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
            }
        }

        private void ExporttoExceltemplate_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.ExportExcelTemplate(this, _tbl, _tbl1, ReportDocumentParameters,
              MAU, LAN, ReportFile, ExcelTemplateFileFull, ReportTitle);
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

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                _soLienIn = (int) numSoLien.Value;
                //if (!IsInvoice)
                //{
                //    crystalReportViewer1.PrintReport();
                //    return;
                //}

                var dfp = DefaultPrinter;
                if (string.IsNullOrEmpty(PrinterName))
                {
                    _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();

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
                        Print(selectedPrinter);
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
                    Print(PrinterName);
                }
                
            }
            catch (Exception ex)
            {
                PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                this.ShowErrorMessage(GetType() + ".Print: " + ex.Message, "InChungTuViewBase");
            }
        }

        private void cboMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady || string.IsNullOrEmpty(MA_NT)) return;

            txtReportTitle.Text = ReportTitle;
            numSoLien.Value = SelectedSoLien;
            if (ReloadData == "1")
                MakeReport(false, null, (int)numSoLien.Value);
            else
                ViewReport();
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
                f2.InsertSuccessEvent += (data) =>
                {
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
                };
                f2.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ThemMauBC_Click: " + ex.Message);
            }
        }

        private void btnSuaMau_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new FormRptEditor();
                f.rptPath = ReportFileFull;
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message, "InChungTuViewBase");
            }
        }

        private void btnSuaTTMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, AlbcKeys, null);
                f2.UpdateSuccessEvent += (data) =>
                {
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
                };
                f2.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnSuaTTMauBC_Click: " + ex.Message);
            }
        }

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadComboboxSource();
        }

        private void btnLt_Click(object sender, EventArgs e)
        {
            var soLienIn = numSoLien.Value;
            if (soLienIn == 3)
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
            }
            else if (soLienIn == 1)
            {
                crystalReportViewer1.Visible = true;
                crystalReportViewer2.Visible = false;
                crystalReportViewer3.Visible = false;
            }
        }

        private void btnLs_Click(object sender, EventArgs e)
        {
            var soLienIn = numSoLien.Value;
            if (soLienIn == 3)
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
            }
            else if (soLienIn == 1)
            {
                crystalReportViewer1.Visible = true;
                crystalReportViewer2.Visible = false;
                crystalReportViewer3.Visible = false;
            }
        }

        
    }
}
