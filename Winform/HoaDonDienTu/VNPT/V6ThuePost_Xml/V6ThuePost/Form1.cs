using System;
using System.Windows.Forms;
using V6ThuePost.ResponseObjects;
using V6ThuePostXmlApi;

namespace V6ThuePost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string xmlData = Program.ReadDataXml(txtDbfFile.Text);
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnTest.Enabled = true;
            btnSend.Enabled = true;
            btnSendS.Enabled = false;

            Program.vnptWS = new VnptWS(Program._baseLink, Program.account, Program.accountpassword, Program.username, Program.password);
        }
        
        private void btnReadS_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string xmlData = Program.ReadDataXmlS(txtDbfFile.Text);
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnSend.Enabled = false;
            btnSendS.Enabled = true;
        }
        
        private void btnReadT_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string xmlData = Program.ReadDataXmlT(txtDbfFile.Text);
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnSend.Enabled = false;
            btnSendS.Enabled = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string result = Program.ImportAndPublishInv("<V6test>Test</V6test>");
            lblResult.Text = result;
            if (result != null && result.Contains("Dữ liệu xml đầu vào không đúng quy định"))
            {
                BaseMessage.Show("Kết nối ổn!", 500, this);
            }
            else
            {
                BaseMessage.Show(result);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string result = null;
            try
            {
                BaseMessage.Show("CERT:" + Program._SERIAL_CERT, 0, this);
                V6Return v6Return;
                if (string.IsNullOrEmpty(Program._SERIAL_CERT))
                {
                    //result = Program.ImportAndPublishInv(richTextBox1.Text);
                    result = Program.vnptWS.ImportAndPublishInv(richTextBox1.Text, Program._pattern, Program._seri, out v6Return);
                }
                else
                {
                    Program.StartAutoInputTokenPassword();
                    result = Program.vnptWS.PublishInvWithToken_Dll(richTextBox1.Text, Program._pattern, Program._seri, Program._SERIAL_CERT, out v6Return);
                }

                lblResult.Text = result;
                if (result != null)
                {
                    BaseMessage.Show(result, 500, this);
                }
                else
                {
                    BaseMessage.Show("Response is null!", 500, this);
                }
            }
            catch (Exception ex)
            {
                BaseMessage.Show("Error: " + ex.Message + "\n" + result, 0, this);
            }
            
        }

        private void btnSendS_Click(object sender, EventArgs e)
        {
            string result = Program.adjustInv(richTextBox1.Text,"old_fkey");
            lblResult.Text = result;
            if (result != null)
            {
                BaseMessage.Show(result, 500, this);
            }
            else
            {
                MessageBox.Show("Response is null!");
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string result = Program.UploadInvAttachmentFkey(Program.fkeyA, Program.exportName + ".xls");
            BaseMessage.Show(result, 0, this);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string result = "";
            string exportFile;
            if (Program.ExportExcel(txtDbfExcel.Text, out exportFile, ref result)) btnUpload.Enabled = true;
            BaseMessage.Show(result + "\r\n" + exportFile, 0, this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program._TEST_)
            {
                richTextBox1.ReadOnly = false;
            }
        }
    }
}
