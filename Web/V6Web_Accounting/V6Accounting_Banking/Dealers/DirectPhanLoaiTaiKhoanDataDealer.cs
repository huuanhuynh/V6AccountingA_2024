using System;
using System.Linq;
using V6Soft.Accounting.AccountType.Dealers;
using V6Soft.Accounting.Banking.Extensions;
using V6Soft.Accounting.Banking.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.AccountType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Banking.Dealers
{
    /// <summary>
    ///     Provides AccountTypeItem-related operations (accountType CRUD, accountType group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectAccountTypeDataDealer : IAccountTypeDataDealer
    {
        private ILogger m_Logger;
        private IAccountTypeDataFarmer m_AccountTypeFarmer;

        public DirectAccountTypeDataDealer(ILogger logger, IAccountTypeDataFarmer accountTypeFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(accountTypeFarmer, "accountTypeFarmer");

            m_Logger = logger;
            m_AccountTypeFarmer = accountTypeFarmer;
        }
        /// <summary>
        ///     See <see cref="IAccountTypeDataDealer.GetAccountTypes()"/>
        /// </summary>
        public PagedSearchResult<AccountTypeListItem> GetAccountTypes(SearchCriteria criteria)
        {
            PagedSearchResult<AccountTypeListItem> allAccountTypes = m_AccountTypeFarmer.GetAccountTypes(criteria).ToPhanLoaiTaiKhoanViewModel();

            allAccountTypes.Data = allAccountTypes.Data
                .Select(item =>
                {
                    item.MaNhom = VnCodec.TCVNtoUNICODE(item.MaNhom);
                    item.TenNhom = VnCodec.TCVNtoUNICODE(item.TenNhom);
                    return item;
                })
                .ToList();
            return allAccountTypes;
        }
        /// <summary>
        ///     See <see cref="IAccountTypeDataDealer.AddAccountType()"/>
        /// </summary>
        public bool AddAccountType(Models.Accounting.DTO.AccountType accountType)
        {
            accountType.CreatedDate = DateTime.Now;
            accountType.ModifiedDate = DateTime.Now;
            accountType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            accountType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_AccountTypeFarmer.Add(accountType);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteAccountType(string key)
        {
            return m_AccountTypeFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateAccountType(Models.Accounting.DTO.AccountType accountType)
        {
            accountType.CreatedDate = DateTime.Now;
            accountType.ModifiedDate = DateTime.Now;
            accountType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            accountType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_AccountTypeFarmer.Edit(accountType);
        }
    }
}
