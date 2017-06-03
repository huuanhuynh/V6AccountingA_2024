using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;

namespace ApiConnect
{
    public class ODataJsonDeserializer : IDeserializer
    {
        public T Deserialize<T>(IRestResponse response)
        {
            try
            {
                var settings = new JsonSerializerSettings();
                if(!string.IsNullOrEmpty(DateFormat))
                    settings.DateFormatString = DateFormat;

                return JsonConvert.DeserializeObject<T>(JObject.Parse(response.Content)["value"].ToString(), settings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default(T);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
    }
}
