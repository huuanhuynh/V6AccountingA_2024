using System;
using System.Data;
using System.Windows.Forms;
using V6ControlManager.FormManager.DanhMucManager;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class AlvtSelectorForm : V6Form
    {
        public AlvtSelectorForm()
        {
            InitializeComponent();
        }

        public AlvtSelectorForm(string ma_ct, string filter)
        {
            InitializeComponent();
            _ma_ct = ma_ct;
            _filter = filter;
            
            MyInit();
        }

        private string _ma_ct, _filter = null;
        private DanhMucView _dmv;
        private V6ColorDataGridView dataGridView1;
        
        private const string MA_VT = "MA_VT";
        private const string TEN_VT = "TEN_VT";
        private const string DVT = "DVT";
        private const string SO_LUONG1 = "SO_LUONG1";
        private const string SO_LUONG = "SO_LUONG";
        //private const string FIELD_NOSUM = "FIELD_NOSUM";

        private void MyInit()
        {
            try
            {
                _dmv = new DanhMucView("ITEMID", "Chọn vật tư", "ALVT", _filter, "MA_VT", ConfigManager.GetAldmConfig("ALVT"));
                _dmv.Dock = DockStyle.Fill;
                _dmv.btnBack.Visible = false;
                _dmv.btnFull.Visible = false;
                panel1.Controls.Add(_dmv);
                dataGridView1 = _dmv.dataGridView1;
                dataGridView1.KeyDown += dataGridView1_KeyDown;

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
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                {
                    e.Handled = true;
                    btnAddSelect_Click(sender, e);
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

        private void CopyRowLeftToRight(DataGridViewRow row)
        {
            if(row == null) return;

            DataRowView left_row_view = row.DataBoundItem as DataRowView;
            if (left_row_view != null)
            {
                DataRow left_row = left_row_view.Row;
                string ma_vt_value = left_row[MA_VT].ToString().Trim();
                // Check
                if (!chk2.Checked)
                {
                    foreach (DataRow row0 in _targetTable.Rows)
                    {
                        if (row0["MA_VT"].ToString().Trim().ToUpper() == ma_vt_value.ToUpper()) return;
                    }
                }

                DataRow copy = _targetTable.NewRow();
                copy[MA_VT] = left_row[MA_VT];
                copy[TEN_VT] = left_row[TEN_VT];
                copy["DVT1"] = left_row[DVT];
                copy[SO_LUONG1] = 1;
                copy[SO_LUONG] = 1;
                _targetTable.Rows.Add(copy);
            }
        }

        private void RemoveCurrentRowRight()
        {
            if (dataGridView2.CurrentRow == null) return;
            
            DataRowView right_row_view = dataGridView2.CurrentRow.DataBoundItem as DataRowView;
            if (right_row_view != null)
            {
                _targetTable.Rows.Remove(right_row_view.Row);
            }
        }

        private void AddAllRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                CopyRowLeftToRight(row);
            }
        }

        private void btnAddSelect_Click(object sender, EventArgs e)
        {
            CopyRowLeftToRight(dataGridView1.CurrentRow);
        }

        private void btnRemoveSelect_Click(object sender, EventArgs e)
        {
            RemoveCurrentRowRight();
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

        //private void btnTim_Click(object sender, EventArgs e)
        //{
        //    //int flag = dataGridView1.Rows.Count;
        //    if (dataGridView1.SelectedRows.Count == 1)
        //    {
        //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //        {
        //            if (dataGridView1.Rows[i].Selected == false) continue;

        //            for (int ii = i + 1; ii < dataGridView1.Rows.Count; ii++)
        //            {
        //                for (int j = 0; j < dataGridView1.Columns.Count; j++)
        //                {
        //                    if (
        //                        dataGridView1.Rows[ii].Cells[j].Value.ToString()
        //                            .ToLower()
        //                            .Contains(txtTim.Text.Trim().ToLower()))
        //                    {
        //                        dataGridView1.Rows[i].Selected = false;
        //                        dataGridView1.Rows[ii].Selected = true;
        //                        dataGridView1.CurrentCell = dataGridView1.Rows[ii].Cells[0];
        //                        return;
        //                    }
        //                }
        //            }
        //            dataGridView1.Rows[i].Selected = false;
        //            dataGridView1.Rows[0].Selected = true;
        //            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
        //            return;
        //        }
        //    }
        //    else //đang chọn nhiều hoặc không chọn
        //    {
        //        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
        //        {
        //            row.Selected = false;
        //        }
        //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < dataGridView1.Columns.Count; j++)
        //            {
        //                if (
        //                    dataGridView1.Rows[i].Cells[j].Value.ToString()
        //                        .ToLower()
        //                        .Contains(txtTim.Text.Trim().ToLower()))
        //                {
        //                    dataGridView1.Rows[i].Selected = true;
        //                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void btnTimTatCa_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView1.Rows.Count > 0)
        //    {
        //        dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];

        //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //        {
        //            dataGridView1.Rows[i].Selected = false;
        //            for (int j = 0; j < dataGridView1.Columns.Count; j++)
        //            {
        //                if (
        //                    dataGridView1.Rows[i].Cells[j].Value.ToString()
        //                        .ToLower()
        //                        .Contains(txtTim.Text.Trim().ToLower()))
        //                {
        //                    dataGridView1.Rows[i].Selected = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void txtTim_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        btnTim_Click(null, null);
        //}

        public string GetFieldsString()
        {
            string result = "";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                result += ";" + row.Cells[MA_VT].Value.ToString().Trim();
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }
        
        private DataTable _targetTable;
        DataTable GenTable2Struct()
        {
            _targetTable = new DataTable("GenFieldInfoList");
            _targetTable.Columns.Add(MA_VT, typeof(string));
            _targetTable.Columns.Add(TEN_VT, typeof (string));
            _targetTable.Columns.Add("DVT1", typeof(string));
            if(_ma_ct != "IXB") _targetTable.Columns.Add("MA_KHO_I", typeof(string));
            _targetTable.Columns.Add(SO_LUONG1, typeof (decimal));
            _targetTable.Columns.Add(SO_LUONG, typeof (decimal));
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
                panel1.Width = Width/2 - 55;
                btnAddSelect.Left = panel1.Right + 6;
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
            string FIELD = e.Column.DataPropertyName.ToUpper();
            if (FIELD == "SO_LUONG1" || FIELD == "MA_KHO_I")
            {
                e.Column.ReadOnly = false;
            }
            else
            {
                e.Column.ReadOnly = true;
            }
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

        private void AlvtSelectorForm_SizeChanged(object sender, EventArgs e)
        {
            FixSize();
        }
    }
}