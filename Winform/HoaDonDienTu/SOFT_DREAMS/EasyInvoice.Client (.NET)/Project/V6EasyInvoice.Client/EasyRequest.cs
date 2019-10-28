// Decompiled with JetBrains decompiler
// Type: EasyInvoice.Client.EasyRequest
// Assembly: EasyInvoice.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D591C1A-17CB-4CA4-A650-8459834356C1
// Assembly location: E:\Copy\Code\HoaDonDienTu\SOFT_DREAMS\EasyInvoice.Client (.NET)\EasyInvoice.Client.dll

using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace V6EasyInvoice.Client
{
    public class EasyRequest
    {
        private static readonly Version Version = new AssemblyName(Assembly.GetExecutingAssembly().FullName).Version;

        public string ContentType { get; set; }

        protected string UserAgent { get; set; }

        protected string Accept { get; set; }

        protected string RequestBody { get; set; }

        protected byte[] RequestBodyBytes { get; set; }

        protected Uri Uri { get; set; }

        protected Method Method { get; set; }

        protected HttpWebRequest HttpWebRequest { get; set; }

        protected HttpWebResponse HttpWebResponse { get; set; }

        private EasyRequest()
        {
            this.UserAgent = "EasyInvoice-Client/" + (object)EasyRequest.Version;
            this.Accept = "application/json, text/json";
        }

        public EasyRequest(string host, Method method)
            : this()
        {
            this.Uri = new Uri(host);
            this.HttpWebRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
            this.Method = method;
            this.HttpWebRequest.Method = method.ToString();
            this.HttpWebRequest.Timeout = 300000;
        }

        public void AddHeader(string name, string value)
        {
            this.HttpWebRequest.Headers.Add(name, value);
        }

        public void AddBody(string contentType, string value)
        {
            this.ContentType = contentType;
            this.RequestBody = value;
        }

        public void SetCredentials(string id, string password)
        {
            this.AddHeader("Authentication", EasyRequest.GenerateToken(id, password, this.Method.ToString()));
        }

        public EasyResponse Execute()
        {
            EasyResponse easyResponse = new EasyResponse();
            try
            {
                this.HttpWebResponse = this.GetRawResponse(this.HttpWebRequest);
                EasyRequest.ExtractResponseData(easyResponse, this.HttpWebResponse);
            }
            catch (Exception ex)
            {
                EasyRequest.ExtractErrorResponse(easyResponse, ex);
            }
            return easyResponse;
        }

        private HttpWebResponse GetRawResponse(HttpWebRequest request)
        {
            this.ConfigureHttp();
            if (this.Method != Method.GET && this.Method != Method.HEAD && this.Method != Method.COPY || (this.Method == Method.DELETE || this.Method == Method.OPTIONS && !Utils.IsNullOrWhiteSpace(this.RequestBody)))
            {
                this.PreparePostBody(request);
                this.WriteRequestBody(request);
            }
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                    return (HttpWebResponse)ex.Response;
                throw;
            }
        }

        private static void ExtractResponseData(EasyResponse response, HttpWebResponse webResponse)
        {
            using (webResponse)
            {
                response.Headers = webResponse.Headers;
                response.ContentEncoding = webResponse.ContentEncoding;
                response.ContentType = webResponse.ContentType;
                response.ContentLength = webResponse.ContentLength;
                Stream responseStream = webResponse.GetResponseStream();
                response.RawBytes = Utils.ReadAsBytes(responseStream);
                response.HttpStatusCode = webResponse.StatusCode;
                response.StatusDescription = webResponse.StatusDescription;
                webResponse.Close();
            }
        }

        private static void ExtractErrorResponse(EasyResponse easyResponse, Exception ex)
        {
            if (ex is WebException && ((WebException)ex).Status == WebExceptionStatus.Timeout)
            {
                easyResponse.HttpStatusCode = HttpStatusCode.RequestTimeout;
                easyResponse.Exception = ex;
                easyResponse.ErrorMessage = ex.Message;
            }
            else
            {
                easyResponse.Exception = ex;
                easyResponse.ErrorMessage = ex.Message;
            }
        }

        private void PreparePostBody(HttpWebRequest webRequest)
        {
            if (Utils.IsNullOrWhiteSpace(this.ContentType))
                throw new Exception("ContentType required");
            webRequest.ContentType = this.ContentType;
        }

        private void WriteRequestBody(HttpWebRequest webRequest)
        {
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                if (this.RequestBody == null)
                    return;
                this.RequestBodyBytes = Encoding.UTF8.GetBytes(this.RequestBody);
                requestStream.Write(this.RequestBodyBytes, 0, this.RequestBodyBytes.Length);
            }
        }

        private void ConfigureHttp()
        {
            if (!Utils.IsNullOrWhiteSpace(this.UserAgent))
                this.HttpWebRequest.UserAgent = this.UserAgent;
            if (!Utils.IsNullOrWhiteSpace(this.Accept))
                this.HttpWebRequest.Accept = this.Accept;
            this.HttpWebRequest.ServicePoint.Expect100Continue = false;
            this.HttpWebRequest.KeepAlive = true;
            this.HttpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }

        private static string GenerateToken(string username, string password, string httpMethod)
        {
            string str1 = Convert.ToUInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString();
            string str2 = Guid.NewGuid().ToString("N").ToLower();
            string s = string.Format("{0}{1}{2}", (object)httpMethod.ToUpper(), (object)str1, (object)str2);
            using (MD5 md5 = MD5.Create())
                return string.Format("{0}:{1}:{2}:{3}:{4}", (object)Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(s))), (object)str2, (object)str1, (object)username, (object)password);
        }
    }
}
