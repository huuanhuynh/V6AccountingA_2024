using V6Tools.V6Objects;

namespace V6ThuePost.ViettelV2Objects.GetInvoice
{
    public class GetListInvoiceDataControlParams : V6JsonObject
    {
        /// <summary>
        /// Mã số thuế của doanh nghiệp xuất hóa đơn.
        /// </summary>
        public string supplierTaxCode = "";
        /// <summary>
        /// dd/MM/yyyy
        /// </summary>
        public string fromDate = "";
        /// <summary>
        /// dd/MM/yyyy
        /// </summary>
        public string toDate = "";
    }
}
