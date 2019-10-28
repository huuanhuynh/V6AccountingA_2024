using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace V6ThuePostViettelApi
{
    public class V6Http
    {
        readonly HttpClient client;

        public V6Http(string baseurl, string username, string password)
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(baseurl),
            };

            string contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            var encoded = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + password));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "MDEwMDEwOTEwNi05OTA6MTExMTExYUBB");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
        }

        public string POST(string methodUri, string json)
        {
            var hm =
                client.PostAsync(methodUri,
                    new StringContent(json, Encoding.UTF8, "application/json")).Result;
            return hm.Content.ReadAsStringAsync().Result;
        }



        //public async Task<string> PostAsync(string uri, string data)
        //{
        //    var httpClient = new HttpClient();
        //    var response = await httpClient.PostAsync(uri, new StringContent(data));
        //    response.EnsureSuccessStatusCode();
        //    string content = await response.Content.ReadAsStringAsync();
        //    return await Task.Run(() => (content));
        //}
    }
}
