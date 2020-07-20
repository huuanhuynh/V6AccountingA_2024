using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using V6SignToken;
using V6ThuePost.MONET_Objects.GetInvoice;
using V6ThuePost.MONET_Objects.Request;
using V6ThuePost.MONET_Objects.Response;
using V6ThuePost.ResponseObjects;
using V6ThuePost.ViettelObjects;
using V6Tools;

namespace V6ThuePostMonetApi
{
    public class MONET_WS
    {
        private string _baseurl = "";
        /// <summary>
        /// Tên người sử dụng trên hệ thống hóa đơn điện tử.
        /// </summary>
        private readonly string _username;
        private readonly string _password;
        /// <summary>
        /// Mã số thuế của doanh nghiệp.
        /// </summary>
        private string _codetax;

        private readonly RequestManager requestManager = new RequestManager();

        public MONET_WS(string baseurl, string username, string password, string codetax)
        {
            _baseurl = baseurl;
            if (!_baseurl.EndsWith("/")) _baseurl += "/";
            _username = username;
            _password = password;
            _codetax = codetax;
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="uri">Đường dẫn hàm, không kể _baseurl</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public string POST(string uri, string request)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            HttpWebResponse respone = requestManager.SendPOSTRequest(_baseurl + uri, request, _username, _password, true);
            return requestManager.GetResponseContent(respone);
        }

        public string GET(string uri)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            HttpWebResponse respone = requestManager.SendGETRequest(_baseurl + uri, _username, _password, true);
            return requestManager.GetResponseContent(respone);
        }

        private const string create_link0 = @"InvoiceAPI/InvoiceWS/createInvoice"; // + /MST
        private const string cancel_link = @"/api/invoice/delete";


        /// <summary>
        /// Hủy hóa đơn. Chưa test, chưa phân tích result.
        /// </summary>
        /// <param name="token">token_value</param>
        /// <param name="invcCode">Số hóa đơn.</param>
        /// <param name="invcSign">Ký hiệu hóa đơn (vd: AA/19E,..).</param>
        /// <param name="invcSample">Mẫu số hóa đơn (vđ: 01GTTKT0/001).</param>
        /// <param name="description">Ghi chú nội bộ.</param>
        /// <param name="returnDocNo">Số biên bản thu hồi/xóa hóa đơn.</param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public MONET_DELETE_Response POST_DELETE(string token, string invcCode, string invcSign, string invcSample, string description, string returnDocNo, out V6Return v6return)
        {
            v6return = new V6Return();
            MONET_DELETE_Response responseObj = null;
            MONET_DELETE_Request requestObj = new MONET_DELETE_Request()
            {
                token = token,
                invcCode = invcCode,
                invcSign = invcSign,
                invcSample = invcSample,
                description = description,
                returnDocNo = returnDocNo
            };
            try
            {
                string requestStr = requestObj.ToJson();
                string resultStr = POST(cancel_link, requestStr);
                v6return.RESULT_STRING = resultStr;
                responseObj = JsonConvert.DeserializeObject<MONET_DELETE_Response>(resultStr);
                v6return.RESULT_OBJECT = responseObj;
                if (responseObj.isSuccess)
                {
                    v6return.RESULT_MESSAGE = responseObj.errorMessage;
                }
                else
                {
                    v6return.RESULT_ERROR_MESSAGE = responseObj.errorMessage;
                }
            }
            catch (Exception ex)
            {
                v6return.RESULT_ERROR_MESSAGE = ex.Message;
            }
            
            return responseObj;
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

            MONET_DOWNLOAD_Response objGetFile = new MONET_DOWNLOAD_Response();
            
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
            string result = GET(apiLink);

            MONET_DOWNLOAD_Response objFile = JsonConvert.DeserializeObject<MONET_DOWNLOAD_Response>(result);
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
            string result = GET(apiLink);

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
        /// <param name="methodlink">InvoiceAPI/InvoiceWS/createExchangeInvoiceFile</param>
        /// <param name="invoiceNo">Số hóa đơn hệ thống trả về.</param>
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
            
            string result = GET(methodlink + parameters);

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
        /// <param name="methodlink">InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile (getInvoiceRepresentationFile url part.)</param>
        /// <param name="invoiceNo">Số hóa đơn hệ thống trả về.</param>
        /// <param name="pattern"></param>
        /// <param name="savefolder"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDF(string codeTax, string methodlink, string invoiceNo, string pattern, string savefolder)
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

            string result = POST(methodlink, request);

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

        public string CheckConnection(string create_link)
        {
            V6Return v6return;
            MONET_ADD_Response result = POST_NEW(create_link, "", out v6return);
            //Phân tích result
            string message = "";

            if (result.errorMessage.Contains("POST DATA EMPTY"))
            {
                message = null;
            }
            else
            {
                message = v6return.RESULT_ERROR_MESSAGE;
            }
            
            return message;
        }


        public MONET_CHECK_SIGNED_Response check_signed(string token, string oid)
        {
            MONET_CHECK_SIGNED_Response responseObj = null;
            MONET_CHECK_SIGNED_Request requestObj = new MONET_CHECK_SIGNED_Request()
            {
                token = token, oid = oid
            };
            try
            {
                string requestStr = requestObj.ToJson();
                string resultStr = POST("/api/invoice/check_signed", requestStr);
                responseObj = JsonConvert.DeserializeObject<MONET_CHECK_SIGNED_Response>(resultStr);
            }
            catch (Exception ex)
            {
                return new MONET_CHECK_SIGNED_Response() {errorMessage = ex.Message};
            }

            return responseObj;
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
            string result = POST("InvoiceAPI/InvoiceUtilsWS/getListInvoiceDataControl", json);
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
            string result = POST("InvoiceAPI/InvoiceUtilsWS/getInvoices/" + _codetax, json);
            return result;
        }

        public string POST_DRAFT(MONET_WS _V6Http, string jsonBody)
        {
            string result = null;
            
            return result;
        }

        /// <summary>
        /// Gửi hóa đơn mới.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public MONET_ADD_Response POST_NEW1(string jsonBody, out V6Return v6return)
        {
            string url = "/api/invoice/add_type_1";
            return POST_NEW(url, jsonBody, out v6return);
        }

        /// <summary>
        /// Gửi hóa đơn mới.
        /// </summary>
        /// <param name="uri">Đường dẫn hàm</param>
        /// <param name="jsonBody"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public MONET_ADD_Response POST_NEW(string uri, string jsonBody, out V6Return v6return)
        {
            MONET_ADD_Response objResult = null;
            v6return = new V6Return();
            string resultString = null;
            try
            {
                resultString = POST(uri, jsonBody);
                v6return.RESULT_STRING = resultString;
                objResult = JsonConvert.DeserializeObject<MONET_ADD_Response>(resultString);
                v6return.RESULT_OBJECT = objResult;
                if (objResult.isSuccess)
                {
                    v6return.SO_HD = objResult.invoiceNo;
                    v6return.ID = objResult.oid;
                }
                else
                {
                    v6return.RESULT_ERROR_MESSAGE = objResult.errorMessage;
                }
            }
            catch (Exception ex)
            {
                resultString = ex.Message;
                v6return.RESULT_ERROR_MESSAGE = ex.Message;
            }
            Logger.WriteToLog("Program.POST_NEW " + resultString);
            return objResult;
        }
        
        /// <summary>
        /// Hàm giống tạo mới nhưng có khác biệt trong dữ liệu.
        /// </summary>
        /// <param name="_createInvoiceUrl"></param>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public string POST_REPLACE(string _createInvoiceUrl, string jsonBody)
        {
            string result;
            try
            {
                result = POST(_createInvoiceUrl, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("MonetWS.POST_REPLACE " + result);
            return result;
        }

        /// <summary>
        /// Gửi điều chỉnh hóa đơn.
        /// </summary>
        /// <param name="uri">Đường dẫn hàm</param>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public string POST_EDIT(string uri, string jsonBody)
        {
            string result;
            try
            {
                result = POST(uri, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("MonetlWS.POST_EDIT " + result);
            return result;
        }

        public MONET_ADD_Response POST_NEW_TOKEN(string uri, string jsonBody, string templateCode, string token_serial,
            out V6Return v6return)
        {
            MONET_ADD_Response objResult = null;
            v6return = new V6Return();
            string resultString = null;
            try
            {
                resultString = POST(uri, jsonBody);
                v6return.RESULT_STRING = resultString;
                objResult = JsonConvert.DeserializeObject<MONET_ADD_Response>(resultString);
                v6return.RESULT_OBJECT = objResult;
                if (objResult.isSuccess)
                {
                    v6return.SO_HD = objResult.invoiceNo;
                    v6return.ID = objResult.oid;
                }
                else
                {
                    v6return.RESULT_ERROR_MESSAGE = objResult.errorMessage;
                }
                
            }
            catch (Exception ex)
            {
                resultString = ex.Message;
                v6return.RESULT_ERROR_MESSAGE = ex.Message;
            }
            Logger.WriteToLog("Program.POST_NEW " + resultString);
            return objResult;

            //string result = null;
            //CreateInvoiceResponse responseObject = CreateInvoiceUsbTokenGetHash(json, out result);

            //if (responseObject.result != null)
            //{
            //    V6Sign v6sign = new V6Sign();
            //    string sign = v6sign.Sign(responseObject.result.hashString, token_serial);
            //    string result2 = CreateInvoiceUsbTokenInsertSignature(_codetax, templateCode, responseObject.result.hashString, sign);
            //    responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result2);
            //    return responseObject;
            //}
            //else
            //{
            //    Logger.WriteToLog("POST_NEW_TOKEN: " + result);
            //    return responseObject;
            //    //return "{\"errorCode\": \"POST1_RESULT_NULL\",\"description\": \"Lấy hash null.\",\"result\": null}";
            //}
        }

        public string CreateInvoiceUsbTokenGetHash_Sign0(string json, string templateCode, string token_serial)
        {
            string result = null;
            string result2 = null;
            result = POST("InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
            CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);

            if (responseObject.result != null)
            {
                V6Sign v6sign = new V6Sign();
                string sign = v6sign.Sign(responseObject.result.hashString, token_serial);
                result2 = CreateInvoiceUsbTokenInsertSignature(_codetax, templateCode, responseObject.result.hashString, sign);
            }
            else
            {
                Logger.WriteToLog("" + result);
                return "{\"errorCode\": \"POST1_RESULT_NULL\",\"description\": \"Lấy hash null.\",\"result\": null}";
            }

            return result2;
        }


        public CreateInvoiceResponse CreateInvoiceUsbTokenGetHash(string json, out string result)
        {
            result = POST("InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
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
            string methodlink = "InvoiceAPI/InvoiceWS/createInvoiceUsbTokenInsertSignature";
            string request = 
@"{
""supplierTaxCode"":""" + supplierTaxCode + @""",
""templateCode"":""" + templateCode + @""",
""hashString"":""" + hashString + @""",
""signature"":""" + signature + @"""
}";

            string result = POST(methodlink, request);

            return result;
        }
    }
}
