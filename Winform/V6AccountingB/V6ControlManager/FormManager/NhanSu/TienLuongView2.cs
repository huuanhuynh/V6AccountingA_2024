using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.NhanSu.View;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.NhanSu
{
    public partial class TienLuongView2 : V6FormControl
    {
        public TienLuongView2()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                LoadListMenu();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TienLuongView2 MyInit " + ex.Message);
            }
        }

        public TienLuongView2(string itemId, string title, string tableName, string initFilter = "", string sort = "")
        {
            m_itemId = itemId;
            Title = title;
            _tableName = tableName;
            _config = V6Lookup.GetV6lookupConfigByTableName(_tableName);
            CurrentTable = V6TableHelper.ToV6TableName(tableName);

            InitializeComponent();
            MyInit();

            _hideColumnDic = _categories.GetHideColumns(tableName);
            InitFilter = initFilter;
            SelectResult = new V6SelectResult();

            //SetFormatGridView();

            LoadTable(CurrentTable, sort);
        }

        private void TienLuongView2_Load(object sender, EventArgs e)
        {
            FormManagerHelper.HideMainMenu();
            //dataGridView01.Focus();
            if (CurrentTable == V6TableName.Hlns)
            {
                KeyFields = new[] { "MA_BP", "MA_CV", "MA_NS" };
            }

        }

        private readonly V6Categories _categories = new V6Categories();
        private SortedDictionary<string, string> _hideColumnDic;
        private string _tableName;
        private string _viewName = "VPRDMNSTREE";
        private V6lookupConfig _config;
        [DefaultValue(V6TableName.None)]
        public V6TableName CurrentTable { get; set; }
        public V6SelectResult SelectResult { get; set; }


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
                        if (tochucTree1.SelectedItems.Count > 0)
                        {
                            data = tochucTree1.SelectedItemData;
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

                        var f = new FormAddEdit(CurrentTable, V6Mode.Add, null, data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl();
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

        public void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, CurrentTable.ToString());
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _tableName, ex);
            }
        }

        private void DoAddCopy()
        {
            try
            {
                if (V6Login.UserRight.AllowCopy("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (CurrentTable != V6TableName.None)
                    {
                        var data = new SortedDictionary<string, object>();
                        if (tochucTree1.SelectedItems.Count > 0)
                        {
                            data = tochucTree1.SelectedItemData;
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
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl();
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
                        TreeListViewItem item = tochucTree1.SelectedItems[0];

                        //if (item.Level == treeListViewAuto1.MaxLevel)
                        {
                            //var selected_item_data = treeListViewAuto1.SelectedItems[0].ToNhanSuDictionary();
                            var selected_item_data = tochucTree1.SelectedItemData;
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
                                var f = new FormAddEdit(CurrentTable, V6Mode.Edit, keys, __data);
                                f.AfterInitControl += f_AfterInitControl;
                                f.InitFormControl();
                                f.UpdateSuccessEvent += f_UpdateSuccess;
                                f.CallReloadEvent += FCallReloadEvent;
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
                this.ShowErrorException(GetType() + ".DoEdit", ex);
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

                var dictionary = new SortedDictionary<string, string>();
                dictionary.AddRange(data);
                tochucTree1.UpdateSelectedItemData(data);
                //SetFormatGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".f_UpdateSuccess", ex);
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
                        if (tochucTree1.SelectedItems[0] != null)
                        //&& treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                        {
                            var data = new SortedDictionary<string, object>();
                            data.AddRange(tochucTree1.SelectedItems[0].ToNhanSuDictionary());

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
                    if (tochucTree1.SelectedItems[0] != null)
                    //&& treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                    {
                        var selected_item = tochucTree1.SelectedItems[0];
                        var data = tochucTree1.SelectedItemData;

                        var id = _config.vValue;
                        var listTable = _config.ListTable;
                        var value = selected_item.Name;

                        if (!string.IsNullOrEmpty(listTable) && !string.IsNullOrEmpty(id))
                        {
                            if (data.ContainsKey(id.ToUpper()))
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
                            var keys = new SortedDictionary<string, object> { { "UID", data["UID"] } };


                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + value, V6Text.DeleteConfirm)
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
                this.ShowErrorException(GetType() + ".DoDelete", ex);
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
                        if (tochucTree1.SelectedItems[0] != null)
                        //&& treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                        {
                            var data = tochucTree1.SelectedItemData;

                            var keys = new SortedDictionary<string, object>();
                            if (tochucTree1.Columns.ContainsKey("UID"))
                                keys.Add("UID", data["UID"].ToString());

                            if (KeyFields != null)
                                foreach (var keyField in KeyFields)
                                {
                                    var KEYFIELD = keyField.ToUpper();
                                    if (tochucTree1.Columns.ContainsKey(KEYFIELD))
                                    {
                                        keys[KEYFIELD] = data[KEYFIELD];
                                    }
                                }

                            var f = new FormAddEdit(CurrentTable, V6Mode.View, keys, data);
                            f.AfterInitControl += f_AfterInitControl;
                            f.InitFormControl();
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
                this.ShowErrorMessage(GetType() + ".TienLuongView2 DoView " + ex.Message);
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
            try
            {
                //if (page < 1) page = 1;
                CurrentTable = tableName;

                //var sr = _categories.SelectPaging(tableName, "*", page, size, GetWhere(where), sortField, ascending);
                _last_filter = GetNewWhere(where);
                var sr = V6BusinessHelper.Select(_viewName, "*", _last_filter, "", sortField);

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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _viewName), ex);
            }
        }

        private string GetNewWhere(string where)
        {
            if (string.IsNullOrEmpty(where))
            {
                return "";
            }

            SqlParameter[] plist =
            {
                new SqlParameter("@cWhere", where??""),
                new SqlParameter("@return", null),
            };
            string listParent = V6BusinessHelper.ExecuteProcedureScalar("VPH_GetParentNodeList", plist).ToString().Trim();
            return string.Format("{0} or(dbo.VFV_InList0(node,'{1}',',')=1)", where, listParent);
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

            string showFields = _config.GRDS_V1;
            string headerString = V6Setting.IsVietnamese ? _config.GRDHV_V1 : _config.GRDHE_V1;
            string formatStrings = _config.GRDF_V1;

            //V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);

            tochucTree1.SetData(SelectResult.Data, showFields, headerString, formatStrings);
            #endregion nhansutreeview


            lblTotalPage.Text = string.Format(V6Setting.IsVietnamese ? "Tổng cộng: {0} " : "Total {0} ",
                SelectResult.TotalRows,
                string.IsNullOrEmpty(_last_filter)
                    ? ""
                    : (V6Setting.IsVietnamese ? "(Đã lọc)" : "(filtered)"));
        }


        public void First()
        {
            try
            {
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
            try
            {
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
            try
            {
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
                LoadTable(CurrentTable, "");
                //LoadTable(CurrentTable, SelectResult.Page, SelectResult.PageSize,
                //    SelectResult.Where, SelectResult.SortField, SelectResult.IsSortOrderAscending);
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
            if (V6Login.UserRight.AllowCopy("", CurrentTable.ToString().ToUpper() + "6"))
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
            tochucTree1.AddData(data);
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
        private string InnerFilter;

        private string GetWhere(string where = null)
        {
            string result = null;
            if (!string.IsNullOrEmpty(InitFilter))
            {
                result = InitFilter;
            }
            if (!string.IsNullOrEmpty(InnerFilter))
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = InnerFilter;
                }
                else
                {
                    result = "(" + InitFilter + ") and (" + InnerFilter + ")";
                }
            }

            if (!string.IsNullOrEmpty(where))
            {
                if (string.IsNullOrEmpty(result))
                    result = where;
                else
                    result = string.Format("({0}) and ({1})", result, where);
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
            string[] fields = _config.GetDefaultLookupFields;
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

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Setting.IsVietnamese
                ? "F3-Sửa, F4-Thêm, F5-Tìm, F6-Đổi mã, F8-Xóa"
                : "F3-Edit, F4-New, F5-Search, F6-Change Code, F8-Delete");
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
            if (_hideColumnDic.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
            }
        }

        private void DoFillter()
        {
            try
            {
                if (txtLoc.Text.Trim() != "")
                    InnerFilter = string.Format("Ma_ns like N'%{0}%' or Ten_ns like N'%{1}%'", txtLoc.Text, txtLoc.Text);
                else InnerFilter = "";

                ReLoad();
                CleanControlNhanSu();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoFillter", ex);
            }
        }

        private void DoTreeClick()
        {
            try
            {
                if (V6Login.UserRight.AllowView("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (CurrentTable == V6TableName.None)
                    {
                        //V6ControlFormHelper.ShowMainMessage("Hãy chọn danh mục!");
                    }
                    else
                    {
                        if (tochucTree1.SelectedItems[0] != null)
                        //&& treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                        {
                            var data = tochucTree1.SelectedItemData;

                            var keys = new SortedDictionary<string, object>();
                            if (tochucTree1.Columns.ContainsKey("UID"))
                                keys.Add("UID", data["UID"].ToString());

                            if (KeyFields != null)
                                foreach (var keyField in KeyFields)
                                {
                                    var KEYFIELD = keyField.ToUpper();
                                    if (tochucTree1.Columns.ContainsKey(KEYFIELD))
                                    {
                                        keys[KEYFIELD] = data[KEYFIELD];
                                    }
                                }

                            var sttRec = data["STT_REC"].ToString().Trim();
                            var maNhanSu = data["MA_NS"].ToString().Trim();

                            if (sttRec != _stt_rec)
                            {
                                ChiTietNhanSuResetData(sttRec, maNhanSu, data);
                                ShowSelect();
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
                this.ShowErrorMessage(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        protected SortedDictionary<string, V6Control> ControlsDictionary = new SortedDictionary<string, V6Control>();
        private string _stt_rec, _ma_ns;
        private IDictionary<string, object> _nhanSuData;
        private void ChiTietNhanSuResetData(string sttRec, string maNhanSu, IDictionary<string, object> nhanSuData)
        {
            try
            {
                CleanControlNhanSu();
                //this.ShowMessage("LoadData: " + maNhanSu);
                _stt_rec = sttRec;
                _ma_ns = maNhanSu;
                _nhanSuData = nhanSuData;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0}.ChiTietNhanSuResetData {1} {2}", GetType(), maNhanSu, ex.Message));
            }
        }

        private void CleanControlNhanSu()
        {
            try
            {
                int error_count = 0;
                while (ControlsDictionary.Count > 0 || error_count > 5)
                {
                    try
                    {
                        List<string> temp_list = new List<string>();
                        foreach (KeyValuePair<string, V6Control> item in ControlsDictionary)
                        {
                            temp_list.Add(item.Key);
                        }
                        foreach (string key in temp_list)
                        {
                            ControlsDictionary[key].Dispose();
                        }
                    }
                    catch
                    {
                        error_count++;
                    }
                }

                //foreach (KeyValuePair<string, V6Control> item in ControlsDictionary)
                //{
                //    item.Value.Dispose();
                //}
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _tableName), ex);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.ShowConfirmMessage(V6Text.BackConfirm) == DialogResult.Yes)
                {
                    Dispose();
                }
            }
            else if (keyData == Keys.F5)
            {
                if (this.ShowConfirmMessage(V6Text.ReloadConfirm) == DialogResult.Yes)
                {
                    ReLoad();
                }
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }

        private DataTable data;
        private DataView view;
        private string _last_filter;

        public void LoadListMenu()
        {
            try
            {
                data = V6Menu.GetKey2Pr2();
                view = new DataView(data);
                //view.RowFilter = "1=0";
                listBox1.DisplayMember = V6Setting.IsVietnamese ? "vbar" : "vbar2";
                listBox1.DataSource = view;
                listBox1.DisplayMember = V6Setting.IsVietnamese ? "vbar" : "vbar2";
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _tableName), ex);
            }
        }

        public void ShowSelect()
        {
            try
            {
                if (listBox1.SelectedIndex == -1) return;
                if (string.IsNullOrEmpty(_stt_rec))
                {
                    V6ControlFormHelper.ShowMainMessage("Hãy chọn một nhân sự!");
                    return;
                }

                //var selectedItem = listBox1.SelectedItem;
                DataRowView rowView = listBox1.SelectedItem as DataRowView;

                var item_id = rowView["itemid"].ToString().Trim().ToUpper();
                var codeform = rowView["codeform"].ToString().Trim();

                foreach (var control in ControlsDictionary)
                {
                    if (control.Key != item_id) control.Value.Visible = false;
                    //else control.Value.Visible = true;
                }

                if (ControlsDictionary.ContainsKey(item_id) && !ControlsDictionary[item_id].IsDisposed)
                {
                    var check = true;
                    if (codeform != null)
                    {
                        var TABLE_NAME = (codeform.Length > 1 ? codeform.Substring(1) : "").ToUpper();
                        if (TABLE_NAME == "V6USER" || TABLE_NAME == "V6CLIENTS" || TABLE_NAME == "ALSTT" ||
                            TABLE_NAME == "V6OPTION"
                            || codeform.StartsWith("8"))
                        {
                            check = new ConfirmPassword().ShowDialog(this) == DialogResult.OK;
                        }
                    }

                    if (check)
                    {
                        if (!panelView.Contains(ControlsDictionary[item_id]))
                        {
                            panelView.Controls.Add(ControlsDictionary[item_id]);
                        }
                        ControlsDictionary[item_id].Visible = true;
                        ControlsDictionary[item_id].Focus();
                    }
                }
                else
                {
                    //this.ShowMessage(string.Format("GenControl ItemID: {0}, CodeForm: {1}", item_id, codeform));
                    MenuButton mButton = new MenuButton()
                    {
                        ItemID = rowView["itemid"].ToString().Trim().ToUpper(),
                        Text = V6Setting.IsVietnamese
                            ? rowView["vbar"].ToString().Trim()
                            : rowView["vbar2"].ToString().Trim(),
                        CodeForm = rowView["codeform"].ToString().Trim(),
                        Exe = rowView["program"].ToString().Trim(),
                        MaChungTu = rowView["ma_ct"].ToString().Trim(),
                        NhatKy = rowView["nhat_ky"].ToString().Trim(),

                        ReportFile = rowView["rep_file"].ToString().Trim(),
                        ReportTitle = rowView["title"].ToString().Trim(),
                        ReportTitle2 = rowView["title2"].ToString().Trim(),
                        ReportFileF5 = rowView["rep_fileF5"].ToString().Trim(),
                        ReportTitleF5 = rowView["titleF5"].ToString().Trim(),
                        ReportTitle2F5 = rowView["title2F5"].ToString().Trim(),

                        Key1 = rowView["Key1"].ToString().Trim(),
                        Key2 = rowView["Key2"].ToString().Trim(),
                        Key3 = rowView["Key3"].ToString().Trim(),
                        Key4 = rowView["Key4"].ToString().Trim(),
                    };

                    var c = MenuManager.MenuManager.GenControl(this, mButton, null);
                    //V6Control c = NhanSuManager.GenControl(this, rowView, _ma_ns);

                    if (c != null)
                    {
                        V6ControlFormHelper.SetFormControlsReadOnly(c, true);
                        c.SetParentData(_nhanSuData);
                        c.LoadData(_stt_rec);
                        if (c is NoGridControl)
                        {
                            ((NoGridControl)c).HideFilterControl();
                        }

                        if (c is OneGridControl)
                        {
                            ((OneGridControl)c).HideFilterControl();
                        }

                        c.Disposed += delegate(object sender1, EventArgs e1)
                        {
                            ControlsDictionary.Remove(((Control)sender1).Name);
                            FormManagerHelper.ShowCurrentMenu3Menu();
                        };
                        ControlsDictionary[item_id] = c;
                        panelView.Controls.Add(c);
                        c.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ShowSelect " + ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowSelect();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ShowSelect();
        }

        private void treeListViewAuto1_Click(object sender, EventArgs e)
        {
            DoTreeClick();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            DoFillter();
        }


    }
}
