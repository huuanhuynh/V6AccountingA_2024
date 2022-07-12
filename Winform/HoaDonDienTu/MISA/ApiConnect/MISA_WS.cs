using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using V6Tools;
using V6ThuePost.MISA_Objects.Response;
using V6ThuePost.ResponseObjects;
using V6ThuePost.MISA_Objects;
using V6SignToken;
using System.Collections.Generic;
using V6Tools.V6Convert;
using System.Xml;
using V6ThuePost_MISA_Api.Objects;

namespace V6ThuePost_MISA_Api
{
    public class MISA_WS
    {
        /// <summary>
        /// Đường dẫn API thông thường
        /// </summary>
        public static string MEAPIV3URL = "https://api.meinvoice.vn/api/v3";

        /// <summary>
        /// Đường dẫn API hóa đơn có mã
        /// </summary>
        public static string MECODEAPIV3URL = "https://api.meinvoice.vn/api/v3/code";

        /// <summary>
        /// https://testapi.meinvoice.vn
        /// </summary>
        private string _baseurl = "";
        private string _appID = "";
        /// <summary>
        /// Tên người sử dụng trên hệ thống Sinvoice (Viettel), thường là codetax
        /// </summary>
        private readonly string _username;
        private readonly string _password;
        private string __token = "";
        /// <summary>
        /// Mã số thuế của doanh nghiệp.
        /// </summary>
        private string _codetax;

        private bool _co_ma = true;
        private const string login_uri = "/api/v3/auth/token";
        /// <summary>
        /// Hàm gửi và lấy về xml chưa ký?
        /// </summary>
        private const string create_uri = "/api/v3/itg/invoicepublishing/createinvoice";//"itg/invoicepublishing", "createinvoice"
        private const string PUBLISH_uri = "/api/v3/itg/invoicepublishing";
        private const string create_guivakyhoadonhsm_uri = "/api/v3/itg/invoicepublishing/publishhsm";
        private const string sign_hsm_uri = "/api/services/hddtws/XuLyHoaDon/KyHoaDonHSM";
        private const string taipdf_uri = "/api/v3/itg/invoicepublished/downloadinvoice?downloadDataType=PDF"; // cách gọi riêng ở chương trình này: uri là đường dẫn hàm ko bao gồm baseurl.
        private const string taipdf_chuaky_uri = "/api/services/hddtws/TraCuuHoaDon/TaiHoaDonPdfChuaKy";
        private const string chuyendoihoadon_uri = "/api/services/hddtws/QuanLyHoaDon/ChuyenDoiHoaDon";
        private const string thaythehoadon_uri = "/api/services/hddtws/QuanLyHoaDon/LapHoaDonThayThe";
        private const string huyhoadon_uri = "/api/v3/itg/invoicepublished/cancel";
        
        
        /// <summary>
        /// Khởi tạo và đăng nhập WebService
        /// </summary>
        /// <param name="baseurl">https://testapi.meinvoice.vn</param>
        /// <param name="username">email</param>
        /// <param name="password">password</param>
        /// <param name="codetax">mst</param>
        /// <param name="appID">4D983E42-74C0-43B3-801C-DD1088EA8BFD</param>
        /// <param name="co_ma">Hóa đơn có mã của cơ quan thuế</param>
        public MISA_WS(string baseurl, string username, string password, string codetax, string appID, bool co_ma)
        {
            _baseurl = baseurl;
            if (_baseurl.EndsWith("/")) _baseurl = _baseurl.Substring(0, _baseurl.Length-1);
            _username = username;
            _password = password;
            _codetax = codetax;
            _appID = appID;
            _co_ma = co_ma;

            Login();
        }

        private void Login()
        {
            //InitiateSSLTrust();//bypass SSL
            //if (use_ssl)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                       | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12
                                                       | SecurityProtocolType.Ssl3;
            }
            
            Dictionary<string, object> para = new Dictionary<string,object>();
            para["AppID"] = _appID;
            para["TaxCode"] = _codetax;
            para["UserName"] = _username;
            para["Password"] = _password;
            para["DeviceID"] = null;

            //{"AppID":"4D983E42-74C0-43B3-801C-DD1088EA8BFD","TaxCode":"2222222222-433","UserName":"testmisa@yahoo.com","Password":"123456Aa","DeviceID":null}
            string body = V6JsonConverter.ObjectToJson(para, null);

            string result = SendRequest(_baseurl + login_uri, body, "POST", "", "", "", true);
            Login_ServiceResult loginResponse = JsonConvert.DeserializeObject<Login_ServiceResult>(result);
            string token = loginResponse.Data.ToString();
            __token = token;
            string test = "test";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="gui_va_ky">(Cờ đổi hàm) Mặc định false.</param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string POST_CREATE_INVOICE(string jsonBody, out V6Return v6Return)
        {
            string result = "";
            v6Return = new V6Return();

            string link = create_uri;
            if (_co_ma) link = link.Replace("/v3/", "/v3/code/");

            try
            {
                result = POST_BEARERTOKEN(link, jsonBody);
                v6Return.RESULT_STRING = result;
                //{"Success":true,"ErrorCode":null,"Errors":[],"Data":"[{\"RefID\":\"695c0725-4052-40fa-ad6e-c0ffe296c8b4\",\"TransactionID\":null,\"InvNo\":null,\"InvDate\":\"0001-01-01T00:00:00+07:06\",\"InvoiceData\":null,\"ErrorCode\":\"InvalidDeclaration\",\"ErrorData\":\"\",\"TokenCallback\":null,\"CallbackUrl\":null}]","CustomData":null}

                MISA_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.ErrorCode == "UnAuthorize")
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN(link, jsonBody);
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }

                if (responseObject.Success == false)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "ErrorCode:" + responseObject.ErrorCode;
                    if (responseObject.Errors.Count > 0) v6Return.RESULT_ERROR_MESSAGE += ",Error0:" + responseObject.Errors[0];
                }
                else
                {
                    if (responseObject.Data != null)
                    {
                        List<MISA_CreateInvoiceResponseData> responseData = JsonConvert.DeserializeObject<List<MISA_CreateInvoiceResponseData>>(responseObject.Data.ToString());
                        if (responseData.Count == 0)
                        {
                            v6Return.RESULT_ERROR_MESSAGE = "Không có dữ liệu";
                        }
                        else if (!string.IsNullOrEmpty(responseData[0].ErrorCode)) // vẫn có lỗi
                        {
                            v6Return.RESULT_ERROR_MESSAGE = responseData[0].ErrorCode;
                        }
                        else if (string.IsNullOrEmpty(responseData[0].InvNo)) // lỗi gì đó?
                        {
                            v6Return.RESULT_ERROR_MESSAGE = responseObject.Data.ToString();
                        }
                        else // có số hóa đơn InvNo
                        {
                            v6Return.SO_HD = responseData[0].InvNo;
                            v6Return.ID = responseData[0].RefID;
                            v6Return.SECRET_CODE = responseData[0].TransactionID;
                        }
                    }
                    else // không có Data trả về?
                    {
                        v6Return.RESULT_ERROR_MESSAGE = result;
                    }

                }
            }
            catch (Exception ex1)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex1.Message;
            }
            return result;
        }

        public string POST_PUBLISH_INVOICE(string jsonBody, out V6Return v6Return)
        {
            string result = "";
            v6Return = new V6Return();

            string link = create_uri;
            if (_co_ma) link = link.Replace("/v3/", "/v3/code/");

            try
            {
                result = POST_BEARERTOKEN(link, jsonBody);
                v6Return.RESULT_STRING = result;
                //{"Success":true,"ErrorCode":null,"Errors":[],"Data":"[{\"RefID\":\"695c0725-4052-40fa-ad6e-c0ffe296c8b4\",\"TransactionID\":null,\"InvNo\":null,\"InvDate\":\"0001-01-01T00:00:00+07:06\",\"InvoiceData\":null,\"ErrorCode\":\"InvalidDeclaration\",\"ErrorData\":\"\",\"TokenCallback\":null,\"CallbackUrl\":null}]","CustomData":null}

                MISA_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.ErrorCode == "UnAuthorize")
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN(link, jsonBody);
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }

                if (responseObject.Success == false)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "ErrorCode:" + responseObject.ErrorCode;
                    if (responseObject.Errors.Count > 0) v6Return.RESULT_ERROR_MESSAGE += ",Error0:" + responseObject.Errors[0];
                }
                else
                {
                    if (responseObject.Data != null)
                    {
                        List<MISA_CreateInvoiceResponseData> responseData = JsonConvert.DeserializeObject<List<MISA_CreateInvoiceResponseData>>(responseObject.Data.ToString());
                        if (responseData.Count == 0)
                        {
                            v6Return.RESULT_ERROR_MESSAGE = "Không có dữ liệu";
                        }
                        else if (!string.IsNullOrEmpty(responseData[0].ErrorCode)) // vẫn có lỗi
                        {
                            v6Return.RESULT_ERROR_MESSAGE = responseData[0].ErrorCode;
                        }
                        else if (string.IsNullOrEmpty(responseData[0].InvNo)) // lỗi gì đó?
                        {
                            v6Return.RESULT_ERROR_MESSAGE = responseObject.Data.ToString();
                        }
                        else // có số hóa đơn InvNo
                        {
                            v6Return.SO_HD = responseData[0].InvNo;
                            v6Return.ID = responseData[0].RefID;
                            v6Return.SECRET_CODE = responseData[0].TransactionID;
                        }
                    }
                    else // không có Data trả về?
                    {
                        v6Return.RESULT_ERROR_MESSAGE = result;
                    }

                }
            }
            catch (Exception ex1)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex1.Message;
            }
            return result;
        }

        /// <summary>
        /// Thực hiện luôn các bước gửi data, lấy xml, ký, gửi xml đã ký...
        /// </summary>
        /// <param name="json"></param>
        /// <param name="templateCode"></param>
        /// <param name="token_serial"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string CreateInvoice_GetXml_Sign(string json, string token_serial, out V6Return v6Return)
        {
            v6Return = new V6Return();
            POST_CREATE_INVOICE(json, out v6Return);
            // nếu thành công
            if (string.IsNullOrEmpty(v6Return.RESULT_ERROR_MESSAGE))
            {
                // thực hiện ký.
                //V6Sign sign = new V6Sign();
                //sign.Sign("xml", token_serial);

                MISA_CreateInvoiceResponse response1 = (MISA_CreateInvoiceResponse)v6Return.RESULT_OBJECT;
                XmlDocument XmlData = new XmlDocument();
                // gán xml dạng string được trả về khi tạo xml thô
                List<MISA_CreateInvoiceResponseData> list0 = JsonConvert.DeserializeObject<List<MISA_CreateInvoiceResponseData>>(response1.Data.ToString());
                XmlData.LoadXml(list0[0].InvoiceData);
                X509Certificate2 cert = SignXmlUtil.GetCertificateFromStore();
                // truyền các giá trị vào hàm ký số để ký
                SignXmlUtil.SignXml(XmlData, cert, list0[0].TransactionID);

                //Gửi phát hành.
                // Gán giá trị cho đối tượng gọi đi phát hành
                PublishInvoiceData PublishInvoiceData = new PublishInvoiceData();
                List<PublishInvoiceData> lstPublishInvoiceData = new List<PublishInvoiceData>();
                PublishInvoiceData.InvoiceData = XmlData.InnerXml;
                PublishInvoiceData.RefID = list0[0].RefID;
                PublishInvoiceData.TransactionID = list0[0].TransactionID;
                // Kiểm tra có tùy chọn gửi mail kèm phát hành hay không
                //if (chkSendEmail.Checked)
                {
                    // biến đánh dấu gửi mail kèm phát hành
                    //PublishInvoiceData.IsSendEmail = true;
                    // các email được gửi email tới
                    //PublishInvoiceData.ReceiverEmail = txtReceiverEmail.Text;
                    // tên người nhận email
                    //PublishInvoiceData.ReceiverName = txtReceiverName.Text;
                }
                lstPublishInvoiceData.Add(PublishInvoiceData);
                // Phát hành hóa đơn lên hệ thống MISA
                //PublishInvoiceResult = InvoicePublishingObject.PublishInvoice(lstPublishInvoiceData, Session.IsInvoiceCode);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                var publish_json = JsonConvert.SerializeObject(lstPublishInvoiceData, settings);
                
                POST_PUBLISH_INVOICE(publish_json, out v6Return);

            }
            else
            {
                v6Return.RESULT_ERROR_MESSAGE = "Lỗi bước 1. " + v6Return.RESULT_ERROR_MESSAGE;
            }
            return "Chưa hỗ trợ";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="gui_va_ky">(Cờ đổi hàm) Mặc định false.</param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string POST_CREATE_INVOICE_HSM(string jsonBody, out V6Return v6Return)
        {
            string result = "";
            v6Return = new V6Return();

            string link = create_guivakyhoadonhsm_uri;
            if (_co_ma) link = link.Replace("/v3/", "/v3/code/");

            try
            {
                result = POST_BEARERTOKEN(link, jsonBody);
                v6Return.RESULT_STRING = result;

                MISA_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.ErrorCode == "UnAuthorize")
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN(link, jsonBody);
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }

                if (responseObject.Success == false)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "ErrorCode:" + responseObject.ErrorCode;
                    if (responseObject.Errors.Count > 0) v6Return.RESULT_ERROR_MESSAGE += ",Error0:" + responseObject.Errors[0];
                }
                else
                {
                    if (responseObject.Data != null)
                    {
                        List<MISA_CreateInvoiceResponseData> responseData = JsonConvert.DeserializeObject<List<MISA_CreateInvoiceResponseData>>(responseObject.Data.ToString());
                        if (responseData.Count == 0)
                        {
                            v6Return.RESULT_ERROR_MESSAGE = "Không có dữ liệu";
                        }
                        else if (!string.IsNullOrEmpty(responseData[0].ErrorCode)) // vẫn có lỗi
                        {
                            v6Return.RESULT_ERROR_MESSAGE = responseData[0].ErrorCode;
                        }
                        else if (string.IsNullOrEmpty(responseData[0].InvNo)) // lỗi gì đó?
                        {
                            v6Return.RESULT_ERROR_MESSAGE = responseObject.Data.ToString();
                        }
                        else // có số hóa đơn InvNo
                        {
                            v6Return.SO_HD = responseData[0].InvNo;
                            v6Return.ID = responseData[0].RefID;
                            v6Return.SECRET_CODE = responseData[0].TransactionID;
                        }
                    }
                    else // không có Data trả về?
                    {
                        v6Return.RESULT_ERROR_MESSAGE = result;
                    }
                    
                }
            }
            catch (Exception ex1)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex1.Message;
            }
            return result;
        }

        //https://testapi.meinvoice.vn/api/v3/code/itg/invoicepublishing/templates?invYear=2022

        public string SIGN_HSM(string mst, string magiaodich, string ma_hoadon, out V6Return v6Return)
        {
            string result = "";
            v6Return = new V6Return();

            try
            {
                var request = "{\"doanhnghiep_mst\": \""+mst+"\",\"magiaodich\": \""+magiaodich+"\", \"ma_hoadon\": \""+ma_hoadon+"\"}";
                result = POST_BEARERTOKEN(sign_hsm_uri, request);

                MISA_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.ErrorCode == "UnAuthorize")
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN(sign_hsm_uri, request);
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }

                if (responseObject.Success == false)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "ErrorCode:" + responseObject.ErrorCode;
                    if (responseObject.Errors.Count > 0) v6Return.RESULT_ERROR_MESSAGE += ",Error0:" + responseObject.Errors[0];
                }
                else
                {
                    if (responseObject.Data != null)
                    {
                        MISA_CreateInvoiceResponseData responseData = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponseData>(responseObject.Data.ToString());
                        if (!string.IsNullOrEmpty(responseData.ErrorCode)) // vẫn có lỗi
                        {
                            v6Return.RESULT_ERROR_MESSAGE = responseData.ErrorCode;
                        }
                        else if (string.IsNullOrEmpty(responseData.InvNo)) // lỗi gì đó?
                        {
                            v6Return.RESULT_ERROR_MESSAGE = responseObject.Data.ToString();
                        }
                        else // có số hóa đơn InvNo
                        {
                            v6Return.SO_HD = responseData.InvNo;
                            v6Return.ID = responseData.RefID;
                            v6Return.SECRET_CODE = responseData.TransactionID;
                        }
                    }
                    else // không có Data trả về?
                    {
                        v6Return.RESULT_ERROR_MESSAGE = result;
                    }

                }
            }
            catch (Exception ex1)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex1.Message;
            }
            return result;
        }

        /// <summary>
        /// Hàm giống tạo mới nhưng có khác biệt trong dữ liệu.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="invalidate">mặc định false.</param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string POST_REPLACE(string jsonBody, bool invalidate, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = POST_CREATE_INVOICE_HSM(jsonBody, out v6Return);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                v6Return.RESULT_ERROR_MESSAGE += ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_REPLACE " + result);
            return result;
        }
        
        /// <summary>
        /// Gửi điều chỉnh hóa đơn.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="v6Return">Đối tượng trả về chung V6.</param>
        /// <returns></returns>
        public string POST_EDIT(string jsonBody, out V6Return v6Return)
        {
            string result;
            v6Return = new V6Return();
            try
            {
                result = POST_BEARERTOKEN("editlink", jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                v6Return.RESULT_ERROR_MESSAGE += ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_EDIT " + result);
            return result;
        }

        public string POST_USERPASS(string uri, string request)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = SendRequest(_baseurl + uri, request, "POST", _username, _password, "", true);
            return postResult;
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="uri">Đường dẫn hàm, không kể _baseurl</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public string POST_BEARERTOKEN(string uri, string request)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = SendRequest(_baseurl + uri, request, "POST", "", __token, "", true);
            return postResult;
        }

        public string GET_BEARERTOKEN(string uri)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = SendRequest(_baseurl + uri, "", "GET", "", __token, "", true);
            return postResult;
        }

        CookieContainer cookies = new CookieContainer();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="full_uri"></param>
        /// <param name="content"></param>
        /// <param name="method"></param>
        /// <param name="username"></param>
        /// <param name="password_or_bearertoken"></param>
        /// <param name="viettel_token"></param>
        /// <param name="allowAutoRedirect"></param>
        /// <returns></returns>
        public string SendRequest(string full_uri, string content, string method, string username, string password_or_bearertoken, string viettel_token, bool allowAutoRedirect)
            //string proxyIP = "", int port = 0, bool use_ssl = false)
        {
            try
            {
                if (full_uri == null)
                {
                    throw new ArgumentNullException("full_uri");
                }

                method = method != null ? method.ToUpper() : "";

                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(full_uri);

                //if (!string.IsNullOrEmpty(proxyIP))
                //{
                //    WebProxy proxy = new WebProxy(proxyIP, port);
                //    request.Proxy = proxy;
                //}
                

                // Set the Method property of the request to POST.
                request.Method = method;
                // Set cookie container to maintain cookies
                request.CookieContainer = cookies;
                request.AllowAutoRedirect = allowAutoRedirect;
                // If username is empty use defaul credentials
                if (string.IsNullOrEmpty(username))
                {
                    if (string.IsNullOrEmpty(password_or_bearertoken))
                    {
                        if (string.IsNullOrEmpty(viettel_token))
                        {
                            request.Credentials = CredentialCache.DefaultNetworkCredentials;
                        }
                        else if(!string.IsNullOrEmpty(viettel_token)) // 3 Viettel Authorization with Cookie access_token=...
                        {
                            request.Headers.Add("Cookie", string.Format("access_token={0}", viettel_token));
                        }
                    }
                    else // 2 Authorization with Bearer token
                    {
                        request.Headers.Add("Authorization", "Bearer " + password_or_bearertoken); // Lúc này password là Bearer Token.
                    }
                }
                else // 1 Basic Authorization with username + password
                {
                    var encoded = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + password_or_bearertoken));
                    request.Headers.Add("Authorization", "Basic " + encoded);
                    //request.Credentials = new NetworkCredential(username, password);
                }

                // MISA LUÔN DÙNG ???
                if (!string.IsNullOrEmpty(_codetax))
                {
                    request.Headers.Add("CompanyTaxCode", _codetax);
                }

                if (method == "POST")
                {
                    // Convert POST data to a byte array.
                    //Test

                    byte[] byteArray = Encoding.UTF8.GetBytes(content);

                    // Set the ContentType property of the WebRequest.
                    // Custom for Viettel API v2.0
                    if (full_uri.ToLower().EndsWith("InvoiceAPI/InvoiceWS/cancelTransactionInvoice".ToLower())
                        || full_uri.ToLower().EndsWith("InvoiceAPI/InvoiceWS/updatePaymentStatus".ToLower())
                        || full_uri.ToLower().EndsWith("InvoiceAPI/InvoiceWS/createExchangeInvoiceFile".ToLower())
                        || full_uri.ToLower().EndsWith("InvoiceAPI/InvoiceWS/searchInvoiceByTransactionUuid".ToLower())
                        )
                    {
                        request.ContentType = "application/x-www-form-urlencoded";
                    }
                    else
                    {
                        request.ContentType = "application/json; charset=utf-8";
                    }                    
                    request.Accept = "application/json";
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;

                    ServicePointManager.ServerCertificateValidationCallback = delegate(object sender,
                        X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    
                    //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/49.0 Chrome/43.0.2357.126_coc_coc Safari/537.36";
                    // Get the request stream.
                    Stream requestStream = request.GetRequestStream();

                    // Write the data to the request stream.
                    requestStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    requestStream.Close();
                }

                var response = (HttpWebResponse)request.GetResponse();                
                cookies.Add(response.Cookies);
                
                Stream responseStream = null;
                StreamReader reader = null;
                string responseFromServer = null;

                try
                {
                    // Get the stream containing content returned by the server.
                    responseStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    if (responseStream != null)
                    {
                        reader = new StreamReader(responseStream);
                        responseFromServer = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
                finally
                {                
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    if (responseStream != null)
                    {
                        responseStream.Close();
                    }
                    response.Close();
                }
                
                return responseFromServer;
                //return request;
            }
            catch (WebException webex)
            {
                if (webex.Response == null) return null;
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    if (string.IsNullOrEmpty(text)) text = webex.Message;
                    return text;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Cập nhập tình trạng thanh toán.
        /// </summary>
        /// <param name="codeTax">0100109106-712 hoặc 0100109106</param>
        /// <param name="invoiceNo">AA/20E0000002</param>
        /// <param name="strIssueDate">1600154781000</param>
        /// <param name="templateCode">01GTKT0/001</param>
        /// <param name="buyerEmailAddress">tamdtt1@viettel.com.vn</param>
        /// <param name="paymentType">TM</param>
        /// <param name="paymentTypeName">TM</param>
        /// <param name="cusGetInvoiceRight">true/false</param>
        /// <param name="v6Return">Kết quả trả về v6</param>
        /// <returns></returns>
        public string UpdatePaymentStatus(string codeTax, string invoiceNo, string strIssueDate, string templateCode,
            string buyerEmailAddress, string paymentType, string paymentTypeName, string cusGetInvoiceRight, out V6Return v6Return)
        {
            v6Return = new V6Return();
            //@"supplierTaxCode=0100109106-712
            //&invoiceNo=AA%2F20E0000002
            //&strIssueDate=1600154781000
            //&templateCode=01GTKT0%2F002
            //&buyerEmailAddress=tamdtt1%40viettel.com.vn
            //&paymentType=TM
            //&paymentTypeName=TM
            //&cusGetInvoiceRight=true"

            string request =
                @"supplierTaxCode=" + codeTax
                + @"&invoiceNo=" + invoiceNo
                + @"&strIssueDate=" + strIssueDate
                + @"&templateCode=" + templateCode
                + @"&buyerEmailAddress=" + buyerEmailAddress
                + @"&paymentType=" + paymentType
                + @"&paymentTypeName=" + paymentTypeName
                + @"&cusGetInvoiceRight=" + cusGetInvoiceRight;
            string result = POST_BEARERTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/updatePaymentStatus", request);
            v6Return.RESULT_STRING = result;
            // Sample result
            //{"errorCode":null,"description":null,"result":true,"paymentTime":null,"paymentMethod":null}   // word
            //{\"code\":400,\"message\":\"EMAIL_INVALID\",\"data\":\"Email không hợp lệ\"}                  // test
            //{"errorCode":null,"description":null,"result":true,"paymentTime":1601463363413,"paymentMethod":"TC"}  //test
            UpdatePaymentResponse responseObject = JsonConvert.DeserializeObject<UpdatePaymentResponse>(result);
            v6Return.RESULT_OBJECT = responseObject;
            if (responseObject.result)
            {
                v6Return.RESULT_MESSAGE = "OK";
            }
            else if (responseObject.code != 0)
            {
                v6Return.RESULT_ERROR_MESSAGE = responseObject.message + " " + responseObject.data;
            }
            else
            {
                v6Return.RESULT_MESSAGE = responseObject.message;
            }
            return result;
        }

        /// <summary>
        /// Hủy hóa đơn.
        /// </summary>
        /// <param name="codeTax">Mã số thuế service.</param>
        /// <param name="invoiceNo">AB/19E0000001</param>
        /// <param name="strIssueDate">yyyyMMddHHmmss</param>
        /// <param name="so_bien_ban"></param>
        /// <param name="ngay_bien_ban">YYYY-MM-DD hh:mm:ss</param>
        /// <returns></returns>
        public string HUY_HOA_DON(string transactionID, string invNo, string refDate, string ly_do_huy, out V6Return v6Return)
        {
            string result = "";
            v6Return = new V6Return();

            try
            {
                var link = huyhoadon_uri;
                if (_co_ma) link = link.Replace("/v3/", "/v3/code/");

                var o = new Dictionary<string, object>();
                o["TransactionID"] = transactionID;
                o["InvNo"] = invNo;
                o["RefDate"] = refDate;
                o["CancelReason"] = ly_do_huy;
                string request = V6JsonConverter.ObjectToJson(o, "yyyy-MM-dd");
//{
//"TransactionID":"BGUGH_MZM123",
//"InvNo":"00000004",
//"RefDate":"2021-11-29",  
//"CancelReason":"hủy qua API"
//}

                result = POST_BEARERTOKEN(link, request);
                v6Return.RESULT_STRING = result;
//Respone:
//{
//"Success": true,
//"ErrorCode": null,
//"Errors": [],
//"Data": "{\"240b374b-751f-4ce2-b7af-3a5fac7f968d\"}",
//"CustomData": null
//}

                MISA_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.ErrorCode == "UnAuthorize")// == "GENERAL" && result.Contains("\"error\":\"Internal Server Error\""))
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN(huyhoadon_uri, request);
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }

                if (!string.IsNullOrEmpty(responseObject.ErrorCode))
                {
                    v6Return.RESULT_ERROR_MESSAGE = "error:" + responseObject.ErrorCode;
                }
                else if (responseObject.Success)
                {
                    Guid g = JsonConvert.DeserializeObject<Guid>(responseObject.Data.ToString());
                    v6Return.SO_HD = invNo;
                    v6Return.ID = "" + g;
                    v6Return.SECRET_CODE = transactionID;
                }
                else
                {
                    v6Return.RESULT_ERROR_MESSAGE = result;
                }
            }
            catch (Exception ex1)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex1.Message;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="invoiceNo">AA/17E0037914</param>
        /// <param name="fileType">xml zip</param>
        /// <param name="issueDate">20180320152309</param>
        /// <param name="savefolder"></param>
        /// <returns></returns>
        public string DownloadInvoice(string codeTax, string invoiceNo, string fileType, string issueDate, string savefolder)
        {
            //if (uri.StartsWith("/")) uri = uri.Substring(1);
            string apiLink = _baseurl + "InvoiceAPI/InvoiceUtilsWS/getInvoiceFile";

            GetFileRequest objGetFile = new GetFileRequest();
            
            objGetFile.invoiceNo = invoiceNo;// "BR/18E0000014";
            objGetFile.fileType = fileType;
            objGetFile.strIssueDate = issueDate;

            string getData = "?supplierTaxCode=" + codeTax +
                             "&invoiceNo=" + objGetFile.invoiceNo +
                             "&fileType=" + objGetFile.fileType +
                             "&strIssueDate=" + objGetFile.strIssueDate;
            apiLink += getData;
            //string autStr = CreateRequest.Base64Encode(userPass);
            //string contentType = "application/x-www-form-urlencoded";
            //string request = string.Empty;
            //string result = CreateRequest.webRequest(apiLink, request, autStr, "GET", contentType);
            string result = GET_BEARERTOKEN(apiLink);

            ZipFileResponse objFile = JsonConvert.DeserializeObject<ZipFileResponse>(result);
            string fileName = objFile.fileName;
            if (string.IsNullOrEmpty(fileName) || objFile.fileToBytes == null)
            {
                throw new Exception("Download no file!");
            }

            string path = Path.Combine(savefolder, fileName + ".zip");
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {
                    //
                }
            }
            File.WriteAllBytes(path, objFile.fileToBytes);

            return path;
        }

        public string DownloadInvoiceZip(string codeTax, string uri, string savefolder)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            string apiLink = _baseurl + uri;

            GetFileRequest objGetFile = new GetFileRequest();
            objGetFile.fileType = "zip";
            objGetFile.invoiceNo = "BR/18E0000014";
            objGetFile.strIssueDate = "20180320152309";

            string getData = "?supplierTaxCode=" + codeTax +
                             "&invoiceNo=" + objGetFile.invoiceNo +
                             "&fileType=" + objGetFile.fileType +
                             "&strIssueDate=" + objGetFile.strIssueDate;
            apiLink += getData;
            //string autStr = CreateRequest.Base64Encode(userPass);
            //string contentType = "application/x-www-form-urlencoded";
            //string request = string.Empty;
            //string result = CreateRequest.webRequest(apiLink, request, autStr, "GET", contentType);
            string result = GET_BEARERTOKEN(apiLink);

            ZipFileResponse objFile = JsonConvert.DeserializeObject<ZipFileResponse>(result);
            string fileName = objFile.fileName;
            if (string.IsNullOrEmpty(fileName) || objFile.fileToBytes == null)
            {
                throw new Exception("Download no file!");
            }

            string path = Path.Combine(savefolder, fileName + ".zip");
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {
                    //
                }
            }
            File.WriteAllBytes(path, objFile.fileToBytes);

            return path;
        }


        /// <summary>
        /// Download bản thể hiện.
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="transactionID">Số hóa đơn hệ thống Viettel trả về AA/17E0000166.</param>
        /// <param name="templateCode">01GTKT0/151</param>
        /// <param name="uid">uid từ khi lập hóa đơn.</param>
        /// <param name="savefolder"></param>
        /// <param name="v6Return"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string TAI_HOA_DON_PDF(string id, string transactionID, string savefolder, out V6Return v6Return)
        {
            v6Return = new V6Return();
            try
            {
                var link = taipdf_uri;
                if (_co_ma) link = link.Replace("/v3/", "/v3/code/");
                string request = "[\"" + transactionID + "\"]";
                string result = POST_BEARERTOKEN(link, request);
                v6Return.RESULT_STRING = result;
                MISA_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);

                if (responseObject.ErrorCode == "UnAuthorize")
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN(taipdf_uri, request);
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }
                //{"Success":true,"ErrorCode":null,"Errors":[],"Data":"[{\"TransactionID\":\"abcv\",\"Data\":null,\"ErrorCode\":\"InvalidTransactionID\"}]","CustomData":null}

                string fileName = id;
                if (!string.IsNullOrEmpty(responseObject.ErrorCode))
                {
                    v6Return.RESULT_ERROR_MESSAGE = "error:" + responseObject.ErrorCode;
                    return null;
                }
                else if (responseObject.Data != null)
                {
                    MISA_CreateInvoiceResponseData data = JsonConvert.DeserializeObject<MISA_CreateInvoiceResponseData>(result);
                    if (data.Data != null)
                    {
                        v6Return.RESULT_OBJECT = data.Data;
                        v6Return.PATH = Path.Combine(savefolder, fileName + ".pdf");
                    }
                    else
                    {
                        v6Return.RESULT_ERROR_MESSAGE = "download data null:" + responseObject.Data;
                        return null;
                    }
                }

                if (File.Exists(v6Return.PATH))
                {
                    try
                    {
                        File.Delete(v6Return.PATH);
                    }
                    catch
                    {
                        //
                    }
                }
                if (!File.Exists(v6Return.PATH))
                {
                    File.WriteAllBytes(v6Return.PATH, Convert.FromBase64String(v6Return.RESULT_OBJECT.ToString()));
                }
            }
            catch (Exception ex)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex.Message;
            }

            return v6Return.PATH;
        }



        /// <summary>
        /// Download bản chuyển đổi. application/x-www-form-urlencoded
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="invoiceNo">Số hóa đơn hệ thống Viettel trả về.</param>
        /// <param name="strIssueDate"></param>
        /// <param name="savefolder"></param>
        /// <param name="v6Return">Thông tin trả về</param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDF_CHUYENDOI(string codeTax, string invoiceNo, string strIssueDate, string savefolder, out V6Return v6Return)
        {
            v6Return = new V6Return();
            //invoiceNo = invoiceNo.Replace("/", "%2F");
            GetPDFFileRequestE objGetPdfFile = new GetPDFFileRequestE()
            {
                supplierTaxCode = codeTax,
                invoiceNo = invoiceNo,
                strIssueDate = strIssueDate,
                exchangeUser = _username
            };
            //string request = objGetPdfFile.ToJson("VIETTEL");
            
            string parameters = string.Format("?supplierTaxCode={0}&invoiceNo={1}&strIssueDate={2}&exchangeUser={3}",
                codeTax, invoiceNo, strIssueDate, _username);

            //string result = GET_VIETTEL_TOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createExchangeInvoiceFile" + parameters);
            //result = GET_VIETTEL_TOKEN("/InvoiceAPI/InvoiceWS/createExchangeInvoiceFile" + parameters);
            string result = POST_BEARERTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createExchangeInvoiceFile", parameters.Substring(1));
            //string result = POST_VIETTEL_COOKIESTOKEN("/InvoiceAPI/InvoiceWS/createExchangeInvoiceFile", parameters.Substring(1));
            v6Return.RESULT_STRING = result;
            PDFFileResponse responseObject = JsonConvert.DeserializeObject<PDFFileResponse>(result);
            v6Return.RESULT_OBJECT = responseObject;
            if (responseObject.message == "GENERAL" && result.Contains("\"error\":\"Internal Server Error\""))
            {
                // Nếu hết phiên đăng nhập thì đăng nhập lại.
                Login();
                // sau đó gửi lại.
                result = POST_BEARERTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createExchangeInvoiceFile", parameters.Substring(1));
                v6Return.RESULT_STRING = result;
                responseObject = JsonConvert.DeserializeObject<PDFFileResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
            }

            string fileName = responseObject.fileName;
            if (string.IsNullOrEmpty(fileName) || responseObject.fileToBytes == null)
            {
                v6Return.RESULT_ERROR_MESSAGE = "Download no file! " + responseObject.message;
                throw new Exception("Download no file!" + responseObject.message);
            }
            else
            {
                v6Return.PATH = Path.Combine(savefolder, fileName + "_E.pdf");
            }

            if (File.Exists(v6Return.PATH))
            {
                try
                {
                    File.Delete(v6Return.PATH);
                }
                catch
                {
                    //
                }
            }
            if (!File.Exists(v6Return.PATH))
            {
                File.WriteAllBytes(v6Return.PATH, responseObject.fileToBytes);
            }
            
            return v6Return.PATH;
        }

        /// <summary>
        /// Content-Type : application/x-www-form-urlencoded
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="uid"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string SearchInvoiceByTransactionUuid(string codeTax, string uid, out V6Return v6Return)
        {
            v6Return = new V6Return();
            v6Return.RESULT_ERROR_MESSAGE = "chưa hỗ trợ";
            return "chưa hỗ trợ.";
        }

        /// <summary>
        /// Chạy kiểm tra kết nối. Nếu ổn trả về null. Có lỗi trả về câu thông báo.
        /// </summary>
        /// <returns></returns>
        public string CheckConnection(out V6Return v6Return)
        {
            string result = POST_CREATE_INVOICE("[{}]", out v6Return);
            if (v6Return.RESULT_ERROR_MESSAGE != null && v6Return.RESULT_ERROR_MESSAGE.Contains("success:false,error0:Object reference not set to an instance of an object."))
            {
                return null;
            }
            else
            {
                return v6Return.RESULT_STRING;
            }
        }


        



        /// <summary>
        /// Lấy thông tin metadata
        /// </summary>
        /// <param name="templateCode">01GTKT0/001</param>
        /// <returns></returns>
        public string GetSerialList(string templateCode, out V6Return v6Return)
        {
            v6Return = new V6Return();
            string result = "";
            try
            {
                string apiLink = "/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/getCustomFields?taxCode=" + _codetax + "&templateCode=" + templateCode;
                result = GET_BEARERTOKEN(apiLink);
                v6Return.RESULT_STRING = result;
                v6Return.RESULT_OBJECT = result;
            }
            catch (Exception ex)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex.Message;
            }
            return result;
        }

    }
}
