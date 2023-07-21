namespace V6Structs
{
    /// <summary>
    /// Dữ liệu mặc định cho các control trên form.
    /// </summary>
    public class DefaultValueAndTagInfo
    {
        /// <summary>
        /// Control Name.
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// AccessibleName của Control.
        /// </summary>
        public string AName = "";

        /// <summary>
        /// <para>Kiểu gián Value lên control trên form.</para>
        /// <para>0: Value = 0 hoặc null vẫn gán lên form.</para>
        /// <para>1: Value khác null mới gán.</para>
        /// <para>2: Kiểm tra form = null(rỗng) mới gán.</para>
        /// </summary>
        public string Type1 = "";
        
        /// <summary>
        /// Giá trị gán lên control (theo Type1).
        /// </summary>
        public string Value = "";

        /// <summary>
        /// Tag để gán lên control
        /// </summary>
        public string TagString { get; set; }

        /// <summary>
        /// Gán hide nhớ gán thêm tag hide hoặc invisible.
        /// </summary>
        public bool IsHide { get; set; }
        /// <summary>
        /// Gán Readonly, nhớ addTagString.
        /// </summary>
        public bool IsReadOnly { get; set; }

    }
}
