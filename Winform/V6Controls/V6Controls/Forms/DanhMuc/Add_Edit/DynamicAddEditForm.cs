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
                var data = V6BusinessHelper.Select(V6TableName.Alreport1, keys, "*", "", "Stt_Filter").Data;
                int i = 0;
                int baseTop = panel1.AutoScrollPosition.Y;
                int rowHeight = 25;
                foreach (DataRow row in data.Rows)
                {
                    var define = row["Filter"].ToString().Trim();
                    var defineInfo = new DefineInfo(define);
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
            //if (txtMaMauBc.Text.Trim() == "")
            //    errors += "Chưa nhập mã!\r\n";
            //if (txtTenMauBc.Text.Trim() == "")
            //    errors += "Chưa nhập tên !\r\n";


            //if (Mode == V6Mode.Edit)
            //{
            //    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_MAUBC",
            //     txtMaMauBc.Text.Trim(), DataOld["MA_MAUBC"].ToString());
            //    if (!b)
            //        throw new Exception("Không được sửa mã đã tồn tại: "
            //                                        + "MA_MAUBC = " + txtMaMauBc.Text.Trim());
            //}
            //else if (Mode == V6Mode.Add)
            //{
            //    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_MAUBC",
            //     txtMaMauBc.Text.Trim(), txtMaMauBc.Text.Trim());
            //    if (!b)
            //        throw new Exception("Không được thêm mã đã tồn tại: "
            //                                        + "MA_MAUBC = " + txtMaMauBc.Text.Trim());
            //}

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
