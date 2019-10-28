using System.Net;

namespace V6ThuePostApi
{
    public class V6Http
    {
        private string _baseurl = "";
        private readonly string _username;
        private readonly string _password;

        private readonly RequestManager requestManager = new RequestManager();

        public V6Http(string baseurl, string username, string password)
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
        
        public string POST_XML(string uri, string request)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            HttpWebResponse respone = requestManager.SendPOSTRequestXML(_baseurl + uri, request, _username, _password, true);
            return requestManager.GetResponseContent(respone);
        }

        public string GET(string uri)
        {
            if (uri.StartsWith("/")) uri = uri.Substring(1);
            HttpWebResponse respone = requestManager.SendGETRequest(uri, _username, _password, true);
            return requestManager.GetResponseContent(respone);
        }
    }
}
