using System;

using V6Soft.Common.ModelFactory.Validators;
using V6Soft.Common.Utils;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Models.Core.Validators
{
    public class FieldValidator : IFieldValidator
    {
        public void ValidateDefinition(Common.ModelFactory.ModelField target, string definitionName)
        {
            Guard.ArgumentNotNull(target, "target");

            if (!target.DefinitionIndex.HasValue)
            {
                throw new Exception("Must provide definition index before setting definition instance.");
            }

            string expectedName = Enum.GetName(typeof(FieldIndex), target.DefinitionIndex.Value);

            // `expectedName` is NULL when this is not a predefined field.
            if (expectedName != null && !expectedName.Equals(definitionName))
            {
                throw new ArgumentException(
                    string.Format("Field name is '{0}' while it should be '{1}'.",
                        definitionName,
                        expectedName
                    )
                );
            }

        }

        public void ValidateValue(Common.ModelFactory.ModelField target, object value)
        {
            Guard.ArgumentNotNull(target, "target");

            if (target.Definition == null)
            {
                throw new Exception("Must provide field definition before setting value.");
            }

            Type type = target.Definition.Type;
            if (type.IsClass || type.IsInterface)
            {
                Guard.ArgumentNotNull(value,
                    string.Format("Value of type {0} cannot be null.", type.FullName));
            }
        }
    }
}
