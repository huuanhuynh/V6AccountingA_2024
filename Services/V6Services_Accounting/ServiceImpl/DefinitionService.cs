using System.ServiceModel;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Wcf.Common.Attributes;
using V6Soft.Services.Wcf.Common.TranslatorExtensions;
using V6Soft.Services.Wcf.Common.ServiceContracts;


namespace V6Soft.Services.Accounting.ServiceImpl
{
    /// <summary>
    ///     Implements <see cref="IDefinitionService"/>
    /// </summary>
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ServiceLogging]
    public class DefinitionService : IDefinitionService
    {
        private IDefinitionDataBaker m_DataBaker;
        private ILogger m_Logger;

        public DefinitionService(IDefinitionDataBaker definitionDataBaker,
            ILogger logger)
        {
            Guard.ArgumentNotNull(definitionDataBaker, "definitionDataBaker");
            Guard.ArgumentNotNull(logger, "logger");

            m_DataBaker = definitionDataBaker;
            m_Logger = logger;
        }

        /// <summary>
        /// Get one
        /// </summary>
        public GetModelDefinitionsResponse GetModelDefinition(GetModelDefinitionsRequest request)
        {
            var response = new GetModelDefinitionsResponse()
            {
                ModelDefinition = m_DataBaker.GetModelDefinition(request.ModelName),
                ModelMaps = m_DataBaker.GetModelMaps()
            };
            m_Logger.LogTrace("Returned {0} definition(s) and {1} mapping(s).",
                response.ModelDefinition != null ? 1 : 0,
                response.ModelMaps != null ? response.ModelMaps.Count : 0);

            return response;
        }

        /// <summary>
        ///     See <see cref="IDefinitionService.GetModelDefinitions"/>
        /// </summary>
        public GetModelDefinitionsResponse GetModelDefinitions(GetModelDefinitionsRequest request)
        {
            var response = new GetModelDefinitionsResponse()
            {
                ModelDefinitions = m_DataBaker.GetModelDefinitions(),
                ModelMaps = m_DataBaker.GetModelMaps()
            };
            m_Logger.LogTrace("Returned {0} definition(s) and {1} mapping(s).",
                response.ModelDefinitions != null ? response.ModelDefinitions.Count : 0,
                response.ModelMaps != null ? response.ModelMaps.Count : 0);

            return response;                 
        }
    }
}
