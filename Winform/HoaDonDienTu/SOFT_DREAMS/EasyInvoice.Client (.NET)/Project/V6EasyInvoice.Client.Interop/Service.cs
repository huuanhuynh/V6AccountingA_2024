// Decompiled with JetBrains decompiler
// Type: EasyInvoice.Client.Interop.Service
// Assembly: EasyInvoice.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D591C1A-17CB-4CA4-A650-8459834356C1
// Assembly location: E:\Copy\Code\HoaDonDienTu\SOFT_DREAMS\EasyInvoice.Client (.NET)\EasyInvoice.Client.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EasyInvoice.Json;

namespace V6EasyInvoice.Client.Interop
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("EasyInvoice.Client.Interop.Service")]
    public class Service
    {
        private string _configDownloadDirectory = "./EasyInvoice Download";
        private const string ResponseTemplate = "<root><Status>{0}</Status><Message>{1}</Message>{2}</root>";
        private EasyClient _easyClient;
        private static readonly Dictionary<ApiEnum, string> Resources;

        static Service()
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
            string str10 = "api/publish/importAndIssueInvoice";
            dictionary[(ApiEnum)num10] = str10;
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
            Service.Resources = dictionary;
        }

        public Service()
        {
            this.InitializeEasyClientUsingConfigInfo();
        }

        public string Test(string name = null)
        {
            if (Utils.IsNullOrWhiteSpace(name))
                name = Environment.MachineName;
            return "Greetings, " + name + " (from EasyInvoice with loop :')";
        }

        public string ServerImportInvoices(string xmlRequest, int issue = 0, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                XDocument xdocument = XDocument.Parse(xmlRequest);
                string expression1 = "/root/XmlData";
                XElement xelement1 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression1);
                string expression2 = "/root/Pattern";
                XElement xelement2 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression2);
                string expression3 = "/root/Serial";
                XElement xelement3 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression3);
                Request request = new Request()
                {
                    XmlData = Service.HandleExtraTags(xelement1 != null ? xelement1.FirstNode.ToString() : (string)null),
                    Pattern = xelement2 != null ? xelement2.Value : (string)null,
                    Serial = xelement3 != null ? xelement3.Value : (string)null
                };
                ApiEnum index = issue == 0 ? ApiEnum.Import : ApiEnum.ImportAndIssue;
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[index], (object)request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string ServerAdjustInvoice(string xmlRequest, int issue = 0, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                XDocument xdocument = XDocument.Parse(xmlRequest);
                string expression1 = "/root/XmlData";
                XElement xelement1 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression1);
                string expression2 = "/root/Pattern";
                XElement xelement2 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression2);
                string expression3 = "/root/Serial";
                XElement xelement3 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression3);
                string expression4 = "/root/Ikey";
                XElement xelement4 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression4);
                Request request = new Request()
                {
                    XmlData = Service.HandleExtraTags(xelement1 != null ? xelement1.FirstNode.ToString() : (string)null),
                    Pattern = xelement2 != null ? xelement2.Value : (string)null,
                    Serial = xelement3 != null ? xelement3.Value : (string)null,
                    Ikey = xelement4 != null ? xelement4.Value : (string)null
                };
                ApiEnum index = issue == 0 ? ApiEnum.ClientAdjustTemp : ApiEnum.Adjust;
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[index], (object)request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string ServerReplaceInvoice(string xmlRequest, int issue = 0, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                XDocument xdocument = XDocument.Parse(xmlRequest);
                string expression1 = "/root/XmlData";
                XElement xelement1 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression1);
                string expression2 = "/root/Pattern";
                XElement xelement2 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression2);
                string expression3 = "/root/Serial";
                XElement xelement3 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression3);
                string expression4 = "/root/Ikey";
                XElement xelement4 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression4);
                Request request = new Request()
                {
                    XmlData = Service.HandleExtraTags(xelement1 != null ? xelement1.FirstNode.ToString() : (string)null),
                    Pattern = xelement2 != null ? xelement2.Value : (string)null,
                    Serial = xelement3 != null ? xelement3.Value : (string)null,
                    Ikey = xelement4 != null ? xelement4.Value : (string)null
                };
                ApiEnum index = issue == 0 ? ApiEnum.ClientReplaceTemp : ApiEnum.Replace;
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[index], (object)request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string ServerIssueInvoices(string xmlRequest, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                string[] strArray = Enumerable.ToArray<string>(Enumerable.Select<XElement, string>(Enumerable.Where<XElement>(System.Xml.XPath.Extensions.XPathSelectElements((XNode)XDocument.Parse(xmlRequest), "/root/Ikeys/Ikey"), (Func<XElement, bool>)(_ => !Utils.IsNullOrWhiteSpace(_.Value))), (Func<XElement, string>)(_ => _.Value)));
                Request request = new Request()
                {
                    Ikeys = strArray
                };
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[ApiEnum.Issue], (object)request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string ClientImportInvoices(string xmlRequest, int issue = 0, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                XDocument xdocument = XDocument.Parse(xmlRequest);
                string expression1 = "/root/XmlData";
                XElement xelement1 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression1);
                string expression2 = "/root/Pattern";
                XElement xelement2 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression2);
                string expression3 = "/root/Serial";
                XElement xelement3 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression3);
                Request request = new Request()
                {
                    XmlData = Service.HandleExtraTags(xelement1 != null ? xelement1.FirstNode.ToString() : (string)null),
                    Pattern = xelement2 != null ? xelement2.Value : (string)null,
                    Serial = xelement3 != null ? xelement3.Value : (string)null
                };
                X509Certificate2 signCert = (X509Certificate2)null;
                if (issue != 0)
                {
                    request.CertString = Service.GetCertString(out signCert);
                    if (signCert == null || request.CertString == null)
                    {
                        response = new Response()
                        {
                            Status = new int?(),
                            Message = "Không có chứng thư được chọn"
                        };
                        return Service.SerializeResponseToXml(response, convert);
                    }
                }
                ApiEnum index = issue != 0 ? ApiEnum.ClientImport : ApiEnum.Import;
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[index], (object)request);
                if (issue != 0)
                    response = this.ClientSignHash(signCert, response, ApiEnum.ClientLaunchImportation, request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string ClientAdjustInvoice(string xmlRequest, int issue = 0, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                XDocument xdocument = XDocument.Parse(xmlRequest);
                string expression1 = "/root/XmlData";
                XElement xelement1 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression1);
                string expression2 = "/root/Pattern";
                XElement xelement2 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression2);
                string expression3 = "/root/Serial";
                XElement xelement3 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression3);
                string expression4 = "/root/Ikey";
                XElement xelement4 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression4);
                Request request = new Request()
                {
                    XmlData = Service.HandleExtraTags(xelement1 != null ? xelement1.FirstNode.ToString() : (string)null),
                    Pattern = xelement2 != null ? xelement2.Value : (string)null,
                    Serial = xelement3 != null ? xelement3.Value : (string)null,
                    Ikey = xelement4 != null ? xelement4.Value : (string)null
                };
                X509Certificate2 signCert = (X509Certificate2)null;
                if (issue != 0)
                {
                    request.CertString = Service.GetCertString(out signCert);
                    if (signCert == null || request.CertString == null)
                    {
                        response = new Response()
                        {
                            Status = new int?(),
                            Message = "Không có chứng thư được chọn"
                        };
                        return Service.SerializeResponseToXml(response, convert);
                    }
                }
                ApiEnum index = issue != 0 ? ApiEnum.ClientAdjust : ApiEnum.ClientAdjustTemp;
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[index], (object)request);
                if (issue != 0)
                    response = this.ClientSignHash(signCert, response, ApiEnum.ClientLaunchAdjustment, request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string ClientReplaceInvoice(string xmlRequest, int issue = 0, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                XDocument xdocument = XDocument.Parse(xmlRequest);
                string expression1 = "/root/XmlData";
                XElement xelement1 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression1);
                string expression2 = "/root/Pattern";
                XElement xelement2 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression2);
                string expression3 = "/root/Serial";
                XElement xelement3 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression3);
                string expression4 = "/root/Ikey";
                XElement xelement4 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression4);
                Request request = new Request()
                {
                    XmlData = Service.HandleExtraTags(xelement1 != null ? xelement1.FirstNode.ToString() : (string)null),
                    Pattern = xelement2 != null ? xelement2.Value : (string)null,
                    Serial = xelement3 != null ? xelement3.Value : (string)null,
                    Ikey = xelement4 != null ? xelement4.Value : (string)null
                };
                X509Certificate2 signCert = (X509Certificate2)null;
                if (issue != 0)
                {
                    request.CertString = Service.GetCertString(out signCert);
                    if (signCert == null || request.CertString == null)
                    {
                        response = new Response()
                        {
                            Status = new int?(),
                            Message = "Không có chứng thư được chọn"
                        };
                        return Service.SerializeResponseToXml(response, convert);
                    }
                }
                ApiEnum index = issue != 0 ? ApiEnum.ClientReplace : ApiEnum.ClientReplaceTemp;
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[index], (object)request);
                if (issue != 0)
                    response = this.ClientSignHash(signCert, response, ApiEnum.ClientLaunchReplacement, request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string ClientIssueInvoices(string xmlRequest, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                string[] strArray = Enumerable.ToArray<string>(Enumerable.Select<XElement, string>(Enumerable.Where<XElement>(System.Xml.XPath.Extensions.XPathSelectElements((XNode)XDocument.Parse(xmlRequest), "/root/Ikeys/Ikey"), (Func<XElement, bool>)(_ => !Utils.IsNullOrWhiteSpace(_.Value))), (Func<XElement, string>)(_ => _.Value)));
                Request request1 = new Request();
                request1.Ikeys = strArray;
                X509Certificate2 signCert;
                string certString = Service.GetCertString(out signCert);
                request1.CertString = certString;
                Request request2 = request1;
                if (signCert == null || request2.CertString == null)
                {
                    response.Status = new int?();
                    response.Message = "Không có chứng thư được chọn";
                }
                else
                {
                    response = this._easyClient.PostJsonObject<Response>(Service.Resources[ApiEnum.ClientIssue], (object)request2);
                    response = this.ClientSignHash(signCert, response, ApiEnum.ClientLaunchIssuance, request2);
                }
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string GetInvoicesByIkeys(string xmlRequest, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                string[] strArray = Enumerable.ToArray<string>(Enumerable.Select<XElement, string>(Enumerable.Where<XElement>(System.Xml.XPath.Extensions.XPathSelectElements((XNode)XDocument.Parse(xmlRequest), "/root/Ikeys/Ikey"), (Func<XElement, bool>)(_ => !Utils.IsNullOrWhiteSpace(_.Value))), (Func<XElement, string>)(_ => _.Value)));
                Request request = new Request()
                {
                    Ikeys = strArray
                };
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[ApiEnum.GetByIkeys], (object)request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string GetInvoicePdf(string xmlRequest, string path, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            if (!Utils.IsNullOrWhiteSpace(path))
            {
                path = Service.ConvertFont(path, convert);
                if (path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                    return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)("Đường dẫn tệp tin chứa ký tự không hợp lệ: " + path), (object)null);
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                catch (Exception ex)
                {
                    return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)ex.Message, (object)null);
                }
            }
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                XDocument xdocument = XDocument.Parse(xmlRequest);
                XElement xelement1 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, "/root/Option");
                int result;
                if (!int.TryParse(xelement1 != null ? xelement1.Value : (string)null, out result))
                {
                    response.Status = new int?(4);
                    response.Message = "Option có giá trị không hợp lệ: " + (xelement1 != null ? xelement1.Value : (string)null);
                }
                else
                {
                    XElement xelement2 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, "/root/Pattern");
                    XElement xelement3 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, "/root/Ikey");
                    Request request = new Request()
                    {
                        Pattern = xelement2 != null ? xelement2.Value : (string)null,
                        Option = result,
                        Ikey = xelement3 != null ? xelement3.Value : (string)null
                    };
                    EasyResponse easyResponse = this._easyClient.PostJsonObject(Service.Resources[ApiEnum.GetPdf], (object)request);
                    if (easyResponse.HttpStatusCode == HttpStatusCode.OK)
                    {
                        ContentDisposition contentDisposition = new ContentDisposition(easyResponse.Headers["Content-Disposition"]);
                        string fileName = Path.GetFileName(path);
                        if (Utils.IsNullOrWhiteSpace(fileName))
                            fileName = contentDisposition.FileName;
                        string desiredFileFullPath = Service.GetDesiredFileFullPath(Path.Combine(Path.GetDirectoryName(path), fileName), (string)null);
                        Service.SaveBytesToFile(desiredFileFullPath, easyResponse.RawBytes);
                        response.Status = new int?(2);
                        response.Message = desiredFileFullPath;
                    }
                    else
                    {
                        response.Status = new int?((int)easyResponse.HttpStatusCode);
                        response.Message = "Có lỗi xả ra: " + easyResponse.Content;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
        }

        public string CancelInvoice(string xmlRequest, int convert = 1, string host = null, string id = null, string password = null)
        {
            string str = this.CheckCredentialsParams(convert, host, id, password);
            if (!Utils.IsNullOrWhiteSpace(str))
                return string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)4, (object)str, (object)null);
            Response response = new Response();
            try
            {
                xmlRequest = Service.ConvertFont(xmlRequest, convert);
                int startIndex = xmlRequest.IndexOf("<root>", StringComparison.InvariantCulture);
                if (startIndex <= 0)
                    throw new Exception("XmlRequest thiếu thẻ <root>");
                xmlRequest = xmlRequest.Substring(startIndex);
                XDocument xdocument = XDocument.Parse(xmlRequest);
                string expression1 = "/root/Pattern";
                XElement xelement1 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression1);
                string expression2 = "/root/Serial";
                XElement xelement2 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression2);
                string expression3 = "/root/Ikey";
                XElement xelement3 = System.Xml.XPath.Extensions.XPathSelectElement((XNode)xdocument, expression3);
                Request request = new Request()
                {
                    Pattern = xelement1 != null ? xelement1.Value : (string)null,
                    Serial = xelement2 != null ? xelement2.Value : (string)null,
                    Ikey = xelement3 != null ? xelement3.Value : (string)null
                };
                response = this._easyClient.PostJsonObject<Response>(Service.Resources[ApiEnum.Cancel], (object)request);
            }
            catch (Exception ex)
            {
                response.Status = new int?();
                response.Message = ex.Message;
            }
            return Service.SerializeResponseToXml(response, convert);
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
                        string str = Service.SignHash(cert, hash);
                        request.Signature.Add(keyValuePair.Key, str);
                    }
                    response = this._easyClient.PostJsonObject<Response>(Service.Resources[resource], (object)request);
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

        private string CheckCredentialsParams(int convert, string host = null, string id = null, string password = null)
        {
            try
            {
                string source = (string)null;
                int num = 0;
                if (!Utils.IsNullOrWhiteSpace(host))
                    ++num;
                if (!Utils.IsNullOrWhiteSpace(id))
                    num += 2;
                if (!Utils.IsNullOrWhiteSpace(password))
                    num += 4;
                if (num == 7)
                {
                    if (convert != 0)
                    {
                        host = Service.ConvertFont(host, convert);
                        id = Service.ConvertFont(id, convert);
                        password = Service.ConvertFont(password, convert);
                    }
                    this._easyClient = new EasyClient(host, id, password);
                }
                switch (num)
                {
                    case 0:
                        if (this._easyClient == null)
                            this.InitializeEasyClientUsingConfigInfo();
                        if (this._easyClient == null)
                        {
                            source = "Chưa khai báo thông tin kết nối đến hệ thống hoá đơn điện tử Easy Invoice";
                            break;
                        }
                        break;
                    case 1:
                        source = "Chưa khai báo đường dẫn (host) hoá đơn điện tử Easy Invoice";
                        break;
                    case 2:
                        source = "Chưa khai báo tên đăng nhập hoá đơn điện tử Easy Invoice";
                        break;
                    case 3:
                        source = "Chưa khai báo đường dẫn (host) và tên đăng nhập hoá đơn điện tử Easy Invoice";
                        break;
                    case 4:
                        source = "Chưa khai báo mật khẩu hoá đơn điện tử Easy Invoice";
                        break;
                    case 5:
                        source = "Chưa khai báo đường dẫn (host) và mật khẩu hoá đơn điện tử Easy Invoice";
                        break;
                    case 6:
                        source = "Chưa khai báo tên đăng nhập và mật khẩu hoá đơn điện tử Easy Invoice";
                        break;
                }
                return Service.ConvertFont(source, -Math.Abs(convert));
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

        private static string SerializeResponseToXml(Response response, int convert)
        {
            if (response == null)
                return (string)null;
            StringBuilder stringBuilder1 = new StringBuilder();
            if (response.Data != null)
            {
                StringBuilder stringBuilder2 = new StringBuilder();
                if (response.Data.Invoices != null && Enumerable.Any<InvoiceInfo>((IEnumerable<InvoiceInfo>)response.Data.Invoices))
                {
                    foreach (InvoiceInfo invoiceInfo in response.Data.Invoices)
                    {
                        stringBuilder2.Append("<Invoice>");
                        stringBuilder2.AppendFormat("<InvoiceStatus>{0}</InvoiceStatus>", (object)invoiceInfo.InvoiceStatus);
                        stringBuilder2.AppendFormat("<Pattern>{0}</Pattern>", (object)invoiceInfo.Pattern);
                        stringBuilder2.AppendFormat("<Serial>{0}</Serial>", (object)invoiceInfo.Serial);
                        stringBuilder2.AppendFormat("<No>{0}</No>", (object)invoiceInfo.No);
                        stringBuilder2.AppendFormat("<LookupCode>{0}</LookupCode>", (object)invoiceInfo.LookupCode);
                        stringBuilder2.AppendFormat("<Ikey>{0}</Ikey>", (object)invoiceInfo.Ikey);
                        stringBuilder2.AppendFormat("<ArisingDate>{0}</ArisingDate>", (object)invoiceInfo.ArisingDate);
                        stringBuilder2.AppendFormat("<IssueDate>{0}</IssueDate>", (object)invoiceInfo.IssueDate);
                        stringBuilder2.AppendFormat("<CustomerName>{0}</CustomerName>", (object)invoiceInfo.CustomerName);
                        stringBuilder2.AppendFormat("<CustomerCode>{0}</CustomerCode>", (object)invoiceInfo.CustomerCode);
                        stringBuilder2.AppendFormat("<Buyer>{0}</Buyer>", (object)invoiceInfo.Buyer);
                        stringBuilder2.AppendFormat("<Amount>{0}</Amount>", (object)invoiceInfo.Amount);
                        stringBuilder2.Append("</Invoice>");
                    }
                }
                StringBuilder dataInvoiceNos = new StringBuilder();
                if (response.Data.InvoiceNo != null && Enumerable.Any<int>((IEnumerable<int>)response.Data.InvoiceNo))
                    response.Data.InvoiceNo.ForEach((Action<int>)(entry => dataInvoiceNos.AppendFormat("<InvoiceNo>{0}</InvoiceNo>", (object)entry)));
                StringBuilder dataKeyInvoiceMsg = new StringBuilder();
                if (response.Data.KeyInvoiceMsg != null && Enumerable.Any<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>)response.Data.KeyInvoiceMsg))
                    Utils.ForEach<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>)response.Data.KeyInvoiceMsg, (Action<KeyValuePair<string, string>>)(entry => dataKeyInvoiceMsg.AppendFormat("<{0}>{1}</{0}>", (object)entry.Key, (object)entry.Value)));
                StringBuilder dataKeyInvoiceNo = new StringBuilder();
                if (response.Data.KeyInvoiceNo != null && Enumerable.Any<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>)response.Data.KeyInvoiceNo))
                    Utils.ForEach<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>)response.Data.KeyInvoiceNo, (Action<KeyValuePair<string, string>>)(entry => dataKeyInvoiceNo.AppendFormat("<{0}>{1}</{0}>", (object)entry.Key, (object)entry.Value)));
                stringBuilder1.Append("<Data>");
                if (!Utils.IsNullOrWhiteSpace(response.Data.Pattern))
                    stringBuilder1.AppendFormat("<Pattern>{0}</Pattern>", (object)response.Data.Pattern);
                if (!Utils.IsNullOrWhiteSpace(response.Data.Serial))
                    stringBuilder1.AppendFormat("<Serial>{0}</Serial>", (object)response.Data.Serial);
                if (!Utils.IsNullOrWhiteSpace(dataKeyInvoiceNo.ToString()))
                    stringBuilder1.AppendFormat("<KeyInvoiceNo>{0}</KeyInvoiceNo>", (object)dataKeyInvoiceNo);
                if (!Utils.IsNullOrWhiteSpace(dataKeyInvoiceMsg.ToString()))
                    stringBuilder1.AppendFormat("<KeyInvoiceMsg>{0}</KeyInvoiceMsg>", (object)dataKeyInvoiceMsg);
                if (!Utils.IsNullOrWhiteSpace(dataInvoiceNos.ToString()))
                    stringBuilder1.AppendFormat("<InvoiceNos>{0}</InvoiceNos>", (object)dataInvoiceNos);
                if (!Utils.IsNullOrWhiteSpace(stringBuilder2.ToString()))
                    stringBuilder1.AppendFormat("<Invoices>{0}</Invoices>", (object)stringBuilder2);
                stringBuilder1.Append("</Data>");
            }
            return Service.ConvertFont(string.Format("<root><Status>{0}</Status><Message>{1}</Message>{2}</root>", (object)response.Status, (object)response.Message, (object)stringBuilder1), -Math.Abs(convert));
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

        private static string GetCertString(out X509Certificate2 signCert)
        {
            signCert = Service.SelectCertificate();
            if (signCert == null)
                return (string)null;
            return Convert.ToBase64String(signCert.RawData);
        }

        private static string SignHash(X509Certificate2 cert, string hash)
        {
            byte[] rgbHash = Convert.FromBase64String(hash);
            return Convert.ToBase64String(((RSACryptoServiceProvider)cert.PrivateKey).SignHash(rgbHash, "SHA1"));
        }

        private static string ConvertFont(string source, int convert)
        {
            string str = source;
            if (convert == 1)
                str = FontConverter.ConvertTCVN3ToUnicode(source);
            else if (convert == 2)
                str = FontConverter.ConvertVniToUnicode(source);
            else if (convert == -1)
                str = FontConverter.ConvertUnicodeToTCVN3(source);
            else if (convert == -2)
                str = FontConverter.ConvertUnicodeToVni(source);
            return str;
        }
    }
}
