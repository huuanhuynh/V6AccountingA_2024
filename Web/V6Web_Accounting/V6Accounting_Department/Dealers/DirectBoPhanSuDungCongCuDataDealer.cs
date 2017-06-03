using System;
using System.Linq;
using V6Soft.Accounting.Department.Dealers;
using V6Soft.Accounting.Department.Extensions;
using V6Soft.Accounting.Department.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.BoPhanSuDungCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.BoPhanSuDungCongCu.Dealers
{
    /// <summary>
    ///     Provides BoPhanSuDungCongCuItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectBoPhanSuDungCongCuDataDealer : IBoPhanSuDungCongCuDataDealer
    {
        private ILogger m_Logger;
        private IBoPhanSuDungCongCuDataFarmer m_BoPhanSuDungCongCuFarmer;

        public DirectBoPhanSuDungCongCuDataDealer(ILogger logger, IBoPhanSuDungCongCuDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_BoPhanSuDungCongCuFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IBoPhanSuDungCongCuDataDealer.GetBoPhanSuDungCongCus()"/>
        /// </summary>
        public PagedSearchResult<BoPhanSuDungCongCuItem> GetBoPhanSuDungCongCus(SearchCriteria criteria)
        {
            PagedSearchResult<BoPhanSuDungCongCuItem> allBoPhanSuDungCongCus = m_BoPhanSuDungCongCuFarmer.GetBoPhanSuDungCongCus(criteria).ToBoPhanCongCuViewModel();

            allBoPhanSuDungCongCus.Data = allBoPhanSuDungCongCus.Data
                .Select(item =>
                {
                    item.TenBoPhan = VnCodec.TCVNtoUNICODE(item.TenBoPhan);
                    return item;
                })
                .ToList();
            return allBoPhanSuDungCongCus;
        }
        /// <summary>
        ///     See <see cref="IBoPhanSuDungCongCuDataDealer.AddBoPhanSuDungCongCu()"/>
        /// </summary>
        public bool AddBoPhanSuDungCongCu(AccModels.BoPhanSuDungCongCu customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_BoPhanSuDungCongCuFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteBoPhanSuDungCongCu(string key)
        {
            return m_BoPhanSuDungCongCuFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateBoPhanSuDungCongCu(AccModels.BoPhanSuDungCongCu customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_BoPhanSuDungCongCuFarmer.Edit(customer);
        }
    }
}
