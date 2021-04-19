using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;

using V6Tools;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class DataSelectorForm : V6Form
    {
        public DataSelectorForm()
        {
            InitializeComponent();
        }

        public DataSelectorForm(DataTable data, string filter)
        {
            InitializeComponent();
            Data = data;
            _filter = filter;
            
            MyInit();
        }

        public DataTable Data;
        private string _filter = null;
        //private const string MA_VT = "MA_VT";
        
        private void MyInit()
        {
            try
            {
                
                dataGridView1.KeyDown += dataGridView1_KeyDown;
                dataGridView1.DataSource = Data;
                dataGridView2.DataSource = GenTable2Struct();
                dataGridView2.KeyDown += dataGridView2_KeyDown;
                if (dataGridView2.Columns.Contains("MA_KHO_I")) dataGridView2.ChangeColumnType("MA_KHO_I", typeof(V6VvarDataGridViewColumn), "CMA_KHO");
            }
            catch (Exception ex)
            {
                this.ShowErrorException("MyInit", ex);
            }
        }

        private void SelectMultiIDForm_Load(object sender, EventArgs e)
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

        public override bool DoHotKey0(Keys keyData)
        {
            if (dataGridView2.EditingCell != null) return base.DoHotKey0(keyData);
            if (keyData == Keys.Enter)
            {
                btnNhan.PerformClick();
                return true;
            }
            return base.DoHotKey0(keyData);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    e.Handled = true;
                    CopyRowLeftToRight(dataGridView1.CurrentRow.ToDataDictionary());
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_KeyDown", ex);
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CopyRowLeftToRight(IDictionary<string, object> rowData)
        {
            if(rowData == null || rowData.Count == 0) return;

            _targetTable.AddRow(rowData);
        }

        private void RemoveRowsBySelectedCellsRight()
        {
            if (dataGridView2.CurrentRow == null) return;
            
            SortedDictionary<int, DataGridViewRow> rowDic = new SortedDictionary<int, DataGridViewRow>();
            foreach (DataGridViewCell cell in dataGridView2.SelectedCells)
            {
                int i = cell.RowIndex;
                if(!rowDic.ContainsKey(i)) rowDic.Add(i, cell.OwningRow);
            }

            foreach (KeyValuePair<int, DataGridViewRow> item in rowDic)
            {
                DataRowView right_row_view = item.Value.DataBoundItem as DataRowView;
                if (right_row_view != null)
                {
                    _targetTable.Rows.Remove(right_row_view.Row);
                }
            }
        }

        private void AddAllRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                CopyRowLeftToRight(row.ToDataDictionary());
            }
        }

        private void btnAddSelect_Click(object sender, EventArgs e)
        {
            //CopyRowLeftToRight(dataGridView1.CurrentRow);
            SortedDictionary<int, DataGridViewRow> rowDic = new SortedDictionary<int, DataGridViewRow>();
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                int i = cell.RowIndex;
                if(!rowDic.ContainsKey(i)) rowDic.Add(i, cell.OwningRow);
            }

            foreach (KeyValuePair<int, DataGridViewRow> item in rowDic)
            {
                CopyRowLeftToRight(item.Value.ToDataDictionary());
            }
        }

        private void btnRemoveSelect_Click(object sender, EventArgs e)
        {
            RemoveRowsBySelectedCellsRight();
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            AddAllRows();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                while (_targetTable.Rows.Count > 0)
                {
                    _targetTable.Rows.Remove(_targetTable.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException("btnRemoveAll_Click", ex);
            }
        }
        
        private DataTable _targetTable;
        DataTable GenTable2Struct()
        {
            _targetTable = new DataTable("GenFieldInfoList");
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                _targetTable.Columns.Add(column.Name, column.ValueType);
            }
            return _targetTable;
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
                dataGridView2.Left = btnAddSelect.Right + 16;
                dataGridView2.Width = Width - dataGridView2.Left - 50;
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
            //string FIELD = e.Column.DataPropertyName.ToUpper();
            //if (FIELD == "SO_LUONG1" || FIELD == "MA_KHO_I")
            //{
            //    e.Column.ReadOnly = false;
            //}
            //else
            //{
            //    e.Column.ReadOnly = true;
            //}
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

        private void DataSelectorForm_SizeChanged(object sender, EventArgs e)
        {
            FixSize();
        }
        
    }
}