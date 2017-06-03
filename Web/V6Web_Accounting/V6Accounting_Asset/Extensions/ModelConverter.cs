using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.EquipmentChangedReason;
using V6Soft.Models.Accounting.ViewModels.EquipmentType;
using V6Soft.Models.Accounting.ViewModels.PhanNhomCongCu;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Asset.Extensions
{
    public static class ModelConverter
    {
        //public static Models.Accounting.DTO.LyDoTangGiamCongCu TolyDoTangGiamCongCuDto(this EquipmentChangedReasonDetail source)
        //{
        //    return new Models.Accounting.DTO.LyDoTangGiamCongCu
        //    {
        //        CreatedDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
        //        ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
        //        CreatedUserId = source.CreatedUserId,
        //        ModifiedUserId = source.ModifiedUserId,
        //        Ma_TGCungCap = source.Ma_TGCungCap,
        //        Ten_tg_cc = source.Ten_tg_cc,
        //        Ten_tg_cc2 = source.Ten_tg_cc2,
        //        Loai_tg_cc = source.Loai_tg_cc,
        //        TrangThai = source.TrangThai,
        //        MaTuDo1 = source.MaTuDo1,
        //        MaTuDo2 = source.MaTuDo2,
        //        MaTuDo3 = source.MaTuDo3,
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
        //    };
        //}
        //public static Models.Accounting.DTO.PhanLoaiCongCu ToPhanLoaiCongCuDto(this EquipmentTypeDetails source)
        //{
        //    return new Models.Accounting.DTO.PhanLoaiCongCu
        //    {
        //        CreatedDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
        //        ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
        //        CreatedUserId = source.CreatedUserId,
        //        ModifiedUserId = source.ModifiedUserId,
        //        Ten_Loai = source.te
        //        UID = source.UID,
        //    };
        //}
        public static Models.Accounting.DTO.PhanNhomCongCu ToPhanNhomCongCuDto(this ModifiedPhanNhomCongCu source)
        {
            return new Models.Accounting.DTO.PhanNhomCongCu
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
        public static PagedSearchResult<EquipmentChangedReasonListItem> ToLyDoTangGiamCongCuViewModel(this PagedSearchResult<AccModels.LyDoTangGiamCongCu> source)
        {
            var lyDoTangGiamCongCuItem = source.Data.Select(
                x => new EquipmentChangedReasonListItem
                    {
                        Loai_tg_cc = x.Loai_tg_cc,
                        Ma_TGCungCap = x.Ma_TGCungCap,
                        Ten_tg_cc = x.Ten_tg_cc,
                        Ten_tg_cc2 = x.Ten_tg_cc2,
                        NgayKhoiTao = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                        UID = x.UID
                    }
            );
            return new PagedSearchResult<EquipmentChangedReasonListItem>(lyDoTangGiamCongCuItem.ToList(), source.Total);
        }
        public static PagedSearchResult<EquipmentTypeListItem> ToPhanLoaiCongCuViewModel(this PagedSearchResult<AccModels.PhanLoaiCongCu> source)
        {
            var phanLoaiCongCuItem = source.Data.Select(
                x => new EquipmentTypeListItem
                {
                    CreatedDate = x.CreatedDate,
                    CreatedTime = x.CreatedTime,
                    CreatedUserId = (byte)x.CreatedUserId,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedTime = x.ModifiedTime,
                    ModifiedUserId = (byte)x.ModifiedUserId,
                    UID = x.UID,
                    MaLoai =  x.MaLoai,
                    Ten_Loai = x.Ten_Loai,
                    Ten_Loai2 = x.Ten_Loai2
                }
            );
            return new PagedSearchResult<EquipmentTypeListItem>(phanLoaiCongCuItem.ToList(), source.Total);
        }

        public static PagedSearchResult<PhanNhomCongCuItem> ToPhanNhomCongCuViewModel(this PagedSearchResult<AccModels.PhanNhomCongCu> source)
        {
            var phanLoaiCongCuItem = source.Data.Select(
                x => new PhanNhomCongCuItem
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
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID =  x.UID
                    
                }
            );
            return new PagedSearchResult<PhanNhomCongCuItem>(phanLoaiCongCuItem.ToList(), source.Total);
        }
    }
}
