namespace V6ThuePost.MISA_Objects.Response
{
    public class UpdatePaymentResponse
    {
        public bool result { get; set; }
        public string errorCode { get; set; }
        public string description { get; set; }
        public string paymentTime { get; set; }
        public string paymentMethod { get; set; }
        public string invoiceRequestBO { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }

    }    
}
