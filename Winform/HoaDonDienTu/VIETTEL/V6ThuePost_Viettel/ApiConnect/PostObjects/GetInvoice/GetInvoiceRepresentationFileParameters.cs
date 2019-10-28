namespace V6ThuePostViettelApi.PostObjects.GetInvoice
{
    public class GetInvoiceRepresentationFileParameters : JsonObject
    {
        public string supplierTaxCode = "";
        public string invoiceNo = "";
        public string pattern = "";
        public string transactionUuid = "";
        public string fileType = "ZIP";
    }
}
