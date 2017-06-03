using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.ViewModels.DanhMucLoHang
{
    /// <summary>
    /// Danh mục lô hàng
    /// 
    /// </summary>
    public class MerchandiseListItem : V6Model
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
        /// <summary>
        /// Column: ma_kh
        /// Description: 
        /// </summary>
        public string MaKhachHang { get; set; }
    }
}
