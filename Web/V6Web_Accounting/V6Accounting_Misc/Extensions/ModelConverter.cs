using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.Province;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Customer.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.Province ToTinhThanhDto(this ProvinceDetail source)
        {
            return new AccModels.Province
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaTinh = source.MaTinh,
                TenTinh = source.TenTinh,
                TenTinh2 = source.TenTinh2,
                GhiChu = source.GhiChu,
                TrangThai = source.TrangThai,
                SoLuongTuDo1 = source.SoLuongTuDo1,
                SoLuongTuDo2 = source.SoLuongTuDo2,
                SoLuongTuDo3 = source.SoLuongTuDo3,
                NgayTuDo1 = source.NgayTuDo1,
                NgayTuDo2 = source.NgayTuDo2,
                NgayTuDo3 = source.NgayTuDo3,
                GhiChuTuDo1 = source.GhiChuTuDo1,
                GhiChuTuDo2 = source.GhiChuTuDo2,
                GhiChuTuDo3 = source.GhiChuTuDo3,
                Loai = source.Loai,
                UID = source.UID,
                CheckSync = source.CheckSync
            };
        }

        public static PagedSearchResult<ProvinceListItem> ToProvinceViewModel(this PagedSearchResult<AccModels.Province> source)
        {
            var tinhThanhItem = source.Data.Select(
                x => new ProvinceListItem
                    {
                        MaTinh = x.MaTinh,
                        TenTinh = x.TenTinh,
                        TenTinh2 = x.TenTinh2,
                        CreatedDate = x.CreatedDate,
                        CreatedTime = x.CreatedTime,
                        CreatedUserId = (byte)x.CreatedUserId,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedTime = x.ModifiedTime,
                        UID = x.UID,
                        ModifiedUserId = (byte)x.ModifiedUserId
                    }
            );
            return new PagedSearchResult<ProvinceListItem>(tinhThanhItem.ToList(), source.Total);
        }
    }
}
