using System.Windows.Forms;
using V6Init;

namespace V6Controls.Forms
{
    public partial class ConfirmPassword : V6Form
    {
        public UserConfig User = null;
        public ConfirmPassword()
        {
            InitializeComponent();
            txtUserName.Text = V6Login.UserName;
            txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                User = V6Login.LoginCheck(txtUserName.Text, txtPassword.Text, V6Login.Madvcs);
                if (User != null)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    this.ShowWarningMessage("Nhập sai.");
                }
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return base.DoHotKey0(keyData);
        }
    }
}
