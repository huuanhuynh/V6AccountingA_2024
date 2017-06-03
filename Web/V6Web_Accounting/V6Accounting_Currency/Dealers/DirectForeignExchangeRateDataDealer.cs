using System;
using System.Linq;
using V6Soft.Accounting.Currency.Dealers;
using V6Soft.Accounting.Currency.Extensions;
using V6Soft.Accounting.Currency.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.ForeignExchangeRate;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.ForeignExchangeRate.Dealers
{
    /// <summary>
    ///     Provides ForeignExchangeRateItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectForeignExchangeRateDataDealer : IForeignExchangeRateDataDealer
    {
        private ILogger m_Logger;
        private IForeignExchangeRateDataFarmer m_ForeignExchangeRateFarmer;

        public DirectForeignExchangeRateDataDealer(ILogger logger, IForeignExchangeRateDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_ForeignExchangeRateFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IForeignExchangeRateDataDealer.GetForeignExchangeRates()"/>
        /// </summary>
        public PagedSearchResult<ForeignExchangeRateListItem> GetForeignExchangeRates(SearchCriteria criteria)
        {
            PagedSearchResult<ForeignExchangeRateListItem> allForeignExchangeRates = m_ForeignExchangeRateFarmer.GetForeignExchangeRates(criteria).ToForeignExchangeRateViewModel();

            allForeignExchangeRates.Data = allForeignExchangeRates.Data
                .Select(item =>
                {
                    item.MaNgoaiTe = VnCodec.TCVNtoUNICODE(item.MaNgoaiTe);
                    return item;
                })
                .ToList();
            return allForeignExchangeRates;
        }
        /// <summary>
        ///     See <see cref="IForeignExchangeRateDataDealer.AddForeignExchangeRate()"/>
        /// </summary>
        public bool AddForeignExchangeRate(AccModels.ForeignExchangeRate customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_ForeignExchangeRateFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteForeignExchangeRate(string key)
        {
            return m_ForeignExchangeRateFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateForeignExchangeRate(AccModels.ForeignExchangeRate customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_ForeignExchangeRateFarmer.Edit(customer);
        }
    }
}
