using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.DanhMucManager;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.NhanSu
{
    public partial class NhanSuView : V6FormControl
    {
        public NhanSuView()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                //vTitle, eTitle
                var groupFields = ObjectAndString.SplitString(V6Lookup.GetValueByTableName(_tableName, "vTitle"));
                var groupNameFields = ObjectAndString.SplitString(V6Lookup.GetValueByTableName(_tableName, "eTitle"));
                treeListViewAuto1.SetGroupAndNameFieldList(groupFields, groupNameFields);

                //treeListViewAuto1.SetGroupAndNameFieldList(
                //        new string[] { "MA_DVCS", "MA_BPNS", "MA_NS" },
                //        new string[] { "MA_DVCS", "MA_BPNS", "MA_NS" });
                //treeListViewAuto1.AutoGenerateColumns = false;
                //treeListViewAuto1.Columns.Add("ID", "Mã", 200, HorizontalAlignment.Center, -1);
                //treeListViewAuto1.Columns.Add("ten_ns", "Tên nhân sự", 200, HorizontalAlignment.Center, -1);
                //treeListViewAuto1.Columns.Add("ten_ns2", "Tên 2", 100, HorizontalAlignment.Center, -1);
                //treeListViewAuto1.Columns.Add("dia_chi", "Địa chỉ", 250, HorizontalAlignment.Center, -1);

                //LoadListMenu();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".NhanSuView MyInit " + ex.Message);
            }
        }

        public NhanSuView(string itemId, string title, string tableName, string initFilter = "", string sort="")
        {
            m_itemId = itemId;
            Title = title;
            _tableName = tableName;
            CurrentTable = V6TableHelper.ToV6TableName(tableName);

            InitializeComponent();
            MyInit();
            
            _hideColumnDic = _categories.GetHideColumns(tableName);
            InitFilter = initFilter;
            SelectResult = new V6SelectResult();

            //SetFormatGridView();

            if (CurrentTable == V6TableName.V_alts || CurrentTable == V6TableName.V_alcc
                || CurrentTable == V6TableName.V_alts01 || CurrentTable == V6TableName.V_alcc01)
            {
                btnCopy.Visible = false;
                btnDoiMa.Visible = false;
                btnIn.Visible = false;
            }

            LoadTable(CurrentTable, sort);
        }

        private void NhanSuView_Load(object sender, EventArgs e)
        {
            FormManagerHelper.HideMainMenu();
            //dataGridView01.Focus();
            if (CurrentTable == V6TableName.Hlns)
            {
                KeyFields = new[] { "MA_BP", "MA_CV", "MA_NS" };
            }
            MakeStatus2Text();
        }

        private readonly V6Categories _categories = new V6Categories();
        private SortedDictionary<string, string> _hideColumnDic; 
        private string _tableName;
        private string _viewName = "VPRDMNS";
        [DefaultValue(V6TableName.None)]
        public V6TableName CurrentTable { get; set; }
        public V6SelectResult SelectResult { get; set; }

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

        public bool EnableChangeCode
        {
            get { return btnDoiMa.Enabled; }
            set { btnDoiMa.Enabled = value; }
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
            
        }

        #region ==== Do method ====

        private void DoAdd()
        {
            try
            {
                if (V6Login.UserRight.AllowAdd("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (CurrentTable != V6TableName.None)
                    {
                        var data = new SortedDictionary<string, object>();
                        if (treeListViewAuto1.SelectedItems.Count>0)
                        {
                            data = treeListViewAuto1.SelectedItemData;
                        }

                        //var keys = new SortedDictionary<string, object>();
                        //if (data.ContainsKey("UID"))
                        //    keys.Add("UID", data["UID"]);

                        //if (KeyFields != null)
                        //    foreach (var keyField in KeyFields)
                        //    {
                        //        var KEYFIELD = keyField.ToUpper();
                        //        if (data.ContainsKey(KEYFIELD))
                        //        {
                        //            keys[KEYFIELD] = data[KEYFIELD];
                        //        }
                        //    }

                        var f = new FormAddEdit(CurrentTable, V6Mode.Add, null, null);
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);

                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage("Hãy chọn danh mục!");
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
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
                if (V6Login.UserRight.AllowAdd("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (CurrentTable != V6TableName.None)
                    {
                        var data = new SortedDictionary<string, object>();
                        if (treeListViewAuto1.SelectedItems.Count > 0)
                        {
                            data = treeListViewAuto1.SelectedItemData;
                        }

                        var keys = new SortedDictionary<string, object>();
                        if (data.ContainsKey("UID"))
                            keys.Add("UID", data["UID"]);

                        if (KeyFields != null)
                            foreach (var keyField in KeyFields)
                            {
                                var KEYFIELD = keyField.ToUpper();
                                if (data.ContainsKey(KEYFIELD))
                                {
                                    keys[KEYFIELD] = data[KEYFIELD];
                                }
                            }

                        var f = new FormAddEdit(CurrentTable, V6Mode.Add, keys, data);
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);

                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage("Hãy chọn danh mục!");
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message);
            }
        }

        private void DoEdit()
        {
            try
            {
                if (V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (CurrentTable == V6TableName.None)
                    {
                        V6ControlFormHelper.ShowMainMessage("Hãy chọn danh mục!");
                    }
                    else
                    {
                        TreeListViewItem item = treeListViewAuto1.SelectedItems[0];
                        
                        if (item.Level == treeListViewAuto1.MaxLevel)
                        {
                            //var selected_item_data = treeListViewAuto1.SelectedItems[0].ToNhanSuDictionary();
                            var selected_item_data = treeListViewAuto1.SelectedItemData;
                            if (selected_item_data != null)
                            {
                                var keys = new SortedDictionary<string, object>();
                                if (selected_item_data.ContainsKey("UID"))
                                {
                                    keys.Add("UID", selected_item_data["UID"]);
                                }
                                else
                                {
                                    this.ShowInfoMessage(V6Text.NoUID);
                                }
                                if (selected_item_data.ContainsKey("UID"))
                                {
                                    keys["STT_REC"] = selected_item_data["STT_REC"];
                                }
                                else
                                {
                                    this.ShowInfoMessage(V6Text.NoSTTREC);
                                }
                                

                                if (KeyFields != null)
                                    foreach (var keyField in KeyFields)
                                    {
                                        var KEYFIELD = keyField.ToUpper();
                                        if (selected_item_data.ContainsKey(KEYFIELD))
                                        {
                                            keys[KEYFIELD] = selected_item_data[KEYFIELD];
                                        }
                                    }

                                var __data = new SortedDictionary<string, object>();
                                __data.AddRange(selected_item_data);
                                var f = new FormAddEdit(CurrentTable, V6Mode.Edit, keys, null);
                                f.UpdateSuccessEvent += f_UpdateSuccess;
                                //f.CallReloadEvent += FCallReloadEvent;
                                f.ShowDialog(this);
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMainMessage("Hãy chọn một dòng dữ liệu!");
                            }
                        }

                        if (item.Level == 0)
                        {
                            //V6ControlFormHelper.ShowInfoMessage("Đơn vị: " + item.Text);
                        }
                        else if (item.Level == 1)
                        {
                            //V6ControlFormHelper.ShowInfoMessage("Bộ phận nhân sự: " + item.Text);
                        }
                        else if (item.Level == 2)
                        {
                            
                        }
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
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
                ReLoad();

                //if (data == null) return;
                //var dictionary = new SortedDictionary<string, string>();
                //dictionary.AddRange(data);
                //treeListViewAuto1.UpdateSelectedItemData(data);
                //SetFormatGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".f_UpdateSuccess: " + ex.Message);
            }
        }

        private void DoChangeCode()
        {
            try
            {

                if ((V6Login.UserRight.AllowAdd("", CurrentTable.ToString().ToUpper() + "6"))
                    && (V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6")))
                {

                    if (CurrentTable == V6TableName.None)
                    {
                        V6ControlFormHelper.ShowMainMessage("Hãy chọn danh mục!");
                    }
                    else
                    {
                        //DataGridViewRow row = dataGridView01.GetFirstSelectedRow();
                        if (treeListViewAuto1.SelectedItems[0] != null
                            && treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                        {
                            var data = new SortedDictionary<string, object>();
                            data.AddRange(treeListViewAuto1.SelectedItems[0].ToNhanSuDictionary());

                            var f = DanhMucManager.ChangeCode.ChangeCodeManager.GetChangeCodeControl(_tableName, data);
                            if (f != null)
                            {
                                f.DoChangeCodeFinish += f_DoChangeCodeFinish;
                                f.ShowDialog(this);
                            }
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMainMessage("Hãy chọn một dòng dữ liệu!");
                        }

                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
             }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }

        private void f_DoChangeCodeFinish(SortedDictionary<string, object> data)
        {
            ReLoad();
        }

        private void DoDelete()
        {
            try
            {
                if (V6Login.UserRight.AllowDelete("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (treeListViewAuto1.SelectedItems[0] != null
                        && treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                    {
                        var selected_item = treeListViewAuto1.SelectedItems[0];
                        var data = treeListViewAuto1.SelectedItemData;
                        
                        var id = V6Lookup.ValueByTableName[CurrentTable.ToString(), "vValue"].ToString().Trim();
                        var listTable =
                            V6Lookup.ValueByTableName[CurrentTable.ToString(), "ListTable"].ToString().Trim();
                        var value = selected_item.Name;

                        if (!string.IsNullOrEmpty(listTable) && !string.IsNullOrEmpty(id))
                        {
                            if(data.ContainsKey(id.ToUpper()))
                                value = data[id.ToUpper()].ToString().Trim();
                            var v = _categories.IsExistOneCode_List(listTable, id, value);
                            if (v)
                            {
                                //khong duoc
                                this.ShowWarningMessage(V6Text.DaPhatSinh_KhongDuocXoa);
                                return;
                            }
                        }
                        
                        var isDeleted = 0;

                        if (data.ContainsKey("UID"))
                        {
                            var keys = new SortedDictionary<string, object> {{"UID", data["UID"]}};
                            

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + value, "Xóa?")
                                == DialogResult.Yes)
                            {
                                isDeleted = _categories.Delete(CurrentTable, keys);
                            }
                            else
                            {
                                return;
                            }
                        }
                        //else
                        //{
                        //    this.ShowWarningMessage("Không có khóa. Vẫn xóa dựa trên dữ liệu đang có!");

                        //    isDeleted = _categories.Delete(CurrentTable, data);
                        //}

                        if (isDeleted > 0)
                        {
                            //ReLoad();
                            //treeListViewAuto1.Items.Remove(selected_item);
                            selected_item.Remove();
                            V6ControlFormHelper.ShowMessage(V6Text.DeleteSuccess);
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                        }
                        
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
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
                if (V6Login.UserRight.AllowView("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (CurrentTable == V6TableName.None)
                    {
                        V6ControlFormHelper.ShowMainMessage("Hãy chọn danh mục!");
                    }
                    else
                    {
                        if (treeListViewAuto1.SelectedItems[0] != null
                            && treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                        {
                            var data = treeListViewAuto1.SelectedItemData;

                            var keys = new SortedDictionary<string, object>();
                            if (treeListViewAuto1.Columns.ContainsKey("UID"))
                                keys.Add("UID", data["UID"].ToString());

                            if (KeyFields != null)
                                foreach (var keyField in KeyFields)
                                {
                                    var KEYFIELD = keyField.ToUpper();
                                    if (treeListViewAuto1.Columns.ContainsKey(KEYFIELD))
                                    {
                                        keys[KEYFIELD] = data[KEYFIELD];
                                    }
                                }

                            var f = new FormAddEdit(CurrentTable, V6Mode.View, keys, data);
                            f.ShowDialog(this);
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMainMessage("Hãy chọn một dòng dữ liệu!");
                        }
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".NhanSuView DoView " + ex.Message);
            }
        }

        private void DoPrint()
        {
             try
            {
                if (V6Login.UserRight.AllowPrint("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    FormManagerHelper.ShowDanhMucPrint(this, _tableName, ReportFile, ReportTitle, ReportTitle2);
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
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
            int pageSize = 0;
            
            LoadTable(tableName, 1, pageSize, GetWhere(), sort, true);
        }

        private void LoadTable(V6TableName tableName, int page, int size, string where, string sortField, bool ascending)
        {
            try { 
                if (page < 1) page = 1;
                CurrentTable = tableName;

                //var sr = _categories.SelectPaging(tableName, "*", page, size, GetWhere(where), sortField, ascending);
                var sr = V6BusinessHelper.Select(_viewName, "*", where, "", sortField);
                
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
            LoadTable(CurrentTable, page, 0, SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
        }


        public void ViewResultToForm()
        {
            #region --- NhanSuTreeView

            //nhanSuTreeView1.FieldsHeaderDictionary = SelectResult.FieldsHeaderDictionary;
            //nhanSuTreeView1.HideColumnDic = _hideColumnDic;
            //nhanSuTreeView1.DataSource = SelectResult.Data;

            string showFields = V6Lookup.ValueByTableName[_tableName, "GRDS_V1"].ToString().Trim();
            string headerString = V6Lookup.ValueByTableName[_tableName, V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1"].ToString().Trim();
            string formatStrings = V6Lookup.ValueByTableName[_tableName, "GRDF_V1"].ToString().Trim();
            
            //V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);

            treeListViewAuto1.SetData(SelectResult.Data, showFields, headerString, formatStrings);
            #endregion nhansutreeview


            lblTotalPage.Text = string.Format(V6Setting.IsVietnamese ? "Tổng cộng: {0} " : "Total {0} ",
                SelectResult.TotalRows,
                string.IsNullOrEmpty(SelectResult.Where)
                    ? ""
                    : (V6Setting.IsVietnamese ? "(Đã lọc)" : "(filtered)"));
        }


        public void First()
        {
            try { 
            LoadTable(CurrentTable, 1, SelectResult.PageSize,
                SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
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
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
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
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
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
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
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
                //SetFormatGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
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

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowPrint("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoPrint();
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
            //treeListViewAuto1.AddData(data);
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
        private void btnDoiMa_Click(object sender, EventArgs e)
        {
            DoChangeCode();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DoDelete();
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

        private string status2text = "";

        private void MakeStatus2Text()
        {
            var text = "";
            if (V6Setting.IsVietnamese)
            {
                if (EnableEdit) text += ", F3-Sửa";
                if (EnableAdd) text += ", F4-Thêm";
                text += ", F5-Tìm";
                if (EnableChangeCode) text += ", F6-Đổi mã";
                if (EnableDelete) text += ", F8-Xóa";
            }
            else
            {
                if (EnableEdit) text += ", F3-Edit";
                if (EnableAdd) text += ", F4-New";
                text += ", F5-Search";
                if (EnableChangeCode) text += ", F6-Change code";
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
                    btnFull.Image = Properties.Resources.ZoomIn32;
                    btnFull.Text = V6Text.ZoomIn;
                };
                btnFull.Image = Properties.Resources.ZoomOut2;
                btnFull.Text = V6Text.ZoomOut;
                f.ShowDialog(container);
            }
        }


        private void treeListViewAuto1_Click(object sender, EventArgs e)
        {
            //DoTreeClick();
        }

    }
}
