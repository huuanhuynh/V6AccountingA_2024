using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6SqlConnect;
using V6Tools;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormExportSqlTableToFile : V6Form
    {
        public FormExportSqlTableToFile()
        {
            InitializeComponent();
        }

        string sqlGetTablesName = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME";
        private void ToolExportSqlToExcel_Load(object sender, EventArgs e)
        {
            GetDataTableList();
        }
        
        string selectedTableName = "";
        
        private void GetDataTableList()
        {
            try
            {
                DataTable tablesName = SqlConnect.ExecuteDataset(CommandType.Text, sqlGetTablesName).Tables[0];
                listBoxTablesName.Items.Clear();
                foreach (DataRow row in tablesName.Rows)
                {
                    string name = row["TABLE_NAME"].ToString();
                    listBoxTablesName.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GetDataTableList: " + ex.Message);
            }
        }
        DataTable data;
        string sqlSelectTable = "Select * from [{TableName}]";
        private void GetDataTable()
        {

            try
            {
                if (listBoxTablesName.SelectedItems.Count <= 0)
                {
                    if (listBoxTablesName.Items.Count > 0)
                        selectedTableName = listBoxTablesName.Items[0].ToString();
                }
                else
                {
                    selectedTableName = listBoxTablesName.SelectedItems[0].ToString();
                }
                data = SqlConnect.ExecuteDataset(CommandType.Text, sqlSelectTable.Replace("{TableName}", selectedTableName)).Tables[0];
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GetData: " + ex.Message);
            }
        }

        private void listBoxTablesName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDataTable();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (data != null)
                {
                    DataTable exportData;
                    if (chkConvert.Checked)
                    {
                        string from = txtFrom.Text.Trim(), to = txtTo.Text.Trim();
                        exportData = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(data, from, to);
                    }
                    else
                    {
                        exportData = data.Copy();
                    }
                    SaveFileDialog o = new SaveFileDialog();
                    o.Filter = "Excel 2003|*.xls|Excel 2007-2010|*.xlsx|DatabaseFox|*.dbf|Text file|*.txt|Xml file|*.xml";

                    if (o.ShowDialog(this) == DialogResult.OK && o.FileName != null)
                    {
                        string ext = Path.GetExtension(o.FileName).ToLower();
                        bool no = false;
                        if (ext.StartsWith(".xls"))
                        {
                            V6Tools.V6Export.ExportData.ToExcel(exportData, o.FileName, "");
                        }
                        else if (ext == ".dbf")
                        {
                            V6Tools.V6Export.ExportData.ToDbfFile(exportData, o.FileName);
                        }
                        else if (ext == ".txt")
                        {
                            V6Tools.V6Export.ExportData.ToTextFile(exportData, o.FileName);
                        }
                        else if (ext == ".xml")
                        {
                            V6Tools.V6Export.ExportData.ToXmlFile(exportData, o.FileName);
                        }
                        else
                        {
                            no = true;
                            V6Message.Show("Chưa hỗ trợ " + ext);
                            V6Tools.V6Export.ExportData.ToTextFile(exportData, o.FileName);
                        }
                        if (!no) V6Message.Show("Xong.", 500, this);
                    }
                }
                else
                {
                    V6Message.Show("Không có dữ liệu kết quả.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }
        
        private void btnExportXml_Click(object sender, EventArgs e)
        {
            try
            {
                if (data != null)
                {
                    DataTable exportData;
                    if (chkConvert.Checked)
                    {
                        string from = txtFrom.Text.Trim(), to = txtTo.Text.Trim();
                        exportData = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(data, from, to);
                    }
                    else
                    {
                        exportData = data.Copy();
                    }
                    SaveFileDialog o = new SaveFileDialog();
                    o.Filter = "Xml|*.xml";

                    if (o.ShowDialog(this) == DialogResult.OK && o.FileName != null)
                    {
                        //string ext = Path.GetExtension(o.FileName).ToLower();
                        bool no = false;
                        V6Tools.V6Export.ExportData.ToXmlFile(exportData, o.FileName);
                        V6Message.Show("Xong.", 500, this);
                    }
                }
                else
                {
                    V6Message.Show("Không có dữ liệu kết quả.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }
        
        private void btnRowsToXml_Click(object sender, EventArgs e)
        {
            try
            {
                if (data != null)
                {
                    string saveFile = V6ControlFormHelper.ChooseSaveFile(this, "Xml|*.xml");
                    if (string.IsNullOrEmpty(saveFile)) return;

                    DataTable exportData = data.Clone();
                    //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    //{
                    //    exportData.AddRow(((DataRowView) row.DataBoundItem).Row);
                    //}
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsSelect())
                        {
                            exportData.AddRow(((DataRowView)row.DataBoundItem).Row);
                        }
                    }
                    //if (chkConvert.Checked)
                    //{
                    //    string from = txtFrom.Text.Trim(), to = txtTo.Text.Trim();
                    //    exportData = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(exportData, from, to);
                    //}

                    // Bỏ cột UID
                    if (exportData.Columns.Contains("UID"))
                    {
                        exportData.Columns.Remove("UID");
                    }

                    V6Tools.V6Export.ExportData.ToXmlFile(exportData, saveFile);
                    V6Message.Show("Xong.", 500, this);
                }
                else
                {
                    V6Message.Show("Không có dữ liệu kết quả.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }
        
        private void btnImportXml_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                var openFile = V6ControlFormHelper.ChooseOpenFile(this, "Xml|*.xml");
                if (string.IsNullOrEmpty(openFile)) return;
                FileStream fs = new FileStream(openFile, FileMode.Open);
                var _ds = new DataSet();
                _ds.ReadXml(fs);
                fs.Close();
                if (_ds.Tables.Count > 0)
                {
                    var importData = _ds.Tables[0];
                    // Bỏ cột UID
                    if (importData.Columns.Contains("UID"))
                    {
                        importData.Columns.Remove("UID");
                    }
                    
                    foreach (DataRow row in importData.Rows)
                    {
                        V6BusinessHelper.Insert(selectedTableName, row.ToDataDictionary());
                        count++;
                    }
                    V6Message.Show("Xong. " + count, 500, this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage("Import count: " + count + "\r\n" + ex.Message);
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                RunSql(richTextBox1.Text);
            }
        }

        private void RunSql(string sql)
        {
            try
            {
                var ds = SqlConnect.ExecuteDataset(CommandType.Text, sql);
                data = ds.Tables.Count > 0 ? ds.Tables[0] : null;
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".RunSql", ex);
            }
        }
    }
}
