using System;
using System.Windows.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms
{
    /// <summary>
    /// <para>Hộp thoại xác nhận mật khẩu V6.</para>
    /// <para>Nếu đúng trả về DialogResult.OK</para>
    /// </summary>
    public partial class ConfirmPasswordF368 : V6Form
    {
        /// <summary>
        /// <para>Hộp thoại xác nhận mật khẩu V6.</para>
        /// <para>Nếu đúng trả về DialogResult.OK</para>
        /// </summary>
        public ConfirmPasswordF368()
        {
            InitializeComponent();
            txtUserName.Text = Random4();
            txtPassword.Focus();
            TopMost = true;
        }

        private string Random4()
        {
            Random random = new Random();
            string s = ("0000" + random.Next(1111, 10000)).Right(4);
            return s;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (UtilityHelper.EnCrypt(txtPassword.Text) == DatabaseConfig.PasswordV6)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    if (txtUserName.Text.Length != 4) DialogResult = DialogResult.Cancel;
                    // số tt1 + tt2*2 + tt3*3 + tt4 + V6OPTION.M_RULE_PASS (bỏ trắng =6)
                    var calculator = ObjectAndString.ObjectToInt(txtUserName.Text.Left(1));
                    calculator += ObjectAndString.ObjectToInt(txtUserName.Text.Substring(1, 1)) * 2;
                    calculator += ObjectAndString.ObjectToInt(txtUserName.Text.Substring(2, 1)) * 3;
                    calculator += ObjectAndString.ObjectToInt(txtUserName.Text.Substring(3, 1));
                    calculator += ObjectAndString.ObjectToInt(V6Options.M_RULE_PASS);
                    if (ObjectAndString.ObjectToInt(txtPassword.Text) == calculator)
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
