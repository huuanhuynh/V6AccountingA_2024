using System;
using System.Linq;
using V6Soft.Accounting.Banking.Dealers;
using V6Soft.Accounting.Banking.Extensions;
using V6Soft.Accounting.Banking.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Account;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Account.Dealers
{
    /// <summary>
    ///     Provides AccountItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectAccountDataDealer : IAccountDataDealer
    {
        private ILogger m_Logger;
        private ITaiKhoanDataFarmer m_AccountFarmer;

        public DirectAccountDataDealer(ILogger logger, ITaiKhoanDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_AccountFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IAccountDataDealer.GetAccounts()"/>
        /// </summary>
        public PagedSearchResult<AccountListItem> GetAccounts(SearchCriteria criteria)
        {
            PagedSearchResult<AccountListItem> allAccounts = m_AccountFarmer.GetAccounts(criteria).ToTaiKhoanViewModel();

            allAccounts.Data = allAccounts.Data
                .Select(item =>
                {
                    item.Ten_TaiKhoan = VnCodec.TCVNtoUNICODE(item.Ten_TaiKhoan);
                    item.Ten_TaiKhoan2 = VnCodec.TCVNtoUNICODE(item.Ten_TaiKhoan2);
                    return item;
                })
                .ToList();
            return allAccounts;
        }
        /// <summary>
        ///     See <see cref="IAccountDataDealer.AddAccount()"/>
        /// </summary>
        public bool AddAccount(AccModels.TaiKhoan customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_AccountFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteAccount(string key)
        {
            return m_AccountFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateAccount(AccModels.TaiKhoan customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_AccountFarmer.Edit(customer);
        }
    }
}
