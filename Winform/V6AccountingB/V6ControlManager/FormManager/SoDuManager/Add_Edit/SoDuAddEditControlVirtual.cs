﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class SoDuAddEditControlVirtual : V6FormControl
    {
        protected V6Categories Categories;
        protected string _maCt;
        public V6TableName TableName { get; set; }
        protected string _table2Name, _table3Name, _table4Name, _table5Name;
        protected V6TableStruct _TableStruct;
        public V6Mode Mode = V6Mode.Add;
        /// <summary>
        /// Mode != V6Mode.Add && Mode != V6Mode.Edit
        /// </summary>
        public bool NotAddEdit
        {
            get
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return true;
                return false;
            }
        }
        protected DataTable data3, data4, data5;
        /// <summary>
        /// Data đưa vào để edit.
        /// </summary>
        protected IDictionary<string, object> DataOld { get; set; }
        public DataTable AD{get; set; }
        
        /// <summary>
        /// Dùng khi gọi form update, chứa giá trị cũ trước khi update.
        /// </summary>
        private IDictionary<string, object> _keys = new SortedDictionary<string, object>();
        /// <summary>
        /// Chứa data dùng để insert hoặc edit.
        /// </summary>
        public IDictionary<string, object> DataDic { get; set; }
        public IDictionary<string, object> DataDic2 { get; set; }

        protected DataGridViewRow _gv1EditingRow;
        protected DataGridViewRow _gv2EditingRow;
        protected DataGridViewRow _gv3EditingRow;
        protected DataGridViewRow _gv4EditingRow;

        #region ==== For API Mode ====

        public string AddLink = "";
        public string EditLink = "";
        #endregion for api mode

        /// <summary>
        /// Dùng tự do, gán các propertie, field xong sẽ gọi loadAll
        /// </summary>
        public SoDuAddEditControlVirtual()
        {
            InitializeComponent();
            Categories = new V6Categories();
        }

        protected bool _call_LoadDetails_in_base = true;
        private void AddEditControlVirtual_Load(object sender, EventArgs e)
        {
            //virtual
            if (_call_LoadDetails_in_base)
            LoadDetails();
            //load truoc lop ke thua
            if (Mode == V6Mode.Add)
            {
                DoBeforeAdd();
                if (DataOld != null) DoBeforeCopy();
            }
            else if (Mode == V6Mode.Edit)
            {
                DoBeforeEdit();
            }
            else if (Mode == V6Mode.View)
            {
                DoBeforeView();
            }
            _ready0 = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">Bảng đang xử lý</param>
        /// <param name="mode">Add/Edit/View</param>
        /// <param name="keys">Nếu data null thì load bằng keys</param>
        /// <param name="data">Gán dữ liệu này lên form</param>
        public void MyInit(V6TableName tableName, V6Mode mode,
            IDictionary<string, object> keys, IDictionary<string, object> data)
        {
            TableName = tableName;
            Mode = mode;
            _keys = keys;
            DataOld = data;
            LoadAdvanceControls(TableName);
            if(Mode == V6Mode.View)  V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            LoadAll();
            
        }

        public DataTable GetAlct1(string mact)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@list_fix", ""),
                new SqlParameter("@order_fix", ""),
                new SqlParameter("@vvar_fix", ""),
                new SqlParameter("@type_fix", ""),
                new SqlParameter("@checkvvar_fix", ""),
                new SqlParameter("@notempty_fix", ""),
                new SqlParameter("@fdecimal_fix", "")
            };

            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure,
                    "VPA_GET_AUTO_COLULMN", plist).Tables[0];
        }

        #region ===== LoadAdvanceControls =====

        private void LoadAdvanceControls(V6TableName tableName)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(this, tableName.ToString(), All_Objects);// CreateFormControls();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls", ex);
            }
        }

        protected Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        
        #endregion LoadAdvanceControls
        
        private void LoadAll()
        {
            LoadStruct();//MaxLength...
            V6ControlFormHelper.LoadAndSetFormInfoDefine(TableName.ToString(), this, Parent);

            if (Mode==V6Mode.Edit)
            {
                if(DataOld!=null) SetData(DataOld); else LoadData();
            }
            else if(Mode == V6Mode.Add)
            {
                if (DataOld != null) SetData(DataOld);
                else
                {
                    if(_keys!=null) LoadData();
                }
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
        /// Hàm tải dữ liệu chi tiết, gọi ở Virtual_Load
        /// Khi cần dùng sẽ viết override, không cần gọi lại!
        /// </summary>
        public virtual void LoadDetails()
        {
            //try
            //{
            //    if (DataOld != null) // All Mode
            //    {
            //        string sttRec = DataOld["STT_REC"].ToString();
            //        {
            //            string sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table2Name +
            //                         " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
            //            SqlParameter[] plist = {new SqlParameter("@rec", sttRec)};
            //            AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];

            //            dataGridView1.DataSource = AD;
            //            dataGridView1.HideColumnsAldm(_table2Name);
            //            dataGridView1.SetCorplan2();
            //        }
            //    }
            //    else
            //    {
            //        string sttRec = "";
            //        string sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table2Name +
            //                    " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
            //        SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
            //        AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];

            //        SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            //}
        }

        /// <summary>
        /// Gán dữ liệu và format.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="data"></param>
        /// <param name="mact"></param>
        /// <param name="tableName">Dùng để load format HideColumns. Nếu để null sẽ tự lấy _table2Name</param>
        public void SetDataToGrid(V6ColorDataGridView dgv, object data, string mact, string tableName = null)
        {
            if (AD != null)
            {
                if (tableName == null) tableName = _table2Name;
                dgv.DataSource = data;

                if (string.Equals(tableName, _table2Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    var invoice = new V6InvoiceBase(mact);
                    dgv.SetCorplan2();
                    V6ControlFormHelper.FormatGridViewAndHeader(dgv, invoice.GRDS_AD, invoice.GRDF_AD,
                        V6Setting.IsVietnamese ? invoice.GRDHV_AD : invoice.GRDHE_AD);
                    dgv.HideColumnsAldm(tableName);
                }
                else
                {
                    AldmConfig aldm = ConfigManager.GetAldmConfig(tableName);
                    V6ControlFormHelper.FormatGridViewAndHeader(dgv, aldm.GRDS_V1, aldm.GRDF_V1,
                        V6Setting.IsVietnamese ? aldm.GRDHV_V1 : aldm.GRDHE_V1);
                }
            }
        }

        protected virtual void SetDefaultDetail() { }

        private void LoadStruct()
        {
            try
            {   
                _TableStruct = V6BusinessHelper.GetTableStruct(TableName.ToString());
                V6ControlFormHelper.SetFormStruct(this, _TableStruct);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Load struct eror!\n" + ex.Message);
            }

        }

        ///// <summary>
        ///// Tải thông tin tự định nghĩa lên form
        ///// </summary>
        //internal void LoadUserDefineInfo()
        //{
        //    try
        //    {
        //        var key = new SortedDictionary<string, object> {{"ma_dm", TableName.ToString()}};
        //        var selectResult = Categories.Select(V6TableName.Altt, key);
        //        V6ControlFormHelper.SetFormInfoDefine(this, selectResult.Data, V6Setting.Language);
                
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorMessage(GetType() + ".Load info error!\n" + ex.Message);
        //    }
        //}

        private void LoadData()
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
                        throw new Exception(V6Text.WrongData + " >1");
                    }
                    else
                    {
                        throw new Exception(V6Text.NoData);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Load data error!\n" + ex.Message);
            }
        }
        
        public bool DoInsertOrUpdate(bool showMessage = true)
        {
            try
            {
                FixFormData();
                DataDic = GetData();
                All_Objects["data"] = DataDic;
                All_Objects["dataOld"] = DataOld;
                ValidateData();
                //InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
                //InvokeFormEvent("BEFOREINSERTORUPDATE");
                //string checkV6Valid = CheckV6Valid(DataDic, TableName.ToString());
                //if (!string.IsNullOrEmpty(checkV6Valid))
                //{
                //    this.ShowInfoMessage(checkV6Valid);
                //    return false;
                //}
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
                    DataDic = GetData();
                    int b = UpdateData();
                    if (b > 0)
                    {
                        AfterUpdate();
                        return true;
                    }
                    
                    if (showMessage) ShowTopLeftMessage(V6Text.UpdateFail);
                }
                catch (Exception e1)
                {
                    if (showMessage) this.ShowErrorException(GetType() + ".UpdateFail", e1);
                }
            }
            else if(Mode == V6Mode.Add)
            {
                try
                {
                    DataDic = GetData();
                    bool b = InsertNew();
                    if (b)
                    {
                        AfterInsert();
                    }
                    else
                    {
                        if (showMessage) ShowTopLeftMessage(V6Text.AddFail);
                    }
                    return b;
                }
                catch (Exception ex)
                {
                    if (showMessage) this.ShowErrorException(GetType() + ".AddFail", ex);
                }
            }
            return false;
        }

        public virtual bool InsertNew()
        {
            try
            {
                FixFormData();
                DataDic = GetData();
                ValidateData();
                var result = Categories.Insert(TableName, DataDic);
                return result;
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
                this.WriteExLog(GetType() + ".InsertNew", ex);
                return false;
            }
        }

        public virtual int UpdateData()
        {
            try
            {
                FixFormData();
                DataDic = GetData();
                ValidateData();
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

        public override void SetData(IDictionary<string, object> d)
        {
            base.SetData(d);
            AfterSetData();
        }

        /// <summary>
        /// Chuẩn hóa lại dữ liệu trước khi xử lý.
        /// </summary>
        public virtual void FixFormData()
        {

        }

        /// <summary>
        /// Các form kế thừa cần override hàm này.
        /// Throw new [V6Categories]Exception nếu có dữ liệu sai
        /// </summary>
        /// <returns></returns>
        public virtual void ValidateData()
        {
            // Code mẫu cho hàm check động ValidateMasterData.
            string error = ValidateMasterData(_maCt);
            if(!string.IsNullOrEmpty(error)) throw new Exception(error);
        }

        protected string ValidateMasterData(string maCt)
        {
            string error = "";
            var v6validConfig = ConfigManager.GetV6ValidConfig(maCt, 1);

            if (v6validConfig != null && v6validConfig.HaveInfo)
            {
                var a_fields = v6validConfig.A_field.Split(',');
                foreach (string field in a_fields)
                {
                    var control = V6ControlFormHelper.GetControlByAccessibleName(this, field);
                    if (control is V6DateTimeColor)
                    {
                        if (((V6DateTimeColor)control).Value == null)
                        {
                            string message = string.Format("{0}: [{1}] {2}", V6Text.Text("CHUANHAPGIATRI"), field, V6Text.FieldCaption(field));
                            error += message + "\n";
                            this.ShowWarningMessage(message);
                            control.Focus();
                        }
                    }
                    else if (control is V6NumberTextBox)
                    {
                        if (((V6NumberTextBox)control).Value == 0)
                        {
                            string message = string.Format("{0}: [{1}] {2}", V6Text.Text("CHUANHAPGIATRI"), field, V6Text.FieldCaption(field));
                            error += message + "\n";
                            this.ShowWarningMessage(message);
                            control.Focus();
                        }
                    }
                    else if (control is TextBox)
                    {
                        if (string.IsNullOrEmpty(control.Text))
                        {
                            string message = string.Format("{0}: [{1}] {2}", V6Text.Text("CHUANHAPGIATRI"), field, V6Text.FieldCaption(field));
                            error += message + "\n";
                            this.ShowWarningMessage(message);
                            control.Focus();
                        }
                    }
                }
            }
            else
            {
                V6ControlFormHelper.SetStatusText(V6Text.Text("NoInfo") + " V6Valid!");
            }
            return error;
        }

        /// <summary>
        /// <para>Kiểm tra dữ liệu chi tiết hợp lệ quy định trong V6Valid.</para>
        /// <para>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</para>
        /// </summary>
        /// <param name="table2Struct">Cấu trúc bảng chi tiết.</param>
        /// <param name="data"></param>
        /// <param name="maCt"></param>
        /// <returns>Nếu hợp lệ trả về rỗng hoặc null, Nếu ko trả về message.</returns>
        protected string ValidateDetailData(string maCt, V6TableStruct table2Struct, IDictionary<string, object> data)
        {
            string error = "";
            try
            {
                var config = ConfigManager.GetV6ValidConfig(maCt, 2);

                if (config != null && config.HaveInfo)
                {
                    //Trường bắt buột nhập dữ liệu.
                    var a_fields = ObjectAndString.SplitString(config.A_field);
                    foreach (string field in a_fields)
                    {
                        string FIELD = field.Trim().ToUpper();
                        if (!data.ContainsKey(FIELD))
                        {
                            //error += string.Format("{0}: [{1}]\n", V6Text.NoData, FIELD);
                            continue;
                        }

                        V6ColumnStruct columnS = table2Struct[FIELD];
                        object value = data[FIELD];
                        if (ObjectAndString.IsDateTimeType(columnS.DataType))
                        {
                            if (value == null) error += V6Text.NoInput + " [" + FIELD + "]\n";
                        }
                        else if (ObjectAndString.IsNumberType(columnS.DataType))
                        {
                            if (ObjectAndString.ObjectToDecimal(value) == 0) error += V6Text.NoInput + " [" + FIELD + "]\n";
                        }
                        else // string
                        {
                            if (("" + value).Trim() == "") error += V6Text.NoInput + " [" + FIELD + "]\n";
                        }
                    }

                    //Trường vvar
                    var a_field2s = ObjectAndString.SplitString(config.A_field2);
                    foreach (string field2 in a_field2s)
                    {
                        var vvar = GetControlByAccessibleName(field2) as V6VvarTextBox;
                        if (vvar != null)
                        {
                            if (vvar.CheckNotEmpty && vvar.CheckOnLeave && !vvar.ExistRowInTable(true))
                            {
                                error += V6Text.Wrong + " [" + field2 + "]\n";
                            }
                        }
                    }
                }
                else
                {
                    ShowMainMessage(V6Text.Text("NoInfo") + " V6Valid!");
                }
            }
            catch (Exception ex)
            {
                //error += ex.Message;//Lỗi chương trình không liên quan lỗi nhập liệu
                this.WriteExLog(GetType() + ".ValidateData_Detail " + _sttRec, ex);
            }
            return error;
        }

        /// <summary>
        /// Được gọi sau khi thêm thành công.
        /// </summary>
        public virtual void AfterInsert()
        {

        }

        /// <summary>
        /// Được gọi sau khi sửa thành công.
        /// </summary>
        public virtual void AfterUpdate()
        {

        }

        /// <summary>
        /// Hàm được gọi sau khi gán data chính(AM) lên form.
        /// </summary>
        public virtual void AfterSetData()
        {
            this.SetAllVvarBrotherFields();
        }

        /// <summary>
        /// Xử lý khi load form trường hợp sửa
        /// Dùng hàm check IsExistOneCode proc
        /// </summary>
        public virtual void DoBeforeEdit()
        {
            
        }
        public virtual void DoBeforeCopy()
        {

        }
        public virtual void DoBeforeAdd()
        {

        }
        public virtual void DoBeforeView()
        {

        }

        //protected void CreateFormProgram()
        //{
        //    try
        //    {
        //        //DMETHOD
        //        if (_aldmConfig.NoInfo || string.IsNullOrEmpty(_aldmConfig.DMETHOD))
        //        {
        //            //this.ShowWarningMessage("No column name [DMETHOD]");
        //            return;
        //        }
        //        string using_text = "";
        //        string method_text = "";
        //        //foreach (DataRow dataRow in Invoice.Alct1.Rows)
        //        {
        //            var xml = _aldmConfig.DMETHOD;
        //            if (xml == "") return;
        //            DataSet ds = new DataSet();
        //            ds.ReadXml(new StringReader(xml));
        //            if (ds.Tables.Count <= 0) return;
        //            var data = ds.Tables[0];
        //            foreach (DataRow event_row in data.Rows)
        //            {
        //                var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
        //                var method_name = event_row["method"].ToString().Trim();
        //                Event_Methods[EVENT_NAME] = method_name;
        //                using_text += data.Columns.Contains("using") ? event_row["using"] : "";
        //                method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
        //            }
        //        }
        //    Build:
        //        Event_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + _aldmConfig.MA_DM, using_text, method_text);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteExLog(GetType() + ".CreateProgram0", ex);
        //    }
        //}

        ///// <summary>
        ///// Gọi hàm động theo tên event đã định nghĩa.
        ///// </summary>
        ///// <param name="eventName"></param>
        ///// <returns></returns>
        //public object InvokeFormEvent(string eventName)
        //{
        //    try // Dynamic invoke
        //    {
        //        if (Event_Methods.ContainsKey(eventName))
        //        {
        //            var method_name = Event_Methods[eventName];
        //            return V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
        //        }
        //    }
        //    catch (Exception ex1)
        //    {
        //        this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
        //    }
        //    return null;
        //}

        /// <summary>
        /// Kiểm tra dữ liệu để thêm hoặc sửa, Trả về chuỗi lỗi, nếu hợp lệ trả về null hoặc rỗng.
        /// </summary>
        protected string CheckValid(string tableName, IList<string> KEY_LIST)
        {
            var keys_new = new SortedDictionary<string, object>();
            foreach (string KEY in KEY_LIST)
            {
                keys_new.Add(KEY, DataDic[KEY].ToString().Trim());
            }

            string where_new = SqlGenerator.GenWhere(V6BusinessHelper.GetTableStruct(tableName), keys_new);

            AldmConfig config = ConfigManager.GetAldmConfig(tableName);
            bool exist_new = V6BusinessHelper.CheckDataExistStruct(tableName, keys_new, config.CHECK_LONG);

            if (Mode == V6Mode.Edit)
            {
                SortedDictionary<string, object> keys_old = new SortedDictionary<string, object>();
                foreach (string KEY in KEY_LIST)
                {
                    keys_old.Add(KEY, DataOld[KEY].ToString().Trim());
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
