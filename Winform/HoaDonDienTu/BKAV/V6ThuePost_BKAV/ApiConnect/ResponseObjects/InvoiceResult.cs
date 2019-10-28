using System;

namespace V6ThuePostBkavApi.ResponseObjects
{
    [Serializable]
    public class InvoiceResult
    {
        public string PartnerInvoiceID { get; set; }
        public string PartnerInvoiceStringID { get; set; }

        public Guid InvoiceGUID { get; set; }

        public string InvoiceForm { get; set; }
        public string InvoiceSerial { get; set; }
        public int InvoiceNo { get; set; }
        public string MTC { get; set; }

        /// <summary>
        /// Trạng thái xử lý: 0 - thêm mới thành công, 1 - lỗi
        /// </summary>
        public int Status { get; set; }
        public string MessLog { get; set; }
    }
}
