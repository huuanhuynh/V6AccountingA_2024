using System;
using System.IO;
using System.Windows.Forms;
using V6ThuePostViettelApi;
using V6ThuePostViettelApi.ResponseObjects;
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            string result = Program.POST_NEW(richTextBox1.Text);
            lblResult.Text = result;
            
            string message = "";
            try
            {
                CreateInvoiceResponse responseObject = MyJson.ConvertJson<CreateInvoiceResponse>(result);
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

        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            Program.generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
            {
                Field = "adjustmentType",
                Value = "1",
            };
            //Guid new_uid = Guid.NewGuid();
            //Program.generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
            //{
            //    Field = "transactionUuid",
            //    Value = "" + new_uid,
            //};

            string jsonBody = Program.ReadData(txtDbfFile.Text);
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtURL.Text = Program.baseUrl;
            richTextBox1.Text = jsonBody;

            Program._V6Http = new V6Http(txtURL.Text, txtUsername.Text, txtPassword.Text);
        }

        private void btnSendTest_Click(object sender, EventArgs e)
        {
            if (Program._V6Http != null)
            {
                string result = Program._V6Http.POST("InvoiceAPI/InvoiceWS/createInvoice/0100109106-990", richTextBox1.Text);
                //Phân tích result
                string message = "";
                try
                {
                    CreateInvoiceResponse responseObject = MyJson.ConvertJson<CreateInvoiceResponse>(result);
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
                result = message + "\n" + result;
                lblResult.Text = result;
            }
            else
            {
                lblResult.Text = "Chưa khởi tạo";
            }
        }

    }//End class
}//End namespace
