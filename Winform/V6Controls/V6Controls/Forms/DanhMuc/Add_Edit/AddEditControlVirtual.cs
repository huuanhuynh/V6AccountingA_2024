using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AddEditControlVirtual : V6FormControl, IV6AddEditInterface
    {
        protected V6Categories Categories;
        public string _MA_DM { get; set; }
        /// <summary>
        /// Tên bảng lấy dữ liệu.
        /// </summary>
        public string CONFIG_TABLE_NAME
        {
            get
            {
                string table = _MA_DM;
                // Tuanmh 01/07/2019 set TABLE_VIEW
                //if (CurrentTable == V6TableName.None && _aldmConfig != null)
                if (_aldmConfig != null && _aldmConfig.IS_ALDM)
                {
                    if (!string.IsNullOrEmpty(_aldmConfig.TABLE_NAME)
                        && V6BusinessHelper.IsExistDatabaseTable(_aldmConfig.TABLE_NAME))
                    {
                        table = _aldmConfig.TABLE_NAME;
                    }
                }
                return table;
            }
        }
        public V6TableStruct _TableStruct;
        public V6Mode Mode = V6Mode.Add;
        public Control _grandFatherControl;
        public IDictionary<string, object> _parentData;
        public AldmConfig _aldmConfig;
        /// <summary>
        /// Cờ thể hiện người dùng bấm nút copy hay nút add.
        /// </summary>
        public bool IS_COPY;
        public string TitleLang
        {
            get
            {
                string title = CONFIG_TABLE_NAME;
                if (_aldmConfig.HaveInfo) title = V6Setting.IsVietnamese ? _aldmConfig.TITLE : _aldmConfig.TITLE2;
                return title;
            }
        }

        /// <summary>
        /// Bật tắt tính năng gọi hàm Reload sau khi insert hoặc update thành công.
        /// </summary>
        public bool ReloadFlag;
        /// <summary>
        /// Gán trường mã của Table trong AddEditControl.MyInit để chạy CopyData_Here2Data
        /// </summary>
        public string KeyField1 = "";
        public string KeyField2 = "";
        public string KeyField3 = "";
        /// <summary>
        /// Data đưa vào để edit. (data cũ, gán ban đầu lên form)
        /// </summary>
        public IDictionary<string, object> DataOld { get; set; }
        /// <summary>
        /// Dùng khi gọi form update, chứa giá trị cũ trước khi update.
        /// </summary>
        public IDictionary<string, object> _keys = new SortedDictionary<string, object>();

        public Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        /// <summary>
        /// Code động từ aldmConfig.
        /// </summary>
        protected Type Event_program;
        public Dictionary<string, object> All_Objects = new Dictionary<string, object>();

        /// <summary>
        /// Chứa data mới (trên form) dùng để insert hoặc edit.
        /// </summary>
        public SortedDictionary<string, object> DataDic { get; set; }

        public IDictionary<string, object> ParentData
        {
            get
            {
                return _parentData;
            }
            set
            {
                _parentData = value;
            }
        }

        public void SetGrandFather(Control grandFatherControl)
        {
            _grandFatherControl = grandFatherControl;
        }

        public void SetParentData()
        {
            try
            {
                if (Mode == V6Mode.Add && _parentData != null) SetParentData(_parentData);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + " SetParrentData " + ex.Message, Application.ProductName);
            }
        }

        public override void SetParentData(IDictionary<string, object> data)
        {
            V6ControlFormHelper.SetSomeDataDictionary(this, data);
        }

        /// <summary>
        /// Dùng tự do, gán các propertie, field xong sẽ gọi loadAll
        /// </summary>
        public AddEditControlVirtual()
        {
            InitializeComponent();
            Categories = new V6Categories();
        }

        private void AddEditControlVirtual_Load(object sender, EventArgs e)
        {
            if (V6Setting.NotLoggedIn) return;

            _ready0 = true;
            //load truoc lop ke thua
            if (Mode == V6Mode.Add)
            {
                GetNewID();
                DoBeforeAdd();
                if(DataOld != null) DoBeforeCopy();
            }
            else if (Mode == V6Mode.Edit)
            {
                DoBeforeEdit();
            }
            else if (Mode == V6Mode.View)
            {
                DoBeforeView();
            }

            EnableFormControls_Alctct(_MA_DM);
            CheckVvarTextBox();

            InvokeFormEvent(FormDynamicEvent.INIT2);
        }

        /// <summary>
        /// Chạy ExistRowInTable cho các V6VvarTextBox.
        /// </summary>
        private void CheckVvarTextBox()
        {
            try
            {
                if (this is DynamicAddEditForm) return;
                foreach (Control control0 in this.Controls)
                {
                    if (control0 is V6TabControl)
                    {
                        V6TabControl v6TabControl1 = control0 as V6TabControl;
                        foreach (TabPage tabPage in v6TabControl1.TabPages)
                        {
                            if (tabPage.Text == "Advance")
                            {
                                Panel panel1 = tabPage.Controls[0] as Panel;
                                if (panel1 == null) return;
                                foreach (Control control in panel1.Controls)
                                {
                                    var vT = control as V6VvarTextBox;
                                    if (vT != null && !string.IsNullOrEmpty(vT.VVar))
                                    {
                                        vT.ExistRowInTable();
                                    }
                                }
                            }
                        }

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CheckVvarTextBox", ex);
            }
        }

        /// <summary>
        /// Khởi tạo giá trị cho control với các tham số
        /// </summary>
        /// <param name="ma_dm">Mã danh mục đang xử lý</param>
        /// <param name="mode">Add/Edit/View</param>
        /// <param name="keys">Nếu data null thì load bằng keys</param>
        /// <param name="data">Gán dữ liệu này lên form</param>
        public virtual void InitValues(string ma_dm, V6Mode mode,
            IDictionary<string, object> keys, IDictionary<string, object> data)
        {
            _MA_DM = ma_dm.ToUpper();
            //_aldmConfig = ConfigManager.GetAldmConfig(TableName.ToString());
            Mode = mode;
            _keys = keys;
            DataOld = data;
            LoadAdvanceControls();
            if (Mode == V6Mode.View) V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            
            All_Objects["thisForm"] = this;
            CreateFormProgram();
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, ma_dm, Event_program, All_Objects);
            InvokeFormEvent(FormDynamicEvent.INIT);
            
            LoadAll();
            //virtual
            LoadDetails();
            
            LoadTag(2, "", _MA_DM, ItemID);
        }

        private void LoadAdvanceControls()
        {
            try
            {
                //FormManagerHelper.CreateAdvanceFormControls(this, _MA_DM, All_Objects);
                // Replace by:
                //var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, null, someData);
                //f.AfterInitControl += f_AfterInitControl;
                //f.InitFormControl(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls", ex);
            }
        }

        protected virtual void LoadAll()
        {
            LoadStruct();//MaxLength...
            //EnableFormControls_Alctct();
            V6ControlFormHelper.LoadAndSetFormInfoDefine(_MA_DM, this, Parent);

            if (Mode==V6Mode.Edit)
            {
                if (DataOld == null) DataOld = LoadData();
                SetData(DataOld);
            }
            else if(Mode == V6Mode.Add)
            {
                if (DataOld == null && _keys != null)
                {
                    DataOld = LoadData();
                }

                var dataOld2 = new SortedDictionary<string, object>();
                if (DataOld == null) goto default0;

                if (!IS_COPY && _aldmConfig != null && _aldmConfig.HaveInfo && _aldmConfig.EXTRA_INFOR.ContainsKey("NEWMODE"))
                {
                    // NEWMODE EXTRAINFO
                    // mặc định NEWMODE:0;notfield1;notfield2... (lất tất loại trừ...)
                    // NEWMODE:1;field1;field2... (some field)
                    var NEWMODE = _aldmConfig.EXTRA_INFOR["NEWMODE"];
                    var fields = ObjectAndString.SplitString(NEWMODE);
                    if (fields.Length > 0)
                    {
                        string mode = fields[0];
                        if (mode == "0")
                        {
                            dataOld2.AddRange(DataOld);
                            for (int i = 1; i < fields.Length; i++)
                            {
                                string FIELD = fields[i].Trim().ToUpper();
                                if (DataOld.ContainsKey(FIELD)) dataOld2.Remove(FIELD);
                            }
                        }
                        else if (mode == "1")
                        {
                            for (int i = 1; i < fields.Length; i++)
                            {
                                string FIELD = fields[i].Trim().ToUpper();
                                if (DataOld.ContainsKey(FIELD)) dataOld2[FIELD] = DataOld[FIELD];
                            }
                        }
                        else // chưa định nghĩa, chạy như mặc định.
                        {
                            dataOld2.AddRange(DataOld);
                        }
                    }
                }
                else // mặc định copy all.
                {
                    dataOld2.AddRange(DataOld);
                }

                default0:
                dataOld2["STATUS"] = "1"; // khi tạo mới luôn đặt STATUS = 1
                SetSomeData(dataOld2);
                LoadDefaultData(2, "", _MA_DM, m_itemId);
            }
            else if (Mode == V6Mode.View)
            {
                if (DataOld != null)
                {
                    SetData(DataOld);
                }
                else
                {
                    if (_keys != null) LoadData();
                }
            }

            LoadTag(2, "", _MA_DM, m_itemId);
        }

        /// <summary>
        /// Hàm tải dữ liệu chi tiết
        /// Khi cần dùng sẽ viết override, không cần gọi lại!
        /// </summary>
        public virtual void LoadDetails()
        {

        }

        protected virtual void LoadStruct()
        {
            try
            {   
                _TableStruct = V6BusinessHelper.GetTableStruct(CONFIG_TABLE_NAME);
                V6ControlFormHelper.SetFormStruct(this, _TableStruct);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Load struct eror!", ex);
            }

        }

        public virtual IDictionary<string, object> LoadData()
        {
            try
            {
                if (_keys != null && _keys.Count > 0)
                {
                    var selectResult = Categories.Select(CONFIG_TABLE_NAME, _keys);
                    if (selectResult.Data.Rows.Count == 1)
                    {
                        var data = selectResult.Data.Rows[0].ToDataDictionary();
                        return data;
                    }
                    else if (selectResult.Data.Rows.Count > 1)
                    {
                        throw new Exception("Lấy dữ liệu sai >1");
                    }
                    else
                    {
                        throw new Exception("Không lấy được dữ liệu!");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadData", ex);
            }

            return null;
        }
        
        private void UpdateBackTk()
        {
            try
            {
                var tk = DataDic["TK_ME"].ToString().Trim();

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UpdateBacTk",
                    new SqlParameter("@tk", tk));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateBackTk", ex);
            }
        }
        private void UpdateLoaiTk(string action )
        {
            try
            {
                var tk = DataDic["TK"].ToString().Trim();
                var tk_me_new = DataDic["TK_ME"].ToString().Trim();
                var tk_me_old = Mode == V6Mode.Edit ? DataOld["TK_ME"].ToString().Trim() : tk_me_new;
               


            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UpdateLoaiTk",
                    new SqlParameter("@tk", tk),
                    new SqlParameter("@tk_me_old", tk_me_old),
                    new SqlParameter("@tk_me_new", tk_me_new),
                    new SqlParameter("@action", action.Trim())
                    );
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateLoaiTk", ex);
            }
        }
        private void UpdateBackVv()
        {
            try
            {
                var ma_vv = DataDic["MA_VV_ME"].ToString().Trim();

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UpdateBacVv",
                    new SqlParameter("@ma_vv", ma_vv));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateBackVv", ex);
            }
        }
        private void UpdateLoaiVv(string action)
        {
            try
            {
                var ma_vv = DataDic["MA_VV"].ToString().Trim();
                var ma_vv_me_new = DataDic["MA_VV_ME"].ToString().Trim();
                var ma_vv_me_old = Mode == V6Mode.Edit ? DataOld["MA_VV_ME"].ToString().Trim() : ma_vv_me_new;



                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UpdateLoaiVv",
                        new SqlParameter("@ma_vv", ma_vv),
                        new SqlParameter("@ma_vv_me_old", ma_vv_me_old),
                        new SqlParameter("@ma_vv_me_new", ma_vv_me_new),
                        new SqlParameter("@action", action.Trim())
                        );
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateLoaiVv", ex);
            }
        }
        private void UpdateInheritUser()
        {
            try
            {
                // Update user inherit 19/01/2015
                //[VPA_UPDATE_inherit_USER] @pinherit_user NUMERIC(3,0)

                var inherit_ch = DataDic["INHERIT_CH"].ToString().Trim();
                if (inherit_ch == "1")
                {
                    var inher_user = Mode == V6Mode.Edit
                        ? Convert.ToInt16(DataOld["USER_ID"])
                        : Convert.ToInt16(DataDic["USER_ID"]);

                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_inherit_USER",
                        new SqlParameter("@pinherit_user", inher_user),
                        new SqlParameter("@type", DataDic["INHERIT_TYPE"])
                        );
                    ReloadFlag = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateInheritUser", ex);
            }
        }
        private void UpdateAdvanceInforUser()
        {
            try
            {
                // Tuanmh Update user UpdateAdvanceInforUser 27/08/2018
                var mode = Mode == V6Mode.Edit ? "S" :"M";
                var user = Mode == V6Mode.Edit
                        ? Convert.ToInt16(DataOld["USER_ID"])
                        : Convert.ToInt16(DataDic["USER_ID"]);

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_AdvanceInfor_USER",
                    new SqlParameter("@user_id", user),
                    new SqlParameter("@user_id_login", V6Login.UserId),
                    new SqlParameter("@mode", mode));
                        
                    ReloadFlag = true;
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateAdvanceInforUser", ex);
            }
        }
        private void Update_Auto_From_Personal()
        {
            try
            {
                var stt_rec = DataDic["STT_REC"].ToString().Trim();
                var mode = Mode == V6Mode.Edit ? "S" :"M";

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPH_ADD_AUTO_FROM_PERSONAL",
                    new SqlParameter("@stt_rec", stt_rec),
                    new SqlParameter("@mode", mode));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".VPH_ADD_AUTO_FROM_PERSONAL", ex);
            }
        }

        private void UpdateV6Option()
        {
            try
            {
                var KEY = DataDic["NAME"].ToString().Trim().ToUpper();
                var VALUE = DataDic["VAL"].ToString().Trim();
                V6Options.SetValue(KEY, VALUE);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".UpdateV6Option", ex);
            }
        }

        /// <summary>
        /// Thực hiện thêm mới hoặc sửa khi bấm nút nhận.
        /// </summary>
        /// <param name="showMessage">Bật/tắt thông báo lỗi hoặc sai sót.</param>
        /// <returns></returns>
        public virtual bool DoInsertOrUpdate(bool showMessage = true)
        {
            //ReloadFlag = false;
            try
            {
                FixFormData();
                DataDic = GetData();
                All_Objects["data"] = DataDic;
                All_Objects["dataDic"] = DataDic;
                All_Objects["dataOld"] = DataOld;
                if (_aldmConfig.IS_ALDM)
                {
                    ValidateData_IsAldm();
                }
                else
                {
                    ValidateData();
                }
                
                InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
                InvokeFormEvent("BEFOREINSERTORUPDATE");
                string checkV6Valid = CheckV6Valid(DataDic, CONFIG_TABLE_NAME);
                if (!string.IsNullOrEmpty(checkV6Valid))
                {
                    this.ShowInfoMessage(checkV6Valid, 500);
                    return false;
                }

                string checkChar = CheckChar(DataDic);
                if (!string.IsNullOrEmpty(checkChar))
                {
                    this.ShowWarningMessage(checkChar);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message, 500);
                this.WriteExLog(GetType() + ".DoInsertOrUpdate ValidateData", ex);
                return false;
            }

            if (Mode==V6Mode.Edit)
            {
                try
                {

                    int b = UpdateData();
                    if (b > 0)
                    {
                        SaveEditHistory(DataOld, DataDic);
                        AfterSaveBase();
                        AfterSave();
                        AfterUpdate();
                        InvokeFormEvent(FormDynamicEvent.AFTERUPDATE);

                        if (CONFIG_TABLE_NAME == "V6USER")
                        {
                            UpdateInheritUser();
                            UpdateAdvanceInforUser();
                        }

                        if (CONFIG_TABLE_NAME == "V6OPTION")
                        {
                            UpdateV6Option();
                        }

                        if (CONFIG_TABLE_NAME == "ALTK0")
                        {
                            UpdateBackTk();
                            UpdateLoaiTk("E");
                        }
                        if (CONFIG_TABLE_NAME == "ALVV")
                        {
                            UpdateBackVv();
                            UpdateLoaiVv("E");
                        }
                        if (CONFIG_TABLE_NAME == "HRPERSONAL")
                        {
                            Update_Auto_From_Personal();
                        }

                        if (!string.IsNullOrEmpty(KeyField1))
                        {
                            var newKey1 = DataDic[KeyField1].ToString().Trim();
                            var newKey2 = "";
                            if (!string.IsNullOrEmpty(KeyField2) && DataDic.ContainsKey(KeyField2))
                                newKey2 = DataDic[KeyField2].ToString().Trim();
                            var newKey3 = "";
                            if (!string.IsNullOrEmpty(KeyField3) && DataDic.ContainsKey(KeyField3))
                                newKey3 = DataDic[KeyField3].ToString().Trim();
                            var oldKey1 = newKey1;
                            var oldKey2 = newKey2;
                            var oldKey3 = newKey3;
                            V6ControlFormHelper.Copy_Here2Data(CONFIG_TABLE_NAME, Mode,
                                KeyField1, KeyField2, KeyField3,
                                newKey1, newKey2, newKey3,
                                oldKey1, oldKey2, oldKey3,
                                "");
                        }
                        return true;
                    }
                    else
                    {
                        if (showMessage) ShowMainMessage(V6Text.UpdateFail);
                    }
                    
                }
                catch (Exception e1)
                {
                    if (showMessage) this.ShowErrorException(GetType() + ".DoUpdate Exception: ", e1);
                }
            }
            else if(Mode == V6Mode.Add)
            {
                try
                {
                    bool b = InsertNew();
                    if (b)
                    {
                        AfterSaveBase();
                        AfterSave();
                        AfterInsert();
                        InvokeFormEvent(FormDynamicEvent.AFTERINSERT);

                        if (CONFIG_TABLE_NAME == "V6USER")
                        {
                            UpdateInheritUser();
                            UpdateAdvanceInforUser();
                        }

                        if (CONFIG_TABLE_NAME == "ALTK0")
                        {
                            UpdateBackTk();
                            UpdateLoaiTk("A");
                        }
                        if (CONFIG_TABLE_NAME == "ALVV")
                        {
                            UpdateBackVv();
                            UpdateLoaiVv("A");
                        }
                        if (CONFIG_TABLE_NAME == "HRPERSONAL")
                        {
                            Update_Auto_From_Personal();
                        }
                        if (!string.IsNullOrEmpty(KeyField1))
                        {
                            var newKey1 = DataDic[KeyField1].ToString().Trim();
                            var newKey2 = "";
                            if (!string.IsNullOrEmpty(KeyField2) && DataDic.ContainsKey(KeyField2))
                                newKey2 = DataDic[KeyField2].ToString().Trim();
                            var newKey3 = "";
                            if (!string.IsNullOrEmpty(KeyField3) && DataDic.ContainsKey(KeyField3))
                                newKey3 = DataDic[KeyField3].ToString().Trim();

                            string oldKey1 = "", oldKey2 = "", oldKey3 = "", UID = "";
                            if (DataOld != null)
                            {
                                oldKey1 = DataOld[KeyField1].ToString().Trim();
                                if (!string.IsNullOrEmpty(KeyField2) && DataOld.ContainsKey(KeyField2))
                                    oldKey2 = DataOld[KeyField2].ToString().Trim();
                                if (!string.IsNullOrEmpty(KeyField3) && DataOld.ContainsKey(KeyField3))
                                    oldKey3 = DataOld[KeyField3].ToString().Trim();
                                UID = DataOld.ContainsKey("UID") ? DataOld["UID"].ToString() : "";
                            }

                            V6ControlFormHelper.Copy_Here2Data(CONFIG_TABLE_NAME, Mode,
                                KeyField1, KeyField2, KeyField3,
                                newKey1, newKey2, newKey3,
                                oldKey1, oldKey2, oldKey3,
                                UID);
                        }
                        return true;
                    }
                    else
                    {
                        if (showMessage) ShowMainMessage(V6Text.AddFail);
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (showMessage) this.ShowErrorException(GetType() + "DoInsert Exception: ", ex);
                }
            }
            return false;
        }

        public virtual bool InsertNew()
        {
            try
            {
                var result = Categories.Insert(CONFIG_TABLE_NAME, DataDic);
                if (result && update_stt13)
                {
                    AddStt13();
                }
                return result;
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message, 500);
                this.WriteExLog(GetType() + ".InsertNew", ex);
                return false;
            }
        }

        public void AfterSaveBase()
        {
            try
            {
                if (_aldmConfig == null || _aldmConfig.NoInfo) return;

                var KEYS = ObjectAndString.SplitString(_aldmConfig.KEY.ToUpper());
                var data_new = "";
                var data_old = "";
                var key_old = new Dictionary<string, object>();
                var key_new = new Dictionary<string, object>();
                foreach (string KEY in KEYS)
                {
                    if (!_TableStruct.ContainsKey(KEY)) continue;
                    var sct = _TableStruct[KEY];
                    if (!DataDic.ContainsKey(KEY)) return;
                    if (DataOld != null && DataOld.ContainsKey(KEY)) key_old[KEY] = DataOld[KEY];
                    key_new[KEY] = DataDic[KEY];
                    var s_new = SqlGenerator.GenSqlStringValue(DataDic[KEY], sct.sql_data_type_string, sct.ColumnDefault, false, sct.MaxLength);
                    if (s_new.ToUpper().StartsWith("N'")) s_new = s_new.Substring(1);
                    data_new += "|" + s_new;
                    var s_old = Mode == V6Mode.Edit ? SqlGenerator.GenSqlStringValue(DataOld[KEY], sct.sql_data_type_string, sct.ColumnDefault, false, sct.MaxLength) : s_new;
                    if (s_old.ToUpper().StartsWith("N'")) s_old = s_old.Substring(1);
                    data_old += "|" + s_old;
                }
                
                if (data_new.Length > 1) data_new = data_new.Substring(1);
                if (data_old.Length > 1) data_old = data_old.Substring(1);
                
                SqlParameter[] plist =
                {
                    new SqlParameter("@TableName", _aldmConfig.TABLE_NAME),
                    new SqlParameter("@Fields", _aldmConfig.KEY),
                    new SqlParameter("@datas_old", data_old),
                    new SqlParameter("@datas_new", data_new),
                    new SqlParameter("@uid", Mode == V6Mode.Edit ? DataOld["UID"].ToString() : ""),
                    new SqlParameter("@mode", Mode == V6Mode.Add ? "M" : "S"),
                    new SqlParameter("@User_id", V6Login.UserId),
                };
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_AL_ALL", plist);

                // Database2
                var EXTRA_INFOR = _aldmConfig.EXTRA_INFOR;
                if (EXTRA_INFOR.ContainsKey("SERVER") && EXTRA_INFOR.ContainsKey("DATABASE") && EXTRA_INFOR.ContainsKey("USERID") && EXTRA_INFOR.ContainsKey("PASSWORD"))
                {
                    if (UtilityHelper.DeCrypt(EXTRA_INFOR["SERVER"]).ToUpper() != DatabaseConfig.Server.ToUpper()) goto EndIf;
                        
                    string conString2 = string.Format(@"Server={0};Database={1};User Id={2};Password={3};",
                        UtilityHelper.DeCrypt(EXTRA_INFOR["SERVER"]), UtilityHelper.DeCrypt(EXTRA_INFOR["DATABASE"]),
                        UtilityHelper.DeCrypt(EXTRA_INFOR["USERID"]), UtilityHelper.DeCrypt(EXTRA_INFOR["PASSWORD"]));
                    // Delete old, Delete new, Insert new;
                    if (Mode == V6Mode.Edit)
                    {
                        string delete_old_sql = SqlGenerator.GenDeleteSql(_TableStruct, key_old);
                        SqlHelper.ExecuteNonQuery(conString2, CommandType.Text,delete_old_sql, DatabaseConfig.TimeOut);
                    }
                    string delete_new_sql = SqlGenerator.GenDeleteSql(_TableStruct, key_new);
                    SqlHelper.ExecuteNonQuery(conString2, CommandType.Text, delete_new_sql, DatabaseConfig.TimeOut);

                    string insert_new_sql = SqlGenerator.GenInsertSql(V6Login.UserId, _aldmConfig.TABLE_NAME, _TableStruct, DataDic);
                    SqlHelper.ExecuteNonQuery(conString2, CommandType.Text, insert_new_sql, DatabaseConfig.TimeOut);

                    SqlParameter[] plist2 =
                    {
                        new SqlParameter("@TableName", _aldmConfig.TABLE_NAME),
                        new SqlParameter("@Fields", _aldmConfig.KEY),
                        new SqlParameter("@datas_old", data_old),
                        new SqlParameter("@datas_new", data_new),
                        new SqlParameter("@uid", Mode == V6Mode.Edit ? DataOld["UID"].ToString() : ""),
                        new SqlParameter("@mode", Mode == V6Mode.Add ? "M" : "S"),
                        new SqlParameter("@User_id", V6Login.UserId),
                    };
                    SqlHelper.ExecuteNonQuery(conString2, CommandType.StoredProcedure, "VPA_UPDATE_AL_ALL", DatabaseConfig.TimeOut, plist2);
                EndIf: ;
                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".AfterSaveBase", ex);
            }
        }

        /// <summary>
        /// Được gọi sau khi thêm hoặc sửa thành công.
        /// </summary>
        public virtual void AfterSave() 
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.AFTERSAVE))
                {
                    var method_name = Event_Methods[FormDynamicEvent.AFTERSAVE];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke AFTERSAVE", ex1);
            }
        }

        /// <summary>
        /// Được gọi sau khi thêm thành công. (hàm ảo).
        /// </summary>
        public virtual void AfterInsert()
        {
            
        }

        public virtual int UpdateData()
        {
            try
            {
                //Lấy thêm UID từ DataEditNếu có.
                if (DataOld.ContainsKey("UID"))
                {
                    _keys["UID"] = DataOld["UID"];
                }
                if (_TableStruct.TableName.ToUpper() == "CORPLAN")
                {
                    _keys["ID"] = DataOld["ID"];
                }

                int count = V6BusinessHelper.SelectCount(CONFIG_TABLE_NAME, _keys, "UID");
                if (count != 1)
                {
                    if (_aldmConfig.HaveInfo)
                    {
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@TableName", _aldmConfig.TABLE_NAME),
                            new SqlParameter("@Fields", _aldmConfig.KEY),
                            new SqlParameter("@uid", Mode == V6Mode.Edit ? DataOld["UID"].ToString() : ""),
                            new SqlParameter("@mode", Mode == V6Mode.Add ? "M" : "S"),
                            new SqlParameter("@User_id", V6Login.UserId),
                        };
                        V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_FIX_CONFLICT_AL_ALL", plist);
                    }
                    // TỰ ĐÓNG FORM VÀ RELOAD Ở DANHMUCVIEW.
                    throw new Exception("Trùng khóa! Đã tự động sửa lỗi.\n Vui lòng thực hiện lại!\nDATA_COUNT = " + count);
                }
                Dictionary<string, object> updateData = new Dictionary<string, object>(DataDic);
                if (_aldmConfig != null && _aldmConfig.HaveInfo)
                {
                    if (_aldmConfig.EXTRA_INFOR.ContainsKey("NOUPDATE"))
                    {
                        foreach (string FIELD in ObjectAndString.SplitString(_aldmConfig.EXTRA_INFOR["NOUPDATE"].ToUpper()))
                        {
                            if (updateData.ContainsKey(FIELD)) updateData.Remove(FIELD);
                        }
                    }
                }
                var result = Categories.Update(CONFIG_TABLE_NAME, updateData, _keys);
                return result;
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message, 500);
                this.WriteExLog(GetType() + ".UpdateData", ex);
                return 0;
            }
        }

        /// <summary>
        /// Được gọi sau khi sửa thành công.
        /// </summary>
        public virtual void AfterUpdate()
        {
            
        }

        /// <summary>
        /// Save Edit history.
        /// </summary>
        /// <param name="data_old">Dữ liệu trước đó.</param>
        /// <param name="data_new">Dữ liệu mới</param>
        protected void SaveEditHistory(IDictionary<string, object> data_old, IDictionary<string, object> data_new)
        {
            try
            {
                if (V6Options.SaveEditLogList && _aldmConfig != null && _aldmConfig.HaveInfo && ObjectAndString.ObjectToBool(_aldmConfig.DMFIX))
                {
                    string info = V6ControlFormHelper.CompareDifferentData(data_old, data_new);
                    V6BusinessHelper.WriteV6ListHistory(ItemID, MethodBase.GetCurrentMethod().Name,
                        string.IsNullOrEmpty(CodeForm) ? "N" : CodeForm[0].ToString(),
                        _aldmConfig.MA_DM,  ObjectAndString.ObjectToString(data_new[_aldmConfig.VALUE]), info, ObjectAndString.ObjectToString(data_old["UID"]));
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SaveEditHistory", ex);
            }
        }

        /// <summary>
        /// Chuẩn hóa lại dữ liệu trước khi xử lý.
        /// </summary>
        public virtual void FixFormData()
        {

        }

        /// <summary>
        /// Các form kế thừa cần override hàm này.
        /// Throw new Exception nếu có dữ liệu sai.
        /// Hàm được chạy trước khi Insert hoặc Update.
        /// </summary>
        /// <returns></returns>
        public virtual void ValidateData()
        {
            
        }

        public void ValidateData_IsAldm()
        {
            var errors = "";

            // check notempty
            //foreach (KeyValuePair<string, DefineInfo> item in DefineInfo_Data)
            //{
            //    if (item.Value.NotEmpty)
            //    {
            //        if (DataDic.ContainsKey(item.Key))
            //        {
            //            if ((DataDic[item.Key] ?? "").ToString().Trim() == "")
            //            {
            //                errors += string.Format(V6Text.CheckInfor + "{0}: {1}\r\n", item.Key, item.Value.TextLang(V6Setting.IsVietnamese));
            //            }
            //        }
            //        else
            //        {
            //            errors += string.Format(V6Text.CheckDeclare + "{0}: {1}\r\n", item.Key, item.Value.TextLang(V6Setting.IsVietnamese));
            //        }
            //    }
            //}

            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.VALIDATEDATA))
                {
                    var method_name = Event_Methods[FormDynamicEvent.VALIDATEDATA];
                    All_Objects["ParentData"] = ParentData;
                    All_Objects["DataOld"] = DataOld;
                    errors += V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke VALIDATEDATA", ex1);
            }

            // check code
            //_dataRow;// aldm
            var GRD_COL = _aldmConfig.GRD_COL.ToUpper();
            var KEY_LIST = ObjectAndString.SplitString(_aldmConfig.KEY.ToUpper());
            string KEY1 = "", KEY2 = "", KEY3 = "", KEY4 = "";

            if (GRD_COL == "AL" && KEY_LIST.Length > 0)
            {
                errors += CheckValid(_MA_DM, KEY_LIST);
            }
            else if (GRD_COL == "ONECODE" && KEY_LIST.Length > 0)
            {
                KEY1 = KEY_LIST[0].Trim().ToUpper();
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, KEY1,
                     DataDic[KEY1].ToString(), DataOld[KEY1].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied + " {0} = {1}", KEY1, DataDic[KEY1]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, KEY1,
                        DataDic[KEY1].ToString(), DataDic[KEY1].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.AddDenied + "{0} = {1}", KEY1, DataDic[KEY1]));
                }
            }
            else if (GRD_COL == "TWOCODE" && KEY_LIST.Length > 1)
            {
                KEY1 = KEY_LIST[0].Trim().ToUpper();
                KEY2 = KEY_LIST[1].Trim().ToUpper();
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_Full(_MA_DM, 0,
                        KEY1, DataDic[KEY1].ToString(), DataOld[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataOld[KEY2].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied + " {0},{1} = {2},{3}",
                            KEY1, KEY2, DataDic[KEY1], DataDic[KEY2]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_Full(_MA_DM, 1,
                        KEY1, DataDic[KEY1].ToString(), DataDic[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataDic[KEY2].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.AddDenied + " {0},{1} = {2},{3}",
                            KEY1, KEY2, DataDic[KEY1], DataDic[KEY2]));
                }
            }
            else if (GRD_COL == "THREECODE" && KEY_LIST.Length > 2)
            {
                KEY1 = KEY_LIST[0].Trim().ToUpper();
                KEY2 = KEY_LIST[1].Trim().ToUpper();
                KEY3 = KEY_LIST[2].Trim().ToUpper();
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode(_MA_DM, 0,
                        KEY1, DataDic[KEY1].ToString(), DataOld[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataOld[KEY2].ToString(),
                        KEY3, DataDic[KEY3].ToString(), DataOld[KEY3].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied + " {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode(_MA_DM, 1,
                        KEY1, DataDic[KEY1].ToString(), DataDic[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataDic[KEY2].ToString(),
                        KEY3, DataDic[KEY3].ToString(), DataDic[KEY3].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.AddDenied + ": {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
            }
            else if (GRD_COL == "TWOCODEONEDAY" && KEY_LIST.Length > 2)
            {
                KEY1 = KEY_LIST[0].Trim().ToUpper();
                KEY2 = KEY_LIST[1].Trim().ToUpper();
                KEY3 = KEY_LIST[2].Trim().ToUpper();
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_OneDate(_MA_DM, 0,
                        KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataOld[KEY1]),
                        KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataOld[KEY2]),
                        KEY3, ObjectAndString.ObjectToString(DataDic[KEY3], "yyyyMMdd"), ObjectAndString.ObjectToString(DataOld[KEY3], "yyyyMMdd"));
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied + " {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_OneDate(_MA_DM, 1,
                        KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataDic[KEY1]),
                        KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataDic[KEY2]),
                        KEY3, ObjectAndString.ObjectToString(DataDic[KEY3], "yyyyMMdd"), ObjectAndString.ObjectToString(DataDic[KEY3], "yyyyMMdd"));
                    if (!b)
                        throw new Exception(string.Format(V6Text.AddDenied + " {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
            }
            else if (GRD_COL == "THREECODEONEDAY" && KEY_LIST.Length > 3)
            {
                KEY1 = KEY_LIST[0].Trim().ToUpper();
                KEY2 = KEY_LIST[1].Trim().ToUpper();
                KEY3 = KEY_LIST[2].Trim().ToUpper();
                KEY4 = KEY_LIST[3].Trim().ToUpper();

                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode_OneDate(_MA_DM, 0,
                        KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataOld[KEY1]),
                        KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataOld[KEY2]),
                        KEY3, DataDic[KEY3].ToString(), ObjectAndString.ObjectToString(DataOld[KEY3]),
                        KEY4, ObjectAndString.ObjectToString(DataDic[KEY4], "yyyyMMdd"), ObjectAndString.ObjectToString(DataOld[KEY4], "yyyyMMdd"));
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied + " {0},{1},{2},{3} = {4},{5},{6},{7}",
                            KEY1, KEY2, KEY3, KEY4, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3], DataDic[KEY4]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode_OneDate(_MA_DM, 1,
                        KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataDic[KEY1]),
                        KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataDic[KEY2]),
                        KEY3, DataDic[KEY3].ToString(), ObjectAndString.ObjectToString(DataDic[KEY3]),
                        KEY4, ObjectAndString.ObjectToString(DataDic[KEY4], "yyyyMMdd"), ObjectAndString.ObjectToString(DataDic[KEY4], "yyyyMMdd"));
                    if (!b)
                        throw new Exception(string.Format(V6Text.AddDenied + " {0},{1},{2},{3} = {4},{5},{6},{7}",
                            KEY1, KEY2, KEY3, KEY4, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3], DataDic[KEY4]));
                }
            }
            else
            {
                DoNothing();
            }

        end:
            if (errors.Length > 0) throw new Exception(errors);
        }

        protected virtual void GetNewID()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig(_MA_DM);
                // Get new id proc 
                if (_aldmConfig.HaveInfo)
                {
                    string ID_FIELD = _aldmConfig.VALUE.ToUpper();
                    // Trường hợp mã có phân nhóm.
                    // DataOld cần thêm dữ liệu AUTOID_LOAINH AUTOID_NHVALUE
                    if (DataOld != null && DataOld.ContainsKey("AUTOID_LOAINH") && DataOld.ContainsKey("AUTOID_NHVALUE"))
                    {

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@MA_DM", _MA_DM),
                            new SqlParameter("@Vvalue", DataOld.ContainsKey(ID_FIELD) ? DataOld[ID_FIELD].ToString().Trim() : ""),
                            new SqlParameter("@Loai_nh", DataOld["AUTOID_LOAINH"]),
                            new SqlParameter("@NhValue", DataOld["AUTOID_NHVALUE"]),
                            new SqlParameter("@User_id", V6Login.UserId),
                            
                        };
                        var data = V6BusinessHelper.ExecuteProcedure("VPA_GET_AUTOID_AL_ALL", plist).Tables[0];
                        if (data.Rows.Count > 0)
                        {
                            string value = data.Rows[0]["Vvalue"].ToString().Trim();
                            if (value != "")
                            {
                                IDictionary<string, object> value_dic = new SortedDictionary<string, object>();
                                value_dic.Add(ID_FIELD, value);
                                V6ControlFormHelper.SetSomeDataDictionary(this, value_dic);
                                return;
                            }
                        }
                    }


                    if (_aldmConfig.INCREASE_YN)
                    {
                        // Kiểm tra mã tự tăng có tồn tại trong dữ liệu chưa và tăng tiếp (trong 100 số)
                        update_stt13 = true;
                        var stt13 = ObjectAndString.ObjectToInt(_aldmConfig.STT13);
                        var transform = _aldmConfig.TRANSFORM;
                        var value0 = string.Format(transform, stt13);
                        var value100 = string.Format(transform, stt13 + 100);
                        
                        var check_data = V6BusinessHelper.Select(CONFIG_TABLE_NAME, "*",
                            string.Format("{0} BETWEEN '{1}' AND '{2}'", ID_FIELD, value0, value100), "", ID_FIELD).Data;

                        int increase = 1;
                        var value = string.Format(transform, stt13 + increase);
                        if (check_data != null && check_data.Rows.Count > 0)
                        {
                            var check_view = new DataView(check_data);
                            check_view.RowFilter = string.Format("{0}='{1}'", ID_FIELD, value);
                            while (check_view.Count > 0) // increase <= check_data.Rows.Count)
                            {
                                increase++;
                                value = string.Format(transform, stt13 + increase);
                                check_view.RowFilter = string.Format("{0}='{1}'", ID_FIELD, value);
                            }
                        }

                        IDictionary<string, object> value_dic = new SortedDictionary<string, object>();
                        value_dic.Add(ID_FIELD, value);
                        V6ControlFormHelper.SetSomeDataDictionary(this, value_dic);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetNewID", ex);
            }
        }

        protected bool update_stt13;
        
        protected virtual void AddStt13()
        {
            try
            {
                var sql = "Update Aldm set Stt13=Stt13+1 where ma_dm=@ma_dm";
                SqlParameter[] plist = new []{new SqlParameter("@ma_dm", _MA_DM)};
                V6BusinessHelper.ExecuteSqlNoneQuery(sql, plist);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddStt13", ex);
            }
        }

        /// <summary>
        /// Làm gì đó khi khởi động form trường hợp thêm. Chạy trước hàm load của lớp kế thừa.
        /// </summary>
        public virtual void DoBeforeAdd()
        {

        }
        /// <summary>
        /// Chạy sau DoBeforeAdd. Chạy trước hàm load của lớp kế thừa.
        /// </summary>
        public virtual void DoBeforeCopy()
        {

        }
        /// <summary>
        /// Dùng hàm check IsExistOneCode proc. Chạy trước hàm load của lớp kế thừa.
        /// </summary>
        public virtual void DoBeforeEdit()
        {
            
        }
        /// <summary>
        /// Dùng hàm check IsExistOneCode proc. Chạy trước hàm load của lớp kế thừa.
        /// </summary>
        public virtual void DoBeforeView()
        {
            
        }


        protected void CreateFormProgram()
        {
            try
            {
                //DMETHOD
                if (_aldmConfig.NoInfo || string.IsNullOrEmpty(_aldmConfig.DMETHOD))
                {
                    //this.ShowWarningMessage("No column name [DMETHOD]");
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

        /// <summary>
        /// Bật tắt các control theo thông tin ghi chú trong Alctct
        /// </summary>
        protected void EnableFormControls_Alctct(string tableName)
        {
            try
            {
                var alctct = V6BusinessHelper.GetAlctCt_TableName(tableName);
                var alctct_GRD_HIDE = new string[] { };
                var alctct_GRD_READONLY = new string[] { };
                if (!V6Login.IsAdmin)
                {
                    if (alctct != null && alctct.Rows.Count > 0)
                    {
                        var GRD_HIDE = alctct.Rows[0]["GRD_HIDE"].ToString().ToUpper();
                        var GRD_READONLY = alctct.Rows[0]["GRD_READONLY"].ToString().ToUpper();
                        alctct_GRD_HIDE = ObjectAndString.SplitString(GRD_HIDE);
                        alctct_GRD_READONLY = ObjectAndString.SplitString(GRD_READONLY);
                    }
                }
                foreach (string field_info in alctct_GRD_HIDE)
                {
                    var sss = field_info.Split(':');
                    string field = sss[0];
                    string format = sss.Length > 1 ? sss[1] : null;

                    Control c = V6ControlFormHelper.GetControlByAccessibleName(this, field);
                    if (c != null)
                    {
                        c.InvisibleTag();
                    }
                    else
                    {
                        c = GetControlByName(field);
                        if (c != null) c.InvisibleTag();
                    }
                }

                foreach (string field_info in alctct_GRD_READONLY)
                {
                    var sss = field_info.Split(':');
                    string field = sss[0];
                    string format = sss.Length > 1 ? sss[1] : null;

                    Control c = V6ControlFormHelper.GetControlByAccessibleName(this, field);
                    if(c is TextBox) ((TextBox)c).ReadOnlyTag();
                    if (c is ComboBox) ((ComboBox)c).DisableTag();
                    if (c is RadioButton) ((RadioButton)c).DisableTag();
                    if (c is DateTimePicker) ((DateTimePicker)c).DisableTag();

                    if (c == null)
                    {
                        c = GetControlByName(field);
                        if (c != null) c.DisableTag();
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "EnableFormControls_Alctct", ex);
            }
        }

        private string CheckChar(IDictionary<string, object> data)
        {
            string error = "";
            if (_aldmConfig != null && _aldmConfig.HaveInfo && !string.IsNullOrEmpty(_aldmConfig.VALID_CHARS))
            {
                var check_items = ObjectAndString.StringToStringDictionary(_aldmConfig.VALID_CHARS);
                foreach (KeyValuePair<string, string> item in check_items)
                {
                    string FIELD = item.Key.Trim().ToUpper();
                    if (!_TableStruct.ContainsKey(FIELD)) continue;
                    if (!data.ContainsKey(FIELD)) continue;
                    if (!(data[FIELD] is string)) continue;
                    if (ObjectAndString.IsNumberType(_TableStruct[FIELD].DataType)) continue;
                    if (ObjectAndString.IsDateTimeType(_TableStruct[FIELD].DataType)) continue;

                    string value = data[FIELD].ToString();
                    string error1 = "";
                    foreach (char c in value)
                    {
                        if (_aldmConfig.VALID_CHARS.IndexOf(c) < 0)
                        {
                            error1 += " [" + c + "]";
                        }
                    }

                    if (error1.Length > 0)
                    {
                        if (error.Length > 0) error += "\n";
                        error += string.Format("[{0}] {1}:{2}", FIELD, V6Text.NotAllowChars, error1);
                    }
                }
            }
            return error;
        }

        /// <summary>
        /// Kiểm tra thông tin bắt buộc ghi chú trong V6Valid (Hệ thống/Quản lý hệ thống/Thông tin bắt buộc khi nhập liệu).
        /// </summary>
        /// <param name="data">Dữ liệu cần kiểm tra.</param>
        /// <param name="tableName">Bảng kiểm tra.</param>
        /// <returns></returns>
        protected string CheckV6Valid(IDictionary<string, object> data, string tableName)
        {
            string error = null;
            try
            {
                var config = ConfigManager.GetV6ValidConfigDanhMuc(tableName);

                if (config != null && config.HaveInfo)
                {
                    var a_fields = ObjectAndString.SplitString(config.A_field);
                    foreach (string field in a_fields)
                    {
                        string FIELD = field.Trim().ToUpper();

                        if (data.ContainsKey(FIELD) && (data[FIELD] == null || data[FIELD].ToString().Trim() == ""))
                        {
                            error += string.Format("{0} [{1}]\n", V6Text.NoInput, FIELD);
                        }
                    }
                }
                else
                {
                    //ShowMainMessage("No V6Valid info!");
                }
            }
            catch (Exception ex)
            {
                //error += ex.Message;//Lỗi chương trình không liên quan lỗi nhập liệu
                this.WriteExLog(GetType() + ".ValidateData_Detail", ex);
            }
            return error;
        }

        /// <summary>
        /// Kiểm tra dữ liệu để thêm hoặc sửa, Trả về chuỗi lỗi, nếu hợp lệ trả về null hoặc rỗng.
        /// </summary>
        protected string CheckValid(string tableName, IList<string> key_list)
        {
            var keys_new = new SortedDictionary<string, object>();
            foreach (string key in key_list)
            {
                string KEY = key.ToUpper();
                keys_new.Add(KEY, DataDic[KEY].ToString().Trim());
            }

            string where_new = SqlGenerator.GenWhere(V6BusinessHelper.GetTableStruct(tableName), keys_new);

            AldmConfig config = ConfigManager.GetAldmConfig(tableName);
            bool exist_new = V6BusinessHelper.CheckDataExistStruct(tableName, keys_new, config.CHECK_LONG);

            if (Mode == V6Mode.Edit)
            {
                SortedDictionary<string, object> keys_old = new SortedDictionary<string, object>();
                foreach (string key in key_list)
                {
                    string KEY = key.ToUpper();
                    keys_old.Add(KEY, DataOld[KEY].ToString().Trim());
                }
                string where_old = SqlGenerator.GenWhere(V6BusinessHelper.GetTableStruct(tableName), keys_old);
                //bool exist_old = V6BusinessHelper.CheckDataExistStruct(TableName, keys_old);

                if (where_new != where_old && exist_new)
                    return V6Text.Exist + " " + V6Text.EditDenied + " " + where_new;
            }
            else if (Mode == V6Mode.Add)
            {
                if (exist_new)
                    return V6Text.Exist + " " + V6Text.AddDenied + " " + where_new;
            }

            return "";
        }

        public override void ShowAlinitAddEdit(Control control)
        {
            V6Mode v6mode = V6Mode.Add;
            IDictionary<string, object> keys0 = new Dictionary<string, object>();
            IDictionary<string, object> keys = null;

            keys0["LOAI"] = 2;
            //keys0["MA_CT_ME"] = _invoice.Mact;
            keys0["MA_DM"] = _MA_DM;
            keys0["NHOM"] = "00";
            keys0["NAMETAG"] = control.Name.ToUpper();
            if (!string.IsNullOrEmpty(control.AccessibleName)) keys0["NAMEVAL"] = control.AccessibleName.ToUpper();
            // Lấy dữ liệu mặc định của Form parent.
            var defaultData = V6BusinessHelper.GetDefaultValueData(2, "", _MA_DM, ItemID, "");
            DataRow dataRow = null;
            foreach (DataRow row in defaultData.Rows)
            {
                if (row["NAMETAG"].ToString().Trim().ToUpper() == control.Name.ToUpper())
                {
                    dataRow = row;
                    break;
                }

                if (!string.IsNullOrEmpty(control.AccessibleName) && row["NAMEVAL"].ToString().Trim().ToUpper() == control.AccessibleName.ToUpper())
                {
                    dataRow = row;
                    break;
                }
            }

            if (dataRow != null) // nếu tồn tại dữ liệu.
            {
                v6mode = V6Mode.Edit;
                keys = new Dictionary<string, object>();
                keys["UID"] = dataRow["UID"];
            }
            else
            {
                v6mode = V6Mode.Add;
                keys0["KIEU"] = "0";
            }

            V6ControlFormHelper.CallShowAlinitAddEdit(v6mode, keys, keys0);
        }

    }
}
