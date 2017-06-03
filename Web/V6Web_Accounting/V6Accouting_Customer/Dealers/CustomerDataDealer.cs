using System.Collections.Generic;
using V6Accounting_EntityFramework;
using V6Accounting_EntityFramework.Entities;
using V6Accouting_Customer.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Accouting_Customer.Dealers
{
    public class CustomerDataDealer : ICustomerDataDealer
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICustomerDataFarmer farmer;

        public CustomerDataDealer()
        {
            var context = new V6AccountingContext();
            unitOfWork = new UnitOfWork(context);
            farmer = new CustomerDataFarmer(context);
        }

        public PagedSearchResult<Customer> GetCustomers(SearchCriteria criteria)
        {
            PagedSearchResult<Customer> a =
                farmer.SearchCustomers(criteria);
            return a;
        }


        public PagedSearchResult<Customer> Add(Customer obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
