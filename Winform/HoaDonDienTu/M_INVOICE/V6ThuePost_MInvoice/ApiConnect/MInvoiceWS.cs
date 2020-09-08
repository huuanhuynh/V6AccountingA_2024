using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using V6ThuePost.MInvoiceObject;
using V6ThuePost.MInvoiceObject.Request;
using V6ThuePost.MInvoiceObject.Response;
using V6ThuePost.ResponseObjects;
using V6Tools;

namespace V6ThuePostMInvoiceApi
{
    public class MInvoiceWS
    {
        private readonly string _baseurl;
        /// <summary>
        /// Tên đăng nhập hệ thống.
        /// </summary>
        private readonly string _username;
        private readonly string _password;
        private readonly string _ma_dvcs;
        private readonly string _logintoken;
        public bool IsLoggedIn { get { return !string.IsNullOrEmpty(_logintoken); } }
        /// <summary>
        /// Mã số thuế của doanh nghiệp.
        /// </summary>
        private string _codetax;

        private readonly RequestManager requestManager = new RequestManager();
        private string _cancel_link = "api/Invoice/xoaboHD";
        private const string _createInvoiceUrl = "api/InvoiceAPI/Save";
        private const string _editLink = "api/InvoiceAPI/Save";
        private const string _modifylinkT = "api/InvoiceAPI/DcTang";
        private const string _modifylinkG = "api/InvoiceAPI/DcGiam";
        private const string _replaceInvoiceUrl = "api/InvoiceAPI/Save";

        public MInvoiceWS(string baseurl, string username, string password, string ma_dvcs, string codetax)
        {
            _baseurl = baseurl;
            if (!_baseurl.EndsWith("/")) _baseurl += "/";
            _username = username;
            _password = password;
            _ma_dvcs = ma_dvcs;
            _codetax = codetax;
            _logintoken = Login(_username, _password, ma_dvcs);
        }

        private string Login(string username, string password, string ma_dvcs)
        {
            string request = "{\"username\" : \"" + username + "\",\"password\" : \"" + password + "\",\"ma_dvcs\" : \"" + ma_dvcs + "\"}";
            string result = POST0("api/Account/Login", request);
            LoginResponse objFile = JsonConvert.DeserializeObject<LoginResponse>(result);
            if (string.IsNullOrEmpty(objFile.error) && !string.IsNullOrEmpty(objFile.token))
            {
                return objFile.token;
            }
            else
            {
                
            }
            return null;
        }

        /// <summary>
        /// POST login
        /// </summary>
        /// <param name="uri">Đường dẫn hàm, không kể _baseurl</param>
        /// <param name="request"></param>
        /// <returns></returns>
        private string POST0(string uri, string request)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            HttpWebResponse respone = requestManager.SendPOSTRequest(_baseurl + uri, request, "", "", true);
            return requestManager.GetResponseContent(respone);
        }

        /// <summary>
        /// POST với username password
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

        /// <summary>
        /// POST Authorization Bearer
        /// </summary>
        /// <param name="uri">Đường dẫn hàm, không kể _baseurl</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public string POST_Bearer(string uri, string request)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            HttpWebResponse respone = requestManager.SendPOSTRequest(_baseurl + uri, request, "", _logintoken, true);
            return requestManager.GetResponseContent(respone);
        }

        public string GET(string uri)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            HttpWebResponse respone = requestManager.SendGETRequest(_baseurl + uri, _username, _password, true);
            return requestManager.GetResponseContent(respone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">Đường dẫn hàm, không kể _baseurl</param>
        /// <returns></returns>
        public string GET_Bearer(string uri)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            HttpWebResponse respone = requestManager.SendGETRequest(_baseurl + uri, "", _logintoken, true);
            return requestManager.GetResponseContent(respone);
        }

        /// <summary>
        /// Hủy hóa đơn.
        /// </summary>
        /// <param name="inv_InvoiceAuth_id"></param>
        /// <param name="sovb"></param>
        /// <param name="ngayvb"></param>
        /// <param name="ghi_chu"></param>
        /// <returns></returns>
        public string POST_CANCEL(string inv_InvoiceAuth_id, string sovb, DateTime ngayvb, string ghi_chu)
        {
            string request =
                "{\"inv_InvoiceAuth_id\":\"" + inv_InvoiceAuth_id + "\",\"sovb\":\"" + sovb + "\",\"ngayvb\":\""+ngayvb.ToString("yyyy-MM-dd")+"\",\"ghi_chu\":\""+ghi_chu+"\"}";

            string result = POST_Bearer(_cancel_link, request);

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
            string path = Path.Combine(savefolder, "fileName" + ".zip");
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
            File.WriteAllBytes(path, null);

            return path;
        }

        //public string DownloadInvoiceZip(string codeTax, string uri, string savefolder)
        //{
        //    if (uri.StartsWith("/")) uri = uri.Substring(1);
        //    string apiLink = _baseurl + uri;

        //    GetFileRequest objGetFile = new GetFileRequest();
        //    objGetFile.fileType = "zip";
        //    objGetFile.invoiceNo = "BR/18E0000014";
        //    objGetFile.strIssueDate = "20180320152309";

        //    string getData = "?supplierTaxCode=" + codeTax +
        //                     "&invoiceNo=" + objGetFile.invoiceNo +
        //                     "&fileType=" + objGetFile.fileType +
        //                     "&strIssueDate=" + objGetFile.strIssueDate;
        //    apiLink += getData;
        //    //string autStr = CreateRequest.Base64Encode(userPass);
        //    //string contentType = "application/x-www-form-urlencoded";
        //    //string request = string.Empty;
        //    //string result = CreateRequest.webRequest(apiLink, request, autStr, "GET", contentType);
        //    string result = GET(apiLink);

        //    ZipFileResponse objFile = JsonConvert.DeserializeObject<ZipFileResponse>(result);
        //    string fileName = objFile.fileName;
        //    if (string.IsNullOrEmpty(fileName) || objFile.fileToBytes == null)
        //    {
        //        throw new Exception("Download no file!");
        //    }

        //    string path = Path.Combine(savefolder, fileName + ".zip");
        //    if (File.Exists(path))
        //    {
        //        try
        //        {
        //            File.Delete(path);
        //        }
        //        catch
        //        {
        //            //
        //        }
        //    }
        //    File.WriteAllBytes(path, objFile.fileToBytes);

        //    return path;
        //}

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
           

            string result = GET(methodlink + parameters);

            
            
            if (string.IsNullOrEmpty("fileName") || "objFile.fileToBytes" == null)
            {
                throw new Exception("Download no file!");
            }
            string path = Path.Combine(savefolder, "fileName" + ".pdf");

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
                File.WriteAllBytes(path, null);
            }
            
            return path;
        }

        /// <summary>
        /// Download bản thể hiện.
        /// </summary>
        /// <param name="id">inv_InvoiceAuth_id</param>
        /// <param name="savefolder">Nơi lưu file</param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public string DownloadInvoicePDF(string id, string savefolder)
        {
            string uri = string.Format("/api/Invoice/Preview?id={0}", id);
            string response = GET_Bearer(uri);
            //"There is no row at position 0."
            string fileName = "objFile.fileName";
            if (string.IsNullOrEmpty(fileName) || "objFile.fileToBytes" == null)
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
                File.WriteAllBytes(path, null);
            }

            return path;
        }

        /// <summary>
        /// Ok trả về null hoặc rỗng.
        /// </summary>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string CheckConnection(out V6Return v6Return)
        {
            var responseObject = POST_NEW(new MInvoicePostObject(), out v6Return);
            if (v6Return.RESULT_STRING.Contains("windowid\":null")) return null;
            return v6Return.RESULT_STRING;
        }
        
        /// <summary>
        /// Gửi hóa đơn mới.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="v6return">Thông tin trả về cho V6.</param>
        /// <returns></returns>
        public MInvoiceResponse POST_NEW(MInvoicePostObject jsonBody, out V6Return v6return)
        {
            string result;
            MInvoiceResponse responseObject = null;
            v6return = new V6Return();
            try
            {
                jsonBody.editmode = "1";
                result = POST_Bearer(_createInvoiceUrl, jsonBody.ToJson());
                v6return.RESULT_STRING = result;

                try
                {
                    responseObject = JsonConvert.DeserializeObject<MInvoiceResponse>(result);

                    v6return.RESULT_OBJECT = responseObject;
                    v6return.RESULT_MESSAGE = "" + responseObject.Message;
                    v6return.RESULT_ERROR_MESSAGE = "" + responseObject.error + responseObject.Message;

                    if (responseObject.ok == "true" && responseObject.data != null &&
                        responseObject.data.ContainsKey("inv_invoiceNumber")
                        && !string.IsNullOrEmpty((string)responseObject.data["inv_invoiceNumber"]))
                    {
                        v6return.SO_HD = "" + responseObject.data["inv_invoiceNumber"];
                        v6return.ID = "" + responseObject.data["inv_InvoiceAuth_id"];
                        if (responseObject.data.ContainsKey("sobaomat")) v6return.SECRET_CODE = "" + responseObject.data["sobaomat"];
                    }
                    else
                    {
                        
                    }
                }
                catch (Exception ex)
                {
                    v6return.RESULT_ERROR_MESSAGE = "Convert response object error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                v6return.RESULT_ERROR_CODE = "WS_EXCEPTION";
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;

                result = ex.Message;
            }
            Logger.WriteToLog("WS.POST_NEW " + result);
            return responseObject;
        }

        public MInvoiceResponse POST_NEW(string jsonBody, out V6Return v6return)
        {
            string result;
            MInvoiceResponse responseObject = null;
            v6return = new V6Return();
            try
            {
                MInvoicePostObject jsonBodyObject = JsonConvert.DeserializeObject<MInvoicePostObject>(jsonBody);
                if(jsonBodyObject.editmode != "1") throw new Exception("editmode != \"1\"");
                result = POST_Bearer(_createInvoiceUrl, jsonBody);
                v6return.RESULT_STRING = result;

                try
                {
                    responseObject = JsonConvert.DeserializeObject<MInvoiceResponse>(result);

                    v6return.RESULT_OBJECT = responseObject;
                    v6return.RESULT_MESSAGE = "" + responseObject.Message;
                    v6return.RESULT_ERROR_MESSAGE = "" + responseObject.error + responseObject.Message;

                    if (responseObject.ok == "true" && responseObject.data != null &&
                        responseObject.data.ContainsKey("inv_invoiceNumber")
                        && !string.IsNullOrEmpty((string)responseObject.data["inv_invoiceNumber"]))
                    {
                        v6return.ID = "" + responseObject.data["inv_InvoiceAuth_id"];
                        v6return.SO_HD = "" + responseObject.data["inv_invoiceNumber"];
                        //v6return.SECRET_CODE = "" + responseObject.data["inv_InvoiceCode_id"];
                        if (responseObject.data.ContainsKey("sobaomat")) v6return.SECRET_CODE = "" + responseObject.data["sobaomat"];
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    v6return.RESULT_ERROR_MESSAGE = "Convert response object error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                v6return.RESULT_ERROR_CODE = "WS_EXCEPTION";
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;

                result = ex.Message;
            }
            Logger.WriteToLog("WS.POST_NEW " + result);
            return responseObject;
        }

        public MInvoiceResponse POST_NEW_TOKEN(MInvoicePostObject jsonBody, out V6Return v6return)
        {
            throw new Exception("POST_NEW_TOKEN NOT SUPPORTED");
        }

        /// <summary>
        /// Hàm giống tạo mới nhưng có khác biệt trong dữ liệu.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public string POST_REPLACE(MInvoicePostObject jsonBody)
        {
            string result;
            try
            {
                result = POST(_replaceInvoiceUrl, jsonBody.ToJson());
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("WS.POST_REPLACE " + result);
            return result;
        }

        /// <summary>
        /// Gửi điều chỉnh hóa đơn.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public MInvoiceResponse POST_EDIT(MInvoicePostObject jsonBody, out V6Return v6return)
        {
            string result;
            MInvoiceResponse responseObject = null;
            v6return = new V6Return();
            try
            {
                jsonBody.editmode = "2";
                //jsonBody.data[0]["inv_invoiceNumber"] = invoiceNumber;
                result = POST_Bearer(_createInvoiceUrl, jsonBody.ToJson());
                v6return.RESULT_STRING = result;

                try
                {
                    responseObject = JsonConvert.DeserializeObject<MInvoiceResponse>(result);

                    v6return.RESULT_OBJECT = responseObject;
                    v6return.RESULT_MESSAGE = "" + responseObject.Message;
                    v6return.RESULT_ERROR_MESSAGE = "" + responseObject.error + responseObject.Message;

                    if (responseObject.ok == "true" && responseObject.data != null &&
                        responseObject.data.ContainsKey("inv_invoiceNumber")
                        && !string.IsNullOrEmpty((string)responseObject.data["inv_invoiceNumber"]))
                    {
                        v6return.ID = "" + responseObject.data["inv_InvoiceAuth_id"];
                        v6return.SO_HD = "" + responseObject.data["inv_invoiceNumber"];
                        //v6return.SECRET_CODE = "" + responseObject.data["inv_InvoiceCode_id"];
                        if (responseObject.data.ContainsKey("sobaomat")) v6return.SECRET_CODE = "" + responseObject.data["sobaomat"];
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    v6return.RESULT_ERROR_MESSAGE = "Convert response object error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                v6return.RESULT_ERROR_CODE = "WS_EXCEPTION";
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;

                result = ex.Message;
            }
            Logger.WriteToLog("WS.POST_EDIT " + result);
            return responseObject;
        }

        public string POST_MODIFY_t(string jsonBody, out V6Return v6return)
        {
            string result;
            v6return = new V6Return();
            try
            {
                result = POST_Bearer(_modifylinkT, jsonBody);
                v6return.RESULT_STRING = result;

                try
                {
                    MInvoiceResponse responseObject = JsonConvert.DeserializeObject<MInvoiceResponse>(result);

                    v6return.RESULT_OBJECT = responseObject;
                    v6return.RESULT_MESSAGE = "" + responseObject.Message;
                    v6return.RESULT_ERROR_MESSAGE = "" + responseObject.error + responseObject.Message;

                    if (responseObject.ok == "true" && responseObject.data != null &&
                        responseObject.data.ContainsKey("inv_invoiceNumber")
                        && !string.IsNullOrEmpty((string)responseObject.data["inv_invoiceNumber"]))
                    {
                        v6return.ID = "" + responseObject.data["inv_InvoiceAuth_id"];
                        v6return.SO_HD = "" + responseObject.data["inv_invoiceNumber"];
                        //v6return.SECRET_CODE = "" + responseObject.data["inv_InvoiceCode_id"];
                        if (responseObject.data.ContainsKey("sobaomat")) v6return.SECRET_CODE = "" + responseObject.data["sobaomat"];
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    v6return.RESULT_ERROR_MESSAGE = "Convert response object error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                v6return.RESULT_ERROR_CODE = "WS_EXCEPTION";
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;

                result = ex.Message;
            }
            Logger.WriteToLog("WS.POST_NEW " + result);
            return result;
        }

        //public CreateInvoiceResponse POST_NEW_TOKEN(string json, string templateCode, string token_serial)
        //{
        //    string result = null;
        //    CreateInvoiceResponse responseObject = CreateInvoiceUsbTokenGetHash(json, out result);

        //    if (responseObject.result != null)
        //    {
        //        V6Sign v6sign = new V6Sign();
        //        string sign = v6sign.Sign(responseObject.result.hashString, token_serial);
        //        string result2 = CreateInvoiceUsbTokenInsertSignature(_codetax, templateCode, responseObject.result.hashString, sign);
        //        responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result2);
        //        return responseObject;
        //    }
        //    else
        //    {
        //        Logger.WriteToLog("POST_NEW_TOKEN: " + result);
        //        return responseObject;
        //        //return "{\"errorCode\": \"POST1_RESULT_NULL\",\"description\": \"Lấy hash null.\",\"result\": null}";
        //    }
        //}

        //public string CreateInvoiceUsbTokenGetHash_Sign(string json, string templateCode, string token_serial)
        //{
        //    string result = null;
        //    string result2 = null;
        //    result = POST("InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
        //    CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);

        //    if (responseObject.result != null)
        //    {
        //        V6Sign v6sign = new V6Sign();
        //        string sign = v6sign.Sign(responseObject.result.hashString, token_serial);
        //        result2 = CreateInvoiceUsbTokenInsertSignature(_codetax, templateCode, responseObject.result.hashString, sign);
        //    }
        //    else
        //    {
        //        Logger.WriteToLog("" + result);
        //        return "{\"errorCode\": \"POST1_RESULT_NULL\",\"description\": \"Lấy hash null.\",\"result\": null}";
        //    }

        //    return result2;
        //}


        //public CreateInvoiceResponse CreateInvoiceUsbTokenGetHash(string json, out string result)
        //{
        //    result = POST("InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/" + _codetax, json);
        //    CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
        //    return responseObject;
        //}

//        /// <summary>
//        /// Gửi chữ ký số
//        /// </summary>
//        /// <param name="supplierTaxCode">Mã số thuế của doanh nghiệp/chi nhánh phát hành hóa đơn. Mẫu 1: 0312770607 Mẫu 2: 0312770607-001</param>
//        /// <param name="templateCode">Mã mẫu hóa đơn: 01GTKT0/001</param>
//        /// <param name="hashString">Chuỗi hash trả về từ hàm CreateInvoiceUsbTokenGetHash.</param>
//        /// <param name="signature">Chuỗi đã ký.</param>
//        /// <returns></returns>
//        public string CreateInvoiceUsbTokenInsertSignature(string supplierTaxCode, string templateCode, string hashString, string signature)
//        {
//            string methodlink = "InvoiceAPI/InvoiceWS/createInvoiceUsbTokenInsertSignature";
//            string request = 
//@"{
//""supplierTaxCode"":""" + supplierTaxCode + @""",
//""templateCode"":""" + templateCode + @""",
//""hashString"":""" + hashString + @""",
//""signature"":""" + signature + @"""
//}";

//            string result = POST(methodlink, request);

//            return result;
//        }
    }
}
