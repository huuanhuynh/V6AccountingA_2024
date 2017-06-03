using System;

using V6Soft.Common.ModelFactory.Validators;
using V6Soft.Common.Utils;


namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    /// Represents a model fields which contains values.
    /// </summary>
    [Obsolete("This class is ever used.")]
    public class ModelField
    {
        private object m_Value;
        private ModelFieldDefinition m_Definition;

        /// <summary>
        /// Gets or internally sets field definition index. This index is used to
        /// look up the relevant <see cref="ModelFieldDefinition"/> instance.
        /// </summary>
        public byte? DefinitionIndex { get; internal set; }

        /// <summary>
        /// Gets or internally sets the owner model.
        /// </summary>
        public RuntimeModel OwnerModel { get; internal set; }

        /// <summary>
        /// Gets or internally sets field definition.
        /// </summary>
        public ModelFieldDefinition Definition 
        {
            get { return m_Definition; }
            internal set
            {
                Guard.ArgumentNotNull(value, "Definition");

                m_Definition = value;
            }
        }

        /// <summary>
        /// Gets or sets value stored in this field.
        /// </summary>
        public object Value 
        { 
            get { return m_Value; }
            set
            {                
                // Ex:
                //  `value`: long
                //  `type`: int
                //  => converts from 64 bytes to 32 bytes
                m_Value = Convert.ChangeType(value, Definition.Type);
            }
        }


        /// <summary>
        /// Creates new instance of <see cref="ModelField"/>.
        /// </summary>
        public ModelField()
        {
            DefinitionIndex = null;
        }

        /// <summary>
        /// Creates new instance of <see cref="ModelField"/>.
        /// </summary>
        public ModelField(byte definitionIndex)
        {
            DefinitionIndex = definitionIndex;
        }

        public override string ToString()
        {
            return string.Format("Value={0}", Value);
        }
    }
}
