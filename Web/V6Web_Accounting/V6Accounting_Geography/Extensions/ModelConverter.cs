using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.District;
using V6Soft.Models.Accounting.ViewModels.Location;
using V6Soft.Models.Accounting.ViewModels.Nation;
using V6Soft.Models.Accounting.ViewModels.Ward;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Geography.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.Ward ToPhuongXaDto(this WardDetail source)
        {
            return new AccModels.Ward
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaPhuong = source.MaPhuong,
                TenPhuong = source.TenPhuong,
                TenPhuong2 = source.TenPhuong2,
                GhiChu = source.GhiChu,
                SoLuongTuDo1 = source.SoLuongTuDo1,
                SoLuongTuDo2 = source.SoLuongTuDo2,
                SoLuongTuDo3 = source.SoLuongTuDo3,
                NgayTuDo1 = source.NgayTuDo1,
                NgayTuDo2 = source.NgayTuDo2,
                NgayTuDo3 = source.NgayTuDo3,
                GhiChuTuDo1 = source.GhiChuTuDo1,
                GhiChuTuDo2 = source.GhiChuTuDo2,
                GhiChuTuDo3 = source.GhiChuTuDo3,
                TrangThai = source.TrangThai,
                UID = source.UID,
                CheckSync = source.CheckSync
            };
        }
        public static AccModels.District ToQuanHuyenDto(this DistrictDetail source)
        {
            return new AccModels.District
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaQuan = source.MaQuan,
                Ten_quan = source.Ten_quan,
                Ten_quan2 = source.Ten_quan2,
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
                UID = source.UID,
                CheckSync = source.CheckSync
            };
        }
        public static AccModels.Nation ToQuocGiaDto(this NationDetail source)
        {
            return new AccModels.Nation
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaQuocGia = source.MaQuocGia,
                TenQuocGia = source.TenQuocGia,
                TenQuocGia2 = source.TenQuocGia2,
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
                UID = source.UID,
            };
        }
        public static AccModels.Location ToViTriDto(this LocationDetail source)
        {
            return new AccModels.Location()
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,

                MaKho = source.MaKho,
                MaViTri = source.MaViTri,
                TenViTri = source.TenViTri,
                TenViTri2 = source.TenViTri2,
                STT_NhapTruocXuatTruoc = source.STT_NhapTruocXuatTruoc,
                MaLoai = source.MaLoai,
                KieuNhap = source.KieuNhap,
                KieuXuat = source.KieuXuat,
                KieuBan = source.KieuBan,
                MaVatTu = source.MaVatTu,

                GhiChu = source.GhiChu,
                TrangThai = source.TrangThai,
                MaTuDo1 = source.MaTuDo1,
                MaTuDo2 = source.MaTuDo2,
                MaTuDo3 = source.MaTuDo3,
                NgayTuDo1 = source.NgayTuDo1,
                NgayTuDo2 = source.NgayTuDo2,
                NgayTuDo3 = source.NgayTuDo3,
                SoLuongTuDo1 = source.SoLuongTuDo1,
                SoLuongTuDo2 = source.SoLuongTuDo2,
                SoLuongTuDo3 = source.SoLuongTuDo3,
                GhiChuTuDo1 = source.GhiChuTuDo1,
                GhiChuTuDo2 = source.GhiChuTuDo2,
                GhiChuTuDo3 = source.GhiChuTuDo3,
                CheckSync = source.CheckSync,
                UID = source.UID,
            };
        }

        public static PagedSearchResult<WardListItem> ToWardViewModel(this PagedSearchResult<AccModels.Ward> source)
        {
            var phuongXaItem = source.Data.Select(
                x => new WardListItem
                    {
                        TenPhuong = x.TenPhuong,
                        TenPhuong2 = x.TenPhuong2,
                        MaPhuong = x.MaPhuong,
                        NgayKhoiTao = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                        UID =  x.UID
                    }
            );
            return new PagedSearchResult<WardListItem>(phuongXaItem.ToList(), source.Total);
        }
        public static PagedSearchResult<DistrictListItem> ToDistrictViewModel(this PagedSearchResult<AccModels.District> source)
        {
            var quanHuyenItem = source.Data.Select(
                x => new DistrictListItem
                {
                    MaQuan = x.MaQuan,
                    Ten_quan = x.Ten_quan,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID = x.UID
                }
            );
            return new PagedSearchResult<DistrictListItem>(quanHuyenItem.ToList(), source.Total);
        }
        public static PagedSearchResult<NationListItem> ToNationViewModel(this PagedSearchResult<AccModels.Nation> source)
        {
            var quocGiaItem = source.Data.Select(
                x => new NationListItem
                {
                    MaQuocGia = x.MaQuocGia,
                    TenQuocGia = x.TenQuocGia,
                    TenQuocGia2 = x.TenQuocGia2,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID = x.UID
                }
            );
            return new PagedSearchResult<NationListItem>(quocGiaItem.ToList(), source.Total);
        }
        public static PagedSearchResult<LocationListItem> ToLocationViewModel(this PagedSearchResult<AccModels.Location> source)
        {
            var viTriItem = source.Data.Select(
                x => new LocationListItem
                {
                    MaKho = x.MaKho,
                    MaViTri = x.MaViTri,
                    TenViTri2 = x.TenViTri2,
                    STT_NhapTruocXuatTruoc = x.STT_NhapTruocXuatTruoc,
                    MaLoai = x.MaLoai,
                    KieuBan = x.KieuBan,
                    KieuNhap = x.KieuNhap,
                    KieuXuat = x.KieuXuat,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID = x.UID
                }
            );
            return new PagedSearchResult<LocationListItem>(viTriItem.ToList(), source.Total);
        }
    }
}
