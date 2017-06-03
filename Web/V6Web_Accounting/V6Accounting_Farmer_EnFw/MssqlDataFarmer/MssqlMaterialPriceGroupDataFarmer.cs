using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialPriceGroup;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.MaterialPriceGroup.Farmers;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IMaterialPriceGroupDataFarmer"/>
    /// </summary>
    public class MssqlMaterialPriceGroupDataFarmer : EnFwDataFarmerBase<Alnhvt2, Models.Accounting.DTO.MaterialPriceGroup>, IMaterialPriceGroupDataFarmer
    {
        private readonly IMappingRelatedToMaterialPriceGroup mappingRelatedToUser;
        public MssqlMaterialPriceGroupDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToMaterialPriceGroup mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.MaterialPriceGroup> GetMaterialPriceGroups(SearchCriteria criteria)
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

        public Models.Accounting.DTO.MaterialPriceGroup GetMaterialPriceGroupById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.MaterialPriceGroup ToAppModel(Alnhvt2 dbModel)
        {
            return mappingRelatedToUser.MapToMaterialPriceGroup<Models.Accounting.DTO.MaterialPriceGroup>(dbModel);
        }

        protected override Alnhvt2 ToEntityModel(Models.Accounting.DTO.MaterialPriceGroup appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alnhvt2>(appModel);
            return dbModel;
        }
    }
}
