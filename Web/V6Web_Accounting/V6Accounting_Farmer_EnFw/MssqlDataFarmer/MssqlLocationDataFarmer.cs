using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Location;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.Geography.Farmers;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ILocationDataFarmer"/>
    /// </summary>
    public class MssqlLocationDataFarmer : EnFwDataFarmerBase<ALvitri, Models.Accounting.DTO.Location>, ILocationDataFarmer
    {
        private readonly IMappingRelatedToLocation mappingRelatedToUser;
        public MssqlLocationDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToLocation mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Location> GetLocations(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaViTri")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Location GetLocationById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Location ToAppModel(ALvitri dbModel)
        {
            return mappingRelatedToUser.MapToLocation<Models.Accounting.DTO.Location>(dbModel);
        }

        protected override ALvitri ToEntityModel(Models.Accounting.DTO.Location appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALvitri>(appModel);
            return dbModel;
        }
    }
}
