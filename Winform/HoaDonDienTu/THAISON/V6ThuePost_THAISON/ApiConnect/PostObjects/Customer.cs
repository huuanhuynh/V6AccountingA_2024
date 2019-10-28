using System.Collections.Generic;

namespace V6ThuePostApi.PostObjects
{
    public class Customer
    {
        /// <summary>
        /// &lt;Inv>.......&lt;/Inv>
        /// </summary>
        public Customer()
        {
            Customer_Info = new Dictionary<string, object>();
        }

        /// <summary>
        /// &lt;Customer>  &lt;key>value&lt;/key>[key2...]  &lt;/Customer>
        /// </summary>
        public Dictionary<string, object> Customer_Info;
    }
}
