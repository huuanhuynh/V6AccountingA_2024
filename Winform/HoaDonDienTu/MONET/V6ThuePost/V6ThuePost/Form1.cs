using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using V6ThuePost.MONET_Objects.Response;
using V6ThuePost.ResponseObjects;
using V6ThuePostMonetApi;

using V6Tools;

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
            //Program.generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
            //{
            //    Field = "adjustmentType",
            //    Value = "1",
            //};

            if (!string.IsNullOrEmpty(Program._SERIAL_CERT))
            {
                Program.generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                {
                    Field = "certificateSerial",
                    Value = Program._SERIAL_CERT,
                };
            }
            //Guid new_uid = Guid.NewGuid();
            //Program.generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
            //{
            //    Field = "transactionUuid",
            //    Value = "" + new_uid,
            //};

            string jsonBody = Program.ReadData(txtDbfFile.Text, "M");
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.baseUrl;
            richTextBox1.Text = jsonBody;
            btnTest.Enabled = true;
            btnSend.Enabled = true;

            Program._ws = new MONET_WS(txtURL.Text, txtUsername.Text, txtPassword.Text, Program._codetax);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Program._ws != null)
            {
                V6Return v6return;
                MONET_ADD_Response result = Program._ws.POST_NEW(Program.createInvoiceUri, "", out v6return);
                //Phân tích result
                string message = "";
                
                if (result.errorMessage.Contains("POST DATA EMPTY"))
                {
                    message += "Kết nối ổn. ";
                }

                lblResult.Text = message + "\n" + v6return.RESULT_STRING;
            }
            else
            {
                lblResult.Text = "Chưa khởi tạo";
            }
        }

        private MONET_ADD_Response send_result = null;
        private void btnSend_Click(object sender, EventArgs e)
        {
            send_result = new MONET_ADD_Response();
            V6Return v6return = new V6Return();

            if (string.IsNullOrEmpty(Program._SERIAL_CERT))
            {
                send_result = Program._ws.POST_NEW(Program.createInvoiceUri, richTextBox1.Text, out v6return);
                lblResult.Text = v6return.RESULT_STRING;
            }
            else
            {
                //string templateCode = Program.generalInvoiceInfoConfig["templateCode"].Value;
                //result = Program._ws.CreateInvoiceUsbTokenGetHash_Sign(richTextBox1.Text, templateCode, Program._SERIAL_CERT);
                //lblResult.Text = result;
            }
            
            string message = "";

            if (send_result.isSuccess && !string.IsNullOrEmpty(send_result.invoiceNo))
            {
                message += " " + send_result.invoiceNo;
                Program.WriteFlag(Program.flagFileName4, send_result.invoiceNo);
                File.Create(Program.flagFileName2).Close();
                btnCheckSigned.Enabled = true;
            }
            else
            {
                message += " " + send_result.invoiceNo + "" + send_result.errorMessage;
                File.Create(Program.flagFileName3).Close();
            }

            MessageBox.Show(message + "\n" + v6return.RESULT_STRING);
        }

        private void btnCheckSigned_Click(object sender, EventArgs e)
        {
            try
            {
                var check = Program._ws.check_signed(Program._SERIAL_CERT, send_result.oid);
                if (check.signed)
                {
                    BaseMessage.Show("signed");
                }
                else
                {
                    BaseMessage.Show("no sign " + check.errorMessage);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }//End class
}//End namespace
