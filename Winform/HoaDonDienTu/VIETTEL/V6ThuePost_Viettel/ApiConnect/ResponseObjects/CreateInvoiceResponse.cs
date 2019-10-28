namespace V6ThuePostViettelApi.ResponseObjects
{
    public class CreateInvoiceResponse : BaseResponse
    {
        public CreateInvoiceResponse_Result result { get; set; }
        public string invoiceRequestBO { get; set; }
    }    
}
