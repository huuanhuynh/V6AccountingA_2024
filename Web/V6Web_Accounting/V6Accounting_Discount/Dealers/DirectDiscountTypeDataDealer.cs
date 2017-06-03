using System;
using System.Linq;
using V6Soft.Accounting.Discount.Farmers;
using V6Soft.Accounting.DiscountType.Dealers;
using V6Soft.Accounting.Duscount.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.DiscountType;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Discount.Dealers
{
    /// <summary>
    ///     Provides DiscountTypeItem-related operations (discountType CRUD, discountType group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectDiscountTypeDataDealer : IDiscountTypeDataDealer
    {
        private ILogger m_Logger;
        private IDiscountTypeDataFarmer m_DiscountTypeFarmer;

        public DirectDiscountTypeDataDealer(ILogger logger, IDiscountTypeDataFarmer discountTypeFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(discountTypeFarmer, "discountTypeFarmer");

            m_Logger = logger;
            m_DiscountTypeFarmer = discountTypeFarmer;
        }
        /// <summary>
        ///     See <see cref="IDiscountTypeDataDealer.GetDiscountTypes()"/>
        /// </summary>
        public PagedSearchResult<DiscountTypeListItem> GetDiscountTypes(SearchCriteria criteria)
        {
            PagedSearchResult<DiscountTypeListItem> allDiscountTypes = m_DiscountTypeFarmer.GetDiscountTypes(criteria).ToDiscountTypeViewModel();

            allDiscountTypes.Data = allDiscountTypes.Data
                .Select(item =>
                {
                    item.Ten_Loai = VnCodec.TCVNtoUNICODE(item.Ten_Loai);
                    return item;
                })
                .ToList();
            return allDiscountTypes;
        }
        /// <summary>
        ///     See <see cref="IDiscountTypeDataDealer.AddDiscountType()"/>
        /// </summary>
        public bool AddDiscountType(Models.Accounting.DTO.DiscountType discountType)
        {
            discountType.CreatedDate = DateTime.Now;
            discountType.ModifiedDate = DateTime.Now;
            discountType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            discountType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_DiscountTypeFarmer.Add(discountType);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteDiscountType(string key)
        {
            return m_DiscountTypeFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateDiscountType(Models.Accounting.DTO.DiscountType discountType)
        {
            discountType.CreatedDate = DateTime.Now;
            discountType.ModifiedDate = DateTime.Now;
            discountType.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            discountType.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_DiscountTypeFarmer.Edit(discountType);
        }
    }
}
