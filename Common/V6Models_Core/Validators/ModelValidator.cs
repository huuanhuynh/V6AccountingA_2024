using System;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Validators;
using V6Soft.Common.Utils;

using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Soft.Models.Core.Validators
{
    public class ModelValidator : IModelValidator
    {
        public void ValidateDefinition(RuntimeModel target, string definitionName)
        {
            Guard.ArgumentNotNull(target, "target");

            if (target.Definition == null)
            {
                throw new Exception("Definition is required.");
            }

            string expectedName = Enum.GetName(typeof(ModelIndex), 
                target.Definition.Index);

            // `expectedName` is NULL when this is not a predefined field.
            if (expectedName != null && !expectedName.Equals(definitionName))
            {
                throw new ArgumentException(
                    string.Format("Model name is '{0}' while it should be '{1}'.",
                        definitionName,
                        expectedName
                    )
                );
            }
        }
    }
}
