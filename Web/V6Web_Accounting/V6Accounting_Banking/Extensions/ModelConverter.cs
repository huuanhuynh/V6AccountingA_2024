using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.Account;
using V6Soft.Models.Accounting.ViewModels.AccountType;
using V6Soft.Models.Accounting.ViewModels.BankAccount;
using V6Soft.Models.Accounting.ViewModels.PhanNhomTieuKhoan;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Banking.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.TaiKhoan ToTaiKhoanDto(this AccountDetail source)
        {
            return new AccModels.TaiKhoan
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                Tai_Khoan = source.Tai_Khoan,
                Ten_TaiKhoan = source.Ten_TaiKhoan,
                Ten_TaiKhoan2 = source.Ten_TaiKhoan2,
                Ten_ngan = source.Ten_ngan,
                Ten_ngan2 = source.Ten_ngan2,
                MaNgoaiTe = source.MaNgoaiTe,
                Loai_TaiKhoan = source.Loai_TaiKhoan,
                TaiKhoan_me = source.TaiKhoan_me,
                bac_TaiKhoan = source.bac_TaiKhoan,
                TaiKhoan_sc = source.TaiKhoan_sc,
                TaiKhoanCongNo = source.TaiKhoanCongNo,
                nh_TaiKhoan0 = source.nh_TaiKhoan0,
                nh_TaiKhoan2 = source.nh_TaiKhoan2,
                TrangThai = source.TrangThai,
                Loai_cl_no = source.Loai_cl_no,
                Loai_cl_co = source.Loai_cl_co,
                CheckSync = source.CheckSync,
                UID = source.UID

            };
        }
        public static AccModels.AccountType ToPhanLoaiTaiKhoanDto(this ModifiedAccountType source)
        {
            return new AccModels.AccountType
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
                UID = source.UID
            };
        }
        public static AccModels.PhanNhomTieuKhoan ToPhanNhomTieuKhoanDto(this ModifiedPhanNhomTieuKhoan source)
        {
            return new AccModels.PhanNhomTieuKhoan
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
                UID = source.UID
            };
        }
        public static AccModels.BankAccount ToTaiKhoanNganHangDto(this BankAccountDetail source)
        {
            return new AccModels.BankAccount
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                TaiKhoan = source.TaiKhoan,
                TaiKhoanNganHang = source.TaiKhoanNganHang,
                TenTaiKhoanNganHang = source.TenTaiKhoanNganHang,
                TenTaiKhoanNganHang2 = source.TenTaiKhoanNganHang2,
                DiaChi = source.DiaChi,
                TrangThai = source.TrangThai,
                UID = source.UID
            };
        }
      
        public static PagedSearchResult<AccountListItem> ToTaiKhoanViewModel(this PagedSearchResult<AccModels.TaiKhoan> source)
        {
            var taiKhoanItem = source.Data.Select(
                x => new AccountListItem
                    {
                        Tai_Khoan = x.Tai_Khoan,
                        Ten_TaiKhoan = x.Ten_TaiKhoan,
                        MaNgoaiTe = x.MaNgoaiTe,
                        Loai_TaiKhoan = x.Loai_TaiKhoan,
                        TaiKhoanCongNo = x.TaiKhoanCongNo,
                        TaiKhoan_sc = x.TaiKhoan_sc,
                        TaiKhoan_me = x.TaiKhoan_me,
                        bac_TaiKhoan = x.bac_TaiKhoan,
                        nh_TaiKhoan0 = x.nh_TaiKhoan0,
                        nh_TaiKhoan2 = x.nh_TaiKhoan2,
                        Ten_ngan = x.Ten_ngan,
                        Ten_TaiKhoan2 = x.Ten_TaiKhoan,
                        Ten_ngan2 = x.Ten_TaiKhoan2,
                        NgayKhoiTao = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                        UID =  x.UID
                    }
            );
            return new PagedSearchResult<AccountListItem>(taiKhoanItem.ToList(), source.Total);
        }

        public static PagedSearchResult<AccountTypeListItem> ToPhanLoaiTaiKhoanViewModel(this PagedSearchResult<AccModels.AccountType> source)
        {
            var phanLoaiTaiKhoanItem = source.Data.Select(
                x => new AccountTypeListItem()
                {
                   MaNhom = x.MaNhom,
                   TenNhom = x.TenNhom,
                   TenNhom2 = x.TenNhom2,
                   UID =  x.UID
                   
                   
                }
            );
            return new PagedSearchResult<AccountTypeListItem>(phanLoaiTaiKhoanItem.ToList(), source.Total);
        }
        public static PagedSearchResult<PhanNhomTieuKhoanItem> ToPhanNhomTieuKhoanViewModel(this PagedSearchResult<AccModels.PhanNhomTieuKhoan> source)
        {
            var phanNhomTieuKhoanItem = source.Data.Select(
                x => new PhanNhomTieuKhoanItem
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

                }
            );
            return new PagedSearchResult<PhanNhomTieuKhoanItem>(phanNhomTieuKhoanItem.ToList(), source.Total);
        }
        public static PagedSearchResult<BankAccountListItem> ToBankAccountViewModel(this PagedSearchResult<AccModels.BankAccount> source)
        {
            var taiKhoanNganHangItem = source.Data.Select(
                x => new BankAccountListItem
                {
                    UID = x.UID,
                    TaiKhoan = x.TaiKhoan,
                    TaiKhoanNganHang = x.TaiKhoanNganHang,
                    TenTaiKhoanNganHang = x.TenTaiKhoanNganHang,
                    TenTaiKhoanNganHang2 = x.TenTaiKhoanNganHang2,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    

                }
            );
            return new PagedSearchResult<BankAccountListItem>(taiKhoanNganHangItem.ToList(), source.Total);
        }

    }
}
