using System.Collections.Generic;

namespace V6ThuePost.ViettelV2Object.Response
{
    public class ViettelV2Response
    {
        public string windowid;
        public string ok;
        public string Message;
        public string error;

        public Dictionary<string, object> data = new Dictionary<string, object>();
    }
}
