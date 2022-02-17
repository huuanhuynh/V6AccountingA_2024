using System;
using System.Drawing;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program._TEST_)
            {
                richTextBox1.ReadOnly = false;
                richTextBox1.BackColor = Color.White;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string xmlData = Program.ReadDataXml(txtDbfFile.Text);
            txtUsername.Text = Program._username;
            txtPassword.Text = Program._password;
            //txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnImportCertWithToken.Enabled = true;
            btnSend.Enabled = true;
            btnSend0.Enabled = true;
            btnSendS.Enabled = false;
            btnSendNoSign.Enabled = true;
            btnTest.Enabled = true;


            txtPattern.Text = Program.pattern;
            txtSeri.Text = Program.seri;
            btnUpdatePatternSeri.Enabled = true;
            Program._vnptWS = new Vnpt78WS(Program._baseUrl, Program._account, Program._accountpassword, Program._username, Program._password);
        }
        
        private void btnReadS_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string xmlData = Program.ReadDataXmlS(txtDbfFile.Text);
            txtUsername.Text = Program._username;
            txtPassword.Text = Program._password;
            //txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            txtPattern.Text = Program.pattern;
            txtSeri.Text = Program.seri;

            btnSend.Enabled = false;
            btnSendS.Enabled = true;
            btnUpdatePatternSeri.Enabled = true;
            Program._vnptWS = new Vnpt78WS(Program._baseUrl, Program._account, Program._accountpassword, Program._username, Program._password);
        }
        
        private void btnReadT_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string xmlData = Program.ReadDataXmlT(txtDbfFile.Text);
            txtUsername.Text = Program._username;
            txtPassword.Text = Program._password;
            //txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnSend.Enabled = false;
            btnSendS.Enabled = true;
            Program._vnptWS = new Vnpt78WS(Program._baseUrl, Program._account, Program._accountpassword, Program._username, Program._password);
        }

        private void btnSend0_Click(object sender, EventArgs e)
        {
            string result = null;
            try
            {
                V6Return v6Return;
                result = Program._vnptWS.ImportAndPublishInv(richTextBox1.Text, Program.pattern, Program.seri, out v6Return);
                
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
                BaseMessage.Show("CERT:" + Program.SERIAL_CERT, 0, this);
                V6Return v6Return;
                if (string.IsNullOrEmpty(Program.SERIAL_CERT))
                {
                    //result = Program.ImportAndPublishInv(richTextBox1.Text);
                    result = Program._vnptWS.ImportAndPublishInv(richTextBox1.Text, Program.pattern, Program.seri, out v6Return);
                }
                else
                {
                    Program.StartAutoInputTokenPassword();
                    result = Program._vnptWS.PublishInvWithToken32_Dll(richTextBox1.Text, Program.pattern, Program.seri, Program.SERIAL_CERT, out v6Return);
                }

                lblResult.Text = result;
                if (result != null)
                {
                    if (string.IsNullOrEmpty(v6Return.RESULT_ERROR_MESSAGE))
                    {
                        BaseMessage.Show(v6Return.RESULT_STRING, 500, this);
                    }
                    else
                    {
                        BaseMessage.Show(v6Return.RESULT_ERROR_MESSAGE, 500, this);
                    }
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
            string result = Program._vnptWS.UploadInvAttachmentFkey(Program.fkeyA, Program.exportName + ".xls");
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
            string result = Program._vnptWS.ImportCertWithToken_Dll(Program.SERIAL_CERT);
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
            V6Return v6Return;
            string result = Program._vnptWS.ImportAndPublishInv("<V6test>Test</V6test>", Program.pattern, Program.seri, out v6Return);
            lblResult.Text = result;
            if (v6Return.RESULT_ERROR_MESSAGE != null && v6Return.RESULT_ERROR_MESSAGE.Contains("Dữ liệu xml đầu vào không đúng quy định"))
            {
                BaseMessage.Show("Kết nối ổn!", 500, this);
            }
            else
            {
                BaseMessage.Show(v6Return.RESULT_ERROR_MESSAGE, 0, this);
            }
        }

        private void btnUpdatePatternSeri_Click(object sender, EventArgs e)
        {
            Program.pattern = txtPattern.Text;
            txtPattern.BackColor = Color.White;
            Program.seri = txtSeri.Text;
            txtSeri.BackColor = Color.White;
        }

        private void txtPattern_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtPattern.BackColor = Color.Red;
        }

        private void txtSeri_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSeri.BackColor = Color.Red;
        }
        
    }
}
