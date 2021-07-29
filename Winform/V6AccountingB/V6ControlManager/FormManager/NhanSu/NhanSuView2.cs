using System;
using System.Collections.Generic;
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

namespace V6ControlManager.FormManager.NhanSu
{
    public partial class NhanSuView2 : V6FormControl
    {
        public NhanSuView2()
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
                this.ShowErrorMessage(GetType() + ".NhanSuView2 MyInit " + ex.Message);
            }
        }

        public NhanSuView2(string itemId, string title, string tableName, string initFilter = "", string sort="")
        {
            m_itemId = itemId;
            Title = title;
            TABLE_NAME = tableName.ToUpper();
            _v6LookupConfig = V6Lookup.GetV6lookupConfigByTableName(TABLE_NAME);
            
            InitializeComponent();
            MyInit();
            
            InitFilter = initFilter;
            SelectResult = new V6SelectResult();
            CloseFilterForm();
            LoadTable(TABLE_NAME, GetWhere(), sort);
        }

        private void NhanSuView2_Load(object sender, EventArgs e)
        {
            FormManagerHelper.HideMainMenu();
            
            if (TABLE_NAME == "HLNS")
            {
                KeyFields = new[] { "MA_BP", "MA_CV", "MA_NS" };
            }
          
        }

        private readonly V6Categories _categories = new V6Categories();
        private string TABLE_NAME;
        private string VIEW_NAME = "VPRDMNSTREE";
        private V6lookupConfig _v6LookupConfig;
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
                if (V6Login.UserRight.AllowAdd("", TABLE_NAME + "6"))
                {
                    IDictionary<string, object> data = new SortedDictionary<string, object>();
                    if (tochucTree1.SelectedItems.Count > 0)
                    {
                        data = tochucTree1.SelectedItemData;
                    }

                    var f = new FormAddEdit(TABLE_NAME, V6Mode.Add, null, data);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
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

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, TABLE_NAME);
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
        }

        private void DoAddCopy()
        {
            try
            {
                if (V6Login.UserRight.AllowCopy("", TABLE_NAME + "6"))
                {
                    IDictionary<string, object> data = new SortedDictionary<string, object>();
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

                    var f = new FormAddEdit(TABLE_NAME, V6Mode.Add, keys, data);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
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
                if (V6Login.UserRight.AllowEdit("", TABLE_NAME + "6"))
                {
                    TreeListViewItem item = tochucTree1.SelectedItems[0];

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
                        var f = new FormAddEdit(TABLE_NAME, V6Mode.Edit, keys, __data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(this);
                        f.UpdateSuccessEvent += f_UpdateSuccess;
                        f.CallReloadEvent += FCallReloadEvent;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage(V6Text.Text("CHON1DL"));
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
        private void f_UpdateSuccess(IDictionary<string, object> data)
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
                if ((V6Login.UserRight.AllowAdd("", TABLE_NAME + "6"))
                    && (V6Login.UserRight.AllowEdit("", TABLE_NAME + "6")))
                {
                    if (tochucTree1.SelectedItems[0] != null)
                    {
                        IDictionary<string, object> data = new SortedDictionary<string, object>();
                        data.AddRange(tochucTree1.SelectedItems[0].ToNhanSuDictionary());

                        var f = DanhMucManager.ChangeCode.ChangeCodeManager.GetChangeCodeControl(TABLE_NAME, data);
                        if (f != null)
                        {
                            f.DoChangeCodeFinish += f_DoChangeCodeFinish;
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage(V6Text.Text("CHON1DL"));
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
             }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, TABLE_NAME, ex.Message));
            }
        }

        private void f_DoChangeCodeFinish(IDictionary<string, object> data)
        {
            ReLoad();
        }

        private void DoDelete()
        {
            try
            {
                if (V6Login.UserRight.AllowDelete("", TABLE_NAME + "6"))
                {
                    if (tochucTree1.SelectedItems[0] != null)
                    {
                        var selected_item = tochucTree1.SelectedItems[0];
                        var data = tochucTree1.SelectedItemData;
                        var id = _v6LookupConfig.vValue;
                        var listTable = _v6LookupConfig.ListTable;
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
                                isDeleted = _categories.Delete(TABLE_NAME, keys);
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
                        V6ControlFormHelper.ShowMainMessage(V6Text.Text("CHON1DL"));
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
                if (V6Login.UserRight.AllowView("", TABLE_NAME + "6"))
                {
                    if (tochucTree1.SelectedItems[0] != null)
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

                        var f = new FormAddEdit(TABLE_NAME, V6Mode.View, keys, data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(this);
                        f.ShowDialog(this);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage(V6Text.Text("CHON1DL"));
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".NhanSuView2 DoView " + ex.Message);
            }
        }

        private void DoPrint()
        {
            try
            {
                if (V6Login.UserRight.AllowPrint("", TABLE_NAME + "6"))
                {
                    bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                    bool is_DX = false; // _aldmConfig.HaveInfo && _aldmConfig.EXTRA_INFOR.ContainsKey("XTRAREPORT") && _aldmConfig.EXTRA_INFOR["XTRAREPORT"] == "1";
                    if (shift) is_DX = !is_DX;
                    FormManagerHelper.ShowDanhMucPrint(this, TABLE_NAME, ReportFile, ReportTitle, ReportTitle2, true, is_DX);
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, TABLE_NAME, ex.Message));
            }
        }

        #endregion do method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <param name="sortField"></param>
        private void LoadTable(string tableName, string where, string sortField)
        {
            try
            {
                TABLE_NAME = tableName.ToUpper();

                _last_filter = GetNewWhere(where);
                var sr = V6BusinessHelper.Select(VIEW_NAME, "*", _last_filter, "", sortField);
                
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, VIEW_NAME), ex);
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
        
        public void ViewResultToForm()
        {
            #region --- NhanSuTreeView
            
            string showFields = _v6LookupConfig.GRDS_V1;
            string headerString = V6Setting.IsVietnamese ? _v6LookupConfig.GRDHV_V1 : _v6LookupConfig.GRDHE_V1;
            string formatStrings = _v6LookupConfig.GRDF_V1;
            
            tochucTree1.SetData(SelectResult.Data, showFields, headerString, formatStrings);
            #endregion nhansutreeview


            lblTotalPage.Text = string.Format(V6Setting.IsVietnamese ? "Tổng cộng: {0} " : "Total {0} ",
                SelectResult.TotalRows,
                string.IsNullOrEmpty(_last_filter)
                    ? ""
                    : (V6Setting.IsVietnamese ? "(Đã lọc)" : "(filtered)"));
        }


        /// <summary>
        /// Reload and setFormatGridView
        /// </summary>
        public void ReLoad()
        {
            try
            {
                SelectResult = new V6SelectResult();
                CloseFilterForm();
                LoadTable(TABLE_NAME, GetWhere(), SelectResult.SortField);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, TABLE_NAME, ex.Message));
            }
        }
        
        //Reload
        private void f_InsertSuccess(IDictionary<string, object> data)
        {
            tochucTree1.AddData(data);
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

            if(!string.IsNullOrEmpty(where))
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
        
        
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Setting.IsVietnamese
                ? "F3-Sửa, F4-Thêm, F5-Tìm, F6-Đổi mã, F8-Xóa"
                : "F3-Edit, F4-New, F5-Search, F6-Change Code, F8-Delete");
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
                if (V6Login.UserRight.AllowView("", TABLE_NAME + "6"))
                {
                    if (tochucTree1.SelectedItems[0] != null)
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
                            //ChiTietNhanSuResetData(sttRec, maNhanSu, data);
                            _stt_rec = sttRec;
                            _ma_ns = maNhanSu;
                            _nhanSuData = data;
                            ShowSelect();
                        }
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage(V6Text.Text("CHON1DL"));
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
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, TABLE_NAME), ex);
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

        private DataTable _menuData;
        private DataView _menuView;
        private string _last_filter;

        public void LoadListMenu()
        {
            try
            {
                _menuData = V6Menu.GetKey2Hm2();
                _menuView = new DataView(_menuData);
                //view.RowFilter = "1=0";
                listBoxMenu.DisplayMember = V6Setting.IsVietnamese ? "vbar" : "vbar2";
                listBoxMenu.DataSource = _menuView;
                listBoxMenu.DisplayMember = V6Setting.IsVietnamese ? "vbar" : "vbar2";
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, TABLE_NAME), ex);
            }
        }

        public void ShowSelect()
        {
            try
            {
                if (listBoxMenu.SelectedIndex == -1) return;
                if (string.IsNullOrEmpty(_stt_rec))
                {
                    V6ControlFormHelper.ShowMainMessage("Hãy chọn một nhân sự!");
                    return;
                }

                //var selectedItem = listBox1.SelectedItem;
                DataRowView menuRowView = listBoxMenu.SelectedItem as DataRowView;
                var menuRowData = menuRowView.Row.ToDataDictionary();

                var item_id = menuRowView["itemid"].ToString().Trim().ToUpper();
                var codeform = menuRowView["codeform"].ToString().Trim();

                foreach (var control in ControlsDictionary)
                {
                    if (control.Key != item_id) control.Value.Visible = false;
                    //else control.Value.Visible = true;
                }

                if (ControlsDictionary.ContainsKey(item_id) && !ControlsDictionary[item_id].IsDisposed)
                {
                    var selectControl = ControlsDictionary[item_id];
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

                    if (check) // Hiện lại control
                    {
                        if (!panelView.Contains(selectControl))
                        {
                            panelView.Controls.Add(selectControl);
                        }
                        selectControl.Visible = true;

                        V6ControlFormHelper.SetFormControlsReadOnly(selectControl, true);
                        selectControl.SetParentData(_nhanSuData);
                        selectControl.LoadData(_stt_rec);
                        if (selectControl is NoGridControl)
                        {
                            ((NoGridControl) selectControl).HideFilterControl();
                        }

                        if (selectControl is OneGridControl)
                        {
                            ((OneGridControl) selectControl).HideFilterControl();
                        }
                        selectControl.Focus();
                    }
                }
                else
                {
                    MenuButton mButton = new MenuButton(menuRowData);
                    var selectControl = MenuManager.MenuManager.GenControl(this, mButton, null);
                    
                    if (selectControl != null)
                    {
                        V6ControlFormHelper.SetFormControlsReadOnly(selectControl, true);
                        selectControl.SetParentData(_nhanSuData);

                        selectControl.LoadData(_stt_rec);

                        if (selectControl is NoGridControl)
                        {
                            ((NoGridControl)selectControl).HideFilterControl();
                        }

                        if (selectControl is OneGridControl)
                        {
                            ((OneGridControl)selectControl).HideFilterControl();
                        }
                        
                        selectControl.Disposed += delegate(object sender1, EventArgs e1)
                        {
                            ControlsDictionary.Remove(((Control)sender1).Name);
                            FormManagerHelper.ShowCurrentMenu3Menu();
                        };
                        ControlsDictionary[item_id] = selectControl;
                        panelView.Controls.Add(selectControl);
                        selectControl.Focus();
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
