using System.Collections.Generic;

namespace V6ThuePostThaiSonApi.ThaiSonPostObjects
{
    public class AdjustInv
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
