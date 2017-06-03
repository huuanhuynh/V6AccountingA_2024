using System;
using System.Linq;
using V6Soft.Accounting.LoaiNhapXuat.Farmers;
using V6Soft.Accounting.Warehouse.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.LoaiNhapXuat;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.LoaiNhapXuat.Dealers
{
    /// <summary>
    ///     Provides LoaiNhapXuatItem-related operations (loaiNhapXuat CRUD, loaiNhapXuat group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectLoaiNhapXuatDataDealer : ILoaiNhapXuatDataDealer
    {
        private ILogger m_Logger;
        private ILoaiNhapXuatDataFarmer m_LoaiNhapXuatFarmer;

        public DirectLoaiNhapXuatDataDealer(ILogger logger, ILoaiNhapXuatDataFarmer loaiNhapXuatFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(loaiNhapXuatFarmer, "loaiNhapXuatFarmer");

            m_Logger = logger;
            m_LoaiNhapXuatFarmer = loaiNhapXuatFarmer;
        }
        /// <summary>
        ///     See <see cref="ILoaiNhapXuatDataDealer.GetLoaiNhapXuats()"/>
        /// </summary>
        public PagedSearchResult<LoaiNhapXuatItem> GetLoaiNhapXuats(SearchCriteria criteria)
        {
            PagedSearchResult<LoaiNhapXuatItem> allLoaiNhapXuats = m_LoaiNhapXuatFarmer.GetLoaiNhapXuats(criteria).ToLoaiNhapXuatViewModel();

            allLoaiNhapXuats.Data = allLoaiNhapXuats.Data
                .Select(item =>
                    {
                        item.ten_Loai = VnCodec.TCVNtoUNICODE(item.ten_Loai);
                        item.ten_Loai2 = VnCodec.TCVNtoUNICODE(item.ten_Loai2);
                        return item;
                    })
                .ToList();
            return allLoaiNhapXuats;
        }
        /// <summary>
        ///     See <see cref="ILoaiNhapXuatDataDealer.AddLoaiNhapXuat()"/>
        /// </summary>
        public bool AddLoaiNhapXuat(AccModels.LoaiNhapXuat loaiNhapXuat)
        {
            loaiNhapXuat.CreatedDate = DateTime.Now;
            loaiNhapXuat.ModifiedDate = DateTime.Now;
            loaiNhapXuat.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            loaiNhapXuat.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_LoaiNhapXuatFarmer.Add(loaiNhapXuat);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteLoaiNhapXuat(string key)
        {
            return m_LoaiNhapXuatFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateLoaiNhapXuat(AccModels.LoaiNhapXuat loaiNhapXuat)
        {
            loaiNhapXuat.CreatedDate = DateTime.Now;
            loaiNhapXuat.ModifiedDate = DateTime.Now;
            loaiNhapXuat.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            loaiNhapXuat.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_LoaiNhapXuatFarmer.Edit(loaiNhapXuat);
        }
    }
}
