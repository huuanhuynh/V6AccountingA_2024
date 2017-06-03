using System;
using System.Collections.Generic;
using H_Utility.MyTypes;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ApiConnect
{
    public static class SendRequest
    {
        static string _baseUrl = "http://localhost/"; // !!!!! Cần thay đổi đọc từ config.
        private static RestClient _restClient = new RestClient(_baseUrl);

        

        /// <summary>
        /// Chỉnh lại đường dẫn server.
        /// </summary>
        /// <param name="url"></param>
        public static void SetServer(string url)
        {
            _baseUrl = url;
            _restClient = new RestClient(_baseUrl);
        }

        

        /// <summary>
        /// GetInvoke
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="link">Đường dẫn controler, không kể link server</param>
        /// <param name="parameters"></param>
        /// <param name="authenticator"></param>
        /// <returns></returns>
        public static T GET<T>(string link, Dictionary<string, object> parameters, IAuthenticator authenticator = null)
        {
            return GET<T>(link, null, parameters, authenticator);
        }

        /// <summary>
        /// GetInvoke
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="link">Đường dẫn controler, không kể link server</param>
        /// <param name="headers"></param>
        /// <param name="parameters"></param>
        /// <param name="authenticator"></param>
        /// <returns></returns>
        public static T GET<T>(string link, Dictionary<string, string> headers, Dictionary<string, object> parameters,
            IAuthenticator authenticator = null)
        {
            var request = new RestRequest(link, Method.GET);
            if (headers != null)
                foreach (KeyValuePair<string, string> item in headers)
                {
                    request.AddHeader(item.Key, item.Value);
                }

            if (parameters != null)
                foreach (KeyValuePair<string, object> item in parameters)
                {
                    request.AddParameter(item.Key, item.Value);
                }

            _restClient.Authenticator = authenticator;

            IRestResponse response = _restClient.Execute(request);

            try
            {
                var data = JsonConvert.DeserializeObject<T>(response.Content, MyJson.JsSetting);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + response.Content);
            }
        }




        /// <summary>
        /// PostInvoke
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="link">Đường dẫn controler, không kể link server</param>
        /// <param name="headerType"></param>
        /// <param name="parameters"></param>
        /// <param name="jsonBody">Nếu không dùng thì để null.</param>
        /// <param name="authenticator"></param>
        /// <returns></returns>
        public static T POST<T>(string link, HeaderType headerType, Dictionary<string, object> parameters,
            object jsonBody, IAuthenticator authenticator = null)
        {
            var headers = new Dictionary<string, string>();
            switch (headerType)
            {
                case HeaderType.none:
                    break;
                case HeaderType.binary:
                    headers.Add("Content-Type", "binary");//application/octet-stream
                    break;
                case HeaderType.form_data:
                    headers.Add("Content-Type", "multipart/form-data");
                    break;
                case HeaderType.raw:
                    headers.Add("Content-Type", "raw");
                    break;
                case HeaderType.x_www_form_urlencoded:
                    headers.Add("Content-Type", "x-www-form-urlencoded");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("headerType", headerType, null);
            }

            return POST<T>(link, headers, parameters, jsonBody, authenticator);
        }

        /// <summary>
        /// PostInvoke
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="link">Đường dẫn controler, không kể link server</param>
        /// <param name="headers">Nếu không dùng thì để null.</param>
        /// <param name="parameters">Nếu không dùng thì để null.</param>
        /// <param name="jsonBody">Nếu không dùng thì để null.</param>
        /// <param name="authenticator">Chứng thực. Nếu không dùng thì để null.</param>
        /// <returns></returns>
        public static T POST<T>(string link, Dictionary<string, string> headers, Dictionary<string, object> parameters,
            Object jsonBody, IAuthenticator authenticator = null)
        {
            var request = new RestRequest(link, Method.POST);
            if (headers != null)
                foreach (KeyValuePair<string, string> item in headers)
                {
                    request.AddHeader(item.Key, item.Value);
                }

            if (parameters != null)
                foreach (KeyValuePair<string, object> item in parameters)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            if (jsonBody != null)
                request.AddJsonBody(jsonBody);
            //request.add
            _restClient.Authenticator = authenticator;

            IRestResponse response = _restClient.Execute(request);

            try
            {
                var data = JsonConvert.DeserializeObject<T>(response.Content, MyJson.JsSetting);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + response.Content);
            }
        }
    }
}
