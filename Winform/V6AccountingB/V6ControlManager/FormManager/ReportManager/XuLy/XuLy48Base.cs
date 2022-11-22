using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Export;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class XuLy48Base : V6FormControl
    {
        #region Biến toàn cục
        private AldmConfig _aldmConfig;
        private string[] _KEY_FIELDS;
        public SortedDictionary<string, string> HideFields = new SortedDictionary<string, string>();
        public SortedDictionary<string, string> ReadOnlyFields = new SortedDictionary<string, string>();
        private readonly IDictionary<string, object> _defaultData = new Dictionary<string, object>();
        /// <summary>
        /// Các trường thay đổi sau khi xử lý tính toán. Khi xử lý update sẽ kết hợp thêm trường hiện tại (currentColumn).
        /// </summary>
        public List<string> updateFieldList = new List<string>();
        protected readonly bool _updateDatabase = true;
        public bool HaveChange { get; set; }

        protected List<DataGridViewRow> remove_list_g = new List<DataGridViewRow>();
        protected List<DataRow> remove_list_d = new List<DataRow>();

        public string _reportProcedure;
        protected string _reportFile;
        protected string _program, _reportCaption, _reportCaption2;
        protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        private DataTable MauInData;
        
        public AlbcConfig _albcConfig = new AlbcConfig();

        /// <summary>
        /// Danh sách event_method của Form_program.
        /// </summary>
        private Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        private Type Form_program;
        protected Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        public FilterBase FilterControl { get; set; }
        public void AddFilterControl(string program)
        {
            FilterControl = Filter.Filter.GetFilterControl(program, _reportProcedure, _reportFile, toolTipV6FormControl);
            panel1.Controls.Add(FilterControl);
            //FilterControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            FilterControl.Focus();
        }
        /// <summary>
        /// Gọi hàm động trong Event_Methods theo tên Event trên form.
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        protected object InvokeFormEvent(string eventName)
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(eventName))
                {
                    var method_name = Event_Methods[eventName];
                    return V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
            }
            return null;
        }

        private void CreateFormProgram()
        {
            try
            {
                var albcConfig_program = ConfigManager.GetAlbcConfigByMA_FILE(_program); // ma_file = program.
                if (albcConfig_program.NoInfo) return;
                var xml = albcConfig_program.MMETHOD;
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
                Form_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "M" + _program, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
            }
        }

        private void CreateFormControls()
        {
            try
            {
                //var M_COMPANY_BY_MA_DVCS = V6Options.ContainsKey("M_COMPANY_BY_MA_DVCS") ? V6Options.GetValue("M_COMPANY_BY_MA_DVCS").Trim() : "";
                //if (M_COMPANY_BY_MA_DVCS == "1" && V6Login.MadvcsCount == 1)
                //{
                //    var dataRow = V6Setting.DataDVCS;
                //    var GET_FIELD = "TEN_NLB";
                //    if (dataRow.Table.Columns.Contains(GET_FIELD))
                //        txtM_TEN_NLB.Text = V6Setting.DataDVCS[GET_FIELD].ToString();
                //    GET_FIELD = "TEN_NLB2";
                //    if (dataRow.Table.Columns.Contains(GET_FIELD))
                //        txtM_TEN_NLB2.Text = V6Setting.DataDVCS[GET_FIELD].ToString();
                //}

                FilterControl = QuickReportManager.AddFilterControl44Base(_program, _reportProcedure, _reportFile, panel1, toolTipV6FormControl);
                All_Objects["thisForm"] = this;
                InvokeFormEvent(FormDynamicEvent.AFTERADDFILTERCONTROL);
                //QuickReportManager.MadeFilterControls(FilterControl, _program, All_Objects, toolTipV6FormControl);
                FilterControl.MadeFilterControls(_program, All_Objects, toolTipV6FormControl);
                All_Objects["thisForm"] = this;
                SetStatus2Text();
                //gridViewSummary1.Visible = FilterControl.ViewSum;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateFormControls", ex);
            }
        }

        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        protected List<SqlParameter> _pList;

        public bool ViewDetail { get; set; }

        public DataRow MauInSelectedRow
        {
            get
            {
                return MauInData == null || MauInData.Rows.Count == 0 ? null : MauInData.Rows[0];
            }
        }

        #region ===== EXTRA_INFOR =====
        public SortedDictionary<string, string> EXTRA_INFOR
        {
            get
            {
                //if (_extraInfor == null || _extraInfor.Count == 0)
                {
                    GetExtraInfor();
                }
                return _extraInfor;
            }
        }

        private SortedDictionary<string, string> _extraInfor = null;

        private void GetExtraInfor()
        {
            _extraInfor = new SortedDictionary<string, string>();
            if (MauInSelectedRow == null) return;
            _extraInfor.AddRange(ObjectAndString.StringToStringDictionary("" + MauInSelectedRow["EXTRA_INFOR"]));
        }

        #endregion EXTRA_INFOR

        private string Report_GRDSV1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    
                    result = MauInData.Rows[0]["GRDS_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDSV2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    
                    result = MauInData.Rows[0]["GRDS_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDFV1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    
                    result = MauInData.Rows[0]["GRDF_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDFV2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    
                    result = MauInData.Rows[0]["GRDF_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHV_V1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    
                    result = MauInData.Rows[0]["GRDHV_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHE_V1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    
                    result = MauInData.Rows[0]["GRDHE_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHV_V2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    
                    result = MauInData.Rows[0]["GRDHV_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDHE_V2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    
                    result = MauInData.Rows[0]["GRDHE_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDT_V1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDT_V1"].ToString().Trim();
                }
                return result;
            }
        }
        private string Report_GRDT_V2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["GRDT_V2"].ToString().Trim();
                }
                return result;
            }
        }
        private string ReportTitle
        {
            get
            {
                var result = V6Setting.IsVietnamese ? _reportCaption : _reportCaption2;
                if (MauInSelectedRow != null)
                {
                    result = MauInSelectedRow["Title"].ToString().Trim();
                }
                return result;
            }
        }
        #endregion 
        public XuLy48Base()
        {
            InitializeComponent();
        }

        public XuLy48Base(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2, bool viewDetail,
            string reportFileF5 = "", string reportTitleF5 = "", string reportTitle2F5 = "")
        {
            m_itemId = itemId;
            _program = program;
            _reportProcedure = reportProcedure;
            _reportFile = reportFile;
            _reportCaption = reportCaption;
            _reportCaption2 = reportCaption2;

            _reportFileF5 = reportFileF5;
            _reportTitleF5 = reportTitleF5;
            _reportTitle2F5 = reportTitle2F5;

            ViewDetail = viewDetail;
            
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                Text = _reportCaption;
                All_Objects["thisForm"] = this;
                
                GetConfig();
                CreateFormProgram();
                CreateFormControls();   //AddFilterControl(_program);
                if (((ReportFilter44Base) FilterControl).alreportConfig.NoInfo)
                {
                    FilterControl.Dispose();
                    AddFilterControl(_program);
                }
                AddAllControlsToAll_Objects();
                //ApplyConfigSetting();
                InvokeFormEvent(FormDynamicEvent.AFTERADDFILTERCONTROL);
                if (ViewDetail)
                    ShowDetailGridView();
                //else
                //    FixGridView1();

                LoadComboboxSource();

                string key3 = "1";
                var menuRow = V6Menu.GetRowByMact(ItemID);
                if (menuRow != null)
                {
                    key3 = ("" + menuRow["Key3"]).Trim().ToUpper();
                    if (key3 == "") key3 = "1";
                }

                if (!V6Login.IsAdmin)
                {
                    if (menuRow != null)
                    {
                        var user_acc = V6Login.UserInfo["USER_ACC"].ToString().Trim();
                        if (user_acc != "1")
                        {
                            //if (!key3.Contains("1")) exportToExcelTemplate.Visible = false;
                            //if (!key3.Contains("2")) exportToExcelView.Visible = false;
                            if (!key3.Contains("3")) exportToExcelMenu.Visible = false;
                            // if (!key3.Contains("4")) exportToXmlToolStripMenuItem.Visible = false;
                            if (!key3.Contains("5")) printGridMenu.Visible = false;
                            //if (!key3.Contains("6")) viewDataToolStripMenuItem.Visible = false;
                            //if (!key3.Contains("7")) exportToPdfToolStripMenuItem.Visible = false;
                        }
                    }
                }

                if (key3.Length > 0)
                    switch (key3[0])
                    {
                        //case '1': DefaultMenuItem = exportToExcelTemplateMenu; break;
                        //case '2': DefaultMenuItem = exportToExcelViewMenu; break;
                        case '3': DefaultMenuItem = exportToExcelMenu; break;
                        //case '4': DefaultMenuItem = exportToXmlMenu; break;
                        case '5': DefaultMenuItem = printGridMenu; break;
                        //case '6': DefaultMenuItem = viewDataMenu; break;
                        //case '7': DefaultMenuItem = exportToPdfMenu; break;
                    }

                if (EXTRA_INFOR.ContainsKey("ENTER2TAB"))
                {
                    dataGridView1.enter_to_tab = ObjectAndString.ObjectToBool(EXTRA_INFOR["ENTER2TAB"]);
                    dataGridView2.enter_to_tab = dataGridView1.enter_to_tab;
                }

                InvokeFormEvent(FormDynamicEvent.INIT);
                Ready();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _message), ex);
            }
        }

        private void GetConfig()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig(_program);

                if (_aldmConfig.HaveInfo)
                {
                    Text = _aldmConfig.TITLE;
                    if (!string.IsNullOrEmpty(_aldmConfig.TABLE_KEY))
                    {
                        var T_KEYS = ObjectAndString.SplitString(_aldmConfig.TABLE_KEY.ToUpper());
                        var list = new List<string>();
                        foreach (string KEY in T_KEYS)
                        {
                            if (!list.Contains(KEY)) list.Add(KEY);
                        }
                        _KEY_FIELDS = list.ToArray();
                    }
                }

                //if (string.IsNullOrEmpty(_showFields) && _config.HaveInfo)
                //{
                //    _showFields = _config.GRDS_V1;
                //}
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        public void ApplyConfigExtra_GRD_ALLOW(AldmConfig config)
        {
            try
            {
                // Cho phép gridview thêm sửa xóa
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToDeleteRows = false;

                if (config.NoInfo) return;

                if (config.EXTRA_INFOR.ContainsKey("GRD_ALLOW"))
                {
                    var grd_allow = config.EXTRA_INFOR["GRD_ALLOW"].Trim();
                    if (grd_allow.Length > 0) dataGridView1.AllowUserToAddRows = grd_allow[0] == '1';
                    if (grd_allow.Length > 1)
                    {
                        if (grd_allow[1] == '1')
                        {
                            dataGridView1.ReadOnly = false;
                            dataGridView1.ChangeEditColumnType(config.FIELD);
                        }
                    }
                    if (grd_allow.Length > 2) dataGridView1.AllowUserToDeleteRows = grd_allow[2] == '1';
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _message), ex);
            }
        }

        private ToolStripMenuItem DefaultMenuItem = null;

        private void AddAllControlsToAll_Objects()
        {
            var all = V6ControlFormHelper.GetAllControls(this);
            foreach (Control c in all)
            {
                All_Objects[c.Name] = c;
            }
        }

        private void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_reportFile, "", "", "");
            _albcConfig = new AlbcConfig(MauInSelectedRow.ToDataDictionary());
        }

        private void FixGridViewSize()
        {
            try
            {
                if (!ViewDetail) dataGridView1.Height = Height - 30;
                dataGridView1.Width = Width - dataGridView1.Left - 3;
                dataGridView2.Width = dataGridView1.Width;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixGridViewSize", ex);
            }
        }

        void XuLy48Base_SizeChanged(object sender, EventArgs e)
        {
            FixGridViewSize();
        }

        private void ShowDetailGridView()
        {
            try
            {
                dataGridView2.Height = Height/2 - 10;
                dataGridView2.Top = Height / 2 + 10;
                dataGridView2.Visible = true;
                dataGridView1.Height = Height / 2 - 20;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadDefaultData(4, "", _reportProcedure, m_itemId, "");
            LoadTag(4, "", _reportProcedure, m_itemId, "");
            InvokeFormEvent(FormDynamicEvent.INIT2);
            GetSumCondition();
        }

        private void GetSumCondition()
        {
            try
            {
                gridViewSummary1.NoSumColumns = Report_GRDT_V1;
                if (MauInSelectedRow != null)
                {
                    gridViewSummary1.SumCondition = new Condition
                    (
                        MauInSelectedRow["FIELD_S"].ToString().Trim(),
                        MauInSelectedRow["OPER_S"].ToString().Trim(),
                        MauInSelectedRow["VALUE_S"].ToString().Trim()
                    );
                    if (!string.IsNullOrEmpty(gridViewSummary1.SumConditionString)) toolTipV6FormControl.SetToolTip(gridViewSummary1, gridViewSummary1.SumConditionString);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetSumCondition", ex);
            }
        }

        protected void XuLyCongThucTinhToanAll(DataGridViewRow cRow, string EDIT_FIELD, string CHANGE_FIELD, AldmConfig config)
        {
            try
            {
                if (!config.HaveInfo) return;
                if (UpdateFieldOnRight(CHANGE_FIELD, config.CACH_TINH1, config)) XuLyCongThucTinhToan1(cRow, EDIT_FIELD, config.CACH_TINH1, config);
                if (UpdateFieldOnRight(CHANGE_FIELD, config.CACH_TINH2, config)) XuLyCongThucTinhToan1(cRow, EDIT_FIELD, config.CACH_TINH2, config);
                if (UpdateFieldOnRight(CHANGE_FIELD, config.CACH_TINH3, config)) XuLyCongThucTinhToan1(cRow, EDIT_FIELD, config.CACH_TINH3, config);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XyLyCongThucTinhToan", ex);
            }
        }

        private void XuLyCongThucTinhToan1(DataGridViewRow cRow, string EDIT_FIELD, string congThuc, AldmConfig config)
        {
            var ss = congThuc.Split('=');
            if (ss.Length == 2)
            {
                var CACULATED_FIELD = ss[0].Trim().ToUpper();
                if (CACULATED_FIELD == EDIT_FIELD) return;

                updateFieldList.Add(CACULATED_FIELD);
                var bieu_thuc = ss[1].Trim();

                //var cRow = dataGridView1.CurrentRow;
                if (cRow != null) cRow.Cells[CACULATED_FIELD].Value = Number.GiaTriBieuThuc(bieu_thuc, cRow.ToDataDictionary());
                XuLyCongThucTinhToanAll(cRow, EDIT_FIELD, CACULATED_FIELD, config);
            }
        }

        /// <summary>
        /// Kiểm tra EDIT_FIELD có nằm bên phải dấu = hay không?
        /// </summary>
        /// <param name="UPDATE_FIELD"></param>
        /// <param name="congThuc"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private bool UpdateFieldOnRight(string UPDATE_FIELD, string congThuc, AldmConfig config)
        {
            if (!config.HaveInfo) return false;
            if (!string.IsNullOrEmpty(congThuc) && congThuc.IndexOf(UPDATE_FIELD, congThuc.IndexOf("=", StringComparison.Ordinal), StringComparison.Ordinal) >= 0)
                return true;
            
            return false;
        }

        /// <summary>
        /// Khi sửa thành công, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        /// <param name="data">Dữ liệu đã sửa</param>
        private void f_UpdateSuccess(IDictionary<string, object> data)
        {
            try
            {
                HaveChange = true;
                if (data == null) return;
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                V6ControlFormHelper.UpdateGridViewRow(row, data);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".f_UpdateSuccess", ex);
            }
        }

        string delete_info = "";

        /// <summary>
        /// Thêm dữ liệu vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IDictionary<string, object> AddData(IDictionary<string, object> data)
        {
            try
            {
                SetStatusText(V6Text.Add + _aldmConfig.TABLE_NAME);

                //Gán dữ liệu mặc định
                if (_defaultData != null)
                {
                    data.AddRange(_defaultData, true);
                }
                //Remove UID in data
                if (data.ContainsKey("UID")) data.Remove("UID");
                //Tạo keys giả
                IDictionary<string, object> keys = new Dictionary<string, object>();
                foreach (string field in _KEY_FIELDS)
                {
                    var FIELD = field.ToUpper();
                    if (FIELD != "UID")
                    {
                        object value = "0";
                        if (_defaultData != null && _defaultData.ContainsKey(FIELD))
                        {
                            value = _defaultData[FIELD];
                        }
                        data[FIELD] = value;
                        keys.Add(FIELD, value);
                    }
                }
                //Full string keys neu key rong
                if (keys.Count == 0)
                {
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        string FIELD = column.DataPropertyName.ToUpper();
                        if (FIELD != "TIME0" && FIELD != "TIME2"
                            && column.ValueType == typeof(string))
                        {
                            if (data.ContainsKey(FIELD)) keys[FIELD] = data[FIELD];
                        }
                    }
                    //keys.AddRange(data);
                }

                var result = V6BusinessHelper.Insert(_aldmConfig.TABLE_NAME, data);
                if (result)
                {
                    HaveChange = true;
                    SetStatusText(string.Format("{0} {1}", _aldmConfig.TABLE_NAME, V6Text.AddSuccess));

                    var selectResult = V6BusinessHelper.Select(_aldmConfig.TABLE_NAME, keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        return selectResult.Data.Rows[0].ToDataDictionary();
                    }
                    else if (selectResult.TotalRows > 1)
                    {
                        SetStatusText("Có 2 dòng gần giống nhau.");
                        return selectResult.Data.Rows[selectResult.TotalRows - 1].ToDataDictionary();
                    }
                    else
                    {
                        SetStatusText("Dữ liệu không xác định.");
                    }
                }
            }
            catch (Exception ex)
            {
                SetStatusText(string.Format("{0} {1}", _aldmConfig.TABLE_NAME, V6Text.AddFail));
                this.WriteExLog(GetType() + ".AddData", ex);
            }
            return null;
        }

        private object _cellBeginEditValue;

        /// <summary>
        /// Update dữ liệu vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="row">Dòng thực hiện updateDatabase</param>
        /// <param name="columnIndex">Cột đang sửa.</param>
        /// <param name="config">Cấu hình aldm.</param>
        protected void UpdateData(DataGridViewRow row, int columnIndex, AldmConfig config)
        {
            if (config == null || config.NoInfo)
            {
                SetStatusText("No config");
                return;
            }
            var update_info = "";
            string[] _KEY_FIELDS = ObjectAndString.SplitString(config.TABLE_KEY);
            try
            {
                SetStatusText(string.Format("{0} {1}", V6Text.Edit, config.TABLE_NAME));
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                foreach (string field in _KEY_FIELDS)
                {
                    var currentKeyFieldColumnIndex = row.Cells[field].ColumnIndex;
                    if (columnIndex == currentKeyFieldColumnIndex)
                    {
                        keys.Add(field.ToUpper(), _cellBeginEditValue);
                    }
                    else
                    {
                        keys.Add(field.ToUpper(), row.Cells[field].Value);
                    }
                }

                SortedDictionary<string, object> updateData = new SortedDictionary<string, object>();
                var UPDATE_FIELD = dataGridView1.Columns[columnIndex].DataPropertyName.ToUpper();
                var update_value = row.Cells[columnIndex].Value;
                updateData.Add(UPDATE_FIELD, update_value);
                update_info += UPDATE_FIELD + " = " + ObjectAndString.ObjectToString(update_value);
                foreach (string FIELD in updateFieldList)
                {
                    update_value = row.Cells[FIELD].Value;
                    updateData[FIELD] = update_value;
                    update_info += "  " + FIELD + " = " + ObjectAndString.ObjectToString(update_value);
                }

                var result = V6BusinessHelper.UpdateSimple(config.TABLE_NAME, updateData, keys);
                if (result > 0)
                {
                    HaveChange = true;
                    SetStatusText(string.Format("{0} {1} {2}", config.TABLE_NAME, V6Text.EditSuccess, update_info));
                }
            }
            catch (Exception ex)
            {
                SetStatusText(string.Format("{0} {1} {2}", config.TABLE_NAME, V6Text.EditFail, update_info));
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }


        /// <summary>
        /// Xóa dữ liệu trong cơ sở dữ liệu, nếu thành công xóa luôn trên dataGridview.
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="rowIndex"></param>
        private void DeleteData(IDictionary<string, object> keys, int rowIndex)
        {
            try
            {
                SetStatusText(string.Format("{0} {1} {2}", V6Text.Delete, _aldmConfig.TABLE_NAME, delete_info));
                if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                {
                    var result = V6BusinessHelper.Delete(_aldmConfig.TABLE_NAME, keys);
                    if (result > 0)
                    {
                        HaveChange = true;
                        dataGridView1.Rows.RemoveAt(rowIndex);
                        SetStatusText(string.Format("{0} {1} {2}", _aldmConfig.TABLE_NAME, V6Text.DeleteSuccess, delete_info));
                    }
                }
            }
            catch (Exception ex)
            {
                SetStatusText(string.Format("{0} {1} {2}", _aldmConfig.TABLE_NAME, V6Text.DeleteFail, delete_info));
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }

        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (_executing)
            {
                return;
            }

            try
            {
                FormManagerHelper.HideMainMenu();
                btnNhanImage = btnNhan.Image;
                MakeReport2();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ReportError\n" + ex.Message);
            }
        }

        /// <summary>
        /// Lấy các tham số cho proc từ filterControl vào _pList
        /// </summary>
        /// <returns></returns>
        protected bool GenerateProcedureParameters()
        {
            try
            {
                _pList = new List<SqlParameter>();
                _pList.AddRange(FilterControl.GetFilterParameters());
                
                return true;
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("GenerateProcedureParameters: " + ex.Message);
                return false;
            }
        }

        protected void LockButtons()
        {
            btnNhan.Enabled = false;
            btnHuy.Enabled = false;
        }

        protected void UnlockButtons()
        {
            btnNhan.Enabled = true;
            btnHuy.Enabled = true;
        }

        #region ==== LoadData MakeReport ====

        void LoadData()
        {
            All_Objects["_plist"] = _pList;
            object beforeLoadData = InvokeFormEvent(FormDynamicEvent.BEFORELOADDATA);
            try
            {
                if (beforeLoadData != null && !ObjectAndString.ObjectToBool(beforeLoadData))
                {
                    _message = V6Text.CheckInfor;
                    _executing = false;
                    return;
                }

                _executing = true;
                _executesuccess = false;
                _ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, _pList.ToArray());
                if (_ds.Tables.Count > 0)
                {
                    _tbl = _ds.Tables[0];
                    _tbl.TableName = "DataTable1";
                }
                
                //if (this is AAPPR_SOA1)
                //{
                //    DoNothing();
                //}
                //else
                if (_ds.Tables.Count > 1)
                {
                    _tbl2 = _ds.Tables[1];
                    _tbl2.TableName = "DataTable2";
                }
                else
                {
                    _tbl2 = null;
                }
                
                _executesuccess = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadData XuLy48Base", ex);
                _message = ex.Message;
                _tbl = null;
                _tbl2 = null;
                _ds = null;
                _executesuccess = false;
            }
            _executing = false;
        }

        void TinhToan()
        {
            try
            {
                _executing = true;
                V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, _pList.ToArray());
                
                _executesuccess = true;
                _executing = false;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhToan!\n" + ex.Message);
                _executing = false;
                _executesuccess = false;
            }
        }
        /// <summary>
        /// GenerateProcedureParameters();//Add các key
        /// var tLoadData = new Thread(LoadData);
        /// tLoadData.Start();
        /// timerViewReport.Start();
        /// </summary>
        protected virtual void MakeReport2()
        {
            if (GenerateProcedureParameters()) //Add các key khác
            {
                CheckForIllegalCrossThreadCalls = false;
                _executing = true;
                _executesuccess = false;
                if (Load_Data)
                {
                    var tLoadData = new Thread(LoadData);
                    tLoadData.Start();
                    timerViewReport.Start();
                }
                else
                {
                    // Tinh toan
                    var tLoadData = new Thread(TinhToan);
                    tLoadData.Start();
                    timerViewReport.Start();
                }
            }
        }

        protected bool Load_Data = true;
        protected virtual void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    FilterControl.LoadDataFinish(_ds);
                    All_Objects["_ds"] = _ds;
                    InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                    if (Load_Data)
                    {
                        dataGridView1.SetFrozen(0);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = _tbl;
                        
                        //FormatGridViewBase();
                        FormatGridView();
                        FormatGridViewExtern();
                        dataGridView1.Focus();
                    }
                    else
                    {
                        //Thong bao tinh toan xong
                        V6ControlFormHelper.ShowMessage(V6Text.Finish);
                    }
                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();

                    _executesuccess = false;
                    this.ShowErrorException(GetType() + ".TimerView", ex);
                }
            }
            else if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                ShowMainMessage(_message);
            }
        }

        /// <summary>
        /// Hàm format luôn chạy trong base sau khi set dữ liệu cho grid 1.
        /// </summary>
        protected void FormatGridViewBase()
        {
            //Header
            if (_tbl != null)
            {
                //V6ControlFormHelper.FormatGridViewBoldColor(dataGridView1, _program);
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                        _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
                }
                var fieldList = (from DataColumn column in _tbl.Columns select column.ColumnName).ToList();
                var fieldDic = CorpLan2.GetFieldsHeader(fieldList);
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (fieldDic.ContainsKey(dataGridView1.Columns[i].DataPropertyName.ToUpper()))
                    {
                        dataGridView1.Columns[i].HeaderText =
                            fieldDic[dataGridView1.Columns[i].DataPropertyName.ToUpper()];
                    }
                }
                //Format
                var f = dataGridView1.Columns["so_luong"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
                }
                f = dataGridView1.Columns["TIEN2"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
                }
                f = dataGridView1.Columns["GIA2"];
                if (f != null)
                {
                    f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
                }
            }

            //Format mới
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1, V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);
            if (ViewDetail) V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, Report_GRDSV2, Report_GRDFV2, V6Setting.IsVietnamese ? Report_GRDHV_V2 : Report_GRDHE_V2);
            if (FilterControl != null) FilterControl.FormatGridView(dataGridView1);
            if (MauInSelectedRow != null)
            {
                int frozen = ObjectAndString.ObjectToInt(MauInSelectedRow["FROZENV"]);
                dataGridView1.SetFrozen(frozen);
            }
        }

        private void FormatGridView()
        {
            try
            {
                if (_aldmConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1, V6Setting.IsVietnamese ? _aldmConfig.GRDHV_V1 : _aldmConfig.GRDHE_V1);
                }

                if (!string.IsNullOrEmpty(_aldmConfig.GRDS_V1))
                {
                    var showFieldSplit = ObjectAndString.SplitString(_aldmConfig.GRDS_V1);
                    var showFieldList = new List<string>();
                    foreach (string field in showFieldSplit)
                    {
                        if (field.Contains(":"))
                        {
                            var ss = field.Split(':');
                            string FIELD = ss[0].Trim().ToUpper();
                            // Rigth hide-readonly
                            if (HideFields.ContainsKey(FIELD)) continue;

                            showFieldList.Add(FIELD);
                            //GetHeader(FIELD);
                            var column = dataGridView1.Columns[FIELD];
                            if (column != null && (ss[1].ToUpper() == "R" || ReadOnlyFields.ContainsKey(FIELD)))
                            {
                                column.ReadOnly = true;
                            }
                        }
                        else
                        {
                            string FIELD = field.Trim().ToUpper();
                            if (HideFields.ContainsKey(FIELD)) continue;
                            showFieldList.Add(FIELD);
                            //GetHeader(FIELD);

                            if (ReadOnlyFields.ContainsKey(FIELD))
                            {
                                var column = dataGridView1.Columns[FIELD];
                                if (column != null) column.ReadOnly = true;
                            }
                        }
                    }

                    dataGridView1.ShowColumns(true, showFieldList.ToArray());
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".FormatGridView " + ex.Message);
            }
        }

        /// <summary>
        /// Hàm luôn gọi sau hàm FormatGridViewBase.
        /// </summary>
        protected virtual void FormatGridViewExtern()
        {
            //try
            //{
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (row.Cells["TS0"].ToString() == "1")
            //            row.DefaultCellStyle.Font = new Font(Parent.Font, FontStyle.Bold);

            //        row.Select();
            //        row.DefaultCellStyle.BackColor = Color.FromArgb(247, 192, 91);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowErrorMessage(GetType() + ".FormatGridViewExtern: " + ex.Message);
            //}
        }

        /// <summary>
        /// Chỉ định các trường ẩn đi bắt buộc.
        /// </summary>
        /// <param name="fields"></param>
        public void SetHideFields(params string[] fields)
        {
            foreach (string field in fields)
            {
                string FIELD = ObjectAndString.SplitStringBy(field, ':')[0].Trim().ToUpper();
                HideFields[FIELD] = FIELD;
            }
        }

        #endregion ==== LoadData MakeReport ====

        
        
        
        

        #region Linh tinh        

        public bool IsRunning
        {
            get { return _executing || f9Running; }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                ShowMainMessage(V6Text.ProcessNotComplete);
                return;
            }
            Dispose();
        }
        
        #endregion Linh tinh

        private string GetExportFileName()
        {
            string result = ChuyenMaTiengViet.ToUnSign(ReportTitle);
            if (EXTRA_INFOR.ContainsKey("EXPORT")) result = EXTRA_INFOR["EXPORT"];
            // Value
            if (_tbl2 != null && _tbl2.Rows.Count > 0)
            {
                var am_data = _tbl2.Rows[0].ToDataDictionary();
                var regex = new Regex("{(.+?)}");
                foreach (Match match in regex.Matches(result))
                {
                    var matchGroup0 = match.Groups[0].Value;
                    var matchContain = match.Groups[1].Value;
                    var matchColumn = matchContain.ToUpper();
                    var matchFormat = "";
                    if (matchContain.Contains(":"))
                    {
                        int _2dotIndex = matchContain.IndexOf(":", StringComparison.InvariantCulture);
                        matchColumn = matchContain.Substring(0, _2dotIndex).ToUpper();
                        matchFormat = matchContain.Substring(_2dotIndex + 1);
                    }
                    if (am_data.ContainsKey(matchColumn)
                        && am_data[matchColumn] is DateTime && matchFormat == "")
                    {
                        matchFormat = "yyyMMdd";
                    }
                    result = result.Replace(matchGroup0, am_data.ContainsKey(matchColumn) ? ObjectAndString.ObjectToString(am_data[matchColumn], matchFormat).Trim() : "");
                }
            }
            // remove any invalid character from the filename.  
            result = Regex.Replace(result.Trim(), "[^A-Za-z0-9_. ]+", "");
            return result;
        }

        private void exportToExcelMenu_Click(object sender, EventArgs e)
        {
            var data = dataGridView1.Focused ? _tbl : _tbl2;
            if (data == null)
            {
                ShowMainMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = @"Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    FileName = GetExportFileName()
                };
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        V6Tools.V6Export.ExportData.ToExcel(data, new ExportExcelSetting(), save.FileName, _reportCaption, true);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorException(GetType() + ".ExportFail", ex);
                        return;
                    }
                    this.ShowInfoMessage(V6Text.ExportFinish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ExportFail", ex);
            }
        }

        private void printGrid_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                ShowMainMessage(V6Text.NoData);
                return;
            }
            try
            {
                V6ControlFormHelper.PrintGridView(dataGridView1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".PrintGrid", ex);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            var aControl = ActiveControl;
            if (aControl is V6VvarTextBox)
            {
                return true;
            }

            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.E))
            {
                btnExport3_Click(null, null);
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            else if ((keyData & Keys.F3) == Keys.F3 && FilterControl.F3)
            {
                XuLyHienThiFormSuaChungTuF3();
            }
            else if ((keyData & Keys.F4) == Keys.F4)// && FilterControl.F4)
            {
                XuLyBoSungThongTinChungTuF4();
            }
            //else if (keyData == Keys.F5 && FilterControl.F5)
            //{
            //    if(dataGridView1.Focused || dataGridView2.Focused) XuLyXemChiTietF5();
            //    else return base.DoHotKey0(keyData);
            //}
            else if (keyData == Keys.F6 && FilterControl.F6)
            {
                XuLyF6();
            }
            else if ((keyData & Keys.F7) == Keys.F7 && FilterControl.F7)
            {
                XuLyF7();
            }
            else if (keyData == Keys.F8 && FilterControl.F8)
            {
                XuLyF8();
            }
            if (keyData == Keys.Delete && dataGridView1.AllowUserToDeleteRows && !dataGridView1.IsCurrentCellInEditMode)
            {
                if (!_updateDatabase)
                {
                    if (dataGridView1.CurrentRow != null) dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    //toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.DeleteSuccess, delete_info);
                    return true;
                }
                if (dataGridView1.CurrentRow != null)
                {
                    var selectedRowIndex = dataGridView1.CurrentRow.Index;
                    if (selectedRowIndex < dataGridView1.NewRowIndex)
                    {
                        //var rowData = dataGridView1.CurrentRow.ToDataDictionary();
                        var keys = new SortedDictionary<string, object>();
                        delete_info = "";
                        foreach (string field in _KEY_FIELDS)
                        {
                            var UPDATE_FIELD = field.ToUpper();
                            var update_value = dataGridView1.CurrentRow.Cells[field].Value;
                            keys.Add(UPDATE_FIELD, update_value);
                            delete_info += UPDATE_FIELD + " = " + ObjectAndString.ObjectToString(update_value);
                        }
                        if (keys.Count > 0) DeleteData(keys, selectedRowIndex);
                    }
                }
                return true;
            }
            else if (keyData == Keys.F9 && FilterControl.F9)
            {
                //F9Thread();
                XuLyF9();
            }
            else if (keyData == Keys.F10 && FilterControl.F10)
            {
                XuLyF10();
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }
        
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5 && FilterControl.F5)
                {
                    if (dataGridView1.Focused) XuLyXemChiTietF5();
                }
                else if (e.KeyData == (Keys.Control | Keys.A))
                {
                    e.Handled = true;
                    XuLyCtrA();
                }
                else if (e.KeyData == (Keys.Control | Keys.U))
                {
                    XuLyCtrU();
                }
                else if (e.KeyData == (Keys.Space))
                {
                    XuLySpacebar();
                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException("XuLy48Base GridView1 KeyDown", ex);
            }
        }

        protected virtual void XuLyHienThiFormSuaChungTuF3()
        {
            try
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.F3))
                {
                    InvokeFormEvent(FormDynamicEvent.F3);
                    return;
                }

                F3(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException("XuLy48Base XuLyHienThiFormSuaChungTu", ex);
            }
        }

        
        protected virtual void XuLyBoSungThongTinChungTuF4()
        {
            if (Event_Methods.ContainsKey(FormDynamicEvent.F4))
            {
                InvokeFormEvent(FormDynamicEvent.F4);
            }
            else
            {
                F4(this);
            }
        }

#region ==== temp Code writing ====
        public void F3(Control thisForm)
        {
            try
            {
                //MessageBox.Show("Test F4. Trong form config bao cao moi (advance) chi co F3F5F7.\nTam thoi bo code check FilterControl.F4");
                //DataGridView dataGridView1 = (DataGridView) V6ControlFormHelper.GetControlByName(thisForm, "dataGridView1");
                if (dataGridView1.DataSource == null)
                {
                    this.ShowWarningMessage(V6Text.NoData);
                    return;
                }
                if (dataGridView1.CurrentRow == null)
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                    return;
                }

                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                string stt_rec = row.Cells["STT_REC"].ToString();
                IDictionary<string, object> key = new SortedDictionary<string, object>();
                IDictionary<string, object> data = row.ToDataDictionary();

                key["STT_REC"] = stt_rec;
                FormAddEdit f = new FormAddEdit("ARS82", V6Mode.Edit, key, data);
                f.AfterInitControl += f_AfterInitControlARS82;
                f.InitFormControl(this);
                f.UpdateSuccessEvent += (dataDic) =>
                {
                    try
                    {
                        V6ControlFormHelper.UpdateGridViewRow(row, dataDic);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                };
                f.ShowDialog(thisForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void F4(Control thisForm)
        {
            try
            {
                //Button btnNhan = (Button) V6ControlFormHelper.GetControlByName(thisForm, "btnNhan");
                SaveSelectedCellLocation(dataGridView1);
                string stt_rec = V6BusinessHelper.GetNewSttRec("ARC");
                SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                data["STT_REC"] = stt_rec;
                FormAddEdit f = new FormAddEdit("ARS82", V6Mode.Add, null, data);
                f.AfterInitControl += f_AfterInitControlARS82;
                f.InitFormControl(this);
                f.ShowDialog(thisForm);
                SetStatus2Text();
                if (f.InsertSuccess)
                {
                    //var data = f.FormControl.DataDic;
                    try
                    {
                        btnNhan.PerformClick();
                        LoadSelectedCellLocation(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
#endregion ==== temp Code writing ====


        protected virtual void XuLyXemChiTietF5()
        {
            var oldKeys = FilterControl.GetFilterParameters();
            var view = new XuLy48Base(m_itemId, _program + "F5", _program + "F5", _reportFile, _reportCaption, _reportCaption2, false);
            view.CodeForm = CodeForm;
            view.Dock = DockStyle.Fill;
            view.FilterControl.InitFilters = oldKeys;
            view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
            view.btnNhan_Click(null, null);
            //view.autocl
            view.ShowToForm(this, _reportCaption, true);

            SetStatus2Text();
        }

        protected virtual void XuLyF6()
        {
            try
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.F6))
                {
                    InvokeFormEvent(FormDynamicEvent.F6);
                }
                else
                {
                    //Code base
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF6", ex);
            }
        }
        
        protected virtual void XuLyF7()
        {
            try
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.F7))
                {
                    InvokeFormEvent(FormDynamicEvent.F7);
                }
                else
                {
                    //Code base
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF7", ex);
            }
        }

        protected virtual void XuLyF8()
        {
            try
            {
                if (Event_Methods.ContainsKey(FormDynamicEvent.F8))
                {
                    InvokeFormEvent(FormDynamicEvent.F8);
                }
                else
                {
                    F8();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF8", ex);
            }
        }

        private void F8()
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            SaveSelectedCellLocation(dataGridView1);

            //if (row != null)
            //{
            //    SortedDictionary<string, object> keys = new SortedDictionary<string, object> { { "UID", row.Cells["UID"].Value } };
            //    string value_show = row.Cells["STT_REC"].Value.ToString();
            //    if (V6Message.Show(V6Text.DeleteConfirm + " " + value_show, V6Text.DeleteConfirm, 0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, 0, this)
            //        == DialogResult.Yes)
            //    {
            //        int t = V6BusinessHelper.Delete("ARS82", keys);
            //        if (t > 0)
            //        {
            //            btnNhan.PerformClick();
            //            LoadSelectedCellLocation(dataGridView1);
            //        }
            //    }
            //}
        }

        #region ==== Xử lý F9 ====

        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string _oldDefaultPrinter;

        protected virtual void XuLyF9()
        {
            try
            {
                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF9_Tick;
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                f9Running = true;
                Thread t = new Thread(F9Thread);
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
                tF9.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private void F9Thread()
        {
            f9ErrorAll = "";
            InvokeFormEvent(FormDynamicEvent.F9);
            f9Running = false;
            SetStatusText("F9 end.");
        }

        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                try
                {
                    btnNhan.Image = waitingImages.Images[ii];
                    ii++;
                    if (ii >= waitingImages.Images.Count) ii = 0;

                    var cError = f9Error;
                    f9Error = f9Error.Substring(cError.Length);
                    V6ControlFormHelper.SetStatusText("F9 running "
                        + (cError.Length > 0 ? "Error: " : "")
                        + cError);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.Image = btnNhanImage;
                btnNhan.PerformClick();
                try
                {
                    PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                SetStatusText("F9 end.");
            }
        }
        #endregion xulyF9


        #region ==== Xử lý F10 ====

        private bool f10Running;
        private string f10Message = "";
        private string f10ErrorAll = "";

        protected virtual void XuLyF10()
        {
            try
            {
                Timer t10 = new Timer();
                t10.Interval = 500;
                t10.Tick += tF10_Tick;
                CheckForIllegalCrossThreadCalls = false;
                Thread t = new Thread(F10Thread);
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
                t10.Start();

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }
        private void F10Thread()
        {
            f10Running = true;
            f10ErrorAll = "";
            InvokeFormEvent(FormDynamicEvent.F10);
            f10Running = false;
        }

        void tF10_Tick(object sender, EventArgs e)
        {
            if (f10Running)
            {
                var cMessage = f10Message;
                f10Message = f10Message.Substring(cMessage.Length);
                V6ControlFormHelper.SetStatusText("F10 running " + cMessage);
            }
            else
            {
                ((Timer)sender).Stop();
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (f10ErrorAll.Length > 0 ? "Error: " : "")
                    + f10ErrorAll);

                SetStatusText("F10 " + V6Text.Finish);
            }
        }
        #endregion xulyF10

        protected virtual void XuLyCtrA()
        {
            if (!_Ctrl_A) return;
            dataGridView1.SelectAllRow();
        }

        protected virtual void XuLyCtrU()
        {
            if (!_Ctrl_U) return;
            dataGridView1.UnSelectAllRow();
        }

        protected virtual void XuLySpacebar()
        {
            if (!_SpaceBar) return;
            var row = dataGridView1.CurrentRow;
            if (row != null)
            {
                row.ChangeSelect();
            }
        }

        private void UpdateGridView2(DataGridViewRow row)
        {
            ViewDetails(row);
            FormatGridView2();
        }

        protected virtual void ViewDetails(DataGridViewRow row)
        {
            //Viết code ở lớp kế thừa
        }

        protected virtual void FormatGridView2()
        {
            //Viết code ở lớp kế thừa
        }

        public virtual void GridView1CellEndEdit_Extern(DataGridViewRow row, string FIELD, object fieldData)
        {
            //Viết code ở lớp kế thừa
        }

        public virtual void GridView2CellEndEdit(DataGridViewRow row, string FIELD, object fieldData)
        {
            //Viết code ở lớp kế thừa
        }

        /// <summary>
        /// Xóa dòng chứa trong remove_list_g trên gridView1
        /// </summary>
        protected void RemoveGridViewRow()
        {
            try
            {
                while (remove_list_g.Count > 0)
                {
                    dataGridView1.Rows.Remove(remove_list_g[0]);
                    remove_list_g.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".RemoveGridViewRow", ex);
            }
        }
        
        /// <summary>
        /// Dùng chứa dòng xóa đi trên gridView1
        /// </summary>
        protected void RemoveDataRows(DataTable table)
        {
            try
            {
                while (remove_list_d.Count > 0)
                {
                    table.Rows.Remove(remove_list_d[0]);
                    remove_list_d.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType().Name + ".RemoveGridViewRows", ex);
            }
        }

        public override void SetStatus2Text()
        {
            if (FilterControl != null) FilterControl.SetStatus2Text(_reportProcedure);
        }

        private void XuLy48Base_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (FilterControl.GridViewHideFields != null && FilterControl.GridViewHideFields.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
            }
        }
        
        protected int _oldIndex = -1;
        /// <summary>
        /// Bật tắt chức năng chọn 1 dòng trên gridView bằng spacebar
        /// </summary>
        protected bool _SpaceBar = true;
        /// <summary>
        /// Bật tắt chức năng chọn tất cả trên gridView bằng Ctrl+A
        /// </summary>
        protected bool _Ctrl_A = true;
        /// <summary>
        /// Bật tắt chức năng bỏ chọn tất cả trên gridView bằng Ctrl+U
        /// </summary>
        protected bool _Ctrl_U = true;

        
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                var row = dataGridView1.CurrentRow;
                if (ViewDetail && row != null && _oldIndex != row.Index)
                {
                    _oldIndex = row.Index;
                    UpdateGridView2(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Gridview1Select", ex);
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var index = dataGridView1.SelectedCells[0].RowIndex;
                _oldIndex = index;

                UpdateGridView2(dataGridView1.Rows[index]);
                
            }
            else
            {
                dataGridView2.DataSource = null;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellEndEdit_Virtual(sender, e);
        }

        public virtual void dataGridView1_CellEndEdit_Virtual(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            var field = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
            updateFieldList = new List<string>();
            GridView1CellEndEdit_Extern(row, field.ToUpper(), row.Cells[field].Value);
            
            // Mới thêm
            var EDIT_FIELD = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.ToUpper();
            //Xu ly cong thuc tinh toan
            XuLyCongThucTinhToanAll(row, EDIT_FIELD, EDIT_FIELD, _aldmConfig);

            if (_updateDatabase) UpdateData(row, e.ColumnIndex, _aldmConfig);
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView2.Rows[e.RowIndex];
            var field = dataGridView2.Columns[e.ColumnIndex].DataPropertyName;
            GridView1CellEndEdit_Extern(row, field.ToUpper(), row.Cells[field].Value);
        }

        private void btnThemMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (MauInSelectedRow != null) return;

                ConfirmPasswordV6 f_v6 = new ConfirmPasswordV6();
                if (f_v6.ShowDialog(this) == DialogResult.OK)
                {
                    SortedDictionary<string, object> data0 = null;
                    //var viewt = new DataView(MauInData);
                    //viewt.RowFilter = "mau='" + MAU + "'" + " and lan='" + LAN + "'";
                    var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _reportFile},
                        {"MAU", "VN"},
                        {"LAN", "V"},
                        {"REPORT", _reportFile}
                    };
                    if (MauInData == null || MauInData.Rows.Count == 0)
                    {
                        data0 = new SortedDictionary<string, object>();
                        data0.AddRange(keys);
                        data0["CAPTION"] = _reportCaption;
                        data0["CAPTION2"] = _reportCaption;
                        data0["TITLE"] = _reportCaption;
                        data0["FirstAdd"] = "1";
                    }

                    var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Add, keys, data0);
                    f2.AfterInitControl += f_AfterInitControl;
                    f2.InitFormControl(this);
                    f2.ShowDialog(this);
                    SetStatus2Text();
                    if (f2.InsertSuccess)
                    {
                        //cap nhap thong tin
                        LoadComboboxSource();
                    };
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ThemMauBC_Click: ", ex);
            }
            SetStatus2Text();
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, "Albc");
        }
        void f_AfterInitControlARS82(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, "ARS82");
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, All_Objects);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
        }

        private void btnSuaTTMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (MauInData == null || MauInData.Rows.Count == 0) return;
                var row0 = MauInData.Rows[0];
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", row0["MA_FILE"].ToString().Trim()},
                        {"MAU", row0["MAU"].ToString().Trim()},
                        {"LAN", row0["LAN"].ToString().Trim()},
                        {"REPORT", row0["REPORT"].ToString().Trim()}
                    };
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, keys, null);
                f2.AfterInitControl += f_AfterInitControl;
                f2.InitFormControl(this);
                f2.ShowDialog(this);
                SetStatus2Text();
                if (f2.UpdateSuccess)
                {
                    //cap nhap thong tin
                    var data = f2.FormControl.DataDic;
                    V6ControlFormHelper.UpdateDataRow(MauInSelectedRow, data);
                    _albcConfig = new AlbcConfig(data);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnSuaTTMauBC_Click", ex);
            }
            SetStatus2Text();
        }

        private void panel1_Leave(object sender, EventArgs e)
        {
            //btnNhan.Focus();
        }

        private void btnExport3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int index = dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None) - 1;
            if (index < 0) return;
            var row = dataGridView1.Rows[index];

            if (_updateDatabase)
            {
                var newData = row.ToDataDictionary();
                var afterData = AddData(newData);
                if (afterData != null)
                {
                    foreach (KeyValuePair<string, object> item in afterData)
                    {
                        if (dataGridView1.Columns.Contains(item.Key))
                        {
                            row.Cells[item.Key].Value = item.Value;
                        }
                    }

                    if (dataGridView1.EditingCell != null)
                    {
                        _cellBeginEditValue = dataGridView1.EditingCell.Value;
                    }
                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _cellBeginEditValue = dataGridView1.CurrentCell.Value;
        }

        private void btnSuaLine_Click(object sender, EventArgs e)
        {
            if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                var title = V6Setting.IsVietnamese ? "Sửa báo cáo động" : "Edit dynamic report";
                var f = new DanhMucView(ItemID, title, "Alreport", "ma_bc='" + _program + "'",
                    V6TableHelper.GetDefaultSortField(V6TableName.Alreport), new AldmConfig());
                f.EnableAdd = false;
                f.EnableCopy = false;
                f.EnableDelete = false;
                f.EnableFullScreen = false;

                f.ShowToForm(this, title);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnSuaLine_Click: " + ex.Message);
            }
        }

        private void btnEditGrid_Click(object sender, EventArgs e)
        {
            if (this.ShowMessage(V6Setting.IsVietnamese ? "Cho phép sửa số liệu?" : "Allow edit?") == DialogResult.OK)
            {
                ApplyConfigExtra_GRD_ALLOW(_aldmConfig);
                btnEditGrid.Enabled = false;
            }
        }

        private void thayTheMenu_Click(object sender, EventArgs e)
        {
            ThayThe_GiaTri(dataGridView1.CurrentCell);
        }


        /// <summary>
        /// Chức năng sửa hàng loạt một cột dữ liệu. Lấy giá trị ô hiện tại gán xuống các ô dòng dưới nó.
        /// </summary>
        /// <param name="currentCell"></param>
        public void ThayThe_GiaTri(DataGridViewCell currentCell)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }

                if (!_aldmConfig.EXTRA_INFOR.ContainsKey("CT_REPLACE"))
                {
                    ShowMainMessage(V6Text.NoDefine + "EXTRA_INFOR[CT_REPLACE]");
                    return;
                }
                var listFieldCanReplace = ObjectAndString.SplitString(_aldmConfig.EXTRA_INFOR["CT_REPLACE"]);

                int field_index = dataGridView1.CurrentCell.ColumnIndex;
                string FIELD = dataGridView1.CurrentCell.OwningColumn.DataPropertyName.ToUpper();
                object VALUE = dataGridView1.CurrentCell.Value;

                if (!listFieldCanReplace.Contains(FIELD))
                {
                    ShowMainMessage(V6Text.NoDefine + " CT_REPLACE:" + FIELD);
                    return;
                }

                V6ColorTextBox textBox = new V6ColorTextBox();
                if (dataGridView1.Columns[FIELD] is V6VvarDataGridViewColumn) textBox = new V6VvarTextBox();
                else if (dataGridView1.Columns[FIELD] is V6DateTimeColorGridViewColumn) textBox = new V6DateTimeColor();
                else if (dataGridView1.Columns[FIELD] is V6NumberDataGridViewColumn) textBox = new V6NumberTextBox();

                Type valueType = dataGridView1.CurrentCell.OwningColumn.ValueType;

                //Check
                if (textBox == null)
                {
                    ShowMainMessage(V6Text.Text("UNKNOWNOBJECT"));
                    return;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index <= dataGridView1.CurrentRow.Index) continue;
                    if (row.IsNewRow) continue;

                    //object newValue = ObjectAndString.ObjectTo(valueType, f.Value);
                    if (ObjectAndString.IsDateTimeType(valueType) && VALUE != null)
                    {
                        DateTime newDate = (DateTime)VALUE;
                        if (newDate < new DateTime(1700, 1, 1))
                        {
                            VALUE = null;
                        }
                    }

                    SortedDictionary<string, object> newData = new SortedDictionary<string, object>();
                    newData.Add(FIELD, VALUE);
                    V6ControlFormHelper.UpdateGridViewRow(row, newData);
                    updateFieldList = new List<string>();
                    //Xu ly cong thuc tinh toan
                    XuLyCongThucTinhToanAll(row, FIELD, FIELD, _aldmConfig);
                    // Update database
                    UpdateData(row, currentCell.ColumnIndex, _aldmConfig);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang_ThayThe ", ex);
            }
        }
    }
}
