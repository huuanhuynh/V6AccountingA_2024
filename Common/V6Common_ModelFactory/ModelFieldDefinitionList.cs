using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    ///     Represents a list of type ModelFieldDefinition.
    /// </summary>
    public class ModelFieldDefinitionList : List<ModelFieldDefinition>
    {
        /// <summary>
        ///     Gets field definition by its name.
        ///     <para/> Returns null if not found.
        /// </summary>
        public ModelFieldDefinition this[string fieldName] 
        { 
            get
            {
                return this.Where(item => item.Name.ToLower() == fieldName.ToLower()).FirstOrDefault();
            }
        }

        /// <summary>
        ///     Initializes a new instance of ModelFieldDefinitionList.
        /// </summary>
        public ModelFieldDefinitionList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of ModelFieldDefinitionList by copying
        ///     items from specified list.
        /// </summary>
        public ModelFieldDefinitionList(IList<ModelFieldDefinition> sourceList)
        {
            if (sourceList == null || !sourceList.Any()) { return; }

            foreach (var fieldDef in sourceList)
            {
                this.Add(fieldDef);
            }
        }

        /// <summary>
        ///     Checks if this list contains the specified field name.
        /// </summary>
        public bool HasField(string fieldName)
        {
            return this.Any(item => item.Name == fieldName);
        }
    }
}
