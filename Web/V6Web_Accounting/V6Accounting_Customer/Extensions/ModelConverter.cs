using System;
using System.Linq;
using V6Soft.Models.Accounting.ViewModels.Customer;
using V6Soft.Models.Accounting.ViewModels.CustomerGroup;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Customer.Extensions
{
    public static class ModelConverter
    {
        //Todo: Check if this function is nescessary any longer
        //public static AccModels.Customer ToCustomerDto(this CustomerDetail source)
        //{
        //    return new AccModels.Customer
        //    {
        //        CreatedDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
        //        ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
        //        CreatedUserId = source.CreatedUserId,
        //        ModifiedUserId = source.ModifiedUserId,
        //        DiaChi = source.DiaChi,
        //        DienThoai = source.DienThoai,
        //        DoiTac = source.DoiTac,
        //        Du13 = source.Du13,
        //        DuNgoaiTe13 = source.DuNgoaiTe13,
        //        Email = source.Email,
        //        GhiChu = source.GhiChu,
        //        Fax = source.Fax,
        //        GhiChuTuDo1 = source.GhiChuTuDo1,
        //        GhiChuTuDo2 = source.GhiChuTuDo2,
        //        GhiChuTuDo3 = source.GhiChuTuDo3,
        //        HanThanhToan = source.HanThanhToan > 0,
        //        LaKhachHang = source.LaKhachHang > 0,
        //        LaNhaCungCap = source.LaNhaCungCap > 0,
        //        LaNhanVien = source.LaNhanVien > 0,
        //        DienThoaiDiDong = source.DienThoaiDiDong,
        //        MaHinhThucThanhToan = source.MaHinhThucThanhToan,
        //        MaKhachHang = source.MaKhachHang,
        //        MaPhuong = source.MaPhuong,
        //        MaQuan = source.MaQuan,
        //        MaTinh = source.MaTinh,
        //        MaSoNhanVien = source.MaSoNhanVien,
        //        MaSoThue = source.MaSoThue,
        //        MaTuDo1 = source.MaTuDo1,
        //        MaTuDo2 = source.MaTuDo2,
        //        MaTuDo3 = source.MaTuDo3,
        //        NganHang = source.NganHang,
        //        NgayGioiHan = source.NgayGioiHan,
        //        NgayTuDo1 = source.NgayTuDo1,
        //        NgayTuDo2 = source.NgayTuDo2,
        //        NgayTuDo3 = source.NgayTuDo3,
        //        NhomKhachHang1 = source.NhomKhachHang1,
        //        NhomKhachHang2 = source.NhomKhachHang2,
        //        NhomKhachHang3 = source.NhomKhachHang3,
        //        NhomKhachHang9 = source.NhomKhachHang9,
        //        OngBa = source.OngBa,
        //        SoLuongTuDo1 = source.SoLuongTuDo1,
        //        SoLuongTuDo2 = source.SoLuongTuDo2,
        //        SoLuongTuDo3 = source.SoLuongTuDo3,
        //        TaiKhoan = source.TaiKhoan,
        //        TaiKhoanNganHang = source.TaiKhoanNganHang,
        //        TenBoPhan = source.TenBoPhan,
        //        TenKhachHang = source.TenKhachHang,
        //        TenTiengAnh = source.TenTiengAnh,
        //        ThongTinSoNha = source.ThongTinSoNha,
        //        TongTienCongNo = source.TongTienCongNo,
        //        TongTienHoaDon = source.TongTienHoaDon,
        //        TrangChu = source.TrangChu,
        //        TrangThai = source.TrangThai,
        //        UID = source.UID,
        //        CheckSync = source.CheckSync
        //    };
        //}

        //public static PagedSearchResult<CustomerListItem> ToCustomerViewModel(this PagedSearchResult<AccModels.Customer> source)
        //{
        //    var customerItems = source.Data.Select(
        //        x => new CustomerListItem
        //            {
        //                UID = x.UID,
        //                CreatedDate = x.CreatedDate,
        //                ModifiedUserId = x.ModifiedUserId,
        //                CreatedUserId = x.CreatedUserId,
        //                CreatedTime = x.CreatedTime,
        //                ModifiedDate = x.ModifiedDate,
        //                ModifiedTime = x.ModifiedTime,
        //                TrangThai = x.TrangThai,
        //                DiaChi = x.DiaChi,
        //                DienThoai = x.DienThoai,
        //                DienThoaiDiDong = x.DienThoaiDiDong,
        //                Email = x.Email,
        //                Fax = x.Fax,
        //                GhiChu = x.GhiChu,
        //                LaKhachHang = x.LaKhachHang,
        //                LaNhaCungCap = x.LaNhaCungCap,
        //                LaNhanVien = x.LaNhanVien,
        //                MaKhachHang = x.MaKhachHang,
        //                MaSoThue = x.MaSoThue,
        //                TenKhachHang = x.TenKhachHang,
        //            }
        //    );
        //    return new PagedSearchResult<CustomerListItem>(customerItems.ToList(), source.Total);
        //}
        public static AccModels.NhomKhachHang ToNhomKhachHangDto(this CustomerGroupDetail source)
        {
            return new AccModels.NhomKhachHang
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
                CheckSync = source.CheckSync
            };
        }

        public static PagedSearchResult<CustomerGroupListItem> ToNhomKhachHangViewModel(this PagedSearchResult<AccModels.NhomKhachHang> source)
        {
            var nhomKhachHangItems = source.Data.Select(
                x => new CustomerGroupListItem
                {
                    UID = x.UID,
                    CreatedDate = x.CreatedDate,
                    ModifiedUserId = x.ModifiedUserId,
                    CreatedUserId = x.CreatedUserId,
                    CreatedTime = x.CreatedTime,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedTime = x.ModifiedTime,
                    TenNhom = x.TenNhom,
                    TenNhom2 = x.TenNhom2,
                    MaNhom = x.MaNhom,
                    LoaiNhom = x.LoaiNhom,

                }
            );
            return new PagedSearchResult<CustomerGroupListItem>(nhomKhachHangItems.ToList(), source.Total);
        }
    }
}
