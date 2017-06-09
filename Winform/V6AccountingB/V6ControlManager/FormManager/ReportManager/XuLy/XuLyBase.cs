using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class XuLyBase : V6FormControl
    {
        #region Biến toàn cục
        protected DataGridViewPrinter _myDataGridViewPrinter;
        protected List<DataGridViewRow> remove_list_g = new List<DataGridViewRow>();
        protected List<DataRow> remove_list_d = new List<DataRow>(); 

        protected string _reportProcedure, _reportFile;
        protected string _program, _reportCaption, _reportCaption2;
        protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        private DataTable MauInData;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        protected List<SqlParameter> _pList;

        public bool ViewDetail { get; set; }


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
        #endregion 
        public XuLyBase()
        {
            InitializeComponent();
            //MyInit();
        }

        public XuLyBase(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2, bool viewDetail,
            string reportFileF5 = "", string reportTitleF5 = "", string reportTitle2F5 = "")
        {
            m_itemId = itemId;
            _program = program;
            _reportProcedure = reportProcedure;
            _reportFile = reportFile;
            _reportCaption = reportCaption;
            _reportCaption2 = reportCaption2;

            _reportFileF5 = reportFileF5;
            _reportTitleF5 = reportTitleF5;
            _reportTitle2F5 = reportTitle2F5;

            ViewDetail = viewDetail;
            
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            Text = _reportCaption;
            
            AddFilterControl(_program);
            if (ViewDetail)
                ShowDetailGridView();
            //else
            //    FixGridView1();

            LoadComboboxSource();

            if (!V6Login.IsAdmin)
            {
                exportToExcel.Visible = false;
                //viewDataToolStripMenuItem.Visible = false;
            }

        }

        private void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_reportFile);
        }

        private void FixGridViewSize()
        {
            try
            {
                if (!ViewDetail) dataGridView1.Height = Height - 10;
                dataGridView1.Width = Width - dataGridView1.Left - 3;
                dataGridView2.Width = dataGridView1.Width;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixGridViewSize", ex);
            }
        }

        void XuLyBase_SizeChanged(object sender, EventArgs e)
        {
            FixGridViewSize();
        }

        private void ShowDetailGridView()
        {
            try
            {
                dataGridView2.Height = Height/2 - 10;
                dataGridView2.Top = Height / 2 + 10;
                dataGridView2.Visible = true;
                dataGridView1.Height = Height / 2;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public FilterBase FilterControl { get; set; }
        protected void AddFilterControl(string program)
        {
            FilterControl = Filter.Filter.GetFilterControl(program);
            panel1.Controls.Add(FilterControl);
            //FilterControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            FilterControl.Focus();
        }
        
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (_dataLoading)
            {
                return;
            }

            try
            {
                FormManagerHelper.HideMainMenu();
                btnNhanImage = btnNhan.Image;
                MakeReport2();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message);
            }
        }

        /// <summary>
        /// Lấy các tham số cho proc từ filterControl vào _pList
        /// </summary>
        /// <returns></returns>
        protected bool GenerateProcedureParameters()
        {
            try
            {
                _pList = new List<SqlParameter>();
                _pList.AddRange(FilterControl.GetFilterParameters());
                
                return true;
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("GenerateProcedureParameters: " + ex.Message);
                return false;
            }
        }

        protected void LockButtons()
        {
            btnNhan.Enabled = false;
            btnHuy.Enabled = false;
        }

        protected void UnlockButtons()
        {
            btnNhan.Enabled = true;
            btnHuy.Enabled = true;
        }

        #region ==== LoadData MakeReport ====

        protected bool _dataLoaded;
        protected bool _dataLoading;
        void LoadData()
        {
            try
            {
                _dataLoading = true;
                _dataLoaded = false;
                _ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, _pList.ToArray());
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
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadData XuLyBase", ex);
                _tbl = null;
                _tbl2 = null;
                _ds = null;
                _dataLoaded = false;
            }
            _dataLoading = false;
        }

        void TinhToan()
        {
            try
            {
                _dataLoading = true;
                V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, _pList.ToArray());
                
                _dataLoaded = true;
                _dataLoading = false;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhToan Error!\n" + ex.Message);
                _dataLoading = false;
                _dataLoaded = false;
            }
        }
        /// <summary>
        /// GenerateProcedureParameters();//Add các key
        /// var tLoadData = new Thread(LoadData);
        /// tLoadData.Start();
        /// timerViewReport.Start();
        /// </summary>
        protected virtual void MakeReport2()
        {
            if (GenerateProcedureParameters()) //Add các key khác
            {
                if (Load_Data)
                {
                    var tLoadData = new Thread(LoadData);
                    tLoadData.Start();
                    timerViewReport.Start();
                }
                else
                {
                    // Tinh toan
                    var tLoadData = new Thread(TinhToan);
                    tLoadData.Start();
                    timerViewReport.Start();
                }
            }
        }

        protected bool Load_Data = true;
        protected virtual void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_dataLoaded)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    FilterControl.LoadDataFinish(_ds);
                    if (Load_Data)
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = _tbl;
                        
                        FormatGridViewBase();
                        FormatGridViewExtern();
                        dataGridView1.Focus();
                    }
                    else
                    {
                        //Thong bao tinh toan xong
                        V6ControlFormHelper.ShowMessage(V6Text.Finish);
                    }
                    _dataLoaded = false;
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();

                    _dataLoaded = false;
                    this.ShowErrorException(GetType() + ".TimerView", ex);
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
            }
        }

        /// <summary>
        /// Hàm format luôn chạy trong base sau khi set dữ liệu cho grid 1.
        /// </summary>
        protected void FormatGridViewBase()
        {
            //Header
            if (_tbl != null)
            {
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

            //Format mới
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1, V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);
            if (ViewDetail) V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, Report_GRDSV2, Report_GRDFV2, V6Setting.IsVietnamese ? Report_GRDHV_V2 : Report_GRDHE_V2);
        }

        /// <summary>
        /// Hàm luôn gọi sau hàm FormatGridViewBase.
        /// </summary>
        protected virtual void FormatGridViewExtern()
        {
            //try
            //{
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (row.Cells["TS0"].ToString() == "1")
            //            row.DefaultCellStyle.Font = new Font(Parent.Font, FontStyle.Bold);

            //        row.Select();
            //        row.DefaultCellStyle.BackColor = Color.FromArgb(247, 192, 91);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowErrorMessage(GetType() + ".FormatGridViewExtern: " + ex.Message);
            //}
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
            var data = dataGridView1.Focused ? _tbl : _tbl2;
            if (data == null)
            {
                ShowTopMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog {Filter = @"Excel files (*.xls)|*.xls|Xlsx|*.xlsx"};
                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {   
                        V6Tools.V6Export.Data_Table.ToExcel(data, save.FileName, _reportCaption, true);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorException(GetType() + ".ExportFail", ex);
                        return;
                    }
                    this.ShowInfoMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ExportFail", ex);
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

            if (myPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            MyPrintDocument.DocumentName = _reportCaption;
            MyPrintDocument.PrinterSettings = myPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = myPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

            _myDataGridViewPrinter = new DataGridViewPrinter(dataGridView1, MyPrintDocument,
                this.ShowConfirmMessage("PrintAlignmentCenter") == DialogResult.Yes,
                true, _reportCaption, new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

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

        public override bool DoHotKey0(Keys keyData)
        {
            var aControl = ActiveControl;
            if (aControl is V6VvarTextBox)
            {
                return true;
            }

            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            else if (keyData == Keys.F3 && FilterControl.F3)
            {
                XuLyHienThiFormSuaChungTuF3();
            }
            else if (keyData == Keys.F4 && FilterControl.F4)
            {
                XuLyBoSungThongTinChungTuF4();
            }
            //else if (keyData == Keys.F5 && FilterControl.F5)
            //{
            //    if(dataGridView1.Focused || dataGridView2.Focused) XuLyXemChiTietF5();
            //    else return base.DoHotKey0(keyData);
            //}
            else if (keyData == Keys.F7 && FilterControl.F7)
            {
                XuLyF7();
            }
            else if (keyData == Keys.F8 && FilterControl.F8)
            {
                XuLyF8();
            }
            else if (keyData == Keys.F9 && FilterControl.F9)
            {
                XuLyF9();
            }
            else if (keyData == Keys.F10 && FilterControl.F10)
            {
                XuLyF10();
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }
        
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F5 && FilterControl.F5)
                {
                    if (dataGridView1.Focused) XuLyXemChiTietF5();
                }
                else if (e.KeyData == (Keys.Control | Keys.A))
                {
                    e.Handled = true;
                    XuLyCtrA();
                }
                else if (e.KeyData == (Keys.Control | Keys.U))
                {
                    XuLyCtrU();
                }
                else if (e.KeyData == (Keys.Space))
                {
                    XuLySpacebar();
                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException("XuLyBase GridView1 KeyDown", ex);
            }
        }

        protected virtual void XuLyHienThiFormSuaChungTuF3()
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
                            var alctRow = V6BusinessHelper.Select("Alct", "ten_ct,ten_ct2,m_phdbf,m_ctdbf,m_gtdbf",
                                "ma_CT = '" + selectedMaCt + "'").Data.Rows[0];
                            var amName = (alctRow["m_phdbf"] ?? "").ToString().Trim();
                            var adName = (alctRow["m_ctdbf"] ?? "").ToString().Trim();
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


                                f.ShowDialog();
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
                this.ShowErrorException("XuLyBase XuLyHienThiFormSuaChungTu", ex);
            }
        }

        
        protected virtual void XuLyBoSungThongTinChungTuF4()
        {
            
        }
        protected virtual void XuLyXemChiTietF5()
        {
            var oldKeys = FilterControl.GetFilterParameters();
            var view = new XuLyBase(m_itemId, _program + "F5", _program + "F5", _reportFile, _reportCaption, _reportCaption2, false);
            view.CodeForm = CodeForm;
            view.Dock = DockStyle.Fill;
            view.FilterControl.InitFilters = oldKeys;
            view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
            view.btnNhan_Click(null, null);
            view.ShowToForm(_reportCaption, true);

            //var f = new V6Form();
            //f.WindowState = FormWindowState.Maximized;
            //f.Controls.Add(view);
            //view.btnNhan_Click(null, null);
            //f.ShowDialog();
            SetStatus2Text();
        }

        protected virtual void XuLyF7()
        {
            throw new NotImplementedException();
        }

        protected virtual void XuLyF8()
        {
            throw new NotImplementedException();
        }

        protected virtual void XuLyF9()
        {
            throw new NotImplementedException();
        }

        protected virtual void XuLyF10()
        {
            throw new NotImplementedException();
        }

        protected virtual void XuLyCtrA()
        {
            if (!_Ctrl_A) return;
            dataGridView1.SelectAllRow();
        }

        protected virtual void XuLyCtrU()
        {
            if (!_Ctrl_U) return;
            dataGridView1.UnSelectAllRow();
        }

        protected virtual void XuLySpacebar()
        {
            if (!_SpaceBar) return;
            var row = dataGridView1.CurrentRow;
            if (row != null)
            {
                row.ChangeSelect();
            }
        }

        private void UpdateGridView2(DataGridViewRow row)
        {
            ViewDetails(row);
            FormatGridView2();
        }

        protected virtual void ViewDetails(DataGridViewRow row)
        {
            //Viết code ở lớp kế thừa
        }

        protected virtual void FormatGridView2()
        {
            //Viết code ở lớp kế thừa
        }

        public virtual void GridView1CellEndEdit(DataGridViewRow row, string FIELD, object fieldData)
        {
            //Viết code ở lớp kế thừa
        }

        public virtual void GridView2CellEndEdit(DataGridViewRow row, string FIELD, object fieldData)
        {
            //Viết code ở lớp kế thừa
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
        
        /// <summary>
        /// Dùng chứa dòng xóa đi trên gridView1
        /// </summary>
        protected void RemoveDataRows(DataTable table)
        {
            try
            {
                while (remove_list_d.Count > 0)
                {
                    table.Rows.Remove(remove_list_d[0]);
                    remove_list_d.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType().Name + ".RemoveGridViewRows", ex);
            }
        }

        public override void SetStatus2Text()
        {
            FilterControl.SetStatus2Text();
        }

        private void XuLyBase_VisibleChanged(object sender, EventArgs e)
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
        
        protected int _oldIndex = -1;
        /// <summary>
        /// Bật tắt chức năng chọn 1 dòng trên gridView bằng spacebar
        /// </summary>
        protected bool _SpaceBar = true;
        /// <summary>
        /// Bật tắt chức năng chọn tất cả trên gridView bằng Ctrl+A
        /// </summary>
        protected bool _Ctrl_A = true;
        /// <summary>
        /// Bật tắt chức năng bỏ chọn tất cả trên gridView bằng Ctrl+U
        /// </summary>
        protected bool _Ctrl_U = true;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var row = dataGridView1.CurrentRow;
                if (ViewDetail && row != null && _oldIndex != row.Index)
                {
                    _oldIndex = row.Index;
                    UpdateGridView2(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Gridview1Select", ex);
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var index = dataGridView1.SelectedCells[0].RowIndex;
                _oldIndex = index;

                UpdateGridView2(dataGridView1.Rows[index]);
                
            }
            else
            {
                dataGridView2.DataSource = null;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            var field = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
            GridView1CellEndEdit(row, field.ToUpper(), row.Cells[field].Value);
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView2.Rows[e.RowIndex];
            var field = dataGridView2.Columns[e.ColumnIndex].DataPropertyName;
            GridView1CellEndEdit(row, field.ToUpper(), row.Cells[field].Value);
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
                        //Chọn cái mới.
                        //var reportFileNew = data["REPORT"].ToString().Trim();
                        //var dataV = MauInView.ToTable();
                        //for (int i = 0; i < dataV.Rows.Count; i++)
                        //{
                        //    if (dataV.Rows[i]["Report"].ToString().Trim().ToUpper() == reportFileNew.ToUpper())
                        //    {
                        //        cboMauIn.SelectedIndex = i;
                        //        break;
                        //    }
                        //}
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
                    //Chọn cái mới.
                    //var reportFileNew = data["REPORT"].ToString().Trim();
                    //var dataV = MauInView.ToTable();
                    //for (int i = 0; i < dataV.Rows.Count; i++)
                    //{
                    //    if (dataV.Rows[i]["Report"].ToString().Trim().ToUpper() == reportFileNew.ToUpper())
                    //    {
                    //        cboMauIn.SelectedIndex = i;
                    //        break;
                    //    }
                    //}
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
        
        
    }
}
