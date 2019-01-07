﻿using System;
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

        public ToChucView(string itemId, string title, string procedure, string initFilter = "", string sort="")
        {
            m_itemId = itemId;
            Title = title;
            _procedure = procedure;
            //CurrentTable = V6TableHelper.ToV6TableName(tableName);
            //CurrentTable = V6TableName.HRPERSONAL
            _tableName = "HRPERSONAL";
            _config = V6Lookup.GetV6lookupConfigByTableName(_tableName);
            InitializeComponent();
            MyInit();
            
            _hideColumnDic = _categories.GetHideColumns(procedure);
            InitFilter = initFilter;
            SelectResult = new V6SelectResult();

            LoadTable(_tableName, sort);
        }

        private void ToChucView_Load(object sender, EventArgs e)
        {
            FormManagerHelper.HideMainMenu();
            MakeStatus2Text();
        }

        private readonly V6Categories _categories = new V6Categories();
        private SortedDictionary<string, string> _hideColumnDic; 
        private string _procedure;
        private string _tableName;
        private string _viewName = "VPRDMNS";
        private V6lookupConfig _config;
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
        
        private void SetFormatGridView()
        {
            
        }

        #region ==== Do method ====

        private void DoAdd()
        {
            try
            {
                int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;

                var tag = toChucTreeListView1.SelectedItems[0].Tag;
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
                    f.InitFormControl();
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
            LoadAdvanceControls((Control)sender, _tableName.ToString());
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
                int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;

                var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                IDictionary<string, object> someData = new SortedDictionary<string, object>();

                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    IDictionary<string, object> keys = new Dictionary<string, object>();

                    if (fSort == 9)
                    {
                        keys["STT_REC"] = selectedItemData["NODE"];
                        var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                        if (selectResult.TotalRows == 1)
                        {
                            var row = selectResult.Data.Rows[0];
                            someData = row.ToDataDictionary();
                        }
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
                    f.InitFormControl();
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
                int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;

                var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                IDictionary<string, object> someData = new SortedDictionary<string, object>();

                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    IDictionary<string, object> keys = new Dictionary<string, object>();

                    if (fSort == 9)
                    {
                        keys["STT_REC"] = selectedItemData["NODE"];
                        var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                        if (selectResult.TotalRows == 1)
                        {
                            var row = selectResult.Data.Rows[0];
                            someData = row.ToDataDictionary();
                        }
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
                    f.InitFormControl();
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
                ReLoad();

                //if (data == null) return;
                //var dictionary = new SortedDictionary<string, string>();
                //dictionary.AddRange(data);
                //treeListViewAuto1.UpdateSelectedItemData(data);
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
                int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;

                var tag = toChucTreeListView1.SelectedItems[0].Tag;
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
                    if (fSort == 9)
                    {
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
                int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;
                var t = 0;
                string confirm_value1 = "";
                var tag = toChucTreeListView1.SelectedItems[0].Tag;
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
                    //aldm_config = V6BusinessHelper.GetAldmConfigByTableName(tableName);
                    v6lookup_config = V6Lookup.GetV6lookupConfigByTableName(tableName);

                    IDictionary<string, object> select_keys = new Dictionary<string, object>();

                    if (fSort == 9)
                    {
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
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    aldm_config = ConfigManager.GetAldmConfigByTableName(tableName);
                    //v6lookup_config = V6Lookup.GetV6lookupConfigByTableName(tableName);
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
                    var data = row0.ToDataDictionary();
                    var id = aldm_config.HaveInfo ? aldm_config.TABLE_KEY : v6lookup_config.vValue;
                    var id_check = aldm_config.HaveInfo ? aldm_config.DOI_MA : v6lookup_config.DOI_MA;
                    var listTable = aldm_config.HaveInfo ? aldm_config.F8_TABLE : v6lookup_config.ListTable;

                    if (!string.IsNullOrEmpty(listTable) && !string.IsNullOrEmpty(id))
                    {
                        //if (data.ContainsKey(id.ToUpper()))
                        //    checkValid_value = data[id.ToUpper()].ToString().Trim();
                        var v = _categories.IsExistOneCode_List(listTable, id_check, row0[id].ToString().Trim());
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

                        t = _categories.Delete(tableName, delete_keys);

                        if (t > 0)
                        {
                            ReLoad();
                            V6ControlFormHelper.ShowMainMessage("Đã xóa!");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning(this);
                }
                return;
                ////fsort == 9 + else
                //if (V6Login.UserRight.AllowDelete("", CurrentTable.ToString().ToUpper() + "6"))
                //{
                //    if (toChucTreeListView1.SelectedItems[0] != null)
                //    {
                //        var selected_item = toChucTreeListView1.SelectedItems[0];
                //        var data = toChucTreeListView1.SelectedItemData;
                //        object UID = data["UID"];
                //        var id = V6Lookup.GetValueByTableName(CurrentTable.ToString(), "vValue"].ToString().Trim();
                //        var listTable =
                //            V6Lookup.GetValueByTableName(CurrentTable.ToString(), "ListTable"].ToString().Trim();
                //        var value = selected_item.Name;

                //        if (!string.IsNullOrEmpty(listTable) && !string.IsNullOrEmpty(id))
                //        {
                //            if(data.ContainsKey(id.ToUpper()))
                //                value = data[id.ToUpper()].ToString().Trim();
                //            var v = _categories.IsExistOneCode_List(listTable, id, value);
                //            if (v)
                //            {
                //                //khong duoc
                //                this.ShowWarningMessage(V6Text.DaPhatSinh_KhongDuocXoa);
                //                return;
                //            }
                //        }
                        
                //        var isDeleted = 0;

                //        if (data.ContainsKey("UID"))
                //        {
                //            var keys = new SortedDictionary<string, object> {{"UID", UID}};


                //            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + value, V6Text.DeleteConfirm)
                //                == DialogResult.Yes)
                //            {
                //                SqlParameter[] plist =
                //                {
                //                    new SqlParameter("@UID", UID.ToString()),
                //                };

                //                if (V6BusinessHelper.ExecuteProcedureNoneQuery("VPH_DELETE_AUTO_FROM_PERSONAL", plist) > 0)
                //                {
                //                    isDeleted = 1;
                //                }
                //                if (_categories.Delete(CurrentTable, keys) > 0)
                //                {
                //                    isDeleted++;
                //                }
                //            }
                //            else
                //            {
                //                return;
                //            }
                //        }
                //        if (isDeleted == 2)
                //        {
                //            //ReLoad();
                //            //treeListViewAuto1.Items.Remove(selected_item);
                //            selected_item.Remove();
                //            V6ControlFormHelper.ShowMessage(V6Text.DeleteSuccess);
                //        }
                //        else if (isDeleted == 1)
                //        {
                //            this.WriteToLog(GetType() + ".DoDelete", "Xóa dang dở UID:" + UID);
                //            V6ControlFormHelper.ShowMessage("Xóa dang dở UID:" + UID);
                //        }
                //        else
                //        {
                //            V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                //        }
                        
                //    }
                //    else
                //    {
                //        V6ControlFormHelper.ShowMainMessage("Hãy chọn một dòng dữ liệu!");
                //    }
                //}
                //else
                //{
                //    V6ControlFormHelper.NoRightWarning();
                //}
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
                int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;

                var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                IDictionary<string, object> someData = new SortedDictionary<string, object>();

                if (fSort == 9)
                {
                    tableName = "Hrpersonal";
                    IDictionary<string, object> keys = new Dictionary<string, object>();

                    if (fSort == 9)
                    {
                        keys["STT_REC"] = selectedItemData["NODE"];
                        var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                        if (selectResult.TotalRows == 1)
                        {
                            var row = selectResult.Data.Rows[0];
                            someData = row.ToDataDictionary();
                        }
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
                    f.InitFormControl();
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
                int level = toChucTreeListView1.SelectedItems[0].Level;
                //if (level != ) return;

                var tag = toChucTreeListView1.SelectedItems[0].Tag;
                var selectedItemData = toChucTreeListView1.SelectedItemData;
                int fSort = ObjectAndString.ObjectToInt(selectedItemData["FSORT"]);
                string tableName;
                IDictionary<string, object> someData = new SortedDictionary<string, object>();
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
                    //IDictionary<string, object> keys = new Dictionary<string, object>();
                    //if (fSort == 9)
                    //{
                    //    keys["STT_REC"] = selectedItemData["NODE"];
                    //    var selectResult = V6BusinessHelper.Select(tableName, keys, "*");
                    //    if (selectResult.TotalRows == 1)
                    //    {
                    //        var row = selectResult.Data.Rows[0];
                    //        someData = row.ToDataDictionary();
                    //    }
                    //}
                }
                else
                {
                    tableName = "HRLSTORGUNIT";
                    aldm_config = ConfigManager.GetAldmConfigByTableName(tableName);
                    //v6lookup_config = V6Lookup.GetV6lookupConfigByTableName(tableName);
                    reportTitle = aldm_config.TITLE;
                    reportTitle2 = aldm_config.TITLE2;
                    //IDictionary<string, object> keys = new Dictionary<string, object>();
                    //keys.Add("ID", selectedItemData["NODE"]);
                    //var selectResult = V6BusinessHelper.Select("HRLSTORGUNIT", keys, "*");
                    //if (selectResult.TotalRows == 1)
                    //{
                    //    var row = selectResult.Data.Rows[0];
                    //    someData = row.ToDataDictionary();
                    //}
                    //else
                    //{
                    //    this.ShowWarningMessage(V6Text.CheckData);
                    //}
                }

                if (V6Login.UserRight.AllowPrint("", tableName.ToUpper() + "6"))
                {
                    FormManagerHelper.ShowDanhMucPrint(this, tableName, tableName, reportTitle, reportTitle2);
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


        /// <summary>
        /// Được gọi từ DanhMucControl
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="sort"></param>
        public void LoadTable(string tableName, string sort)
        {
            SelectResult = new V6SelectResult();
            CloseFilterForm();
            int pageSize = 0;
            
            LoadData(tableName, 1, pageSize, sort, true);
        }

        private void LoadData(string tableName, int page, int size, string sortField, bool ascending)
        {
            try { 
                
                //var sr = _categories.SelectPaging(tableName, "*", page, size, GetWhere(where), sortField, ascending);
                //var sr = V6BusinessHelper.Select(_viewName, "*", where, "", sortField);
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
                
                //SelectResult.Page = sr.Page;
                
                //SelectResult.TotalRows = sr.TotalRows;
                //SelectResult.PageSize = sr.PageSize;
                //SelectResult.Fields = sr.Fields;
                //SelectResult.FieldsHeaderDictionary = sr.FieldsHeaderDictionary;
                //SelectResult.Where = _last_filter;// sr.Where;
                //SelectResult.SortField = sr.SortField;
                //SelectResult.IsSortOrderAscending = sr.IsSortOrderAscending;

                ViewResultToForm();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _procedure), ex);
            }
        }

        private void LoadAtPage(int page)
        {
            LoadData(_tableName, page, 0, SelectResult.SortField, SelectResult.IsSortOrderAscending);
        }


        public void ViewResultToForm()
        {
            //nhanSuTreeView1.FieldsHeaderDictionary = SelectResult.FieldsHeaderDictionary;
            //nhanSuTreeView1.HideColumnDic = _hideColumnDic;
            //nhanSuTreeView1.DataSource = SelectResult.Data;

            AldmConfig config = ConfigManager.GetAldmConfigByTableName(_procedure);

            string showFields = config.GRDS_V1;
            string headerString = V6Setting.IsVietnamese ? config.GRDHV_V1 : config.GRDHE_V1;
            string formatStrings = config.GRDF_V1;
            
            //V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);

            toChucTreeListView1.SetData(SelectResult.Data, showFields, headerString, formatStrings);
            //treeView1.data

            lblTotalPage.Text = string.Format(V6Setting.IsVietnamese ? "Tổng cộng: {0} " : "Total {0} ",
                SelectResult.TotalRows,
                string.IsNullOrEmpty(_last_filter)
                    ? ""
                    : (V6Setting.IsVietnamese ? "(Đã lọc)" : "(filtered)"));
        }


        public void First()
        {
            try {
                LoadData(_tableName, 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _procedure, ex.Message));
            }
        }

        public void Previous()
        {
            try {
                LoadData(_tableName, SelectResult.Page - 1, SelectResult.PageSize,
                SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _procedure, ex.Message));
            }
        }

        public void Next()
        {
            try
            {
                if (SelectResult.Page == SelectResult.TotalPages) return;
                LoadData(_tableName, SelectResult.Page + 1, SelectResult.PageSize,
                    SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _procedure, ex.Message));
            }
        }

        public void Last()
        {
            try {
                LoadData(_tableName, SelectResult.TotalPages, SelectResult.PageSize,
                SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _procedure, ex.Message));
            }
        }

        /// <summary>
        /// Reload and setFormatGridView
        /// </summary>
        public void ReLoad()
        {
            try
            {
                LoadData(_tableName, SelectResult.Page, SelectResult.PageSize,
                    SelectResult.SortField, SelectResult.IsSortOrderAscending);
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
            if (V6Login.UserRight.AllowPrint("", _tableName.ToString().ToUpper() + "6"))
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


        private IDictionary<string, object> _data = new SortedDictionary<string, object>();
        private void btnSua_Click(object sender, EventArgs e)
        {
            DoEdit();
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
            V6TableStruct structTable = V6BusinessHelper.GetTableStruct(_tableName);
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
            _search = query;
            LoadAtPage(1);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            _search = "";
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


        private void toChucTreeListView1_Click(object sender, EventArgs e)
        {
            //DoTreeClick();
        }

    }
}
