﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.DanhMucManager;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using SortOrder = System.Windows.Forms.SortOrder;

namespace V6Controls.Controls
{
    public partial class CategoryView : V6FormControl
    {
        public CategoryView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="title"></param>
        /// <param name="tableName"></param>
        /// <param name="initFilter">Không có thì truyền null</param>
        /// <param name="sort">Không có thì truyền null</param>
        /// <param name="parentData">Dữ liệu cha</param>
        public CategoryView(string itemId, string title, string tableName, string initFilter, string sort, IDictionary<string, object> parentData)
        {
            m_itemId = itemId;
            Title = title;
            _tableName = tableName;
            _parentData = parentData;

            InitializeComponent();

            
            CurrentTable = V6TableHelper.ToV6TableName(tableName);
            _hideColumnDic = _categories.GetHideColumns(tableName);
            InitFilter = initFilter;
            SelectResult = new V6SelectResult();
            SelectResult.SortField = sort;

            if (CurrentTable == V6TableName.V_alts || CurrentTable == V6TableName.V_alcc
                || CurrentTable == V6TableName.V_alts01 || CurrentTable == V6TableName.V_alcc01)
            {
                btnCopy.Visible = false;
            }

            if (CurrentTable == V6TableName.Alnhkh || CurrentTable == V6TableName.Alnhvt
                || CurrentTable == V6TableName.Alnhvv)
            {
                
            }

            dataGridView1.DataSource = new DataTable();
        }

        private void DanhMucView_Load(object sender, EventArgs e)
        {
            LoadTable(CurrentTable, SelectResult.SortField);
            
            dataGridView1.Focus();
            if (CurrentTable == V6TableName.Alkh)
            {
                KeyFields = new[] {"MA_KH"};
            }
            MakeStatus2Text();
        }

        private readonly V6Categories _categories = new V6Categories();
        private SortedDictionary<string, string> _hideColumnDic;
        private IDictionary<string, object> _parentData;
        /// <summary>
        /// Tên gốc gửi vào
        /// </summary>
        private string _tableName;
        /// <summary>
        /// Tên theo enum V6TableName
        /// </summary>
        [DefaultValue(V6TableName.None)]
        [Description("Tên theo enum V6TableName")]
        public V6TableName CurrentTable { get; set; }
        /// <summary>
        /// Nơi chứa dữ liệu
        /// </summary>
        public V6SelectResult SelectResult { get; set; }

        public bool EnableAdd
        {
            get { return btnThem.Enabled; }
            set { btnThem.Enabled = value; }
        }

        public bool EnableCopy
        {
            get { return btnCopy.Enabled; }
            set { btnCopy.Enabled = value; }
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
        
        private void SetFormatGridView()
        {
            if (CurrentTable == V6TableName.Altk0)
            {
                //dataGridView1.Columns[0].DefaultCellStyle.Padding;
                dataGridView1.CellFormatting += (s, e) =>
                {
                    if (e.ColumnIndex == 1)
                    {
                        int b = ObjectAndString.ObjectToInt(dataGridView1.Rows[e.RowIndex].Cells["Bac_tk"].Value);
                        int l = ObjectAndString.ObjectToInt(dataGridView1.Rows[e.RowIndex].Cells["Loai_tk"].Value);
                        if (l == 0) dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);

                        if (b == 1) b = 0;
                        var p = b * 8;
                        e.CellStyle.Padding = new Padding(p, 0, 0, 0);
                    }
                };
            }
            else if(CurrentTable == V6TableName.Almaubcct)
            {
                V6ControlFormHelper.FormatGridView(dataGridView1, "BOLD", "=", 1, true, false, Color.White);
            }
            
            // Đè format cũ
            string showFields = V6Lookup.ValueByTableName[_tableName, "GRDS_V1"].ToString().Trim();
            string formatStrings = V6Lookup.ValueByTableName[_tableName, "GRDF_V1"].ToString().Trim();
            string headerString = V6Lookup.ValueByTableName[_tableName, V6Setting.Language == "V" ? "GRDHV_V1" : "GRDHE_V1"].ToString().Trim();
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);

        }

        #region ==== Do method ====

        public override void DoHotKey(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.F6))
                {
                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".DoHotKey " + ex.Message, Application.ProductName);
            }
            base.DoHotKey(keyData);
        }

        private void DoAdd()
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
                    FormAddEdit f;
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
                        f = new FormAddEdit(CurrentTable, V6Mode.Add, keys, _data);
                        f.ParentData = _parentData;
                        //f.SetParentData();
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        f = new FormAddEdit(CurrentTable);
                        f.ParentData = _parentData;
                        f.SetParentData();
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message);
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
                        var f = new FormAddEdit(CurrentTable, V6Mode.Add, keys, _data);
                        f.ParentData = _parentData;
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
                this.ShowErrorMessage(GetType() + ".DoAddCopy: " + ex.Message);
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
                        var f = new FormAddEdit(CurrentTable, V6Mode.Edit, keys, _data);
                        f.ParentData = _parentData;
                        f.UpdateSuccessEvent += f_UpdateSuccess;
                        f.CallReloadEvent += FCallReloadEvent;
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
                this.ShowErrorMessage(GetType() + ".DoEdit: " + ex.Message);
            }
        }

        private void FCallReloadEvent(object sender, EventArgs eventArgs)
        {
            ReLoad();
        }
        /// <summary>
        /// Khi sửa thành công, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        /// <param name="data">Dữ liệu đã sửa</param>
        private void f_UpdateSuccess(SortedDictionary<string, object> data)
        {
            try
            {
                if (data == null) return;
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                V6ControlFormHelper.UpdateGridViewRow(row, data);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".f_UpdateSuccess: " + ex.Message);
            }
        }

        
        private void DoDelete()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    _data = row.ToDataDictionary();
                    if (CurrentTable == V6TableName.V_alts)
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();


                        data.Add("MA_GIAM_TS", "");
                        data.Add("NGAY_GIAM", null);
                        data.Add("LY_DO_GIAM", "");
                        data.Add("SO_CT", "");


                        var so_the_ts = row.Cells["SO_THE_TS"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + so_the_ts, V6Text.Delete)
                            == DialogResult.Yes)
                        {
                            var t = _categories.Update(CurrentTable, data, keys);
                            if (t > 0)
                            {
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage("Đã xóa!");
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                            }
                        }
                    }
                    else if (CurrentTable == V6TableName.V_alcc)
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();


                        data.Add("MA_GIAM_CC", "");
                        data.Add("NGAY_GIAM", "");
                        data.Add("LY_DO_GIAM", "");
                        data.Add("SO_CT", "");


                        var so_the_cc = row.Cells["SO_THE_CC"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + so_the_cc, "Xóa?")
                            == DialogResult.Yes)
                        {
                            var t = _categories.Update(CurrentTable, data, keys);
                            if (t > 0)
                            {
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage("Đã xóa " + t);
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                            }
                        }
                    }
                    else if (CurrentTable == V6TableName.V_alts01)
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                        data.Add("NGAY_KH1", "");
                        var so_the_ts = row.Cells["SO_THE_TS"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + so_the_ts, "Xóa?")
                            == DialogResult.Yes)
                        {
                            var t = _categories.Update(CurrentTable, data, keys);
                            if (t > 0)
                            {
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage("Đã xóa " + t);
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                            }
                        }
                    }
                    else if (CurrentTable == V6TableName.V_alcc01)
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                        data.Add("NGAY_PB1", "");
                        var so_the_cc = row.Cells["SO_THE_CC"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + so_the_cc, "Xóa?")
                            == DialogResult.Yes)
                        {
                            var t = _categories.Update(CurrentTable, data, keys);
                            if (t > 0)
                            {
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage("Đã xóa " + t);
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                            }
                        }
                    }
                    else if (CurrentTable == V6TableName.V6user)
                    {
                        if (!V6Login.IsAdmin)
                        {
                            V6ControlFormHelper.ShowMainMessage(V6Text.NoRight);
                            return;
                        }
                        
                        var userId = ObjectAndString.ObjectToInt(row.Cells["USER_ID"].Value);
                        if (V6Login.UserId == userId)
                        {
                            V6ControlFormHelper.ShowMainMessage(V6Text.DeleteDenied);
                            return;
                        }

                        //var code = UtilityHelper.DeCrypt(row.Cells["CODE_USER"].Value.ToString().Trim());
                        //if (!string.IsNullOrEmpty(code))
                        //{
                        //    var isAdmin = code.Right(1) == "1";
                            
                        //}
                        if (dataGridView1.Columns.Contains("UID"))
                        {
                            var keys = new SortedDictionary<string, object> { { "UID", row.Cells["UID"].Value } };

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + userId, V6Text.Delete)
                                == DialogResult.Yes)
                            {
                                var t = _categories.Delete(CurrentTable, keys);

                                if (t > 0)
                                {
                                    ReLoad();
                                    V6ControlFormHelper.ShowMainMessage("Đã xóa " + userId);
                                }
                                else
                                {
                                    V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                                }
                            }
                        }

                    }
                    else
                    {

                        var id = V6Lookup.ValueByTableName[CurrentTable.ToString(), "vValue"].ToString().Trim();
                        var listTable =
                            V6Lookup.ValueByTableName[CurrentTable.ToString(), "ListTable"].ToString().Trim();
                        var value = "";
                        
                        if (String.IsNullOrEmpty(listTable) == false)
                        {
                            value = string.IsNullOrEmpty(id) ? "" : row.Cells[id].Value.ToString().Trim();
                            var v = _categories.IsExistOneCode_List(listTable, id, value);
                            if (v)
                            {
                                //khong duoc
                                this.ShowWarningMessage("Mã đã có phát sinh, không được xóa!");
                                return;
                            }
                        }

                        if (dataGridView1.Columns.Contains("UID"))
                        {
                            var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + value, "Xóa?")
                                == DialogResult.Yes)
                            {
                                var t = _categories.Delete(CurrentTable, keys);

                                if (t > 0)
                                {
                                    if (CurrentTable == V6TableName.Altk0)
                                    {

                                        try
                                        {
                                            var tk = row.Cells["TK"].Value.ToString().Trim();
                                            var tk_me_new = row.Cells["TK_ME"].Value.ToString().Trim();

                                            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UpdateLoaiTk",
                                                new SqlParameter("@tk", tk),
                                                new SqlParameter("@tk_me_old", tk_me_new),
                                                new SqlParameter("@tk_me_new", tk_me_new),
                                                new SqlParameter("@action", "D")
                                                );
                                        }
                                        catch (Exception ex)
                                        {
                                            this.ShowErrorMessage(GetType() + ".UpdateLoaiTk: " + ex.Message);
                                        }

                                    }
                                    ReLoad();
                                    V6ControlFormHelper.ShowMainMessage("Đã xóa " + t);
                                }
                                else
                                {
                                    V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                                }
                            }
                        }
                        else
                        {
                            this.ShowWarningMessage("Không có khóa UID. Vẫn xóa!");
                            
                            _categories.Delete(CurrentTable, _data);
                        }
                    }

                    var aev = AddEditManager.Init_Control(CurrentTable);//ảo
                    if (!string.IsNullOrEmpty(aev.KeyField1))
                    {
                        var oldKey1 = _data[aev.KeyField1].ToString().Trim();
                        var oldKey2 = "";
                        if (!string.IsNullOrEmpty(aev.KeyField2) && _data.ContainsKey(aev.KeyField2))
                            oldKey2 = _data[aev.KeyField2].ToString().Trim();
                        var oldKey3 = "";
                        if (!string.IsNullOrEmpty(aev.KeyField3) && _data.ContainsKey(aev.KeyField3))
                            oldKey3 = _data[aev.KeyField3].ToString().Trim();

                        var uid = _data.ContainsKey("UID") ? _data["UID"].ToString() : "";
                        
                        V6ControlFormHelper.Copy_Here2Data(CurrentTable, V6Mode.Delete,
                            aev.KeyField1, aev.KeyField2, aev.KeyField3,
                            oldKey1, oldKey2, oldKey3,
                            oldKey1, oldKey2, oldKey3,
                            uid);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Xóa lỗi!\n" + ex.Message);
            }
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
                        var f = new FormAddEdit(CurrentTable, V6Mode.View, keys, _data);
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
                this.ShowErrorMessage(ex.Message, "DanhMucView DoView");
            }
        }
        
        #endregion do method


        /// <summary>
        /// Được gọi từ DanhMucControl
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="sort"></param>
        public void LoadTable(V6TableName tableName, string sort)
        {
            SelectResult = new V6SelectResult();
            CloseFilterForm();
            int pageSize = 20;
            if (comboBox1.SelectedIndex >= 0)
            {
                int.TryParse(comboBox1.Text, out pageSize);
            }
            //else comboBox1.Text = "20";//gây lỗi index changed
            LoadTable(tableName, 1, pageSize, GetWhere(), sort, true);
        }

        private void LoadTable(V6TableName tableName, int page, int size, string where, string sortField, bool @ascending)
        {
            try { 
                if (page < 1) page = 1;
                CurrentTable = tableName;
                var sr = _categories.SelectPaging(tableName, "*", page, size, GetWhere(where), sortField, @ascending);
                
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
                this.ShowErrorMessage(ex.Message, "DanhMucView LoadTable_prs");
            }
        }

        private void LoadAtPage(int page)
        {
            LoadTable(CurrentTable, page, SelectResult.PageSize,SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
        }


        public void ViewResultToForm()
        {
            
            dataGridView1.DataSource = SelectResult.Data;

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
            }

            txtCurrentPage.Text = SelectResult.Page.ToString(CultureInfo.InvariantCulture);
            txtCurrentPage.BackColor = Color.White;
            lblTotalPage.Text = string.Format(
                V6Setting.Language == "V"
                    ? "Trang {0}/{1} của {2} dòng {3}"
                    : "Page {0}/{1} of {2} row(s) {3}",
                SelectResult.Page, SelectResult.TotalPages, SelectResult.TotalRows,
                string.IsNullOrEmpty(SelectResult.Where)
                    ? ""
                    : (V6Setting.Language == "V" ? "(Đã lọc)" : "(filtered)"));

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

        public void First()
        {
            try { 
            LoadTable(CurrentTable, 1, SelectResult.PageSize,
                SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView First");
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
                this.ShowErrorMessage(ex.Message, "DanhMucView Previous");
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
                this.ShowErrorMessage(ex.Message, "DanhMucView Next");
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
                this.ShowErrorMessage(ex.Message, "DanhMucView Last");
            }
        }

        /// <summary>
        /// Reload and setFormatGridView
        /// </summary>
        public void ReLoad()
        {
            try
            {
                LoadTable(CurrentTable, SelectResult.Page, SelectResult.PageSize,
                    SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView Reload");
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
            var txt = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                int page;
                int.TryParse(txt.Text, out page);
                if (page < 1) page = 1;
                LoadAtPage(page);
            }
            else
            {
                if (txt.SelectionLength > 0)
                {
                    txt.Text = txt.Text.Substring(0, txt.SelectionStart) +
                        txt.Text.Substring(txt.SelectionStart + txt.SelectionLength);
                }

                if (e.KeyValue >= '0' && e.KeyValue <= '9')
                {
                    //txt.Text += (char) e.KeyValue;
                    txtCurrentPage.Text = txt.Text.Insert(txt.SelectionStart, ((char)e.KeyValue).ToString());
                    txt.BackColor = Color.Red;
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
                    //txt.Text += n;
                    txt.Text = txt.Text.Insert(txt.SelectionStart, n);
                    txt.BackColor = Color.Red;
                }
                if (e.KeyCode == Keys.Back) // && txt.SelectionStart>0)
                {
                    if (txt.TextLength > 0)
                    {
                        txt.Text = txt.Text.Substring(0, txt.TextLength - 1);
                    }
                }
            }
            txt.SelectionStart = txt.TextLength;
        }

        private void txtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoAdd();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoAddCopy();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowView("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoView();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }
        
        //Reload
        private void f_InsertSuccess(SortedDictionary<string, object> data)
        {
            ReLoad();
        }


        private SortedDictionary<string, object> _data = new SortedDictionary<string, object>();
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoEdit();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }
        
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowDelete("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoDelete();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }


        private FilterForm _filterForm;
        private string InitFilter;

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
            _filterForm = new FilterForm(structTable, fields);
            _filterForm.FilterApplyEvent += FilterFilterApplyEvent;
            _filterForm.Opacity = 0.9;
            _filterForm.TopMost = true;
            //_filterForm.Location = Location;
            _filterForm.Show();
        }

        void FilterFilterApplyEvent(string query)
        {
            SelectResult.Where = query;
            LoadAtPage(1);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            SelectResult.Where = "";
            LoadAtPage(1);
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
                // ignored
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectResult.PageSize = int.Parse(comboBox1.Text);
            LoadAtPage(1);
        }

        private string status2text = "";

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


        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var column = dataGridView1.Columns[e.ColumnIndex];
                var columnName = column.DataPropertyName;
                if (columnName == "RowNum") return;

                foreach (DataGridViewColumn column0 in dataGridView1.Columns)
                {
                    if(column0 != column) column0.HeaderCell.SortGlyphDirection = SortOrder.None;
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

        public string AddInitFilter(string where, bool and = true)
        {
            if (string.IsNullOrEmpty(where)) return InitFilter;
            InitFilter += (string.IsNullOrEmpty(InitFilter) ? "" : (and ? " and " : " or ")) + where;
            return InitFilter;
        }

    }
}
