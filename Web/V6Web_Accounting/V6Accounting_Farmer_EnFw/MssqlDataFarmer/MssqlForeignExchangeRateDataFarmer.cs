using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Currency.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.ForeignExchangeRate;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IForeignExchangeRateDataFarmer"/>
    /// </summary>
    public class MssqlForeignExchangeRateDataFarmer : EnFwDataFarmerBase<ALtgnt, Models.Accounting.DTO.ForeignExchangeRate>, IForeignExchangeRateDataFarmer
    {
        private readonly IMappingRelatedToForeignExchangeRate mappingRelatedToUser;
        public MssqlForeignExchangeRateDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToForeignExchangeRate mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.ForeignExchangeRate> GetForeignExchangeRates(SearchCriteria criteria)
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

        public Models.Accounting.DTO.ForeignExchangeRate GetForeignExchangeRateById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.ForeignExchangeRate ToAppModel(ALtgnt dbModel)
        {
            return mappingRelatedToUser.MapToForeignExchangeRate<Models.Accounting.DTO.ForeignExchangeRate>(dbModel);
        }

        protected override ALtgnt ToEntityModel(Models.Accounting.DTO.ForeignExchangeRate appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALtgnt>(appModel);
            return dbModel;
        }
    }
}
