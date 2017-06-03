using System;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;

namespace V6AccountingB
{
    public partial class LockForm : V6Form
    {
        public LockForm()
        {
            InitializeComponent();
        }
        public LockForm(string userName)
        {
            InitializeComponent();
            txtUserName.Text = userName;
            lblCompanyName.Text = V6Soft.V6SoftValue["M_TEN_CTY"].ToUpper();
        }

        private bool _hardClose;
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (V6Login.Login(txtUserName.Text, txtPassword.Text, V6Login.Madvcs))
                {
                    txtPassword.Clear();
                    _hardClose = true;
                    Close();
                }
            }
        }

        private void LockControl_Load(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtPassword.Focus();
        }

        private void LockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!_hardClose)
                e.Cancel = e.CloseReason == CloseReason.UserClosing;
        }
    }
}
