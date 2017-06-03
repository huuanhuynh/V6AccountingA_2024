using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class Shipment : V6Model
    {
        /// <summary>
        /// Column: ma_vc
        /// Description: 
        /// </summary>
        public string MaVanChuyen { get; set; }
        /// <summary>
        /// Column: ten_vc
        /// Description: 
        /// </summary>
        public string TenVanChuyen { get; set; }
        /// <summary>
        /// Column: ten_vc2
        /// Description: 
        /// </summary>
        public string TenVanChuyen2 { get; set; }
        /// <summary>
        /// Column: loai_vc
        /// Description: 
        /// </summary>
        public string LoaiVanChuyen { get; set; }
        /// <summary>
        /// Column: ong_ba
        /// Description: 
        /// </summary>
        public string OngBa { get; set; }
        /// <summary>
        /// Column: height
        /// Description: 
        /// </summary>
        public decimal? ChieuCao { get; set; }
        /// <summary>
        /// Column: length
        /// Description: 
        /// </summary>
        public decimal? ChieuDai { get; set; }
        /// <summary>
        /// Column: volume
        /// Description: 
        /// </summary>
        public decimal? TheTich { get; set; }
        /// <summary>
        /// Column: weight
        /// Description: 
        /// </summary>
        public decimal? TrongLuong { get; set; }
        /// <summary>
        /// Column: width
        /// Description: 
        /// </summary>
        public decimal? ChieuNgang { get; set; }
        /// <summary>
        /// Column: dvtheight
        /// Description: 
        /// </summary>
        public string DonViTinhChieuCao { get; set; }
        /// <summary>
        /// Column: dvtlength
        /// Description: 
        /// </summary>
        public string DonViTinhChieuDai { get; set; }
        /// <summary>
        /// Column: dvtvolume
        /// Description: 
        /// </summary>
        public string DonViTinhTheTich { get; set; }
        /// <summary>
        /// Column: dvtweight
        /// Description: 
        /// </summary>
        public string DonViTinhTrongLuong { get; set; }
        /// <summary>
        /// Column: dvtwidth
        /// Description: 
        /// </summary>
        public string DonViTinhChieuNgang { get; set; }
        /// <summary>
        /// Column: tg_xephang
        /// Description: 
        /// </summary>
        public decimal? ThoiGianXepHang { get; set; }
        /// <summary>
        /// Column: tg_dohang
        /// Description: Thời gian dở hàng
        /// </summary>
        public decimal? ThoiGianDoHang { get; set; }
        /// <summary>
        /// Column: dvt_xep
        /// Description: 
        /// </summary>
        public string DonViTinh_Xep { get; set; }
        /// <summary>
        /// Column: dvt_do
        /// Description: Đơn vị tính - dở (hàng)
        /// </summary>
        public string DonViTinh_Do { get; set; }
        /// <summary>
        /// Column: bien_so
        /// Description: 
        /// </summary>
        public string BienSo { get; set; }
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
    }
}
