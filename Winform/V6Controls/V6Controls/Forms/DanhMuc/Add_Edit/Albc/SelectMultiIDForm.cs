using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Albc
{
    public partial class SelectMultiFieldsForm : Form
    {
        public SelectMultiFieldsForm()
        {
            InitializeComponent();
        }
        
        public SelectMultiFieldsForm(string tempTable, string keyField, string nameField, string sourceTable)
        {
            InitializeComponent();
        }

        private const string FIELD_NAME = "FIELD_NAME";
        private const string FIELD_TYPE = "FIELD_TYPE";
        private const string FIELD_WIDTH = "FIELD_WIDTH";

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
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    btnRemoveSelect_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView2_KeyDown", ex);
            }
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
                    _sourceTable.Rows.Add(copy);
                    _targetTable.Rows.Remove(right_row);
                }
            }
        }

        private void MoveDataGridViewRows(DataTable table1, DataTable table2)
        {
            while (table1.Rows.Count>0)
            {
                DataRow copy = table2.NewRow();
                //for (int i = 0; i < copy.Cells.Count; i++)
                //{
                //    copy.Cells[i].Value = table1.Rows[0].Cells[i].Value;
                //}
                //table2.Rows.Add(copy);
                //table1.Rows.Remove(table1.Rows[0]);
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

        public string GetFormatsString()
        {
            string result = "";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string dataType = row.Cells[FIELD_TYPE].Value.ToString().Trim();
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
                result += ";" + CorpLan2.GetFieldHeader(fieldName);
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

        public string GetCaptionsStringE()
        {
            string result = "";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                result += ";" + row.Cells[FIELD_NAME].Value.ToString().Trim();
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
            foreach (AlbcFieldInfo info in fieldInfoList)
            {
                DataRow row = table.NewRow();
                row[FIELD_NAME] = info.FieldName;
                row[FIELD_TYPE] = info.FieldType;
                row[FIELD_WIDTH] = info.FieldWidth;
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
                        listRow.Add(sourceRow);
                        break;
                    }
                }
                //foreach (AlbcFieldInfo fi0 in _sourceFieldInfoList)
                //{
                //    if (fi0.FieldName.ToUpper() == fi.FieldName.ToUpper())
                //    {
                //        _sourceFieldInfoList.Remove(fi0);
                //        break;
                //    }
                //}
            }
            while (listRow.Count>0)
            {
                _sourceTable.Rows.Remove(listRow[0]);
                listRow.RemoveAt(0);
            }
        }
    }
}