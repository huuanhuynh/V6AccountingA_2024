using System.Collections.Generic;

namespace V6ThuePost.SoftDreamObjects
{
    public class InvoiceFee
    {
        public InvoiceFee()
        {
            Details = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Details;
    }
}
