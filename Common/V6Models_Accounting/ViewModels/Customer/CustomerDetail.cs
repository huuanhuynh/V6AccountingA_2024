using System;
using System.ComponentModel.DataAnnotations;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Customer
{
    public class CustomerDetail : V6Model
    {        
        /// <summary>
        ///     Column: ma_kh
        ///     Description: 
        /// </summary>
        [MaxLength(16)]
        [Required]
        public string MaKhachHang { get; set; }

        /// <summary>
        ///     Column: ten_kh
        ///     Description: 
        /// </summary>
        [MaxLength(128)]
        [Required]
        public string TenKhachHang { get; set; }

        /// <summary>
        ///     Column: ten_kh2
        ///     Description: 
        /// </summary>
        [MaxLength(128)]
        [Required]
        public string TenTiengAnh { get; set; }

        /// <summary>
        ///     Column: ma_so_thue
        ///     Description: 
        /// </summary>
        [MaxLength(18)]
        [Required]
        public string MaSoThue { get; set; }

        /// <summary>
        ///     Column: dia_chi
        ///     Description: 
        /// </summary>
        [MaxLength(128)]
        public string DiaChi { get; set; }

        /// <summary>
        ///     Column: dien_thoai
        ///     Description: 
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string DienThoai { get; set; }

        /// <summary>
        ///     Column: fax
        ///     Description: 
        /// </summary>
        [Required]
        [MaxLength(16)]
        public string Fax { get; set; }

        /// <summary>
        ///     Column: e_mail
        ///     Description: 
        /// </summary>
        [Required]
        [MaxLength(16)]
        public string Email { get; set; }

        /// <summary>
        ///     Column: home_page
        ///     Description: 
        /// </summary>
        public string TrangChu { get; set; }

        /// <summary>
        ///     Column: doi_tac
        ///     Description: 
        /// </summary>
        public string DoiTac { get; set; }

        /// <summary>
        ///     Column: ong_ba
        ///     Description: 
        /// </summary>
        public string OngBa { get; set; }

        /// <summary>
        ///     Column: ten_bp
        ///     Description: 
        /// </summary>
        public string TenBoPhan { get; set; }

        /// <summary>
        ///     Column: ngan_hang
        ///     Description: 
        /// </summary>
        public string NganHang { get; set; }

        /// <summary>
        ///     Column: ghi_chu
        ///     Description: 
        /// </summary>
        public string GhiChu { get; set; }

        /// <summary>
        ///     Column: han_tt
        ///     Description: 
        /// </summary>
        public byte HanThanhToan { get; set; }

        /// <summary>
        ///     Column: tk
        ///     Description: 
        /// </summary>
        public string TaiKhoan { get; set; }

        /// <summary>
        ///     Column: nh_kh1
        ///     Description: 
        /// </summary>
        public string NhomKhachHang1 { get; set; }

        /// <summary>
        ///     Column: nh_kh2
        ///     Description: 
        /// </summary>
        public string NhomKhachHang2 { get; set; }

        /// <summary>
        ///     Column: nh_kh3
        ///     Description: 
        /// </summary>
        public string NhomKhachHang3 { get; set; }

        /// <summary>
        ///     Column: du_nt13
        ///     Description: 
        /// </summary>
        public decimal? DuNgoaiTe13 { get; set; }

        /// <summary>
        ///     Column: du13
        ///     Description: 
        /// </summary>
        public decimal? Du13 { get; set; }

        /// <summary>
        ///     Column: t_tien_cn
        ///     Description: 
        /// </summary>
        public decimal? TongTienCongNo { get; set; }

        /// <summary>
        ///     Column: t_tien_hd
        ///     Description: 
        /// </summary>
        public decimal? TongTienHoaDon { get; set; }

        /// <summary>
        ///     Column: Ma_httt
        ///     Description: 
        /// </summary>
        public string MaHinhThucThanhToan { get; set; }

        /// <summary>
        ///     Column: Nh_kh9
        ///     Description: 
        /// </summary>
        public string NhomKhachHang9 { get; set; }

        /// <summary>
        ///     Column: Ma_snvien
        ///     Description: 
        /// </summary>
        public string MaSoNhanVien { get; set; }

        /// <summary>
        ///     Column: Ngay_gh
        ///     Description: 
        /// </summary>
        public DateTime? NgayGioiHan { get; set; }

        /// <summary>
        ///     Column: date0
        ///     Description: 
        /// </summary>
        public DateTime NgayNhap { get; set; }

        /// <summary>
        ///     Column: status
        ///     Description: 
        /// </summary>
        public string TrangThai { get; set; }

        /// <summary>
        ///     Column: ma_td1
        ///     Description: 
        /// </summary>
        public string MaTuDo1 { get; set; }

        /// <summary>
        ///     Column: ma_td2
        ///     Description: 
        /// </summary>
        public string MaTuDo2 { get; set; }

        /// <summary>
        ///     Column: ma_td3
        ///     Description: 
        /// </summary>
        public string MaTuDo3 { get; set; }

        /// <summary>
        ///     Column: ngay_td1
        ///     Description: 
        /// </summary>
        public DateTime? NgayTuDo1 { get; set; }

        /// <summary>
        ///     Column: ngay_td2
        ///     Description: 
        /// </summary>
        public DateTime? NgayTuDo2 { get; set; }

        /// <summary>
        ///     Column: ngay_td3
        ///     Description: 
        /// </summary>
        public DateTime? NgayTuDo3 { get; set; }

        /// <summary>
        ///     Column: sl_td1
        ///     Description: 
        /// </summary>
        public decimal? SoLuongTuDo1 { get; set; }

        /// <summary>
        ///     Column: sl_td2
        ///     Description: 
        /// </summary>
        public decimal? SoLuongTuDo2 { get; set; }

        /// <summary>
        ///     Column: sl_td3
        ///     Description: 
        /// </summary>
        public decimal? SoLuongTuDo3 { get; set; }

        /// <summary>
        ///     Column: gc_td1
        ///     Description: 
        /// </summary>
        public string GhiChuTuDo1 { get; set; }

        /// <summary>
        ///     Column: gc_td2
        ///     Description: 
        /// </summary>
        public string GhiChuTuDo2 { get; set; }

        /// <summary>
        ///     Column: gc_td3
        ///     Description: 
        /// </summary>
        public string GhiChuTuDo3 { get; set; }

        /// <summary>
        ///     Column: kh_yn
        ///     Description: 
        /// </summary>
        public byte? LaKhachHang { get; set; }

        /// <summary>
        ///     Column: cc_yn
        ///     Description: 
        /// </summary>
        public byte? LaNhaCungCap { get; set; }

        /// <summary>
        ///     Column: nv_yn
        ///     Description: 
        /// </summary>
        public byte? LaNhanVien { get; set; }

        /// <summary>
        ///     Column: TK_NH
        ///     Description: 
        /// </summary>
        public string TaiKhoanNganHang { get; set; }

        /// <summary>
        ///     Column: DT_DD
        ///     Description: 
        /// </summary>
        public string DienThoaiDiDong { get; set; }

        /// <summary>
        ///     Column: TT_SONHA
        ///     Description: 
        /// </summary>
        public string ThongTinSoNha { get; set; }

        /// <summary>
        ///     Column: MA_PHUONG
        ///     Description: 
        /// </summary>
        public string MaPhuong { get; set; }

        /// <summary>
        ///     Column: MA_TINH
        ///     Description: 
        /// </summary>
        public string MaTinh { get; set; }

        /// <summary>
        ///     Column: MA_QUAN
        ///     Description: 
        /// </summary>
        public string MaQuan { get; set; }

        /// <summary>
        ///     Column: CHECK_SYNC
        ///     Description: 
        /// </summary>
        public string CheckSync { get; set; }
    }
}
