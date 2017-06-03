using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Banking.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Account;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IAccountDataFarmer"/>
    /// </summary>
    public class MssqlAccountDataFarmer : EnFwDataFarmerBase<Altk0, Models.Accounting.DTO.TaiKhoan>, ITaiKhoanDataFarmer
    {
        private readonly IMappingRelatedToAccount mappingRelatedToUser;
        public MssqlAccountDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToAccount mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.TaiKhoan> GetAccounts(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaNgoaiTe")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.TaiKhoan GetAccountById(Guid guid)
        {
            var taiKhoanDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(taiKhoanDb);
        }

        protected override Models.Accounting.DTO.TaiKhoan ToAppModel(Altk0 dbModel)
        {
            return mappingRelatedToUser.MapToAccount<Models.Accounting.DTO.TaiKhoan>(dbModel);
        }

        protected override Altk0 ToEntityModel(Models.Accounting.DTO.TaiKhoan appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Altk0>(appModel);
            return dbModel;
        }
    }
}
