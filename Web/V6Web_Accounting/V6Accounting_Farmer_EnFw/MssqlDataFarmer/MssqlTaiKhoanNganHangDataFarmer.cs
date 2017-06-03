using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Banking.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.BankAccount;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IBankAccountDataFarmer"/>
    /// </summary>
    public class MssqlBankAccountDataFarmer : EnFwDataFarmerBase<ALtknh, Models.Accounting.DTO.BankAccount>, IBankAccountDataFarmer
    {
        private readonly IMappingRelatedToBankAccount mappingRelatedToUser;
        public MssqlBankAccountDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToBankAccount mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.BankAccount> GetBankAccounts(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("TaiKhoan")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.BankAccount GetBankAccountById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.BankAccount ToAppModel(ALtknh dbModel)
        {
            return mappingRelatedToUser.MapToBankAccount<Models.Accounting.DTO.BankAccount>(dbModel);
        }

        protected override ALtknh ToEntityModel(Models.Accounting.DTO.BankAccount appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALtknh>(appModel);
            return dbModel;
        }
    }
}
