﻿using System;
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
            btnSendS.Enabled = false;
            btnSendNoSign.Enabled = true;
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


        private void btnSend_Click(object sender, EventArgs e)
        {
            string result = null;
            try
            {
                //result = Program.ImportAndPublishInv(richTextBox1.Text);
                //result = Program.PhatHanhHoaDon_Token4buoc(richTextBox1.Text);
                Program.StartAutoInputTokenPassword();
                result = Program.PublishInvWithToken_Dll(richTextBox1.Text);

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
            catch (Exception ex)
            {
                Program.StopAutoInputTokenPassword();
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
        
    }
}
