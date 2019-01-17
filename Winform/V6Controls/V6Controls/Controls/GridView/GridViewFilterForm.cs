using System;
using System.Data;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Controls.GridView
{
    public partial class GridViewFilterForm : V6Form
    {
        public GridViewFilterForm()
        {
            InitializeComponent();
        }

        private DataGridView _grid;
        public GridViewFilterForm(DataGridView grid)
        {
            InitializeComponent();
            _grid = grid;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                if (_grid.DataSource is DataView)
                {
                    if(!string.IsNullOrEmpty(((DataView)_grid.DataSource).RowFilter))
                        chkFindNext.Checked = true;
                }
                comboBox1.Items.Add("start");
                comboBox1.SelectedItem = "start";
                var currentColumn = _grid.Columns[_grid.CurrentCell.ColumnIndex];
                lblField.Text = currentColumn.HeaderText;
                Field = currentColumn.DataPropertyName;
                FindNext = chkFindNext.Checked;
                FindOR = chkFindOr.Checked;
                if (ObjectAndString.IsStringType(currentColumn.ValueType))
                {
                    comboBox1.Visible = true;
                    txtValue.Visible = true;
                    Operator = comboBox1.SelectedItem.ToString();
                    comboBox1.SelectedIndexChanged += (s, e) =>
                    {
                        Operator = comboBox1.SelectedItem.ToString();
                    };
                    txtValue.TextChanged += (sender, e) =>
                    {
                        Value = txtValue.Text;
                    };
                }
                else if (ObjectAndString.IsNumberType(currentColumn.ValueType))
                {
                    label1.Visible = true;
                    label2.Visible = true;
                    numValue.Top = txtValue.Top;
                    numValue2.Top = txtValue.Top;
                    numValue.Visible = true;
                    numValue2.Visible = true;
                    
                    Value = numValue.Value;
                    Value2 = numValue2.Value;
                    numValue.TextChanged += (sender, e) =>
                    {
                        Value = numValue.Value;
                    };
                    numValue2.TextChanged += (sender, e) =>
                    {
                        Value2 = numValue2.Value;
                    };
                }
                else if (ObjectAndString.IsDateTimeType(currentColumn.ValueType))
                {
                    label1.Visible = true;
                    label2.Visible = true;
                    dateValue.Top = txtValue.Top;
                    dateValue2.Top = txtValue.Top;
                    dateValue.Visible = true;
                    dateValue2.Visible = true;

                    var currentCellValue = ObjectAndString.ObjectToFullDateTime(_grid.CurrentCell.Value);
                    dateValue.SetValue(currentCellValue);
                    dateValue2.SetValue(currentCellValue);

                    Value = dateValue.Date;
                    Value2 = dateValue2.Date;
                    dateValue.ValueChanged += (sender, e) =>
                    {
                        Value = dateValue.Date;
                    };
                    dateValue2.ValueChanged += (sender, e) =>
                    {
                        Value2 = dateValue2.Date;
                    };
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        

        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public object Value2 { get; set; }
        public bool FindNext { get; set; }
        public bool FindOR { get; set; }


        private void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void chkFindNext_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFindNext.Checked) chkFindOr.Checked = false;
            FindNext = chkFindNext.Checked;
        }

        private void chkFindOr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFindOr.Checked) chkFindNext.Checked = false;
            FindOR = chkFindOr.Checked;
        }
    }
}
