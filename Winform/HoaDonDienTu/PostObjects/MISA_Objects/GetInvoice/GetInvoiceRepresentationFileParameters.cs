using V6Tools.V6Objects;

namespace V6ThuePost.MISA_Objects.GetInvoice
{
    public class GetInvoiceRepresentationFileParameters : V6JsonObject
    {
        public string supplierTaxCode = "";
        public string invoiceNo = "";
        public string pattern = "";
        public string transactionUuid = "";
        public string fileType = "ZIP";
    }
}
