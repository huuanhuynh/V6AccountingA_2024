using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Currency.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Capital;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ICapitalDataFarmer"/>
    /// </summary>
    public class MssqlCapitalDataFarmer : EnFwDataFarmerBase<Alnv, Models.Accounting.DTO.Capital>, ICapitalDataFarmer
    {
        private readonly IMappingRelatedToCapital mappingRelatedToUser;
        public MssqlCapitalDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToCapital mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Capital> GetCapitals(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaNguonVon")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Capital GetCapitalById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Capital ToAppModel(Alnv dbModel)
        {
            return mappingRelatedToUser.MapToCapital<Models.Accounting.DTO.Capital>(dbModel);
        }

        protected override Alnv ToEntityModel(Models.Accounting.DTO.Capital appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alnv>(appModel);
            return dbModel;
        }
    }
}
