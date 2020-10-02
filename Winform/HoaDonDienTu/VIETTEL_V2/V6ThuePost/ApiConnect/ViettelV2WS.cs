﻿using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using V6SignToken;
using V6ThuePost.ResponseObjects;
using V6ThuePost.ViettelV2Objects;
using V6ThuePost.ViettelV2Objects.GetInvoice;
using V6ThuePost.ViettelV2Objects.Response;
using V6Tools;

namespace V6ThuePostViettelApi
{
    public class ViettelWS
    {
        /// <summary>
        /// https://api-vinvoice.viettel.vn
        /// </summary>
        private string _baseurl = "";

        /// <summary>
        /// Tên người sử dụng trên hệ thống Sinvoice (Viettel), thường là codetax
        /// </summary>
        private readonly string _username;
        private readonly string _password;
        private string _viettel_token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX25hbWUiOiIwMTAwMTA5MTA2LTcxNSIsInNjb3BlIjpbIm9wZW5pZCJdLCJleHAiOjE2MDA5MTk2NTEsInR5cGUiOjEsImlhdCI6MTYwMDkxOTM1MSwiaW52b2ljZV9jbHVzdGVyIjoiY2x1c3RlcjEiLCJhdXRob3JpdGllcyI6WyJST0xFX1VTRVIiXSwianRpIjoiZTI1MjE2MmYtM2ZmNC00N2MzLThlZGUtMmU1YzViOTRlNzg2IiwiY2xpZW50X2lkIjoid2ViX2FwcCJ9.A7P5VzB2Tztuz1FMLUUsHZ_Rf5dE2ut7M9VloNT5P-Yo6M9WaNtYk-7xW5arKkNUf0AQBj52pTkdq3QEbkFE5FB5SGT3-DuOzjlzCm2oGg9p0Rq5GqCDk9cKR5k472fuDoV_nCr7VWaPt5bsmGLqK9qzLeB9Y94obEe08OQQjB2sprpd2ZDzsdZFNQtB5jr343WzXReLQfh8gA7RaCJJspu932KMdoBTGhlEWC1W37g7cpgNawdmPeA60H25y9O6f0ZtY0IFhn1TYzEk8ist2Hd1F3ipK9844ZUyJdY-2SQAqtzwbw9VR8OV3lsWC9iAKr9mmT_VuSjFXgpPnxSRKw";
        /// <summary>
        /// Mã số thuế của doanh nghiệp.
        /// </summary>
        private string _codetax;

        private const string create_link = @"/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createInvoice/";
        //private const string cancel_link = @"/InvoiceAPI/InvoiceWS/cancelTransactionInvoice";
        private const string cancel_link = @"/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/cancelTransactionInvoice";

        //private readonly RequestManager requestManager = new RequestManager();

        public ViettelWS(string baseurl, string username, string password, string codetax)
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
            string body = "{\"username\" : \""+_username+"\", \"password\" : \""+_password+"\"}";
            //Thread thread = new Thread(ABC_Login);
            //thread.Start();
            //Thread.Sleep(1000);
            string result = SendRequest(_baseurl + "/auth/login", body, "POST", "", "", "", true);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);
            string token = loginResponse.access_token;
            _viettel_token = token;
        }

        //private void ABC_Login()
        //{
        //    try
        //    {
        //        string body = "{\"username\" : \""+_username+"\", \"password\" : \""+_password+"\"}";
        //        string result = SendRequest(_baseurl + "/auth/login", body, "POST", "", "", "", true,
        //            "10.61.11.42", 3128, true);
        //    }
        //    catch (Exception e)
        //    {
        //        //
        //    }
        //}

        public string POST_CREATE_INVOICE(string body, out V6Return v6return)
        {
            v6return = new V6Return();
            string result = POST_VIETTEL_COOKIESTOKEN(create_link + _codetax, body);
            v6return.RESULT_STRING = result;
            try
            {
                //{"errorCode":null,"description":null,"result":{"supplierTaxCode":"0100109106-715","invoiceNo":"XL/20E0000006","transactionID":"160145663940682045","reservationCode":"PU3ZQOPMTC9VM4L"}}
                CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
                v6return.RESULT_OBJECT = responseObject;
                if (responseObject.result == null)
                {
                    v6return.RESULT_ERROR_MESSAGE = responseObject.message + " " + responseObject.data;
                }
                else
                {
                    v6return.SO_HD = responseObject.result.invoiceNo;
                    v6return.ID = responseObject.result.transactionID;
                    v6return.SECRET_CODE = responseObject.result.reservationCode;
                }
            }
            catch (Exception ex1)
            {
                v6return.RESULT_ERROR_MESSAGE = ex1.Message;
            }
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
        public string POST_VIETTEL_COOKIESTOKEN(string uri, string request)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = SendRequest(_baseurl + uri, request, "POST", "", "", _viettel_token, true);
            return postResult;
        }

        public string GET_VIETTEL_TOKEN(string uri)
        {
            if (!uri.StartsWith("/")) uri = "/" + uri;
            string postResult = SendRequest(_baseurl + uri, "", "GET", "", "", _viettel_token, true);
            return postResult;
            //HttpWebResponse respone = requestManager.SendGETRequest(_baseurl + uri, _username, _password, true);
            //return requestManager.GetResponseContent(respone);
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
                        || full_uri.ToLower().EndsWith("InvoiceAPI/InvoiceWS/updatePaymentStatus".ToLower()))
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
        /// <param name="v6return">Kết quả trả về v6</param>
        /// <returns></returns>
        public string UpdatePaymentStatus(string codeTax, string invoiceNo, string strIssueDate, string templateCode,
            string buyerEmailAddress, string paymentType, string paymentTypeName, string cusGetInvoiceRight, out V6Return v6return)
        {
            v6return = new V6Return();
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
            string result = POST_VIETTEL_COOKIESTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/updatePaymentStatus", request);
            v6return.RESULT_STRING = result;
            // Sample result
            //{"errorCode":null,"description":null,"result":true,"paymentTime":null,"paymentMethod":null}   // word
            //{\"code\":400,\"message\":\"EMAIL_INVALID\",\"data\":\"Email không hợp lệ\"}                  // test
            //{"errorCode":null,"description":null,"result":true,"paymentTime":1601463363413,"paymentMethod":"TC"}  //test
            UpdatePaymentResponse responseObject = JsonConvert.DeserializeObject<UpdatePaymentResponse>(result);
            v6return.RESULT_OBJECT = responseObject;
            if (responseObject.result)
            {
                v6return.RESULT_MESSAGE = "OK";
            }
            else if (responseObject.code != 0)
            {
                v6return.RESULT_ERROR_MESSAGE = responseObject.message + " " + responseObject.data;
            }
            else
            {
                v6return.RESULT_MESSAGE = responseObject.message;
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
        public string CancelTransactionInvoice(string codeTax, string invoiceNo, string strIssueDate, string additionalReferenceDesc, string additionalReferenceDate)
        {
            //codeTax = "0100109106";
            //invoiceNo = "AA/17E0037914";
            //strIssueDate = "20170907161438";
            //additionalReferenceDesc = "viettel_1234";
            //additionalReferenceDate = "20170907161438";

            string request =
                @"supplierTaxCode=" + codeTax
                + @"&invoiceNo=" + invoiceNo
                + @"&strIssueDate=" + strIssueDate
                + @"&additionalReferenceDesc=" + additionalReferenceDesc
                + @"&additionalReferenceDate=" + additionalReferenceDate;
            string result = POST_VIETTEL_COOKIESTOKEN(cancel_link, request);

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
            string result = GET_VIETTEL_TOKEN(apiLink);

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
            string result = GET_VIETTEL_TOKEN(apiLink);

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
        /// Download bản chuyển đổi.
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="methodlink">/services/einvoiceapplication/api/InvoiceAPI/InvoiceAPI/InvoiceWS/createExchangeInvoiceFile</param>
        /// <param name="invoiceNo">Số hóa đơn hệ thống Viettel trả về.</param>
        /// <param name="strIssueDate"></param>
        /// <param name="savefolder"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDFexchange(string codeTax, string methodlink, string invoiceNo, string strIssueDate, string savefolder)
        {
            //GetFileRequestE objGetFile = new GetFileRequestE()
            //{
            //    supplierTaxCode = codeTax,
            //    invoiceNo = invoiceNo,
            //    strIssueDate = strIssueDate,
            //    exchangeUser = _username
            //};
            string parameters =
                string.Format("?supplierTaxCode={0}&invoiceNo={1}&strIssueDate={2}&exchangeUser={3}", codeTax, invoiceNo,
                    strIssueDate, _username);
            //string q = objGetFile.ToJson();

            //q = "{" + string.Format(
            //        @"""supplierTaxCode"" : ""{0}"", ""invoiceNo"" : ""{1}"", ""strIssueDate"" : ""{2}"", ""exchangeUser"" : ""{3}""",
            //        codeTax, invoiceNo, "20170907161438", "viettel") + "}";
//                string request = @"{
//                            ""supplierTaxCode"":""" + codeTax + @""",
//                            ""invoiceNo"":""" + objGetFile.invoiceNo + @""",
//                            ""pattern"":""" + objGetFile.pattern + @""",
//                            ""transactionUuid"":""" + objGetFile.transactionUuid + @""",
//                            ""fileType"":""" + objGetFile.fileType + @"""
//                            }";

            string result = GET_VIETTEL_TOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceAPI/InvoiceWS/createExchangeInvoiceFile" + parameters);

            PDFFileResponse objFile = JsonConvert.DeserializeObject<PDFFileResponse>(result);
            string fileName = objFile.fileName;
            if (string.IsNullOrEmpty(fileName) || objFile.fileToBytes == null)
            {
                throw new Exception("Download no file!");
            }
            string path = Path.Combine(savefolder, fileName + ".pdf");

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
            if (!File.Exists(path))
            {
                File.WriteAllBytes(path, objFile.fileToBytes);
            }
            
            return path;
        }

        /// <summary>
        /// Download bản thể hiện.
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="invoiceNo">Số hóa đơn hệ thống Viettel trả về.</param>
        /// <param name="pattern"></param>
        /// <param name="savefolder"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDF(string codeTax, string invoiceNo, string pattern, string savefolder)
        {
            GetFileRequest objGetFile = new GetFileRequest();
            objGetFile.invoiceNo = invoiceNo;
            objGetFile.pattern = pattern;
            objGetFile.fileType = "pdf";
            objGetFile.transactionUuid = "";

            string request = @"{
                            ""supplierTaxCode"":""" + codeTax + @""",
                            ""invoiceNo"":""" + objGetFile.invoiceNo + @""",
                            ""pattern"":""" + objGetFile.pattern + @""",
                            ""transactionUuid"":""" + objGetFile.transactionUuid + @""",
                            ""fileType"":""" + objGetFile.fileType + @"""
                            }";

            string result = POST_VIETTEL_COOKIESTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile", request);

            PDFFileResponse objFile = JsonConvert.DeserializeObject<PDFFileResponse>(result);
            string fileName = objFile.fileName;
            if (string.IsNullOrEmpty(fileName) || objFile.fileToBytes == null)
            {
                throw new Exception("Download no file!");
            }

            string path = Path.Combine(savefolder, fileName + ".pdf");

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
            if (!File.Exists(path))
            {
                File.WriteAllBytes(path, objFile.fileToBytes);
            }

            return path;
        }

        /// <summary>
        /// Chạy kiểm tra kết nối. Nếu ổn trả về null. Có lỗi trả về câu thông báo.
        /// </summary>
        /// <returns></returns>
        public string CheckConnection()
        {
            V6Return v6return;
            string result = POST_CREATE_INVOICE("", out v6return);
            if (v6return.RESULT_ERROR_MESSAGE != null && v6return.RESULT_ERROR_MESSAGE.Contains("JSON_PARSE_ERROR"))
            {
                return null;
            }
            else
            {
                return v6return.RESULT_ERROR_MESSAGE;
            }
        }

        /// <summary>
        /// cung cấp danh sách hóa đơn theo khoảng thời gian
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public string GetListInvoiceDataControl(DateTime from, DateTime to)
        {
            GetListInvoiceDataControlParams input = new GetListInvoiceDataControlParams()
            {
                supplierTaxCode = _codetax,
                fromDate = from.ToString("dd/MM/yyyy"),
                toDate = from.ToString("dd/MM/yyyy"),
            };
            string json = input.ToJson();
            string result = POST_VIETTEL_COOKIESTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceUtilsWS/getListInvoiceDataControl", json);
            return result;
        }

        /// <summary>
        /// Lấy thông tin hóa đơn (tìm kiếm).
        /// </summary>
        /// <param name="input">Các thông tin cần thiết: startDate endDate invoiceType "02GTTT" rowPerPage pageNum templateCode "01GTKT0/001"</param>
        /// <returns>GetInvoiceResponse json</returns>
        public string GetInvoices(GetInvoiceInput input)
        {
            string json = input.ToJson();
            string result = POST_VIETTEL_COOKIESTOKEN("InvoiceAPI/InvoiceUtilsWS/getInvoices/" + _codetax, json);
            return result;
        }

        public string POST_DRAFT(string jsonBody, out V6Return v6return)
        {
            string result;
            v6return = new V6Return();
            try
            {
                result = POST_VIETTEL_COOKIESTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createOrUpdateInvoiceDraft/" + _codetax, jsonBody);
                v6return.RESULT_STRING = result;
                try
                {
                    // {"errorCode":"","description":"","result":{}}
                    // {"code":400,"message":"TRANSACTION_UUID_INVALID","data":"Transaction Uuid đã được lập hóa đơn"}
                    CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
                    v6return.RESULT_OBJECT = responseObject;
                    if (string.IsNullOrEmpty(responseObject.message))
                    {
                        v6return.SO_HD = responseObject.result.invoiceNo;
                        v6return.ID = responseObject.result.transactionID;
                        v6return.SECRET_CODE = responseObject.result.reservationCode;
                    }
                    else
                    {
                        v6return.RESULT_ERROR_MESSAGE = responseObject.message + " " + responseObject.data;
                    }
                }
                catch (Exception ex1)
                {
                    v6return.RESULT_ERROR_MESSAGE = ex1.Message;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                v6return.RESULT_ERROR_MESSAGE += ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_DRAFT " + result);
            return result;
        }
        
        /// <summary>
        /// Hàm giống tạo mới nhưng có khác biệt trong dữ liệu.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public string POST_REPLACE(string jsonBody, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = POST_CREATE_INVOICE(jsonBody, out v6return);
                v6return.RESULT_STRING = result;
                try
                {
                    //{"errorCode":null,"description":null,"result":{"supplierTaxCode":"0100109106-715","invoiceNo":"XL/20E0000006","transactionID":"160145663940682045","reservationCode":"PU3ZQOPMTC9VM4L"}}
                    CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
                    v6return.RESULT_OBJECT = responseObject;
                    if (responseObject.result == null)
                    {
                        v6return.RESULT_ERROR_MESSAGE = responseObject.message + " " + responseObject.data;
                    }
                    else
                    {
                        v6return.SO_HD = responseObject.result.invoiceNo;
                        v6return.ID = responseObject.result.transactionID;
                        v6return.SECRET_CODE = responseObject.result.reservationCode;
                    }
                }
                catch (Exception ex1)
                {
                    v6return.RESULT_ERROR_MESSAGE = ex1.Message;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                v6return.RESULT_ERROR_MESSAGE += ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_REPLACE " + result);
            return result;
        }

        /// <summary>
        /// Gửi điều chỉnh hóa đơn.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public string POST_EDIT(string jsonBody, out V6Return v6return)
        {
            string result;
            v6return = new V6Return();
            try
            {
                result = POST_VIETTEL_COOKIESTOKEN("editlink", jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                v6return.RESULT_ERROR_MESSAGE += ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_EDIT " + result);
            return result;
        }

        public CreateInvoiceResponse POST_NEW_USBTOKEN(string json, string templateCode, string token_serial)
        {
            string result = null;
            CreateInvoiceResponse responseObject = CreateInvoiceUsbTokenGetHash(json, out result);

            if (responseObject.result != null)
            {
                V6Sign v6sign = new V6Sign();
                string sign = v6sign.Sign(responseObject.result.hashString, token_serial);
                string result2 = CreateInvoiceUsbTokenInsertSignature(_codetax, templateCode, responseObject.result.hashString, sign);
                responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result2);
                return responseObject;
            }
            else
            {
                Logger.WriteToLog("POST_NEW_TOKEN: " + result);
                return responseObject;
                //return "{\"errorCode\": \"POST1_RESULT_NULL\",\"description\": \"Lấy hash null.\",\"result\": null}";
            }
        }

        public string CreateInvoiceUsbTokenGetHash_Sign(string json, string templateCode, string token_serial, out V6Return v6return)
        {
            string hash_result = null;
            string sign_result2 = null;
            v6return = new V6Return();
            hash_result = POST_VIETTEL_COOKIESTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
            v6return.RESULT_STRING = hash_result;
            CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(hash_result);
            v6return.RESULT_OBJECT = responseObject;
            if (responseObject.result != null)
            {
                V6Sign v6sign = new V6Sign();
                string sign = v6sign.Sign(responseObject.result.hashString, token_serial);
                sign_result2 = CreateInvoiceUsbTokenInsertSignature(_codetax, templateCode, responseObject.result.hashString, sign);
                v6return.RESULT_STRING = sign_result2;
                responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(sign_result2);
                v6return.RESULT_OBJECT = responseObject;
                if (responseObject.result == null)
                {
                    v6return.RESULT_ERROR_MESSAGE = responseObject.message + " " + responseObject.data;
                }
                else
                {
                    v6return.SO_HD = responseObject.result.invoiceNo;
                    v6return.ID = responseObject.result.transactionID;
                    v6return.SECRET_CODE = responseObject.result.reservationCode;
                }
            }
            else
            {
                v6return.RESULT_ERROR_MESSAGE = hash_result;
                Logger.WriteToLog("" + hash_result);
                return "{\"errorCode\": \"POST1_RESULT_NULL\",\"description\": \"Lấy hash null.\",\"result\": null}";
            }

            return sign_result2;
        }


        public CreateInvoiceResponse CreateInvoiceUsbTokenGetHash(string json, out string result)
        {
            result = POST_VIETTEL_COOKIESTOKEN("/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
            CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
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

            string result = POST_VIETTEL_COOKIESTOKEN(methodlink, request);

            return result;
        }
    }
}