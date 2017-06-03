using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.DanhMucLoHang
{
    public class MerchandiseDetail : V6Model
    {
        /// <summary>
        /// Column: ma_vt
        /// Description: 
        /// </summary>
        public string MaVatTu { get; set; }
        /// <summary>
        /// Column: ma_lo
        /// Description: 
        /// </summary>
        public string MaLo { get; set; }
        /// <summary>
        /// Column: ten_lo
        /// Description: 
        /// </summary>
        public string TenLo { get; set; }
        /// <summary>
        /// Column: ten_lo2
        /// Description: 
        /// </summary>
        public string TenLo2 { get; set; }
        /// <summary>
        /// Column: ngay_nhap
        /// Description: 
        /// </summary>
        public DateTime? NgayNhap { get; set; }
        /// <summary>
        /// Column: ngay_sx
        /// Description: 
        /// </summary>
        public DateTime? NgaySanXuat { get; set; }
        /// <summary>
        /// Column: ngay_bdsd
        /// Description: 
        /// </summary>
        public DateTime? Ngay_BD_SuDung { get; set; }
        /// <summary>
        /// Column: ngay_kt
        /// Description: 
        /// </summary>
        public DateTime? NgayKiemTra { get; set; }
        /// <summary>
        /// Column: ngay_hhsd
        /// Description: 
        /// </summary>
        public DateTime? NgayHetHanSuDung { get; set; }
        /// <summary>
        /// Column: ngay_hhbh
        /// Description: 
        /// </summary>
        public DateTime? NgayHetHanBaoHanh { get; set; }
        /// <summary>
        /// Column: ma_vt2
        /// Description: 
        /// </summary>
        public string MaVatTu2 { get; set; }
        /// <summary>
        /// Column: sl_nhap
        /// Description: 
        /// </summary>
        public decimal SoLuongNhap { get; set; }
        /// <summary>
        /// Column: sl_xuat
        /// Description: 
        /// </summary>
        public decimal SoLuongXuat { get; set; }
        /// <summary>
        /// Column: ghi_chu
        /// Description: 
        /// </summary>
        public string GhiChu { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: sl_td1
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo1 { get; set; }
        /// <summary>
        /// Column: sl_td2
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo2 { get; set; }
        /// <summary>
        /// Column: sl_td3
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo3 { get; set; }
        /// <summary>
        /// Column: ngay_td1
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo1 { get; set; }
        /// <summary>
        /// Column: ngay_td2
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo2 { get; set; }
        /// <summary>
        /// Column: ngay_td3
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo3 { get; set; }
        /// <summary>
        /// Column: gc_td1
        /// Description: 
        /// </summary>
        public string GhiChuTuDo1 { get; set; }
        /// <summary>
        /// Column: gc_td2
        /// Description: 
        /// </summary>
        public string GhiChuTuDo2 { get; set; }
        /// <summary>
        /// Column: gc_td3
        /// Description: 
        /// </summary>
        public string GhiChuTuDo3 { get; set; }
        /// <summary>
        /// Column: SO_LOSX
        /// Description: 
        /// </summary>
        public string SoLoSanXuat { get; set; }
        /// <summary>
        /// Column: SO_LODK
        /// Description: 
        /// </summary>
        public string SoLoDangKy { get; set; }
        /// <summary>
        /// Column: ma_kh
        /// Description: 
        /// </summary>
        public string MaKhachHang { get; set; }
    }
}
