using System.Collections.Generic;

namespace V6ThuePostApi
{
    public static class V6Request
    {
        /// <summary>
        /// Đổi thông tin bảo mật.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void Login(string username, string password)
        {
            RequestSender._username = username;
            RequestSender._password = password;
        }

        /// <summary>
        /// POST trả về object (từ json).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Request<T>(string url, Dictionary<string, object> data)
        {
            var jstring = Request(url, data);
            var result = MyJson.ConvertJson<T>(jstring);
            return result;
        }
        /// <summary>
        /// GET trả về object (từ json).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T Request<T>(string url)
        {
            var jstring = Request(url);
            var result = MyJson.ConvertJson<T>(jstring);
            return result;
        }

        /// <summary>
        /// POST trả về string.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Request(string url, Dictionary<string, object> data)
        {
            string json = MyJson.ConvertToJson(data);
            string result = Request(url, json);
            return result;
        }
       
        /// <summary>
        /// POST trả về string.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string Request(string uri, string json)
        {
            string result = RequestSender.POST(uri, json);
            return result;
        }

        /// <summary>
        /// GET trả về string.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Request(string url)
        {
            string result = RequestSender.GET(url);
            return result;
        }
        
    }
}
