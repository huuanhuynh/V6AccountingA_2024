using System;
using System.Linq;
using V6Soft.Accounting.Asset.Extensions;
using V6Soft.Accounting.Asset.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.PhanNhomCongCu;
using V6Soft.Models.Accounting.ViewModels.PhanNhomCongCu;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.PhanNhomCongCu.Dealers
{
    /// <summary>
    ///     Provides PhanNhomCongCuItem-related operations (phanNhomCongCu CRUD, phanNhomCongCu group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectPhanNhomCongCuDataDealer : IPhanNhomCongCuDataDealer
    {
        private ILogger m_Logger;
        private IPhanNhomCongCuDataFarmer m_PhanNhomCongCuFarmer;

        public DirectPhanNhomCongCuDataDealer(ILogger logger, IPhanNhomCongCuDataFarmer phanNhomCongCuFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(phanNhomCongCuFarmer, "phanNhomCongCuFarmer");

            m_Logger = logger;
            m_PhanNhomCongCuFarmer = phanNhomCongCuFarmer;
        }
        /// <summary>
        ///     See <see cref="IPhanNhomCongCuDataDealer.GetPhanNhomCongCus()"/>
        /// </summary>
        public PagedSearchResult<PhanNhomCongCuItem> GetPhanNhomCongCus(SearchCriteria criteria)
        {
            PagedSearchResult<PhanNhomCongCuItem> allPhanNhomCongCus = m_PhanNhomCongCuFarmer.GetPhanNhomCongCus(criteria).ToPhanNhomCongCuViewModel();

            allPhanNhomCongCus.Data = allPhanNhomCongCus.Data
                .Select(item =>
                {
                    item.MaNhom = VnCodec.TCVNtoUNICODE(item.MaNhom);
                    return item;
                })
                .ToList();
            return allPhanNhomCongCus;
        }
        /// <summary>
        ///     See <see cref="IPhanNhomCongCuDataDealer.AddPhanNhomCongCu()"/>
        /// </summary>
        public bool AddPhanNhomCongCu(AccModels.PhanNhomCongCu phanNhomCongCu)
        {
            phanNhomCongCu.CreatedDate = DateTime.Now;
            phanNhomCongCu.ModifiedDate = DateTime.Now;
            phanNhomCongCu.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            phanNhomCongCu.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_PhanNhomCongCuFarmer.Add(phanNhomCongCu);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeletePhanNhomCongCu(string key)
        {
            return m_PhanNhomCongCuFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdatePhanNhomCongCu(AccModels.PhanNhomCongCu phanNhomCongCu)
        {
            phanNhomCongCu.CreatedDate = DateTime.Now;
            phanNhomCongCu.ModifiedDate = DateTime.Now;
            phanNhomCongCu.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            phanNhomCongCu.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_PhanNhomCongCuFarmer.Edit(phanNhomCongCu);
        }
    }
}
