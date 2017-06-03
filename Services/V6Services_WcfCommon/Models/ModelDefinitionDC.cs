using System.Collections.Generic;
using System.Runtime.Serialization;


namespace V6Soft.Services.Wcf.Common.Models
{
    [DataContract]
    public class ModelDefinitionDC
    {
        /// <summary>
        ///     Gets or sets the index.
        /// </summary>
        [DataMember]
        public ushort Index { get; set; }

        /// <summary>
        ///     Gets or sets model name, which acts as type name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets list of fields.
        /// </summary>
        [DataMember]
        public IList<ModelFieldDefinitionDC> Fields { get; set; }

        [DataMember]
        public string DBName { get; set; }
    }
}
