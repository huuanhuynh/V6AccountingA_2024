using System;
using System.Linq;
using V6Soft.Accounting.Currency.Extensions;
using V6Soft.Accounting.Currency.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.ForeignCurrency;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.ForeignCurrency.Dealers
{
    /// <summary>
    ///     Provides ForeignCurrencyItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectForeignCurrencyDataDealer : IForeignCurrencyDataDealer
    {
        private ILogger m_Logger;
        private IForeignCurrencyDataFarmer m_ForeignCurrencyFarmer;

        public DirectForeignCurrencyDataDealer(ILogger logger, IForeignCurrencyDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_ForeignCurrencyFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IForeignCurrencyDataDealer.GetForeignCurrencys()"/>
        /// </summary>
        public PagedSearchResult<ForeignCurrencyListItem> GetForeignCurrencys(SearchCriteria criteria)
        {
            PagedSearchResult<ForeignCurrencyListItem> allForeignCurrencys = m_ForeignCurrencyFarmer.GetForeignCurrencys(criteria).ToForeignCurrencyViewModel();

            allForeignCurrencys.Data = allForeignCurrencys.Data
                .Select(item =>
                {
                    item.TenNgoaiTe = VnCodec.TCVNtoUNICODE(item.TenNgoaiTe);
                    item.TenNgoaiTe2 = VnCodec.TCVNtoUNICODE(item.TenNgoaiTe2);
                    return item;
                })
                .ToList();
            return allForeignCurrencys;
        }
        /// <summary>
        ///     See <see cref="IForeignCurrencyDataDealer.AddForeignCurrency()"/>
        /// </summary>
        public bool AddForeignCurrency(AccModels.ForeignCurrency customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_ForeignCurrencyFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteForeignCurrency(string key)
        {
            return m_ForeignCurrencyFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateForeignCurrency(AccModels.ForeignCurrency customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_ForeignCurrencyFarmer.Edit(customer);
        }
    }
}
