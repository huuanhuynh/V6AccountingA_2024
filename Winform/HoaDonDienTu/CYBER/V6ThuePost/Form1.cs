﻿using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using V6ThuePost_CYBER_Api;
using V6Tools;
using V6ThuePost.ResponseObjects;
using V6ThuePost.CYBER_Objects;

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

                if (!string.IsNullOrEmpty(Program._SERIAL_CERT))
                {
                    Program.generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                    {
                        Field = "certificateSerial",
                        Value = Program._SERIAL_CERT,
                    };
                }
                string MODE = "M";
                if (radS.Checked) MODE = "S"; if (radT.Checked) MODE = "T";
                string jsonBody = Program.ReadData_CYBER(txtDbfFile.Text, MODE);
                
                txtUsername.Text = Program.username;
                txtPassword.Text = Program.password;
                txtURL.Text = Program.baseUrl;
                richTextBox1.Text = jsonBody;
                btnTest.Enabled = true;
                btnSend.Text = string.Format("Send({0})", MODE);
                btnSend.Enabled = true;

                Program._CYBER_WS = new CYBER_WS(txtURL.Text, txtUsername.Text, txtPassword.Text, Program._codetax);
            }
            catch (Exception ex)
            {
                BaseMessage.Show(ex.Message, 0, this);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string result = null;
            V6Return v6Return = new V6Return();
            if (btnSend.Text.EndsWith("(M)"))
            {
                if (string.IsNullOrEmpty(Program._SERIAL_CERT))
                {
                    result = Program._CYBER_WS.POST_CREATE_INVOICE(richTextBox1.Text, Program._SIGNMODE == "HSM", out v6Return);
                    lblResult.Text = result;
                }
                else
                {
                    string templateCode = Program.generalInvoiceInfoConfig["templateCode"].Value;
                    result = Program._CYBER_WS.CreateInvoiceUsbTokenGetHash_Sign(richTextBox1.Text, templateCode, Program._SERIAL_CERT, out v6Return);
                    lblResult.Text = result;
                }
            }
            else if (btnSend.Text.EndsWith("(S)"))
            {
                if (string.IsNullOrEmpty(Program._SERIAL_CERT))
                {
                    result = Program._CYBER_WS.POST_EDIT(richTextBox1.Text, Program._SIGNMODE == "HSM", out v6Return);
                    lblResult.Text = result;
                }
                else
                {
                    string templateCode = Program.generalInvoiceInfoConfig["templateCode"].Value;
                    result = Program._CYBER_WS.POST_EDIT(richTextBox1.Text, Program._SIGNMODE == "HSM", out v6Return);
                    lblResult.Text = result;
                }
            }
            else if (btnSend.Text.EndsWith("(T)"))
            {
                if (string.IsNullOrEmpty(Program._SERIAL_CERT))
                {
                    result = Program._CYBER_WS.POST_REPLACE(richTextBox1.Text, Program._SIGNMODE == "HSM", out v6Return);
                    lblResult.Text = result;
                }
                else
                {
                    string templateCode = Program.generalInvoiceInfoConfig["templateCode"].Value;
                    result = Program._CYBER_WS.POST_REPLACE(richTextBox1.Text, Program._SIGNMODE == "HSM", out v6Return);
                    lblResult.Text = result;
                }
            }
            

            string message = "";
            try
            {
                CYBER_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CYBER_CreateInvoiceResponse>(result);
                _magiaodich = responseObject.result.magiaodich;
                _so_hoadon = responseObject.result.sohoadon;
                btnSignHSM.Enabled = true;
                btnDownloadPDF.Enabled = true;

                if (!responseObject.success)
                {
                    message += "Không thành công: " + responseObject.result.motaketqua;
                }
                else if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.sohoadon))
                {
                    message += "Thành công. Số hóa đơn: " + responseObject.result.sohoadon;
                    Program.WriteFlag(Program.flagFileName4, responseObject.result.sohoadon);
                    File.Create(Program.flagFileName2).Close();
                }
                else
                {
                    message = "Result: " + v6Return.RESULT_STRING;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Result:" + result);
                message = "Execption:" + ex.Message;
            }

            MessageBox.Show(message);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Program._CYBER_WS != null)
            {
                V6Return v6return = null;
                string result = Program._CYBER_WS.CheckConnection();
                

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
            
                result = message + "\n" + result;
                lblResult.Text = result;
            }
            else
            {
                lblResult.Text = "Chưa khởi tạo";
            }
        }

        



        private string _magiaodich, _ma_hoadon, _so_hoadon;

        private void btnSignHSM_Click(object sender, EventArgs e)
        {
            string result = null;
            V6Return v6Return;
            string message = "";

            try
            {
                Program._CYBER_WS.SIGN_HSM(_magiaodich, Program._ma_hoadon_or_fkey, out v6Return);

                CYBER_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CYBER_CreateInvoiceResponse>(result);
                if (!string.IsNullOrEmpty(responseObject.result.motaketqua))
                {
                    message += " " + responseObject.result.motaketqua;
                }

                if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.sohoadon))
                {
                    message += " " + responseObject.result.sohoadon;
                    Program.WriteFlag(Program.flagFileName4, responseObject.result.sohoadon);
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
                Program._CYBER_WS.TAI_HOA_DON_PDF(Program._codetax, _magiaodich, Program._ma_hoadon_or_fkey, Path.GetDirectoryName(Application.StartupPath), out v6Return);
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
                var result = Program._CYBER_WS.GetMetaDataDefine(templateCode, out v);
                lblResult.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        
    }//End class
}//End namespace