using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Common.Dealers;
using V6Soft.Accounting.Customer.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using DTO = V6Soft.Models.Accounting.DTO;
using System.Web.Http.OData.Query;

namespace V6Soft.Accounting.Customer.Dealers
{
    /// <summary>
    ///     Provides CustomerItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectCustomerDataDealer : DataDealerBase, ICustomerDataDealer
    {
        private readonly ILogger m_Logger;
        private readonly ICustomerDataFarmer m_CustomerFarmer;
        
        public DirectCustomerDataDealer(ILogger logger, ICustomerDataFarmer customerFarmer)
            : base(customerFarmer.AsQueryable())
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_CustomerFarmer = customerFarmer;

        }

        public IQueryable<DTO.Customer> AsQueryable()
        {
            return m_CustomerFarmer.AsQueryable();
        }

        public void Save(IList<DynamicObject> models)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DTO.Customer> AsQueryable(ODataQueryOptions<DTO.Customer> queryOptions)
        {
            return (IQueryable<DTO.Customer>)queryOptions.ApplyTo(m_CustomerFarmer.AsQueryable());
        }

        public DTO.Customer GetCustomer(Guid guid)
        {
            return m_CustomerFarmer.AsQueryable().SingleOrDefault(re => re.UID.Equals(guid));
        }

        /// <summary>
        ///     See <see cref="ICustomerDataDealer.GetCustomers()"/>
        /// </summary>
        //public PagedSearchResult<CustomerListItem> GetCustomers(SearchCriteria criteria)
        //{
        //    PagedSearchResult<CustomerListItem> allCustomers = m_CustomerFarmer.GetCustomers(criteria).ToCustomerViewModel();

        //    allCustomers.Data = allCustomers.Data
        //        .Select(item =>
        //            {
        //                item.TenKhachHang = VnCodec.TCVNtoUNICODE(item.TenKhachHang);
        //                item.DiaChi = VnCodec.TCVNtoUNICODE(item.DiaChi);
        //                return item;
        //            })
        //        .ToList();
        //    return allCustomers;
        //}
        ///// <summary>
        /////     See <see cref="ICustomerDataDealer.AddCustomer()"/>
        ///// </summary>
        //public bool AddCustomer(DTO.Customer customer)
        //{
        //    customer.CreatedDate = DateTime.Now;
        //    customer.ModifiedDate = DateTime.Now;
        //    customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
        //    customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
        //    var result = m_CustomerFarmer.Add(customer);
        //    return result != null;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool DeleteCustomer(string key)
        //{
        //    return m_CustomerFarmer.Delete(key);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool UpdateCustomer(DTO.Customer customer)
        //{
        //    customer.CreatedDate = DateTime.Now;
        //    customer.ModifiedDate = DateTime.Now;
        //    customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
        //    customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
        //    return m_CustomerFarmer.Edit(customer);
        //}

        ///// <summary>
        /////     See <see cref="IODataFriendly.AsQueryable"/>
        ///// </summary>
        //public IQueryable<DTO.Customer> AsQueryable()
        //{
        //    return m_QueryProvider.CreateQuery<DTO.Customer>();
        //}

        //public void Save(IList<DynamicObject> models)
        //{

        //}
    }
}
