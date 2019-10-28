using System.Collections.Generic;
using V6Tools.V6Objects;

namespace V6ThuePost.VnptObjects
{
    public class AdjustInv : V6JsonObject
    {
        /// <summary>
        /// &lt;AdjustInv>...........&lt;/AdjustInv>
        /// </summary>
        public AdjustInv()
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
