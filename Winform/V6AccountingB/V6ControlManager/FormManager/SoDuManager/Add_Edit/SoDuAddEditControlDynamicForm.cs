using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class SoDuAddEditControlDynamicForm : SoDuAddEditControlVirtual
    {
        public SoDuAddEditControlDynamicForm()
        {
            InitializeComponent();
        }

        private new string _MA_DM;//Đè kiểu cũ. Các hàm cũ đã override.
        //private new AldmConfig _aldmConfig;
        /// <summary>
        /// Bật tắt tính năng gọi hàm Reload sau khi insert hoặc update thành công.
        /// </summary>
        public bool ReloadFlag; // !!!!!
        /// <summary>
        /// Gán trường mã của Table trong AddEditControl.MyInit để chạy CopyData_Here2Data
        /// </summary>
        public string KeyField1 = "";
        public string KeyField2 = "";
        public string KeyField3 = "";

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
        //private Dictionary<string, string> Event_Methods = new Dictionary<string, string>(); 
        
        //private Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        private Dictionary<string, Label> Label_Controls = new Dictionary<string, Label>();
        private Dictionary<string, Control> Input_Controls = new Dictionary<string, Control>(); 

        public SoDuAddEditControlDynamicForm(string ma_dm, AldmConfig aldmConfig)
        {
            _MA_DM = ma_dm;
            _aldmConfig = aldmConfig;
            InitializeComponent();
            MyInit();
        }

        private Type Form_program;
        //private Type Event_program;
        private string all_using_text = "", all_method_text = "";
        private void MyInit()
        {
            CreateFormProgram();
            CreateFormControls();
            EnableFormControls_Alctct(_MA_DM);

            //try // Dynamic invoke
            //{
            //    if (Event_Methods.ContainsKey(FormDynamicEvent.INIT))
            //    {
            //        var method_name = Event_Methods[FormDynamicEvent.INIT];
            //        V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
            //    }
            //}
            //catch (Exception ex1)
            //{
            //    this.WriteExLog(GetType() + ".Dynamic invoke INIT", ex1);
            //}
            Disposed += SoDuAddEditControlDynamicAddEditForm_Disposed;
        }

        void SoDuAddEditControlDynamicAddEditForm_Disposed(object sender, EventArgs e)
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
                keys.Add("MA_BC", _MA_DM);
                Alreport1Data = V6BusinessHelper.Select(V6TableName.Alreport1, keys, "*", "", "Stt_Filter").Data;
                //int i_index = 0;
                int baseTop = 10;
                int rowHeight = 25;
                Dictionary<string, TabPage> tabList = new Dictionary<string, TabPage>();
                Dictionary<string, int> indexList = new Dictionary<string, int>();
                tabList[""] = tabThongTinChinh;
                indexList[""] = 0;
                foreach (DataRow row in Alreport1Data.Rows)
                {
                    var define = row["Filter"].ToString().Trim();
                    var key4 = row["KEY4"].ToString().Trim();
                    string KEY4 = "";
                    if (key4 != "")
                    {
                        string[] sss = ObjectAndString.SplitString(key4);
                        KEY4 = sss[0].Trim().ToUpper();
                     
                        if (!tabList.ContainsKey(KEY4))
                        {
                            string key4CapV = KEY4;
                            string key4CapE = KEY4;
                            if (sss.Length > 1) key4CapV = sss[1];
                            if (sss.Length > 2) key4CapE = sss[2];
                            TabPage key4Tab = new TabPage(V6Setting.IsVietnamese ? key4CapV : key4CapE);
                            key4Tab.Name = "tab" + KEY4;
                            tabList[KEY4] = key4Tab;
                            v6TabControl1.TabPages.Add(key4Tab);
                            indexList[KEY4] = 0;
                        }
                    }
                    var defineInfo = new DefineInfo(define);
                    var AccessibleName_KEY = string.IsNullOrEmpty(defineInfo.AccessibleName)
                        ? defineInfo.Field.ToUpper()
                        : defineInfo.AccessibleName.ToUpper();
                    DefineInfo_Data[AccessibleName_KEY.ToUpper()] = defineInfo;
                    //Label
                    var top = baseTop + indexList[KEY4] * rowHeight;

                    var label = new V6Label();
                    label.Name = "lbl" + defineInfo.Field;
                    label.AutoSize = true;
                    label.Left = 10;
                    label.Top = top;
                    label.Text = defineInfo.TextLang(V6Setting.IsVietnamese);
                    label.Visible = defineInfo.Visible;
                    tabList[KEY4].Controls.Add(label);
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
                        //Tạo control theo loại.
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
                        else if (defineInfo.ControlType.ToUpper() == "DATETIME")
                        {
                            input = new V6DateTimePicker()
                            {
                                Name = "date" + defineInfo.Field,
                                AccessibleName = defineInfo.AccessibleName,
                                
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "DATETIMECOLOR")
                        {
                            input = new V6DateTimeColor()
                            {
                                Name = "date" + defineInfo.Field,
                                AccessibleName = defineInfo.AccessibleName,
                                
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "FILEBUTTON")
                        {
                            input = new FileButton()
                            {
                                Name = "fbt" + defineInfo.Field,
                                AccessibleName = defineInfo.AccessibleName,
                                Text = defineInfo.TextLang(V6Setting.IsVietnamese),
                                UseVisualStyleBackColor = true,
                                Height = 25,
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "VVARTEXTBOX" || defineInfo.ControlType.ToUpper() == "V6VVARTEXTBOX")
                        {
                            DoNothing();
                            input = new V6VvarTextBox()
                            {
                                Name = "txt" + defineInfo.Field,
                                AccessibleName = defineInfo.AccessibleName,
                                VVar = defineInfo.Vvar,
                                CheckOnLeave = defineInfo.NotEmpty,
                                CheckNotEmpty = defineInfo.NotEmpty,
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
                                CheckOnLeave = true,
                                CheckNotEmpty = defineInfo.NotEmpty,
                                UseChangeTextOnSetFormData = defineInfo.UseChangeText,
                                UseLimitCharacters0 = defineInfo.UseLimitChars0,
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "V6LOOKUPPROC")
                        {
                            input = new V6LookupProc()
                            {
                                MA_CT = _MA_DM,// defineInfo.ma ma_bc,
                                Name = "txt" + defineInfo.Field,
                                Ma_dm = defineInfo.MA_DM, //Mã danh mục trong Aldm
                                AccessibleName = defineInfo.AccessibleName, //Trường get dữ liệu
                                AccessibleName2 = defineInfo.AccessibleName2, //Trường get text hiển thị
                                ValueField = defineInfo.Field, //Trường dữ liệu
                                ShowTextField = defineInfo.Field2, //Trường text hiển thị
                                CheckOnLeave = true,
                                CheckNotEmpty = defineInfo.NotEmpty,
                                F2 = defineInfo.F2,
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "NUMBERYEAR")
                        {
                            input = new NumberYear();
                        }
                        else if (defineInfo.ControlType.ToUpper() == "NUMBERMONTH")
                        {
                            input = new NumberMonth();
                        }
                        else if (defineInfo.ControlType.ToUpper() == "NUMBER" || defineInfo.ControlType.ToUpper() == "V6NUMBERTEXTBOX")
                        {
                            input = new V6NumberTextBox();
                            var nT = (V6NumberTextBox)input;
                            //nT.DecimalPlaces = defineInfo.Decimals;
                            NumberTextBox_Decimals[nT] = defineInfo.Decimals;
                        }
                        else if (defineInfo.ControlType.ToUpper() == "RICHTEXTBOX")
                        {
                            input = new RichTextBox()
                            {
                                Name = "rxt" + defineInfo.Field,
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "LABEL")
                        {
                            input = new V6LabelTextBox()
                            {
                                Name = "lbt" + defineInfo.Field,
                            };
                        }
                        else if (defineInfo.ControlType.ToUpper() == "CHECKBOX")
                        {
                            input = new V6CheckBox()
                            {
                                Name = "chk" + defineInfo.Field
                            };
                        }
                        else
                        {
                            goto Next_1;
                        }

                        goto EndIf_1;
                    }
                Next_1:
                    if (ObjectAndString.IsDateTimeType(defineInfo.DataType))
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
                            CheckOnLeave = defineInfo.NotEmpty,
                            CheckNotEmpty = defineInfo.NotEmpty,
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
                EndIf_1:

                    if (input != null)
                    {
                        //Thêm một số thuộc tính khác.
                        if (!(input is V6NumberTextBox) && !(input is V6DateTimeColor) &&!(input is Button))
                        {
                            if (input is V6ColorTextBox)
                            V6ColorTextBox_MaxLength.Add((V6ColorTextBox) input, defineInfo.MaxLength);
                        }
                        if (input is V6ColorTextBox)
                        {
                            if (defineInfo.UseLimitChars0)
                            {
                                var tb = input as V6ColorTextBox;
                                tb.Multiline = defineInfo.MultiLine;
                                tb.UseChangeTextOnSetFormData = defineInfo.UseChangeText;
                                tb.UseLimitCharacters0 = defineInfo.UseLimitChars0;
                                tb.LimitCharacters0 = defineInfo.LimitChars0;
                            }
                        }
                        
                        //Bao lại các thuộc tính nếu chưa có.
                        if (input.AccessibleName == null) input.AccessibleName = AccessibleName_KEY;
                        if (string.IsNullOrEmpty(input.Name)) input.Name = "txt" + defineInfo.Field;
                        if (!string.IsNullOrEmpty(defineInfo.DefaultValue))
                        {
                            object defaultValue = V6ControlFormHelper.GetDefaultSystemValue(defineInfo.DefaultValue);
                            SetControlValue(input, defaultValue);
                        }
                        input.Enabled = defineInfo.Enabled;
                        input.Visible = defineInfo.Visible;
                        input.Width = string.IsNullOrEmpty(defineInfo.Width)
                            ? 150
                            : ObjectAndString.ObjectToInt(defineInfo.Width);
                        input.Left = 150;
                        input.Top = top;
                        if (defineInfo.MultiLine || (defineInfo.ControlType + "").ToUpper() == "RICHTEXTBOX")
                        {
                            input.Height = rowHeight*2;
                            indexList[KEY4]++;
                        }

                        tabList[KEY4].Controls.Add(input);
                        Input_Controls[defineInfo.Field.ToUpper()] = input;
                        All_Objects[input.Name] = input;
                        All_Objects[defineInfo.Field.ToUpper()] = input;
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
                                //nơi sử dụng: QuickReportManager, SoDuAddEditControlDynamicAddEditForm
                                try
                                {
                                    var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                                    var method_name = event_row["method"].ToString().Trim();

                                    all_using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                                    all_method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";

                                    //Make dynamic event and call
                                    switch (EVENT_NAME)
                                    {
                                        case "INIT":
                                            input.EnabledChanged += (s, e) =>
                                            {
                                                if (Form_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["e"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "TEXTCHANGE":
                                        case "TEXTCHANGED":
                                            input.TextChanged += (s, e) =>
                                            {
                                                if (Form_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["e"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "VALUECHANGE":
                                        case "VALUECHANGED":
                                            V6NumberTextBox numInput = input as V6NumberTextBox;
                                            if (numInput == null) break;

                                            numInput.StringValueChange += (s, e) =>
                                            {
                                                if (Form_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["e"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "GOTFOCUS":
                                        case "ENTER":
                                            input.Enter += (s, e) =>
                                            {
                                                if (Form_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["e"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "LOSTFOCUS":
                                        case "LEAVE":
                                            input.Leave += (s, e) =>
                                            {
                                                if (Form_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["e"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "V6LOSTFOCUS":
                                            var colorInput = input as V6ColorTextBox;
                                            if (colorInput == null) break;
                                            colorInput.V6LostFocus += (s) =>
                                            {
                                                if (Form_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["eventargs"] = null;
                                                V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "KEYDOWN":
                                            input.KeyDown += (s, e) =>
                                            {
                                                if (Form_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["e"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                                            };
                                            break;

                                        case "CLICK":
                                            input.Click += (s, e) =>
                                            {
                                                if (Form_program == null) return;

                                                All_Objects["sender"] = s;
                                                All_Objects["e"] = e;
                                                V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
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
                            tT.BrotherFields2 = defineInfo.BField2;
                            tT.NeighborFields = defineInfo.NField;
                            if(!string.IsNullOrEmpty(defineInfo.ShowName)) tT.ShowName = defineInfo.ShowName  == "1";
                                                        
                            var txtB = new V6LabelTextBox();
                            txtB.Name = "txt" + defineInfo.BField;
                            txtB.AccessibleName = defineInfo.BField;
                            txtB.Top = top;
                            txtB.Left = left;
                            txtB.Width = tabList[KEY4].Width - txtB.Left - 10;
                            txtB.ReadOnly = true;
                            txtB.TabStop = false;
                            txtB.AddTagString("cancelset");

                            All_Objects[txtB.Name] = txtB;
                            tabList[KEY4].Controls.Add(txtB);
                            left = txtB.Right + 10;
                        }
                        if (input is V6LookupTextBox && !string.IsNullOrEmpty(defineInfo.BField))
                        {
                            var tT = (V6LookupTextBox)input;
                            tT.BrotherFields = defineInfo.BField;
                            tT.BrotherFields2 = defineInfo.BField2;
                            tT.NeighborFields = defineInfo.NField;
                            //tT.CheckOnLeave = defineInfo.NotEmpty;
                            //tT.CheckNotEmpty = defineInfo.NotEmpty;
                            var txtB = new V6LabelTextBox();
                            txtB.Name = "txt" + defineInfo.BField;
                            txtB.AccessibleName = defineInfo.BField;
                            txtB.Top = top;
                            txtB.Left = left;
                            txtB.Width = tabList[KEY4].Width - txtB.Left - 10;
                            txtB.ReadOnly = true;
                            txtB.TabStop = false;
                            txtB.AddTagString("cancelset");

                            All_Objects[txtB.Name] = txtB;
                            tabList[KEY4].Controls.Add(txtB);
                            left = txtB.Right + 10;
                        }
                        if (input is V6LookupProc && !string.IsNullOrEmpty(defineInfo.BField))
                        {
                            var tT = (V6LookupProc)input;
                            tT.BrotherFields = defineInfo.BField;
                            tT.BrotherFields2 = defineInfo.BField2;
                            tT.NeighborFields = defineInfo.NField;
                            if (!string.IsNullOrEmpty(defineInfo.ShowName)) tT.ShowName = defineInfo.ShowName == "1";
                            //tT.CheckOnLeave = defineInfo.NotEmpty;
                            //tT.CheckNotEmpty = defineInfo.NotEmpty;

                            var txtB = new V6LabelTextBox();
                            txtB.Name = "txt" + defineInfo.BField;
                            txtB.AccessibleName = defineInfo.BField;
                            txtB.Top = top;
                            txtB.Left = left;
                            txtB.Width = tabList[KEY4].Width - txtB.Left - 10;
                            txtB.ReadOnly = true;
                            txtB.TabStop = false;
                            txtB.AddTagString("cancelset");

                            All_Objects[txtB.Name] = txtB;
                            tabList[KEY4].Controls.Add(txtB);
                            left = txtB.Right + 10;
                        }
                        if (input is V6LookupData && !string.IsNullOrEmpty(defineInfo.BField))
                        {
                            var tT = (V6LookupData)input;
                            tT.BrotherFields = defineInfo.BField;
                            tT.BrotherFields2 = defineInfo.BField2;
                            tT.NeighborFields = defineInfo.NField;
                            if (!string.IsNullOrEmpty(defineInfo.ShowName)) tT.ShowName = defineInfo.ShowName == "1";
                            //tT.CheckOnLeave = defineInfo.NotEmpty;
                            //tT.CheckNotEmpty = defineInfo.NotEmpty;

                            var txtB = new V6LabelTextBox();
                            txtB.Name = "txt" + defineInfo.BField;
                            txtB.AccessibleName = defineInfo.BField;
                            txtB.Top = top;
                            txtB.Left = left;
                            txtB.Width = tabList[KEY4].Width - txtB.Left - 10;
                            txtB.ReadOnly = true;
                            txtB.TabStop = false;
                            txtB.AddTagString("cancelset");

                            All_Objects[txtB.Name] = txtB;
                            tabList[KEY4].Controls.Add(txtB);
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
                            tabList[KEY4].Controls.Add(labelD);
                            All_Objects[labelD.Name] = labelD;
                        }
                    }
                indexList[KEY4]++;
                }
                Form_program = V6ControlsHelper.CreateProgram("EventNameSpace", "EventClass", "D" + _MA_DM, all_using_text, all_method_text);
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
                keys.Add("MA_BC", _MA_DM);
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
                    method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                }
                Event_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "M" + _MA_DM, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
            }
        }


        /// <summary>
        /// Khởi tạo giá trị cho control với các tham số
        /// </summary>
        /// <param name="ma_dm">Bảng đang xử lý</param>
        /// <param name="mode">Add/Edit/View</param>
        /// <param name="keys">Nếu data null thì load bằng keys</param>
        /// <param name="data">Gán dữ liệu này lên form</param>
        public override void InitValues(string ma_dm, V6Mode mode,
            IDictionary<string, object> keys, IDictionary<string, object> data)
        {
            //TableName = tableName;
            Mode = mode;
            _keys = keys;
            DataOld = data;
            if (Mode == V6Mode.View) V6ControlFormHelper.SetFormControlsReadOnly(this, true);
            
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
            InvokeFormEvent(FormDynamicEvent.INIT);
            LoadAll();
            //virtual
            LoadDetails();

            LoadTag(2, "", ma_dm, ItemID);
        }

        public override void LoadAll()
        {
            LoadStruct();//MaxLength...
            FixMaxLengFormat();
            V6ControlFormHelper.LoadAndSetFormInfoDefine(_MA_DM, this, Parent);

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
                    else LoadDefaultData(2, "", _MA_DM, m_itemId);
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

        public override void LoadStruct()
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

        public override void LoadData()
        {
            try
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.LOADDATA))
                {
                    All_Objects["ParentData"] = ParentData;
                    All_Objects["DataOld"] = DataOld;
                    var method_name = Event_Methods[FormDynamicEvent.LOADDATA];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
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

        public override void FixFormData()
        {
            try
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.FIXFORMDATA))
                {
                    All_Objects["ParentData"] = ParentData;
                    All_Objects["DataOld"] = DataOld;
                    var method_name = Event_Methods[FormDynamicEvent.FIXFORMDATA];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixFormData", ex);
            }
        }

        public override bool DoInsertOrUpdate(bool showMessage = true)
        {
            ReloadFlag = false;

            try
            {
                FixFormData();
                DataDic = GetData();
                ValidateData();
                string checkV6Valid = CheckV6Valid(DataDic, _MA_DM);
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

            if (Mode == V6Mode.Edit)
            {
                try
                {
                    int b = UpdateData();
                    if (b > 0)
                    {
                        AfterSave();
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
            else if (Mode == V6Mode.Add)
            {
                try
                {
                    bool b = InsertNew();
                    if (b)
                    {
                        AfterSave();
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

        public override bool InsertNew()
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
                this.ShowInfoMessage(ex.Message);
                this.WriteExLog(GetType() + ".InsertNew", ex);
                return false;
            }
        }

        public override void AfterSave()
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

        public override void AfterInsert()
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.AFTERINSERT))
                {
                    var method_name = Event_Methods[FormDynamicEvent.AFTERINSERT];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
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
                //Lấy thêm UID từ DataEditNếu có.
                if (DataOld.ContainsKey("UID"))
                {
                    _keys["UID"] = DataOld["UID"];
                }
                var result = Categories.Update(CONFIG_TABLE_NAME, DataDic, _keys);
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
                if (Event_Methods.ContainsKey(FormDynamicEvent.AFTERUPDATE))
                {
                    var method_name = Event_Methods[FormDynamicEvent.AFTERUPDATE];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
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
                SqlParameter[] plist = new[] { new SqlParameter("@ma_dm", _MA_DM) };
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
                if (Event_Methods.ContainsKey(FormDynamicEvent.DOBEFORECOPY))
                {
                    var method_name = Event_Methods[FormDynamicEvent.DOBEFORECOPY];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
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
                if (Event_Methods.ContainsKey(FormDynamicEvent.DOBEFOREADD))
                {
                    var method_name = Event_Methods[FormDynamicEvent.DOBEFOREADD];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
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
                CheckPhatSinh();

                if (Event_Methods.ContainsKey(FormDynamicEvent.DOBEFOREEDIT))
                {
                    var method_name = Event_Methods[FormDynamicEvent.DOBEFOREEDIT];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
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
                if (Event_Methods.ContainsKey(FormDynamicEvent.DOBEFOREVIEW))
                {
                    var method_name = Event_Methods[FormDynamicEvent.DOBEFOREVIEW];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
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
                        if ((DataDic[item.Key]??"").ToString().Trim() == "")
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
                        throw new Exception(string.Format(V6Text.EditDenied+" {0} = {1}", KEY1, DataDic[KEY1]));
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

        private void CheckVvarTextBox()
        {
            try
            {
                foreach (TabPage tabPage in v6TabControl1.TabPages)
                {
                    foreach (Control control in tabPage.Controls)
                    {
                        var vT = control as V6VvarTextBox;
                        if (vT != null && !string.IsNullOrEmpty(vT.VVar))
                        {
                            vT.ExistRowInTable();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CheckVvarTextBox", ex);
            }
        }

        private void SoDuAddEditControlDynamicAddEditForm_Load(object sender, EventArgs e)
        {
            CheckVvarTextBox();
            
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.INIT2))
                {
                    var method_name = Event_Methods[FormDynamicEvent.INIT2];
                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke INIT2", ex1);
            }
        }

        private void CheckPhatSinh()
        {
            try
            {
                AldmConfig v6lookup_config = ConfigManager.GetAldmConfigByTableName(_MA_DM);
                var id = v6lookup_config.TABLE_KEY;
                Control c = GetControlByAccessibleName(id);
                string value = (V6ControlFormHelper.GetControlValue(c)??"").ToString().Trim();
                var id_check = v6lookup_config.DOI_MA;
                var listTable = v6lookup_config.F8_TABLE;

                if (!string.IsNullOrEmpty(listTable) && !string.IsNullOrEmpty(id) && c!= null && value != "")
                {
                    var v = Categories.IsExistOneCode_List(listTable, id_check, value);
                    if (v) // Đã có phát sinh
                    {
                        V6ControlFormHelper.SetControlReadOnly(c, true);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CheckPhatSinh", ex);
            }
        }
    }
}
