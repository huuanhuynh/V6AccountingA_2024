using System.Collections.Generic;

namespace ApiConnect.YtaApi
{
    public static class LichKhamBenhApi
    {
        public static YtaApiResponeModel Add(SortedDictionary<string, object> data)
        {
            Dictionary<string, object> parameters0 = new Dictionary<string, object>(data);
            return SendRequest.POST<YtaApiResponeModel>(UrlHelper.LichKhamBenh.Add, HeaderType.none, parameters0, null);
        }
    }
}
