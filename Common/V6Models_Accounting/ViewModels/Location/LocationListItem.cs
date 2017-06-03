using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Location
{
    /// <summary>
    /// Vị trí
    /// </summary>
    public class LocationListItem : V6Model
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
        /// Column: date0
        /// Description: 
        /// </summary>
        public DateTime NgayKhoiTao { get; set; }
        /// <summary>
        /// Column: time0
        /// Description: 
        /// </summary>
        public string GioKhoiTao { get; set; }
        /// <summary>
        /// Column: user_id0
        /// Description: 
        /// </summary>
        public byte NguoiNhap { get; set; }
        /// <summary>
        /// Column: date2
        /// Description: 
        /// </summary>
        public DateTime? NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte? NguoiSua { get; set; }
    }
}
