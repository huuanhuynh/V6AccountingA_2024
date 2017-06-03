using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Shipment.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Shipment;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IShipmentDataFarmer"/>
    /// </summary>
    public class MssqlShipmentDataFarmer : EnFwDataFarmerBase<Alvc, Models.Accounting.DTO.Shipment>, IShipmentDataFarmer
    {
        private readonly IMappingRelatedToShipment mappingRelatedToUser;
        public MssqlShipmentDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToShipment mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Shipment> GetShipments(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaVanChuyen")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Shipment GetShipmentById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Shipment ToAppModel(Alvc dbModel)
        {
            return mappingRelatedToUser.MapToShipment<Models.Accounting.DTO.Shipment>(dbModel);
        }

        protected override Alvc ToEntityModel(Models.Accounting.DTO.Shipment appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alvc>(appModel);
            return dbModel;
        }
    }
}
