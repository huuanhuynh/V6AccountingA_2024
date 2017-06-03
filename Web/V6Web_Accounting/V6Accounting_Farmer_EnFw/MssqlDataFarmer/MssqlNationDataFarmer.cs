using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Geography.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Nation;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.INationDataFarmer"/>
    /// </summary>
    public class MssqlNationDataFarmer : EnFwDataFarmerBase<Alqg, Models.Accounting.DTO.Nation>, INationDataFarmer
    {
        private readonly IMappingRelatedToNation mappingRelatedToUser;
        public MssqlNationDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToNation mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Nation> GetNations(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaQuocGia")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Nation GetNationById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Nation ToAppModel(Alqg dbModel)
        {
            return mappingRelatedToUser.MapToNation<Models.Accounting.DTO.Nation>(dbModel);
        }

        protected override Alqg ToEntityModel(Models.Accounting.DTO.Nation appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alqg>(appModel);
            return dbModel;
        }
    }
}
