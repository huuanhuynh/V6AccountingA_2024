using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter.Base0
{
    public partial class XADVXNK01_Filter : FilterBase
    {
        private AldmConfig _aldmConfig;

        public XADVXNK01_Filter()
        {
            InitializeComponent();
            
            MyInit();
        }

        void MyInit()
        {
            try
            {
                Anchor = (AnchorStyles) 0xF;
                ExecuteMode = ExecuteMode.ExecuteProcedure;
                //lineMact.SetValue("POH");
                LoadConfig();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        private void LoadConfig()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig("ma_dm");
            }
            catch (Exception ex)
            {
                this.ShowErrorException("LoadConfig", ex);
            }
        }

        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@LSTMA_CT", lineMact.StringValue));
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_BP", "MA_KH"
            }, true);
            result.Add(new SqlParameter("@advance", key0));

            return result;
        }

        public override void LoadDataFinish(DataSet ds)
        {
            try
            {
                _ds = ds;
                dataGridView1.DataSource = _ds.Tables[0];
                if (dataGridView1.RowCount > 0) btnGuiDanhSach.Enabled = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadDataFinish", ex);
            }
        }

        public override void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            dataGridView1 = this.dataGridView1;
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig("ASENDEMAILALL");
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

            //if (_updateDatabase)
            {
                var newData = currentRow.ToDataDictionary();
                var afterData = AddData(newData);
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
        }

        private void dataGridView1_DataSourceChanged2(object sender, EventArgs e)
        {
            try
            {
                if (_showFields != null)
                {
                    var showFieldSplit = ObjectAndString.SplitString(_showFields);
                    foreach (string field in showFieldSplit)
                    {
                        if (field.Contains(":"))
                        {
                            var ss = field.Split(':');
                            DataGridViewColumn column = null;

                            if (ss.Length > 2)
                            {
                                string NM_IP = ss[2].ToUpper(); // N2 hoac NM_IP_SL
                                if (NM_IP.StartsWith("N"))
                                {
                                    string newFormat = NM_IP.Length == 2 ? NM_IP : V6Options.GetValueNull(NM_IP.Substring(1));
                                    column = dataGridView1.ChangeColumnType(ss[0], typeof(V6NumberDataGridViewColumn), newFormat);
                                }
                                else if (NM_IP.StartsWith("C")) // CVvar
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0], typeof(V6VvarDataGridViewColumn), null);
                                    ((V6VvarDataGridViewColumn)column).Vvar = NM_IP.Substring(1);
                                }
                                else if (NM_IP.StartsWith("D0")) // ColorDateTime
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0], typeof(V6DateTimeColorGridViewColumn), null);
                                }
                                else if (NM_IP.StartsWith("D1")) // DateTimePicker
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0], typeof(V6DateTimePickerGridViewColumn), null);
                                }
                            }

                            if (ss[1].ToUpper() == "R" && column != null)
                            {
                                column.ReadOnly = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_DataSourceChanged", ex);
            }
        }

        private string _tableName, _showFields;
        private string[] _keyFields;
        private IDictionary<string, object> _defaultData = null;
        public bool HaveChange { get; set; }
        /// <summary>
        /// Thêm dữ liệu vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IDictionary<string, object> AddData(IDictionary<string, object> data)
        {
            try
            {
                SetStatusText(V6Text.Add + _tableName);

                //Gán dữ liệu mặc định
                if (_defaultData != null)
                {
                    data.AddRange(_defaultData, true);
                }
                //Remove UID in data
                if (data.ContainsKey("UID")) data.Remove("UID");
                //Tạo keys giả
                IDictionary<string, object> keys = new Dictionary<string, object>();
                foreach (string field in _keyFields)
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

                var result = V6BusinessHelper.Insert(_tableName, data);
                if (result)
                {
                    HaveChange = true;
                    SetStatusText(string.Format("{0} {1}", _tableName, V6Text.AddSuccess));

                    var selectResult = V6BusinessHelper.Select(_tableName, keys, "*");
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
                SetStatusText(string.Format("{0} {1}", _tableName, V6Text.AddFail));
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
                SetStatusText(string.Format("{0} {1} {2}", V6Text.Delete, _tableName, delete_info));
                if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                {
                    var result = V6BusinessHelper.Delete(_tableName, keys);
                    if (result > 0)
                    {
                        HaveChange = true;
                        dataGridView1.Rows.RemoveAt(rowIndex);
                        SetStatusText(string.Format("{0} {1} {2}", _tableName, V6Text.DeleteSuccess, delete_info));
                    }
                }
            }
            catch (Exception ex)
            {
                SetStatusText(string.Format("{0} {1} {2}", _tableName, V6Text.DeleteFail, delete_info));
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
                SetStatusText(string.Format("{0} {1}", V6Text.Edit, _tableName));
                var row = dataGridView1.Rows[rowIndex];
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                foreach (string field in _keyFields)
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
                foreach (string FIELD in updateFieldList)
                {
                    update_value = row.Cells[FIELD].Value;
                    updateData[FIELD] = update_value;
                    update_info += FIELD + " = " + ObjectAndString.ObjectToString(update_value) + ". ";
                }

                var result = V6BusinessHelper.UpdateSimple(_tableName, updateData, keys);
                if (result > 0)
                {
                    HaveChange = true;
                    SetStatusText(string.Format("{0} {1} {2}", _tableName, V6Text.EditSuccess, update_info));
                }
            }
            catch (Exception ex)
            {
                SetStatusText(string.Format("{0} {1} {2}", _tableName, V6Text.EditFail, update_info));
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _cellBeginEditValue = dataGridView1.CurrentCell.Value;
        }

        private List<string> updateFieldList = new List<string>();
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //var UPDATE_FIELD = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.ToUpper();
            //Xu ly cong thuc tinh toan
            updateFieldList = new List<string>();
            
            //if (CheckUpdateField(UPDATE_FIELD)) XuLyCongThucTinhToan();

            //if (_updateDatabase)
                UpdateData(e.RowIndex, e.ColumnIndex);
        }

        string delete_info = "";
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var selectedRowIndex = dataGridView1.CurrentRow.Index;
                    if (selectedRowIndex < dataGridView1.NewRowIndex)
                    {
                        //var rowData = dataGridView1.CurrentRow.ToDataDictionary();
                        var keys = new SortedDictionary<string, object>();
                        delete_info = "";
                        foreach (string field in _keyFields)
                        {
                            var UPDATE_FIELD = field.ToUpper();
                            var update_value = dataGridView1.CurrentRow.Cells[field].Value;
                            keys.Add(UPDATE_FIELD, update_value);
                            delete_info += UPDATE_FIELD + " = " + ObjectAndString.ObjectToString(update_value) + ". ";
                        }
                        if (keys.Count > 0) DeleteData(keys, selectedRowIndex);
                    }
                }
            }
        }



    }
}
