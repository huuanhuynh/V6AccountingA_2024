using System.Collections.Generic;
using V6Tools.V6Objects;

namespace V6ThuePostXmlApi.PostObjects
{
    public class AdjustInv : V6JsonObject
    {
        /// <summary>
        /// &lt;AdjustInv>...........&lt;/AdjustInv>
        /// </summary>
        public AdjustInv()
        {
            key = "";
            Invoice = new Dictionary<string, object>();
        }

        /// <summary>
        /// &lt;key>xxxxxxx&lt;/key>
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// &lt;key>value&lt;/key>[key2...]
        /// </summary>
        public Dictionary<string, object> Invoice;
    }
}
