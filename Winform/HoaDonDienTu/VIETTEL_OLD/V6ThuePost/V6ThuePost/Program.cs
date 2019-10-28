using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using V6ThuePostViettelApi;
using V6Tools;
using V6Tools.V6Convert;
using Newtonsoft.Json;
using V6ThuePost.ViettelObjects;

namespace V6ThuePost
{
    static class Program
    {
        #region ===== VAR =====
        public static ViettelWS _viettel_ws = null;
        /// <summary>
        /// Link host
        /// </summary>
        public static string baseUrl = "";
        /// <summary>
        /// Link hàm tạo mới (không chứa baseUrl, khi dùng sẽ nối lại sau).
        /// </summary>
        public static string createInvoiceUrl = "";
        /// <summary>
        /// Link hàm sửa đổi (không chứa baseUrl, khi dùng sẽ nối lại sau).
        /// </summary>
        public static string modifyUrl = "";
        //{
        //"supplierTaxCode":"0100109106",
        //"invoiceNo":"AA/17E0000166",
        //"pattern":"01GTKT0/151",
        //"transactionUuid":"testuuid9999999",
        //"fileType":"ZIP"
        //}
        public static string getInvoiceRepresentationFileUrl = "InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile";

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

                    ReadXmlInfo(arg1_xmlFile);
                    string dbfFile = arg2;

                    _viettel_ws = new ViettelWS(baseUrl, username, password);

                    if (mode.ToUpper() == "MTEST")
                    {
                        ReadData(arg2, "M"); // đọc để lấy tên flag.
                        jsonBody = "";
                        File.Create(flagFileName1).Close();
                        result = POST_NEW(jsonBody);
                        if (result.Contains("\"errorCode\":\"TEMPLATE_NOT_FOUND\""))
                        {
                            result = "Kết nối ổn. " + result;
                            File.Create(flagFileName2).Close();
                            goto End;
                        }
                    }
                    else if (mode.StartsWith("M"))
                    {
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

                            var fs = new FileStream(flagFileName5, FileMode.Create);
                            StreamWriter sw = new StreamWriter(fs);
                            sw.Write("" + new_uid);
                            sw.Close();
                            fs.Close();
                        }
                        jsonBody = ReadData(dbfFile, "M");
                        File.Create(flagFileName1).Close();
                        result = POST_NEW(jsonBody);
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

                        jsonBody = ReadData(dbfFile, "S");
                        File.Create(flagFileName1).Close();
                        result = POST_EDIT(jsonBody);
                    }
                    else if (mode == "T")
                    {
                        jsonBody = ReadData(dbfFile, "T");
                        File.Create(flagFileName1).Close();
                        result = POST_NEW(jsonBody);
                    }

                    //Phân tích result
                    string message = "";
                    try
                    {
                        CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);// MyJson.ConvertJson<CreateInvoiceResponse>(result);
                        if (!string.IsNullOrEmpty(responseObject.description))
                        {
                            message += " " + responseObject.description;
                        }

                        if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.invoiceNo))
                        {
                            message += " " + responseObject.result.invoiceNo;
                            WriteFlag(flagFileName4, responseObject.result.invoiceNo);
                            File.Create(flagFileName2).Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteToLog("Program.Main ConverResultObjectException: " + ex.Message);
                        message = "Kết quả:";
                    }
                    result = message + "\n" + result;

                }
                catch (Exception ex)
                {
                    File.Create(flagFileName3).Close();
                    //MessageBox.Show(ex.Message);
                    BaseMessage.Show(ex.Message, 500);
                }
            End:
                File.Create(flagFileName9).Close();
                BaseMessage.Show(result, 500);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
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
        public static string ReadData(string dbfFile, string mode)
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                postObject = new PostObjectViettel();
                //ReadXmlInfo(xmlFile);
                DataTable dataDbf =  ParseDBF.ReadDBF(dbfFile);
                DataTable data = Data_Table.FromTCVNtoUnicode(dataDbf);
                //Fill data to postObject
                DataRow row0 = data.Rows[0];

                fkeyA = fkey0 + row0["STT_REC"];
                MakeFlagNames(fkeyA);
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
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

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
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
                //private static Dictionary<string, XmlLine> taxBreakdownsConfig = null; 
                Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    taxBreakdown[item.Key] = GetValue(row0, item.Value);
                }
                postObject.taxBreakdowns.Add(taxBreakdown);//One only!

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

                result = postObject.ToJson();
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
                    var fields = ObjectAndString.SplitStringBy(config.FieldV6, '+');
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
                                fieldValueString += field;
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
                    else fieldValue = fieldValueString;
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

        public static string POST_NEW(string jsonBody)
        {
            string result;
            try
            {
                result = _viettel_ws.POST(createInvoiceUrl, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("Program.POST_NEW " + result);
            return result;
        }

        public static string POST_EDIT(string jsonBody)
        {
            string result;
            try
            {
                result = _viettel_ws.POST(modifyUrl, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("Program.POST_EDIT " + result);
            return result;
        }

        //private static Dictionary<string, string> sellerInfo;
        private static PostObjectViettel postObject;

        internal static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        internal static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        internal static Dictionary<string, ConfigLine> sellerInfoConfig = null;
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
                                    case "baselink":
                                        baseUrl = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "createlink":
                                        createInvoiceUrl = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "modifylink":
                                        modifyUrl = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
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
            config.Value = reader.GetAttribute("Value");
            config.FieldV6 = reader.GetAttribute("FieldV6");
            config.Type = reader.GetAttribute("Type");
            config.DataType = reader.GetAttribute("DataType");
            config.Format = reader.GetAttribute("Format");
            return config;
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
