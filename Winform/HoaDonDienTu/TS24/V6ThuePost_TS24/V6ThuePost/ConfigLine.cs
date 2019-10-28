namespace V6ThuePost
{
    internal class ConfigLine
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public string FieldV6 { get; set; }
        /// <summary>
        /// <para>Field -> lấy từ dữ liệu theo field.</para>
        /// <para>Field:Date -> Date là kiểu dữ liệu để xử lý.</para>
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Bool,Int,Decimal,String,...
        /// </summary>
        public string DataType { get; set; }
        public string Format { get; set; }
    }
}
