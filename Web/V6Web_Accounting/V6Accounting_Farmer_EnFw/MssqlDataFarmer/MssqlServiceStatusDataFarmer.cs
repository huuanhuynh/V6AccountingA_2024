using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.ServiceStatus;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.ServiceStatus.Farmers;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IServiceStatusDataFarmer"/>
    /// </summary>
    public class MssqlServiceStatusDataFarmer : EnFwDataFarmerBase<ALttvt, Models.Accounting.DTO.ServiceStatus>, IServiceStatusDataFarmer
    {
        private readonly IMappingRelatedToServiceStatus mappingRelatedToUser;
        public MssqlServiceStatusDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToServiceStatus mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.ServiceStatus> GetServiceStatuss(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("TinhTrangVatTu")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.ServiceStatus GetServiceStatusById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.ServiceStatus ToAppModel(ALttvt dbModel)
        {
            return mappingRelatedToUser.MapToServiceStatus<Models.Accounting.DTO.ServiceStatus>(dbModel);
        }

        protected override ALttvt ToEntityModel(Models.Accounting.DTO.ServiceStatus appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALttvt>(appModel);
            return dbModel;
        }
    }
}
