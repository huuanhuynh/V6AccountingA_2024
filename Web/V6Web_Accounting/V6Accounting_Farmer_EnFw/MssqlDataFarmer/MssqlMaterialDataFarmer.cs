using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using V6Soft.Accounting.Material.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Material;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using DTO = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IMaterialDataFarmer"/>
    /// </summary>
    public class MssqlMaterialDataFarmer : EnFwDataFarmerBase<ALvt, DTO.Material>, IMaterialDataFarmer
    {
        private readonly IMappingRelatedToMaterial mappingRelatedToUser;
        public MssqlMaterialDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToMaterial mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<DTO.Material> GetMaterials(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaVatTu")
                };
            }
            return FindByCriteria(criteria);
        }

        public DTO.Material GetMaterialById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        public IQueryable<DTO.Material> AsQueryable()
        {
            return m_Dbset.ProjectTo<DTO.Material>();
        }
        
        protected override DTO.Material ToAppModel(ALvt dbModel)
        {
            return mappingRelatedToUser.MapToMaterial<DTO.Material>(dbModel);
        }

        protected override ALvt ToEntityModel(DTO.Material appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALvt>(appModel);
            return dbModel;
        }
    }
}
