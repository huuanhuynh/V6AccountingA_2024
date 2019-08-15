using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.ReportD;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class FilterBase : V6Control
    {
        #region ==== FIELDS and PROPERTIES ====
        protected DataSet _ds = null;
        protected string _program;
        protected string MAU = "", LAN = "";

        //{tuanmh 11/09/2016
        private DataGridView _parentGridView;
        /// <summary>
        /// Mã liên kết giữa 2 bảng.
        /// Mặc định không thay đổi là stt_rec
        /// Thay đổi ở Init của FilterControl
        /// </summary>
        public string Ref_key = "Stt_rec";

        public string ReportFileF5 = null;
        public string ReportTitleF5 = null;
        public string ReportTitle2F5 = null;
        /// <summary>
        /// Các trường ẩn trên gridview
        /// </summary>
        public SortedDictionary<string, string> GridViewHideFields;
        /// <summary>
        /// Tùy trường hợp có dùng hay không khi gọi từ report này qua report khác
        /// </summary>
        public List<SqlParameter> InitFilters = new List<SqlParameter>();

        public IDictionary<string, object> ParentRowData;
        public virtual string Kieu_post { get; set; }
        /// <summary>
        /// <para>Chỉ định kiểu thực thi cho hàm gốc</para>
        /// <para></para>
        /// </summary>
        public ExecuteMode ExecuteMode = ExecuteMode.ExecuteProcedureNoneQuery;
        /// <summary>
        /// key1,key1
        /// </summary>
        public Dictionary<string, object> ParameterNameList = new Dictionary<string, object>();
        /// <summary>
        /// key2,key2
        /// </summary>
        public Dictionary<string, object> ParameterNameList2 = new Dictionary<string, object>();

        public FilterLineDynamic lineNgay_ct1 = null;
        public FilterLineDynamic lineNgay_ct2 = null;
        public FilterLineDynamic lineMauBC { get; set; }
        public FilterLineDynamic lineLAN { get; set; }
        public FilterLineDynamic lineUserID { get; set; }

        protected string Alreport_advance = "";

        //Các biến xài tùy ý.
        public IDictionary<string, object> ObjectDictionary = new SortedDictionary<string, object>();
        public event StringValueChanged String1ValueChanged;
        public event CheckValueChanged Check1ValueChanged;


        public string String2, String3;
        /// <summary>
        /// Advance filter get albc
        /// </summary>
        public string Advance { get; set; }
        /// <summary>
        /// Dùng procedure này để lấy dữ liệu. Code trong phần gọi proc của formReportBase.
        /// </summary>
        public string ProcedureName;

        public decimal Number1;
        public decimal Number2;
        public decimal Number3;

        /// <summary>
        /// Gán giá trị ở Filter GetFilterParameters
        /// </summary>
        public DateTime Date1 { get; set; }
        /// <summary>
        /// Gán giá trị ở Filter GetFilterParameters
        /// </summary>
        public DateTime Date2 { get; set; }
        /// <summary>
        /// Gán giá trị ở Filter GetFilterParameters
        /// </summary>
        public DateTime Date3 { get; set; }
        public bool Check2, Check3;

        private bool _check1 = false;
        [DefaultValue(false)]
        public bool Check1
        {
            get
            {
                return _check1;
            }
            set
            {
                var old = _check1;
                _check1 = value;
                OnCheck1ValueChanged(old, value);
            }
        }


        /// <summary>
        /// XuLyHienThiFormSuaChungTuF3
        /// </summary>
        public bool F3 { get; set; }
        /// <summary>
        /// XuLyBoSungThongTinChungTuF4
        /// </summary>
        public bool F4 { get; set; }
        /// <summary>
        /// XuLyXemChiTietF5
        /// </summary>
        public bool F5 { get; set; }
        /// <summary>
        /// Thay thế
        /// </summary>
        public bool F6 { get; set; }
        /// <summary>
        /// Đồ thị
        /// </summary>
        public bool F7 { get; set; }
        /// <summary>
        /// Duyệt
        /// </summary>
        public bool F8 { get; set; }
        /// <summary>
        /// View / In hóa đơn
        /// </summary>
        public bool F9 { get; set; }
        /// <summary>
        /// Xử lý khác.
        /// </summary>
        public bool F10 { get; set; }
        /// <summary>
        /// Hiển thị phần tổng cộng của GridView. Mặc định true.
        /// </summary>
        [DefaultValue(true)]
        [Description("Hiển thị phần tổng cộng của GridView. Mặc định true.")]
        public bool ViewSum { get { return _viewSum; } set { _viewSum = value; } }
        private bool _viewSum = true;
        /// <summary>
        /// Ngôn ngữ tùy chọn khi xuất báo cáo
        /// </summary>
        public string RLan = "V";
        public string RTien = "VN";
        /// <summary>
        /// vị trí bắt đầu cột dữ liệu (cho báo cáo cột động)
        /// </summary>
        public int fstart = 5;
        /// <summary>
        /// số cột data (cho báo cáo cột động)
        /// </summary>
        public int ffixcolumn = 6;
        /// <summary>
        /// Tham số thêm cho file rpt
        /// </summary>
        public IDictionary<string, object> RptExtraParameters { get; set; }
        /// <summary>
        /// Chứa dữ liệu gán vào từ hàm GetFilterData của lớp cha
        /// </summary>
        public IDictionary<string, object> ParentFilterData { get; set; }
        public IDictionary<string, object> FilterData { get; set; }

        protected delegate void SetParentAllRowHandle(DataGridView DataGridView1);
        protected event SetParentAllRowHandle SetParentAllRowEvent;
        protected virtual void OnSetParentAllRow(DataGridView DataGridView1)
        {
            var handler = SetParentAllRowEvent;
            if (handler != null) handler(DataGridView1);
        }

        #endregion FIELDS PROPERTIES


        public FilterBase()
        {
            InitializeComponent();
        }
        
        public FilterBase(string program)
        {
            InitializeComponent();
            _program = program;
        }

        private void FilterBase_Load(object sender, EventArgs e)
        {
            FixFilterLineSize();
        }

        public void MyInitDynamic(string program)
        {
            _program = program;
            //return;//Chưa hoàn thành
            try
            {
                //Add filterLine dynamic.
                MadeFilterControls(_program, new Dictionary<string,object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInitDynamic", ex);
            }
        }
        
        public void MadeFilterControls(string program, Dictionary<string, object> all_Objects)
        {
            Type Event_program = null;
            //Dictionary<string, object> All_Objects = new Dictionary<string, object>();
            all_Objects["filterControl"] = this;
            string all_using_text = "", all_method_text = "";

            SqlParameter[] plist =
            {
                new SqlParameter("@ma_bc", program), 
            };
            var dataALREPORT1 = V6BusinessHelper.Select("ALREPORT1", "*", "Ma_bc=@ma_bc", "", "Stt_filter", plist).Data;
            if (dataALREPORT1 == null || dataALREPORT1.Rows.Count == 0) return;

            var viewSortSttKey = new DataView(dataALREPORT1);
            viewSortSttKey.Sort = "Stt_Key";
            var keyList = viewSortSttKey.ToTable().ToDataDictionary("key1", "key1");
            var keyList2 = viewSortSttKey.ToTable().ToDataDictionary("key2", "key2");
            ParameterNameList = keyList;
            ParameterNameList2 = keyList2;
            //FilterControl.
            string err = "";
            try
            {
                int i = 0, lineTop = 35;
                //get lineTop
                GroupBox groupBox1 = GetControlByName("groupBox1") as GroupBox;
                if (groupBox1 != null)
                    foreach (Control control in groupBox1.Controls)
                    {
                        if(lineTop < control.Top + 35)
                        lineTop = control.Top + 35;
                    }

                foreach (DataRow row in dataALREPORT1.Rows)
                {
                    var ten = row["ten"].ToString();
                    try
                    {
                        string define = row["filter"].ToString().Trim();


                        var lineControl0 = V6ControlFormHelper.MadeLineDynamicControl(define);
                        all_Objects[lineControl0.Name] = lineControl0;

                        if (lineControl0 is FilterLineDynamic)
                        {
                            FilterLineDynamic lineControl = lineControl0 as FilterLineDynamic;
                            var key1 = row["key1"].ToString().Trim();
                            var key2 = row["key2"].ToString().Trim();
                            var key3 = row["key3"].ToString().Trim();
                            var key4 = row["key4"].ToString().Trim();
                            var loai_key = row["loai_key"].ToString().Trim();
                            lineControl.DefineInfo.Key1 = key1;
                            lineControl.DefineInfo.Key2 = key2;
                            lineControl.DefineInfo.Key3 = key3;
                            lineControl.DefineInfo.Key4 = key4;
                            lineControl.DefineInfo.Loai_key = loai_key;


                            //Vị trí
                            lineControl.Location = new Point(3, lineTop);
                            if (lineControl.DefineInfo.Visible)
                            {
                                lineTop += lineControl.Height;
                            }
                            AddLineControls(lineControl);
                            //panel1.Controls.Add(lineControl);

                            //Giữ lại control ngày.
                            if (lineControl.DefineInfo.DefaultValue == "M_NGAY_CT1")
                                lineNgay_ct1 = lineControl;
                            if (lineControl.DefineInfo.DefaultValue == "M_NGAY_CT2")
                                lineNgay_ct2 = lineControl;
                            //Giu lai tiente, ngonnguBC
                            if (lineControl.DefineInfo.DefaultValue == "M_MAU_BC")
                                lineMauBC = lineControl;
                            if (lineControl.DefineInfo.DefaultValue == "M_LAN")
                                lineLAN = lineControl;
                            //Giữ lại user_id
                            if (lineControl.DefineInfo.DefaultValue == "M_USER_ID")
                                lineUserID = lineControl;

                            string xml = row["DMETHOD"].ToString().Trim();
                            if (!string.IsNullOrEmpty(xml))
                            {
                                DataSet ds = new DataSet();
                                ds.ReadXml(new StringReader(xml));
                                if (ds.Tables.Count <= 0) break;

                                var data = ds.Tables[0];

                                foreach (DataRow event_row in data.Rows)
                                {
                                    //nơi sử dụng: QuickReportManager, DynamicAddEditForm
                                    //Cần viết lại lineControl events.
                                    try
                                    {
                                        var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                                        var method_name = event_row["method"].ToString().Trim();

                                        all_using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                                        all_method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";

                                        //Make dynamic event and call
                                        switch (EVENT_NAME)
                                        {
                                            case ControlDynamicEvent.TEXTCHANGE:
                                                lineControl.TextChanged += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.VALUECHANGE:
                                                //V6NumberTextBox numInput = lineControl._ as V6NumberTextBox;
                                                //if (numInput == null) break;

                                                lineControl.ValueChanged += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.LOSTFOCUS:
                                                lineControl.LostFocus += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.GOTFOCUS:
                                                lineControl.GotFocus += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.V6LOSTFOCUS:
                                                lineControl.V6LostFocus += (s) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["eventargs"] = null;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.KEYDOWN:
                                                lineControl.KeyDown += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.CLICK:
                                                lineControl.Click += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;
                                        }//end switch
                                    }
                                    catch (Exception exfor)
                                    {
                                        V6ControlFormHelper.WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".EventFor", exfor);
                                    }
                                }//end for
                            }

                        }
                        else if (lineControl0 is FilterGroup)
                        {
                            //Copy code
                            FilterGroup lineControl = lineControl0 as FilterGroup;
                            var key1 = row["key1"].ToString().Trim();
                            var key2 = row["key2"].ToString().Trim();
                            var key3 = row["key3"].ToString().Trim();
                            var key4 = row["key4"].ToString().Trim();
                            var loai_key = row["loai_key"].ToString().Trim();
                            lineControl.DefineInfo.Key1 = key1;
                            lineControl.DefineInfo.Key2 = key2;
                            lineControl.DefineInfo.Key3 = key3;
                            lineControl.DefineInfo.Key4 = key4;
                            lineControl.DefineInfo.Loai_key = loai_key;


                            //Vị trí
                            lineControl.Location = new Point(3, lineTop);
                            lineTop += lineControl.Height;
                            AddLineGroupControls(lineControl);
                        }
                        i++;
                    }
                    catch (Exception e1)
                    {
                        err += "\n" + i + " " + ten + ": " + e1.Message;
                    }
                }
                Event_program = V6ControlsHelper.CreateProgram("EventNameSpace", "EventClass", "D" + program, all_using_text, all_method_text);
            }
            catch (Exception ex)
            {
                err += "\n" + ex.Message;
                this.WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".MadeFilterControls", ex);
            }
            if (err.Length > 0)
            {
                this.ShowErrorMessage("MadeFilterControls error: " + err);
            }

            all_Objects = all_Objects;
        }
        
        public void AddLineControls(FilterLineDynamic lineControl)
        {
            GroupBox groupBox1 = GetControlByName("groupBox1") as GroupBox;
            if (groupBox1 == null)
            {
                this.ShowMessage("groupBox1 null.");
                return;
            }

            lineControl.Width = groupBox1.Width - 30;
            groupBox1.Controls.Add(lineControl);

            if (lineControl.DefineInfo.Visible && groupBox1.Height - 10 < lineControl.Bottom)
            {
                Height = groupBox1.Top + lineControl.Bottom + 20;
            }

            if (lineControl.DefineInfo.Loai_key == "A1" && lineControl._checkBox != null)
            {
                if (lineControl._checkBox.Checked) Advance = Alreport_advance;
                lineControl._checkBox.CheckedChanged += delegate
                {
                    if (lineControl._checkBox.Checked)
                    {
                        Advance = Alreport_advance;
                    }
                    else
                    {
                        Advance = "";
                    }
                };
            }

            string description = lineControl.DefineInfo.DescriptionLang(V6Setting.IsVietnamese);
            if (!string.IsNullOrEmpty(description))
            {
                filterBaseToolTip1.SetToolTip(lineControl, description);
                foreach (Control control in lineControl.Controls)
                {
                    filterBaseToolTip1.SetToolTip(control, description);
                }
            }
        }

        public void AddLineGroupControls(FilterGroup lineControl)
        {
            GroupBox groupBox1 = GetControlByName("groupBox1") as GroupBox;
            if (groupBox1 == null)
            {
                this.ShowMessage("groupBox1 null.");
                return;
            }

            lineControl.Width = groupBox1.Width - 10;
            groupBox1.Controls.Add(lineControl);

            if (lineControl.DefineInfo.Visible && groupBox1.Height - 10 < lineControl.Bottom)
            {
                Height = groupBox1.Top + lineControl.Bottom + 20;
            }

            //if (lineControl.DefineInfo.Loai_key == "A1" && lineControl._checkBox != null)
            //{
            //    if (lineControl._checkBox.Checked) Advance = Alreport_advance;
            //    lineControl._checkBox.CheckedChanged += delegate
            //    {
            //        if (lineControl._checkBox.Checked)
            //        {
            //            Advance = Alreport_advance;
            //        }
            //        else
            //        {
            //            Advance = "";
            //        }
            //    };
            //}
        }
        
        private void FixFilterLineSize()
        {
            try
            {
                GroupBox groupBox1 = V6ControlFormHelper.GetControlByName(this, "groupBox1") as GroupBox;
                if (groupBox1 == null) return;
                int filterLineFixWidth = groupBox1.Width - 30;
                foreach (Control control in groupBox1.Controls)
                {
                    if (control is FilterLineDynamic)
                    {
                        control.Width = filterLineFixWidth;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixFilterLineSize", ex);
            }
        }

        protected virtual void OnString1ValueChanged(string oldvalue, string newvalue)
        {
            var handler = String1ValueChanged;
            if (handler != null) handler(oldvalue, newvalue);
        }
        protected virtual void OnCheck1ValueChanged(bool oldvalue, bool newvalue)
        {
            var handler = Check1ValueChanged;
            if (handler != null) handler(oldvalue, newvalue);
        }
        private string _string1;

        public string String1
        {
            get
            {
                return _string1;
            }
            set
            {
                var old = _string1;
                _string1 = value;
                OnString1ValueChanged(old, value);
            }
        }


        public virtual void Call1(object s = null)
        {
            
        }
        public virtual void Call2(object s = null)
        {

        }
        public virtual void Call3(object s = null)
        {

        }

        /// <summary>
        /// Hàm override lấy các dữ liệu cần thiết để gán vào ParentFilterData cho lớp sau.
        /// </summary>
        /// <returns></returns>
        protected virtual void GetFilterData()
        {
            FilterData = new SortedDictionary<string, object>();
        }


        /// <summary>
        /// Lấy danh sách tham số.
        /// </summary>
        /// <returns></returns>
        public virtual List<SqlParameter> GetFilterParameters()
        {
            var list_result = new List<SqlParameter>();
            list_result.AddRange(InitFilters);

            var lineQuery = "";
            foreach (Control control in Controls)
            {
                var lineControl = control as FilterLineBase;
                if (lineControl != null && lineControl.IsSelected)
                {
                    lineQuery += (true ? "\nand " : "\nor  ") + lineControl.Query;
                }
            }
            
            if(lineQuery.Length>0)
            list_result.Add(new SqlParameter("@cKey", lineQuery.Length > 4 ? lineQuery.Substring(4) : ""));

            return list_result;
        }

        public List<SqlParameter> GetFilterParameters2()
        {
            //if (lineNgay_ct1 != null)
            //    V6Setting.M_ngay_ct1 = ObjectAndString.ObjectToFullDateTime(lineNgay_ct1.ObjectValue);
            //if (lineNgay_ct2 != null)
            //    V6Setting.M_ngay_ct2 = ObjectAndString.ObjectToFullDateTime(lineNgay_ct2.ObjectValue);

            
            var parent00 = Parent.Parent.Parent;
            //if (parent00 is ReportD99ViewBase)
            //{
            //    var parent99 = (ReportD99ViewBase)parent00;
            //    MAU = parent99.MAU;
            //    LAN = parent99.LAN;
            //    if (lineMauBC != null)
            //        lineMauBC.SetValue(MAU == "VN" ? "0" : "1");
            //    if (lineLAN != null)
            //        lineLAN.SetValue(LAN);
            //    if (lineUserID != null)
            //        lineUserID.SetValue(V6Login.UserId);
            //}
            //if (parent00 is ReportR44ViewBase)
            //{
            //    var parent44 = (ReportR44ViewBase)parent00;
            //    MAU = parent44.MAU;
            //    LAN = parent44.LAN;
            //    if (lineMauBC != null)
            //        lineMauBC.SetValue(MAU == "VN" ? "0" : "1");
            //    if (lineLAN != null)
            //        lineLAN.SetValue(LAN);
            //    if (lineUserID != null)
            //        lineUserID.SetValue(V6Login.UserId);
            //}
            if (parent00 is ReportDViewBase)
            {
                var parent44 = (ReportDViewBase)parent00;
                MAU = parent44.MAU;
                LAN = parent44.LAN;
            }
            if (parent00 is ReportRViewBase)
            {
                var parent44 = (ReportRViewBase)parent00;
                MAU = parent44.MAU;
                LAN = parent44.LAN;
            }
            if (parent00 is ReportRView2Base)
            {
                var parent44 = (ReportRView2Base)parent00;
                MAU = parent44.MAU;
                LAN = parent44.LAN;
            }
            if (parent00 is ReportTreeViewBase)
            {
                var parent44 = (ReportTreeViewBase)parent00;
                MAU = parent44.MAU;
                LAN = parent44.LAN;
            }

            if (lineMauBC != null)
                lineMauBC.SetValue(MAU == "VN" ? "0" : "1");
            if (lineLAN != null)
                lineLAN.SetValue(LAN);
            if (lineUserID != null)
                lineUserID.SetValue(V6Login.UserId);

            RadioButton radAnd = GetControlByName("radAnd") as RadioButton;
            var and = true;
            if(radAnd != null)    and = radAnd.Checked;
            var and_or = and ? " and" : " or ";
            var result = new List<SqlParameter>();
            //result.AddRange(InitFilters);
            //var resultDic = new Dictionary<string,object>();

            // Duyệt qua danh sách parameter name, tạo giá trị cho từng cái một.
            foreach (KeyValuePair<string, object> item in ParameterNameList)
            {
                string parameterName = ("" + item.Value).Trim();
                if (parameterName == "") continue;
                string string_value = "";
                object object_value = "";
                List<string> in_fieldList_Alkh = new List<string>();
                List<string> in_fieldList_Alvt = new List<string>();
                List<string> in_fieldList_Altk = new List<string>();
                List<string> in_fieldList_Alvv = new List<string>();
                List<string> in_fieldList_Alphi = new List<string>();
                List<string> in_fieldList_Alhd = new List<string>();
                List<string> in_fieldList_Alku = new List<string>();
                List<string> in_fieldList_Alsp = new List<string>();

                List<string> in_fieldList_Allo = new List<string>();
                List<string> in_fieldList_Albp = new List<string>();
                List<string> in_fieldList_Alnvien = new List<string>();
                List<string> in_fieldList_Albpht = new List<string>();
                List<string> in_fieldList_Althau = new List<string>();
                List<string> in_fieldList_Alsonb = new List<string>();
                List<string> in_fieldList_Aldvcs = new List<string>();
                List<string> in_fieldList_Alkho = new List<string>();

                string last_key = "";
                GroupBox groupBox1 = V6ControlFormHelper.GetControlByName(this, "groupBox1") as GroupBox;
                if (groupBox1 != null)
                foreach (Control control in groupBox1.Controls)
                {
                    if (control is FilterGroup)
                    {
                        var group = control as FilterGroup;
                        if (group.DefineInfo.Key1.ToUpper() != parameterName.ToUpper()) continue;

                        last_key = group.DefineInfo.Loai_key;

                        switch (last_key)
                        {
                            case "A1"://1value
                                object_value = group.StringValue;
                                break;
                            case "11"://1value
                                {
                                    object_value = group.ObjectValue;
                                }
                                break;
                        }
                    }
                    else if (control is FilterLineDynamic)
                    {
                        var line = control as FilterLineDynamic;
                        if (line.DefineInfo.Key1.ToUpper() != parameterName.ToUpper()) continue;

                        if (line.CheckNotEmpty && line.StringValue == "")
                        {
                            throw new Exception(line.FieldName);
                        }
                        last_key = line.DefineInfo.Loai_key;

                        switch (last_key)
                        {
                            case "10"://ngay
                                if (line.IsSelected)
                                    string_value = line.StringValue;
                                break;
                            case "A1"://1value
                                object_value = line.StringValue;
                                break;
                            case "11"://1value
                                if (line.IsSelected)
                                {
                                    object_value = line.ObjectValue;
                                }
                                else
                                {
                                    object_value = null;
                                }
                                break;
                            case "21"://in alkh
                                if (line.IsSelected)
                                    in_fieldList_Alkh.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "22"://in alvt
                                if (line.IsSelected)
                                    in_fieldList_Alvt.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "23"://in altk
                                if (line.IsSelected)
                                    in_fieldList_Altk.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "24"://in alvv
                                if (line.IsSelected)
                                    in_fieldList_Alvv.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "25"://in alphi
                                if (line.IsSelected)
                                    in_fieldList_Alphi.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "26"://in alhd
                                if (line.IsSelected)
                                    in_fieldList_Alhd.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "27"://in alku
                                if (line.IsSelected)
                                    in_fieldList_Alku.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "28"://in alsp
                                if (line.IsSelected)
                                    in_fieldList_Alsp.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;

                            case "29": //in allo
                                if (line.IsSelected)
                                    in_fieldList_Allo.Add(line.FieldName);
                                break;
                            case "2A": //in albp
                                if (line.IsSelected)
                                    in_fieldList_Albp.Add(line.FieldName);
                                break;
                            case "2B": //in alnvien
                                if (line.IsSelected)
                                    in_fieldList_Alnvien.Add(line.FieldName);
                                break;
                            case "2C": //in albpht
                                if (line.IsSelected)
                                    in_fieldList_Albpht.Add(line.FieldName);
                                break;
                            case "2D": //in althau
                                if (line.IsSelected)
                                    in_fieldList_Althau.Add(line.FieldName);
                                break;
                            case "2E": //in alsonb
                                if (line.IsSelected)
                                    in_fieldList_Alsonb.Add(line.FieldName);
                                break;
                            case "2F": //in aldvcs
                                if (line.IsSelected)
                                    in_fieldList_Aldvcs.Add(line.FieldName);
                                break;
                            case "2G": //in alkho
                                if (line.IsSelected)
                                    in_fieldList_Alkho.Add(line.FieldName);
                                break;

                            case "30"://advance, cộng dồn
                                if (line.IsSelected)
                                    string_value = string.Format("{0}{1} {2}", string_value, and_or, line.Query);
                                break;
                        }// end switch
                    }
                }// end for lines

                switch (last_key)
                {
                    case "10": //ngay
                        result.Add(new SqlParameter(parameterName, string_value));
                        break;
                    case "A1": //1value
                        result.Add(new SqlParameter(parameterName, object_value));
                        break;
                    case "11": //1value
                        result.Add(new SqlParameter(parameterName, object_value));
                        break;
                    case "21": //in alkh
                    case "22": //in alvt
                    case "23": //in altk
                    case "24": //in alvv
                    case "25": //in alphi
                    case "26": //in alhd
                    case "27": //in alku
                    case "28": //in alsp(alvt)

                    case "29": //in allo
                    case "2A": //in albp
                    case "2B": //in alnvien
                    case "2C": //in albpht
                    case "2D": //in althau
                    case "2E": //in alsonb
                    case "2F": //in aldvcs
                    case "2G": //in alkho

                    case "30": //advance
                        var advance_key_string = GenAdvanceKeyString(and, string_value, in_fieldList_Alkh, in_fieldList_Alvt, in_fieldList_Altk, in_fieldList_Alvv,
                            in_fieldList_Alphi, in_fieldList_Alhd, in_fieldList_Alku, in_fieldList_Alsp,
                            in_fieldList_Allo, in_fieldList_Albp, in_fieldList_Alnvien, in_fieldList_Albpht,
                            in_fieldList_Althau, in_fieldList_Alsonb, in_fieldList_Aldvcs, in_fieldList_Alkho
                            );
                        result.Add(new SqlParameter(item.Key, advance_key_string));
                        break;
                }
            }//End for ParameterNameList

            foreach (KeyValuePair<string, object> item in ParameterNameList2)
            {
                string parameterName = ("" + item.Value).Trim();
                if (parameterName == "") continue;
                string string_value = "";
                object object_value = "";
                List<string> in_fieldList_Alkh = new List<string>();
                List<string> in_fieldList_Alvt = new List<string>();
                List<string> in_fieldList_Altk = new List<string>();
                List<string> in_fieldList_Alvv = new List<string>();
                List<string> in_fieldList_Alphi = new List<string>();
                List<string> in_fieldList_Alhd = new List<string>();
                List<string> in_fieldList_Alku = new List<string>();
                List<string> in_fieldList_Alsp = new List<string>();

                List<string> in_fieldList_Allo = new List<string>();
                List<string> in_fieldList_Albp = new List<string>();
                List<string> in_fieldList_Alnvien = new List<string>();
                List<string> in_fieldList_Albpht = new List<string>();
                List<string> in_fieldList_Althau = new List<string>();
                List<string> in_fieldList_Alsonb = new List<string>();
                List<string> in_fieldList_Aldvcs = new List<string>();
                List<string> in_fieldList_Alkho = new List<string>();

                string last_key = "";
                GroupBox groupBox1 = V6ControlFormHelper.GetControlByName(this, "groupBox1") as GroupBox;
                if (groupBox1 != null)
                foreach (Control control in groupBox1.Controls)
                {
                    if (control is FilterGroup)
                    {
                        var group = control as FilterGroup;
                        if (group.DefineInfo.Key1.ToUpper() != parameterName.ToUpper()) continue;

                        last_key = group.DefineInfo.Loai_key;

                        switch (last_key)
                        {
                            case "A1"://1value
                                object_value = group.StringValue;
                                break;
                            case "11"://1value
                                {
                                    object_value = group.ObjectValue;
                                }
                                break;
                        }
                    }
                    else if (control is FilterLineDynamic)
                    {
                        var line = control as FilterLineDynamic;
                        if (line == null) continue;
                        if (line.DefineInfo.Key2.ToUpper() != parameterName.ToUpper()) continue;

                        if (line.CheckNotEmpty && line.StringValue == "")
                        {
                            throw new Exception(line.FieldName);
                        }
                        last_key = line.DefineInfo.Loai_key;

                        switch (last_key)
                        {
                            case "10"://ngay
                                if (line.IsSelected)
                                    string_value = line.StringValue;
                                break;
                            case "A1"://1value
                                object_value = line.StringValue;
                                break;
                            case "11"://1value
                                if (line.IsSelected)
                                {
                                    object_value = line.ObjectValue;
                                }
                                else
                                {
                                    object_value = null;
                                }
                                break;
                            case "21"://in alkh
                                if (line.IsSelected)
                                    in_fieldList_Alkh.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "22"://in alvt
                                if (line.IsSelected)
                                    in_fieldList_Alvt.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "23"://in altk
                                if (line.IsSelected)
                                    in_fieldList_Altk.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "24"://in alvv
                                if (line.IsSelected)
                                    in_fieldList_Alvv.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "25"://in alphi
                                if (line.IsSelected)
                                    in_fieldList_Alphi.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "26"://in alhd
                                if (line.IsSelected)
                                    in_fieldList_Alhd.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "27"://in alku
                                if (line.IsSelected)
                                    in_fieldList_Alku.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "28"://in alsp
                                if (line.IsSelected)
                                    in_fieldList_Alsp.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;

                            case "29": //in allo
                                if (line.IsSelected)
                                    in_fieldList_Allo.Add(line.FieldName);
                                break;
                            case "2A": //in albp
                                if (line.IsSelected)
                                    in_fieldList_Albp.Add(line.FieldName);
                                break;
                            case "2B": //in alnvien
                                if (line.IsSelected)
                                    in_fieldList_Alnvien.Add(line.FieldName);
                                break;
                            case "2C": //in albpht
                                if (line.IsSelected)
                                    in_fieldList_Albpht.Add(line.FieldName);
                                break;
                            case "2D": //in althau
                                if (line.IsSelected)
                                    in_fieldList_Althau.Add(line.FieldName);
                                break;
                            case "2E": //in alsonb
                                if (line.IsSelected)
                                    in_fieldList_Alsonb.Add(line.FieldName);
                                break;
                            case "2F": //in aldvcs
                                if (line.IsSelected)
                                    in_fieldList_Aldvcs.Add(line.FieldName);
                                break;
                            case "2G": //in alkho
                                if (line.IsSelected)
                                    in_fieldList_Alkho.Add(line.FieldName);
                                break;

                            case "30"://advance, cộng dồn
                                if (line.IsSelected)
                                    string_value = string.Format("{0}{1} {2}", string_value, and_or, line.Query);
                                break;
                        }// end switch
                    }
                }// end for lines

                switch (last_key)
                {
                    case "10": //ngay
                        result.Add(new SqlParameter(parameterName, string_value));
                        break;
                    case "A1": //1value
                        result.Add(new SqlParameter(parameterName, object_value));
                        break;
                    case "11": //1value
                        result.Add(new SqlParameter(parameterName, object_value));
                        break;
                    case "21": //in alkh
                    //param_value = line.StringValue;
                    case "22": //in alvt
                    //param_value = line.StringValue;
                    case "23": //in altk
                    //param_value = line.StringValue;
                    case "24": //in alvv
                    //param_value = line.StringValue;
                    case "25": //in alphi
                    case "26": //in alhd
                    case "27": //in alku
                    case "28": //in alsp(alvt)

                    case "29": //in allo
                    case "2A": //in albp
                    case "2B": //in alnvien
                    case "2C": //in albpht
                    case "2D": //in althau
                    case "2E": //in alsonb
                    case "2F": //in aldvcs
                    case "2G": //in alkho

                    case "30": //advance
                        var advance_key_string = GenAdvanceKeyString(and, string_value, in_fieldList_Alkh, in_fieldList_Alvt, in_fieldList_Altk, in_fieldList_Alvv,
                            in_fieldList_Alphi, in_fieldList_Alhd, in_fieldList_Alku, in_fieldList_Alsp,
                            in_fieldList_Allo, in_fieldList_Albp, in_fieldList_Alnvien, in_fieldList_Albpht,
                            in_fieldList_Althau, in_fieldList_Alsonb, in_fieldList_Aldvcs, in_fieldList_Alkho
                            );
                        result.Add(new SqlParameter(item.Key, advance_key_string));
                        break;
                }
            }//End for ParameterNameList2

            return result;
        }

        /// <summary>
        /// Tạo key advance
        /// </summary>
        /// <param name="and"></param>
        /// <param name="string_value">Key lẻ của loại key 30</param>
        /// <param name="in_fieldList_Alkh"></param>
        /// <param name="in_fieldList_Alvt"></param>
        /// <param name="in_fieldList_Altk"></param>
        /// <param name="in_fieldList_Alvv"></param>
        /// <param name="in_fieldList_Alphi"></param>
        /// <param name="in_fieldList_Alhd"></param>
        /// <param name="in_fieldList_Alku"></param>
        /// <param name="in_fieldList_Alsp"></param>
        /// <returns></returns>
        protected string GenAdvanceKeyString(bool and, string string_value,
            List<string> in_fieldList_Alkh, List<string> in_fieldList_Alvt, List<string> in_fieldList_Altk, List<string> in_fieldList_Alvv,
            List<string> in_fieldList_Alphi, List<string> in_fieldList_Alhd, List<string> in_fieldList_Alku, List<string> in_fieldList_Alsp,
            List<string> in_fieldList_Allo, List<string> in_fieldList_Albp, List<string> in_fieldList_Alnvien, List<string> in_fieldList_Albpht,
            List<string> in_fieldList_Althau, List<string> in_fieldList_Alsonb, List<string> in_fieldList_Aldvcs, List<string> in_fieldList_Alkho)
        {
            var string_in_Alkh = GenkeyStringInTable(in_fieldList_Alkh, and, "MA_KH", "ma_kh", "alkh");
            var string_in_Alvt = GenkeyStringInTable(in_fieldList_Alvt, and, "MA_VT", "ma_vt", "alvt");
            var string_in_Altk = GenkeyStringInTable(in_fieldList_Altk, and, "TK", "tk", "altk");
            var string_in_Alvv = GenkeyStringInTable(in_fieldList_Alvv, and, "MA_VV", "ma_vv", "alvv");

            var string_in_Alphi = GenkeyStringInTable(in_fieldList_Alphi, and, "MA_PHI", "ma_phi", "alphi");
            var string_in_Alhd = GenkeyStringInTable(in_fieldList_Alhd, and, "MA_HD", "ma_hd", "alhd");
            var string_in_Alku = GenkeyStringInTable(in_fieldList_Alku, and, "MA_KU", "ma_ku", "alku");
            var string_in_Alsp = GenkeyStringInTable(in_fieldList_Alsp, and, "MA_SP", "ma_vt", "alvt");

            var string_in_Allo = GenkeyStringInTable(in_fieldList_Allo, and, "MA_LO", "ma_lo", "allo");
            var string_in_Albp = GenkeyStringInTable(in_fieldList_Albp, and, "MA_BP", "ma_bp", "albp");
            var string_in_Alnvien = GenkeyStringInTable(in_fieldList_Alnvien, and, "MA_NVIEN", "ma_nvien", "alnvien");
            var string_in_Albpht = GenkeyStringInTable(in_fieldList_Albpht, and, "MA_BPHT", "ma_bpht", "albpht");
            var string_in_Althau = GenkeyStringInTable(in_fieldList_Althau, and, "MA_THAU", "ma_thau", "althau");
            var string_in_Alsonb = GenkeyStringInTable(in_fieldList_Alsonb, and, "MA_SONB", "ma_sonb", "alsonb");
            var string_in_Aldvcs = GenkeyStringInTable(in_fieldList_Aldvcs, and, "MA_DVCS", "ma_dvcs", "aldvcs");
            var string_in_Alkho = GenkeyStringInTable(in_fieldList_Alkho, and, "MA_KHO", "ma_kho", "alkho");

            var last_string_value = string_value + string_in_Alkh + string_in_Alvt + string_in_Altk + string_in_Alvv
                + string_in_Alphi + string_in_Alhd + string_in_Alku + string_in_Alsp
                + string_in_Allo + string_in_Albp + string_in_Alnvien + string_in_Albpht
                + string_in_Althau + string_in_Alsonb + string_in_Aldvcs + string_in_Alkho
                ;
            if (last_string_value.Length > 4) last_string_value = last_string_value.Substring(4);
            return last_string_value;
        }


        /// <summary>
        /// Tạo key advance in cho một bảng.
        /// </summary>
        /// <param name="in_fieldList_Alsp">Danh sách các field where</param>
        /// <param name="and"></param>
        /// <param name="FIELD_in"></param>
        /// <param name="select_field"></param>
        /// <param name="from_table"></param>
        /// <returns></returns>
        protected string GenkeyStringInTable(List<string> in_fieldList_Alsp, bool and, string FIELD_in, string select_field, string from_table)
        {
            var result = "";

            if (in_fieldList_Alsp != null && in_fieldList_Alsp.Count > 0)
            {
                var and_or = and ? " and" : " or ";
                var key0 = GetFilterStringByFields(in_fieldList_Alsp, and);
                if (!string.IsNullOrEmpty(key0))
                {
                    result = string.Format("{0} {2} in (select {3} from {4} where {1} )",
                        and_or, key0, FIELD_in, select_field, from_table);
                }
            }

            return result;
        }

        public virtual List<SqlParameter> GetFilterParametersNew()
        {
            var listAll = GetFilterParameters();
            try
            {
                var list2 = GetFilterParameters2();
                foreach (SqlParameter parameter in list2)
                {
                    bool contain = false;
                    string newName = parameter.ParameterName;
                    object newValue = parameter.Value;
                    foreach (SqlParameter sqlparameterAll in listAll)
                    {
                        if (sqlparameterAll.ParameterName.ToLower() == parameter.ParameterName.ToLower())
                        {
                            contain = true;
                            //Xuly cong don
                            if (sqlparameterAll.ToString().Trim() != "" && parameter.Value.ToString().Trim() != "")
                            {
                                sqlparameterAll.Value = sqlparameterAll.Value + " and (" + parameter.Value + ")";
                            }
                            else if (parameter.Value.ToString().Trim() != "")
                            {
                                sqlparameterAll.Value = parameter.Value;
                            }
                            break;
                        }
                    }

                    if (!contain)
                    {
                        listAll.Add(new SqlParameter(newName, newValue));
                    }
                }
            }
            catch (Exception ex2)
            {
                this.WriteExLog(GetType() + ".GetFilterParameter2 ex2", ex2);
            }
            
            //listAll.AddRange(GetFilterParameters2());
            return listAll;
        }

        public virtual string GetFilterStringByFields(List<string> fields, bool and, string table = null)
        {
            var result = GetFilterStringByFieldsR(this, fields, and, table);
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        private string GetFilterStringByFieldsR(Control control, List<string> fields, bool and, string table)
        {
            var result = "";
            var line = control as FilterLineBase;
            if (line != null && line.IsSelected && fields.Contains(line.FieldName.ToUpper()))
            {
                result += (and ? "\nand " : "\nor  ") + line.GetQuery(table);
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    result += GetFilterStringByFieldsR(c, fields, and, table);
                }
            }
            return result;
        }

        public delegate void SetParentRowHandle(IDictionary<string,object> row);

        public event SetParentRowHandle SetParentRowEvent;
        
        public void SetParentRow(IDictionary<string,object> currentRow)
        {
            ParentRowData = currentRow;
            OnSetParentRow(ParentRowData);
        }

        public virtual void OnSetParentRow(IDictionary<string,object> currentRow)
        {
            var handler = SetParentRowEvent;
            if (handler != null) handler(currentRow);
        }

        

        
        public void SetParentAllRow(DataGridView DataGridView1)
        {
            _parentGridView = DataGridView1;
            OnSetParentAllRow(_parentGridView);
        }

        //}tuanmh 11/09/2016

        /// <summary>
        /// Gán các giá trị String1, Number1...
        /// </summary>
        public virtual void UpdateValues()
        {
            
        }
        
        /// <summary>
        /// Tạo bảng dữ liệu cho cbo trong ChartReportForm
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GenTableForReportType()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Nhận dữ liệu khi tải xong.
        /// </summary>
        /// <param name="ds"></param>
        public virtual void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
        }

        public virtual void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            
        }

        public virtual void FormatGridView2(V6ColorDataGridView dataGridView2)
        {
            
        }

        /// <summary>
        /// Cần override
        /// </summary>
        /// <param name="data"></param>
        public override void SetData(IDictionary<string, object> data)
        {
            V6ControlFormHelper.SetSomeDataDictionary(this, data);
        }

        public virtual void SetStatus2Text()
        {
            var text = "";

            if (F3) text += "F3-Sửa chứng từ";
            if (F5)
            {
                if (F3) text += "; ";
                text += "F5-Xem chi tiết";
            }
            if (F7)
            {
                if (F3|F5) text += "; ";
                text += "F7-Xem biểu đồ.";
            }
            V6ControlFormHelper.SetStatusText2(text);
        }

        /// <summary>
        /// Lấy tham số cho rpt theo định nghĩa Extra_para
        /// </summary>
        /// <param name="ExtraParameterInfo"></param>
        /// <param name="LAN"></param>
        /// <returns></returns>
        public IDictionary<string, object> GetRptParametersD(string ExtraParameterInfo, string LAN)
        {
            try
            {
                var result = new SortedDictionary<string, object>();
                var lineList = GetFilterLineList();
                try
                {
                    if (string.IsNullOrEmpty(ExtraParameterInfo)) return null;
                    var sss = ExtraParameterInfo.Split('~');

                    foreach (string ss in sss)
                    {
                        DefineInfo di = new DefineInfo(ss);
                        if (string.IsNullOrEmpty(di.Name)) continue;
                        
                        if (di.Ptype.ToUpper() == "TABLE2")
                        {
                            var dataTable2 = _ds.Tables[1];
                            var _tbl2Row = dataTable2.Rows[0];
                            if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBANNT")
                            {
                                var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);// "T_TIEN_NT2_IN"]);
                                var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien_nt2_in, LAN, ma_nt);
                            }
                            else if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBAN")
                            {
                                var t_tien2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);//"T_TIEN2_IN"]);
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien2_in, LAN,
                                    V6Options.M_MA_NT0);
                            }
                            else
                            {
                                //Bỏ qua giá trị rỗng.
                                if (di.NotEmpty && string.IsNullOrEmpty("" + _tbl2Row[di.Field])) continue;
                                result[di.Name.ToUpper()] = _tbl2Row[di.Field];
                            }
                        }
                        else if (di.Ptype.ToUpper() == "PARENT")
                        {

                        }
                            // Tuanmh 23/08/2017
                        else if (di.Ptype.ToUpper() == "ONEVALUE")
                        {
                            result[di.Name.ToUpper()] = di.Fname;
                        }
                        else if (di.Ptype.ToUpper() == "FILTER")
                        {
                            var lineKey = "line" + di.Field.ToUpper();
                            if (lineList.ContainsKey(lineKey))
                            {
                                var line = lineList[lineKey];
                                if (line.IsSelected)
                                {
                                    //Bỏ qua giá trị rỗng.
                                    if (di.NotEmpty && string.IsNullOrEmpty("" + line.ObjectValue)) continue;
                                    result[di.Name] = line.ObjectValue;
                                }
                                else
                                {
                                    if (di.NotEmpty) continue;

                                    if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                        result[di.Name] = 0;
                                    else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                        result[di.Name] = new DateTime(1900, 1, 1);
                                    else
                                        result[di.Name] = "";
                                }
                            }
                        }
                        else if (di.Ptype.ToUpper() == "FILTER_BROTHER")
                        {
                            var lineKey = "line" + di.Field.ToUpper();
                            if (lineList.ContainsKey(lineKey))
                            {
                                var line = lineList[lineKey];
                                if (line is FilterLineVvarTextBox)
                                {
                                    var lineV = line as FilterLineVvarTextBox;

                                    var vvar_data = lineV.VvarTextBox.Data;
                                    if (line.IsSelected == false)
                                    {
                                        vvar_data = null;
                                    }

                                    if (vvar_data != null && vvar_data.Table.Columns.Contains(di.Fname))
                                    {
                                        if (line.IsSelected)
                                        {
                                            //Bỏ qua giá trị rỗng.
                                            if (di.NotEmpty && string.IsNullOrEmpty("" + line.ObjectValue)) continue;
                                            result[di.Name] = vvar_data[di.Fname];
                                        }
                                        else
                                        {
                                            if (di.NotEmpty) continue;

                                            if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                                result[di.Name] = 0;
                                            else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                                result[di.Name] = new DateTime(1900, 1, 1);
                                            else
                                                result[di.Name] = "";
                                        }
                                    }
                                    else
                                    {
                                        if (di.NotEmpty) continue;
                                        // Tuanmh Null loi
                                        if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                            result[di.Name] = 0;
                                        else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                            result[di.Name] = new DateTime(1900, 1, 1);
                                        else
                                            result[di.Name] = "";
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.WriteToLog(GetType() + ".GetRprParametersD else", "di.Ptype?");
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".GetRptParametersD try2", ex);
                }
                return result;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetRptParametersD", ex);
                return null;
            }
        }

        /// <summary>
        /// Trả về từ điển dạng &lt;"lineField",lineControl&gt;
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, FilterLineBase> GetFilterLineList()
        {
            return GetFilterLineListRecursive(this);
        }

        public SortedDictionary<string,FilterLineBase> GetFilterLineListRecursive(Control control)
        {
            SortedDictionary<string, FilterLineBase> result = new SortedDictionary<string, FilterLineBase>();
            foreach (Control control1 in control.Controls)
            {
                var line = control1 as FilterLineBase;
                try
                {
                    if (line != null)
                    {
                        result.Add("line" + line.FieldName.ToUpper(), line);
                    }
                    else
                    {
                        result.AddRange(GetFilterLineListRecursive(control1));
                    }
                }
                catch (Exception ex)
                {
                    DoNothing();
                }
            }
            return result;
        }
    }
}
