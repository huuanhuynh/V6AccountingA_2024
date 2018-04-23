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
    public partial class AINVTBAR5_Control : XuLyBase0
    {
        public AINVTBAR5_Control()
        {
            InitializeComponent();
        }

        public AINVTBAR5_Control(string itemId, string program, string reportProcedure, string reportFile, string text)
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
            dataGridView2.Height = panel1.Height - 10 - dataGridView1.Height;
            dataGridView1.Width = panel1.Width - dataGridView1.Left - 5;
            dataGridView2.Width = panel1.Width - dataGridView1.Left - 5;
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


        private void AINVTBAR5_Control_Load(object sender, EventArgs e)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9 Chấp nhận, F10 Tạo phiếu xuất.");
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

        protected override void Huy()
        {
            if (dataGridView2.Rows.Count > 0)
            {
                if (V6Message.Show("Chú ý: chưa tạo phiếu xuất (F10).\n" + "Bạn vẫn muốn quay ra?", "Cảnh báo!", 500, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 2, this)
                    != DialogResult.Yes)
                {
                    return;
                }
            }
            base.Huy();
        }

        private string _error = "";
        private void ExecProc()
        {
            try
            {
                _message = "";
                var plist = FilterControl.GetFilterParameters();
                _ds = V6BusinessHelper.ExecuteProcedure("AINVTBAR5", plist.ToArray());
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
                dataGridView1.DataSource = _tbl;
                FormatGridView(dataGridView1);
                dataGridView1.Focus();
                if (_tbl == null || _tbl.Rows.Count == 0)
                {
                    FilterControl.Call2();//Alert mã đã sử dụng.
                    ShowStatusLable();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private string _gc_td3;
        private void AddDataToGridView2(DataTable data)
        {
            if (_tblGridView2 == null)
            {
                _tblGridView2 = data.Clone();
                _gc_td3 = V6BusinessHelper.GetServerDateTime().ToString("yyyMMddHHmmssFFF");
            }
            
            foreach (DataRow row in data.Rows)
            {
                var dataDic = row.ToDataDictionary();
                dataDic["GC_TD3"] = _gc_td3;
                _tblGridView2.AddRow(dataDic);
            }
            
            dataGridView2.DataSource = _tblGridView2;
            FormatGridView(dataGridView2);
        }

        protected override void DoAfterExecuteSuccess()
        {
            if (FilterControl.Check1 && dataGridView1.Rows.Count > 0)
            {
                XuLyF9();
            }
        }

        private void FormatGridView(V6ColorDataGridView gridView)
        {
            try
            {
                string FIELDV, OPERV, BOLD_YN, COLOR_YN, COLORV;
                object VALUEV;
                V6BusinessHelper.GetFormatGridView(_program, "REPORT", out FIELDV, out OPERV, out VALUEV, out BOLD_YN, out COLOR_YN, out COLORV);
                //Color.MediumAquamarine
                V6ControlFormHelper.FormatGridView(gridView, FIELDV, OPERV, VALUEV, BOLD_YN == "1", COLOR_YN == "1", Color.FromName(COLORV));

                //Header
                var fieldList = (from DataColumn column in _tbl.Columns select column.ColumnName).ToList();

                var fieldDic = CorpLan2.GetFieldsHeader(fieldList);
                for (int i = 0; i < gridView.ColumnCount; i++)
                {
                    if (fieldDic.ContainsKey(gridView.Columns[i].DataPropertyName.ToUpper()))
                    {
                        gridView.Columns[i].HeaderText =
                            fieldDic[gridView.Columns[i].DataPropertyName.ToUpper()];
                    }
                }
                //Format
                var f = gridView.Columns["so_luong"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
                }
                f = gridView.Columns["TIEN2"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
                }
                f = gridView.Columns["GIA2"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIA"];
                }

                FilterControl.FormatGridView(gridView);

                V6ControlFormHelper.FormatGridViewAndHeader(gridView, Report_GRDSV1, Report_GRDFV1, V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);
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
        
        private void DoExportExcel()
        {
            try
            {
                dataGridView1.EndEdit();
                var data = GenData();
                var saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel file|*.xls";
                saveDialog.Title = "Save as";
                //saveDialog.InitialDirectory = Application.StartupPath;
                if (saveDialog.ShowDialog(this) == DialogResult.OK)
                {
                    V6Tools.V6Export.Data_Table.ToExcel(data, saveDialog.FileName, null, false);
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
            if (keyData == Keys.F10)
            {
                XuLyF10();
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
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
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
                    FilterControl.Call1();//ClearAll();
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
                AddDataToGridView2(_tbl);
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

        #region ==== Xử lý F10 ====

        private bool f10Running;
        private string f10Error = "";
        private string f10ErrorAll = "";
        protected List<DataGridViewRow> remove_list_g_f10 = new List<DataGridViewRow>();
        protected override void XuLyF10()
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


                Timer tF10 = new Timer();
                tF10.Interval = 500;
                tF10.Tick += tF10_Tick;
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g_f10 = new List<DataGridViewRow>();
                F10Thread();
                //Thread t = new Thread(F10Thread);
                //t.SetApartmentState(ApartmentState.STA);
                //t.IsBackground = true;
                //t.Start();
                tF10.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF10: " + ex.Message);
            }
        }

        private void F10Thread()
        {
            f10Running = true;
            f10ErrorAll = "";

            try
            {
                //if (dataGridView1.RowCount > 0)
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        remove_list_g_f10.Add(row);
                    }

                    var plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@cgc_td3", _gc_td3));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    V6BusinessHelper.ExecuteProcedure(_program + "F10", plist.ToArray());
                    //FilterControl.Call1();
                }
            }
            catch (Exception ex)
            {
                f10ErrorAll += ex.Message;
            }

            SetStatus2Text();

            f10Running = false;
        }

        void tF10_Tick(object sender, EventArgs e)
        {
            if (f10Running)
            {
                var cError = f10Error;
                f10Error = f10Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F10 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                
                RemoveGridViewRow_f10();
                _tblGridView2 = null;
                //dataGridView2.DataSource = null;
                _gc_td3 = null;
                //FilterControl.Call3();
                //  btnNhan.PerformClick();
                try
                {
                    //V6Tools.PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                //InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (f10ErrorAll.Length > 0 ? "Error: " : "")
                    + f10ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F10 Xử lý xong!");
            }
        }

        /// <summary>
        /// Xóa dòng chứa trong remove_list_g_f10 trên gridView1
        /// </summary>
        protected void RemoveGridViewRow_f10()
        {
            try
            {
                while (remove_list_g_f10.Count > 0)
                {
                    dataGridView2.Rows.Remove(remove_list_g_f10[0]);
                    remove_list_g_f10.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".RemoveGridViewRow", ex);
            }
        }
        #endregion xulyF10
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
