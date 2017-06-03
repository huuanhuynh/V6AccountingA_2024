using System.Collections.Generic;

using V6Soft.Common.ModelFactory;
using V6Soft.Services.Wcf.Common.Models;


namespace V6Soft.Services.Accounting.Interfaces
{
    /// <summary>
    ///     Collects model definitions and mappings from farmer(s) then 
    ///     do some processings before returning results to definition service.
    /// </summary>
    public interface IDefinitionDataBaker
    {
        ModelDefinitionDC GetModelDefinition(string modelName);
        /// <summary>
        ///     Gets all model definitions.
        /// </summary>
        IList<ModelDefinitionDC> GetModelDefinitions();

        /// <summary>
        ///     Gets all model mappings.
        /// </summary>
        IList<ModelMapDC> GetModelMaps();
    }
}
