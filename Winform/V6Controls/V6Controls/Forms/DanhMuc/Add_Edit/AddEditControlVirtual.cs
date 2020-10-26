using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
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

            InvokeFormEvent(FormDynamicEvent.INIT2);
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
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
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
                if(DataOld!=null) SetData(DataOld); else LoadData();
            }
            else if(Mode == V6Mode.Add)
            {
                var dataOld2 = new SortedDictionary<string, object>();
                if (DataOld != null) dataOld2.AddRange(DataOld);
                dataOld2["STATUS"] = "1";
                if (DataOld != null)
                {
                    SetSomeData(dataOld2);
                }
                else if (_keys != null)
                {
                    LoadData();
                }
                else
                {
                    LoadDefaultData(2, "", _MA_DM, m_itemId);
                    SetSomeData(dataOld2);
                }
                
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
                _TableStruct = V6BusinessHelper.GetTableStruct(_MA_DM);
                V6ControlFormHelper.SetFormStruct(this, _TableStruct);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Load struct eror!", ex);
            }

        }

        public virtual void LoadData()
        {
            try
            {
                if (_keys != null && _keys.Count > 0)
                {
                    var selectResult = Categories.Select(_MA_DM, _keys);
                    if (selectResult.Data.Rows.Count == 1)
                    {
                        DataOld = selectResult.Data.Rows[0].ToDataDictionary();
                        SetData(DataOld);
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
                ValidateData();
                InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
                InvokeFormEvent("BEFOREINSERTORUPDATE");
                string checkV6Valid = CheckV6Valid(DataDic, _MA_DM);
                if (!string.IsNullOrEmpty(checkV6Valid))
                {
                    this.ShowInfoMessage(checkV6Valid, 500);
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
                        
                        if (_MA_DM == "V6USER")
                        {
                            UpdateInheritUser();
                            UpdateAdvanceInforUser();
                        }

                        if (_MA_DM == "V6OPTION")
                        {
                            UpdateV6Option();
                        }

                        if (_MA_DM == "ALTK0")
                        {
                            UpdateBackTk();
                            UpdateLoaiTk("E");
                        }
                        if (_MA_DM == "ALVV")
                        {
                            UpdateBackVv();
                            UpdateLoaiVv("E");
                        }
                        if (_MA_DM == "HRPERSONAL")
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
                            V6ControlFormHelper.Copy_Here2Data(_MA_DM, Mode,
                                KeyField1, KeyField2, KeyField3,
                                newKey1, newKey2, newKey3,
                                oldKey1, oldKey2, oldKey3,
                                "");
                        }
                        return true;
                    }
                    else
                    {
                        if (showMessage) ShowTopLeftMessage(V6Text.UpdateFail);
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

                        if (_MA_DM == "V6USER")
                        {
                            UpdateInheritUser();
                            UpdateAdvanceInforUser();
                        }

                        if (_MA_DM == "ALTK0")
                        {
                            UpdateBackTk();
                            UpdateLoaiTk("A");
                        }
                        if (_MA_DM == "ALVV")
                        {
                            UpdateBackVv();
                            UpdateLoaiVv("A");
                        }
                        if (_MA_DM == "HRPERSONAL")
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
                            
                            V6ControlFormHelper.Copy_Here2Data(_MA_DM, Mode,
                                KeyField1, KeyField2, KeyField3,
                                newKey1, newKey2, newKey3,
                                oldKey1, oldKey2, oldKey3,
                                UID);
                        }
                        return true;
                    }
                    else
                    {
                        if (showMessage) ShowTopLeftMessage(V6Text.AddFail);
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
                    key_old[KEY] = DataOld[KEY];
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
                var result = Categories.Update(CONFIG_TABLE_NAME, DataDic, _keys);
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

        protected virtual void GetNewID()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig(_MA_DM);
                // Get new id proc 
                if (_aldmConfig.HaveInfo)
                {
                    // Trường hợp mã có phân nhóm.
                    // DataOld cần thêm dữ liệu AUTOID_LOAINH AUTOID_NHVALUE
                    if (DataOld != null && DataOld.ContainsKey("AUTOID_LOAINH") && DataOld.ContainsKey("AUTOID_NHVALUE"))
                    {

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@MA_DM", _MA_DM),
                            new SqlParameter("@Vvalue", DataOld.ContainsKey(_aldmConfig.VALUE.ToUpper()) ? DataOld[_aldmConfig.VALUE.ToUpper()].ToString().Trim() : ""),
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
                                value_dic.Add(_aldmConfig.VALUE.ToUpper(), value);
                                V6ControlFormHelper.SetSomeDataDictionary(this, value_dic);
                                return;
                            }
                        }
                    }
                    
                    
                    {
                        //var _dataRow = aldm.Rows[0];
                        if (_aldmConfig.INCREASE_YN)
                        {
                            update_stt13 = true;
                            var id_field = _aldmConfig.VALUE.ToUpper();
                            var stt13 = ObjectAndString.ObjectToInt(_aldmConfig.STT13);
                            var transform = _aldmConfig.TRANSFORM;
                            var value = string.Format(transform, stt13 + 1);
                            IDictionary<string, object> value_dic = new SortedDictionary<string, object>();
                            value_dic.Add(id_field, value);
                            V6ControlFormHelper.SetSomeDataDictionary(this, value_dic);
                            //var control = V6ControlFormHelper.GetControlByAccesibleName(this, id_field);
                            //if (control != null && control is TextBox)
                            //{
                            //    ((TextBox) control).Text = value;
                            //}
                        }
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

    }

}
