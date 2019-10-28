using System.Collections.Generic;
using V6Tools.V6Objects;

namespace V6ThuePost.VnptObjects
{
    public class ReplaceInv : V6JsonObject
    {
        /// <summary>
        /// &lt;ReplaceInv>...........&lt;/ReplaceInv>
        /// </summary>
        public ReplaceInv()
        {
            Invoice = new Dictionary<string, object>();
            Invoice["Ikey"] = "";
        }

        public string key;

        /// <summary>
        /// &lt;key>value&lt;/key>[key2...]
        /// </summary>
        public Dictionary<string, object> Invoice;
    }
}
