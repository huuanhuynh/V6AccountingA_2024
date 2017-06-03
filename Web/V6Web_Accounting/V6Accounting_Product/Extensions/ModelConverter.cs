using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.IntermediateProduct;
using V6Soft.Models.Accounting.ViewModels.MeasurementConversion;
using V6Soft.Models.Accounting.ViewModels.MeasurementUnit;
using V6Soft.Models.Accounting.ViewModels.ServiceStatus;
using V6Soft.Models.Accounting.ViewModels.ServiceType;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Product.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.MeasurementUnit ToDonViTinhDto(this MeasurementUnitDetail source)
        {
            return new AccModels.MeasurementUnit
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                Ma_DonViTinh = source.Ma_DonViTinh,
                Ten_DonViTinh = source.Ten_DonViTinh,
                Ten_DonViTinh2 = source.Ten_DonViTinh2,
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
                CheckSync = source.CheckSync
            };
        }
        public static AccModels.ServiceType ToLoaiDichVuDto(this ServiceTypeDetail source)
        {
            return new AccModels.ServiceType
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
                UID = source.UID,
            };
        }
        public static AccModels.MeasurementConversion ToQuyDoiDonViTinhDto(this MeasurementConversionDetail source)
        {
            return new AccModels.MeasurementConversion
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaVatTu = source.MaVatTu,
                DonViTinh = source.DonViTinh,
                DonViTinhQuyDoi = source.DonViTinhQuyDoi,
                HeSo = source.HeSo,
                Xtype = source.Xtype,
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
                CheckSync = source.CheckSync
            };
        }
        public static AccModels.IntermediateProduct ToSanPhamTrungGianDto(this IntermediateProductDetail source)
        {
            return new AccModels.IntermediateProduct
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,

                MaVatTutg = source.MaVatTutg,
                PartNo = source.PartNo,
                TenVatTutg = source.TenVatTutg,
                TenVatTutg2 = source.TenVatTutg2,
                DonViTinh = source.DonViTinh,
                DonViTinh1 = source.DonViTinh1,
                HeSo1 = source.HeSo1,
                VatTuTonKho = source.VatTuTonKho,
                GiaTon = source.GiaTon,
                ChoPhepSuaTaiKhoanVatTu = source.ChoPhepSuaTaiKhoanVatTu,
                TaiKhoan_cl_vt = source.TaiKhoan_cl_vt,
                TaiKhoanVatTu = source.TaiKhoanVatTu,
                TaiKhoanGiaVon = source.TaiKhoanGiaVon,
                TaiKhoanDoanhThu = source.TaiKhoanDoanhThu,
                TaiKhoanTraLai = source.TaiKhoanTraLai,
                TaiKhoan_spdd = source.TaiKhoan_spdd,
                NhomVatTu1 = source.NhomVatTu1,
                NhomVatTu2 = source.NhomVatTu2,
                NhomVatTu3 = source.NhomVatTu3,
                SoLuongToiThieu = source.SoLuongToiThieu,
                SoLuongToiDa = source.SoLuongToiDa,
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
                ShortName = source.ShortName,
                BarCode = source.BarCode,
                Loai_VatTu = source.Loai_VatTu,
               // TinhTrangDichVu = source.TinhTrangVatTu,
                nhieu_DonViTinh = source.nhieu_DonViTinh,
                IsParcel = source.IsParcel,
                KK_YN = source.KK_YN,
                TrongLuong = source.TrongLuong,
                DonViTinhTrongLuong = source.DonViTinhTrongLuong,
                TrongLuong0 = source.TrongLuong0,
                DonViTinhTrongLuong0 = source.DonViTinhTrongLuong0,
                Length = source.Length,
                ChieuNgang = source.ChieuNgang,
                ChieuCao = source.ChieuCao,
                Diamet = source.Diamet,
                DonViTinhLength = source.DonViTinhLength,
                DonViTinhChieuNgang = source.DonViTinhChieuNgang,
                DonViTinhChieuCao = source.DonViTinhChieuCao,
                DonViTinhDiamet = source.DonViTinhDiamet,
                Size = source.Size,
                MauSac = source.MauSac,
                Kieu = source.Kieu,
                MaQuocGia = source.MaQuocGia,
                Packs = source.Packs,
                Packs1 = source.Packs1,
                ABCCode = source.ABCCode,
                cycle_kk = source.cycle_kk,
                MaViTri = source.MaViTri,
                MaKho = source.MaKho,
                HanSuDung = source.HanSuDung,
                HanBaoHanh = source.HanBaoHanh,
                KieuLo = source.KieuLo,
                CachXuat = source.CachXuat,
              //  NhanVien = source.NhanVienCodeL,
                Ldateqc = source.Ldateqc,
                Lsoqty = source.Lsoqty,
                Lsoqtymin = source.Lsoqtymin,
                Lsoqtymax = source.Lsoqtymax,
                Lcycle = source.Lcycle,
                Lpolicy = source.Lpolicy,
                pMaNhanVien = source.pMaNhanVien,
                pMaKhachHangc = source.pMaKhachHangc,
                MaKhachHangtg = source.MaKhachHangtg,
                pMaKhachHangp = source.pMaKhachHangp,
                Prating = source.Prating,
                Pdeliver = source.Pdeliver,
                Pflex = source.Pflex,
                Ptech = source.Ptech,
                NhomVatTu9 = source.NhomVatTu9,
                MaThue = source.MaThue,
                MaThuenk = source.MaThuenk,
                TaiKhoan_ck = source.TaiKhoan_ck,
                TheoDoiDate = source.TheoDoiDate,
                TaiKhoan_cp = source.TaiKhoan_cp,
                MaBoPhanHachToan = source.MaBoPhanHachToan,
                TheoDoiViTri = source.TheoDoiViTri,
                pMaKhachHangl = source.pMaKhachHangl,
                DonViTinhPacks = source.DonViTinhPacks,
                EmployeeCodeL = source.EmployeeCodeL,
                Pquality = source.pquanlity,
                pquanlity = source.pquanlity,
                ThongTinVatTu = source.ThongTinVatTu,
                PurchaseDateL = source.PurchaseDateL,
                UID = source.UID,
            };
        }
        public static AccModels.ServiceStatus ToTinhTrangDichVuDto(this ServiceStatusDetail source)
        {
            return new AccModels.ServiceStatus
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                Ten_tt = source.Ten_tt,
                Ten_tt2 = source.Ten_tt2,
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
                UID = source.UID,
                ThongTinVatTu = source.ThongTinVatTu
            };
        }


        public static PagedSearchResult<MeasurementUnitListItem> ToMeasurementUnitViewModel(this PagedSearchResult<AccModels.MeasurementUnit> source)
        {
            var donViTinhItem = source.Data.Select(
                x => new MeasurementUnitListItem
                    {
                        Ma_DonViTinh = x.Ma_DonViTinh,
                        Ten_DonViTinh = x.Ten_DonViTinh,
                        Ten_DonViTinh2 = x.Ten_DonViTinh2,
                        NgayNhap = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                    }
            );
            return new PagedSearchResult<MeasurementUnitListItem>(donViTinhItem.ToList(), source.Total);
        }
        public static PagedSearchResult<ServiceTypeListItem> ToServiceTypeViewModel(this PagedSearchResult<AccModels.ServiceType> source)
        {
            var loaiDichVuItem = source.Data.Select(
                x => new ServiceTypeListItem
                {
                    MaLoai = x.MaLoai,
                    Ten_Loai = x.Ten_Loai,
                    Ten_Loai2 = x.Ten_Loai2,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    UID = x.UID,
                    NguoiSua = (byte)x.ModifiedUserId,
                }
            );
            return new PagedSearchResult<ServiceTypeListItem>(loaiDichVuItem.ToList(), source.Total);
        }
        public static PagedSearchResult<MeasurementConversionListItem> ToMeasurementConversionViewModel(this PagedSearchResult<AccModels.MeasurementConversion> source)
        {
            var quyDoiDonViTinhItem = source.Data.Select(
                x => new MeasurementConversionListItem
                {
                    MaVatTu = x.MaVatTu,
                    DonViTinhQuyDoi = x.DonViTinhQuyDoi,
                    DonViTinh = x.DonViTinh,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID = x.UID
                }
            );
            return new PagedSearchResult<MeasurementConversionListItem>(quyDoiDonViTinhItem.ToList(), source.Total);
        }
        public static PagedSearchResult<IntermediateProductListItem> ToIntermediateProductViewModel(this PagedSearchResult<AccModels.IntermediateProduct> source)
        {
            var sanPhamTrungGianItem = source.Data.Select(
                x => new IntermediateProductListItem
                {
                    MaVatTutg = x.MaVatTutg,
                    PartNo = x.PartNo,
                    TenVatTutg = x.TenVatTutg,
                    DonViTinh = x.DonViTinh,
                    VatTuTonKho = x.VatTuTonKho,
                    GiaTon = x.GiaTon,
                    TaiKhoanVatTu = x.TaiKhoanVatTu,
                    TaiKhoanDoanhThu = x.TaiKhoanDoanhThu,
                    TaiKhoanGiaVon = x.TaiKhoanGiaVon,
                    TaiKhoanTraLai = x.TaiKhoanTraLai,
                    TaiKhoan_spdd = x.TaiKhoan_spdd,
                    TaiKhoan_cl_vt = x.TaiKhoan_cl_vt,
                    NhomVatTu1 = x.NhomVatTu1,
                    NhomVatTu2 = x.NhomVatTu2,
                    NhomVatTu3 = x.NhomVatTu3,
                    SoLuongToiDa = x.SoLuongToiDa,
                    SoLuongToiThieu = x.SoLuongToiThieu,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID = x.UID
                }
            );
            return new PagedSearchResult<IntermediateProductListItem>(sanPhamTrungGianItem.ToList(), source.Total);
        }
        public static PagedSearchResult<ServiceStatusListItem> ToServiceStatusViewModel(this PagedSearchResult<AccModels.ServiceStatus> source)
        {
            var tinhTrangDichVuItem = source.Data.Select(
                x => new ServiceStatusListItem
                {
                    Ten_tt = x.Ten_tt,
                    Ten_tt2 = x.Ten_tt,
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    UID = x.UID,
                    ThongTinVatTu = x.ThongTinVatTu
                }
            );
            return new PagedSearchResult<ServiceStatusListItem>(tinhTrangDichVuItem.ToList(), source.Total);
        }
    }
}
