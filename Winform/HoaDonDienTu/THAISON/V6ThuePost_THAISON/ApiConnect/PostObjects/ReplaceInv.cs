using System.Collections.Generic;

namespace V6ThuePostApi.PostObjects
{
    public class ReplaceInv
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
