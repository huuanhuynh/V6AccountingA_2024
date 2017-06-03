using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Currency.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.ForeignCurrency;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IForeignCurrencyDataFarmer"/>
    /// </summary>
    public class MssqlForeignCurrencyDataFarmer : EnFwDataFarmerBase<Alnt, Models.Accounting.DTO.ForeignCurrency>, IForeignCurrencyDataFarmer
    {
        private readonly IMappingRelatedToForeignCurrency mappingRelatedToUser;
        public MssqlForeignCurrencyDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToForeignCurrency mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.ForeignCurrency> GetForeignCurrencys(SearchCriteria criteria)
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
        
        public Models.Accounting.DTO.ForeignCurrency GetForeignCurrencyById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.ForeignCurrency ToAppModel(Alnt dbModel)
        {
            return mappingRelatedToUser.MapToForeignCurrency<Models.Accounting.DTO.ForeignCurrency>(dbModel);
        }

        protected override Alnt ToEntityModel(Models.Accounting.DTO.ForeignCurrency appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alnt>(appModel);
            return dbModel;
        }
    }
}
