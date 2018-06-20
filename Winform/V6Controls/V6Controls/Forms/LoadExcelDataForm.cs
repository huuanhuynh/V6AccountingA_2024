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
        public LoadExcelDataForm()
        {
            InitializeComponent();
        }

        private DataTable data;
        public event DataTableHandler AcceptData;
        public string CheckFields = null;
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
            if (!string.IsNullOrEmpty(MA_CT))
            {
                //Check khác
            }
            InvokeDynamicFix();
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
            try
            {
                //IMPORT_EXCEL MA_CT + _AD
                string path1 = Application.StartupPath;
                string file1 = MA_CT + "_AD.XLS";
                path1 = Path.Combine(path1, "IMPORT_EXCEL");
                path1 = Path.Combine(path1, file1);
                if (File.Exists(path1))
                {
                    //Copy to tempfolder
                    string path2 = V6ControlsHelper.CreateV6SoftLocalAppDataDirectory();
                    path2 = Path.Combine(path2, file1);
                    if(File.Exists(path2)) File.Delete(path2);
                    File.Copy(path1, path2);

                    ProcessStartInfo info1 = new ProcessStartInfo(path2);
                    Process.Start(info1);
                }
                else
                {
                    ShowTopLeftMessage(string.Format("{0} [{1}]", V6Text.NotExist, file1));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XemMauExcel", ex);
            }
        }

        private object InvokeDynamicFix()
        {
            try // Dynamic invoke
            {
                if (Program != null && !string.IsNullOrEmpty(DynamicFixMethodName))
                {
                    All_Objects["data"] = data;
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
