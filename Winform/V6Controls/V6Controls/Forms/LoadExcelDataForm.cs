using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls.Forms
{
    public partial class LoadExcelDataForm : V6Form
    {
        public LoadExcelDataForm()
        {
            InitializeComponent();
        }

        private DataTable data;
        public event DataTableHandler AcceptData;
        public string CheckFields = null;

        protected virtual void OnAcceptData(DataTable table)
        {
            var handler = AcceptData;
            if (handler != null) handler(table);
        }

        private void Nhan()
        {
            if (data == null)
            {
                this.ShowMessage(V6Text.NoData);
                return;
            }
            Close();
            OnAcceptData(data);
        }

        private void Huy()
        {
            Close();
        }

        private void LoadExcel(string file)
        {
            if (!File.Exists(txtFile.Text))
            {
                this.ShowInfoMessage(V6Text.NotExist);
                return;
            }

            data = V6Tools.V6Convert.Excel_File.Sheet1ToDataTable(file);
            if (chkChuyenMa.Checked && comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0)
            {
                data = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(data, comboBox1.SelectedItem.ToString(),
                    comboBox2.SelectedItem.ToString());
            }

            if (!string.IsNullOrEmpty(CheckFields))
            {
                if (!V6ControlFormHelper.CheckDataFields(data, ObjectAndString.SplitString(CheckFields)))
                    this.ShowWarningMessage("Dữ liệu không hợp lệ! " + CheckFields);
            }
            dataGridView1.DataSource = data;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog f = new OpenFileDialog
                {
                    Filter = "Excel|*.xls;*.xlsx",
                    Multiselect = false,
                    Title = "Chọn tệp excel"
                };
                if (f.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = f.FileName;
                    LoadExcel(f.FileName);
                    f.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFile.Text != "")
                {
                    LoadExcel(txtFile.Text);
                }
                else
                {
                    this.ShowInfoMessage("Chưa chọn tập tin.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ReLoad", ex);
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void chkChuyenMa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkChuyenMa.Checked)
                {
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    if (comboBox1.SelectedIndex < 0) comboBox1.SelectedIndex = 0;
                    if (comboBox2.SelectedIndex < 0) comboBox2.SelectedIndex = 0;
                }
                else
                {
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".chk", ex);
            }
        }

        

        
    }
}
