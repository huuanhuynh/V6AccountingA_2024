// Decompiled with JetBrains decompiler
// Type: EasyInvoice.Client.Services.EasyService
// Assembly: EasyInvoice.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D591C1A-17CB-4CA4-A650-8459834356C1
// Assembly location: E:\Copy\Code\HoaDonDienTu\SOFT_DREAM_VNPTSTYLE\EasyInvoice.Client (.NET)\EasyInvoice.Client.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using EasyInvoice.Json;

namespace V6EasyInvoice.Client.Services
{
    public class EasyService
    {
        private string _configDownloadDirectory = "./EasyInvoice Download";
        private static readonly Dictionary<ApiEnum, string> API_LINK_DIC;
        private EasyClient _easyClient;

        static EasyService()
        {
            Dictionary<ApiEnum, string> dictionary = new Dictionary<ApiEnum, string>();
            int num1 = 15;
            string str1 = "api/publish/externalGetDigestForReplacement";
            dictionary[(ApiEnum)num1] = str1;
            int num2 = 17;
            string str2 = "api/publish/externalWrapAndLaunchReplacement";
            dictionary[(ApiEnum)num2] = str2;
            int num3 = 18;
            string str3 = "api/publish/externalGetDigestForImportation";
            dictionary[(ApiEnum)num3] = str3;
            int num4 = 19;
            string str4 = "api/publish/externalWrapAndLaunchImportation";
            dictionary[(ApiEnum)num4] = str4;
            int num5 = 20;
            string str5 = "api/publish/externalGetDigestForAdjustment";
            dictionary[(ApiEnum)num5] = str5;
            int num6 = 22;
            string str6 = "api/publish/externalWrapAndLaunchAdjustment";
            dictionary[(ApiEnum)num6] = str6;
            int num7 = 23;
            string str7 = "api/publish/issueInvoices";
            dictionary[(ApiEnum)num7] = str7;
            int num8 = 24;
            string str8 = "api/publish/externalWrapAndLaunchForIssuance";
            dictionary[(ApiEnum)num8] = str8;
            int num9 = 4;
            string str9 = "api/publish/importInvoice";
            dictionary[(ApiEnum)num9] = str9;
            int num10 = 6;
            string str_importAndIssueInvoice = "api/publish/importAndIssueInvoice";
            dictionary[(ApiEnum)num10] = str_importAndIssueInvoice;
            int num11 = 0;
            string str11 = "api/business/adjustInvoice";
            dictionary[(ApiEnum)num11] = str11;
            int num12 = 2;
            string str12 = "api/business/replaceInvoice";
            dictionary[(ApiEnum)num12] = str12;
            int num13 = 9;
            string str13 = "api/publish/issueInvoices";
            dictionary[(ApiEnum)num13] = str13;
            int num14 = 1;
            string str14 = "api/business/cancelInvoice";
            dictionary[(ApiEnum)num14] = str14;
            int num15 = 8;
            string str15 = "api/publish/getInvoicesByIkeys";
            dictionary[(ApiEnum)num15] = str15;
            int num16 = 16;
            string str16 = "api/business/importUnsignedReplacement";
            dictionary[(ApiEnum)num16] = str16;
            int num17 = 21;
            string str17 = "api/business/importUnsignedAdjustment";
            dictionary[(ApiEnum)num17] = str17;
            int num18 = 27;
            string str18 = "api/publish/getInvoicePdf";
            dictionary[(ApiEnum)num18] = str18;
            int num19 = 13;
            string str19 = "api/business/createInvoiceStrip";
            dictionary[(ApiEnum)num19] = str19;
            EasyService.API_LINK_DIC = dictionary;
        }

        public EasyService()
        {
            this.InitializeEasyClientUsingConfigInfo();
        }

        public Response ServerImportInvoices(Request request, bool issue = false, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            try
            {
                request.XmlData = EasyService.HandleExtraTags(request.XmlData);
                ApiEnum index = issue ? ApiEnum.ImportAndIssue : ApiEnum.Import;
                return this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[index], (object)request);
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
        }

        public Response ServerAdjustInvoice(Request request, bool issue = false, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            try
            {
                request.XmlData = EasyService.HandleExtraTags(request.XmlData);
                ApiEnum index = issue ? ApiEnum.Adjust : ApiEnum.ClientAdjustTemp;
                return this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[index], (object)request);
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
        }

        public Response ServerReplaceInvoice(Request request, bool issue = false, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            try
            {
                request.XmlData = EasyService.HandleExtraTags(request.XmlData);
                ApiEnum index = issue ? ApiEnum.Replace : ApiEnum.ClientReplaceTemp;
                return this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[index], (object)request);
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
        }

        public Response ServerIssueInvoices(Request request, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            try
            {
                return this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[ApiEnum.Issue], (object)request);
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
        }

        public Response ClientImportInvoices(Request request, string token_serial, bool issue = false, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            Response response;
            try
            {
                request.XmlData = EasyService.HandleExtraTags(request.XmlData);
                X509Certificate2 signCert = (X509Certificate2)null;
                if (issue)
                    request.CertString = EasyService.GetCertString(token_serial, out signCert);
                ApiEnum index = issue ? ApiEnum.ClientImport : ApiEnum.Import;
                response = this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[index], (object)request);
                if (issue)
                    response = this.ClientSignHash(signCert, response, ApiEnum.ClientLaunchImportation, request);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
            return response;
        }

        public Response ClientAdjustInvoice(Request request, string token_serial, bool issue = false, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            Response response;
            try
            {
                request.XmlData = EasyService.HandleExtraTags(request.XmlData);
                X509Certificate2 signCert = (X509Certificate2)null;
                if (issue)
                    request.CertString = EasyService.GetCertString(token_serial, out signCert);
                ApiEnum index = issue ? ApiEnum.ClientAdjust : ApiEnum.ClientAdjustTemp;
                response = this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[index], (object)request);
                if (issue)
                    response = this.ClientSignHash(signCert, response, ApiEnum.ClientLaunchAdjustment, request);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
            return response;
        }

        public Response ClientReplaceInvoice(Request request, string token_serial, bool issue = false, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            Response response;
            try
            {
                request.XmlData = EasyService.HandleExtraTags(request.XmlData);
                X509Certificate2 signCert = (X509Certificate2)null;
                if (issue)
                    request.CertString = EasyService.GetCertString(token_serial, out signCert);
                ApiEnum index = issue ? ApiEnum.ClientReplace : ApiEnum.ClientReplaceTemp;
                response = this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[index], (object)request);
                if (issue)
                    response = this.ClientSignHash(signCert, response, ApiEnum.ClientLaunchReplacement, request);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
            return response;
        }

        public Response ClientIssueInvoices(Request request, string token_serial, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            Response response1;
            try
            {
                X509Certificate2 signCert;
                request.CertString = EasyService.GetCertString(token_serial, out signCert);
                if (signCert == null || request.CertString == null)
                {
                    response1 = new Response()
                    {
                        Status = new int?(),
                        Message = "Không có chứng thư được chọn"
                    };
                }
                else
                {
                    Response response2 = this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[ApiEnum.ClientIssue], (object)request);
                    response1 = this.ClientSignHash(signCert, response2, ApiEnum.ClientLaunchIssuance, request);
                }
            }
            catch (Exception ex)
            {
                response1 = new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
            return response1;
        }

        public Response GetInvoicesByIkeys(Request request, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            try
            {
                return this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[ApiEnum.GetByIkeys], (object)request);
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
        }

        public Response GetInvoicePdf(Request request, string path, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            Response response = new Response();
            if (!Utils.IsNullOrWhiteSpace(path))
            {
                if (path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                    return new Response()
                    {
                        Status = new int?(),
                        Message = "Đường dẫn tệp tin chứa ký tự không hợp lệ: " + path
                    };
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                catch (Exception ex)
                {
                    return new Response()
                    {
                        Status = new int?(),
                        Message = ex.Message
                    };
                }
            }
            try
            {
                EasyResponse easyResponse = this._easyClient.PostJsonObject(EasyService.API_LINK_DIC[ApiEnum.GetPdf], (object)request);
                if (easyResponse.HttpStatusCode == HttpStatusCode.OK)
                {
                    ContentDisposition contentDisposition = new ContentDisposition(easyResponse.Headers["Content-Disposition"]);
                    string fileName = Path.GetFileName(path);
                    if (Utils.IsNullOrWhiteSpace(fileName))
                        fileName = contentDisposition.FileName;
                    string desiredFileFullPath = EasyService.GetDesiredFileFullPath(Path.Combine(Path.GetDirectoryName(path), fileName), (string)null);
                    EasyService.SaveBytesToFile(desiredFileFullPath, easyResponse.RawBytes);
                    response.Status = new int?(2);
                    response.Message = desiredFileFullPath;
                }
                else
                {
                    response.Status = new int?((int)easyResponse.HttpStatusCode);
                    response.Message = "Có lỗi xả ra: " + easyResponse.Content;
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
            return response;
        }

        public Response CancelInvoice(Request request, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            try
            {
                return this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[ApiEnum.Cancel], (object)request);
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
        }

        public Response CreateReservedInvoices(Request request, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return new Response()
                {
                    Status = new int?(),
                    Message = str
                };
            try
            {
                return this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[ApiEnum.CreateInvoiceStrip], (object)request);
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = new int?(),
                    Message = ex.Message
                };
            }
        }

        public EasyResponse PostJsonObject(object request, string resource, string host = null, string id = null, string password = null)
        {
            string message = this.CheckCredentialsParams(host, id, password);
            if (!Utils.IsNullOrWhiteSpace(message))
                throw new ArgumentException(message);
            return this._easyClient.PostJsonObject(resource, request);
        }

        public EasyResponse<TResult> PostJsonObject<TResult>(object request, string resource, string host = null, string id = null, string password = null)
        {
            EasyResponse easyResponse1 = this.PostJsonObject(request, resource, host, id, password);
            EasyResponse<TResult> easyResponse2 = new EasyResponse<TResult>();
            WebHeaderCollection headers = easyResponse1.Headers;
            easyResponse2.Headers = headers;
            long contentLength = easyResponse1.ContentLength;
            easyResponse2.ContentLength = contentLength;
            string contentEncoding = easyResponse1.ContentEncoding;
            easyResponse2.ContentEncoding = contentEncoding;
            string content = easyResponse1.Content;
            easyResponse2.Content = content;
            int num = (int)easyResponse1.HttpStatusCode;
            easyResponse2.HttpStatusCode = (HttpStatusCode)num;
            string statusDescription = easyResponse1.StatusDescription;
            easyResponse2.StatusDescription = statusDescription;
            byte[] rawBytes = easyResponse1.RawBytes;
            easyResponse2.RawBytes = rawBytes;
            string errorMessage = easyResponse1.ErrorMessage;
            easyResponse2.ErrorMessage = errorMessage;
            string contentType = easyResponse1.ContentType;
            easyResponse2.ContentType = contentType;
            Exception exception = easyResponse1.Exception;
            easyResponse2.Exception = exception;
            TResult result = default(TResult);
            easyResponse2.Data = result;
            EasyResponse<TResult> easyResponse3 = easyResponse2;
            if (Utils.IsValidJson(easyResponse1.Content))
                easyResponse3.Data = JsonConvert.DeserializeObject<TResult>(easyResponse1.Content);
            else if (easyResponse3.Exception == null)
            {
                easyResponse3.ErrorMessage = string.Format("Response content is not valid json:\r\n{0}", (object)easyResponse3.Content);
                easyResponse3.Exception = new Exception(easyResponse3.ErrorMessage);
            }
            return easyResponse3;
        }

        private Response ClientSignHash(X509Certificate2 cert, Response response, ApiEnum resource, Request request)
        {
            if (response != null)
            {
                int? status = response.Status;
                int num = 2;
                if (status.GetValueOrDefault() == num & status.HasValue)
                {
                    request.Signature = new Dictionary<string, string>();
                    ResponseData data = response.Data;
                    foreach (KeyValuePair<string, string> keyValuePair in (data != null ? data.DigestData : (Dictionary<string, string>)null) ?? request.Signature)
                    {
                        string hash = keyValuePair.Value;
                        string str = EasyService.SignHash(cert, hash);
                        request.Signature.Add(keyValuePair.Key, str);
                    }
                    response = this._easyClient.PostJsonObject<Response>(EasyService.API_LINK_DIC[resource], (object)request);
                }
            }
            return response;
        }

        private static string HandleExtraTags(string xmlData)
        {
            if (Utils.IsNullOrWhiteSpace(xmlData))
                throw new ArgumentNullException("XmlData đang bỏ trống");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlData);
            XmlNodeList xmlNodeList1 = xmlDocument.SelectNodes("/Invoices/Inv/Invoice");
            XmlNode[] xmlNodeArray1 = xmlNodeList1 != null ? Enumerable.ToArray<XmlNode>(Enumerable.Cast<XmlNode>((IEnumerable)xmlNodeList1)) : (XmlNode[])null;
            if (xmlNodeArray1 == null || !Enumerable.Any<XmlNode>((IEnumerable<XmlNode>)xmlNodeArray1))
            {
                XmlNodeList xmlNodeList2 = xmlDocument.SelectNodes("/ReplaceInv");
                xmlNodeArray1 = xmlNodeList2 != null ? Enumerable.ToArray<XmlNode>(Enumerable.Cast<XmlNode>((IEnumerable)xmlNodeList2)) : (XmlNode[])null;
            }
            if (xmlNodeArray1 == null || !Enumerable.Any<XmlNode>((IEnumerable<XmlNode>)xmlNodeArray1))
            {
                XmlNodeList xmlNodeList2 = xmlDocument.SelectNodes("/AdjustInv");
                xmlNodeArray1 = xmlNodeList2 != null ? Enumerable.ToArray<XmlNode>(Enumerable.Cast<XmlNode>((IEnumerable)xmlNodeList2)) : (XmlNode[])null;
            }
            if (xmlNodeArray1 != null && Enumerable.Any<XmlNode>((IEnumerable<XmlNode>)xmlNodeArray1))
            {
                foreach (XmlNode xmlNode1 in xmlNodeArray1)
                {
                    XmlNode xmlNode2 = xmlNode1.SelectSingleNode("Extra");
                    if (xmlNode2 != null && xmlNode2.HasChildNodes)
                    {
                        XmlNode[] xmlNodeArray2 = Enumerable.ToArray<XmlNode>(Enumerable.Where<XmlNode>(Enumerable.Cast<XmlNode>((IEnumerable)xmlNode2.ChildNodes), (Func<XmlNode, bool>)(node => node.NodeType == XmlNodeType.Element)));
                        if (Enumerable.Any<XmlNode>((IEnumerable<XmlNode>)xmlNodeArray2))
                        {
                            Dictionary<string, string> kvp = new Dictionary<string, string>();
                            Utils.ForEach<XmlNode>((IEnumerable<XmlNode>)xmlNodeArray2, (Action<XmlNode>)(node =>
                            {
                                if (kvp.ContainsKey(node.Name))
                                    throw new XmlException("Invoice extra chứa thẻ '" + node.Name + "' bị trùng lặp");
                                kvp.Add(node.Name, node.InnerText);
                            }));
                            xmlNode2.InnerText = JsonConvert.SerializeObject((object)kvp);
                        }
                    }
                    XmlNodeList xmlNodeList2 = xmlNode1.SelectNodes("Products/Product");
                    XmlNode[] xmlNodeArray3 = xmlNodeList2 != null ? Enumerable.ToArray<XmlNode>(Enumerable.Cast<XmlNode>((IEnumerable)xmlNodeList2)) : (XmlNode[])null;
                    if (xmlNodeArray3 == null || !Enumerable.Any<XmlNode>((IEnumerable<XmlNode>)xmlNodeArray3))
                        throw new Exception("Không tìm thấy thẻ Product: " + xmlNode1.OuterXml);
                    foreach (XmlNode xmlNode3 in xmlNodeArray3)
                    {
                        XmlNode xmlNode4 = xmlNode3.SelectSingleNode("Extra");
                        if (xmlNode4 != null && xmlNode4.HasChildNodes)
                        {
                            XmlNode[] xmlNodeArray2 = Enumerable.ToArray<XmlNode>(Enumerable.Where<XmlNode>(Enumerable.Cast<XmlNode>((IEnumerable)xmlNode4.ChildNodes), (Func<XmlNode, bool>)(node => node.NodeType == XmlNodeType.Element)));
                            if (Enumerable.Any<XmlNode>((IEnumerable<XmlNode>)xmlNodeArray2))
                            {
                                Dictionary<string, string> kvp = new Dictionary<string, string>();
                                Utils.ForEach<XmlNode>((IEnumerable<XmlNode>)xmlNodeArray2, (Action<XmlNode>)(node =>
                                {
                                    if (kvp.ContainsKey(node.Name))
                                        throw new XmlException("Product extra chứa thẻ '" + node.Name + "' bị trùng lặp");
                                    kvp.Add(node.Name, node.InnerText);
                                }));
                                xmlNode4.InnerText = JsonConvert.SerializeObject((object)kvp);
                            }
                        }
                    }
                }
            }
            return xmlDocument.OuterXml;
        }

        private string CheckCredentialsParams(string host = null, string id = null, string password = null)
        {
            try
            {
                string str = (string)null;
                int num = 0;
                if (!Utils.IsNullOrWhiteSpace(host))
                    ++num;
                if (!Utils.IsNullOrWhiteSpace(id))
                    num += 2;
                if (!Utils.IsNullOrWhiteSpace(password))
                    num += 4;
                if (num == 7)
                    this._easyClient = new EasyClient(host, id, password);
                switch (num)
                {
                    case 0:
                        if (this._easyClient == null)
                            this.InitializeEasyClientUsingConfigInfo();
                        if (this._easyClient == null)
                        {
                            str = "Chưa khai báo thông tin kết nối đến hệ thống hoá đơn điện tử Easy Invoice";
                            break;
                        }
                        break;
                    case 1:
                        str = "Chưa khai báo đường dẫn (host) hoá đơn điện tử Easy Invoice";
                        break;
                    case 2:
                        str = "Chưa khai báo tên đăng nhập hoá đơn điện tử Easy Invoice";
                        break;
                    case 3:
                        str = "Chưa khai báo đường dẫn (host) và tên đăng nhập hoá đơn điện tử Easy Invoice";
                        break;
                    case 4:
                        str = "Chưa khai báo mật khẩu hoá đơn điện tử Easy Invoice";
                        break;
                    case 5:
                        str = "Chưa khai báo đường dẫn (host) và mật khẩu hoá đơn điện tử Easy Invoice";
                        break;
                    case 6:
                        str = "Chưa khai báo tên đăng nhập và mật khẩu hoá đơn điện tử Easy Invoice";
                        break;
                }
                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void InitializeEasyClientUsingConfigInfo()
        {
            try
            {
                string str1 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "EasyInvoice.Client.xml");
                if (!System.IO.File.Exists(str1))
                    return;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(str1);
                XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/configuration/downloadDirectory");
                string path = xmlNode1 != null ? xmlNode1.InnerText : (string)null;
                if (!Utils.IsNullOrWhiteSpace(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        this._configDownloadDirectory = path;
                    }
                    catch
                    {
                        Directory.CreateDirectory(this._configDownloadDirectory);
                    }
                }
                XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/configuration/host");
                string str2 = xmlNode2 != null ? xmlNode2.InnerText : (string)null;
                XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/configuration/id");
                string str3 = xmlNode3 != null ? xmlNode3.InnerText : (string)null;
                XmlNode xmlNode4 = xmlDocument.SelectSingleNode("/configuration/password");
                string str4 = xmlNode4 != null ? xmlNode4.InnerText : (string)null;
                if (Utils.IsNullOrWhiteSpace(str2) || Utils.IsNullOrWhiteSpace(str3) || Utils.IsNullOrWhiteSpace(str4))
                    return;
                if (Cryptography.IsEncrypted(str3))
                {
                    str2 = Cryptography.Decrypt(str2);
                    str3 = Cryptography.Decrypt(str3);
                    str4 = Cryptography.Decrypt(str4);
                }
                else
                {
                    try
                    {
                        string str5 = Cryptography.Encrypt(str2);
                        string str6 = Cryptography.Encrypt(str3);
                        string str7 = Cryptography.Encrypt(str4);
                        xmlDocument.SelectSingleNode("/configuration/host").InnerText = str5;
                        xmlDocument.SelectSingleNode("/configuration/id").InnerText = str6;
                        xmlDocument.SelectSingleNode("/configuration/password").InnerText = str7;
                        xmlDocument.Save(str1);
                    }
                    catch
                    {
                    }
                }
                this._easyClient = new EasyClient(str2, str3, str4);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khởi tạo EasyClient", ex);
            }
        }

        private static void SaveBytesToFile(string fileFullPath, byte[] bytes)
        {
            if (bytes.Length == 0)
                return;
            using (Stream stream = (Stream)new MemoryStream(bytes))
            {
                using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        private static string GetDesiredFileFullPath(string fileFullPath, string remark = null)
        {
            string withoutExtension = Path.GetFileNameWithoutExtension(fileFullPath);
            string extension = Path.GetExtension(fileFullPath);
            string directoryName = Path.GetDirectoryName(fileFullPath);
            string path = Path.Combine(directoryName, withoutExtension + remark + extension);
            int num = 1;
            while (System.IO.File.Exists(path))
            {
                path = Path.Combine(directoryName, string.Format("{0}{1} ({2}){3}", (object)withoutExtension, (object)remark, (object)num, (object)extension));
                ++num;
            }
            return path;
        }

        private static X509Certificate2 SelectCertificate()
        {
            X509Certificate2 x509Certificate2_1 = (X509Certificate2)null;
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            int num = 0;
            x509Store.Open((OpenFlags)num);
            foreach (X509Certificate2 x509Certificate2_2 in X509Certificate2UI.SelectFromCollection(x509Store.Certificates, "Danh sách chứng thư", "Vui lòng chọn chứng thư", X509SelectionFlag.SingleSelection))
                x509Certificate2_1 = x509Certificate2_2;
            return x509Certificate2_1;
        }

        private static X509Certificate2 SelectCertificate(string token_serial)
        {
            X509Certificate2 x509Certificate2_1 = (X509Certificate2)null;
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            int num = 0;
            x509Store.Open((OpenFlags)num);
            foreach (X509Certificate2 certificate in x509Store.Certificates)
            {
                if (certificate.SerialNumber != null && certificate.SerialNumber.ToUpper() == token_serial.ToUpper())
                {
                    x509Certificate2_1 = certificate;
                    break;
                }
            }
            return x509Certificate2_1;
        }

        private static string GetCertString(string token_serial, out X509Certificate2 signCert)
        {
            signCert = EasyService.SelectCertificate(token_serial);
            if (signCert == null)
                return (string)null;
            return Convert.ToBase64String(signCert.RawData);
        }

        private static string SignHash(X509Certificate2 cert, string hash)
        {
            byte[] rgbHash = Convert.FromBase64String(hash);
            return Convert.ToBase64String(((RSACryptoServiceProvider)cert.PrivateKey).SignHash(rgbHash, "SHA1"));
        }
    }
}
