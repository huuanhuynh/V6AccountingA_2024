using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Material.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialType;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.MaterialType.Farmers;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IMaterialTypeDataFarmer"/>
    /// </summary>
    public class MssqlMaterialTypeDataFarmer : EnFwDataFarmerBase<ALloaivt, Models.Accounting.DTO.MaterialType>, IMaterialTypeDataFarmer
    {
        private readonly IMappingRelatedToMaterialType mappingRelatedToUser;
        public MssqlMaterialTypeDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToMaterialType mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.MaterialType> GetMaterialTypes(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("Loai_VatTu")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.MaterialType GetMaterialTypeById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.MaterialType ToAppModel(ALloaivt dbModel)
        {
            return mappingRelatedToUser.MapToMaterialType<Models.Accounting.DTO.MaterialType>(dbModel);
        }

        protected override ALloaivt ToEntityModel(Models.Accounting.DTO.MaterialType appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALloaivt>(appModel);
            return dbModel;
        }
    }
}
