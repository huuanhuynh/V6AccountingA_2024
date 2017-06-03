using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.Indenture;
using V6Soft.Models.Accounting.ViewModels.IndentureGroup;
using V6Soft.Models.Accounting.ViewModels.InvoiceTemplate;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Receipt.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.InvoiceTemplate ToMauHoaDonDto(this InvoiceTemplateDetail source)
        {
            return new AccModels.InvoiceTemplate
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaMauHoaDon = source.MaMauHoaDon,
                TenMauHoaDon = source.TenMauHoaDon,
                TenMauHoaDon2 = source.TenMauHoaDon2,
                LoaiMau = source.LoaiMau,
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
            };
        }
        public static AccModels.IndentureGroup ToNhomKheUocDto(this IndentureGroupDetail source)
        {
            return new AccModels.IndentureGroup
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                LoaiNhom = source.LoaiNhom,
                MaNhom = source.MaNhom,
                TenNhom = source.TenNhom,
                TenNhom2 = source.TenNhom2,
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }
        public static AccModels.Indenture ToKheUocDto(this IndentureDetail source)
        {
            return new AccModels.Indenture
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaKheUoc = source.MaKheUoc,
                TenKheUoc = source.TenKheUoc,
                TenKheUoc2 = source.TenKheUoc2,
                MaNgoaiTe = source.MaNgoaiTe,
                Tien0 = source.Tien0,
                TienNgoaiTe0 = source.TienNgoaiTe0,
                NgayKheUoc1 = source.NgayKheUoc1,
                NgayKheUoc2 = source.NgayKheUoc2,
                LaiSuat1 = source.LaiSuat1,
                LaiSuat2 = source.LaiSuat2,
                NhomKheUoc1 = source.NhomKheUoc1,
                NhomKheUoc2 = source.NhomKheUoc2,
                NhomKheUoc3 = source.NhomKheUoc3,
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
                SoKheUoc = source.SoKheUoc,
                MaKhachHang = source.MaKhachHang,
                TaiKhoan_kc = source.TaiKhoan_kc,
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }

        public static PagedSearchResult<IndentureListItem> ToIndentureViewModel(this PagedSearchResult<AccModels.Indenture> source)
        {
            var kheUocItem = source.Data.Select(
                x => new IndentureListItem
                    {
                        MaKheUoc = x.MaKheUoc,
                        TenKheUoc = x.TenKheUoc,
                        TenKheUoc2 = x.TenKheUoc2,
                        NhomKheUoc1 = x.NhomKheUoc1,
                        NhomKheUoc2 = x.NhomKheUoc2,
                        NhomKheUoc3 = x.NhomKheUoc3,
                        NgayNhap = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        UID = x.UID,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId
                    }
            );
            return new PagedSearchResult<IndentureListItem>(kheUocItem.ToList(), source.Total);
        }
        public static PagedSearchResult<IndentureGroupListItem> ToIndentureGroupViewModel(this PagedSearchResult<AccModels.IndentureGroup> source)
        {
            var nhomKheUocItem = source.Data.Select(
                x => new IndentureGroupListItem
                {
                    LoaiNhom = x.LoaiNhom,
                    MaNhom = x.MaNhom,
                    TenNhom = x.TenNhom,
                    TenNhom2 = x.TenNhom2,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    UID = x.UID,
                    NguoiSua = (byte)x.ModifiedUserId,
                }
            );
            return new PagedSearchResult<IndentureGroupListItem>(nhomKheUocItem.ToList(), source.Total);
        }
        public static PagedSearchResult<InvoiceTemplateListItem> ToInvoiceTemplateViewModel(this PagedSearchResult<AccModels.InvoiceTemplate> source)
        {
            var mauHoaDonItem = source.Data.Select(
                x => new InvoiceTemplateListItem
                {
                    MaMauHoaDon = x.MaMauHoaDon,
                    TenMauHoaDon = x.TenMauHoaDon,
                    TenMauHoaDon2 = x.TenMauHoaDon2,
                    LoaiMau = x.LoaiMau,
                    NgayNhap = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID =  x.UID
                }
            );
            return new PagedSearchResult<InvoiceTemplateListItem>(mauHoaDonItem.ToList(), source.Total);
        }
    }
}
