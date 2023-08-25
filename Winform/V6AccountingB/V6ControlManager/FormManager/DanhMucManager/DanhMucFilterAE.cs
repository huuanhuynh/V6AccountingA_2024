using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.DanhMucManager
{
    public partial class DanhMucFilterAE : V6Form
    {

        protected void CallAEApply()
        {
            try
            {
                foreach (KeyValuePair<DataGridViewRow, DataGridViewRow> item in editedRows)
                {
                    SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                    var item_data = item.Value.ToDataDictionary();
                    if (item_data.ContainsKey("UID") && ("" + item_data["UID"]).Length > 5
                        && item_data["UID"].ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        keys["UID"] = item_data["UID"];
                        var update_data = new Dictionary<string, object>();
                        foreach (string field in edit_field_list)
                        {
                            string FIELD = field.ToUpper();
                            if (item_data.ContainsKey(FIELD)) update_data[FIELD] = item_data[FIELD];
                        }
                        V6BusinessHelper.Update(_aldmConfig.TABLE_NAME, update_data, keys);
                    }
                    else
                    {
                        V6BusinessHelper.Insert(_aldmConfig.TABLE_NAME, item_data);
                    }
                }
                editedRows.Clear();
                Close();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CallAEApply error!\n" + ex.Message);
            }
        }

        private string QueryString
        {
            get
            {
                return "panel1.QueryString";
            }
        }
        private readonly V6TableStruct _structTable;
        
        private AldmConfig _aldmConfig, _aldmConfigAE;

        public DanhMucFilterAE()
        {
            InitializeComponent();
        }

        public DanhMucFilterAE(V6TableStruct structTable, AldmConfig aldmConfig)
        {
            InitializeComponent();
            _structTable = structTable;
            
            _aldmConfig = aldmConfig;
            MyInit();
        }

        private void MyInit()
        {
            MadeControls();
            SetGridViewEvent();
        }

        private void SetGridViewEvent()
        {
            try
            {
                dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
                dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
                dataGridView1.enter_to_tab = false;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SetGridViewEvent error!\n" + ex.Message);
            }
        }

        AlbcConfig _albcConfig;
        public FilterBase FilterControl { get; set; }
        string ma_bc = "";
        private void MadeControls()
        {
            try
            {
                ma_bc = _aldmConfig.EXTRA_INFOR["SF5"];

                _albcConfig = ConfigManager.GetAlbcConfigByMA_FILE(ma_bc);
                _aldmConfigAE = ConfigManager.GetAldmConfig(ma_bc);
                FilterControl = QuickReportManager.AddFilterControl44Base(ma_bc, ma_bc, ma_bc, panel1, toolTipV6FormControl);
                panel1.Controls.Add(FilterControl);
                FilterControl.Focus();
                
                FilterControl.MadeFilterControls(ma_bc, All_Objects, toolTipV6FormControl);
                //FilterControl = QuickReportManager.AddFilterControl44Base(_program, _reportProcedure, _Ma_File, panel1, toolTipV6FormControl);

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MadeControls error!\n" + ex.Message);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Enter))
                {
                    CallAEApply();
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return base.DoHotKey0(keyData);
        }

        public void MakeReport()
        {
            try
            {
                GenerateProcedureParameters();
                LoadData();
                if (_executesuccess)
                {
                    SetTBLdata();
                    ShowData();
                }
                else
                {
                    this.ShowMessage(_message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MakeReport error!\n" + ex.Message);
            }
        }

        public List<SqlParameter> _pList;
        private bool GenerateProcedureParameters()
        {
            try
            {
                _pList = new List<SqlParameter>();
                _pList.AddRange(FilterControl.GetFilterParameters());
                //_pList.Add(new SqlParameter("@cKey", "1=1" + (sKey.Length>0?" And " +sKey:"")));
                return true;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("GenerateProcedureParameters", ex);
                return false;
            }
        }
        public bool GenerateProcedureParameters_New()
        {
            try
            {
                _pList = new List<SqlParameter>();
                var tList = FilterControl.GetFilterParametersNew();
                foreach (SqlParameter p in tList)
                {
                    _pList.Add(new SqlParameter(p.ParameterName, p.Value));
                }
                //_pList.Add(new SqlParameter("@cKey", "1=1" + (sKey.Length>0?" And " +sKey:"")));
                return true;
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("GenerateProcedureParameters: " + ex.Message);
                return false;
            }
        }

        private DataSet _ds;
        public DataTable _tbl1, _tbl2, _tbl3;
        public void LoadData()
        {
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
                var proc = ma_bc;
                _ds = V6BusinessHelper.ExecuteProcedure(proc, _pList.ToArray());
                SetTBLdata();

                _executesuccess = true;
            }
            catch (Exception ex)
            {
                _message = V6Text.Text("QUERY_FAILED") + "\n";
                if (ex.Message.StartsWith("Could not find stored procedure")) _message += V6Text.NotExist + ex.Message.Substring(31);
                else _message += ex.Message;

                _tbl1 = null;
                _tbl2 = null;
                _ds = null;
                _executesuccess = false;
            }
            _executing = false;
        }

        private void SetTBLdata()
        {
            if (_ds.Tables.Count > 0)
            {
                _tbl1 = _ds.Tables[0];
                _tbl1.TableName = "DataTable1";
                //_view1 = new DataView(_tbl1);
            }
            if (_ds.Tables.Count > 1)
            {
                _tbl2 = _ds.Tables[1];
                _tbl2.TableName = "DataTable2";
                //_view2 = new DataView(_tbl2);
            }
            else
            {
                _tbl2 = null;
            }

            if (_ds.Tables.Count > 2)
            {
                _tbl3 = _ds.Tables[2];
                _tbl3.TableName = "DataTable3";
                //_view3 = new DataView(_tbl3);
            }
            else
            {
                _tbl3 = null;
            }
        }

        void ShowData()
        {
            try
            {
                FilterControl.LoadDataFinish(_ds);
                All_Objects["_ds"] = _ds;
                InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                dataGridView1.SetFrozen(0);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _tbl1;

                
                if (_albcConfig != null && _albcConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, _albcConfig.FIELDV, _albcConfig.OPERV, _albcConfig.VALUEV,
                        _albcConfig.BOLD_YN == "1", _albcConfig.COLOR_YN == "1", ObjectAndString.StringToColor(_albcConfig.COLORV));
                }

                FormatGridView();
                SetGridViewReadonly(dataGridView1, _aldmConfigAE);

                dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                //timerViewReport.Stop();
                _executesuccess = false;
                this.ShowErrorException(GetType() + ".ShowReport", ex);
            }
        }

        private bool formated;
        private void FormatGridView()
        {
            if (formated) return;
            try
            {
                if (_aldmConfigAE != null && _aldmConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1,
                        V6Setting.IsVietnamese ? _aldmConfig.GRDHV_V1 : _aldmConfig.GRDHE_V1);
                    var conditionColor = ObjectAndString.StringToColor(_aldmConfig.COLORV);
                    V6ControlFormHelper.FormatGridView(dataGridView1, _aldmConfig.FIELDV, _aldmConfig.OPERV, _aldmConfig.VALUEV,
                        _aldmConfig.BOLD_YN, _aldmConfig.COLOR_YN, conditionColor);

                    int frozen = ObjectAndString.ObjectToInt(_aldmConfig.FROZENV);
                    dataGridView1.SetFrozen(frozen);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
            formated = true;
        }

        List<string> edit_field_list = new List<string>();
        public void SetGridViewReadonly(V6ColorDataGridView dataGridView1, AldmConfig config)
        {
            try
            {
                dataGridView1.ReadOnly = false;
                //dataGridView1.SetEditColumn();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.ReadOnly = true;
                }
                //dataGridView1.SetEditColumn();//Alctct right. copy dataeditor sua nhieu dong.
                //invoice.GRD_READONLY
                // FIELD:E/R invoice extra info ADFIELDS

                string _showFields = null;
                if (config.EXTRA_INFOR != null && config.EXTRA_INFOR.ContainsKey("ADFIELDS"))
                {
                    _showFields = config.EXTRA_INFOR["ADFIELDS"];
                }

                edit_field_list = new List<string>();
                if (_showFields != null)
                {
                    var showFieldSplit = ObjectAndString.SplitString(_showFields);
                    foreach (string field in showFieldSplit)
                    {
                        if (field.Contains(":"))
                        {
                            var ss = field.Split(':');
                            DataGridViewColumn column = dataGridView1.Columns[ss[0]];

                            if (ss.Length > 2)
                            {
                                string NM_IP = ss[2].ToUpper(); // N2 hoac NM_IP_SL
                                if (NM_IP.StartsWith("N"))
                                {
                                    string newFormat = NM_IP.Length == 2
                                        ? NM_IP
                                        : V6Options.GetValueNull(NM_IP.Substring(1));
                                    column = dataGridView1.ChangeColumnType(ss[0],
                                        typeof(V6NumberDataGridViewColumn), newFormat);
                                }
                                else if (NM_IP.StartsWith("C")) // CVvar
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0],
                                        typeof(V6VvarDataGridViewColumn), null);
                                    ((V6VvarDataGridViewColumn)column).Vvar = NM_IP.Substring(1);
                                }
                                else if (NM_IP.StartsWith("D0")) // ColorDateTime
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0],
                                        typeof(V6DateTimeColorGridViewColumn), null);
                                }
                                else if (NM_IP.StartsWith("D1")) // DateTimePicker
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0],
                                        typeof(V6DateTimePickerGridViewColumn), null);
                                }
                            }
                            else
                            {

                            }

                            if (ss[1].ToUpper() == "R" && column != null)
                            {
                                column.ReadOnly = true;
                            }
                            else if (ss[1].ToUpper() == "E" && column != null)
                            {
                                column.ReadOnly = false;
                                edit_field_list.Add(column.DataPropertyName.ToUpper());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void ChonVatTuHangHoa()
        {
            try
            {
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                
                string filter1 = null;
                var form = new DanhMucSelectorForm(ConfigManager.GetAldmConfig("ALVT"), filter1);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var filterData = FilterControl.GetData();
                    var map_info = ObjectAndString.SplitStringBy(_aldmConfigAE.EXTRA_INFOR["SELECT_MAPPING"], ',');
                    foreach (DataRow row in form._targetTable.Rows)
                    {
                        var newRowData = row.ToDataDictionary();
                        newRowData.AddRange(filterData);

                        var mappedData = new Dictionary<string, object>();
                        mappedData.AddRange(newRowData);
                        foreach (var item in map_info)
                        {
                            string[] ss = ObjectAndString.SplitStringBy(item, '=');
                            if (ss.Length > 1 && newRowData.ContainsKey(ss[1])) mappedData[ss[0]] = newRowData[ss[1]];
                        }
                        _tbl1.AddRow(mappedData);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        private void DanhMucFilterAE_Load(object sender, EventArgs e)
        {

        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            MakeReport();
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            CallAEApply();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ChonVatTuHangHoa();
        }

        private void AddRow()
        {
            try
            {
                var newRow = _tbl1.NewRow();
                newRow["??"] = "???";
                _tbl1.Rows.Add(newRow);
                var last_row = dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible)];
                editedRows[last_row] = last_row;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AddRow error!\n" + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Hỏi

                // Xóa hết, xóa luôn ở database (theo UID) theo cấu hình table _aldmConfigAE
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {   
                    var row_data = row.ToDataDictionary();
                    if (ObjectAndString.IsUID(row_data["UID"]))
                    {
                        // Nếu dòng cũ có UID, thì thực hiện xóa ở csdl.
                        var key = new Dictionary<string, object>();
                        key["UID"] = row_data["UID"];
                        V6BusinessHelper.Delete(_aldmConfigAE.TABLE_NAME, key);

                    }
                    else
                    {
                        // Nếu dòng mới thêm (không có UID) thì chỉ việc remove khỏi table.
                    }

                }

                dataGridView1.Rows.Clear();



            }
            catch (Exception ex)
            {
                
            }
        }

        Dictionary<DataGridViewRow, DataGridViewRow> editedRows = new Dictionary<DataGridViewRow, DataGridViewRow>();
        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string FIELD = null;
            try
            {
                var row = dataGridView1.Rows[e.RowIndex];
                var col = dataGridView1.Columns[e.ColumnIndex];
                FIELD = col.DataPropertyName.ToUpper();

                editedRows[row] = row;
                ShowMainMessage("cell_begin_edit: " + FIELD);

                switch (FIELD)
                {
                    case "SO_LUONG1":
                        #region ==== SO_LUONG1 ====
                        
                        #endregion ==== SO_LUONG1 ====
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string FIELD = null;
            try
            {
                var row = dataGridView1.Rows[e.RowIndex];
                var col = dataGridView1.Columns[e.ColumnIndex];
                FIELD = col.DataPropertyName.ToUpper();
                var cell = row.Cells[e.ColumnIndex];
                
                ShowMainMessage("cell_end_edit: " + FIELD);

                switch (FIELD)
                {
                    case "SO_LUONG1":
                        
                        
                        break;

                    case "GIA_NT21":
                        #region ==== GIA_NT21 ====
                        
                        #endregion ==== GIA_NT21 ====
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            
        }


    }
}
