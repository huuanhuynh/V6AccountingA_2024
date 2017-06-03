using System.Windows.Forms;
using V6SqlConnect;
using V6Tools;

namespace V6Controls.Forms
{
    public partial class ConfirmPasswordV6 : V6Form
    {
        public ConfirmPasswordV6()
        {
            InitializeComponent();
            //txtUserName.Text = V6LoginInfo.UserName;
            txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            if (txtUserName.Text == "V6" && txtPassword.Text != ""
                && UtilityHelper.EnCrypt(txtPassword.Text) == DatabaseConfig.PasswordV6)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
