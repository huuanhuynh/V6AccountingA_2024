using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.ShippingMethod.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.ShippingMethod;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IShippingMethodDataFarmer"/>
    /// </summary>
    public class MssqlShippingMethodDataFarmer : EnFwDataFarmerBase<Alhtvc, Models.Accounting.DTO.ShippingMethod>, IShippingMethodDataFarmer
    {
        private readonly IMappingRelatedToShippingMethod mappingRelatedToUser;
        public MssqlShippingMethodDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToShippingMethod mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.ShippingMethod> GetShippingMethods(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaHinhThucVanChuyen")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.ShippingMethod GetShippingMethodById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.ShippingMethod ToAppModel(Alhtvc dbModel)
        {
            return mappingRelatedToUser.MapToShippingMethod<Models.Accounting.DTO.ShippingMethod>(dbModel);
        }

        protected override Alhtvc ToEntityModel(Models.Accounting.DTO.ShippingMethod appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alhtvc>(appModel);
            return dbModel;
        }
    }
}
