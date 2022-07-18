using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using V6Tools;
using V6Tools.V6Convert;
using Newtonsoft.Json;
using PdfiumViewer;
using Spy;
using Spy.SpyObjects;
using V6Controls.Forms.Viewer;
using V6ThuePost.ResponseObjects;
using V6ThuePost_MISA_Api;
using V6ThuePost.MISA_Objects;
using V6ThuePost_MISA_Api.Objects;

namespace V6ThuePost
{
    static class Program
    {
        public static bool _TEST_ = true;
        private static DateTime _TEST_DATE_ = DateTime.Now;
        public static bool _write_log = false;
        public static string _appID = null;

        #region ==== SETTING ====
        public static Dictionary<string,string> _setting = new Dictionary<string,string>();
        public static string SETTING(string KEY)
        {
            if (_setting.ContainsKey(KEY)) return _setting[KEY];
            return null;
        }
        public static bool USETAXBREAKDOWNS{ get{ return SETTING("USETAXBREAKDOWNS") == "1";}}
        public static bool USEMISAOBJECT { get { return SETTING("USEMISAOBJECT") == "1"; } }
        public static string SIGNMODE { get { return SETTING("SIGNMODE"); } }
        public static bool COMACQT { get { return SETTING("COMACQT") == "1"; } }
        #endregion ==== SETTING ====

        #region ===== VAR =====
        public static MISA_WS _MISA_WS = null;
        /// <summary>
        /// Link host
        /// </summary>
        public static string baseUrl = "", _dateType = "";
        
        //public static string mst = "";
        /// <summary>
        /// Tên đăng nhập vào host.
        /// </summary>
        public static string username = "";
        /// <summary>
        /// Password đăng nhập vào host.
        /// </summary>
        public static string password = "";
        /// <summary>
        /// Mã số thuế.
        /// </summary>
        public static string _codetax = "";
        public static string _version = "";
        /// <summary>
        /// Seri usb token.
        /// </summary>
        public static string _SERIAL_CERT = "";
        private static string token_password_title = "";
        private static string token_password = "";
        /// <summary>
        /// Cờ bắt đầu.
        /// </summary>
        static string flagFileName1 = ".flag1";
        /// <summary>
        /// Cờ thành công
        /// </summary>
        public static string flagFileName2 = ".flag2";
        /// <summary>
        /// Cờ lỗi
        /// </summary>
        static string flagFileName3 = ".flag3";
        /// <summary>
        /// Lấy về số hóa đơn
        /// </summary>
        public static string flagFileName4 = ".flag4";
        /// <summary>
        /// Ghi xuốn UID tự tạo
        /// </summary>
        static string flagFileName5 = ".flag5";
        /// <summary>
        /// Cờ kết thúc
        /// </summary>
        static string flagFileName9 = ".flag9";

        /// <summary>
        /// key đầu ngữ
        /// </summary>
        private static string fkey0 = "";
        public static string _RefID_or_fkey = "";
        
        
        
        #endregion var

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var startupPath = Application.StartupPath;
            var dir = new DirectoryInfo(startupPath);
            var dir_name = dir.Name.ToLower();
            if (dir_name == "debug")
            {
                _TEST_ = true;
                MessageBox.Show("Test! UID được tạo mới tự động mỗi lần Read.");
            }
            else
            {
                _TEST_ = false;
            }

            string message = "";

            if (args != null && args.Length > 0)
            {
                string result = "";
                string mode = "";
                string arg1_xmlFile = "";
                string arg2 = "";
                string arg3 = "";//File upload mode M1, data mode M2, old_fkey mode S
                string arg4 = "";//File upload mode S

                mode = args[0];
                if (args.Length > 1) arg1_xmlFile = args[1];
                if (args.Length > 2) arg2 = args[2];
                if (args.Length > 3) arg3 = args[3];
                if (args.Length > 4) arg4 = args[4];

                try
                {
                    
                    string jsonBody = "";
                    V6Return v6return = new V6Return();
                    ReadXmlInfo(arg1_xmlFile);
                    

                    string dbfFile = arg2;

                    string dbfFile3 = "";
                    if (USETAXBREAKDOWNS)
                    {
                        dbfFile3 = dbfFile.ToLower().Replace(".dbf", "3.dbf");
                    }

                    _MISA_WS = new MISA_WS(baseUrl, username, password, _codetax, _appID, COMACQT);

                    if (mode.ToUpper() == "MTEST")
                    {
                        ReadData_MISA(dbfFile, dbfFile3, "M", "HSM"); // đọc để lấy tên flag.
                        jsonBody = "";
                        File.Create(flagFileName1).Close();
                        result = _MISA_WS.POST_CREATE_INVOICE_HSM(jsonBody, out v6return);
                        if (v6return.RESULT_ERROR_MESSAGE != null && v6return.RESULT_ERROR_MESSAGE.Contains("JSON_PARSE_ERROR"))
                        {
                            result = "Kết nối ổn. " + result;
                            File.Create(flagFileName2).Close();
                            goto End;
                        }
                        if (result.Contains("\"errorCode\":\"TEMPLATE_NOT_FOUND\""))
                        {
                            result = "Kết nối ổn. " + result;
                            File.Create(flagFileName2).Close();
                            goto End;
                        }
                    }
                    else if (mode.StartsWith("M"))
                    {
                        StartAutoInputTokenPassword();
                        //generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        //{
                        //    Field = "adjustmentType",
                        //    Value = "1",
                        //};
                        //Guid new_uid = Guid.NewGuid();
                        //if (mode == "MG")
                        //{
                        //    generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
                        //    {
                        //        Field = "transactionUuid",
                        //        Value = "" + new_uid,
                        //    };
                        //}

                        if (mode == "M0") // DRAF
                        {
                            jsonBody = ReadData_MISA(dbfFile, dbfFile3, "M");
                            File.Create(flagFileName1).Close();
                            result = _MISA_WS.POST_CREATE_INVOICE(jsonBody, out v6return);
                        }
                        else if (string.IsNullOrEmpty(_SERIAL_CERT))
                        {
                            jsonBody = ReadData_MISA(dbfFile, dbfFile3, "M", "HSM");
                            if(mode == "MG") WriteFlag(flagFileName5, "" + _RefID_or_fkey);
                            File.Create(flagFileName1).Close();
                            result = _MISA_WS.POST_CREATE_INVOICE_HSM(jsonBody, out v6return);
                        }
                        else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                        {
                            generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                            {
                                Field = "certificateSerial",
                                Value = _SERIAL_CERT,
                            };
                            jsonBody = ReadData_MISA(dbfFile, dbfFile3, "M");
                            if(mode == "MG") WriteFlag(flagFileName5, "" + _RefID_or_fkey);
                            string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                            result = _MISA_WS.CreateInvoice_GetXml_Sign(jsonBody, _SERIAL_CERT, out v6return);
                        }

                        if (string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE))
                        {
                            message = "Thành công. Số hóa đơn: " + v6return.SO_HD;
                        }
                        else
                        {
                            message = v6return.RESULT_ERROR_MESSAGE;
                        }
                    }
                    else if (mode.StartsWith("S"))
                    {
                        if (mode.EndsWith("3"))
                        {
                            generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                            {
                                Field = "adjustmentType",
                                Value = "3",
                            };
                        }
                        else if (mode.EndsWith("5"))
                        {
                            generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                            {
                                Field = "adjustmentType",
                                Value = "5",
                            };
                        }
                        else
                        {
                            generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                            {
                                Field = "adjustmentType",
                                Value = "3",
                            };
                        }

                        jsonBody = ReadData_MISA(dbfFile, dbfFile3, "S");
                        File.Create(flagFileName1).Close();
                        result = _MISA_WS.POST_EDIT(jsonBody, out v6return);
                    }
                    else if (mode.StartsWith("T") && mode.EndsWith("_JSON"))
                    {
                        // Lưu ý dữ liệu mode Replace. (T)
                        jsonBody = ReadText(arg2);
                        result = _MISA_WS.POST_REPLACE(jsonBody, _version == "V45I", out v6return);
                    }
                    else if (mode == "T")
                    {
                        jsonBody = ReadData_MISA(dbfFile, dbfFile3, "T");
                        File.Create(flagFileName1).Close();
                        result = _MISA_WS.POST_REPLACE(jsonBody, _version == "V45I", out v6return);
                    }
                    else if (mode == "GET_INVOICE" || mode == "GET_INVOICE_JSON")
                    {
                        // V6ThuePostViettelV2.exe GET_INVOICE "V6ThuePost.xml" "UID"
                        string uid = arg2;
                        MakeFlagNames(uid);
                        File.Create(flagFileName1).Close();
                        result = _MISA_WS.SearchInvoiceByTransactionUuid(_codetax, uid, out v6return);


                    }
                    else if (mode.StartsWith("G")) // call exe như mode M
                    {
                        BaseMessage.Show("Chưa hỗ trợ!");
                        //jsonBody = ReadData(dbfFile, "M");
                        //if (mode == "G1" || mode == "G") // Gạch nợ theo fkey
                        //{
                        //    File.Create(flagFileName1).Close();
                        //    string invoiceNo = ""  + postObject.generalInvoiceInfo["invoiceSeries"] +  row0["SO_CT"].ToString().Trim();
                        //    string strIssueDate = V6JsonConverter.ObjectToJson(postObject.generalInvoiceInfo["invoiceIssuedDate"], null);
                        //    string templateCode = postObject.generalInvoiceInfo["templateCode"].ToString();
                        //    string buyerEmailAddress = postObject.buyerInfo["buyerEmail"].ToString();
                        //    string paymentType = postObject.generalInvoiceInfo["paymentType"].ToString();
                        //    string paymentTypeName = postObject.generalInvoiceInfo["paymentTypeName"].ToString();
                        //    result = _viettelV2_ws.UpdatePaymentStatus(_codetax, invoiceNo, strIssueDate, templateCode,
                        //        buyerEmailAddress, paymentType, paymentTypeName, "true", out v6return);
                        //}
                        ////else if (mode == "G2") // Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
                        ////{
                        ////    File.Create(flagFileName1).Close();
                        ////    result = confirmPayment(arg2);
                        ////}
                        //else if (mode == "G3") // Hủy gạch nợ theo fkey
                        //{
                        //    File.Create(flagFileName1).Close();
                        //    string invoiceNo = ""  + postObject.generalInvoiceInfo["invoiceSeries"] +  row0["SO_CT"].ToString().Trim();
                        //    string strIssueDate = V6JsonConverter.ObjectToJson(postObject.generalInvoiceInfo["invoiceIssuedDate"], null);
                        //    string templateCode = postObject.generalInvoiceInfo["templateCode"].ToString();
                        //    string buyerEmailAddress = postObject.buyerInfo["buyerEmail"].ToString();
                        //    string paymentType = postObject.generalInvoiceInfo["paymentType"].ToString();
                        //    string paymentTypeName = postObject.generalInvoiceInfo["paymentTypeName"].ToString();
                        //    result = _viettelV2_ws.UpdatePaymentStatus(_codetax, invoiceNo, strIssueDate, templateCode,
                        //        buyerEmailAddress, paymentType, paymentTypeName, "false", out v6return);
                        //}

                    }
                    else if (mode == "H" || mode == "H_JSON")
                    {
                        // V6ThuePostViettelV2.exe H "V6ThuePost.xml" "AB/19E0000341" "27/11/2019" "stt_rec"
                        string soseri_soct = arg2;
                        DateTime ngay_ct = ObjectAndString.StringToDate(arg3);
                        string strIssueDate = V6JsonConverter.ObjectToJson(ngay_ct, "VIETTEL");
                        string stt_rec = arg4;
                        MakeFlagNames(stt_rec);
                        File.Create(flagFileName1).Close();
                        result = _MISA_WS.HUY_HOA_DON("transactionID", "invNo", "2022-01-01", "số liệu sai", out v6return);
                    }
                    else if (mode.StartsWith("P"))
                    {
                        string V6SoftLocalAppData_Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "V6Soft");
                        if (!Directory.Exists(V6SoftLocalAppData_Directory))
                            Directory.CreateDirectory(V6SoftLocalAppData_Directory);
                        string soseri_soct = arg2;
                        string templateCode = arg3;
                        string uid = arg4;
                        MakeFlagNames(uid);
                        File.Create(flagFileName1).Close();
                        if (mode.StartsWith("P2") || mode.StartsWith("PP2"))
                        {
                            MakeFlagNames(uid);
                            //string strIssueDate = V6JsonConverter.ObjectToJson(ngay_ct, "VIETTEL");
                            string info = _MISA_WS.SearchInvoiceByTransactionUuid(_codetax, uid, out v6return);
                            SearchInvoiceResponseV2 infoObj =  v6return.RESULT_OBJECT as SearchInvoiceResponseV2;
                            if (infoObj != null)
                            {
                                string strIssueDate = "infoObj.result[0].issueDate";
                                try
                                {
                                    result = _MISA_WS.DownloadInvoicePDF_CHUYENDOI(_codetax, soseri_soct, strIssueDate, V6SoftLocalAppData_Directory, out v6return);
                                }
                                catch (Exception download_ex)
                                {
                                    result = download_ex.Message;
                                    v6return.RESULT_ERROR_MESSAGE += result;
                                }
                            }
                            else
                            {
                                v6return.RESULT_ERROR_MESSAGE += "SearchError";
                            }
                        }
                        else
                        {
                            try
                            {
                                result = _MISA_WS.TAI_HOA_DON_PDF(uid, "transactionID", V6SoftLocalAppData_Directory, out v6return);
                            }
                            catch (Exception download_ex)
                            {
                                result = download_ex.Message;
                                v6return.RESULT_ERROR_MESSAGE += result;
                            }
                        }
                        
                        if (string.IsNullOrEmpty(result))
                        {
                            // Lỗi
                            File.Create(flagFileName3).Close();
                            goto End;
                        }
                        else
                        {

                        }
                        // In

                        if (mode.EndsWith("JSON")) // Không view, không in.
                        {
                            goto return_result;
                        }

                        if (mode.StartsWith("PP"))
                        {
                            PDF_ViewPrintForm view = new PDF_ViewPrintForm(v6return.PATH);
                            view.ShowDialog();
                        }
                        else
                        {
                            
                            PrintDialog printDialog = new PrintDialog();
                            printDialog.AllowSomePages = false;//
                            //printDialog.Document = printDocument;
                            printDialog.UseEXDialog = true;
                            //printDialog.Document.PrinterSettings.FromPage = 1;
                            //printDialog.Document.PrinterSettings.ToPage = pdfDocument1.PageCount;
                            if (printDialog.ShowDialog() != DialogResult.OK)
                                return;
                            string pdf_file = result;
                            PdfDocument pdfDocument1 = PdfDocument.Load(pdf_file);
                            using (PrintDocument printDocument = pdfDocument1.CreatePrintDocument(PdfPrintMode.ShrinkToMargin))
                            {
                                printDocument.PrinterSettings = printDialog.PrinterSettings;
                                printDialog.Document = printDocument;
                                try
                                {
                                    if (printDialog.Document.PrinterSettings.FromPage <= pdfDocument1.PageCount)
                                        printDialog.Document.Print();
                                }
                                catch(Exception ex)
                                {
                                    //f9Error += ex.Message;
                                    //f9ErrorAll += ex.Message;
                                    //f9MessageAll += ex.Message;
                                }
                            }
                        }

                    }

                    //Phân tích result
                    return_result:
                    if (mode.EndsWith("JSON"))
                    {
                        EncodeV6Return(v6return);
                        Console.Write(v6return.ToJson());
                    }
                    else
                    {
                        message = "";
                        if (string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE))
                        {
                            message = "OK.";
                            WriteFlag(flagFileName4, v6return.SO_HD + ":" + v6return.ID + ":" + v6return.SECRET_CODE);
                            File.Create(flagFileName2).Close();
                        }
                        else
                        {
                            message = "ERR:";
                            WriteFlag(flagFileName3, v6return.RESULT_ERROR_MESSAGE);
                        }
                        result = message + "\n" + result;
                    }
                }
                catch (Exception ex)
                {
                    StopAutoInputTokenPassword();
                    File.Create(flagFileName3).Close();
                    //MessageBox.Show(ex.Message);
                    BaseMessage.Show(ex.Message, 500);
                }
            End:
                if (mode.EndsWith("JSON"))
                {

                }
                else
                {
                    File.Create(flagFileName9).Close();
                    BaseMessage.Show(message, 500);
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            //Environment.Exit(0);
            Process.GetCurrentProcess().Kill(); // AAARGHHHGHglglgghh...
        }

        private static string SearchUID(string jsonBody)
        {
            //","transactionUuid":"1cc173cd-7a97-eb11-80f5-6c2b59a11324"
            int index1 = jsonBody.IndexOf("\"transactionUuid\":\"") + "\"transactionUuid\":\"".Length;
            int index2 = jsonBody.IndexOf("\"", index1) + "".Length;
            string result = jsonBody.Substring(index1, index2 - index1);
            return result;
        }

        private static void EncodeV6Return(V6Return v6return)
        {
            if (!string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE))
            {
                v6return.RESULT_ERROR_MESSAGE = ChuyenMaTiengViet.UNICODEtoTCVN(v6return.RESULT_ERROR_MESSAGE);
            }
            if (!string.IsNullOrEmpty(v6return.RESULT_MESSAGE))
            {
                v6return.RESULT_MESSAGE = ChuyenMaTiengViet.UNICODEtoTCVN(v6return.RESULT_MESSAGE);
            }
        }

        private static string ReadText(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }

        internal static void WriteFlag(string fileName, string content)
        {
            var fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Close();
            fs.Close();
        }

        


        /// <summary>
        /// Cần đọc xml trước!
        /// </summary>
        /// <param name="dbfFile"></param>
        /// <param name="mode">M mới hoặc T thay thế</param>
        /// <param name="mode2">null hoặc HSM</param>
        /// <returns></returns>
        public static string ReadData_MISA(string dbfFile, string dbfFile3, string mode, string mode2 = "HSM")
        {
            string result = "";
            try
            {
                var list_hoadon = new MISA_PostObject();
                var hoadon = new Dictionary<string, object>();
                list_hoadon.Add(hoadon);

                var hoadonMISA = new OriginalInvoiceData();
                var list_hoadonMISA = new List<OriginalInvoiceData>();


                list_hoadonMISA.Add(hoadonMISA);
                //postObject.hoadon = hoadon;
                //ReadXmlInfo(xmlFile);
                DataTable dataDbf =  ParseDBF.ReadDBF(dbfFile);
                DataTable data = Data_Table.FromTCVNtoUnicode(dataDbf);
                DataTable table3 = null;
                if (USETAXBREAKDOWNS && !string.IsNullOrEmpty(dbfFile3))
                {
                    DataTable dataDbf3 = ParseDBF.ReadDBF(dbfFile3);
                    table3 = Data_Table.FromTCVNtoUnicode(dataDbf3);
                }
                //Fill data to postObject
                row0 = data.Rows[0];

                //fkeyA = fkey0 + row0["STT_REC"];
                
                
                
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    hoadon[item.Key] = item.Value.GetValue(row0);
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, item.Key, item.Value.GetValue(row0));
                }



                if (mode == "S")
                {
                    //Lập hóa đơn điều chỉnh: trong chi tiết và dsthuesuat có thêm trường dieuchinh_tanggiam
                    hoadon["OrgInvNo"] = row0["SO_CT_OLD"].ToString().Trim();
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, "OrgInvNo", row0["SO_CT_OLD"].ToString().Trim());
                }

                if (mode == "T")
                {
                    //Lập hóa đơn thay thế:
                    hoadon["OrgInvNo"] = row0["SO_CT_OLD"].ToString().Trim();
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, "OrgInvNo", row0["SO_CT_OLD"].ToString().Trim());
                    ////thông tin hóa đơn thay thế hoặc điều chỉnh 
                    //if (rbAdjust.Checked)
                    //{
                    //    invoiceData.ReferenceType = 2;
                    //    invoiceData.OrgInvoiceType = 1;
                    //    invoiceData.OrgInvNo = txtNumberOrg.Text;
                    //    invoiceData.OrgInvTemplateNo = cboKyHieu.Text.Substring(0, 1);
                    //    invoiceData.OrgInvSeries = cboKyHieu.Text;
                    //    invoiceData.OrgInvDate = dteOrgDate.Value.Date;
                    //}
                    //else if (rbReplace.Checked)
                    //{
                    //    invoiceData.ReferenceType = 1;
                    //    invoiceData.OrgInvoiceType = 1;
                    //    invoiceData.OrgInvNo = txtNumberOrg.Text;
                    //    invoiceData.OrgInvTemplateNo = cboKyHieu.Text.Substring(0, 1);
                    //    invoiceData.OrgInvSeries = cboKyHieu.Text;
                    //    invoiceData.OrgInvDate = dteOrgDate.Value.Date;
                    //}
                }

                if (_TEST_)
                {
                    Guid new_uid = Guid.NewGuid();
                    hoadon["RefID"] = "" + new_uid;
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, "RefID", "" + new_uid);
                }

                _RefID_or_fkey = "" + hoadonMISA.RefID;
                MakeFlagNames(_RefID_or_fkey);

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    hoadon[item.Key] = item.Value.GetValue(row0);
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, item.Key, item.Value.GetValue(row0));
                    
                }
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    hoadon[item.Key] = item.Value.GetValue(row0);
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, item.Key, item.Value.GetValue(row0));
                }
                
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    hoadon[item.Key] = item.Value.GetValue(row0);
                    ObjectAndString.SetObjectPropertyValue(hoadonMISA, item.Key, item.Value.GetValue(row0));
                }

                if (metadataConfig != null)
                {
                    foreach (KeyValuePair<string, ConfigLine> metaItem in metadataConfig)
                    {
                        Dictionary<string, object> metadata = new Dictionary<string, object>();
                        metadata["invoiceCustomFieldId"] = ObjectAndString.ObjectToInt(metaItem.Value.SL_TD1);
                        metadata["keyTag"] = metaItem.Key;
                        metadata["valueType"] = metaItem.Value.DataType; // text, number, date
                        if (metaItem.Value.DataType.ToLower() == "date")
                        {
                            metadata["dateValue"] = metaItem.Value.GetValue(row0);
                        }
                        else if (metaItem.Value.DataType.ToLower() == "number")
                        {
                            metadata["numberValue"] = metaItem.Value.GetValue(row0);
                        }
                        else if (metaItem.Value.DataType.ToUpper() == "N2C0VNDE")
                        {
                            // N2C0VNDE thêm đọc số tiền nếu ma_nt != VND
                            string ma_nt = row0["MA_NT"].ToString().Trim().ToUpper();
                            if (ma_nt != "VND")
                            {
                                metadata["stringValue"] = ObjectAndString.ObjectToString(metaItem.Value.GetValue(row0));
                                metadata["valueType"] = "text";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            
                            metadata["stringValue"] =  ObjectAndString.ObjectToString(metaItem.Value.GetValue(row0));
                        }
                        metadata["keyLabel"] = ObjectAndString.ObjectToString(metaItem.Value.MA_TD2);
                        metadata["isRequired"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD2);
                        metadata["isSeller"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD3);

                        //{
                        //   "invoiceCustomFieldId": 1135,
                        //   "keyTag": "dueDate",
                        //   "valueType": "date",
                        //   "dateValue": 1544115600000,
                        //   "keyLabel": "Hạn thanh toán",
                        //   "isRequired": false,
                        //   "isSeller": false
                        // },
                        //hoadon["metadata"] = metadata; // bị bỏ qua, code thừa.

                    }
                }




                if (USETAXBREAKDOWNS && table3 != null)
                {
                    var dsthuesuat = new List<TaxRateInfo>();
                    foreach (DataRow row3 in table3.Rows)
                    {
                        Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                        {
                            taxBreakdown[item.Key] = item.Value.GetValue(row3);
                        }
                        dsthuesuat.Add(taxBreakdown.ToModel<TaxRateInfo>());
                    }
                    hoadon["TaxRateInfo"] = dsthuesuat; // nhiều dòng tùy vào table3
                    hoadonMISA.TaxRateInfo = dsthuesuat;
                }
                else
                {
                    var dsthuesuat = new List<TaxRateInfo>();
                    Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                    {
                        taxBreakdown[item.Key] = item.Value.GetValue(row0);
                    }
                    dsthuesuat.Add(taxBreakdown.ToModel<TaxRateInfo>());
                    hoadon["TaxRateInfo"] = dsthuesuat;
                    hoadonMISA.TaxRateInfo = dsthuesuat; // Chỉ có 1 dòng.
                }

                if (optionUserDefinedConfig != null)
                {
                    var options = new Dictionary<string, object>();
                    
                    foreach (KeyValuePair<string, ConfigLine> item in optionUserDefinedConfig)
                    {
                        options[item.Key] = item.Value.GetValue(row0);
                    }
                    hoadon["OptionUserDefined"] = options;
                    hoadonMISA.OptionUserDefined = options.ToModel<OptionUserDefined>();
                }

                var dschitiet = new List<Dictionary<string,object>>();
                var dschitietMISA = new List<OriginalInvoiceDetail>();
                foreach (DataRow row in data.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = item.Value.GetValue(row);
                    }
                    dschitiet.Add(rowData);
                    dschitietMISA.Add(rowData.ToModel <OriginalInvoiceDetail>());
                }
                hoadon["OriginalInvoiceDetail"] = dschitiet;
                hoadonMISA.OriginalInvoiceDetail = dschitietMISA;


                //result = postObject.ToJson(_dateType);
                if (mode2 == "HSM")
                {
                    var hsm_send_data = new List<Dictionary<string, object>>();
                    var one = new Dictionary<string, object>();
                    one["RefID"] = hoadonMISA.RefID;
                    if (USEMISAOBJECT)
                    {
                        one["OriginalInvoiceData"] = hoadonMISA;
                    }
                    else
                    {
                        one["OriginalInvoiceData"] = hoadon;
                    }
                    
                    hsm_send_data.Add(one);

                    //result = V6JsonConverter.ObjectToJson(hsm_send_data, _dateType);
                    //result = Newtonsoft.Json.Utilities. SerializeUtil.SerializeObject(parameter);
        
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    result = JsonConvert.SerializeObject(hsm_send_data, settings);
        

                }
                else
                {
                    if (USEMISAOBJECT)
                    {
                        //result = V6JsonConverter.ObjectToJson(list_hoadonMISA, _dateType);
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                        result = JsonConvert.SerializeObject(list_hoadonMISA, settings);
                    }
                    else
                    {
                        result = V6JsonConverter.ObjectToJson(list_hoadon, _dateType);
                    }
                    
                }

                if (_write_log)
                {
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            catch (Exception ex)
            {
                BaseMessage.Show(ex.Message, 0);
            }
            return result;
        }

        

        private static void MakeFlagNames(string flagName)
        {
            flagFileName1 = flagName + ".flag1";
            flagFileName2 = flagName + ".flag2";
            flagFileName3 = flagName + ".flag3";
            flagFileName4 = flagName + ".flag4";
            flagFileName5 = flagName + ".flag5";
            flagFileName9 = flagName + ".flag9";
        }

        //public static string MoneyToWords(decimal money, string lang, string ma_nt)
        //{
        //    if (lang == "V")
        //    {
        //        return DocSo.DOI_SO_CHU_NEW(money, V6Alnt_begin1(ma_nt), V6Alnt_end1(ma_nt), V6Alnt_only1(ma_nt), V6Alnt_point1(ma_nt),
        //            V6Alnt_endpoint1(ma_nt));
        //    }
        //    else
        //    {
        //        return DocSo.NumWordsWrapper(money, V6Alnt_begin2(ma_nt), V6Alnt_end2(ma_nt), V6Alnt_only2(ma_nt), V6Alnt_point2(ma_nt),
        //            V6Alnt_endpoint2(ma_nt));
        //    }
        //}

        

        //private static Dictionary<string, string> sellerInfo;
        private static MISA_PostObject postObject;
        public static DataRow row0;

        internal static Dictionary<string, ConfigLine> v6infoInvoiceInfoConfig = null;

        internal static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        internal static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        internal static Dictionary<string, ConfigLine> sellerInfoConfig = null;
        internal static Dictionary<string, ConfigLine> metadataConfig = null;
        internal static Dictionary<string, ConfigLine> paymentsConfig = null;
        internal static Dictionary<string, ConfigLine> itemInfoConfig = null;
        internal static Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        internal static Dictionary<string, ConfigLine> taxBreakdownsConfig = null;
        internal static Dictionary<string, ConfigLine> optionUserDefinedConfig = null;
        
        public static void ReadXmlInfo(string xmlFile)
        {
            //postObject = new PostObject();
            v6infoInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            //metadataConfig = new Dictionary<string, ConfigLine>();
            metadataConfig = null;
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();
            optionUserDefinedConfig = new Dictionary<string, ConfigLine>();
            XmlTextReader reader = new XmlTextReader(xmlFile.ToLower());
            try
            {
                string error = "";
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "V6Info":
                            {
                                ConfigLine line = V6ThuePostUtility.ReadXmlLine(reader);
                                string line_field = line.Field.ToLower();
                                v6infoInvoiceInfoConfig.Add(line_field, line);
                                switch (line_field)
                                {
                                    case "username":
                                        username = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "password":
                                        password = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "codetax":
                                        _codetax = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "version":
                                        _version = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "serialcert":
                                    case "certificateserial":
                                        _SERIAL_CERT = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "token_password_title":
                                        token_password_title = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                        break;
                                    case "token_password":
                                        token_password = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "baselink":
                                        baseUrl = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "v6fkey":
                                        fkey0 = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                        break;
                                    //case "version":
                                    //    _version = UtilityHelper.DeCrypt(line.Value);
                                    //    break;
                                    
                                        break;
                                    case "datetype":
                                        _dateType = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                        break;
                                    case "writelog":
                                        _write_log = ObjectAndString.ObjectToBool(line.Value);
                                        break;
                                    case "setting":
                                        _setting = ObjectAndString.StringToStringDictionary(line.Value);
                                        break;
                                    case "appid":
                                        _appID = UtilityHelper.DeCrypt(line.Value);
                                        break;

                                        
                                }
                                break;
                            }
                        case "GeneralInvoiceInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (generalInvoiceInfoConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else generalInvoiceInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "BuyerInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (buyerInfoConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else buyerInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "SellerInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (sellerInfoConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else sellerInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "MetaData":
                            {
                                if (metadataConfig == null) metadataConfig = new Dictionary<string, ConfigLine>();
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (metadataConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else metadataConfig[key] = V6ThuePostUtility.ReadXmlLine(reader);
                                }
                                break;
                            }
                        case "Payments":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (paymentsConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else paymentsConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "ItemInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (itemInfoConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else itemInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "SummarizeInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (summarizeInfoConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else summarizeInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "TaxBreakdowns":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (taxBreakdownsConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else taxBreakdownsConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "OptionUserDefined":
                            {
                                if (optionUserDefinedConfig == null) optionUserDefinedConfig = new Dictionary<string, ConfigLine>();
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (optionUserDefinedConfig.ContainsKey(key)) error += "\nTrùng key:" + key;
                                    else optionUserDefinedConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                                }
                            }
                            break;
                        default:
                            {
                                break;
                            }
                    }
                }
                reader.Close();

                if (error.Length > 0)
                {
                    BaseMessage.Show(error);
                }
            }
            catch (Exception)
            {
                reader.Close();
                throw;
            }
        }

        


        private static void AutoInputTokenPassword()
        {
            try
            {
                //Find input password windows.
                Spy001 spy = new Spy001();
                var thisProcessID = Process.GetCurrentProcess().Id;
                SpyWindowHandle input_password_window = spy.FindWindow(token_password_title, thisProcessID);

                while (input_password_window == null)
                {
                    input_password_window = spy.FindWindow(token_password_title, thisProcessID);
                }
                //Find input password textbox, ok button
                //SpyWindowHandle input_handle = null;
                //SpyWindowHandle chk_soft_handle = null;
                //SpyWindowHandle ok_button_handle = null;
                //SpyWindowHandle soft_keyboard = null;

                //foreach (KeyValuePair<string, SpyWindowHandle> child_item in input_password_window.Childs)
                //{
                //    if (child_item.Value.Class.ClassName == "Edit")//Kích hoạt bàn phím ảo
                //    {
                //        input_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text == "Đăng nhập")
                //    {
                //        ok_button_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text.StartsWith("Kích hoạt"))
                //    {
                //        chk_soft_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text == "soft keyboard")
                //    {
                //        soft_keyboard = child_item.Value;
                //    }
                //}

                //Input password
                {
                    input_password_window.SetForegroundWindow();
                    //if (input_handle != null) input_handle.SetFocus();
                    foreach (char c in token_password)
                    {
                        spy.SendKeyPress(c);
                    }
                    spy.SendKeyPressEnter();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Program.AutoInputTokenPassword " + ex.Message);
            }
        }

        public static void StartAutoInputTokenPassword()
        {
            StopAutoInputTokenPassword();
            if (string.IsNullOrEmpty(token_password)) return;
            if (string.IsNullOrEmpty(token_password_title)) return;
            autoToken = new Thread(AutoInputTokenPassword);
            //autoToken.IsBackground = true;
            autoToken.Start();
        }

        private static Thread autoToken = null;
        public static void StopAutoInputTokenPassword()
        {
            if (autoToken != null && autoToken.IsAlive) autoToken.Abort();
        }

    }
}
