using System.Runtime.Serialization;


namespace V6Soft.Services.Wcf.Common.Models
{
    /// <summary>
    ///     Data contract for runtime model.
    /// </summary>
    [DataContract]
    public class RuntimeModelDC
    {
        /// <summary>
        ///     Gets or sets field values.
        /// </summary>
        [DataMember]
        public object[] Fields { get; set; }
    }
}
