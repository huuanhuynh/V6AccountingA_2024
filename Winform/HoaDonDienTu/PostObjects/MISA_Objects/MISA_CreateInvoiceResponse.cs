namespace V6ThuePost.MISA_Objects
{
    public class MISA_CreateInvoiceResponse
    {
        #region "Property"

        /// <summary>
        /// Trạng thái: true-thành công / false-thất bại
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Giá trị trả về: Nội dung lỗi, dữ liệu
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Nội dung lỗi
        /// </summary>
        public System.Collections.Generic.List<string> Errors { get; set; }

        /// <summary>
        /// Nội dung cấu hình riêng nếu có
        /// </summary>
        public string CustomData { get; set; }

        #endregion "Property"

        #region "Sub/Func"

        /// <summary>
        /// Phương thức khởi tạo đổi tượng <see cref="Login_ServiceResult"/>
        /// </summary>
        public MISA_CreateInvoiceResponse()
        {
            this.Success = true;
            this.Errors = new System.Collections.Generic.List<string>();
            this.ErrorCode = "";
            this.CustomData = "";
        }

        #endregion "Sub/Func"
    }
}
