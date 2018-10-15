using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.HeThong.V6BarcodePrint;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AINVTBAR3_Control : XuLyBase0
    {
        public AINVTBAR3_Control()
        {
            InitializeComponent();
        }

        public AINVTBAR3_Control(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            InitializeComponent();
            _reportCaption = text;
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

        private DataRow MauInSelectedRow
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
                if (MauInData != null && MauInData.Rows.Count > 0)
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
                if (MauInData != null && MauInData.Rows.Count > 0)
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
                if (MauInData != null && MauInData.Rows.Count > 0)
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
                if (MauInData != null && MauInData.Rows.Count > 0)
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
                if (MauInData != null && MauInData.Rows.Count > 0)
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
                if (MauInData != null && MauInData.Rows.Count > 0)
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
                if (MauInData != null && MauInData.Rows.Count > 0)
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
                if (MauInData != null && MauInData.Rows.Count > 0)
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


        private void AINVTBAR3_Control_Load(object sender, EventArgs e)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9 Chấp nhận.");
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

                HideStatusLable();
                FormManagerHelper.HideMainMenu();
                dataGridView1.DataSource = null;

                CheckForIllegalCrossThreadCalls = false;
                Thread T = new Thread(ExecProc);
                T.IsBackground = true;
                T.Start();

                Timer timerRunAll = new Timer();
                timerRunAll.Interval = 100;
                timerRunAll.Tick += timerRunAll_Tick;
                _success = false;
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
                _ds = V6BusinessHelper.ExecuteProcedure("AINVTBAR3", plist.ToArray());
                _executing = false;
                _success = true;
            }
            catch (Exception ex)
            {
                _error = _message + " " + ex.Message;
                _success = false;
                _executing = false;
            }
        }

        private void timerRunAll_Tick(object sender, EventArgs e)
        {
            if (_success)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    ViewData();
                    
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage("Tải dữ liệu xong!\r\n" + _message);

                    _success = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _success = false;
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
                dataGridView1.SetFrozen(0);
                dataGridView1.DataSource = _tbl;
                FormatGridView();
                dataGridView1.Focus();
                if (_tbl == null || _tbl.Rows.Count == 0)
                {
                    FilterControl.Call2();
                    ShowStatusLable();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected override void DoAfterExecuteSuccess()
        {
            if (FilterControl.Check1 && dataGridView1.Rows.Count > 0)
            {
                XuLyF9();
            }
        }

        private void FormatGridView()
        {
            try
            {
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

        private void ShowStatusLable()
        {

            lblStatus.Text = "Mã đã sử dụng!";
            lblStatus.Visible = true;

            HideStatusLable();
        }
        
        private void HideStatusLable()
        {
            lblStatus.Visible = false;
            lblStatus.Text = "";
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

                string code = row["MA_VT"].ToString().Trim();
                string name = row["TEN_VT"].ToString().Trim();
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
                    V6Tools.V6Export.ExportData.ToExcel(data, saveDialog.FileName, null, false);
                    V6ControlFormHelper.ShowMainMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoExportExcel: " + ex.Message);
            }
        }
        
        
        public override void DoHotKey(Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                XuLyF9();
                return;
            }
            if (keyData == Keys.F8)
            {
                FilterControl.Call3();
                return;
            }
            base.DoHotKey(keyData);
        }

        #region ==== Xử lý F9 ====

        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        protected List<DataGridViewRow> remove_list_g = new List<DataGridViewRow>();
        protected override void XuLyF9()
        {
            try
            {
                PrintDialog p = new PrintDialog();
                p.AllowCurrentPage = false;
                p.AllowPrintToFile = false;
                p.AllowSelection = false;
                p.AllowSomePages = false;
                p.PrintToFile = false;
                p.UseEXDialog = true; //Fix win7


                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF9_Tick;
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                F9Thread();
                //Thread t = new Thread(F9Thread);
                //t.SetApartmentState(ApartmentState.STA);
                //t.IsBackground = true;
                //t.Start();
                tF9.Start();
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

            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        remove_list_g.Add(row);
                    }

                    DataGridViewRow row0 = dataGridView1.Rows[0];
                    string ma_lo = row0.Cells["Ma_lo"].Value.ToString().Trim();
                    var plist = FilterControl.GetFilterParameters();
                    plist.Add(new SqlParameter("@cMa_lo", ma_lo));
                    plist.Add(new SqlParameter("@cMa_dvcs", V6Login.Madvcs));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    V6BusinessHelper.ExecuteProcedure(_program + "F9", plist.ToArray());
                    FilterControl.Call1();
                }
            }
            catch (Exception ex)
            {
                f9ErrorAll += ex.Message;
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
                RemoveGridViewRow();
                FilterControl.Call3();
                //  btnNhan.PerformClick();
                try
                {
                    //V6Tools.PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                //InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F9 Xử lý xong!");
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

       
        #region ==== Mẫu báo cáo ====
        protected string _reportCaption, _reportCaption2;
        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        private DataTable MauInData;

        private void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_reportFile, "", "", "");
        }


        private void btnThemMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (MauInData != null && MauInData.Rows.Count > 0) return;

                ConfirmPasswordV6 f_v6 = new ConfirmPasswordV6();
                if (f_v6.ShowDialog(this) == DialogResult.OK)
                {
                    SortedDictionary<string, object> data0 = null;
                    //var viewt = new DataView(MauInData);
                    //viewt.RowFilter = "mau='" + MAU + "'" + " and lan='" + LAN + "'";
                    var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _reportFile},
                        {"MAU", "VN"},
                        {"LAN", "V"},
                        {"REPORT", _reportFile}
                    };
                    if (MauInData == null || MauInData.Rows.Count == 0)
                    {
                        data0 = new SortedDictionary<string, object>();
                        data0.AddRange(keys);
                        data0["CAPTION"] = _reportCaption;
                        data0["CAPTION2"] = _reportCaption;
                        data0["TITLE"] = _reportCaption;
                        data0["FirstAdd"] = "1";
                    }

                    var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Add, keys, data0);
                    f2.InsertSuccessEvent += (data) =>
                    {
                        //cap nhap thong tin
                        LoadComboboxSource();
                    };
                    f2.ShowDialog(this);
                    SetStatus2Text();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ThemMauBC_Click: ", ex);
            }
            SetStatus2Text();
        }

        private void btnSuaTTMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (MauInData == null || MauInData.Rows.Count == 0) return;
                var row0 = MauInData.Rows[0];
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", row0["MA_FILE"].ToString().Trim()},
                        {"MAU", row0["MAU"].ToString().Trim()},
                        {"LAN", row0["LAN"].ToString().Trim()},
                        {"REPORT", row0["REPORT"].ToString().Trim()}
                    };
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, keys, null);
                f2.UpdateSuccessEvent += (data) =>
                {
                    //cap nhap thong tin
                    LoadComboboxSource();
                };
                f2.ShowDialog(this);
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnSuaTTMauBC_Click", ex);
            }
            SetStatus2Text();
        }

        #endregion
    }
}
