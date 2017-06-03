using System;
using System.ComponentModel.DataAnnotations;
using V6Soft.Models.Core;
using System.Collections.Generic;


namespace V6Soft.Models.Accounting.DTO
{
    public class Customer : V6Model
    {
        [Key]
        public string MaKhachHang { get; set; }

        public string TenKhachHang { get; set; }

        public string TenTiengAnh { get; set; }

        public string MaSoThue { get; set; }

        public string DiaChi { get; set; }

        public string DienThoai { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }
        public string TrangChu { get; set; }
        public string DoiTac { get; set; }
        public string OngBa { get; set; }
        public string TenBoPhan { get; set; }
        public string NganHang { get; set; }
        public string GhiChu { get; set; }
        public decimal? HanThanhToan { get; set; }
        public string TaiKhoan { get; set; }
        public string NhomKhachHang1 { get; set; }
        public string NhomKhachHang2 { get; set; }
        public string NhomKhachHang3 { get; set; }
        public decimal? DuNgoaiTe13 { get; set; }
        public decimal? Du13 { get; set; }
        public decimal? TongTienCongNo { get; set; }
        public decimal? TongTienHoaDon { get; set; }
        public string MaHinhThucThanhToan { get; set; }
        public string NhomKhachHang9 { get; set; }
        public string MaSoNhanVien { get; set; }
        public DateTime? NgayGioiHan { get; set; }
        public string TrangThai { get; set; }
        public string MaTuDo1 { get; set; }
        public string MaTuDo2 { get; set; }
        public string MaTuDo3 { get; set; }
        public DateTime? NgayTuDo1 { get; set; }
        public DateTime? NgayTuDo2 { get; set; }
        public DateTime? NgayTuDo3 { get; set; }
        public decimal? SoLuongTuDo1 { get; set; }
        public decimal? SoLuongTuDo2 { get; set; }
        public decimal? SoLuongTuDo3 { get; set; }
        public string GhiChuTuDo1 { get; set; }
        public string GhiChuTuDo2 { get; set; }
        public string GhiChuTuDo3 { get; set; }
        public decimal? LaKhachHang { get; set; }
        public decimal? LaNhaCungCap { get; set; }
        public decimal? LaNhanVien { get; set; }
        public string TaiKhoanNganHang { get; set; }

        //[Column("DT_DD")]
        //[MaxLength(20)]
        //public string DienThoaiDiDong { get; set; }

        //[Column("TT_SONHA")]
        //[MaxLength(100)]
        //public string ThongTinSoNha { get; set; }

        //[Column("MA_PHUONG")]
        //[MaxLength(16)]
        //public string MaPhuong { get; set; }

        //[Column("MA_TINH")]
        //[MaxLength(16)]
        //public string MaTinh { get; set; }

        //[Column("MA_QUAN")]
        //[MaxLength(16)]
        //public string MaQuan { get; set; }

        //[Column("CHECK_SYNC")]
        //[MaxLength(100)]
        //public string CheckSync { get; set; }

        public Guid UID { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
