﻿using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using V6ThuePost.MInvoiceObject.Request;
using V6ThuePost.MInvoiceObject.Response;
using V6ThuePost.ResponseObjects;
using V6ThuePostMInvoiceApi;
using V6Tools;

namespace V6ThuePost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            if (Program._TEST_)
            {
                toolTip1.SetToolTip(btnSend, "Bấm giữ Control để gửi object gốc.");
            }
        }

        private MInvoicePostObject _jsonBodyObject = null;
        private MInvoicePostObject_View _jsonBodyObject_View = null;
        private void btnRead_Click(object sender, EventArgs e)
        {
            Program.ReadXmlInfo(txtXmlFile.Text);
            if (string.IsNullOrEmpty(Program._version))
            {
                Program.generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                {
                    Field = "adjustmentType",
                    Value = "1",
                };
            }            

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

            _jsonBodyObject = Program.ReadData_Minvoice(txtDbfFile.Text, "M");
            _jsonBodyObject_View = new MInvoicePostObject_View(_jsonBodyObject);
            propertyGrid1.SelectedObject = _jsonBodyObject_View;
            txtUsername.Text = Program.username;
            txtPassword.Text = Program.password;
            txtMaDVCS.Text = Program._ma_dvcs;
            txtURL.Text = Program.baseUrl;
            richTextBox1.Text = _jsonBodyObject.ToJson();
            btnTest.Enabled = true;
            btnSend.Enabled = true;

            Program._WS = new MInvoiceWS(txtURL.Text, txtUsername.Text, txtPassword.Text, txtMaDVCS.Text, Program._codetax, Program._version);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Program._WS != null && Program._WS.IsLoggedIn)
            {
                V6Return v6Return;
                string result = Program._WS.CheckConnection(out v6Return);
                //Phân tích result
                string message = "";
                if (string.IsNullOrEmpty(result))
                {
                    message += "Kết nối ổn. " + v6Return.RESULT_STRING;
                }
                else
                {
                    message = result;
                }

                lblResult.Text = message;
                BaseMessage.Show(message, this);
            }
            else
            {
                lblResult.Text = "Chưa khởi tạo hoặc đăng nhập sai.";
                BaseMessage.Show(lblResult.Text, this);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            bool control_is_down = (ModifierKeys & Keys.Control) == Keys.Control;
            string result = null;
            MInvoiceResponse responseObject = null;
            V6Return v6Return;
            if (string.IsNullOrEmpty(Program._SERIAL_CERT))
            {
                if (control_is_down || !Program._TEST_)
                {
                    responseObject = Program._WS.POST_NEW(_jsonBodyObject, out v6Return);
                }
                else
                {
                    responseObject = Program._WS.POST_NEW(_jsonBodyObject, out v6Return);
                }
                
                lblResult.Text = v6Return.RESULT_STRING;
            }
            else
            {
                responseObject = Program._WS.POST_NEW_TOKEN(_jsonBodyObject, out v6Return);
                lblResult.Text = v6Return.RESULT_STRING;
            }

            if (!string.IsNullOrEmpty(v6Return.ID))
            {
                btnPDF.AccessibleName = v6Return.ID;
                btnPDF.Enabled = true;
            }

            string message = "";
            try
            {
                //responseObject = JsonConvert.DeserializeObject<MInvoiceResponse>(v6Return.RESULT_STRING);
                if (!string.IsNullOrEmpty(v6Return.RESULT_ERROR_MESSAGE))
                {
                    message += " " + v6Return.RESULT_ERROR_MESSAGE;
                }
                else if (Program._version == "78")
                {
                    message += "Số HD:" + v6Return.SO_HD + ", ID:" + v6Return.ID + ", SECRET_CODE:" + v6Return.SECRET_CODE;
                    Program.WriteFlag(Program.flagFileName4, "" + v6Return.SO_HD + ":" + v6Return.ID + ":" + v6Return.SECRET_CODE);
                    File.Create(Program.flagFileName2).Close();
                }
                else if (responseObject.ok == "true" && responseObject.data != null && responseObject.data.ContainsKey("inv_invoiceNumber")
                            && !string.IsNullOrEmpty((string)responseObject.data["inv_invoiceNumber"]))
                {
                    message += " " + responseObject.data["inv_invoiceNumber"];
                    Program.WriteFlag(Program.flagFileName4, "" + v6Return.SO_HD + ":" + v6Return.ID + ":" + v6Return.SECRET_CODE);
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

        private void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                V6Return v6Return;
                string result = Program._WS.DownloadInvoicePDF(btnPDF.AccessibleName, "F:\\Test", out v6Return);
                BaseMessage.Show(result, 0, this);
            }
            catch (Exception ex)
            {
                BaseMessage.Show(ex.Message, this);
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            richTextBox1.Text = _jsonBodyObject.ToJson();
        }
    }//End class
}//End namespace
