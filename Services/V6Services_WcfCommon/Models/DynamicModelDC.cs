using System.Collections.Generic;
using System.Runtime.Serialization;


namespace V6Soft.Services.Wcf.Common.Models
{
    [DataContract]
    public class DynamicModelDC
    {
        /// <summary>
        ///     Gets or sets field values.
        /// </summary>
        [DataMember]
        public IDictionary<string, object> Fields { get; set; }
    }
}
