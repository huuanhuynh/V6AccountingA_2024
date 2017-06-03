using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class CongCu : V6Model
    {
        /// <summary>
        /// Column: so_the_cc
        /// Description: 
        /// </summary>
        public string SoTheCongCu { get; set; }
        /// <summary>
        /// Column: ten_cc
        /// Description: 
        /// </summary>
        public string TenCongCu { get; set; }
        /// <summary>
        /// Column: so_hieu_cc
        /// Description: 
        /// </summary>
        public string SoHieuCongCu { get; set; }
        /// <summary>
        /// Column: ten_cc2
        /// Description: 
        /// </summary>
        public string TenCongCu2 { get; set; }
        /// <summary>
        /// Column: ma_dvcs
        /// Description: 
        /// </summary>
        public string MaDonViCoSo { get; set; }
        /// <summary>
        /// Column: dvt
        /// Description: 
        /// </summary>
        public string DonViTinh { get; set; }
        /// <summary>
        /// Column: nuoc_sx
        /// Description: 
        /// </summary>
        public string NuocSanXuat { get; set; }
        /// <summary>
        /// Column: nam_sx
        /// Description: 
        /// </summary>
        public decimal? NamSanXuat { get; set; }
        /// <summary>
        /// Column: nh_cc1
        /// Description: 
        /// </summary>
        public string NhomCongCu1 { get; set; }
        /// <summary>
        /// Column: nh_cc2
        /// Description: 
        /// </summary>
        public string NhomCongCu2 { get; set; }
        /// <summary>
        /// Column: nh_cc3
        /// Description: 
        /// </summary>
        public string NhomCongCu3 { get; set; }
        /// <summary>
        /// Column: tinh_pb
        /// Description: 
        /// </summary>
        public decimal? TinhPhanBo { get; set; }
        /// <summary>
        /// Column: ma_tg_cc
        /// Description: 
        /// </summary>
        public string MaTangGiamCongCu { get; set; }
        /// <summary>
        /// Column: ngay_mua
        /// Description: 
        /// </summary>
        public DateTime? NgayMua { get; set; }
        /// <summary>
        /// Column: ngay_pb0
        /// Description: 
        /// </summary>
        public DateTime? NgayPhanBo { get; set; }
        /// <summary>
        /// Column: so_ky_pb
        /// Description: 
        /// </summary>
        public decimal? SoKyPhanBo { get; set; }
        /// <summary>
        /// Column: tien_cl
        /// Description: 
        /// </summary>
        public decimal? Tien_CL { get; set; }
        /// <summary>
        /// Column: ngay_pb1
        /// Description: 
        /// </summary>
        public DateTime? NgayPhanBo1 { get; set; }
        /// <summary>
        /// Column: kieu_pb
        /// Description: 
        /// </summary>
        public decimal? KieuPhanBo { get; set; }
        /// <summary>
        /// Column: ty_le_pb
        /// Description: 
        /// </summary>
        public decimal? TyLePhanBo { get; set; }
        /// <summary>
        /// Column: tong_sl
        /// Description: 
        /// </summary>
        public decimal TongSoLuong { get; set; }
        /// <summary>
        /// Column: loai_pb
        /// Description: 
        /// </summary>
        public decimal? LoaiPhanBo { get; set; }
        /// <summary>
        /// Column: ma_bp
        /// Description: 
        /// </summary>
        public string MaBoPhan { get; set; }
        /// <summary>
        /// Column: tk_cc
        /// Description: 
        /// </summary>
        public string TaiKhoanCongCu { get; set; }
        /// <summary>
        /// Column: tk_pb
        /// Description: 
        /// </summary>
        public string TaiKhoanPhanBo { get; set; }
        /// <summary>
        /// Column: tk_cp
        /// Description: 
        /// </summary>
        public string tk_cp { get; set; }
        /// <summary>
        /// Column: cong_suat
        /// Description: 
        /// </summary>
        public string CongSuat { get; set; }
        /// <summary>
        /// Column: loai_cc
        /// Description: 
        /// </summary>
        public string LoaiCongCu { get; set; }
        /// <summary>
        /// Column: ts_kt
        /// Description: 
        /// </summary>
        public string ts_kt { get; set; }
        /// <summary>
        /// Column: ngay_dc
        /// Description: 
        /// </summary>
        public DateTime? Ngay_DC { get; set; }
        /// <summary>
        /// Column: ly_do_dc
        /// Description: 
        /// </summary>
        public string LyDo_DC { get; set; }
        /// <summary>
        /// Column: ngay_giam
        /// Description: 
        /// </summary>
        public DateTime? NgayGiam { get; set; }
        /// <summary>
        /// Column: ma_giam_cc
        /// Description: 
        /// </summary>
        public string MaGiamCongCu { get; set; }
        /// <summary>
        /// Column: ly_do_giam
        /// Description: 
        /// </summary>
        public string ly_do_giam { get; set; }
        /// <summary>
        /// Column: so_luong
        /// Description: 
        /// </summary>
        public decimal? SoLuong { get; set; }
        /// <summary>
        /// Column: ghi_chu
        /// Description: 
        /// </summary>
        public string GhiChu { get; set; }
        /// <summary>
        /// Column: ngay_dvsd
        /// Description: 
        /// </summary>
        public DateTime? ngay_dvsd { get; set; }
        /// <summary>
        /// Column: so_ct
        /// Description: 
        /// </summary>
        public string SoChungTu { get; set; }
        /// <summary>
        /// Column: date0
        /// Description: 
        /// </summary>
        public DateTime NgayNhap { get; set; }
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
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: date2
        /// Description: 
        /// </summary>
        public DateTime NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte NguoiSua { get; set; }
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
        /// Column: ma_qg
        /// Description: 
        /// </summary>
        public string MaQuocGia { get; set; }
        /// <summary>
        /// Column: loai_cc0
        /// Description: 
        /// </summary>
        public string LoaiCongCu0 { get; set; }
        /// <summary>
        /// Column: thuoc_nhom
        /// Description: 
        /// </summary>
        public string thuoc_nhom { get; set; }
        /// <summary>
        /// Column: trang_thai
        /// Description: 
        /// </summary>
        public string Trang_Thai { get; set; }
    }
}
