using System.Collections.Generic;
using System.Linq;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.Utils;
using V6Soft.Interfaces.Accounting.Assistant.DataDealers;


namespace V6Soft.Web.Common
{
    public class ServiceModelDefinitionLoader : IModelDefinitionManager
    {
        private IModelDefinitionDataDealer m_DataDealer;


        public ServiceModelDefinitionLoader(IModelDefinitionDataDealer dataDealer)
        {
            Guard.ArgumentNotNull(dataDealer, "dataDealer");
            m_DataDealer = dataDealer;
        }

        public ModelDefinition this[string modelName]
        {
            get { return Load(modelName); }
        }

        public ModelDefinition this[ushort definitionIndex]
        {
            get { return Load(definitionIndex); }
        }

        public ModelDefinition Load(string modelName)
        {
            IList<ModelDefinition> defs = LoadAll();
            if (defs == null) { return null; }

            ModelDefinition model =
                defs.FirstOrDefault(d => d.Name == modelName);
            return model;
        }

        public ModelDefinition Load(ushort definitionIndex)
        {
            IList<ModelDefinition> defs = LoadAll();
            if (defs == null ||
                definitionIndex < 0 ||
                definitionIndex >= defs.Count
                )
            {
                return null;
            }

            ModelDefinition model = defs[definitionIndex];
            return model;
        }

        public IList<ModelDefinition> LoadAll()
        {
            return m_DataDealer.GetModelDefinitions().Result;
        }

        public ModelMap GetMapping(ushort index)
        {
            IList<ModelMap> maps = GetAllMappings();
            if (maps == null || index < 0 || index >= maps.Count)
            {
                return null;
            }

            ModelMap mapping = maps[index];
            return mapping;
        }
        
        public IList<ModelMap> GetAllMappings()
        {
            return m_DataDealer.GetModelMaps().Result;
        }


        public bool AddField(ushort modelIndex, IList<ModelFieldDefinition> newFields)
        {
            throw new System.NotImplementedException();
        }
    }
}
