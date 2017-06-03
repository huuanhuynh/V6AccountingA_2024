using System.Collections.Generic;
using System.Runtime.Serialization;

using V6Soft.Common.ModelFactory;


namespace V6Soft.Services.Wcf.Common.Models
{
    [DataContract]
    public class ModelMapDC
    {
        /// <summary>
        ///     Gets or sets model mapping.
        /// </summary>
        [DataMember]
        public NameMapping NameMapping { get; set; }

        /// <summary>
        ///     Gets or sets list of field mappings.
        /// </summary>
        [DataMember]
        public IList<FieldMapping> FieldMappings { get; set; }

        /// <summary>
        ///     Gets or internally sets field groups
        /// </summary>
        [DataMember]
        public string[] FieldGroups { get; set; }
    }
}
