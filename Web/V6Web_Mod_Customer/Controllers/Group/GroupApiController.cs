using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Interfaces.Accounting.Customer.DataDealers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Web.Common.Models;
using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Web.Accounting.Modules.Customer.Controllers.GroupApi
{
    public class GroupApiController : ApiController
    {
        private readonly ICustomerDataDealer m_CustomerDealer;

        public GroupApiController(ICustomerDataDealer customerDealer)
        {
            m_CustomerDealer = customerDealer;
        }


        /// <summary>
        ///     Gets customer groups
        /// </summary>
        [HttpGet]
        public async Task<DynamicTableModel> GetGroups()
        {
            PagedList<CustomerGroup> models = null;
            models = await m_CustomerDealer.GetCustomerGroups(
                new List<string>()
                {
                    DefinitionName.Fields.Code, 
                    DefinitionName.Fields.Name, 
                    DefinitionName.Fields.Status
                },
                null, 1, 100
            );
            return new DynamicTableModel()
                {
                    Columns = new string[] { "Code", "Name", "Status" },
                    ValueContainer = models
                };
        }

        /// <summary>
        ///     Gets customer groups
        /// </summary>
        [HttpGet]
        public async Task<PagedList<CustomerGroup>> GetGroups(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize)
        {
            PagedList<CustomerGroup> menuItems = await m_CustomerDealer.GetCustomerGroups(
                outputFields, criteria, pageIndex, pageSize);
            return menuItems ?? new PagedList<CustomerGroup>();
        }
    }
}