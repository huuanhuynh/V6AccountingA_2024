using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Merchandise.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Merchandise;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IMerchandiseDataFarmer"/>
    /// </summary>
    public class MssqlMerchandiseDataFarmer : EnFwDataFarmerBase<Allo, Models.Accounting.DTO.Merchandise>, IMerchandiseDataFarmer
    {
        private readonly IMappingRelatedToMerchandise mappingRelatedToUser;
        public MssqlMerchandiseDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToMerchandise mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Merchandise> GetMerchandises(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaLo")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Merchandise GetMerchandiseById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Merchandise ToAppModel(Allo dbModel)
        {
            return mappingRelatedToUser.MapToMerchandise<Models.Accounting.DTO.Merchandise>(dbModel);
        }

        protected override Allo ToEntityModel(Models.Accounting.DTO.Merchandise appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Allo>(appModel);
            return dbModel;
        }
    }
}
