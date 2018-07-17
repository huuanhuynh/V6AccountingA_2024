namespace V6ThuePostManager.Viettel.ResponseObjects
{
    public class CreateInvoiceResponse
    {
        public string errorCode { get; set; }
        public string description { get; set; }
        public CreateInvoiceResponse_Result result { get; set; }
        public string invoiceRequestBO { get; set; }
    }
}
