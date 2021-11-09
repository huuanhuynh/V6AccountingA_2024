using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6ThuePost.ViettelObjects.GetInvoice
{
    public class GetInvoiceResponse
    {
        public string errorCode;
        public string description;
        public int totalRow;
        public List<Dictionary<string, object>> invoices;
    }
}
