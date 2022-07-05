using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using V6ThuePost.ResponseObjects;
using V6ThuePost.ViettelObjects;
using V6ThuePostViettelV2Api;
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
            try
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

                string dbfFile = txtDbfFile.Text;
                string dbfFile3 = "";
                if (Program._useTaxBreakdowns)
                {
                    dbfFile3 = dbfFile.ToLower().Replace(".dbf", "3.dbf");
                }

                string jsonBody = Program.ReadData(dbfFile, dbfFile3, "M");
                txtUsername.Text = Program.username;
                txtPassword.Text = Program.password;
                txtURL.Text = Program.baseUrl;
                richTextBox1.Text = jsonBody;
                btnTest.Enabled = true;
                btnSend.Enabled = true;

                Program._viettelV2_ws = new ViettelV2WS(txtURL.Text, txtUsername.Text, txtPassword.Text, Program._codetax);
            }
            catch (Exception ex)
            {
                BaseMessage.Show(ex.Message, 0, this);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Program._viettelV2_ws != null)
            {
                V6Return v6return = null;
                string result = Program._viettelV2_ws.POST_CREATE_INVOICE("", Program._version == "V45I", out v6return);
                
                string message = "";
                if (v6return.RESULT_ERROR_MESSAGE != null && v6return.RESULT_ERROR_MESSAGE.Contains("JSON_PARSE_ERROR"))
                {
                    message += "Kết nối ổn. " + v6return.RESULT_ERROR_MESSAGE;
                    BaseMessage.Show(message, 0, this);
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
            V6Return v6Return;
            if (string.IsNullOrEmpty(Program._SERIAL_CERT))
            {

                result = Program._viettelV2_ws.POST_CREATE_INVOICE(richTextBox1.Text, Program._version == "V45I", out v6Return);
                lblResult.Text = result;
            }
            else
            {
                string templateCode = Program.generalInvoiceInfoConfig["templateCode"].Value;
                result = Program._viettelV2_ws.CreateInvoiceUsbTokenGetHash_Sign(richTextBox1.Text, templateCode, Program._SERIAL_CERT, out v6Return);
                lblResult.Text = result;
            }
            
            string message = "";
            try
            {
                VIETTEL_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIETTEL_CreateInvoiceResponse>(result);
                if (!string.IsNullOrEmpty(responseObject.description))
                {
                    message += " " + responseObject.description;
                }
                
                if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.invoiceNo))
                {
                    message += " " + responseObject.result.invoiceNo;
                    Program.WriteFlag(Program.flagFileName4, responseObject.result.invoiceNo);
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

        private void btnGetMeta_Click(object sender, EventArgs e)
        {
            try
            {
                var v = new V6Return();
                string templateCode = Program.row0["MA_MAUHD"].ToString();
                var result = Program._viettelV2_ws.GetMetaDataDefine(templateCode, out v);
                lblResult.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }//End class
}//End namespace
