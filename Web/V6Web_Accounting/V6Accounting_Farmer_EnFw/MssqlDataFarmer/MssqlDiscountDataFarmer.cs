using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Discount.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Discount;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IDiscountDataFarmer"/>
    /// </summary>
    public class MssqlDiscountDataFarmer : EnFwDataFarmerBase<Alck, Models.Accounting.DTO.Discount>, IDiscountDataFarmer
    {
        private readonly IMappingRelatedToDiscount mappingRelatedToUser;
        public MssqlDiscountDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToDiscount mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Discount> GetDiscounts(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaChietKhau")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Discount GetDiscountById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Discount ToAppModel(Alck dbModel)
        {
            return mappingRelatedToUser.MapToDiscount<Models.Accounting.DTO.Discount>(dbModel);
        }

        protected override Alck ToEntityModel(Models.Accounting.DTO.Discount appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alck>(appModel);
            return dbModel;
        }
    }
}
