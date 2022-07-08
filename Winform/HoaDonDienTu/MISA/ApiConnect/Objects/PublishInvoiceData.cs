namespace V6ThuePost_MISA_Api.Objects
{
    /// <summary>
    /// Thông tin hóa đơn phát hành
    /// </summary>
    public class PublishInvoiceData
    {
        /// <summary>
        /// ID của hóa đơn trên Client App
        /// </summary>
        public string RefID { get; set; }

        /// <summary>
        /// Mã tra cứu của hóa đơn
        /// </summary>
        public string TransactionID { get; set; }

        /// <summary>
        /// Đường dẫn API để cập nhật trạng thái hóa đơn sau khi phát hành
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Nội dung hóa đơn đã được ký điện tử
        /// </summary>
        public string InvoiceData { get; set; }

        /// <summary>
        /// Gửi hóa đơn cho khách hàng sau khi phát hành
        /// </summary>
        public bool IsSendEmail { get; set; }

        /// <summary>
        /// Tên người nhận Email
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// Danh sách Email nhận (cách nhau bởi dấu ;)
        /// </summary>
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// Có hủy hóa đơn gốc không (đối với phát hành hóa đơn thay thế)
        /// </summary>
        public bool? IsDeleteOrg { get; set; }
    }
}