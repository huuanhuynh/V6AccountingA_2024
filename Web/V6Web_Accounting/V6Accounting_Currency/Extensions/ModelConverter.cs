using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.Capital;
using V6Soft.Models.Accounting.ViewModels.ForeignCurrency;
using V6Soft.Models.Accounting.ViewModels.ForeignExchangeRate;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Currency.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.ForeignCurrency ToNgoaiTeDto(this ForeignCurrencyDetail source)
        {
            return new AccModels.ForeignCurrency
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaNgoaiTe = source.MaNgoaiTe,
                TenNgoaiTe = source.TenNgoaiTe,
                TenNgoaiTe2 = source.TenNgoaiTe2,
                TaiKhoanPhatSinhCL_No = source.TaiKhoanPhatSinhCL_No,
                TaiKhoanPhatSinhCL_Co = source.TaiKhoanPhatSinhCL_Co,
                TaiKhoanDGCL_No = source.TaiKhoanDGCL_No,
                TaiKhoanDGCL_Co = source.TaiKhoanDGCL_Co,
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }
        public static AccModels.Capital ToNguonVonDto(this CapitalDetail source)
        {
            return new AccModels.Capital
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaNguonVon = source.MaNguonVon,
                TenNguonVon = source.TenNguonVon,
                TenNguonVon2 = source.TenNguonVon2,
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }
        public static AccModels.ForeignExchangeRate ToTyGiaNgoaiTeDto(this ForeignExchangeRateDetail source)
        {
            return new AccModels.ForeignExchangeRate
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaNgoaiTe = source.MaNgoaiTe,
                NgayChungTu = source.NgayChungTu,
                ty_Gia = source.ty_Gia,
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }

        public static PagedSearchResult<ForeignCurrencyListItem> ToForeignCurrencyViewModel(this PagedSearchResult<AccModels.ForeignCurrency> source)
        {
            var ngoaiTeItem = source.Data.Select(
                x => new ForeignCurrencyListItem
                    {
                        MaNgoaiTe = x.MaNgoaiTe,
                        TenNgoaiTe = x.TenNgoaiTe,
                        TenNgoaiTe2 = x.TenNgoaiTe2,
                        NgayKhoiTao = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                    }
            );
            return new PagedSearchResult<ForeignCurrencyListItem>(ngoaiTeItem.ToList(), source.Total);
        }


        public static PagedSearchResult<CapitalListItem> ToCapitalViewModel(this PagedSearchResult<AccModels.Capital> source)
        {
            var nguonVonItem = source.Data.Select(
                x => new CapitalListItem
                {
                    MaNguonVon = x.MaNguonVon,
                    TenNguonVon = x.TenNguonVon,
                    TenNguonVon2 = x.TenNguonVon2,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    UID =  x.UID,
                    NguoiSua = (byte)x.ModifiedUserId
                }
            );
            return new PagedSearchResult<CapitalListItem>(nguonVonItem.ToList(), source.Total);
        }
        public static PagedSearchResult<ForeignExchangeRateListItem> ToForeignExchangeRateViewModel(this PagedSearchResult<AccModels.ForeignExchangeRate> source)
        {
            var tyGiaNgoaiTeItem = source.Data.Select(
                x => new ForeignExchangeRateListItem
                {
                    MaNgoaiTe = x.MaNgoaiTe,
                    NgayChungTu = x.NgayChungTu,
                    ty_Gia = x.ty_Gia,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID =  x.UID

                }
            );
            return new PagedSearchResult<ForeignExchangeRateListItem>(tyGiaNgoaiTeItem.ToList(), source.Total);
        }
    }
}
