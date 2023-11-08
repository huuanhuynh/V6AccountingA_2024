using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls.Forms
{
    public partial class LoadExcelDataForm : V6Form
    {
        /// <summary>
        /// Form đọc dữ liệu từ Excel.
        /// </summary>
        public LoadExcelDataForm()
        {
            InitializeComponent();
        }
        
        private DataTable data;
        public event DataTableHandler AcceptData;
        public event ControlEventHandle LoadDataComplete;
        public string CheckFields = null;
        public string MODE = "";
        /// <summary>
        /// Các cột kiểu ngày. Sửa lỗi khi đọc ngày thành chuỗi số trên file excel.
        /// </summary>
        public IList<string> CheckDateFields = null;
        /// <summary>
        /// Dùng kiểm tra thông tin khác khi tải dữ liệu Excel.
        /// </summary>
        public string MA_CT;

        public Type Program { get; set; }
        public IDictionary<string, object> All_Objects = new SortedDictionary<string, object>();
        public string DynamicFixMethodName { get; set; }

        private void LoadExcelDataForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(MA_CT))
                {
                    btnXemMauExcel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Form_Load", ex);
            }
        }

        protected virtual void OnLoadDataComplete()
        {
            var handler = LoadDataComplete;
            if (handler != null) handler(this);
        }
        
        protected virtual void OnAcceptData(DataTable table)
        {
            var handler = AcceptData;
            if (handler != null) handler(table);
        }

        public string ERROR = "";
        private void Nhan()
        {
            if (data == null)
            {
                this.ShowMessage(V6Text.NoData);
                return;
            }
            if (!string.IsNullOrEmpty(ERROR))
            {
                this.ShowErrorMessage(ERROR);
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

            data = Excel_File.Sheet1ToDataTable(file);
            if (chkChuyenMa.Checked && comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0)
            {
                data = Data_Table.ChuyenMaTiengViet(data, comboBox1.SelectedItem.ToString(),
                    comboBox2.SelectedItem.ToString());
            }

            if (CheckDateFields != null)
            {
                foreach (string date_field in CheckDateFields)
                {
                    V6ControlFormHelper.FixDateColumn(data, date_field);
                }
            }
            if (!string.IsNullOrEmpty(CheckFields))
            {
                if (!V6ControlFormHelper.CheckDataFields(data, ObjectAndString.SplitString(CheckFields)))
                    this.ShowWarningMessage("Dữ liệu không hợp lệ! " + CheckFields);
            }
            if (!string.IsNullOrEmpty(MA_CT))
            {
                //Check khác
            }
            dataGridView1.DataSource = data;
            OnLoadDataComplete();
            InvokeDynamicFix();
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
                if (f.ShowDialog(this) == DialogResult.OK)
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

        private void btnXemMauExcel_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.OpenExcelTemplate(MA_CT + MODE + "_AD.XLS", "IMPORT_EXCEL");
        }

        private object InvokeDynamicFix()
        {
            try // Dynamic invoke
            {
                if (Program != null && !string.IsNullOrEmpty(DynamicFixMethodName))
                {
                    All_Objects["data"] = data;
                    All_Objects["excelForm"] = this;
                    return V6ControlsHelper.InvokeMethodDynamic(Program, DynamicFixMethodName, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + DynamicFixMethodName, ex1);
            }
            return null;
        }

        private void btnDynamicFix_Click(object sender, EventArgs e)
        {
            InvokeDynamicFix();
        }

        
    }
}
