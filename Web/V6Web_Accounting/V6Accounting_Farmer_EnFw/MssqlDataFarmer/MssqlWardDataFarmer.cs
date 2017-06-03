using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Geography.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Ward;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IWardDataFarmer"/>
    /// </summary>
    public class MssqlWardDataFarmer : EnFwDataFarmerBase<Alphuong, Models.Accounting.DTO.Ward>, IWardDataFarmer
    {
        private readonly IMappingRelatedToWard mappingRelatedToUser;
        public MssqlWardDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToWard mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Ward> GetWards(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaPhuong")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Ward GetWardById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Ward ToAppModel(Alphuong dbModel)
        {
            return mappingRelatedToUser.MapToWard<Models.Accounting.DTO.Ward>(dbModel);
        }

        protected override Alphuong ToEntityModel(Models.Accounting.DTO.Ward appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alphuong>(appModel);
            return dbModel;
        }
    }
}
