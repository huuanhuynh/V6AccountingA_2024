using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Indenture.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Indenture;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IIndentureDataFarmer"/>
    /// </summary>
    public class MssqlIndentureDataFarmer : EnFwDataFarmerBase<Alku, Models.Accounting.DTO.Indenture>, IIndentureDataFarmer
    {
        private readonly IMappingRelatedToIndenture mappingRelatedToUser;
        public MssqlIndentureDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToIndenture mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Indenture> GetIndentures(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaKheUoc")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Indenture GetIndentureById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Indenture ToAppModel(Alku dbModel)
        {
            return mappingRelatedToUser.MapToIndenture<Models.Accounting.DTO.Indenture>(dbModel);
        }

        protected override Alku ToEntityModel(Models.Accounting.DTO.Indenture appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alku>(appModel);
            return dbModel;
        }
    }
}
