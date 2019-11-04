using System;
using System.Windows.Forms;
using V6ThuePost.ResponseObjects;
using V6ThuePostThaiSonApi;
using V6ThuePostThaiSonApi.EinvoiceService;
using V6Tools.V6Convert;

namespace V6ThuePost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private HoaDonEntity _invoice;

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            _invoice = Program.ReadDataXml(txtDbfFile.Text);
            Program._ThaiSon_ws = new ThaiSonWS(Program._baseUrl, Program._username, Program._password, Program._token_serial);
            txtUsername.Text = Program._username;
            txtPassword.Text = Program._password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = V6XmlConverter.ClassToXml(_invoice);

            btnSend.Enabled = true;
            btnSendS.Enabled = false;
        }
        
        private void btnReadS_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            _invoice = Program.ReadDataXml(txtDbfFile.Text);
            txtUsername.Text = Program._username;
            txtPassword.Text = Program._password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = V6XmlConverter.ClassToXml(_invoice); ;

            btnSend.Enabled = false;
            btnSendS.Enabled = true;
        }
        
        private void btnReadT_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            _invoice = Program.ReadDataXml(txtDbfFile.Text);
            txtUsername.Text = Program._username;
            txtPassword.Text = Program._password;
            txtURL.Text = Program.link_Publish;
            richTextBox1.Text = V6XmlConverter.ClassToXml(_invoice); ;

            btnSend.Enabled = false;
            btnSendS.Enabled = true;
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            V6Return v6return;
            string result = Program._ThaiSon_ws.XuatHoaDonDienTu(_invoice, out v6return);
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
            //string result = Program.adjustInv(richTextBox1.Text,"old_fkey");
            //lblResult.Text = result;
            //if (result != null)
            //{
            //    BaseMessage.Show(result, 500, this);
            //}
            //else
            //{
            //    MessageBox.Show("Response is null!");
            //}
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
    }
}
