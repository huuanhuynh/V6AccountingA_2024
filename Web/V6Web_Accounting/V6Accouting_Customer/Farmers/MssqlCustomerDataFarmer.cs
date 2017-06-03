using System.Collections;
using System.Collections.Generic;
using System.Linq;
using V6Accounting_EntityFramework;
using V6Accounting_EntityFramework.Entities;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.Extensions;
using V6Soft.Models.Core.ViewModels;

namespace V6Accouting_Customer.Farmers
{
    public class CustomerDataFarmer : GenericDataFarmer<Alkh>, ICustomerDataFarmer
    {
        public CustomerDataFarmer(V6AccountingContext dbContext)
            : base(dbContext)
        {
        }

        public PagedSearchResult<Customer> SearchCustomers(SearchCriteria criteria)
        {
            return GetAll().AsQueryable().OrderBy(x => x.ma_kh).Select(x => new Customer
            {
                MaKhachHang = x.ma_kh,
                TenKhachHang = x.ten_kh,
                TenTiengAnh = x.ten_kh2,
                MaSoThue = x.ma_so_thue,
                DiaChi = x.dia_chi,
                DienThoai = x.dien_thoai,
                Fax = x.fax,
                Email = x.e_mail,
                TrangChu = x.home_page,
                DoiTac = x.doi_tac,
                OngBa = x.ong_ba,
                TenBoPhan = x.ten_bp,
                NganHang = x.ngan_hang,
                GhiChu = x.ghi_chu,
                HanThanhToan = x.han_tt,
                TaiKhoan = x.tk,
                NhomKhachHang1 = x.nh_kh1,
                NhomKhachHang2 = x.nh_kh2,
                NhomKhachHang3 = x.nh_kh3,
                DuNgoaiTe13 = x.du_nt13,
                Du13 = x.du13,
                TongTienCongNo = x.t_tien_cn,
                TongTienHoaDon = x.t_tien_hd,
                MaHinhThucThanhToan = x.Ma_httt,
                NhomKhachHang9 = x.Nh_kh9,
                MaSoNhanVien = x.Ma_snvien,
                NgayGioiHan = x.Ngay_gh,
                NgayNhap = x.date0,
                GioKhoiTao = x.time0,
                NguoiNhap = x.user_id0,
                NgaySua = x.date2,
                ThoiGianSua = x.time2,
                NguoiSua = x.user_id2,
                TrangThai = x.status,
                MaTuDo1 = x.ma_td1,
                MaTuDo2 = x.ma_td2,
                MaTuDo3 = x.ma_td3,
                NgayTuDo1 = x.ngay_td1,
                NgayTuDo2 = x.ngay_td2,
                NgayTuDo3 = x.ngay_td3,
                SoLuongTuDo1 = x.sl_td1,
                SoLuongTuDo2 = x.sl_td2,
                SoLuongTuDo3 = x.sl_td3,
                GhiChuTuDo1 = x.gc_td1,
                GhiChuTuDo2 = x.gc_td2,
                GhiChuTuDo3 = x.gc_td3,
                LaKhachHang = x.kh_yn,
                LaNhaCungCap = x.cc_yn,
                LaNhanVien = x.nv_yn,
                TaiKhoanNganHang = x.TK_NH,
                DienThoaiDiDong = x.DT_DD,
                ThongTinSoNha = x.TT_SONHA,
                MaPhuong = x.MA_PHUONG,
                MaTinh = x.MA_TINH,
                MaQuan = x.MA_QUAN,
                CheckSync = x.CHECK_SYNC,
                //.............
            }).ToPagedSearchResult(criteria);
        }
    }
}
