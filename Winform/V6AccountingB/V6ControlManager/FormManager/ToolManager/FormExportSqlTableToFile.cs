using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Export;

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
        
        /// <summary>
        /// Tên bảng trong csdl đang được chọn.
        /// </summary>
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

                if (chkAutoLoad.Checked) GetDataTable();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Select table change: " + ex.Message);
            }
            
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
                        if (ext.StartsWith(".xls")) // xls và xlsx
                        {
                            var setting = new ExportExcelSetting();
                            setting.data = exportData;
                            setting.saveFile = o.FileName;
                            V6Tools.V6Export.ExportData.ToExcel(setting);
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

                    if (exportData.Rows.Count == 0)
                    {
                        this.ShowInfoMessage("Chưa chọn dòng dữ liệu nào! Dùng Space hoặc Ctrl+A để chọn.");
                        return;
                    }
                    //if (chkConvert.Checked)
                    //{
                    //    string from = txtFrom.Text.Trim(), to = txtTo.Text.Trim();
                    //    exportData = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(exportData, from, to);
                    //}

                    // Bỏ cột UID
                    if (!chkUID.Checked && exportData.Columns.Contains("UID"))
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

        int count = 0;
        bool finish = false;
        string openFile = "";
        string insertTable = "";
        private void ImportBIG_XML()
        {
            count = 0;
            try
            {   
                using (XmlReader reader = XmlReader.Create(openFile))
                {
                    reader.MoveToContent();
                    Dictionary<string, object> one_row = null;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Depth == 1)
                        {
                            if (one_row != null && one_row.Count > 0)
                            {
                                //string stt_rec = one_row["STT_REC"].ToString().Trim();
                                //if (stt_rec == "Z202302024GL4")
                                //{
                                //    string a = stt_rec;
                                //}
                                // insert
                                SortedDictionary<string, object> insert_data = new SortedDictionary<string, object>(one_row);
                                if (insert_data.ContainsKey("UID")) insert_data.Remove("UID");
                                V6BusinessHelper.Insert(insertTable, insert_data);
                                count++;
                            }

                            one_row = new Dictionary<string, object>();
                            
                        }
                        else if (reader.Depth == 2 && reader.Name != "")
                        {
                            one_row[reader.Name.ToUpper()] = reader.ReadElementContentAsString();
                        }
                        else // so depth
                        {
                            // do ???
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage("Import count: " + count + "\r\n" + ex.Message);
            }
            finish = true;
        }
        
        private void btnUpdateXmlBy_Click(object sender, EventArgs e)
        {
            int count = 0;
            int count_fail = 0;
            string keys_fail = "";
            try
            {
                string BY = txtBy.Text.Trim().ToUpper();
                var openFile = V6ControlFormHelper.ChooseOpenFile(this, "Xml|*.xml");
                if (string.IsNullOrEmpty(openFile)) return;
                FileStream fs = new FileStream(openFile, FileMode.Open);
                var _ds = new DataSet();
                _ds.ReadXml(fs);
                fs.Close();
                if (_ds.Tables.Count > 0)
                {
                    var importData = _ds.Tables[0];
                    if (!importData.Columns.Contains(BY))
                    {
                        V6Message.ShowWarning("Không có cột dữ liệu: " + BY);
                        return;
                    }
                    
                    foreach (DataRow row in importData.Rows)
                    {
                        var row_data = row.ToDataDictionary();
                        IDictionary<string, object> key = new Dictionary<string, object>();
                        key[BY] = row_data[BY];
                        if (V6BusinessHelper.Update(selectedTableName, row_data, key) > 0)
                        {
                            count++;
                        }
                        else
                        {
                            count_fail++;
                            keys_fail += " " + key[BY];
                        }
                    }

                    if (keys_fail.Length > 0) keys_fail = keys_fail.Substring(1);
                    V6Message.Show(string.Format("Xong. {0}\nFail: {1}\nFail keys: {2}", count, count_fail, keys_fail) , 500, this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage("Import count: " + count + "\r\n" + ex.Message);
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
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

        private void btnGenInsertSQL_Click(object sender, EventArgs e)
        {
            try
            {
                var structTable = V6BusinessHelper.GetTableStruct(listBoxTablesName.SelectedItem.ToString());
                richTextBox1.Clear();
                var datas = dataGridView1.GetSelectedData();
                foreach (IDictionary<string, object> data in datas)
                {
                    richTextBox1.AppendText("\nGO\n");
                    var insert1 = SqlGenerator.GenInsertSql(V6Login.UserId, structTable.TableName, structTable, data);
                    richTextBox1.AppendText(insert1.Replace("\n", ""));
                }

                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {

                this.ShowErrorException(GetType() + ".btnGenInsertSQL_Click", ex);
            }
        }

        private void exportFoxProMenu_Click(object sender, EventArgs e)
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
                    o.Filter = "DatabaseFoxPro|*.dbf";

                    if (o.ShowDialog(this) == DialogResult.OK && o.FileName != null)
                    {
                        string ext = Path.GetExtension(o.FileName).ToLower();
                        bool no = false;
                        if (ext.StartsWith(".xls")) // xls và xlsx
                        {
                            var setting = new ExportExcelSetting();
                            setting.saveFile = o.FileName;
                            setting.data = exportData;
                            V6Tools.V6Export.ExportData.ToExcel(setting);
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
                V6Message.ShowErrorMessage(ex.Message, this);
            }
        }

        private void toExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnExportExcel_Click(sender, e);
        }

        private void rowsToXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRowsToXml_Click(sender, e);
        }

        private void toXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnExportXml_Click(sender, e);
        }

        private void importXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnImportXml_Click(sender, e);
        }

        private void importBIGXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedTableName))
                {
                    this.ShowMessage("Chưa chọn bảng dữ liệu sql.");
                    return;
                }
                openFile = V6ControlFormHelper.ChooseOpenFile(this, "Xml|*.xml");
                if (string.IsNullOrEmpty(openFile)) return;

                Thread T = new Thread(ImportBIG_XML);
                insertTable = selectedTableName;
                finish = false;
                count = 0;

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 1000;
                timer.Tick += Timer_Tick;
                T.Start();
                timer.Start();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {   
            if (finish)
            {
                ((System.Windows.Forms.Timer)sender).Stop();
                lblStatus.Text = "Insert finish: " + count;
                V6Message.Show("Đã nhập:  " + count, 500, this);
            }
            else
            {
                lblStatus.Text = "Insert count: " + count;
            }
        }

        private void reloadSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetDataTable();
        }
    }
}
