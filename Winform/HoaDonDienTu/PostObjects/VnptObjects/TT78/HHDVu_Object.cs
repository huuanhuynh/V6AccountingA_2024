using System.Collections.Generic;

namespace V6ThuePost.VnptObjects
{
    public class HHDVu_Object
    {
        public HHDVu_Object()
        {
            Data = new Dictionary<string, object>();
            TTKhac = new TTKhac_Object();
        }

        public Dictionary<string, object> Data;
        public TTKhac_Object TTKhac { get; set; }

    }
}
