using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.PaymentMethod;
using V6Soft.Models.Accounting.ViewModels.Shipment;
using V6Soft.Models.Accounting.ViewModels.ShippingMethod;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Customer.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.PaymentMethod ToHinhThucThanhToanDto(this PaymentMethodDetail source)
        {
            return new AccModels.PaymentMethod
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaHinhThucThanhToan = source.MaHinhThucThanhToan,
                TenHinhThucThanhToan = source.TenHinhThucThanhToan,
                TenHinhThucThanhToan2 = source.TenHinhThucThanhToan2,
                HanThanhToan = source.HanThanhToan,
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
        public static AccModels.ShippingMethod ToHinhThucVanChuyenDto(this ShippingMethodDetail source)
        {
            return new AccModels.ShippingMethod
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaHinhThucVanChuyen = source.MaHinhThucVanChuyen,
                TenHinhThucVanChuyen = source.TenHinhThucVanChuyen,
                TenHinhThucVanChuyen2 = source.TenHinhThucVanChuyen2,
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
                UID = source.UID,
            };
        }
        public static AccModels.Shipment ToVanChuyenDto(this ShipmentDetail source)
        {
            return new AccModels.Shipment
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaVanChuyen = source.MaVanChuyen,
                TenVanChuyen = source.TenVanChuyen,
                TenVanChuyen2 = source.TenVanChuyen2,
                LoaiVanChuyen = source.LoaiVanChuyen,
                OngBa = source.OngBa,
                ChieuCao = source.ChieuCao,
                ChieuDai = source.ChieuDai,
                TheTich = source.TheTich,
                TrongLuong = source.TrongLuong,
                ChieuNgang = source.ChieuNgang,
                DonViTinhChieuCao = source.DonViTinhChieuCao,
                DonViTinhChieuDai = source.DonViTinhChieuDai,
                DonViTinhTheTich = source.DonViTinhTheTich,
                DonViTinhTrongLuong = source.DonViTinhTrongLuong,
                DonViTinhChieuNgang = source.DonViTinhChieuNgang,
                ThoiGianXepHang = source.ThoiGianXepHang,
                ThoiGianDoHang = source.ThoiGianDoHang,
                DonViTinh_Xep = source.DonViTinh_Xep,
                DonViTinh_Do = source.DonViTinh_Do,
                BienSo = source.BienSo,
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
                UID = source.UID,
            };
        }

        public static PagedSearchResult<PaymentMethodListItem> ToPaymentMethodViewModel(this PagedSearchResult<AccModels.PaymentMethod> source)
        {
            var hinhThucThanhToanItem = source.Data.Select(
                x => new PaymentMethodListItem
                    {
                        TenHinhThucThanhToan = x.TenHinhThucThanhToan,
                        TenHinhThucThanhToan2 = x.TenHinhThucThanhToan,
                        MaHinhThucThanhToan = x.MaHinhThucThanhToan,
                        NgayKhoiTao = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        UID = x.UID,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,

                    }
            );
            return new PagedSearchResult<PaymentMethodListItem>(hinhThucThanhToanItem.ToList(), source.Total);
        }
        public static PagedSearchResult<ShippingMethodListItem> ToShippingMethodViewModel(this PagedSearchResult<AccModels.ShippingMethod> source)
        {
            var hinhThucVanChuyenItem = source.Data.Select(
                x => new ShippingMethodListItem
                {
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    TenHinhThucVanChuyen = x.TenHinhThucVanChuyen,
                    UID = x.UID,
                    TenHinhThucVanChuyen2 = x.TenHinhThucVanChuyen2,
                    MaHinhThucVanChuyen = x.MaHinhThucVanChuyen
                }
            );
            return new PagedSearchResult<ShippingMethodListItem>(hinhThucVanChuyenItem.ToList(), source.Total);
        }
        public static PagedSearchResult<ShipmentListItem> ToShipmentViewModel(this PagedSearchResult<AccModels.Shipment> source)
        {
            var vanChuyenItem = source.Data.Select(
                x => new ShipmentListItem
                {
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                     TenVanChuyen = x.TenVanChuyen,
                     TenVanChuyen2 = x.TenVanChuyen2,
                     UID = x.UID,
                     MaVanChuyen = x.MaVanChuyen
                }
            );
            return new PagedSearchResult<ShipmentListItem>(vanChuyenItem.ToList(), source.Total);
        }
    }
}
