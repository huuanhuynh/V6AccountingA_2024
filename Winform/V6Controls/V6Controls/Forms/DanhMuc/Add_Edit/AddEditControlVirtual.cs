using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public V6TableName TableName { get; set; }
        protected V6TableStruct _structTable;
        public V6Mode Mode = V6Mode.Add;
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
        public SortedDictionary<string,object> DataOld { get; set; }
        /// <summary>
        /// Dùng khi gọi form update, chứa giá trị cũ trước khi update.
        /// </summary>
        public SortedDictionary<string, object> _keys = new SortedDictionary<string, object>();

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

        protected IDictionary<string, object> _parentData; 

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
            if (V6Setting.IsDesignTime) return;
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
        }
        
        /// <summary>
        /// Khởi tạo giá trị cho control với các tham số
        /// </summary>
        /// <param name="tableName">Bảng đang xử lý</param>
        /// <param name="mode">Add/Edit/View</param>
        /// <param name="keys">Nếu data null thì load bằng keys</param>
        /// <param name="data">Gán dữ liệu này lên form</param>
        public virtual void MyInit(V6TableName tableName, V6Mode mode,
            SortedDictionary<string, object> keys, SortedDictionary<string, object> data)
        {
            TableName = tableName;
            Mode = mode;
            _keys = keys;
            DataOld = data;
            if (Mode == V6Mode.View) V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            LoadAll();
            //virtual
            LoadDetails();
            
            LoadTag(2, "", TableName.ToString(), ItemID);
        }

        protected virtual void LoadAll()
        {

            LoadStruct();//MaxLength...
            V6ControlFormHelper.LoadAndSetFormInfoDefine(TableName.ToString(), this, Parent);

            if (Mode==V6Mode.Edit)
            {
                if(DataOld!=null) SetData(DataOld); else LoadData();
            }
            else if(Mode == V6Mode.Add)
            {
                var dataOld2 = new SortedDictionary<string, object>();
                if(DataOld != null) dataOld2.AddRange(DataOld);
                dataOld2["STATUS"] = "1";

                if (DataOld != null)
                {
                    SetData(dataOld2);
                }
                else if (_keys != null)
                {
                    LoadData();
                }
                else
                {
                    SetData(dataOld2);
                }
                
                LoadDefaultData(2, "", TableName.ToString(), m_itemId);
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
                _structTable = V6BusinessHelper.GetTableStruct(TableName.ToString());
                V6ControlFormHelper.SetFormStruct(this, _structTable);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Load struct eror!", ex);
            }

        }

        ///// <summary>
        ///// Tải thông tin tự định nghĩa lên form
        ///// </summary>
        //public void LoadUserDefineInfo ()
        //{
        //    try
        //    {
        //        var key = new SortedDictionary<string, object> {{"ma_dm", TableName.ToString()}};
        //        var selectResult = Categories.Select(V6TableName.Altt, key);
        //        V6ControlFormHelper.SetFormInfoDefine(this, selectResult.Data, V6Setting.Language);
                
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorException(GetType() + ".Load info error!", ex);
        //    }
        //}

        public virtual void LoadData()
        {
            try
            {
                if (_keys != null && _keys.Count > 0)
                {
                    var selectResult = Categories.Select(TableName, _keys);
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
                V6Options.V6OptionValues[KEY] = VALUE;
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
                ValidateData();
                string checkV6Valid = CheckV6Valid(DataDic, TableName.ToString());
                if (!string.IsNullOrEmpty(checkV6Valid))
                {
                    this.ShowInfoMessage(checkV6Valid);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
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
                        AfterUpdate();
                        
                        if (TableName == V6TableName.V6user)
                        {
                            UpdateInheritUser();
                        }

                        if (TableName == V6TableName.V6option)
                        {
                            UpdateV6Option();
                        }

                        if (TableName == V6TableName.Altk0)
                        {
                            UpdateBackTk();
                            UpdateLoaiTk("E");
                        }
                        if (TableName == V6TableName.Hrpersonal)
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
                            V6ControlFormHelper.Copy_Here2Data(TableName, Mode,
                                KeyField1, KeyField2, KeyField3,
                                newKey1, newKey2, newKey3,
                                oldKey1, oldKey2, oldKey3,
                                "");
                        }
                        return true;
                    }
                    else
                    {
                        if (showMessage) ShowTopMessage(V6Text.UpdateFail);
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
                        AfterInsert();
                        
                        if (TableName == V6TableName.V6user)
                        {
                            UpdateInheritUser();
                        }

                        if (TableName == V6TableName.Altk0)
                        {
                            UpdateBackTk();
                            UpdateLoaiTk("A");
                        }
                        if (TableName == V6TableName.Hrpersonal)
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
                            
                            V6ControlFormHelper.Copy_Here2Data(TableName, Mode,
                                KeyField1, KeyField2, KeyField3,
                                newKey1, newKey2, newKey3,
                                oldKey1, oldKey2, oldKey3,
                                UID);
                        }
                        return true;
                    }
                    else
                    {
                        if (showMessage) ShowTopMessage(V6Text.AddFail);
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
                
                var result = Categories.Insert(TableName, DataDic);
                if (result && update_stt13)
                {
                    AddStt13();
                }
                return result;
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
                this.WriteExLog(GetType() + ".InsertNew", ex);
                return false;
            }
        }

        /// <summary>
        /// Được gọi sau khi thêm thành công.
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
                var result = Categories.Update(TableName, DataDic, _keys);
                return result;
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
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
                //Get aldm row
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_DM", TableName.ToString());
                var aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                if (aldm.Rows.Count == 1)
                {
                    var _dataRow = aldm.Rows[0];
                    var increase = _dataRow["increase_yn"].ToString().Trim();
                    if (increase == "1")
                    {
                        update_stt13 = true;
                        var id_field = _dataRow["Value"].ToString().Trim().ToUpper();
                        var stt13 = ObjectAndString.ObjectToInt(_dataRow["Stt13"]);
                        var transform = _dataRow["transform"].ToString().Trim();
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
                SqlParameter[] plist = new []{new SqlParameter("@ma_dm", TableName.ToString())};
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
                var config = V6ControlsHelper.GetV6ValidConfigDanhMuc(tableName);

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
        protected string CheckValid(string tableName, IList<string> KEY_LIST)
        {
            var keys_new = new SortedDictionary<string, object>();
            foreach (string KEY in KEY_LIST)
            {
                keys_new.Add(KEY, DataDic[KEY]);
            }

            string where_new = SqlGenerator.GenWhere(V6BusinessHelper.GetTableStruct(tableName), keys_new);

            AldmConfig config = V6ControlsHelper.GetAldmConfig(tableName);
            bool exist_new = V6BusinessHelper.CheckDataExistStruct(tableName, keys_new, config.CHECK_LONG);

            if (Mode == V6Mode.Edit)
            {
                SortedDictionary<string, object> keys_old = new SortedDictionary<string, object>();
                foreach (string KEY in KEY_LIST)
                {
                    keys_old.Add(KEY, DataOld[KEY]);
                }
                string where_old = SqlGenerator.GenWhere(V6BusinessHelper.GetTableStruct(tableName), keys_old);
                //bool exist_old = V6BusinessHelper.CheckDataExistStruct(TableName, keys_old);

                if (where_new != where_old && exist_new)
                    return V6Text.EditDenied + " " + where_new;
            }
            else if (Mode == V6Mode.Add)
            {
                if (exist_new)
                    return V6Text.AddDenied + " " + where_new;
            }

            return "";
        }
        
    }

}
