using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Banking.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.AccountType;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IAccountTypeDataFarmer"/>
    /// </summary>
    public class MssqlAccountTypeDataFarmer : EnFwDataFarmerBase<ALnhtk0, Models.Accounting.DTO.AccountType>, IAccountTypeDataFarmer
    {
        private IMappingRelatedToAccountType _mappingRelatedToAccountType;
        public MssqlAccountTypeDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToAccountType _mappingRelatedToAccountType)
            : base(dbContext)
        {
            this._mappingRelatedToAccountType = _mappingRelatedToAccountType;
        }

        public PagedSearchResult<Models.Accounting.DTO.AccountType> GetAccountTypes(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaNhom")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.AccountType GetAccountTypeById(Guid guid)
        {
            var phanLoaiTaiKhoanDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(phanLoaiTaiKhoanDb);
        }



        protected override Models.Accounting.DTO.AccountType ToAppModel(ALnhtk0 dbModel)
        {
            return _mappingRelatedToAccountType.MapToAccountType<Models.Accounting.DTO.AccountType>(dbModel);
        }

        protected override ALnhtk0 ToEntityModel(Models.Accounting.DTO.AccountType appModel)
        {
            var dbModel = _mappingRelatedToAccountType.MapToALnhtk0<ALnhtk0>(appModel);
            return dbModel;
        }
    }
}
