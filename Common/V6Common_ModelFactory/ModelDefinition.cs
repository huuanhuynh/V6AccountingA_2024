using System.Collections.Generic;
using System.Linq;
using V6Soft.Common.Utils;


namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    /// Represents definition of a runtime model.
    /// </summary>
    public class ModelDefinition
    {
        /// <summary>
        ///     Gets or internally sets the index.
        /// </summary>
        public ushort Index { get; internal set; }

        /// <summary>
        ///     Gets or internally sets model name, which acts as type name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     Gets or internally sets list of fields.
        /// </summary>
        public ModelFieldDefinitionList Fields { get; internal set; }

        
        /// <summary>
        ///     Creates a new instance of type <see cref="ModelDefinition"/>
        ///     with an empty Field list.
        /// </summary>
        public ModelDefinition(string name)
        {
            Guard.ArgumentNotNullOrEmpty(name, "name");

            Name = name;
            Fields = new ModelFieldDefinitionList();
        }

        /// <summary>
        ///     Creates a new instance of type <see cref="ModelDefinition"/>
        /// </summary>
        public ModelDefinition(ushort index, string name, IList<ModelFieldDefinition> fields = null)
        {
            Guard.ArgumentNotNullOrEmpty(name, "name");

            Index = index;
            Name = name;
            Fields = new ModelFieldDefinitionList(fields);
        }
        
        /// <summary>
        ///     Gets field definition by name.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public ModelFieldDefinition GetField(string fieldName)
        {
            return Fields.Where(f => f.Name == fieldName).FirstOrDefault();
        }

        public override string ToString()
        {
            return string.Format("Name={0}, {1} field(s), Index={2}",
                Name, Fields.Count, Index);
        }

    }
}
