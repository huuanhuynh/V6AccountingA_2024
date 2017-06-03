using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.Utils;


namespace V6Soft.Common.ModelFactory.Validators
{
    public class DefaultModelValidator : IDynamicModelValidator
    {
        public bool Validate(DynamicModel model, IList<byte> excludedFieldIndeses)
        {
            Guard.ArgumentNotNull(model, "model");
            object[] values = model.Fields;
            Guard.ArgumentNotNullOrEmpty(values, "Field values");
            return true; //!!! huuan add 19/05/2015
            //ModelDefinition modelDef = RuntimeModelFactory.DefinitionLoader.Load(
            //    model.DefinitionIndex);
            ModelDefinition modelDef = DynamicModelFactory.DefinitionLoader.Load(model.DefinitionIndex);
            IList<ModelFieldDefinition> fieldDefs = modelDef.Fields;
            IList<IFieldConstraint> constraints;
            ModelFieldDefinition fDef;
            
            for (byte i = 0; i < fieldDefs.Count; ++i)
            {
                fDef = fieldDefs[i];
                if (excludedFieldIndeses != null 
                    && excludedFieldIndeses.Contains(i))
                { continue; }

                constraints = fDef.Constraints;
                // If one field fails, the whole validation fails.
                if (!ValidateConstraints(values[i], constraints))
                {
                    return false;
                }
            }
            return true;
        }


        public bool Validate(DynamicModel model)
        {
            return Validate(model, null);
        }

        private bool ValidateConstraints(object value,
            IList<IFieldConstraint> constraints)
        {
            foreach (var constraint in constraints)
            {
                // If one constraint fails, the whole validation fails.
                if (!constraint.Validate(value))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
