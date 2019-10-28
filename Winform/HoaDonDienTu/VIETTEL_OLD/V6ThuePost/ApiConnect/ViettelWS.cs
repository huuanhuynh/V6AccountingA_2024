using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using Newtonsoft.Json;
using V6ThuePost.ViettelObjects;

namespace V6ThuePostViettelApi
{
    public class ViettelWS
    {
        private string _baseurl = "";
        private readonly string _username;
        private readonly string _password;

        private readonly RequestManager requestManager = new RequestManager();

        public ViettelWS(string baseurl, string username, string password)
        {
            _baseurl = baseurl;
            if (!_baseurl.EndsWith("/")) _baseurl += "/";
            _username = username;
            _password = password;
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

        private const string cancel_link = @"InvoiceAPI/InvoiceWS/cancelTransactionInvoice";

        /// <summary>
        /// Chưa thành công!!!!!
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="strIssueDate"></param>
        /// <param name="additionalReferenceDesc"></param>
        /// <param name="additionalReferenceDate"></param>
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
            string result = POST(cancel_link, request);

            return result;
        }

        public string DownloadInvoiceZip(string codeTax, string uri, string savefolder)
        {
            //try
            {
                //string userPass = ConfigurationManager.AppSettings["UserPass"].ToString();
                //string codeTax = ConfigurationManager.AppSettings["CodeTax"].ToString();
                
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

                string request = string.Empty;
                //string result = CreateRequest.webRequest(apiLink, request, autStr, "GET", contentType);
                string result = GET(apiLink);
                int startIndex = result.IndexOf(@"fileName"":""") + ((@"""fileName"":""").Length);
                int length = result.IndexOf(@""",""fileToBytes") - startIndex;
                string fileName = result.Substring(startIndex, length);

                ZipFileResponse objFile = JsonConvert.DeserializeObject<ZipFileResponse>(result);
                string path = Path.Combine(savefolder, fileName + ".zip");
                File.WriteAllBytes(path, objFile.fileToBytes);

                //MessageBox.Show("File in " + path);
                return path;
            }
            //catch (Exception ex)
            {
                //MessageBox.Show("NOK " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Download bản chuyển đổi.
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="methodlink">InvoiceAPI/InvoiceWS/createExchangeInvoiceFile</param>
        /// <param name="invoiceNo">Số hóa đơn hệ thống Viettel trả về.</param>
        /// <param name="strIssueDate"></param>
        /// <param name="savefolder"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDFexchange(string codeTax, string methodlink, string invoiceNo, string strIssueDate, string savefolder)
        {
            //try
            {
                //methodlink = "InvoiceAPI/InvoiceWS/createExchangeInvoiceFile";

                //GetFileRequestE objGetFile = new GetFileRequestE()
                //{
                //    supplierTaxCode = codeTax,
                //    invoiceNo = invoiceNo,
                //    strIssueDate = strIssueDate,
                //    exchangeUser = _username
                //};
                string parameters =
                    string.Format("?supplierTaxCode={0}&invoiceNo={1}&strIssueDate={2}&exchangeUser={3}", codeTax, invoiceNo, strIssueDate, _username);
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

                string result = GET(methodlink + parameters);
                int startIndex = result.IndexOf(@"fileName"":""") + ((@"""fileName"":""").Length);
                int length = result.IndexOf(@""",""fileToBytes") - startIndex;
                string fileName = result.Substring(startIndex, length);

                PDFFileResponse objFile = JsonConvert.DeserializeObject<PDFFileResponse>(result);
                string path = Path.Combine(savefolder, fileName + ".pdf");
                if (!File.Exists(path))
                {
                    File.WriteAllBytes(path, objFile.fileToBytes);
                }
                //MessageBox.Show("File in " + path);
                return path;
            }
            //catch (Exception ex)
            {
                //MessageBox.Show("NOK " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Download bản thể hiện.
        /// </summary>
        /// <param name="codeTax"></param>
        /// <param name="methodlink">InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile (getInvoiceRepresentationFile url part.)</param>
        /// <param name="invoiceNo">Số hóa đơn hệ thống Viettel trả về.</param>
        /// <param name="pattern"></param>
        /// <param name="savefolder"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDF(string codeTax, string methodlink, string invoiceNo, string pattern, string savefolder)
        {
            //try
            {
                //string apiLink = _baseurl + methodlink;

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
                int startIndex = result.IndexOf(@"fileName"":""") + ((@"""fileName"":""").Length);
                int length = result.IndexOf(@""",""fileToBytes") - startIndex;
                string fileName = result.Substring(startIndex, length);

                PDFFileResponse objFile = JsonConvert.DeserializeObject<PDFFileResponse>(result);
                string path = Path.Combine(savefolder, fileName + ".pdf");
                if (!File.Exists(path))
                {
                    File.WriteAllBytes(path, objFile.fileToBytes);
                }
                //MessageBox.Show("File in " + path);
                return path;
            }
            //catch (Exception ex)
            {
                //MessageBox.Show("NOK " + ex.Message);
            }
            return null;
        }
    }
}
