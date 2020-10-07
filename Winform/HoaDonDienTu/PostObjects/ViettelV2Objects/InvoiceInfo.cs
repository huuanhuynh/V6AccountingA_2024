using V6Tools.V6Objects;

namespace V6ThuePost.ViettelV2Objects
{ 
    public class InvoiceInfo
    {
        public string uuId { get; set; }
        public string templateCode { get; set; }
        public string invoiceSeries { get; set; }
        public string invoiceIssuedDate { get; set; }
        public string invoiceType { get; set; }
        public string currencyCode { get; set; }
        public string adjustmentType { get; set; }
        public string paymentStatus { get; set; }
        public string paymentType { get; set; }
        public string paymentTypeName { get; set; }
        public string cusGetInvoiceRight { get; set; }
        public string buyerIdNo { get; set; }
        public string buyerIdType { get; set; }
        public string invoiceNote { get; set; }
        public string adjustmentInvoiceType { get; set; }
        public string originalInvoiceId { get; set; }
        public string originalInvoiceIssueDate { get; set; }
        public string additionalReferenceDesc { get; set; }
        public string additionalReferenceDate { get; set; }
    }
    
    public class ZipFileResponse
    {
        public string errorCode { get; set; }
        public string description { get; set; }
        public string fileName { get; set; }
        public byte[] fileToBytes { get; set; }
        public bool paymentStatus { get; set; } 
    }

    public class GetFileRequest : V6JsonObject
    {
        public string invoiceNo { get; set; }
        public string fileType { get; set; }
        public string strIssueDate { get; set; }
        public string additionalReferenceDesc { get; set; }
        public string additionalReferenceDate { get; set; }
        public string pattern { get; set; }
        public string transactionUuid { get; set; }
    }

    public class GetPDFFileRequest : V6JsonObject
    {
        public string supplierTaxCode { get; set; }
        public string invoiceNo { get; set; }
        public string templateCode { get; set; }
        public string transactionUuid { get; set; }
        public string fileType { get; set; }
    }

    public class GetPDFFileRequestE : V6JsonObject
    {
        public string supplierTaxCode { get; set; }
        public string invoiceNo { get; set; }
        public string strIssueDate { get; set; }
        public string exchangeUser { get; set; }
    }

    public class PDFFileResponse
    {
        public string errorCode { get; set; }
        public string description { get; set; }
        public string fileName { get; set; }
        public byte[] fileToBytes { get; set; }
        public bool paymentStatus { get; set; }
    }

    public class TaxBreakdowns
    {
        public string taxPercentage { get; set; }
        public string taxableAmount { get; set; }
        public string taxAmount { get; set; }
    }
}
