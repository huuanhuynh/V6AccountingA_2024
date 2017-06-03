using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Discount.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.DiscountType;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IDiscountTypeDataFarmer"/>
    /// </summary>
    public class MssqlDiscountTypeDataFarmer : EnFwDataFarmerBase<Alloaick, Models.Accounting.DTO.DiscountType>, IDiscountTypeDataFarmer
    {
        private readonly IMappingRelatedToDiscountType mappingRelatedToUser;
        public MssqlDiscountTypeDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToDiscountType mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.DiscountType> GetDiscountTypes(SearchCriteria criteria)
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

        public Models.Accounting.DTO.DiscountType GetDiscountTypeById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.DiscountType ToAppModel(Alloaick dbModel)
        {
            return mappingRelatedToUser.MapToDiscountType<Models.Accounting.DTO.DiscountType>(dbModel);
        }

        protected override Alloaick ToEntityModel(Models.Accounting.DTO.DiscountType appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alloaick>(appModel);
            return dbModel;
        }
    }
}
