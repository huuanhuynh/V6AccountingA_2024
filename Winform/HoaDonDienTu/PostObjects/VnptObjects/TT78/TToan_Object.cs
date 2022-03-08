using System.Collections.Generic;

namespace V6ThuePost.VnptObjects
{
    public class TToan_Object
    {
        public TToan_Object()
        {
            Data = new Dictionary<string, object>();
            THTTLTSuat = new THTTLTSuat_Object();
        }

        public Dictionary<string, object> Data;
        public THTTLTSuat_Object THTTLTSuat { get; set; }

    }
}
