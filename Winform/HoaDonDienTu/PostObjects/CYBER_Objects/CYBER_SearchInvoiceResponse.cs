namespace V6ThuePost.CYBER_Objects
{
    public class CYBER_SearchInvoiceResponseV2
    {
        public string errorCode { get; set; }
        public string description { get; set; }
        public CYBER_CreateInvoiceResponse_Result[] result { get; set; }
        public string invoiceRequestBO { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }

    }    
}
