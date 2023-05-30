﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class FormEditDataDynamic : V6Form
    {
        public FormEditDataDynamic()
        {
            InitializeComponent();
        }

        private new string _MA_DM;//Đè kiểu cũ. Các hàm cũ đã override.
        public AldmConfig _aldmConfig;
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
        
        IDictionary<string, object> DataOld;
        public IDictionary<string, object> Data = new SortedDictionary<string, object>();

        public FormEditDataDynamic(string ma_dm, IDictionary<string, object> dataOld)
        {
            _MA_DM = ma_dm;
            //_aldmConfig = aldmConfig;
            _aldmConfig = ConfigManager.GetAldmConfig(_MA_DM);
            DataOld = dataOld;
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

            V6ControlFormHelper.SetFormDataDictionary(this, DataOld);

            Text = V6Login.SelectedLanguage == "V" ? "Thay đổi giá trị các tham số" : "Change parameters value.";

            Disposed += FormEditDataDynamic_Disposed;
        }

        void FormEditDataDynamic_Disposed(object sender, EventArgs e)
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
                Alreport1Data = V6BusinessHelper.Select("Alreport1", keys, "*", "", "Stt_Filter").Data;
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
                            
                            input = new V6VvarTextBox()
                            {
                                Name = "txt" + defineInfo.Field,
                                AccessibleName = defineInfo.AccessibleName,
                                VVar = defineInfo.Vvar,
                                CheckOnLeave = defineInfo.NotEmpty,
                                CheckNotEmpty = defineInfo.NotEmpty,
                                F2 = defineInfo.F2,
                                FilterStart = defineInfo.FilterStart,
                            };
                            
                            var vvar_input = (V6VvarTextBox)input;
                            if (defineInfo.ToUpper) vvar_input.CharacterCasing = CharacterCasing.Upper;
                            var maxlength = 1;
                            if (!string.IsNullOrEmpty(defineInfo.LimitChars))
                            {
                                vvar_input.LimitCharacters = defineInfo.LimitChars;
                                vvar_input.MaxLength = maxlength;
                            }

                            vvar_input.SetInitFilter(defineInfo.InitFilter);
                            vvar_input.F2 = defineInfo.F2;
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
                            F2 = defineInfo.F2,
                            FilterStart = defineInfo.FilterStart,
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
                        if (defineInfo.Readonly) input.ReadOnlyTag();
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
                                //nơi sử dụng: QuickReportManager, FormEditDataDynamic
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
                AlreportData = V6BusinessHelper.Select("Alreport", keys, "*").Data;
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
        
        
        /// <summary>
        /// Chạy ExistRowInTable cho các V6VvarTextBox.
        /// </summary>
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            try
            {
                Data = GetData();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".btnNhan_Click", ex);
            }
        }

        private void FormEditDataDynamic_Load(object sender, EventArgs e)
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
        
    }
}