using System.Collections.Generic;

namespace V6ThuePost.MISA_Objects.GetInvoice
{
    public class GetInvoiceResponse
    {
        public string errorCode;
        public string description;
        public int totalRow;
        public List<Dictionary<string, object>> invoices;
    }
}
