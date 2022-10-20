using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using V6Tools.V6Export;

namespace Tools
{
    public partial class ToolExportSqlToExcel : Form
    {
        public ToolExportSqlToExcel()
        {
            InitializeComponent();
        }

        SqlConnection con;
        //string connectionString0 = "Data Source=.; Integrated Security=True;";
        string connectionString1 = "Server=.;Database={DatabaseName};Trusted_Connection=Yes;";
        string connectionString2 = "Data Source={ServerName};Initial Catalog={DatabaseName};User ID={UserName};Password={Password}";
        string conString = "";
        string sqlGetTablesName = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='{DatabaseName}' ORDER BY TABLE_NAME";
        private void ToolExportSqlToExcel_Load(object sender, EventArgs e)
        {
            GetDatabaseList();
        }
        string svName = ".";
        string dbName = "";
        string tbName = "";
        private string GetConString1()
        {
            return connectionString1.Replace("{DatabaseName}", dbName);
        }
        private string GetConString()
        {
            svName = ".";
            dbName = "";
            tbName = "";
            
            if (!chkLogin.Checked)
            {
                conString = connectionString1;
                
                if (listBoxDatabases.SelectedItems.Count <= 0)
                {
                    if (listBoxDatabases.Items.Count > 0)
                        dbName = listBoxDatabases.Items[0].ToString();
                }
                else
                {
                    dbName = listBoxDatabases.SelectedItems[0].ToString();
                }
                conString = conString.Replace("{DatabaseName}", dbName);
            }
            else{
                conString = connectionString2;
                if (txtServerName.Text != "")
                    svName = txtServerName.Text;

                conString = conString.Replace("{ServerName}", svName);
                if (listBoxDatabases.SelectedItems.Count <= 0)
                {
                    if (listBoxDatabases.Items.Count > 0)
                        dbName = listBoxDatabases.Items[0].ToString();
                }
                else
                {
                    dbName = listBoxDatabases.SelectedItems[0].ToString();
                }
                conString = conString.Replace("{DatabaseName}", dbName);
                conString = conString.Replace("{UserName}", txtUserName.Text);
                conString = conString.Replace("{Password}", txtPassword.Text);
            }

            return conString;
        }
        
        private void GetDatabaseList()
        {
            listBoxDatabases.Items.Clear();
            GetConString();
            con = new SqlConnection(conString);

            try
            {
                con.Open();
                DataTable databases = con.GetSchema("Databases");
                con.Close();
                if (databases.Rows.Count > 0)
                {
                    listBoxDatabases.Items.Clear();
                    foreach (DataRow database in databases.Rows)
                    {
                        string databaseName = database["database_name"].ToString();
                        listBoxDatabases.Items.Add(databaseName);
                        //short dbID = database.Field<short>("dbid");
                        //DateTime creationDate = database.Field<DateTime>("create_date");
                    }
                    GetDataTableList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void GetDataTableList()
        {
            GetConString();
            DataTable tablesName =
            V6Tools.SqlHelper.ExecuteDataset(conString, CommandType.Text, sqlGetTablesName.Replace("{DatabaseName}", dbName)).Tables[0];
            listBoxTablesName.Items.Clear();
            foreach (DataRow row in tablesName.Rows)
            {
                string name = row["TABLE_NAME"].ToString();
                listBoxTablesName.Items.Add(name);
            }
        }
        DataTable data;
        string sqlSelectTable = "Select * from [{TableName}]";
        private void GetData()
        {
            
            if (listBoxTablesName.SelectedItems.Count <= 0)
            {
                if (listBoxTablesName.Items.Count > 0)
                    tbName = listBoxTablesName.Items[0].ToString();
            }
            else
            {
                tbName = listBoxTablesName.SelectedItems[0].ToString();
            }
            data = V6Tools.SqlHelper.ExecuteDataset(conString, CommandType.Text, sqlSelectTable.Replace("{TableName}", tbName)).Tables[0];
            dataGridView1.DataSource = data;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            listBoxDatabases.Items.Clear();
            GetConString();
            GetDatabaseList();
        }

        private void chkLogin_CheckedChanged(object sender, EventArgs e)
        {
            if(chkLogin.Checked)
            {
                grbLogin.Enabled = true;
            }
            else
            {
                grbLogin.Enabled = false;
                GetDatabaseList();
            }
        }

        private void listBoxDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetConString();
            GetDataTableList();
        }

        private void listBoxTablesName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (data != null)
                {
                    DataTable table2 = data;
                    if(chkConvert.Checked)
                    {
                        string from = txtFrom.Text.Trim(), to = txtTo.Text.Trim();

                        table2 = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(data, from, to);
                    }
                    SaveFileDialog o = new SaveFileDialog();
                    o.Filter = "Excel 2003|*.xls|Excel 2007-2010|*.xlsx|DatabaseFox|*.dbf|Text file|*.txt|Xml file|*.xml";

                    if (o.ShowDialog() == DialogResult.OK)
                    {
                        string ext = Path.GetExtension(o.FileName).ToLower();
                        bool no = false;
                        if (ext.StartsWith(".xls"))
                        {
                            ExportData.ToExcel(table2, new ExportExcelSetting(), o.FileName, "");
                        }
                        else if (ext == ".dbf")
                        {
                            ExportData.ToDbfFile(table2, o.FileName);
                        }
                        else if (ext == ".txt")
                        {
                            ExportData.ToTextFile(table2, o.FileName);
                        }
                        else if (ext == ".xml")
                        {
                            ExportData.ToXmlFile(table2, o.FileName);
                        }
                        else
                        {
                            no = true;
                            MessageBox.Show("Chưa hỗ trợ " + ext);
                            ExportData.ToTextFile(table2, o.FileName);
                        }
                        if (!no) MessageBox.Show("Xong.");
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu kết quả.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void txtServerName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                GetDatabaseList();
            }
        }

        private void btnBrowMdf_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "All file|*.*|Mdf file|*.mdf";
                if(op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtDatabaseFileAttach.Text = op.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnBrowLdf_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "All file|*.*|Mdf file|*.mdf";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtLogFileAttach.Text = op.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Server=(local);	Data Source=;Integrated Security=SSPI");
                SqlCommand cmd = new SqlCommand("", conn);

                string dbName = txtDatabaseNameAttach.Text;
                string dbFile = txtDatabaseFileAttach.Text;
                string logFile = "";
                if(dbFile.Contains('.') && dbFile.Length>5)
                {
                    logFile = dbFile.Substring(0, dbFile.Length - 3) + "ldf";
                }
                bool haveLog = File.Exists(logFile);
                string sql = "";
                if (radAttachSP.Checked)
                {
                    sql = "exec sys.sp_attach_db " + dbName + ", '" + dbFile + "'";
                    if (haveLog) sql += ", '" + logFile + "'";
                }
                if (radAttachSql.Checked)
                {
                    sql =
                        "CREATE DATABASE '" + dbName + "' ON " +
                        "PRIMARY ( FILENAME =  '" + dbFile + "' ) " +
                        (haveLog ? ", FILEGROUP MyDatabase_Log ( FILENAME = '" + logFile + "') " : "") +
                        "FOR ATTACH";
                }
                cmd.CommandText = sql;

                conn.Open();

                int i = cmd.ExecuteNonQuery();
                lblAttachResult.Text = "result " + i.ToString();

                cmd.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.F5)
                {
                    string constring = GetConString();
                    string sql = txtQuery.Text;
                    DataSet ds = V6Tools.SqlHelper.ExecuteDataset(constring, CommandType.Text, sql);
                    if(ds.Tables.Count>0)
                    {
                        data = ds.Tables[0];
                        dataGridView1.DataSource = data;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExportXml_Click(object sender, EventArgs e)
        {
            try
            {
                if (data != null)
                {
                    DataTable table2 = data;
                    if (chkConvert.Checked)
                    {
                        string from = txtFrom.Text.Trim(), to = txtTo.Text.Trim();

                        table2 = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(data, from, to);
                    }
                    SaveFileDialog o = new SaveFileDialog();
                    o.Filter = "Excel 2003|*.xls|Excel 2007-2010|*.xlsx|DatabaseFox|*.dbf|Text file|*.txt";

                    if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string ext = Path.GetExtension(o.FileName).ToLower();
                        bool no = false;
                        if (ext.StartsWith(".xls"))
                        {
                            ExportData.ToExcel(table2, new ExportExcelSetting(), o.FileName, "");
                        }
                        else if (ext == ".dbf")
                        {
                            ExportData.ToDbfFile(table2, o.FileName);
                        }
                        else if (ext == ".txt")
                        {
                            ExportData.ToTextFile(table2, o.FileName);
                        }
                        else
                        {
                            no = true;
                            MessageBox.Show("Chưa hỗ trợ " + ext);
                            ExportData.ToTextFile(table2, o.FileName);
                        }
                        if (!no) MessageBox.Show("Xong.");
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu kết quả.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
