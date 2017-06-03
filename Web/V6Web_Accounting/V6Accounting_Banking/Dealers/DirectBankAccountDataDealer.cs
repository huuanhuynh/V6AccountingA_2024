using System;
using System.Linq;
using V6Soft.Accounting.Banking.Dealers;
using V6Soft.Accounting.Banking.Extensions;
using V6Soft.Accounting.Banking.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.BankAccount;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.BankAccount.Dealers
{
    /// <summary>
    ///     Provides BankAccountItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectBankAccountDataDealer : IBankAccountDataDealer
    {
        private ILogger m_Logger;
        private IBankAccountDataFarmer m_BankAccountFarmer;

        public DirectBankAccountDataDealer(ILogger logger, IBankAccountDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_BankAccountFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IBankAccountDataDealer.GetBankAccounts()"/>
        /// </summary>
        public PagedSearchResult<BankAccountListItem> GetBankAccounts(SearchCriteria criteria)
        {
            PagedSearchResult<BankAccountListItem> allBankAccounts = m_BankAccountFarmer.GetBankAccounts(criteria).ToBankAccountViewModel();

            allBankAccounts.Data = allBankAccounts.Data
                .Select(item =>
                {
                    item.TenTaiKhoanNganHang = VnCodec.TCVNtoUNICODE(item.TenTaiKhoanNganHang);
                    item.TenTaiKhoanNganHang2 = VnCodec.TCVNtoUNICODE(item.TenTaiKhoanNganHang2);
                    return item;
                })
                .ToList();
            return allBankAccounts;
        }
        /// <summary>
        ///     See <see cref="IBankAccountDataDealer.AddBankAccount()"/>
        /// </summary>
        public bool AddBankAccount(AccModels.BankAccount customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_BankAccountFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteBankAccount(string key)
        {
            return m_BankAccountFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateBankAccount(AccModels.BankAccount customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_BankAccountFarmer.Edit(customer);
        }
    }
}
