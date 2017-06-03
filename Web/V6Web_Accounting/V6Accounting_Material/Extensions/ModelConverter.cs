using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.Material;
using V6Soft.Models.Accounting.ViewModels.MaterialGroup;
using V6Soft.Models.Accounting.ViewModels.MaterialType;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Material.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.MaterialType ToLoaiVatTuDto(this MaterialTypeDetail source)
        {
            return new AccModels.MaterialType
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                Loai_VatTu = source.Loai_VatTu,
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
        public static AccModels.MaterialGroup ToNhomVatTuDto(this MaterialGroupDetail source)
        {
            return new AccModels.MaterialGroup
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
                CheckSync = source.CheckSync,
                TrangThai = source.TrangThai,
                UID = source.UID,
            };
        }
        public static AccModels.Material ToVatTuDto(this MaterialDetail source)
        {
            return new AccModels.Material
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaVatTu = source.MaVatTu,
                PartNo = source.PartNo,
                TenVatTu = source.TenVatTu,
                TenVatTu2 = source.TenVatTu2,
                DonViTinh = source.DonViTinh,
                DonViTinh1 = source.DonViTinh1,
                HeSo1 = source.HeSo1,
                VatTuTonKho = source.VatTuTonKho,
                GiaTon = source.GiaTon,
                ChoPhepSuaTaiKhoanVatTu = source.ChoPhepSuaTaiKhoanVatTu.HasValue && source.ChoPhepSuaTaiKhoanVatTu.Value > 0,
                TaiKhoan_CL_VT = source.TaiKhoan_CL_VT,
                TaiKhoanVatTu = source.TaiKhoanVatTu,
                TaiKhoanGiaVon = source.TaiKhoanGiaVon,
                TaiKhoanDoanhThu = source.TaiKhoanDoanhThu,
                TaiKhoanTraLai = source.TaiKhoanTraLai,
                TaiKhoanSanPhamDoDang = source.TaiKhoanSanPhamDoDang,
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
                TinhTrangVatTu = source.TinhTrangVatTu,
                NhieuDonViTinh = source.nhieu_DonViTinh,
                IsParcel = source.IsParcel,
                KK_YN = source.KK_YN,
                TrongLuong = source.TrongLuong,
                DonViTinhTrongLuong = source.DonViTinhTrongLuong,
                TrongLuong0 = source.TrongLuong0,
                DonViTinhTrongLuong0 = source.DonViTinhTrongLuong0,
                ChieuDai = source.ChieuDai,
                ChieuNgang = source.ChieuNgang,
                ChieuCao = source.ChieuCao,
                Diamet = source.Diamet,
                DonViTinhChieuDai = source.DonViTinhChieuDai,
                DonViTinhChieuCao = source.DonViTinhChieuCao,
                DonViTinhDiamet = source.DonViTinhDiamet,
                Size = source.Size,
                MauSac = source.MauSac,
                MaQuocGia = source.MaQuocGia,
                Packs = source.Packs,
                Packs1 = source.Packs1,
                ABCCode = source.ABCCode,
                Cycle_KK = source.Cycle_KK,
                MaViTri = source.MaViTri,
                MaKho = source.MaKho,
                HanSuDung = source.HanSuDung,
                HanBaoHanh = source.HanBaoHanh,
                KieuLo = source.KieuLo,
                CachXuat = source.CachXuat,
                NgayMuaL = source.NgayMuaL,
                Ldateqc = source.Ldateqc,
                Lsoqty = source.Lsoqty,
                Lsoqtymin = source.Lsoqtymin,
                Lsoqtymax = source.Lsoqtymax,
                Lcycle = source.Lcycle,
                Lpolicy = source.Lpolicy,
                KhachHangCodePC = source.pMaKhachHangc,
                KhachHangCodePP = source.pMaKhachHangp,
                KhachHangCodePL = source.pMaKhachHangl,
                Prating = source.Prating,
                ChatLuongP = source.ChatLuongP,
                SoLuongP = source.SoLuongP,
                Pdeliver = source.Pdeliver,
                Pflex = source.Pflex,
                Ptech = source.Ptech,
                NhomVatTu9 = source.NhomVatTu9,
                MaThue = source.MaThue,
                MaThueNhapKhau = source.MaThuenk,
                TaiKhoanChietKhau = source.TaiKhoanChietKhau,
                TheoDoiDate = source.TheoDoiDate,
                TaiKhoan_CP = source.TaiKhoan_CP,
                MaBoPhanHachToan = source.MaBoPhanHachToan,
                TheoDoiViTri = source.TheoDoiViTri,
                MaVatTuTheGioi = source.MaVatTutg,
                MaKhachHangTheGioi = source.MaKhachHangtg,
                TenKhachHangTheGioi = source.TenKhachHangTheGioi,
                TenQuocGia = source.TenQuocGia,
                ThueSuat = source.ThueSuat,
                NhomVatTu4 = source.NhomVatTu4,
                NhomVatTu5 = source.NhomVatTu5,
                NhomVatTu6 = source.NhomVatTu6,
                NhomVatTu7 = source.NhomVatTu7,
                NhomVatTu8 = source.NhomVatTu8,
                Kieu = source.Kieu,
                MaVuViec = source.MaVuViec,
                CheckSync = source.CheckSync,
                UID = source.UID,
                DonViTinhChieuNgang = source.DonViTinhChieuNgang,
                DonViTinhPacks = source.DonViTinhPacks,
                NhanVienCodeL = source.lMaNguonVonien,
                NhanVienCodeP = source.pMaNguonVonien
            };
        }

        public static PagedSearchResult<MaterialTypeItem> ToMaterialTypeViewModel(this PagedSearchResult<AccModels.MaterialType> source)
        {
            var loaiVatTuItem = source.Data.Select(
                x => new MaterialTypeItem
                    {
                        Loai_VatTu = x.Loai_VatTu,
                        Ten_Loai = x.Ten_Loai,
                        Ten_Loai2 = x.Ten_Loai2,
                        NgayNhap = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        UID = x.UID
                    }
            );
            return new PagedSearchResult<MaterialTypeItem>(loaiVatTuItem.ToList(), source.Total);
        }

        public static PagedSearchResult<MaterialGroupListItem> ToMaterialGroupViewModel(this PagedSearchResult<AccModels.MaterialGroup> source)
        {
            var nhomVatTuItem = source.Data.Select(
                x => new MaterialGroupListItem
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
                    UID = x.UID
                }
            );
            return new PagedSearchResult<MaterialGroupListItem>(nhomVatTuItem.ToList(), source.Total);
        }
        public static PagedSearchResult<MaterialListItem> ToMaterialViewModel(this PagedSearchResult<AccModels.Material> source)
        {
            var vatTuItem = source.Data.Select(
                x => new MaterialListItem
                {
                    MaVatTu = x.MaVatTu,
                    PartNo = x.PartNo,
                    TenVatTu = x.TenVatTu,
                    DonViTinh = x.DonViTinh,
                    VatTuTonKho = x.VatTuTonKho,
                    GiaTon = x.GiaTon,
                    TaiKhoanVatTu = x.TaiKhoanVatTu,
                    TaiKhoanDoanhThu = x.TaiKhoanDoanhThu,
                    TaiKhoanGiaVon = x.TaiKhoanGiaVon,
                    TaiKhoanTraLai = x.TaiKhoanTraLai,
                    TaiKhoanSanPhamDoDang = x.TaiKhoanSanPhamDoDang,
                    TaiKhoan_CL_VT = x.TaiKhoan_CL_VT,
                    NhomVatTu1 = x.NhomVatTu1,
                    NhomVatTu2 = x.NhomVatTu2,
                    NhomVatTu3 = x.NhomVatTu3,
                    SoLuongToiDa = x.SoLuongToiDa,
                    SoLuongToiThieu = x.SoLuongToiThieu,
                    TenVatTu2 = x.TenVatTu2,
                    CreatedDate = x.CreatedDate,
                    CreatedTime = x.CreatedTime,
                    CreatedUserId = (byte)x.CreatedUserId,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedTime = x.ModifiedTime,
                    UID = x.UID,
                    ModifiedUserId = (byte)x.ModifiedUserId,
                }
            );
            return new PagedSearchResult<MaterialListItem>(vatTuItem.ToList(), source.Total);
        }
    }
}
