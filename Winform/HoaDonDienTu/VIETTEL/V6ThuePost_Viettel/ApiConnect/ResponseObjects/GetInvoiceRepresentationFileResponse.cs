namespace V6ThuePostViettelApi.ResponseObjects
{
    public class GetInvoiceRepresentationFileResponse : BaseResponse
    {
        public string fileName { get; set; }
        public byte[] fileToBytes { get; set; }
    }    
}
