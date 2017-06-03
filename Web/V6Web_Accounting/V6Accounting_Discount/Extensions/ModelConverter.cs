using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.Discount;
using V6Soft.Models.Accounting.ViewModels.DiscountType;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Duscount.Extensions
{
    public static class ModelConverter
    {
        public static Models.Accounting.DTO.Discount ToChietKhauDto(this DiscountDetail source)
        {
            return new Models.Accounting.DTO.Discount
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaChietKhau = source.MaChietKhau,
                TenChietKhau = source.TenChietKhau,
                TenChietKhau2 = source.TenChietKhau2,
                LoaiChietKhau = source.LoaiChietKhau,
                Muc_DO = source.Muc_DO,
                ChoPhepSuaTien = source.ChoPhepSuaTien,
                ChoPhepSuaTienH = source.ChoPhepSuaTienH,
                Cong_YN = source.Cong_YN,
                ConLai_YN = source.ConLai_YN,
                NgayChungTu1 = source.NgayChungTu1,
                NgayChungTu2 = source.NgayChungTu2,
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
                UID = source.UID
            };
        }
        public static Models.Accounting.DTO.DiscountType ToLoaiChietKhauDto(this DiscountTypeDetail source)
        {
            return new Models.Accounting.DTO.DiscountType
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaLoai = source.MaLoai,
                Ten_Loai = source.Ten_Loai,
                Ten_Loai2 = source.Ten_Loai2,
                Xtype = source.Xtype,
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
                UID = source.UID
            };
        }

        public static PagedSearchResult<DiscountListItem> ToDiscountViewModel(this PagedSearchResult<AccModels.Discount> source)
        {
            var chietKhauItem = source.Data.Select(
                x => new DiscountListItem
                    {
                        MaChietKhau = x.MaChietKhau,
                        TenChietKhau = x.TenChietKhau,
                        TenChietKhau2 = x.TenChietKhau2,
                        NgayNhap = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                        UID =  x.UID
                    }
            );
            return new PagedSearchResult<DiscountListItem>(chietKhauItem.ToList(), source.Total);
        }
        public static PagedSearchResult<DiscountTypeListItem> ToDiscountTypeViewModel(this PagedSearchResult<AccModels.DiscountType> source)
        {
            var loaiChietKhauItem = source.Data.Select(
                x => new DiscountTypeListItem
                {
                    MaLoai = x.MaLoai,
                    Ten_Loai = x.Ten_Loai,
                    Ten_Loai2 = x.Ten_Loai2,
                    CreatedDate = x.CreatedDate,
                    CreatedTime = x.CreatedTime,
                    CreatedUserId = (byte)x.CreatedUserId,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedTime = x.ModifiedTime,
                    ModifiedUserId = (byte)x.ModifiedUserId,
                    UID = x.UID,
                }
            );
            return new PagedSearchResult<DiscountTypeListItem>(loaiChietKhauItem.ToList(), source.Total);
        }
    }
}
