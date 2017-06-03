using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class Location : V6Model
    {
        /// <summary>
        /// Column: ma_kho
        /// Description: 
        /// </summary>
        public string MaKho { get; set; }
        /// <summary>
        /// Column: ma_vitri
        /// Description: 
        /// </summary>
        public string MaViTri { get; set; }
        /// <summary>
        /// Column: ten_vitri
        /// Description: 
        /// </summary>
        public string TenViTri { get; set; }
        /// <summary>
        /// Column: ten_vitri2
        /// Description: 
        /// </summary>
        public string TenViTri2 { get; set; }
        /// <summary>
        /// Column: stt_ntxt
        /// Description: 
        /// </summary>
        public byte? STT_NhapTruocXuatTruoc { get; set; }
        /// <summary>
        /// Column: ma_loai
        /// Description: 
        /// </summary>
        public string MaLoai { get; set; }
        /// <summary>
        /// Column: kieu_nhap
        /// Description: 
        /// </summary>
        public decimal? KieuNhap { get; set; }
        /// <summary>
        /// Column: kieu_xuat
        /// Description: 
        /// </summary>
        public decimal? KieuXuat { get; set; }
        /// <summary>
        /// Column: kieu_ban
        /// Description: 
        /// </summary>
        public decimal? KieuBan { get; set; }
        /// <summary>
        /// Column: ma_vt
        /// Description: 
        /// </summary>
        public string MaVatTu { get; set; }
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
        /// Column: ma_td1
        /// Description: 
        /// </summary>
        public string MaTuDo1 { get; set; }
        /// <summary>
        /// Column: ma_td2
        /// Description: 
        /// </summary>
        public string MaTuDo2 { get; set; }
        /// <summary>
        /// Column: ma_td3
        /// Description: 
        /// </summary>
        public string MaTuDo3 { get; set; }
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
        /// Column: CHECK_SYNC
        /// Description: 
        /// </summary>
        public string CheckSync { get; set; }
    }
}
