namespace V6ThuePost
{
    public class ConfigLine
    {
        public string MA_TD2;
        public string MA_TD3;
        public decimal SL_TD1;
        public decimal SL_TD2;
        public decimal SL_TD3;

        /// <summary>
        /// Trường dữ liệu post
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// Giá trị mặc định
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Trường dữ liệu tương ứng của V6
        /// </summary>
        public string FieldV6 { get; set; }
        /// <summary>
        /// <para>Field -> lấy từ dữ liệu theo field.</para>
        /// <para>Field:Date -> Date là kiểu dữ liệu để xử lý.</para>
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Kiểu dữ liệu: Bool,Int,Long,Decimal,String,...
        /// </summary>
        public string DataType { get; set; }

        public string Format { get; set; }
        /// <summary>
        /// Cờ không tạo object (khi đúng điều kiện)
        /// </summary>
        public bool NoGen { get; set; }
        /// <summary>
        /// V6Remark=3+So_luong1=0 (chưa dùng)
        /// </summary>
        public string NoGenCondition { get; set; }
    }
}