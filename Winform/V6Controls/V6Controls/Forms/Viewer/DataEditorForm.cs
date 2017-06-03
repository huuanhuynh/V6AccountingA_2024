using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.Viewer
{
    public partial class DataEditorForm : V6Form
    {
        public DataEditorForm()
        {
            InitializeComponent();
        }

        private object _data;
        private string _tableName, _showFields;
        private string[] _keyFields;
        private bool newRowNeeded;

        public DataEditorForm(object data, string tableName, string showFields, string keyFields, string title,
            bool allowAdd, bool allowDelete, bool showSum = true)
        {
            InitializeComponent();
            if (!showSum)
            {
                dataGridView1.Height = dataGridView1.Bottom - dataGridView1.Top + gridViewSummary1.Height;
                gridViewSummary1.Visible = false;
            }
            dataGridView1.AllowUserToAddRows = allowAdd;
            dataGridView1.AllowUserToDeleteRows = allowDelete;
            Text = title;
            _data = data;
            _tableName = tableName;
            _showFields = showFields;
            _keyFields = keyFields.Split(keyFields.Contains(";") ? ';' : ',');
            
            MyInit();
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void MyInit()
        {
            try
            {
                dataGridView1.DataSource = _data;
                FormatGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MyInit " + ex.Message);
            }
        }

        private void FormatGridView()
        {
            try
            {
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                keys.Add("MA_DM", _tableName);
                var data_aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                if (data_aldm.Rows.Count == 1)
                {
                    var row_data = data_aldm.Rows[0];
                    var showFields = row_data["GRDS_V1"].ToString().Trim();
                    var formatStrings = row_data["GRDF_V1"].ToString().Trim();
                    var headerString = row_data[V6Setting.IsVietnamese?"GRDHV_V1":"GRDHE_V1"].ToString().Trim();
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
                }

                if (_showFields != null)
                {
                    var showFieldSplit = ObjectAndString.SplitString(_showFields);
                    var showFieldList = new List<string>();
                    foreach (string field in showFieldSplit)
                    {
                        if (field.Contains(":"))
                        {
                            var ss = field.Split(':');
                            showFieldList.Add(ss[0]);
                            var column = dataGridView1.Columns[ss[0]];
                            if (ss[1].ToUpper() == "R" && column != null)
                            {
                                column.ReadOnly = true;
                            }
                        }
                        else
                        {
                            showFieldList.Add(field);
                        }
                    }

                    dataGridView1.ShowColumns(showFieldList.ToArray());
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".FormatGridView " + ex.Message);
            }
        }

        public event Action<Keys> HotKeyAction;
        protected virtual void OnHotKeyAction(Keys keyData)
        {
            var handler = HotKeyAction;
            if (handler != null) handler(keyData);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            if (keyData == Keys.Delete && dataGridView1.AllowUserToDeleteRows && !dataGridView1.IsCurrentCellInEditMode)
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
                            delete_info += UPDATE_FIELD + " = " + update_value + ". ";
                        }
                        if (keys.Count > 0) DeleteData(keys, selectedRowIndex);
                    }
                }
                return true;
            }

            OnHotKeyAction(keyData);
            return base.DoHotKey0(keyData);
        }

        string delete_info = "";

        private void AddData(IDictionary<string, object> data)
        {
            try
            {
                toolStripStatusLabel1.Text = V6Text.Add + _tableName;
                
                var result = V6BusinessHelper.Insert(_tableName, data);
                if (result)
                {
                    toolStripStatusLabel1.Text = string.Format("{0} {1}", _tableName, V6Text.AddSuccess);
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1}", _tableName, V6Text.AddFail);
                Logger.WriteToLog("DataEditorForm UpdateData " + ex.Message, Application.ProductName);
            }
        }
        

        private void DeleteData(IDictionary<string, object> keys, int rowIndex)
        {
            try
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", V6Text.Delete, _tableName, delete_info);
                if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                {
                    var result = V6BusinessHelper.Delete(_tableName, keys);
                    if (result > 0)
                    {
                        dataGridView1.Rows.RemoveAt(rowIndex);
                        toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.DeleteSuccess, delete_info);
                    }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.DeleteFail, delete_info);
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }

        private object _cellBeginEditValue;
        private void UpdateData(int rowIndex, int columnIndex)
        {
            var update_info = "";
            try
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1}", V6Text.Edit, _tableName);
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
                update_info += UPDATE_FIELD + " = " + update_value + ". ";

                var result = V6BusinessHelper.UpdateSimple(_tableName, updateData, keys);
                if (result > 0)
                {
                    toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.EditSuccess, update_info);
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.EditFail, update_info);
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _cellBeginEditValue = dataGridView1.CurrentCell.Value;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex, e.ColumnIndex);
        }

        

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
            var newData = e.Row.ToDataDictionary();
            AddData(newData);
        }

        
    }
}
