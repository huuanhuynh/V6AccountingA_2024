using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using V6Soft.Common.ModelFactory;
using V6Soft.Interfaces.Accounting.Customer.DataDealers;
using V6Soft.Models.Core;
using V6Soft.Web.Common.Models;
using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;
using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;

namespace V6Soft.Web.Accounting.Modules.Customer.Controllers.ModelDefinitionApi
{
    public class ModelDefinitionApiController : ApiController
    {
        private readonly IModelDefinitionDataDealer m_DefinitionDealer;

        public ModelDefinitionApiController(IModelDefinitionDataDealer definitionDealer)
        {
            m_DefinitionDealer = definitionDealer;
        }

        [HttpGet]
        public async Task<DynamicTableModel> GetModelDefinition(ushort modelIndex)
        {
            IList<ModelDefinition> defs = await m_DefinitionDealer.GetModelDefinitions();
            ModelDefinition result = null;
            foreach (ModelDefinition item in defs)
            {
                if (item.Index == modelIndex)
                {
                    result = item;
                    break;
                }
            }
            return new DynamicTableModel
            {
                ValueContainer = result
            };
        }


        // PUT api/customerapi/5
        public void Put(int id, [FromBody]string value)
        {
        }


    }
}
