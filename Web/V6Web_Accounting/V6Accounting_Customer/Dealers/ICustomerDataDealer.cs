using System;
using System.Linq;
using System.Web.Http.OData.Query;
using V6Soft.Accounting.Common.Dealers;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Customer.Dealers
{
    /// <summary>
    ///     Acts as a service client to get customer data from CustomerService.
    /// </summary>
    public interface ICustomerDataDealer : IODataFriendly<DTO.Customer>
    {
        IQueryable<DTO.Customer> AsQueryable(ODataQueryOptions<DTO.Customer> queryOptions);
        DTO.Customer GetCustomer(Guid guid);
    }
}
