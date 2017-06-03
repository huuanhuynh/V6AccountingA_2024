using System.Runtime.Serialization;


namespace V6Soft.Services.Wcf.Common.Models
{
    /// <summary>
    ///     Represents the results of an operation
    /// </summary>
    [DataContract]
    public sealed class ResultDC
    {
        /// <summary>
        ///     Indicates whether the operations is successul or not
        /// </summary>
        [DataMember]
        public bool IsSuccessful { get; set; }

        /// <summary>
        ///     Is used to look up appropriate error message
        /// </summary>
        [DataMember]
        public int? ErrorCode { get; set; }
    }
}
