using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.Material
{
    /// <summary>
    /// Vật tư
    /// </summary>
    public class MaterialListItem : V6Model
    {
        /// <summary>
        /// Column: ma_vt
        /// Description: 
        /// </summary>
        public string MaVatTu { get; set; }
        /// <summary>
        /// Column: part_no
        /// Description: 
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// Column: ten_vt
        /// Description: 
        /// </summary>
        public string TenVatTu { get; set; }
        /// <summary>
        /// Column: ten_vt2
        /// Description: 
        /// </summary>
        public string TenVatTu2 { get; set; }
        /// <summary>
        /// Column: dvt
        /// Description: 
        /// </summary>
        public string DonViTinh { get; set; }
        /// <summary>
        /// Column: dvt1
        /// Description: 
        /// </summary>
        public string DonViTinh1 { get; set; }
        /// <summary>
        /// Column: he_so1
        /// Description: 
        /// </summary>
        public decimal? HeSo1 { get; set; }
        /// <summary>
        /// Column: vt_ton_kho
        /// Description: 
        /// </summary>
        public byte VatTuTonKho { get; set; }
        /// <summary>
        /// Column: gia_ton
        /// Description: 
        /// </summary>
        public byte GiaTon { get; set; }
        /// <summary>
        /// Column: sua_tk_vt
        /// Description: 
        /// </summary>
        public byte? ChoPhepSuaTaiKhoanVatTu { get; set; }
        /// <summary>
        /// Column: tk_cl_vt
        /// Description: 
        /// </summary>
        public string TaiKhoan_CL_VT { get; set; }
        /// <summary>
        /// Column: tk_vt
        /// Description: 
        /// </summary>
        public string TaiKhoanVatTu { get; set; }
        /// <summary>
        /// Column: tk_gv
        /// Description: 
        /// </summary>
        public string TaiKhoanGiaVon { get; set; }
        /// <summary>
        /// Column: tk_dt
        /// Description: 
        /// </summary>
        public string TaiKhoanDoanhThu { get; set; }
        /// <summary>
        /// Column: tk_tl
        /// Description: 
        /// </summary>
        public string TaiKhoanTraLai { get; set; }
        /// <summary>
        /// Column: tk_spdd
        /// Description: 
        /// </summary>
        public string TaiKhoanSanPhamDoDang { get; set; }
        /// <summary>
        /// Column: nh_vt1
        /// Description: 
        /// </summary>
        public string NhomVatTu1 { get; set; }
        /// <summary>
        /// Column: nh_vt2
        /// Description: 
        /// </summary>
        public string NhomVatTu2 { get; set; }
        /// <summary>
        /// Column: nh_vt3
        /// Description: 
        /// </summary>
        public string NhomVatTu3 { get; set; }
        /// <summary>
        /// Column: sl_min
        /// Description: 
        /// </summary>
        public decimal SoLuongToiThieu { get; set; }
        /// <summary>
        /// Column: sl_max
        /// Description: 
        /// </summary>
        public decimal SoLuongToiDa { get; set; }
        /// <summary>
        /// Column: ghi_chu
        /// Description: 
        /// </summary>
        public string GhiChu { get; set; }
        /// <summary>
        /// Column: Loai_vt
        /// Description: 
        /// </summary>
        public string Loai_VatTu { get; set; }
        /// <summary>
        /// Column: Tt_vt
        /// Description: 
        /// </summary>
        public string TinhTrangVatTu { get; set; }
        /// <summary>
        /// Column: Nhieu_dvt
        /// Description: 
        /// </summary>
        public string nhieu_DonViTinh { get; set; }
        /// <summary>
        /// Column: Ma_vitri
        /// Description: 
        /// </summary>
        public string MaViTri { get; set; }
        /// <summary>
        /// Column: Ma_kho
        /// Description: 
        /// </summary>
        public string MaKho { get; set; }
        /// <summary>
        /// Column: Han_sd
        /// Description: 
        /// </summary>
        public decimal? HanSuDung { get; set; }
        /// <summary>
        /// Column: Han_bh
        /// Description: 
        /// </summary>
        public decimal? HanBaoHanh { get; set; }
     
        /// <summary>
        /// Column: MA_VTTG
        /// Description: 
        /// </summary>
        public string MaVatTutg { get; set; }
        /// <summary>
        /// Column: MA_KHTG
        /// Description: 
        /// </summary>
        public string MaKhachHangtg { get; set; }
        /// <summary>
        /// Column: TEN_KHTG
        /// Description: 
        /// </summary>
        public string TenKhachHangTheGioi { get; set; }
        /// <summary>
        /// Column: NH_VT4
        /// Description: 
        /// </summary>
        public string NhomVatTu4 { get; set; }
        /// <summary>
        /// Column: NH_VT5
        /// Description: 
        /// </summary>
        public string NhomVatTu5 { get; set; }
        /// <summary>
        /// Column: NH_VT6
        /// Description: 
        /// </summary>
        public string NhomVatTu6 { get; set; }
        /// <summary>
        /// Column: NH_VT7
        /// Description: 
        /// </summary>
        public string NhomVatTu7 { get; set; }
        /// <summary>
        /// Column: NH_VT8
        /// Description: 
        /// </summary>
        public string NhomVatTu8 { get; set; }
    }
}
