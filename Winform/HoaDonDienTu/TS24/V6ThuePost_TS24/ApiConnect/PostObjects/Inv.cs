using System.Collections.Generic;

namespace V6ThuePostXmlApi.PostObjects
{
    public class Inv
    {
        /// <summary>
        /// &lt;Inv>.......&lt;/Inv>
        /// </summary>
        public Inv()
        {
            key = "";
            Invoice = new Dictionary<string, object>();
        }

        /// <summary>
        /// &lt;key>?????&lt;/key>
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// &lt;Invoice>  &lt;key>value&lt;/key>[key2...]  &lt;/Invoice>
        /// </summary>
        public Dictionary<string, object> Invoice { get; set; }
    }
}
