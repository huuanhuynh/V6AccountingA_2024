using System;
using System.Windows.Forms;
using V6Tools;

namespace V6Controls.Forms
{
    public partial class EmailSettingForm : V6Form
    {
        public EmailSettingForm()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            //txtEmail.Text = txtEmail.Text;
            //txtPassword
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            EmailSender mailSender = new EmailSender();
            try
            {
                mailSender.SendEmail(txtEmail.Text, txtPassword.Text, txtSendTo.Text,
                        txtSubject.Text, txtContent.Text, txtAttachment.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrownFile_Click(object sender, EventArgs e)
        {
            string file = V6ControlFormHelper.ChooseOpenFile(this, "All file|*.*");
            txtAttachment.Text = file;
        }
    }
}
