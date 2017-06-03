using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.District;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.Geography.Farmers;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IDistrictDataFarmer"/>
    /// </summary>
    public class MssqlDistrictDataFarmer : EnFwDataFarmerBase<Alquan, Models.Accounting.DTO.District>, IDistrictDataFarmer
    {
        private readonly IMappingRelatedToDistrict mappingRelatedToUser;
        public MssqlDistrictDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToDistrict mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.District> GetDistricts(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaQuan")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.District GetDistrictById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.District ToAppModel(Alquan dbModel)
        {
            return mappingRelatedToUser.MapToDistrict<Models.Accounting.DTO.District>(dbModel);
        }

        protected override Alquan ToEntityModel(Models.Accounting.DTO.District appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alquan>(appModel);
            return dbModel;
        }
    }
}
