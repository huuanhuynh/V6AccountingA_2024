using System;
using System.Linq;

using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Customer.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface ICustomerDataFarmer : IDataFarmerBase<DTO.Customer>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<DTO.Customer> GetCustomers(SearchCriteria criteria);

        /// <summary>
        /// Get a customer by guid
        /// </summary>
        DTO.Customer GetCustomerById(Guid guid);

        // TODO: Should put this method in IDataFarmerBase
        /// <summary>
        /// 
        /// </summary>
        IQueryable<DTO.Customer> AsQueryable();
    }
}
