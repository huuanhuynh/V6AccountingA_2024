using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.HeThong.V6BarcodePrint;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6ControlManager.FileTool;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;
using V6Tools.V6Export;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class ACOVVBAR2_Control : XuLyBase0
    {
        public ACOVVBAR2_Control()
        {
            InitializeComponent();
        }

        public ACOVVBAR2_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                LoadComboboxSource();
                panel1.SizeChanged += panel1_SizeChanged;
                dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".V6COPY_RA Init: " + ex.Message);
            }
        }

        public DataRow MauInSelectedRow
        {
            get
            {
                return MauInData == null || MauInData.Rows.Count == 0 ? null : MauInData.Rows[0];
            }
        }

        private string Report_GRDSV1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInData.Rows[0]["GRDS_V1"].ToString().Trim();
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

                    result = MauInData.Rows[0]["GRDS_V2"].ToString().Trim();
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

                    result = MauInData.Rows[0]["GRDF_V1"].ToString().Trim();
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

                    result = MauInData.Rows[0]["GRDF_V2"].ToString().Trim();
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

                    result = MauInData.Rows[0]["GRDHV_V1"].ToString().Trim();
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

                    result = MauInData.Rows[0]["GRDHE_V1"].ToString().Trim();
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

                    result = MauInData.Rows[0]["GRDHV_V2"].ToString().Trim();
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

                    result = MauInData.Rows[0]["GRDHE_V2"].ToString().Trim();
                }
                return result;
            }
        }

        void panel1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Height = panel1.Height - 10;
            dataGridView1.Width = panel1.Width - dataGridView1.Left - 5;
            if (FilterControl != null)
            {
                FilterControl.Width = dataGridView1.Left - 5;
            }
        }

        void dataGridView1_EditingControlShowing(object sender0, DataGridViewEditingControlShowingEventArgs e0)
        {
            e0.Control.KeyPress += (sender, e) =>
            {
                try
                {
                    if (dataGridView1.CurrentCell == null) return;
                    var columnName = dataGridView1.CurrentCell.OwningColumn.DataPropertyName;
                    if (columnName == "GIA_IN")
                    {
                        if (e.KeyChar == '.' && dataGridView1.CurrentCell.EditedFormattedValue.ToString().Contains("."))
                        {
                            e.Handled = true;
                        }
                        else if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.')
                        {
                            e.Handled = true;
                        }
                    }
                    else if (columnName == "SL_IN")
                    {
                        if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
                }
            };
        }


        private void ACOVVBAR2_Control_Load(object sender, EventArgs e)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("F7 Word, F9 {0}, F10 {1}.", V6Text.Text("IN"), V6Text.Text("ExportExcel"));
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        
        protected override void Nhan()
        {
            try
            {
                if (_executing)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Executing);
                    return;
                }

                FormManagerHelper.HideMainMenu();
                dataGridView1.DataSource = null;

                CheckForIllegalCrossThreadCalls = false;
                Thread T = new Thread(ExecProc);
                T.IsBackground = true;
                T.Start();

                Timer timerRunAll = new Timer();
                timerRunAll.Interval = 100;
                timerRunAll.Tick += timerRunAll_Tick;
                _executesuccess = false;
                _executing = true;
                timerRunAll.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Nhan: " + ex.Message);
            }
        }

        private string _error = "";
        private void ExecProc()
        {
            try
            {
                _message = "";
                var plist = FilterControl.GetFilterParameters();
                _ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, plist.ToArray()); // "ACOVVBAR2"
                _executing = false;
                _executesuccess = true;
            }
            catch (Exception ex)
            {
                _error = _message + " " + ex.Message;
                _executesuccess = false;
                _executing = false;
            }
        }

        private void timerRunAll_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    ViewData();
                    //dataGridView1.Height = panel1.Height - 10;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column.DataPropertyName == "SL_IN" || column.DataPropertyName == "GIA_IN")
                        {
                            column.ReadOnly = false;
                        }
                        else
                        {
                            column.ReadOnly = true;
                        }
                    }
                    
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage(V6Text.Text("TAIDULIEUXONG") + "!\r\n" + _message);

                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _executesuccess = false;
                    this.ShowErrorMessage(GetType() + ".TimerView" + ex.Message, ex.Source);
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
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                this.ShowErrorMessage(_error);
            }
        }

        private void ViewData()
        {
            try
            {
                if (_ds == null || _ds.Tables.Count == 0) return;
                _tbl = _ds.Tables[0];
                dataGridView1.DataSource = _tbl;
                FormatGridView();
                dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void FormatGridView()
        {
            try
            {
                
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                        _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
                }
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

                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1, V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);
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


        private DataTable GenData()
        {
            var data = new DataTable();
            data.Columns.Add("Code", typeof(string));
            data.Columns.Add("Name", typeof(string));
            data.Columns.Add("Price", typeof(decimal));
            var data0 = _ds.Tables[0];
            foreach (DataRow row in data0.Rows)
            {
                int sl_in = ObjectAndString.ObjectToInt(row["SL_IN"]);
                if (sl_in <= 0) continue;

                string code = row["MA_VV"].ToString().Trim();
                string name = row["TEN_VV"].ToString().Trim();
                decimal price = ObjectAndString.ObjectToDecimal(row["GIA_IN"]);
                for (int i = 0; i < sl_in; i++)
                {
                    var newRow = data.NewRow();
                    newRow[0] = code;
                    newRow[1] = name;
                    newRow[2] = price;
                    data.Rows.Add(newRow);
                }
            }
            return data;
        }

        private void DoPrint()
        {
            try
            {
                dataGridView1.EndEdit();

                var data = GenData();
                PrintBarcodeForm pForm = new PrintBarcodeForm(data);
                
                pForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoPrint: " + ex.Message);
            }
        }

        private void DoExportExcel()
        {
            try
            {
                dataGridView1.EndEdit();
                var data = GenData();
                var saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel file|*.xls";
                saveDialog.Title = "Save as";
                
                if (saveDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var setting = new ExportExcelSetting();
                    setting.data = data;
                    setting.saveFile = saveDialog.FileName;
                    V6Tools.V6Export.ExportData.ToExcel(setting);
                    V6ControlFormHelper.ShowMainMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoExportExcel: " + ex.Message);
            }
        }


        private void XuLyF7() // Xuất Word template
        {
            f9Error = "";
            string ma_vv = "";
            string select_folder = "";
            
            int i = 0;
            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow grow = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (grow.IsSelect())
                    {
                        if (string.IsNullOrEmpty(select_folder))
                        {
                            select_folder = V6ControlFormHelper.ChooseSaveFolder(this, null);
                        }
                        //Add selected row.
                        ma_vv = grow.Cells["MA_VV"].Value.ToString().Trim();

                        var reportFileF7 = _reportFile + "F7";
                        var reportTitleF7 = _program;
                        var reportTitle2F7 = _program;

                        var oldKeys = FilterControl.GetFilterParameters();


                        var view = new ReportR_DX(m_itemId, _program + "F9", _program + "F7", reportFileF7, // Giữ _program + "F9" để dùng lại FilterControl.
                            reportTitleF7, reportTitle2F7, "", "", "");
                        view.PrintMode = V6PrintMode.AutoLoadData;
                        view.CodeForm = CodeForm;
                        SortedDictionary<string, object> filterData = new SortedDictionary<string, object>();
                        filterData.Add("MA_VV", ma_vv);
                        V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, filterData);
                        view.CodeForm = CodeForm;
                        view.Advance = FilterControl.Advance;
                        view.FilterControl.String1 = FilterControl.String1;
                        view.FilterControl.String2 = FilterControl.String2;
                        view.Dock = DockStyle.Fill;
                        view.FilterControl.InitFilters = oldKeys;
                        
                        //view.Close_after_print = true;
                        //view.ShowToForm(this, reportTitleF7, true);
                        view.GenerateProcedureParameters();
                        view.LoadData();
                        var data = view.DataSet.Tables[0];
                        IDictionary<string, string> mappingData = new SortedDictionary<string, string>();
                        foreach (DataRow row in data.Rows)
                        {
                            string vType = row["VTYPE"].ToString().Trim();
                            if (string.IsNullOrEmpty(vType) || vType == "N")//Kiểu số
                            {
                                string NAME = row["NAME"].ToString().Trim().ToUpper();
                                string FCOLUMN = row["FCOLUMN"].ToString().Trim().ToUpper();
                                object value = row["Value"];
                                object fvalue = row["fvalue"];
                                if (mappingData.ContainsKey(NAME))
                                {
                                    ShowMainMessage(V6Text.DataExist + "NAME=" + NAME);
                                }
                                else
                                {
                                    mappingData.Add("{@" + NAME + "}", ObjectAndString.ObjectToString(value));
                                }
                            }
                            else
                            {
                                string NAME = row["NAME"].ToString().Trim().ToUpper();
                                string FCOLUMN = row["FCOLUMN"].ToString().Trim().ToUpper();
                                object value = row[vType + "Value"];
                                object fvalue = row[vType + "fvalue"];
                                if (mappingData.ContainsKey(NAME))
                                {
                                    ShowMainMessage(V6Text.DataExist + "NAME=" + NAME);
                                }
                                else
                                {
                                    mappingData.Add("{@" + NAME + "}", ObjectAndString.ObjectToString(value));
                                }
                            }
                        }

                        string saveFile = "";
                        string name = ma_vv + _program + "F7";
                        string ext = Path.GetExtension(view.WordTemplateFileFull);
                        if (string.IsNullOrEmpty(select_folder))
                        {
                            saveFile = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, name + ext);
                        }
                        else
                        {
                            saveFile = Path.Combine(select_folder, name + ext);
                        }
                        File.Copy(view.WordTemplateFileFull, saveFile, true);

                        WordUtility wf = new WordUtility();
                        wf.ReplaceTextInterop(saveFile, mappingData);
                        view.Dispose();

                        remove_list_g.Add(grow);
                        grow.UnSelect();
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }
            }

            if (f9Error.Length > 0) this.ShowErrorMessage(f9Error);
            ShowMainMessage(V6Text.Finish);


            SetStatus2Text();

            f9Running = false;
        }
        
        
        public override void DoHotKey(Keys keyData)
        {
            if (keyData == Keys.F7){
                XuLyF7();
            }
            else if (keyData == Keys.F9)
            {
                XuLyF9();
                return;
            }
            else if (keyData == Keys.F10)
            {
                DoExportExcel();
                return;
            }
            base.DoHotKey(keyData);
        }

        #region ==== Xử lý F9 ====

        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private V6PrintMode _PrintMode = V6PrintMode.DoNoThing;
        private string _oldDefaultPrinter, _PrinterName;
        private int _PrintCopies;
        private bool printting;
        protected List<DataGridViewRow> remove_list_g = new List<DataGridViewRow>();
        private bool shift_is_down;
        protected override void XuLyF9()
        {
            try
            {
                _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();
                
                if (FilterControl.Check1)
                {
                    if (this.ShowConfirmMessage(V6Text.Text("ASKINLIENTUC")) != DialogResult.Yes)
                    {
                        return;
                    }

                    var printerst = V6ControlFormHelper.ChoosePrinter(this, _oldDefaultPrinter);
                    if (printerst != null)
                    {
                        _PrintMode = V6PrintMode.AutoPrint;
                        _PrinterName = printerst.PrinterName;
                        _PrintCopies = printerst.Copies;
                        V6BusinessHelper.WriteOldSelectPrinter(_PrinterName);
                        printting = true;


                        Timer tF9 = new Timer();
                        tF9.Interval = 500;
                        tF9.Tick += tF9_Tick;
                        CheckForIllegalCrossThreadCalls = false;
                        shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                        remove_list_g = new List<DataGridViewRow>();
                        F9Thread();
                        tF9.Start();
                    }
                    else
                    {
                        ShowMainMessage("Stop");
                    }
                }
                else // TỰ CLICK IN TỪNG BÁO CÁO.
                {
                    _PrintMode = V6PrintMode.AutoLoadData;
                    _PrinterName = null;
                    _PrintCopies = 1;
                    printting = true;

                    Timer tF9 = new Timer();
                    tF9.Interval = 500;
                    tF9.Tick += tF9_Tick;
                    CheckForIllegalCrossThreadCalls = false;
                    shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                    remove_list_g = new List<DataGridViewRow>();
                    F9Thread();
                    tF9.Start();
                }

                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private void F9Thread()
        {
            f9Running = true;
            f9ErrorAll = "";
            string ma_vv = "";
            int i = 0;
            
            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        //Add selected row.
                        ma_vv = row.Cells["MA_VV"].Value.ToString().Trim();
                        
                        var reportFileF9 = _reportFile + "F9";
                        var reportTitleF9 = _program;
                        var reportTitle2F9 = _program;

                        var oldKeys = FilterControl.GetFilterParameters();
                        if (MenuButton.UseXtraReport != shift_is_down)
                        {
                            var view = new ReportR_DX(m_itemId, _program + "F9", _reportProcedure + "F9", reportFileF9,
                                reportTitleF9, reportTitle2F9, "", "", "");
                            view.PrintMode = _PrintMode;
                            view.PrinterName = _PrinterName;
                            view.PrintCopies = _PrintCopies;
                            view.CodeForm = CodeForm;
                            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                            data.Add("MA_VV", ma_vv);
                            V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, data);
                            view.CodeForm = CodeForm;
                            view.Advance = FilterControl.Advance;
                            view.FilterControl.String1 = FilterControl.String1;
                            view.FilterControl.String2 = FilterControl.String2;
                            view.Dock = DockStyle.Fill;
                            view.FilterControl.InitFilters = oldKeys;
                            view.PrintSuccess += delegate
                            {
                                try
                                {
                                    V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "UPDATEF9", view.FilterControl.GetFilterParameters().ToArray());
                                }
                                catch (Exception ex)
                                {
                                    ShowMainMessage("UPDATEF9 Error: " + ex.Message);
                                }
                            };
                            
                            view.Close_after_print = true;
                            view.ShowToForm(this, reportTitleF9, true);
                        }
                        else
                        {
                            var view = new ReportRViewBase(m_itemId, _program + "F9", _program + "F9", reportFileF9,
                                reportTitleF9, reportTitle2F9, "", "", "");
                            view.PrintMode = FilterControl.Check1 ? V6PrintMode.AutoPrint : V6PrintMode.DoNoThing;
                            view.PrinterName = _PrinterName;
                            view.CodeForm = CodeForm;
                            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                            data.Add("MA_VV", ma_vv);
                            V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, data);
                            view.CodeForm = CodeForm;
                            view.Advance = FilterControl.Advance;
                            view.FilterControl.String1 = FilterControl.String1;
                            view.FilterControl.String2 = FilterControl.String2;
                            view.Dock = DockStyle.Fill;
                            view.FilterControl.InitFilters = oldKeys;
                            view.PrintSuccess += delegate
                            {
                                try
                                {
                                    V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "UPDATEF9", view.FilterControl.GetFilterParameters().ToArray());
                                }
                                catch (Exception ex)
                                {
                                    ShowMainMessage("UPDATEF9 Error: " + ex.Message);
                                }
                            };
                            view.PrintMode = V6PrintMode.AutoLoadData;
                            view.Close_after_print = true;
                            view.ShowToForm(this, reportTitleF9, true);
                        }

                        
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }
            }

            
            
            
            SetStatus2Text();

            f9Running = false;
        }
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                printting = false;
                RemoveGridViewRow();
                //  btnNhan.PerformClick();
                try
                {
                    //PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                //InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                SetStatusText("F9 end.");
            }
        }

        /// <summary>
        /// Xóa dòng chứa trong remove_list_g trên gridView1
        /// </summary>
        protected void RemoveGridViewRow()
        {
            try
            {
                while (remove_list_g.Count > 0)
                {
                    dataGridView1.Rows.Remove(remove_list_g[0]);
                    remove_list_g.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".RemoveGridViewRow", ex);
            }
        }
        #endregion xulyF9


        void grid_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid != null && grid.CurrentRow != null)
            {
                if (e.KeyCode == Keys.Space)
                {
                    grid.CurrentRow.ChangeSelect();
                }
            }
        }

    }
}
