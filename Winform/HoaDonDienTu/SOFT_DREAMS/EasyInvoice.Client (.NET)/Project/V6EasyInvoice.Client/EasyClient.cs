// Decompiled with JetBrains decompiler
// Type: EasyInvoice.Client.EasyClient
// Assembly: EasyInvoice.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D591C1A-17CB-4CA4-A650-8459834356C1
// Assembly location: E:\Copy\Code\HoaDonDienTu\SOFT_DREAMS\EasyInvoice.Client (.NET)\EasyInvoice.Client.dll

using System;
using System.Net;
using EasyInvoice.Json;

namespace V6EasyInvoice.Client
{
    public class EasyClient
    {
        private readonly NetworkCredential _credential;
        private const string JsonContent = "application/json";

        public EasyClient(string host, string userName, string password)
        {
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (password == null)
                throw new ArgumentNullException("password");
            if (host == null)
                throw new ArgumentNullException("host");
            this._credential = new NetworkCredential(userName, password, host);
        }

        public EasyClient(NetworkCredential credential)
        {
            if (credential == null)
                throw new ArgumentNullException("credential");
            this._credential = credential;
        }

        public T GetJsonObject<T>(string resource, object obj = null) where T : new()
        {
            string content = obj is string ? Convert.ToString(obj) : JsonConvert.SerializeObject(obj);
            return this.CallApi<T>(Method.GET, resource, "application/json", content);
        }

        public T PostJsonObject<T>(string resource, object obj = null) where T : new()
        {
            string content = obj is string ? Convert.ToString(obj) : JsonConvert.SerializeObject(obj);
            return this.CallApi<T>(Method.POST, resource, "application/json", content);
        }

        public T Get<T>(string resource, string contentType, string content = null) where T : new()
        {
            return this.CallApi<T>(Method.GET, resource, contentType, content);
        }

        public T Post<T>(string resource, string contentType, string content = null) where T : new()
        {
            return this.CallApi<T>(Method.POST, resource, contentType, content);
        }

        public EasyResponse GetJsonObject(string resource, object obj = null)
        {
            string content = obj is string ? Convert.ToString(obj) : JsonConvert.SerializeObject(obj);
            return this.CallApi(Method.GET, resource, "application/json", content);
        }

        public EasyResponse PostJsonObject(string resource, object obj = null)
        {
            string content = obj is string ? Convert.ToString(obj) : JsonConvert.SerializeObject(obj);
            return this.CallApi(Method.POST, resource, "application/json", content);
        }

        public EasyResponse Get(string resource, string contentType, string content = null)
        {
            return this.CallApi(Method.GET, resource, contentType, content);
        }

        public EasyResponse Post(string resource, string contentType, string content = null)
        {
            return this.CallApi(Method.POST, resource, contentType, content);
        }

        private EasyResponse CallApi(Method method, string resource, string contentType, string content = null)
        {
            if (this._credential == null)
                throw new Exception("EasyClient chưa được khởi tạo");
            EasyRequest easyRequest = new EasyRequest(Utils.MergeBaseUrlAndResource(new Uri(this._credential.Domain), resource), method);
            string userName = this._credential.UserName;
            string password = this._credential.Password;
            easyRequest.SetCredentials(userName, password);
            string contentType1 = contentType;
            string str = content;
            easyRequest.AddBody(contentType1, str);
            return easyRequest.Execute();
        }

        private T CallApi<T>(Method method, string resource, string contentType, string content = null) where T : new()
        {
            if (this._credential == null)
                throw new Exception("EasyClient chưa được khởi tạo");
            EasyRequest easyRequest = new EasyRequest(Utils.MergeBaseUrlAndResource(new Uri(this._credential.Domain), resource), method);
            string userName = this._credential.UserName;
            string password = this._credential.Password;
            easyRequest.SetCredentials(userName, password);
            string contentType1 = contentType;
            string str = content;
            easyRequest.AddBody(contentType1, str);
            EasyResponse easyResponse = easyRequest.Execute();
            if (easyResponse == null)
                return default(T);
            if (easyResponse.HttpStatusCode == HttpStatusCode.GatewayTimeout || easyResponse.HttpStatusCode == HttpStatusCode.RequestTimeout || easyResponse.HttpStatusCode == (HttpStatusCode)0)
                throw new TimeoutException("Lỗi kết nối đến máy chủ");
            if (easyResponse.Exception != null)
                throw easyResponse.Exception;
            if (Utils.IsValidJson(easyResponse.Content))
                return JsonConvert.DeserializeObject<T>(easyResponse.Content);
            throw new Exception(string.Format("Response content is not valid json:\r\n{0}", (object)easyResponse.Content));
        }
    }
}
