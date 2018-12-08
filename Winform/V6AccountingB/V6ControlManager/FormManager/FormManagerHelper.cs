using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.HeThong.QuanLyHeThong.NgonNgu;
using V6ControlManager.FormManager.MenuManager;
using V6ControlManager.FormManager.ReportManager.DanhMuc;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager
{
    public static class FormManagerHelper
    {
        public static Menu3Control CurrentMenu3Control { get; set; }

        public static void HideMainMenu()
        {
            V6ControlFormHelper.HideMainMenu();
        }
        public static void ShowMainMenu()
        {
            V6ControlFormHelper.ShowMainMenu();
        }
        public static void HideCurrentMenu3Menu()
        {
            if(CurrentMenu3Control!=null)
                CurrentMenu3Control.HideMenu();
        }
        public static void ShowCurrentMenu3Menu()
        {
            if (CurrentMenu3Control != null)
                CurrentMenu3Control.ShowMenu();
        }

        public static void ShowDanhMucPrint(IWin32Window owner, string tableName, string reportFile, string reportTitle, string reportTitle2, bool dialog = true)
        {
            var f = new DanhMucReportForm(tableName, reportFile, reportTitle, reportTitle2);
            if(dialog)
                f.ShowDialog(owner);
            else
                f.Show();
        }

        public static V6FormControl GetGeneralControl(string code, string itemId)
        {
            switch (code)
            {
                case "CORPLAN":
                    return new CorplanContainer(itemId);
                case "V6TOOLS":
                    return new ToolManager.AllTool(itemId);
            }

            return new V6UserControlEmpty("GeneralControl" + code);
        }


        /// <summary>
        /// Tạo các control và sự kiện [động] của nó theo các thông tin định nghĩa [Alreport1].
        /// <para>Trong form chính cần có panel PanelAdvance hoặc v6TabControl1.</para>
        /// </summary>
        /// <param name="thisForm">Form chính.</param>
        /// <param name="ma_bc">TableName đối với danh mục, Ma_ct đối với chứng từ.</param>
        /// <param name="All_Objects">Các đối tượng dùng làm tham số trong các hàm động.</param>
        public static void CreateAdvanceFormControls(Control thisForm, string ma_bc, Dictionary<string, object> All_Objects)
        {
            Type Event_program2 = thisForm.GetType();
            DataTable Alreport1Data = null;
            Dictionary<string, DefineInfo> DefineInfo_Data = new Dictionary<string, DefineInfo>();
            Dictionary<string, Label> Label_Controls = new Dictionary<string, Label>();
            Dictionary<V6NumberTextBox, int> NumberTextBox_Decimals = new Dictionary<V6NumberTextBox, int>();
            Dictionary<V6ColorTextBox, int> V6ColorTextBox_MaxLength = new Dictionary<V6ColorTextBox, int>();
            Dictionary<string, Control> Input_Controls = new Dictionary<string, Control>();
            string using_text2 = "", method_text2 = "";

            Panel panel1 = null; //Phải get trên form theo tên nào đó. AdvanceControlsPanel
            panel1 = V6ControlFormHelper.GetControlByName(thisForm, "PanelAdvance") as Panel;
            if (panel1 == null)
            {
                //Tạo tab Advance nếu có tabControl1
                TabControl tabControl1 = V6ControlFormHelper.GetControlByName(thisForm, "v6TabControl1") as TabControl;
                if (tabControl1 == null) tabControl1 = V6ControlFormHelper.GetControlByName(thisForm, "tabControl1") as TabControl;
                if (tabControl1 != null)
                {
                    TabPage advanceTabPage = new TabPage("Advance");
                    advanceTabPage.BackColor = Color.FromArgb(246, 243, 226);
                    tabControl1.TabPages.Add(advanceTabPage);
                    panel1 = new Panel();
                    panel1.Name = "PanelAdvance";
                    panel1.Dock = DockStyle.Fill;
                    advanceTabPage.Controls.Add(panel1);
                }
            }

            if (panel1 == null) return;

            //Đưa form chính vào listObj
            All_Objects["thisForm"] = thisForm;
            //Tạo control động
            IDictionary<string, object> keys = new Dictionary<string, object>();
            keys.Add("MA_BC", ma_bc);
            Alreport1Data = V6BusinessHelper.Select(V6TableName.Alreport1, keys, "*", "", "Stt_Filter").Data;
            int i = 0;
            int baseTop = 10;
            int rowHeight = 25;
            foreach (DataRow row in Alreport1Data.Rows)
            {
                var define = row["Filter"].ToString().Trim();
                var define_M = row["Filter_M"].ToString().Trim();
                var defineInfo = new DefineInfo(define);
                var defineInfo_M = new DefineInfo(define_M);
                var AccessibleName_KEY = string.IsNullOrEmpty(defineInfo.AccessibleName)
                    ? defineInfo.Field.ToUpper()
                    : defineInfo.AccessibleName.ToUpper();
                //Bỏ qua nếu đã tồn tại control trên form.
                if (V6ControlFormHelper.GetControlByAccessibleName(thisForm, AccessibleName_KEY) != null) continue;

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
                    else if (defineInfo.ControlType.ToUpper() == "FILEBUTTON")
                    {
                        input = new FileButton()
                        {
                            Name = "fbt" + defineInfo.Field,
                            AccessibleName = defineInfo.AccessibleName,
                            Text = defineInfo.TextLang(V6Setting.IsVietnamese),
                            UseVisualStyleBackColor = true
                        };
                    }
                    else if (defineInfo.ControlType.ToUpper() == "V6VVARTEXTBOX")
                    {
                        input = new V6VvarTextBox()
                        {
                            VVar = defineInfo.Vvar,
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
                            F2 = defineInfo.F2,
                        };
                    }
                    else if (defineInfo.ControlType.ToUpper() == "V6LOOKUPPROC")
                    {
                        input = new V6LookupProc()
                        {
                            MA_CT = ma_bc,
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
                    if (!(input is V6NumberTextBox) && !(input is V6DateTimeColor) && !(input is Button))
                    {
                        if (input is V6ColorTextBox)
                            V6ColorTextBox_MaxLength.Add((V6ColorTextBox)input, defineInfo.MaxLength);
                    }
                    if (input is V6ColorTextBox)
                    {
                        if (defineInfo.UseLimitChars0)
                        {
                            var tb = input as V6ColorTextBox;
                            tb.UseLimitCharacters0 = defineInfo.UseLimitChars0;
                            tb.LimitCharacters0 = defineInfo.LimitChars0;
                        }
                    }

                    //Bao lại các thuộc tính nếu chưa có.
                    if (input.AccessibleName == null) input.AccessibleName = AccessibleName_KEY;
                    if (string.IsNullOrEmpty(input.Name)) input.Name = "txt" + defineInfo.Field;
                    if (!string.IsNullOrEmpty(defineInfo.DefaultValue))
                    {
                        V6ControlFormHelper.SetControlValue(input, defineInfo.DefaultValue);
                        //input.Text = defineInfo.DefaultValue;
                    }
                    input.Enabled = defineInfo.Enabled;
                    input.Visible = defineInfo.Visible;
                    input.Width = string.IsNullOrEmpty(defineInfo.Width)
                        ? 150
                        : ObjectAndString.ObjectToInt(defineInfo.Width);
                    input.Left = 150;
                    input.Top = top;

                    panel1.Controls.Add(input);
                    Input_Controls[defineInfo.Field.ToUpper()] = input;
                    All_Objects[input.Name] = input;
                    //Lookup button
                    if (defineInfo_M.Visible)
                    {
                        LookupButton lButton = new LookupButton();
                        panel1.Controls.Add(lButton);
                        lButton.ReferenceControl = input;

                        lButton.Name = "lbt" + defineInfo.Field;

                        lButton.R_DataType = defineInfo_M.R_DataType;
                        //lButton.R_Value = defineInfo_M.R_Value;
                        //lButton.R_Vvar = defineInfo_M.R_Vvar;
                        //lButton.R_Stt_rec = defineInfo_M.R_Stt_rec;
                        lButton.R_Ma_ct = defineInfo_M.R_Ma_ct;

                        lButton.M_DataType = defineInfo_M.M_DataType;
                        lButton.M_Value = defineInfo_M.M_Value;
                        lButton.M_Vvar = defineInfo_M.M_Vvar;
                        lButton.M_Stt_Rec = defineInfo_M.M_Stt_Rec;
                        lButton.M_Ma_ct = defineInfo_M.M_Ma_ct;

                        lButton.M_Type = defineInfo_M.M_Type;
                        //lButton.M_User_id = defineInfo_M.M_User_id;
                        //lButton.M_Lan = defineInfo_M.V6Login.SelectedLanguage;

                        lButton.Visible = defineInfo_M.Visible;

                        lButton.LookupButtonF3Event += (sender, e) =>
                        {
                            string title = "Chứng từ " + e.MaCt;
                            var alct = V6BusinessHelper.Select("Alct", "*", "ma_ct=@mact", "", "", new SqlParameter("@mact", e.MaCt)).Data;
                            if (alct != null && alct.Rows.Count == 1)
                            {
                                title = alct.Rows[0][V6Setting.IsVietnamese?"Ten_ct":"Ten_ct2"].ToString();
                            }
                            var hoaDonForm = ChungTuF3.GetChungTuControl(e.MaCt, "Name", e.Stt_rec);
                            
                            hoaDonForm.ShowToForm(lButton, title, true, true, true);
                        };
                    }

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

                                using_text2 += data.Columns.Contains("using") ? event_row["using"] : "";
                                method_text2 += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";

                                //Make dynamic event and call
                                switch (EVENT_NAME)
                                {
                                    case "INIT":
                                        input.EnabledChanged += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "TEXTCHANGE":
                                    case "TEXTCHANGED":
                                        input.TextChanged += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "VALUECHANGE":
                                    case "VALUECHANGED":
                                        V6NumberTextBox numInput = input as V6NumberTextBox;
                                        if (numInput == null) break;

                                        numInput.StringValueChange += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "GOTFOCUS":
                                        input.GotFocus += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "LOSTFOCUS":
                                        input.LostFocus += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "V6LOSTFOCUS":
                                        var colorInput = input as V6ColorTextBox;
                                        if (colorInput == null) break;
                                        colorInput.V6LostFocus += (s) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["eventargs"] = null;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "KEYDOWN":
                                        input.KeyDown += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "CLICK":
                                        input.Click += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;
                                } //end switch
                            }
                            catch (Exception exfor)
                            {
                                thisForm.WriteExLog(thisForm.GetType() + ".CreateFormControls ReadEventInForLoop", exfor);
                            }
                        } //end for
                    } //end if DMETHOD

                    //Add brother
                    int left = input.Right + 30;
                    if (input is V6VvarTextBox && !string.IsNullOrEmpty(defineInfo.BField))
                    {
                        var tT = (V6VvarTextBox)input;
                        tT.BrotherFields = defineInfo.BField;
                        tT.BrotherFields2 = defineInfo.BField2;
                        if (!string.IsNullOrEmpty(defineInfo.ShowName)) tT.ShowName = defineInfo.ShowName == "1";
                        var txtB = new V6LabelTextBox();
                        txtB.Name = "txt" + defineInfo.BField;
                        txtB.AccessibleName = defineInfo.BField;
                        txtB.Top = top;
                        txtB.Left = left;
                        //txtB.Width = 300;
                        txtB.AutoSize = true;
                        txtB.ReadOnly = true;
                        txtB.TabStop = false;

                        All_Objects[txtB.Name] = txtB;
                        panel1.Controls.Add(txtB);
                        left = txtB.Right + 10;
                    }
                    if (input is V6LookupTextBox && !string.IsNullOrEmpty(defineInfo.BField))
                    {
                        var tT = (V6LookupTextBox)input;
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
            Event_program2 = V6ControlsHelper.CreateProgram("EventNameSpace", "EventClass", "D" + ma_bc, using_text2,
                method_text2);
        }
    }
}
