using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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

namespace V6ControlManager.FormManager.ReportManager.DanhMuc
{
    public partial class DanhMucReportForm : V6Form
    {
        #region Biến toàn cục
        DataGridViewPrinter MyDataGridViewPrinter;
        private ReportDocument _rpDoc;
        private string _tableName, _ma_File, _reportTitle, _reportTitle2;
        //private DataTable tbl;
        private V6TableStruct _tStruct;
        private List<SqlParameter> pList;

        private DataTable MauInData;
        private DataView MauInView;
        private DataSet _ds;
        private DataTable _tbl, _tbl2;


        /// <summary>
        /// MA_FILE, MAU, LAN, REPORT
        /// </summary>
        private SortedDictionary<string, object> AlbcKeys
        {
            get
            {
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _ma_File},
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

        private string LAN
        {
            get { return rTiengViet.Checked ? "V" : rEnglish.Checked ? "E" : "B"; }
        }
        
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

        private bool print_one = false;

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
                if (cboMauIn.Items.Count > 0 && cboMauIn.SelectedIndex >= 0)
                {
                    var data = MauInView.ToTable();
                    result = data.Rows[cboMauIn.SelectedIndex]["Reload_data"].ToString().Trim();
                }
                return result;
            }
        }
        #endregion 

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="reportFile">use as ma_file</param>
        /// <param name="reportTitle">title</param>
        /// <param name="reportTitle2">title2</param>
        public DanhMucReportForm(string tableName, string reportFile, string reportTitle, string reportTitle2)
        {
            _tableName = tableName;
            _ma_File = reportFile;
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

            string[] fields = V6Lookup.GetDefaultLookupFields(_tableName);
            MadeFilterControls(fields);
            
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
            //LoadDefaultData(4, "", _Ma_File, m_itemId, "");

            if (!V6Login.IsAdmin)
            {
                exportToExcel.Visible = false;
                //viewDataToolStripMenuItem.Visible = false;
            }
            
            Ready();
        }

        private void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_ma_File);
            
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

        ///// <summary>
        ///// Gán dữ liệu mặc định lên form.
        ///// </summary>
        ///// <param name="loai">1ct 4report</param>
        ///// <param name="mact"></param>
        ///// <param name="madm"></param>
        ///// <param name="itemId"></param>
        ///// <param name="adv"></param>
        //protected void LoadDefaultData(int loai, string mact, string madm, string itemId, string adv = "")
        //{
        //    try
        //    {
        //        var data = GetDefaultData(V6Setting.Language, 4, mact, madm, itemId, adv);
        //        var data0 = new SortedDictionary<string, object>();
        //        data0.AddRange(data);
        //        V6ControlFormHelper.SetFormDataDictionary(this, data0, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteExLog(GetType() + ".LoadDefaultData", ex);
        //    }
        //}
        ///// <summary>
        ///// Tải dữ liệu và trả về DefaultData.
        ///// </summary>
        ///// <param name="lang"></param>
        ///// <param name="loai">1ct, 4report</param>
        ///// <param name="mact"></param>
        ///// <param name="madm"></param>
        ///// <param name="itemId"></param>
        ///// <param name="adv"></param>
        ///// <returns></returns>
        //private SortedDictionary<string, string> GetDefaultData(string lang, int loai, string mact, string madm, string itemId, string adv = "")
        //{
        //    if (defaultData != null && defaultData.Count > 0) return defaultData;
        //    if (alinitData == null || alinitData.Rows.Count == 0)
        //        alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
        //    var result = new SortedDictionary<string, string>();
        //    foreach (DataRow row in alinitData.Rows)
        //    {
        //        var cell = row["Default" + lang]; if (cell == null) continue;
        //        var value = cell.ToString().Trim(); if (value == "") continue;

        //        var name = row["NameVal"].ToString().Trim().ToUpper();
        //        result[name] = value;
        //    }
        //    defaultData = result;
        //    return result;
        //}

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
            
        }

        private void MadeFilterControls(params string[] fields)
        {
            string err = "";
            try
            {
                int i = 0;
                foreach (string field in fields)
                {
                    try
                    {
                        MadeLineControl(i, field.Trim());
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
                //this.ShowErrorMessage(GetType() + ".MadeControls error!" + err);
            }
        }

        private void MadeLineControl(int index, string fieldName)
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
            lineControl.Location = new Point(10, 10 + 25 * index);
            panel1.Controls.Add(lineControl);
        }

        
        private void btnNhan_Click(object sender, EventArgs e)
        {
            if (_dataLoading)
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
                this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message);
            }
        }
        private void GenerateKeys()
        {
            //var keys = GetData();
            //sKey = SqlGenerator.GenWhere2(_tStruct, keys, "like");
            sKey = QueryString;
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

        private void AddProcParams()
        {
            var _order = V6TableHelper.GetDefaultSortField(V6TableHelper.ToV6TableName(_tableName));
            
            pList.Add(new SqlParameter("@cTable", _tableName));
            pList.Add(new SqlParameter("@cOrder", _order));
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

            foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
            {
                _rpDoc.SetParameterValue(item.Key, item.Value);
            }
        }
        
        #region ==== LoadData MakeReport ====
        bool _dataLoaded;
        bool _dataLoading;
        private string error_message = "";
        void LoadData()
        {
            try
            {
                _dataLoading = true;
                _dataLoaded = false;
                error_message = "";
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
                _dataLoaded = true;
                _dataLoading = false;
            }
            catch (Exception ex)
            {
                error_message = "Load Data Error\n"+ex.Message;
                _tbl = null;
                _tbl2 = null;
                _ds = null;
                _dataLoading = false;
                _dataLoaded = false;
            }
        }
        private void MakeReport2()
        {
            pList = new List<SqlParameter>();
            AddProcParams();//Add các key khác
            var tLoadData = new Thread(LoadData);
            tLoadData.Start();
            timerViewReport.Start();
        }
        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_dataLoaded)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                try
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _tbl;

                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1, V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);

                    ViewReport();

                    dataGridView1.Focus();
                }
                catch (Exception ex)
                {
                    _dataLoaded = false;
                    timerViewReport.Stop();
                    this.ShowErrorMessage(GetType() + ".TimerView: " + ex.Message);
                }
            }
            else if (_dataLoading)
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
            //if (_hardExit)
            //{
            //}
            //else
            //{
            //    try
            //    {
            //        //if (V6Message.Show(myMessage.Exit"), "V6Soft", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //        //{
            //        //    e.Cancel = true;
            //        //}
            //        //else
            //    }
            //    catch
            //    {
            //        // ignored
            //    }
            //}
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
                //this.ShowWarningMessage("NoData");
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    FileName = ChuyenMaTiengViet.ToUnSign(ReportTitle)
                };
                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {   
                        V6Tools.V6Export.Data_Table.ToExcel(_tbl, save.FileName, Name, true);
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

        private void exportToExcelTemplate_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.ExportExcelTemplate(_tbl, _tbl2, ReportDocumentParameters,
                MAU, LAN, ReportFile, ExcelTemplateFileFull, ReportTitle);
        }

        private void exportToXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                //this.ShowWarningMessage("NoData");
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Xml files (*.xml)|*.xml",
                    FileName = ChuyenMaTiengViet.ToUnSign(ReportTitle)
                };
                if (save.ShowDialog() == DialogResult.OK)
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
                    MakeReport2();
                else
                    ViewReport();
            }
        }

        private void crystalReportViewer1_DoubleClick(object sender, EventArgs e)
        {
            
        }
        

        private bool SetupThePrinting()
        {
            PrintDialog MyPrintDialog = new PrintDialog
            {
                AllowCurrentPage = false,
                AllowPrintToFile = false,
                AllowSelection = false,
                AllowSomePages = false,
                PrintToFile = false,
                ShowHelp = false,
                ShowNetwork = false
            };

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
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
                //this.ShowWarningMessage("NoData");
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

        private void viewGrid_Click(object sender, EventArgs e)
        {
            dataGridView1.ToFullForm("Xem");
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
                crystalReportViewer1.Zoom(1);
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
        
        private void btnIn_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.PrintReport();
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
                MakeReport2();
            else
                ViewReport();
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
                var f = new FormRptEditor {rptPath = ReportFileFull};
                f.ShowDialog();
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

        
    }
}
