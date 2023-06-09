using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormUpdateTableDic : V6Form
    {
        public FormUpdateTableDic()
        {
            InitializeComponent();
        }

        List<string> _file, _file_result;
        Dictionary<string,string> _replace1, _replace2;
        OpenFileDialog o = new OpenFileDialog();

        private void LoadAutoComplete()
        {
            try
            {
                txtTableName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtTableName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection data = new AutoCompleteStringCollection();
                string sqlGetTablesName = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME";
                DataTable tablesName = SqlConnect.ExecuteDataset(CommandType.Text, sqlGetTablesName).Tables[0];
                foreach (DataRow row in tablesName.Rows)
                {
                    string name = row["TABLE_NAME"].ToString();
                    data.Add(name);
                }
                txtTableName.AutoCompleteCustomSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void btnGenUpdateSql_Click(object sender, EventArgs e)
        {
            GenUpdateSql();
        }

        private void GenUpdateSql()
        {
            try
            {
                string sql = string.Format("Update {0} Set {1} = @Value1 Where {2} = @Kvalue",
                    txtTableName.Text, txtUpdateField.Text, txtKeyField.Text);
                
                richSQL.Text = sql;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void CheckFilterField()
        {
            try
            {
                var struc = V6BusinessHelper.GetTableStruct(txtTableName.Text);
                if (struc == null || struc.Count == 0)
                {
                    return;
                }
                // nếu bảng thay đổi
                if (struc.ContainsKey(txtFilterField.Text.ToUpper()))
                {
                    // giữ nguyên
                }
                else
                {
                    txtFilterField.Clear();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        string threadMessage = "";
        bool finish = false, _override = false;
        private void ThreadUpate()
        {
            try
            {
                var struc = V6BusinessHelper.GetTableStruct(txtTableName.Text);
                // Get old data
                string where0 = string.Format("{0}=@Fvalue", txtFilterField.Text);
                List<SqlParameter> plist = new List<SqlParameter>();
                plist.Add(new SqlParameter("Fvalue", txtFilterValue.Text));
                Dictionary<string, object> keys = new Dictionary<string, object>();
                keys[txtFilterField.Text.ToUpper()] = txtFilterValue.Text;
                string where = SqlGenerator.GenWhere(struc, keys);
                var oldData = V6BusinessHelper.Select(txtTableName.Text,
                    txtKeyField.Text + "," + txtUpdateField.Text + "," + txtFilterField.Text, where).Data;
                // Lặp qua các dòng để tạo Update.

                foreach (DataRow item in oldData.Rows)
                {

                }

            }
            catch (Exception ex)
            {
                threadMessage += "\r\n" + ex.Message;
            }
            finish = true;
        }

        private void DoUpdate()
        {
            try
            {
                // Chuẩn bị giá trị.
                finish = false;
                threadMessage = "Bắt đầu";
                _override = checkBox1.Checked;
                // Tạo Thread Update
                Thread thread = new Thread(ThreadUpate);
                Timer timer = new Timer();
                timer.Tick += Timer_Tick;
                timer.Start();
                thread.Start();

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                
                int newMessageLength = threadMessage.Length;
                richStatus.AppendText(threadMessage.Substring(0, newMessageLength));
                threadMessage = threadMessage.Substring(newMessageLength);
                richStatus.SelectionStart = richStatus.TextLength;
                richStatus.ScrollToCaret();
                
                if (finish)
                {
                    ((Timer)sender).Stop();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        string _tableName = "";
        private void txtTableName_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtTableName_V6LostFocus(object sender)
        {
            _tableName = txtTableName.Text;
            CheckFilterField();
            GenUpdateSql();
        }

        private void btnDoUpdate_Click(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void FormHuuanEditText_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAutoComplete();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        

        
        
    }
}
