﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.Filter.Xuly;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Export;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AINVTBAR5_Control : XuLyBase0
    {
        public AINVTBAR5_Control()
        {
            InitializeComponent();
        }

        public AINVTBAR5_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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
            dataGridView2.Height = panel1.Height - 10 - dataGridView1.Height;
            dataGridView1.Width = panel1.Width - dataGridView1.Left - 5;
            dataGridView2.Width = panel1.Width - dataGridView1.Left - 5;
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

        private void AINVTBAR5_Control_Load(object sender, EventArgs e)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("F9 {0}, F10 {1}.", V6Text.Text("Accept"), V6Text.Text("TaoPX"));
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
                _executesuccess = false;
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
                if (V6Message.Show(V6Text.Text("CHUYCHUATAOPXF10") + "\n" + V6Text.BackConfirm, V6Text.Warning, 500, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 2, this)
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
                dataGridView1.SetFrozen(0);
                dataGridView1.DataSource = _tbl;
                FormatGridView(dataGridView1);
                FormatGridView(dataGridView2);
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
                
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                        _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
                }

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
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
                }
                f = gridView.Columns["TIEN2"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
                }
                f = gridView.Columns["GIA2"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
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
                
                if (saveDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var setting = new ExportExcelSetting();
                    setting.data = data;
                    setting.saveFile = saveDialog.FileName;

                    V6Tools.V6Export.ExportData.ToExcel(setting);
                    if (V6Options.AutoOpenExcel)
                    {
                        V6ControlFormHelper.OpenFileProcess(saveDialog.FileName);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage(V6Text.ExportFinish);
                    }
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
            if (f9Running) return;
            btnNhanImage = btnNhan.Image;

            try
            {
                if (ctype == "1" && dataGridView2.DataSource != null && dataGridView2.RowCount > 0)
                {
                    this.ShowWarningMessage(V6Text.UnFinished);
                    return;
                }

                ctype = "0";
                if (_tblGridView2 == null)
                {
                    _gc_td3 = V6BusinessHelper.GetServerDateTime().ToString("yyyyMMddHHmmssFFF");
                }
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
                    plist.Add(new SqlParameter("@cgc_td3", _gc_td3));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F9", plist.ToArray());
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
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;

                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;

                AddDataToGridView2(_tbl);
                RemoveGridViewRow();
                FilterControl.Call3();
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

        #region ==== Xử lý F10 ====

        private bool f10Running;
        private string f10Error = "";
        private string f10ErrorAll = "";
        protected List<DataGridViewRow> remove_list_g_f10 = new List<DataGridViewRow>();
        protected override void XuLyF10()
        {
            if (f10Running) return;
            btnNhanImage = btnNhan.Image;

            try
            {
                Timer tF10 = new Timer();
                tF10.Interval = 500;
                tF10.Tick += tF10_Tick;
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g_f10 = new List<DataGridViewRow>();
                //F10Thread();
                Thread t = new Thread(F10Thread);
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
                tF10.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF10: " + ex.Message);
            }
        }

        private string ctype = "";
        private void F10Thread()
        {
            f10Running = true;
            f10ErrorAll = "";

            try
            {
                //if (dataGridView1.RowCount > 0)
                {
                    string l_gc_td3 = "";
                    Dictionary<string, string> temp = new Dictionary<string, string>();
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        string gc_td3 = row.Cells["GC_TD3"].Value.ToString().Trim();
                        if (temp.ContainsKey(gc_td3))
                        {
                            DoNothing();
                        }
                        else
                        {
                            temp.Add(gc_td3, gc_td3);
                            l_gc_td3 += "," + gc_td3;
                        }
                        remove_list_g_f10.Add(row);
                    }
                    if (l_gc_td3.Length > 0) l_gc_td3 = l_gc_td3.Substring(1);

                    var plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@cgc_td3", _gc_td3));
                    plist.Add(new SqlParameter("@lgc_td3", l_gc_td3));//list
                    plist.Add(new SqlParameter("@ctype", ctype));
                    plist.Add(new SqlParameter("@dngay_ct", ((AINVTBAR5)FilterControl).dateNgay_ct.Date.ToString("yyyyMMdd")));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F10", plist.ToArray());
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
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;

                var cError = f10Error;
                f10Error = f10Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F10 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;

                RemoveGridViewRow_f10();
                _tblGridView2 = null;
                //dataGridView2.DataSource = null;
                _gc_td3 = null;
                ((AINVTBAR5)FilterControl).txtMalo.Enabled = true;
                ((AINVTBAR5)FilterControl).txtMalo.Focus();
                //FilterControl.Call3();
                //  btnNhan.PerformClick();
                try
                {
                    //PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                //InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (f10ErrorAll.Length > 0 ? "Error: " : "")
                    + f10ErrorAll);

                SetStatusText("F10 " + V6Text.Finish);
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
        
        public void LocPhieuCu(DateTime ngay)
        {
            try
            {
                if (dataGridView2.DataSource == null || dataGridView2.Rows.Count == 0)
                {
                    ctype = "1";
                    var txtMaLo = ((AINVTBAR5) FilterControl).txtMalo;
                    _gc_td3 = V6BusinessHelper.GetServerDateTime().ToString("yyyyMMddHHmmssFFF");

                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@cTable", "V_ALLO1"));
                    plist.Add(new SqlParameter("@cOrder", "MA_VT"));
                    plist.Add(new SqlParameter("@cKey", "ngay_td1='" + ngay.ToString("yyyyMMdd") + "'"));
                    plist.Add(new SqlParameter("@cGC_TD3", _gc_td3));
                    _tblGridView2 = V6BusinessHelper.ExecuteProcedure("AINVTBAR5F10A", plist.ToArray()).Tables[0];
                    if (_tblGridView2 != null && _tblGridView2.Rows.Count > 0)
                    {
                        txtMaLo.Enabled = false;
                    }
                    else
                    {
                        txtMaLo.Enabled = true;
                    }
                    //foreach (DataRow row in data2.Rows)
                    //{
                    //    row["GC_TD3"] = _gc_td3;
                    //}
                    dataGridView2.DataSource = _tblGridView2;
                    FormatGridView(dataGridView2);
                }
                else
                {
                    this.ShowWarningMessage(V6Text.UnFinished);
                }
            }
            catch (Exception ex)
            {
                 this.ShowErrorException(GetType() + ".LocPhieuCu", ex);
            }
        }
    }
}
