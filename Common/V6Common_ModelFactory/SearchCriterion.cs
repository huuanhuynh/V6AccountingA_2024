
namespace V6Soft.Common.ModelFactory
{
    public class SearchCriterion
    {
        /// <summary>
        ///     Gets or sets the target field index.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        ///     Gets or sets the operator used for comparing.
        /// </summary>
        public CompareOperator CompareOperator { get; set; }

        /// <summary>
        ///     Gets or sets the value used for comparing.
        /// </summary>
        public object ConditionValue { get; set; }
    }
}
