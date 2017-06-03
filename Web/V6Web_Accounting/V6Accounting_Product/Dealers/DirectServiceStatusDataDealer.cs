using System;
using System.Linq;
using V6Soft.Accounting.Product.Extensions;
using V6Soft.Accounting.ServiceStatus.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.ServiceStatus;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.ServiceStatus.Dealers
{
    /// <summary>
    ///     Provides ServiceStatusItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectServiceStatusDataDealer : IServiceStatusDataDealer
    {
        private ILogger m_Logger;
        private IServiceStatusDataFarmer m_ServiceStatusFarmer;

        public DirectServiceStatusDataDealer(ILogger logger, IServiceStatusDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_ServiceStatusFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IServiceStatusDataDealer.GetServiceStatuss()"/>
        /// </summary>
        public PagedSearchResult<ServiceStatusListItem> GetServiceStatuss(SearchCriteria criteria)
        {
            PagedSearchResult<ServiceStatusListItem> allServiceStatuss = m_ServiceStatusFarmer.GetServiceStatuss(criteria).ToServiceStatusViewModel();

            allServiceStatuss.Data = allServiceStatuss.Data
                .Select(item =>
                {
                    item.Ten_tt = VnCodec.TCVNtoUNICODE(item.Ten_tt);
                    item.Ten_tt2 = VnCodec.TCVNtoUNICODE(item.Ten_tt2);
                    return item;
                })
                .ToList();
            return allServiceStatuss;
        }
        /// <summary>
        ///     See <see cref="IServiceStatusDataDealer.AddServiceStatus()"/>
        /// </summary>
        public bool AddServiceStatus(AccModels.ServiceStatus customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_ServiceStatusFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteServiceStatus(string key)
        {
            return m_ServiceStatusFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateServiceStatus(AccModels.ServiceStatus customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_ServiceStatusFarmer.Edit(customer);
        }
    }
}
