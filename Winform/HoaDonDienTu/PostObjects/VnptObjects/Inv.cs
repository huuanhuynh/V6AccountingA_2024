using System.Collections.Generic;

namespace V6ThuePost.VnptObjects
{
    public class Inv
    {
        /// <summary>
        /// &lt;Inv>.......&lt;/Inv>
        /// </summary>
        public Inv()
        {
            Invoice = new Dictionary<string, object>();
            //Invoice["Ikey"] = "";
        }

        public string key { get; set; }

        /// <summary>
        /// &lt;Invoice>  &lt;key>value&lt;/key>[key2...]  &lt;/Invoice>
        /// </summary>
        public Dictionary<string, object> Invoice { get; set; }

        public AdjustInv ToAdjustInv()
        {
            AdjustInv result = new AdjustInv();
            result.key = key;
            result.Invoice = new Dictionary<string, object>(Invoice);
            return result;
        }

        public ReplaceInv ToReplaceInv()
        {
            ReplaceInv result = new ReplaceInv();
            result.key = key;
            result.Invoice = new Dictionary<string, object>(Invoice);
            return result;
        }
    }
}
