namespace V6Structs
{
    /// <summary>
    /// Dữ liệu mặc định cho các trường liên quan khi chọn (ma_vt)
    /// </summary>
    public class DefaultValueInfo
    {
        public string Value = "";
        /// <summary>
        /// <para>Kiểu</para>
        /// <para>0: Value = 0 hoặc null vẫn gán lên form.</para>
        /// <para>1: Value khác null mới gán.</para>
        /// <para>2: Kiểm tra form = null(rỗng) mới gán.</para>
        /// </summary>
        public string Type1 = "";

        /// <summary>
        /// AccessibleName của Control.
        /// </summary>
        public string AName = "";

        /// <summary>
        /// Control Name.
        /// </summary>
        public string CName { get; set; }
    }
}
