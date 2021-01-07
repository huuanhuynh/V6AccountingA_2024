using System;
using System.Windows.Forms;

namespace V6ThuePost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program._TEST_) richTextBox1.ReadOnly = false;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string xmlData = Program.ReadDataXml(txtDbfFile.Text);
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnImportCertWithToken.Enabled = true;
            btnSend.Enabled = true;
            btnSend0.Enabled = true;
            btnSendS.Enabled = false;
            btnSendNoSign.Enabled = true;
            btnTest.Enabled = true;
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

        private void btnSend0_Click(object sender, EventArgs e)
        {
            string result = null;
            try
            {
                result = Program.ImportInv(richTextBox1.Text);
                
                lblResult.Text = result;
                if (result != null)
                {
                    BaseMessage.Show(result, 500, this);
                }
                else
                {
                    BaseMessage.Show("Response is null!", 0, this);
                }
            }
            catch (Exception ex)
            {
                BaseMessage.Show(ex.Message, 0, this);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string result = null;
            try
            {
                //result = Program.ImportAndPublishInv(richTextBox1.Text);
                //result = Program.PhatHanhHoaDon_Token4buoc(richTextBox1.Text);

                if (string.IsNullOrEmpty(Program.SERIAL_CERT))
                {
                    result = Program.ImportAndPublishInv(richTextBox1.Text);
                    //result = Program.ImportInv(richTextBox1.Text);
                }
                else
                {
                    Program.StartAutoInputTokenPassword();
                    result = Program.PublishInvWithToken_Dll(richTextBox1.Text);
                }
                

                lblResult.Text = result;
                if (result != null)
                {
                    BaseMessage.Show(result, 500, this);
                }
                else
                {
                    BaseMessage.Show("Response is null!", 0, this);
                }
            }
            catch (Exception ex)
            {
                Program.StopAutoInputTokenPassword();
                BaseMessage.Show(ex.Message, 0, this);
            }
        }
        
        private void btnSendNoSign_Click(object sender, EventArgs e)
        {
            string result = null;
            
            result = Program.getHashInvWithToken(richTextBox1.Text);
            
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

        private void btnImportCertWithToken_Click(object sender, EventArgs e)
        {
            string result = Program.ImportCertWithToken_Dll();
            lblResult.Text = result;
            BaseMessage.Show(result, 0, this);
        }

        private void btnAutoInput_Click(object sender, EventArgs e)
        {
            Program.StartAutoInputTokenPassword();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.StopAutoInputTokenPassword();
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
                BaseMessage.Show(result, 0, this);
            }
        }
        
    }
}
