using System.Collections.Generic;
using Newtonsoft.Json;

namespace HaUtility.MyTypes
{
    public class HJson
    {
        private static string DateFormatString { get; set; }
        public static JsonSerializerSettings JsSetting = new JsonSerializerSettings();

        public static void SetDateFormat(string format)
        {
            DateFormatString = format;
            JsSetting.DateFormatString = DateFormatString;
        }

        public Dictionary<string, string> ParseJson(string jsonString)
        {
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            return values;
        }

        public static T ConvertJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JsSetting);
        }

        public static string ConvertToJson(Dictionary<string, object> obj)
        {
            return JsonConvert.SerializeObject(obj, JsSetting);
        }

        public static string ConvertToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, JsSetting);
        }
    }
}
