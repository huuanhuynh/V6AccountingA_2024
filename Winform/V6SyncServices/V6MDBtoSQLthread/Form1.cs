using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6MDBtoSQLthread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string[] sss = new string[6];
            string error = "";

            if (txtServer.Text.Trim() != "")
                sss[0] = txtServer.Text.Trim();
            else error += "Server không được để trống!\n";

            if (txtDatabase.Text.Trim() != "")
                sss[1] = txtDatabase.Text.Trim();
            else error += "Database không được để trống!\n";

            if (txtUser.Text.Trim() != "")
                sss[2] = txtUser.Text.Trim();
            else error += "User không được để trống!\n";

            if (txtPassword.Text.Trim() != "")
                sss[3] = V6Library.UtilityHelper.EnCrypt(txtPassword.Text.Trim());
            else sss[3] = "";

            sss[4] = txtRun.Text.Trim();

            //sss[5] = txtGhiChu.Text;

            if (error.Length > 0)
            {
                MessageBox.Show(error, "V6Sync", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //if (dtServerList != null)
                //{
                //    DataRow row = dtServerList.NewRow();
                //    row["Server"] = sss[0];
                //    row["Database"] = sss[1];
                //    row["User"] = sss[2];
                //    row["EPass"] = sss[3];
                //    row["Run"] = sss[4];
                //    dtServerList.Rows.Add(row);
                //}
                //else
                //{
                //    throw new NullReferenceException("Bảng cấu hình Server không tồn tại!");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "V6Sync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtServer.Clear();
            txtDatabase.Clear();
            txtUser.Clear();
            txtPassword.Clear();
            txtRun.Clear();

            txtServer.Focus();
        }

        private void chkViewPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViewPass.Checked)
            {
                if (MessageBox.Show("Có chắc bạn muốn hiện mật khẩu?", "V6Sync", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    txtPassword.PasswordChar = (char)0;
                }
                else
                {
                    chkViewPass.Checked = false;
                }
            }
            else
            {
                txtPassword.PasswordChar = '#';
            }
        }
    }
}
