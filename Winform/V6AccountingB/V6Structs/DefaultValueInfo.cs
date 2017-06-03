namespace V6Structs
{
    /// <summary>
    /// Dữ liệu mặc định cho các trường liên quan khi chọn (ma_vt)
    /// </summary>
    public class DefaultValueInfo
    {
        public string Value = "";
        /// <summary>
        /// 0: Value = 0 hoặc null vẫn gán lên form.
        /// 1: Value khác null mới gán
        /// 2: Kiểm tra form = null(rỗng) mới gán
        /// </summary>
        public string Type1 = "";

        public string Name = "";
    }
}
