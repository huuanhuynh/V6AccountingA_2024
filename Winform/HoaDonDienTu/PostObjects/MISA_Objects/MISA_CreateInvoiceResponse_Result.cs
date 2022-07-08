using System.Collections.Generic;
namespace V6ThuePost.MISA_Objects
{
    public class MISA_CreateInvoiceResponseData
    {
        public string RefID { get; set; }

        /// <summary>
        /// Mã tra cứu hóa đơn
        /// </summary>
        public string TransactionID { get; set; }

        public string Data { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string InvNo { get; set; }

        /// <summary>
        /// Ngày hóa đơn
        /// </summary>
        public System.DateTime InvDate { get; set; }

        /// <summary>
        /// Thông tin hóa đơn dạng XML
        /// </summary>
        public string InvoiceData { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }

    }
}
