namespace V6ThuePost.CYBER_Objects
{
    public class CYBER_CreateInvoiceResponse
    {

        public CYBER_CreateInvoiceResponse_Result result { get; set; }
        
        public string targetUrl { get; set; }
        //public string message { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }

    }
}
