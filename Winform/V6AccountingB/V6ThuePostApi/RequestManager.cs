using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using V6Tools;

namespace V6ThuePostApi
{
    /// <summary>
    /// Lớp quản lý POST GET
    /// </summary>
    public static class RequestManager
    {
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
