using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.DanhMucManager.ChangeCode;
using V6ControlManager.FormManager.ReportManager.SoDu;
using V6ControlManager.FormManager.SoDuManager.Add_Edit;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools.V6Convert;
using SortOrder = System.Windows.Forms.SortOrder;

namespace V6ControlManager.FormManager.SoDuManager
{
    public partial class SoDuView2 : V6FormControl
    {
        public SoDuView2()
        {
            InitializeComponent();
        }
        public SoDuView2(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
        }
        public SoDuView2(string title, string maCt, string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            Title = title;
            GetInfo(maCt);
            
            SelectResult = new V6SelectResult();
            dataGridView1.DataSource = new DataTable();
        }

        private void GetInfo(string maCt)
        {
            _maCt = maCt;
            DataTable alct = V6BusinessHelper.GetAlct(maCt);
            _tableName = V6BusinessHelper.GetAMname(alct.Rows[0]);
            CurrentTable = V6TableHelper.ToV6TableName(_tableName);
            _tableName2 = V6BusinessHelper.GetADname(alct.Rows[0]);
            if(maCt == "SO2")
            {
                _tableName3 = "ADCTTS";
                _tableName4 = "ADCTTSBP";
            }
            _hideColumnDic = _categories.GetHideColumns(_tableName);
            InitFilter = V6Login.GetInitFilter(_tableName);
        }

        private void SoDuView2_Load(object sender, EventArgs e)
        {
            LoadTable(CurrentTable, "");
            FormManagerHelper.HideMainMenu();
            dataGridView1.Focus();
        }

        private readonly V6Categories _categories = new V6Categories();
        private SortedDictionary<string, string> _hideColumnDic;
        private string _maCt;
        private string _tableName, _tableName2, _tableName3, _tableName4;

        public string CurrentSttRec { get; set; }

        public int CurrentIndex { get; set; }


        [DefaultValue(V6TableName.None)]
        public V6TableName CurrentTable { get; set; }
        //public V6TableName CurrentTable2 { get; set; }
        public V6SelectResult SelectResult { get; set; }
        
        public SortedDictionary<string, DataTable> ADTables;
        public DataTable AD;//Luon la bang copy

        public bool EnableAdd
        {
            get { return btnThem.Enabled; }
            set { btnThem.Enabled = value; }
        }

        public bool EnableEdit
        {
            get { return btnSua.Enabled; }
            set { btnSua.Enabled = value; }
        }

        public bool EnableDelete
        {
            get { return btnXoa.Enabled; }
            set { btnXoa.Enabled = value; }
        }

        //public bool EnableChangeCode
        //{
        //    get { return btnDoiMa.Enabled; }
        //    set { btnDoiMa.Enabled = value; }
        //}
        public string Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        public string[] KeyFields { get; set; }

        public string ReportFile { get; set; }
        public string ReportTitle { get; set; }
        public string ReportTitle2 { get; set; }

        private void btnThem_EnabledChanged(object sender, EventArgs e)
        {
            btnCopy.Enabled = btnThem.Enabled;
        }
        
        private void LoadAD()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var sttrec = dataGridView1.CurrentRow.Cells["Stt_rec"].Value.ToString();
                    CurrentIndex = dataGridView1.CurrentRow.Index;
                    CurrentSttRec = sttrec;

                    if (CurrentTable == V6TableName.Alts)
                    {
                        LoadAD(CurrentSttRec, "TS0=1");
                    }
                    else if (CurrentTable == V6TableName.Alcc)
                    {
                        LoadAD(CurrentSttRec, "CC0=1");
                    }
                    else
                    {
                        LoadAD(CurrentSttRec);
                    }
                }
                else
                {
                    LoadAD("");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadAD", ex);
            }
        }

        

        private void LoadAD(string sttRec, string key = "")
        {
            if (ADTables == null) ADTables = new SortedDictionary<string, DataTable>();
            if (ADTables.ContainsKey(sttRec)) AD = ADTables[sttRec].Copy();
            else
            {
                ADTables.Add(sttRec, LoadAD0(sttRec, key));
                AD = ADTables[sttRec].Copy();
            }
            dataGridView2.DataSource = AD;
            dataGridView2.HideColumnsAldm(_tableName2);
            dataGridView2.SetCorplan2();
        }
        private DataTable LoadAD0(string sttRec, string key = "")
        {
            string sql = "SELECT * FROM " + _tableName2
                + "  Where stt_rec = @rec"
                + (string.IsNullOrEmpty(key)? "" : " and " + key)
                ;
            var listParameters = new List<SqlParameter> { new SqlParameter("@rec", sttRec) };
            DataTable tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters.ToArray())
                .Tables[0];
            return tbl;
        }
        

        #region ==== Do method ====

        private void DoAdd()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable != V6TableName.None)
                {
                    var f = new SoDuFormAddEdit(CurrentTable);
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
                }
                else
                {
                    V6ControlFormHelper.ShowMessage("Hãy chọn danh mục!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoAdd", ex);
            }
        }
        private void DoAddCopy()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                    if (row != null)
                    {
                        var keys = new SortedDictionary<string, object>();
                        if (dataGridView1.Columns.Contains("UID"))//Luôn có trong thiết kế rồi.
                            keys.Add("UID", row.Cells["UID"].Value);

                        if (KeyFields != null)
                            foreach (var keyField in KeyFields)
                            {
                                if (dataGridView1.Columns.Contains(keyField))
                                {
                                    keys[keyField] = row.Cells[keyField].Value;
                                }
                            }

                        _data = row.ToDataDictionary();
                        var f = new SoDuFormAddEdit(CurrentTable, V6Mode.Add, keys, _data);
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Copy", ex);
            }
        }
        private void DoEdit()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                    if (row != null)
                    {
                        var keys = new SortedDictionary<string, object>();
                        if (dataGridView1.Columns.Contains("UID"))//Luôn có trong thiết kế rồi.
                            keys.Add("UID", row.Cells["UID"].Value);

                        if (KeyFields != null)
                            foreach (var keyField in KeyFields)
                            {
                                if (dataGridView1.Columns.Contains(keyField))
                                {
                                    keys[keyField] = row.Cells[keyField].Value;
                                }
                            }

                        _data = row.ToDataDictionary();
                        var f = new SoDuFormAddEdit(CurrentTable, V6Mode.Edit, keys, _data);//, AD.Copy());
                        
                        f.UpdateSuccessEvent += f_UpdateSuccess;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }

        /// <summary>
        /// Khi đóng form sửa, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        void f_UpdateSuccess(SoDuAddEditControlVirtual sender, SortedDictionary<string, object> data)
        {
            if (data == null) return;
            DataGridViewRow row = null;
            if (dataGridView1.CurrentRow != null)
                row = dataGridView1.CurrentRow;
            else if (dataGridView1.SelectedCells.Count > 0)
                row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            else return;//no selected row

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                var field = dataGridView1.Columns[i].DataPropertyName.ToUpper();
                if (data.ContainsKey(field))
                {
                    row.Cells[field].Value = data[field]??DBNull.Value;
                }
            }

            //datagridview2
            ADTables[CurrentSttRec] = sender.AD.Copy();
            dataGridView2.DataSource = ADTables[CurrentSttRec].Copy();
            dataGridView2.HideColumnsAldm(_tableName2);
        }
        
        private void DoDelete()//!!! chưa có rollback khi bị lỗi.
        {
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("DeleteSoDuView2");
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                var selectedData = row.ToDataDictionary();

                if (row != null)
                {
                    if (selectedData.ContainsKey("STT_REC") && selectedData.ContainsKey("MA_CT"))
                    {
                        var stt_rec = selectedData["STT_REC"].ToString();
                        var ma_ct = selectedData["MA_CT"].ToString();
                        var keys = new SortedDictionary<string, object> { { "STT_REC", stt_rec } };

                        var SORT_FIELD = V6TableHelper.GetDefaultSortField(_tableName).ToUpper();
                        var value = selectedData[SORT_FIELD].ToString();
                        if (V6BusinessHelper.AllCheckExist(_tableName, value))
                        {
                            this.ShowInfoMessage(V6Text.DaPhatSinh_KhongDuocXoa);
                            return;
                        }
                        
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm , V6Text.Delete)
                            == DialogResult.Yes)
                        {
                            //Xoa chi tiet truoc
                            if (_maCt == "SO2")
                            {
                                _categories.Delete(TRANSACTION, _tableName2, keys);
                                _categories.Delete(TRANSACTION, _tableName3, keys);
                                _categories.Delete(TRANSACTION, _tableName4, keys);
                            }
                            else
                            {
                                _categories.Delete(TRANSACTION, _tableName2, keys);
                            }

                            //Xoa bang chinh
                            var t = _categories.Delete(TRANSACTION, CurrentTable, keys);

                            if (t > 0)
                            {
                                TRANSACTION.Commit();
                                ADTables.Remove(stt_rec);
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage("Đã xóa.");
                            }
                            else
                            {
                                TRANSACTION.Rollback();
                                V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                            }
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Không có khóa STT_REC và MA_CT.");

                        _data = row.ToDataDictionary();
                        _categories.Delete(CurrentTable, _data);
                    }
                }
            }
            catch (Exception ex)
            {
                TRANSACTION.Rollback();
                this.ShowErrorException(GetType() + ".Xóa lỗi!\n", ex);
            }
            TRANSACTION.Dispose();
        }

        private void DoView()
        {
            try
            {
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                    if (row != null)
                    {
                        var keys = new SortedDictionary<string, object>();
                        if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                            keys.Add("UID", row.Cells["UID"].Value);

                        if (KeyFields != null)
                            foreach (var keyField in KeyFields)
                            {
                                if (dataGridView1.Columns.Contains(keyField))
                                {
                                    keys[keyField] = row.Cells[keyField].Value;
                                }
                            }

                        _data = row.ToDataDictionary();
                        var f = new SoDuFormAddEdit(CurrentTable, V6Mode.View, keys, _data);
                        f.ShowDialog(this);
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoView", ex);
            }
        }

        private void DoPrint()
        {
            try
            {
                var f = new SoDuReportForm(_tableName, ReportFile, ReportTitle, ReportTitle2, InitFilter);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoPrint", ex);
            }
        }

        #endregion do method


        /// <summary>
        /// Được gọi từ DanhMucControl
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="sortField"></param>
        public void LoadTable(V6TableName tableName, string sortField)
        {
            SelectResult = new V6SelectResult();
            CloseFilterForm();
            int pageSize = 20;
            if (comboBox1.SelectedIndex >= 0)
            {
                int.TryParse(comboBox1.Text, out pageSize);
            }
            //else comboBox1.Text = "20";//gây lỗi index changed
            LoadTable(tableName, 1, pageSize, GetWhere(), sortField, true);
        }

        private void LoadTable(V6TableName tableName, int page, int size, string where, string sortField, bool ascending)
        {
            try { 
                if (page < 1) page = 1;
                CurrentTable = tableName;

                var sr = _categories.SelectPaging(tableName, "*", page, size, GetWhere(@where), sortField, @ascending);
                
                SelectResult.Data = sr.Data;
                SelectResult.Page = sr.Page;
                SelectResult.TotalRows = sr.TotalRows;
                SelectResult.PageSize = sr.PageSize;
                SelectResult.Fields = sr.Fields;
                SelectResult.FieldsHeaderDictionary = sr.FieldsHeaderDictionary;
                SelectResult.Where = where;// sr.Where;
                SelectResult.SortField = sr.SortField;
                SelectResult.IsSortOrderAscending = sr.IsSortOrderAscending;

                ViewResultToForm();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _tableName), ex);
            }
        }

        private void LoadAtPage(int page)
        {
            LoadTable(CurrentTable, page, SelectResult.PageSize,SelectResult.Where,
                SelectResult.SortField, SelectResult.IsSortOrderAscending);
        }


        public void ViewResultToForm()
        {
            
            dataGridView1.DataSource =  SelectResult.Data;
            dataGridView1.HideColumnsAldm(_tableName);
            
            var column = dataGridView1.Columns[SelectResult.SortField];
            if (column != null)
                column.HeaderCell.SortGlyphDirection = SelectResult.IsSortOrderAscending ? SortOrder.Ascending : SortOrder.Descending;
            
            //var st = V6BusinessHelper.GetTableStruct("V6struct1".ToString());
            
            if(SelectResult.FieldsHeaderDictionary != null && SelectResult.FieldsHeaderDictionary.Count>0)
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                var field = dataGridView1.Columns[i].DataPropertyName.ToUpper();
                if(SelectResult.FieldsHeaderDictionary.ContainsKey(field))
                {
                    dataGridView1.Columns[i].HeaderText =
                        SelectResult.FieldsHeaderDictionary[field];
                }
                //if (st.ContainsKey(field))
                //{
                //    var columnStruct = st[field];
                //    if (columnStruct.DataType == typeof(string) && columnStruct.MaxLength>60)
                //    {
                //        dataGridView1.Columns[i].Width = 300;
                //    }
                //}
            }

            txtCurrentPage.Text = SelectResult.Page.ToString(CultureInfo.InvariantCulture);
            txtCurrentPage.BackColor = Color.White;
            lblTotalPage.Text = string.Format(
                V6Setting.IsVietnamese
                    ? "Trang {0}/{1} của {2} dòng {3}"
                    : "Page {0}/{1} of {2} row(s) {3}",
                SelectResult.Page, SelectResult.TotalPages, SelectResult.TotalRows,
                string.IsNullOrEmpty(SelectResult.Where)
                    ? ""
                    : (V6Setting.IsVietnamese ? "(Đã lọc)" : "(filtered)"));

            if (SelectResult.Page <= 1)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else
            {
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
            }

            if (SelectResult.Page >= SelectResult.TotalPages)
            {
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
            else
            {
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            SetFormatGridView();
        }

        private void SetFormatGridView()
        {
            V6InvoiceBase _invoice = new V6InvoiceBase(_maCt);
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _invoice.GRDS_AM, _invoice.GRDF_AM,
                        V6Setting.IsVietnamese ? _invoice.GRDHV_AM : _invoice.GRDHE_AM);
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, _invoice.GRDS_AD, _invoice.GRDF_AD,
                        V6Setting.IsVietnamese ? _invoice.GRDHV_AD : _invoice.GRDHE_AD);

        }
        //private void SetFormatGridView()
        //{
        //    try
        //    {
        //        dataGridView1.HideColumnsAldm(_tableName);
        //        dataGridView2.HideColumnsAldm(_tableName2);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorException(GetType() + ".SetFormatGridView", ex);
        //    }
        //}

        public void First()
        {
            try { 
            LoadTable(CurrentTable, 1, SelectResult.PageSize,
                SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void Previous()
        {
            try { 
            LoadTable(CurrentTable, SelectResult.Page - 1, SelectResult.PageSize,
                SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void Next()
        {
            try
            {
                if (SelectResult.Page == SelectResult.TotalPages) return;
                LoadTable(CurrentTable, SelectResult.Page + 1, SelectResult.PageSize,
                    SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void Last()
        {
            try { 
            LoadTable(CurrentTable, SelectResult.TotalPages, SelectResult.PageSize,
                SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void ReLoad()
        {
            try
            {
                dataGridView2.DataSource = null;
                //dataGridView2.Refresh();

                LoadTable(CurrentTable, SelectResult.Page, SelectResult.PageSize,
                    SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
                //LoadAD();
                LoadSelectedCellLocation(dataGridView1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Reload", ex);
            }
        }
        

        private void btnFirst_Click(object sender, EventArgs e)
        {
            First();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Previous();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Last();
        }

        private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            //if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    ((TextBox)sender).BackColor = Color.Red;
            //}
        }

        private void txtCurrentPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int page;
                int.TryParse(txtCurrentPage.Text, out page);
                if (page < 1) page = 1;
                LoadAtPage(page);
            }
            else
            {
                if (e.KeyValue >= '0' && e.KeyValue <= '9')
                {
                    txtCurrentPage.Text += (char) e.KeyValue;
                    ((TextBox)sender).BackColor = Color.Red;
                }
                if (e.KeyValue >= 96 && e.KeyValue <= 105)
                {
                    var n = "";
                    switch (e.KeyValue)
                    {
                        case 96:
                            n = "0";
                            break;
                        case 97:
                            n = "1";
                            break;
                        case 98:
                            n = "2";
                            break;
                        case 99:
                            n = "3";
                            break;
                        case 100:
                            n = "4";
                            break;
                        case 101:
                            n = "5";
                            break;
                        case 102:
                            n = "6";
                            break;
                        case 103:
                            n = "7";
                            break;
                        case 104:
                            n = "8";
                            break;
                        case 105:
                            n = "9";
                            break;
                    }
                    txtCurrentPage.Text += n;
                    ((TextBox)sender).BackColor = Color.Red;
                }
                if (e.KeyCode == Keys.Back) // && txtCurrentPage.SelectionStart>0)
                {
                    if (txtCurrentPage.TextLength > 0)
                    {
                        txtCurrentPage.Text = txtCurrentPage.Text.Substring(0, txtCurrentPage.TextLength - 1);
                    }
                }
            }
            txtCurrentPage.SelectionStart = txtCurrentPage.TextLength;
        }

        private void txtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", "B" + _maCt))
            {
                DoAdd();
            }
            else
            {
                this.ShowWarningMessage(V6Text.NoRight);
            }
            
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", "B" + _maCt))
            {
                DoAddCopy();
            }
            else
            {
                this.ShowWarningMessage(V6Text.NoRight);
            }
            
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowView("", "B" + _maCt))
            {
                DoView();
            }
            else
            {
                this.ShowWarningMessage(V6Text.NoRight);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowPrint("", "B" + _maCt))
            {
                DoPrint();
            }
            else
            {
                this.ShowWarningMessage(V6Text.NoRight);
            }
        }

        //Reload
        void f_InsertSuccess(SoDuAddEditControlVirtual sender, SortedDictionary<string, object> dataDic)
        {
            ReLoad();
        }


        private SortedDictionary<string, object> _data = new SortedDictionary<string, object>();
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", "B" + _maCt))
            {
                DoEdit();
            }
            else
            {
                this.ShowWarningMessage(V6Text.NoRight);
            }
        }
        
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (V6Login.UserRight.AllowDelete("", "B" + _maCt))
                {
                    DoDelete();
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoRight);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Xoa", ex);
            }
        }


        private SoDuFilterForm _filterForm;
        private string InitFilter = "";

        public string AddInitFilter(string where, bool and = true)
        {
            if (string.IsNullOrEmpty(where)) return InitFilter;
            InitFilter += (string.IsNullOrEmpty(InitFilter) ? "" : (and ? " and " : " or ")) + where;
            return InitFilter;
        }

        private string GetWhere(string where = null)
        {
            string result;
            if (string.IsNullOrEmpty(InitFilter))
            {
                result = where;
            }
            else
            {
                if (string.IsNullOrEmpty(where))
                    result = InitFilter;
                else
                    result = string.Format("{0} and({1})", InitFilter, where);
            }
            return result;
        }

        private void CloseFilterForm()
        {
            if (_filterForm != null)
            {
                _filterForm.Close();
                _filterForm.Dispose();
                _filterForm = null;
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            V6TableStruct structTable = V6BusinessHelper.GetTableStruct(CurrentTable.ToString());
            //var keys = new SortedDictionary<string, object>();
            string[] fields = V6Lookup.GetDefaultLookupFields(CurrentTable.ToString());
            _filterForm = new SoDuFilterForm(structTable, fields);
            _filterForm.FilterOkClick += filter_FilterOkClick;
            _filterForm.Opacity = 0.9;
            _filterForm.TopMost = true;
            //_filterForm.Location = Location;
            _filterForm.Show(this);
        }

        void filter_FilterOkClick(string query)
        {
            SelectResult.Where = query;
            LoadAtPage(1);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            SelectResult.Where = "";
            LoadAtPage(1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectResult.PageSize = int.Parse(comboBox1.Text);
            LoadAtPage(1);
        }

        public override void SetStatus2Text()
        {
            if (V6Setting.IsVietnamese)
            {
                V6ControlFormHelper.SetStatusText2("F3-Sửa, F4-Thêm, F5-Tìm, F8-Xóa");
            }
            else
            {
                V6ControlFormHelper.SetStatusText2("F3-Edit, F4-New, F5-Search, F8-Delete");
            }
        }
        
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
            if (_hideColumnDic.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
            }
        }

        private void btnFull_Click(object sender, EventArgs e)
        {
            var container = Parent;
            var child = this;
            if (container is Form)
            {
                ((Form)container).Close();
            }
            else
            {

                var f = new V6Form
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false,
                    FormBorderStyle = FormBorderStyle.None
                };
                f.Controls.Add(child);
                f.FormClosing += (se, a) =>
                {
                    container.Controls.Add(child);
                    btnFull.Image = Properties.Resources.ZoomIn24;
                    btnFull.Text = V6Text.ZoomIn;
                };
                btnFull.Image = Properties.Resources.ZoomOut24;
                btnFull.Text = V6Text.ZoomOut;
                f.ShowDialog();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                var f = Parent;
                base.Dispose();
                if (f is Form) f.Dispose();
            }
            catch
            {

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            LoadAD();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var column = dataGridView1.Columns[e.ColumnIndex];
                var columnName = column.DataPropertyName;
                if (columnName == "RowNum") return;

                foreach (DataGridViewColumn column0 in dataGridView1.Columns)
                {
                    if (column0 != column) column0.HeaderCell.SortGlyphDirection = SortOrder.None;
                }

                var new_sortOrder = column.HeaderCell.SortGlyphDirection != SortOrder.Ascending;
                var sort_field = column.DataPropertyName;

                LoadTable(CurrentTable, SelectResult.Page, SelectResult.PageSize,
                    SelectResult.Where, sort_field, new_sortOrder);
            }
            catch
            {
                // ignored
            }
        }

        private void btnDoiMa_Click_1(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", CurrentTable.ToString().ToUpper() + "6")
                && V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoChangeCode();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }


        private void DoChangeCode()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                    if (row != null)
                    {
                        _data = row.ToDataDictionary();

                        var f = ChangeCodeManager.GetChangeCodeControl(CurrentTable, _data);
                        if (f != null)
                        {
                            f.DoChangeCodeFinish += f_DoChangeCodeFinish;
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView DoChangeCode");
            }
        }
        private void f_DoChangeCodeFinish(SortedDictionary<string, object> data)
        {
            if (ADTables.ContainsKey(CurrentSttRec))
            {
                ADTables.Remove(CurrentSttRec);
            }
            ReLoad();
            
            LoadAD();
        }
        
    }
}
