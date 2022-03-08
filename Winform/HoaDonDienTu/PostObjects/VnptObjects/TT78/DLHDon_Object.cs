using System.Collections.Generic;

namespace V6ThuePost.VnptObjects
{
    public class DLHDon_Object
    {
        /// <summary>
        /// &lt;Inv>.......&lt;/Inv>
        /// </summary>
        public DLHDon_Object()
        {
            TTChung = new Dictionary<string, object>();
            //Invoice["Ikey"] = "";
        }

        public Dictionary<string, object> TTChung { get; set; }

        /// <summary>
        /// &lt;Invoice>  &lt;key>value&lt;/key>[key2...]  &lt;/Invoice>
        /// </summary>
        public NDHDon_Object NDHDon { get; set; }

    }
}
