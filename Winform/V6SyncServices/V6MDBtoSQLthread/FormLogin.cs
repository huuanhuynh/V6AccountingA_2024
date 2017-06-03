using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SingleInstanceThreads
{
    public partial class FormLogin : Form
    {
        int count = 0;
        public FormLogin()
        {
            InitializeComponent();
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckLogin(textBox1.Text.Trim(), textBox2.Text.Trim()))
                {
                    this.DialogResult = DialogResult.OK;
                    //this Form will close and return Result for ShowDialog method
                }
                else
                {
                    if (++count == 3)
                    {
                        this.DialogResult = DialogResult.No;
                    }
                    errorProvider1.SetError(textBox1, "Nhập sai!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Có lỗi kết nối SQL!\n" + ex.Message,
                    "V6Sync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "V6Sync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        bool CheckLogin(string username, string password)
        {
            DataTable dtb;
            string EPass = GetEPassFromSqlFunction(textBox2.Text);
            string sql = "select [user_name], [password], [is_admin] from [V6user]" +
                " where [user_name] = @user and [password] = '" + EPass + "'";

            SqlParameter prUser = new SqlParameter("@user", textBox1.Text.Trim());
            
            dtb = V6Library.SqlHelper.ExecuteDataset(FormMain.CONSTRING, CommandType.Text,
                sql,prUser).Tables[0];

            if (dtb.Rows.Count == 1 && dtb.Rows[0]["is_admin"].ToString().ToLower() == "true" && dtb.Rows[0]["password"].ToString().Substring(0,EPass.Length) == EPass)
            {                    
                return true;
            }
            else return false;
        }

        public string GetEPassFromSqlFunction(string pass)
        {
            pass = pass.Replace("'", "''");
            string queryString = "select dbo.VFA_FEADString('"+pass+"',30)";
            SqlConnection conn = new SqlConnection(FormMain.CONSTRING);
            SqlCommand comm = new SqlCommand(queryString, conn);
            conn.Open();
            SqlDataReader dataReaderSql = comm.ExecuteReader();
            //SqlDataReader dataReaderSql = V6Library.SqlHelper.ExecuteReader(FormMain.CONSTRING, CommandType.Text, queryString);// dataReaderSql = comm.ExecuteReader();
            string retu = "";
            while (dataReaderSql.Read())
            {
                retu = dataReaderSql[0].ToString();
                break;
            }
            dataReaderSql.Close();
            conn.Close();
            dataReaderSql.Dispose();
            conn.Dispose();
            
            return retu;
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
