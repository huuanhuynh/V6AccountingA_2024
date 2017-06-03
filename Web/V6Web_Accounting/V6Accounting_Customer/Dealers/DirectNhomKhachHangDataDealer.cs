using System.Linq;
using V6Soft.Accounting.Customer.Extensions;
using V6Soft.Accounting.NhomKhachHang.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.CustomerGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.NhomKhachHang.Dealers
{
    /// <summary>
    ///     Provides NhomKhachHang-related operations (nhomkhachhang CRUD, nhomkhachhang group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectNhomKhachHangDataDealer : INhomKhachHangDataDealer
    {
        private ILogger m_Logger;
        private INhomKhachHangDataFarmer m_NhomKhachHangFarmer;

        public DirectNhomKhachHangDataDealer(ILogger logger, INhomKhachHangDataFarmer nhomkhachhangFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(nhomkhachhangFarmer, "nhomkhachhangFarmer");

            m_Logger = logger;
            m_NhomKhachHangFarmer = nhomkhachhangFarmer;
        }

        /// <summary>
        ///     See <see cref="INhomKhachHangDataDealer.GetNhomKhachHangs()"/>
        /// </summary>
        public PagedSearchResult<CustomerGroupListItem> GetNhomKhachHangs(SearchCriteria criteria)
        {
            PagedSearchResult<CustomerGroupListItem> allNhomKhachHangs = 
                m_NhomKhachHangFarmer.GetNhomKhachHangs(criteria).ToNhomKhachHangViewModel();

            allNhomKhachHangs.Data = allNhomKhachHangs.Data
                .Select(item =>
                    {
                        item.LoaiNhom = item.LoaiNhom;
                        item.TenNhom = VnCodec.TCVNtoUNICODE(item.TenNhom);
                        item.TenNhom2 = VnCodec.TCVNtoUNICODE(item.TenNhom2);
                        return item;
                    })
                .ToList();
            return allNhomKhachHangs;
        }

        /// <summary>
        ///     See <see cref="INhomKhachHangDataDealer.AddNhomKhachHang()"/>
        /// </summary>
        public bool AddNhomKhachHang(CustomerGroupDetail obj)
        {
            var nhomKhachHangDto = obj.ToNhomKhachHangDto();
            var result = m_NhomKhachHangFarmer.Add(nhomKhachHangDto);
            return result != null;
        }



    }
}
