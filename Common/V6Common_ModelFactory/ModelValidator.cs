using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.ModelFactory
{
    public class ModelValidator : IModelValidator
    {
        public void ValidateDefinition(RuntimeModel target)
        {
            if (!target.DefinitionIndex.HasValue)
            {
                throw new Exception("Definition index is required.");
            }

            string expectedName = Enum.GetName(typeof(target.ModelIndex), 
                target.DefinitionIndex.Value);

            // `expectedName` is NULL when this is not a predefined field.
            if (expectedName != null && !expectedName.Equals(value.Name))
            {
                throw new ArgumentException(
                    string.Format("Model name is '{0}' while it should be '{1}'.",
                        value.Name,
                        expectedName
                    )
                );
            }
        }
    }
}
