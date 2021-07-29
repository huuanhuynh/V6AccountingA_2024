using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".NhanSuView", ex);
            }
        }

        public NhanSuView(string itemId, string title, string tableName, string initFilter = "", string sort="")
        {
            m_itemId = itemId;
            Title = title;
            TABLE_NAME = tableName.ToUpper();
            _v6LookupConfig = V6Lookup.GetV6lookupConfigByTableName(TABLE_NAME);
            CurrentTable = V6TableHelper.ToV6TableName(tableName);

            InitializeComponent();
            MyInit();
            
            InitFilter = initFilter;
            SelectResult = new V6SelectResult();

            if (CurrentTable == V6TableName.V_alts || CurrentTable == V6TableName.V_alcc
                || CurrentTable == V6TableName.V_alts01 || CurrentTable == V6TableName.V_alcc01)
            {
                btnCopy.Visible = false;
                btnDoiMa.Visible = false;
                btnIn.Visible = false;
            }

            CloseFilterForm();
            LoadTable(CurrentTable, sort);
        }

        private void NhanSuView_Load(object sender, EventArgs e)
        {
            FormManagerHelper.HideMainMenu();
            if (CurrentTable == V6TableName.Hlns)
            {
                KeyFields = new[] { "MA_BP", "MA_CV", "MA_NS" };
            }
            MakeStatus2Text();
        }

        private string TABLE_NAME;
        private string VIEW_NAME = "VPRDMNSTREE";
        private V6lookupConfig _v6LookupConfig;
        private AldmConfig _aldmConfig = new AldmConfig();
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
        
        #region ==== Do method ====

        public override void DoHotKey(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.F6))
                {

                }
                else if (keyData == Keys.F9)
                {
                    if (!V6BusinessHelper.CheckRightKey("", "F9", TABLE_NAME)) return;
                    InvokeFormEvent(FormDynamicEvent.F9);
                }
                else if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoHotKey", ex);
            }
            base.DoHotKey(keyData);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                if (Navigation(keyData)) return true;
            }
            return base.DoHotKey0(keyData);
        }

        private void DoAdd()
        {
            try
            {
                if (V6Login.UserRight.AllowAdd("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (CurrentTable != V6TableName.None)
                    {
                        IDictionary<string, object> data = new SortedDictionary<string, object>();
                        if (treeListViewAuto1.SelectedItems.Count>0)
                        {
                            data = treeListViewAuto1.SelectedItemData;
                        }

                        string stt_rec = data["STT_REC"].ToString().Trim();
                        if (stt_rec == "")
                        {
                            //Nếu node đang chọn ko phải ct, ko có con và ...
                            if (treeListViewAuto1.SelectedItems[0].Items.Count == 0
                                && data.ContainsKey("ORGUNIT_ID") && data["ORGUNIT_ID"] != null && data["ORGUNIT_ID"].ToString() != "")
                            {
                                //Xử lý lại phần someData trong Add/Edit
                                IDictionary<string, object> someData = new SortedDictionary<string, object>();
                                someData["STT_REC"] = "";
                                someData["ORGUNIT_ID"] = data["ORGUNIT_ID"];
                                //someData["POSITION_ID"] = data["POSITION_ID"];

                                var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, null, someData);
                                f.AfterInitControl += f_AfterInitControl;
                                f.InitFormControl(this);
                                f.InsertSuccessEvent += f_InsertSuccess;
                                f.ShowDialog(this);
                            }
                            else
                            {
                                //Tạo data nhóm.
                                //if (treeListViewAuto1.SelectedItems[0].Level != 2)
                                {
                                    DoNothing();
                                }
                            }
                        }
                        else
                        {
                            IDictionary<string, object> someData = new SortedDictionary<string, object>();
                            someData["STT_REC"] = data["STT_REC"];

                            var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, null, someData);
                            f.AfterInitControl += f_AfterInitControl;
                            f.InitFormControl(this);
                            f.InsertSuccessEvent += f_InsertSuccess;
                            f.ShowDialog(this);
                        }
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

        void f_AfterInitControl(object sender, EventArgs e)
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
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
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
                        IDictionary<string, object> data = new SortedDictionary<string, object>();
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

                        var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, keys, null);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(this);
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
                        
                        var selected_item_data = treeListViewAuto1.SelectedItemData;
                        var stt_rec = selected_item_data["STT_REC"].ToString().Trim();
                        if (stt_rec != "")
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

                            if (selected_item_data.ContainsKey("STT_REC"))
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
                            var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Edit, keys, null);
                            f.AfterInitControl += f_AfterInitControl;
                            f.InitFormControl(this);
                            f.UpdateSuccessEvent += f_UpdateSuccess;
                            
                            f.ShowDialog(this);
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMainMessage("Hãy chọn một nhân viên!");
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

        
        /// <summary>
        /// Khi sửa thành công, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        /// <param name="data">Dữ liệu đã sửa</param>
        private void f_UpdateSuccess(IDictionary<string, object> data)
        {
            try
            {
                ReLoad();
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
                        if (treeListViewAuto1.SelectedItems[0] != null)
                        {
                            IDictionary<string, object> data = new SortedDictionary<string, object>();
                            data.AddRange(treeListViewAuto1.SelectedItems[0].ToNhanSuDictionary());

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
                if (V6Login.UserRight.AllowDelete("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (treeListViewAuto1.SelectedItems[0] != null)
                        //&& treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                    {
                        var selected_item = treeListViewAuto1.SelectedItems[0];
                        var data = treeListViewAuto1.SelectedItemData;
                        object UID = data["UID"];
                        var id = _v6LookupConfig.vValue;
                        var listTable = _v6LookupConfig.ListTable;
                        var value = selected_item.Name;

                        if (!string.IsNullOrEmpty(listTable) && !string.IsNullOrEmpty(id))
                        {
                            if(data.ContainsKey(id.ToUpper()))
                                value = data[id.ToUpper()].ToString().Trim();
                            var v = V6BusinessHelper.IsExistOneCode_List(listTable, id, value);
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
                            var keys = new SortedDictionary<string, object> {{"UID", UID}};


                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + value, V6Text.DeleteConfirm)
                                == DialogResult.Yes)
                            {
                                SqlParameter[] plist =
                                {
                                    new SqlParameter("@UID", UID.ToString()),
                                };

                                if (V6BusinessHelper.ExecuteProcedureNoneQuery("VPH_DELETE_AUTO_FROM_PERSONAL", plist) > 0)
                                {
                                    isDeleted = 1;
                                }
                                if (V6BusinessHelper.Delete(CurrentTable, keys) > 0)
                                {
                                    isDeleted++;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }

                        if (isDeleted == 2)
                        {
                            //ReLoad();
                            //treeListViewAuto1.Items.Remove(selected_item);
                            selected_item.Remove();
                            V6ControlFormHelper.ShowMessage(V6Text.DeleteSuccess);
                        }
                        else if (isDeleted == 1)
                        {
                            this.WriteToLog(GetType() + ".DoDelete", "Xóa dang dở UID:" + UID);
                            V6ControlFormHelper.ShowMessage("Xóa dang dở UID:" + UID);
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
                if (V6Login.UserRight.AllowView("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    if (CurrentTable == V6TableName.None)
                    {
                        V6ControlFormHelper.ShowMainMessage("Hãy chọn danh mục!");
                    }
                    else
                    {
                        if (treeListViewAuto1.SelectedItems[0] != null)
                            //&& treeListViewAuto1.SelectedItems[0].Level == treeListViewAuto1.MaxLevel)
                        {
                            var data = treeListViewAuto1.SelectedItemData;

                            var keys = new SortedDictionary<string, object>();
                            if (data.ContainsKey("UID"))
                                keys.Add("UID", data["UID"].ToString());
                            if (data.ContainsKey("STT_REC"))
                                keys.Add("STT_REC", data["STT_REC"].ToString());

                            if (KeyFields != null)
                                foreach (var keyField in KeyFields)
                                {
                                    var KEYFIELD = keyField.ToUpper();
                                    if (treeListViewAuto1.Columns.ContainsKey(KEYFIELD))
                                    {
                                        keys[KEYFIELD] = data[KEYFIELD];
                                    }
                                }

                            var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.View, keys, null);
                            f.AfterInitControl += f_AfterInitControl;
                            f.InitFormControl(this);
                            f.ShowDialog(this);
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMainMessage(V6Text.Text("CHON1DL"));
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
                this.ShowErrorException(GetType() + ".DoView", ex);
            }
        }

        private void DoPrint()
        {
             try
            {
                if (V6Login.UserRight.AllowPrint("", CurrentTable.ToString().ToUpper() + "6"))
                {
                    bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                    bool is_DX = _aldmConfig.HaveInfo && _aldmConfig.EXTRA_INFOR.ContainsKey("XTRAREPORT") && _aldmConfig.EXTRA_INFOR["XTRAREPORT"] == "1";
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
        /// <param name="sortField"></param>
        private void LoadTable(V6TableName tableName, string sortField)
        {
            try
            {
                CurrentTable = tableName;

                _last_filter = GetWhere();
                var sr = V6BusinessHelper.Select(VIEW_NAME, "*", _last_filter, "", sortField);
                
                SelectResult.Data = sr.Data;
                
                SelectResult.Page = sr.Page;
                
                SelectResult.TotalRows = sr.TotalRows;
                SelectResult.PageSize = sr.PageSize;
                SelectResult.Fields = sr.Fields;
                SelectResult.FieldsHeaderDictionary = sr.FieldsHeaderDictionary;
                SelectResult.Where = _last_filter;// sr.Where;
                SelectResult.SortField = sr.SortField;
                SelectResult.IsSortOrderAscending = sr.IsSortOrderAscending;

                ViewResultToForm();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, VIEW_NAME), ex);
            }
        }
        
        public void ViewResultToForm()
        {
            string showFields = _v6LookupConfig.GRDS_V1;
            string headerString = V6Setting.IsVietnamese ? _v6LookupConfig.GRDHV_V1 : _v6LookupConfig.GRDHE_V1;
            string formatStrings = _v6LookupConfig.GRDF_V1;
            
            treeListViewAuto1.SetData(SelectResult.Data, showFields, headerString, formatStrings);
            
            lblTotalPage.Text = string.Format(V6Setting.IsVietnamese ? "Tổng cộng: {0} " : "Total {0} ",
                SelectResult.TotalRows,
                string.IsNullOrEmpty(_last_filter)
                    ? ""
                    : (V6Setting.IsVietnamese ? "(Đã lọc)" : "(filtered)"));
        }
        
        /// <summary>
        /// Reload
        /// </summary>
        public void ReLoad()
        {
            try
            {
                LoadTable(CurrentTable, SelectResult.SortField);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, TABLE_NAME, ex.Message));
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
        private void f_InsertSuccess(IDictionary<string, object> data)
        {
            ReLoad();
            //treeListViewAuto1.AddData(data);
        }

        /// <summary>
        /// Kiểm tra có cấu hình Fpass
        /// </summary>
        /// <param name="index">0 cho F3, 1 cho F6, 2 cho F8</param>
        /// <returns></returns>
        bool NO_CONFIG_FPASS(int index)
        {
            if (_aldmConfig.NoInfo) return true;
            if (!_aldmConfig.EXTRA_INFOR.ContainsKey("F368_PASS")) return true;
            string f368 = _aldmConfig.EXTRA_INFOR["F368_PASS"].Trim();
            if (f368.Length > index && f368[index] == '1') return false;
            return true;
        }


        //private IDictionary<string, object> _data = new SortedDictionary<string, object>();
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6"))
            {
                if (NO_CONFIG_FPASS(0) || new ConfirmPasswordF368().ShowDialog(this) == DialogResult.OK)
                {
                    DoEdit();
                }
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }
        private void btnDoiMa_Click(object sender, EventArgs e)
        {
            if (NO_CONFIG_FPASS(1) || new ConfirmPasswordF368().ShowDialog(this) == DialogResult.OK)
            {
                DoChangeCode();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (NO_CONFIG_FPASS(2) || new ConfirmPasswordF368().ShowDialog(this) == DialogResult.OK)
            {
                DoDelete();
            }
        }


        private FilterForm _filterForm;
        private readonly string InitFilter;
        private string _search;

        private string GetWhere()
        {
            string result = "";
            if (!string.IsNullOrEmpty(InitFilter))
            {
                result = InitFilter;
            }

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
                _filterForm = new FilterForm(structTable, fields, null);
                _filterForm.FilterApplyEvent += FilterFilterApplyEvent;
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

        void FilterFilterApplyEvent(string query)
        {
            _search = query;
            ReLoad();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            _search = "";
            ReLoad();
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
        private string _last_filter;

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
