using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using V6ThuePost.HDDT_GDT_GOV;
using V6ThuePost.HDDT_GDT_GOV.Purchase;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ThuePostViettelV2Api
{
    public class HoadondientuAPI
    {
        /// <summary>
        /// Đường dẫn không / cuối
        /// </summary>
        private string _baseurl = "";
        bool WriteExtraLog = false;
        /// <summary>
        /// Tên người sử dụng trên hệ thống Sinvoice (Viettel), thường là codetax
        /// </summary>
        private string _username;
        private string _password;
        private string _token = "";
        /// <summary>
        /// Mã số thuế của doanh nghiệp.
        /// </summary>
        private string _codetax;

        private const string _capcha_uri = "/captcha";
        

        public HoadondientuAPI(string baseurl)
        {
            _baseurl = baseurl;
            if (_baseurl.EndsWith("/")) _baseurl = _baseurl.Substring(0, _baseurl.Length-1);
           
        }

        public Capcha captcha = null;
        public Capcha GetLoginCapcha()
        {
            if (true)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                       | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12
                                                       | SecurityProtocolType.Ssl3;
            }
            
            try
            {
                string response = GET(_capcha_uri);
                captcha = JsonConvert.DeserializeObject<Capcha>(response);
            }
            catch (Exception ex)
            {
                captcha.errmsg = ex.Message;
            }
            return captcha;
        }

        public LoginResponse Login(string username, string password, string captcha, string capkey)
        {
            
            _username = username; _password = password;

            LoginParams pa = new LoginParams()
            {
                username = _username,
                password = _password,
                cvalue = captcha,
                ckey = capkey
            };
            string body = JsonConvert.SerializeObject(pa);
            //string body = "{\"username\" : \""+_username+"\", \"password\" : \""+_password+ "\", \"cvalue\": \""+capcha+"\", \"ckey\": \""+capkey+"\"}";

            
            string result = SendRequest(_baseurl + "/security-taxpayer/authenticate", body, "POST", "", "", "", true);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);
            string token = loginResponse.token;
            _token = token;
            return loginResponse;
        }

        /// <summary>
        /// lấy mỗi lần chỉ tối đa 50 dòng.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public PurchaseResponse GetPurchase(DateTime date1, DateTime date2)
        {
            string uri = "/query/invoices/purchase?sort=tdlap:desc,khmshdon:asc,shdon:desc&size=50&search=tdlap=ge="
                + ObjectAndString.ObjectToString(date1)
                + "T00:00:00;tdlap=le="
                + ObjectAndString.ObjectToString(date2)
                + "T23:59:59;ttxly==5";
            string response = GET_WITH_TOKEN(uri);
            PurchaseResponse pur = JsonConvert.DeserializeObject<PurchaseResponse>(response);
            return pur;
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="uri">Đường dẫn hàm, không kể _baseurl</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public string POST_WITH_TOKEN(string uri, string request)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = SendRequest(_baseurl + uri, request, "POST", "", _token, null, true);
            return postResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">Đường dẫn không kể baseurl</param>
        /// <returns></returns>
        public string GET_WITH_TOKEN(string uri)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = SendRequest(_baseurl + uri, "", "GET", "", _token, null, true);
            return postResult;
        }

        /// <summary>
        /// Thành công trả về null.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="saveFile"></param>
        /// <returns></returns>
        public string GET_DOWNLOAD(string uri, string saveFile)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = Download(_baseurl + uri, "", "GET", "", _token, true, saveFile);
            return postResult;
        }

        public string GET(string uri)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = SendRequest(_baseurl + uri, "", "GET", "", "", null, true);
            return postResult;
        }

        CookieContainer cookies = new CookieContainer();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="full_url"></param>
        /// <param name="content"></param>
        /// <param name="method"></param>
        /// <param name="username"></param>
        /// <param name="password_or_bearertoken"></param>
        /// <param name="viettel_token"></param>
        /// <param name="allowAutoRedirect"></param>
        /// <returns></returns>
        public string SendRequest(string full_url, string content, string method, string username, string password_or_bearertoken, string viettel_token, bool allowAutoRedirect)
            //string proxyIP = "", int port = 0, bool use_ssl = false)
        {
            try
            {
                if (full_url == null)
                {
                    throw new ArgumentNullException("full_url");
                }

                method = method != null ? method.ToUpper() : "";

                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(full_url);

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
                    if (full_url.ToLower().EndsWith("InvoiceAPI/InvoiceWS/cancelTransactionInvoice".ToLower())
                        || full_url.ToLower().EndsWith("InvoiceAPI/InvoiceWS/updatePaymentStatus".ToLower())
                        || full_url.ToLower().EndsWith("InvoiceAPI/InvoiceWS/createExchangeInvoiceFile".ToLower())
                        || full_url.ToLower().EndsWith("InvoiceAPI/InvoiceWS/searchInvoiceByTransactionUuid".ToLower())
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
                    if (WriteExtraLog)
                    {
                        Logger.WriteToLog("" + method + "" + full_url + "\n" + content, "HoadondientuAPI");
                    }
                }

                if (request.ContentType == null) request.ContentType = "application/json";
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

                if (WriteExtraLog)
                {
                    Logger.WriteToLog("\nRESPONSE:\n" + responseFromServer, "HoadondientuAPI");
                }
                return responseFromServer;
            }
            catch (WebException webex)
            {
                if (webex.Response == null) return null;
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    if (WriteExtraLog)
                    {
                        Logger.WriteToLog("\nRESPONSE:\n" + text, "HoadondientuAPI");
                    }
                    return text;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="full_url"></param>
        /// <param name="content"></param>
        /// <param name="method"></param>
        /// <param name="username"></param>
        /// <param name="password_or_bearertoken"></param>
        /// <param name="allowAutoRedirect"></param>
        /// <returns></returns>
        public string Download(string full_url, string content, string method, string username, string password_or_bearertoken, bool allowAutoRedirect,
            string filePath)
        {
            try
            {
                if (full_url == null)
                {
                    throw new ArgumentNullException("full_url");
                }

                method = method != null ? method.ToUpper() : "";

                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(full_url);
                
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
                        request.Credentials = CredentialCache.DefaultNetworkCredentials;
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
                    byte[] byteArray = Encoding.UTF8.GetBytes(content);

                    if (request.ContentType == null) request.ContentType = "application/json";
                    request.Accept = "application/json";
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;

                    ServicePointManager.ServerCertificateValidationCallback = delegate (object sender,
                        X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    
                    // Get the request stream.
                    Stream requestStream = request.GetRequestStream();

                    // Write the data to the request stream.
                    requestStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    requestStream.Close();
                    if (WriteExtraLog)
                    {
                        Logger.WriteToLog("" + method + "" + full_url + "\n" + content, "HoadondientuAPI");
                    }
                }

                
                var response = (HttpWebResponse)request.GetResponse();
                cookies.Add(response.Cookies);

                Stream responseStream = null;
                //StreamReader reader = null;
                
                try
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        const int BUFFER_SIZE = 16 * 1024;
                        {
                            using (responseStream = response.GetResponseStream())
                            {
                                var buffer = new byte[BUFFER_SIZE];
                                int bytesRead;
                                do
                                {
                                    bytesRead = responseStream.Read(buffer, 0, BUFFER_SIZE);
                                    fileStream.Write(buffer, 0, bytesRead);
                                } while (bytesRead > 0);
                            }

                            fileStream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (responseStream != null)
                    {
                        responseStream.Close();
                    }
                    response.Close();
                }
                return null;
            }
            catch (WebException webex)
            {
                if (webex.Response == null) return null;
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    const int BUFFER_SIZE = 16 * 1024;
                    var buffer = new byte[BUFFER_SIZE];
                    int bytesRead;
                    do
                    {
                        bytesRead = respStream.Read(buffer, 0, BUFFER_SIZE);
                        fileStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead > 0);
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "I don't know.";
        }


        
        /// <summary>
        /// Tải Excel danh sách hóa đơn bán ra.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="saveFile"></param>
        /// <returns></returns>
        public string DownloadInvoiceList_Excel(DateTime date1, DateTime date2, string saveFile)
        {
            try
            {
                string apiLink = "/query/invoices/export-excel?sort=tdlap:desc,khmshdon:asc,shdon:desc&search=tdlap=ge="
                + ObjectAndString.ObjectToString(date1)
                + "T00:00:00;tdlap=le="
                + ObjectAndString.ObjectToString(date2)
                + "T23:59:59";

                string result = GET_DOWNLOAD(apiLink, saveFile);

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Tải Excel danh sách hóa đơn mua vào.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="saveFile"></param>
        /// <returns></returns>
        public string DownloadPurchaseInvoiceList_Excel(DateTime date1, DateTime date2, string saveFile)
        {
            try
            {
                string apiLink = "/query/invoices/export-excel-sold?sort=tdlap:desc,khmshdon:asc,shdon:desc&search=tdlap=ge="
                + ObjectAndString.ObjectToString(date1)
                + "T00:00:00;tdlap=le="
                + ObjectAndString.ObjectToString(date2)
                + "T23:59:59;ttxly==5%20%20%20%20&type=purchase";

                string result = GET_DOWNLOAD(apiLink, saveFile);

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Tải xuống 1 hóa đơn gồm html và xml... đóng trong 1 file zip.
        /// </summary>
        /// <param name="nbmst">Người bán mst</param>
        /// <param name="khhdon">Ký hiệu hóa đơn C23TMH</param>
        /// <param name="shdon">số hóa đơn</param>
        /// <param name="khmshdon">ký hiệu mẫu số</param>
        /// <param name="saveFile"></param>
        /// <returns></returns>
        public string DownloadInvoiceXml_zip(string nbmst, string khhdon, string shdon, string khmshdon, string saveFile)
        {
            try
            {
                string apiLink = "/query/invoices/export-xml?nbmst=" + nbmst
                + "&khhdon=" + khhdon
                + "&shdon=" + shdon
                + "&khmshdon=" + khmshdon;

                string result = GET_DOWNLOAD(apiLink, saveFile);

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        /// <summary>
        /// Chạy kiểm tra kết nối. Nếu ổn trả về null. Có lỗi trả về câu thông báo.
        /// </summary>
        /// <returns></returns>
        public string CheckConnection()
        {
            return "";
            //V6Return v6Return;
            //string result = POST_CREATE_INVOICE("", false, out v6Return);
            //if (v6Return.RESULT_ERROR_MESSAGE != null && v6Return.RESULT_ERROR_MESSAGE.Contains("JSON_PARSE_ERROR"))
            //{
            //    return null;
            //}
            //else
            //{
            //    return v6Return.RESULT_ERROR_MESSAGE;
            //}
        }
        
        
        
        
        

    }
}
