using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.Tax;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Customer.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.Tax ToDanhMucThueSuatDto(this TaxDetail source)
        {
            return new AccModels.Tax
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaThue = source.MaThue,
                Ten_thue = source.Ten_thue,
                Ten_thue2 = source.Ten_thue2,
                ThueSuat = source.ThueSuat,
                TaiKhoan_thue_co = source.TaiKhoan_thue_co,
                TaiKhoan_thue_no = source.TaiKhoan_thue_no,
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }

        public static PagedSearchResult<TaxListItem> ToTaxViewModel(this PagedSearchResult<AccModels.Tax> source)
        {
            var danhMucThueSuatItem = source.Data.Select(
                x => new TaxListItem
                    {
                        NgayKhoiTao = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                        Ten_thue = x.Ten_thue,
                        MaThue = x.MaThue,
                        Ten_thue2 = x.Ten_thue2,
                        TaiKhoan_thue_co = x.TaiKhoan_thue_co,
                        TaiKhoan_thue_no = x.TaiKhoan_thue_no,
                        ThueSuat = x.ThueSuat,
                        UID = x.UID
                    }
            );
            return new PagedSearchResult<TaxListItem>(danhMucThueSuatItem.ToList(), source.Total);
        }
    }
}
