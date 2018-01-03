using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6ThuePostApi.ResponseObjects
{
    public class CreateInvoiceResponse
    {
        public string errorCode { get; set; }
        public string description { get; set; }
        public string result { get; set; }
        public string invoiceRequestBO { get; set; }
    }
}
