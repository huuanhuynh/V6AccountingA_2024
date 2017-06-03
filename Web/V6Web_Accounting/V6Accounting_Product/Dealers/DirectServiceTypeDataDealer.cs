using System;
using System.Linq;
using V6Soft.Accounting.Product.Extensions;
using V6Soft.Accounting.ServiceType.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.ServiceType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.ServiceType.Dealers
{
    /// <summary>
    ///     Provides ServiceTypeItem-related operations (serviceType CRUD, serviceType group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectServiceTypeDataDealer : IServiceTypeDataDealer
    {
        private ILogger m_Logger;
        private IServiceTypeDataFarmer m_ServiceTypeFarmer;

        public DirectServiceTypeDataDealer(ILogger logger, IServiceTypeDataFarmer serviceTypeFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(serviceTypeFarmer, "serviceTypeFarmer");

            m_Logger = logger;
            m_ServiceTypeFarmer = serviceTypeFarmer;
        }
        /// <summary>
        ///     See <see cref="IServiceTypeDataDealer.GetServiceTypes()"/>
        /// </summary>
        public PagedSearchResult<ServiceTypeListItem> GetServiceTypes(SearchCriteria criteria)
        {
            PagedSearchResult<ServiceTypeListItem> allServiceTypes = m_ServiceTypeFarmer.GetServiceTypes(criteria).ToServiceTypeViewModel();

            allServiceTypes.Data = allServiceTypes.Data
                .Select(item =>
                {
                    item.Ten_Loai = VnCodec.TCVNtoUNICODE(item.Ten_Loai);
                    item.Ten_Loai2 = VnCodec.TCVNtoUNICODE(item.Ten_Loai2);
                    return item;
                })
                .ToList();
            return allServiceTypes;
        }
        /// <summary>
        ///     See <see cref="IServiceTypeDataDealer.AddServiceType()"/>
        /// </summary>
        public bool AddServiceType(AccModels.ServiceType serviceType)
        {
            serviceType.CreatedDate = DateTime.Now;
            serviceType.ModifiedDate = DateTime.Now;
            serviceType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            serviceType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_ServiceTypeFarmer.Add(serviceType);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteServiceType(string key)
        {
            return m_ServiceTypeFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateServiceType(AccModels.ServiceType serviceType)
        {
            serviceType.CreatedDate = DateTime.Now;
            serviceType.ModifiedDate = DateTime.Now;
            serviceType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            serviceType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_ServiceTypeFarmer.Edit(serviceType);
        }
    }
}
