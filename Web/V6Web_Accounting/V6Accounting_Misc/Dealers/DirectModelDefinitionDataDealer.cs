using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.Utils.TaskExtensions;
using V6Soft.Interfaces.Accounting.Assistant.DataDealers;


namespace V6Soft.Dealers.Accounting.Customer
{
    /// <summary>
    ///     Implements <see cref="IModelDefinitionDataDealer"/>
    /// </summary>
    public class DirectModelDefinitionDataDealer : IModelDefinitionDataDealer
    {
        private IModelDefinitionManager m_DefinitionLoader;


        /// <summary>
        ///     Initializes an instance of DirectModelDefinitionDataDealer
        /// </summary>
        public DirectModelDefinitionDataDealer(
            IModelDefinitionManager definitionLoader)
        {
            m_DefinitionLoader = definitionLoader;
        }

        /// <summary>
        ///     See <see cref="IModelDefinitionDataDealer.GetModelDefinitions"/>
        /// </summary>
        public Task<IList<ModelDefinition>> GetModelDefinitions()
        {
            var source = new TaskCompletionSource<IList<ModelDefinition>>();

            Task.Factory.StartNew(() =>
            {
                return m_DefinitionLoader.LoadAll();
            }).Then(modelDefinitions =>
            {
                source.SetResult(modelDefinitions);
            });

            return source.Task;
        }

        /// <summary>
        ///     See <see cref="IModelDefinitionDataDealer.GetModelMaps"/>
        /// </summary>
        public Task<IList<ModelMap>> GetModelMaps()
        {
            var source = new TaskCompletionSource<IList<ModelMap>>();

            Task.Factory.StartNew(() =>
            {
                return m_DefinitionLoader.GetAllMappings();
            }).Then(modelMaps =>
            {
                source.SetResult(modelMaps);
            });

            return source.Task;
        }
    }
}
