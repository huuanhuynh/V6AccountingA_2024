using System;
using System.Windows.Forms;
using V6ThuePost.ResponseObjects;
using V6ThuePost.VnptObjects;
using V6ThuePostSoftDreamsApi;
using V6Tools.V6Convert;

namespace V6ThuePost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Invoices invoices;
        private AdjustInv adjustInv;
        private ReplaceInv replaceInv;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program._TEST_) richTextBox1.ReadOnly = false;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            Program._softDreams_ws = new SoftDreamsWS(Program.baseUrl, Program.username, Program.password, Program.token_serial);
            invoices = Program.ReadDataXml(txtDbfFile.Text);
            string xmlData = V6XmlConverter.ClassToXml(invoices);
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnTest.Enabled = true;
            btnSend0.Enabled = true;
            btnSend.Enabled = true;
            btnSendS.Enabled = false;
        }
        
        private void btnReadS_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            Program._softDreams_ws = new SoftDreamsWS(Program.baseUrl, Program.username, Program.password, Program.token_serial);
            adjustInv = Program.ReadDataXmlS(txtDbfFile.Text);
            string xmlData = adjustInv.ToXml();
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnSend0.Enabled = true;
            btnSend.Enabled = false;
            btnSendS.Enabled = true;
        }
        
        private void btnReadT_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            Program._softDreams_ws = new SoftDreamsWS(Program.baseUrl, Program.username, Program.password, Program.token_serial);
            replaceInv = Program.ReadDataXmlT(txtDbfFile.Text);
            string xmlData = replaceInv.ToXml();
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnSend0.Enabled = true;
            btnSend.Enabled = false;
            btnSendS.Enabled = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            V6Return v6return;
            string result = Program._softDreams_ws.ImportInvoices(new Invoices(), Program.pattern, Program.seri, false, Program._signmode, out v6return);
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

        private void btnSend0_Click(object sender, EventArgs e)
        {
            V6Return v6return;
            string result = Program._softDreams_ws.ImportInvoices(invoices, Program.pattern, Program.seri, false, Program._signmode, out v6return);
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                V6Return v6return;
                Program.StartAutoInputTokenPassword();
                string result = Program._softDreams_ws.ImportInvoices(invoices, Program.pattern, Program.seri, true, Program._signmode, out v6return);
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
                Program.StopAutoInputTokenPassword();
            }
        }

        private void btnSendS_Click(object sender, EventArgs e)
        {
            V6Return v6return;
            string result = Program._softDreams_ws.AdjustInvoice(adjustInv, adjustInv.Invoice["Ikey"].ToString(), Program.pattern, Program.seri, false, Program._signmode, out v6return);
            lblResult.Text = result;
            if (v6return.RESULT_OBJECT != null)
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
            string result = Program._softDreams_ws.UploadInvAttachmentFkey(Program.fkeyA, Program.exportName + ".xls");
            BaseMessage.Show(result, 0, this);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string result = "";
            string exportFile;
            if (Program.ExportExcel(txtDbfExcel.Text, out exportFile, ref result)) btnUpload.Enabled = true;
            BaseMessage.Show(result + "\r\n" + exportFile, 0, this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.StopAutoInputTokenPassword();
        }
        
    }
}
