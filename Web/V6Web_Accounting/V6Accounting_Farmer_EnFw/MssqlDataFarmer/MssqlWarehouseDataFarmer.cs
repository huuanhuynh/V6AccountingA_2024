using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Warehouse.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Warehouse;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IWarehouseDataFarmer"/>
    /// </summary>
    public class MssqlWarehouseDataFarmer : EnFwDataFarmerBase<Alkho, Models.Accounting.DTO.Warehouse>, IWarehouseDataFarmer
    {
        private readonly IMappingRelatedToWarehouse mappingRelatedToUser;
        public MssqlWarehouseDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToWarehouse mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Warehouse> GetWarehouses(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaKho")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Warehouse GetWarehouseById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Warehouse ToAppModel(Alkho dbModel)
        {
            return mappingRelatedToUser.MapToWarehouse<Models.Accounting.DTO.Warehouse>(dbModel);
        }

        protected override Alkho ToEntityModel(Models.Accounting.DTO.Warehouse appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alkho>(appModel);
            return dbModel;
        }
    }
}
