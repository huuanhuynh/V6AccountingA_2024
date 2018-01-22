using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6ReportControls;
using V6RptEditor;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.ReportD
{
    public partial class ReportD99ViewBase : V6FormControl
    {
        #region ==== Biến toàn cục ====
        DataGridViewPrinter _myDataGridViewPrinter;
        private ReportDocument _rpDoc;

        private string _reportProcedure;
        private string _program, _Ma_File, _reportTitle, _reportTitle2;
        private string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        /// <summary>
        /// Advance filter get albc
        /// </summary>
        public string Advance = "";

        private DataTable MauInData;
        private DataView MauInView;
        private DataSet _ds;
        private DataTable _tbl, _tbl2, _tblv;
        private DataTable[] _tbls;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        private List<SqlParameter> _pList;

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
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_BC", _program);
                var AlreportData = V6BusinessHelper.Select(V6TableName.Alreport, keys, "*").Data;
                if (AlreportData.Rows.Count == 0) return;

                var dataRow = AlreportData.Rows[0];
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

                FilterControl = QuickReportManager.AddFilterControl44Base(_program, panel1);
                InvokeFormEvent(QuickReportManager.FormEvent.AFTERADDFILTERCONTROL);
                QuickReportManager.MadeFilterControls(FilterControl, _program, out All_Objects);
                All_Objects["thisForm"] = this;
                gridViewSummary1.Visible = FilterControl.ViewSum;

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
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
        }

        public string LAN
        {
            get { return rTiengViet.Checked ? "V" : rEnglish.Checked ? "E" : "B"; }
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
                var result = V6Setting.IsVietnamese ? _reportTitle : _reportTitle2;
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["Title"].ToString().Trim();
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

        private string ReportFileFullF7
        {
            get
            {
                var result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + "F7.rpt";//ReportFile co su thay doi khi chon o combobox
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
        private string ExcelTemplateFileFull
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
        
        private int F_START
        {
            get
            {
                var result = 2;
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = ObjectAndString.ObjectToInt(data.Rows[cboMauIn.SelectedIndex]["FSTART"]);
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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = ObjectAndString.ObjectToInt(data.Rows[cboMauIn.SelectedIndex]["FFIXCOLUMN"]);
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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["Reload_data"].ToString().Trim();
                }
                return result;
            }
        }
        #endregion 
        
        public ReportD99ViewBase(string itemId, string program, string reportProcedure,
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
            if (V6Login.IsAdmin) chkHienTatCa.Enabled = true;
            CreateFormProgram();
            CreateFormControls();
            InvokeFormEvent(QuickReportManager.FormEvent.INIT);
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
                LoadDefaultData(4, "", _Ma_File, m_itemId);

                if (!V6Login.IsAdmin)
                {
                    var menuRow = V6Menu.GetRow(ItemID);
                    if (menuRow != null)
                    {
                        var key3 = menuRow["Key3"].ToString().Trim().ToUpper();
                        var user_acc = V6Login.UserInfo["USER_ACC"].ToString().Trim();
                        if (user_acc != "1")
                        {
                            if (!key3.Contains("1")) exportToExcelTemplate.Visible = false;
                            if (!key3.Contains("2")) exportToExcelView.Visible = false;
                            if (!key3.Contains("3")) exportToExcel.Visible = false;
                            if (!key3.Contains("4")) exportToXmlToolStripMenuItem.Visible = false;
                            if (!key3.Contains("5")) printGrid.Visible = false;
                            if (!key3.Contains("6")) viewDataToolStripMenuItem.Visible = false;
                        }
                    }
                }

                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2", ex);
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


        public ReportFilter44Base FilterControl { get; set; }
        

        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (Data_Loading)
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

        private SortedDictionary<string, object> ReportDocumentParameters; 
        /// <summary>
        /// Lưu ý: chạy sau khi add dataSource để tránh lỗi nhập parameter value
        /// </summary>
        private void SetAllReportParams()
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
                {"M_MA_THUE", V6Options.V6OptionValues["M_MA_THUE"]},
                {"M_RTEN_VSOFT", V6Options.V6OptionValues["M_RTEN_VSOFT"]},

                {"M_TEN_NLB", txtM_TEN_NLB.Text.Trim()},
                {"M_TEN_NLB2", txtM_TEN_NLB2.Text.Trim()},
                {"M_TEN_KHO_BD", V6Options.V6OptionValues["M_TEN_KHO_BD"]},
                {"M_TEN_KHO2_BD", V6Options.V6OptionValues["M_TEN_KHO2_BD"]},
                {"M_DIA_CHI_BD", V6Options.V6OptionValues["M_DIA_CHI_BD"]},
                {"M_DIA_CHI2_BD", V6Options.V6OptionValues["M_DIA_CHI2_BD"]},

                {"M_TEN_GD", V6Options.V6OptionValues["M_TEN_GD"]},
                {"M_TEN_GD2", V6Options.V6OptionValues["M_TEN_GD2"]},
                {"M_TEN_KTT", V6Options.V6OptionValues["M_TEN_KTT"]},
                {"M_TEN_KTT2", V6Options.V6OptionValues["M_TEN_KTT2"]},

                {"M_RFONTNAME", V6Options.V6OptionValues["M_RFONTNAME"]},
                {"M_R_FONTSIZE", V6Options.V6OptionValues["M_R_FONTSIZE"]},
            };

            V6Login.SetCompanyInfo(ReportDocumentParameters);

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

            foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
            {
                _rpDoc.SetParameterValue(item.Key, item.Value);
            }

            //SetReportParams2();
        }

        private void SetReportParams2()
        {
            if (FilterControl.RptExtraParameters != null)
            foreach (KeyValuePair<string, object> key_value_pair in FilterControl.RptExtraParameters)
            {
                _rpDoc.SetParameterValue(key_value_pair.Key, key_value_pair.Value);
            }
        }

        #region ==== LoadData MakeReport ====
        
        void LoadData()
        {
            All_Objects["_plist"] = _pList;
            object beforeLoadData = InvokeFormEvent(QuickReportManager.FormEvent.BEFORELOADDATA);

            try
            {
                if (beforeLoadData != null && !(bool)beforeLoadData)
                {
                    _message = V6Text.CheckInfor;
                    Data_Loading = false;
                    return;
                }

                Data_Loading = true;
                _load_data_success = false;
                _ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, _pList.ToArray());
                if (_ds.Tables.Count > 0)
                {
                    _tbl = _ds.Tables[0];
                    _tbl.TableName = "DataTable1";
                    _tbls = TachBang(_tbl, F_START, F_FIXCOLUMN);
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
                
                _load_data_success = true;
            }
            catch (Exception ex)
            {
                _message = "Query Error!\n"+ex.Message;
                _tbl = null;
                _tbl2 = null;
                _ds = null;
                _load_data_success = false;
            }
            Data_Loading = false;
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
                Data_Loading = true;
                var tLoadData = new Thread(LoadData);
                tLoadData.Start();
                timerViewReport.Start();
            }
        }
        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (Data_Loading)
            {
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else if (_load_data_success)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;

                try
                {
                    FilterControl.LoadDataFinish(_ds);
                    All_Objects["_ds"] = _ds;
                    InvokeFormEvent(QuickReportManager.FormEvent.AFTERLOADDATA);

                    dataGridView1.TableSource = _tbl;

                    try
                    {
                        string FIELDV, OPERV, BOLD_YN, COLOR_YN, COLORV;
                        object VALUEV;
                        V6BusinessHelper.GetFormatGridView(CodeForm.Substring(1), "REPORT", out FIELDV, out OPERV,
                            out VALUEV, out BOLD_YN, out COLOR_YN, out COLORV);
                        V6ControlFormHelper.FormatGridView(dataGridView1, FIELDV, OPERV, VALUEV, BOLD_YN == "1",
                            COLOR_YN == "1", Color.FromName(COLORV));
                    }
                    catch
                    {
                        // ignored
                    }

                    FormatGridView();
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1,
                        V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);

                    FilterControl.FormatGridView(dataGridView1);

                    ViewReportIndex();

                    dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    _load_data_success = false;
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

        }

        #endregion ==== LoadData MakeReport ====

        
        
        
        

         #region Linh tinh        

        
        private void btnHuy_Click(object sender, EventArgs e)
        {
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
        
        private bool _radioChange = false;

        private void rbtLanguage_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;
            _radioChange = true;
            txtReportTitle.Text = (rTiengViet.Checked ? _reportTitle : rEnglish.Checked ? _reportTitle2 : (_reportTitle + "/" + _reportTitle2));
            SetFormReportFilter();
            if (((RadioButton) sender).Checked)
            {
                if (ReloadData == "1")
                    MakeReport();
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
                this.ShowErrorException(GetType() + ".PrintGrid", ex);
            }
        }
        

        void ViewReport()
        {
            if (_ds == null) return;
            try
            {
                _rpDoc = new ReportDocument();
                _rpDoc.Load(ReportFileFull);

                _tblv = ConvertTable(_tbls[current_report_index], F_START, F_FIXCOLUMN);
                var new_ds = new DataSet();
                _tblv.TableName = "DataTable1";
                new_ds.Tables.Add(_tblv);
                new_ds.Tables.Add(_tbl2.Copy());
                _rpDoc.SetDataSource(new_ds);

                SetAllReportParams();

                crystalReportViewer1.ReportSource = _rpDoc;
                crystalReportViewer1.Show();
                crystalReportViewer1.Zoom(1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewReport " + ReportFileFull, ex);
            }
        }

        void ViewReportIndex(int currentReport = 0)
        {
            current_report_index = currentReport;
            if (sobangtach == 0)
            {
                V6ControlFormHelper.ShowMessage(V6Text.NoData);
                return;
            }
            if (ReloadData == "1")
                MakeReport();
            else
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
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Bottom < crystalReportViewer1.Top)
            {
                //Phóng lớn dataGridView
                dataGridView1.BringToFront();
                gridViewSummary1.BringToFront();
                dataGridView1.Height = Height - grbDieuKienLoc.Top - 25;
                dataGridView1.Width = Width - 5;
                dataGridView1.Top = grbDieuKienLoc.Top;
                dataGridView1.Left = grbDieuKienLoc.Left;

                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            else
            {
                dataGridView1.Top = grbDieuKienLoc.Top;
                dataGridView1.Left = grbDieuKienLoc.Right + 5;
                dataGridView1.Height = crystalReportViewer1.Top - grbDieuKienLoc.Top - 25;
                dataGridView1.Width = crystalReportViewer1.Width;
                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
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
                crystalReportViewer1.Top = dataGridView1.Bottom + 25;
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
                            var alctRow = V6BusinessHelper.Select("Alct", "ten_ct,ten_ct2,m_phdbf,m_ctdbf",
                                "ma_CT = '" + selectedMaCt + "'").Data.Rows[0];
                            var amName = alctRow["m_phdbf"].ToString().Trim();
                            var adName = alctRow["m_ctdbf"].ToString().Trim();
                            var fText = (alctRow[V6Setting.IsVietnamese ? "ten_ct" : "ten_ct2"] ?? "").ToString().Trim();
                            if (amName != "" && adName != "")
                            {
                                var f = new V6Form
                                {
                                    WindowState = FormWindowState.Maximized,
                                    Text = fText
                                };
                                
                                    var hoaDonForm = ChungTuF3.GetChungTuControl(selectedMaCt, Name, selectedSttRec);
                                    hoaDonForm.Dock = DockStyle.Fill;
                                    f.Controls.Add(hoaDonForm);
                                
                                f.ShowDialog(this);
                                SetStatus2Text();
                            }
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Không được phép sửa chi tiết!");
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

                var oldKeys = FilterControl.GetFilterParameters();
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

        public override void SetStatus2Text()
        {
            FilterControl.SetStatus2Text();
        }

        private void ReportRViewBase_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (FilterControl._hideFields != null && FilterControl._hideFields.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ds == null)
                {
                    this.ShowErrorMessage(V6Text.NoData);
                    return;
                }

                crystalReportViewer1.PrintReport();
                return;

                var dfp = DefaultPrinter;
                var selectedPrinter = V6ControlFormHelper.PrintRpt(this, _rpDoc, dfp);
                if (!string.IsNullOrEmpty(selectedPrinter) && selectedPrinter != dfp)
                {
                    print_one = true;
                    DefaultPrinter = selectedPrinter;
                }
            }
            catch (Exception ex)
            {
                ShowTopMessage("Có lỗi khi in: " + ex.Message);
                this.WriteExLog(GetType() + ".btnIn_Click", ex);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (sobangtach == 0 || current_report_index == 1)
                return;
            current_report_index = 1;
            //if (FilterControl.ReportType == "T")
            //    cbbLoaiBaoCao.SelectedIndex = 0;
            //else
            //    cbbLoaiBaoCao.SelectedIndex = 1;
            ViewReportIndex(current_report_index);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if(current_report_index>0)
            ViewReportIndex(current_report_index-1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (current_report_index < sobangtach)
                ViewReportIndex(current_report_index + 1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (current_report_index != sobangtach-1)
                ViewReportIndex(sobangtach-1);
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
                new ChartReportForm(FilterControl, ReportFileFullF7, _tbl, _tbl2.Copy(), ReportDocumentParameters)
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

        private void cboMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

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
                f2.UpdateSuccessEvent += data =>
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
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnSuaTTMauBC_Click: " + ex.Message);
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
                f2.InsertSuccessEvent += data =>
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
                SetStatus2Text();
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
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message);
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
                    V6TableHelper.GetDefaultSortField(V6TableName.Alreport), false);
                f.EnableAdd = false;
                f.EnableCopy = false;
                f.EnableDelete = false;
                f.DisableZoomButton();

                f.ShowToForm(this, title);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message);
            }
        }

        private void exportToExcel_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                ShowTopMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = @"Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    FileName = ChuyenMaTiengViet.ToUnSign(ReportTitle)
                };
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        V6Tools.V6Export.Data_Table.ToExcel(_tbl, save.FileName, txtReportTitle.Text, true);
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
                this.ShowErrorMessage(GetType() + ".ExportFail\n" + ex.Message);
            }
        }

        private void exportToExcelTemplate_Click(object sender, EventArgs e)
        {
            string excelColumns = "";
            string excelHeaders = "";
            if (_tbl2 != null && _tbl2.Rows.Count>0)
            {
                if (_tbl2.Columns.Contains("GRDS_V1")) excelColumns = _tbl2.Rows[0]["GRDS_V1"].ToString();
                var f = V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1";
                if (_tbl2.Columns.Contains(f)) excelHeaders = _tbl2.Rows[0][f].ToString();
            }
            V6ControlFormHelper.ExportExcelTemplateD(this, _tbl, _tbl2, "R", ReportDocumentParameters,
                MAU, LAN, ReportFile, ExcelTemplateFileFull, ReportTitle, excelColumns, excelHeaders);
        }

        private void exportToXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                ShowTopMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Xml files (*.xml)|*.xml",
                    FileName = ChuyenMaTiengViet.ToUnSign(ReportTitle)
                };
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        V6Tools.V6Export.Data_Table.ToXmlFile(_tbl, save.FileName);
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

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
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
                    exportToExcel_Click(sender, e);
                }
                else
                {
                    V6ControlFormHelper.ExportExcelTemplateD(this, _tbl, _tbl2, "V", ReportDocumentParameters,
                     MAU, LAN, ReportFile, ExcelTemplateFileView, ReportTitle, showFields, headerString);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ExportToExcelView " + ex.Message);
            }
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            btnNhan.Focus();
        }
    }
}
