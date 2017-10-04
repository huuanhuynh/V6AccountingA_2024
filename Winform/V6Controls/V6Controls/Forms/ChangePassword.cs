using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms
{
    public partial class ChangePassword : V6Form
    {
        public ChangePassword()
        {
            InitializeComponent();
            txtUserName.Text = V6Login.UserName;
            txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DoChangePassword();
        }

        private void DoChangePassword()
        {
            try
            {
                if (V6Login.Login(txtUserName.Text, txtPassword.Text, V6Login.Madvcs))
                {
                    //Không cho phép giống password cũ, không được để trống password mới
                    if (txtPassword1.Text == txtPassword.Text || txtPassword1.Text.Trim() == "")
                    {
                        this.ShowWarningMessage(V6Text.CheckInfor);
                        return;
                    }

                    if (txtPassword1.Text != txtPassword2.Text)
                    {
                        this.ShowWarningMessage(V6Text.Wrong);
                        return;
                    }

                    var TxtPassword_Text = UtilityHelper.EnCrypt(txtUserName.Text.Trim() + txtPassword1.Text.Trim());
                    var TxtCodeUser_Text =
                        UtilityHelper.EnCrypt(txtUserName.Text.Trim() + txtPassword1.Text.Trim() +
                                                (V6Login.IsAdmin ? "1" : "0"));
                    DateTime pass_date = V6Setting.M_SV_DATE;

                    V6Categories ct = new V6Categories();
                    SortedDictionary<string, object> newData = new SortedDictionary<string, object>();
                    newData["PASSWORD"] = TxtPassword_Text;
                    newData["CODE_USER"] = TxtCodeUser_Text;
                    newData["PASS_DATE"] = pass_date;
                    SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                    keys["UID"] = V6Login.UserInfo["UID"];
                    int updated = ct.Update(V6TableName.V6user, newData, keys);
                    
                    if (updated > 0)
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.ShowWarningMessage("Có lỗi xảy ra!");// + ct.Message);
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.Wrong);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoChangePassword", ex);
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

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            DoChangePassword();
        }

        private void buttonCANCEL_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
