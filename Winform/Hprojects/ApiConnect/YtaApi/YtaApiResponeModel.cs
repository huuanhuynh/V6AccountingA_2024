using H_Utility.MyTypes;

namespace ApiConnect.YtaApi
{
    public class YtaApiResponeModel
    {
        public int status{ get; set; }

        public string message { get; set; }
        public string data { get; set; }

        public T GetDataObject<T>()
        {
            return MyJson.ConvertJson<T>(data);
        }
    }
}
