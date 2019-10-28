using System.Net;

namespace V6ThuePostBkavApi
{
    public static class RequestSender
    {
        #region ==== POST GET ====

        public static string username = "";
        public static string password = "";
        public static HttpWebResponse Response = null;

        private static readonly RequestManager requestManager = new RequestManager();
        public static string POST(string uri, string request)
        {
            Response = requestManager.SendPOSTRequest(uri, request, username, password, true);
            return requestManager.GetResponseContent(Response);
        }
        public static string GET(string uri)
        {
            Response = requestManager.SendGETRequest(uri, username, password, true);
            return requestManager.GetResponseContent(Response);
        }
        #endregion post get
    }
}
