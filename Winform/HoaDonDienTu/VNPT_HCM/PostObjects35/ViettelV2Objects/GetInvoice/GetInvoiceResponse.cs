﻿using System.Collections.Generic;

namespace V6ThuePost.ViettelV2Objects.GetInvoice
{
    public class GetInvoiceResponse
    {
        public string errorCode;
        public string description;
        public int totalRow;
        public List<Dictionary<string, object>> invoices;
    }
}
