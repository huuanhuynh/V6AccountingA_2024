namespace V6Soft.Common.Utils.Linq
{
    /// <summary>
    ///     Describes how search results should be sorted
    /// </summary>
    public class SortDescriptor
    {
        /// <summary>
        ///     Field name
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        ///     Is ascendingly sorting?
        /// </summary>
        public bool IsAscending { get; set; }

        /// <summary>
        ///     Initializes new instance of SortDescriptor
        /// </summary>
        public SortDescriptor(string field, bool isAscending = true)
        {
            Field = field;
            IsAscending = isAscending;
        }
    }
}
