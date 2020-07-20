namespace V6ThuePost.MONET_Objects.Response
{
    public class MONET_DOWNLOAD_Response
    {
        public string fileName;
        public string fileType;
        public string strIssueDate;
        public byte[] fileToBytes;
        public string supplierTaxCode { get; set; }
        public string invoiceNo { get; set; }
        public string transactionID { get; set; }
        public string hashString { get; set; }
        public string reservationCode { get; set; }
    }
}
