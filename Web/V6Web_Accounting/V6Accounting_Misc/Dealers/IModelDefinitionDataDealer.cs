using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using V6Soft.Common.ModelFactory;


namespace V6Soft.Interfaces.Accounting.Assistant.DataDealers
{
    public interface IModelDefinitionDataDealer
    {
        /// <summary>
        ///     Gets all model definitions.
        /// </summary>
        Task<IList<ModelDefinition>> GetModelDefinitions();

        /// <summary>
        ///     Gets all model mappings.
        /// </summary>
        Task<IList<ModelMap>> GetModelMaps();
    }
}
