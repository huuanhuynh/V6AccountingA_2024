using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.IntermediateProduct
{
    /// <summary>
    /// Sản phẩm trung gian
    /// </summary>
   public class IntermediateProductListItem : V6Model
    {
        /// <summary>
        /// Column: ma_vttg
        /// Description: 
        /// </summary>
        public string MaVatTutg { get; set; }
        /// <summary>
        /// Column: part_no
        /// Description: 
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// Column: ten_vttg
        /// Description: 
        /// </summary>
        public string TenVatTutg { get; set; }
        /// <summary>
        /// Column: ten_vttg2
        /// Description: 
        /// </summary>
        public string TenVatTutg2 { get; set; }
        /// <summary>
        /// Column: dvt
        /// Description: 
        /// </summary>
        public string DonViTinh { get; set; }
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
        /// Column: tk_cl_vt
        /// Description: 
        /// </summary>
        public string TaiKhoan_cl_vt { get; set; }
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
        public string TaiKhoan_spdd { get; set; }
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
    }
}
