using System;

namespace V6ThuePost.MISA_Objects.Response
{
    public class MISA_LoginResponse
    {
        public Login_ServiceResult result { get; set; }
    }

    /// <summary>
    /// Đối tượng chứa kết quả trả về từ các lời gọi service
    /// </summary>
    public class Login_ServiceResult
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
        public Login_ServiceResult()
        {
            this.Success = true;
            this.Errors = new System.Collections.Generic.List<string>();
            this.ErrorCode = "";
            this.CustomData = "";
        }

        #endregion "Sub/Func"
    }
}
