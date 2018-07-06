using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6ReportControls;
using V6RptEditor;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.SoDu
{
    public partial class SoDuReportForm : V6Form
    {
        #region Biến toàn cục
        DataGridViewPrinter MyDataGridViewPrinter;
        private ReportDocument _rpDoc;
        private string _tableName, _ma_File, _reportTitle, _reportTitle2, _inifilter;
        private V6TableStruct _tStruct;
        private List<SqlParameter> pList;

        private DataTable MauInData;
        private DataView MauInView;
        private DataSet _ds;
        private DataTable _tbl, _tbl2;
        private string MAU
        {
            get { return rTienViet.Checked ? "VN" : "FC"; }
        }

        private string LAN
        {
            get { return rTiengViet.Checked ? "V" : rEnglish.Checked ? "E" : "B"; }
        }

        private DataRow MauInSelectedRow
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
                if (_extraInfor == null || _extraInfor.Count == 0)
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

        private string ReportFile
        {
            get
            {
                var result = _ma_File;
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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = MauInSelectedRow["Title"].ToString().Trim();
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
                       + _ma_File + ".rpt";//_reportFile gốc
                }
                return result;
            }
        }

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
                       + _ma_File + ".xls";
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
        #endregion 

        public SoDuReportForm(string tableName, string reportFile, string reportTitle, string reportTitle2,string inifilter)
        {
            V6ControlFormHelper.AddLastAction(GetType() + " " + tableName);
            _tableName = tableName;
            _ma_File = reportFile;
            _reportTitle = reportTitle;
            _reportTitle2 = reportTitle2;
            _inifilter = inifilter;
            
            _tStruct = V6BusinessHelper.GetTableStruct(_tableName);
            
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
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

            //string[] fields = V6Lookup.GetDefaultLookupFields(_tableName);

           string[] fields= new [] {"",""};

            switch (_tableName.ToUpper())
            {
                case "ABVT":

                    fields = "MA_KHO,MA_VT".Split(',');
                    break;
                case "ABLO":

                    fields = "MA_KHO,MA_VT,MA_LO".Split(',');
                    break;
                case "ABKH":

                    fields = "MA_KH".Split(',');
                    break;
                case "ABTK":

                    fields = "TK".Split(',');
                    break;
                default:

                    fields = new [] {"",""};
                    break;
            }

            MadeControls(fields);
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
                        //if (!key3.Contains("2")) exportToExcelView.Visible = false;
                        if (!key3.Contains("3")) exportToExcel.Visible = false;
                        if (!key3.Contains("4")) exportToXmlToolStripMenuItem.Visible = false;
                        if (!key3.Contains("5")) printGrid.Visible = false;
                        //if (!key3.Contains("6")) viewDataToolStripMenuItem.Visible = false;
                        if (!key3.Contains("7")) exportToPdfToolStripMenuItem.Visible = false;
                    }
                }
                else//Chưa gửi ItemID
                {
                    contextMenuStrip1.Visible = false;
                }
            }

            Ready();
        }

        private void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_ma_File, "", "", "");
            
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
                btnThemMauBC.Enabled = false;
            }
        }
        private void SetFormReportFilter()
        {
            try
            {
                MauInView.RowFilter = "mau='" + MAU + "'" + " and lan='" + LAN + "'"
                    + (chkHienTatCa.Checked?"":" and status='1'");

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
            
        }

        private void SettingValues()
        {
            
        }

        private void MadeControls(params string[] fields)
        {
            string err = "";
            try
            {
                int i = 0;
                foreach (string field in fields)
                {
                    try
                    {
                        MadeControl(i, field.Trim());
                        i++;
                    }
                    catch (Exception e1)
                    {
                        err += "\n" + i + " " + field + ": " + e1.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                err += "\n" + ex.Message;
            }
            if (err.Length > 0)
            {
                Logger.WriteToLog(GetType() + ".MadeControls error!" + err);
            }
        }
        private void MadeControl(int index, string fieldName)
        {
            var lineControl = new FilterLineDynamic
            {
                FieldName = fieldName.ToUpper(),
                FieldCaption = CorpLan2.GetFieldHeader(fieldName)
            };
            if (_tStruct.ContainsKey(fieldName.Trim().ToUpper()))
            {
                if (",nchar,nvarchar,ntext,char,varchar,text,xml,"
                    .Contains("," + _tStruct[fieldName.ToUpper()].sql_data_type_string + ","))
                {
                    lineControl.AddTextBox();
                }
                else if (",date,smalldatetime,datetime,"
                    .Contains("," + _tStruct[fieldName.ToUpper()].sql_data_type_string + ","))
                {
                    lineControl.AddDateTimePick();
                }
                else
                {
                    lineControl.AddNumberTextBox();
                }
            }
            lineControl.Location = new Point(10, 10 + 30 * index);
            panel1.Controls.Add(lineControl);
        }

        
        private void btnNhan_Click(object sender, EventArgs e)
        {
            if (Data_Loading)
            {
                return;
            }

            try
            {
                GenerateKeys();
                btnNhanImage = btnNhan.Image;
                MakeReport2();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ReportError " + ReportFileFull, ex);
            }
        }

        private void GenerateKeys()
        {
            sKey = QueryString;
            if (_inifilter.Length>0)
            {
                if (sKey.Length>0)
                {
                    sKey += " and " + _inifilter;                    
                }
                else
                    sKey = _inifilter;
            }
        }
        private void AddProcParams()
        {
            pList.Add(new SqlParameter("@cTable", _tableName));
            pList.Add(new SqlParameter("@cOrder", "Nam"));
            pList.Add(new SqlParameter("@cKey", "1=1" + (sKey.Length>0?" And " +sKey:"")));
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
            string errors = "";
            foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
            {
                try
                {
                    _rpDoc.SetParameterValue(item.Key, item.Value);
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
        
        private string error_message = "";
        void LoadData()
        {
            try
            {
                Data_Loading = true;
                _load_data_success = false;
                _ds = V6BusinessHelper.ExecuteProcedure("VPA_R_AL_ALL", pList.ToArray());
                if (_ds.Tables.Count > 0)
                {
                    _tbl = _ds.Tables[0];
                    _tbl.TableName = "DataTable1";
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
                error_message = "Load Data Error\n" + ex.Message;
                _tbl = null;
                _tbl2 = null;
                _ds = null;
                _load_data_success = false;
            }
            Data_Loading = false;
        }

        private void MakeReport2()
        {
            pList = new List<SqlParameter>();
            AddProcParams();//Add các key khác
            _load_data_success = false;
            Data_Loading = true;
            var tLoadData = new Thread(LoadData);
            tLoadData.Start();
            timerViewReport.Start();
        }
        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_load_data_success)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                try
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _tbl;

                    ViewReport();

                    dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    _load_data_success = false;
                    timerViewReport.Stop();
                    this.ShowErrorException(GetType() + ".TimerView " + ReportFileFull, ex);
                }
            }
            else if (Data_Loading)
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
        #endregion ==== LoadData MakeReport ====

        
        
        
        

         #region Linh tinh        

        
        private void FormBaoCaoHangTonKho_V2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        #endregion Linh tinh

        private void exportToExcel_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                return;
            }

            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    FileName = ChuyenMaTiengViet.ToUnSign(ReportTitle)
                };
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {   
                        V6Tools.V6Export.ExportData.ToExcel(_tbl, save.FileName, Name, true);
                    }
                    catch (Exception ex)
                    {
                        this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
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
            if (((RadioButton)sender).Checked) ViewReport();
        }

        private void crystalReportViewer1_DoubleClick(object sender, EventArgs e)
        {
            
        }
        

        private bool SetupThePrinting()
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (MyPrintDialog.ShowDialog(this) != DialogResult.OK)
                return false;

            MyPrintDocument.DocumentName = Text;
            MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

            MyDataGridViewPrinter = new DataGridViewPrinter(dataGridView1, MyPrintDocument,
                this.ShowConfirmMessage("PrintAlignmentCenter") == DialogResult.Yes,
                true, Text, new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }

        private void MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more)
                e.HasMorePages = true;
        }

        private void printGrid_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                return;
            }

            try
            {
                MyPrintDocument.PrintPage += new PrintPageEventHandler(MyPrintDocument_PrintPage);
                if (SetupThePrinting())
                {
                    MyPrintDocument.Print();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
        void ViewReport()
        {
            try
            {
                _rpDoc = new ReportDocument();
                _rpDoc.Load(ReportFileFull);

                _rpDoc.SetDataSource(_ds);

                SetAllReportParams();

                crystalReportViewer1.ReportSource = _rpDoc;
                crystalReportViewer1.Show();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewReport " + ReportFileFull, ex);
            }
        }
        
        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            _tbl = null;
            _rpDoc = null;
            GC.Collect();
        }

        private void F_ResizeEnd(object sender, EventArgs e)
        {
            if (Width < 800)
                Width = 800;
            if (Height < 600)
                Height = 600;
        }

        private void exportToExcelTemplate_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.ExportExcelTemplate_ChooseFile(this, _tbl, _tbl2, ReportDocumentParameters,
                MAU, LAN, ReportFile, ExcelTemplateFileFull, ReportTitle);
        }

        private void exportToXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                ShowTopLeftMessage(V6Text.NoData);
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
                        V6Tools.V6Export.ExportData.ToXmlFile(_tbl, save.FileName);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorException(GetType() + ".ExportFail " + ReportFileFull + " " + save.FileName, ex);
                        return;
                    }
                    this.ShowInfoMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".exportToXmlToolStripMenuItem_Click " + ReportFileFull, ex);
            }
        }

        private void btnSuaTTMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _ma_File},
                        {"MAU", MAU},
                        {"LAN", LAN},
                        {"REPORT", ReportFile}
                    };
                
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, keys, null);
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SuaTTMauBC_Click " + ReportFileFull, ex);
            }
        }

        private void btnThemMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _ma_File},
                        {"MAU", MAU},
                        {"LAN", LAN},
                        {"REPORT", ReportFile}
                    };
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Add, keys, null);
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ThemMauBC_Click " + ReportFileFull, ex);
            }
        }

        private void btnSuaMau_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new FormRptEditor { rptPath = ReportFileFull };
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SuaMau_Click " + ReportFileFull, ex);
            }
        }

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadComboboxSource();
        }

        private void cboMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

            txtReportTitle.Text = ReportTitle;
            ViewReport();
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            btnNhan.Focus();
        }

        private void exportToPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_rpDoc == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                if (V6ControlFormHelper.ExportRptToPdf_As(this, _rpDoc, ReportTitle))
                {
                    ShowMainMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Export PDF", ex);
            }
        }
        
    }
}
