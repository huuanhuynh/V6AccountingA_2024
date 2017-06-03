using System;
using System.Linq;
using V6Soft.Accounting.Discount.Farmers;
using V6Soft.Accounting.Duscount.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Discount;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Discount.Dealers
{
    /// <summary>
    ///     Provides DiscountItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectDiscountDataDealer : IDiscountDataDealer
    {
        private ILogger m_Logger;
        private IDiscountDataFarmer m_DiscountFarmer;

        public DirectDiscountDataDealer(ILogger logger, IDiscountDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_DiscountFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IDiscountDataDealer.GetDiscounts()"/>
        /// </summary>
        public PagedSearchResult<DiscountListItem> GetDiscounts(SearchCriteria criteria)
        {
            PagedSearchResult<DiscountListItem> allDiscounts = m_DiscountFarmer.GetDiscounts(criteria).ToDiscountViewModel();

            allDiscounts.Data = allDiscounts.Data
                .Select(item =>
                {
                    item.TenChietKhau = VnCodec.TCVNtoUNICODE(item.TenChietKhau);
                    return item;
                })
                .ToList();
            return allDiscounts;
        }
        /// <summary>
        ///     See <see cref="IDiscountDataDealer.AddDiscount()"/>
        /// </summary>
        public bool AddDiscount(AccModels.Discount customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_DiscountFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteDiscount(string key)
        {
            return m_DiscountFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateDiscount(AccModels.Discount customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_DiscountFarmer.Edit(customer);
        }
    }
}
