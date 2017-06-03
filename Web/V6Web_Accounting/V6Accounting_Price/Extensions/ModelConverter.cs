using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.CustomerPriceGroup;
using V6Soft.Models.Accounting.ViewModels.MaterialPriceGroup;
using V6Soft.Models.Accounting.ViewModels.PriceCode;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Customer.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.PriceCode ToDanhMucMaGiaDto(this PriceCodeDetail source)
        {
            return new AccModels.PriceCode
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaGia = source.MaGia,
                Ten_Gia = source.Ten_Gia,
                Ten_Gia2 = source.Ten_Gia2,
                Loai = source.Loai,
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }
        public static AccModels.CustomerPriceGroup ToNhomGiaKhachHangDto(this CustomerPriceGroupDetail source)
        {
            return new AccModels.CustomerPriceGroup
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaNhom = source.MaNhom,
                TenNhom = source.TenNhom,
                TenNhom2 = source.TenNhom2,
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
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }
        public static AccModels.MaterialPriceGroup ToNhomGiaVatTuDto(this MaterialPriceGroupDetail source)
        {
            return new AccModels.MaterialPriceGroup
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaNhom = source.MaNhom,
                TenNhom = source.TenNhom,
                TenNhom2 = source.TenNhom2,
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
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }

        public static PagedSearchResult<PriceCodeListItem> ToPriceCodeViewModel(this PagedSearchResult<AccModels.PriceCode> source)
        {
            var danhMucMaGiaItem = source.Data.Select(
                x => new PriceCodeListItem
                    {
                        MaGia = x.MaGia,
                        Loai = x.Loai,
                        Ten_Gia = x.Ten_Gia,
                        NgayKhoiTao = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                        TrangThai = x.TrangThai,
                        UID = x.UID
                    }
            );
            return new PagedSearchResult<PriceCodeListItem>(danhMucMaGiaItem.ToList(), source.Total);
        }
        public static PagedSearchResult<CustomerPriceGroupListItem> ToCustomerPriceGroupViewModel(this PagedSearchResult<AccModels.CustomerPriceGroup> source)
        {
            var nhomKhachHang2Item = source.Data.Select(
                x => new CustomerPriceGroupListItem
                {
                    MaNhom = x.MaNhom,
                    TenNhom = x.TenNhom,
                    TenNhom2 = x.TenNhom2,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID =  x.UID
                }
            );
            return new PagedSearchResult<CustomerPriceGroupListItem>(nhomKhachHang2Item.ToList(), source.Total);
        }
        public static PagedSearchResult<MaterialPriceGroupListItem> ToMaterialPriceGroupViewModel(this PagedSearchResult<AccModels.MaterialPriceGroup> source)
        {
            var nhomVatTu2Item = source.Data.Select(
                x => new MaterialPriceGroupListItem
                {
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
            return new PagedSearchResult<MaterialPriceGroupListItem>(nhomVatTu2Item.ToList(), source.Total);
        }
    }
}
