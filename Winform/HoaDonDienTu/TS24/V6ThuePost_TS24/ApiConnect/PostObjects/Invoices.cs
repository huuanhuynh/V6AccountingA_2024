using System.Collections.Generic;

namespace V6ThuePostXmlApi.PostObjects
{
    public class Invoices// : JsonObject
    {
        public Invoices()
        {
            Inv = new List<Inv>();
        }

        /// <summary>
        /// &lt;Inv>...&lt;/Inv>[...]
        /// </summary>
        public List<Inv> Inv;
    }
}
