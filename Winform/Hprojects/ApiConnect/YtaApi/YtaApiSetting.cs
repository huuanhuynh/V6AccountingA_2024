using H_Utility.MyTypes;

namespace ApiConnect.YtaApi
{
    public static class YtaApiSetting
    {
        public static void Setting(string server, string dateFormat = "yyyy-MM-dd HH:mm:ss")
        {
            SendRequest.SetServer(server);
            MyJson.SetDateFormat(dateFormat);
        }
    }
}
