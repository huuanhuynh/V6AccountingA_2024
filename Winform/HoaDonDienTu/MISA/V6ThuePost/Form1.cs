using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using V6ThuePost_MISA_Api;
using V6Tools;
using V6ThuePost.ResponseObjects;
using V6ThuePost.MISA_Objects;
using System.Data;
using System.Collections.Generic;
using V6Tools.V6Convert;

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
                btnExportInfo.Enabled = true;
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
                if (Program.USETAXBREAKDOWNS)
                {
                    dbfFile3 = dbfFile.ToLower().Replace(".dbf", "3.dbf");
                }
                string jsonBody = "";
                if (string.IsNullOrEmpty(Program._SERIAL_CERT))
                {
                    jsonBody = Program.ReadData_MISA(dbfFile, dbfFile3, "M", Program.SIGNMODE);
                }
                else
                {
                    jsonBody = Program.ReadData_MISA(dbfFile, dbfFile3, "M", Program.SIGNMODE);
                }
                txtUsername.Text = Program.username;
                txtPassword.Text = Program.password;
                txtURL.Text = Program.baseUrl;
                richTextBox1.Text = jsonBody;
                btnTest.Enabled = true;
                btnSend.Enabled = true;

                Program._MISA_WS = new MISA_WS(txtURL.Text, txtUsername.Text, txtPassword.Text, Program._codetax, Program._appID, Program.COMACQT);
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
            if (Program.SIGNMODE == "HSM")
            {
                result = Program._MISA_WS.POST_CREATE_INVOICE_HSM(richTextBox1.Text, out v6Return);
                lblResult.Text = result;
            }
            else if(Program.SIGNMODE == "TOKEN")
            {
                result = Program._MISA_WS.CreateInvoice_GetXml_Sign(richTextBox1.Text, Program._SERIAL_CERT, out v6Return);
                lblResult.Text = result;
            }
            else
            {
                result = Program._MISA_WS.POST_CREATE_INVOICE(richTextBox1.Text, out v6Return);
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

        private void btnExportInfo_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog o = new SaveFileDialog();
                o.Filter = "Excel|*.xls;*.xlsx";
                if (o.ShowDialog() == DialogResult.OK)
                {
                    var data = new DataTable();
                    data.Columns.Add("GROUPNAME", typeof(string));
                    data.Columns.Add("FIELD", typeof(string));
                    data.Columns.Add("VALUE", typeof(string));
                    data.Columns.Add("FIELDV6", typeof(string));
                    data.Columns.Add("TYPE", typeof(string));
                    data.Columns.Add("DATATYPE", typeof(string));
                    data.Columns.Add("Format", typeof(string));
                    data.Columns.Add("LOAI", typeof(string));
                    data.Columns.Add("STT_LOAI", typeof(int));  //=0
                    data.Columns.Add("STT", typeof(int));  //=0
                    data.Columns.Add("TEN", typeof(string));
                    data.Columns.Add("TEN2", typeof(string));
                    data.Columns.Add("ADV_AL1", typeof(string));
                    data.Columns.Add("ADV_AL2", typeof(string));
                    //data.Columns.Add("DATATYPE", typeof(string));
                    data.Columns.Add("DMETHOD", typeof(string));
                    data.Columns.Add("date0", typeof(string));
                    data.Columns.Add("time0", typeof(string));
                    data.Columns.Add("user_id0", typeof(string));
                    data.Columns.Add("DATE2", typeof(string));
                    data.Columns.Add("TIME2", typeof(string));
                    data.Columns.Add("user_id2", typeof(string));

                    data.Columns.Add("status", typeof(string));

                    data.Columns.Add("MA_TD1", typeof(string));
                    data.Columns.Add("MA_TD2", typeof(string));
                    data.Columns.Add("MA_TD3", typeof(string));

                    data.Columns.Add("ngay_td1", typeof(string));
                    data.Columns.Add("ngay_td2", typeof(string));
                    data.Columns.Add("ngay_td3", typeof(string));

                    data.Columns.Add("sl_td1", typeof(string));
                    data.Columns.Add("sl_td2", typeof(string));
                    data.Columns.Add("sl_td3", typeof(string));

                    data.Columns.Add("gc_td1", typeof(string));
                    data.Columns.Add("gc_td2", typeof(string));
                    data.Columns.Add("gc_td3", typeof(string));

                    data.Columns.Add("NOGEN", typeof(string));

                    int count = 0;
                    DateTime now = DateTime.Now;
                    string today_dd_MM_yyyy = ObjectAndString.ObjectToString(now, "dd/MM/yyyy");

                    foreach (KeyValuePair<string, ConfigLine> item in Program.v6infoInvoiceInfoConfig)
                    {
                        count++;
                        var newrow = data.NewRow();
                        newrow["GROUPNAME"] = "V6Info";
                        AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                        data.Rows.Add(newrow);
                    }

                    foreach (KeyValuePair<string, ConfigLine> item in Program.generalInvoiceInfoConfig)
                    {
                        count++;
                        var newrow = data.NewRow();
                        newrow["GROUPNAME"] = "GeneralInvoiceInfo";
                        AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                        data.Rows.Add(newrow);
                    }

                    if (Program.buyerInfoConfig != null)
                        foreach (KeyValuePair<string, ConfigLine> item in Program.buyerInfoConfig)
                        {
                            count++;
                            var newrow = data.NewRow();
                            newrow["GROUPNAME"] = "BuyerInfo";
                            AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                            data.Rows.Add(newrow);
                        }

                    if (Program.sellerInfoConfig != null)
                        foreach (KeyValuePair<string, ConfigLine> item in Program.sellerInfoConfig)
                        {
                            count++;
                            var newrow = data.NewRow();
                            newrow["GROUPNAME"] = "SellerInfo";
                            AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                            data.Rows.Add(newrow);
                        }
                    if (Program.metadataConfig != null)
                        foreach (KeyValuePair<string, ConfigLine> item in Program.metadataConfig)
                        {
                            count++;
                            var newrow = data.NewRow();
                            newrow["GROUPNAME"] = "Metadata";
                            AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                            data.Rows.Add(newrow);
                        }
                    if (Program.paymentsConfig != null)
                        foreach (KeyValuePair<string, ConfigLine> item in Program.paymentsConfig)
                        {
                            count++;
                            var newrow = data.NewRow();
                            newrow["GROUPNAME"] = "Payments";
                            AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                            data.Rows.Add(newrow);
                        }
                    if (Program.optionUserDefinedConfig != null)
                        foreach (KeyValuePair<string, ConfigLine> item in Program.optionUserDefinedConfig)
                        {
                            count++;
                            var newrow = data.NewRow();
                            newrow["GROUPNAME"] = "OptionUserDefined";
                            AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                            data.Rows.Add(newrow);
                        }
                    if (Program.taxBreakdownsConfig != null)
                        foreach (KeyValuePair<string, ConfigLine> item in Program.taxBreakdownsConfig)
                        {
                            count++;
                            var newrow = data.NewRow();
                            newrow["GROUPNAME"] = "TaxBreakdowns";
                            AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                            data.Rows.Add(newrow);
                        }
                    if (Program.summarizeInfoConfig != null)
                        foreach (KeyValuePair<string, ConfigLine> item in Program.summarizeInfoConfig)
                        {
                            count++;
                            var newrow = data.NewRow();
                            newrow["GROUPNAME"] = "SummarizeInfo";
                            AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                            data.Rows.Add(newrow);
                        }
                    if (Program.itemInfoConfig != null)
                        foreach (KeyValuePair<string, ConfigLine> item in Program.itemInfoConfig)
                        {
                            count++;
                            var newrow = data.NewRow();
                            newrow["GROUPNAME"] = "ItemInfo";
                            AddGinfo(newrow, now, today_dd_MM_yyyy, count, item.Value);
                            data.Rows.Add(newrow);
                        }

                    V6Tools.V6Export.ExportData.ToExcel(data, o.FileName, "");
                }
            }
            catch (Exception ex)
            {
                BaseMessage.Show(ex.Message, 0, this);
            }
        }

        private void AddGinfo(DataRow newrow, DateTime now, string today_dd_MM_yyyy, int count, ConfigLine line)
        {

            newrow["Field"] = line.Field;
            newrow["Value"] = line.Value;
            newrow["Type"] = line.Type;
            newrow["FieldV6"] = line.FieldV6;
            newrow["DataType"] = line.DataType;
            newrow["Format"] = line.Format;
            newrow["LOAI"] = "AAPPR_SOA2"; // XUẤT EXCEL TỰ SỬA.
            newrow["TEN"] = line.Note;


            newrow["DATE0"] = today_dd_MM_yyyy;
            newrow["TIME0"] = ObjectAndString.ObjectToString(now.AddSeconds(count), "HH:mm:ss");
            newrow["USER_ID0"] = 53;
            newrow["DATE2"] = today_dd_MM_yyyy;
            newrow["TIME2"] = newrow["TIME0"];
            newrow["USER_ID2"] = 53;

            newrow["MA_TD1"] = "10";
            newrow["GC_TD1"] = "MISA_" + ("000" + count).Right(3);

            newrow["MA_TD2"] = line.MA_TD2;
            newrow["MA_TD3"] = line.MA_TD2;
            newrow["SL_TD1"] = line.SL_TD1;
            newrow["SL_TD2"] = line.SL_TD2;
            newrow["SL_TD3"] = line.SL_TD3;
            newrow["NOGEN"] = line.NoGen ? "1" : "";
        }

        

        
    }//End class
}//End namespace
