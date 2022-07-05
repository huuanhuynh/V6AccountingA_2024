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
using V6ThuePost.ViettelV2Objects;
using V6ThuePostViettelV2Api;

namespace V6ThuePost
{
    static class Program
    {
        public static bool _TEST_ = true;
        private static DateTime _TEST_DATE_ = DateTime.Now;
        public static bool _write_log = false;
        #region ===== VAR =====
        public static ViettelV2WS _viettelV2_ws = null;
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
        private static string _uid = "";
        private static string _debug = "";
        public static bool _useTaxBreakdowns = false;
        /// <summary>
        /// key trong data
        /// </summary>
        public static string fkeyA;
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
                    if (_debug == "1")
                    {
                        MessageBox.Show("debug=1");
                    }
                    string dbfFile = arg2;
                    string dbfFile3 = "";
                    if (_useTaxBreakdowns)
                    {
                        dbfFile3 = dbfFile.ToLower().Replace(".dbf", "3.dbf");
                    }

                    _viettelV2_ws = new ViettelV2WS(baseUrl, username, password, _codetax);

                    if (mode.ToUpper() == "MTEST")
                    {
                        ReadData(dbfFile, dbfFile3, "M"); // đọc để lấy tên flag.
                        jsonBody = "";
                        File.Create(flagFileName1).Close();
                        result = _viettelV2_ws.POST_CREATE_INVOICE(jsonBody, _version == "V45I", out v6return);
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
                    else if (mode.ToUpper() == "MTEST_JSON")
                    {
                        //ReadData(arg2, "M"); // đọc để lấy tên flag.
                        jsonBody = "";
                        result = _viettelV2_ws.POST_CREATE_INVOICE(jsonBody, _version == "V45I", out v6return);
                        EncodeV6Return(v6return);
                        Console.Write(v6return.ToJson());
                        goto End;
                    }
                    else if (mode.StartsWith("M") && mode.EndsWith("_JSON"))
                    {
                        jsonBody = ReadText(arg2);
                        
                        if (mode.StartsWith("M_F4_"))
                        {
                            result = _viettelV2_ws.POST_DRAFT(jsonBody, out v6return);
                        }
                        else
                        {
                            result = _viettelV2_ws.POST_CREATE_INVOICE(jsonBody, _version == "V45I", out v6return);
                        }

                        // Nếu error null mà ko có so_hd thì chạy hàm lấy thông tin.
                        if (string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE)
                            && string.IsNullOrEmpty(v6return.SO_HD))
                        {
                            Thread.Sleep(10000); // Chờ 10 giây.
                            string oldTransactionID = v6return.ID;
                            _uid = SearchUID(jsonBody);
                            _viettelV2_ws.SearchInvoiceByTransactionUuid(_codetax, _uid, out v6return);
                            v6return.ID = oldTransactionID;
                        }
                    }
                    else if (mode.StartsWith("M"))
                    {
                        StartAutoInputTokenPassword();
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "1",
                        };
                        Guid new_uid = Guid.NewGuid();
                        if (mode == "MG")
                        {
                            generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
                            {
                                Field = "transactionUuid",
                                Value = "" + new_uid,
                            };
                        }

                        if (mode == "M0") // DRAF
                        {
                            jsonBody = ReadData(dbfFile, dbfFile3, "M");
                            File.Create(flagFileName1).Close();
                            result = _viettelV2_ws.POST_DRAFT(jsonBody, out v6return);
                        }
                        else if (string.IsNullOrEmpty(_SERIAL_CERT))
                        {
                            jsonBody = ReadData(dbfFile, dbfFile3, "M");
                            if(mode == "MG") WriteFlag(flagFileName5, "" + new_uid);
                            File.Create(flagFileName1).Close();
                            result = _viettelV2_ws.POST_CREATE_INVOICE(jsonBody, _version == "V45I", out v6return);
                        }
                        else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                        {
                            generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                            {
                                Field = "certificateSerial",
                                Value = _SERIAL_CERT,
                            };
                            jsonBody = ReadData(dbfFile, dbfFile3, "M");
                            if(mode == "MG") WriteFlag(flagFileName5, "" + new_uid);
                            string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                            result = _viettelV2_ws.CreateInvoiceUsbTokenGetHash_Sign(jsonBody, templateCode, _SERIAL_CERT, out v6return);
                        }

                        // Nếu error null mà ko có so_hd thì chạy hàm lấy thông tin.
                        if (string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE)
                            && string.IsNullOrEmpty(v6return.SO_HD))
                        {
                            Thread.Sleep(10000); // Chờ 10 giây.
                            string oldTransactionID = v6return.ID;
                            _viettelV2_ws.SearchInvoiceByTransactionUuid(_codetax, _uid, out v6return);
                            v6return.ID = oldTransactionID;
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

                        jsonBody = ReadData(dbfFile, dbfFile3, "S");
                        File.Create(flagFileName1).Close();
                        result = _viettelV2_ws.POST_EDIT(jsonBody, out v6return);
                    }
                    else if (mode.StartsWith("T") && mode.EndsWith("_JSON"))
                    {
                        // Lưu ý dữ liệu mode Replace. (T)
                        jsonBody = ReadText(arg2);
                        result = _viettelV2_ws.POST_REPLACE(jsonBody, _version == "V45I", out v6return);
                    }
                    else if (mode == "T")
                    {
                        jsonBody = ReadData(dbfFile, dbfFile3, "T");
                        File.Create(flagFileName1).Close();
                        result = _viettelV2_ws.POST_REPLACE(jsonBody, _version == "V45I", out v6return);
                    }
                    else if (mode == "GET_INVOICE" || mode == "GET_INVOICE_JSON")
                    {
                        // V6ThuePostViettelV2.exe GET_INVOICE "V6ThuePost.xml" "UID"
                        string uid = arg2;
                        MakeFlagNames(uid);
                        File.Create(flagFileName1).Close();
                        result = _viettelV2_ws.SearchInvoiceByTransactionUuid(_codetax, uid, out v6return);


                    }
                    else if (mode.StartsWith("G")) // call exe như mode M
                    {
                        //MakeFlagNames(arg2);
                        jsonBody = ReadData(dbfFile, dbfFile3, "M");
                        if (mode == "G1" || mode == "G") // Gạch nợ theo fkey
                        {
                            File.Create(flagFileName1).Close();
                            string invoiceNo = ""  + postObject.generalInvoiceInfo["invoiceSeries"] +  row0["SO_CT"].ToString().Trim();
                            string strIssueDate = V6JsonConverter.ObjectToJson(postObject.generalInvoiceInfo["invoiceIssuedDate"], null);
                            string templateCode = postObject.generalInvoiceInfo["templateCode"].ToString();
                            string buyerEmailAddress = postObject.buyerInfo["buyerEmail"].ToString();
                            string paymentType = postObject.generalInvoiceInfo["paymentType"].ToString();
                            string paymentTypeName = postObject.generalInvoiceInfo["paymentTypeName"].ToString();
                            result = _viettelV2_ws.UpdatePaymentStatus(_codetax, invoiceNo, strIssueDate, templateCode,
                                buyerEmailAddress, paymentType, paymentTypeName, "true", out v6return);
                        }
                        //else if (mode == "G2") // Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
                        //{
                        //    File.Create(flagFileName1).Close();
                        //    result = confirmPayment(arg2);
                        //}
                        else if (mode == "G3") // Hủy gạch nợ theo fkey
                        {
                            File.Create(flagFileName1).Close();
                            string invoiceNo = ""  + postObject.generalInvoiceInfo["invoiceSeries"] +  row0["SO_CT"].ToString().Trim();
                            string strIssueDate = V6JsonConverter.ObjectToJson(postObject.generalInvoiceInfo["invoiceIssuedDate"], null);
                            string templateCode = postObject.generalInvoiceInfo["templateCode"].ToString();
                            string buyerEmailAddress = postObject.buyerInfo["buyerEmail"].ToString();
                            string paymentType = postObject.generalInvoiceInfo["paymentType"].ToString();
                            string paymentTypeName = postObject.generalInvoiceInfo["paymentTypeName"].ToString();
                            result = _viettelV2_ws.UpdatePaymentStatus(_codetax, invoiceNo, strIssueDate, templateCode,
                                buyerEmailAddress, paymentType, paymentTypeName, "false", out v6return);
                        }

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
                        result = _viettelV2_ws.CancelTransactionInvoice(_codetax, soseri_soct, strIssueDate, stt_rec, strIssueDate, out v6return);
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
                            string info = _viettelV2_ws.SearchInvoiceByTransactionUuid(_codetax, uid, out v6return);
                            SearchInvoiceResponseV2 infoObj =  v6return.RESULT_OBJECT as SearchInvoiceResponseV2;
                            if (infoObj != null)
                            {
                                string strIssueDate = infoObj.result[0].issueDate;
                                try
                                {
                                    result = _viettelV2_ws.DownloadInvoicePDFexchange(_codetax, soseri_soct, strIssueDate, V6SoftLocalAppData_Directory, out v6return);
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
                                result = _viettelV2_ws.DownloadInvoicePDF(_codetax, soseri_soct, templateCode, uid, V6SoftLocalAppData_Directory, out v6return);
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
                        string message = "";
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
                    BaseMessage.Show(result, 500);
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
        /// <returns></returns>
        public static string ReadData(string dbfFile, string dbfFile3, string mode)
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                postObject = new PostObjectViettelV2();
                //ReadXmlInfo(xmlFile);
                DataTable dataDbf =  ParseDBF.ReadDBF(dbfFile);
                DataTable data = Data_Table.FromTCVNtoUnicode(dataDbf);
                DataTable table3 = null;
                if (_useTaxBreakdowns && !string.IsNullOrEmpty(dbfFile3))
                {
                    DataTable dataDbf3 = ParseDBF.ReadDBF(dbfFile3);
                    table3 = Data_Table.FromTCVNtoUnicode(dataDbf3);
                }
                
                //Fill data to postObject
                row0 = data.Rows[0];

                fkeyA = fkey0 + row0["STT_REC"];
                
                MakeFlagNames(fkeyA);
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }

                // Giữ lại giá trị ngày ct viettel invoiceIssuedDate
                if (_dateType == "VIETTELNOW")
                {
                    var sv_date_10 = DateTime.Now.AddMinutes(-10);
                    sv_date_10 = sv_date_10.AddSeconds(-sv_date_10.Second);
                    sv_date_10 = sv_date_10.AddMilliseconds(-sv_date_10.Millisecond);
                    postObject.generalInvoiceInfo["invoiceIssuedDate"] = sv_date_10;
                }

                //ngay_ct_viettel = V6JsonConverter.ObjectToJson(postObject.generalInvoiceInfo["invoiceIssuedDate"], _dateType);


                if (mode == "S")
                {
                    postObject.generalInvoiceInfo["adjustmentType"] = "3";
                }

                if (mode == "T")
                {
                    //Lập hóa đơn thay thế:
                    //adjustmentType = ‘3’
                    postObject.generalInvoiceInfo["adjustmentType"] = "3";
                    //Các trường dữ liệu về hóa đơn gốc là bắt buộc
                    //originalInvoiceId
                    postObject.generalInvoiceInfo["originalInvoiceId"] = row0["FKEY_TT_OLD"].ToString().Trim();  // [AA/17E0003470]
                    //originalInvoiceIssueDate
                    postObject.generalInvoiceInfo["originalInvoiceIssueDate"] = row0["NGAY_CT_OLD"];

                    //Thông tin về biên bản đính kèm hóa đơn gốc:
                    //additionalReferenceDate
                    postObject.generalInvoiceInfo["additionalReferenceDate"] = row0["NGAY_CT_OLD"];
                    //additionalReferenceDesc
                    postObject.generalInvoiceInfo["additionalReferenceDesc"] = row0["GHI_CHU03"];
                }

                if (_TEST_)
                {
                    Guid new_uid = Guid.NewGuid();
                    postObject.generalInvoiceInfo["transactionUuid"] = "" + new_uid;
                }

                _uid = "" + postObject.generalInvoiceInfo["transactionUuid"];

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
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
                            metadata["dateValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToLower() == "number")
                        {
                            metadata["numberValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToUpper() == "N2C0VNDE")
                        {
                            // N2C0VNDE thêm đọc số tiền nếu ma_nt != VND
                            string ma_nt = row0["MA_NT"].ToString().Trim().ToUpper();
                            if (ma_nt != "VND")
                            {
                                metadata["stringValue"] = ObjectAndString.ObjectToString(GetValue(row0, metaItem.Value));
                                metadata["valueType"] = "text";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            
                            metadata["stringValue"] =  ObjectAndString.ObjectToString(GetValue(row0, metaItem.Value));
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
                        postObject.metadata.Add(metadata);
                    }
                }
                //private static Dictionary<string, XmlLine> paymentsConfig = null;
                Dictionary<string, object> payment = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    payment[item.Key] = GetValue(row0, item.Value);
                }
                postObject.payments.Add(payment);//One payment only!
                //private static Dictionary<string, XmlLine> itemInfoConfig = null;
                foreach (DataRow row in data.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    postObject.itemInfo.Add(rowData);
                }
                
                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    postObject.summarizeInfo[item.Key] = GetValue(row0, item.Value);
                }

                postObject.taxBreakdowns = new List<Dictionary<string, object>>();

                if (_useTaxBreakdowns && table3 != null)
                {
                    foreach (DataRow row3 in table3.Rows)
                    {
                        Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                        {
                            taxBreakdown[item.Key] = GetValue(row3, item.Value);
                        }
                        postObject.taxBreakdowns.Add(taxBreakdown);//Many rows
                    }
                }
                else
                {
                    Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                    {
                        taxBreakdown[item.Key] = GetValue(row0, item.Value);
                    }
                    postObject.taxBreakdowns.Add(taxBreakdown);//One only!
                }
                
                

                //if (taxBreakdownsConfig != null && ad3_table != null && ad3_table.Rows.Count > 0)
                //{
                //    foreach (DataRow ad3_row in ad3_table.Rows)
                //    {
                //        Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                //        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                //        {
                //            taxBreakdown[item.Key] = GetValue(ad3_row, item.Value);
                //        }
                //        postObject.taxBreakdowns.Add(taxBreakdown);
                //    }
                //}

                result = postObject.ToJson(_dateType);
                if (_write_log)
                {
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            //catch (Exception ex)
            {
                //
            }
            return result;
        }

        private static object GetValue(DataRow row, ConfigLine config)
        {
            object fieldValue = config.Value;
            DataTable table = row.Table;
            //if (string.IsNullOrEmpty(config.Type))
            //{
            //    return fieldValue;
            //}

            string configTYPE = null, configDATATYPE = null;
            if (!string.IsNullOrEmpty(config.Type))
            {
                string[] ss = config.Type.Split(':');
                configTYPE = ss[0].ToUpper();
                if (ss.Length > 1) configDATATYPE = ss[1].ToUpper();
            }
            if (string.IsNullOrEmpty(configDATATYPE))
            {
                configDATATYPE = (config.DataType ?? "").ToUpper();
            }

            if (configTYPE == "ENCRYPT")
            {
                return UtilityHelper.DeCrypt(fieldValue.ToString());
            }

            if (configTYPE == "FIELD" && !string.IsNullOrEmpty(config.FieldV6))
            {
                // FieldV6 sẽ có dạng thông thường là (Field) hoặc dạng ghép là (Field1 + Field2) hoặc (Field1 + "abc" + field2)
                if (table.Columns.Contains(config.FieldV6))
                {
                    fieldValue = row[config.FieldV6];
                    if (table.Columns[config.FieldV6].DataType == typeof(string))
                    {
                        //Trim
                        fieldValue = fieldValue.ToString().Trim();
                    }
                }
                else
                {
                    decimal giatribt;
                    if (Number.GiaTriBieuThucTry(config.FieldV6, row.ToDataDictionary(), out giatribt))
                    {
                        fieldValue = giatribt;
                    }
                    else
                    {
                        var fields = ObjectAndString.SplitStringBy(config.FieldV6.Replace("\\+", "~plus~"), '+');

                        string fieldValueString = "";

                        foreach (string s in fields)
                        {
                            string field = s.Trim();
                            if (table.Columns.Contains(field))
                            {
                                fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                            }
                            else
                            {
                                //if (field.StartsWith("\"") && field.EndsWith("\""))
                                //{
                                //    field = field.Substring(1, field.Length - 2);
                                //}
                                fieldValueString += field;
                            }
                        }
                        // Chốt.
                        fieldValue = fieldValueString.Replace("~plus~", "+");
                    }// end else giatribieuthuc
                }
                //// FieldV6 sẽ có dạng thông thường là (Field) hoặc dạng ghép là (Field1 + Field2) hoặc (Field1 + "abc" + field2)
                //if (table.Columns.Contains(config.FieldV6))
                //{
                //    fieldValue = row[config.FieldV6];
                //    if (table.Columns[config.FieldV6].DataType == typeof(string))
                //    {
                //        //Trim
                //        fieldValue = fieldValue.ToString().Trim();
                //    }
                //}
                //else
                //{
                //    var fields = ObjectAndString.SplitStringBy(config.FieldV6.Replace("\\+", "~plus~"), '+');
                //    fieldValue = null;
                //    string fieldValueString = null;
                //    decimal fieldValueNumber = 0m;
                //    bool still_number = true;
                //    foreach (string s in fields)
                //    {
                //        string field = s.Trim();
                //        if (table.Columns.Contains(field))
                //        {
                //            fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                //            if (still_number && ObjectAndString.IsNumberType(table.Columns[field].DataType))
                //            {
                //                fieldValueNumber += ObjectAndString.ObjectToDecimal(row[field]);
                //            }
                //            else
                //            {
                //                still_number = false;
                //            }
                //        }
                //        else
                //        {
                //            if (still_number)
                //            {
                //                fieldValueString += field;
                //                decimal tempNumber;
                //                if (Decimal.TryParse(field, out tempNumber))
                //                {
                //                    fieldValueNumber += tempNumber;
                //                }
                //                else
                //                {
                //                    still_number = false;
                //                }
                //            }
                //            else
                //            {
                //                if (field.StartsWith("\"") && field.EndsWith("\""))
                //                {
                //                    field = field.Substring(1, field.Length - 2);
                //                }
                //                fieldValueString += field;
                //            }
                //        }
                //    }
                //    // Chốt.
                //    if (still_number) fieldValue = fieldValueNumber;
                //    else fieldValue = fieldValueString.Replace("~plus~", "+");
                //}
            }

            if (!string.IsNullOrEmpty(configDATATYPE))
            {
                switch (configDATATYPE)
                {
                    case "BOOL":
                        if (fieldValue is bool)
                        {
                            return fieldValue;
                        }
                        else
                        {
                            return fieldValue != null &&
                                (fieldValue.ToString() == "1" ||
                                    fieldValue.ToString().ToLower() == "true" ||
                                    fieldValue.ToString().ToLower() == "yes");
                        }
                    case "DATE":
                    case "DATETIME":
                        return ObjectAndString.ObjectToDate(fieldValue, config.Format);
                        break;
                    case "N2C":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                    case "N2CE":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", "VND");
                    case "N2CMANT":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", row["MA_NT"].ToString().Trim());
                    case "N2CMANTE":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim());
                    case "N2C0VNDE": 
                    {
                        string ma_nt = row["MA_NT"].ToString().Trim().ToUpper();
                        if (ma_nt != "VND")
                        {
                            return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim());
                        }
                        else
                        {
                            return "";
                        }
                    }
                    case "DECIMAL":
                    case "MONEY":
                        return ObjectAndString.ObjectToDecimal(fieldValue);
                    case "INT":
                        return ObjectAndString.ObjectToInt(fieldValue);
                    case "INT64":
                    case "LONG":
                        return ObjectAndString.ObjectToInt64(fieldValue);
                    case "UPPER": // Chỉ dùng ở exe gọi bằng Foxpro.
                        return (fieldValue + "").ToUpper();
                    default:
                        return fieldValue;
                }
            }
            else
            {
                return fieldValue;
            }
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

        public static string MoneyToWords(decimal money, string lang, string ma_nt)
        {
            if (lang == "V")
            {
                return DocSo.DOI_SO_CHU_NEW(money, V6Alnt_begin1(ma_nt), V6Alnt_end1(ma_nt), V6Alnt_only1(ma_nt), V6Alnt_point1(ma_nt),
                    V6Alnt_endpoint1(ma_nt));
            }
            else
            {
                return DocSo.NumWordsWrapper(money, V6Alnt_begin2(ma_nt), V6Alnt_end2(ma_nt), V6Alnt_only2(ma_nt), V6Alnt_point2(ma_nt),
                    V6Alnt_endpoint2(ma_nt));
            }
        }

        #region ==== ALNT ====
        private static string V6Alnt_endpoint1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "xu";
                case "EUR":
                    return "";
                case "USD":
                    return "cent";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_point1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "phẩy";
                case "EUR":
                    return "";
                case "USD":
                    return "phẩy";
                case "VND":
                    return "phẩy";
                default:
                    return "";
            }
        }

        private static string V6Alnt_only1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "chẵn";
                default:
                    return "";
            }
        }

        private static string V6Alnt_end1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "đô la Úc";
                case "EUR":
                    return "Euro";
                case "USD":
                    return "đô la Mỹ";
                case "VND":
                    return "đồng";
                default:
                    return "";
            }
        }

        private static string V6Alnt_begin1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_endpoint2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "Cent(s)";
                case "EUR":
                    return "";
                case "USD":
                    return "Cent(s)";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_point2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "and";
                case "EUR":
                    return "";
                case "USD":
                    return "and";
                case "VND":
                    return "point";
                default:
                    return "";
            }
        }

        private static string V6Alnt_only2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "only";
                case "EUR":
                    return "";
                case "USD":
                    return "only";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_end2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "AUD Dollars";
                case "EUR":
                    return "";
                case "USD":
                    return "Dollars";
                case "VND":
                    return "VND";
                default:
                    return "";
            }
        }

        private static string V6Alnt_begin2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }
        #endregion ==== ALNT ====

        //private static Dictionary<string, string> sellerInfo;
        private static PostObjectViettelV2 postObject;
        public static DataRow row0;

        internal static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        internal static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        internal static Dictionary<string, ConfigLine> sellerInfoConfig = null;
        internal static Dictionary<string, ConfigLine> metadataConfig = null;
        internal static Dictionary<string, ConfigLine> paymentsConfig = null;
        internal static Dictionary<string, ConfigLine> itemInfoConfig = null;
        internal static Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        internal static Dictionary<string, ConfigLine> taxBreakdownsConfig = null;
        public static void ReadXmlInfo(string xmlFile)
        {
            //postObject = new PostObject();

            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            //metadataConfig = new Dictionary<string, ConfigLine>();
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();
            XmlTextReader reader = new XmlTextReader(xmlFile.ToLower());
            try
            {
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "V6Info":
                            {
                                ConfigLine line = ReadXmlLine(reader);
                                string line_field = line.Field.ToLower();
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
                                    case "useTaxBreakdowns":
                                        _useTaxBreakdowns = line.Value == "1";
                                        break;
                                    case "datetype":
                                        _dateType = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                        break;
                                    case "writelog":
                                        _write_log = ObjectAndString.ObjectToBool(line.Value);
                                        break;
                                }
                                break;
                            }
                        case "GeneralInvoiceInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    generalInvoiceInfoConfig.Add(key, ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "BuyerInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    buyerInfoConfig.Add(key, ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "SellerInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    sellerInfoConfig.Add(key, ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "MetaData":
                            {
                                if (metadataConfig == null) metadataConfig = new Dictionary<string, ConfigLine>();
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    metadataConfig[key] = ReadXmlLine(reader);
                                }
                                break;
                            }
                        case "Payments":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    paymentsConfig.Add(key, ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "ItemInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    itemInfoConfig.Add(key, ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "SummarizeInfo":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    summarizeInfoConfig.Add(key, ReadXmlLine(reader));
                                }
                                break;
                            }
                        case "TaxBreakdowns":
                            {
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    taxBreakdownsConfig.Add(key, ReadXmlLine(reader));
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                reader.Close();

            }
            catch (Exception)
            {
                reader.Close();
                throw;
            }
        }

        private static ConfigLine ReadXmlLine(XmlTextReader reader)
        {
            ConfigLine config = new ConfigLine();
            config.Field = reader.GetAttribute("Field");
            config.Value = reader.GetAttribute("Value");
            config.FieldV6 = reader.GetAttribute("FieldV6");
            config.Type = reader.GetAttribute("Type");
            config.DataType = reader.GetAttribute("DataType");
            config.Format = reader.GetAttribute("Format");
            
            config.MA_TD2 = reader.GetAttribute("MA_TD2");
            config.MA_TD3 = reader.GetAttribute("MA_TD3");
            config.SL_TD1 = ObjectAndString.StringToDecimal(reader.GetAttribute("SL_TD1"));
            config.SL_TD2 = ObjectAndString.StringToDecimal(reader.GetAttribute("SL_TD2"));
            config.SL_TD3 = ObjectAndString.StringToDecimal(reader.GetAttribute("SL_TD3"));
            return config;
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

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows.Forms;
//using V6ThuePostViettelApi;

//namespace V6ThuePost
//{
//    static class Program
//    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main(string[] args)
//        {
//            if (args != null && args.Length > 0)
//            {
//                string username = args[0];
//                string password = args[1];
//                string mst = args[2];
//                string jsonBody = "";
//                string result = POST(username, password, mst, jsonBody);
//                //
//                return;
//            }
//            else
//            {
//                Application.EnableVisualStyles();
//                Application.SetCompatibleTextRenderingDefault(false);
//                Application.Run(new Form1());
//            }
//        }

//        public static string POST(string username, string password, string mst, string jsonBody)
//        {
//            V6Http sender = new V6Http("https://e-invoice.com.vn:8443/", username, password);
//            string requestUrl = string.Format("InvoiceAPI/InvoiceWS/createInvoice/{0}", mst);
//            string result = sender.POST(requestUrl, jsonBody);
            
//            return result;
//        }
//    }
//}
