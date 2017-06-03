using System.Collections.Generic;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Accouting_Customer.Dealers
{
    public interface ICustomerDataDealer
    {
        PagedSearchResult<Customer> GetCustomers(SearchCriteria criteria);

        PagedSearchResult<Customer> Add(Customer obj);
    }
}
