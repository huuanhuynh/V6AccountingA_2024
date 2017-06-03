using System.Collections.Generic;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.Utils;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Wcf.Common.Models;
using V6Soft.Services.Wcf.Common.TranslatorExtensions;


namespace V6Soft.Services.Accounting.DataBakers
{
    /// <summary>
    ///     Implements interface <see cref="IDefinitionDataBaker"/>
    /// </summary>
    public class DefinitionDataBaker : IDefinitionDataBaker
    {
        private IModelDefinitionManager m_DefinitionLoader;


        public DefinitionDataBaker(IModelDefinitionManager definitionLoader)
        {
            Guard.ArgumentNotNull(definitionLoader, "definitionLoader");
            m_DefinitionLoader = definitionLoader;
        }


        public ModelDefinitionDC GetModelDefinition(string modelName)
        {
            return m_DefinitionLoader.Load(modelName).ToDataContract();//.ToDataContracts();
        }

        /// <summary>
        ///     See <see cref="IDefinitionDataBaker.GetModelDefinitions"/>
        /// </summary>
        public IList<ModelDefinitionDC> GetModelDefinitions()
        {
            return m_DefinitionLoader.LoadAll().ToDataContracts();
        }

        

        /// <summary>
        ///     See <see cref="IDefinitionDataBaker.GetModelMaps"/>
        /// </summary>
        public IList<ModelMapDC> GetModelMaps()
        {
            return m_DefinitionLoader.GetAllMappings().ToDataContracts();
        }
    }
}
