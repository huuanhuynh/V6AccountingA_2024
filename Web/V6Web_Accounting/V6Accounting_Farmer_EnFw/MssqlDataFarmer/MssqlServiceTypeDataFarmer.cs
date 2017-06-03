using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.ServiceType.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.ServiceType;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IServiceTypeDataFarmer"/>
    /// </summary>
    public class MssqlServiceTypeDataFarmer : EnFwDataFarmerBase<Alloaivc, Models.Accounting.DTO.ServiceType>, IServiceTypeDataFarmer
    {
        private readonly IMappingRelatedToServiceType mappingRelatedToUser;
        public MssqlServiceTypeDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToServiceType mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.ServiceType> GetServiceTypes(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaLoai")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.ServiceType GetServiceTypeById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.ServiceType ToAppModel(Alloaivc dbModel)
        {
            return mappingRelatedToUser.MapToServiceType<Models.Accounting.DTO.ServiceType>(dbModel);
        }

        protected override Alloaivc ToEntityModel(Models.Accounting.DTO.ServiceType appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alloaivc>(appModel);
            return dbModel;
        }
    }
}
