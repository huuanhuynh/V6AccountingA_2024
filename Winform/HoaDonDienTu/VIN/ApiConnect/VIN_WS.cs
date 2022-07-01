using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using V6Tools;
using V6ThuePost.VIN_Objects.Response;
using V6ThuePost.ResponseObjects;
using V6ThuePost.VIN_Objects;
using V6SignToken;
using System.Collections.Generic;
using V6Tools.V6Convert;

namespace V6ThuePost_VIN_Api
{
    public class VIN_WS
    {
        /// <summary>
        /// https://demows.vin-hoadon.com
        /// </summary>
        private string _baseurl = "";

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

        private const string login_uri = "/api/services/hddtws/Authentication/GetToken";
        private const string create_uri = @"/api/services/hddtws/QuanLyHoaDon/LapHoaDonGoc";
        private const string create_guivakyhoadonhsm_uri = @"/api/services/hddtws/QuanLyHoaDon/GuiVaKyHoadonGocHSM";
        private const string sign_hsm_uri = @"/api/services/hddtws/XuLyHoaDon/KyHoaDonHSM";
        private const string taipdf_uri = "/api/services/hddtws/TraCuuHoaDon/TaiHoaDonPdf"; // cách gọi riêng ở chương trình này: uri là đường dẫn hàm ko bao gồm baseurl.
        
        //private const string cancel_link = @"/InvoiceAPI/InvoiceWS/cancelTransactionInvoice";
        private const string cancel_link = @"/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/cancelTransactionInvoice";

        //private readonly RequestManager requestManager = new RequestManager();

        public VIN_WS(string baseurl, string username, string password, string codetax)
        {
            _baseurl = baseurl;
            if (_baseurl.EndsWith("/")) _baseurl = _baseurl.Substring(0, _baseurl.Length-1);
            _username = username;
            _password = password;
            _codetax = codetax;

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
            string body = "{\"doanhnghiep_mst\":\""+_codetax+"\", \"username\" : \""+_username+"\", \"password\" : \""+_password+"\"}";

//            {
//- access_token: Token 
//- encrypted_access_token: Mã client ID 
//- expire_in_seconds: Thời hạn sử dụng của token, ms
//- token_type: bear
//}
            string result = SendRequest(_baseurl + login_uri, body, "POST", "", "", "", true);
            VIN_LoginResponse loginResponse = JsonConvert.DeserializeObject<VIN_LoginResponse>(result);
            string token = loginResponse.result.access_token;
            __token = token;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="gui_va_ky">(Cờ đổi hàm) Mặc định false.</param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string POST_CREATE_INVOICE(string jsonBody, bool gui_va_ky, out V6Return v6Return)
        {
            string result = "";
            v6Return = new V6Return();

            string link = gui_va_ky ? create_guivakyhoadonhsm_uri : create_uri;
            
            try
            {
                result = POST_BEARERTOKEN(link, jsonBody);
                v6Return.RESULT_STRING = result;
                
                VIN_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.unAuthorizedRequest)// == "GENERAL" && result.Contains("\"error\":\"Internal Server Error\""))
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN(link + _codetax, jsonBody);
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }

                if (responseObject.result == null)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "result:null,error:" + responseObject.error;
                }
                else
                {
                    v6Return.SO_HD = responseObject.result.sohoadon;
                    if (responseObject.result.datahd.ContainsKey("guid")) v6Return.ID = responseObject.result.datahd["guid"].ToString();
                    v6Return.SECRET_CODE = responseObject.result.magiaodich;
                }
            }
            catch (Exception ex1)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex1.Message;
            }
            return result;
        }


        public string SIGN_HSM(string mst, string magiaodich, string ma_hoadon, out V6Return v6Return)
        {
            string result = "";
            v6Return = new V6Return();

            try
            {
                var request = "{\"doanhnghiep_mst\": \""+mst+"\",\"magiaodich\": \""+magiaodich+"\", \"ma_hoadon\": \""+ma_hoadon+"\"}";
                result = POST_BEARERTOKEN(sign_hsm_uri, request);

                VIN_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.unAuthorizedRequest)// == "GENERAL" && result.Contains("\"error\":\"Internal Server Error\""))
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN(sign_hsm_uri, request);
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }

                if (responseObject.result == null)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "result:null,error:" + responseObject.error;
                }
                else
                {
                    v6Return.SO_HD = responseObject.result.sohoadon;
                    if (responseObject.result.datahd.ContainsKey("guid")) v6Return.ID = responseObject.result.datahd["guid"].ToString();
                    v6Return.SECRET_CODE = responseObject.result.magiaodich;
                }
            }
            catch (Exception ex)
            {
                v6Return.RESULT_ERROR_MESSAGE = ex.Message;
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
                result = POST_CREATE_INVOICE(jsonBody, invalidate, out v6Return);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                v6Return.RESULT_ERROR_MESSAGE += ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_REPLACE " + result);
            return result;
        }

        public string POST_DRAFT(string jsonBody, out V6Return v6Return)
        {
            string result;
            v6Return = new V6Return();
            try
            {
                result = POST_BEARERTOKEN(create_uri, jsonBody);
                v6Return.RESULT_STRING = result;
                try
                {
                    // {"errorCode":"","description":"","result":{}}
                    // {"code":400,"message":"TRANSACTION_UUID_INVALID","data":"Transaction Uuid đã được lập hóa đơn"}
                    VIN_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                    if (responseObject.unAuthorizedRequest)// == "GENERAL" && result.Contains("\"error\":\"Internal Server Error\""))
                    {
                        // Nếu hết phiên đăng nhập thì đăng nhập lại.
                        Login();
                        // sau đó gửi lại.
                        result = POST_BEARERTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createOrUpdateInvoiceDraft/" + _codetax, jsonBody);
                        v6Return.RESULT_STRING = result;
                        responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                        v6Return.RESULT_OBJECT = responseObject;
                    }

                    if (responseObject.success)
                    {
                        v6Return.SO_HD = responseObject.result.sohoadon;
                        if (responseObject.result.datahd.ContainsKey("guid")) v6Return.ID = responseObject.result.datahd["guid"].ToString();
                        v6Return.SECRET_CODE = responseObject.result.magiaodich;
                    }
                    else
                    {
                        v6Return.RESULT_ERROR_MESSAGE = "error:" + responseObject.error;
                    }
                }
                catch (Exception ex1)
                {
                    v6Return.RESULT_ERROR_MESSAGE = ex1.Message;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                v6Return.RESULT_ERROR_MESSAGE += ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_DRAFT " + result);
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
                        request.ContentType = "application/json";
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
        /// <param name="additionalReferenceDesc"></param>
        /// <param name="additionalReferenceDate">strIssueDate</param>
        /// <returns></returns>
        public string CancelTransactionInvoice(string codeTax, string invoiceNo, string strIssueDate,
            string additionalReferenceDesc, string additionalReferenceDate, out V6Return v6Return)
        {
            v6Return = new V6Return();
            //codeTax = "0100109106";
            //invoiceNo = "AA/17E0037914";
            //strIssueDate = "20170907161438";
            //additionalReferenceDesc = "viettel_1234";
            //additionalReferenceDate = "20170907161438";

            //supplierTaxCode=0100109106-712
            //&invoiceNo=AA%2F20E0000001
            //&strIssueDate=1600102800000
            //&additionalReferenceDesc=hello
            //&additionalReferenceDate=1600230649604

            string request =
                @"supplierTaxCode=" + codeTax
                + @"&invoiceNo=" + invoiceNo.Replace("/", "%2F")
                + @"&strIssueDate=" + strIssueDate
                + @"&additionalReferenceDesc=" + additionalReferenceDesc
                + @"&additionalReferenceDate=" + additionalReferenceDate;
            string result = POST_BEARERTOKEN(cancel_link, request);
            v6Return.RESULT_STRING = result;
            CancelResponse responseObject = JsonConvert.DeserializeObject<CancelResponse>(result);

            if (responseObject.message == "GENERAL" && result.Contains("\"error\":\"Internal Server Error\""))
            {
                // Nếu hết phiên đăng nhập thì đăng nhập lại.
                Login();
                // sau đó gửi lại.
                result = POST_BEARERTOKEN(cancel_link, request);
                v6Return.RESULT_STRING = result;
                responseObject = JsonConvert.DeserializeObject<CancelResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
            }

            if (!string.IsNullOrEmpty(responseObject.code))
            {
                v6Return.RESULT_ERROR_MESSAGE = responseObject.message + " : " + responseObject.data;
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
        /// <param name="magiaodich">Số hóa đơn hệ thống Viettel trả về AA/17E0000166.</param>
        /// <param name="templateCode">01GTKT0/151</param>
        /// <param name="uid">uid từ khi lập hóa đơn.</param>
        /// <param name="savefolder"></param>
        /// <param name="v6Return"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDF(string codeTax, string magiaodich, string uid, string savefolder, out V6Return v6Return)
        {
            v6Return = new V6Return();
            
            var requestO = new Dictionary<string, object>();
            requestO["doanhnghiep_mst"] = codeTax;
            requestO["magiaodich"] = magiaodich;
            requestO["ma_hoadon"] = uid;
            string request = V6JsonConverter.ObjectToJson(requestO, null);
//{
//"doanhnghiep_mst": "string",
//"magiaodich": "string",
//"ma_hoadon": "string"
//}

            string result = POST_BEARERTOKEN(taipdf_uri, request);

            v6Return.RESULT_STRING = result;
            VIN_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);

            if (responseObject.unAuthorizedRequest)
            {
                // Nếu hết phiên đăng nhập thì đăng nhập lại.
                Login();
                // sau đó gửi lại.
                result = POST_BEARERTOKEN(taipdf_uri, request);
                v6Return.RESULT_STRING = result;
                responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
            }

            string fileName = uid;
            if (responseObject.result.base64pdf == null)
            {
                v6Return.RESULT_ERROR_MESSAGE = "Download no file! " + responseObject.result.motaketqua;
                //throw new Exception("Download no file! " + responseObject.result.motaketqua);
                return null;
            }
            else
            {
                v6Return.PATH = Path.Combine(savefolder, fileName + ".pdf");
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
                File.WriteAllBytes(v6Return.PATH, Convert.FromBase64String(responseObject.result.base64pdf));
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
        public string DownloadInvoicePDFexchange(string codeTax, string invoiceNo, string strIssueDate, string savefolder, out V6Return v6Return)
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
            
            string parameters =
                string.Format("?supplierTaxCode={0}&transactionUuid={1}", codeTax, uid);

            string result = POST_BEARERTOKEN("/api/services/hddtws/TraCuuHoaDon/TraThongTinHoaDon", parameters.Substring(1));
            //string result = POST_VIETTEL_COOKIESTOKEN("/InvoiceAPI/InvoiceWS/createExchangeInvoiceFile", parameters.Substring(1));
            v6Return.RESULT_STRING = result;
            try
            {
                VIN_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.unAuthorizedRequest)// == "GENERAL" && result.Contains("\"error\":\"Internal Server Error\""))
                {
                    // Nếu hết phiên đăng nhập thì đăng nhập lại.
                    Login();
                    // sau đó gửi lại.
                    result = POST_BEARERTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/searchInvoiceByTransactionUuid", parameters.Substring(1));
                    v6Return.RESULT_STRING = result;
                    responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
                    v6Return.RESULT_OBJECT = responseObject;
                }

                if (responseObject.result == null || !responseObject.success)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "error:" + responseObject.error;
                }
                else
                {
                    v6Return.SO_HD = responseObject.result.sohoadon;
                    v6Return.ID = responseObject.result.magiaodich;
                    v6Return.SECRET_CODE = responseObject.result.magiaodich;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                v6Return.RESULT_ERROR_MESSAGE += ex.Message;
            }
            return result;
            
        }

        /// <summary>
        /// Chạy kiểm tra kết nối. Nếu ổn trả về null. Có lỗi trả về câu thông báo.
        /// </summary>
        /// <returns></returns>
        public string CheckConnection()
        {
            V6Return v6Return;
            string result = DownloadInvoicePDF(_codetax, "V6SOFT", "V6SOFT", "", out v6Return);
            if (v6Return.RESULT_ERROR_MESSAGE != null && v6Return.RESULT_ERROR_MESSAGE.Contains("Lỗi phát sinh"))
            {
                return null;
            }
            else
            {
                return v6Return.RESULT_STRING;
            }
        }


        public VIN_CreateInvoiceResponse POST_NEW_USBTOKEN(string json, string templateCode, string token_serial)
        {
            string result = null;
            VIN_CreateInvoiceResponse responseObject = CreateInvoiceUsbTokenGetHash(json, out result);

            if (responseObject.result != null)
            {
                V6Sign v6sign = new V6Sign();
                string sign = v6sign.Sign(responseObject.result.base64xml, token_serial);
                string result2 = CreateInvoiceUsbTokenInsertSignature(_codetax, templateCode, responseObject.result.base64xml, sign);//hashString=>base64xml
                responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result2);
                return responseObject;
            }
            else
            {
                Logger.WriteToLog("POST_NEW_TOKEN: " + result);
                return responseObject;
                //return "{\"errorCode\": \"POST1_RESULT_NULL\",\"description\": \"Lấy hash null.\",\"result\": null}";
            }
        }

        public string CreateInvoiceUsbTokenGetHash_Sign(string json, string templateCode, string token_serial, out V6Return v6Return)
        {
            string hash_result = null;
            string sign_result2 = null;
            v6Return = new V6Return();
            hash_result = POST_BEARERTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
            v6Return.RESULT_STRING = hash_result;
            VIN_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(hash_result);
            v6Return.RESULT_OBJECT = responseObject;
            if (responseObject.unAuthorizedRequest)// == "GENERAL" && hash_result.Contains("\"error\":\"Internal Server Error\""))
            {
                // Nếu hết phiên đăng nhập thì đăng nhập lại.
                Login();
                // sau đó gửi lại.
                hash_result = POST_BEARERTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
                v6Return.RESULT_STRING = hash_result;
                responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(hash_result);
                v6Return.RESULT_OBJECT = responseObject;
            }


            if (responseObject.result != null)
            {
                V6Sign v6sign = new V6Sign();
                string sign = v6sign.Sign(responseObject.result.base64xml, token_serial);
                sign_result2 = CreateInvoiceUsbTokenInsertSignature(_codetax, templateCode, responseObject.result.base64xml, sign);
                v6Return.RESULT_STRING = sign_result2;
                responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(sign_result2);
                v6Return.RESULT_OBJECT = responseObject;
                if (responseObject.error != null)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "error:" + responseObject.error;
                }
                else
                {
                    v6Return.SO_HD = responseObject.result.sohoadon;
                    v6Return.ID = responseObject.result.magiaodich;
                    v6Return.SECRET_CODE = responseObject.result.magiaodich;
                }
            }
            else
            {
                v6Return.RESULT_ERROR_MESSAGE = hash_result;
                Logger.WriteToLog("" + hash_result);
                return "{\"errorCode\": \"POST1_RESULT_NULL\",\"description\": \"Lấy hash null.\",\"result\": null}";
            }

            return sign_result2;
        }


        public VIN_CreateInvoiceResponse CreateInvoiceUsbTokenGetHash(string json, out string result)
        {
            result = POST_BEARERTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
            VIN_CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<VIN_CreateInvoiceResponse>(result);
            return responseObject;
        }

        /// <summary>
        /// Gửi chữ ký số
        /// </summary>
        /// <param name="supplierTaxCode">Mã số thuế của doanh nghiệp/chi nhánh phát hành hóa đơn. Mẫu 1: 0312770607 Mẫu 2: 0312770607-001</param>
        /// <param name="templateCode">Mã mẫu hóa đơn: 01GTKT0/001</param>
        /// <param name="hashString">Chuỗi hash trả về từ hàm CreateInvoiceUsbTokenGetHash.</param>
        /// <param name="signature">Chuỗi đã ký.</param>
        /// <returns></returns>
        public string CreateInvoiceUsbTokenInsertSignature(string supplierTaxCode, string templateCode, string hashString, string signature)
        {
            string methodlink = "/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createInvoiceUsbTokenInsertSignature";
            string request = 
@"{
""supplierTaxCode"":""" + supplierTaxCode + @""",
""templateCode"":""" + templateCode + @""",
""hashString"":""" + hashString + @""",
""signature"":""" + signature + @"""
}";

            string result = POST_BEARERTOKEN(methodlink, request);

            return result;
        }

        /// <summary>
        /// Lấy thông tin metadata
        /// </summary>
        /// <param name="templateCode">01GTKT0/001</param>
        /// <returns></returns>
        public string GetMetaDataDefine(string templateCode, out V6Return v6Return)
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
