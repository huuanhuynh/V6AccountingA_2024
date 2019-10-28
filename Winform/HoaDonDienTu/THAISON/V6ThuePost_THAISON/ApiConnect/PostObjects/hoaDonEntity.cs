using System.Collections.Generic;

namespace V6ThuePostApi.PostObjects
{
    public class hoaDonEntity
    {
        /// <summary>
        /// &lt;Inv>.......&lt;/Inv>
        /// </summary>
        public hoaDonEntity()
        {
            Invoice = new Dictionary<string, object>();
        }
        
        /// <summary>
        ///  &lt;key>value&lt;/key>[key2...]
        /// </summary>
        public Dictionary<string, object> Invoice;
    }
}
