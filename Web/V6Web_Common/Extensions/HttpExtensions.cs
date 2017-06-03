using System.Web;


namespace V6Soft.Web.Common.HttpExtensions
{
    public static class HttpExtensions
    {
        /// <summary>
        ///     Checks if current request is PJAX or not.
        /// </summary>
        public static bool IsPjaxRequest(this HttpRequestBase request)
        {
            return !string.IsNullOrEmpty(request.Headers["X-PJAX"]);
        }
    }
}
