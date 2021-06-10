using System;
using System.Data;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager
{
    public partial class DataSelectorTagForm : V6Form
    {
        public DataSelectorTagForm()
        {
            InitializeComponent();
        }

        public DataSelectorTagForm(DataTable data)
        {
            InitializeComponent();
            Data = data;
            MyInit();
        }

        public DataTable Data;
        
        private void MyInit()
        {
            try
            {
                dataGridView1.DataSource = Data;
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
            if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
                return true;
            }
            return base.DoHotKey0(keyData);
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.ReadOnly = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.ReadOnly = true;
                }
                // cho phép sửa
                var column1 = dataGridView1.Columns["TI_LE_VV"];
                if (column1 != null)
                {
                    column1.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException("dataGridView1_DataSourceChanged", ex);
            }
        }

        private void dataGridView1_RowSelectChanged(object sender, SelectRowEventArgs e)
        {
            try
            {
                // Khi đổi lựa chọn thành có. Gán TI_LE_VV = 100
                if (e.Select)
                {
                    e.Row.Cells["TI_LE_VV"].Value = 100;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException("dataGridView1_RowSelectChanged", ex);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.OwningColumn == dataGridView1.Columns["TI_LE_VV"])
                {
                    decimal currentCellValue = ObjectAndString.ObjectToDecimal(dataGridView1.CurrentCell);
                    if (currentCellValue > 100)
                    {
                        dataGridView1.CurrentCell.Value = 100;
                    }
                    else if (currentCellValue < 0)
                    {
                        dataGridView1.CurrentCell.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}