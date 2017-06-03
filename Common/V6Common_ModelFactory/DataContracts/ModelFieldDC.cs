using System.Runtime.Serialization;


namespace V6Soft.Common.ModelFactory.DataContracts
{
    [DataContract]
    public class ModelFieldDC
    {
        /// <summary>
        ///     Gets or sets field definition index. This index is used to
        ///     look up the relevant <see cref="ModelFieldDefinition"/> instance.
        /// </summary>
        [DataMember]
        public byte? DefinitionIndex { get; set; }
        
        /// <summary>
        ///     Gets or sets the owner model.
        /// </summary>
        [DataMember]
        public RuntimeModelDC OwnerModel { get; set; }
        
        /// <summary>
        ///     Gets or sets value stored in this field.
        /// </summary>
        [DataMember]
        public object Value { get; set; }
    }
}
