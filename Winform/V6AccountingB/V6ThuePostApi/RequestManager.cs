using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6ThuePostManager.Viettel;
using V6ThuePostManager.Viettel.PostObjects;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ThuePostManager
{
    /// <summary>
    /// Lớp quản lý POST GET
    /// </summary>
    public static class RequestManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="string1">1:Viettel, 2:Vnpt, 3:Bkav</param>
        /// <returns></returns>
        public static string PowerPost(DataSet ds, string string1, out string result, out string sohoadon, out string id)
        {
            string result0 = "";
            try
            {

            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("RequestManager.PowerPost", ex);
            }
            result = result0;
            sohoadon = "";
            id = "";
            return result0;
        }

        public static string ReadData_Viettel(DataSet ds)
        {
            string result = "";
            try
            {
                var postObject = new PostObject();
                DataTable map_table = ds.Tables[0];
                DataTable ad_table = ds.Tables[1];
                DataTable am_table = ds.Tables[2];
                DataRow row0 = am_table.Rows[0];
                DataTable ad2_table = ds.Tables[3];

                ReadConfigInfo(map_table);
                //DataTable dataDbf = ParseDBF.ReadDBF(dbfFile);
                //DataTable data = V6Tools.V6Convert.Data_Table.FromTCVNtoUnicode(dataDbf);
                //Fill data to postObject

                //private Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private Dictionary<string, XmlLine> paymentsConfig = null;
                Dictionary<string, object> payment = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    payment[item.Key] = GetValue(row0, item.Value);
                }
                postObject.payments.Add(payment);//One payment only!

                //itemInfo
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["LOAI"].ToString() != "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    postObject.itemInfo.Add(rowData);
                }

                //private Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    postObject.summarizeInfo[item.Key] = GetValue(row0, item.Value);
                }

                //taxBreakdowns 
                foreach (DataRow row in ad2_table.Rows)
                {
                    Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                    {
                        taxBreakdown[item.Key] = GetValue(row, item.Value);
                    }
                    postObject.taxBreakdowns.Add(taxBreakdown);
                }

                result = postObject.ToJson();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("RequestManager.ReadData", ex);
            }
            return result;
        }

        private static object GetValue(DataRow row, ConfigLine config)
        {
            object fieldValue = config.Value;
            //if (string.IsNullOrEmpty(config.Type))
            //{
            //    return fieldValue;
            //}

            string configFIELD = null, configDATATYPE = null;
            if (!string.IsNullOrEmpty(config.Type))
            {
                string[] ss = config.Type.Split(':');
                configFIELD = ss[0].ToUpper();
                if (ss.Length > 1) configDATATYPE = ss[1].ToUpper();
            }
            if (string.IsNullOrEmpty(configDATATYPE))
            {
                configDATATYPE = config.DataType.ToUpper();
            }

            if (configFIELD == "ENCRYPT")
            {
                return UtilityHelper.DeCrypt(fieldValue.ToString());
            }

            if (configFIELD == "FIELD"
                && !string.IsNullOrEmpty(config.FieldV6)
                && row.Table.Columns.Contains(config.FieldV6))
            {
                fieldValue = row[config.FieldV6];
                if (row.Table.Columns[config.FieldV6].DataType == typeof(string))
                {
                    //Trim
                    fieldValue = fieldValue.ToString().Trim();
                }
            }

            if (!string.IsNullOrEmpty(configDATATYPE))
            {
                if (configDATATYPE == "BOOL")
                {
                    if (fieldValue is bool)
                    {
                        return fieldValue;
                    }
                    else
                    {
                        return fieldValue.ToString() == "1" ||
                               fieldValue.ToString().ToLower() == "true" ||
                               fieldValue.ToString().ToLower() == "yes";
                    }
                }
                else if (configDATATYPE == "N2C") // Đọc số tiền thành chữ.
                {
                    return V6BusinessHelper.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                }
                else
                {
                    //Chưa xử lý
                    return fieldValue;
                }
            }
            else
            {
                return fieldValue;
            }
        }
        #region ==== POST GET ====

        public static string _username = "";
        public static string _password = "";
        private static string baseUrl = "", methodUrl = "", mst = "";
        private static readonly RequestSender0 requestManager = new RequestSender0();
        public static HttpWebResponse Response = null;

        /// <summary>
        /// Đổi thông tin bảo mật.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void SetLogin(string username, string password)
        {
            _username = username;
            _password = password;
        }

        
        public static string POST(string uri, string request)
        {
            Response = requestManager.SendPOSTRequest(uri, request, _username, _password, true);
            return requestManager.GetResponseContent(Response);
        }
        public static string GET(string uri)
        {
            Response = requestManager.SendGETRequest(uri, _username, _password, true);
            return requestManager.GetResponseContent(Response);
        }
        #endregion post get



        //Đang chuẩn bị chuyển toàn bộ code POST_THUE xuống API
        private static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        private static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        private static Dictionary<string, ConfigLine> sellerInfoConfig = null;
        private static Dictionary<string, ConfigLine> paymentsConfig = null;
        private static Dictionary<string, ConfigLine> itemInfoConfig = null;
        private static Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        private static Dictionary<string, ConfigLine> taxBreakdownsConfig = null;
        
        /// <summary>
        /// Cần viết thêm đọc xml.
        /// </summary>
        /// <param name="map_table"></param>
        public static void ReadConfigInfo(DataTable map_table)
        {
            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();

            try
            {
                foreach (DataRow row in map_table.Rows)
                {
                    string GROUP_NAME = row["GroupName"].ToString().Trim().ToUpper();
                    ConfigLine line = ReadConfigLine(row);
                    switch (GROUP_NAME)
                    {
                        case "V6INFO":
                            {
                                if (line.Field.ToUpper() == "USERNAME")
                                {
                                    _username = line.Value;
                                }
                                else if (line.Field.ToUpper() == "PASSWORD")
                                {
                                    _password = UtilityHelper.DeCrypt(line.Value);
                                }
                                else if (line.Field.ToUpper() == "LINK")
                                {
                                    baseUrl = line.Value;
                                    methodUrl = "";
                                    mst = "";
                                }
                                break;
                            }
                        case "GENERALINVOICEINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    generalInvoiceInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "BUYERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    buyerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "SELLERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    sellerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "PAYMENTS":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    paymentsConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "ITEMINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    itemInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "SUMMARIZEINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    summarizeInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "TAXBREAKDOWNS":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    taxBreakdownsConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                //f9Error += ex.Message;
                //f9ErrorAll += ex.Message;
            }
        }

        private static ConfigLine ReadConfigLine(DataRow reader)
        {
            ConfigLine config = new ConfigLine();
            config.Field = reader["Field"].ToString().Trim();
            config.Value = reader["Value"].ToString().Trim();
            config.FieldV6 = reader["FieldV6"].ToString().Trim();
            config.Type = reader["Type"].ToString().Trim();
            config.DataType = reader["DataType"].ToString().Trim();
            return config;
        }
        
    }
}
