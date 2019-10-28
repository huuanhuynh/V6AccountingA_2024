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

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            string xmlData = Program.ReadDataXml(txtDbfFile.Text);
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            //txtURL.Text = Program.link_Publish;
            richTextBox1.Text = xmlData;

            btnTest.Enabled = true;
            btnSend.Enabled = true;
            btnSendS.Enabled = true;
        }
        
        private void btnReadS_Click(object sender, EventArgs e)
        {
            
        }
        
        private void btnReadT_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var result = Program.InsertInvoice(Program._amData, Program._adList);
            lblResult.Text = result.ToString();
            if (result)
            {
                BaseMessage.Show(true.ToString(), 500, this);
            }
            else
            {
                BaseMessage.Show("false", 500, this);
            }
        }

        private void btnSendS_Click(object sender, EventArgs e)
        {
            var result = Program.UpdateInvoice(Program._amData, Program._adList);
            lblResult.Text = result.ToString();
            if (result)
            {
                BaseMessage.Show(true.ToString(), 500, this);
            }
            else
            {
                BaseMessage.Show("false", 500, this);
            }
        }
        
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string result = "";
            string exportFile;
            Program.ExportExcel(txtDbfExcel.Text, out exportFile, ref result);
            BaseMessage.Show(result + "\r\n" + exportFile, 0, this);
        }
    }
}
