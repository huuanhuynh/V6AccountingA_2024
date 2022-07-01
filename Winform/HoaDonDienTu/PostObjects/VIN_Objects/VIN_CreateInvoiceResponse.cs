namespace V6ThuePost.VIN_Objects
{
    public class VIN_CreateInvoiceResponse
    {

        public VIN_CreateInvoiceResponse_Result result { get; set; }
        
        public string targetUrl { get; set; }
        //public string message { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }

    }
}
