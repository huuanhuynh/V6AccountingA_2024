using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Common.Dealers;
using V6Soft.Accounting.PriceCode.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using DTO = V6Soft.Models.Accounting.DTO;
using System.Web.Http.OData.Query;

namespace V6Soft.Accounting.PriceCode.Dealers
{
    /// <summary>
    ///     Provides PriceCodeItem-related operations (PriceCode CRUD, PriceCode group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectPriceCodeDataDealer : DataDealerBase, IPriceCodeDataDealer
    {
        private readonly ILogger m_Logger;
        private readonly IPriceCodeDataFarmer m_PriceCodeFarmer;

        public DirectPriceCodeDataDealer(ILogger logger, IPriceCodeDataFarmer PriceCodeFarmer)
            : base(PriceCodeFarmer.AsQueryable())
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(PriceCodeFarmer, "PriceCodeFarmer");

            m_Logger = logger;
            m_PriceCodeFarmer = PriceCodeFarmer;

        }

        public IQueryable<DTO.PriceCode> AsQueryable()
        {
            return m_PriceCodeFarmer.AsQueryable();
        }

        public void Save(IList<DynamicObject> models)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DTO.PriceCode> AsQueryable(ODataQueryOptions<DTO.PriceCode> queryOptions)
        {
            return (IQueryable<DTO.PriceCode>)queryOptions.ApplyTo(m_PriceCodeFarmer.AsQueryable());
        }

        public DTO.PriceCode GetPriceCode(Guid guid)
        {
            return m_PriceCodeFarmer.AsQueryable().SingleOrDefault(re => re.UID.Equals(guid));
        }

        /// <summary>
        ///     See <see cref="IPriceCodeDataDealer.GetPriceCodes()"/>
        /// </summary>
        //public PagedSearchResult<PriceCodeListItem> GetPriceCodes(SearchCriteria criteria)
        //{
        //    PagedSearchResult<PriceCodeListItem> allPriceCodes = m_PriceCodeFarmer.GetPriceCodes(criteria).ToPriceCodeViewModel();

        //    allPriceCodes.Data = allPriceCodes.Data
        //        .Select(item =>
        //            {
        //                item.TenKhachHang = VnCodec.TCVNtoUNICODE(item.TenKhachHang);
        //                item.DiaChi = VnCodec.TCVNtoUNICODE(item.DiaChi);
        //                return item;
        //            })
        //        .ToList();
        //    return allPriceCodes;
        //}
        ///// <summary>
        /////     See <see cref="IPriceCodeDataDealer.AddPriceCode()"/>
        ///// </summary>
        //public bool AddPriceCode(DTO.PriceCode PriceCode)
        //{
        //    PriceCode.CreatedDate = DateTime.Now;
        //    PriceCode.ModifiedDate = DateTime.Now;
        //    PriceCode.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
        //    PriceCode.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
        //    var result = m_PriceCodeFarmer.Add(PriceCode);
        //    return result != null;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool DeletePriceCode(string key)
        //{
        //    return m_PriceCodeFarmer.Delete(key);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool UpdatePriceCode(DTO.PriceCode PriceCode)
        //{
        //    PriceCode.CreatedDate = DateTime.Now;
        //    PriceCode.ModifiedDate = DateTime.Now;
        //    PriceCode.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
        //    PriceCode.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
        //    return m_PriceCodeFarmer.Edit(PriceCode);
        //}

        ///// <summary>
        /////     See <see cref="IODataFriendly.AsQueryable"/>
        ///// </summary>
        //public IQueryable<DTO.PriceCode> AsQueryable()
        //{
        //    return m_QueryProvider.CreateQuery<DTO.PriceCode>();
        //}

        //public void Save(IList<DynamicObject> models)
        //{

        //}
    }
}
