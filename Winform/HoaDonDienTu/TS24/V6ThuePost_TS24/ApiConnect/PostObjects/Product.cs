using System.Collections.Generic;

namespace V6ThuePostXmlApi.PostObjects
{
    public class Product
    {
        public Product()
        {
            Details = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Details;
    }
}
