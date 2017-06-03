using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.BoPhanSuDungCongCu;
using V6Soft.Models.Accounting.ViewModels.Department;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Department.Extensions
{
    public static class ModelConverter
    {
        //public static Models.Accounting.DTO.Department ToBoPhanDto(this DepartmentDetail source)
        //{
        //    return new Models.Accounting.DTO.Department
        //    {
        //        CreatedDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
        //        ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
        //        CreatedUserId = source.CreatedUserId,
        //        ModifiedUserId = source.ModifiedUserId,
        //        MaBoPhan = source.MaBoPhan,
        //        TenBoPhan = source.TenBoPhan,
        //        TenBoPhan2 = source.TenBoPhan2,
        //        GhiChu = source.GhiChu,
        //        TrangThai = source.TrangThai,
        //        SoLuongTuDo1 = source.SoLuongTuDo1,
        //        SoLuongTuDo2 = source.SoLuongTuDo2,
        //        SoLuongTuDo3 = source.SoLuongTuDo3,
        //        NgayTuDo1 = source.NgayTuDo1,
        //        NgayTuDo2 = source.NgayTuDo2,
        //        NgayTuDo3 = source.NgayTuDo3,
        //        GhiChuTuDo1 = source.GhiChuTuDo1,
        //        GhiChuTuDo2 = source.GhiChuTuDo2,
        //        GhiChuTuDo3 = source.GhiChuTuDo3,
        //        UID = source.UID,
        //        CheckSync = source.CheckSync,
        //    };
        //}
        public static Models.Accounting.DTO.BoPhanSuDungCongCu ToBoPhanSuDungCongCuDto(this ModifiedBoPhanSuDungCongCu source)
        {
            return new Models.Accounting.DTO.BoPhanSuDungCongCu
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaBoPhan = source.MaBoPhan,
                TenBoPhan = source.TenBoPhan,
                TenBoPhan2 = source.TenBoPhan2,
                TrangThai = source.TrangThai,
                MaTuDo1 = source.MaTuDo1,
                MaTuDo2 = source.MaTuDo2,
                MaTuDo3 = source.MaTuDo3,
                SoLuongTuDo1 = source.SoLuongTuDo1,
                SoLuongTuDo2 = source.SoLuongTuDo2,
                SoLuongTuDo3 = source.SoLuongTuDo3,
                NgayTuDo1 = source.NgayTuDo1,
                NgayTuDo2 = source.NgayTuDo2,
                NgayTuDo3 = source.NgayTuDo3,
                GhiChuTuDo1 = source.GhiChuTuDo1,
                GhiChuTuDo2 = source.GhiChuTuDo2,
                GhiChuTuDo3 = source.GhiChuTuDo3,
                UID = source.UID,
            };
        }

        public static PagedSearchResult<DepartmentListItem> ToBoPhanViewModel(this PagedSearchResult<Models.Accounting.DTO.Department> source)
        {
            var boPhanItem = source.Data.Select(
                x => new DepartmentListItem
                    {
                        MaBoPhan = x.MaBoPhan,
                        TenBoPhan = x.TenBoPhan,
                        TenBoPhan2 = x.TenBoPhan2,
                        CreatedDate = x.CreatedDate,
                        CreatedTime = x.CreatedTime,
                        CreatedUserId = (byte) x.CreatedUserId,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedTime = x.ModifiedTime,
                        ModifiedUserId = (byte) x.ModifiedUserId,
                        UID = x.UID
                    }
            );
            return new PagedSearchResult<DepartmentListItem>(boPhanItem.ToList(), source.Total);
        }

        public static PagedSearchResult<BoPhanSuDungCongCuItem> ToBoPhanCongCuViewModel(this PagedSearchResult<Models.Accounting.DTO.BoPhanSuDungCongCu> source)
        {
            var boPhanSuDungCongCuItem = source.Data.Select(
                x => new BoPhanSuDungCongCuItem
                {
                    UID = x.UID,
                    MaBoPhan = x.MaBoPhan,
                    TenBoPhan = x.TenBoPhan,
                    TenBoPhan2 = x.TenBoPhan2,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgayKhoiTao = x.CreatedDate,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                }
            );
            return new PagedSearchResult<BoPhanSuDungCongCuItem>(boPhanSuDungCongCuItem.ToList(), source.Total);
        }
    }
}
