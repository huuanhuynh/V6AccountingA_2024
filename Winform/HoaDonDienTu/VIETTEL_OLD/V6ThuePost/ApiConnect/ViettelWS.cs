using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using V6SignToken;
using V6ThuePost.ResponseObjects;
using V6ThuePost.ViettelObjects;
using V6ThuePost.ViettelObjects.GetInvoice;
using V6Tools;

namespace V6ThuePostViettelApi
{
    public class ViettelWS
    {
        private string _baseurl = "";
        /// <summary>
        /// Tên người sử dụng trên hệ thống Sinvoice (Viettel), thường là codetax
        /// </summary>
        private readonly string _username;
        private readonly string _password;
        /// <summary>
        /// Mã số thuế của doanh nghiệp.
        /// </summary>
        private string _codetax;
        private const string _create_link = @"InvoiceAPI/InvoiceWS/createInvoice"; // + MST
        private const string _cancel_link = @"InvoiceAPI/InvoiceWS/cancelTransactionInvoice";

        private readonly RequestManager requestManager = new RequestManager();

        public ViettelWS(string baseurl, string username, string password, string codetax)
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
        private string POST(string uri, string request)
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
            string result = POST(_cancel_link, request);

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
            string result = GET(apiLink);

            ZipFileResponse objFile = JsonConvert.DeserializeObject<ZipFileResponse>(result);
            string fileName = objFile.fileName;
            if (string.IsNullOrEmpty(fileName) || objFile.fileToBytes == null)
            {
                throw new Exception("Download no file!");
            }

            string path = Path.Combine(savefolder, fileName + ".zip");
            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch
            {
            }
            finally
            {
                if (!File.Exists(path)) File.WriteAllBytes(path, objFile.fileToBytes);
            }

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
            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch
            {
            }
            finally
            {
                if (!File.Exists(path)) File.WriteAllBytes(path, objFile.fileToBytes);
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
        /// <param name="v6Return">Thông tin trả về.</param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDF(string codeTax, string invoiceNo, string pattern, string savefolder, out V6Return v6Return)
        {
            v6Return = new V6Return();
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

            string result = POST("InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile", request);
            v6Return.RESULT_STRING = result;
            PDFFileResponse responseObject = JsonConvert.DeserializeObject<PDFFileResponse>(result);
            v6Return.RESULT_OBJECT = responseObject;
            string fileName = responseObject.fileName;
            if (string.IsNullOrEmpty(fileName) || responseObject.fileToBytes == null)
            {
                v6Return.RESULT_ERROR_MESSAGE = "Download no file!";
                throw new Exception("Download no file!");
            }

            v6Return.PATH = Path.Combine(savefolder, fileName + ".pdf");

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
        /// Download bản chuyển đổi.
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="invoiceNo">Số hóa đơn hệ thống Viettel trả về.</param>
        /// <param name="strIssueDate"></param>
        /// <param name="savefolder"></param>
        /// <param name="v6Return"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDFexchange(string codeTax, string invoiceNo, string strIssueDate, string savefolder, out V6Return v6Return)
        {
            v6Return = new V6Return();
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

            string result = GET("InvoiceAPI/InvoiceWS/createExchangeInvoiceFile" + parameters);
            v6Return.RESULT_STRING = result;
            PDFFileResponse responseObject = JsonConvert.DeserializeObject<PDFFileResponse>(result);
            v6Return.RESULT_OBJECT = responseObject;
            string fileName = responseObject.fileName;
            if (string.IsNullOrEmpty(fileName) || responseObject.fileToBytes == null)
            {
                v6Return.RESULT_ERROR_MESSAGE = "Download no file! " + result;
                throw new Exception("Download no file!");
            }

            v6Return.PATH = Path.Combine(savefolder, fileName + "_E.pdf");
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
        /// Hàm kiểm tra kết nối. Nếu thành công trả về rỗng hoặc null.
        /// </summary>
        /// <returns></returns>
        public string CheckConnection()
        {
            string result = POST(_create_link + "/" + _codetax, "");
            
            if (result != null && result.StartsWith("<html>") && result.Contains("<div class=\"header\">Request failed.</div>"))
            {
                return "REQUEST FAILED.";
            }
            CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
            if (!string.IsNullOrEmpty(responseObject.description) && responseObject.description.Contains("Phải chọn loại template hóa đơn"))
            {
                return null;
            }
            else
            {
                return result;
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
        
        /// <summary>
        /// Gửi hóa đơn mới.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public string POST_NEW(string jsonBody)
        {
            string result;
            try
            {
                result = POST(_create_link + "/" + _codetax, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_NEW " + result);
            return result;
        }

        /// <summary>
        /// Tạo mới hoặc sửa hóa đơn nháp.
        /// </summary>
        /// <param name="taxCode">Mã số thuế doanh nghiệp sử dụng HĐĐT.</param>
        /// <param name="jsonBody">Dữ liệu hóa đơn</param>
        /// <returns></returns>
        public string POST_DRAFT(string taxCode, string jsonBody)
        {
            string result;
            try
            {
                result = POST("/InvoiceAPI/InvoiceWS/createOrUpdateInvoiceDraft/" + taxCode, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_NEW " + result);
            return result;
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
            Logger.WriteToLog("ViettelWS.POST_REPLACE " + result);
            return result;
        }

        /// <summary>
        /// Gửi điều chỉnh hóa đơn.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public string POST_EDIT(string jsonBody)
        {
            string result;
            try
            {
                result = POST(_create_link + "/" + _codetax, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("ViettelWS.POST_EDIT " + result);
            return result;
        }

        public CreateInvoiceResponse POST_NEW_TOKEN(string json, string templateCode, string token_serial)
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

        public string CreateInvoiceUsbTokenGetHash_Sign(string json, string templateCode, string token_serial)
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
