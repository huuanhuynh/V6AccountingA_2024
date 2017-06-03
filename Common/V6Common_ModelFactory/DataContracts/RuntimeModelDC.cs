using System.Collections.Generic;
using System.Runtime.Serialization;


namespace V6Soft.Common.ModelFactory.DataContracts
{
    [DataContract]
    public class RuntimeModelDC
    {
        /// <summary>
        ///     Gets or sets model type.
        /// </summary>
        [DataMember]
        public byte? DefinitionIndex { get; set; }

        /// <summary>
        ///     Gets or sets list of fields.
        /// </summary>
        [DataMember]
        public IList<ModelFieldDC> Fields { get; set; }

    }
}
