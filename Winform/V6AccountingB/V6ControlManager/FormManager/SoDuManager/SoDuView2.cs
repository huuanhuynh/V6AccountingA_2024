using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.DanhMucManager.ChangeCode;
using V6ControlManager.FormManager.ReportManager.SoDu;
using V6ControlManager.FormManager.SoDuManager.Add_Edit;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using SortOrder = System.Windows.Forms.SortOrder;

namespace V6ControlManager.FormManager.SoDuManager
{
    public partial class SoDuView2 : V6FormControl
    {
        #region ===== Var =====
        private AldmConfig _aldmConfig, _aldmConfig2;
        private V6lookupConfig _v6LookupConfig;
        private readonly V6Categories _categories = new V6Categories();
        private SortedDictionary<string, string> _hideColumnDic;
        private string _maCt;
        private AlctConfig _alctConfig;

        /// <summary>
        /// _tableName3,_tableName4...
        /// </summary>
        private string[] _DetailTableNameList = null;

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


        private SoDuFilterForm _filterForm;
        private string _initFilter;
        public string InitFilter
        {
            get
            {
                if (_initFilter == null)
                {
                    _initFilter = V6Login.GetInitFilter(_alctConfig.TableNameAM, V6ControlFormHelper.FindFilterType(this));
                }
                return ("" + _initFilter).Replace("{MA_DVCS}", "'" + V6Login.Madvcs + "'");
            }
            set { _initFilter = value; }
        }
        private string _search;

        //private string FILTER_FIELD
        //{
        //    get
        //    {
        //        if (_aldmConfig.HaveInfo) return _aldmConfig.FILTER_FIELD;
        //        return _v6LookupConfig.FILTER_FIELD;
        //    }
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
        protected Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        /// <summary>
        /// Code động từ aldmConfig.
        /// </summary>
        protected Type Event_program;
        public Dictionary<string, object> All_Objects = new Dictionary<string, object>();

        private void btnThem_EnabledChanged(object sender, EventArgs e)
        {
            btnCopy.Enabled = btnThem.Enabled;
        }
        #endregion var

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
            _maCt = maCt;
            SelectResult = new V6SelectResult();
            dataGridView1.DataSource = new DataTable();
            MyInit();
        }

        private void GetADnameList()
        {
            _DetailTableNameList = ObjectAndString.SplitString(_alctConfig.TableNameADlist);
        }

        private void MyInit()
        {
            try
            {
                //DataTable alct = V6BusinessHelper.GetAlct(maCt);
                _alctConfig = ConfigManager.GetAlctConfig(_maCt);
                _aldmConfig = ConfigManager.GetAldmConfigByTableName(_alctConfig.TableNameAM);
                _aldmConfig2 = ConfigManager.GetAldmConfigByTableName(_alctConfig.TableNameAD);
                _v6LookupConfig = V6Lookup.GetV6lookupConfigByTableName(_alctConfig.TableNameAM);
                CurrentTable = V6TableHelper.ToV6TableName(_alctConfig.TableNameAM);

                GetADnameList();
                _hideColumnDic = _categories.GetHideColumns(_alctConfig.TableNameAM);
                
                All_Objects["thisForm"] = this;
                CreateFormProgram();
                V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
                InvokeFormEvent(FormDynamicEvent.INIT);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
            }
        }

        private void SoDuView2_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTable(CurrentTable, "");
                FormManagerHelper.HideMainMenu();
                dataGridView1.Focus();
                MakeStatus2Text();
                InvokeFormEvent(ControlDynamicEvent.INIT2);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2", ex);
            }
        }
        
        protected void CreateFormProgram()
        {
            try
            {
                //DMETHOD
                if (_aldmConfig.NoInfo || string.IsNullOrEmpty(_aldmConfig.DMETHOD))
                {
                    return;
                }

                string using_text = "";
                string method_text = "";
                //foreach (DataRow dataRow in Invoice.Alct1.Rows)
                {
                    var xml = _aldmConfig.DMETHOD;
                    if (xml == "") return;
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xml));
                    if (ds.Tables.Count <= 0) return;
                    var data = ds.Tables[0];
                    foreach (DataRow event_row in data.Rows)
                    {
                        var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                        var method_name = event_row["method"].ToString().Trim();
                        Event_Methods[EVENT_NAME] = method_name;

                        using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                        method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                    }
                }

            Build:
                Event_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + _aldmConfig.MA_DM, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
            }
        }

        /// <summary>
        /// Gọi hàm động theo tên event đã định nghĩa.
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public object InvokeFormEvent(string eventName)
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(eventName))
                {
                    var method_name = Event_Methods[eventName];
                    return V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
            }
            return null;
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
            dataGridView2.HideColumnsAldm(_alctConfig.TableNameAD);
            dataGridView2.SetCorplan2();
        }
        private DataTable LoadAD0(string sttRec, string key = "")
        {
            string sql = "SELECT * FROM " + LOAD_TABLE2
                + "  Where stt_rec = @rec"
                + (string.IsNullOrEmpty(key)? "" : " and " + key)
                ;
            var plist = new List<SqlParameter> { new SqlParameter("@rec", sttRec) };
            DataTable tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist.ToArray())
                .Tables[0];
            return tbl;
        }
        

        #region ==== Do method ====

        private void DoAdd()
        {
            try
            {
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
                        this.ShowWarningMessage(V6Text.NoSelection);
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
                        this.ShowWarningMessage(V6Text.NoSelection);
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
        void f_UpdateSuccess(SoDuAddEditControlVirtual sender, IDictionary<string, object> data)
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
            dataGridView2.HideColumnsAldm(_alctConfig.TableNameAD);
        }

        private void DoChangeCode()
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
                        _data = row.ToDataDictionary();

                        var f = ChangeCodeManager.GetChangeCodeControl(_alctConfig.TableNameAM, _data);
                        if (f != null)
                        {
                            f.DoChangeCodeFinish += f_DoChangeCodeFinish;
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.NoSelection);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView DoChangeCode");
            }
        }
        private void f_DoChangeCodeFinish(IDictionary<string, object> data)
        {
            if (ADTables.ContainsKey(CurrentSttRec))
            {
                ADTables.Remove(CurrentSttRec);
            }
            ReLoad();

            LoadAD();
        }
        
        private void DoDelete()//!!! chưa có rollback khi bị lỗi.
        {
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("DeleteSoDuView2");
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                var selectedData = row.ToDataDictionary();

                if (row != null)
                {
                    if (selectedData.ContainsKey("STT_REC") && selectedData.ContainsKey("MA_CT"))
                    {
                        var stt_rec = selectedData["STT_REC"].ToString();
                        var ma_ct = selectedData["MA_CT"].ToString();
                        var keys = new SortedDictionary<string, object> { { "STT_REC", stt_rec } };

                        var SORT_FIELD = V6TableHelper.GetDefaultSortField(_alctConfig.TableNameAM).ToUpper();
                        var value = selectedData[SORT_FIELD].ToString();
                        if (V6BusinessHelper.AllCheckExist(_alctConfig.TableNameAM, value))
                        {
                            this.ShowInfoMessage(V6Text.DaPhatSinh_KhongDuocXoa);
                            return;
                        }
                        
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm , V6Text.Delete)
                            == DialogResult.Yes)
                        {
                            //Xoa chi tiet truoc
                            _categories.Delete(TRANSACTION, _alctConfig.TableNameAD, keys);

                            if (_DetailTableNameList != null)
                            {
                                foreach (string table in _DetailTableNameList)
                                {
                                    if (V6BusinessHelper.IsExistDatabaseTable(table))
                                    {
                                        _categories.Delete(TRANSACTION, table, keys);
                                    }
                                }
                            }

                            //Xoa bang chinh
                            var t = _categories.Delete(TRANSACTION, CurrentTable, keys);
                            InvokeFormEvent(FormDynamicEvent.AFTERDELETESUCCESS);
                            if (t > 0)
                            {
                                TRANSACTION.Commit();
                                ADTables.Remove(stt_rec);
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                            }
                            else
                            {
                                TRANSACTION.Rollback();
                                V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
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
                this.ShowErrorException(GetType() + " " + V6Text.Text("XOALOI"), ex);
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
                        this.ShowWarningMessage(V6Text.NoSelection);
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
                var f = new SoDuReportForm(_alctConfig.TableNameAM, ReportFile, ReportTitle, ReportTitle2, InitFilter);
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoPrint", ex);
            }
        }

        #endregion do method

        private string LOAD_TABLE
        {
            get
            {
                string load_table = _aldmConfig.TABLE_NAME;
                if (!string.IsNullOrEmpty(_aldmConfig.TABLE_VIEW) && V6BusinessHelper.IsExistDatabaseTable(_aldmConfig.TABLE_VIEW))
                {
                    load_table = _aldmConfig.TABLE_VIEW;
                }
                return load_table;
            }
        }

        private string LOAD_TABLE2
        {
            get
            {
                string load_table2 = _alctConfig.TableNameAD;
                if (_aldmConfig2.HaveInfo)
                {
                    load_table2 = _aldmConfig2.TABLE_NAME;
                    if (!string.IsNullOrEmpty(_aldmConfig2.TABLE_VIEW) && V6BusinessHelper.IsExistDatabaseTable(_aldmConfig2.TABLE_VIEW))
                    {
                        load_table2 = _aldmConfig2.TABLE_VIEW;
                    }
                }
                return load_table2;
            }
        }

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
            LoadTable(tableName, 1, pageSize, sortField, true);
        }

        private void LoadTable(V6TableName tableName, int page, int size, string sortField, bool ascending)
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (page < 1) page = 1;
                CurrentTable = tableName;
                if (_aldmConfig != null && CurrentTable == V6TableName.Notable)
                {
                    if (string.IsNullOrEmpty(sortField)) sortField = _aldmConfig.ORDER;
                }

                _last_filter = GetWhere();
                var sr = _categories.SelectPaging(LOAD_TABLE, "*", page, size, _last_filter, sortField, @ascending);
                
                SelectResult.Data = sr.Data;
                SelectResult.Page = sr.Page;
                SelectResult.TotalRows = sr.TotalRows;
                SelectResult.PageSize = sr.PageSize;
                SelectResult.Fields = sr.Fields;
                SelectResult.FieldsHeaderDictionary = sr.FieldsHeaderDictionary;
                SelectResult.Where = _last_filter;
                SelectResult.SortField = sr.SortField;
                SelectResult.IsSortOrderAscending = sr.IsSortOrderAscending;

                ViewResultToForm();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _alctConfig.TableNameAM), ex);
            }
        }

        private void LoadAtPage(int page)
        {
            LoadTable(CurrentTable, page, SelectResult.PageSize,
                SelectResult.SortField, SelectResult.IsSortOrderAscending);
        }


        public void ViewResultToForm()
        {
            dataGridView1.DataSource =  SelectResult.Data;
            dataGridView1.HideColumnsAldm(_alctConfig.TableNameAM);
            LoadSelectedCellLocation(dataGridView1);

            if (!string.IsNullOrEmpty(SelectResult.SortField))
            {
                var column = dataGridView1.Columns[SelectResult.SortField];
                if (column != null) column.HeaderCell.SortGlyphDirection = SelectResult.IsSortOrderAscending
                        ? SortOrder.Ascending
                        : SortOrder.Descending;
            }
            
            txtCurrentPage.Text = SelectResult.Page.ToString(CultureInfo.InvariantCulture);
            txtCurrentPage.BackColor = Color.White;
            
            lblTotalPage.Text = string.Format(
                V6Setting.IsVietnamese
                    ? "Trang {0}/{1} của {2} dòng {3}"
                    : "Page {0}/{1} of {2} row(s) {3}",
                SelectResult.Page, SelectResult.TotalPages, SelectResult.TotalRows,
                string.IsNullOrEmpty(_last_filter)
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

        private bool formated;
        private void SetFormatGridView()
        {
            if (formated) return;
            try
            {
                if (SelectResult.FieldsHeaderDictionary != null && SelectResult.FieldsHeaderDictionary.Count > 0)
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        var field = dataGridView1.Columns[i].DataPropertyName.ToUpper();
                        if (SelectResult.FieldsHeaderDictionary.ContainsKey(field))
                        {
                            dataGridView1.Columns[i].HeaderText =
                                SelectResult.FieldsHeaderDictionary[field];
                        }
                    }

                V6InvoiceBase _invoice = new V6InvoiceBase(_maCt);
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _invoice.AlctConfig.GRDS_AM, _invoice.AlctConfig.GRDF_AM,
                            V6Setting.IsVietnamese ? _invoice.AlctConfig.GRDHV_AM : _invoice.AlctConfig.GRDHE_AM);
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, _invoice.AlctConfig.GRDS_AD, _invoice.AlctConfig.GRDF_AD,
                            V6Setting.IsVietnamese ? _invoice.AlctConfig.GRDHV_AD : _invoice.AlctConfig.GRDHE_AD);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetFormatGridView", ex);
            }
            formated = true;
        }

        public override void DoHotKey(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.F6))
                {

                }
                else if (keyData == Keys.Escape)
                {
                    Dispose();
                }
                if (keyData == Keys.F9)
                {
                    All_Objects["dataGridView1"] = dataGridView1;
                    InvokeFormEvent(FormDynamicEvent.F9);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoHotKey", ex);
            }
            base.DoHotKey(keyData);
        }

        public void First()
        {
            try { 
            LoadTable(CurrentTable, 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void Previous()
        {
            try { 
            LoadTable(CurrentTable, SelectResult.Page - 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
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
                LoadTable(CurrentTable, SelectResult.Page + 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void Last()
        {
            try { 
            LoadTable(CurrentTable, SelectResult.TotalPages, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
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

                LoadTable(CurrentTable, SelectResult.Page, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
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
            if (V6Login.UserRight.AllowCopy("", "B" + _maCt))
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
        void f_InsertSuccess(SoDuAddEditControlVirtual sender, IDictionary<string, object> datadic)
        {
            ReLoad();
        }


        private IDictionary<string, object> _data = new SortedDictionary<string, object>();
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


        public string AddInitFilter(string where, bool and = true)
        {
            if (string.IsNullOrEmpty(where)) return InitFilter;
            InitFilter += (string.IsNullOrEmpty(InitFilter) ? "" : (and ? " and " : " or ")) + where;
            return InitFilter;
        }

        private string GetWhere()
        {
            string result = "";
            if (!string.IsNullOrEmpty(InitFilter))
            {
                result = InitFilter;
            }

            ////Thêm lọc Filter_Field
            //if (cboFilter.Visible && cboFilter.SelectedIndex > 0)
            //{
            //    string filter = string.Format("{0}='{1}'", FILTER_FIELD, cboFilter.SelectedValue);
            //    result += string.Format("{0}{1}", result.Length > 0 ? " and " : "", filter);
            //}

            //Thêm lọc where
            if (!string.IsNullOrEmpty(_search))
            {
                result += string.Format("{0}({1})", result.Length > 0 ? " and " : "", _search);
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
            try
            {
                V6TableStruct structTable = V6BusinessHelper.GetTableStruct(CurrentTable.ToString());

                if (!_v6LookupConfig.HaveInfo)
                {
                    this.ShowWarningMessage(V6Text.NoDefine, 500);
                    return;
                }
                string[] fields = _v6LookupConfig.GetDefaultLookupFields;
                _filterForm = new SoDuFilterForm(structTable, fields);
                _filterForm.FilterOkClick += filter_FilterOkClick;
                _filterForm.Opacity = 0.9;
                _filterForm.TopMost = true;
                //_filterForm.Location = Location;
                _filterForm.Show(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Find_Click", ex);
            }
        }

        void filter_FilterOkClick(string query)
        {
            _search = query;
            LoadAtPage(1);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            _search = "";
            LoadAtPage(1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectResult.PageSize = int.Parse(comboBox1.Text);
            LoadAtPage(1);
        }

        private string status2text = "";
        private string _last_filter;

        private void MakeStatus2Text()
        {
            var text = "";
            if (V6Setting.IsVietnamese)
            {
                if (EnableEdit) text += ", F3-Sửa";
                if (EnableAdd) text += ", F4-Thêm";
                text += ", F5-Tìm";
                //if (EnableChangeCode) text += ", F6-Đổi mã";
                if (EnableDelete) text += ", F8-Xóa";
            }
            else
            {
                if (EnableEdit) text += ", F3-Edit";
                if (EnableAdd) text += ", F4-New";
                text += ", F5-Search";
                //if (EnableChangeCode) text += ", F6-Change code";
                if (EnableDelete) text += ", F8-Delete";
            }
            status2text = text.Substring(2);
        }
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(status2text);
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
                f.ShowDialog(container);
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

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
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

                LoadTable(CurrentTable, SelectResult.Page, SelectResult.PageSize, sort_field, new_sortOrder);
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
        
    }
}
