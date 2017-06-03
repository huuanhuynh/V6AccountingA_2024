using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.IntermediateProduct.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.IntermediateProduct;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IIntermediateProductDataFarmer"/>
    /// </summary>
    public class MssqlIntermediateProductDataFarmer : EnFwDataFarmerBase<ALvttg, Models.Accounting.DTO.IntermediateProduct>, IIntermediateProductDataFarmer
    {
        private readonly IMappingRelatedToIntermediateProduct mappingRelatedToUser;
        public MssqlIntermediateProductDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToIntermediateProduct mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.IntermediateProduct> GetIntermediateProducts(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaVatTuTheGioi")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.IntermediateProduct GetIntermediateProductById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.IntermediateProduct ToAppModel(ALvttg dbModel)
        {
            return mappingRelatedToUser.MapToIntermediateProduct<Models.Accounting.DTO.IntermediateProduct>(dbModel);
        }

        protected override ALvttg ToEntityModel(Models.Accounting.DTO.IntermediateProduct appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALvttg>(appModel);
            return dbModel;
        }
    }
}
