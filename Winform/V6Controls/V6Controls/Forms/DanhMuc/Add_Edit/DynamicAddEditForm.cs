using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
using V6AccountingBusiness;
using V6Controls.Controls;
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
        private DataTable AlreportData = null;
        private DataTable Alreport1Data = null;
        private Dictionary<V6NumberTextBox, int> NumberTextBox_Decimals = new Dictionary<V6NumberTextBox, int>();
        private Dictionary<V6ColorTextBox, int> V6ColorTextBox_MaxLength = new Dictionary<V6ColorTextBox, int>();
        /// <summary>
        /// AccessibleName_KEY - DefineInfo
        /// </summary>
        private Dictionary<string, DefineInfo> DefineInfo_Data = new Dictionary<string, DefineInfo>();
        /// <summary>
        /// Danh sách event_method của Form_program.
        /// </summary>
        private Dictionary<string, string> Event_Methods = new Dictionary<string, string>(); 
        
        private Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        private Dictionary<string, Label> Label_Controls = new Dictionary<string, Label>();
        private Dictionary<string, Control> Input_Controls = new Dictionary<string, Control>(); 

        public DynamicAddEditForm(string tableName, DataRow dataRow)
        {
            TableName = tableName;
            _dataRow = dataRow;
            InitializeComponent();
            MyInit();
        }

        private Type Form_program;
        private Type Event_program;
        private string all_using_text = "", all_method_text = "";
        private void MyInit()
        {
            CreateFormProgram();
            CreateFormControls();

            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey("INIT"))
                {
                    var method_name = Event_Methods["INIT"];
                    V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke INIT", ex1);
            }
            Disposed += DynamicAddEditForm_Disposed;
        }

        void DynamicAddEditForm_Disposed(object sender, EventArgs e)
        {
            //try
            //{
            //    if(Form_program != null) AppDomain.Unload(Form_program);
            //}
            //catch (Exception)
            //{
                
            //}
        }

        /// <summary>
        /// Tạo các control và sự kiện [động] của nó theo các thông tin định nghĩa [Alreport1]
        /// </summary>
        private void CreateFormControls()
        {
            try
            {
                //Đưa form chính vào listObj
                All_Objects["thisForm"] = this;
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
                    var AccessibleName_KEY = string.IsNullOrEmpty(defineInfo.AccessibleName)
                        ? defineInfo.Field.ToUpper()
                        : defineInfo.AccessibleName.ToUpper();
                    DefineInfo_Data[AccessibleName_KEY.ToUpper()] = defineInfo;
                    //Label
                    var top = baseTop + i * rowHeight;

                    var label = new V6Label();
                    label.Name = "lbl" + defineInfo.Field;
                    label.AutoSize = true;
                    label.Left = 10;
                    label.Top = top;
                    label.Text = defineInfo.TextLang(V6Setting.IsVietnamese);
                    label.Visible = defineInfo.Visible;
                    panel1.Controls.Add(label);
                    Label_Controls[defineInfo.Field.ToUpper()] = label;
                    All_Objects[label.Name] = label;
                    //Input
                    // Các thuộc tính của input sẽ được gán sau cùng nếu chưa khởi tạo
                    // AccessibleName = AccessibleName_KEY (AccessibleName hoặc Field)
                    // Text = defineInfo.DefaultValue
                    // Visible = defineInfo.Visible;
                    // Luôn gán
                    // Width = define.Width, // Left = 150, Top = top
                    Control input = null;
                    if (defineInfo.ControlType != null)
                    {
                        if (defineInfo.ControlType.ToUpper() == "BUTTON")
                        {
                            input = new V6FormButton()
                            {
                                Name = "btn" + defineInfo.Field,
                                AccessibleName = "",
                                Text = defineInfo.TextLang(V6Setting.IsVietnamese),
                                UseVisualStyleBackColor = true
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "LOOKUPTEXTBOX")
                        {
                            input = new V6LookupTextBox()
                            {
                                Name = "txt" + defineInfo.Field,
                                Ma_dm = defineInfo.MA_DM, //Mã danh mục trong Aldm
                                AccessibleName = defineInfo.AccessibleName, //Trường get dữ liệu
                                AccessibleName2 = defineInfo.AccessibleName2, //Trường get text hiển thị
                                ValueField = defineInfo.Field, //Trường dữ liệu
                                ShowTextField = defineInfo.Field2, //Trường text hiển thị
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "LABEL")
                        {
                            input = new V6LabelTextBox()
                            {
                                Name = "lbt" + defineInfo.Field,
                            };
                        }
                    }
                    else if (ObjectAndString.IsDateTimeType(defineInfo.DataType))
                    {
                        input = new V6DateTimeColor();
                    }
                    else if (ObjectAndString.IsNumberType(defineInfo.DataType))
                    {
                        input = new V6NumberTextBox();
                        var nT = (V6NumberTextBox)input;
                        //nT.DecimalPlaces = defineInfo.Decimals;
                        NumberTextBox_Decimals[nT] = defineInfo.Decimals;
                    }
                    else
                    {
                        input = new V6VvarTextBox()
                        {
                            VVar = defineInfo.Vvar,
                        };
                        var vV = (V6VvarTextBox)input;
                        if (defineInfo.ToUpper) vV.CharacterCasing = CharacterCasing.Upper;

                        var maxlength = 1;
                        if (!string.IsNullOrEmpty(defineInfo.LimitChars))
                        {
                            vV.LimitCharacters = defineInfo.LimitChars;
                            vV.MaxLength = maxlength;
                        }

                        var tT = (V6VvarTextBox)input;
                        tT.SetInitFilter(defineInfo.InitFilter);
                        tT.F2 = defineInfo.F2;
                    }
                    if (input != null)
                    {
                        if (!(input is V6NumberTextBox) && !(input is V6DateTimeColor) &&!(input is Button))
                        {
                            if (input is V6ColorTextBox)
                            V6ColorTextBox_MaxLength.Add((V6ColorTextBox) input, defineInfo.MaxLength);
                        }
                        
                        //Bao lại các thuộc tính nếu chưa có.
                        if (input.AccessibleName == null) input.AccessibleName = AccessibleName_KEY;
                        if (string.IsNullOrEmpty(input.Name)) input.Name = "txt" + defineInfo.Field;
                        if (!string.IsNullOrEmpty(defineInfo.DefaultValue))
                        {
                            SetControlValue(input, defineInfo.DefaultValue);
                            //input.Text = defineInfo.DefaultValue;
                        }
                        input.Visible = defineInfo.Visible;
                        input.Width = string.IsNullOrEmpty(defineInfo.Width)
                            ? 150
                            : ObjectAndString.ObjectToInt(defineInfo.Width);
                        input.Left = 150;
                        input.Top = top;

                        panel1.Controls.Add(input);
                        Input_Controls[defineInfo.Field.ToUpper()] = input;
                        All_Objects[input.Name] = input;
                        //Sự kiện của input
                        string DMETHOD = "" + row["DMETHOD"];
                        if (!string.IsNullOrEmpty(DMETHOD))
                        {
                            //Nhiều event
                            var xml = DMETHOD;
                            DataSet ds = new DataSet();
                            ds.ReadXml(new StringReader(xml));
                            if (ds.Tables.Count <= 0) break;

                            var data = ds.Tables[0];

                            foreach (DataRow event_row in data.Rows)
                            {
                                //nơi sử dụng: QuickReportManager, DynamicAddEditForm
                                try
                                {
                                    var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                                    var method_name = event_row["method"].ToString().Trim();

                                    if(data.Columns.Contains("using")) all_using_text += event_row["using"];
                                    all_method_text += event_row["content"];
                                    all_method_text += " ";

                                    //Make dynamic event and call
                                    switch (EVENT_NAME)
                                    {
                                        case "INIT":
                                            input.EnabledChanged += (s, e) =>
                                            {
                                                if (Event_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "TEXTCHANGE":
                                            input.TextChanged += (s, e) =>
                                            {
                                                if (Event_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "VALUECHANGE":
                                            V6NumberTextBox numInput = input as V6NumberTextBox;
                                            if (numInput == null) break;

                                            numInput.StringValueChange += (s, e) =>
                                            {
                                                if (Event_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "GOTFOCUS":
                                            input.GotFocus += (s, e) =>
                                            {
                                                if (Event_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "LOSTFOCUS":
                                            input.LostFocus += (s, e) =>
                                            {
                                                if (Event_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "V6LOSTFOCUS":
                                            var colorInput = input as V6ColorTextBox;
                                            if (colorInput == null) break;
                                            colorInput.V6LostFocus += (s) =>
                                            {
                                                if (Event_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = null;
                                                V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "KEYDOWN":
                                            input.KeyDown += (s, e) =>
                                            {
                                                if (Event_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "CLICK":
                                            input.Click += (s, e) =>
                                            {
                                                if (Event_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                            };
                                            break;
                                    }//end switch
                                }
                                catch (Exception exfor)
                                {
                                    this.WriteExLog(GetType() + ".CreateFormControls ReadEventInForLoop", exfor);
                                }
                            }//end for
                        }//end if DMETHOD

                        //Add brother
                        int left = input.Right + 10;
                        if (input is V6VvarTextBox && !string.IsNullOrEmpty(defineInfo.BField))
                        {
                            var tT = (V6VvarTextBox)input;
                            tT.BrotherFields = defineInfo.BField;
                            var txtB = new V6LabelTextBox();
                            txtB.Name = "txt" + defineInfo.BField;
                            txtB.AccessibleName = defineInfo.BField;
                            txtB.Top = top;
                            txtB.Left = left;
                            txtB.Width = panel1.Width - txtB.Left - 10;
                            txtB.ReadOnly = true;
                            txtB.TabStop = false;

                            All_Objects[txtB.Name] = txtB;
                            panel1.Controls.Add(txtB);
                            left = txtB.Right + 10;
                        }
                        //Add description
                        var description = defineInfo.DescriptionLang(V6Setting.IsVietnamese);
                        if (!string.IsNullOrEmpty(description))
                        {
                            var labelD = new V6Label();
                            labelD.Name = "lblD" + defineInfo.Field;
                            labelD.AutoSize = true;
                            labelD.Left = left;
                            labelD.Top = top;
                            labelD.Text = description;
                            panel1.Controls.Add(labelD);
                            All_Objects[labelD.Name] = labelD;
                        }
                    }
                    i++;
                }
                Event_program = V6ControlsHelper.CreateProgram("EventNameSpace", "EventClass", "D" + TableName, all_using_text, all_method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateFormControls", ex);
            }
        }

        public void ChooseColor(TextBox txtR, TextBox txtG, TextBox txtB, Label lblR, Label lblG, Label lblB)
        {
            int r = 0, g = 0, b = 0;
            int.TryParse(txtR.Text, out r);
            int.TryParse(txtG.Text, out g);
            int.TryParse(txtB.Text, out b);
            Color c = Color.FromArgb(r, g, b);
            ColorDialog d = new ColorDialog();
            d.Color = c;
            if (d.ShowDialog(this) == DialogResult.OK)
            {
                lblR.BackColor = d.Color;
                lblG.BackColor = d.Color;
                lblB.BackColor = d.Color;

                txtR.Text = "" + d.Color.R;
                txtG.Text = "" + d.Color.G;
                txtB.Text = "" + d.Color.B;
            }
        }

        private void CreateFormProgram()
        {
            try
            {
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_BC", TableName);
                AlreportData = V6BusinessHelper.Select(V6TableName.Alreport, keys, "*").Data;
                if (AlreportData.Rows.Count == 0) return;

                var dataRow = AlreportData.Rows[0];
                var xml = dataRow["MMETHOD"].ToString().Trim();
                if (xml == "") return;
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xml));
                if (ds.Tables.Count <= 0) return;
                            
                var data = ds.Tables[0];

                string using_text = "";
                string method_text = "";
                foreach (DataRow event_row in data.Rows)
                {
                    var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                    var method_name = event_row["method"].ToString().Trim();
                    Event_Methods[EVENT_NAME] = method_name;

                    using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                    method_text += event_row["content"];
                    method_text += "\n";
                }
                Form_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "M" + TableName, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
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
            FixMaxLengFormat();
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

        private void FixMaxLengFormat()
        {
            try
            {
                foreach (KeyValuePair<V6NumberTextBox, int> item in NumberTextBox_Decimals)
                {
                    var addLength = item.Value - item.Key.DecimalPlaces;
                    if (item.Key.DecimalPlaces == 0 && item.Value > 0) addLength++;
                    item.Key.DecimalPlaces = item.Value;
                    item.Key.MaxLength += addLength;
                }

                foreach (KeyValuePair<V6ColorTextBox, int> item in V6ColorTextBox_MaxLength)
                {
                    if(item.Value > 0) item.Key.MaxLength = item.Value;
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

        public override void AfterInsert()
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey("AFTERINSERT"))
                {
                    var method_name = Event_Methods["AFTERINSERT"];
                    V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke AFTERINSERT", ex1);
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
                this.ShowInfoMessage(ex.Message);
                this.WriteExLog(GetType() + ".UpdateData", ex);
                return 0;
            }
        }

        public override void AfterUpdate()
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey("AFTERUPDATE"))
                {
                    var method_name = Event_Methods["AFTERUPDATE"];
                    V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke AFTERUPDATE", ex1);
            }
        }

        protected override void GetNewID()
        {
            try
            {
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
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetNewID", ex);
            }
        }

        protected override void AddStt13()
        {
            try
            {
                var sql = "Update Aldm set Stt13=Stt13+1 where ma_dm=@ma_dm";
                SqlParameter[] plist = new[] { new SqlParameter("@ma_dm", TableName) };
                V6BusinessHelper.ExecuteSqlNoneQuery(sql, plist);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddStt13 override", ex);
            }
        }

        public override void DoBeforeCopy()
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey("DOBEFORECOPY"))
                {
                    var method_name = Event_Methods["DOBEFORECOPY"];
                    V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke DOBEFORECOPY", ex1);
            }
        }

        public override void DoBeforeAdd()
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey("DOBEFOREADD"))
                {
                    var method_name = Event_Methods["DOBEFOREADD"];
                    V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke DOBEFOREADD", ex1);
            }
        }

        public override void DoBeforeEdit()
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey("DOBEFOREEDIT"))
                {
                    var method_name = Event_Methods["DOBEFOREEDIT"];
                    V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke DOBEFOREEDIT", ex1);
            }
        }

        public override void DoBeforeView()
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey("DOBEFOREVIEW"))
                {
                    var method_name = Event_Methods["DOBEFOREVIEW"];
                    V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke DOBEFOREVIEW", ex1);
            }
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
                            errors += string.Format(V6Text.CheckInfor +"{0}: {1}\r\n", item.Key, item.Value.TextLang(V6Setting.IsVietnamese));
                        }
                    }
                    else
                    {
                        errors += string.Format(V6Text.CheckDeclare + "{0}: {1}\r\n", item.Key, item.Value.TextLang(V6Setting.IsVietnamese));
                    }
                }
            }

            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey("VALIDATEDATA"))
                {
                    var method_name = Event_Methods["VALIDATEDATA"];
                    errors += V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke VALIDATEDATA", ex1);
            }

            // check code
            //_dataRow;// aldm
            var GRD_COL = _dataRow["GRD_COL"].ToString().Trim().ToUpper();
            var KEY_LIST = ObjectAndString.SplitString(_dataRow["KEY"].ToString().Trim());
            string KEY1 = "", KEY2 = "", KEY3 = "", KEY4 = "";
            if (GRD_COL == "ONECODE" && KEY_LIST.Length > 0)
            {
                KEY1 = KEY_LIST[0].Trim().ToUpper();
                if (Mode == V6Mode.Edit)
                {
                    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName, 0, KEY1,
                     DataDic[KEY1].ToString(), DataOld[KEY1].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied+" {0} = {1}", KEY1, DataDic[KEY1]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName, 1, KEY1,
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
                    bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName, 0,
                        KEY1, DataDic[KEY1].ToString(), DataOld[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataOld[KEY2].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied + " {0},{1} = {2},{3}",
                            KEY1, KEY2, DataDic[KEY1], DataDic[KEY2]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName, 1,
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
                    bool b = V6BusinessHelper.IsValidThreeCode(TableName, 0,
                        KEY1, DataDic[KEY1].ToString(), DataOld[KEY1].ToString(),
                        KEY2, DataDic[KEY2].ToString(), DataOld[KEY2].ToString(),
                        KEY3, DataDic[KEY3].ToString(), DataOld[KEY3].ToString());
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied + " {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidThreeCode(TableName, 1,
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
                    bool b = V6BusinessHelper.IsValidTwoCode_OneDate(TableName, 0,
                        KEY1, DataDic[KEY1].ToString(), ObjectAndString.ObjectToString(DataOld[KEY1]),
                        KEY2, DataDic[KEY2].ToString(), ObjectAndString.ObjectToString(DataOld[KEY2]),
                        KEY3, ObjectAndString.ObjectToString(DataDic[KEY3], "yyyyMMdd"), ObjectAndString.ObjectToString(DataOld[KEY3], "yyyyMMdd"));
                    if (!b)
                        throw new Exception(string.Format(V6Text.EditDenied + " {0},{1},{2} = {3},{4},{5}",
                            KEY1, KEY2, KEY3, DataDic[KEY1], DataDic[KEY2], DataDic[KEY3]));
                }
                else if (Mode == V6Mode.Add)
                {
                    bool b = V6BusinessHelper.IsValidTwoCode_OneDate(TableName, 1,
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
                    bool b = V6BusinessHelper.IsValidThreeCode_OneDate(TableName, 0,
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
                    bool b = V6BusinessHelper.IsValidThreeCode_OneDate(TableName, 1,
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
