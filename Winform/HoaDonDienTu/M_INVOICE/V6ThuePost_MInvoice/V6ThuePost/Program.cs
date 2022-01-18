using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using V6Tools;
using V6Tools.V6Convert;
using Spy;
using Spy.SpyObjects;
using V6ThuePost.MInvoiceObject.Request;
using V6ThuePost.MInvoiceObject.Response;
using V6ThuePost.ResponseObjects;
using V6ThuePostMInvoiceApi;
using System.Globalization;

namespace V6ThuePost
{
    static class Program
    {
        public static bool _TEST_ = true;
        private static DateTime _TEST_DATE_ = DateTime.Now;
        #region ===== VAR =====
        public static MInvoiceWS _WS = null;
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
        public static string _ma_dvcs = "";
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
        /// <summary>
        /// key trong data
        /// </summary>
        public static string fkeyA;
        public static string _seri_test = "";
        #endregion var

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            var startupPath = Application.StartupPath;
            var dir = new DirectoryInfo(startupPath);
            var dir_name = dir.Name.ToLower();
            if (dir_name == "debug")
            {
                _TEST_ = true;
                MessageBox.Show("Test. Ngày tự động. Seri lấy ở xml seri_test.");
            }
            else
            {
                _TEST_ = false;
            }

            if (args != null && args.Length > 0)
            {
                string result_message = "";
                V6Return v6Return = null;
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
                    MInvoicePostObject jsonBodyObject = null;
                    MInvoiceResponse responseObject = null;

                    ReadXmlInfo(arg1_xmlFile);
                    string dbfFile = arg2;

                    _WS = new MInvoiceWS(baseUrl, username, password, _ma_dvcs, _codetax, _version);

                    if (mode.ToUpper() == "MTEST")
                    {
                        jsonBodyObject = ReadData_Minvoice(arg2, "M"); // đọc để lấy tên flag.
                        
                        File.Create(flagFileName1).Close();
                        responseObject = _WS.POST_NEW(new MInvoicePostObject(), out v6Return);
                        if (v6Return.RESULT_STRING.Contains("\"errorCode\":\"TEMPLATE_NOT_FOUND\""))
                        {
                            result_message = "Kết nối ổn. " + result_message;
                            File.Create(flagFileName2).Close();
                            goto End;
                        }
                    }
                    else if (mode.StartsWith("M"))
                    {
                        if (string.IsNullOrEmpty(_SERIAL_CERT))
                        {
                            jsonBodyObject = ReadData_Minvoice(dbfFile, "M");
                            File.Create(flagFileName1).Close();
                            responseObject = _WS.POST_NEW(jsonBodyObject, out v6Return);
                        }
                        else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                        {
                            StartAutoInputTokenPassword();
                            generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                            {
                                Field = "certificateSerial",
                                Value = _SERIAL_CERT,
                            };
                            jsonBodyObject = ReadData_Minvoice(dbfFile, "M");
                            responseObject = _WS.POST_NEW_TOKEN(jsonBodyObject, out v6Return);                            
                        }

                        if (string.IsNullOrEmpty(v6Return.RESULT_ERROR_MESSAGE))
                        {
                            result_message = "Số HD:" + v6Return.SO_HD + "  ID:" + v6Return.ID + "  CODE:" +
                                             v6Return.SECRET_CODE + "\n" + v6Return.RESULT_STRING;
                            if (!string.IsNullOrEmpty(v6Return.SO_HD))
                            {
                                string flag4_content = v6Return.SO_HD + ":" + v6Return.ID;
                                if (!string.IsNullOrEmpty(v6Return.SECRET_CODE))
                                    flag4_content += ":" + v6Return.SECRET_CODE;
                                WriteFlag(flagFileName4, flag4_content);
                                //File.Create(flagFileName2).Close(); // Ghi phía dưới, phần phân tích result
                            }
                            else
                            {
                                WriteFlag(flagFileName3, "SO_HD_EMPTY");
                            }
                        }
                        else
                        {
                            
                            //WriteFlag(flagFileName3, v6Return.RESULT_ERROR_MESSAGE); // Ghi phía dưới, phần phân tích result
                        }

                    }
                    else if (mode.StartsWith("S"))
                    {
                        jsonBodyObject = ReadData_Minvoice(dbfFile, "S");
                        File.Create(flagFileName1).Close();
                        responseObject = _WS.POST_EDIT(jsonBodyObject, out v6Return);
                    }
                    else if (mode == "T")
                    {
                        jsonBodyObject = ReadData_Minvoice(dbfFile, "T");
                        File.Create(flagFileName1).Close();
                        result_message = _WS.POST_REPLACE(jsonBodyObject);
                    }
                    else if (mode == "H")
                    {
                        string id = arg2;
                        string sovb = arg3;
                        DateTime ngay_ct = ObjectAndString.StringToDate(arg4);
                        MakeFlagNames(id);
                        File.Create(flagFileName1).Close();
                        responseObject = _WS.POST_CANCEL(id, sovb, ngay_ct, "ghi_chu", out v6Return);
                        result_message = v6Return.RESULT_STRING;
                    }

                    //Phân tích result
                    if (v6Return != null && string.IsNullOrEmpty(v6Return.RESULT_ERROR_MESSAGE))
                    {   
                        File.Create(flagFileName2).Close();
                    }
                    else if (v6Return == null)
                    {
                        result_message = "v6Return null";
                        WriteFlag(flagFileName3, "v6Return null");
                    }
                    else
                    {
                        result_message = v6Return.RESULT_ERROR_MESSAGE;
                        WriteFlag(flagFileName3, v6Return.RESULT_ERROR_MESSAGE);
                    }

                }
                catch (Exception ex)
                {
                    StopAutoInputTokenPassword();
                    File.Create(flagFileName3).Close();
                    BaseMessage.Show(ex.Message, 500);
                }
            End:
                File.Create(flagFileName9).Close();
                BaseMessage.Show(result_message, 500);                
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

            StopAutoInputTokenPassword();
            Application.Exit();
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
        /// <param name="mode">M mới hoặc S thay thế</param>
        /// <returns></returns>
        public static MInvoicePostObject ReadData_Minvoice(string dbfFile, string mode)
        {
            string result = "";
            //try
            {
                postObject = new MInvoicePostObject();
                postObject.windowid = "WIN00187";
                if(mode.StartsWith("M")) postObject.editmode = "1";
                if(mode.StartsWith("S")) postObject.editmode = "2";
                postObject.data = new List<InvoiceData>();
                InvoiceData invoiceData = new InvoiceData();
                postObject.data.Add(invoiceData);
                
                DataTable dataDbf =  ParseDBF.ReadDBF(dbfFile);
                DataTable data = Data_Table.FromTCVNtoUnicode(dataDbf);
                //Fill data to postObject
                DataRow row0 = data.Rows[0];

                fkeyA = fkey0 + row0["STT_REC"];
                MakeFlagNames(fkeyA);
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }

                if (mode.StartsWith("S"))
                {
                    invoiceData["inv_invoiceNumber"] = ("" + row0["SO_CT"]).Trim();
                }
                if (mode == "T")
                {
                    //Lập hóa đơn thay thế:
                    //adjustmentType = '3'
                    invoiceData["adjustmentType"] = "3";
                    //Các trường dữ liệu về hóa đơn gốc là bắt buộc
                    //originalInvoiceId
                    invoiceData["originalInvoiceId"] = row0["FKEY_TT_OLD"].ToString().Trim();  // [AA/17E0003470]
                    //originalInvoiceIssueDate
                    invoiceData["originalInvoiceIssueDate"] = row0["NGAY_CT_OLD"];

                    //Thông tin về biên bản đính kèm hóa đơn gốc:
                    //additionalReferenceDate
                    invoiceData["additionalReferenceDate"] = row0["NGAY_CT_OLD"];
                    //additionalReferenceDesc
                    invoiceData["additionalReferenceDesc"] = row0["GHI_CHU03"];
                }

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }
                
                List<DetailObject> listDetailObject = new List<DetailObject>();
                DetailObject detailObject = new DetailObject();
                listDetailObject.Add(detailObject);
                detailObject.tab_id = "TAB00188";
                invoiceData["details"] = listDetailObject;
                
                foreach (DataRow row in data.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    detailObject.data.Add(rowData);
                }
                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }
                
                result = postObject.ToJson();
            }
            //catch (Exception ex)
            {
                //
            }

            if (_TEST_)
            {
                postObject.data[0]["inv_invoiceIssuedDate"] = _TEST_DATE_.Date;
                postObject.data[0]["inv_invoiceSeries"] = _seri_test;
            }
            
            return postObject;
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

        //public static string POST_NEW(string jsonBody)
        //{
        //    string result;
        //    try
        //    {
        //        result = _WS.POST_NEW(jsonBody);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //    }
        //    Logger.WriteToLog("Program.POST_NEW " + result);
        //    return result;
        //}

        //public static string POST_EDIT(string jsonBody)
        //{
        //    string result;
        //    try
        //    {
        //        result = _WS.POST_EDIT(jsonBody);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //    }
        //    Logger.WriteToLog("Program.POST_EDIT " + result);
        //    return result;
        //}

        //private static Dictionary<string, string> sellerInfo;
        private static MInvoicePostObject postObject;

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
            metadataConfig = null;
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
                                    case "ma_dvcs":
                                        _ma_dvcs = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "codetax":
                                        _codetax = UtilityHelper.DeCrypt(line.Value);
                                        break;
                                    case "seri_test":
                                        _seri_test = line.Value;
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
                        case "MetaData":
                            {
                                if (metadataConfig == null) metadataConfig = new Dictionary<string, ConfigLine>();
                                string key = reader.GetAttribute("Field");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    metadataConfig.Add(key, ReadXmlLine(reader));
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