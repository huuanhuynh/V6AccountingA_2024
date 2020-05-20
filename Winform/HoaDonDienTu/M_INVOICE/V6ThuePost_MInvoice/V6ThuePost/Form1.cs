using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using V6ThuePost.MInvoiceObject.Response;
using V6ThuePostMInvoiceApi;
using V6Tools;

namespace V6ThuePost
{
    public partial class Form1 : Form
    {
        //private string userName = "0100109106-997_5";
        //private string password = "111111a@A";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            Program.generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
            {
                Field = "adjustmentType",
                Value = "1",
            };

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
            txtMaDVCS.Text = Program.ma_dvcs;
            txtURL.Text = Program.baseUrl;
            richTextBox1.Text = jsonBody;
            btnTest.Enabled = true;
            btnSend.Enabled = true;

            Program._WS = new MInvoiceWS(txtURL.Text, txtUsername.Text, txtPassword.Text, txtMaDVCS.Text, Program._codetax);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Program._WS != null)
            {
                string result = Program._WS.POST_NEW("{}");
                //Phân tích result
                string message = "";
                try
                {
                    MInvoiceResponse responseObject = JsonConvert.DeserializeObject<MInvoiceResponse>(result);
                    if (!string.IsNullOrEmpty(responseObject.Message) || !string.IsNullOrEmpty(responseObject.error))
                    {
                        message += "Kết nối ổn. " + responseObject.Message + responseObject.error;
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteToLog("Program.Main ConverResultObjectException: " + ex.Message);
                    message = "Kết quả:";
                }
                result = message + "\n" + result;
                lblResult.Text = result;
            }
            else
            {
                lblResult.Text = "Chưa khởi tạo";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string result = null;

            if (string.IsNullOrEmpty(Program._SERIAL_CERT))
            {
                result = Program._WS.POST_NEW(richTextBox1.Text);
                lblResult.Text = result;
            }
            else
            {
                string templateCode = Program.generalInvoiceInfoConfig["templateCode"].Value;
                result = Program._WS.CreateInvoiceUsbTokenGetHash_Sign(richTextBox1.Text, templateCode, Program._SERIAL_CERT);
                lblResult.Text = result;
            }
            
            string message = "";
            try
            {
                MInvoiceResponse responseObject = JsonConvert.DeserializeObject<MInvoiceResponse>(result);// MyJson.ConvertJson<CreateInvoiceResponse>(result);
                if (!string.IsNullOrEmpty(responseObject.Message))
                {
                    message += " " + responseObject.Message;
                }
                if (!string.IsNullOrEmpty(responseObject.error))
                {
                    message += " " + responseObject.error;
                }

                if (responseObject.ok == "true" && responseObject.data != null && responseObject.data.ContainsKey("inv_invoiceNumber")
                            && !string.IsNullOrEmpty((string)responseObject.data["inv_invoiceNumber"]))
                {
                    message += " " + responseObject.data["inv_invoiceNumber"];
                    Program.WriteFlag(Program.flagFileName4, "" + responseObject.data["inv_invoiceNumber"]);
                    File.Create(Program.flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Program.Main ConverResultObjectException: " + ex.Message);
                message = "Kết quả:";
            }

            MessageBox.Show(message + "\n" + result);
        }
    }//End class
}//End namespace
