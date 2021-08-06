using V6Tools.V6Objects;

namespace V6ThuePost.ViettelV2Objects
{
    public class CancelResponse : V6JsonObject
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Key lỗi?
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Câu chữ báo lỗi.
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// Nếu thành công thì null
        /// </summary>
        public string errorCode { get; set; }
        public string description { get; set; }
    }
}
