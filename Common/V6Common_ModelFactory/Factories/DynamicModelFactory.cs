using System;

using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.ModelFactory.Validators;
using V6Soft.Common.Utils;


namespace V6Soft.Common.ModelFactory.Factories
{
    public class DynamicModelFactory
    {
        /// <summary>
        ///     Gets or sets definition loader.
        /// </summary>
        public static IModelDefinitionManager DefinitionLoader { get; set; }

        /// <summary>
        ///     Gets or sets model validator
        /// </summary>
        public static IDynamicModelValidator DynamicModelValidator { get; set; }

        /// <summary>
        ///     Spawns a runtime instance of model with specified name.
        ///     <para />Throws exception if no definition for this model name.
        /// </summary>
        /// <param name="definitionIndex">Model definition index.</param>
        public static DynamicModel CreateModel(ushort definitionIndex)
        {
            Guard.ArgumentNotNull(DefinitionLoader, "DefinitionLoader");

            ModelDefinition definition = DefinitionLoader.Load(definitionIndex);

            if (definition == null)
            {
                throw new NoDefinitionException(definitionIndex);
            }

            var modelInstance = new DynamicModel();
            modelInstance.DefinitionIndex = definition.Index;

            return modelInstance;
        }
    }
}
