using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using BSECUS;
using V6ThuePost.ResponseObjects;
using V6ThuePostBkavApi;
using V6ThuePostBkavApi.PostObjects;
using V6ThuePostBkavApi.ResponseObjects;
using V6Tools;
using V6Tools.V6Convert;
using ParseDBF = V6Tools.ParseDBF;

namespace V6ThuePost
{
    static class Program
    {
        public static bool _TEST_ = true;
        private static DateTime _TEST_DATE_ = DateTime.Now;
        #region ==== VAR ====
        /// <summary>
        /// key đầu ngữ
        /// </summary>
        private static string fkey0 = "V6";
        /// <summary>
        /// key thêm
        /// </summary>
        private static string fkeyA;
        /// <summary>
        /// key sửa
        /// </summary>
        private static string fkeyE;

        /// <summary>
        /// Tên cờ V6STT_REC
        /// </summary>
        private static string flagName = "";
        /// <summary>
        /// Cờ bắt đầu.
        /// </summary>
        static string flagFileName1 = flagName + ".flag1";
        /// <summary>
        /// Cờ thành công
        /// </summary>
        static string flagFileName2 = flagName + ".flag2";
        /// <summary>
        /// Cờ lỗi
        /// </summary>
        static string flagFileName3 = flagName + ".flag3";
        /// <summary>
        /// Cờ lấy về số hd kết quả
        /// </summary>
        static string flagFileName4 = flagName + ".flag4";
        /// <summary>
        /// Cờ kết thúc
        /// </summary>
        static string flagFileName9 = flagName + ".flag9";

        private static RemoteCommand remoteCommand = null;
        public static Dictionary<string, string> V6Infos = new Dictionary<string, string>();
        public static string BkavPartnerGUID = "";
        public static string BkavPartnerToken = "";
        private static string baseUrl = "";

        //private static Dictionary<string, string> sellerInfo;
        private static PostObjectBkav postObject;

        private static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        private static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        private static Dictionary<string, ConfigLine> sellerInfoConfig = null;
        private static Dictionary<string, ConfigLine> paymentsConfig = null;
        private static Dictionary<string, ConfigLine> itemInfoConfig = null;
        private static Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        private static Dictionary<string, ConfigLine> taxBreakdownsConfig = null; 
        #endregion var
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                var startupPath = Application.StartupPath;
                var dir = new DirectoryInfo(startupPath);
                var dir_name = dir.Name.ToLower();
                if (dir_name == "debug")
                {
                    _TEST_ = true;
                    MessageBox.Show("Test");
                }
                else
                {
                    _TEST_ = false;
                }

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
                    ReadXmlInfo(arg1_xmlFile);
                    string jsonBody = null;
                    //string dbfFile = args[1];
                    BkavWS bkavWS = new BkavWS();
                    ExecCommandFunc wsExecCommand = null;
                    var webservice = new V6ThuePostBkavApi.vn.ehoadon.wsdemo.WSPublicEHoaDon(baseUrl);
                    wsExecCommand = webservice.ExecuteCommand;
                    uint Constants_Mode = RemoteCommand.DefaultMode;
                    remoteCommand = new RemoteCommand(wsExecCommand, BkavPartnerGUID, BkavPartnerToken, Constants_Mode);
                    V6Return v6return = null;

                    if (mode.ToUpper() == "MTEST")
                    {
                        jsonBody = "";
                        File.Create(flagFileName1).Close();
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._100_CreateNew, out v6return);
                        if (result.Contains("Dữ liệu xml đầu vào không đúng quy định"))
                        {
                            result = "OK:" + result;
                        }
                        else
                        {
                            MessageBox.Show("Test:" + result);
                        }
                    }
                    else if (mode == "M" || mode == "M100")
                    {
                        jsonBody = ReadData(arg2, mode);
                        File.Create(flagFileName1).Close();
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._100_CreateNew, out v6return);
                        if (!string.IsNullOrEmpty(v6return.SO_HD))
                        {
                            WriteFlag(flagFileName4, v6return.SO_HD + ":" + v6return.ID + ":" + v6return.SECRET_CODE);
                            if (V6Infos.ContainsKey("BKAVSIGN") &&  V6Infos["BKAVSIGN"] == "1")
                            {
                                V6Return v6return2;
                                string result2 = bkavWS.SignInvoice(remoteCommand, v6return.ID, out v6return2);
                                result += "\r\n" + result2;
                            }
                        }
                    }
                    else if (mode == "M101")
                    {
                        jsonBody = ReadData(arg2, mode);
                        File.Create(flagFileName1).Close();
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._101_CreateEmpty, out v6return);
                        if (!string.IsNullOrEmpty(v6return.SO_HD))
                        {
                            WriteFlag(flagFileName4, v6return.SO_HD + ":" + v6return.ID + ":" + v6return.SECRET_CODE);
                        }
                    }
                    else if (mode == "M200") // update 101
                    {
                        jsonBody = ReadData(arg2, mode);
                        File.Create(flagFileName1).Close();
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._200_Update, out v6return);
                        if (!string.IsNullOrEmpty(v6return.SO_HD))
                        {
                            WriteFlag(flagFileName4, v6return.SO_HD + ":" + v6return.ID + ":" + v6return.SECRET_CODE);
                        }
                    }
                    else if (mode.StartsWith("M112"))
                    {
                        jsonBody = ReadData(arg2, mode);
                        File.Create(flagFileName1).Close();
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._112_CreateWithParternSerial, out v6return);
                        if (!string.IsNullOrEmpty(v6return.SO_HD))
                        {
                            WriteFlag(flagFileName4, v6return.SO_HD + ":" + v6return.ID + ":" + v6return.SECRET_CODE);
                            if (V6Infos.ContainsKey("BKAVSIGN") &&  V6Infos["BKAVSIGN"] == "1")
                            {
                                V6Return v6return2;
                                string result2 = bkavWS.SignInvoice(remoteCommand, v6return.ID, out v6return2);
                                //"Tài khoản không sử dụng CKS BKAV-HSM [!|637368896620789548|!]. Status: 1"
                                result += "\r\n" + result2;
                            }
                        }
                    }
                    else  if (mode == "S")
                    {
                        jsonBody = ReadData(arg2, mode);
                        File.Create(flagFileName1).Close();
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._121_CreateAdjust, out v6return);
                        if (!string.IsNullOrEmpty(v6return.SO_HD))
                        {
                            WriteFlag(flagFileName4, v6return.SO_HD + ":" + v6return.ID + ":" + v6return.SECRET_CODE);
                        }
                    }
                    else if (mode == "T")
                    {
                        jsonBody = ReadData(arg2, mode);
                        File.Create(flagFileName1).Close();
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._120_CreateReplace, out v6return);
                        if (!string.IsNullOrEmpty(v6return.SO_HD))
                        {
                            WriteFlag(flagFileName4, v6return.SO_HD + ":" + v6return.ID + ":" + v6return.SECRET_CODE);
                        }
                    }
                    else if (mode == "E_G1")
                    {
                        //
                    }
                    else if (mode == "E_H1")
                    {
                        ReadData(arg2, mode);
                        File.Create(flagFileName1).Close();
                        jsonBody = fkeyA;// paras.Fkey_hd;
                        //MessageBox.Show("Test jsonBody " + jsonBody);
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._202_CancelInvoiceByPartnerInvoiceID, out v6return);
                    }
                    else if (mode == "E_T1")
                    {
                        jsonBody = ReadData(arg2, "T");
                        File.Create(flagFileName1).Close();
                        result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._123_CreateReplace, out v6return);
                    }

                    if (result.StartsWith("ERR"))
                    {
                        File.Create(flagFileName3).Close();
                    }
                    else
                    {
                        File.Create(flagFileName2).Close();
                    }
                }
                catch (Exception ex)
                {
                    File.Create(flagFileName3).Close();
                    result += "ERR:EX\r\n" + ex.Message;
                }

                File.Create(flagFileName9).Close();
                V6Message.Show(result, 500);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }

        private static void WriteFlag(string fileName, string content)
        {
            var fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Close();
            fs.Close();
        }
        

        public static string ReadData(string dbfFile, string mode)
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                postObject = new PostObjectBkav();
                
                ExecCommandFunc wsExecCommand = null;
                var webservice = new V6ThuePostBkavApi.vn.ehoadon.wsdemo.WSPublicEHoaDon(baseUrl);
                wsExecCommand = webservice.ExecuteCommand;
                //wsExecCommand = webservice.ExecCommand;

                uint Constants_Mode = RemoteCommand.DefaultMode;
                remoteCommand = new RemoteCommand(wsExecCommand, BkavPartnerGUID, BkavPartnerToken, Constants_Mode);
                DataTable dataDbf = ParseDBF.ReadDBF(dbfFile);
                DataTable data = V6Tools.V6Convert.Data_Table.FromTCVNtoUnicode(dataDbf);
                //Fill data to postObject
                DataRow row0 = data.Rows[0];
                fkeyA = fkey0 + row0["STT_REC"];
                flagName = fkeyA;
                flagFileName1 = flagName + ".flag1";
                flagFileName2 = flagName + ".flag2";
                flagFileName3 = flagName + ".flag3";
                flagFileName4 = flagName + ".flag4";
                flagFileName9 = flagName + ".flag9";
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    if (item.Key == "OriginalInvoiceIdentify")
                    {
                        if (mode == "T")
                        {
                            postObject.Invoice[item.Key] = GetValue(row0, item.Value);
                            postObject.Invoice["InvoiceCode"] = null;
                            continue;
                            //postObject.Invoice.OriginalInvoiceIdentify = "";
                            //postObject.Invoice.InvoiceNo = 0;
                            //postObject.Invoice.InvoiceForm = "";
                            //postObject.Invoice.InvoiceSerial = "";
                            string OriginalInvoiceIdentify = string.Format("[{0}]_[{1}]_[{2}]",
                                //  "[01GTKT0/003]_[AA/17E]_[0000105]";
                                postObject.Invoice["InvoiceForm"],
                                postObject.Invoice["InvoiceSerial"],
                                postObject.Invoice["InvoiceNo"]);

                            postObject.Invoice["OriginalInvoiceIdentify"] = OriginalInvoiceIdentify;
                        }
                    }
                    else
                    {
                        postObject.Invoice[item.Key] = GetValue(row0, item.Value);
                    }
                }
                
                //foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                //{
                //    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                //}
                
                //foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                //{
                //    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                //}
                
                //Dictionary<string, object> payment = new Dictionary<string, object>();
                //foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                //{
                //    payment[item.Key] = GetValue(row0, item.Value);
                //}
                //postObject.payments.Add(payment);//One payment only!
                
                foreach (DataRow row in data.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    postObject.ListInvoiceDetailsWS.Add(rowData);
                }
                
                //foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                //{
                //    postObject.summarizeInfo[item.Key] = GetValue(row0, item.Value);
                //}
                if (summarizeInfoConfig.ContainsKey("ListInvoiceAttachFileWS"))
                {
                    postObject.ListInvoiceAttachFileWS = new List<string>()
                    {
                        GetValue(row0, summarizeInfoConfig["ListInvoiceAttachFileWS"]).ToString()
                    };
                }
                if (summarizeInfoConfig.ContainsKey("PartnerInvoiceID"))
                {
                    postObject.PartnerInvoiceID =
                        ObjectAndString.ObjectToString(GetValue(row0, summarizeInfoConfig["PartnerInvoiceID"]), "ddMMyyyyHHmmss");
                    if (postObject.PartnerInvoiceID.ToString().Length < 14 && ObjectAndString.ObjectToInt(postObject.PartnerInvoiceID) != 0)
                    {
                        postObject.PartnerInvoiceID = ("00000000000000" + postObject.PartnerInvoiceID).Right("ddMMyyyyHHmmss".Length);
                    }
                }
                if (summarizeInfoConfig.ContainsKey("PartnerInvoiceStringID"))
                {
                    postObject.PartnerInvoiceStringID =
                        GetValue(row0, summarizeInfoConfig["PartnerInvoiceStringID"]).ToString();
                }
                
                //Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                //foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                //{
                //    taxBreakdown[item.Key] = GetValue(row0, item.Value);
                //}
                //postObject.taxBreakdowns.Add(taxBreakdown);//One only!

                result = postObject.ToJson();
            }
            //catch (Exception ex)
            {
                //
            }
            return "[" + result + "]";
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
                    var fields = ObjectAndString.SplitStringBy(config.FieldV6.Replace("\\+", "~plus~"), '+');
                    fieldValue = null;
                    string fieldValueString = null;
                    decimal fieldValueNumber = 0m;
                    bool still_number = true;
                    foreach (string s in fields)
                    {
                        string field = s.Trim();
                        if (table.Columns.Contains(field))
                        {
                            fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                            if (still_number && ObjectAndString.IsNumberType(table.Columns[field].DataType))
                            {
                                fieldValueNumber += ObjectAndString.ObjectToDecimal(row[field]);
                            }
                            else
                            {
                                still_number = false;
                            }
                        }
                        else
                        {
                            if (still_number)
                            {
                                if (field.StartsWith("\"") && field.EndsWith("\""))
                                {
                                    fieldValueString += field.Substring(1, field.Length - 2);
                                }
                                else
                                {
                                    fieldValueString += field;
                                }
                                decimal tempNumber;
                                if (Decimal.TryParse(field, out tempNumber))
                                {
                                    fieldValueNumber += tempNumber;
                                }
                                else
                                {
                                    still_number = false;
                                }
                            }
                            else
                            {
                                if (field.StartsWith("\"") && field.EndsWith("\""))
                                {
                                    field = field.Substring(1, field.Length - 2);
                                }
                                fieldValueString += field;
                            }
                        }
                    }
                    // Chốt.
                    if (still_number) fieldValue = fieldValueNumber;
                    else fieldValue = fieldValueString.Replace("~plus~", "+");
                }
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
                    case "N2CMANT":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", row["MA_NT"].ToString().Trim());
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

        public static string POST0(string jsonBody, int comandType)
        {
            string result = null;
            try
            {
                string sGUID = null;
                string msg = "";
                switch (comandType)
                {
                    case BkavConst._100_CreateNew:
                    case BkavConst._112_CreateWithParternSerial:
                    case BkavConst._101_CreateEmpty:
                    case BkavConst._200_Update:
                        msg = DoCreateInvoice(jsonBody, comandType, out sGUID);
                        break;
                    case BkavConst._121_CreateAdjust:
                        msg = DoAdjustInvoice(jsonBody, out sGUID);
                        break;
                    case BkavConst._120_CreateReplace:
                        msg = DoReplaceInvoice(jsonBody, out sGUID);
                        break;
                    default:
                        msg = "V6 not supported.";
                        break;
                }
                
                if (msg.Length > 0)
                {
                    result = "ERR: " + msg;
                }
                else
                {
                    result += "\r\nGUID: " + sGUID;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            Logger.WriteToLog("POST " + result);
            return result;
        }
        
        


        static string DoCreateInvoice(string listInvoice_json, int commandType, out string sGUID)
        {
            string msg = "";
            sGUID = null;

            Result result = null;
            
            msg = remoteCommand.TransferCommandAndProcessResult(commandType, listInvoice_json, out result);
            if (msg.Length > 0) return msg;

            // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0) return msg;

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                if (invoiceResult.Status == 0) sGUID = sGUID + "; " + invoiceResult.InvoiceGUID;
                else msg = msg + "; " + invoiceResult.MessLog;
            }

            return msg;
        }
        
        static string DoAdjustInvoice(string listInvoice_json, out string sGUID)
        {
            string msg = "";
            sGUID = null;

            Result result = null;
            
            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._121_CreateAdjust, listInvoice_json, out result);
            if (msg.Length > 0) return msg;

            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0) return msg;

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                if (invoiceResult.Status == 0) sGUID = sGUID + "; " + invoiceResult.InvoiceGUID;
                else msg = msg + "; " + invoiceResult.MessLog;
            }

            return msg;
        }
        
        static string DoReplaceInvoice(string listInvoice_json, out string sGUID)
        {
            string msg = "";
            sGUID = null;

            Result result = null;
            
            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._120_CreateReplace, listInvoice_json, out result);
            if (msg.Length > 0) return msg;

            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0) return msg;

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                if (invoiceResult.Status == 0) sGUID = sGUID + "; " + invoiceResult.InvoiceGUID;
                else msg = msg + "; " + invoiceResult.MessLog;
            }

            return msg;
        }

        public static void ReadXmlInfo(string xmlFile)
        {
            //postObject = new PostObject();
            
            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();
            XmlTextReader reader = new XmlTextReader(xmlFile.ToLower());
            try
            {
                while (reader.Read())
                {
                    ConfigLine line = ReadXmlLine(reader);

                    switch (reader.Name)
                    {
                        case "V6Info":
                        {
                            V6Infos[line.Field.ToUpper()] = line.Value;
                            switch (line.Field)
                            {
                                case "BkavPartnerGUID":
                                    BkavPartnerGUID = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "BkavPartnerToken":
                                    BkavPartnerToken = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "baselink":
                                    baseUrl = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "v6fkey":
                                    fkey0 = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
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
            config.FieldV6 = reader.GetAttribute("FieldV6");
            config.Format = reader.GetAttribute("Format");
            config.Value = reader.GetAttribute("Value");
            
            config.Type = reader.GetAttribute("Type");
            config.DataType = reader.GetAttribute("DataType");
            return config;
        }

    }
}
