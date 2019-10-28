namespace V6ThuePostViettelApi.PostObjects.GetInvoice
{
    public class GetInvoiceFilePortalParameters : JsonObject
    {
        public string supplierTaxCode = "";
        public string invoiceNo = "";
        public string buyerIdNo = "";
        public string reservationCode = "";
        public string fileType = "XML";
        public string strIssueDate = "";
    }
}
