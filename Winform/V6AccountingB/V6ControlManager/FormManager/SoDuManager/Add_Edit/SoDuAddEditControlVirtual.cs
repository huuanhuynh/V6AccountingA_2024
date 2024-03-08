using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
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
        public AldmConfig _aldmConfig;
        public DataTable Alct1Data, Alct2Data;
        protected V6Categories Categories;
        public bool IS_COPY;
        
        /// <summary>
        /// Khi set Mact thì Alct1Data sẽ được tải.
        /// </summary>
        protected string Mact
        {
            get { return _maCt; }
            set
            {
                _maCt = value;
                Alct1Data = GetAlct1(_maCt);
                Alct2Data = GetAlct2();
            }
        }

        private string _maCt;

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
                    else if (!string.IsNullOrEmpty(_aldmConfig.TABLE_NAME)
                        && V6BusinessHelper.IsExistDatabaseTable(_aldmConfig.TABLE_NAME))
                    {
                        table = _aldmConfig.TABLE_NAME;
                    }
                }
                return table;
            }
        }
        protected string _table2Name, _table3Name, _table4Name, _table5Name;
        protected V6TableStruct _TableStruct;
        public V6Mode Mode = V6Mode.Add;
        protected SortedDictionary<int, Control> dynamicControlList1, dynamicControlList2, dynamicControlList3, dynamicControlList4;

        public string TitleLang
        {
            get
            {
                string title = CONFIG_TABLE_NAME;
                if (_aldmConfig != null && _aldmConfig.HaveInfo) title = V6Setting.IsVietnamese ? _aldmConfig.TITLE : _aldmConfig.TITLE2;
                return title;
            }
        }

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

        public DataTable data3, data4, data5;
        /// <summary>
        /// Data đưa vào để edit.
        /// </summary>
        protected IDictionary<string, object> DataOld { get; set; }
        public DataTable AD{get; set; }
        
        /// <summary>
        /// Dùng khi gọi form update, chứa giá trị cũ trước khi update.
        /// </summary>
        public IDictionary<string, object> _keys = new SortedDictionary<string, object>();
        
        /// <summary>
        /// Chứa data dùng để insert hoặc edit.
        /// </summary>
        public IDictionary<string, object> DataDic { get; set; }
        public IDictionary<string, object> DataDic2 { get; set; }
        public IDictionary<string, object> _parentData;
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
        public SoDuAddEditControlVirtual(AldmConfig aldmConfig)
        {
            _aldmConfig = aldmConfig;
            InitializeComponent();
            Categories = new V6Categories();
        }

        protected bool _call_LoadDetails_in_base = true;
        private void AddEditControlVirtual_Load(object sender, EventArgs e)
        {
            //virtual
            if (_call_LoadDetails_in_base) LoadDetails();
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
            CheckVvarTextBox();
            _ready0 = true;
            InvokeFormEvent(FormDynamicEvent.INIT2);
        }

        /// <summary>
        /// Chạy ExistRowInTable cho các V6VvarTextBox.
        /// </summary>
        private void CheckVvarTextBox()
        {
            try
            {
                if (this is SoDuAddEditControlDynamicForm) return;
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
        /// 
        /// </summary>
        /// <param name="ma_dm">Bảng đang xử lý</param>
        /// <param name="mode">Add/Edit/View</param>
        /// <param name="keys">Nếu data null thì load bằng keys</param>
        /// <param name="data">Gán dữ liệu này lên form</param>
        public virtual void InitValues(string ma_dm, V6Mode mode,
            IDictionary<string, object> keys, IDictionary<string, object> data)
        {
            _MA_DM = ma_dm.ToUpper();
            if (_aldmConfig == null) _aldmConfig = ConfigManager.GetAldmConfig(_MA_DM);
            Mode = mode;
            _keys = keys;
            DataOld = data;
            LoadAdvanceControls(_MA_DM);
            LoadDefaultData(2, "", _MA_DM, ItemID);
            if (Mode == V6Mode.View)  V6ControlFormHelper.SetFormControlsReadOnly(this, true);

            All_Objects["thisForm"] = this;
            CreateFormProgram();
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, ma_dm, Form_program, All_Objects);
            InvokeFormEvent(FormDynamicEvent.INIT);
            LoadAll();
            //LoadTag(2, "", _MA_DM, ItemID);
        }

        protected void CreateFormProgram()
        {
            try
            {
                string using_text = "";
                string method_text = "";

                //aldm.DMETHOD
                if (_aldmConfig.NoInfo || string.IsNullOrEmpty(_aldmConfig.DMETHOD))
                {
                    goto Alct1_DMETHOD;
                }
                
                //foreach (DataRow dataRow in Invoice.Alct1.Rows)
                {
                    var xml = _aldmConfig.DMETHOD;
                    if (xml == "") goto Alct1_DMETHOD;
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xml));
                    if (ds.Tables.Count <= 0) goto Alct1_DMETHOD;
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
                
                Alct1_DMETHOD:
                if (!Alct1Data.Columns.Contains("DMETHOD"))
                {
                    this.ShowWarningMessage("No column name [DMETHOD] in [Alct1]");
                    goto Alct2_DMETHOD;
                }

                foreach (DataRow dataRow in Alct1Data.Rows)
                {
                    var xml = dataRow["DMETHOD"].ToString().Trim();
                    if (xml == "") goto Build;
                    DataSet ds = ObjectAndString.XmlStringToDataSet(xml);
                    if (ds == null || ds.Tables.Count <= 0) goto Build;

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

                Alct2_DMETHOD:
                if (!Alct2Data.Columns.Contains("DMETHOD"))
                {
                    this.ShowWarningMessage("No column name [DMETHOD] in [Alct2]");
                    goto Build;
                }

                foreach (DataRow dataRow in Alct2Data.Rows)
                {
                    var xml = dataRow["DMETHOD"].ToString().Trim();
                    if (xml == "") goto Build;
                    DataSet ds = ObjectAndString.XmlStringToDataSet(xml);
                    if (ds == null || ds.Tables.Count <= 0) goto Build;

                    var data = ds.Tables[0];
                    foreach (DataRow event_row in data.Rows)
                    {
                        var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                        var method_name = event_row["method"].ToString().Trim();
                        if (!Event_Methods.ContainsKey(EVENT_NAME)) Event_Methods[EVENT_NAME] = method_name;

                        using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                        method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                    }
                }

                Build:
                Form_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + _aldmConfig.MA_DM, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
            }
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
        public DataTable GetAlct2()
        {
            SqlParameter[] pList =
                {
                    new SqlParameter("@ma_ct", Mact),
                    new SqlParameter("@list_fix", ""),
                    new SqlParameter("@order_fix", ""),
                    new SqlParameter("@vvar_fix", ""),
                    new SqlParameter("@type_fix", ""),
                    new SqlParameter("@checkvvar_fix", ""),
                    new SqlParameter("@notempty_fix", ""),
                    new SqlParameter("@fdecimal_fix", "")
                };

            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure,
                    "VPA_GET_AUTO_COLUMN_GT", pList).Tables[0];
        }

        #region ===== LoadAdvanceControls =====

        private void LoadAdvanceControls(string ma_bc)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(this, ma_bc, All_Objects);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls", ex);
            }
        }

        protected Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        
        #endregion LoadAdvanceControls
        
        public virtual void LoadAll()
        {
            LoadStruct();//MaxLength...
            V6ControlFormHelper.LoadAndSetFormInfoDefine(_MA_DM, this, Parent);

            if (Mode==V6Mode.Edit)
            {
                if(DataOld!=null) SetData(DataOld); else LoadData();
            }
            else if(Mode == V6Mode.Add)
            {
                //if (DataOld != null) SetData(DataOld);
                //else
                //{
                //    if(_keys!=null) LoadData();
                //}
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
                    var invoice = V6InvoiceBase.GetInvoiceBase(mact);
                    dgv.SetCorplan2();
                    V6ControlFormHelper.FormatGridViewAndHeader(dgv, invoice.GRDS_AD, invoice.GRDF_AD,
                        V6Setting.IsVietnamese ? invoice.GRDHV_AD : invoice.GRDHE_AD);
                    dgv.HideColumnsAldm(tableName);
                }
                else
                {
                    AldmConfig aldm = ConfigManager.GetAldmConfig(tableName);
                    if (aldm.HaveInfo)
                    V6ControlFormHelper.FormatGridViewAndHeader(dgv, aldm.GRDS_V1, aldm.GRDF_V1,
                        V6Setting.IsVietnamese ? aldm.GRDHV_V1 : aldm.GRDHE_V1);
                }
            }
        }

        protected virtual void SetDefaultDetail()
        {
            //SetDefaultDataHDDetail(Invoice, detail1);
        }

        public virtual void LoadStruct()
        {
            try
            {   
                _TableStruct = V6BusinessHelper.GetTableStruct(_MA_DM);
                if ((_TableStruct == null || _TableStruct.Count == 0) && _aldmConfig.HaveInfo)
                    _TableStruct = V6BusinessHelper.GetTableStruct(_aldmConfig.TABLE_NAME);
                V6ControlFormHelper.SetFormStruct(this, _TableStruct);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Load struct eror!\n" + ex.Message);
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

            return null;
        }
        
        public virtual bool DoInsertOrUpdate(bool showMessage = true)
        {
            try
            {
                FixFormData();
                DataDic = GetData();
                All_Objects["data"] = DataDic;
                All_Objects["dataOld"] = DataOld;
                ValidateData();
                InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
                
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
                        SaveEditHistory(DataOld, DataDic);
                        AfterSaveBase();
                        AfterSave();
                        AfterUpdate();
                        InvokeFormEvent(FormDynamicEvent.AFTERUPDATE);
                        return true;
                    }
                    
                    if (showMessage) ShowMainMessage(V6Text.UpdateFail);
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
                        AfterSaveBase();
                        AfterSave();
                        AfterInsert();
                        InvokeFormEvent(FormDynamicEvent.AFTERINSERT);
                    }
                    else
                    {
                        if (showMessage) ShowMainMessage(V6Text.AddFail);
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
                var result = Categories.Insert(CONFIG_TABLE_NAME, DataDic);
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

        protected virtual void GetNewID()
        {
            try
            {
                if (_aldmConfig == null) _aldmConfig = ConfigManager.GetAldmConfig(_MA_DM);
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
                SqlParameter[] plist = new[] { new SqlParameter("@ma_dm", _MA_DM) };
                V6BusinessHelper.ExecuteSqlNoneQuery(sql, plist);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddStt13", ex);
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
                foreach (string KEY in KEYS)
                {
                    if (!_TableStruct.ContainsKey(KEY)) continue;
                    var sct = _TableStruct[KEY];
                    var o_new = DataDic[KEY];
                    data_new += "|" + SqlGenerator.GenSqlStringValue(o_new, sct.sql_data_type_string, sct.ColumnDefault, false, sct.MaxLength);
                    var o_old = Mode == V6Mode.Edit ? DataOld[KEY] : o_new;
                    data_old += "|" + SqlGenerator.GenSqlStringValue(o_old, sct.sql_data_type_string, sct.ColumnDefault, false, sct.MaxLength);
                }

                if (data_new.Length > 1) data_new = data_new.Substring(1);
                if (data_old.Length > 1) data_old = data_old.Substring(1);

                SqlParameter[] plist =
                {
                    new SqlParameter("@TableName", _aldmConfig.TABLE_NAME),
                    new SqlParameter("@Fields", _aldmConfig.KEY),
                    new SqlParameter("@datas_old", data_old),
                    new SqlParameter("@datas_new", data_new),
                    new SqlParameter("@uid", Mode == V6Mode.Edit ? DataOld["UID"] : ""),
                    new SqlParameter("@mode", Mode == V6Mode.Add ? "M" : "S"),
                    new SqlParameter("@User_id", V6Login.UserId),
                };
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_AL_ALL", plist);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateAlqddvt", ex);
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
                    V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke AFTERSAVE", ex1);
            }
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
        /// Thêm vào lịch sử thay đổi dòng chi tiết.
        /// </summary>
        /// <param name="stt_rec0">Mã dòng</param>
        /// <param name="controlList1"></param>
        /// <param name="oldData">Dữ liệu cũ. null nếu thêm dòng.</param>
        /// <param name="newData">Dữ liệu mới. null nếu xóa.</param>
        protected void UpdateDetailChangeLog(string stt_rec0, IDictionary<int, Control> controlList1, IDictionary<string, object> oldData, IDictionary<string, object> newData)
        {
            if (controlList1 == null) return;

            if (oldData == null) // add
            {
                SortedDictionary<string, object> newData1 = new SortedDictionary<string, object>();
                foreach (KeyValuePair<int, Control> item in controlList1)
                {
                    string KEY = item.Value.AccessibleName.ToUpper();
                    if (item.Value.Visible && item.Value.Enabled && newData.ContainsKey(KEY))
                    {
                        if (!ObjectAndString.IsNoValue(newData[KEY]))
                        {
                            newData1[KEY] = newData[KEY];
                        }
                    }
                }

                editLogData["ADD_" + stt_rec0] = new OldNewData() {OldData = null, NewData = newData1};
            }
            else if (newData == null) // delete
            {
                SortedDictionary<string, object> oldData1 = new SortedDictionary<string, object>();
                foreach (KeyValuePair<int, Control> item in controlList1)
                {
                    string KEY = item.Value.AccessibleName.ToUpper();
                    if (item.Value.Visible && item.Value.Enabled && oldData.ContainsKey(KEY))
                    {
                        if (!ObjectAndString.IsNoValue(oldData[KEY]))
                        {
                            oldData1[KEY] = oldData[KEY];
                        }
                    }
                }

                editLogData["DELETE_" + stt_rec0] = new OldNewData() {OldData = oldData1, NewData = null};
            }
            else // edit
            {
                if (editLogData.ContainsKey("EDIT_" + stt_rec0))
                {
                    IDictionary<string, object> newData1 = new Dictionary<string, object>();
                    foreach (KeyValuePair<int, Control> item in controlList1)
                    {
                        if (item.Value.Visible && item.Value.Enabled)
                        {
                            string FIELD = item.Value.AccessibleName.ToUpper();
                            newData1[FIELD] = newData[FIELD];
                        }
                    }

                    editLogData["EDIT_" + stt_rec0].NewData = newData1;
                }
                else
                {
                    IDictionary<string, object> oldData1 = new Dictionary<string, object>();
                    IDictionary<string, object> newData1 = new Dictionary<string, object>();
                    foreach (KeyValuePair<int, Control> item in controlList1)
                    {
                        if (item.Value.Visible && item.Value.Enabled)
                        {
                            string FIELD = item.Value.AccessibleName.ToUpper();
                            oldData1[FIELD] = oldData[FIELD];
                            newData1[FIELD] = newData[FIELD];
                        }
                    }

                    editLogData["EDIT_" + stt_rec0] = new OldNewData() {OldData = oldData1, NewData = newData1};
                }
            }
        }

        //public void InitEditLog()
        //{
        //    editLogData = new Dictionary<string, OldNewData>();
        //}

        /// <summary>
        /// stt_rec0 add?edit?delete data
        /// </summary>
        private Dictionary<string, OldNewData> editLogData = new Dictionary<string, OldNewData>();

        
        private class OldNewData
        {
            public IDictionary<string, object> OldData = null;
            public IDictionary<string, object> NewData = null;
        }

        /// <summary>
        /// Trả về xml của List string
        /// </summary>
        /// <returns></returns>
        private string GetDetailInfo()
        {
            List<string> result = new List<string>();
            foreach (KeyValuePair<string, OldNewData> item in editLogData)
            {
                result.Add(item.Key + " " + ObjectAndString.DictionaryToString(
                    V6ControlFormHelper.CompareDifferentData(item.Value.OldData, item.Value.NewData)));
            }
            //if (result.Length > 1) result = result.Substring(1);
            return ObjectAndString.ListToXml(result);
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
                    string info = ObjectAndString.DictionaryToString(
                        V6ControlFormHelper.CompareDifferentData(data_old, data_new));
                    //V6BusinessHelper.write.WriteV6ListHistory(ItemID, MethodBase.GetCurrentMethod().Name,
                    //    string.IsNullOrEmpty(CodeForm) ? "N" : CodeForm[0].ToString(),
                    //    _aldmConfig.MA_DM,  ObjectAndString.ObjectToString(data_new[_aldmConfig.VALUE]), info, ObjectAndString.ObjectToString(data_old["UID"]));
                    string detailInfo = GetDetailInfo();
                    V6BusinessHelper.WriteV6InvoiceHistory(ItemID, MethodBase.GetCurrentMethod().Name, string.IsNullOrEmpty(CodeForm) ? "N" : CodeForm[0].ToString(),
                        ObjectAndString.ObjectToString(data_old["UID"]), Mact, _sttRec, info, detailInfo);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SaveEditHistory", ex);
            }
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
        //        Form_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + _aldmConfig.MA_DM, using_text, method_text);
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
        //            return V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
        //        }
        //    }
        //    catch (Exception ex1)
        //    {
        //        this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
        //    }
        //    return null;
        //}

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
                    //string format = sss.Length > 1 ? sss[1] : null;

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
                    //string format = sss.Length > 1 ? sss[1] : null;

                    Control c = V6ControlFormHelper.GetControlByAccessibleName(this, field);
                    if (c is TextBox) ((TextBox)c).ReadOnlyTag();
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
        protected string CheckValid(string ma_dm, IList<string> KEY_LIST)
        {
            var keys_new = new SortedDictionary<string, object>();
            foreach (string KEY in KEY_LIST)
            {
                keys_new.Add(KEY, DataDic[KEY].ToString().Trim());
            }
            
            string where_new = SqlGenerator.GenWhere(_TableStruct, keys_new);

            AldmConfig config = ConfigManager.GetAldmConfig(ma_dm);
            bool exist_new = V6BusinessHelper.CheckDataExistStruct(ma_dm, keys_new, config.CHECK_LONG);

            if (Mode == V6Mode.Edit)
            {
                SortedDictionary<string, object> keys_old = new SortedDictionary<string, object>();
                foreach (string KEY in KEY_LIST)
                {
                    keys_old.Add(KEY, DataOld[KEY].ToString().Trim());
                }
                string where_old = SqlGenerator.GenWhere(_TableStruct, keys_old);
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

        public virtual bool XuLyThemDetail(IDictionary<string, object> data)
        {
            throw new NotImplementedException();
        }
    }

}
