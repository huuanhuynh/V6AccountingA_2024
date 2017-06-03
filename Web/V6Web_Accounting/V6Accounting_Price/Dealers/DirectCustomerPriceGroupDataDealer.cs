using System;
using System.Linq;
using V6Soft.Accounting.Customer.Extensions;
using V6Soft.Accounting.CustomerPriceGroup.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.CustomerPriceGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.CustomerPriceGroup.Dealers
{
    /// <summary>
    ///     Provides CustomerPriceGroupItem-related operations (customerPriceGroup CRUD, customerPriceGroup group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectCustomerPriceGroupDataDealer : ICustomerPriceGroupDataDealer
    {
        private ILogger m_Logger;
        private ICustomerPriceGroupDataFarmer m_CustomerPriceGroupFarmer;

        public DirectCustomerPriceGroupDataDealer(ILogger logger, ICustomerPriceGroupDataFarmer customerPriceGroupFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerPriceGroupFarmer, "customerPriceGroupFarmer");

            m_Logger = logger;
            m_CustomerPriceGroupFarmer = customerPriceGroupFarmer;
        }
        /// <summary>
        ///     See <see cref="ICustomerPriceGroupDataDealer.GetCustomerPriceGroups()"/>
        /// </summary>
        public PagedSearchResult<CustomerPriceGroupListItem> GetCustomerPriceGroups(SearchCriteria criteria)
        {
            PagedSearchResult<CustomerPriceGroupListItem> allCustomerPriceGroups = m_CustomerPriceGroupFarmer.GetCustomerPriceGroups(criteria).ToCustomerPriceGroupViewModel();

            allCustomerPriceGroups.Data = allCustomerPriceGroups.Data
                .Select(item =>
                {
                    item.TenNhom = VnCodec.TCVNtoUNICODE(item.TenNhom);
                    item.TenNhom2 = VnCodec.TCVNtoUNICODE(item.TenNhom2);
                    return item;
                })
                .ToList();
            return allCustomerPriceGroups;
        }
        /// <summary>
        ///     See <see cref="ICustomerPriceGroupDataDealer.AddCustomerPriceGroup()"/>
        /// </summary>
        public bool AddCustomerPriceGroup(AccModels.CustomerPriceGroup customerPriceGroup)
        {
            customerPriceGroup.CreatedDate = DateTime.Now;
            customerPriceGroup.ModifiedDate = DateTime.Now;
            customerPriceGroup.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customerPriceGroup.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_CustomerPriceGroupFarmer.Add(customerPriceGroup);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteCustomerPriceGroup(string key)
        {
            return m_CustomerPriceGroupFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateCustomerPriceGroup(AccModels.CustomerPriceGroup customerPriceGroup)
        {
            customerPriceGroup.CreatedDate = DateTime.Now;
            customerPriceGroup.ModifiedDate = DateTime.Now;
            customerPriceGroup.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customerPriceGroup.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_CustomerPriceGroupFarmer.Edit(customerPriceGroup);
        }
    }
}
