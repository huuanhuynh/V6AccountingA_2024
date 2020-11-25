using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter.Base0
{
    public partial class XADVXNK01_Filter : FilterBase
    {
        private AldmConfig _aldmConfig;
        private XuLyBase0 _base0;

        /// <summary>
        /// Tên bảng từ AldmConfig.
        /// </summary>
        private string TableName
        {
            get { return _aldmConfig.TABLE_NAME; }
        }

        private string EditFieldFormat
        {
            get { return _aldmConfig.FIELD; }
        }

        private string[] KeyFields
        {
            get
            {
                return ObjectAndString.SplitString(_aldmConfig.KEY);
            }
        }

        private IDictionary<string, object> _defaultData = null;
        public bool HaveChange { get; set; }

        protected Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        /// <summary>
        /// Code động từ aldmConfig.
        /// </summary>
        protected Type Event_program;
        public Dictionary<string, object> All_Objects = new Dictionary<string, object>();

        public XADVXNK01_Filter()
        {
            InitializeComponent();
            
            MyInit();
        }

        void MyInit()
        {
            try
            {
                DynamicOff = true;
                Anchor = (AnchorStyles) 0xF;
                ExecuteMode = ExecuteMode.ExecuteProcedure;
                //lineMact.SetValue("POH");
                // Sau hàm này cũng đã tự có program + reportProc
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        private void XADVXNK01_Filter_Load(object sender, EventArgs e)
        {
            try
            {
                LoadConfig();
                FilterControl = AddFilterControl44Base("ADVXNK01." + _base0._reportFile, _base0._reportProcedure, panel1);
                FilterControl2 = AddFilterControl44Base2("ADVXNK01." + _base0._reportFile + "_ADD", _base0._reportProcedure, panel2);
                CreateFormProgram();

                FilterControl2.groupBox1.Text = "";
                FilterControl2.radAnd.Visible = false;
                FilterControl2.radOr.Visible = false;
                //FilterControl2
                // Right
                dataGridView1.AllowUserToAddRows = V6Login.UserRight.AllowAdd(ItemID, _base0._reportFile + "6");
                dataGridView1.ReadOnly = !V6Login.UserRight.AllowEdit(ItemID, _base0._reportFile + "6");
                InvokeFormEvent(FormDynamicEvent.INIT);
                InvokeFormEvent(FormDynamicEvent.INIT2);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XADVXNK01_Filter_Load", ex);
            }
        }

        public ReportFilter44Base FilterControl { get; set; }
        public ReportFilter44Base FilterControl2 { get; set; }

        private void LoadConfig()
        {
            try
            {
                _base0 = FindParent<XuLyBase0>() as XuLyBase0;
                if (_base0 != null)
                {
                    //this.ShowInfoMessage("Test code alkh line 59");
                    //_aldmConfig = ConfigManager.GetAldmConfig("ALKH");
                    _aldmConfig = ConfigManager.GetAldmConfig(_base0._reportFile); // Đảo program bằng reportFile.
                    // Test data
                    //var table = V6BusinessHelper.Select(_aldmConfig.TABLE_NAME, "top 100 *", "", "", "").Data;
                    //dataGridView1.DataSource = table;
                    //FormatGridView(dataGridView1);
                }
                else
                {
                    throw new Exception(V6Text.NoDefine);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException("LoadConfig", ex);
            }
        }

        public static ReportFilter44Base AddFilterControl44Base(string program, string reportProcedure, Panel panel1)
        {
            panel1.Controls.Clear();

            var FilterControl = Filter.GetFilterControl44(program, reportProcedure);
            panel1.Controls.Add(FilterControl);
            FilterControl.LoadLanguage();
            FilterControl.Focus();
            return FilterControl;
        }
        
        public static ReportFilter44Base AddFilterControl44Base2(string program, string reportProcedure, Panel panel2)
        {
            panel2.Controls.Clear();

            var FilterControl2 = Filter.GetFilterControl44(program, reportProcedure);
            panel2.Controls.Add(FilterControl2);
            FilterControl2.LoadLanguage();
            FilterControl2.Focus();
            return FilterControl2;
        }

        protected void CreateFormProgram()
        {
            try
            {
                All_Objects["thisForm"] = this;
                //DMETHOD
                if (_aldmConfig.NoInfo || string.IsNullOrEmpty(_aldmConfig.DMETHOD))
                {
                    return;
                }

                string using_text = "";
                string method_text = "";
                //foreach (DataRow dataRow in Invoice.Alct1.Rows)
                {
                    var xml = _aldmConfig.DMETHOD;
                    if (xml == "") return;
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xml));
                    if (ds.Tables.Count <= 0) return;
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

            Build:
                Event_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + _aldmConfig.MA_DM, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
            }
        }

        /// <summary>
        /// Gọi hàm động theo tên event đã định nghĩa.
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public object InvokeFormEvent(string eventName)
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(eventName))
                {
                    var method_name = Event_Methods[eventName];
                    return V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
            }
            return null;
        }

        public override List<SqlParameter> GetFilterParameters()
        {
            var result = FilterControl.GetFilterParameters();
            return result;
        }

        public override void LoadDataFinish(DataSet ds)
        {
            try
            {
                _ds = ds;
                All_Objects["_ds"] = _ds;
                InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                dataGridView1.DataSource = _ds.Tables[0];
                FormatGridView(dataGridView1);
                if (_ds.Tables.Count > 1)
                {
                    dataGridView2.DataSource = _ds.Tables[1];
                    if (_ds.Tables[1].Rows.Count > 0)
                    {
                        _defaultData = _ds.Tables[1].Rows[0].ToDataDictionary();
                        dataGridView1.Enabled = true;
                    }
                    else
                    {
                        _defaultData = new Dictionary<string, object>();
                        dataGridView1.Enabled = false;
                    }
                    SetFilterControl2Data();
                }
                else
                {
                    _defaultData = new Dictionary<string, object>();
                    dataGridView1.Enabled = false;
                }
                if (dataGridView1.RowCount > 0) btnGuiDanhSach.Enabled = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadDataFinish", ex);
            }
        }

        private void SetFilterControl2Data()
        {
            try
            {
                foreach (Control control in FilterControl2.groupBox1.Controls)
                {
                    var line = control as FilterLineBase;
                    if (line == null) continue;
                    if (!_defaultData.ContainsKey(line.FieldName.ToUpper()))
                    {
                        //line.Visible = false;
                    }
                    else
                    {
                        line.SetValue(_defaultData[line.FieldName.ToUpper()]);
                    }
                }

                FilterControl2.SortFilterLine();

                V6ControlFormHelper.SetFormDataDictionary(panel2, _defaultData);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetFilterControl2Data", ex);
            }
        }

        public override void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            try
            {
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1,
                    V6Setting.IsVietnamese ? _aldmConfig.GRDHV_V1 : _aldmConfig.GRDHE_V1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
        }

        
        private void btnGuiDanhSach_Click(object sender, EventArgs e)
        {
            
        }
        
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //e.Cancel = true;
            e.ThrowException = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

            dataGridView1_DataSourceChanged2(sender, e);
        }

        protected int _oldIndex = -1;
        public string _sttRec = null;
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var row = dataGridView1.CurrentRow;
                if (row != null && _oldIndex != row.Index)
                {
                    _oldIndex = row.Index;
                    if (dataGridView1.Columns.Contains("STT_REC"))
                    {
                        _sttRec = row.Cells["STT_REC"].Value.ToString();
                    }
                    UpdateGridView2(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Gridview1Select", ex);
            }
        }

        public void UpdateGridView2(DataGridViewRow row)
        {
            ViewDetails(row);
            FormatGridView2();
        }

        private string mact_format = null;
        protected void FormatGridView2()
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;
                string mact = dataGridView1.CurrentRow.Cells["Ma_ct"].Value.ToString().Trim();
                if (mact != mact_format)
                {
                    mact_format = mact;
                    //var alctconfig = ConfigManager.GetAlctConfig(mact);
                    var aldmConfig = ConfigManager.GetAldmConfig("AAPPR_ALL_AD_" + mact);
                    if (!aldmConfig.HaveInfo) return;

                    var headerString = V6Setting.IsVietnamese ? aldmConfig.GRDHV_V1 : aldmConfig.GRDHE_V1;
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, aldmConfig.GRDS_V1, aldmConfig.GRDF_V1, headerString);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView2", ex);
            }
        }

        protected void ViewDetails(DataGridViewRow row)
        {
            return;
            try
            {
                if (row != null)
                {
                    var ngay_ct = ObjectAndString.ObjectToFullDateTime(row.Cells["ngay_ct"].Value);
                    var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                    var ma_ct = row.Cells["Ma_ct"].Value.ToString().Trim();
                    DataTable data = null;

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@ngay_ct", ngay_ct.ToString("yyyyMMdd")),
                        new SqlParameter("@ma_ct", ma_ct),
                        new SqlParameter("@stt_rec", sttRec),
                        new SqlParameter("@user_id", V6Login.UserId),
                        new SqlParameter("@advance", ""),
                    };
                    data = V6BusinessHelper.ExecuteProcedure("AAPPR_XULY_ALL_EMAIL_AD", plist).Tables[0];

                    dataGridView2.AutoGenerateColumns = true;
                    dataGridView2.DataSource = data;
                }
                else
                {
                    dataGridView2.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XBASE0EDIT ViewDetails: " + ex.Message);
            }
        }


        private bool newRowNeeded;
        private void dataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
            newRowNeeded = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (newRowNeeded)
            {
                newRowNeeded = false;
                //numberOfRows = numberOfRows + 1;
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            var currentRow = dataGridView1.CurrentRow;
            if (currentRow == null) return;

            var newData = currentRow.ToDataDictionary();
            var afterData = AddData(newData);
            InvokeFormEvent(FormDynamicEvent.AFTERADDSUCCESS);
            if (afterData != null)
            {
                foreach (KeyValuePair<string, object> item in afterData)
                {
                    if (dataGridView1.Columns.Contains(item.Key))
                    {
                        currentRow.Cells[item.Key].Value = item.Value;
                    }
                }
            }
        }

        private void dataGridView1_DataSourceChanged2(object sender, EventArgs e)
        {
            try
            {
                List<DataGridViewColumn> cList = new List<DataGridViewColumn>();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    cList.Add(column);
                }
                foreach (DataGridViewColumn column in cList)
                {
                    string FIELD = column.Name.Trim().ToUpper();
                    DataGridViewColumn column1 = null;
                    
                    var editFieldDic = ObjectAndString.StringToStringDictionary(EditFieldFormat);
                    if (editFieldDic.ContainsKey(column.Name.ToUpper()))
                    {
                        string EorR_FORMAT = editFieldDic[FIELD];
                        
                        var ss = EorR_FORMAT.Split(':');

                        if (ss[0].ToUpper() == "R")
                        {
                            column.ReadOnly = true;
                            continue;
                        }
                        if (!EorR_FORMAT.Contains(":")) goto Default;

                        string NM_IP = ss[1].ToUpper(); // N2 hoac NM_IP_SL
                        if (NM_IP.StartsWith("N"))
                        {
                            string newFormat = NM_IP.Length == 2 ? NM_IP : V6Options.GetValueNull(NM_IP.Substring(1));
                            dataGridView1.ChangeColumnType(FIELD, typeof(V6NumberDataGridViewColumn), newFormat);
                        }
                        else if (NM_IP.StartsWith("C")) // CVvar
                        {
                            column1 = dataGridView1.ChangeColumnType(FIELD, typeof(V6VvarDataGridViewColumn), null);
                            ((V6VvarDataGridViewColumn) column1).Vvar = NM_IP.Substring(1);
                        }
                        else if (NM_IP.StartsWith("D0")) // ColorDateTime
                        {
                            dataGridView1.ChangeColumnType(FIELD, typeof(V6DateTimeColorGridViewColumn), null);
                        }
                        else if (NM_IP.StartsWith("D1")) // DateTimePicker
                        {
                            dataGridView1.ChangeColumnType(FIELD, typeof(V6DateTimePickerGridViewColumn), null);
                        }


                        continue;
                    }

                    Default:
                    {
                        if (ObjectAndString.IsNumberType(column.ValueType))
                        {
                            column1 = dataGridView1.ChangeColumnType(column.Name, typeof(V6NumberDataGridViewColumn), null);
                        }
                        //else if (NM_IP.StartsWith("C")) // CVvar
                        //{
                        //    column1 = dataGridView1.ChangeColumnType(ss[0], typeof(V6VvarDataGridViewColumn), null);
                        //    ((V6VvarDataGridViewColumn)column).Vvar = NM_IP.Substring(1);
                        //}
                        else if (ObjectAndString.IsDateTimeType(column.ValueType)) // ColorDateTime
                        {
                            column1 = dataGridView1.ChangeColumnType(column.Name, typeof(V6DateTimeColorGridViewColumn), null);
                        }
                        //else if (NM_IP.StartsWith("D1")) // DateTimePicker
                        //{
                        //    column1 = dataGridView1.ChangeColumnType(ss[0], typeof(V6DateTimePickerGridViewColumn), null);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_DataSourceChanged", ex);
            }
        }

        
        /// <summary>
        /// Thêm dữ liệu vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IDictionary<string, object> AddData(IDictionary<string, object> data)
        {
            try
            {
                SetStatusText(V6Text.Add + TableName);
                if (_defaultData == null || _defaultData.Count == 0)
                {
                    this.ShowErrorMessage("Thiếu dữ liệu mặc định.");
                    return null;
                }
                //Gán dữ liệu mặc định
                if (_defaultData != null)
                {
                    data.AddRange(_defaultData, true);
                }

                var filterData = new Dictionary<string, object>();
                foreach (Control control in FilterControl2.groupBox1.Controls)
                {
                    var line = control as FilterLineBase;
                    if (line == null) continue;
                    filterData[line.FieldName.ToUpper()] = line.ObjectValue;
                }
                data.AddRange(filterData, true);
                //Remove UID in data
                if (data.ContainsKey("UID")) data.Remove("UID");
                //Tạo keys giả
                IDictionary<string, object> keys = new Dictionary<string, object>();
                foreach (string field in KeyFields)
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

                var result = V6BusinessHelper.Insert(TableName, data);
                if (result)
                {
                    HaveChange = true;
                    SetStatusText(string.Format("{0} {1}", TableName, V6Text.AddSuccess));

                    var selectResult = V6BusinessHelper.Select(TableName, keys, "*");
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
                SetStatusText(string.Format("{0} {1}", TableName, V6Text.AddFail));
                this.WriteExLog(GetType() + ".AddData", ex);
            }
            return null;
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
                SetStatusText(string.Format("{0} {1} {2}", V6Text.Delete, TableName, delete_info));
                if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                {
                    var result = V6BusinessHelper.Delete(TableName, keys);
                    if (result > 0)
                    {
                        HaveChange = true;
                        dataGridView1.Rows.RemoveAt(rowIndex);
                        SetStatusText(string.Format("{0} {1} {2}", TableName, V6Text.DeleteSuccess, delete_info));
                    }
                }
            }
            catch (Exception ex)
            {
                SetStatusText(string.Format("{0} {1} {2}", TableName, V6Text.DeleteFail, delete_info));
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }

        private object _cellBeginEditValue;
        /// <summary>
        /// Update dữ liệu vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        private void UpdateData(int rowIndex, int columnIndex)
        {
            var update_info = "";
            try
            {
                SetStatusText(string.Format("{0} {1}", V6Text.Edit, TableName));
                var row = dataGridView1.Rows[rowIndex];
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                foreach (string field in KeyFields)
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
                update_info += UPDATE_FIELD + " = " + ObjectAndString.ObjectToString(update_value) + ". ";
                foreach (KeyValuePair<string, string> item in dataGridView1.CongThuc)
                {
                    update_value = row.Cells[item.Key].Value;
                    updateData[item.Key] = update_value;
                    update_info += item.Key + " = " + ObjectAndString.ObjectToString(update_value) + ". ";
                }
                // more modified data by dynamic code.
                foreach (KeyValuePair<string, object> item in beforeEditData)
                {
                    if (item.Key != UPDATE_FIELD && ObjectAndString.ObjectToString(item.Value) !=
                        ObjectAndString.ObjectToString(afterEditData[item.Key]))
                    {
                        updateData[item.Key] = afterEditData[item.Key];
                    }
                }

                var result = V6BusinessHelper.UpdateSimple(TableName, updateData, keys);
                if (result > 0)
                {
                    HaveChange = true;
                    SetStatusText(string.Format("{0} {1} {2}", TableName, V6Text.EditSuccess, update_info));
                }
            }
            catch (Exception ex)
            {
                SetStatusText(string.Format("{0} {1} {2}", TableName, V6Text.EditFail, update_info));
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _cellBeginEditValue = dataGridView1.CurrentCell.Value;
            beforeEditData = dataGridView1.CurrentRow.ToDataDictionary();
        }

        private IDictionary<string, object> beforeEditData;
        private IDictionary<string, object> afterEditData;
        //private List<string> updateFieldList0 = new List<string>();
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dataGridView1.CurrentCell;
            string FIELD = cell.OwningColumn.DataPropertyName.ToUpper();
            All_Objects["cell"] = cell;
            V6ControlsHelper.InvokeMethodDynamic(Event_program, FIELD + "_LOSTFOCUS", All_Objects);
            
            //var UPDATE_FIELD = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.ToUpper();
            //Xu ly cong thuc tinh toan
            //updateFieldList = new List<string>(); // Đổi qua dùng CongThuc trong datagridview.
            //if (CheckUpdateField(UPDATE_FIELD)) XuLyCongThucTinhToan();

            afterEditData = dataGridView1.CurrentRow.ToDataDictionary();

            UpdateData(e.RowIndex, e.ColumnIndex);
            InvokeFormEvent(FormDynamicEvent.AFTERUPDATE);
        }

        string delete_info = "";
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete || e.KeyData == Keys.F8)
            {
                if (!V6Login.UserRight.AllowDelete(ItemID, _base0._reportFile + "6"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    e.Handled = true;
                    return;
                }
                if (dataGridView1.CurrentRow != null)
                {
                    var selectedRowIndex = dataGridView1.CurrentRow.Index;
                    if (selectedRowIndex < dataGridView1.NewRowIndex)
                    {
                        //var rowData = dataGridView1.CurrentRow.ToDataDictionary();
                        var keys = new SortedDictionary<string, object>();
                        delete_info = "";
                        foreach (string field in KeyFields)
                        {
                            var UPDATE_FIELD = field.ToUpper();
                            var update_value = dataGridView1.CurrentRow.Cells[field].Value;
                            keys.Add(UPDATE_FIELD, update_value);
                            delete_info += UPDATE_FIELD + " = " + ObjectAndString.ObjectToString(update_value) + ". ";
                        }

                        if (keys.Count > 0)
                        {
                            DeleteData(keys, selectedRowIndex);
                            InvokeFormEvent(FormDynamicEvent.AFTERDELETESUCCESS);
                        }
                    }
                }
            }
        }

        private void XADVXNK01_Filter_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                int height = (btnGuiDanhSach.Top - 10) / 2;
                panel1.Height = height;
                panel2.Height = height;
                panel2.Top = panel1.Bottom;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void thayTheMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe();
        }

        private void thayTheNhieuMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe(true);
        }

        /// <summary>
        /// Chức năng sửa hàng loạt một cột dữ liệu.
        /// </summary>
        /// <param name="many">Thanh thế hết giá trị cho các cột được cấu hình Alct.Extra_info.CT_REPLACE bằng giá trị của dòng đang đứng.</param>
        public void ChucNang_ThayThe(bool many = false)
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

                if (many)
                {
                    IDictionary<string, object> data = new Dictionary<string, object>();
                    if (dataGridView1.CurrentRow != null)
                    {
                        foreach (string field in listFieldCanReplace)
                        {
                            data[field.ToUpper()] = dataGridView1.CurrentRow.Cells[field].Value;
                        }

                        V6ControlFormHelper.UpdateDKlistAll(data, listFieldCanReplace, _ds.Tables[0], dataGridView1.CurrentRow.Index);
                    }
                }
                else // Thay thế giá trị của cột đang đứng từ dòng hiện tại trở xuống bằng giá trị mới.
                {
                    int field_index = dataGridView1.CurrentCell.ColumnIndex;
                    string FIELD = dataGridView1.CurrentCell.OwningColumn.DataPropertyName.ToUpper();


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

                    ChucNangThayTheForm f = new ChucNangThayTheForm(ObjectAndString.IsNumberType(dataGridView1.CurrentCell.OwningColumn.ValueType), textBox);
                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        if (f.ChucNangDaChon == f._ThayThe)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Index < dataGridView1.CurrentRow.Index) continue;
                                if (row.IsNewRow) continue;

                                object newValue = ObjectAndString.ObjectTo(valueType, f.Value);
                                if (ObjectAndString.IsDateTimeType(valueType) && newValue != null)
                                {
                                    DateTime newDate = (DateTime)newValue;
                                    if (newDate < new DateTime(1700, 1, 1))
                                    {
                                        newValue = null;
                                    }
                                }

                                SortedDictionary<string, object> newData = new SortedDictionary<string, object>();
                                newData.Add(FIELD, newValue);
                                V6ControlFormHelper.UpdateGridViewRow(row, newData);
                            }
                        }
                        else // Đảo ngược
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Index < dataGridView1.CurrentRow.Index) continue;
                                if (row.IsNewRow) continue;

                                var newValue = ObjectAndString.ObjectToDecimal(row.Cells[field_index].Value) * -1;
                                SortedDictionary<string, object> newData = new SortedDictionary<string, object>();
                                newData.Add(FIELD, newValue);
                                V6ControlFormHelper.UpdateGridViewRow(row, newData);
                            }
                        }

                        //All_Objects["replaceField"] = FIELD;
                        //All_Objects["dataGridView1"] = dataGridView1;
                        //All_Objects["detail1"] = detail1;
                        //if (Event_Methods.ContainsKey(FormDynamicEvent.AFTERREPLACE))
                        //{
                        //    InvokeFormEvent(FormDynamicEvent.AFTERREPLACE);
                        //}
                        //else
                        //{
                        //    AfterReplace(All_Objects);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChucNang_ThayThe " + _sttRec, ex);
            }
        }



    }
}
