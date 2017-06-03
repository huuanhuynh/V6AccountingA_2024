using System.Collections.Generic;
using V6Accounting_EntityFramework;
using V6Accounting_EntityFramework.Entities;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Accouting_Customer.Farmers
{
    public interface ICustomerDataFarmer : IGenericRepository<Alkh>
    {
        PagedSearchResult<Customer> SearchCustomers(SearchCriteria criteria);
    }
}
