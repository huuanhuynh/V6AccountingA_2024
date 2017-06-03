using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.CustomerPriceGroup.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.CustomerPriceGroup;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ICustomerPriceGroupDataFarmer"/>
    /// </summary>
    public class MssqlCustomerPriceGroupDataFarmer : EnFwDataFarmerBase<Alnhkh2, Models.Accounting.DTO.CustomerPriceGroup>, ICustomerPriceGroupDataFarmer
    {
        private readonly IMappingRelatedToCustomerPriceGroup mappingRelatedToUser;
        public MssqlCustomerPriceGroupDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToCustomerPriceGroup mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.CustomerPriceGroup> GetCustomerPriceGroups(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaNhom")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.CustomerPriceGroup GetCustomerPriceGroupById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.CustomerPriceGroup ToAppModel(Alnhkh2 dbModel)
        {
            return mappingRelatedToUser.MapToCustomerPriceGroup<Models.Accounting.DTO.CustomerPriceGroup>(dbModel);
        }

        protected override Alnhkh2 ToEntityModel(Models.Accounting.DTO.CustomerPriceGroup appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alnhkh2>(appModel);
            return dbModel;
        }
    }
}
