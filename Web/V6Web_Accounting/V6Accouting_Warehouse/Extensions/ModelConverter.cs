using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.DanhMucLoHang;
using V6Soft.Models.Accounting.ViewModels.DonViCoSo;
using V6Soft.Models.Accounting.ViewModels.LoaiNhapXuat;
using V6Soft.Models.Accounting.ViewModels.Warehouse;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Warehouse.Extensions
{
    public static class ModelConverter
    {
        public static AccModels.DanhMucKhoHang ToKhoHangDto(this WarehouseDetail source)
        {
            return new AccModels.DanhMucKhoHang
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaKho = source.MaKho,
                TenKho = source.TenKho,
                TenKho2 = source.TenKho2,
                TaiKhoanDaiLy = source.TaiKhoanDaiLy,
                STT_NhapTruocXuatTruoc = source.STT_NhapTruocXuatTruoc,
                ThuKho = source.ThuKho,
                DiaChi = source.DiaChi,
                DienThoai = source.DienThoai,
                Fax = source.Fax,
                Email = source.Email,
                MaLoTrinh = source.MaLoTrinh,
                ma_vc = source.ma_vc,
                GhiChu = source.GhiChu,
                TrangThai = source.TrangThai,
                MaDonVi = source.MaDonVi,
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
                TheoDoiDate = source.TheoDoiDate,
                IsParcel = source.IsParcel,
                Nhom_DVCS1 = source.Nhom_DVCS1,
                UID = source.UID,
            };
        }
        public static AccModels.Merchandise ToLoHangDto(this MerchandiseDetail source)
        {
            return new AccModels.Merchandise
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaVatTu = source.MaVatTu,
                MaLo = source.MaLo,
                TenLo = source.TenLo,
                TenLo2 = source.TenLo2,
                NgayNhap = source.NgayNhap,
                NgaySanXuat = source.NgaySanXuat,
                Ngay_BD_SuDung = source.Ngay_BD_SuDung,
                NgayKiemTra = source.NgayKiemTra,
                NgayHetHanSuDung = source.NgayHetHanSuDung,
                NgayHetHanBaoHanh = source.NgayHetHanBaoHanh,
                MaVatTu2 = source.MaVatTu2,
                SoLuongNhap = source.SoLuongNhap,
                SoLuongXuat = source.SoLuongXuat,
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
                SoLoSanXuat = source.SoLoSanXuat,
                SoLoDangKy = source.SoLoDangKy,
                MaKhachHang = source.MaKhachHang,
                UID = source.UID,
            };
        }
        public static AccModels.LoaiNhapXuat ToLoaiNhapXuatDto(this ModifiedLoaiNhapXuat source)
        {
            return new AccModels.LoaiNhapXuat
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaLoaiNhapXuat = source.MaLoaiNhapXuat,
                ten_Loai = source.ten_Loai,
                ten_Loai2 = source.ten_Loai2,
                Loai = source.Loai,
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
        public static AccModels.BranchUnit ToDonViCoSoDto(this BranchUnitListItemDetail source)
        {
            return new AccModels.BranchUnit
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
                MaDonVi = source.MaDonVi,
                TenDonVi = source.TenDonVi,
                TenDonVi2 = source.TenDonVi2,
                TrangThai = source.TrangThai,
                DiaChi = source.DiaChi,
                DiaChi2 = source.DiaChi2,
                DienThoai = source.DienThoai,
                Nhom_DVCS1 = source.Nhom_DVCS1,
                Nhom_DVCS2 = source.Nhom_DVCS2,
                Nhom_DVCS3 = source.Nhom_DVCS3,
                UID = source.UID,
            };
        }


        public static PagedSearchResult<WarehouseListItem> ToWarehouseViewModel(this PagedSearchResult<AccModels.Warehouse> source)
        {
            var danhMucKhoHangItemItem = source.Data.Select(
                x => new WarehouseListItem
                    {
                        MaKho = x.MaKho,
                        NgayNhap = x.CreatedDate,
                        GioKhoiTao = x.CreatedTime,
                        NguoiNhap = (byte)x.CreatedUserId,
                        NgaySua = x.ModifiedDate,
                        ThoiGianSua = x.ModifiedTime,
                        NguoiSua = (byte)x.ModifiedUserId,
                        TenKho = x.TenKho,
                        TenKho2 = x.TenKho2,
                        TaiKhoanDaiLy = x.TaiKhoanDaiLy,
                        UID = x.UID

                    }
            );
            return new PagedSearchResult<WarehouseListItem>(danhMucKhoHangItemItem.ToList(), source.Total);
        }
        public static PagedSearchResult<MerchandiseListItem> ToMerchandiseViewModel(this PagedSearchResult<AccModels.Merchandise> source)
        {
            var danhMucLoHangItem = source.Data.Select(
                x => new MerchandiseListItem
                {
                    NgayNhap = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    MaKhachHang = x.MaKhachHang,
                    MaLo = x.MaLo,
                    MaVatTu = x.MaVatTu,
                    TenLo2 = x.TenLo2,
                    UID = x.UID,
                    TenLo = x.TenLo,
                    NgaySanXuat = x.NgaySanXuat,
                    Ngay_BD_SuDung = x.Ngay_BD_SuDung,
                    NgayKiemTra = x.NgayKiemTra,
                    NgayHetHanSuDung = x.NgayHetHanSuDung,
                    NgayHetHanBaoHanh = x.NgayHetHanBaoHanh,
                    MaVatTu2 = x.MaVatTu2,
                    NgayKhoiTao = x.CreatedDate
                }
            );
            return new PagedSearchResult<MerchandiseListItem>(danhMucLoHangItem.ToList(), source.Total);
        }
        public static PagedSearchResult<LoaiNhapXuatItem> ToLoaiNhapXuatViewModel(this PagedSearchResult<AccModels.LoaiNhapXuat> source)
        {
            var loaiNhapXuatItem = source.Data.Select(
                x => new LoaiNhapXuatItem
                {
                    NgayKhoiTao = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    UID = x.UID,
                    ThoiGianSua = x.ModifiedTime,
                    NguoiSua = (byte)x.ModifiedUserId,
                    ten_Loai = x.ten_Loai,
                    MaLoaiNhapXuat = x.MaLoaiNhapXuat,
                    ten_Loai2 = x.ten_Loai2,
                    Loai = x.Loai,
                }
            );
            return new PagedSearchResult<LoaiNhapXuatItem>(loaiNhapXuatItem.ToList(), source.Total);
        }
        public static PagedSearchResult<BranchUnitListItem> ToBranchUnitViewModel(this PagedSearchResult<AccModels.BranchUnit> source)
        {
            var donViCoSoItem = source.Data.Select(
                x => new BranchUnitListItem
                {
                    NgayNhap = x.CreatedDate,
                    GioKhoiTao = x.CreatedTime,
                    NguoiNhap = (byte)x.CreatedUserId,
                    NgaySua = x.ModifiedDate,
                    ThoiGianSua = x.ModifiedTime,
                    UID = x.UID,
                    NguoiSua = (byte)x.ModifiedUserId,
                    TenDonVi = x.TenDonVi,
                    TenDonVi2 = x.TenDonVi2,
                    MaDonVi = x.MaDonVi,
                }
            );
            return new PagedSearchResult<BranchUnitListItem>(donViCoSoItem.ToList(), source.Total);
        }
    }
}
