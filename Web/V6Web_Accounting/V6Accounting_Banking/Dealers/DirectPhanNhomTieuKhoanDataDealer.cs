using System;
using System.Linq;
using V6Soft.Accounting.Banking.Dealers;
using V6Soft.Accounting.Banking.Extensions;
using V6Soft.Accounting.Banking.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.PhanNhomTieuKhoan;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.PhanNhomTieuKhoan.Dealers
{
    /// <summary>
    ///     Provides PhanNhomTieuKhoanItem-related operations (customer CRUD, customer group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectPhanNhomTieuKhoanDataDealer : IPhanNhomTieuKhoanDataDealer
    {
        private ILogger m_Logger;
        private IPhanNhomTieuKhoanDataFarmer m_PhanNhomTieuKhoanFarmer;

        public DirectPhanNhomTieuKhoanDataDealer(ILogger logger, IPhanNhomTieuKhoanDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_Logger = logger;
            m_PhanNhomTieuKhoanFarmer = customerFarmer;
        }
        /// <summary>
        ///     See <see cref="IPhanNhomTieuKhoanDataDealer.GetPhanNhomTieuKhoans()"/>
        /// </summary>
        public PagedSearchResult<PhanNhomTieuKhoanItem> GetPhanNhomTieuKhoans(SearchCriteria criteria)
        {
            PagedSearchResult<PhanNhomTieuKhoanItem> allPhanNhomTieuKhoans = m_PhanNhomTieuKhoanFarmer.GetPhanNhomTieuKhoans(criteria).ToPhanNhomTieuKhoanViewModel();

            allPhanNhomTieuKhoans.Data = allPhanNhomTieuKhoans.Data
                .Select(item =>
                {
                    item.TenNhom = VnCodec.TCVNtoUNICODE(item.TenNhom);
                    return item;
                })
                .ToList();
            return allPhanNhomTieuKhoans;
        }
        /// <summary>
        ///     See <see cref="IPhanNhomTieuKhoanDataDealer.AddPhanNhomTieuKhoan()"/>
        /// </summary>
        public bool AddPhanNhomTieuKhoan(AccModels.PhanNhomTieuKhoan customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_PhanNhomTieuKhoanFarmer.Add(customer);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeletePhanNhomTieuKhoan(string key)
        {
            return m_PhanNhomTieuKhoanFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdatePhanNhomTieuKhoan(AccModels.PhanNhomTieuKhoan customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            customer.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_PhanNhomTieuKhoanFarmer.Edit(customer);
        }
    }
}
