using System.Collections.Generic;
using V6Tools.V6Objects;

namespace V6ThuePostXmlApi.PostObjects
{
    public class ReplaceInv : V6JsonObject
    {
        /// <summary>
        /// &lt;ReplaceInv>...........&lt;/ReplaceInv>
        /// </summary>
        public ReplaceInv()
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
