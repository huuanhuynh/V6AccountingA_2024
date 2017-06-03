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
using V6Soft.Web.Common.Controllers;
using V6Soft.Web.Common.Models;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Web.Accounting.Controllers.Customer
{
    public class Customer2ApiController : V6ApiControllerBase
    {
        private readonly ICustomerDataDealer m_CustomerDealer;

        public Customer2ApiController(ICustomerDataDealer customerDealer)
        {
            m_CustomerDealer = customerDealer;
        }

        /// <summary>
        ///     Gets customers
        /// </summary>
        [HttpPost]
        public async Task<PagedList<DynamicViewModel>> AddCustomer(ushort pageIndex = 1, ushort pageSize = 100)
        {
            PagedList<V6Soft.Models.Accounting.Customer> models = null;
            models = await m_CustomerDealer.GetCustomers(
                new List<string>() { DefinitionName.Fields.Code, 
                    DefinitionName.Fields.Name, 
                    DefinitionName.Fields.Note, 
                    DefinitionName.Fields.Status },
                null, pageIndex, pageSize
            );
            PagedList<DynamicViewModel> viewModels = ToViewModel(models);
            return viewModels;
        }

        /// <summary>
        /// Remove a customer
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<OperationResult> DeleteCustomer(string uid)
        {
            Guid g = Guid.Parse(uid);
            OperationResult result = await m_CustomerDealer.RemoveCustomer(g);
            return result;
        }

        /// <summary>
        ///     Gets customers
        /// </summary>
        [HttpGet]
        public async Task<PagedList<DynamicViewModel>> GetCustomers(ushort pageIndex = 1, ushort pageSize = 100)
        {
            PagedList<V6Soft.Models.Accounting.Customer> models = null;
            models = await m_CustomerDealer.GetCustomers(
                new List<string>() { DefinitionName.Fields.Code, 
                    DefinitionName.Fields.Name, 
                    DefinitionName.Fields.Note, 
                    DefinitionName.Fields.Status },
                null, pageIndex, pageSize
            );
            PagedList<DynamicViewModel> viewModels = ToViewModel(models);
            return viewModels;
        }

        /// <summary>
        ///     Gets customer groups
        /// </summary>
        [HttpGet]
        public async Task<PagedList<DynamicViewModel>> GetGroups()
        {
            PagedList<CustomerGroup> models = null;
            models = await m_CustomerDealer.GetCustomerGroups(
                new List<string>() { DefinitionName.Fields.Code, 
                    DefinitionName.Fields.Name, 
                    DefinitionName.Fields.Note, 
                    DefinitionName.Fields.Status },
                null, 1, 100
            );
            PagedList<DynamicViewModel> viewModels = ToViewModel(models);
            return viewModels;
        }

        /// <summary>
        ///     Gets customers
        /// </summary>
        [HttpGet]
        public async Task<PagedList<V6Soft.Models.Accounting.Customer>> GetCustomers(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize)
        {
            PagedList<V6Soft.Models.Accounting.Customer> items = await m_CustomerDealer.GetCustomers(
                outputFields, criteria, pageIndex, pageSize);
            return items ?? new PagedList<V6Soft.Models.Accounting.Customer>();
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