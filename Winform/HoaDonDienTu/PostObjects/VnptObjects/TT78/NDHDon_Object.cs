using System.Collections.Generic;

namespace V6ThuePost.VnptObjects
{
    public class NDHDon_Object
    {
        public NDHDon_Object()
        {
            NBan = new Dictionary<string, object>();
            NMua = new Dictionary<string, object>();
        }

        public Dictionary<string, object> NBan { get; set; }
        public Dictionary<string, object> NMua { get; set; }


        public List<HHDVu_Object> DSHHDVu { get; set; }
        public TToan_Object TToan { get; set; }

    }
}
