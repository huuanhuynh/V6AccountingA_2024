using System;
using System.Linq;
using V6Soft.Accounting.Merchandise.Farmers;
using V6Soft.Accounting.Warehouse.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.DanhMucLoHang;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Merchandise.Dealers
{
    /// <summary>
    ///     Provides MerchandiseItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectMerchandiseDataDealer : IMerchandiseDataDealer
    {
        private ILogger m_Logger;
        private IMerchandiseDataFarmer m_MerchandiseFarmer;

        public DirectMerchandiseDataDealer(ILogger logger, IMerchandiseDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_MerchandiseFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IMerchandiseDataDealer.GetMerchandises()"/>
        /// </summary>
        public PagedSearchResult<MerchandiseListItem> GetMerchandises(SearchCriteria criteria)
        {
            PagedSearchResult<MerchandiseListItem> allMerchandises = m_MerchandiseFarmer.GetMerchandises(criteria).ToMerchandiseViewModel();

            allMerchandises.Data = allMerchandises.Data
                .Select(item =>
                {
                    item.TenLo = VnCodec.TCVNtoUNICODE(item.TenLo);
                    item.TenLo2 = VnCodec.TCVNtoUNICODE(item.TenLo2);
                    return item;
                })
                .ToList();
            return allMerchandises;
        }
        /// <summary>
        ///     See <see cref="IMerchandiseDataDealer.AddMerchandise()"/>
        /// </summary>
        public bool AddMerchandise(AccModels.Merchandise customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_MerchandiseFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMerchandise(string key)
        {
            return m_MerchandiseFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateMerchandise(AccModels.Merchandise customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_MerchandiseFarmer.Edit(customer);
        }
    }
}
