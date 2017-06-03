using System;
using System.Linq;
using V6Soft.Accounting.Currency.Extensions;
using V6Soft.Accounting.Currency.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Capital;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Capital.Dealers
{
    /// <summary>
    ///     Provides CapitalItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectCapitalDataDealer : ICapitalDataDealer
    {
        private ILogger m_Logger;
        private ICapitalDataFarmer m_CapitalFarmer;

        public DirectCapitalDataDealer(ILogger logger, ICapitalDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_CapitalFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="ICapitalDataDealer.GetCapitals()"/>
        /// </summary>
        public PagedSearchResult<CapitalListItem> GetCapitals(SearchCriteria criteria)
        {
            PagedSearchResult<CapitalListItem> allCapitals = m_CapitalFarmer.GetCapitals(criteria).ToCapitalViewModel();

            allCapitals.Data = allCapitals.Data
                .Select(item =>
                {
                    item.TenNguonVon = VnCodec.TCVNtoUNICODE(item.TenNguonVon);
                    item.TenNguonVon2 = VnCodec.TCVNtoUNICODE(item.TenNguonVon2);
                    return item;
                })
                .ToList();
            return allCapitals;
        }
        /// <summary>
        ///     See <see cref="ICapitalDataDealer.AddCapital()"/>
        /// </summary>
        public bool AddCapital(AccModels.Capital customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_CapitalFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteCapital(string key)
        {
            return m_CapitalFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateCapital(AccModels.Capital customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_CapitalFarmer.Edit(customer);
        }
    }
}
