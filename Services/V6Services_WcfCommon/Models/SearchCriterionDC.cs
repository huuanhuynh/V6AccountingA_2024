using System.Runtime.Serialization;
using V6Soft.Common.ModelFactory;


namespace V6Soft.Services.Wcf.Common.Models
{
    /// <summary>
    ///     Data contract for search criterion.
    /// </summary>
    [DataContract]
    public class SearchCriterionDC
    {
        /// <summary>
        ///     Gets or sets field index.
        /// </summary>
        [DataMember]
        public string FieldName { get; set; }

        /// <summary>
        ///     Gets or sets operator for comparing.
        /// </summary>
        [DataMember]
        public CompareOperator CompareOperator { get; set; }

        /// <summary>
        ///     Gets or sets value for comparing.
        /// </summary>
        [DataMember]
        public object ConditionValue { get; set; }
    }
}
