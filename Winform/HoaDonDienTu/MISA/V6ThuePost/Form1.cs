using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using V6ThuePost_MISA_Api;
using V6Tools;
using V6ThuePost.ResponseObjects;
using V6ThuePost.MISA_Objects;

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
                //Program.generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                //{
                //    Field = "adjustmentType",
                //    Value = "1",
                //};

                //if (!string.IsNullOrEmpty(Program._SERIAL_CERT))
                //{
                //    Program.generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                //    {
                //        Field = "certificateSerial",
                //        Value = Program._SERIAL_CERT,
                //    };
                //}
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
                string jsonBody = "";
                if (string.IsNullOrEmpty(Program._SERIAL_CERT))
                {
                    jsonBody = Program.ReadData_MISA(dbfFile, dbfFile3, "M", "HSM");
                }
                else
                {
                    jsonBody = Program.ReadData_MISA(dbfFile, dbfFile3, "M", "");
                }
                txtUsername.Text = Program.username;
                txtPassword.Text = Program.password;
                txtURL.Text = Program.baseUrl;
                richTextBox1.Text = jsonBody;
                btnTest.Enabled = true;
                btnSend.Enabled = true;

                Program._MISA_WS = new MISA_WS(txtURL.Text, txtUsername.Text, txtPassword.Text, Program._codetax, Program._appID);
            }
            catch (Exception ex)
            {
                BaseMessage.Show(ex.Message, 0, this);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Program._MISA_WS != null)
            {
                V6Return v6Return = null;
                string result = Program._MISA_WS.CheckConnection(out v6Return);
                

                string message = "";
                if (string.IsNullOrEmpty(result))
                {
                    message += "Kết nối ổn.";
                    BaseMessage.Show(message, 0, this);
                }
                else
                {
                    BaseMessage.Show(result, 0, this);
                }
            
                result = message + "\n" + v6Return.RESULT_STRING;
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
                result = Program._MISA_WS.POST_CREATE_INVOICE_HSM(richTextBox1.Text, out v6Return);
                lblResult.Text = result;
            }
            else
            {
                string templateCode = Program.generalInvoiceInfoConfig["templateCode"].Value;
                result = Program._MISA_WS.CreateInvoice_GetXml_Sign(richTextBox1.Text, templateCode, Program._SERIAL_CERT, out v6Return);
                lblResult.Text = result;
            }
            
            string message = "";
            try
            {
                
                

                if (!string.IsNullOrEmpty(v6Return.RESULT_ERROR_MESSAGE))
                {
                    message += "Không thành công: " + v6Return.RESULT_ERROR_MESSAGE;
                }
                else if (!string.IsNullOrEmpty(v6Return.SO_HD))
                {
                    _magiaodich = v6Return.SECRET_CODE;
                    _so_hoadon = v6Return.SO_HD;
                    btnSignHSM.Enabled = true;
                    btnDownloadPDF.Enabled = true;

                    message += "Thành công. Số hóa đơn: " + v6Return.SO_HD;
                    Program.WriteFlag(Program.flagFileName4, v6Return.SO_HD);
                    File.Create(Program.flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Result:" + result);
                message = "Execption:" + ex.Message;
            }

            MessageBox.Show(message);
        }


        private string _magiaodich, _RefID, _so_hoadon;

        private void btnSignHSM_Click(object sender, EventArgs e)
        {
            string result = null;
            V6Return v6Return;
            string message = "";

            try
            {
                Program._MISA_WS.SIGN_HSM(Program._codetax, _magiaodich, Program._RefID_or_fkey, out v6Return);

                MISA_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                if (!string.IsNullOrEmpty(v6Return.RESULT_ERROR_MESSAGE))
                {
                    message += " " + v6Return.RESULT_ERROR_MESSAGE;
                }

                if (!string.IsNullOrEmpty(v6Return.SO_HD))
                {
                    message += " " + v6Return.SO_HD;
                    Program.WriteFlag(Program.flagFileName4, v6Return.SO_HD);
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

        private void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            try
            {
                V6Return v6Return;
                Program._MISA_WS.TAI_HOA_DON_PDF(Program._RefID_or_fkey, _magiaodich, Path.GetDirectoryName(Application.StartupPath), out v6Return);
                if (!string.IsNullOrEmpty(v6Return.RESULT_ERROR_MESSAGE))
                {
                    BaseMessage.Show(v6Return.RESULT_ERROR_MESSAGE, 0, this);
                }
                else
                {
                    BaseMessage.Show(v6Return.PATH, 0, this);
                }
            }
            catch (Exception ex)
            {
                BaseMessage.Show(ex.Message, 0, this);
            }
        }

        private void btnGetMeta_Click(object sender, EventArgs e)
        {
            try
            {
                var v = new V6Return();
                string templateCode = Program.row0["MA_MAUHD"].ToString();
                var result = Program._MISA_WS.GetSerialList(templateCode, out v);
                lblResult.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        
    }//End class
}//End namespace
