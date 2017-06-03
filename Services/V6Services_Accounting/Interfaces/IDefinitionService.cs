using System.ServiceModel;

using V6Soft.Services.Wcf.Common.ServiceContracts;


namespace V6Soft.Services.Accounting.Interfaces
{
    /// <summary>
    ///     Provides operations to work with model definitions and mappings.
    /// </summary>
    [ServiceContract]
    public interface IDefinitionService
    {
        [OperationContract]
        GetModelDefinitionsResponse GetModelDefinition(GetModelDefinitionsRequest request);

        /// <summary>
        ///     Gets all model definitions and model mappings
        /// </summary>
        [OperationContract]
        GetModelDefinitionsResponse GetModelDefinitions(GetModelDefinitionsRequest request);

    }
}
