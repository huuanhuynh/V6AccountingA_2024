using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.NhanSu
{
    public partial class ToChucView : V6FormControl
    {
        public ToChucView()
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
                this.ShowErrorMessage(GetType() + ".ToChucView MyInit " + ex.Message);
            }
        }

        public ToChucView(string itemId, string title, string procedure, string initFilter = "")
        {
            m_itemId = itemId;
            Title = title;
            _procedure = procedure;
            TABLE_NAME = "HRPERSONAL";
            _v6LookupConfig = V6Lookup.GetV6lookupConfigByTableName(TABLE_NAME);
            _aldmConfig = ConfigManager.GetAldmConfigByTableName(TABLE_NAME);
            InitializeComponent();
            MyInit();
            
            InitFilter = initFilter;
            SelectResult = new V6SelectResult();

            CloseFilterForm();
            LoadData();
        }

        private void ToChucView_Load(object sender, EventArgs e)
        {
            FormManagerHelper.HideMainMenu();
            MakeStatus2Text();
        }

        public string _procedure;
        public string TABLE_NAME;
        public string VIEW_NAME = "VPRDMNS";
        public V6lookupConfig _v6LookupConfig;
        public AldmConfig _aldmConfig = new AldmConfig();
        [DefaultValue(V6TableName.None)]
        //public V6TableName CurrentTable { get; set; }
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
                //int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;
                //var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName = "";

                IDictionary<string, object> someData = new SortedDictionary<string, object>();
                someData["STATUS"] = 1;
                if (fSort == 9 || toChucTreeListView1.SelectedItems[0].Items.Count == 0)
                {
                    tableName = "Hrpersonal";
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    
                    if (fSort == 9)
                    {
                        keys["STT_REC"] = selectedItemData["NODE"];
                        var selectResult = V6BusinessHelper.Select("HRPOSITION", keys, "*");
                        if (selectResult.TotalRows == 1)
                        {
                            var row = selectResult.Data.Rows[0];
                            someData["POSITION_ID"] = row["POSITION_ID"];
                            someData["ORGUNIT_ID"] = row["ORGUNIT_ID"];
                        }
                        
                        someData["STT_REC"] = selectedItemData["NODE"];
                        someData["MA_BP"] = selectedItemData["MA_BP"];
                        someData["MA_CV"] = selectedItemData["MA_CV"];
                    }
                    else
                    {
                        someData["ORGUNIT_ID"] = selectedItemData["PARENT"];
                        someData["MA_BP"] = selectedItemData["MA_BP"];
                    }
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ID", selectedItemData["NODE"]);
                    var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        //TYPE, OFFICE, PARENTUNIT
                        someData["TYPE"] = row["TYPE"];
                        someData["OFFICE"] = row["OFFICE"];
                        someData["PARENTUNIT"] = row["ID"];
                        someData["PICTURE"] = row["PICTURE"];
                        someData["STATUS"] = 1;
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.CheckData);
                    }
                }

                if (V6Login.UserRight.AllowAdd("", tableName.ToUpper() + "6"))
                {
                    var f = new FormAddEdit(tableName, V6Mode.Add, null, someData);
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
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + ma_ct, ex);
            }
        }

        private void DoAddCopy()
        {
            try
            {
                //int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;
                //var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                IDictionary<string, object> someData = new SortedDictionary<string, object>();

                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    IDictionary<string, object> keys = new Dictionary<string, object>();

                    keys["STT_REC"] = selectedItemData["NODE"];
                    var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        someData = row.ToDataDictionary();
                    }
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ID", selectedItemData["NODE"]);
                    var selectResult = V6BusinessHelper.Select("HRLSTORGUNIT", keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        someData = row.ToDataDictionary();
                        //TYPE, OFFICE, PARENTUNIT
                        //someData["TYPE"] = row["TYPE"];
                        //someData["OFFICE"] = row["OFFICE"];
                        //someData["PARENTUNIT"] = row["ID"];
                        //someData["PICTURE"] = row["PICTURE"];
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.CheckData);
                    }
                }

                if (V6Login.UserRight.AllowCopy("", tableName.ToUpper() + "6"))
                {
                    var f = new FormAddEdit(tableName, V6Mode.Add, null, someData);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.UpdateSuccessEvent += f_UpdateSuccess;
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
                //int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;
                //var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                IDictionary<string, object> someData = new SortedDictionary<string, object>();

                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    IDictionary<string, object> keys = new Dictionary<string, object>();

                    keys["STT_REC"] = selectedItemData["NODE"];
                    var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        someData = row.ToDataDictionary();
                    }
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ID", selectedItemData["NODE"]);
                    var selectResult = V6BusinessHelper.Select("HRLSTORGUNIT", keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        someData = row.ToDataDictionary();
                        //TYPE, OFFICE, PARENTUNIT
                        //someData["TYPE"] = row["TYPE"];
                        //someData["OFFICE"] = row["OFFICE"];
                        //someData["PARENTUNIT"] = row["ID"];
                        //someData["PICTURE"] = row["PICTURE"];
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.CheckData);
                    }
                }

                if (V6Login.UserRight.AllowAdd("", tableName.ToUpper() + "6"))
                {
                    var f = new FormAddEdit(tableName, V6Mode.Edit, null, someData);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.UpdateSuccessEvent += f_UpdateSuccess;
                    f.ShowDialog(this);
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
                //int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;
                //var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                IDictionary<string, object> someData = new SortedDictionary<string, object>();
                //AldmConfig aldm_config = new AldmConfig();
                //V6lookupConfig v6lookup_config = new V6lookupConfig();
                
                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    //aldm_config = V6BusinessHelper.GetAldmConfigByTableName(tableName);
                    //v6lookup_config = V6Lookup.GetV6lookupConfigByTableName(tableName);
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys["STT_REC"] = selectedItemData["NODE"];
                    var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        someData = row.ToDataDictionary();
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.CheckData);
                    }
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    //aldm_config = V6BusinessHelper.GetAldmConfigByTableName(tableName);
                    //v6lookup_config = V6Lookup.GetV6lookupConfigByTableName(tableName);
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ID", selectedItemData["NODE"]);
                    var selectResult = V6BusinessHelper.Select("HRLSTORGUNIT", keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        someData = row.ToDataDictionary();
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.CheckData);
                    }
                }

                if (V6Login.UserRight.AllowAdd("", tableName.ToUpper() + "6")
                    && V6Login.UserRight.AllowEdit("", tableName.ToUpper() + "6"))
                {
                    var f = DanhMucManager.ChangeCode.ChangeCodeManager.GetChangeCodeControl(tableName, someData);
                    if (f != null)
                    {
                        f.DoChangeCodeFinish += f_DoChangeCodeFinish;
                        f.ShowDialog(this);
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _procedure, ex.Message));
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
                //int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;
                var t = 0;
                string confirm_value1 = "";
                //var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                DataRow row0 = null;
                var delete_keys = new SortedDictionary<string, object>();
                AldmConfig aldm_config = new AldmConfig();
                V6lookupConfig v6lookup_config = new V6lookupConfig();

                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    v6lookup_config = V6Lookup.GetV6lookupConfigByTableName(tableName);

                    IDictionary<string, object> select_keys = new Dictionary<string, object>();

                    select_keys["STT_REC"] = selectedItemData["NODE"];
                    var selectResult = V6BusinessHelper.Select(tableName, select_keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        row0 = row;
                        delete_keys["UID"] = row["UID"];
                        confirm_value1 = row["EMP_ID"].ToString();
                    }
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    aldm_config = ConfigManager.GetAldmConfigByTableName(tableName);
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ID", selectedItemData["NODE"]);
                    var selectResult = V6BusinessHelper.Select("HRLSTORGUNIT", keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        row0 = row;
                        delete_keys["UID"] = row["UID"];
                        confirm_value1 = row["NAME"].ToString();
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.CheckData);
                    }
                }

                if (row0 == null)
                {
                    this.ShowWarningMessage(V6Text.CheckData);
                    return;
                }

                if (V6Login.UserRight.AllowDelete("", tableName.ToUpper() + "6"))
                {
                    //var data = row0.ToDataDictionary();
                    var id = aldm_config.HaveInfo ? aldm_config.TABLE_KEY : v6lookup_config.vValue;
                    var id_check = aldm_config.HaveInfo ? aldm_config.DOI_MA : v6lookup_config.DOI_MA;
                    var listTable = aldm_config.HaveInfo ? aldm_config.F8_TABLE : v6lookup_config.ListTable;

                    if (!string.IsNullOrEmpty(listTable) && !string.IsNullOrEmpty(id))
                    {
                        //if (data.ContainsKey(id.ToUpper()))
                        //    checkValid_value = data[id.ToUpper()].ToString().Trim();
                        var v = V6BusinessHelper.IsExistOneCode_List(listTable, id_check, row0[id].ToString().Trim());
                        if (v)
                        {
                            //khong duoc
                            this.ShowWarningMessage(V6Text.DaPhatSinh_KhongDuocXoa);
                            return;
                        }
                    }

                    if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + confirm_value1, V6Text.DeleteConfirm) ==
                        DialogResult.Yes)
                    {
                        if (fSort == 9)
                        {
                            V6BusinessHelper.ExecuteProcedureNoneQuery("VPH_DELETE_AUTO_FROM_PERSONAL",
                                new SqlParameter("@UID", delete_keys["UID"].ToString()));
                        }

                        t = V6BusinessHelper.Delete(tableName, delete_keys);

                        if (t > 0)
                        {
                            ReLoad();
                            V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                        }
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning(this);
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
                //int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;
                //var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                IDictionary<string, object> someData = new SortedDictionary<string, object>();

                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    IDictionary<string, object> keys = new Dictionary<string, object>();

                    keys["STT_REC"] = selectedItemData["NODE"];
                    var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        someData = row.ToDataDictionary();
                    }
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ID", selectedItemData["NODE"]);
                    var selectResult = V6BusinessHelper.Select("HRLSTORGUNIT", keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        var row = selectResult.Data.Rows[0];
                        someData = row.ToDataDictionary();
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.CheckData);
                    }
                }

                if (V6Login.UserRight.AllowView("", tableName.ToUpper() + "6"))
                {
                    var f = new FormAddEdit(tableName, V6Mode.View, null, someData);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.ShowDialog(this);
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ToChucView DoView " + ex.Message);
            }
        }

        private void DoPrint()
        {
             try
            {
                //int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;

                //var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                //IDictionary<string, object> someData = new SortedDictionary<string, object>();
                AldmConfig aldm_config = new AldmConfig();
                V6lookupConfig v6lookup_config = new V6lookupConfig();
                string reportFile = "", reportTitle = "", reportTitle2 = "";

                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    //aldm_config = V6BusinessHelper.GetAldmConfigByTableName(tableName);
                    v6lookup_config = V6Lookup.GetV6lookupConfigByTableName(tableName);
                    reportTitle = v6lookup_config.vTitle;
                    reportTitle2 = v6lookup_config.eTitle;
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    aldm_config = ConfigManager.GetAldmConfigByTableName(tableName);
                    //v6lookup_config = V6Lookup.GetV6lookupConfigByTableName(tableName);
                    reportTitle = aldm_config.TITLE;
                    reportTitle2 = aldm_config.TITLE2;
                }

                if (V6Login.UserRight.AllowPrint("", tableName.ToUpper() + "6"))
                {
                    bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                    bool is_DX = _aldmConfig.HaveInfo && _aldmConfig.EXTRA_INFOR.ContainsKey("XTRAREPORT") && _aldmConfig.EXTRA_INFOR["XTRAREPORT"] == "1";
                    if (shift) is_DX = !is_DX;
                    FormManagerHelper.ShowDanhMucPrint(this, tableName, tableName, reportTitle, reportTitle2, true, is_DX);
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _procedure, ex.Message));
            }
        }

        #endregion do method

        private void LoadData()
        {
            try
            {
                _last_filter = GetWhere();
                SqlParameter[] plist =
                {
                    new SqlParameter("@Advance", _last_filter),
                    new SqlParameter("@User_id", V6Login.UserId),
                };
                var ds = V6BusinessHelper.ExecuteProcedure(_procedure, plist);
                if (ds == null || ds.Tables.Count == 0)
                {
                    this.ShowErrorMessage(V6Text.NoData);
                    return;
                }
                
                SelectResult.Data = ds.Tables[0];
                SelectResult.TotalRows = SelectResult.Data.Rows.Count;
                
                ViewResultToForm();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _procedure), ex);
            }
        }
        
        public void ViewResultToForm()
        {
            AldmConfig config = ConfigManager.GetAldmConfigByTableName(_procedure);

            string showFields = config.GRDS_V1;
            string headerString = V6Setting.IsVietnamese ? config.GRDHV_V1 : config.GRDHE_V1;
            string formatStrings = config.GRDF_V1;
            
            toChucTreeListView1.SetData(SelectResult.Data, showFields, headerString, formatStrings);
            
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
                LoadData();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _procedure, ex.Message));
            }
        }
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            DoAdd();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            DoAddCopy();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DoView();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowPrint("", TABLE_NAME.ToUpper() + "6"))
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


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (NO_CONFIG_FPASS(0) || new ConfirmPasswordF368().ShowDialog(this) == DialogResult.OK)
            {
                DoEdit();
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
        private string InitFilter;
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
                V6TableStruct structTable = V6BusinessHelper.GetTableStruct(TABLE_NAME);

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
                _filterForm.Show();
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


        private void toChucTreeListView1_Click(object sender, EventArgs e)
        {
            //DoTreeClick();
        }

    }
}
