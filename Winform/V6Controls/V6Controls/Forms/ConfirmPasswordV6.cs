using System.Windows.Forms;
using V6SqlConnect;
using V6Tools;

namespace V6Controls.Forms
{
    /// <summary>
    /// <para>Hộp thoại xác nhận mật khẩu V6.</para>
    /// <para>Nếu đúng trả về DialogResult.OK</para>
    /// </summary>
    public partial class ConfirmPasswordV6 : V6Form
    {
        /// <summary>
        /// <para>Hộp thoại xác nhận mật khẩu V6.</para>
        /// <para>Nếu đúng trả về DialogResult.OK</para>
        /// </summary>
        public ConfirmPasswordV6()
        {
            InitializeComponent();
            //txtUserName.Text = V6LoginInfo.UserName;
            txtPassword.Focus();
            TopMost = true;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
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
