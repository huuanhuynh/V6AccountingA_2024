using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class DynamicAddEditForm : AddEditControlVirtual
    {
        public DynamicAddEditForm()
        {
            InitializeComponent();
        }

        private string TableName;//Đè kiểu cũ. Các hàm cũ đã override.
        private DataRow _dataRow;
        private DataTable Alreport1Data = null;
        private Dictionary<V6NumberTextBox, int> NumberTextBox_Format = new Dictionary<V6NumberTextBox, int>();
        private Dictionary<string, DefineInfo> DefineInfo_Data = new Dictionary<string, DefineInfo>(); 

        public DynamicAddEditForm(string tableName, DataRow dataRow)
        {
            TableName = tableName;
            _dataRow = dataRow;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                //Tạo control động
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_BC", TableName);
                Alreport1Data = V6BusinessHelper.Select(V6TableName.Alreport1, keys, "*", "", "Stt_Filter").Data;
                int i = 0;
                int baseTop = panel1.AutoScrollPosition.Y;
                int rowHeight = 25;
                foreach (DataRow row in Alreport1Data.Rows)
                {
                    var define = row["Filter"].ToString().Trim();
                    var defineInfo = new DefineInfo(define);
                    DefineInfo_Data[defineInfo.Field.ToUpper()] = defineInfo;
                    //Label
                    var top = baseTop + i * rowHeight;
                    var label = new V6Label();
                    label.AutoSize = true;
                    label.Left = 10;
                    label.Top = top;
                    label.Text = defineInfo.TextLang(V6Setting.IsVietnamese);
                    panel1.Controls.Add(label);
                    //Input
                    V6ColorTextBox input = null;
                    if (ObjectAndString.IsDateTimeType(defineInfo.DataType))
                    {
                        input = new V6DateTimeColor();
                    }
                    else if (ObjectAndString.IsNumberType(defineInfo.DataType))
                    {
                        input = new V6NumberTextBox();
                        var nT = (V6NumberTextBox)input;
                        //nT.DecimalPlaces = defineInfo.Decimals;
                        NumberTextBox_Format[nT] = defineInfo.Decimals;
                    }
                    else
                    {
                        input = new V6VvarTextBox()
                        {
                            VVar = defineInfo.Vvar,
                        };
                        var tT = (V6VvarTextBox)input;
                        tT.SetInitFilter(defineInfo.InitFilter);
                        tT.F2 = defineInfo.F2;
                    }
                    if (input != null)
                    {
                        input.AccessibleName = defineInfo.Field;
                        input.Width = string.IsNullOrEmpty(defineInfo.Width)
                            ? 150
                            : ObjectAndString.ObjectToInt(defineInfo.Width);
                        input.Left = 150;
                        input.Top = top;
                        
                        panel1.Controls.Add(input);
                        //Add brother
                        if (input is V6VvarTextBox && !string.IsNullOrEmpty(defineInfo.BField))
                        {
                            var tT = (V6VvarTextBox)input;
                            tT.BrotherFields = defineInfo.BField;
                            var txtB = new V6ColorTextBox();
                            txtB.AccessibleName = defineInfo.BField;
                            txtB.Top = top;
                            txtB.Left = input.Right + 10;
                            txtB.Width = panel1.Width - txtB.Left - 10;
                            txtB.ReadOnly = true;
                            txtB.TabStop = false;

                            panel1.Controls.Add(txtB);
                        }
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        /// <summary>
        /// Khởi tạo giá trị cho control với các tham số
        /// </summary>
        /// <param name="tableName">Bảng đang xử lý</param>
        /// <param name="mode">Add/Edit/View</param>
        /// <param name="keys">Nếu data null thì load bằng keys</param>
        /// <param name="data">Gán dữ liệu này lên form</param>
        public override void MyInit(V6TableName tableName, V6Mode mode,
            SortedDictionary<string, object> keys, SortedDictionary<string, object> data)
        {
            //TableName = tableName;
            Mode = mode;
            _keys = keys;
            DataOld = data;
            if (Mode == V6Mode.View) V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            LoadAll();
            //virtual
            LoadDetails();

            LoadTag(2, "", TableName.ToString());
        }

        protected override void LoadAll()
        {
            LoadStruct();//MaxLength...
            FixNumberTextBoxFormat();
            V6ControlFormHelper.LoadAndSetFormInfoDefine(TableName.ToString(), this, Parent);

            if (Mode == V6Mode.Edit)
            {
                if (DataOld != null) SetData(DataOld); else LoadData();
            }
            else if (Mode == V6Mode.Add)
            {
                if (DataOld != null) SetData(DataOld);
                else
                {
                    if (_keys != null) LoadData();
                    else LoadDefaultData(2, "", TableName.ToString(), m_itemId);
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

        private void FixNumberTextBoxFormat()
        {
            try
            {
                foreach (KeyValuePair<V6NumberTextBox, int> item in NumberTextBox_Format)
                {
                    var addLength = item.Value - item.Key.DecimalPlaces;
                    if (item.Key.DecimalPlaces == 0 && item.Value > 0) addLength++;
                    item.Key.DecimalPlaces = item.Value;
                    item.Key.MaxLength += addLength;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixNumberTextBoxFormat", ex);
            }
        }

        protected override void LoadStruct()
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

        public override void LoadData()
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

        public override bool DoInsertOrUpdate(bool showMessage = true)
        {
            ReloadFlag = false;
            if (Mode == V6Mode.Edit)
            {
                try
                {

                    int b = UpdateData();
                    if (b > 0)
                    {
                        AfterUpdate();
                        
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
            else if (Mode == V6Mode.Add)
            {
                try
                {
                    bool b = InsertNew();
                    if (b)
                    {
                        AfterInsert();

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

        public override bool InsertNew()
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
                this.ShowErrorException(GetType() + ".InsertNew", ex);
                return false;
            }
        }

        public override int UpdateData()
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
                this.ShowErrorException(GetType() + ".UpdateData", ex);
                return 0;
            }
        }

        public override void DoBeforeEdit()
        {
            //txtMaMauBc.Enabled = false;
        }

        public override void ValidateData()
        {
            var errors = "";

            // check notempty
            foreach (KeyValuePair<string, DefineInfo> item in DefineInfo_Data)
            {
                if (item.Value.NotEmpty)
                {
                    if (DataDic.ContainsKey(item.Key))
                    {
                        if (DataDic[item.Key].ToString().Trim() == "")
                        {
                            errors += string.Format("Chưa nhập {0}: {1}\r\n", item.Key, item.Value.TextLang(V6Setting.IsVietnamese));
                        }
                    }
                    else
                    {
                        errors += string.Format("Không lấy được dữ liệu {0}: {1}\r\n", item.Key, item.Value.TextLang(V6Setting.IsVietnamese));
                    }
                }
            }

            // check code
            //_dataRow;// aldm
            var GRD_COL = _dataRow["GRD_COL"].ToString().Trim().ToUpper();
            var KEY_LIST = ObjectAndString.SplitString(_dataRow["KEY"].ToString().Trim());
            string KEY1 = "", KEY2 = "", KEY3 = "";
            if (GRD_COL == "ONECODE" && KEY_LIST.Length > 0)
            {
                KEY1 = KEY_LIST[0].Trim().ToUpper();
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName, 0, KEY1,
                     DataDic[KEY1].ToString(), DataOld[KEY1].ToString());
                    if (!b)
                        throw new Exception(string.Format("Không được sửa mã đã tồn tại: {0} = {1}", KEY1, DataDic[KEY1]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName, 1, KEY1,
                        DataDic[KEY1].ToString(), DataDic[KEY1].ToString());
                    if (!b)
                        throw new Exception(string.Format("Không được thêm mã đã tồn tại: {0} = {1}", KEY1, DataDic[KEY1]));
                }
            }
            else if (GRD_COL == "TWOCODE" && KEY_LIST.Length > 1)
            {
                KEY1 = KEY_LIST[0].Trim().ToUpper();
                KEY2 = KEY_LIST[1].Trim().ToUpper();
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName, 0,
                        KEY1, DataDic[KEY1].ToString(), DataOld[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataOld[KEY2].ToString());
                    if (!b)
                        throw new Exception(string.Format("Không được sửa mã đã tồn tại: {0},{1} = {2},{3}",
                            KEY1, KEY2, DataDic[KEY1], DataDic[KEY2]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName, 1,
                        KEY1, DataDic[KEY1].ToString(), DataDic[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataDic[KEY2].ToString());
                    if (!b)
                        throw new Exception(string.Format("Không được thêm mã đã tồn tại: {0},{1} = {2},{3}",
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
                    bool b = V6BusinessHelper.IsValidThreeCode(TableName, 0,
                        KEY1, DataDic[KEY1].ToString(), DataOld[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataOld[KEY2].ToString(),
                        KEY3, DataDic[KEY3].ToString(), DataOld[KEY3].ToString());
                    if (!b)
                        throw new Exception(string.Format("Không được sửa mã đã tồn tại: {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode(TableName, 1,
                        KEY1, DataDic[KEY1].ToString(), DataDic[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataDic[KEY2].ToString(),
                        KEY3, DataDic[KEY3].ToString(), DataDic[KEY3].ToString());
                    if (!b)
                        throw new Exception(string.Format("Không được thêm mã đã tồn tại: {0},{1},{2} = {3},{4},{5}",
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
                    bool b = V6BusinessHelper.IsValidThreeCode(TableName, 0,
                        KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataOld[KEY1], "yyyyMMdd"),
                        KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataOld[KEY2], "yyyyMMdd"),
                        KEY3, DataDic[KEY3].ToString(), ObjectAndString.ObjectToString(DataOld[KEY3], "yyyyMMdd"));
                    if (!b)
                        throw new Exception(string.Format("Không được sửa mã đã tồn tại: {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_OneDate(TableName, 1,
                        KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataDic[KEY1], "yyyyMMdd"),
                        KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataDic[KEY2], "yyyyMMdd"),
                        KEY3, DataDic[KEY3].ToString(), ObjectAndString.ObjectToString(DataDic[KEY3], "yyyyMMdd"));
                    if (!b)
                        throw new Exception(string.Format("Không được thêm mã đã tồn tại: {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
            }
            else
            {
                DoNothing();
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void CheckVvarTextBox()
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    var vT = control as V6VvarTextBox;
                    if (vT != null && !string.IsNullOrEmpty(vT.VVar))
                    {
                        vT.ExistRowInTable();
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CheckVvarTextBox", ex);
            }
        }

        private void DynamicAddEditForm_Load(object sender, EventArgs e)
        {
            CheckVvarTextBox();
        }
    }
}
