namespace V6ThuePost.MISA_Objects
{
    public class SearchInvoiceResponseV2
    {
        public string errorCode { get; set; }
        public string description { get; set; }
        public MISA_CreateInvoiceResponseData[] result { get; set; }
        public string invoiceRequestBO { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }

    }    
}
