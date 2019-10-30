using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Albc
{
    public partial class FieldSelectorForm : Form
    {
        public FieldSelectorForm()
        {
            InitializeComponent();
        }
        
        //public FieldSelectorForm(string tempTable, string keyField, string nameField, string sourceTable)
        //{
        //    InitializeComponent();
        //}

        private const string FIELD_NAME = "FIELD_NAME";
        private const string FIELD_TYPE = "FIELD_TYPE";
        private const string FIELD_WIDTH = "FIELD_WIDTH";
        private const string FIELD_CAPTION_V = "FIELD_CAPTION_V";
        private const string FIELD_CAPTION_E = "FIELD_CAPTION_E";
        //private const string FIELD_NOSUM = "FIELD_NOSUM";

        private void SelectMultiIDForm_Load(object sender, EventArgs e)
        {
            
        }        
        
        private void SelectMultiIDForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    btnAddSelect_Click(sender, e);
                }
            }
            catch(Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_KeyDown", ex);
            }
        }
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MoveSelectedRowLeftToRight()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataRowView left_row_view = row.DataBoundItem as DataRowView;
                if (left_row_view != null)
                {
                    DataRow left_row = left_row_view.Row;
                    DataRow copy = _targetTable.NewRow();
                    copy[FIELD_NAME] = left_row[FIELD_NAME];
                    copy[FIELD_TYPE] = left_row[FIELD_TYPE];
                    copy[FIELD_WIDTH] = left_row[FIELD_WIDTH];
                    copy[FIELD_CAPTION_V] = left_row[FIELD_CAPTION_V];
                    copy[FIELD_CAPTION_E] = left_row[FIELD_CAPTION_E];
                    //copy[FIELD_NOSUM] = left_row[FIELD_NOSUM];
                    _targetTable.Rows.Add(copy);
                    _sourceTable.Rows.Remove(left_row);
                }
            }
        }
        private void MoveSelectedRowRightToLeft()
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                DataRowView right_row_view = row.DataBoundItem as DataRowView;
                if (right_row_view != null)
                {
                    DataRow right_row = right_row_view.Row;
                    DataRow copy = _sourceTable.NewRow();
                    copy[FIELD_NAME] = right_row[FIELD_NAME];
                    copy[FIELD_TYPE] = right_row[FIELD_TYPE];
                    copy[FIELD_WIDTH] = right_row[FIELD_WIDTH];
                    copy[FIELD_CAPTION_V] = right_row[FIELD_CAPTION_V];
                    copy[FIELD_CAPTION_E] = right_row[FIELD_CAPTION_E];
                    //copy[FIELD_NOSUM] = right_row[FIELD_NOSUM];
                    _sourceTable.Rows.Add(copy);
                    _targetTable.Rows.Remove(right_row);
                }
            }
        }

        private void MoveDataGridViewRows(DataTable table1, DataTable table2)
        {
            while (table1.Rows.Count>0)
            {
                DataRow row1 = table1.Rows[0];
                DataRow copy = table2.NewRow();
                copy[FIELD_NAME] = row1[FIELD_NAME];
                copy[FIELD_TYPE] = row1[FIELD_TYPE];
                copy[FIELD_WIDTH] = row1[FIELD_WIDTH];
                copy[FIELD_CAPTION_V] = row1[FIELD_CAPTION_V];
                copy[FIELD_CAPTION_E] = row1[FIELD_CAPTION_E];
                //copy[FIELD_NOSUM] = row1[FIELD_NOSUM];

                table2.Rows.Add(copy);
                table1.Rows.Remove(row1);
            }
        }

        private void btnAddSelect_Click(object sender, EventArgs e)
        {
            MoveSelectedRowLeftToRight();
        }

        private void btnRemoveSelect_Click(object sender, EventArgs e)
        {
            MoveSelectedRowRightToLeft();
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            MoveDataGridViewRows(_sourceTable, _targetTable);
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            MoveDataGridViewRows(_targetTable, _sourceTable);
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            //int flag = dataGridView1.Rows.Count;
            if (dataGridView1.SelectedRows.Count==1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected == false) continue;
                    
                    for (int ii = i + 1; ii < dataGridView1.Rows.Count; ii++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if (dataGridView1.Rows[ii].Cells[j].Value.ToString().ToLower().Contains(txtTim.Text.Trim().ToLower()))
                            {
                                dataGridView1.Rows[i].Selected = false;
                                dataGridView1.Rows[ii].Selected = true;
                                dataGridView1.CurrentCell = dataGridView1.Rows[ii].Cells[0];
                                return;
                            }
                        }
                    }
                    dataGridView1.Rows[i].Selected = false;
                    dataGridView1.Rows[0].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                    return;
                }
            }
            else//đang chọn nhiều hoặc không chọn
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    row.Selected = false;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().ToLower().Contains(txtTim.Text.Trim().ToLower()))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                            return;
                        }
                    }
                }   
            }
        }

        private void btnTimTatCa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().ToLower().Contains(txtTim.Text.Trim().ToLower()))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void txtTim_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            btnTim_Click(null, null);
        }

        public string GetFieldsString()
        {
            string result = "";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                result += ";" + row.Cells[FIELD_NAME].Value.ToString().Trim();
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }
        
        //public string GetNoSumFieldsString()
        //{
        //    string result = "";
        //    foreach (DataGridViewRow row in dataGridView2.Rows)
        //    {
        //        result += ";" + row.Cells[FIELD_NOSUM].Value.ToString().Trim();
        //    }
        //    if (result.Length > 0) result = result.Substring(1);
        //    return result;
        //}

        public string GetFormatsString()
        {
            string result = "";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string dataType = ((AlbcFieldType)row.Cells[FIELD_TYPE].Value).ToString();
                string fieldWidth = row.Cells[FIELD_WIDTH].Value.ToString().Trim();
                result += ";" + dataType;
                if (dataType.StartsWith("N")) // N0 N1 N2...
                {
                    result += ":";
                }
                result += fieldWidth;
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

        public string GetCaptionsStringV()
        {
            string result = "";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string fieldName = row.Cells[FIELD_NAME].Value.ToString().Trim();
                string caption = row.Cells[FIELD_CAPTION_V].Value.ToString();
                if (caption == "") caption = CorpLan2.GetFieldHeader(fieldName, "V");
                result += ";" + caption;
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

        public string GetCaptionsStringE()
        {
            string result = "";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string fieldName = row.Cells[FIELD_NAME].Value.ToString().Trim();
                string caption = row.Cells[FIELD_CAPTION_E].Value.ToString();
                if (caption == "") caption = CorpLan2.GetFieldHeader(fieldName, "V");
                result += ";" + caption;
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

        private List<AlbcFieldInfo> _sourceFieldInfoList;
        private List<AlbcFieldInfo> _targetFieldInfoList;
        private DataTable _sourceTable;
        private DataTable _targetTable;
        DataTable GenTable(List<AlbcFieldInfo> fieldInfoList)
        {
            DataTable table = new DataTable("GenFieldInfoList");
            table.Columns.Add(FIELD_NAME, typeof(string));
            table.Columns.Add(FIELD_TYPE, typeof(AlbcFieldType));
            table.Columns.Add(FIELD_WIDTH, typeof(int));
            table.Columns.Add(FIELD_CAPTION_V, typeof(string));
            table.Columns.Add(FIELD_CAPTION_E, typeof(string));
            //table.Columns.Add(FIELD_NOSUM, typeof(string));
            foreach (AlbcFieldInfo info in fieldInfoList)
            {
                DataRow row = table.NewRow();
                row[FIELD_NAME] = info.FieldName;
                row[FIELD_TYPE] = info.FieldType;
                row[FIELD_WIDTH] = info.FieldWidth;
                row[FIELD_CAPTION_V] = info.FieldHeaderV;
                row[FIELD_CAPTION_E] = info.FieldHeaderE;
                //row[FIELD_NOSUM] = info.FieldNoSum;
                table.Rows.Add(row);
            }
            return table;
        }

        public void AddSourceFieldList(List<AlbcFieldInfo> fieldInfoList)
        {
            _sourceFieldInfoList = fieldInfoList;
            DataTable temp = GenTable(fieldInfoList);
            _sourceTable = temp;
            dataGridView1.DataSource = temp;
        }

        public void AddTargetFieldList(List<AlbcFieldInfo> fieldInfoList)
        {
            _targetFieldInfoList = fieldInfoList;
            DataTable temp = GenTable(fieldInfoList);
            _targetTable = temp;
            dataGridView2.DataSource = temp;

            List<DataRow> listRow = new List<DataRow>();
            foreach (DataRow targetRow in _targetTable.Rows)
            {
                foreach (DataRow sourceRow in _sourceTable.Rows)
                {
                    if (sourceRow[FIELD_NAME].ToString().Trim().ToUpper() == targetRow[FIELD_NAME].ToString().Trim().ToUpper())
                    {
                        //targetRow[FIELD_TYPE] = sourceRow[FIELD_TYPE];
                        //targetRow[FIELD_WIDTH] = sourceRow[FIELD_WIDTH];
                        //targetRow[FIELD_CAPTION_V] = sourceRow[FIELD_CAPTION_V];
                        //targetRow[FIELD_CAPTION_E] = sourceRow[FIELD_CAPTION_E];
                        listRow.Add(sourceRow);
                        break;
                    }
                }
            }
            while (listRow.Count>0)
            {
                _sourceTable.Rows.Remove(listRow[0]);
                listRow.RemoveAt(0);
            }
        }

        private void ChangeRowData(DataRow row1, DataRow row2)
        {
            if (row1 == null || row2 == null)
            {
                return;
            }
            if (row1.Table != row2.Table)
            {
                V6Message.ShowWarning("Khác bảng", this);
                return;
            }
            foreach (DataColumn column in row1.Table.Columns)
            {
                object o1 = row1[column.ColumnName];
                row1[column.ColumnName] = row2[column.ColumnName];
                row2[column.ColumnName] = o1;
            }
        }

        private void FixSize()
        {
            try
            {
                dataGridView1.Width = Width/2 - 55;
                btnAddSelect.Left = dataGridView1.Right + 6;
                btnAddAll.Left = btnAddSelect.Left;
                btnRemoveSelect.Left = btnAddSelect.Left;
                btnRemoveAll.Left = btnAddSelect.Left;
                dataGridView2.Left = btnAddSelect.Right + 6;
                dataGridView2.Width = dataGridView1.Width;
                btnMove2Up.Left = dataGridView2.Right + 6;
                btnMove2Down.Left = btnMove2Up.Left;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixSize", ex);
            }
        }

        private void dataGridView2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void btnMove2Up_Click(object sender, EventArgs e)
        {
            var currentRow = dataGridView2.CurrentRow;
            if (currentRow != null && currentRow.Index > 0)
            {
                var changeRow = dataGridView2.Rows[currentRow.Index - 1];
                ChangeRowData(((DataRowView)currentRow.DataBoundItem).Row, ((DataRowView)changeRow.DataBoundItem).Row);
                dataGridView2.CurrentCell = changeRow.Cells[0];
            }
        }

        private void btnMove2Down_Click(object sender, EventArgs e)
        {
            var currentRow = dataGridView2.CurrentRow;
            if (currentRow != null && currentRow.Index < dataGridView2.RowCount - 1)
            {
                var changeRow = dataGridView2.Rows[currentRow.Index + 1];
                ChangeRowData(((DataRowView)currentRow.DataBoundItem).Row, ((DataRowView)changeRow.DataBoundItem).Row);
                dataGridView2.CurrentCell = changeRow.Cells[0];
            }
        }

        private void FieldSelectorForm_SizeChanged(object sender, EventArgs e)
        {
            FixSize();
        }
    }
}