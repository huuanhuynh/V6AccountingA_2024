using System.Collections.Generic;
using Newtonsoft.Json;

namespace V6ThuePostManager
{
    public class MyJson
    {
        //string json = @"{""key1"":""value1"",""key2"":""value2""}";

        public Dictionary<string, string> ParseJson(string jsonString)
        {
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            return values;
        }

        public static Dictionary<string, object> ConvertJson(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
        public static T ConvertJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ConvertToJson(Dictionary<string,object> obj)
        {
            JsonSerializerSettings set = new JsonSerializerSettings();
            set.DateFormatString = "dd/MM/yyyy";

            return JsonConvert.SerializeObject(obj, set);
        }

        public static string ConvertToJson<T>(T obj)
        {
            JsonSerializerSettings set = new JsonSerializerSettings();
            set.DateFormatString = "dd/MM/yyyy";

            return JsonConvert.SerializeObject(obj, set);
        }
    }
}
