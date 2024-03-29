﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
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

namespace V6ControlManager.FormManager.ReportManager.DanhMuc
{
    public partial class DanhMucReportForm : V6Form
    {
        #region Biến toàn cục
        private ReportDocument _rpDoc0;
        private string _tableName, _Ma_File, _reportTitle, _reportTitle2;
        /// <summary>
        /// Advance filter get albc
        /// </summary>
        public string Advance = "";
        //private DataTable tbl;
        private V6TableStruct _tStruct;
        private List<SqlParameter> pList;

        public DataTable MauInData;
        public DataView MauInView;
        public DataSet _ds;
        public DataTable _tbl1, _tbl2;

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
        /// Tên file excel tự động xuất.
        /// </summary>
        public string AutoExportExcel = null;

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

        public AlbcConfig _albcConfig = new AlbcConfig();

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
        /// Zoom mặc định cho crystalreport khi in chứng từ.
        /// <para>Crystal: 1 PageWidth, 2 AllPage, >=50 is %</para>
        /// <para>Xtra: 1 = PageWidth, 2 AllPage, >=50 is this %</para>
        /// </summary>
        public int ExtraInfo_PrintVCzoom
        {
            get
            {
                int result = 100;
                if (EXTRA_INFOR.ContainsKey("PRINTVCZOOM")) result = ObjectAndString.ObjectToInt(EXTRA_INFOR["PRINTVCZOOM"]);
                return result;
            }
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
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    result = _albcConfig.RPT_DIR;
                    if (result != "") result += result + @"\";
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
        
        private string QueryString
        {
            get
            {
                string result = "";
                foreach (FilterLineDynamic c in panel1.Controls)
                {
                    if (c.IsSelected)
                        result += " And " + c.Query;
                }
                if (result.Length > 4) result = result.Substring(4);
                return result;
            }
        }

        private string sKey;

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

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="itemId"></param>
        /// <param name="reportFile">use as ma_file</param>
        /// <param name="reportTitle">title</param>
        /// <param name="reportTitle2">title2</param>
        public DanhMucReportForm(string tableName, string itemId, string reportFile, string reportTitle,
            string reportTitle2)
        {
            _tableName = tableName;
            m_itemId = itemId;
            _Ma_File = reportFile;
            _reportTitle = reportTitle;
            _reportTitle2 = reportTitle2;
            
            //_reportFileF5 = reportFileF5;
            //_reportTitleF5 = reportTitleF5;
            //_reportTitle2F5 = reportTitle2F5;

            _tStruct = V6BusinessHelper.GetTableStruct(_tableName);
            //_hardExit = hardExit;
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

                var fields_vvar_filter = V6Lookup.GetValueByTableName(_tableName, "vLfScatter");
                MadeControls(_tableName, fields_vvar_filter);
                CheckRightReport();
                //InvokeFormEvent(FormDynamicEvent.INIT);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2", ex);
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
                dataGridView1.Height = Height - dataGridView1.Top - 40;
                if (no_print)
                {
                    dataGridView1.LockCopy = true;
                    while (contextMenuStrip1.Items.Count > 0)
                    {
                        contextMenuStrip1.Items[0].Enabled = false;
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
                //LoadDefaultData(4, "", _Ma_File, m_itemId, "");
                //GetSumCondition();

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
                            //if (!key3.Contains("2")) exportToExcelView.Visible = false;
                            if (!key3.Contains("3")) exportToExcelMenu.Visible = false;
                            if (!key3.Contains("4")) exportToXmlMenu.Visible = false;
                            if (!key3.Contains("5")) printGridMenu.Visible = false;
                            //if (!key3.Contains("6")) viewDataToolStripMenuItem.Visible = false;
                            if (!key3.Contains("7")) exportToPdfMenu.Visible = false;
                            if (!key3.Contains("8")) viewInvoiceInfoMenu.Visible = false;
                        }
                    }
                    else//Chưa gửi ItemID
                    {
                        contextMenuStrip1.Visible = false;
                    }
                }

                if (key3.Length > 0)
                    switch (key3[0])
                    {
                        case '1': DefaultMenuItem = exportToExcelTemplateMenu; break;
                        //case '2': DefaultMenuItem = exportToExcelView; break;
                        case '3': DefaultMenuItem = exportToExcelMenu; break;
                        case '4': DefaultMenuItem = exportToXmlMenu; break;
                        case '5': DefaultMenuItem = printGridMenu; break;
                        //case '6': DefaultMenuItem = viewDataMenu; break;
                        case '7': DefaultMenuItem = exportToPdfMenu; break;
                        //case '8': DefaultMenuItem = viewInvoiceInfoMenu; break;
                    }

                //InvokeFormEvent(FormDynamicEvent.INIT2);
                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2", ex);
            }
        }
        private ToolStripMenuItem DefaultMenuItem = null;
        
        private void MadeControls(string tableName, string fields_vvar_filter)
        {
            string err = "";
            try
            {
                _tStruct = V6BusinessHelper.GetTableStruct(tableName);
                panel1.AddMultiFilterLine(_tStruct, fields_vvar_filter);
            }
            catch (Exception ex)
            {
                err += "\n" + ex.Message;
            }
            if (err.Length > 0)
            {
                this.WriteToLog(GetType() + ".MadeControls error!", err);
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
                    btnThemMauBC.Enabled = false;
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

        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
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
        
        private void btnNhan_Click(object sender, EventArgs e)
        {
            if (_executing)
            {
                return;
            }

            try
            {
                GenerateKeys();
                btnNhanImage = btnNhan.Image;
                MakeReport2(V6PrintMode.DoNoThing, null, 1);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message);
            }
        }

        private void GenerateKeys()
        {
            sKey = QueryString;
        }

        private bool GenerateProcedureParameters()
        {
            var _order = V6TableHelper.GetDefaultSortField(V6TableHelper.ToV6TableName(_tableName));
            
            pList.Add(new SqlParameter("@cTable", _tableName));
            pList.Add(new SqlParameter("@cOrder", _order));
            pList.Add(new SqlParameter("@cKey", "1=1" + (sKey.Length>0?" And " +sKey:"")));
            return true;
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

            var rptExtraParametersD = GetRptParametersD(Extra_para, LAN);
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
        }

        /// <summary>
        /// Lấy tham số cho rpt theo định nghĩa Extra_para
        /// </summary>
        /// <param name="ExtraParameterInfo"></param>
        /// <param name="LAN"></param>
        /// <returns></returns>
        public IDictionary<string, object> GetRptParametersD(string ExtraParameterInfo, string LAN)
        {
            try
            {
                var result = new SortedDictionary<string, object>();
                var lineList = GetFilterLineList(panel1);
                try
                {
                    if (string.IsNullOrEmpty(ExtraParameterInfo)) return null;
                    var sss = ExtraParameterInfo.Split('~');

                    foreach (string ss in sss)
                    {
                        DefineInfo di = new DefineInfo(ss);
                        if (string.IsNullOrEmpty(di.Name)) continue;

                        if (di.Ptype.ToUpper() == "TABLE2")
                        {
                            var dataTable2 = _ds.Tables[1];
                            var _tbl2Row = dataTable2.Rows[0];
                            if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBANNT")
                            {
                                var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);// "T_TIEN_NT2_IN"]);
                                var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien_nt2_in, LAN, ma_nt);
                            }
                            else if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBAN")
                            {
                                var t_tien2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);//"T_TIEN2_IN"]);
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien2_in, LAN,
                                    V6Options.M_MA_NT0);
                            }
                            // Đọc tiền bằng chữ V
                            else if (di.Name.ToUpper() == "SOTIENVIETBANGCHUV_TIENBANNT")
                            {
                                var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);
                                var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien_nt2_in, "V", ma_nt);
                            }
                            else if (di.Name.ToUpper() == "SOTIENVIETBANGCHUV_TIENBAN")
                            {
                                var t_tien2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien2_in, "V",
                                    V6Options.M_MA_NT0);
                            }
                            // Đọc tiền bằng chữ E
                            else if (di.Name.ToUpper() == "SOTIENVIETBANGCHUE_TIENBANNT")
                            {
                                var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);
                                var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien_nt2_in, "E", ma_nt);
                            }
                            else if (di.Name.ToUpper() == "SOTIENVIETBANGCHUE_TIENBAN")
                            {
                                var t_tien2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien2_in, "E",
                                    V6Options.M_MA_NT0);
                            }
                            else
                            {
                                result[di.Name.ToUpper()] = _tbl2Row[di.Field];
                            }
                        }
                        else if (di.Ptype.ToUpper() == "PARENT")
                        {

                        }
                        else if (di.Ptype.ToUpper() == "FILTER")
                        {
                            var lineKey = "line" + di.Field.ToUpper();
                            if (lineList.ContainsKey(lineKey))
                            {
                                var line = lineList[lineKey];
                                if (line.IsSelected)
                                {
                                    result[di.Name] = line.ObjectValue;
                                }
                                else
                                {
                                    if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                        result[di.Name] = 0;
                                    else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                        result[di.Name] = new DateTime(1900, 1, 1);
                                    else
                                        result[di.Name] = "";
                                }
                            }
                        }
                        else if (di.Ptype.ToUpper() == "FILTER_BROTHER")
                        {
                            var lineKey = "line" + di.Field.ToUpper();
                            if (lineList.ContainsKey(lineKey))
                            {
                                var line = lineList[lineKey];
                                if (line is FilterLineVvarTextBox)
                                {
                                    var lineV = line as FilterLineVvarTextBox;

                                    var vvar_data = lineV.VvarTextBox.Data;
                                    if (line.IsSelected == false)
                                    {
                                        vvar_data = null;
                                    }

                                    if (vvar_data != null && vvar_data.Table.Columns.Contains(di.Fname))
                                    {
                                        if (line.IsSelected)
                                        {
                                            //Bỏ qua giá trị rỗng.
                                            if (di.NotEmpty && string.IsNullOrEmpty("" + line.ObjectValue)) continue;
                                            result[di.Name] = vvar_data[di.Fname];
                                        }
                                        else
                                        {
                                            if (di.NotEmpty) continue;

                                            if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                                result[di.Name] = 0;
                                            else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                                result[di.Name] = new DateTime(1900, 1, 1);
                                            else
                                                result[di.Name] = "";
                                        }
                                    }
                                    else
                                    {
                                        if (di.NotEmpty) continue;
                                        // Tuanmh Null loi
                                        if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                            result[di.Name] = 0;
                                        else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                            result[di.Name] = new DateTime(1900, 1, 1);
                                        else
                                            result[di.Name] = "";
                                    }
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception)
                {

                }
                return result;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetRptParametersD", ex);
                return null;
            }
        }

        public SortedDictionary<string, FilterLineBase> GetFilterLineList(Control control)
        {
            SortedDictionary<string, FilterLineBase> result = new SortedDictionary<string, FilterLineBase>();
            foreach (Control control1 in control.Controls)
            {
                var line = control1 as FilterLineBase;
                if (line != null)
                {
                    result.Add("line" + line.FieldName.ToUpper(), line);
                }
                else
                {
                    result.AddRange(GetFilterLineList(control1));
                }
            }
            return result;
        }

        #region ==== LoadData MakeReport ====
        
        private string error_message = "";
        void LoadData()
        {
            try
            {
                _executing = true;
                _executesuccess = false;
                error_message = "";
                _ds = V6BusinessHelper.ExecuteProcedure("VPA_R_AL_ALL", pList.ToArray());
                SetTBLdata();
                _executesuccess = true;
                _executing = false;
            }
            catch (Exception ex)
            {
                error_message = "Load Data Error\n"+ex.Message;
                _tbl1 = null;
                _tbl2 = null;
                _ds = null;
                _executing = false;
                _executesuccess = false;
            }
        }

        private void SetTBLdata()
        {
            if (_ds.Tables.Count > 0)
            {
                _tbl1 = _ds.Tables[0];
                _tbl1.TableName = "DataTable1";
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
        }

        private void MakeReport2( )
        {
            pList = new List<SqlParameter>();
            GenerateProcedureParameters();//Add các key khác
            var tLoadData = new Thread(LoadData);
            tLoadData.Start();
            timerViewReport.Start();
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
                    dataGridView1.SetFrozen(0);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _tbl1;

                    FormatGridView();
                    ShowReport();

                    dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    _executesuccess = false;
                    timerViewReport.Stop();
                    this.ShowErrorMessage(GetType() + ".TimerView: " + ex.Message);
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
                this.ShowErrorMessage(error_message);
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

        private void FormatGridView()
        {
            try
            {
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1,
                        _albcConfig.GRDS_V1, _albcConfig.GRDF_V1, _albcConfig.GRDH_LANG_V1);
                    dataGridView1.SetFrozen(_albcConfig.FROZENV);
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
            Close();
        }
        
        #endregion Linh tinh

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
                var save = new SaveFileDialog
                {
                    Filter = "Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    FileName = ChuyenMaTiengViet.ToUnSign(GetExportFileName())
                };
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var setting = new ExportExcelSetting();
                        setting.data = _tbl1;
                        setting.saveFile = save.FileName;
                        setting.title = Name;
                        setting.isDrawLine = true;
                        V6Tools.V6Export.ExportData.ToExcel(setting);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorMessage(GetType() + ".ExportFail: " + ex.Message);
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
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Error!\n" + ex.Message);
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
                    txtReportTitle.Text = (rTiengViet.Checked ? _reportTitle : rEnglish.Checked ? _reportTitle2 : (_reportTitle + "/" + _reportTitle2));
                    SetFormReportFilter();
                    if (MauInView.Count > 0 && cboMauIn.SelectedIndex >= 0)
                    {
                        _albcConfig = new AlbcConfig(MauInSelectedRow.ToDataDictionary());
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

        private void viewGrid_Click(object sender, EventArgs e)
        {
            dataGridView1.ToFullForm("Xem");
        }

        void ShowReport()
        {
            try
            {
                //panel1.LoadDataFinish(_ds);
                All_Objects["_ds"] = _ds;
                InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                dataGridView1.SetFrozen(0);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _tbl1;

                
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                        _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
                }
                FormatGridView();
                //gridViewTopFilter1.MadeFilterItems();

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

                if (!string.IsNullOrEmpty(AutoExportExcel))
                {
                    var setting = new ExportExcelSetting();
                    setting.data = _tbl1;
                    setting.data2 = _tbl2;
                    setting.reportParameters = ReportDocumentParameters;
                    setting.albcConfigData = _albcConfig.DATA;
                    setting.xlsTemplateFile = ExcelTemplateFileFull;
                    setting.saveFile = AutoExportExcel;
                    V6ControlFormHelper.ExportExcelTemplate(this, setting);
                    Dispose();
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
        
        void ViewReport()
        {
            try
            {
                CleanUp();
                var rpDoc = new ReportDocument();
                rpDoc.Load(ReportFileFull);
                rpDoc.SetDataSource(_ds);

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
        
        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            _tbl1 = null;
        }

        private void F_ResizeEnd(object sender, EventArgs e)
        {
            if (Width < 800)
                Width = 800;
            if (Height < 600)
                Height = 600;
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnIn_Click " + V6Text.Text("LOIIN"), ex);
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

            //GetSumCondition();
            _albcConfig = new AlbcConfig(MauInSelectedRow.ToDataDictionary());
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
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _Ma_File},
                        {"MAU", MAU},
                        {"LAN", LAN},
                        {"REPORT", ReportFile}
                    };
                
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, keys, null);
                f2.AfterInitControl += f_AfterInitControl;
                f2.InitFormControl(this);
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
            try
            {
                if (crystalReportViewer1.IsInvisibleTag())
                {
                    return;
                }
                var f = new FormRptEditor {rptPath = ReportFileFull};
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SuaMau_Click: " + ex.Message);
            }
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
            {
                DefaultMenuItem.PerformClick();
            }
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
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (crystalReportViewer1.Visible)
            {
                // Phóng lớn dataGridView
                dataGridView1.BringToFront();
                dataGridView1.Height = crystalReportViewer1.Bottom - 5;
                dataGridView1.Top = 5;
                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                crystalReportViewer1.Visible = false;
            }
            else if (crystalReportViewer1.IsVisibleTag())
            {
                dataGridView1.Top = grbDieuKienLoc.Bottom;
                dataGridView1.Height = crystalReportViewer1.Top - 3 - dataGridView1.Top;
                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                crystalReportViewer1.Visible = true;
            }
            else
            {
                dataGridView1.Top = grbDieuKienLoc.Bottom;
                dataGridView1.Height = Height - dataGridView1.Top - 40;
                dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
        }

        private void crystalReportViewer1_DoubleClick(object sender, EventArgs e)
        {
            if (crystalReportViewer1.Top > dataGridView1.Bottom)
            {
                // Phóng lớn crystalReportViewer1
                crystalReportViewer1.BringToFront();
                crystalReportViewer1.Height = crystalReportViewer1.Bottom - 5;
                crystalReportViewer1.Top = 5;
            }
            else
            {
                crystalReportViewer1.Height = crystalReportViewer1.Bottom - dataGridView1.Bottom - 5;
                crystalReportViewer1.Top = dataGridView1.Bottom + 5;
            }
        }
        
    }
}
