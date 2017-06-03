using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using V6Soft.Common.ModelFactory;


namespace V6Soft.Services.Wcf.Common.Models
{
    [KnownType(typeof(NotNullFieldConstraintDC))]
    [KnownType(typeof(LengthConstraintDC))]
    [DataContract]
    public class ModelFieldDefinitionDC
    {
        /// <summary>
        ///     Gets or sets label that is used to map a localized string.
        /// </summary>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        ///     Gets or sets field name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets field type.
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        ///     Gets or sets field group.
        /// </summary>
        [DataMember]
        public string Group { get; set; }

        /// <summary>
        ///     Gets or sets list of constraints applied to this field.
        /// </summary>
        [DataMember]
        public IList<IFieldConstraintDC> Constraints { get; set; }

    }
}
