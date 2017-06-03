using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Material.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialGroup;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IMaterialGroupDataFarmer"/>
    /// </summary>
    public class MssqlMaterialGroupDataFarmer : EnFwDataFarmerBase<ALnhvt, Models.Accounting.DTO.MaterialGroup>, IMaterialGroupDataFarmer
    {
        private readonly IMappingRelatedToMaterialGroup mappingRelatedToUser;
        public MssqlMaterialGroupDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToMaterialGroup mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.MaterialGroup> GetMaterialGroups(SearchCriteria criteria)
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

        public Models.Accounting.DTO.MaterialGroup GetMaterialGroupById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.MaterialGroup ToAppModel(ALnhvt dbModel)
        {
            return mappingRelatedToUser.MapToMaterialGroup<Models.Accounting.DTO.MaterialGroup>(dbModel);
        }

        protected override ALnhvt ToEntityModel(Models.Accounting.DTO.MaterialGroup appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALnhvt>(appModel);
            return dbModel;
        }
    }
}
